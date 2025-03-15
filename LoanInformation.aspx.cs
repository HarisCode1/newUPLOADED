using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class LoanInformation : System.Web.UI.Page
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
                            if (Row["PageName"].ToString() == "Loan Category")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Loan Category")
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
    #region Control Event
    protected void grdLoanInformation_RowCommand(object sender, GridViewCommandEventArgs e)
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
                            if (Row["PageUrl"].ToString() == "LoanInformation.aspx" && Row["Can_Delete"].ToString() == "True")
                            {
                                using (vt_EMSEntities db = new vt_EMSEntities())
                                {
                                    int ID = vt_Common.CheckInt(e.CommandArgument);
                                    vt_tbl_Loan l = db.vt_tbl_Loan.FirstOrDefault(x => x.LoanID == ID);
                                    db.vt_tbl_Loan.Remove(l);
                                    db.SaveChanges();
                                    LoadData();
                                    UpView.Update();
                                    MsgBox.Show(Page, MsgBox.success, l.Name, "Successfully Deleted");
                                }
                            }

                            else if (Row["PageUrl"].ToString() == "LoanInformation.aspx" && Row["Can_Delete"].ToString() == "False")
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
                        if (Row["PageUrl"].ToString() == "LoanInformation.aspx" && Row["Can_Update"].ToString() == "True")
                        {
                            using (vt_EMSEntities db = new vt_EMSEntities())
                            {

                                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                                vt_Common.ReloadJS(this.Page, "$('#loaninformation').modal();");
                                UpDetail.Update();
                                break;
                            }
                        }
                        else if (Row["PageUrl"].ToString() == "LoanInformation.aspx" && Row["Can_Update"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                        }
                    }

                    break;
            }
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "You are Loged in by SuperAdmin");
        }
        //vt_Common.ReloadJS(this.Page, "$('.confirm').confirm();");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int companyID = 0;
                //if (Session["CompanyId"] != null)
                //{
                //    companyID = (Convert.ToInt32(Session["CompanyId"]));
                //}
                //else
                //{
                //    companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                //}
                companyID = (Convert.ToInt32(Session["CompanyId"]));

                var record = db.vt_tbl_Loan.FirstOrDefault(o => o.LoanID != recordID && o.CompanyID == companyID && o.Name.ToLower().Replace(" ", "").Equals(txtName.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Loan Category with the same name already exist');");
                }
                else
                {
                    vt_tbl_Loan l = new vt_tbl_Loan();
                    l.CompanyID = companyID;
                    l.Name = txtName.Text;
                    l.ShortName = txtShortName.Text;
                    if (ViewState["PageID"] != null)
                    {
                        l.LoanID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_Loan.Add(l);
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
        LoadData();
        UpView.Update();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        foreach (DataRow Row in Dt.Rows)
        {
            if (Row["PageUrl"].ToString() == "LoanInformation.aspx" && Row["Can_Insert"].ToString() == "True")
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
                    vt_Common.ReloadJS(this.Page, "$('#loaninformation').modal();");
                }
                else
                {
                    ddlCompany.Visible = false;
                    trCompany.Visible = false;
                    vt_Common.ReloadJS(this.Page, "$('#loaninformation').modal();");
                }
            }

            else if (Row["PageUrl"].ToString() == "LoanInformation.aspx" && Row["Can_Insert"].ToString() == "False")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            }
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
                grdLoanInformation.Columns[1].Visible = false;
                companyID = (Convert.ToInt32(Session["CompanyId"]));
            }
            var Query = db.VT_SP_GetLoanInformation(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
            grdLoanInformation.DataSource = Query;
            grdLoanInformation.DataBind();
        }
        //}
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#loaninformation').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Loan l = db.vt_tbl_Loan.FirstOrDefault(x => x.LoanID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(l.CompanyID);
            txtName.Text = l.Name;
            txtShortName.Text = l.ShortName;
        }
        ViewState["PageID"] = ID;
    }
    #endregion
}