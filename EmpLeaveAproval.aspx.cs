using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmpLeaveAproval : System.Web.UI.Page
{
    #region HelperMethod
    public void LoadData(DateTime attendanceDate, int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetCompanyEmployeeAttandance(CompanyID, attendanceDate).ToList();
            if (Query.Count > 0)
            {
                int EmployeeID = Query.FirstOrDefault().EmployeeID;
                grdAttendance.DataSource = Query;
                grdAttendance.DataBind();
                var record = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.EmployeeID == EmployeeID && o.EntryDate == attendanceDate);
                if (record != null)
                {
                    btnSaveAttendance.Visible = false;
                }
                else
                {
                    btnSaveAttendance.Visible = true;
                }
            }
            else
            {
                grdAttendance.DataSource = null;
                grdAttendance.DataBind();
                btnSaveAttendance.Visible = false;
            }
            //       hdCompany.Value = CompanyID.ToString();
            //    hdDate.Value = attendanceDate.ToString();
        }
    }
    private int GetDayID(DateTime AttendanceDay)
    {
        int DayID;
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.vt_tbl_AttendaceDay.Where(x => x.Date == AttendanceDay).Select(x => x.DayID).FirstOrDefault();
            if (Query == 0)
            {
                vt_tbl_AttendaceDay DAttendance = new vt_tbl_AttendaceDay();
                DAttendance.Date = AttendanceDay;
                db.vt_tbl_AttendaceDay.Add(DAttendance);
                db.SaveChanges();

                var ID = db.vt_tbl_AttendaceDay.Where(x => x.Date == AttendanceDay).Select(x => x.DayID).SingleOrDefault();
                DayID = ID;
            }
            else
            {
                DayID = Query;
            }
            return DayID;
        }
    }
    private void GetMultipleTime(DateTime DateAttendance, int EmployeeID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int DayID = GetDayID(Convert.ToDateTime(DateTime.Now)); //txtDate.Text
            var Query = (from E in db.vt_tbl_Employee
                         join EA in db.vt_tbl_EmpAttendance on E.EmployeeID equals EA.EmployeeID
                         where EA.EmployeeID == EmployeeID && EA.DayID == DayID
                         select EA).ToList();
            //ViewState["mydata"] = Query.ToArray();
            Session["myData"] = Query;
            grdMultiAttendance.DataSource = Query;
            grdMultiAttendance.DataBind();
            if (Query.Count > 0)
            {
                grdMultiAttendance.Rows[0].Enabled = false;
            }
        }
    }
    #endregion
    // Outer Grid
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                using (var db = new vt_EMSEntities())
                {

                }
                int RoleID;
                RoleID = Convert.ToInt32(Session["RoleId"]);
                if (RoleID == 2)
                {
                    BindGridHR();
                }
                else
                {
                    BindGridLM();
                }

                //using (vt_EMSEntities db = new vt_EMSEntities())
                //{
                //    //int employee = (int)((EMS_Session)Session["EMS_Session"]).Employee.EmployeeID;
                //    int employee = Convert.ToInt32(Session["UserId"]);

                //    //string companyID = vt_Common.CheckString(((EMS_Session)Session["EMS_Session"]).Employee.CompanyID);
                //    string companyID = vt_Common.CheckString(Session["CompanyId"]);

                //    if (!string.IsNullOrEmpty(companyID) && employee > 0)
                //    {
                //        int compID = vt_Common.CheckInt(companyID);
                //        var getAppLeave = (from L in db.vt_tbl_LeaveApplication
                //                           join Emp in db.vt_tbl_Employee on L.EnrollId equals Emp.EmployeeID into EmpLeaves
                //                           from x in EmpLeaves
                //                           where x.CompanyID == compID && x.ManagerID == employee
                //                           select new { x.EmployeeID, x.EmployeeName, L.LeaveApplicationID, L.FromDate, L.ToDate, L.isApproved }).ToList();
                //        grdAttendance.DataSource = getAppLeave;
                //        grdAttendance.DataBind();
                //    }
                //    else
                //    {
                //        var getAppLeave = (from L in db.vt_tbl_LeaveApplication
                //                           join Emp in db.vt_tbl_Employee on L.EnrollId equals Emp.EmployeeID into EmpLeaves
                //                           from x in EmpLeaves
                //                           where x.CompanyID > 0 && x.ManagerID > 0
                //                           select new { x.EmployeeID, x.EmployeeName, L.LeaveApplicationID, L.FromDate, L.ToDate, L.isApproved }).ToList();
                //        grdAttendance.DataSource = getAppLeave;
                //        grdAttendance.DataBind();
                //    }
                //}

            }
        }
    }

    private void BindGridLM()
    {
        int EmployeeID = Convert.ToInt32(Session["EmployeeID"]);
        
        DataSet Ds = ProcedureCall.SpCall_BindLeaveApplicationToLM(EmployeeID);
        if(Ds != null && Ds.Tables.Count >0)
        {
            DataTable Dt = Ds.Tables[0];
            if(Dt != null && Dt.Rows.Count > 0)
            {
                grdAttendance.DataSource = Dt;
            }
            else
            {
                grdAttendance.DataSource = null;

            }
        }
        else
        {
            grdAttendance.DataSource = null;
        }
        grdAttendance.DataBind();
    }
    private void BindGridHR()
    {
        string companyID = vt_Common.CheckString(Session["CompanyId"]);
        int compID = vt_Common.CheckInt(companyID);

        DataSet Ds = ProcedureCall.SpCall_BindLeaveApplicationToHR(compID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                grdAttendance.DataSource = Dt;
            }
            else
            {
                grdAttendance.DataSource = null;
            }
        }
        else
        {
            grdAttendance.DataSource = null;
        }
        grdAttendance.DataBind();
    }
    #region Control Events
    protected void btnSaveAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime entryDate = Convert.ToDateTime(DateTime.Now);
            int companyID = Convert.ToInt32(1099);
            int DayID = GetDayID(Convert.ToDateTime(DateTime.Now));
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                foreach (GridViewRow item in grdAttendance.Rows)
                {
                    Label lblEmployeeAttendanceID = (Label)item.FindControl("lblEmployeeAttendanceID");
                    CheckBox chkAttendance = (CheckBox)item.FindControl("chkAttendance");
                    Label lblEmployeeName = (Label)item.FindControl("lblEmployeeName");
                    Label lblEmployeeID = (Label)item.FindControl("lblEmployeeID");
                    TextBox grdtxtInTime = (TextBox)item.FindControl("grdtxtInTime");
                    TextBox grdtxtOutTime = (TextBox)item.FindControl("grdtxtOutTime");
                    if (chkAttendance != null)
                    {
                        //int EmployeeAttendanceID = vt_Common.CheckInt(lblEmployeeAttendanceID.Text);
                        //int EmployeeID = vt_Common.CheckInt(lblEmployeeID.Text);
                        //var record = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.EmpAttendancID == EmployeeAttendanceID);
                        //if(record == null)
                        //{
                        //    vt_tbl_EmpAttendance EmpAttendance = new vt_tbl_EmpAttendance();
                        //    EmpAttendance.EntryDate = entryDate;
                        //    EmpAttendance.EmployeeID = EmployeeID;
                        //    EmpAttendance.CompanyID = companyID;
                        //    if (chkAttendance.Checked)
                        //    {
                        //        EmpAttendance.InTime = grdtxtInTime.Text;
                        //        EmpAttendance.OutTime = grdtxtOutTime.Text;
                        //    }                         
                        //    EmpAttendance.Status = chkAttendance.Checked;
                        //    db.vt_tbl_EmpAttendance.Add(EmpAttendance);
                        //    db.SaveChanges();                           
                        //}
                    }
                }
                MsgBox.Show(Page, MsgBox.success, "Attendance", "Done Successfully");
                vt_Common.ReloadJS(this.Page, "$('#ContentPlaceHolder1_btnSearch').click();");
            }
        }
        catch (Exception ex)
        {
            MsgBox.Show(Page, MsgBox.danger, "Attendance", "Record Not Save");
        }
    }
    protected void grdAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        foreach (GridViewRow item in grdAttendance.Rows)
        {
            Label lblEmployeeAttendanceID = (Label)item.FindControl("lblEmployeeAttendanceID");
            CheckBox chkAttendance = (CheckBox)item.FindControl("chkAttendance");
            Label lblEmployeeName = (Label)item.FindControl("lblEmployeeName");
            Label lblEmployeeID = (Label)item.FindControl("lblEmployeeID");
            TextBox grdtxtInTime = (TextBox)item.FindControl("grdtxtInTime");
            TextBox grdtxtOutTime = (TextBox)item.FindControl("grdtxtOutTime");
        }
        switch (e.CommandName)
        {
            case "AddMultipleAttendance":
                //      GetMultipleTime(Convert.ToDateTime(txtDate.Text), vt_Common.CheckInt(e.CommandArgument));
                //       hdDate.Value = txtDate.Text;
                vt_Common.ReloadJS(this.Page, "$('#EmployeeAttendance').modal();");
                UpDetail.Update();
                break;
            default:
                break;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        vt_Common.ReloadJS(this.Page, "$('#EmployeeAttendance').modal('hide');");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int DayID = GetDayID(Convert.ToDateTime(DateTime.Now)); //txtDate.Text
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int EmployeeID = 0;
                foreach (GridViewRow item in grdMultiAttendance.Rows)
                {
                    Label lblEmployeeID = (Label)item.FindControl("lblMultiGridEmpID");
                    Label lblEmployeeAttendanceID = (Label)item.FindControl("lblMultiGridEmpAttendanceID");
                    TextBox txtInTime = (TextBox)item.FindControl("MultitxtInTime");
                    TextBox txtOutTime = (TextBox)item.FindControl("MultitxtOutTime");
                    TextBox txtReason = (TextBox)item.FindControl("txtMultiGridReason");
                    if (lblEmployeeID.Text != "0")
                    {
                        EmployeeID = Convert.ToInt32(lblEmployeeID.Text);
                    }
                    vt_tbl_EmpAttendance EmpAttendance = new vt_tbl_EmpAttendance();
                    EmpAttendance.DayID = DayID;
                    EmpAttendance.EmployeeID = EmployeeID;
                    EmpAttendance.InTime = txtInTime.Text;
                    EmpAttendance.OutTime = txtOutTime.Text;
                    EmpAttendance.Reason = txtReason.Text;
                    if (string.IsNullOrEmpty(lblEmployeeAttendanceID.Text) || lblEmployeeAttendanceID.Text == "0")
                    {
                        db.vt_tbl_EmpAttendance.Add(EmpAttendance);
                    }
                    else
                    {
                        EmpAttendance.EmpAttendancID = vt_Common.CheckInt(lblEmployeeAttendanceID.Text);
                        db.Entry(EmpAttendance).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                db.SaveChanges();
                MsgBox.Show(Page, MsgBox.success, "Attendance", "Done Successfully");
                vt_Common.ReloadJS(this.Page, "$('#EmployeeAttendance').modal('hide');");
            }
        }
        catch (Exception ex)
        {
            MsgBox.Show(Page, MsgBox.danger, "Attendance", "Record Not Save");
        }
    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        //AddRow();
        List<vt_tbl_EmpAttendance> lst = Session["myData"] as List<vt_tbl_EmpAttendance>;
        lst.Add(new vt_tbl_EmpAttendance());
        //Session["myData"] = lst.ToArray();
        grdMultiAttendance.DataSource = lst;
        grdMultiAttendance.DataBind();
        grdMultiAttendance.Rows[0].Enabled = false;
    }
    #endregion
    protected void grdAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEmployeeAttendanceID = (Label)e.Row.FindControl("lblEmployeeAttendanceID");
                CheckBox chkAttendance = (CheckBox)e.Row.FindControl("chkAttendance");
                Label lblEmployeeID = (Label)e.Row.FindControl("lblEmployeeID");
                TextBox grdtxtInTime = (TextBox)e.Row.FindControl("grdtxtInTime");
                TextBox grdtxtOutTime = (TextBox)e.Row.FindControl("grdtxtOutTime");
                if (lblEmployeeAttendanceID != null && chkAttendance != null && lblEmployeeID != null && grdtxtInTime != null && grdtxtOutTime != null)
                {
                    int EmployeeID = (int)DataBinder.Eval(e.Row.DataItem, "EmployeeID");
                    DateTime entryDate = Convert.ToDateTime(DateTime.Now);
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        var record = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.EmployeeID == EmployeeID && o.EntryDate == entryDate);
                        if (record != null)
                        {
                            lblEmployeeAttendanceID.Text = record.EmpAttendancID.ToString();
                            chkAttendance.Checked = (bool)record.Status;
                            grdtxtInTime.Text = record.InTime;
                            grdtxtOutTime.Text = record.OutTime;
                        }
                    }
                }
            }
        }
    }
}