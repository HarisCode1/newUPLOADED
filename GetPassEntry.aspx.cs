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

public partial class GetPassEntry : System.Web.UI.Page
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
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_GatePassEntry gpe = new vt_tbl_GatePassEntry();

                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    gpe.CompanyID = vt_Common.CompanyId;
                }
                else
                {
                    gpe.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                gpe.EnrollId = Convert.ToInt32(ddlEmployee.SelectedValue);
                gpe.Date = vt_Common.CheckDateTime(txtDate.Text);
                gpe.FromTime = txtFromTime.Text;
                gpe.ToTime = txtToTime.Text;
                gpe.TotalHrs = txtDuration.Text;
                gpe.Reason = ddlReason.SelectedValue;
                gpe.Remark = txtRemark.Text;
                gpe.ApprovedByName = txtApprovedBy.Text;

                if (ViewState["PageID"] != null)
                {
                    gpe.GatePassID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(gpe).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_GatePassEntry.Add(gpe);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, ddlReason.SelectedValue, "Successfully Save");
            ClearForm();
            LoadData();
            UpView.Update();

        }
        catch (DbUpdateException ex)
        {

        }
    }

    protected void grdGatePass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_GatePassEntry gpe = db.vt_tbl_GatePassEntry.FirstOrDefault(x => x.GatePassID== ID);
                        db.vt_tbl_GatePassEntry.Remove(gpe);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, gpe.GatePassID.ToString(), "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#getpassentry').modal();");
                upMAttendance.Update();
                break;
            default:
                break;
        }
    }


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        upMAttendance.Update();


        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {


          
                
          
           
          vt_Common.ReloadJS(this.Page, "$('#getpassentry').modal();");
               

           

        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#getpassentry').modal();");

        }


    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetGatePassEntries(Convert.ToInt32(ddlCompany.SelectedValue));
            grdGatePass.DataSource = Query;
            grdGatePass.DataBind();
            UpView.Update();
        }

    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    
    

    #endregion

    #region Helper Method
    public void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                if (((EMS_Session)Session["EMS_Session"]).Company == null)
                {
                    var Query = db.VT_SP_GetGatePassEntries(0).ToList();
                    grdGatePass.DataSource = Query;
                    grdGatePass.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetGatePassEntries(vt_Common.CompanyId).ToList();
                    grdGatePass.DataSource = Query;
                    grdGatePass.DataBind();
                }
               
            }
        }
    }

    [WebMethod]
    [ScriptMethod]
    public static dynamic getEmpList()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            return db.vt_tbl_Employee.ToList().Select(x => new string[] { x.EmployeeID.ToString(), x.EmployeeName });
        }
    }


    public void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#getpassentry').modal('hide');");
    }


    public void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_GatePassEntry gpe = db.vt_tbl_GatePassEntry.FirstOrDefault(x => x.GatePassID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(gpe.CompanyID);
            ddlEmployee.SelectedValue = vt_Common.CheckString(gpe.EnrollId);
            txtDate.Text = gpe.Date.ToString();
            txtToTime.Text = gpe.ToTime;
            txtFromTime.Text = gpe.FromTime;
            txtDuration.Text = gpe.TotalHrs;
            txtRemark.Text = gpe.Remark;
            txtApprovedBy.Text = gpe.ApprovedByName;
            ddlReason.SelectedValue = gpe.Reason;
            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == gpe.EnrollId);
            
            
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