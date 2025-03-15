using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmailSetting : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        int ModuleID = 6;
        int RoleID = Convert.ToInt32(Session["RoleId"]);
        DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                string PageName = null;
                int UserID = Convert.ToInt32(Session["UserId"]);
                string UserName = Convert.ToString(Session["UserName"]);
                var query = db.vt_tbl_User.Where(x => x.UserId == UserID).ToList();
                if (query.Count > 0 || UserName == "SuperAdmin")
                {
                    BtnAddNew.Visible = false;
                }
                if (Ds != null && Ds.Tables.Count > 0)
                {
                    DataTable Dt = Ds.Tables[0];
                    DataTable DtPer = Session["PagePermissions"] as DataTable;

                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageName"].ToString() == "Advance Salary")
                        {
                            PageName = Row["PageName"].ToString();
                            break;
                        }
                    }
                    if (PageName == "Advance Salary")
                    {
                        loadData();
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }
            }
        }
    }
    #region ControlEvents
    protected void grdEmailSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;

        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageUrl"].ToString() == "AdvanceSalary.aspx" && Row["Can_Delete"].ToString() == "True")
                        {
                            using (vt_EMSEntities db = new vt_EMSEntities())
                            {
                                int ID = vt_Common.CheckInt(e.CommandArgument);
                                vt_tbl_EmployeeAdvSalary em = db.vt_tbl_EmployeeAdvSalary.FirstOrDefault(x => x.AdvSalaryID == ID);
                                db.vt_tbl_EmployeeAdvSalary.Remove(em);
                                db.SaveChanges();
                                loadData();
                                UpView.Update();
                                MsgBox.Show(Page, MsgBox.success, "Advance Salary", "Successfully Deleted");
                            }
                        }
                        else if (Row["PageUrl"].ToString() == "AdvanceSalary.aspx" && Row["Can_Delete"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditCompany":

                foreach (DataRow Row in Dt.Rows)
                {
                    if (Row["PageUrl"].ToString() == "AdvanceSalary.aspx" && Row["Can_Update"].ToString() == "True")
                    {
                        FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                        vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal();");
                        UpDetail.Update();
                    }
                    else if (Row["PageUrl"].ToString() == "AdvanceSalary.aspx" && Row["Can_Update"].ToString() == "False")
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to edit this record");
                    }
                }
               

                break;
            default:
                break;
        }
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int companyID = 0;
                int empID = Convert.ToInt32(ddlEmployee.SelectedValue);
                DateTime salaryDateTime = Convert.ToDateTime(txtDate.Text);
                decimal advAmount = Convert.ToDecimal(txtAmt.Text);
                if (Session["CompanyId"] != null)
                {
                    companyID = Convert.ToInt32(Session["CompanyId"]);
                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                var isValid = true;
                var salaryAmount = db.vt_tbl_EmployeeAdvSalary.Where(o => o.CompanyID == companyID && o.EmployeeID == empID && System.Data.Objects.EntityFunctions.TruncateTime(o.SalaryOfMonth) == System.Data.Objects.EntityFunctions.TruncateTime(salaryDateTime)).Sum(o => o.SalaryAmount);
                if (salaryAmount > 0)
                {
                    var Salary = db.vt_tbl_EmployeeGrossSalary.FirstOrDefault(x => x.EmployeeID == empID);
                    if (Salary != null)
                    {
                        decimal empAmount = vt_Common.Checkdecimal(Salary.BasicSalary + Salary.TransportAllownce + Salary.FuelAllownce + Salary.MedicalAllowance + Salary.SpecialAllownce + Salary.HouseRentAllownce);
                        decimal? remianAmount = empAmount - salaryAmount;
                        if (remianAmount == 0)
                        {
                            vt_Common.ReloadJS(this.Page, "showMessage('You already avail full salary amount of this month');");
                            isValid = false;
                        }
                        else
                        {
                            if (advAmount > remianAmount)
                            {
                                vt_Common.ReloadJS(this.Page, "showMessage('You have " + remianAmount + " amount left for advance salary of this month');");
                                isValid = false;
                            }
                        }
                    }
                }
                if (isValid)
                {
                    vt_tbl_EmployeeAdvSalary advance = new vt_tbl_EmployeeAdvSalary();
                    advance.EmployeeID = empID;
                    advance.SalaryOfMonth = salaryDateTime;
                    advance.SalaryAmount = advAmount;
                    advance.AdvSalaryReleaseDate = Convert.ToDateTime(txtDateRelease.Text);
                    advance.ModifiedDate = DateTime.Now;
                    advance.CompanyID = companyID;
                    if (ViewState["PageID"] != null)
                    {
                        advance.AdvSalaryID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(advance).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        advance.CreatedBy = Convert.ToInt32(Session["CompanyId"]);
                        advance.CreatedDate = DateTime.Now;
                        db.vt_tbl_EmployeeAdvSalary.Add(advance);
                    }
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, "Advance Created", "Successfully Save");
                    ClearForm();
                    loadData();
                    UpView.Update();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadData();
        UpView.Update();
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        foreach (DataRow Row in Dt.Rows)
        {
            if (Row["PageUrl"].ToString() == "AdvanceSalary.aspx" && Row["Can_Insert"].ToString() == "True")
            {
                int EmpID = Convert.ToInt32(Session["UserId"]);
                string UserName = Convert.ToString(Session["UserName"]);


                ViewState["PageID"] = null;
                vt_Common.Clear(pnlDetail.Controls);
                UpDetail.Update();
                ddlEmployee.Items.Clear();
                //ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));

                ddlEmployee.SelectedValue = EmpID.ToString();
                ddlEmployee.Enabled = false;
                if (Session["CompanyId"] == null)
                {
                    if (ddlcomp.Items.FindByValue(ddlCompany.SelectedValue) != null)
                    {
                        ddlcomp.SelectedValue = ddlCompany.SelectedValue;
                        int EntryID = vt_Common.CheckInt(ddlCompany.SelectedValue);
                        BindEmployeeGrid(EntryID);
                    }
                    vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal();");
                }
                else
                {
                    ddlCompany.Visible = false;
                    trCompany.Visible = false;
                    BindEmployeeGrid(Convert.ToInt32(Session["CompanyId"]));
                    vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal();");
                }
            }
            else if (Row["PageUrl"].ToString() == "AdvanceSalary.aspx" && Row["Can_Insert"].ToString() == "False")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            }
        }
        
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee.Items.Clear();
        if (ddlcomp.SelectedValue != "")
        {
            BindEmployeeGrid(Convert.ToInt32(ddlcomp.SelectedValue));
        }
        else
        {
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
        }
        UpDetail.Update();
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
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal('hide');");
    }
    # endregion
    #region Healper Method
    public void loadData()
    {

        //using (vt_EMSEntities db = new vt_EMSEntities())
        //{
        //    if (Session["CompanyId"] == null)
        //    {
        //        int companyID = Convert.ToInt32(ddlCompany.SelectedValue);
        //        if (companyID == 0)
        //        {
        //            var Query = db.vt_tbl_EmployeeAdvSalary.Select(x => new
        //            {
        //                EmployeeName = x.vt_tbl_Employee.EmployeeName,
        //                CompanyID = x.CompanyID,
        //                SalaryOfMonth = x.SalaryOfMonth,
        //                x.SalaryAmount,
        //                x.AdvSalaryReleaseDate,
        //                x.AdvSalaryID
        //            }).ToList();
        //            grdEmailSetting.DataSource = Query;
        //            grdEmailSetting.DataBind();
        //        }
        //        else
        //        {
        //            var Query = db.vt_tbl_EmployeeAdvSalary.Where(x => x.CompanyID == companyID).Select(x => new
        //            {
        //                EmployeeName = x.vt_tbl_Employee.EmployeeName,
        //                CompanyID = x.CompanyID,
        //                SalaryOfMonth = x.SalaryOfMonth,
        //                x.SalaryAmount,
        //                x.AdvSalaryReleaseDate,
        //                x.AdvSalaryID
        //            }).ToList();
        //            grdEmailSetting.DataSource = Query;
        //            grdEmailSetting.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        divCompany.Visible = false;
        //        grdEmailSetting.Columns[1].Visible = false;
        //        int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        //        var Query = db.vt_tbl_EmployeeAdvSalary.Where(x => x.CompanyID == CompanyID).Select(x => new
        //        {
        //            EmployeeName = x.vt_tbl_Employee.EmployeeName,
        //            CompanyID = x.CompanyID,
        //            SalaryOfMonth = x.SalaryOfMonth,
        //            x.SalaryAmount,
        //            x.AdvSalaryReleaseDate,
        //            x.AdvSalaryID
        //        }).ToList();
        //        grdEmailSetting.DataSource = Query;
        //        grdEmailSetting.DataBind();
        //    }
        //}

    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_EmployeeAdvSalary e = db.vt_tbl_EmployeeAdvSalary.FirstOrDefault(x => x.AdvSalaryID == ID);
            //   txtEmpID.Text = e.EmployeeID.ToString();
            ///  txtEmpName.Text = e.vt_tbl_Employee.EmployeeName;
            ///  
            ddlcomp.SelectedValue = e.CompanyID.ToString();
            BindEmployeeGrid(e.CompanyID);

            ddlEmployee.SelectedValue = vt_Common.CheckString(e.EmployeeID);
            txtDate.Text = e.SalaryOfMonth.Value.ToShortDateString();
            txtAmt.Text = e.SalaryAmount.ToString();
            txtDateRelease.Text = e.AdvSalaryReleaseDate.Value.ToShortDateString();
            hdnAdvSalaryID.Value = e.AdvSalaryID.ToString();
            var Salary = db.vt_tbl_EmployeeGrossSalary.Where(x => x.EmployeeID == e.EmployeeID && x.CurrentSalary == true).SingleOrDefault();
            if (Salary != null)
            {
                txtGrossSalary.Text = (Salary.BasicSalary + Salary.TransportAllownce + Salary.FuelAllownce + Salary.MedicalAllowance + Salary.SpecialAllownce + Salary.HouseRentAllownce).ToString();
            }
            else { txtGrossSalary.Text = ""; }
        }
        ViewState["PageID"] = ID;
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal('hide');");
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        int empID = vt_Common.CheckInt(ddlEmployee.SelectedValue);
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Salary = db.vt_tbl_EmployeeGrossSalary.FirstOrDefault(x => x.EmployeeID == empID);
            if (Salary != null)
            {
                txtGrossSalary.Text = (Salary.BasicSalary + Salary.TransportAllownce + Salary.FuelAllownce + Salary.MedicalAllowance + Salary.SpecialAllownce + Salary.HouseRentAllownce).ToString();
            }
            else { txtGrossSalary.Text = ""; }
        }
    }
    #endregion
    protected void grdEmailSetting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["CompanyId"] == null)
        {
            if (e.Row != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblCompany = (Label)e.Row.FindControl("lblCompany");
                    if (lblCompany != null)
                    {
                        int CompanyID = (int)DataBinder.Eval(e.Row.DataItem, "CompanyID");
                        using (vt_EMSEntities db = new vt_EMSEntities())
                        {
                            var Query = db.vt_tbl_Company.FirstOrDefault(o => o.CompanyID == CompanyID);
                            if (Query != null)
                            {
                                lblCompany.Text = Query.CompanyName;
                            }
                        }
                    }
                }
            }
        }
    }
}