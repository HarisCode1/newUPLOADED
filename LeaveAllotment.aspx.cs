using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class LeaveAllotment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }

    #region Control Event

    protected void grdleaveAllotment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_EmployeeLeaveDetails eld = db.vt_tbl_EmployeeLeaveDetails.FirstOrDefault(x => x.ELDID == ID);
                        db.vt_tbl_EmployeeLeaveDetails.Remove(eld);
                        db.SaveChanges();
                        //LoadData();
                        //UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, eld.Allotment, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#leaveallotment').modal();binddata();");
                //UpDetail.Update();
                break;
            default:
                break;
        }

        
    }

    

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    


    #endregion

    #region Healper Method
    //void LoadData()
    //{
    //    if (Session["EMS_Session"] != null)
    //    {
            
    //        EDS_Department.DataBind();
    //        EDS_LeaveShortName.DataBind();
    //        EDS_Year.DataBind();

    //        EDS_LeaveAllotment.WhereParameters.Clear();
    //        EDS_LeaveAllotment.WhereParameters.Add("CompanyId", TypeCode.Int32, ((EMS_Session)Session["EMS_Session"]).Company.CompanyID.ToString());
    //        EDS_LeaveAllotment.WhereParameters.Add("DepartmentID", TypeCode.Int32,
    //            ddlDepartment.SelectedValue.Equals("") ? "1" : ddlDepartment.SelectedValue);
    //        grdleaveAllotment.DataBind();
    //        if (grdleaveAllotment.Rows.Count > 0)
    //        {
    //            grdleaveAllotment.HeaderRow.TableSection = TableRowSection.TableHeader;
    //        }
    //    }
    //}

    void ClearForm()
    {
        ViewState["PageID"] = null;
        //vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#leaveallotment').modal('hide');binddata();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_LeaveApplication l = db.vt_tbl_LeaveApplication.FirstOrDefault(x => x.LeaveApplicationID == ID);
            //txtName.Text = h.SalaryHeadName;
            //txtPrintingName.Text = h.PrintingName;
            //ddlType.SelectedValue = h.Type;
            //txtAppFromDate.Text = ((DateTime)h.ApplicableFrom).ToString("MM/dd/yyyy");
            //chkVariable.Checked = h.Variable;
            //chkPF.Checked = h.PF;
            
        }
        ViewState["PageID"] = ID;
    }

    private void LoadData(int CompanyID) 
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                var DeptList = db.VT_SP_GetDepartment(CompanyID).ToList();
                ddlDepartment.DataSource = DeptList;
                ddlDepartment.DataValueField = "DepartmentID";
                ddlDepartment.DataTextField = "Department";
                ddlDepartment.DataBind();
                //riaz
                var LeaveList = db.VT_SP_GetLeaves(CompanyID,1).ToList();
                ddlLeaveShortName.DataSource = LeaveList;
                ddlLeaveShortName.DataValueField = "LeaveID";
                ddlLeaveShortName.DataTextField = "LeaveShortName";
                ddlLeaveShortName.DataBind();
            }
        }
        catch (Exception ex) 
        {

        }
    }

    #endregion


    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            int LeaveID=Convert.ToInt32(ddlLeaveShortName.SelectedValue);
            int DepartmentID=Convert.ToInt32(ddlDepartment.SelectedValue);
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_EmployeeLeaveDetails eld = new vt_tbl_EmployeeLeaveDetails();

                eld.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                eld.LeaveID = LeaveID;
                eld.Allotment = txtLeave.Text;
                eld.DepartmentID = DepartmentID;

                var ELDID = db.vt_tbl_EmployeeLeaveDetails.Where(x => x.LeaveID == LeaveID && x.DepartmentID == DepartmentID).Select(x => x.ELDID).SingleOrDefault();
                if (ELDID != 0)
                {
                    eld.ELDID = ELDID;
                    db.Entry(eld).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_EmployeeLeaveDetails.Add(eld);
                }
                db.SaveChanges();
                MsgBox.Show(Page, MsgBox.success, txtLeave.Text, "Successfully Save");
                
            }
        }
        catch (DbUpdateException ex)
        {
            MsgBox.Show(Page, MsgBox.danger, txtLeave.Text, "Not Save");
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {
            ddlDepartment.Items.Clear();
            ddlLeaveShortName.Items.Clear();
            LoadData(Convert.ToInt32(ddlCompany.SelectedValue));
        }
    }
}