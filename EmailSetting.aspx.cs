using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;




public partial class EmailSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
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
                vt_tbl_EmailSettings emailSettings = new vt_tbl_EmailSettings();

                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    emailSettings.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }

                else
                {
                    emailSettings.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }

                
                emailSettings.FromEmailId = txtEmail.Text;
                emailSettings.Password = txtPassword.Text;
                
                emailSettings.SmtpServer = txtSmtp.Text;
                emailSettings.Port = txtPort.Text;

                //hdEmailSettingID.Value == null || hdEmailSettingID.Value == "" ||

                if ( ViewState["PageID"] != null)
                {
                    emailSettings.SettingID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(emailSettings).State = System.Data.Entity.EntityState.Modified;
                   
                }
                else
                {
                    db.vt_tbl_EmailSettings.Add(emailSettings);
                }
                
                
                    db.SaveChanges();
                
            }
            MsgBox.Show(Page, MsgBox.success, txtEmail.Text, "Successfully Save");
            ClearForm();
            loadData();
            UpView.Update();
        }
        catch (Exception ex)
        {

        }
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetEmailSetting(Convert.ToInt32(ddlCompany.SelectedValue));
            grdEmailSetting.DataSource = Query;
            grdEmailSetting.DataBind();
            UpView.Update();
        }

    }


    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();



        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {


           
          vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal();");
                
            

        }


        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal();");
        }
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

            if (Session["EMS_Session"] != null)
            {
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    if (((EMS_Session)Session["EMS_Session"]).Company == null)
                    {
                        var Query = db.VT_SP_GetEmailSetting(0).ToList();
                        grdEmailSetting.DataSource = Query;
                        grdEmailSetting.DataBind();
                    }
                    else
                    {
                        divCompany.Visible = false;
                        var Query = db.VT_SP_GetEmailSetting(vt_Common.CompanyId).ToList();
                        grdEmailSetting.DataSource = Query;
                        grdEmailSetting.DataBind();
                    }
                }
            }   
        
    }




    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_EmailSettings e = db.vt_tbl_EmailSettings.FirstOrDefault(x => x.SettingID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(e.CompanyID);
            txtEmail.Text = e.FromEmailId;
            txtPassword.Text = e.Password;
            txtPort.Text = e.Port;
            txtSmtp.Text = e.SmtpServer;
        }
        ViewState["PageID"] = ID;
    }


    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#EmailSetting').modal('hide');");
    }







    public void GetEmailSettings(int CompanyId)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_EmailSettings e = db.vt_tbl_EmailSettings.FirstOrDefault(x => x.CompanyID == CompanyId);
            if (CompanyId == 0)
            {
                var query = (from E in db.vt_tbl_EmailSettings
                             select new
                             {
                                 E.SettingID,
                                 E.SmtpServer,
                                 E.FromEmailId,
                                 E.Password,
                                 E.Port
                             }).ToList();
            }
            else
            {
                var query = (from E in db.vt_tbl_EmailSettings
                             join C in db.vt_tbl_Company on E.CompanyID equals C.CompanyID
                             where E.SettingID == CompanyId
                             select new
                             {
                                 E.SettingID,
                                 E.SmtpServer,
                                 E.FromEmailId,
                                 E.Password,
                                 E.Port
                             }).ToList();

            }
        }

    }
    #endregion



   
}