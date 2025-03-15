using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmployeeAttendance : System.Web.UI.Page
{
    #region HelperMethod
    public void LoadData(DateTime attendanceDate,int CompanyID)
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
                    btnSaveAttendance.Visible = true;
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
            hdCompany.Value = CompanyID.ToString();
            hdDate.Value = attendanceDate.ToString();
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
            int DayID = GetDayID(Convert.ToDateTime(txtDate.Text));
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
        if(Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                if (Session["CompanyId"] != null)
                {
                    trCompany.Visible = false;
                    hdCompany.Value = Session["CompanyId"].ToString();
                }
                grdAttendance.DataSource = null;
                grdAttendance.DataBind();
                //LoadData(DateTime.Today);
            }
        }
       
    }
    #region Control Events
    protected void btnSaveAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime entryDate = Convert.ToDateTime(hdDate.Value);
            int companyID = Convert.ToInt32(hdCompany.Value);
            int DayID = GetDayID(Convert.ToDateTime(txtDate.Text));
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
                    if(lblEmployeeAttendanceID != null && chkAttendance != null && lblEmployeeName != null && lblEmployeeID != null && grdtxtInTime != null && grdtxtOutTime != null)
                    {
                        int EmployeeAttendanceID = vt_Common.CheckInt(lblEmployeeAttendanceID.Text);
                        int EmployeeID = vt_Common.CheckInt(lblEmployeeID.Text);
                        var record = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.EmpAttendancID == EmployeeAttendanceID);
                        if(record == null)
                        {
                            vt_tbl_EmpAttendance EmpAttendance = new vt_tbl_EmpAttendance();
                            EmpAttendance.EntryDate = entryDate;
                            EmpAttendance.EmployeeID = EmployeeID;
                            EmpAttendance.CompanyID = companyID;
                            if (chkAttendance.Checked)
                            {
                                EmpAttendance.InTime = grdtxtInTime.Text;
                                EmpAttendance.OutTime = grdtxtOutTime.Text;
                            }                         
                            EmpAttendance.Status = chkAttendance.Checked;
                            db.vt_tbl_EmpAttendance.Add(EmpAttendance);
                            db.SaveChanges();                           
                        }
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
        switch (e.CommandName)
        {
            case "AddMultipleAttendance":
                GetMultipleTime(Convert.ToDateTime(txtDate.Text), vt_Common.CheckInt(e.CommandArgument));
                hdDate.Value = txtDate.Text;
                vt_Common.ReloadJS(this.Page, "$('#EmployeeAttendance').modal();");
                UpDetail.Update();
                break;
            default:
                break;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int companyID = 0;
        if (Session["CompanyId"] != null)
        {
            companyID = vt_Common.CheckInt(hdCompany.Value);
        }
        else
        {
            companyID = Convert.ToInt32(ddlCompany.SelectedValue);
        }
        LoadData(Convert.ToDateTime(txtDate.Text), companyID);
        UpView.Update();
    }
    // Inner Grid
    protected void btnClose_Click(object sender, EventArgs e)
    {
        vt_Common.ReloadJS(this.Page, "$('#EmployeeAttendance').modal('hide');");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int DayID = GetDayID(Convert.ToDateTime(txtDate.Text));
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
                    DateTime entryDate = Convert.ToDateTime(hdDate.Value);
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        var record = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.EmployeeID == EmployeeID && o.EntryDate == entryDate);
                        if(record != null)
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