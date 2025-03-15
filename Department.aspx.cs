using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Department : System.Web.UI.Page
{
    int Count = 0;
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        int ModuleID = 9;
        vt_EMSEntities db = new vt_EMSEntities();
        
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                BindHeadDeparment();
                if ((string)Session["UserName"] != "SuperAdmin")
                {
                    int RoleID = (int)Session["RoleId"];
                    DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);
                    string PageName = null;
                    if (Ds != null && Ds.Tables.Count > 0)
                    {
                        DataTable Dt = Ds.Tables[0];
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageName"].ToString() == "Department")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Department")
                        {
                            LoadData();
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                    }
                }
                else
                {

                    LoadData();
                }
            }
        }
    }

    public void BindHeadDeparment()
    {
        try
        {
            var query = db.vt_tbl_HeadDepartment.ToList();
            if (query != null)
            {

                ddlheaddeparment.DataSource = query;
                ddlheaddeparment.DataTextField = "HeadDeparment";
                ddlheaddeparment.DataValueField = "Id";
                ddlheaddeparment.DataBind();
                ddlheaddeparment.Items.Insert(0, new ListItem("--Select Head Department--", "0"));
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #region Control Event

    protected void grddepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        if ((string)Session["UserName"] != "SuperAdmin")
        {
            switch (e.CommandName)
            {
                case "DeleteCompany":
                    try
                    {
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_Delete"].ToString() == "True")
                            {
                                int ID = vt_Common.CheckInt(e.CommandArgument);
                                using (vt_EMSEntities db = new vt_EMSEntities())
                                { var checkDeprt = db.vt_tbl_Designation.Where(x => x.DepartmentID == ID).FirstOrDefault();
                                    if (checkDeprt == null)
                                    {
                                        
                                        vt_tbl_Department b = db.vt_tbl_Department.FirstOrDefault(x => x.DepartmentID == ID);
                                        db.vt_tbl_Department.Remove(b);
                                        db.SaveChanges();
                                        LoadData();
                                        //      UpView.Update();
                                        MsgBox.Show(Page, MsgBox.success, b.Department, "Successfully Deleted");
                                    }
                                    else
                                    {
                                        MsgBox.Show(Page, MsgBox.danger, "", "Sorry this department can not be deleted because designation exist");

                                    }
                                   
                                }
                            }
                            else if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_Update"].ToString() == "False")
                            {
                                MsgBox.Show(Page, MsgBox.danger, txtDepartment.Text, "You Dont have Permission to delete this record");
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
                        if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_Update"].ToString() == "True")
                        {
                            FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                            vt_Common.ReloadJS(this.Page, "$('#department').modal();");
                            UpDetail.Update();
                            break;
                            //default:
                        }
                        else if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_Update"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, txtDepartment.Text, "You Dont have Permission to Update this record");
                        }
                    }
                    break;
            }
            
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, txtDepartment.Text, "You are Loged in by SuperAdmin");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int companyID = 0;
                if (Session["CompanyId"] != null)
                {
                    //companyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                    companyID = Convert.ToInt32(Session["CompanyId"]);

                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                var record = db.vt_tbl_Department.FirstOrDefault(o => o.DepartmentID != recordID && o.CompanyID == companyID && o.Department.ToLower().Replace(" ", "").Equals(txtDepartment.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Department with the same name already exist');");
                }
                else
                {
                    vt_tbl_Department b = new vt_tbl_Department();
                    b.CompanyID = companyID;
                    b.HeadDepartmentId =Convert.ToInt32(ddlheaddeparment.SelectedValue)   ;
                    b.Department =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtDepartment.Text);
                    if (ViewState["PageID"] != null)
                    {
                        b.DepartmentID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_Department.Add(b);
                    }
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, txtDepartment.Text, "Successfully Save");
                    ClearForm();
                    LoadData();
                    UpView.Update();
                    UpDetail.Update();
                }
            }
        }
        catch (DbUpdateException ex)
        {
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        UpView.Update();
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        if ((string)Session["UserName"] != "SuperAdmin")
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    ViewState["PageID"] = null;
                    vt_Common.Clear(pnlDetail.Controls);
                    UpDetail.Update();
                    if (Session["CompanyId"] == null)
                    {
                        if (ddlcomp.Items.FindByValue(ddlCompany.SelectedValue) != null)
                        {
                            ddlcomp.SelectedValue = ddlCompany.SelectedValue;
                        }
                    }
                    else
                    {
                        ddlCompany.Visible = false;
                        trCompany.Visible = false;
                    }
                    vt_Common.ReloadJS(this.Page, "$('#department').modal();");
                }
                else if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, txtDepartment.Text, "You Dont have Permission to Update this record");
                }
            }
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, txtDepartment.Text, "You are Loged in by SuperAdmin");
        }
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
            if (Session["CompanyId"] == null)
            {
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            }
            else
            {
                divCompany.Visible = false;
                grddepartment.Columns[1].Visible = false;
                //companyID = vt_Common.CompanyId;
                companyID = Convert.ToInt32(Session["CompanyId"]);

            }
            var Query = db.VT_SP_GetDepartment(companyID).ToList();
            grddepartment.DataSource = Query;
            grddepartment.DataBind();
          
        }

        //}
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#department').modal('hide');");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Department d = db.vt_tbl_Department.FirstOrDefault(x => x.DepartmentID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(d.CompanyID);
            ddlcomp.Enabled = false;
            ddlheaddeparment.SelectedValue = Convert.ToInt32(d.HeadDepartmentId).ToString();
            
            txtDepartment.Text = d.Department;
        }
        ViewState["PageID"] = ID;
    }
    #endregion

}