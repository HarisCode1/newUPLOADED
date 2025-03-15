using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.IO;
using System.Data;
public partial class EmailSetting : System.Web.UI.Page
{
    string Pagename = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }
    }
    #region ControlEvents
    protected void grdEmailSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_EmailSettings em = db.vt_tbl_EmailSettings.FirstOrDefault(x => x.SettingID == ID);
                        db.vt_tbl_EmailSettings.Remove(em);
                        db.SaveChanges();
                        loadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, em.FromEmailId, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal();");
                UpDetail.Update();

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
                vt_tbl_TaxMaster taxMaster = new vt_tbl_TaxMaster();

                //if (((EMS_Session)Session["EMS_Session"]).Company != null)
                //{
                //    emailSettings.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                //}

                //else
                //{
                //    emailSettings.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                //}
                taxMaster.CompanyID = Convert.ToInt32(Session["CompanyId"]);
                taxMaster.FirstCalculationRangeFrom = Convert.ToDecimal(txtRangeFrom.Text);
                taxMaster.FirstCalculationRageTo = Convert.ToDecimal(txtRangeTo.Text);
                if (string.IsNullOrWhiteSpace(txtPer.Text))
                {
                    taxMaster.FirstCalculationPercentageValue = null;
                }
                else taxMaster.FirstCalculationPercentageValue = Convert.ToDecimal(txtPer.Text);

                if (string.IsNullOrWhiteSpace(txtAmt.Text))
                {
                    taxMaster.FirstCalculationFixedValue = null;
                }
                else taxMaster.FirstCalculationFixedValue = Convert.ToDecimal(txtAmt.Text);

                if (string.IsNullOrWhiteSpace(TxtExeCond.Text))
                {
                    taxMaster.SecondCalculationExceding = null;
                }
                else taxMaster.SecondCalculationExceding = Convert.ToDecimal(TxtExeCond.Text);

                if (string.IsNullOrWhiteSpace(TxtExeAmt.Text))
                {
                    taxMaster.SecondCalculationFixedValue = null;
                }
                else taxMaster.SecondCalculationFixedValue = Convert.ToDecimal(TxtExeAmt.Text);
                //hdEmailSettingID.Value == null || hdEmailSettingID.Value == "" ||
                if (ViewState["PageID"] != null)
                {
                    taxMaster.TaxRangeID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(taxMaster).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_TaxMaster.Add(taxMaster);
                }
                db.SaveChanges();
            }
            MsgBox.Show(Page, MsgBox.success, "Tax Item Saved", "Successfully Save");
            ClearForm();
            loadData();
            UpView.Update();
        }
        catch (Exception ex)
        {
        }
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal();");
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
        //if (Session["EMS_Session"] != null)
        //{
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                var Query = db.VT_SP_GetTax(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                grdEmailSetting.DataSource = Query;
                grdEmailSetting.DataBind();
            }
        //}
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_TaxMaster e = db.vt_tbl_TaxMaster.FirstOrDefault(x => x.TaxRangeID == ID);
            //ddlcomp.SelectedValue = vt_Common.CheckString(e.CompanyID);
            txtRangeFrom.Text = e.FirstCalculationRangeFrom.ToString();
            txtRangeTo.Text = e.FirstCalculationRageTo.ToString();
            txtPer.Text = e.FirstCalculationPercentageValue.ToString();
            txtAmt.Text = e.FirstCalculationFixedValue.ToString();
            TxtExeCond.Text = e.SecondCalculationExceding.ToString();
            TxtExeAmt.Text = e.SecondCalculationFixedValue.ToString();

            //txtEmail.Text = e.FromEmailId;
            //txtPassword.Text = e.Password;
            //txtPort.Text = e.Port;
            //txtSmtp.Text = e.SmtpServer;
        }
        ViewState["PageID"] = ID;
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal('hide');");
    }
    #endregion
}