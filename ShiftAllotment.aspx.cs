using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.Data;
using System.IO;

public partial class ShiftAllotment : System.Web.UI.Page
{
    string Pagename = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata()");
       
    }

    #region Control Event

    protected void grdshiftAllotment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_ShiftAllotment sa = db.vt_tbl_ShiftAllotment.FirstOrDefault(x => x.ShiftAllotmentID == ID);
                        db.vt_tbl_ShiftAllotment.Remove(sa);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, sa.Date.ToString(), "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#shiftallotment').modal();binddata();");
                UpDetail.Update();
                break;
            default:
                break;
        }

        
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_ShiftAllotment sa = new vt_tbl_ShiftAllotment();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    sa.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                else
                {
                    sa.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }

                sa.EnrollId = vt_Common.CheckInt(ddlEmployee.SelectedValue);
                sa.Date = DateTime.Parse(txtAppFromDate.Text);
                sa.Shift = ddlShift.Text;
                
         
                if (ViewState["PageID"] != null)
                {
                    sa.ShiftAllotmentID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(sa).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_ShiftAllotment.Add(sa);
                }

                db.SaveChanges();
            }
            
            MsgBox.Show(Page, MsgBox.success, txtAppFromDate.Text, "Successfully Save");
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

          vt_Common.ReloadJS(this.Page, "$('#shiftallotment').modal();");     
        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#shiftallotment').modal();");

        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetShiftAllotments(Convert.ToInt32(ddlCompany.SelectedValue));
            grdshiftAllotment.DataSource = Query;
            grdshiftAllotment.DataBind();
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
                    var Query = db.VT_SP_GetShiftAllotments(0).ToList();
                    grdshiftAllotment.DataSource = Query;
                    grdshiftAllotment.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetShiftAllotments(vt_Common.CompanyId).ToList();
                    grdshiftAllotment.DataSource = Query;
                    grdshiftAllotment.DataBind();
                }
            
            }


        }

    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        
        vt_Common.ReloadJS(this.Page, "$('#shiftallotment').modal('hide');binddata();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_ShiftAllotment sa = db.vt_tbl_ShiftAllotment.FirstOrDefault(x => x.ShiftAllotmentID == ID);
            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == sa.EnrollId);
            ddlcomp.SelectedValue = vt_Common.CheckString(sa.CompanyID);
            txtAppFromDate.Text = vt_Common.CheckString(sa.Date);
            ddlEmployee.SelectedValue = vt_Common.CheckString(sa.EnrollId);
            ddlShift.Text = vt_Common.CheckString(sa.Shift);
            
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
            
            UpView.Update();
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