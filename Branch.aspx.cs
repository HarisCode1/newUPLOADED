using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Branch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
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
                            if (Row["PageName"].ToString() == "Branch")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Branch")
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
            vt_Common.ReloadJS(this.Page, "binddata();");
        }
        //if (!IsPostBack)
        //{
        //    LoadData();
        //}
        //vt_Common.ReloadJS(this.Page, "binddata();");
    }
    #region Control Event
    protected void grdBranch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Branch b = db.vt_tbl_Branch.FirstOrDefault(x => x.BranchID == ID);
                        db.vt_tbl_Branch.Remove(b);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, b.BranchName, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#myModalBranch').modal();");
                UpDetail.Update();
                break;
            default:
                break;
        }
    }
    protected void grdBranch_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        int CompanyID = (int)DataBinder.Eval(e.Row.DataItem, "CompanyId");
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
                    companyID = Convert.ToInt32(Session["CompanyId"]);
                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                var record = db.vt_tbl_Branch.FirstOrDefault(o => o.BranchID != recordID && o.CompanyId == companyID && o.BranchName.ToLower().Replace(" ", "").Equals(txtName.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Branch with the same name already exist');");
                }
                else
                {
                    vt_tbl_Branch b = new vt_tbl_Branch();
                    b.CompanyId = companyID;
                    b.BranchName = txtName.Text;
                    b.BranchShortName = txtShortName.Text;
                    b.ContactPerson = txtCPerson.Text;
                    b.Phone = txtPhone.Text;
                    b.Email = txtEmail.Text;
                    b.Address = txtAddress.Text;
                    if (ViewState["PageID"] != null)
                    {
                        b.BranchID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_Branch.Add(b);
                    }
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, txtName.Text, "Successfully Save");
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
    protected void BtnAddNew_Click(object sender, EventArgs e)
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
        vt_Common.ReloadJS(this.Page, "$('#myModalBranch').modal();");
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
                    grdBranch.Columns[1].Visible = false;
                    companyID = vt_Common.CompanyId;
                }
                var Query = db.VT_SP_GetBranches(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                grdBranch.DataSource = Query;
                grdBranch.DataBind();
            }
        //}
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#myModalBranch').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Branch c = db.vt_tbl_Branch.FirstOrDefault(x => x.BranchID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(c.CompanyId);
            txtName.Text = c.BranchName;
            txtShortName.Text = c.BranchShortName;
            txtCPerson.Text = c.ContactPerson;
            txtPhone.Text = c.Phone;
            txtEmail.Text = c.Email;
            txtAddress.Text = c.Address;
        }
        ViewState["PageID"] = ID;
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        UpView.Update();
    }
    #endregion
    
}