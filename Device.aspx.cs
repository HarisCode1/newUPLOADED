using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Device : System.Web.UI.Page
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

    protected void grdDevice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Device d = db.vt_tbl_Device.FirstOrDefault(x => x.DeviceID == ID);
                        db.vt_tbl_Device.Remove(d);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, d.DeviceName, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#device').modal();");
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
                vt_tbl_Device d = new vt_tbl_Device();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    d.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                
                else
                {
                    d.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                                
                d.DeviceName = txtName.Text;
                d.IP = txtIP.Text;
                d.Port = txtPort.Text;
                d.MachineNo = txtMachine.Text;
                d.DeviceCompany = ddlCompany.SelectedValue;
                d.Type = ddlType.SelectedValue;
                if (ViewState["PageID"] != null)
                {
                    d.DeviceID= vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_Device.Add(d);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, txtName.Text, "Successfully Save");
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


             vt_Common.ReloadJS(this.Page, "$('#device').modal();");
                

            

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#device').modal();");
        }
    }

    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetDevices(Convert.ToInt32(ddlCompany.SelectedValue));
            grdDevice.DataSource = Query;
            grdDevice.DataBind();
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
                    var Query = db.VT_SP_GetDevices(0).ToList();
                    grdDevice.DataSource = Query;
                    grdDevice.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetDevices(vt_Common.CompanyId).ToList();
                    grdDevice.DataSource = Query;
                    grdDevice.DataBind();
                }
            }
        }

        
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#device').modal('hide');");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Device d = db.vt_tbl_Device.FirstOrDefault(x => x.DeviceID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(d.CompanyID);
            txtName.Text = d.DeviceName;
            txtIP.Text = d.IP;
            txtMachine.Text = d.MachineNo;
            txtPort.Text = d.Port;
            ddlCompany.SelectedValue = d.DeviceCompany;
            ddlType.SelectedValue = d.Type;
        }
        ViewState["PageID"] = ID;
    }

    #endregion      
    
}