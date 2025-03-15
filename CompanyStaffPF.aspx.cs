using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

/// <summary>
/// Defines the <see cref="CompanyStaffPF" />
/// </summary>
public partial class CompanyStaffPF : System.Web.UI.Page
{
    /// <summary>
    /// Defines the CompanystaffPF
    /// </summary>
    internal CompanyStaffpf_BAl CompanystaffPF = new CompanyStaffpf_BAl();

    /// <summary>
    /// The Page_Load
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                LoadData();
                BindComp();
                DropdownBind();
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                ddlcomp.SelectedValue = Companyid.ToString();
            }
        }
    }
    public void BindComp()
    {
        try
        {
            vt_Common.Bind_DropDown(ddlcomp, "vt_sp_GetCompanies", "CompanyName", "CompanyID");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    /// <summary>
    /// The LoadData
    /// </summary>
    protected void LoadData()
    {
        //vt_Common.Bind_GridView(GridEmployee, CompanystaffPF.GetPF_RulesByID(1));
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //int Companyid = Convert.ToInt32(Session["CompanyId"]);
            //ddlcomp.SelectedValue = Companyid.ToString();
            var Query = db.sp_GetCompanyPFrules(1, Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
            GridEmployee.DataSource = Query;
            GridEmployee.DataBind();
        }
    }

    /// <summary>
    /// The DropdownBind
    /// </summary>
    protected void DropdownBind()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ddSalaryType.Items.Clear();
            var SalaryType = (from m in db.vt_tbl_TypeofSalary
                              select new
                              {
                                  m.Type,
                                  m.Id
                              }).ToList();

            ddSalaryType.DataSource = SalaryType;
            ddSalaryType.DataTextField = "Type";
            ddSalaryType.DataValueField = "Id";
            ddSalaryType.DataBind();
            ddSalaryType.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ddlEmployeeType.Items.Clear();
            var EmployeeType = (from m in db.vt_tbl_TypeofEmployee
                                select new
                                {
                                    m.Type,
                                    m.Id
                                }).ToList();

            ddlEmployeeType.DataSource = EmployeeType;
            ddlEmployeeType.DataTextField = "Type";
            ddlEmployeeType.DataValueField = "Id";
            ddlEmployeeType.DataBind();
            ddlEmployeeType.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }

    /// <summary>
    /// The BindCompany
    /// </summary>
    internal void BindCompany()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var query = db.vt_tbl_Company.Where(y => y.CompanyID > 0).ToList();

            ddlcomp.DataSource = query;
            ddlcomp.DataTextField = "CompanyName";
            ddlcomp.DataValueField = "CompanyID";
            ddlcomp.DataBind();
            ddlcomp.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }

    /// <summary>
    /// The lbtnEdit_Command
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="CommandEventArgs"/></param>
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        DropdownBind();

        try
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "CompanyStaffPF.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    vt_Common.ReloadJS(this.Page, "$('#CompanyModal').modal('show');");

                    if (e.CommandArgument.ToString() != "")
                    {
                        CompanyStaffpf_BAl companystaff = CompanystaffPF.GetCompanyStaffPFID(Convert.ToInt32(e.CommandArgument));
                        ddlcomp.SelectedValue = companystaff.CompanyID.ToString();
                        ddlcomp_SelectedIndexChanged(sender, e);
                        ddSalaryType.SelectedValue = companystaff.SalaryTypeID.ToString();
                        ddlEmployeeType.SelectedValue = companystaff.EmployeeTypeId.ToString();
                        txtpercent.Text = Convert.ToString(companystaff.Percentage);
                        //TxtBxWitnessNameFirst.Text = companystaff.WitnesName1;
                        //TxtBxWitnessCNICFirst.Text = companystaff.WitnesCNIC1;
                        //TxtBxWitnessNameSecond.Text = companystaff.WitnesName2;
                        //TxtBxWitnessCNICSecond.Text = companystaff.WitnesCNIC2;
                        chkActive.Checked = companystaff.IsActive;
                        ViewState["PfId"] = companystaff.StaffPFID.ToString();
                        ddlcomp.SelectedValue = companystaff.CompanyID.ToString();
                        upCompanyStaffPF.Update();
                    }
                }
                else if (Row["PageUrl"].ToString() == "CompanyStaffPF.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                }
            }

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
        }
    }

    /// <summary>
    /// The lbtnDelete_Command
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="CommandEventArgs"/></param>
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "CompanyStaffPF.aspx" && Row["Can_Delete"].ToString() == "True")
                {
                    CompanystaffPF.StaffPFID = Convert.ToInt32(TxtIDs.Text);
                    CompanystaffPF.DeleteStaffID(CompanystaffPF.StaffPFID);
                    vt_Common.ReloadJS(this.Page, "$('#deleteform').modal('hide')");
                    //vt_Common.Bind_GridView(GridEmployee, CompanystaffPF.GetPF_RulesByID(1));
                    MsgBox.Show(Page, MsgBox.success, CompanystaffPF.WitnesName1, "Successfully Deleted");
                    LoadData();
                    UpView.Update();
                }

                else if (Row["PageUrl"].ToString() == "CompanyStaffPF.aspx" && Row["Can_Delete"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                }

            }

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    /// <summary>
    /// The lbtnDelete_Modalshow
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="CommandEventArgs"/></param>
    protected void lbtnDelete_Modalshow(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            CompanyStaffpf_BAl companystaff = CompanystaffPF.GetCompanyStaffPFID(Convert.ToInt32(e.CommandArgument));
            TxtIDs.Text = companystaff.StaffPFID.ToString();

            vt_Common.ReloadJS(this.Page, "$('#deleteform').modal();");
        }
    }

    /// <summary>
    /// The ClearForm
    /// </summary>
    /// <param name="isModalShow">The isModalShow<see cref="bool"/></param>
    internal void ClearForm(bool isModalShow)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        upCompanyStaffPF.Update();
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        ddlcomp.SelectedValue = Companyid.ToString();
        if (isModalShow)
        {
            vt_Common.ReloadJS(this.Page, "$('#CompanyModal').modal('show');");
        }
        else
        {
            vt_Common.ReloadJS(this.Page, "$('#CompanyModal').modal('hide');");
        }
    }

    /// <summary>
    /// The BtnAddNew_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;

        foreach (DataRow Row in Dt.Rows)
        {
            if (Row["PageUrl"].ToString() == "CompanyStaffPF.aspx" && Row["Can_Insert"].ToString() == "True")
            {
                vt_Common.ReloadJS(this.Page, "ClearFields();");
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                ddlcomp.SelectedValue = Companyid.ToString();
                ClearForm(true);

            }
            else if (Row["PageUrl"].ToString() == "CompanyStaffPF.aspx" && Row["Can_Insert"].ToString() == "False")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            }
        }
    }

    /// <summary>
    /// The btnClose_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm(false);
    }

    /// <summary>
    /// The ddlcomp_SelectedIndexChanged
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
            // BindDesignation(CompanyID);
        }
        catch (Exception ex)
        {
        }
    }

    //void BindDesignation(int CompanyID)
    //{
    //    ddlEmployee.Items.Clear();
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        var query = db.vt_tbl_Employee.ToList();
    //        if (CompanyID > 0)
    //        {
    //            query = db.vt_tbl_Employee.Where(x => x.CompanyID == CompanyID).ToList();
    //        }

    //        ddlEmployee.DataSource = query;s
    //        ddlEmployee.DataTextField = "EmployeeName";
    //        ddlEmployee.DataValueField = "EmployeeID";
    //        ddlEmployee.DataBind();
    //        ddlEmployee.Items.Insert(0, new ListItem("Please Select", "0"));
    //    }
    //}

    /// <summary>
    /// The ddlEmployee_SelectedIndexChanged
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //    int employeeID = vt_Common.CheckInt(ddlEmployee.SelectedValue);
                //    var getEmployee = db.vt_tbl_Employee.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
            }
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// The btnSave_Click
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="e">The e<see cref="EventArgs"/></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_CompanyStaffPF c = new vt_tbl_CompanyStaffPF();

                int StaffpfID = 0;
                StaffpfID = vt_Common.CheckInt(ViewState["PfId"]);
                int CompanyId = 0;
                CompanyId = vt_Common.CheckInt(ddlcomp.SelectedValue);
                int EmployeeType = 0;
                EmployeeType = vt_Common.CheckInt(ddlEmployeeType.SelectedValue);
                int SalaryType = 0;
                SalaryType = vt_Common.CheckInt(ddSalaryType.SelectedValue);
                bool chkactive = true;
                chkactive = vt_Common.CheckBoolean(chkActive.Checked);
                int recordID = vt_Common.CheckInt(ViewState["PfId"]);
                c = db.vt_tbl_CompanyStaffPF.Where(x => x.StaffPFID == recordID).FirstOrDefault();
                if (c == null)
                {
                    c = new vt_tbl_CompanyStaffPF();
                }

                var query = db.vt_tbl_CompanyStaffPF.Where(x => x.CompanyID == CompanyId && x.StaffPFID != StaffpfID && x.EmployeeTypeId == EmployeeType && x.SalaryTypeId == SalaryType && x.IsActive == chkactive).ToList();
                //var querys = db.vt_tbl_CompanyStaffPF.Where(x => x.CompanyID.Equals(ddlcomp.SelectedValue) && x.EmployeeTypeId.Equals(ddlEmployeeType.SelectedValue) && x.SalaryTypeId.Equals(ddSalaryType.SelectedValue) && x.IsActive.Equals(chkActive.Checked)).ToList();
                if (query.Count <= 0)
                {

                    int CompanyID = Convert.ToInt32(Session["CompanyId"]);
                    c.CompanyID = CompanyID;
                    c.EmployeeID = Convert.ToInt32(Session["UserId"]);
                    c.EmployeeTypeId = Convert.ToInt32(ddlEmployeeType.SelectedValue);
                    c.SalaryTypeId = Convert.ToInt32(ddSalaryType.SelectedValue);
                    c.IsActive = chkActive.Checked;
                    c.Percentage = Convert.ToDecimal(txtpercent.Text);
                    //c.WitnesCNIC1 = TxtBxWitnessCNICFirst.Text;
                    //c.WitnesName1 = TxtBxWitnessNameFirst.Text;
                    //c.WitnesName2 = TxtBxWitnessNameSecond.Text;
                    //c.WitnesCNIC2 = TxtBxWitnessCNICSecond.Text;

                    if (ViewState["PfId"] != null)
                    {
                        c.Modifiedon = DateTime.Now;
                        c.Modifiedby = Convert.ToInt32(Session["UserId"]);
                        c.StaffPFID = vt_Common.CheckInt(ViewState["PfId"]);
                        db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        c.Createdby = Convert.ToInt32(Session["UserId"]);
                        c.Createdon = DateTime.Now;
                        db.vt_tbl_CompanyStaffPF.Add(c);
                    }

                    UpView.Update();
                    MsgBox.Show(Page, MsgBox.success, CompanystaffPF.WitnesName1, "Record Saved Successfully.");
                    db.SaveChanges();
                    ClearForm(false);
                    LoadData();

                }
                else
                {
                    MsgBox.Show(Page, MsgBox.danger, CompanystaffPF.SalaryTypeID.ToString(), "Record Already Exist.");

                }
            }
            ClearForm(false);
        }

        catch (DbUpdateException ex)
        {
            MsgBox.Show(Page, MsgBox.danger, "Employee Not Saving due to this", ex.Message);
        }
    }
}
