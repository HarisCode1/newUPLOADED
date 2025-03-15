using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Viftech;


public partial class Employes_Edit : System.Web.UI.Page
{
    
    string uploadDirectory = ConfigurationManager.AppSettings["LocalPathPrefix"];
    Custommethods customMethods = new Custommethods();
    int ID = 0;
    DateTime EntryDate = DateTime.Now;
    public static int CompanyID = 0;
    public static int employeeid = 0;
    bool isDataLoaded = false;
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {

            employeeid = Convert.ToInt32(HttpContext.Current.Request.QueryString["ID"]);
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (!IsPostBack)
            {
                txtDOB.Text = EntryDate.ToString("dd/MM/yyyy");
                txtJoiningDate.Text = EntryDate.ToString("dd/MM/yyyy");
                txtpjoiningdate.Text = EntryDate.ToString("dd/MM/yyyy");
                //txtconfirmdate.Text = EntryDate.ToString("dd/MM/yyyy");
                txtpenddate.Text = EntryDate.ToString("dd/MM/yyyy");
                ID = Convert.ToInt32(Request.QueryString["ID"]);
                ViewState["empid"] = ID;
                if (ID > 0)
                {
                 
                   TxtEmployeeCode.ReadOnly = true;
                    ClearForm();
                    FillDetailForm(ID, sender, e);
                }
            
            }
        }
    }
    public void BindLineManager()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        int Des = 0;
        int Dept = 0;
        Des = (Convert.ToInt32(ddlDesignation.SelectedValue));
        Dept = (Convert.ToInt32(ddldepartment.SelectedValue));
        int CompanyId = (Convert.ToInt32(Session["CompanyId"]));
        SqlParameter[] param =
        {
            new SqlParameter("@DesignationID",Des),
            new SqlParameter("@DepartmentID",Dept),
            new SqlParameter("@CompanyID",CompanyId)

        };
        var query = db.vt_tbl_Designation.Where(x => x.DesignationID.Equals(Des) && x.TopDesignationID == 0).FirstOrDefault();
        int id = 0;
        if (query != null)
        {
            id = Des;
        }
        //var qry = db.VT_sp_BindLineManager(Des, Dept, CompanyId);
        //if (qry != null)
        //{
        //    ddlLineManager.DataSource = qry;
        //    ddlLineManager.DataTextField = "Designation";
        //    ddlLineManager.DataValueField = "DesignationID";
        //    ddlLineManager.DataBind();
        //}
        vt_Common.Bind_DropDown(ddlLineManager, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
        if (ddlLineManager.Items.Count == 1 && Des == id)
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
        else if (ddlLineManager.Items.Count == 1 && Des > 1)
        {
            vt_Common.Bind_DropDown(ddlLineManager, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
        }
        if (TxtFirstName.Text == "")
        {
            MsgBox.Show(Page, MsgBox.danger, "", "Please Enter First Name");


        }
        else
        {
            //Txtusername.Text = TxtFirstName.Text + (new Random()).Next(1, 999);
            // Txtusername.Text = TxtFirstName.Text;
            int companyid = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);

            //var EmployeeObj = db.vt_tbl_Employee.Where(x => x.EmployeeName == Txtusername.Text && x.CompanyID == companyid && x.JobStatus == "Active").FirstOrDefault();
            //if (EmployeeObj != null)
            //{
            //    MsgBox.Show(Page, MsgBox.danger, "", "User Name Already Exist");
            //    ddEmployeType.Items.Insert(0, new ListItem("    ", "0"));
            //    //return true;
            //}
            //else
            //{
            //    // MsgBox.Show(Page, MsgBox.danger, "", "Please Enter First Name");
            //    //return false;
            //}
        }

    }
    protected void ddlLineManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        vt_EMSEntities db = new vt_EMSEntities();

        try
        {
            int desiId = Convert.ToInt32(ddlDesignation.SelectedValue);
            //if (ddlDesignation.SelectedIndex >0)
            //{
            //    ddlLineManager.Items.Clear();

            //}
            //else
            //{
                if (TxtFirstName.Text == "")
                {
                    ddlLineManager.Items.Clear();
                    MsgBox.Show(Page, MsgBox.danger, "", "Please Enter First Name");
                    ddlDesignation.SelectedIndex = 0;
                    UpDetail.Update();
                }
                else
                {
                    ddlLineManager.Items.Clear();
                    BindLineManager();
                   // ddlLineManager.Items.RemoveAt(0);
                    if (desiId != 0)
                    {

                        var query = db.vt_tbl_Designation.Where(x => x.DesignationID == desiId).FirstOrDefault();
                        if (query != null)
                        {
                            var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query.ReportTo).ToList();
                            if (qry.Count > 0)
                            {
                                //ddltopdesignation.DataSource = qry;
                                //ddltopdesignation.DataTextField = "Topdesignations";
                                //ddltopdesignation.DataValueField = "Id";
                                //ddltopdesignation.DataBind();
                                //ddltopdesignation.Visible = true;
                                //div2.Visible = true;
                                ddlLineManager.DataSource = qry;
                                ddlLineManager.DataTextField = "Topdesignations";
                                ddlLineManager.DataValueField = "Id";
                                ddlLineManager.DataBind();
                                ddlLineManager.Visible = true;
                                //ddlLineManagername.Items.Clear();
                                //div2.Visible = true;
                            }
                            else
                            {
                                ddltopdesignation.Visible = false;
                                div2.Visible = false;
                            }
                        }
                    }
                }
            //}


        }
        catch (Exception)
        {

            throw;
        }
        //Previous One
        //ddlLineManager.Items.Clear();
        //BindLineManager();
    }
    //protected void ddlLineManager_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    ddlLineManager.Items.Clear();
    //    BindLineManager();
    //}
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
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            int ID = (Convert.ToInt32(Session["CompanyId"]));
            BindDesignation(ID);
            //ddlLineManagerDesignation_SelectedIndexChanged(sender, e);
            ddldepartment_SelectedIndexChanged(sender, e);
            int desiId = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (desiId != 0)
            {

                var query = db.vt_tbl_Designation.Where(x => x.DesignationID == desiId).FirstOrDefault();
                if (query != null)
                {
                    var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query.ReportTo).ToList();
                    if (qry.Count > 0)
                    {
                        ddlLineManager.DataSource = qry;
                        ddlLineManager.DataTextField = "Topdesignations";
                        ddlLineManager.DataValueField = "Id";
                        ddlLineManager.DataBind();
                        ddlLineManager.Visible = true;
                    }
                    else
                    {
                        ddltopdesignation.Visible = false;
                        div2.Visible = false;
                    }
                }
            }
            //UpView.Update();

            vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
        }
        catch (Exception ex)
        {
            Response.Write("An error occurred: " + ex.Message);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
        }
    }

    
    public void FillDetailForm(int ID, object sender, EventArgs e)
    {
        try
        {

            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (isDataLoaded) return;
                vt_tbl_Employee em = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ID);
                ViewState["ManagerID"] = em.ManagerID;
                TxtFirstName.Text = em.FirstName;
                TxtLastName.Text = em.LastName;
                Txtemail.Text = em.Email;
                txtreligion.Text = em.Religion;
                txtHidden_Academic.Value = "";
                //if (((EMS_Session)Session["EMS_Session"]).Company != null)
                //if (((PayRoll_Session)Session["PayrollSess"]).UserName == "SUPERADMIN")

                //{
                //    ddlcomp.SelectedIndex = 0;
                //}
                //else
                //{
                //    ddlcomp.SelectedValue = em.CompanyID.ToString();
                //}

                // ddlcomp.SelectedValue = vt_Common.CheckString(em.CompanyID);
                //SetDdl_value(ddlcomp, vt_Common.CheckString(em.CompanyID));
                BindDep();
                ddldepartment.SelectedValue = em.DepartmentID.ToString();
                ddldepartment_SelectedIndexChanged(sender, e);
                ddlDesignation.SelectedValue = em.DesignationID.ToString();
                ddlLineManager_SelectedIndexChanged(sender, e);
                //BindLineManager();
                ddlDesignation.SelectedValue = em.DesignationID.ToString();
                SetDdl_value(ddlLineManager, em.ManagerID.ToString());
                //ddlcomp_SelectedIndexChanged(sender,e);

                int desiId = Convert.ToInt32(ddlDesignation.SelectedValue);
                if (desiId != 0)
                {
                    var query = db.vt_tbl_Designation.Where(x => x.DesignationID == desiId).FirstOrDefault();
                    if (query != null)
                    {
                        var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query.ReportTo).ToList();
                        if (qry.Count > 0)
                        {
                            ddlLineManager.DataSource = qry;
                            ddlLineManager.DataTextField = "Topdesignations";
                            ddlLineManager.DataValueField = "Id";
                            ddlLineManager.DataBind();
                            ddlLineManager.Visible = true;
                       
                        }
                        else
                        {
                            ddltopdesignation.Visible = false;
                            div2.Visible = false;
                        }
                    }
                }





                BindDesignation(ID);
                ddEmployeType.SelectedValue = (em.Type);
                if (ddEmployeType.SelectedValue != 0.ToString())
                {
                    //    ddEmployeType.Enabled = false;

                }
                else
                {
                    //  ddEmployeType.Enabled = true;

                }

                hdprofileimage.Value = em.ImageName;
                //Txtusername.Text = em.EmployeeName;
                //TxtPassword.Attributes.Add("value", vt_Common.Decrypt(em.EmpPassword));
                //TxtConfirmPassword.Attributes.Add("value", vt_Common.Decrypt(em.EmpPassword));
                TxtEmployeeCode.Text = em.EnrollId;
                ddlempguardian.Text = em.FatherHusbandName;
                //txtguardianname.Text = em.txtguardianname;
                //dynamicLabel1.Text = "Name of " + vt_Common.IsStringEmpty(em.FatherHusbandName);
                txtguardianname.Text = em.RelationWithEmployee;

                //ddlempguardian.SelectedValue=em.FatherHusbandName;

                SetDdl_value(ddlLineManager, em.ManagerID.ToString());
                SetDdl_value(ddlLineManager, em.ManagerID.ToString());
                SetDdl_value(ddlLineManager, em.ManagerID.ToString());

               
              
                
                txtDOB.Text = em.DOB.HasValue ? em.DOB.Value.ToString("dd/MM/yyyy") : "";

                
                rdoMarriedStatus.Text = em.MartialStatus;
                //lblNIC.Text = em.NICImage;
                TxtHomePhone.Text = em.HomePhone;
                txtcnic.Text = em.CNIC;
                txtemgnumber.Text = em.Phone;
                ddlSex.SelectedValue = em.Sex;
                //JOB INFORMATION
                //ddlBranch.SelectedValue = em.BranchID.ToString();
                SetDdl_value(ddlBranch, em.BranchID.ToString());
                //ddlEmpType.SelectedValue = em.Type;
                //emp.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                // ddlBank.SelectedValue = em.BankID.ToString();
                SetDdl_value(ddlBank, em.BankID.ToString());
                txtAccountNo.Text = em.BankAccountNo;
                txttax.Text = em.Tax.ToString();
                txtaccounttitle.Text = em.AccountTitle;
                txtaccounttitile2.Text = em.AccountTitle;
                ddlpaymentmethod.Text = em.PaymentMethod;
                txtJoiningDate.Text = em.JoiningDate.HasValue ? em.JoiningDate.Value.ToString("dd/MM/yyyy") : "";

                //txtJoiningDate.Text =  em.JoiningDate != null ? em.JoiningDate.Value.ToShortDateString() : "";


                //For Probation Period
                if (em.IsConfirmed == false)
                {
                    rdoisconfirm.Text = "Probation";
                    txtprobationperiod.Text = em.ProvisionalPeriod;
                    txtprobationperiod.Text = em.ProvisionalPeriod;
                }
                else
                {
                    rdoisconfirm.Text = "Confirm";
                    txtconfirmdate.Text = em.ConfirmationDate.ToString();
                }
              
                //rdoJobStatus.SelectedValue = em.JobStatus;

                if (em.JobStatus == "Deactive")
                {
                    rdoJobStatus.Text = "Deactive";
                }
                else
                {
                    rdoJobStatus.Text = "Active";
                }

                string isConf_Pro = em.JobPeriod;

                //SHIFT WOFF
                //rdoShiftType.SelectedValue = em.ShiftType;


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

    //            public string Permenant_Address1 { get; set; }
    //public string Permenant_Address2 { get; set; }
    //public string Permenant_Address3 { get; set; }
    //public string Permenant_City { get; set; }
    //public string Permenant_State { get; set; }
    //public string Permenant_Zip { get; set; }
    //public string Permenant_Country { get; set; }

    txtBasicSalary.Text = vt_Common.CheckString(em.BasicSalary);
                bool isTrue = (bool)(em.ProvidentFund == null ? false : em.ProvidentFund);
                bool isGraTrue = (bool)(em.IsGraduaty == null ? false : em.IsGraduaty);
                //txtbankname.Text = vt_Common.CheckString(em.FromBank);
                //txtbrachfrom.Text = vt_Common.CheckString(em.FromBranch);
                //txtbankto.Text = vt_Common.CheckString(em.ToBank);
                txtbranchcode.Text = vt_Common.CheckString(em.ToBranch);
                txtfrombranch.Text = em.FromBranch;
                txtaccount.Text = vt_Common.CheckString(em.AccountNo);
                TxtCurrent_Address1.Text = em.Current_Address1;
                TxtCurrent_City.Text = em.Current_City;
                TxtCurrent_State.Text = em.Current_State;
                TxtCurrent_Zip.Text = em.Current_Zip;
                TxtCurrent_Country.Text = em.Current_Country;
                txthouserentallowance.Text = Convert.ToDecimal(em.HouseRentAllownce).ToString();
                txttransportallowance.Text = Convert.ToDecimal(em.TransportAllownce).ToString();
                txtovertimeamount.Text = Convert.ToDecimal(em.OverTime).ToString();
                txtmedicalalowwance.Text = Convert.ToDecimal(em.MedicalAllowance).ToString();
                ddlbankname.Text = em.FromBank;
                txtPermanentAddress1.Text = em.Permenant_Address1;
                txtPermanentCity.Text = em.Permenant_City;
                txtPermanentState.Text = em.Permenant_State;
                txtPermanentPostalCode.Text = em.Permenant_Zip;
                txtPermanentCounry.Text = em.Permenant_Country;

                if (!string.IsNullOrEmpty(em.ImageName))
                {
                    //hdEmpPhotoID.Value = em.ImageName.ToString();
                    //empImageVieW.ImageUrl = hdEmpPhotoID.Value;
                    hdEmpPhotoID.Value = em.ImageName.ToString();
                    empImageVieW.ImageUrl = "~/images/Employees/ProfileImage/" + hdEmpPhotoID.Value;
                }
                else
                {
                    empImageVieW.ImageUrl = "~/assets/img/user2-160x160.jpg";
                }
                //if (!string.IsNullOrEmpty(em.NICImage))
                //{
                //    hdNICName.Value = em.NICImage.ToString();
                //    //NICUpload.HasFile = "~/images/Employees/" + hdNICName.Value;
                //}
                //else
                //{
                //    //string NICUpload = Server.MapPath("images/Employees"+em.NICImage);
                //    //empImageView.ImageUrl = "~/assets/img/user2-160x160.jpg";
                //}
                //if (ddEmployeType.SelectedValue == "Contract")
                //{
                //    mybtn1.Visible = false;
                //}
                //else
                //{
                //  mybtn1.Visible = true;





                //Qualification Details Data
                var qualification = db.sp_GetEditQualificationDetails(em.EmployeeID, 1).ToList();  //Academic Qualification
                var json = JsonConvert.SerializeObject(qualification);
                if (qualification.Count > 0)
                {
                    
                    txtHidden_Academic.Value = json;
                }

                //Previous Job Information
                var PjobInfo = db.vt_tbl_PreviousJobDetails.Where(x => x.EmployeeID == em.EmployeeID).ToList();  //Previous Job Information
                if (PjobInfo.Count>0)
                {
                    var Pjobinfojson = JsonConvert.SerializeObject(PjobInfo);
                    hdnpjob.Value = Pjobinfojson;
                }

              

                Console.WriteLine(txtHidden_Academic.Value);
                //Certificate Details Data
                var Certificate = db.sp_GetEditQualificationDetails(em.EmployeeID, 2).ToList();  //Academic Qualification

                var certificatejson = JsonConvert.SerializeObject(Certificate);

                txt_HiidenCertificate.Value = certificatejson;
                //LoadData();
                //}
                isDataLoaded = true;
            }
            ViewState["PageID"] = ID;

        }
         
         catch (Exception ex)
        {
            Response.Write("An error occurred: " + ex.Message);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred: " + ex.Message + "');", true);
        }
}
    protected void btnSaveEmployee_Click(object sender, EventArgs e)
    {
        int EmployeeId = Convert.ToInt32(Request.QueryString["ID"]);
        try
        {
            DeleteAllPath();
            using (var db = new vt_EMSEntities())
            {
                vt_tbl_Employee emp = new vt_tbl_Employee();

                //int? Employid = Convert.ToInt32(Request.QueryString["ID"]);
                vt_tbl_User user = new vt_tbl_User();
                user = db.vt_tbl_User.Where(w => w.EmployeeID.Equals(EmployeeId)).FirstOrDefault();

                var guid = Guid.NewGuid().ToString();
                int Company_ID = Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];
                emp = db.vt_tbl_Employee.Where(w => w.EmployeeID.Equals(EmployeeId)).FirstOrDefault();
                if (emp != null)
                {
                    //db.vt_tbl_Resignations.Remove(user);
                    ////  File.Delete(Server.MapPath(user.Image));    
                    //dbContext.SaveChanges();
                }
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                string joiningDate1 = emp.JoiningDate.HasValue ? emp.JoiningDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                string dobDate1 = emp.DOB.HasValue ? emp.DOB.Value.ToString("dd/MM/yyyy") : string.Empty;
                DateTime? joiningDate = emp.JoiningDate;

                // var record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.CompanyID == Company_ID && o.EmployeeName.ToLower().Replace(" ", "").Equals(Txtusername.Text.ToLower().Replace(" ", "")));
                //if (record != null)
                //{
                //    vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same username already exist');");
                //}
                //else
                //{
                //var record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.EnrollId.ToLower().Replace(" ", "").Equals(TxtEmployeeCode.Text.ToLower().Replace(" ", "")));
                //if (record != null)
                //{
                //    vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same code already exist');");
                //}
                //else
                //{
                        if (ViewState["ddlShiftID"] == null)
                        {
                            //   ViewState["ddlShiftID"] = ddlShift.SelectedValue;
                        }
                        else
                        {
                            string val = ViewState["ddlShiftID"].ToString();

                            int ID = vt_Common.CheckInt(val);
                            vt_tbl_Shift sh = db.vt_tbl_Shift.FirstOrDefault(x => x.ShiftID == ID);
                            if (sh != null)
                            {
                            }
                        }

                        //ACCOUNT SETUP//
                        emp.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtFirstName.Text);
                        emp.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtLastName.Text);
                        //emp.CompanyID = int.Parse(ddlcomp.SelectedValue);
                        emp.Email = Txtemail.Text;
                        emp.DesignationID = int.Parse(ddlDesignation.SelectedValue);
                        emp.Type = ddEmployeType.Text;
                        emp.RoleID = 4;
                        emp.Religion = txtreligion.Text;
                        emp.EmployeeName = TxtFirstName.Text + TxtLastName.Text;

                       
                        emp.ManagerID = vt_Common.CheckInt(ddlLineManager.SelectedValue);
                        if (ddlLineManager.SelectedItem.Text.Contains("HR Admin"))
                        {
                            emp.IsHRLineManager = true;
                        }
                        else
                        {
                            emp.IsHRLineManager = false;
                        }

                
                        emp.FatherHusbandName = ddlempguardian.Text;
                        emp.RelationWithEmployee =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtguardianname.Text);
                        emp.HomePhone = TxtHomePhone.Text;
                        emp.CNIC = txtcnic.Text;
                        emp.Phone = txtemgnumber.Text;
                        emp.Sex = ddlSex.SelectedValue;
                        if (!string.IsNullOrEmpty(txtDOB.Text))
                        {
                            string txtDateValue  =       txtDOB.Text;
                            DateTime? resultDate = customMethods.GetDateFromTextBox(txtDateValue);
                            if (dobDate1 != txtDateValue)
                            {
                                emp.DOB = resultDate;
                            }
                        }
                        emp.MartialStatus = rdoMarriedStatus.Text;
                        emp.CurrentAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCurrentAddress.Text);

                        //JOB INFORMATION
                        emp.BranchID = Convert.ToInt32(ddlBranch.SelectedValue);
                        emp.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        emp.BankID = Convert.ToInt32(ddlBank.SelectedValue);
                        // emp.BankAccountNo = txtAccountNo.Text;
                        emp.AccountNo = txtaccount.Text;
                       // emp.Tax =Convert.ToDecimal(txttax.Text);
                       // emp.AccountNo = Convert.ToInt64(txtaccount.Text);
                        if (!string.IsNullOrEmpty(txtJoiningDate.Text))
                        {
                            string txtDateValue1 = txtJoiningDate.Text;
                            DateTime? resultDate1 = customMethods.GetDateFromTextBox(txtDateValue1);
                            //emp.JoiningDate = resultDate1;
                            //emp.JoiningDate = Convert.ToDateTime(txtDateValue1);


                               if (joiningDate1 == txtDateValue1)

                               {
                                 emp.JoiningDate = joiningDate;
                               }
                                else
                                {
                                    emp.JoiningDate = resultDate1;
                                    
                                }
                           
                        }
                        emp.JobStatus = rdoJobStatus.SelectedValue;

                        //For Probatio Period
                        emp.ProvisionalPeriod = txtprobationperiod.Text;
                        string rdovalue = rdoisconfirm.Text;
                        if (rdovalue == "Probation")
                        { emp.IsConfirmed = false; 
                        } 
                        else 
                        {   emp.IsConfirmed = true;
                            string txtDateValue1 = txtconfirmdate.Text;
                            DateTime? resultDate1 = customMethods.GetDateFromTextBox(txtDateValue1);
                            emp.IsConfirmed = true;
                            emp.ConfirmationDate = resultDate1;
                        }
    
                        emp.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        emp.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                        emp.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        emp.OverTime = vt_Common.Checkdecimal(txtovertimeamount.Text);
                        emp.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);


                //emp.NICImage = hdNICName.Value;
                //if (hdNICName.Value == "")
                //{
                //    hdNICName.Value = null;
                //    emp.NICImage = hdNICName.Value;
                //}
                //else
                //{
                //    emp.NICImage = hdNICName.Value == "" ? hdNICImage.Value : hdNICName.Value;
                //}

                //string BankName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtbankname.Text);
                //emp.FromBank = vt_Common.CheckString(BankName);
                //emp.FromBranch = vt_Common.CheckString(txtbrachfrom.Text);
                //emp.ToBank = vt_Common.CheckString(txtbankto.Text);
                        emp.FromBranch = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtfrombranch.Text);
                        emp.ToBranch = vt_Common.CheckString(txtbranchcode.Text);
                       // emp.AccountNo = vt_Common.CheckInt64(txtaccount.Text);
                        if (ViewState["PageID"] != null)
                        {
                            emp.EmployeeID = vt_Common.CheckInt(ViewState["PageID"]);
                            db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                            
                        }
                        else
                        {
                            db.vt_tbl_Employee.Add(emp);
                        }
                        emp.Current_Address1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_Address1.Text);
                        emp.Current_City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_City.Text);
                        emp.Current_State = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_State.Text);
                        emp.Current_Zip = TxtCurrent_Zip.Text;
                        emp.Current_Country = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_Country.Text);
          

                        emp.Permenant_Address1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentAddress1.Text);
                        emp.Permenant_City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentCity.Text);
                        emp.Permenant_Country = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentCounry.Text);
                        emp.Permenant_State = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentState.Text);
                        emp.Permenant_Zip = txtPermanentPostalCode.Text;

                        db.SaveChanges();
                            

                        Session["EmployeeId"] = ViewState["EmployeeId"];
                        ViewState["EmployeeId"] = emp.EmployeeID;
                        EmployeeId = Convert.ToInt32(ViewState["EmployeeId"]);
                        vt_tbl_Employee empc = new vt_tbl_Employee();
                        empc = db.vt_tbl_Employee.Where(w => w.EmployeeID.Equals(EmployeeId) && w.IsConfirmed == true && w.ConfirmationDate != null).FirstOrDefault();

                        if (empc != null)
                        {
                            ProcedureCall.VT_SP_AddLeavesAfterConfirmation(EmployeeId, Company_ID, Convert.ToDateTime(empc.ConfirmationDate), empc.Sex);
                        }


                        // Update active status in user table 
                        vt_tbl_Employee em = new vt_tbl_Employee();
                        em = db.vt_tbl_Employee.Where(w => w.EmployeeID.Equals(EmployeeId)).FirstOrDefault();
                        if (em.JobStatus == "Deactive")
                        {
                             user.Active = false;
                        }
                        else
                        {
                             user.Active = true;
                        }
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();


                        ViewState["EmployeeId"] = emp.EmployeeID;
                        int EmployeeIdd = Convert.ToInt32(ViewState["EmployeeId"]);
                        vt_tbl_Employee empp = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmployeeIdd).FirstOrDefault();
                        if (empp != null)
                        {
                            string fullpathimagename = empp.ImageName;
                            string fullpathnicimage = empp.NICImage;
                            if (System.IO.File.Exists(fullpathimagename) && System.IO.File.Exists(fullpathnicimage))
                            {
                                System.IO.File.Delete(empp.ImageName);
                                System.IO.File.Delete(empp.NICImage);
                            }
                            else
                            {

                            }
                        }
                        if (uploadEmpImage.HasFile)
                        {
                            emp.ImageName = guid + "-" + EmployeeId + uploadEmpImage.PostedFile.FileName;
                        }
                        else
                        {
                            emp.ImageName = hdprofileimage.Value;
                        }

                //if (NICUpload.HasFile)
                //{
                //    emp.NICImage = guid + "-" + EmployeeId + NICUpload.PostedFile.FileName;
                //}
                //else
                //{
                //    emp.NICImage = lblNIC.Text == "" ? lblNIC.Text : lblNIC.Text;
                //    //emp.ImageName = hdNICName.Value == "" ? hdNICImage.Value : hdNICImage.Value;
                //}

                // Acadmic Info Start


                        vt_tbl_QualificationDetails obj_qualification = new vt_tbl_QualificationDetails();
                        vt_tbl_PreviousJobDetails obj_pjobinfo = new vt_tbl_PreviousJobDetails();
                        string Pjobinfo = hdnpjob.Value;
                        string qualification = txtHidden_Academic.Value;
                        Console.WriteLine(txtHidden_Academic.Value);
                        List<vt_tbl_QualificationDetails> listQualificationAcademic = JsonConvert.DeserializeObject<List<vt_tbl_QualificationDetails>>(qualification);
                        List<vt_tbl_PreviousJobDetails> listpjobinfo = JsonConvert.DeserializeObject<List<vt_tbl_PreviousJobDetails>>(Pjobinfo);
                        var academic_qualification = db.sp_GetEditQualificationDetails(emp.EmployeeID, 1).Select(s => new vt_tbl_QualificationDetails { Id = s.Id }).ToList();  //Academic Qualification
                        var pjobinfodetail = db.vt_tbl_PreviousJobDetails.Where(x => x.EmployeeID == emp.EmployeeID).ToList();

                        //Removing Old Records from jobinfo on behalf of EmpID
                        if (pjobinfodetail.Count() > 0)
                                {
                            foreach (var item in pjobinfodetail)
                            {
                                var pjob_obj = db.vt_tbl_PreviousJobDetails.Find(item.Id);
                                db.vt_tbl_PreviousJobDetails.Remove(pjob_obj);
                                db.SaveChanges();
                                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }
                           
                        }
                if (listpjobinfo != null)
                {


                    foreach (var item in listpjobinfo)
                    {
                        obj_pjobinfo.EmployeeID = emp.EmployeeID;// CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Qualification);
                        obj_pjobinfo.PreviousCompanyName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.PreviousCompanyName); //Convert.ToInt32(item.Year);
                        obj_pjobinfo.PreviousDesignation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.PreviousDesignation);
                        obj_pjobinfo.JoiningDate = item.JoiningDate;
                        obj_pjobinfo.EndDate = item.EndDate;
                        obj_pjobinfo.CompanyID = emp.CompanyID; ;
                        obj_pjobinfo.CreateDate = DateTime.Now;
                        db.vt_tbl_PreviousJobDetails.Add(obj_pjobinfo);

                        db.SaveChanges();
                    }
                }

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

                if (listQualificationAcademic != null)
                {


                    foreach (var item in listQualificationAcademic)
                    {
                        obj_qualification.InstituteName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.InstituteName);
                        obj_qualification.Qualification = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Qualification);
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

                                try
                                {
                                    var certificate_obj = db.vt_tbl_QualificationDetails.Find(item.Id);
                                    db.vt_tbl_QualificationDetails.Remove(certificate_obj);
                                    db.SaveChanges();
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

                                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }

                            //academic_qualification.ForEach(x => db.vt_tbl_QualificationDetails.Remove(x));
                            //  db.Entry(academic_qualification).State = System.Data.Entity.EntityState.Deleted;
                        }

                        foreach (var item in listQualificationCertificate)
                        {
                            obj_certificate.InstituteName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.InstituteName);
                            obj_certificate.Qualification = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Qualification);
                            obj_certificate.Year = Convert.ToInt32(item.Year);
                            obj_certificate.Grade = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Grade);// item.Qualification();
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
                        GSalary.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                        GSalary.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        GSalary.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);
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


                        //LOG ENTRY
                        //var LastRecord = db.vt_tbl_Employee.OrderByDescending(x => x.EmployeeID).FirstOrDefault();
                        vt_tbl_Employee_TransferLog empTrans = new vt_tbl_Employee_TransferLog();
                        empTrans.EmployeeID = emp.EmployeeID;
                       // empTrans.CompanyID = Convert.ToInt32(ddlcomp.SelectedValue);
                        empTrans.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        empTrans.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        empTrans.ManagerID = Convert.ToInt32(ddlLineManager.SelectedValue);
                        empTrans.EmployeeType = ddEmployeType.SelectedValue;
                        empTrans.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtFirstName.Text);
                        empTrans.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtLastName.Text);
                        empTrans.Email = Txtemail.Text;
                        empTrans.EntryDate = EntryDate;
                        empTrans.Action = "Update";
                        db.Entry(empTrans).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        

                        MsgBox.Show(Page, MsgBox.success, emp.EmployeeName, "Successfully Saved!");
                        ClearForm();
                        if (uploadEmpImage.HasFile)
                        {
                            string Extenion = Path.GetExtension(uploadEmpImage.PostedFile.FileName);
                            if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                                Extenion.ToLower() == ".bmp")
                            {
                                ViewState["ImageName"] = uploadEmpImage.PostedFile.FileName;
                                string ext = Extenion.Substring(1);
                                uploadEmpImage.SaveAs(MapPath("~/images/Employees/ProfileImage/" + guid + "-" + EmployeeId + ViewState["ImageName"].ToString()));
                                //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                                lblmsg.Text = "";
                            }
                            else
                            {
                                lblmsg.Text = Extenion + " Not Supported";
                            }
                           
                           
                            //LoadData();
                            //UpView.Update();
                        }
                        //if (NICUpload.HasFile)
                        //{
                        //    string Extenions = Path.GetExtension(NICUpload.PostedFile.FileName);
                        //    if (Extenions.ToLower() == ".jpg" || Extenions.ToLower() == ".png" || Extenions.ToLower() == ".gif" || Extenions.ToLower() == ".jpeg" ||
                        //        Extenions.ToLower() == ".bmp")
                        //    {
                        //        ViewState["ImageName"] = NICUpload.PostedFile.FileName;
                        //        string ext = Extenions.Substring(1);
                        //        NICUpload.SaveAs(MapPath("~/images/Employees/NicImage/" + guid + "-" + EmployeeId + ViewState["ImageName"].ToString()));
                        //        //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                        //        lblmsg.Text = "";
                        //    }
                        //    else
                        //    {
                        //        lblmsg.Text = Extenions + " Not Supported";
                        //    }
                        //}
        
                     Response.Redirect("Employee.aspx");




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

    
        
    }
    public void DeleteAllPath()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            try
            {
                int Company_ID = Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];
                int getempid = Convert.ToInt32(ViewState["empid"]);
                var checkempid = db.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(getempid)).FirstOrDefault();
                if (checkempid != null)
                {
                    string fullpathprofileimage = Server.MapPath("~/images/Employees/ProfileImage/" + checkempid.ImageName);
                    string fullpathnicimage = Server.MapPath("~/images/Employees/NicImage/" + checkempid.NICImage);
                    if (uploadEmpImage.HasFile == true)
                    {
                        if (File.Exists(fullpathprofileimage))
                        {
                            File.Delete(fullpathprofileimage);
                        }
                        else
                        {

                        }
                    }
                    //if (NICUpload.HasFile == true)
                    //{
                    //    if (File.Exists(fullpathnicimage))
                    //    {
                    //        File.Delete(fullpathnicimage);
                    //    }
                    //    else
                    //    { }
                    //}
                    else
                    {

                    }
                 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
    private void ClearForm()
    {
        ViewState["PageID"] = null;
        //Clear Upload Fields     
        txtHidden_Academic.Value = "";
        txt_HiidenCertificate.Value = "";
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#employes').modal('hide');");
    }
    #region DataBind
    private void BindDesignation(int CompanyID)
    {
       
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
        }
    }
    #endregion

    #region Ddl_IndexChange
    //protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int DepartmentID = vt_Common.CheckInt(ddldepartment.SelectedValue);
    //    int CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
    //    //BindLineManagers(DepartmentID, CompanyID);

    //    //BindLineManagers_New(DepartmentID, CompanyID);
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
        //UpDetail.Update();
    }
    #endregion
    void SetDdl_value(DropDownList Ddl, string Value)
    {
        for (int i = 0; i < Ddl.Items.Count; i++)
        {
            if (Ddl.Items[i].Value == Value)
            {
                Ddl.SelectedValue = Value;
                break;
            }
            else
            {
                Ddl.SelectedIndex = 0;
            }
        }
    }
    protected void btnsavecontractemployee_Click(object sender, EventArgs e)
    {
        try
        {
            DeleteAllPath();
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_Employee emp = new vt_tbl_Employee();
                int Company_ID = Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];
                var guid = Guid.NewGuid().ToString();
                //if (((EMS_Session)Session["EMS_Session"]).Company == null)
                //{
                //    emp.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                //    Company_ID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                //}
                //else
                //{
                //    emp.CompanyID = vt_Common.CompanyId;
                //    Company_ID = vt_Common.CompanyId;
                //}

                int recordID = vt_Common.CheckInt(ViewState["PageID"]);

               // var record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.CompanyID == Company_ID && o.EmployeeName.ToLower().Replace(" ", "").Equals(Txtusername.Text.ToLower().Replace(" ", "")));
                //if (record != null)
                //{
                //    vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same username already exist');");
                //}
                //else
                //{
                var record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.EnrollId.ToLower().Replace(" ", "").Equals(TxtEmployeeCode.Text.ToLower().Replace(" ", "")));
                    if (record != null)
                    {
                        vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same code already exist');");

                    }
                    else
                    {
                        if (ViewState["ddlShiftID"] == null)
                        {

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
                        emp.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtFirstName.Text);
                        emp.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtLastName.Text);
                        //emp.CompanyID = int.Parse(ddlcomp.SelectedValue);
                        emp.Email = Txtemail.Text;
                        emp.DesignationID = int.Parse(ddlDesignation.SelectedValue);
                        emp.Type = ddEmployeType.Text;
                        emp.RoleID = 4;
                        emp.Religion = txtreligion.Text;
                    //if (Txtusername.Text != "" || !string.IsNullOrEmpty(Txtusername.Text) || !string.IsNullOrWhiteSpace(Txtusername.Text))
                    //{
                         emp.EmployeeName = TxtFirstName.Text+ TxtLastName.Text;
                        //}
                        //emp.EnrollId = TxtEmployeeCode.Text;
                        //if (TxtPassword.Text != "" || !string.IsNullOrEmpty(TxtPassword.Text) || !string.IsNullOrWhiteSpace(TxtPassword.Text))
                        //{
                        //   emp.EmpPassword = vt_Common.Encrypt(TxtPassword.Text);
                        //}

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
                        emp.FatherHusbandName =ddlempguardian.Text;
                        emp.RelationWithEmployee = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtguardianname.Text);
                        if (!string.IsNullOrEmpty(txtDOB.Text))
                        {
                            emp.DOB = DateTime.Parse(txtDOB.Text).Add(DateTime.Now.TimeOfDay);
                        }
                        emp.MartialStatus = rdoMarriedStatus.Text;
                        emp.CurrentAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCurrentAddress.Text);
                        //JOB INFORMATION
                        emp.BranchID = Convert.ToInt32(ddlBranch.SelectedValue);
                        //emp.Type = ddlEmpType.SelectedValue;
                        emp.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        emp.BankID = Convert.ToInt32(ddlBank.SelectedValue);
                        //emp.BankAccountNo = txtAccountNo.Text;
                        //  emp.AccountNo = Convert.ToInt64(txtaccount.Text);
                      //  emp.AccountNo = vt_Common.CheckInt64(txtaccount.Text);
                        if (!string.IsNullOrEmpty(txtJoiningDate.Text))
                        {
                            emp.JoiningDate = DateTime.Parse(txtJoiningDate.Text);
                        }
                        emp.JobStatus = rdoJobStatus.SelectedValue;
                        //For Probatio Period
                        emp.ProvisionalPeriod = txtprobationperiod.Text;
                        string rdovalue = rdoisconfirm.Text;
                        if (rdovalue == "Probation")
                        {
                            emp.IsConfirmed = false;
                        }
                        else
                        {
                            emp.IsConfirmed = true;
                        }
                        //End
                        //SHIFT WOFF
                        //emp.ShiftType = rdoShiftType.Text;
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
                        // emp.PFType = vt_Common.CheckInt(ddPFtype.SelectedValue);
                        emp.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        emp.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                        emp.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        emp.OverTime = vt_Common.Checkdecimal(txtovertimeamount.Text);
                        emp.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);



                        //string BankName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtbankname.Text);
                        //emp.FromBank = vt_Common.CheckString(BankName);
                        //emp.FromBranch = vt_Common.CheckString(txtbrachfrom.Text);
                        //emp.ToBank = vt_Common.CheckString(txtbankto.Text);

                        //this is for branch name
                        emp.FromBranch =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtfrombranch.Text);
                        emp.ToBranch = vt_Common.CheckString(txtbranchcode.Text);
                        emp.AccountNo = txtaccount.Text;
                      //  emp.Tax = Convert.ToDecimal(txttax.Text);

                        //Address Working Salman
                        emp.HomePhone = TxtHomePhone.Text;
                        emp.CNIC = txtcnic.Text;
                        emp.Phone = txtemgnumber.Text;
                        emp.Sex = ddlSex.SelectedValue;
                        emp.Current_Address1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_Address1.Text) ;
                        emp.Current_City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_City.Text) ;
                        emp.Current_State = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_State.Text) ;
                        emp.Current_Zip = TxtCurrent_Zip.Text;
                        emp.Current_Country = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_Country.Text);


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
                        ViewState["EmployeeId"] = emp.EmployeeID;
                        int EmployeeId = Convert.ToInt32(ViewState["EmployeeId"]);
                        vt_tbl_Employee empp = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmployeeId).FirstOrDefault();
                        if (empp != null)
                        {

                            string fullpathimagename = empp.ImageName;
                            string fullpathnicimage = empp.NICImage;
                            if (System.IO.File.Exists(fullpathimagename) && System.IO.File.Exists(fullpathnicimage))
                            {
                                System.IO.File.Delete(empp.ImageName);
                                System.IO.File.Delete(empp.NICImage);
                            }
                            else
                            {

                            }
                        

                         }
                        if (uploadEmpImage.HasFile)
                        {
                            emp.ImageName = guid  + "-"+ EmployeeId + uploadEmpImage.PostedFile.FileName;
                        }
                        else
                        {
                            emp.ImageName = hdprofileimage.Value;
                           // emp.ImageName = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
                        }

                        //if (NICUpload.HasFile)
                        //{
                        //    emp.NICImage = guid+   "-" + EmployeeId + NICUpload.PostedFile.FileName;
                        //}
                        //else
                        //{
                        //    emp.NICImage = lblNIC.Text == "" ? lblNIC.Text : lblNIC.Text;
                        //    //emp.ImageName = hdNICName.Value == "" ? hdNICImage.Value : hdNICImage.Value;
                        //}

                        // Acadmic Info Start
                        vt_tbl_QualificationDetails obj_qualification = new vt_tbl_QualificationDetails();
                        vt_tbl_PreviousJobDetails obj_pjobinfo = new vt_tbl_PreviousJobDetails();
                        string Pjobinfo = hdnpjob.Value;
                        string qualification = txtHidden_Academic.Value;
                        List<vt_tbl_QualificationDetails> listQualificationAcademic = JsonConvert.DeserializeObject<List<vt_tbl_QualificationDetails>>(qualification);
                        List<vt_tbl_PreviousJobDetails> listpjobinfo = JsonConvert.DeserializeObject<List<vt_tbl_PreviousJobDetails>>(Pjobinfo);

                        var academic_qualification = db.sp_GetEditQualificationDetails(emp.EmployeeID, 1).Select(s => new vt_tbl_QualificationDetails { Id = s.Id }).ToList();  //Academic Qualification
                        var pjobinfodetail = db.vt_tbl_PreviousJobDetails.Where(x => x.EmployeeID == emp.EmployeeID).ToList();
                        //Removing Old Records from jobinfo on behalf of EmpID
                        if (pjobinfodetail.Count() > 0)
                        {
                            foreach (var item in pjobinfodetail)
                            {
                                var pjob_obj = db.vt_tbl_PreviousJobDetails.Find(item.Id);
                                db.vt_tbl_PreviousJobDetails.Remove(pjob_obj);
                                db.SaveChanges();
                                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }

                        }
                        foreach (var item in listpjobinfo)
                        {
                            obj_pjobinfo.EmployeeID = emp.EmployeeID;// CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Qualification);
                            obj_pjobinfo.PreviousCompanyName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.PreviousCompanyName); //Convert.ToInt32(item.Year);
                            obj_pjobinfo.PreviousDesignation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.PreviousDesignation);
                            obj_pjobinfo.JoiningDate = item.JoiningDate;
                            obj_pjobinfo.EndDate = item.EndDate;
                            obj_pjobinfo.CompanyID = emp.CompanyID; ;
                            obj_pjobinfo.CreateDate = DateTime.Now;
                            db.vt_tbl_PreviousJobDetails.Add(obj_pjobinfo);

                            db.SaveChanges();
                        }
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
                            obj_qualification.InstituteName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.InstituteName);
                            obj_qualification.Qualification = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Qualification);
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
                            obj_certificate.InstituteName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.InstituteName);
                            obj_certificate.Qualification = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Qualification);
                            obj_certificate.Year = Convert.ToInt32(item.Year);
                            obj_certificate.Grade = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Grade);// item.Qualification();
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
                        GSalary.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                        GSalary.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        GSalary.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);
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

                        //LOG ENTRY
                        var LastRecord = db.vt_tbl_Employee.OrderByDescending(x => x.EmployeeID).FirstOrDefault();
                        vt_tbl_Employee_TransferLog empTrans = new vt_tbl_Employee_TransferLog();
                        empTrans.EmployeeID = LastRecord.EmployeeID;
                        //empTrans.CompanyID = Convert.ToInt32(ddlcomp.SelectedValue);
                        empTrans.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        empTrans.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        empTrans.ManagerID = Convert.ToInt32(ddlLineManager.SelectedValue);
                        empTrans.EmployeeType = ddEmployeType.SelectedValue;
                        //if (Txtusername.Text != "" || !string.IsNullOrEmpty(Txtusername.Text) || !string.IsNullOrWhiteSpace(Txtusername.Text))
                        //{
                            empTrans.FirstName = TxtFirstName.Text;
                        //}
                        empTrans.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtLastName.Text) ;
                        empTrans.Email = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Txtemail.Text);
                        empTrans.EntryDate = EntryDate;
                        empTrans.Action = "Insert";
                        db.Entry(empTrans).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        MsgBox.Show(Page, MsgBox.success, emp.EmployeeName, "Successfully Saved!");
                        ClearForm();
                        int EmployeeIdd = Convert.ToInt32(ViewState["EmployeeId"]);
                        if (uploadEmpImage.HasFile)
                        {
                            string Extenion = Path.GetExtension(uploadEmpImage.PostedFile.FileName);
                            if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                                Extenion.ToLower() == ".bmp")
                            {
                                ViewState["ImageName"] = uploadEmpImage.PostedFile.FileName;
                                string ext = Extenion.Substring(1);
                                uploadEmpImage.SaveAs(MapPath("~/images/Employees/ProfileImage/"+guid + "-" + EmployeeIdd + ViewState["ImageName"].ToString()));
                                //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                                lblmsg.Text = "";
                            }
                            else
                            {
                                lblmsg.Text = Extenion + " Not Supported";
                            }
                           
                            MsgBox.Show(Page, MsgBox.success, "", "Successfully Edited!");

                        }
                        //if (NICUpload.HasFile)
                        //{
                        //    string Extenions = Path.GetExtension(NICUpload.PostedFile.FileName);
                        //    if (Extenions.ToLower() == ".jpg" || Extenions.ToLower() == ".png" || Extenions.ToLower() == ".gif" || Extenions.ToLower() == ".jpeg" ||
                        //        Extenions.ToLower() == ".bmp")
                        //    {
                        //        ViewState["ImageName"] = NICUpload.PostedFile.FileName;
                        //        string ext = Extenions.Substring(1);
                        //        NICUpload.SaveAs(MapPath("~/images/Employees/NicImage/" + guid + "-" + EmployeeIdd + ViewState["ImageName"].ToString()));
                        //        //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                        //        lblmsg.Text = "";
                        //    }
                        //    else
                        //    {
                        //        lblmsg.Text = Extenions + " Not Supported";
                        //    }

                        //}
                        //LoadData();
                        //UpView.Update();
                        Response.Redirect("Employee.aspx");

                    }
                //}
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

        //Image work starts

    }
    //public static int GetEmployeeId()
    //{
    //    // Check if HttpContext is available
    //    if (HttpContext.Current != null && HttpContext.Current.Request.QueryString["ID"] != null)
    //    {
    //        // Convert the QueryString parameter to int
    //        int id;
    //        if (int.TryParse(HttpContext.Current.Request.QueryString["ID"], out id))
    //        {
    //            return id;
    //        }
    //        else
    //        {
    //            throw new Exception("Invalid ID format in QueryString.");
    //        }
    //    }
    //    else
    //    {
    //        throw new Exception("ID parameter is missing in QueryString.");
    //    }
    //}

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object LoadData()
    { 

        string List = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                
                User_BAL usr = new User_BAL();
                //int id = Convert.ToInt32(HttpContext.Current.Request.QueryString["ID"]);
               
                if (CompanyID == 0)
                {
                    // MsgBox.Show(Page, MsgBox.danger, "", "Please Select Company First from dashboard");
                    //HttpContext.Current.Response.Write("<script>alert('Please Select Company First from dashboard');</script>");
                }
                else
                {

                    DataTable dt = new DataTable();
                    dt = usr.Getuploadeddocuments(CompanyID, employeeid);
                    if (dt != null)
                    {
                        List = JsonConvert.SerializeObject(dt);

                    }
                    else
                    {
                        //HttpContext.Current.Response.Write("<script>alert('Please Select Company First from dashboard');</script>");
                    }


                }


            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        return List;
     

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void DownloadFile(int fileId)
    {
        try
        {
            User_BAL usr = new User_BAL();
            DataTable dt = new DataTable();
            dt = usr.Getuploadeddocuments(CompanyID, employeeid);
            string documentPath1 = "";
            string documentName1 = "";
         

            DataRow[] rows = dt.Select("Id = " + fileId); 

            if (rows.Length > 0)
            {
                 documentPath1 = rows[0]["DocumentPath"].ToString(); 
                 documentName1 = rows[0]["FileName"].ToString();     

                Console.WriteLine("Document Path: " + documentPath1);
                Console.WriteLine("Document Name: " + documentName1);

               
            }
            else
            {
                Console.WriteLine("No document found with the given FileId.");
            }
            string documentPath = documentPath1;  // GetDocumentPathById(fileId); // Implement this method to get the correct path
            string documentName = documentName1; Path.GetFileName(documentPath);

            // Check if the file exists
            if (File.Exists(documentPath))
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf"; // You can modify this depending on file type
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + documentName);
                HttpContext.Current.Response.TransmitFile(documentPath);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                // Handle file not found case
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.Write("File not found");
                HttpContext.Current.Response.End();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            HttpContext.Current.Response.Write("Error while downloading file.");
            HttpContext.Current.Response.End();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object SaveWithApi(string data)
    {
        try
        {
            string uploadDirectoryl = ConfigurationManager.AppSettings["LocalPathPrefix"];
            vt_EMSEntities db = new vt_EMSEntities();
            long payloadSize = data.Length;
            Console.WriteLine("Payload Size (in bytes): " + payloadSize);

            //var filesArray = new JsonSerializer.Deserialize<List<CustomModels.FileItem>>(data);

            var filesArray = JsonConvert.DeserializeObject<List<CustomModels.FileItem>>(data);

            //var filesArray = new JavaScriptSerializer().Deserialize<List<CustomModels.FileItem>>(data);

            List<CustomModels.FileItem> documentList = HttpContext.Current.Session["Documents"] as List<CustomModels.FileItem> ?? new List<CustomModels.FileItem>();
            
            foreach (var newFile in filesArray)
            {
                bool fileExists = documentList.Any(f => f.id == newFile.id);

                if (!fileExists)
                {
                    documentList.Add(newFile); 
                }
            }

       
            HttpContext.Current.Session["Documents"] = documentList;
            
            if (filesArray != null && filesArray.Count > 0)
            {
                string employeeFolderPath = Path.Combine(uploadDirectoryl, employeeid.ToString());
                if (!Directory.Exists(employeeFolderPath))
                {
                    Directory.CreateDirectory(employeeFolderPath);
                }

                foreach (var file in filesArray)
                {
                
                    if (documentList.Any(f => f.id == file.id))
                    {
                        string fileExtension = Path.GetExtension(file.fileName);
                        string newFileName = Guid.NewGuid().ToString() + fileExtension;
                        string filePath = Path.Combine(employeeFolderPath, newFileName);
                        byte[] imageByteArray = Convert.FromBase64String(file.fileBase64);
                        File.WriteAllBytes(filePath, imageByteArray);

                 
                        vt_tbl_EmployeeDocuments empdocument = new vt_tbl_EmployeeDocuments
                        {
                            EmployeeId = employeeid,
                            DocumentPath = filePath,
                            DocumentType = fileExtension,
                            OriginalFileName = file.fileName,
                            FileName = file.fileType.ToString(),
                            CompanyId = CompanyID
                        };

                        db.vt_tbl_EmployeeDocuments.Add(empdocument);
                        db.SaveChanges();
                    }
                }

                LoadData();
            }
            else
            {
                var response1 = new
                {
                    success = false,
                    message = "No files found in session."
                };
                return new JavaScriptSerializer().Serialize(response1);
            }

     
            var response = new
            {
                success = true,
                message = filesArray.Count + " file(s) processed successfully."
            };
            return new JavaScriptSerializer().Serialize(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                success = false,
                message = "Error occurred: " + ex.Message
            };
            return new JavaScriptSerializer().Serialize(errorResponse);
        }
       
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string DeleteDocument(int ID)
    {
        try
        {
    
            bool isDeleted = DeleteDocumentFromDatabase(ID);

            if (isDeleted)
            {
                return "Success"; 
            }
            else
            {
                return "Error";  
            }
        }
        catch (Exception ex)
        {
            return "Error"; 
        }
    }

    private static bool DeleteDocumentFromDatabase(int ID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        db.sp_DeleteEmpDocumentbyid(ID); ;
        return true;  
    }

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static bool Check_UsernameEdit(string Username, int EmployeeID)
    //{
    //    vt_EMSEntities db = new vt_EMSEntities();
    //    vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.EmployeeName == Username && x.EmployeeID != EmployeeID).SingleOrDefault();
    //    if (EmployeeObj != null)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CheckEmailEdit(string Email, int EmployeeID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        int companyId = Convert.ToInt32(HttpContext.Current.Session["Companyid"]);
        var EmployeeObj = db.vt_tbl_Employee.Where(x => x.Email.ToUpper() == Email.ToUpper() && x.EmployeeID != EmployeeID && x.CompanyID == companyId).SingleOrDefault();
        if (EmployeeObj != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    protected void btnsavepayroll_Click(object sender, EventArgs e)
    {
        try
        {
            vt_EMSEntities db = new vt_EMSEntities();

            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            vt_tbl_Employee emp = db.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(ID)).FirstOrDefault();
            if (emp != null)
            {
                if (ddlpaymentmethod.SelectedValue == "Cheque")
                {
                    emp.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                    emp.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                    emp.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                    emp.OverTime = vt_Common.Checkdecimal(txtovertimeamount.Text);
                    emp.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);
                    emp.Tax = Convert.ToDecimal(txttax.Text == "" ? "0.00" : txttax.Text);
                    emp.FromBank = "";
                    //emp.FromBranch = vt_Common.CheckString(txtbrachfrom.Text);
                    //emp.ToBank = vt_Common.CheckString(txtbankto.Text);

                    //this is for branch name
                    emp.FromBranch = "";
                    emp.ToBranch = "";
                    emp.AccountNo = "";
                    txtbranchcode.Text = "";
                    emp.AccountTitle = txtaccounttitile2.Text;
                    emp.PaymentMethod = ddlpaymentmethod.Text;
                    db.SaveChanges();
                    Response.Redirect("Employee.aspx");
                }
                else if (ddlpaymentmethod.SelectedValue == "Bank")
                {
                    if (txtaccount.Text == "")
                    {
                        lblaccountno.Text = "Please Enter Account Number";
                    }
                    else
                    {
                        emp.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        emp.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                        emp.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        emp.OverTime = vt_Common.Checkdecimal(txtovertimeamount.Text);
                        emp.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text) ;
                        emp.Tax = Convert.ToDecimal(txttax.Text == ""?"0.00": txttax.Text);
                        //string BankName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtbankname.Text);
                        //emp.FromBank = vt_Common.CheckString(BankName);
                        emp.FromBank = ddlbankname.Text;
                        //emp.FromBranch = vt_Common.CheckString(txtbrachfrom.Text);
                        //emp.ToBank = vt_Common.CheckString(txtbankto.Text);

                        //this is for branch name
                        emp.FromBranch = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtfrombranch.Text);
                        emp.ToBranch = vt_Common.CheckString(txtbranchcode.Text);
                        emp.PaymentMethod = ddlpaymentmethod.Text;
                        emp.AccountNo = txtaccount.Text;


                        emp.AccountTitle = txtaccounttitle.Text;



                        db.SaveChanges();
                        Response.Redirect("Employee.aspx");
                    }
                 
                }
                else if (ddlpaymentmethod.SelectedValue == "Cash")
                {
                    emp.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                    emp.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                    emp.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                    emp.OverTime = vt_Common.Checkdecimal(txtovertimeamount.Text);
                    emp.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);
                    emp.Tax = Convert.ToDecimal(txttax.Text == "" ? "0.00" : txttax.Text);
                    emp.FromBank = "";                 
                    emp.FromBranch = "";
                    emp.ToBranch = "";
                    emp.AccountNo = "";
                    txtbranchcode.Text = "";
                    emp.AccountTitle = "";
                    emp.PaymentMethod = ddlpaymentmethod.Text;
                    db.SaveChanges();
                  Response.Redirect("Employee.aspx");

                }


            }
        }
        catch (Exception)
        {

            throw;
        }
      
    }
}
