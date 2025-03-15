using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.IO;
using System.Data;

public partial class Shift : System.Web.UI.Page
{
    string Pagename = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page,"bindLoadData();");
     
    }

    #region Control Events
    protected void grdShift_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Shift s = db.vt_tbl_Shift.FirstOrDefault(x => x.ShiftID == ID);
                        db.vt_tbl_Shift.Remove(s);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, s.ShiftName, "Successfully Deleted");
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
                //vt_Common.ReloadJS(this.Page, "$('#shift').modal();bindLoadData();");
                vt_Common.ReloadJS(this.Page, "$('#shift').modal();");
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
                vt_tbl_Shift s = new vt_tbl_Shift();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                   s.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                
                else
                {
                    s.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                
                s.ShiftName = txtName.Text;
                s.ShiftShortName = txtShortName.Text;
                s.InTime = txtInTime.Text;
                s.LateAllowed = txtLateAllowed.Text;
                s.OutTime = txtOutTime.Text;
                s.EarlyAllowed = txtEarlyAllowed.Text;
                s.FullDayMinutes = txtFullDayMinutes.Text;
                s.HalfDayMinutes = txtHalfDayMinutes.Text;
                s.IsFixShift = chkFixShift.Checked;
                if (!s.IsFixShift)
                {
                    s.BeforeTime = txtBeforeTime.Text;
                    s.AfterTime = txtAfterTime.Text;
                }
                s.OTAllowed = chkOT.Checked;
                if (s.OTAllowed)
                {
                    s.GracePeriod = txtGracePeriod.Text;
                    s.OneDayHrs = txtOneDayHrs.Text;
                    s.MaxOTHrs = txtMaxOTHrs.Text;
                }
                s.LunchApplicable = chkLunchApplicable.Checked;
                if (s.LunchApplicable)
                {
                    s.LunchOut = txtLunchOut.Text;
                    s.LunchIn = txtLunchIn.Text;
                }
                else
                {
                    s.FixLunchMins = txtFixLunchMins.Text;
                }
                s.BreakApplicable = chkBreakApplicable.Checked;
                if (s.BreakApplicable)
                {
                    s.BreakOut = txtBreakOut.Text;
                    s.BreakIn = txtBreakIn.Text;
                }
                s.IsEndsOnNextDay = chkEndsOnNextDay.Checked;

                if (ViewState["PageID"] != null)
                {
                    s.ShiftID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_Shift.Add(s);
                }

                db.SaveChanges();
                MsgBox.Show(Page, MsgBox.success, s.ShiftName, "Successfully Save");
            }

            ClearForm();
            LoadData();
            UpView.Update();
        }
        catch (DbUpdateException ex)
        {

        }
    }


    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();


        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {


               vt_Common.ReloadJS(this.Page, "$('#shift').modal();");
                
            
        }

        else
        {
            ddlCompany.Visible = false;
            trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#shift').modal();");
        }

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetShift(Convert.ToInt32(ddlCompany.SelectedValue));
            grdShift.DataSource = Query;
            grdShift.DataBind();
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
                    var Query = db.VT_SP_GetShift(0).ToList();
                    grdShift.DataSource = Query;
                    grdShift.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetShift(vt_Common.CompanyId).ToList();
                    grdShift.DataSource = Query;
                    grdShift.DataBind();
                }
            }
            
        }
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#shift').modal('hide');bindLoadData();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Shift s = db.vt_tbl_Shift.FirstOrDefault(x => x.ShiftID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(s.CompanyID);
            txtName.Text = s.ShiftName;
            txtShortName.Text = s.ShiftShortName;
            txtInTime.Text = s.InTime;
            txtLateAllowed.Text = s.LateAllowed;
            txtOutTime.Text = s.OutTime;
            txtEarlyAllowed.Text = s.EarlyAllowed;
            txtFullDayMinutes.Text = s.FullDayMinutes;
            txtHalfDayMinutes.Text = s.HalfDayMinutes;
            chkFixShift.Checked = s.IsFixShift;
            if (!s.IsFixShift)
            {
                txtBeforeTime.Text = s.BeforeTime;
                txtAfterTime.Text = s.AfterTime;
            }
            chkOT.Checked = s.OTAllowed;
            if (s.OTAllowed)
            {
                txtGracePeriod.Text = s.GracePeriod;
                txtOneDayHrs.Text = s.OneDayHrs;
                txtMaxOTHrs.Text = s.MaxOTHrs;
            }
            chkLunchApplicable.Checked = s.LunchApplicable;
            if (s.LunchApplicable)
            {
                txtLunchOut.Text = s.LunchOut;
                txtLunchIn.Text = s.LunchIn;
            }
            else
            {
                txtFixLunchMins.Text = s.FixLunchMins;
            }
            chkBreakApplicable.Checked = s.BreakApplicable;
            if (s.BreakApplicable)
            {
                txtBreakOut.Text = s.BreakOut;
                txtBreakIn.Text = s.BreakIn;
            }
            chkEndsOnNextDay.Checked = s.IsEndsOnNextDay;
        }
        ViewState["PageID"] = ID;
    }
    #endregion
    
}