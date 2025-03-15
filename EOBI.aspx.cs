using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EOBI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }


    #region Control Events
    protected void grdEOBISetting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_ESICSettings b = db.vt_tbl_ESICSettings.FirstOrDefault(x => x.ESICSettingID == ID);
                        db.vt_tbl_ESICSettings.Remove(b);
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
                vt_Common.ReloadJS(this.Page, "$('#EOBI').modal();");
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
                vt_tbl_ESICSettings b = new vt_tbl_ESICSettings();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    b.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                
                else 
                {
                    b.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                b.ApplicableFrom = vt_Common.CheckDateTime(txtAppFrom.Text);
                b.EmployeesShare = txtEmployeesShare.Text;
                b.EmployersShare = txtEmployersShare.Text;
                b.ESICLimit = txtEOBILimit.Text;

                if (ViewState["PageID"] != null)
                {
                    b.ESICSettingID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_ESICSettings.Add(b);
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


           
             vt_Common.ReloadJS(this.Page, "$('#EOBI').modal();");
                

            

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#EOBI').modal();");
        }

    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetEOBISettings(Convert.ToInt32(ddlCompany.SelectedValue));
            grdEOBISetting.DataSource = Query;
            grdEOBISetting.DataBind();
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
                    var Query = db.VT_SP_GetEOBISettings(0).ToList();
                    grdEOBISetting.DataSource = Query;
                    grdEOBISetting.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetEOBISettings(vt_Common.CompanyId).ToList();
                    grdEOBISetting.DataSource = Query;
                    grdEOBISetting.DataBind();
                }
            }
        }
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#EOBI').modal('hide');");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_ESICSettings b = db.vt_tbl_ESICSettings.FirstOrDefault(x => x.ESICSettingID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(b.CompanyID);
            txtAppFrom.Text = b.ApplicableFrom.ToString();
            txtEmployeesShare.Text = b.EmployeesShare;
            txtEmployersShare.Text = b.EmployersShare;
            txtEOBILimit.Text = b.ESICLimit;
        }
        ViewState["PageID"] = ID;
    }
    #endregion
    
}