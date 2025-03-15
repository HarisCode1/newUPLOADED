using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.IO;
using System.Data;

public partial class SaleryHead : System.Web.UI.Page
{
    string Pagename = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
        //try
        //{
        //    if (!IsPostBack)
        //    {
        //        if (Session["goAMLSession"] == null)
        //        {
        //            Response.Redirect("Login.aspx");
        //        }
        //        else
        //        {
        //            RolePermission_BAL RP = new RolePermission_BAL();
        //            DataTable dt = new DataTable();
        //            PayRoll_Session goAMLSess = (PayRoll_Session)Session["goAMLSession"];
        //            dt = RP.GetPagePermissionpPagesByRole(goAMLSess.RoleID, goAMLSess.UserID);
        //            string pageName = null;
        //            bool view = false;
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                int row = dt.Rows.IndexOf(dr);
        //                if (dt.Rows[row]["PageUrl"].ToString() == "SalaryHead.aspx")
        //                {
        //                    pageName = dt.Rows[row]["PageUrl"].ToString();
        //                    view = Convert.ToBoolean(dt.Rows[row]["Can_View"].ToString());
        //                    break;
        //                }
        //            }
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (pageName == "SalaryHead.aspx" && view == true)
        //                {
        //                    LoadData();
        //                    // DropdownBind();
        //                    ViewState["PageURL"] = pageName;
        //                    ViewState["ModuleId"] = vt_Common.GetModuleId(ViewState["PageURL"].ToString(), goAMLSess.PermissionTable);
        //                    ViewState["PageId"] = vt_Common.GetPageId(ViewState["PageURL"].ToString(), goAMLSess.PermissionTable);
        //                    Pagename = Path.GetFileNameWithoutExtension(Request.Path);

        //                }
        //                else
        //                {
        //                    Response.Redirect("Default.aspx");
        //                }
        //            }
        //            else
        //            {
        //                Response.Redirect("Default.aspx");
        //            }
        //        }
        //    }
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTableGrid", "DataTableGrid('GridData');", true);
        //}
        //catch (Exception ex)
        //{

        //}
    }
    #region Control Event
    protected void grdSalaryHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_SalaryHead h = db.vt_tbl_SalaryHead.FirstOrDefault(x => x.SalaryHeadID == ID);
                        db.vt_tbl_SalaryHead.Remove(h);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, h.SalaryHeadName, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#saleryhead').modal();binddata();");
                UpDetail.Update();
                break;
            default:
                break;
        }
    }
    protected void grdSalaryHead_RowDataBound(object sender, GridViewRowEventArgs e)
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
                var record = db.vt_tbl_SalaryHead.FirstOrDefault(o => o.SalaryHeadID != recordID && o.CompanyID == companyID && o.SalaryHeadName.ToLower().Replace(" ", "").Equals(txtName.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Salary Head with the same name already exist');");
                }
                else
                {
                    vt_tbl_SalaryHead h = new vt_tbl_SalaryHead();
                    h.CompanyID = companyID;
                    h.SalaryHeadName = txtName.Text;
                    h.PrintingName = txtPrintingName.Text;
                    h.Type = ddlType.SelectedValue;
                    h.ApplicableFrom = vt_Common.CheckDateTime(txtAppFromDate.Text);
                    h.Remark = txtRemark.Text;
                    h.Variable = chkVariable.Checked;
                    if (h.Type == "Deduction")
                    {
                        h.PT = h.PF = h.ESIC = false;
                    }
                    else if (h.Variable)
                    {
                        h.PF = h.PT = false;
                        h.ESIC = chkEOBI.Checked;
                    }
                    else
                    {
                        h.PF = chkPF.Checked;
                        h.PT = chkPT.Checked;
                        h.ESIC = chkEOBI.Checked;
                    }
                    if (ViewState["PageID"] != null)
                    {
                        h.SalaryHeadID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(h).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_SalaryHead.Add(h);
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
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetSalaryHead(Convert.ToInt32(ddlCompany.SelectedValue));
            grdSalaryHead.DataSource = Query;
            grdSalaryHead.DataBind();
            UpView.Update();
        }
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {
            if (ddlcomp.Items.FindByValue(ddlCompany.SelectedValue) != null)
            {
                ddlcomp.SelectedValue = ddlCompany.SelectedValue;
            }
            vt_Common.ReloadJS(this.Page, "$('#saleryhead').modal();");
        }
        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#saleryhead').modal();");
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
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (((EMS_Session)Session["EMS_Session"]).Company == null)
                {
                    var Query = db.VT_SP_GetSalaryHead(Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
                    grdSalaryHead.DataSource = Query;
                    grdSalaryHead.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    grdSalaryHead.Columns[1].Visible = false;
                    var Query = db.VT_SP_GetSalaryHead(vt_Common.CompanyId).ToList();
                    grdSalaryHead.DataSource = Query;
                    grdSalaryHead.DataBind();
                }
            }
        }
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#saleryhead').modal('hide');binddata();");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_SalaryHead h = db.vt_tbl_SalaryHead.FirstOrDefault(x => x.SalaryHeadID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(h.CompanyID);
            txtName.Text = h.SalaryHeadName;
            txtPrintingName.Text = h.PrintingName;
            ddlType.SelectedValue = h.Type;
            txtAppFromDate.Text = ((DateTime)h.ApplicableFrom).ToString("MM/dd/yyyy");
            chkVariable.Checked = h.Variable;
            chkPF.Checked = h.PF;
            chkPT.Checked = h.PT;
            chkEOBI.Checked = h.ESIC;
            txtRemark.Text = h.Remark;
        }
        ViewState["PageID"] = ID;
    }
    #endregion
}