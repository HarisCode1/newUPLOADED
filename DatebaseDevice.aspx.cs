using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class DatebaseDevice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "bindLoadData();");
    }

    #region Control Event

    protected void grdDatabaseDevice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_NACDeviceConfig n = db.vt_tbl_NACDeviceConfig.FirstOrDefault(x => x.NacDevID == ID);
                        db.vt_tbl_NACDeviceConfig.Remove(n);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, n.DeviceName, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#databasedevice').modal();");
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
                vt_tbl_NACDeviceConfig d = new vt_tbl_NACDeviceConfig();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    d.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                
                else
                {
                    d.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                d.DeviceName = txtDeviceName.Text;
                //d.ConStr = 
                d.DeviceCompany = ddlCompany.SelectedValue;
                d.DBType = ddlType.SelectedValue;
                if (ViewState["PageID"] != null)
                {
                    d.NacDevID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_NACDeviceConfig.Add(d);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, txtDeviceName.Text, "Successfully Save");
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


           
           
                vt_Common.ReloadJS(this.Page, "$('#databasedevice').modal();");
                

            

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#databasedevice').modal();");
        }

    }
    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetDatabaseDevices(Convert.ToInt32(ddlCompany.SelectedValue));
            grdDatabaseDevice.DataSource = Query;
            grdDatabaseDevice.DataBind();
            UpView.Update();


        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    protected void btnSaveMySQL_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnectionStringBuilder TanentConStr = new SqlConnectionStringBuilder();
            string ConStr = TanentConStr.ConnectionString;
            //data source=VIFTECH-SERVER\SQLEXPRESS;initial catalog=vt_EMS;persist security info=True;user id=sajjad;password=SrDev2123net!;          MultipleActiveResultSets=True;          App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
            string checkConStr = "data source=" + txtServerName.Text + ";initial catalog=" + txtDatabase.Text + ";persist security info=True;user id=" + txtUserName.Text + ";password=" + txtPassword.Text + ";";
            if (checkConStr == TanentConStr.ConnectionString)
            {
                TanentConStr.IntegratedSecurity = false;
                TanentConStr.InitialCatalog = txtDatabase.Text;
                TanentConStr.DataSource = txtServerName.Text;
                TanentConStr.UserID = txtUserName.Text;
                TanentConStr.Password = txtPassword.Text;

                TanentConStr.ConnectionString = "";
            }
            else
            {

            }
            //using (vt_EMSEntities db = new vt_EMSEntities())
            //{
            //    vt_tbl_NACDeviceConfig d = new vt_tbl_NACDeviceConfig();
            //    d.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
            //    d.DeviceName = txtDeviceName.Text;
            //    //d.ConStr = 
            //    d.DeviceCompany = ddlCompany.SelectedValue;
            //    d.DBType = ddlType.SelectedValue;
            //    if (ViewState["PageID"] != null)
            //    {
            //        d.NacDevID = vt_Common.CheckInt(ViewState["PageID"]);
            //        db.Entry(d).State = System.Data.Entity.EntityState.Modified;
            //    }
            //    else
            //    {
            //        db.vt_tbl_NACDeviceConfig.Add(d);
            //    }

            //    db.SaveChanges();
            //}

            //MsgBox.Show(Page, MsgBox.success, txtDeviceName.Text, "Successfully Save");
            //ClearForm();
            //LoadData();
            //UpView.Update();
        }
        catch (DbUpdateException ex)
        {

        }
    }

    protected void btnDispose_Click(object sender, EventArgs e)
    {
        ClearFormMySQL();
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
                    var Query = db.VT_SP_GetDatabaseDevices(0).ToList();
                    grdDatabaseDevice.DataSource = Query;
                    grdDatabaseDevice.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetDatabaseDevices(vt_Common.CompanyId).ToList();
                    grdDatabaseDevice.DataSource = Query;
                    grdDatabaseDevice.DataBind();
                }
            }


        }
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#databasedevice').modal('hide');");
    }
    void ClearFormMySQL()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlMySQL.Controls);
        vt_Common.ReloadJS(this.Page, "$('#mysqldialog').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_NACDeviceConfig n = db.vt_tbl_NACDeviceConfig.FirstOrDefault(x => x.NacDevID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(n.CompanyID);
            txtDeviceName.Text = n.DeviceName;
            ddlCompany.SelectedValue = n.DeviceCompany;
            ddlType.SelectedValue = n.DBType;
        }
        ViewState["PageID"] = ID;
    }

    #endregion      
    
}