using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class PT : System.Web.UI.Page
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

    protected void grdPTSettings_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_PTSettings b = db.vt_tbl_PTSettings.FirstOrDefault(x => x.PTSettingsID == ID);
                        db.vt_tbl_PTSettings.Remove(b);
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
                vt_Common.ReloadJS(this.Page, "$('#PT').modal();");
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
                vt_tbl_PTSettings b = new vt_tbl_PTSettings();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    b.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }

                else
                {
                    b.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                
                b.State = ddlState.SelectedValue;
                b.MinSalary = vt_Common.Checkdecimal(txtMinSalary.Text);
                b.MaxSalary = vt_Common.Checkdecimal(txtMaxSalary.Text);
                b.PTax = txtPTax.Text;
                b.SplCalcApplicable = chkSpecialCalcApplicable.Checked;

                if (ViewState["PageID"] != null)
                {
                    b.PTSettingsID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_PTSettings.Add(b);
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


            
          vt_Common.ReloadJS(this.Page, "$('#PT').modal();");
                

            

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#PT').modal();");
        }

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetPTSettings(Convert.ToInt32(ddlCompany.SelectedValue));
            grdPTSettings.DataSource = Query;
            grdPTSettings.DataBind();
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
                    var Query = db.VT_SP_GetPTSettings(0).ToList();
                    grdPTSettings.DataSource = Query;
                    grdPTSettings.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetPTSettings(vt_Common.CompanyId).ToList();
                    grdPTSettings.DataSource = Query;
                    grdPTSettings.DataBind();
                }
            }
        }
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#PT').modal('hide');");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_PTSettings b = db.vt_tbl_PTSettings.FirstOrDefault(x => x.PTSettingsID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(b.CompanyID);
            ddlState.SelectedValue = b.State;
            txtMinSalary.Text = b.MinSalary.ToString();
            txtMaxSalary.Text = b.MaxSalary.ToString();
            txtPTax.Text = b.PTax;
            chkSpecialCalcApplicable.Checked = b.SplCalcApplicable;
        }
        ViewState["PageID"] = ID;
    }
    #endregion
    
}