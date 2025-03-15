using System;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;






public partial class ManualAttendance : System.Web.UI.Page
{

    private vt_EMSEntities dbContext = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Authenticate.Confirm())
        {
            
            if (!IsPostBack)
            {
                LoadData();
            }
        }
    }
    #region Control Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int employeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
                DateTime entryDate = vt_Common.CheckDateTime(txtFromDate.Text);
                int companyID = 0;
                //if (((EMS_Session)Session["EMS_Session"]).Company != null)
                //{
                companyID = Convert.ToInt32(Session["CompanyId"]);
                //}
                //else
                //{
                //    companyID = vt_Common.CheckInt(ddlModalCompany.SelectedValue);
                //}

                var emp = db.vt_tbl_Employee.FirstOrDefault(w => w.EmployeeID.Equals(employeeID));
                //int EnrollID = emp != null && emp.EnrollId != null ? int.Parse(emp.EnrollId) : 0;


                var record = db.vt_tbl_ManualAttendance
    .FirstOrDefault(o => o.ManualAttendanceID != recordID
                         && o.CompanyID == companyID
                         //&& o.EnrollId == EnrollID
                         && DbFunctions.TruncateTime(o.Date) == DbFunctions.TruncateTime(entryDate));
                //var record = db.vt_tbl_ManualAttendance.FirstOrDefault(o => o.ManualAttendanceID != recordID && o.CompanyID == companyID && o.EnrollId == employeeID && System.Data.Objects.EntityFunctions.TruncateTime(o.Date) == System.Data.Objects.EntityFunctions.TruncateTime(entryDate));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Employee Attendance on this date already exist');");
                }
                else
                {
                    vt_tbl_ManualAttendance mAttendance = new vt_tbl_ManualAttendance();
                    mAttendance.CompanyID = companyID;
                    mAttendance.Date = entryDate;
                    //mAttendance.EnrollId = EnrollID;
                    mAttendance.InTime = txtInTime.Text;
                    mAttendance.OutTime = txtOutTime.Text;
                    //mAttendance.ShiftID = Convert.ToInt32(ddlShift.SelectedValue);
                    mAttendance.Status = ddlStatus.SelectedValue;
                    //mAttendance.OTHrs = txtOTHrs.Text;
                    if (ViewState["PageID"] != null)
                    {
                        mAttendance.ManualAttendanceID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(mAttendance).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_ManualAttendance.Add(mAttendance);
                    }                   
                    var empRecord = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.CompanyID == companyID && o.EmployeeID == employeeID && o.EntryDate == entryDate);
                    if(empRecord != null)
                    {
                        empRecord.InTime = txtInTime.Text;
                        empRecord.OutTime = txtOutTime.Text;
                        if (ddlStatus.SelectedValue.Equals("P"))
                        {
                            empRecord.Status = true;
                        }
                        else
                        {
                            empRecord.Status = false;
                        }
                    }
                    else
                    {
                        vt_tbl_EmpAttendance empAttendance = new vt_tbl_EmpAttendance();
                        empAttendance.CompanyID = companyID;
                        empAttendance.EntryDate = entryDate;
                        empAttendance.EmployeeID = employeeID;
                        empAttendance.InTime = txtInTime.Text;
                        empAttendance.OutTime = txtOutTime.Text;
                        //mAttendance.ShiftID = Convert.ToInt32(ddlShift.SelectedValue);

                        if (ddlStatus.SelectedValue.Equals("P"))
                        {
                            empAttendance.Status = true;
                        }
                        else
                        {
                            empAttendance.Status = false;
                        }



                        //empAttendance.Status = ddlStatus.SelectedValue.Equals("P");
                        //mAttendance.OTHrs = txtOTHrs.Text;
                        if (ViewState["PageID"] != null)
                        {
                            empAttendance.EmpAttendancID = vt_Common.CheckInt(ViewState["PageID"]);
                            db.Entry(empAttendance).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.vt_tbl_EmpAttendance.Add(empAttendance);
                        }
                    }
                    db.SaveChanges();
                    string empId = employeeID.ToString();
                    var leaveRecord = db.vt_tbl_LeaveApplicationDates
                           .FirstOrDefault(o => o.EnrollId == empId
                         && DbFunctions.TruncateTime(o.Date) == DbFunctions.TruncateTime(entryDate)); 
                    //var leaveRecord = db.vt_tbl_LeaveApplicationDates.FirstOrDefault(o => o.EnrollId == empId && System.Data.Objects.EntityFunctions.TruncateTime(o.Date) == System.Data.Objects.EntityFunctions.TruncateTime(entryDate));
                    if (leaveRecord != null)
                    {
                        int? LeaveAppID = leaveRecord.LeaveAppID;
                        db.vt_tbl_LeaveApplicationDates.Remove(leaveRecord);
                        db.SaveChanges();
                        var recordCount = db.vt_tbl_LeaveApplicationDates.Where(o => o.LeaveAppID == LeaveAppID).Count();
                        if(recordCount == 0)
                        {
                            var leaveAppRecord = db.vt_tbl_LeaveApplication.FirstOrDefault(o => o.LeaveApplicationID == LeaveAppID);
                            if(leaveAppRecord != null)
                            {
                                db.vt_tbl_LeaveApplication.Remove(leaveAppRecord);
                                db.SaveChanges();
                            }
                        }
                    }
                    MsgBox.Show(Page, MsgBox.success, ddlStatus.SelectedValue, "Successfully Save");
                    ClearForm();
                    LoadData();
                    UpView.Update();
                }       
            }          
        }
        catch (DbUpdateException ex)
        {
        }
    }
    protected void grdManualAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_ManualAttendance mA = db.vt_tbl_ManualAttendance.FirstOrDefault(x => x.ManualAttendanceID == ID);
                        db.vt_tbl_ManualAttendance.Remove(mA);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, mA.Status, "Successfully Deleted");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditCompany":
                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                vt_Common.ReloadJS(this.Page, "$('#manualattendance').modal();$('#ddlStatus').trigger('change');");
                upMAttendance.Update();
                break;
            default:
                break;
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        upMAttendance.Update();
        ddlEmployee.Items.Clear();
        //if (Session["CompanyId"] == null)
        //{
        //    if (ddlModalCompany.Items.FindByValue(ddlCompany.SelectedValue) != null)
        //    {
        //        ddlModalCompany.SelectedValue = ddlCompany.SelectedValue;
 
       
            BindEmployeeGrid(vt_Common.CheckInt(Session["CompanyId"]));
        


        
        //    }
        //    vt_Common.ReloadJS(this.Page, "$('#manualattendance').modal();");
        //}
        //else
        //{
        ddlCompany.Visible = false;
        trCompany.Visible = false;
        //    BindEmployeeGrid(vt_Common.CompanyId);
        vt_Common.ReloadJS(this.Page, "$('#manualattendance').modal();");
        //}
    }
    protected void ddlModalCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee.Items.Clear();
        if (ddlModalCompany.SelectedValue != "")
        {
            BindEmployeeGrid(Convert.ToInt32(ddlModalCompany.SelectedValue));
        }
        else
        {
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
        }
        upMAttendance.Update();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        UpView.Update();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    #endregion
    #region Helper Method
    public void LoadData()
    {
        //if (Session["EMS_Session"] != null)
        //{
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
            int RoleId = 0;
            RoleId = Convert.ToInt32(Session["RoleID"]);
            int EnRoleId = 0;
            EnRoleId = Convert.ToInt32(Session["EnrollId"]);
            int companyID = 0;
                if (Session["CompanyId"] == null)
                {
                    companyID = Convert.ToInt32(ddlCompany.SelectedValue);
                }
                else
                {
                    divCompany.Visible = false;
                    grdManualAttendance.Columns[1].Visible = false;
                    companyID = Convert.ToInt32(Session["CompanyId"]);
                    BindEmployeeGrid(companyID);
                }
            var Query = db.VT_SP_GetManualAttendance(companyID, RoleId, EnRoleId).ToList();
            grdManualAttendance.DataSource = Query;
            grdManualAttendance.DataBind();
        }
        //}
    }
    public void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#manualattendance').modal('hide');");
    }
    public void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_ManualAttendance mA = db.vt_tbl_ManualAttendance.FirstOrDefault(x => x.ManualAttendanceID == ID);
            //vt_tbl_Company com = db.vt_tbl_Company.FirstOrDefault(x => x.CompanyID == mA.CompanyID);
            ddlModalCompany.SelectedValue = mA.CompanyID.ToString();
            BindEmployeeGrid(vt_Common.CheckInt(ddlModalCompany.SelectedValue));
            txtFromDate.Text = mA.Date.ToString();
            ddlStatus.SelectedValue = mA.Status;
            txtInTime.Text = mA.InTime;
            txtOutTime.Text = mA.OutTime;
            ddlEmployee.SelectedValue = mA.EnrollId.ToString();
            //ddlShift.SelectedValue = mA.ShiftID.ToString();
            //txtOTHrs.Text = mA.OTHrs;
            //vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == mA.EnrollId);
            //txtEmpID.Text = emp.EmployeeID.ToString();
            //txtEmpName.Text = emp.EmployeeName;
        }
        ViewState["PageID"] = ID;
    }
    public void BindEmployeeGrid(int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {


            //string EnRoleId = Convert.ToInt32(Session["EnrollId"]).ToString();

            int Userid = Convert.ToInt32(Session["UserId"]);
           int  companyID = Convert.ToInt32(Session["CompanyId"]);

            var activeuser = db.vt_tbl_User
                .Where(w => (w.UserId.Equals(Userid) && (w.CompanyId == companyID))).SingleOrDefault();
            string roleid = activeuser.RoleId.ToString();

            //var Employee = db.vt_tbl_Employee.Where(w => (w.RoleID == 4) && (w.CompanyID == companyID)).SingleOrDefault();
            //string Enrollid = Employee.EnrollId;

            string EnRoleId = activeuser.EmployeeEnrollId.ToString(); 
            string EmplEmail = activeuser != null ? activeuser.Email.ToString() : null;

            if (roleid == "4")
            {
                    var EmployeeList = db.vt_tbl_Employee.Where(w =>  (w.RoleID == 4) &&  (w.CompanyID == companyID) &&(w.EnrollId== EnRoleId) ).Select(e => new
                    {
                        EmployeeName = e.EmployeeName,
                        EmployeeID = e.EmployeeID
                        })
                   .ToList();

                ddlEmployee.DataSource = EmployeeList;
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();
            }
            else
            {
                var EmployeeList = db.VT_SP_GetEmployees(CompanyID).ToList();
                ddlEmployee.DataSource = EmployeeList;
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();
            }


            //var Query = db.VT_SP_GetManualAttendance(companyID, 4, Enrollid).ToList();
            //grdManualAttendance.DataSource = Query;
            //grdManualAttendance.DataBind();


        }
    }
    #endregion   
}