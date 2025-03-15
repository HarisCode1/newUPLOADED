using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Other : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadForm();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }

    #region Helper Method

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
          
            vt_tbl_PayrollSettings s = db.vt_tbl_PayrollSettings.Where(x => x.CompanyID == ID).SingleOrDefault();
            if (s != null)
            {
                chkPFApplicable.Checked = s.PFApplicable;
                chkEOBIApplicable.Checked = s.ESICApplicable;
                ddlSalaryRate.SelectedValue = s.SalaryRateType;
                chkFixMonthDays.Checked = s.FixMonthDays;
                if (s.FixMonthDays)
                {
                    txtFixDays.Text = s.FixDays;
                    chkOnlyWorkingDays.Checked = false;
                }

                else
                {
                    chkFixMonthDays.Checked = false;
                    chkOnlyWorkingDays.Checked = s.OnlyMonthDay;
                }

                
            }

            else
            {
                chkPFApplicable.Checked = false;
                chkEOBIApplicable.Checked = false;
                ddlSalaryRate.SelectedIndex = 0;

                if (chkFixMonthDays.Checked == false)
                {
                    txtFixDays.Text = "";
                }
                    chkOnlyWorkingDays.Checked = false;
                
                
                    chkFixMonthDays.Checked = false;
                    chkOnlyWorkingDays.Checked = false;
                
                
            }

            
        }
        
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
                    vt_tbl_PayrollSettings s = db.vt_tbl_PayrollSettings.Where(x => x.CompanyID == CompanyID).SingleOrDefault();
                    divCompany.Visible = false;
                    btnSave.Visible = false;

                    if (s != null)
                    {
                        chkPFApplicable.Checked = s.PFApplicable;
                        chkEOBIApplicable.Checked = s.ESICApplicable;
                        ddlSalaryRate.SelectedValue = s.SalaryRateType;
                        chkFixMonthDays.Checked = s.FixMonthDays;
                        if (s.FixMonthDays)
                        {
                            txtFixDays.Text = s.FixDays;
                            chkOnlyWorkingDays.Checked = false;
                        }
                        else
                        {
                            chkFixMonthDays.Checked = false;
                            chkOnlyWorkingDays.Checked = s.OnlyMonthDay;
                        }

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


                    int CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                    vt_tbl_PayrollSettings s = db.vt_tbl_PayrollSettings.Where(x => x.CompanyID == CompanyID).SingleOrDefault();

                    if (s != null)
                    {
                        chkPFApplicable.Checked = s.PFApplicable;
                        chkEOBIApplicable.Checked = s.ESICApplicable;
                        ddlSalaryRate.SelectedValue = s.SalaryRateType;
                        chkFixMonthDays.Checked = s.FixMonthDays;
                        if (s.FixMonthDays)
                        {
                            txtFixDays.Text = s.FixDays;
                            chkOnlyWorkingDays.Checked = false;
                        }
                        else
                        {
                            chkFixMonthDays.Checked = false;
                            chkOnlyWorkingDays.Checked = s.OnlyMonthDay;
                        }

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


                vt_tbl_PayrollSettings s = new vt_tbl_PayrollSettings();

                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    int CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                    s.CompanyID = CompanyID;
                }

                else
                {
                    s.CompanyID = vt_Common.CheckInt(ddlCompany.SelectedValue);
                }
                
                

                s.PFApplicable = chkPFApplicable.Checked;
                s.ESICApplicable = chkEOBIApplicable.Checked;
                s.SalaryRateType = ddlSalaryRate.SelectedValue;
                s.FixMonthDays = chkFixMonthDays.Checked;
                if (s.FixMonthDays)
                {
                    s.FixDays = txtFixDays.Text;
                    s.OnlyMonthDay = chkOnlyWorkingDays.Checked;
                }
                else
                {
                    s.OnlyMonthDay = chkOnlyWorkingDays.Checked;
                }
                
                
                if ( ViewState["PSettingID"] != null)
                {
                    s.PSettingID = vt_Common.CheckInt(ViewState["PSettingID"]);
                    db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.vt_tbl_PayrollSettings.Add(s);
                }
                db.SaveChanges();
                Upview.Update();

                MsgBox.Show(Page, MsgBox.success, "PAYROLL Setting", "Successfully Save");
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
                vt_tbl_PayrollSettings s = new vt_tbl_PayrollSettings();
                var companies = (from vt_tbl_Company c in db.vt_tbl_Company
                                 orderby c.CompanyName
                                 select new { c.CompanyID, c.CompanyName }).ToList();

                ddlCompany.DataValueField = "CompanyID";
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataSource = companies;
                Upview.Update();
               

                int Companyid = vt_Common.CheckInt(ddlCompany.SelectedValue);
                vt_tbl_Company com = db.vt_tbl_Company.FirstOrDefault(x => x.CompanyID == Companyid);
                FillDetailForm(Companyid);
                Upview.Update();

                

                var quer = (from a in db.vt_tbl_PayrollSettings
                            where a.CompanyID == Companyid
                            select a).FirstOrDefault();

                if ( quer != null)
                {
                    ViewState["PSettingID"] = quer.PSettingID;
                }

                else
                {
                    ViewState["PSettingID"] = null;
                }

                              
            }


          }

        

    }
}