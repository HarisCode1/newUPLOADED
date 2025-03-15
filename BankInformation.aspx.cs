using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class BankInformation : System.Web.UI.Page
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

    protected void grdBankInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    int ID = vt_Common.CheckInt(e.CommandArgument);
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        vt_tbl_Bank b = db.vt_tbl_Bank.FirstOrDefault(x => x.BankID == ID);
                        db.vt_tbl_Bank.Remove(b);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, b.BankName, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#bankinformation').modal();");
                UpDetail.Update();

                break;
            default:
                break;
        }       

        
    }

    protected void grdBankInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        LoadData();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_Bank b = new vt_tbl_Bank();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    b.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }

                else
                {
                    b.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                
                b.BankName = txtName.Text;
                b.IFSCCode = txtIFSCCode.Text;
                b.Address = txtAddress.Text;
                if (ViewState["PageID"] != null)
                {
                    b.BankID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_Bank.Add(b);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, txtName.Text, "Successfully Save");
            ClearForm();
            LoadData();
            UpView.Update();
            UpDetail.Update();
        }
        catch (DbUpdateException ex)
        {

        }
    }

    protected void btnAddnew_Click1(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();




        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {


           
                vt_Common.ReloadJS(this.Page, "$('#bankinformation').modal();");
                
            

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#bankinformation').modal();");
        }

    }

   
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetBanks(Convert.ToInt32(ddlCompany.SelectedValue));
            grdBankInfo.DataSource = Query;
            grdBankInfo.DataBind();
            UpView.Update();


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
                    var Query = db.VT_SP_GetBanks(0).ToList();
                    grdBankInfo.DataSource = Query;
                    grdBankInfo.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetBanks(vt_Common.CompanyId).ToList();
                    grdBankInfo.DataSource = Query;
                    grdBankInfo.DataBind();
                }
            }

        }
    }
    

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#bankinformation').modal('hide');");

    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Bank c = db.vt_tbl_Bank.FirstOrDefault(x => x.BankID == ID);
            //ddlCompany.SelectedValue = c.CompanyID.ToString();
            ddlcomp.SelectedValue = vt_Common.CheckString(c.CompanyID);
            txtName.Text = c.BankName;
            txtIFSCCode.Text = c.IFSCCode;
            txtAddress.Text = c.Address;
        }
        ViewState["PageID"] = ID;
    }

    #endregion

    
}