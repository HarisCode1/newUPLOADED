using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
//using Newtonsoft.Json;
using System.Globalization;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Employes : System.Web.UI.Page
{
    Qualification_BAl QualificationBal = new Qualification_BAl();
    vt_tbl_QualificationDetails obj_qualification = new vt_tbl_QualificationDetails();

    RolePermission RP = new RolePermission();
    vt_tbl_Employee emp = new vt_tbl_Employee();
    string Pagename = string.Empty;
    public static int CompanyID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (!IsPostBack)
            {
                LoadData();
            }
        }
    }
    public void BindLineManager()
    {
        int Des = 0;
        Des = (Convert.ToInt32(ddlDesignation.SelectedValue));
        SqlParameter[] param =
        {
            new SqlParameter("@DesignationID",Des)
        };
        vt_Common.Bind_DropDown(ddlLineManager, "VT_sp_BindLineManager", "EmployeeName", "EmployeeID", param);
        if (ddlLineManager.Items.Count == 1)
        {
            int RoleID = Convert.ToInt32(Session["RoleId"]);
            int ComID = Convert.ToInt32(Session["CompanyId"]);
            SqlParameter[] param1 =
            {
            new SqlParameter("@RoleId",RoleID),
            new SqlParameter("@CompanyId",ComID)

        };
            vt_Common.Bind_DropDown(ddlLineManager, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
        }

    }
    protected void ddlLineManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlLineManager.Items.Clear();
        BindLineManager();
    }
    public void BindDep()
    {
        int companyID = 0;
        companyID = (Convert.ToInt32(Session["CompanyId"]));
        SqlParameter[] param =
        {
            new SqlParameter("@CompanyID",companyID)
        };
        vt_Common.Bind_DropDown(ddldepartment, "VT_SP_BindDepart", "Department", "DepartmentID", param);
    }

    #region Control Event
    #region Salman Code
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void DevExtreme_Actions(int ID, string Action)
    {
        Employes Emp = new Employes();
        Emp.RedirectTo(ID);
    }

    void RedirectTo(int ID)
    {
        this.Response.Redirect("/Employes.aspx");
    }
    #endregion

    protected void grdEmp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int Id = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_EmployeeGrossSalary employeegrosssalary = db.vt_tbl_EmployeeGrossSalary.FirstOrDefault(x => x.EmployeeID == Id);
                        db.vt_tbl_EmployeeGrossSalary.Remove(employeegrosssalary);
                        db.SaveChanges();
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ID);
                        //  string EnrollId = emp.EnrollId;
                        // vt_tbl_EmployeePhoto empPhoto = db.vt_tbl_EmployeePhoto.FirstOrDefault(x => x.EnrollId == EnrollId);
                        //db.vt_tbl_Employee.Remove(emp);
                        emp.JobStatus = "Deactive";
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
                UpDetail.Update();
                break;

            default:
                break;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        UpView.Update();
    }

    #region Healper Method

    #region Salman Code

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load()
    {
        string List;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                //var Query = db.VT_SP_GetEmployees(CompanyID).ToList();
                var Query = db.VT_SP_GetEmployees(CompanyID).ToList();
                List = JsonConvert.SerializeObject(Query);
            }

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        return List;
    }

    #endregion

    private void LoadData()
    {
        try
        {
            BindDep();
            //PayRoll_Session s = (PayRoll_Session)Session["PayrollSess"];

            // if (Session["EMS_Session"] != null)
            //if (Session["PayrollSess"] != null)

            // {
            //   PayRoll_Session PayRoll = (PayRoll_Session)Session["PayrollSess"];

            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //   int companyID = 0;
                //   if (((EMS_Session)Session["EMS_Session"]).Company == null)
                //if (((EMS_Session)Session["EMS_Session"]).user.UserName == "Superadmin")
                //    {
                //       companyID = Convert.ToInt32(ddlCompany.SelectedValue);
                //    }
                //    else
                //    {
                //        divCompany.Visible = false;
                //       divComp.Visible = true;
                //       grdEmp.Columns[1].Visible = false;
                //       companyID = vt_Common.CompanyId;
                //   }

                var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                grdEmp.DataSource = Query;
                grdEmp.DataBind();
            }

            //  }
            // else
            //  {
            //     Response.Redirect("login.aspx");
            // }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    private void ClearForm()
    {
        ViewState["PageID"] = null;
        //Clear Upload Fields
        LblResumeUpload.Text = null;
        lblNIC.Text = null;
        lblpasport.Text = null;
        lbldocuments.Text = null;
        lblotherdocuments.Text = null;
        txtHidden_Academic.Value = "";
        txt_HiidenCertificate.Value = "";
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#employes').modal('hide');");
    }

    private void BindDesignation(int CompanyID)
    {
        ddlDesignation.Items.Clear();
        ddldepartment.Items.Clear();
        ddlLineManagerDesignation.Items.Clear();
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //var query = db.VT_SP_GetDesignation(CompanyID).ToList();
            //ddlDesignation.DataSource = query;
            //ddlDesignation.DataTextField = "Designation";
            //ddlDesignation.DataValueField = "DesignationID";
            //ddlDesignation.DataBind();
            ////ddlDesignation.Items.Insert(0, new ListItem("Please Select", "0"));

            //ddlLineManagerDesignation.DataSource = query;
            //ddlLineManagerDesignation.DataTextField = "Designation";
            //ddlLineManagerDesignation.DataValueField = "DesignationID";
            //ddlLineManagerDesignation.DataBind();
            ////ddlLineManagerDesignation.Items.Insert(0, new ListItem("Please Select", "0"));

            //ddldepartment.DataSource = query;
            //ddldepartment.DataTextField = "Department";
            //ddldepartment.DataValueField = "DepartmentID";
            //ddldepartment.DataBind();
        }

        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ddEmployeType.Items.Clear();
            var Typedropdown = (from m in db.vt_tbl_TypeofEmployee
                                select new
                                {
                                    m.Type,
                                    m.Id
                                }).ToList();

            ddEmployeType.DataSource = Typedropdown;
            ddEmployeType.DataTextField = "Type";
            ddEmployeType.DataValueField = "Id";
            ddEmployeType.DataBind();
            ddEmployeType.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ddPFtype.Items.Clear();
            var PFtype = (from m in db.vt_tbl_TypeofSalary
                          select new
                          {
                              m.Type,
                              m.Id
                          }).ToList();

            ddPFtype.DataSource = PFtype;
            ddPFtype.DataTextField = "Type";
            ddPFtype.DataValueField = "Id";
            ddPFtype.DataBind();
            ddPFtype.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Querydep = db.getdeptbyComID(CompanyID).ToList();
            ddldepartment.DataSource = Querydep;
            ddldepartment.DataTextField = "Department";
            ddldepartment.DataValueField = "DepartmentID";
            ddldepartment.DataBind();
        }
    }

    public void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Employee em = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ID);
            TxtFirstName.Text = em.FirstName;
            TxtLastName.Text = em.LastName;
            Txtemail.Text = em.Email;
            //if (((EMS_Session)Session["EMS_Session"]).Company != null)
            //if (((PayRoll_Session)Session["PayrollSess"]).UserName == "SUPERADMIN")

            //{
            //    ddlcomp.SelectedIndex = 0;
            //}
            //else
            //{
            //    ddlcomp.SelectedValue = em.CompanyID.ToString();
            //}
            ddlcomp.SelectedValue = vt_Common.CheckString(em.CompanyID);
            BindDesignation(vt_Common.CheckInt(em.CompanyID));

            ddlDesignation.SelectedValue = em.DesignationID.ToString();
            // ddlRole.SelectedValue = em.RoleID.ToString();
            ddlLineManagerDesignation.SelectedValue = vt_Common.CheckString(em.DesignationID);
            ddldepartment.SelectedValue = vt_Common.CheckString(em.DepartmentID);
            //BindLineManagers(Convert.ToInt32(em.EmployeeID), vt_Common.CheckInt(em.CompanyID));
            BindLineManagers_New(vt_Common.CheckInt(em.ManagerID), vt_Common.CheckInt(em.CompanyID));
            ddlLineManager.SelectedValue = em.ManagerID.ToString();
            ddEmployeType.SelectedValue = (em.Type);
            Txtusername.Text = em.EmployeeName;
            TxtPassword.Attributes.Add("value", vt_Common.Decrypt(em.EmpPassword));
            TxtConfirmPassword.Attributes.Add("value", vt_Common.Decrypt(em.EmpPassword));
            TxtEmployeeCode.Text = em.EnrollId;
            //PERSONAL INFORMATION
            //emp.EnrollId = txtEnrollID.Text;

            txtFatherName.Text = em.FatherHusbandName;
            ddlrelation.SelectedValue = em.RelationWithEmployee;
            ddlBloodGroup.SelectedValue = em.BloodGroup;
            ddlSex.SelectedValue = em.Sex;
            txtDOB.Text = em.DOB != null ? em.DOB.Value.ToShortDateString() : "";
            rdoMarriedStatus.Text = em.MartialStatus;
            txtWeddAnnev.Text = em.WeddDate != null ? em.WeddDate.Value.ToShortDateString() : "";
            txtPhone.Text = em.Phone;
            txtCurrentAddress.Text = em.CurrentAddress;
            txtParmanantAddress.Text = em.ParmanantAddress;
            LblResumeUpload.Text = em.ResumeUpload;
            lblNIC.Text = em.NICImage;
            lblpasport.Text = em.PassportImage;
            lbldocuments.Text = em.Documents;
            lblotherdocuments.Text = em.OtherDocuments;

            if (txtCurrentAddress.Text == txtParmanantAddress.Text && txtCurrentAddress.Text != "")
            {
                chkCurrentAddress.Checked = true;
            }
            else
            {
                chkCurrentAddress.Checked = false;
            }

            //JOB INFORMATION
            ddlBranch.SelectedValue = em.BranchID.ToString();
            //ddlEmpType.SelectedValue = em.Type;
            //emp.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            ddlBank.SelectedValue = em.BankID.ToString();
            txtAccountNo.Text = em.BankAccountNo;
            txtJoiningDate.Text = em.JoiningDate != null ? em.JoiningDate.Value.ToShortDateString() : "";
            txtConfrDate.Text = em.ConfirmationDate != null ? em.ConfirmationDate.Value.ToShortDateString() : "";
            rdoJobStatus.SelectedValue = em.JobStatus;
            string isConf_Pro = em.JobPeriod;
            if (isConf_Pro == "Confirmation")
            {
                rdoConfirmation.Checked = true;
                //     txtConfrDate.Text = em.ConfirmationDate.ToShortDateString();
                Conf.Style.Remove("display");
            }
            else
            {
                Conf.Style.Add("display", "none");
                txtConfrDate.Text = "";
            }
            if (isConf_Pro == "Probation")
            {
                rdoProb.Checked = true;
                txtProbationPeriod.Text = em.ProvisionalPeriod;
                Prov.Style.Remove("display");
            }
            else
            {
                Prov.Style.Add("display", "none");
                txtProbationPeriod.Text = "";
            }
            //SHIFT WOFF
            //rdoShiftType.SelectedValue = em.ShiftType;
            ddlShift.SelectedValue = em.FixShiftID.ToString();

            //txtLateAllowedMin.Text = em.LateAllowedMin;
            //chkLateAllowdAsShift.Checked = Convert.ToBoolean(em.ShiftRuleLateMinsAllowed);
            //if (chkLateAllowdAsShift.Checked)
            //{
            //    txtLateAllowedMin.Enabled = false;
            //}
            //txtEarlyAllowedMin.Text = em.EarlyAllowedMin;
            //chkEarlyAllowedAsShift.Checked = Convert.ToBoolean(em.ShiftRuleEarlyMinsAllowed);
            //if (chkEarlyAllowedAsShift.Checked)
            //{
            //    txtEarlyAllowedMin.Enabled = false;
            //}
            //txtHalfDayMin.Text = em.HalfDayMin;
            //chkHalfDayMinAsShift.Checked = Convert.ToBoolean(em.ShiftRuleHalfDayMinsAllowed);
            //if (chkHalfDayMinAsShift.Checked)
            //{
            //    txtHalfDayMin.Enabled = false;
            //}
            //txtFullDayMin.Text = em.FullDayMins;
            //chkFullDayMinAsShift.Checked = Convert.ToBoolean(em.ShiftRuleFullDayMinsAllowed);
            //if (chkFullDayMinAsShift.Checked)
            //{
            //    txtFullDayMin.Enabled = false;
            //}
            //if (string.IsNullOrEmpty(em.WOFF))
            //{
            //    chkWoffPayble.Checked = false;
            //}
            //else
            //{
            //    chkWoffPayble.Checked = true;
            //    ddlWeeklyOff.SelectedValue = em.WOFF;
            //}

            //chkSecondWeeklyOff.Checked = Convert.ToBoolean(em.IsSecondWOFF);
            //if (Convert.ToBoolean(em.IsSecondWOFF))
            //{
            //    ddlSecondWeeklyOff.SelectedValue = em.SecondWOFF;
            //    rdoSecondWeeklyOffStatus.SelectedValue = em.IsSecondWOFF.ToString(); ;
            //    foreach (var item in em.Pattern.Split(','))
            //    {
            //        chkSecondWeeklyOffPattern.Items.FindByValue(item.Trim()).Selected = true;
            //    }
            //}
            // PAYROLL SETTINGS

            ddPFtype.SelectedValue = em.PFType.ToString();
            txtBasicSalary.Text = vt_Common.CheckString(em.BasicSalary);
            txtHouseRentAllowance.Text = vt_Common.CheckString(em.HouseRentAllownce);
            txtMedicalAllowance.Text = vt_Common.CheckString(em.MedicalAllowance);
            txtTransportAllowance.Text = vt_Common.CheckString(em.TransportAllownce);
            txtFuelAllowance.Text = vt_Common.CheckString(em.FuelAllowance);
            txtSpecialAllowance.Text = vt_Common.CheckString(em.SpecialAllowance);
            bool isTrue = (bool)(em.ProvidentFund == null ? false : em.ProvidentFund);
            if (isTrue) ChkBxProvidentFund.Checked = true; else ChkBxProvidentFund.Checked = false;
            txtbankfrom.Text = vt_Common.CheckString(em.FromBank);
            txtbrachfrom.Text = vt_Common.CheckString(em.FromBranch);
            txtbankto.Text = vt_Common.CheckString(em.ToBank);
            txtbranchto.Text = vt_Common.CheckString(em.ToBranch);
            txtaccount.Text = vt_Common.CheckString(em.AccountNo);

            if (!string.IsNullOrEmpty(em.ImageName))
            {
                hdEmpPhotoID.Value = em.ImageName.ToString();
                empImageView.ImageUrl = "~/images/Employees/" + hdEmpPhotoID.Value;
            }
            else
            {
                empImageView.ImageUrl = "~/assets/img/user2-160x160.jpg";
            }

            //Qualification Details Data
            var qualification = db.sp_GetEditQualificationDetails(em.EmployeeID, 1).ToList();  //Academic Qualification

            var json = JsonConvert.SerializeObject(qualification);

            txtHidden_Academic.Value = json;

            //Certificate Details Data
            var Certificate = db.sp_GetEditQualificationDetails(em.EmployeeID, 2).ToList();  //Academic Qualification

            var certificatejson = JsonConvert.SerializeObject(Certificate);

            txt_HiidenCertificate.Value = certificatejson;
        }
        ViewState["PageID"] = ID;
    }


    #endregion Healper Method

    protected void btnSaveEmployee_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_Employee emp = new vt_tbl_Employee();
                int Company_ID = Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];
                if (Company_ID == 0)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Please select company from the dashboard');");
                }
                //else
                //{
                //    emp.CompanyID = vt_Common.CompanyId;
                //    Company_ID = vt_Common.CompanyId;
                //}
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);

                var record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.CompanyID == Company_ID && o.EmployeeName.ToLower().Replace(" ", "").Equals(Txtusername.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same username already exist');");
                }
                else
                {
                    record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.EnrollId.ToLower().Replace(" ", "").Equals(TxtEmployeeCode.Text.ToLower().Replace(" ", "")));
                    if (record != null)
                    {
                        vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same code already exist');");
                    }
                    else
                    {
                        if (ViewState["ddlShiftID"] == null)
                        {
                            ViewState["ddlShiftID"] = ddlShift.SelectedValue;
                        }
                        else
                        {
                            string val = ViewState["ddlShiftID"].ToString();

                            int ID = vt_Common.CheckInt(val);
                            vt_tbl_Shift sh = db.vt_tbl_Shift.FirstOrDefault(x => x.ShiftID == ID);
                            if (sh != null)
                            {
                                //hdLateAllowdAsShift.Value = sh.LateAllowed;
                                //hdEarlyAllowedAsShift.Value = sh.EarlyAllowed;
                                //hdFullDayMinAsShift.Value = sh.FullDayMinutes;
                                //hdHalfDayMinAsShift.Value = sh.HalfDayMinutes;
                                //hdOTAsPerShiftRule.Value = sh.GracePeriod;
                                //hdLunchAsPerShiftRule.Value = sh.FixLunchMins;
                            }
                        }
                        //ACCOUNT SETUP//
                        emp.FirstName = TxtFirstName.Text;
                        emp.LastName = TxtLastName.Text;
                        emp.CompanyID = int.Parse(ddlcomp.SelectedValue);
                        emp.Email = Txtemail.Text;
                        emp.DesignationID = int.Parse(ddlDesignation.SelectedValue);
                        emp.Type = ddEmployeType.Text;
                        emp.RoleID = 4;
                        emp.EmployeeName = Txtusername.Text;
                       // emp.EnrollId = TxtEmployeeCode.Text;
                        emp.EmpPassword = vt_Common.Encrypt(TxtPassword.Text);

                        emp.ManagerID = vt_Common.CheckInt(ddlLineManager.SelectedValue);
                        if (ddlLineManager.SelectedItem.Text.Contains("HR Admin"))
                        {
                            emp.IsHRLineManager = true;
                        }
                        else
                        {
                            emp.IsHRLineManager = false;
                        }

                        //PERSONAL INFORMATION
                        //emp.EnrollId = txtEnrollID.Text;
                        emp.FatherHusbandName = txtFatherName.Text;
                        emp.RelationWithEmployee = ddlrelation.SelectedValue;
                        emp.BloodGroup = ddlBloodGroup.SelectedValue;
                        emp.Sex = ddlSex.SelectedValue;
                        if (!string.IsNullOrEmpty(txtDOB.Text))
                        {
                            emp.DOB = DateTime.Parse(txtDOB.Text);
                        }
                        emp.MartialStatus = rdoMarriedStatus.Text;
                        if (!string.IsNullOrEmpty(txtWeddAnnev.Text))
                        {
                            emp.WeddDate = DateTime.Parse(txtWeddAnnev.Text);
                        }
                        emp.Phone = txtPhone.Text;
                        emp.CurrentAddress = txtCurrentAddress.Text;
                        emp.ParmanantAddress = txtParmanantAddress.Text;
                        //JOB INFORMATION
                        emp.BranchID = Convert.ToInt32(ddlBranch.SelectedValue);
                        //emp.Type = ddlEmpType.SelectedValue;
                        emp.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        emp.BankID = Convert.ToInt32(ddlBank.SelectedValue);
                        emp.BankAccountNo = txtAccountNo.Text;

                        if (!string.IsNullOrEmpty(txtJoiningDate.Text))
                        {
                            emp.JoiningDate = DateTime.Parse(txtJoiningDate.Text);
                        }
                        emp.JobStatus = rdoJobStatus.SelectedValue;
                        if (rdoConfirmation.Checked)
                        {
                            emp.JobPeriod = "Confirmation";
                            if (!string.IsNullOrEmpty(txtConfrDate.Text))
                            {
                                emp.ConfirmationDate = DateTime.Parse(txtConfrDate.Text);
                            }
                        }
                        if (rdoProb.Checked)
                        {
                            emp.JobPeriod = "Probation";
                            emp.ProvisionalPeriod = txtProbationPeriod.Text;
                        }
                        //SHIFT WOFF
                        //emp.ShiftType = rdoShiftType.Text;
                        emp.FixShiftID = vt_Common.CheckInt(ddlShift.SelectedValue);
                        //emp.LateAllowedMin = chkLateAllowdAsShift.Checked ? hdLateAllowdAsShift.Value : txtLateAllowedMin.Text;
                        //emp.ShiftRuleLateMinsAllowed = txtLateAllowedMin.Text.Equals("") ? true : false;
                        //emp.EarlyAllowedMin = chkEarlyAllowedAsShift.Checked ? hdEarlyAllowedAsShift.Value : txtEarlyAllowedMin.Text;
                        //emp.ShiftRuleEarlyMinsAllowed = txtEarlyAllowedMin.Text.Equals("") ? true : false;
                        //emp.HalfDayMin = chkHalfDayMinAsShift.Checked ? hdHalfDayMinAsShift.Value : txtHalfDayMin.Text;
                        //emp.ShiftRuleHalfDayMinsAllowed = txtHalfDayMin.Text.Equals("") ? true : false;
                        //emp.FullDayMins = chkFullDayMinAsShift.Checked ? hdFullDayMinAsShift.Value : txtFullDayMin.Text;
                        //emp.ShiftRuleFullDayMinsAllowed = txtFullDayMin.Text.Equals("") ? true : false;
                        //if (chkWoffPayble.Checked)
                        //{
                        //    emp.WOFFPayble = chkWoffPayble.Checked;
                        //    emp.WOFF = ddlWeeklyOff.SelectedValue;
                        //}
                        //if (chkSecondWeeklyOff.Checked)
                        //{
                        //    emp.SecondWOFF = ddlSecondWeeklyOff.SelectedValue;
                        //    emp.IsSecondWOFF = chkSecondWeeklyOff.Checked;
                        //    emp.SecondWOFFStatus = rdoSecondWeeklyOffStatus.SelectedValue;
                        //    emp.Pattern = String.Join(",", chkSecondWeeklyOffPattern.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
                        //}
                        //PAYROLL SETTINGS
                        if (ddPFtype.SelectedValue == null)
                        {
                            ddPFtype.SelectedValue = "0";
                        }
                        else
                        {
                            emp.PFType = vt_Common.CheckInt(ddPFtype.SelectedValue);
                        }
                        // emp.PFType = vt_Common.CheckInt(ddPFtype.SelectedValue);
                        emp.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        emp.HouseRentAllownce = vt_Common.Checkdecimal(txtHouseRentAllowance.Text);
                        emp.MedicalAllowance = vt_Common.Checkdecimal(txtMedicalAllowance.Text);
                        emp.TransportAllownce = vt_Common.Checkdecimal(txtTransportAllowance.Text);
                        emp.FuelAllowance = vt_Common.Checkdecimal(txtFuelAllowance.Text);
                        emp.SpecialAllowance = vt_Common.Checkdecimal(txtSpecialAllowance.Text);
                        if (ChkBxProvidentFund.Checked) emp.ProvidentFund = true; else emp.ProvidentFund = false;

                        if (uploadEmpImage.HasFile)
                        {
                            emp.ImageName = uploadEmpImage.PostedFile.FileName;
                        }
                        else
                        {
                            emp.ImageName = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
                        }

                        if (hdNICName.Value == "")
                        {
                            hdNICName.Value = null;
                        }
                        else
                        {
                            emp.NICImage = hdNICName.Value == "" ? hdNICImage.Value : hdNICName.Value;
                        }
                        if (hdpasportname.Value == "")
                        {
                            hdpasportname.Value = null;
                        }
                        else
                        {
                            emp.PassportImage = hdpasportname.Value == "" ? hdPasportImage.Value : hdpasportname.Value;
                        }
                        if (hdResumeFileUpload.Value == "")
                        {
                            hdResumeFileUpload.Value = null;
                        }
                        else
                        {
                            emp.ResumeUpload = hdResumeFileUpload.Value == "" ? "" : hdResumeFileUpload.Value;
                        }
                        if (hddocumenstupload.Value == "")
                        {
                            hddocumenstupload.Value = null;
                        }
                        else
                        {
                            emp.Documents = hddocumenstupload.Value == "" ? "" : hddocumenstupload.Value;
                        }
                        if (hdotherdocumntsupload.Value == "")
                        {
                            hdotherdocumntsupload.Value = null;
                        }
                        else
                        {
                            emp.OtherDocuments = hdotherdocumntsupload.Value == "" ? "" : hdotherdocumntsupload.Value;
                        }
                        emp.FromBank = vt_Common.CheckString(txtbankfrom.Text);
                        emp.FromBranch = vt_Common.CheckString(txtbrachfrom.Text);
                        emp.ToBank = vt_Common.CheckString(txtbankto.Text);
                        emp.ToBranch = vt_Common.CheckString(txtbranchto.Text);
                        emp.AccountNo = txtaccount.Text;
                        if (ViewState["PageID"] != null)
                        {
                            emp.EmployeeID = vt_Common.CheckInt(ViewState["PageID"]);
                            db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.vt_tbl_Employee.Add(emp);
                        }
                        db.SaveChanges();

                        // Acadmic Info Start
                        vt_tbl_QualificationDetails obj_qualification = new vt_tbl_QualificationDetails();
                        string qualification = txtHidden_Academic.Value;
                        List<vt_tbl_QualificationDetails> listQualificationAcademic = JsonConvert.DeserializeObject<List<vt_tbl_QualificationDetails>>(qualification);

                        var academic_qualification = db.sp_GetEditQualificationDetails(emp.EmployeeID, 1).Select(s => new vt_tbl_QualificationDetails { Id = s.Id }).ToList();  //Academic Qualification

                        //Removing Old Records from Qualification on behalf of EmpID
                        //Type = 1
                        if (academic_qualification.Count() > 0)
                        {
                            foreach (var item in academic_qualification)
                            {
                                var acdemic_obj = db.vt_tbl_QualificationDetails.Find(item.Id);
                                db.vt_tbl_QualificationDetails.Remove(acdemic_obj);
                                db.SaveChanges();
                                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }

                            //academic_qualification.ForEach(x => db.vt_tbl_QualificationDetails.Remove(x));
                            //  db.Entry(academic_qualification).State = System.Data.Entity.EntityState.Deleted;
                        }

                        foreach (var item in listQualificationAcademic)
                        {
                            obj_qualification.InstituteName = item.InstituteName;
                            obj_qualification.Qualification = item.Qualification;
                            obj_qualification.Year = Convert.ToInt32(item.Year);
                            obj_qualification.Marks = item.Marks;
                            obj_qualification.Creadetby = Convert.ToInt32(Session["UserId"]);
                            obj_qualification.Createdon = DateTime.Now;
                            obj_qualification.EmployeeId = emp.EmployeeID;
                            obj_qualification.Type = 1;
                            obj_qualification.IsActive = true;
                            db.vt_tbl_QualificationDetails.Add(obj_qualification);

                            db.SaveChanges();
                        }
                        vt_tbl_QualificationDetails obj_certificate = new vt_tbl_QualificationDetails();
                        string certificate = txt_HiidenCertificate.Value;
                        List<vt_tbl_QualificationDetails> listQualificationCertificate = JsonConvert.DeserializeObject<List<vt_tbl_QualificationDetails>>(certificate);

                        var certification_qualification = db.sp_GetEditQualificationDetails(emp.EmployeeID, 2).ToList();  //Certificate Qualification
                        //Removing Old Records on behalf of EmpID
                        //Type = 2
                        if (certification_qualification.Count() > 0)
                        {
                            foreach (var item in certification_qualification)
                            {
                                var certificate_obj = db.vt_tbl_QualificationDetails.Find(item.Id);
                                db.vt_tbl_QualificationDetails.Remove(certificate_obj);
                                db.SaveChanges();
                                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }

                            //academic_qualification.ForEach(x => db.vt_tbl_QualificationDetails.Remove(x));
                            //  db.Entry(academic_qualification).State = System.Data.Entity.EntityState.Deleted;
                        }

                        foreach (var item in listQualificationCertificate)
                        {
                            obj_certificate.InstituteName = item.InstituteName;
                            obj_certificate.Qualification = item.Qualification;
                            obj_certificate.Year = Convert.ToInt32(item.Year);
                            obj_certificate.Grade = (item.Grade);
                            obj_certificate.Creadetby = Convert.ToInt32(Session["UserId"]);
                            obj_certificate.Createdon = DateTime.Now;
                            obj_certificate.EmployeeId = emp.EmployeeID;
                            obj_certificate.Type = 2;
                            obj_certificate.IsActive = true;
                            db.vt_tbl_QualificationDetails.Add(obj_certificate);

                            db.SaveChanges();
                        }

                        // Acadmic Info End
                        var getEmployeeID = emp.EmployeeID;
                        vt_tbl_EmployeeGrossSalary GSalary = new vt_tbl_EmployeeGrossSalary();
                        GSalary.EmployeeID = getEmployeeID;
                        GSalary.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        GSalary.HouseRentAllownce = vt_Common.Checkdecimal(txtHouseRentAllowance.Text);
                        GSalary.MedicalAllowance = vt_Common.Checkdecimal(txtMedicalAllowance.Text);
                        GSalary.TransportAllownce = vt_Common.Checkdecimal(txtTransportAllowance.Text);
                        GSalary.FuelAllownce = vt_Common.Checkdecimal(txtFuelAllowance.Text);
                        GSalary.SpecialAllownce = vt_Common.Checkdecimal(txtSpecialAllowance.Text);
                        GSalary.CurrentSalary = true;
                        GSalary.CreatedDate = DateTime.Now;
                        GSalary.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        if (ViewState["PageID"] != null)
                        {
                            int SalaryID = db.vt_tbl_EmployeeGrossSalary.Where(x => x.EmployeeID == getEmployeeID).Select(y => y.SalaryID).FirstOrDefault();
                            if (SalaryID > 0)
                            {
                                GSalary.SalaryID = SalaryID;
                                GSalary.ModifiedDate = DateTime.Now;
                                db.Entry(GSalary).State = System.Data.Entity.EntityState.Modified;
                            }
                            else
                            {
                                db.vt_tbl_EmployeeGrossSalary.Add(GSalary);
                            }
                        }
                        else
                        {
                            db.vt_tbl_EmployeeGrossSalary.Add(GSalary);
                        }
                        db.SaveChanges();
                        
                        MsgBox.Show(Page, MsgBox.success, emp.EmployeeName, "Successfully Saved");
                        ClearForm();
                        LoadData();
                        UpView.Update();
                    }
                }
            }
        }
        catch (DbEntityValidationException ex)
        {
            foreach (var eve in ex.EntityValidationErrors)
            {
                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            throw;
        }
        //catch (DbUpdateException ex)
        //{
        //    MsgBox.Show(Page, MsgBox.danger, "Employee Not Saving due to this", ex.Message);
        //}
        //catch (Exception ex)
        //{
        //    MsgBox.Show(Page, MsgBox.danger, "Record Not Saved ! \n", ex.Message);
        //}

        if (uploadEmpImage.HasFile)
        {
            string Extenion = Path.GetExtension(uploadEmpImage.PostedFile.FileName);
            if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                Extenion.ToLower() == ".bmp")
            {
                ViewState["ImageName"] = uploadEmpImage.PostedFile.FileName;
                string ext = Extenion.Substring(1);
                uploadEmpImage.SaveAs(MapPath("~/images/Employees/" + ViewState["ImageName"].ToString() + ext));
                //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                lblmsg.Text = "";
            }
            else
            {
                lblmsg.Text = Extenion + " Not Supported";
            }
        }
    }
    

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        ClearForm();
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
        //ddlDesignation.Items.Clear();
        rdoProb.Checked = true;
        //  if (((EMS_Session)Session["EMS_Session"]).Company == null)
        //   {
        //    if (ddlcomp.Items.FindByValue(ddlCompany.SelectedValue) != null)
        //    {
        //       ddlcomp.SelectedValue = ddlCompany.SelectedValue;
        //       BindDesignation(vt_Common.CheckInt(ddlcomp.SelectedValue));
        //    }
        //    	vt_Common.ReloadJS(this.Page, "$('#employess').modal();");
        //   }
        //  else
        //   {
        //   	divComp.Visible = true;
        // BindDesignation((int)Session["CompanyId"]);
        if (Session["CompanyId"] != null)
        {
            ddlcomp.SelectedValue = Session["CompanyId"].ToString();
            ddlcomp.Enabled = false;
            ddlcomp_SelectedIndexChanged(sender, e);
        }
        else
        {
            ddlcomp.SelectedValue = 0.ToString();
            ddlcomp.Enabled = true;
        }
        vt_Common.ReloadJS(this.Page, "$('#empImageView').attr('src', '/images/user-image.png');");
        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");

        //   }
    }

    protected void ddlShift_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int ID = vt_Common.CheckInt(ddlShift.SelectedValue);
                vt_tbl_Shift sh = db.vt_tbl_Shift.FirstOrDefault(x => x.ShiftID == ID);

                //LoadData();
                ViewState["ddlShiftID"] = ID;
                lblShiftInTime.Text = sh.InTime;
                lblShiftOutTime.Text = sh.OutTime;
            }
        }
        catch (DbUpdateException ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (uploadEmpImage.HasFile)
        {
            string Extenion = Path.GetExtension(uploadEmpImage.PostedFile.FileName);
            if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                Extenion.ToLower() == ".bmp")
            {
                ViewState["ImageName"] = Guid.NewGuid() + Extenion;
                string ext = Extenion.Substring(1);
                ViewState["ContenType"] = "Image/" + ext;
                uploadEmpImage.SaveAs(MapPath("~/images/Employees/" + ViewState["ImageName"].ToString()));
                empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                lblmsg.Text = "";
            }
            else
            {
                lblmsg.Text = Extenion + " Not Supported";
            }
        }
    }

    protected void chkCurrentAddress_CheckedChanged(object sender, EventArgs e)
    {
        txtCurrentAddress.Text = txtParmanantAddress.Text;
    }

    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {

        int ID = Convert.ToInt32(ddlcomp.SelectedValue);
        BindDesignation(ID);
        //ddlLineManagerDesignation_SelectedIndexChanged(sender, e);
        ddldepartment_SelectedIndexChanged(sender, e);
        UpView.Update();

        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }

    //protected void ddlLineManagerDesignation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int designationId = vt_Common.CheckInt(ddlLineManagerDesignation.SelectedValue);
    //    int CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
    //    //BindLineManagers(designationId, CompanyID);

    //    BindLineManagers_New(designationId, CompanyID);
    //    UpDetail.Update();
    //}

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DepID = 0;
        DepID = Convert.ToInt32(ddldepartment.SelectedValue);
        SqlParameter[] param =
        {
            new SqlParameter("@DepID",DepID)

    };
        vt_Common.Bind_DropDown(ddlDesignation, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);

        //int DepartmentID = vt_Common.CheckInt(ddldepartment.SelectedValue);
        //int CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
        ////BindLineManagers(DepartmentID, CompanyID);

        //BindLineManagers_New(DepartmentID, CompanyID);
        UpDetail.Update();
    }

    private void BindLineManagers_New(int managerID, int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            try
            {
                ddlLineManager.Items.Clear();
                //var qLineManger = db.vt_tbl_Employee.Where(x => x.EmployeeID == managerID).Distinct().ToList();
                var QryLineManager_HrAdmin = db.vt_tbl_User.Where(x => x.CompanyId == CompanyID && x.RoleId == 2).FirstOrDefault();
                var QryLineManager = db.vt_tbl_Employee.Where(x => x.CompanyID == CompanyID).Distinct().ToList();
                if (QryLineManager_HrAdmin != null && QryLineManager.Count > 0)
                {
                    ddlLineManager.DataSource = QryLineManager;
                    ddlLineManager.DataTextField = "EmployeeName";
                    ddlLineManager.DataValueField = "EmployeeID";
                    ddlLineManager.DataBind();

                    ddlLineManager.Items.Insert(0, new ListItem("Please Select", "0"));
                    ddlLineManager.Items.Insert(1, new ListItem("HR Admin - " + QryLineManager_HrAdmin.UserName, QryLineManager_HrAdmin.UserId.ToString()));
                }
                else
                {

                    ddlLineManager.Items.Insert(0, new ListItem("Please Select", "0"));
                    ddlLineManager.Items.Insert(1, new ListItem("HR Admin - " + QryLineManager_HrAdmin.UserName, QryLineManager_HrAdmin.UserId.ToString()));
                }
            }
            catch (Exception ex)
            {
                ErrHandler.TryCatchException(ex);
                throw ex;
            }
        }
    }

    //void BindLineManagers(int designationId, int CompanyID)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        try
    //        {
    //            ddlLineManager.Items.Clear();
    //            var qLineManger = db.vt_tbl_Employee.Where(x => x.CompanyID == CompanyID && x.DesignationID == designationId).Distinct().ToList();
    //		 ddlLineManager.DataSource = qLineManger;
    //            var QryLineManager_HrAdmin = db.vt_tbl_User.Where(x => x.CompanyId == CompanyID && x.RoleId == 2).FirstOrDefault();

    //            ddlLineManager.DataTextField = "EmployeeName";
    //            ddlLineManager.DataValueField = "EmployeeID";

    //            ddlLineManager.Items.Insert(0, new ListItem("Please Select", "0"));
    //            ddlLineManager.Items.Insert(1, new ListItem(QryLineManager_HrAdmin.UserName, QryLineManager_HrAdmin.UserId.ToString()));
    //}
    //    catch (Exception ex)
    //  {
    //     ErrHandler.TryCatchException(ex);
    //    throw ex;
    //  }
    //  }
    //   }

    protected void Btnexcelimport_Click(object sender, EventArgs e)
    {
        int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        String CompanyId;
        String DepartmentId;
        String DesignationId;
        String EnrollId;
        String FirstName;
        String LastName;
        String RoleId;
        String Sex;
        String Dob;
        String MaritalStatus;
        String Phone;
        String Email;
        String CurrentAddress;
        String Current_City;
        String Current_State;
        String Current_Zip;
        String Current_Country;
        String JoiningDate;
        String JobStatus;
        String BasicSalary;
        String HouseRentAllownce;
        String TransportAllownce;
        String MedicalAllownce;
        String FuelAllownce;
        String FromBank;
        String FromBranch;
        String ToBank;
        String AccountNo;



        string path = Path.GetFileName(FileExportExcel.FileName);
        path = path.Replace(" ", "");


        FileExportExcel.SaveAs(Server.MapPath("~/ExcelFile/") + path);

        //  string file_name = DropDownList1.SelectedItem.Text;

        String ExcelPath = Server.MapPath("~/ExcelFile/") + path;
        OleDbConnection mycon = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + ExcelPath + "; Extended Properties=Excel 8.0; Persist Security Info = False");
        mycon.Open();
        OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", mycon);
        OleDbDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            // Response.Write("<br/>"+dr[0].ToString());
            CompanyId = dr[0].ToString();
            DepartmentId = dr[1].ToString();
            DesignationId = dr[2].ToString();
            EnrollId = dr[3].ToString();
            FirstName = dr[4].ToString();
            LastName = dr[5].ToString();
            RoleId = dr[6].ToString();
            Sex = dr[7].ToString();
            Dob = dr[8].ToString();
            MaritalStatus = dr[9].ToString();
            Phone = dr[10].ToString();
            Email = dr[11].ToString();
            CurrentAddress = dr[12].ToString();
            Current_City = dr[13].ToString();
            Current_State = dr[14].ToString();
            Current_Zip = dr[15].ToString();
            Current_Country = dr[16].ToString();
            JoiningDate = dr[17].ToString();
            JobStatus = dr[18].ToString();
            BasicSalary = dr[19].ToString();
            HouseRentAllownce = dr[20].ToString();
            HouseRentAllownce = dr[21].ToString();
            TransportAllownce = dr[22].ToString();
            MedicalAllownce = dr[23].ToString();
            FuelAllownce = dr[24].ToString();
            FromBank = dr[25].ToString();
            FromBranch = dr[26].ToString();
            ToBank = dr[27].ToString();
            AccountNo = dr[28].ToString();
            savedata(CompanyID, DepartmentId, DesignationId, EnrollId, FirstName, LastName, RoleId, Sex, Dob, MaritalStatus, Phone, Email, CurrentAddress, Current_City, Current_State, Current_Zip, Current_Country, JoiningDate, JobStatus, BasicSalary, HouseRentAllownce, TransportAllownce, MedicalAllownce, FuelAllownce, FromBank, FromBranch, ToBank, AccountNo);



        }
    }
    //Save Excel Sheet Data into DataBase
    //  [Table("YourTableName")]
    private void savedata(int CompanyID, string DepartmentId, string DesignationId, string EnrollId, string FirstName, string LastName, string RoleId, string Sex, string Dob, string MaritalStatus, string Phone, string Email, string CurrentAddress, string Current_City, string Current_State, string Current_Zip, string Current_Country, string JoiningDate, string JobStatus, string BasicSalary, string HouseRentAllownce, string TransportAllownce, string MedicalAllownce, string FuelAllownce, string FromBank, string FromBranch, string ToBank, string AccountNo)
    {

        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int departmentid = 0;
                int designationid = 0;
                //Get Departmentid
                vt_tbl_Department dpt = db.vt_tbl_Department.Where(x => x.Department == DepartmentId).FirstOrDefault();
                departmentid = dpt.DepartmentID;
                vt_tbl_Designation des = db.vt_tbl_Designation.Where(x => x.Designation == DesignationId).FirstOrDefault();
                designationid = des.DesignationID;
                vt_tbl_Employee employee = new vt_tbl_Employee();
                employee.CompanyID = CompanyID;
                employee.DepartmentID = Convert.ToInt32(departmentid);
                employee.DesignationID = Convert.ToInt32(designationid);
               // employee.EnrollId = EnrollId;
                employee.FirstName = FirstName;
                employee.LastName = LastName;
                employee.RoleID = Convert.ToInt32(RoleId);
                employee.Sex = Sex;
                employee.DOB = DateTime.Parse(Dob);
                employee.MartialStatus = MaritalStatus;
                employee.Phone = Phone;
                employee.Email = Email;
                employee.CurrentAddress = CurrentAddress;
                employee.Current_City = Current_City;
                employee.Current_State = Current_State;
                employee.Current_Zip = Current_Zip;
                employee.Current_Country = Current_Country;
                employee.JoiningDate = DateTime.Now;//DateTime.Parse(JoiningDate);
                employee.JobStatus = JobStatus;
                employee.BasicSalary = Convert.ToDecimal(BasicSalary);
                employee.HouseRentAllownce = Convert.ToInt32(HouseRentAllownce);
                employee.TransportAllownce = Convert.ToInt32(TransportAllownce);
                employee.MedicalAllowance = Convert.ToInt32(MedicalAllownce);
                employee.FromBank = FromBank;
                employee.FromBranch = FromBranch;
                employee.ToBank = ToBank;
                //emp.ToBranch = ToBranch;
                employee.AccountNo = AccountNo;
                db.vt_tbl_Employee.Add(employee);
                db.SaveChanges();
                MsgBox.Show(Page, MsgBox.success, "", "Successfully Uploaded");
            }


            //   db.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }

    }

    //public DataTable ReadExcel()
    //{
    //    List<vt_sp_GetComapanyID_ByCompanyName_Result> CompanyNameList = new List<vt_sp_GetComapanyID_ByCompanyName_Result>();
    //    List<VT_SP_GetDepartment_ByCompanyID_Result> Departlist = new List<VT_SP_GetDepartment_ByCompanyID_Result>();
    //    List<VT_SP_GetDesignation_By_DepartmentID_Result> DesignationList = new List<VT_SP_GetDesignation_By_DepartmentID_Result>();

    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        if (FileExportExcel.HasFile)
    //        {
    //            string extension = Path.GetExtension(FileExportExcel.PostedFile.FileName);
    //            string filename = Path.GetFullPath(FileExportExcel.FileName);
    //            IWorkbook workbook = null;
    //            //Stream uploadFileStream = FileUpload1.PostedFile.InputStream;
    //            HttpPostedFile file = Request.Files[0];
    //            MemoryStream mem = new MemoryStream();
    //            mem.SetLength((int)file.ContentLength);
    //            file.InputStream.Read(mem.GetBuffer(), 0, (int)file.ContentLength);
    //            //using (MemoryStream file= new MemoryStream())
    //            //{
    //            if (extension == ".xlsx")
    //            {
    //                workbook = new XSSFWorkbook(mem);
    //            }
    //            else if (extension == ".xls")
    //            {
    //                workbook = new XSSFWorkbook(mem);
    //            }
    //            else
    //            {
    //                MsgBox.Show(Page, MsgBox.danger, "", "This format is not supported");
    //                //throw new Exception("This format is not supported");
    //                return null;
    //            }
    //            //}
    //            //IWorkbook workbook = WorkbookFactory.Create(uploadFileStream);
    //            ISheet sheet = workbook.GetSheetAt(0);
    //            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

    //            IRow headerRow = sheet.GetRow(0);
    //            int cellCount = headerRow.LastCellNum;
    //            int rowCount = headerRow.RowNum;

    //            for (int j = 0; j < cellCount; j++)
    //            {
    //                ICell cell = headerRow.GetCell(j);
    //                dt.Columns.Add(cell.ToString());
    //            }

    //            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
    //            // for (int i = 1; i < 2; i++)
    //            {
    //                IRow row = sheet.GetRow(i);
    //                DataRow dataRow = dt.NewRow();
    //                if (row == null)
    //                {
    //                    break;
    //                }
    //                for (int j = row.FirstCellNum; j < cellCount; j++)
    //                {
    //                    //if(row.GetCell(j))
    //                    if (j == 0)
    //                    {
    //                        CompanyNameList = GetCompanyID_ByCompanyName(row.GetCell(j).ToString());
    //                        dataRow[j] = CompanyNameList[0].CompanyID;
    //                        // dataRow[j] = CompanyNameList.Where(x => x.CompanyName == row.GetCell(j).ToString()).Select(x => x.CompanyID).SingleOrDefault();
    //                    }
    //                    if (j == 1)
    //                    {
    //                        Departlist = GetDepartment_ByCompanyID(CompanyNameList[0].CompanyID);
    //                        dataRow[j] = Departlist.Where(x => x.Department == row.GetCell(j).ToString()).Select(x => x.DepartmentID).SingleOrDefault();

    //                    }
    //                    if (j == 2)
    //                    {
    //                        DesignationList = GetdesignationByCompanyID(Convert.ToInt32(dataRow[j - 1]), CompanyNameList[0].CompanyID);
    //                        dataRow[j] = DesignationList.Where(x => x.Designation == row.GetCell(j).ToString()).Select(x => x.DesignationID).SingleOrDefault();
    //                    }

    //                    if (j > 2 && row.GetCell(j) != null)
    //                    {

    //                        dataRow[j] = row.GetCell(j).ToString();
    //                    }
    //                    //    if (row.GetCell(j) != null)

    //                    //        dataRow[j] = row.GetCell(j).ToString();
    //                }

    //                //  dataRow["DepartmentID"] = GETDepartID(Departlist, dataRow);
    //                //dataRow["DesignationID"] = GetDesignationID(DesignationList, dataRow);
    //                //  dataRow["CompanyID"] = GetCompanyID(Company_Name_List, dataRow);

    //                dt.Rows.Add(dataRow);
    //                dt.CaseSensitive = false;


    //            }



    //            string sqlConnectionString = vt_Common.PayRollConnectionString;
    //            using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
    //            {
    //                bcp.BatchSize = 100000;
    //                bcp.DestinationTableName = "vt_tbl_Employee";

    //                //  bcp.ColumnMappings.Add("EmployeeID", "EmployeeID");
    //                bcp.ColumnMappings.Add("CompanyID", "CompanyID");

    //                // bcp.ColumnMappings.Add("BranchID", "BranchID");
    //                bcp.ColumnMappings.Add("DepartmentID", "DepartmentID");
    //                bcp.ColumnMappings.Add("DesignationID", "DesignationID");
    //                bcp.ColumnMappings.Add("EnrollId", "EnrollId");
    //                bcp.ColumnMappings.Add("FirstName", "FirstName");
    //                bcp.ColumnMappings.Add("LastName", "LastName");
    //                bcp.ColumnMappings.Add("PFType", "PFType");
    //                bcp.ColumnMappings.Add("RoleID", "RoleID");
    //                bcp.ColumnMappings.Add("BloodGroup", "BloodGroup");
    //                bcp.ColumnMappings.Add("Sex", "Sex");
    //                bcp.ColumnMappings.Add("DOB", "DOB");
    //                bcp.ColumnMappings.Add("MartialStatus", "MartialStatus");
    //                bcp.ColumnMappings.Add("Phone", "Phone");
    //                bcp.ColumnMappings.Add("Email", "Email");
    //                bcp.ColumnMappings.Add("CurrentAddress", "CurrentAddress");
    //                bcp.ColumnMappings.Add("JoiningDate", "JoiningDate");
    //                bcp.ColumnMappings.Add("ConfirmationDate", "ConfirmationDate");
    //                bcp.ColumnMappings.Add("JobStatus", "JobStatus");
    //                bcp.ColumnMappings.Add("BasicSalary", "BasicSalary");
    //                bcp.ColumnMappings.Add("HouseRentAllownce", "HouseRentAllownce");
    //                bcp.ColumnMappings.Add("TransportAllownce", "TransportAllownce");
    //                bcp.ColumnMappings.Add("MedicalAllowance", "MedicalAllowance");
    //                bcp.ColumnMappings.Add("FuelAllowance", "FuelAllowance");
    //                bcp.ColumnMappings.Add("ProvidentFund", "ProvidentFund");
    //                bcp.ColumnMappings.Add("SpecialAllowance", "SpecialAllowance");
    //                bcp.ColumnMappings.Add("FromBank", "FromBank");
    //                bcp.ColumnMappings.Add("FromBranch", "FromBranch");
    //                bcp.ColumnMappings.Add("ToBank", "ToBank");
    //                bcp.ColumnMappings.Add("ToBranch", "ToBranch");
    //                bcp.ColumnMappings.Add("AccountNo", "AccountNo");

    //                bcp.BulkCopyTimeout = 0;

    //                bcp.WriteToServer(dt);
    //                bcp.Close();
    //                MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Added");
    //            }





    //            return dt;



    //        }
    //        else
    //        {
    //            return null;
    //        }

    //    }

    //    catch (Exception ex)
    //    {

    //        MsgBox.Show(Page, MsgBox.danger, "", "Please Enter Correct Data");

    //        return null;
    //    }
    //}
    public int GETDepartID(List<VT_SP_GetDepartment_ByCompanyID_Result> Departdt, DataRow rows)
    {
        int DepartID = Departdt.Where(x => x.Department.ToUpper() == rows["DepartmentID"].ToString().ToUpper()).Select(x => x.DepartmentID).FirstOrDefault();
        return DepartID;

    }
    public int GetDesignationID(List<VT_SP_Getdesignation_ByCompanyID_Result> designationdt, DataRow rows)
    {
        int DesignationID = designationdt.Where(x => x.Designation.ToUpper() == rows["DesignationID"].ToString().ToUpper()).Select(x => x.DesignationID).FirstOrDefault();
        return DesignationID;

    }
    public List<VT_SP_GetDesignation_By_DepartmentID_Result> GetdesignationByCompanyID(int? DepartmentID, int? CompanyID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        var objdesignation = db.VT_SP_GetDesignation_By_DepartmentID(DepartmentID, CompanyID);
        List<VT_SP_GetDesignation_By_DepartmentID_Result> result = objdesignation.ToList();
        DataTable Dt = vt_Common.ConvertToDataTable(result);
        return result;
    }


    public List<VT_SP_GetDepartment_ByCompanyID_Result> GetDepartment_ByCompanyID(int? CompanyID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        var objdepartment = db.VT_SP_GetDepartment_ByCompanyID(CompanyID);
        List<VT_SP_GetDepartment_ByCompanyID_Result> result = objdepartment.ToList();
        DataTable Dt = vt_Common.ConvertToDataTable(result);
        return result;
    }

    //public List<Vt_Sp_GetCompanyID_By_CompanyName_Result> GetCompanyID_By_CompanyName(string ComapnyName)
    //{
    //    vt_EMSEntities db = new vt_EMSEntities();
    //    var obj = db.Vt_Sp_GetCompanyID_By_CompanyName(ComapnyName);
    //    List<Vt_Sp_GetCompanyID_By_CompanyName_Result> result = obj.ToList();
    //    DataTable Dt = vt_Common.ConvertToDataTable(result);
    //    return result;
    //}
    public int GET_CompanyId(List<vt_sp_GetComapanyID_ByCompanyName_Result> CompanyDt, DataRow rows)
    {
        int CompanyID = CompanyDt.Where(x => x.CompanyName == rows["CompanyID"].ToString()).Select(x => x.CompanyID).FirstOrDefault();
        return CompanyID;

    }
    public List<vt_sp_GetComapanyID_ByCompanyName_Result> GetCompanyID_ByCompanyName(string companyName)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        var obj = db.vt_sp_GetComapanyID_ByCompanyName(companyName);
        List<vt_sp_GetComapanyID_ByCompanyName_Result> result = obj.ToList();
        DataTable Dt = vt_Common.ConvertToDataTable(result);
        return result;
    }




}

public class DevExtreme_Actions
{
    public int ID { get; set; }
    public string Actions { get; set; }

}
#endregion

