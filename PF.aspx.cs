using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class PF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }

    #region ControlEvents
    protected void grdPFSetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_PFSettings b = db.vt_tbl_PFSettings.FirstOrDefault(x => x.PFSettingID == ID);
                        db.vt_tbl_PFSettings.Remove(b);
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
            case "EditCompany":
                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                vt_Common.ReloadJS(this.Page, "$('#PF').modal();");
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
                vt_tbl_PFSettings b = new vt_tbl_PFSettings();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    b.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                
                else
                {
                    b.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                b.MonthYear = txtAppFrom.Text;
                b.EmployeesShare = txtEmployeesShare.Text;
                b.FixEmployerShare = chkFixEmployerShare.Checked;
                b.EmployersShare = txtEmployersShare.Text;
                b.PensionFund = txtPensionFund.Text;
                b.PFLimitRs = txtPFLimit.Text;
                b.PFAdminCharges = txtPFAdminCh.Text;
                b.EDLICharges = txtEDLICharges.Text;
                b.EDLIAdminCharges = txtEDLIAdminCharges.Text;

                if (ViewState["PageID"] != null)
                {
                    b.PFSettingID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_PFSettings.Add(b);
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

        }
    }

    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();


        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {


            
                vt_Common.ReloadJS(this.Page, "$('#PF').modal();");
                
        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#PF').modal();");
        }

    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetPFSettings(Convert.ToInt32(ddlCompany.SelectedValue));
            grdPFSetting.DataSource = Query;
            grdPFSetting.DataBind();
            UpView.Update();


        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    #endregion


    #region Helper Method

    void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (((EMS_Session)Session["EMS_Session"]).Company == null)
                {
                    var Query = db.VT_SP_GetPFSettings(0).ToList();
                    grdPFSetting.DataSource = Query;
                    grdPFSetting.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetPFSettings(vt_Common.CompanyId).ToList();
                    grdPFSetting.DataSource = Query;
                    grdPFSetting.DataBind();
                }
            }
            
        }
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#PF').modal('hide');");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_PFSettings b = db.vt_tbl_PFSettings.FirstOrDefault(x => x.PFSettingID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(b.CompanyID);
            txtAppFrom.Text = b.MonthYear;
            txtEmployeesShare.Text = b.EmployeesShare;
            chkFixEmployerShare.Checked = b.FixEmployerShare;
            txtEmployersShare.Text = b.EmployersShare;
            txtPensionFund.Text = b.PensionFund;
            txtPFLimit.Text = b.PFLimitRs;
            txtPFAdminCh.Text = b.PFAdminCharges;
            txtEDLICharges.Text = b.EDLICharges;
            txtEDLIAdminCharges.Text = b.EDLIAdminCharges;
        }
        ViewState["PageID"] = ID;
    }
    #endregion
    
}