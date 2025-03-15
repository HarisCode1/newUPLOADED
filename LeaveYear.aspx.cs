using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class LeaveYear : System.Web.UI.Page
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
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_LeaveYear l = db.vt_tbl_LeaveYear.FirstOrDefault(x => x.LeaveYearID == ID);
                        db.vt_tbl_LeaveYear.Remove(l);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, l.Year, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#leaveyear').modal();binddata();");
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
                vt_tbl_LeaveYear l = new vt_tbl_LeaveYear();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    l.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }

                else
                {
                    l.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }




                
                l.Year = txtLeaveYear.Text;

                if (ViewState["PageID"] != null)
                {
                    l.LeaveYearID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_LeaveYear.Add(l);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, txtLeaveYear.Text, "Successfully Save");
            ClearForm();
            LoadData();
            UpView.Update();
        }
        catch (DbUpdateException ex)
        {

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
                    var Query = db.VT_SP_GetLeaveYear(0).ToList();
                    grdLeaveYear.DataSource = Query;
                    grdLeaveYear.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    //BtnAddNew.Visible = false;
                    var Query = db.VT_SP_GetLeaveYear(vt_Common.CompanyId).ToList();
                    grdLeaveYear.DataSource = Query;
                    grdLeaveYear.DataBind();
                }
            }
        }


    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#leaveyear').modal('hide');binddata();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_LeaveYear l = db.vt_tbl_LeaveYear.FirstOrDefault(x => x.LeaveYearID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(l.CompanyID);
            txtLeaveYear.Text = l.Year;
        }
        ViewState["PageID"] = ID;
    }

    #endregion      
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetLeaveYear(Convert.ToInt32(ddlCompany.SelectedValue));
            grdLeaveYear.DataSource = Query;
            grdLeaveYear.DataBind();
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


            
           
          vt_Common.ReloadJS(this.Page, "$('#leaveyear').modal();");
               
            

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#leaveyear').modal('hide');");
        }

    }
}