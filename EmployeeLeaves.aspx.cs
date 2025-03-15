
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Viftech;

public partial class EmployeeLeaves : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    private int LeavesAppID
    {
        get
        {
            return vt_Common.CheckInt(Request.QueryString["LOA"].Trim());
        }
    }

    private string LeaveViewID
    {
        get
        {
            return Request.QueryString["v2s"].Trim();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadData();
                int RoleID;
                RoleID = Convert.ToInt32(Session["RoleId"]);
                int lid = Convert.ToInt32(lblid.Value);
                var query = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID.Equals(lid)).FirstOrDefault();
                //if (query != null)
                //{
                //    if (query.isApproved == true && RoleID == 2)
                //    {
                //        MsgBox.Show(Page, MsgBox.success, "", "This Application Has been approved!");
                //        btnSaveLeaveApproval.Visible = false;
                //        lblapprove.Text = "Application Has been approved!";
                //        DdlHrApproval.Enabled = false;
                            
                //    }
                //    if (query.isRejected == true && RoleID != 2)
                //    {
                //        MsgBox.Show(Page, MsgBox.danger, "", "This Application Has been Rejected!");
                //        btnSaveLeaveApproval.Visible = false;
                //       lblapprove.Text = "Application Has been Rejected!";
                //    }

                //}
                
               
                if (RoleID == 2)
                {
                    divHR.Visible = true;
                }
            }
        }
    }
    #region Control Event

    protected void btnSaveLeaveApproval_Click(object sender, EventArgs e)
    {
        try
        {
            int RoleID;
            RoleID = Convert.ToInt32(Session["RoleId"]);
            if (RoleID == 2)
            {
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    vt_tbl_LeaveApplication leaves = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID == LeavesAppID).FirstOrDefault();
                    List<vt_tbl_LeaveApplicationDates> days = db.vt_tbl_LeaveApplicationDates.Where(x => x.LeaveAppID == LeavesAppID).ToList();
                    vt_tbl_Attendance att = db.vt_tbl_Attendance
                    .Where(x => DateTime.Parse(x.InTime).Date == DateTime.UtcNow.Date
                                && x.EmployeeID == leaves.EnrollId)
                    .FirstOrDefault();


                    if (att ==null)
                    {

                        leaves.Comments = TxtBxComments.Text;
                        leaves.isApproved = DdlHrApproval.SelectedValue == "1" ? false : true;



                        // leaves.ManagerID = ((EMS_Session)Session["EMS_Session"]).Employee.EmployeeID;

                        //leaves.isRejected = leaves.isApproved == true ? false : true;
                        leaves.isRejected = leaves.isApproved == true ? false : true;
                        db.Entry(leaves).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        if (leaves.isRejected == true)
                        {
                            foreach (var item in days)
                            {
                                item.IsApproved = false;
                                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        //bool isApproved = ddldecision.SelectedValue == "1" ? false : true;
                        bool isApproved = DdlHrApproval.SelectedValue == "1" ? false : true;

                        if (isApproved)
                        {
                            bool firstDayOfLeave = true;
                            vt_tbl_LeaveApplication LeaveApp = db.vt_tbl_LeaveApplication.Where(y => y.LeaveApplicationID == leaves.LeaveApplicationID).FirstOrDefault();
                            DateTime? dtDate = new DateTime();
                            if (LeaveApp != null)
                            {
                                var NumberOfDays = (vt_Common.CheckDateTime(LeaveApp.ToDate) - vt_Common.CheckDateTime(LeaveApp.FromDate)).TotalDays;

                                for (int i = 1; i <= NumberOfDays + 1; i++)
                                {
                                    //vt_tbl_LeaveApplicationDates days = new vt_tbl_LeaveApplicationDates();


                                    foreach (var item in days)
                                    {
                                        item.CompanyID = LeaveApp.CompanyID;
                                        item.EnrollId = vt_Common.CheckString(LeaveApp.EnrollId);
                                        item.LeaveID = vt_Common.CheckInt(LeaveApp.LeaveID);
                                        item.LeaveAppID = LeaveApp.LeaveApplicationID;
                                        item.IsApproved = true;
                                        if (firstDayOfLeave)
                                        {
                                            //      item.Date = vt_Common.CheckDateTime(LeaveApp.FromDate);
                                        }
                                        else
                                        {
                                            //  item.Date = vt_Common.CheckDateTime(LeaveApp.FromDate).AddDays(i - 1);
                                        }
                                        dtDate = item.Date;
                                        firstDayOfLeave = false;
                                        // db.vt_tbl_LeaveApplicationDates.Add(days);
                                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                                        db.SaveChanges();
                                    }


                                    var empAttandenceRecord = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.EmployeeID == LeaveApp.EnrollId);
                                    // && System.Data.Objects.EntityFunctions.TruncateTime(o.EntryDate) == System.Data.Objects.EntityFunctions.TruncateTime(dtDate)
                                    if (empAttandenceRecord != null)
                                    {
                                        empAttandenceRecord.InTime = "";
                                        empAttandenceRecord.OutTime = "";
                                        empAttandenceRecord.Status = false;
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        MsgBox.Show(Page, MsgBox.success, "Decision on Employee Leaves", "Successfully Done");
                        vt_Common.ReloadJS(this.Page, "setInterval(function(){  window.location.href = 'EmpLeaveAproval.aspx'; }, 3000);");


                    }

                    else
                    {
                        vt_Common.ReloadJS(this.Page, "showMessage('Approval or rejection is not permitted as the employee has already checked in today');");
                        return;

                    }
 
                }
            }
            else
            {
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    vt_tbl_LeaveApplication leaves = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID == LeavesAppID).FirstOrDefault();
                    // leaves.LeaveApplicationID = LeavesAppID;
                    //  vt_tbl_Attendance att = db.vt_tbl_Attendance
                    //.Where(x => DateTime.Parse(x.InTime).Date == DateTime.UtcNow
                    //            && x.EmployeeID == leaves.EnrollId)
                    //.FirstOrDefault();


                    var attendances = db.vt_tbl_Attendance
                 .Where(x => x.EmployeeID == leaves.EnrollId)
                 .ToList(); // Pehle List me le aayein, taki ab LINQ ki filtering apply ho sake

                    // Ab TryParse lagao safely
                    var att = attendances
                        .FirstOrDefault(x =>
                        {
                            DateTime inTime;
                            return DateTime.TryParse(x.InTime, out inTime) && inTime.Date == DateTime.UtcNow.Date;
                        });



                    if (att == null)
                    {
                        leaves.Comments = TxtBxComments.Text;
                    //leaves.isApproved = ddldecision.SelectedValue == "1" ? false : true;
                    //leaves.isApproved = null;
                    // leaves.ManagerID = ((EMS_Session)Session["EMS_Session"]).Employee.EmployeeID;
                    if(ddldecision.SelectedValue == "1")
                    {
                        leaves.isManagerMark = false;
                        leaves.AppliedToHR = false;
                        leaves.isRejected = true;
                    }
                    else
                    {
                        leaves.isManagerMark = true;
                        leaves.AppliedToHR = true;
                        leaves.isRejected = false;
                    }
                    //leaves.isRejected = leaves.isApproved == true ? false : true;
                    //leaves.isRejected = leaves.isApproved == true ? false : true;
                    
                    


                    db.Entry(leaves).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    //bool isApproved = ddldecision.SelectedValue == "1" ? false : true;
                    bool isApproved = ddldecision.SelectedValue == "1" ? false : false;

                    if (isApproved)
                    {
                        bool firstDayOfLeave = true;
                        vt_tbl_LeaveApplication LeaveApp = db.vt_tbl_LeaveApplication.Where(y => y.LeaveApplicationID == leaves.LeaveApplicationID).FirstOrDefault();

                        if (LeaveApp != null)
                        {
                            var NumberOfDays = (vt_Common.CheckDateTime(LeaveApp.ToDate) - vt_Common.CheckDateTime(LeaveApp.FromDate)).TotalDays;

                            for (int i = 1; i <= NumberOfDays + 1; i++)
                            {
                                vt_tbl_LeaveApplicationDates days = new vt_tbl_LeaveApplicationDates();
                                days.CompanyID = LeaveApp.CompanyID;
                                days.EnrollId  = vt_Common.CheckString(LeaveApp.EnrollId);
                                days.LeaveID   = vt_Common.CheckInt(LeaveApp.LeaveID);
                                days.LeaveAppID = LeaveApp.LeaveApplicationID;
                                if (firstDayOfLeave)
                                {
                                    days.Date = vt_Common.CheckDateTime(LeaveApp.FromDate);
                                }
                                else
                                {
                                    days.Date = vt_Common.CheckDateTime(LeaveApp.FromDate).AddDays(i - 1);
                                }
                                DateTime? dtDate = days.Date;
                                firstDayOfLeave = false;
                                db.vt_tbl_LeaveApplicationDates.Add(days);
                                db.SaveChanges();
                                var empAttandenceRecord = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.EmployeeID == LeaveApp.EnrollId && System.Data.Objects.EntityFunctions.TruncateTime(o.EntryDate) == System.Data.Objects.EntityFunctions.TruncateTime(dtDate));
                                if (empAttandenceRecord != null)
                                {
                                    empAttandenceRecord.InTime = "";
                                    empAttandenceRecord.OutTime = "";
                                    empAttandenceRecord.Status = false;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }

                    MsgBox.Show(Page, MsgBox.success, "Decision on Employee Leaves", "Successfully Saved");

                    Response.Redirect("EmpLeaveAproval.aspx");
                    }

                    else
                    {
                        vt_Common.ReloadJS(this.Page, "showMessage('Approval or rejection is not permitted as the employee has already checked in today');");
                        return;

                    }
                }
            }
        }
        catch (Exception ex)
        {
          //  MsgBox.Show(Page, MsgBox.danger, "Error", "Request not Approved");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
        Response.Redirect("EmpLeaveAproval.aspx");
    }
    #endregion
    #region Healper Method    
    void LoadData()
    {
        if (LeavesAppID <= 0 || string.IsNullOrEmpty(LeaveViewID))
        {
            Response.Redirect("EmpLeaveAproval.aspx");
        }
        int RoleID;
        RoleID = Convert.ToInt32(Session["RoleId"]);
        if (LeavesAppID > 0)
        {
            //if (Session["EMS_Session"] != null)
            //{
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                var getLeavesDetail = (from L in db.vt_tbl_LeaveApplication
                                       join C in db.vt_tbl_Company on L.CompanyID equals C.CompanyID
                                       join E in db.vt_tbl_Employee on L.EnrollId equals E.EmployeeID
                                       where L.LeaveApplicationID == LeavesAppID
                                       select new { C.CompanyName,E.RoleID,  E.EmployeeName, L.Date, L.ToDate, L.FromDate, L.TotalLeave, L.Reason, L.isApproved, L.Comments, L.isManagerMark,L.isRejected }).FirstOrDefault();
                lblid.Value = LeavesAppID.ToString();
                LblCompanyName.Text = getLeavesDetail.CompanyName;
                LblEmployeeName.Text = getLeavesDetail.EmployeeName;
                LblOnApplyLeaves.Text = String.Format("{0:dd/MM/yyyy}", getLeavesDetail.Date);
                LblTotalNoLeaves.Text = getLeavesDetail.TotalLeave;
                LblLeavesFromDate.Text = String.Format("{0:dd/MM/yyyy}", getLeavesDetail.FromDate);
                LblLeavesTodate.Text = String.Format("{0:dd/MM/yyyy}", getLeavesDetail.ToDate);
                TxtBxReasonOfLeaves.Text = getLeavesDetail.Reason;
                //ddldecision.SelectedValue = getLeavesDetail.isApproved;



                if (getLeavesDetail.isManagerMark == false && RoleID == 2)
                {
                    DdlHrApproval.Enabled = false;
                    ddldecision.Enabled = false;
                    btnSaveLeaveApproval.Enabled = false;

                }
                else
                {
                    if (getLeavesDetail.isManagerMark == true || LeaveViewID == "z")
                    {
                        TxtBxComments.Text = getLeavesDetail.Comments;
                        if (ddldecision.SelectedValue != "0")
                        {

                            ddldecision.SelectedValue = getLeavesDetail.isManagerMark == false ? "1" : "2";
                        }
                        TxtBxComments.Enabled = false;
                        ddldecision.Enabled = false;

                        btnSaveLeaveApproval.Enabled = true;
                    }

                    if (getLeavesDetail.isApproved == true)
                    {
                        ddldecision.SelectedValue = getLeavesDetail.isManagerMark == false ? "1" : "2";
                        DdlHrApproval.SelectedValue = getLeavesDetail.isApproved == false ? "1" : "2";
                        DdlHrApproval.Enabled = false;
                        ddldecision.Enabled = false;
                    }
                    else if (getLeavesDetail.isRejected == true)
                    {
                        ddldecision.SelectedValue = getLeavesDetail.isManagerMark == false ? "1" : "2";
                        DdlHrApproval.SelectedValue = getLeavesDetail.isApproved == false ? "1" : "2";
                        DdlHrApproval.Enabled = false;
                        ddldecision.Enabled = false;
                    }
                    else
                    {
                        {
                            DdlHrApproval.SelectedValue = "0";
                        }
                    }

                }




             






            }
            //}
        }
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        //  vt_Common.Clear(pnlDetail.Controls);
    }
    #endregion
}