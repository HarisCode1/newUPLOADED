using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }
    #region Control Event
    protected void grdCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Category b = db.vt_tbl_Category.FirstOrDefault(x => x.CategoryID == ID);
                        db.vt_tbl_Category.Remove(b);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, b.Category, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#Category').modal();");
                UpDetail.Update();

                break;
            default:
                break;
        }
    }
    protected void grdCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (((EMS_Session)Session["EMS_Session"]).Company == null)
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
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    companyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                var record = db.vt_tbl_Category.FirstOrDefault(o => o.CategoryID != recordID && o.CompanyId == companyID && o.Category.ToLower().Replace(" ", "").Equals(txtName.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Category with the same name already exist');");
                }
                else
                {
                    vt_tbl_Category b = new vt_tbl_Category();
                    b.CompanyId = companyID;
                    b.Category = txtName.Text;
                    if (ViewState["PageID"] != null)
                    {
                        b.CategoryID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_Category.Add(b);
                    }
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, txtName.Text, "Successfully Save");
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
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
        ClearForm();
        if (((EMS_Session)Session["EMS_Session"]).Company == null)
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
        vt_Common.ReloadJS(this.Page, "$('#Category').modal();");
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
    #region Healper Method
    void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int companyID = 0;
                if (((EMS_Session)Session["EMS_Session"]).Company == null)
                {
                    companyID = Convert.ToInt32(ddlCompany.SelectedValue);
                }
                else
                {
                    divCompany.Visible = false;
                    grdCategory.Columns[1].Visible = false;
                    companyID = vt_Common.CompanyId;
                }
                var Query = db.VT_SP_GetCategories(Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
                grdCategory.DataSource = Query;
                grdCategory.DataBind();
            }
        }
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#Category').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Category c = db.vt_tbl_Category.FirstOrDefault(x => x.CategoryID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(c.CompanyId);
            txtName.Text = c.Category;
            ViewState["PageID"] = ID;

        }
    }
    #endregion
}