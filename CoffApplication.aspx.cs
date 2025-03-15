using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class CoffApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page,"binddata();");
    }

    #region Control Event

    protected void grdCoffApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_COFFApplication ca = db.vt_tbl_COFFApplication.FirstOrDefault(x => x.COFFApplicationId == ID);
                        db.vt_tbl_COFFApplication.Remove(ca);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, ca.TotalCOFF, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#Coffapplication').modal();binddata();");
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
                vt_tbl_COFFApplication ca = new vt_tbl_COFFApplication();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    ca.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                else
                {
                    ca.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                ca.Date = vt_Common.CheckDateTime(txtDate.Text);
                ca.EnrollId = vt_Common.CheckInt(ddlEmployee.Text);
                ca.FromDate = vt_Common.CheckDateTime(txtFromDate.Text);
                ca.ToDate = vt_Common.CheckDateTime(txtToDate.Text);
                ca.IsFromHalfDay = chkFromHalfDay.Checked;
                ca.IsToHalfDay = chkToHalfDay.Checked;
                ca.TotalCOFF = txtTotalLeaves.Text;
                ca.Reason = txtReason.Text;
                ca.Balance = txtBalance.Text;

                if (ViewState["PageID"] != null)
                {
                    ca.COFFApplicationId = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(ca).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_COFFApplication.Add(ca);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, txtTotalLeaves.Text, "Successfully Saved");
            CcaarForm();
            LoadData();
            UpView.Update();
        }
        catch (DbUpdateException ex)
        {

        }
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetCOFFApplications(Convert.ToInt32(ddlCompany.SelectedValue));
            grdCoffApplication.DataSource = Query;
            grdCoffApplication.DataBind();
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


           
         vt_Common.ReloadJS(this.Page, "$('#Coffapplication').modal();");
                

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#Coffapplication').modal();");

        }

    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        CcaarForm();
    }
    #endregion

    #region Healper Method

    [WebMethod]
    [ScriptMethod]
    public static dynamic getEmpList()
    {   
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //return db.vt_tbl_Employee.ToList().Where(x =>x.CompanyID==vt_Common.CompanyId).Select(x=> new string[] { x.EmployeeID.ToString(), x.EmployeeName });
            return db.vt_tbl_Employee.ToList().Select(x => new string[] { x.EmployeeID.ToString(), x.EmployeeName });
        }       
    }

    void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {

            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                    if (((EMS_Session)Session["EMS_Session"]).Company == null)
                    {
                        var Query = db.VT_SP_GetCOFFApplications(0).ToList();
                        grdCoffApplication.DataSource = Query;
                        grdCoffApplication.DataBind();
                    }
                    else
                    {
                        divCompany.Visible = false;
                        var Query = db.VT_SP_GetCOFFApplications(vt_Common.CompanyId).ToList();
                        grdCoffApplication.DataSource = Query;
                        grdCoffApplication.DataBind();
                    }

            }

        }
    }

    void CcaarForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#Coffapplication').modal('hide');binddata();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
         
        {
            vt_tbl_COFFApplication ca = db.vt_tbl_COFFApplication.FirstOrDefault(x => x.COFFApplicationId == ID);
            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ca.EnrollId);
            ddlcomp.SelectedValue = vt_Common.CheckString(ca.CompanyID);
            txtDate.Text = vt_Common.CheckString(ca.Date);
            ddlEmployee.SelectedValue = vt_Common.CheckString(ca.EnrollId);
            txtBalance.Text = ca.Balance;
            txtFromDate.Text = vt_Common.CheckString(ca.FromDate);
            txtToDate.Text = vt_Common.CheckString(ca.ToDate);
            txtReason.Text = ca.Reason;
            txtTotalLeaves.Text = ca.TotalCOFF;
            chkFromHalfDay.Checked = (bool)ca.IsFromHalfDay;
            chkToHalfDay.Checked = (bool)ca.IsToHalfDay;
        }
        ViewState["PageID"] = ID;
    }


    public void BindEmployeeGrid(int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var EmployeeList = db.VT_SP_GetEmployees(CompanyID).ToList();

            ddlEmployee.DataSource = EmployeeList;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
        }
    }




    #endregion      
   
    
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {

            if (ddlcomp.SelectedValue != "0")
            {
                BindEmployeeGrid(Convert.ToInt32(ddlcomp.SelectedValue));
            }
            else
            {
                ddlEmployee.Items.Clear();
            }
        }

    }
}