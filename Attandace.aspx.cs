using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Setting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadForm();
        }
        vt_Common.ReloadJS(this.Page, "bindLoadData();");
    }

    #region Helper Method


    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Settings s = db.vt_tbl_Settings.Where(x => x.CompanyID == ID).SingleOrDefault();

            if (s != null)
            {

                txtFullDayMins.Text = s.ShiftTotalMin;
                txtHalfDayMins.Text = s.ShiftHalfDayMin;
                txtLateAllowedMin.Text = s.LateAllowedMin;
                txtEarlyAllowdMin.Text = s.EarlyAllowedMin;
                ddlWeeklyOff.SelectedValue = s.WeeklyOff;
                chkSecondWeeklyOff.Checked = s.IsSecondweeklyOff;
                if (s.IsSecondweeklyOff && (chkSecondWeeklyOff.Checked = true))
                {
                    ddlSecondWeeklyOff.SelectedValue = s.SecondWeeklyOff;
                    rdoSecondWeeklyOffStatus.SelectedValue = s.SecondWeeklyOffStatus;
                    foreach (var item in s.SecondWeeklyOffPattern.Split(','))
                    {
                        chkSecondWeeklyOffPattern.Items.FindByValue(item.Trim()).Selected = true;
                    }
                }
                else
                {
                    ddlSecondWeeklyOff.SelectedIndex = 0;
                    rdoSecondWeeklyOffStatus.ClearSelection();
                    chkSecondWeeklyOffPattern.ClearSelection();
                }
                rdopunch.SelectedValue = s.DevicePunch;
                if (!s.DevicePunch.Equals("Single"))
                {
                    txtTolleranceMin.Text = s.TolleranceMins.ToString();
                    ddlNoOutPunchFound.SelectedValue = s.NoOutPunchFound;
                }
                chkLateCut.Checked = s.IsLateCutAllowed;
                if (s.IsLateCutAllowed && (chkLateCut.Checked=true))
                {
                    txtLateAllowedMin.Text = s.LateAllowedMin;
                    ddlLateCutActions.SelectedValue = s.LateCutAction;
                }
                chkOT.Checked = s.IsOTAllowed;
                if (s.IsOTAllowed && (chkOT.Checked=true))
                {
                    txtOtGracePeriod.Text = s.OTStartMinute;
                    txtOt1DayHrs.Text = s.OTDayHrs;
                }
                chkIsLunch.Checked = s.IsLunchAllowed;
                ddlWOFFAction.SelectedValue = s.IfPresentOnWOFForHOFF;
                txtMaxOtHrs.Text = s.MaxOtHrs;

            }
            

            else
            {
                txtFullDayMins.Text = "";
                txtHalfDayMins.Text = "";
                txtLateAllowedMin.Text = "";
                txtEarlyAllowdMin.Text = "";
                ddlWeeklyOff.SelectedIndex = 0;
                chkSecondWeeklyOff.Checked = false;
               
                    ddlSecondWeeklyOff.SelectedIndex = 0;
                    rdoSecondWeeklyOffStatus.SelectedIndex = 0;
                    chkSecondWeeklyOffPattern.ClearSelection();

                    rdopunch.ClearSelection();
                    
                        txtTolleranceMin.Text = "";
                        ddlNoOutPunchFound.SelectedIndex = 0;
                    

                     chkLateCut.Checked = false;
                    

                        txtLateAllowedMin.Text = "";
                        ddlLateCutActions.SelectedIndex = 0;



                        chkOT.Checked = false;
                    
                        txtOtGracePeriod.Text = "";
                        txtOt1DayHrs.Text = "";
                    
                chkIsLunch.Checked = false;
                ddlWOFFAction.SelectedIndex = 0;
                txtMaxOtHrs.Text = "";
            }

            

        }
        ViewState["PageID"] = ID;
    }

    void LoadForm()
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                
                
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {

                    int CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                    vt_tbl_Settings s = db.vt_tbl_Settings.Where(x => x.CompanyID == CompanyID).SingleOrDefault();

                    divCompany.Visible = false;
                    btnSave.Visible = false;

                    if (s != null)
                    {
                        txtFullDayMins.Text = s.ShiftTotalMin;
                        txtHalfDayMins.Text = s.ShiftHalfDayMin;
                        txtLateAllowedMin.Text = s.LateAllowedMin;
                        txtEarlyAllowdMin.Text = s.EarlyAllowedMin;
                        ddlWeeklyOff.SelectedValue = s.WeeklyOff;
                        chkSecondWeeklyOff.Checked = s.IsSecondweeklyOff;
                        if (s.IsSecondweeklyOff )
                        {
                            ddlSecondWeeklyOff.SelectedValue = s.SecondWeeklyOff;
                            rdoSecondWeeklyOffStatus.SelectedValue = s.SecondWeeklyOffStatus;
                            foreach (var item in s.SecondWeeklyOffPattern.Split(','))
                            {
                                chkSecondWeeklyOffPattern.Items.FindByValue(item.Trim()).Selected = true;
                            }
                        }
                        rdopunch.SelectedValue = s.DevicePunch;
                        if (!s.DevicePunch.Equals("Single"))
                        {
                            txtTolleranceMin.Text = s.TolleranceMins.ToString();
                            ddlNoOutPunchFound.SelectedValue = s.NoOutPunchFound;
                        }
                        chkLateCut.Checked = s.IsLateCutAllowed;
                        if (s.IsLateCutAllowed)
                        {
                            txtLateAllowedMin.Text = s.LateAllowedMin;
                            ddlLateCutActions.SelectedValue = s.LateCutAction;
                        }
                        chkOT.Checked = s.IsOTAllowed;
                        if (s.IsOTAllowed)
                        {
                            txtOtGracePeriod.Text = s.OTStartMinute;
                            txtOt1DayHrs.Text = s.OTDayHrs;
                        }
                        chkIsLunch.Checked = s.IsLunchAllowed;
                        ddlWOFFAction.SelectedValue = s.IfPresentOnWOFForHOFF;
                        txtMaxOtHrs.Text = s.MaxOtHrs;

                        ViewState["PageID"] = s.SettingID;
                    }


                }

                else
                {

                    var companies = (from vt_tbl_Company c in db.vt_tbl_Company
                                     orderby c.CompanyName
                                     select new { c.CompanyID, c.CompanyName }).ToList();

                    ddlCompany.DataValueField = "CompanyID";
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataSource = companies;
                    DataBind();
                    ddlCompany.SelectedIndex = 0;

                    int  CompanyID=Convert.ToInt32(ddlCompany.SelectedValue);
                    vt_tbl_Settings s = db.vt_tbl_Settings.Where(x => x.CompanyID == CompanyID).SingleOrDefault();

                    if (s != null)
                    {
                        txtFullDayMins.Text = s.ShiftTotalMin;
                        txtHalfDayMins.Text = s.ShiftHalfDayMin;
                        txtLateAllowedMin.Text = s.LateAllowedMin;
                        txtEarlyAllowdMin.Text = s.EarlyAllowedMin;
                        ddlWeeklyOff.SelectedValue = s.WeeklyOff;
                        chkSecondWeeklyOff.Checked = s.IsSecondweeklyOff;
                        if (s.IsSecondweeklyOff && (chkSecondWeeklyOff.Checked = true))
                        {
                            ddlSecondWeeklyOff.SelectedValue = s.SecondWeeklyOff;
                            rdoSecondWeeklyOffStatus.SelectedValue = s.SecondWeeklyOffStatus;
                            foreach (var item in s.SecondWeeklyOffPattern.Split(','))
                            {
                                chkSecondWeeklyOffPattern.Items.FindByValue(item.Trim()).Selected = true;
                            }
                        }
                        else
                        {
                            ddlSecondWeeklyOff.ClearSelection();
                            rdoSecondWeeklyOffStatus.ClearSelection();
                            chkSecondWeeklyOffPattern.ClearSelection();
                        }

                        rdopunch.SelectedValue = s.DevicePunch;
                        if (!s.DevicePunch.Equals("Single"))
                        {
                            txtTolleranceMin.Text = s.TolleranceMins.ToString();
                            ddlNoOutPunchFound.SelectedValue = s.NoOutPunchFound;
                        }
                        chkLateCut.Checked = s.IsLateCutAllowed;
                        if (s.IsLateCutAllowed)
                        {
                            txtLateAllowedMin.Text = s.LateAllowedMin;
                            ddlLateCutActions.SelectedValue = s.LateCutAction;
                        }
                        chkOT.Checked = s.IsOTAllowed;
                        if (s.IsOTAllowed)
                        {
                            txtOtGracePeriod.Text = s.OTStartMinute;
                            txtOt1DayHrs.Text = s.OTDayHrs;
                        }
                        chkIsLunch.Checked = s.IsLunchAllowed;
                        ddlWOFFAction.SelectedValue = s.IfPresentOnWOFForHOFF;
                        txtMaxOtHrs.Text = s.MaxOtHrs;

                        ViewState["PageID"] = s.SettingID;
                    }

                }
                
                
            }
        }
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                
                vt_tbl_Settings s = new vt_tbl_Settings();

                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    int CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                    s.CompanyID = CompanyID;
                }
                else
                {
                    s.CompanyID = vt_Common.CheckInt(ddlCompany.SelectedValue);
                }

                s.ShiftTotalMin = txtFullDayMins.Text;
                s.ShiftHalfDayMin = txtHalfDayMins.Text;
                s.LateAllowedMin = txtLateAllowedMin.Text;
                s.EarlyAllowedMin = txtEarlyAllowdMin.Text;
                s.WeeklyOff = ddlWeeklyOff.SelectedValue;
                s.IsSecondweeklyOff = chkSecondWeeklyOff.Checked;

                if (s.IsSecondweeklyOff)
                {
                    s.SecondWeeklyOff = ddlSecondWeeklyOff.SelectedValue;
                    s.SecondWeeklyOffStatus = rdoSecondWeeklyOffStatus.SelectedValue;
                    s.SecondWeeklyOffPattern = String.Join(",", chkSecondWeeklyOffPattern.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
                }
                s.DevicePunch = rdopunch.SelectedValue;
                if (!s.DevicePunch.Equals("Single"))
                {
                    s.TolleranceMins = vt_Common.CheckInt(txtTolleranceMin.Text);
                    s.NoOutPunchFound = ddlNoOutPunchFound.SelectedValue;
                }
                s.IsLateCutAllowed = chkLateCut.Checked;
                if (s.IsLateCutAllowed)
                {
                    s.LateAllowedMin = txtLateAllowedMin.Text;
                    s.LateCutAction = ddlLateCutActions.SelectedValue;

                }
                s.IsOTAllowed = chkOT.Checked;
                if (s.IsOTAllowed)
                {
                    s.OTStartMinute = txtOtGracePeriod.Text;
                    s.OTDayHrs = txtOt1DayHrs.Text;
                }
                s.IsLunchAllowed = chkIsLunch.Checked;
                s.IfPresentOnWOFForHOFF = ddlWOFFAction.SelectedValue;
                s.MaxOtHrs = txtMaxOtHrs.Text;


                if (ViewState["SettingID"] != null)
                {
                    s.SettingID = vt_Common.CheckInt(ViewState["SettingID"]);
                    db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_Settings.Add(s);
                }
                db.SaveChanges();
                Upview.Update();

                MsgBox.Show(Page, MsgBox.success, "Attendance setting", "Successfully Save");
            }
        }
    }
    
    
    
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_Common.Clear(pnlDetail.Controls);
                var companies = (from vt_tbl_Company c in db.vt_tbl_Company
                                 orderby c.CompanyName
                                 select new { c.CompanyID, c.CompanyName }).ToList();

                ddlCompany.DataValueField = "CompanyID";
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataSource = companies;
               
                int Companyid = vt_Common.CheckInt(ddlCompany.SelectedValue);
                vt_tbl_Company com = db.vt_tbl_Company.FirstOrDefault(x => x.CompanyID == Companyid);
                FillDetailForm(Companyid);
                
                Upview.Update();
                

                var quer = (from a in db.vt_tbl_Settings
                            where a.CompanyID == Companyid
                            select a).FirstOrDefault();

                if (quer != null)
                {
                    ViewState["SettingID"] = quer.SettingID;
                }

                else
                {
                    ViewState["SettingID"] = null;
                }
                

            }
        }
        

    }
}