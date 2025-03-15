using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Company : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                if ((string)Session["UserName"] != "SuperAdmin")
                {
                    int ModuleID = 9;
                    int RoleID = (int)Session["RoleId"];
                    vt_EMSEntities db = new vt_EMSEntities();
                    DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);


                    string PageName = null;
                    if (Ds != null && Ds.Tables.Count > 0)
                    {
                        DataTable Dt = Ds.Tables[0];
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageName"].ToString() == "Company")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Company")
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
        //if (Session["EMS_Session"] != null)
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        LoadData();
        //    }
        //}
    }
    void LoadData()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //    if (Session["CompanyId"] == null)
            //    {
            //grdCompany.DataBind();
            //  UpView.Update();
            //}
            //else
            //{
            //    BtnAddNew.Visible = false;
            //    companygrid.Visible = false;
            //    FillDetailForm(Convert.ToInt32(Session["CompanyId"]));
            //}
            var query = db.SP_Get_Company(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
            grdCompany.DataSource = query;
            grdCompany.DataBind();
        }
    }
    protected void grdCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;

        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    if ((string)Session["UserName"] == "SuperAdmin")
                    {
                        FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                    }
                    else
                    {
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageUrl"].ToString() == "Company.aspx" && Row["Can_Delete"].ToString() == "True")
                            {
                                using (vt_EMSEntities db = new vt_EMSEntities())
                                {
                                    int ID = vt_Common.CheckInt(e.CommandArgument);
                                    int? returnValue = db.VT_SP_IsCompanyExist(ID).FirstOrDefault();
                                    if (returnValue == 1)
                                    {
                                        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                                    }
                                    else
                                    {
                                        vt_tbl_Company c = db.vt_tbl_Company.FirstOrDefault(x => x.CompanyID == ID);
                                        db.vt_tbl_Company.Remove(c);
                                        db.SaveChanges();
                                        LoadData();
                                        MsgBox.Show(Page, MsgBox.success, "Company", "Successfully Deleted");
                                    }
                                }
                            }
                            else if (Row["PageUrl"].ToString() == "Company.aspx" && Row["Can_Delete"].ToString() == "False")
                            {
                                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                            }
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
                if ((string)Session["UserName"] == "SuperAdmin")
                {
                    FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                }
                else
                {
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageUrl"].ToString() == "Company.aspx" && Row["Can_Update"].ToString() == "True")
                        {
                            FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                        }
                        else if (Row["PageUrl"].ToString() == "Company.aspx" && Row["Can_Update"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
    void ClearForm(bool isModalShow)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        upCompany.Update();
        if (isModalShow)
        {
            vt_Common.ReloadJS(this.Page, "$('#CompanyModal').modal('show');");
        }
        else
        {
            vt_Common.ReloadJS(this.Page, "$('#CompanyModal').modal('hide');");
        }
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        if ((string)Session["UserName"] == "SuperAdmin")
        {
            ClearForm(true);
        }
        else
        {
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Company.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    ClearForm(true);

                }

                else if (Row["PageUrl"].ToString() == "Company.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm(false);
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Company c = db.vt_tbl_Company.FirstOrDefault(x => x.CompanyID == ID);
            txtName.Text = c.CompanyName;
            txtShortName.Text = c.CompanyShortName;
            txtPhone.Text = c.Phone;
            txtEmail.Text = c.Email;
            txtWebsite.Text = c.Website;
            txtAddress.Text = c.Address;
            //txtEOBIAmount.Text = c.EOBI.ToString();
            txtEOBIAmount.Text = 0.ToString();

            txtWorkingHour.Text = c.HoursInDay.ToString();
            if (c.BreakStartTime.HasValue)
            {
                txtBreakStartTime.Text = c.BreakStartTime.Value.ToString("h:mm tt");
            }
            if (c.BreakEndTime.HasValue)
            {
                txtBreakEndTime.Text = c.BreakEndTime.Value.ToString("h:mm tt");
            }
            ViewState["PageID"] = ID;
            upCompany.Update();
            vt_Common.ReloadJS(this.Page, "$('#CompanyModal').modal('show');");
        }
    }
    static string GenerateCode()
    {
       
        Random random = new Random();
        return random.Next(1000, 9999).ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                var record = db.vt_tbl_Company.FirstOrDefault(o => o.CompanyID != recordID && o.CompanyName.ToLower().Replace(" ", "").Equals(txtName.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Company with the same name already exist');");
                }
                else
                {
                    vt_tbl_Company c = new vt_tbl_Company();
                    c.CompanyName = txtName.Text;
                    c.CompanyShortName = txtShortName.Text;
                    c.Phone = txtPhone.Text;
                    c.Email = txtEmail.Text;
                    c.Website = txtWebsite.Text;
                    c.Address = txtAddress.Text;
                    c.EOBI = 0;
                    //c.EOBI = vt_Common.CheckInt(txtEOBIAmount.Text);
                    c.HoursInDay = vt_Common.CheckInt(txtWorkingHour.Text);
                    //c.BreakStartTime = Convert.ToDateTime(txtBreakStartTime.Text);
                    //c.BreakEndTime = Convert.ToDateTime(txtBreakEndTime.Text);


                    bool CodeExists = true;
                    while (CodeExists)
                    {
                        Random random = new Random();
                        int code = random.Next(1, 9999);
                        var checkcode = db.vt_tbl_Company.FirstOrDefault(x => x.CompanyCode == code.ToString());

                        if (checkcode == null)
                        {
                            c.CompanyCode = code.ToString();
                            CodeExists = false;
                        }
                    }
                    


                    c.BreakStartTime = null;
                    c.BreakEndTime = null;

                    if (ViewState["PageID"] != null)
                    {
                        c.CompanyID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_Company.Add(c);
                        
                    }
                    db.SaveChanges();
                    if (ViewState["PageID"] == null)
                    {
                        db.VT_SP_CreateWorkingCalendarOnCompanyCreate(c.CompanyID);
                    }
                    MsgBox.Show(Page, MsgBox.success, txtName.Text, "Successfully Save");
                    ClearForm(false);
                }
                UpView.Update();
                LoadData();
            }
        }
        catch (DbUpdateException ex)
        {
        }
    }
}