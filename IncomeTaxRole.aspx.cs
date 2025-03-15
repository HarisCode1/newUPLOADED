using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class IncomeTaxRole : System.Web.UI.Page
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

    protected void grdLeaveYear_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteTaxableIncome":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Tax_Rate_Role l = db.vt_tbl_Tax_Rate_Role.FirstOrDefault(x => x.ID == ID);
                        db.vt_tbl_Tax_Rate_Role.Remove(l);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Deleted");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditTaxableIncome":
                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                vt_Common.ReloadJS(this.Page, "$('#IncomeTax').modal();");
                UpDetail.Update();
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_Tax_Rate_Role t = new vt_tbl_Tax_Rate_Role();
                t.FROMTaxableIncome = vt_Common.Checkdecimal(txtFROMTaxableIncome.Text);
                t.ToTaxableIncome = vt_Common.Checkdecimal(txtToTaxableIncome.Text);
                t.RateOfTax = vt_Common.Checkdecimal(txtRateOfTax.Text);
                t.IsActive = chkActive.Checked;

                if (ViewState["PageID"] != null)
                {
                    t.DateOfModification = DateTime.Now;
                    t.ID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(t).Property(x => x.DateOfCreation).IsModified = false;                    
                }
                else
                {
                    t.DateOfCreation = DateTime.Now;
                    db.vt_tbl_Tax_Rate_Role.Add(t);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Save");
            ClearForm();
            LoadData();
            UpView.Update();
        }
        catch (DbUpdateException ex)
        {
            SqlException innerException = ex.GetBaseException() as SqlException;
            vt_Common.PrintfriendlySqlException(innerException, Page);
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
        //    EDS_LeaveYear.WhereParameters.Clear();
        //    EDS_LeaveYear.WhereParameters.Add("CompanyId", TypeCode.Int32, ((EMS_Session)Session["EMS_Session"]).Company.CompanyID.ToString());
        grdInComeTaxRole.DataBind();
        if (grdInComeTaxRole.Rows.Count > 0)
        {
            grdInComeTaxRole.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        //}
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#IncomeTax').modal('hide');");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Tax_Rate_Role t = db.vt_tbl_Tax_Rate_Role.FirstOrDefault(x => x.ID == ID);
            txtFROMTaxableIncome.Text = t.FROMTaxableIncome.ToString();
            txtToTaxableIncome.Text = t.ToTaxableIncome.ToString();
            txtRateOfTax.Text = t.RateOfTax.ToString();
            chkActive.Checked = t.IsActive;            
        }
        ViewState["PageID"] = ID;
    }

    #endregion
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {

    }
}