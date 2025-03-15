using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class LeaveApplication : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    Custommethods custom = new Custommethods();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
              
                #region IsAuthenticated To Page
                string UserName = Session["UserName"].ToString();
                string PageUrl = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                DataTable Dt = Session["PagePermissions"] as DataTable;
                if (!vt_Common.IsAuthenticated(PageUrl, UserName, Dt))
                {
                    Response.Redirect("Default.aspx");
                }
                #endregion
                //LoadData();
                Bind_GV();
                divCompany.Visible = false;

            }
        }

    }
    //int EmpID = Convert.ToInt32(Session["EmployeeID"]);
    private void Bind_GV()
    {
        int EmployeeID = (int)Session["EmployeeID"];
        int CompanyID = (int)Session["CompanyId"];
        System.Data.DataSet Ds = ProcedureCall.SpCall_VT_SP_GetLeaveApplications(CompanyID, EmployeeID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                grdLeaveApplication.DataSource = Dt;
            }
            else
            {
                grdLeaveApplication.DataSource = null;
            }
        }
        else
        {
            grdLeaveApplication.DataSource = null;
        }
        grdLeaveApplication.DataBind();

    }
    #region Control Event
    protected void grdLeaveApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_LeaveApplication b = db.vt_tbl_LeaveApplication.FirstOrDefault(x => x.LeaveApplicationID == ID);
                        var tb = db.vt_tbl_LeaveApplicationDates.Where(o => o.LeaveAppID == ID).ToList();
                        foreach (var rec in tb)
                        {
                            db.vt_tbl_LeaveApplicationDates.Remove(rec);
                            db.SaveChanges();
                        }
                        db.vt_tbl_LeaveApplication.Remove(b);
                        db.SaveChanges();
                        //LoadData();
                        Bind_GV();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, b.Reason, "Successfully Deleted");
                    }
                
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditCompany":
               int lID = vt_Common.CheckInt(e.CommandArgument);
                var query = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID == lID).FirstOrDefault();
                if (query != null)
                {
                    if (query.isApproved ==true)
                    {

                        MsgBox.Show(Page, MsgBox.success, "", "Application has been approved you can not edit");
                    }
                    else if(query.isRejected ==true)
                    {

                        MsgBox.Show(Page, MsgBox.danger, "", "Application has been rejected!");
                    }
                    else if (query.isManagerMark ==true && query.AppliedToHR==true)
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "Your application is currently under process and cannot be edited at this time.");
                    }
                    else
                    {
                        FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                        vt_Common.ReloadJS(this.Page, "$('#leaveapplication').modal();binddata();");
                        UpDetail.Update();
                    }
                }
              
                break;
            default:
                break;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string totalLeaves = hdnTotalLeaves.Value;
        try
        {
            int applicationid = Convert.ToInt32(Session["applicationid"]);
            int empID = (int)Session["EmployeeID"];
            DateTime? fromdate = DateTime.Now;
            DateTime? todate = DateTime.Now;
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (!string.IsNullOrEmpty(txtLeaveFromDate.Text))
                {
                    string txtDateValue = txtLeaveFromDate.Text;
                   fromdate = custom.GetDateFromTextBox(txtDateValue);
                }
                if (!string.IsNullOrEmpty(txtLeaveToDate.Text))
                {

                    string txtDateValue = txtLeaveToDate.Text;
                     todate = custom.GetDateFromTextBox(txtDateValue);

                }
                var checkcreateorupdate = db.vt_tbl_LeaveApplication
              .Where(x => x.LeaveApplicationID == applicationid).FirstOrDefault();

                //if (checkcreateorupdate == null)
                //{
                // var checkleavedate = db.vt_tbl_LeaveApplication
                //.Where(x => x.EmployeeID == empID 
                // && x.isRejected == null
                // && (
                //     // If in edit mode, do not check the date range
                //     (checkcreateorupdate != null) ||
                //     (
                //         System.Data.Entity.DbFunctions.TruncateTime(x.FromDate) <= System.Data.Entity.DbFunctions.TruncateTime(fromdate.Value) &&
                //         System.Data.Entity.DbFunctions.TruncateTime(x.ToDate) >= System.Data.Entity.DbFunctions.TruncateTime(todate.Value)
                //     )))

                ////&&(
                ////    System.Data.Entity.DbFunctions.TruncateTime(x.FromDate) <= System.Data.Entity.DbFunctions.TruncateTime(fromdate.Value) &&
                ////    System.Data.Entity.DbFunctions.TruncateTime(x.ToDate) >= System.Data.Entity.DbFunctions.TruncateTime(todate.Value)
                ////)
                // .FirstOrDefault();

                var checkleavedate = db.vt_tbl_LeaveApplication
                .Where(x =>  x.EmployeeID == empID && x.isRejected == null
                    && ( 
                        System.Data.Entity.DbFunctions.TruncateTime(x.FromDate) <= System.Data.Entity.DbFunctions.TruncateTime(fromdate.Value) &&
                        System.Data.Entity.DbFunctions.TruncateTime(x.ToDate) >= System.Data.Entity.DbFunctions.TruncateTime(todate.Value)
            
                    )
                )   .FirstOrDefault();

                var remains = Convert.ToInt32(lblRemainingLeaves.Text) == 0 ? 0 : Convert.ToInt32(lblRemainingLeaves.Text) - Convert.ToInt32(hdnTotalLeaves.Value);
                    // var remains = Convert.ToInt32(lblRemainingLeaves.Text) - Convert.ToInt32(hdnTotalLeaves.Value);

                    if (Convert.ToInt32(lblRemainingLeaves.Text) == 0)
                    {
                        vt_Common.ReloadJS(this.Page, "showMessage('Remaining Leaves Are Less Then Requested Leaves');");
                        //MsgBox.Show(Page, MsgBox.danger, "", "Remaining Leaves Are Less Then Requested Leaves");
                        return;
                    }

                    if (Convert.ToInt32(remains) >= 0)
                    {
                        int leaveID = vt_Common.CheckInt(ddlLeaveType.SelectedValue);

                        int companyID = 0;
                        if (Session["CompanyId"] != null)
                        {
                            companyID = Convert.ToInt32(Session["CompanyId"]);
                        }
                        else
                        {
                            companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                        }
                        bool isValid = true;
                        var NumberOfDays = (vt_Common.CheckDateTime(txtLeaveToDate.Text) - vt_Common.CheckDateTime(txtLeaveFromDate.Text)).TotalDays;
                        DateTime dt = vt_Common.CheckDateTime(txtLeaveFromDate.Text);
                        int employeeID = vt_Common.CheckInt(Session["UserId"]);
                        var recordCheck = db.vt_tbl_LeaveApplication.FirstOrDefault(o => o.EnrollId == employeeID && o.CompanyID != companyID && System.Data.Entity.DbFunctions.TruncateTime(o.FromDate) == System.Data.Entity.DbFunctions.TruncateTime(dt) || System.Data.Entity.DbFunctions.TruncateTime(o.ToDate) == System.Data.Entity.DbFunctions.TruncateTime(dt));
                        if (checkleavedate == null)
                        {


                            if (isValid)
                            {

                                vt_tbl_Employee         em = db.vt_tbl_Employee.Where(x => x.EmployeeID == empID).FirstOrDefault();
                                vt_tbl_LeaveApplication la = new vt_tbl_LeaveApplication();
                                la.CompanyID =          companyID;
                                la.LeaveID =            vt_Common.CheckInt(ddlLeaveType.SelectedValue);
                                la.Reason = txtReason.Text;
                                la.Date = vt_Common.CheckDateTime(DateTime.Today);
                                la.TotalLeave = totalLeaves;
                                la.FromDate = fromdate.Value;
                                la.ToDate = todate.Value;
                                la.EnrollId = Convert.ToInt32(ddlEmployee.SelectedValue);
                                la.EmployeeID = empID;
                                la.isApproved = false;
                                la.isManagerMark = false;
                                la.AppliedToHR = true;
                                la.ManagerID = em.ManagerID;
                                //la.ManagerID = vt_Common.CheckInt(Session["UserId"]);


                                if (ViewState["PageID"] != null)
                                {
                                    int tID = vt_Common.CheckInt(ViewState["PageID"]);
                                    la.LeaveApplicationID = vt_Common.CheckInt(ViewState["PageID"]);
                                    db.Entry(la).State = System.Data.Entity.EntityState.Modified;
                                    var tb = db.vt_tbl_LeaveApplicationDates.Where(o => o.LeaveAppID == tID).ToList();
                                    foreach (var rec in tb)
                                    {
                                        db.vt_tbl_LeaveApplicationDates.Remove(rec);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    db.vt_tbl_LeaveApplication.Add(la);
                                }
                                db.SaveChanges();

                                MsgBox.Show(Page, MsgBox.success, txtReason.Text, "Successfully Saved");
                                ClearForm();
                                //LoadData();
                                Bind_GV();
                                UpView.Update();
                                int employeID = vt_Common.CheckInt(empID);

                                var getEmpLoyeeID = (from l in db.vt_tbl_LeaveApplication
                                                     join emp in db.vt_tbl_Employee on l.EnrollId equals emp.EmployeeID
                                                     where l.LeaveApplicationID == la.LeaveApplicationID
                                                     select new { emp.ManagerID }).FirstOrDefault();

                                if (getEmpLoyeeID.ManagerID > 0)
                                {
                                    var getManagerEmail = db.vt_tbl_Employee.Where(x => x.EmployeeID == getEmpLoyeeID.ManagerID).Select(y => y.Email).FirstOrDefault();
                                    vt_smtp.vt_smtpSend(URLLink(la.LeaveApplicationID), getManagerEmail);
                                }

                                vt_tbl_LeaveApplication Lea = db.vt_tbl_LeaveApplication.OrderByDescending(x => x.LeaveApplicationID).FirstOrDefault();
                                vt_tbl_LeaveApplication leaves = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID == Lea.LeaveApplicationID).FirstOrDefault();

                                bool firstDayOfLeave = true;
                                vt_tbl_LeaveApplication LeaveApp = db.vt_tbl_LeaveApplication.Where(y => y.LeaveApplicationID == leaves.LeaveApplicationID).FirstOrDefault();
                                //var NumberOfDays = (vt_Common.CheckDateTime(LeaveApp.ToDate) - vt_Common.CheckDateTime(LeaveApp.FromDate)).TotalDays;

                                for (int i = 1; i <= NumberOfDays + 1; i++)
                                {
                                    vt_tbl_LeaveApplicationDates days = new vt_tbl_LeaveApplicationDates();
                                    days.CompanyID = LeaveApp.CompanyID;
                                    days.EnrollId = vt_Common.CheckString(LeaveApp.EnrollId);
                                    days.LeaveID = vt_Common.CheckInt(LeaveApp.LeaveID);
                                    days.LeaveAppID = LeaveApp.LeaveApplicationID;
                                    days.IsApproved = false;
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
                                    
                                }
                            Session["Applicationid"] = la.LeaveApplicationID;
                            Response.Redirect("LeaveApplication.aspx");
                           

                        }
                            //else
                            //    vt_Common.ReloadJS(this.Page, "showMessage('Remaining Leaves Are Less Then Requested Leaves');");
                            ////MsgBox.Show(Page, MsgBox.danger, "", "Remaining Leaves Are Less Then Requested Leaves");
                        }
                        else
                        {
                            //MsgBox.Show(Page, MsgBox.danger, "", "Leave application already exists for the selected date range.");
                            vt_Common.ReloadJS(this.Page, "showMessage('Leave application already exists for the selected date range.');");
                            return;
                        }
                    }

                    else
                    {


                        vt_Common.ReloadJS(this.Page, "showMessage('Remaining Leaves Are Less Then Requested Leaves');");
                        //MsgBox.Show(Page, MsgBox.danger, "", "Remaining Leaves Are Less Then Requested Leaves");
                        return;
                    }

            }
        }
        catch (DbUpdateException ex)
        {
        }
       
    }
    private string URLLink(int currentID)
    {
        string UrlLink = string.Empty;
        string url = Request.Url.Authority;
        if (Request.ServerVariables["HTTPS"] == "on") { url = "https://" + url; }
        else
        {
            url = "http://" + url;
        }
        UrlLink = url + "/EmployeeLeaves.aspx?v2s=y&LOA=" + currentID;
        return UrlLink;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        BindLeaveType(Convert.ToInt32(Session["CompanyId"]));
        if (ddlLeaveType.Items.Count <= 1)
        {
            vt_Common.ReloadJS(this.Page, "showMessage('Leave application cannot be applied because your status is not confirmed.');");
            return;
        }


        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
        ddlEmployee.Items.Clear();
        ddlLeaveType.Items.Clear();
        lblTotalLeave.Text = "";
        lblRemainingLeaves.Text = "";
        hdnTotalLeaves.Value = "";
        if (Session["CompanyId"] == null)
        {
            if (ddlcomp.Items.FindByValue(ddlCompany.SelectedValue) != null)
            {
                ddlcomp.SelectedValue = ddlCompany.SelectedValue;
                BindEmployeeGrid(vt_Common.CheckInt(ddlcomp.SelectedValue));
                BindLeaveType(vt_Common.CheckInt(ddlcomp.SelectedValue));
            }
            vt_Common.ReloadJS(this.Page, "$('#leaveapplication').modal();");
        }
        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            BindEmployeeGrid(Convert.ToInt32(Session["CompanyId"]));
            BindLeaveType(Convert.ToInt32(Session["CompanyId"]));
            vt_Common.ReloadJS(this.Page, "$('#leaveapplication').modal();");
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadData();
        Bind_GV();
        UpView.Update();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    #endregion
    #region Healper Method
    void LoadData()
    {
        //if (Session["EMS_Session"] != null)
        //{
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int companyID = 0;
            //if (Session["CompanyId"] == null)
            if ((String)Session["UserName"] == "SuperAdmin")
            {
                BtnAddNew.Visible = false;
                companyID = Convert.ToInt32(Session["CompanyId"]);
                divCompany.Visible = false;

            }
            else
            {
                divCompany.Visible = false;
                grdLeaveApplication.Columns[1].Visible = false;
                companyID = Convert.ToInt32(Session["CompanyId"]);
                BindLeaveType(companyID);
                BindEmployeeGrid(companyID);
            }
            var Query = db.VT_SP_GetLeaveApplications(companyID, null).ToList();
            grdLeaveApplication.DataSource = Query;
            grdLeaveApplication.DataBind();
        }
        //}
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#leaveapplication').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_LeaveApplication la = db.vt_tbl_LeaveApplication.FirstOrDefault(x => x.LeaveApplicationID == ID);
            long employeeid = Convert.ToInt64(la.EmployeeID);
            vt_tbl_EmployeeLeave el = db.vt_tbl_EmployeeLeave.FirstOrDefault(x => x.LeaveTypeID == ID && x.EmployeeID == employeeid);
            //vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == la.EnrollId);
            ///txtLeaveDate.Text = vt_Common.CheckString(la.Date);
            ddlcomp.SelectedValue = vt_Common.CheckString(la.CompanyID);
            ddlcomp.Visible = false;
            BindLeaveType(vt_Common.CheckInt(la.CompanyID));
            BindEmployeeGrid(vt_Common.CheckInt(la.CompanyID));
            ddlEmployee.SelectedValue = vt_Common.CheckString(la.EnrollId) == null ? "" : vt_Common.CheckString(la.EnrollId);
            ddlLeaveType.SelectedValue = la.LeaveID.ToString();
            // txtBalance.Text = la.CurrentBalance;
            txtLeaveFromDate.Text = Convert.ToDateTime(la.FromDate).ToString("MM/dd/yyyy");
            txtLeaveToDate.Text = Convert.ToDateTime(la.ToDate).ToString("MM/dd/yyyy");
            //txtLeaveFromDate.Text = la.FromDate.ToShortDateString();
            //txtLeaveToDate.Text = la.ToDate.ToShortDateString();
            txtReason.Text = la.Reason;
            txtTotalLeaves.Text = la.TotalLeave;
            //txtcheck.Text = la.TotalLeave;
            CalculateRemainingLeaves();
            hdnTotalLeaves.Value = la.TotalLeave;
            var totalLeaves = db.vt_tbl_Leave.Where(x => x.CompanyID == la.CompanyID && x.LeaveID == la.LeaveID).Sum(x => x.NumberofLeaves);
            lblTotalLeave.Text = totalLeaves.ToString();
            //chkLeavefrmdate.Checked = (bool)la.IsHalfDayFrom;
            //chkLeavetodate.Checked = (bool)la.IsHalfDayTo;
        }
        ViewState["PageID"] = ID;
    }
    public void BindEmployeeGrid(int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var EmployeeList = db.VT_SP_GetEmployees(CompanyID).ToList();
            ddlEmployee.DataSource = EmployeeList;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            //ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
            ddlEmployee.SelectedValue = Session["EmployeeID"].ToString();
            ddlEmployee.Enabled = false;
        }
    }
    public void BindLeaveType(int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int empID = (int)Session["EmployeeID"];
            var LeaveList = db.VT_SP_GetLeaves(CompanyID, empID).ToList();
            ddlLeaveType.DataSource = LeaveList;
            ddlLeaveType.DataValueField = "LeaveID";
            ddlLeaveType.DataTextField = "LeaveName";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("Select Leave Type", ""));
        }
    }
    #endregion
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  ddlEmployee.Items.Clear();
        ddlLeaveType.Items.Clear();
        if (ddlcomp.SelectedValue != "")
        {
            BindEmployeeGrid(Convert.ToInt32(ddlcomp.SelectedValue));
            BindLeaveType(Convert.ToInt32(ddlcomp.SelectedValue));
        }
        else
        {
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
            ddlLeaveType.Items.Insert(0, new ListItem("Select Leave Type", ""));
        }
        UpDetail.Update();
    }
    public void CalculateRemainingLeaves()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            DateTime dtYear = DateTime.Now;
            DateTime? resultDate = DateTime.Now;
            
            if (!string.IsNullOrEmpty(txtLeaveFromDate.Text))
            {
                string txtDateValue = txtLeaveFromDate.Text;
                resultDate = custom.GetDateFromTextBox(txtDateValue);
            }
            //if (vt_Common.CheckInt(ddlcomp.SelectedValue) > 0 && vt_Common.CheckInt(ddlEmployee.SelectedValue) > 0 && vt_Common.CheckInt(ddlLeaveType.SelectedValue) > 0 && !string.IsNullOrWhiteSpace(txtLeaveFromDate.Text) && !string.IsNullOrWhiteSpace(txtLeaveToDate.Text))
            if (vt_Common.CheckInt(ddlcomp.SelectedValue) > 0 && vt_Common.CheckInt(Session["UserId"]) > 0 && vt_Common.CheckInt(ddlLeaveType.SelectedValue) > 0 && !string.IsNullOrWhiteSpace(txtLeaveFromDate.Text) && !string.IsNullOrWhiteSpace(txtLeaveToDate.Text))
            {
                int EmpID = Convert.ToInt32(Session["EmployeeID"]);
                //int companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                //int enrollID = vt_Common.CheckInt(ddlEmployee.SelectedValue);
                int companyID = vt_Common.CheckInt(Session["CompanyId"]);
                int enrollID =Convert.ToInt32(ddlEmployee.SelectedValue); //vt_Common.CheckInt(Session["UserId"]);
                int LeaveID = vt_Common.CheckInt(ddlLeaveType.SelectedValue);
                dtYear = resultDate.Value; // Convert.ToDateTime(DateTime.ParseExact(txtLeaveFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)); //Convert.ToDateTime();
                var remainLeaf = db.vt_tbl_LeaveApplication.Where(x => x.CompanyID == companyID && x.isRejected != true && x.EmployeeID == EmpID && x.LeaveID == LeaveID && System.Data.Entity.DbFunctions.TruncateTime(x.FromDate).Value.Year == System.Data.Entity.DbFunctions.TruncateTime(dtYear).Value.Year).ToList().Sum(x => Convert.ToInt32(x.TotalLeave));
                var TotalLeaf = lblTotalLeave.Text;
                var remains = vt_Common.CheckInt(TotalLeaf) - vt_Common.CheckInt(remainLeaf);

                lblRemainingLeaves.Text = remains.ToString();
                lblTotalLeave.Text = TotalLeaf.ToString();

            }
            else
            {
                if (ddlEmployee.SelectedIndex >0)
                {
                    int EmpID = Convert.ToInt32(Session["EmployeeID"]);
                    int companyID = Convert.ToInt32(Session["CompanyId"]);
                    int enrollID = Convert.ToInt32(ddlEmployee.SelectedValue);// Convert.ToInt32(Session["UserId"]);
                    int LeaveID = vt_Common.CheckInt(ddlLeaveType.SelectedValue);
                    if (!string.IsNullOrEmpty(txtLeaveFromDate.Text))
                    {
                        string txtDateValue = txtLeaveFromDate.Text;
                        resultDate = custom.GetDateFromTextBox(txtDateValue);
                        dtYear = resultDate.Value;

                    }
                    var remainLeaf = db.vt_tbl_LeaveApplication.Where(x => x.CompanyID == companyID && x.isRejected != true && x.EmployeeID == EmpID && x.LeaveID == LeaveID && System.Data.Entity.DbFunctions.TruncateTime(x.FromDate).Value.Year == System.Data.Entity.DbFunctions.TruncateTime(dtYear).Value.Year).ToList().Sum(x => Convert.ToInt32(x.TotalLeave));
                    var TotalLeaf = lblTotalLeave.Text;
                    var remains = vt_Common.CheckInt(TotalLeaf) - vt_Common.CheckInt(remainLeaf);
                    lblRemainingLeaves.Text = remains.ToString();
                    lblTotalLeave.Text = TotalLeaf.ToString();
                }
                else
                {
                    MsgBox.Show(Page, MsgBox.success, "", "Please Select Employee");
                    
                }
            
            }
        }
        //txtLeaveFromDate.Text = "";
        //txtLeaveToDate.Text = "";
        //UpDetail.Update();
    }
    //protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //  CalculateRemainingLeaves();
    //}
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
            int EmpID = Convert.ToInt32(Session["EmployeeID"]);
            if (ddlLeaveType.SelectedValue != "")
            {
                if (CompanyID <= 0)
                {
                    //CompanyID = (int)((EMS_Session)Session["EMS_Session"]).Employee.CompanyID;
                    CompanyID = Convert.ToInt32(Session["CompanyId"]);
                    int EMployeeID = Convert.ToInt32(Session["EmployeeID"]);
                    int LeaveID = vt_Common.CheckInt(ddlLeaveType.SelectedValue);
                    var totalLeaves = db.vt_tbl_EmployeeLeave.Where(x => x.CompanyID == CompanyID && x.LeaveTypeID == LeaveID && x.EmployeeID == EMployeeID).Sum(x => x.RemainingLeaves);
                    lblTotalLeave.Text = totalLeaves.ToString();
                }
                else
                {
                    CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                    int LeaveID = vt_Common.CheckInt(ddlLeaveType.SelectedValue);
                    var totalLeaves = db.vt_tbl_Leave.Where(x => x.CompanyID == CompanyID && x.LeaveID == LeaveID).Sum(x => x.NumberofLeaves);
                    lblTotalLeave.Text = totalLeaves.ToString();
                }
            }
            
            
        }
        CalculateRemainingLeaves();
    }
}