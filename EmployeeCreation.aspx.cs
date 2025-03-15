using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Newtonsoft.Json;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Viftech;

public partial class EmployeeCreation : System.Web.UI.Page
{
    DateTime EntryDate = DateTime.Now;
    string uploadDirectory = ConfigurationManager.AppSettings["LocalPathPrefix"];
    vt_EMSEntities db = new vt_EMSEntities();
    Custommethods customMethods = new Custommethods();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadPage(sender, e);
                //BindDep();
                txtconfirmdate.Text = EntryDate.ToString("dd/MM/yyyy");
                txtprobationperiod.Text = EntryDate.ToString("dd/MM/yyyy");
                txtDOB.Text = EntryDate.ToString("dd/MM/yyyy");
                txtJoiningDate.Text = EntryDate.ToString("dd/MM/yyyy");
                txtpjoiningdate.Text = EntryDate.ToString("dd/MM/yyyy");
                txtpenddate.Text = EntryDate.ToString("dd/MM/yyyy");
                string selectedValue = ddlLineManager.SelectedValue;
            }
        }
    }
    public    void BindLineManager()
    {
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
        ddlLineManagername.Items.Clear();
        ddlLineManager.Items.Clear();
        if (Txtemail.Text == "")
        {
            MsgBox.Show(Page, MsgBox.danger, "", "Please Enter Email First");
        }
        else
        {

            vt_Common.Bind_DropDown(ddlLineManager, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
            string val = ddlLineManager.SelectedValue;
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
                string val1 = ddlLineManager.SelectedValue;
                vt_Common.Bind_DropDown(ddlLineManagername, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
            }

            else if ((ddlLineManager.Items.Count == 1) && (Des > 1))
            {
                int RoleID = Convert.ToInt32(Session["RoleId"]);
                int ComID = Convert.ToInt32(Session["CompanyId"]);
                SqlParameter[] param2 =
                {
                new SqlParameter("@DesignationID",val),
                new SqlParameter("@DepartmentID",Dept),
                new SqlParameter("@CompanyID",CompanyId)
                };
                vt_Common.Bind_DropDown(ddlLineManagername, "VT_sp_BindLineManagerName", "EmployeeName", "EmployeeID", param2);
            }
            else if ((ddlLineManager.Items.Count > 1) && (Des > 1))
            {

                int RoleID = Convert.ToInt32(Session["RoleId"]);
                int ComID = Convert.ToInt32(Session["CompanyId"]);
                SqlParameter[] param1 =
                {
                new SqlParameter("@RoleId",RoleID),
                new SqlParameter("@CompanyId",ComID)
            };
                vt_Common.Bind_DropDown(ddlLineManagername, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
            }
            if (TxtFirstName.Text == "")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "Please Enter First Name");
            }
            else
            {
                int companyid = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            }
        }

    }
    private   void UpdateDropdownRequirement(DropDownList ddlManager)
    {
     
        string selectedValue = ddlManager.SelectedValue;

       
        if (selectedValue == "1" || selectedValue == "2" || selectedValue == "3")
        {
            ddlLineManagername.Attributes.Remove("require"); 
        }
        else
        {
            ddlLineManagername.Attributes.Add("require", "Please Select"); 
        }
    }
    protected void ddlLineManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        int desiId =Convert.ToInt32(ddlDesignation.SelectedValue);

        if (ddlDesignation.SelectedIndex == 0)
        {
            ddlLineManager.Items.Clear();

        }
        else
        {
            if (TxtFirstName.Text == ""  )
            {
                //ddlLineManager.Items.Clear();
                MsgBox.Show(Page, MsgBox.danger, "", "Please Enter First Name");
                ddlDesignation.SelectedIndex =0;
                UpDetail.Update();
            }
            else
            {
                if (Txtemail.Text == "")
                {
                    ddlLineManager.Items.Clear();
                    MsgBox.Show(Page, MsgBox.danger, "", "Please Enter Email");
                    ddlDesignation.SelectedIndex = 0;
                    UpDetail.Update();
                }
                else
                {
                    ddlLineManager.Items.Clear();
                    BindLineManager();
                    //ddlLineManager.Items.RemoveAt(0);
                    //ddlLineManagername.Items.RemoveAt(0);
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
                                ddlLineManagername.Items.Clear();
                                UpdateDropdownRequirement(ddlLineManager);
                            }
                            else
                            {
                                ddltopdesignation.Visible = false;
                                div2.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        //Previous One
        //ddlLineManager.Items.Clear();
        //BindLineManager();
    }
    public    void BindDep()
    {
        int companyID = 0;
        companyID = (Convert.ToInt32(Session["CompanyId"]));
        SqlParameter[] param =
        {
            new SqlParameter("@CompanyID",companyID)
        };
        vt_Common.Bind_DropDown(ddldepartment, "VT_SP_BindDepart", "Department", "DepartmentID", param);
        UpDetail.Update();
    }
    protected void LoadPage(object sender, EventArgs e)
    {
        ClearForm();
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
        ddlDesignation.Items.Clear();
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
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {

        
        int ID = 0;
        if (ddlcomp.SelectedValue != "")
        {
            ID = (Convert.ToInt32(Session["CompanyId"]));
        }
        else
        {
            ID = (Convert.ToInt32(Session["CompanyId"]));
        }

     
        BindDep();
        BindDesignation(ID);
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
                    ddlLineManagername.Items.Clear();
                    UpdateDropdownRequirement(ddlLineManager);
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
    protected void btnSaveEmployee_Click(object sender, EventArgs e)
        {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                        vt_tbl_Employee emp = new vt_tbl_Employee();
                        vt_tbl_empjobinfolog empjoblog = new vt_tbl_empjobinfolog();    
                        int Company_ID = Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];
                        int EmployeeId = 0;
                        var guid = Guid.NewGuid().ToString();
                        if (Company_ID == 0)
                        {
                            vt_Common.ReloadJS(this.Page, "showMessage('Please select company from the dashboard');");
                            return;
                        }
               

                        int recordID = vt_Common.CheckInt(ViewState["PageID"]);
               
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
                        emp.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase( TxtLastName.Text);
                        emp.CompanyID = int.Parse(ddlcomp.SelectedValue);
                        emp.EmpPassword=   vt_Common.Encrypt("123456@");
                        emp.Email = Txtemail.Text;
                        emp.DesignationID = int.Parse(ddlDesignation.SelectedValue);
                        emp.Type = ddEmployeType.Text;
                        emp.RoleID = 4;
                        emp.Religion = txtreligion.Text;
                        emp.EmployeeName = TxtFirstName.Text + TxtLastName.Text;
                       // emp.ManagerID = vt_Common.CheckInt(ddlLineManager.SelectedValue);
                        emp.ManagerID = vt_Common.CheckInt(ddlLineManagername.SelectedValue);
                        if (ddlLineManager.SelectedItem.Text.Contains("HR Admin"))
                        {
                            emp.IsHRLineManager = true;
                        }
                        else
                        {
                            emp.IsHRLineManager = false;
                        }


                        //PERSONAL INFORMATION
                        emp.MartialStatus = rdoMarriedStatus.SelectedValue;
                        emp.FatherHusbandName = ddlempguardianlist.Text;
                        emp.RelationWithEmployee = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtguardianname.Text);

                        if (!string.IsNullOrEmpty(txtDOB.Text)) 
                        {
                            string txtDateValue = txtDOB.Text;
                            DateTime? resultDate = customMethods.GetDateFromTextBox(txtDateValue);
                            emp.DOB = resultDate;
                        }

            


                          emp.CurrentAddress =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCurrentAddress.Text);
                                //JOB INFORMATION
                          emp.BranchID = Convert.ToInt32(ddlBranch.SelectedValue);
                                //emp.Type = ddlEmpType.SelectedValue;
                          emp.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                          emp.BankID = Convert.ToInt32(ddlBank.SelectedValue);
                        //emp.FromBranch = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtfrombranch.Text);
                        //emp.ToBranch = txtbranchcode.ToString();
                        // emp.AccountNo = Convert.ToInt64(txtaccount.Text);
                        //emp.AccountNo = vt_Common.CheckInt64(txtaccount.Text);

                        if (!string.IsNullOrEmpty(txtJoiningDate.Text))
                        {
                            string txtDateValue1 = txtJoiningDate.Text;
                            DateTime? resultDate1 = customMethods.GetDateFromTextBox(txtDateValue1);
                            emp.JoiningDate = resultDate1;
                            empjoblog.joiningdate = resultDate1;
                        }
                        emp.JobStatus = rdoJobStatus.SelectedValue;
                        //For Probatio Period
                        emp.ProvisionalPeriod = txtprobationperiod.Text;
                        string rdovalue = rdoisconfirm.Text;
                        if (rdovalue == "Probation")
                        {
                            emp.IsConfirmed = false;
                            empjoblog.isconfirm = false;    
                        }
                        else
                        {
                            string txtDateValue1 = txtconfirmdate.Text;
                            DateTime? resultDate1 = customMethods.GetDateFromTextBox(txtDateValue1);
                            emp.IsConfirmed = true;
                            empjoblog.isconfirm = true;
                            emp.ConfirmationDate = resultDate1;
                        } 
                        



                        //Address Working Salman
                        emp.HomePhone = TxtHomePhone.Text;
                        emp.CNIC = txtcnic.Text;
                        emp.Phone = txtemgnumber.Text;
                        emp.Sex = ddlSex.SelectedItem.Text;

                        emp.Current_Address1 =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_Address1.Text);
                        emp.Current_City =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_City.Text);
                        emp.Current_State =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_State.Text);
                        emp.Current_Zip = TxtCurrent_Zip.Text;
                        emp.Current_Country =CultureInfo.CurrentCulture.TextInfo.ToTitleCase( TxtCurrent_Country.Text); 
                        
                        emp.Permenant_Address1= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentAddress1.Text);
                        emp.Permenant_City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentCity.Text);
                        emp.Permenant_Country = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentCounry.Text);
                        emp.Permenant_State = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPermanentState.Text);
                        emp.Permenant_Zip = txtPermanentPostalCode.Text;


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

                        //add leave of employees according to their jobtype
                        Session["EmployeeId"] = ViewState["EmployeeId"];
                        ViewState["EmployeeId"] = emp.EmployeeID;
                        EmployeeId = Convert.ToInt32(ViewState["EmployeeId"]);
                        vt_tbl_Employee empc = new vt_tbl_Employee();
                        empc = db.vt_tbl_Employee.Where(w => w.EmployeeID.Equals(EmployeeId) && w.IsConfirmed==true && w.ConfirmationDate!=null).FirstOrDefault();
                        if (empc != null)
                        {
                            ProcedureCall.VT_SP_AddLeavesAfterConfirmation(EmployeeId, Company_ID, Convert.ToDateTime( empc.ConfirmationDate), empc.Sex);
                        }
                        GetFilesFromSession(EmployeeId);


                        //Insert into joblog table
                        empjoblog.jobtype= ddEmployeType.Text;
                        empjoblog.jobstatus = rdoJobStatus.SelectedValue;
                        empjoblog.departmentid= int.Parse(ddldepartment.SelectedValue);
                        empjoblog.employeeid = EmployeeId;
                        empjoblog.companyid = Company_ID;
                        empjoblog.managerid= vt_Common.CheckInt(ddlLineManagername.SelectedValue);
                        empjoblog.designationid = int.Parse(ddlDesignation.SelectedValue);
                        if (rdovalue == "Probation")
                        {
                          empjoblog.isconfirm = false;
                        }
                        else
                        {
                          empjoblog.isconfirm = true;
                        }
                        if (!string.IsNullOrEmpty(txtJoiningDate.Text))
                        {
                            string txtDateValue1 = txtJoiningDate.Text;
                            DateTime? resultDate1 = customMethods.GetDateFromTextBox(txtDateValue1);
                            empjoblog.joiningdate = resultDate1;
                        }
                        if (ddlLineManager.SelectedItem.Text.Contains("HR Admin"))
                        {
                            empjoblog.ishrlinemanager = true;
                        }
                        else
                        {
                             empjoblog.ishrlinemanager = false;
                        }
                        db.vt_tbl_empjobinfolog.Add(empjoblog);
                        db.SaveChanges();

                        if (uploadEmpImage.HasFile)
                        {
                            emp.ImageName = guid + "-" + EmployeeId + uploadEmpImage.PostedFile.FileName;
                        }
                        else
                        {
                            emp.ImageName = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
                        }

                        //if (NICUpload.HasFile)
                        //{
                        //    emp.NICImage = guid + "-" + EmployeeId + NICUpload.PostedFile.FileName;
                        //}
                        //else
                        //{
                        //    emp.ImageName = hdNICName.Value == "" ? hdNICImage.Value : hdNICImage.Value;
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
                        if (Pjobinfo.Count() > 0)
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
                        if (qualification.Count() > 0)
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
                        //if (certification_qualification.Count() > 0)
                        //{
                        //    foreach (var item in certification_qualification)
                        //    {
                        //        var certificate_obj = db.vt_tbl_QualificationDetails.Find(item.Id);
                        //        db.vt_tbl_QualificationDetails.Remove(certificate_obj);
                        //        db.SaveChanges();
                        //        //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        //    }

                        //    //academic_qualification.ForEach(x => db.vt_tbl_QualificationDetails.Remove(x));
                        //    //  db.Entry(academic_qualification).State = System.Data.Entity.EntityState.Deleted;
                        //}
                        if (certificate.Count() > 0)
                        {
                            foreach (var item in listQualificationCertificate)
                            {
                                obj_certificate.InstituteName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.InstituteName);
                                obj_certificate.Qualification = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Qualification);
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
                        }
                        // Acadmic Info End
                        var getEmployeeID = emp.EmployeeID;
                        vt_tbl_EmployeeGrossSalary GSalary = new vt_tbl_EmployeeGrossSalary();
                        GSalary.EmployeeID = getEmployeeID;
                        //GSalary.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        //GSalary.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                        //GSalary.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        //GSalary.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);
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
                        empTrans.CompanyID = Convert.ToInt32(ddlcomp.SelectedValue);
                        empTrans.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        empTrans.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        //empTrans.ManagerID = Convert.ToInt32(ddlLineManager.SelectedValue);
                        emp.ManagerID = vt_Common.CheckInt(ddlLineManagername.SelectedValue);

                        empTrans.EmployeeType = ddEmployeType.SelectedValue;
                        empTrans.LastName = TxtLastName.Text;
                        empTrans.Email = Txtemail.Text;
                        empTrans.EntryDate = EntryDate;
                        empTrans.Action = "Insert";
                        db.Entry(empTrans).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        CreateAutouser();
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
                                uploadEmpImage.SaveAs(MapPath("~/images/Employees/ProfileImage/" +guid+ "-"+ EmployeeIdd + ViewState["ImageName"].ToString()));
                                //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                                lblmsg.Text = "";
                            }
                            else
                            {
                                lblmsg.Text = Extenion + " Not Supported";
                            }                           
                        }                        
                        //LoadData();
                        //UpView.Update();
                        //}
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
 
      
    }
 
  

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object SaveWithApi(string data)
    {
        try
        {
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
            throw;
        }
    }
    public  void GetFilesFromSession(int employeeId )
    {
        try
        {
            int companyID = 0;
            vt_EMSEntities db = new vt_EMSEntities();
            companyID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
            List<CustomModels.FileItem> documentList = HttpContext.Current.Session["Documents"] as List<CustomModels.FileItem>;

            if (documentList != null && documentList.Count > 0)
            {

                vt_tbl_EmployeeDocuments empdocument = new vt_tbl_EmployeeDocuments();
               
                string employeeFolderPath = Path.Combine(uploadDirectory, employeeId.ToString());
                if (!Directory.Exists(employeeFolderPath))
                {
                    Directory.CreateDirectory(employeeFolderPath);
                }

                
                foreach (var file in documentList)
                {
                    string ftype= file.fileType.ToString();  
                    string fileExtension = Path.GetExtension(file.fileName);
                    string newFileName = Guid.NewGuid().ToString() + fileExtension;
                    string filePath = Path.Combine(employeeFolderPath, newFileName);
                    byte[] imageByteArray = Convert.FromBase64String(file.fileBase64);
                    File.WriteAllBytes(filePath, imageByteArray);
                    //file.fileName = filePath;

                    //Save file into database table 
                    empdocument.EmployeeId = employeeId;
                    empdocument.DocumentPath= filePath;
                    empdocument.DocumentType = fileExtension;
                    empdocument.OriginalFileName = file.fileName;
                    empdocument.FileName = ftype;
                    empdocument.CompanyId = companyID;
                    db.vt_tbl_EmployeeDocuments.Add(empdocument);
                    db.SaveChanges();
                }

                var response = new
                {
                    success = true,
                    message = documentList.Count + " file(s) processed successfully."
                };
                string jsonResponse = new JavaScriptSerializer().Serialize(response);
                Console.WriteLine(jsonResponse); 
            }
            else
            {
                Console.WriteLine("No files found in session.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error hi he ye  " + ex.Message);
            throw; // Ye phir error ko phir se throw karega
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object RemoveFileFromSession(string fileId)
    {
        try
        {
            // Retrieve the document list from session
            List<CustomModels.FileItem> documentList = HttpContext.Current.Session["Documents"] as List<CustomModels.FileItem>;

            if (documentList != null)
            {
               
                var fileToRemove = documentList.FirstOrDefault(f => f.id == fileId);
                if (fileToRemove != null)        
                {
                    // Remove the file from the session list
                    documentList.Remove(fileToRemove);
                    // Save the updated list back to session
                    HttpContext.Current.Session["Documents"] = documentList;

                    
                    var response = new
                    {
                        success = true,
                        message = "File removed successfully."
                    };
                    return new JavaScriptSerializer().Serialize(response);
                }
                else
                {
                    
                    var response = new
                    {
                        success = true,
                        message = "File not found in session."
                    };
                    return new JavaScriptSerializer().Serialize(response);
                }
            }
            else
            {

                var response = new
                {
                    success = true,
                    message = "File not found in session."
                };
                return new JavaScriptSerializer().Serialize(response);
            }
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                success = false,
                message = "Error occurred: " + ex.Message
            };

            return new JavaScriptSerializer().Serialize(errorResponse);
            throw;
        }
    }
    private void CreateAutouser()
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                string msg = "";
                int CompID = Session["CompanyId"] != null ? Convert.ToInt32(Session["CompanyId"]) : 0;
                CompID = vt_Common.CheckInt(CompID);
              int   EmployeIeD = Convert.ToInt32(ViewState["EmployeeId"]);
                User_BAL BAL = new User_BAL();
                BAL.UserID = Convert.ToInt32(ViewState["hdnID"]);
                var query = db.vt_tbl_User.Where(x => x.Active == true && x.CompanyId == CompID && x.UserId != BAL.UserID).ToList();
                //var query1 = db.vt_tbl_Employee.Where(x => x.EmployeeName == Txtusername.Text && x.CompanyID == CompID).ToList();
                var LastRecord = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == EmployeIeD);
                string EmployeeeCode = LastRecord.EnrollId;


                if (query.Count != 0 || query.Count == 0) 
                {  
                    BAL.UserName = TxtFirstName.Text+TxtLastName.Text;
                    BAL.Password = vt_Common.Encrypt("123456@");
                    BAL.FirstName = TxtFirstName.Text;
                    BAL.LastName = TxtLastName.Text;
                    BAL.Email = Txtemail.Text;
                    BAL.RoleID = Convert.ToInt32(4);
                    BAL.CompanyID = Convert.ToInt32(CompID);
                    BAL.EmployeeEnrollId = EmployeeeCode;
                    BAL.EmployeIeD = Convert.ToInt32(ViewState["EmployeeId"]);
                    if (BAL.UserID > 0)
                    {
                        BAL.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                        BAL.UpdatedOn = DateTime.Now;
                        BAL.Active = true;
                        BAL.UpdateById(BAL);
                    }
                    else
                    {
                        BAL.CreatedOn = DateTime.Now;
                        BAL.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        BAL.Active = true;
                        BAL.Sp_Insert(BAL);
                    }
                }



            }
        }
        catch (Exception ex)
        { }
    }
    private bool SendEmail(string ToEmail, string UserName, string Password)
    {
        string Subject = "Welcome Email";
        string BccEmail = string.Empty;

        //List<string> AttachedFiles = new List<string>();
        //AttachedFiles.Add(Server.MapPath(@"Uploads/Attachments/Employees.xlsx"));
        //AttachedFiles.Add(Server.MapPath(@"Uploads/Attachments/Test.xlsx"));

        string Body = string.Empty;
        using (StreamReader Reader = new StreamReader(Server.MapPath("~/Uploads/EmailTemplates/WelcomeEmail.html")))
        {
            Body = Reader.ReadToEnd();
            Body = Body.ToString().Replace("#UserName#", UserName);
            Body = Body.ToString().Replace("#Password#", Password);
        }

        return EmailSender.SendEmail(ToEmail, "", BccEmail, Subject, Body, null);
    }
    private void ClearForm()
    {
        ViewState["PageID"] = null;
        //Clear Upload Fields      
        //lblNIC.Text = null;
        txtHidden_Academic.Value = "";
        txt_HiidenCertificate.Value = "";
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#employes').modal('hide');");
    }
    #region DataBind ,

    // BindDesignation was actually binding departments and employetype now updated to bind VT_SP_BindDesigByDepID and employee types
    private void BindDesignation(int CompanyID)
    {
        int DepID = 0;
        //ddlDesignation.Items.Clear();
        //ddldepartment.Items.Clear();
        //ddlLineManagerDesignation.Items.Clear();
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (ddldepartment.Items.Count > 0)
            {
                DepID = Convert.ToInt32(ddldepartment.SelectedValue);
                SqlParameter[] param =
                {
                 new SqlParameter("@DepID",DepID)
                };
                vt_Common.Bind_DropDown(ddlDesignation, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);
                UpDetail.Update();
            }
        }

        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ddEmployeType.Items.Clear();
            var Typedropdown = (from m in db.vt_tbl_TypeofEmployee
                                select new
                                {  m.Type, m.Id  }).ToList();

            ddEmployeType.DataSource = Typedropdown;
            ddEmployeType.DataTextField = "Type";
            ddEmployeType.DataValueField = "Id";
            ddEmployeType.DataBind();
            ddEmployeType.Items.Insert(0, new ListItem("Please Select", "0"));
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
        if (Txtemail.Text == "")
        {
            //ddlLineManager.Items.Clear();
            MsgBox.Show(Page, MsgBox.danger, "", "Please Enter Email");
            ddldepartment.SelectedIndex =0;
            Console.WriteLine(Txtemail.Text+"email");
            //UpDetail.Update();
        }
        else
        {

            if (ddldepartment.Items.Count > 0)
            {
                DepID = Convert.ToInt32(ddldepartment.SelectedValue);
                SqlParameter[] param =
                {
                 new SqlParameter("@DepID",DepID)
                };
                vt_Common.Bind_DropDown(ddlDesignation, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);
                //UpDetail.Update();
                Console.WriteLine(Txtemail.Text + "email");
                
            }
        }
       
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
    //public static string CheckEnrollID(string EnrollId)
    //{

    //    vt_EMSEntities db = new vt_EMSEntities();
    //    vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.EnrollId == EnrollId).SingleOrDefault();


    //    return ("CheckEnrollID");
    //}
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CheckEmail(string Email)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        int ComID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
        var EmployeeObj = db.vt_tbl_Employee.Where(x => x.Email.ToUpper() == Email.ToUpper() &&  x.CompanyID == ComID).SingleOrDefault();
        if (EmployeeObj != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static bool Check_EmployeeCode(string EmployeeCode)
    //{


    //    vt_EMSEntities db = new vt_EMSEntities();
    //    vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.EnrollId == EmployeeCode).SingleOrDefault();
    //    if (EmployeeObj != null)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
     

    //}
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static bool Check_EmployeeCodeInEdit(string EmployeeCode , int EmployeeID)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
           
    //        vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.EnrollId == EmployeeCode && x.EmployeeID != EmployeeID).SingleOrDefault();
    //        if (EmployeeObj != null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
      

    //}
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CheckEmailEdit(string Email, int EmployeeID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        int companyId = Convert.ToInt32(HttpContext.Current.Session["Companyid"]);
        var EmployeeObj = db.vt_tbl_Employee.Where(x => x.Email.ToUpper() == Email.ToUpper() && x.EmployeeID != EmployeeID && x.CompanyID==companyId).SingleOrDefault();
        if (EmployeeObj != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    protected void btnsavecontractemployee_Click(object sender, EventArgs e)
    { 
        try
        {
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
                var record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.CompanyID == Company_ID);
                //var record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.CompanyID == Company_ID && o.EmployeeName.ToLower().Replace(" ", "").Equals(Txtusername.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same username already exist');");
                }
                else
                {
                    //record = db.vt_tbl_Employee.FirstOrDefault(o => o.EmployeeID != recordID && o.EnrollId.ToLower().Replace(" ", "").Equals(TxtEmployeeCode.Text.ToLower().Replace(" ", "")));
                    //if (record != null)
                    //{
                    //    vt_Common.ReloadJS(this.Page, "showMessage('Employee with the same code already exist');");

                    //}
                    //else
                    //{
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
                        emp.CompanyID = int.Parse(ddlcomp.SelectedValue);
                        emp.Email = Txtemail.Text;
                        emp.DesignationID = int.Parse(ddlDesignation.SelectedValue);
                        emp.Type = ddEmployeType.Text;
                        emp.RoleID = 4;
                         emp.Religion = txtreligion.Text;
                        //if (Txtusername.Text != "" || !string.IsNullOrEmpty(Txtusername.Text) || !string.IsNullOrWhiteSpace(Txtusername.Text))
                        //{
                        //    emp.EmployeeName = Txtusername.Text;
                        //}
                        // emp.EnrollId = TxtEmployeeCode.Text;
                        //if (TxtPassword.Text != "" || !string.IsNullOrEmpty(TxtPassword.Text) || !string.IsNullOrWhiteSpace(TxtPassword.Text))
                        //{
                        //    emp.EmpPassword = vt_Common.Encrypt(TxtPassword.Text);
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
                       emp.FatherHusbandName =  ddlempguardianlist.Text;
                        emp.RelationWithEmployee = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtguardianname.Text);
                        if (!string.IsNullOrEmpty(txtDOB.Text))
                        {
                            emp.DOB = DateTime.Parse(txtDOB.Text).Add(DateTime.Now.TimeOfDay); 
                        }
                        emp.MartialStatus = rdoMarriedStatus.SelectedValue;
                        emp.CurrentAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtCurrentAddress.Text);
                        //JOB INFORMATION
                        emp.BranchID = Convert.ToInt32(ddlBranch.SelectedValue);
                        //emp.Type = ddlEmpType.SelectedValue;
                        emp.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        emp.BankID = Convert.ToInt32(ddlBank.SelectedValue);
                        //  emp.BankAccountNo = txtAccountNo.Text;
                       // emp.FromBranch = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtfrombranch.Text);
                        //emp.ToBranch = txtbranchcode.Text.ToString();
                        // emp.AccountNo = Convert.ToInt64(txtaccount.Text);
                       // emp.AccountNo = vt_Common.CheckInt64(txtaccount.Text);
                        if (!string.IsNullOrEmpty(txtJoiningDate.Text))
                        {
                            emp.JoiningDate = DateTime.Parse(txtJoiningDate.Text).Add(DateTime.Now.TimeOfDay);
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
                        //emp.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        //emp.HouseRentAllownce= vt_Common.Checkdecimal(txthouserentallowance.Text);
                        //emp.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        //emp.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);



                        // string BankName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtbankname.Text);
                        //emp.FromBank = vt_Common.CheckString(BankName);                    
                        /*emp.FromBranch = vt_Common.CheckString(txtbrachfrom.Text)*/
                        ;
                       // emp.ToBank = vt_Common.CheckString(txtbranchcode.Text);
                        //emp.ToBranch = vt_Common.CheckString(txtbranchcode.Text);
                    //  emp.AccountNo = vt_Common.CheckInt64(txtaccount.Text);

                        //Address Working Salman
                        emp.HomePhone = TxtHomePhone.Text;
                        emp.CNIC = txtcnic.Text;
                        emp.Phone = txtemgnumber.Text;
                        emp.Sex = ddlSex.SelectedValue;
                        emp.Current_Address1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_Address1.Text);
                        emp.Current_City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_City.Text);
                        emp.Current_State =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_State.Text);
                        emp.Current_Zip = TxtCurrent_Zip.Text;
                        emp.Current_Country = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtCurrent_Country.Text) ;

                          ViewState["EmployeeId"] = emp.EmployeeID;
                        int EmployeeId = Convert.ToInt32(ViewState["EmployeeId"]);
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
                      
                        //if (uploadEmpImage.HasFile)
                        //{
                        //    emp.ImageName = guid  + "-"+ EmployeeId + uploadEmpImage.PostedFile.FileName;
                        //}
                        //else
                        //{
                        //    emp.ImageName = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
                        //}

                        //if (NICUpload.HasFile)
                        //{
                        //    emp.NICImage =  guid + "-"+ EmployeeId + NICUpload.PostedFile.FileName;
                        //}
                        //else
                        //{
                        //    emp.ImageName = hdNICName.Value == "" ? hdNICImage.Value : hdNICImage.Value;
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
                        //GSalary.BasicSalary = vt_Common.Checkdecimal(txtBasicSalary.Text);
                        //GSalary.HouseRentAllownce = vt_Common.Checkdecimal(txthouserentallowance.Text);
                        //GSalary.TransportAllownce = vt_Common.Checkdecimal(txttransportallowance.Text);
                        //GSalary.MedicalAllowance = vt_Common.Checkdecimal(txtmedicalalowwance.Text);

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
                        empTrans.CompanyID = Convert.ToInt32(ddlcomp.SelectedValue);
                        empTrans.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        empTrans.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        empTrans.ManagerID = Convert.ToInt32(ddlLineManager.SelectedValue);
                        empTrans.EmployeeType = ddEmployeType.SelectedValue;
                        //if (Txtusername.Text != "" || !string.IsNullOrEmpty(Txtusername.Text) || !string.IsNullOrWhiteSpace(Txtusername.Text))
                        //{
                        //    empTrans.FirstName =CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Txtusername.Text);
                        //}
                        empTrans.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TxtLastName.Text);
                        empTrans.Email = Txtemail.Text;
                        empTrans.EntryDate = EntryDate;
                        empTrans.Action = "Insert";
                        db.Entry(empTrans).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        MsgBox.Show(Page, MsgBox.success, emp.EmployeeName, "Successfully Saved!");
                        ClearForm();
                        int EmployeeIdd = Convert.ToInt32(ViewState["EmployeeId"]);
                        //if (uploadEmpImage.HasFile)
                        //{
                        //    string Extenion = Path.GetExtension(uploadEmpImage.PostedFile.FileName);
                        //    if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                        //        Extenion.ToLower() == ".bmp")
                        //    {
                        //        ViewState["ImageName"] = uploadEmpImage.PostedFile.FileName;
                        //        string ext = Extenion.Substring(1);
                        //        uploadEmpImage.SaveAs(MapPath("~/images/Employees/ProfileImage/" +guid+ "-" + EmployeeIdd + ViewState["ImageName"].ToString()));
                        //        //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                        //        lblmsg.Text = "";
                        //    }
                        //    else
                        //    {
                        //        lblmsg.Text = Extenion + " Not Supported";
                        //    }
                          
                          
                        //}
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
                        //LoadData();
                        //UpView.Update();
                    //}
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

        //Image work starts

    }
    public void UpdateEmpType()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        int companyId = Convert.ToInt32(HttpContext.Current.Session["Companyid"]);
        var EmployeeObj = db.vt_tbl_Employee.Where(x => x.CompanyID == companyId && x.RoleID==2).SingleOrDefault();
        



    }
    protected void ddlLineManager_SelectedIndexChanged1(object sender, EventArgs e)
    {
       int  val = (Convert.ToInt32(ddlLineManager.SelectedValue));
       int  Dept = (Convert.ToInt32(ddldepartment.SelectedValue));
        int CompanyId = (Convert.ToInt32(Session["CompanyId"]));


        int RoleID = Convert.ToInt32(Session["RoleId"]);
        int ComID = Convert.ToInt32(Session["CompanyId"]);
        SqlParameter[] param2 =
        {
            new SqlParameter("@DesignationID",val),
            new SqlParameter("@DepartmentID",Dept),
            new SqlParameter("@CompanyID",CompanyId)

            };
        vt_Common.Bind_DropDown(ddlLineManagername, "VT_sp_BindLineManagerName", "EmployeeName", "EmployeeID", param2);
    }
}