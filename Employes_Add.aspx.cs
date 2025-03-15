using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employes_Add : System.Web.UI.Page
{
    DateTime EntryDate = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtCurrentAddress.Attributes.Add("placeholder", "Street Address,\nArea,\nCity,\nState,\nZip,\nCountry");
        //txtParmanantAddress.Attributes.Add("placeholder", "Street Address,\nArea,\nCity,\nState,\nZip,\nCountry");
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadPage(sender, e);
                //BindDep();
                txtDOB.Text = EntryDate.ToString("MM/dd/yyyy");
                txtWeddAnnev.Text = EntryDate.ToString("MM/dd/yyyy");
                txtJoiningDate.Text = EntryDate.ToString("MM/dd/yyyy");
                txtConfrDate.Text = EntryDate.ToString("MM/dd/yyyy");

                TxtEmployeeCode.Text = Get_EmployeeCode().ToString(); 
            }
        }
    }

    public int Get_EmployeeCode()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        var Data = db.vt_tbl_Employee.OrderByDescending(u => u.EmployeeID).FirstOrDefault();
        if (Data == null)
        {
            return 1000;
        }
        else
        {
            return 1000 + Data.EmployeeID;
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
    protected void LoadPage(object sender, EventArgs e)
    {
        ClearForm();
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
        ddlDesignation.Items.Clear();
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
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ID = 0;
        if (ddlcomp.SelectedValue != "")
        {
            //ID = Convert.ToInt32(ddlcomp.SelectedValue);
            ID = (Convert.ToInt32(Session["CompanyId"]));
        }
        else
        {
            ID = (Convert.ToInt32(Session["CompanyId"]));
        }
        
        BindDesignation(ID);
        //ddlLineManagerDesignation_SelectedIndexChanged(sender, e);
        ddldepartment_SelectedIndexChanged(sender, e);
        //UpView.Update();

        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }

    //public void BindDep()
    //{
    //    int companyID = 0;
    //    companyID = (Convert.ToInt32(Session["CompanyId"]));
    //    SqlParameter[] param =
    //    {
    //        new SqlParameter("@CompanyID",companyID)
    //    };
    //    vt_Common.Bind_DropDown(ddldepartment, "VT_SP_BindDepart", "Department", "DepartmentID", param);
    //}
    
    protected void btnSaveEmployee_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_Employee emp = new vt_tbl_Employee();
                int Company_ID = Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];
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
                        if (Txtusername.Text != "" || !string.IsNullOrEmpty(Txtusername.Text) || !string.IsNullOrWhiteSpace(Txtusername.Text))
                        {
                            emp.EmployeeName = Txtusername.Text;
                        }
                      //  emp.EnrollId = TxtEmployeeCode.Text;
                        if (TxtPassword.Text != "" || !string.IsNullOrEmpty(TxtPassword.Text) || !string.IsNullOrWhiteSpace(TxtPassword.Text))
                        {
                            emp.EmpPassword = vt_Common.Encrypt(TxtPassword.Text);
                        }

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
                        if (ChkGraduity.Checked) emp.IsGraduaty = true; else emp.IsGraduaty = false;


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
                        if (hdnRearName.Value == "")
                        {
                            hdnRearName.Value = null;
                            //emp.NICImage_Rear = hdnRearName.Value == "" ? hdnRearName.Value : hdnRearName.Value;

                        }
                        else
                        {
                            emp.NICImage_Rear= hdnRearName.Value == "" ? hdnRearName.Value : hdnRearName.Value;
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

                        //Address Working Salman
                        emp.Mobile = TxtMobile.Text;
                        emp.HomePhone = TxtHomePhone.Text;
                        emp.Current_Address1 = TxtCurrent_Address1.Text;
                        emp.Current_Address2 = TxtCurrent_Address2.Text;
                        emp.Current_Address3 = TxtCurrent_Address3.Text;
                        emp.Current_City = TxtCurrent_City.Text;
                        emp.Current_State = TxtCurrent_State.Text;
                        emp.Current_Zip = TxtCurrent_Zip.Text;
                        emp.Current_Country = TxtCurrent_Country.Text;

                        emp.Permenant_Address1 = TxtPermenant_Address1.Text;
                        emp.Permenant_Address2 = TxtPermenant_Address2.Text;
                        emp.Permenant_Address3 = TxtPermenant_Address3.Text;
                        emp.Permenant_City = TxtPermenant_City.Text;
                        emp.Permenant_State = TxtPermenant_State.Text;
                        emp.Permenant_Zip = TxtPermenant_Zip.Text;
                        emp.Permenant_Country = TxtPermenant_Country.Text;


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

                        //LOG ENTRY
                        var LastRecord = db.vt_tbl_Employee.OrderByDescending(x=>x.EmployeeID).FirstOrDefault(); 
                        vt_tbl_Employee_TransferLog empTrans = new vt_tbl_Employee_TransferLog();
                        empTrans.EmployeeID = LastRecord.EmployeeID;
                        empTrans.CompanyID = Convert.ToInt32(ddlcomp.SelectedValue);
                        empTrans.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
                        empTrans.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        empTrans.ManagerID = Convert.ToInt32(ddlLineManager.SelectedValue);
                        empTrans.EmployeeType = ddEmployeType.SelectedValue;
                        if (Txtusername.Text != "" || !string.IsNullOrEmpty(Txtusername.Text) || !string.IsNullOrWhiteSpace(Txtusername.Text))
                        {
                            empTrans.FirstName = Txtusername.Text;
                        }
                        empTrans.LastName = TxtLastName.Text;
                        empTrans.Email = Txtemail.Text;
                        empTrans.EntryDate = EntryDate;
                        empTrans.Action = "Insert";
                        db.Entry(empTrans).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        MsgBox.Show(Page, MsgBox.success, emp.EmployeeName, "Successfully Saved!");
                        ClearForm();
                        //LoadData();
                        //UpView.Update();
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


    #region DataBind
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
        if (ddldepartment.Items.Count > 0)
        {
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

    public static string CheckEnrollID(string EnrollId)
    {
        
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x=>x.EnrollId == EnrollId).SingleOrDefault();

        
        return ("CheckEnrollID");
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool CheckEmail(string Email)
        {
        vt_EMSEntities db = new vt_EMSEntities();
        int companyId = Convert.ToInt32(HttpContext.Current.Session["Companyid"]);
        var EmployeeObj = db.vt_tbl_Employee.Where(x=>x.Email.ToUpper() == Email.ToUpper() && x.CompanyID==companyId).FirstOrDefault();
        var userObj = db.vt_tbl_User
          .Where(x => x.Email.ToUpper() == Email.ToUpper()).FirstOrDefault();

        if (EmployeeObj == null && userObj == null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool Check_EmployeeCode(string EmployeeCode)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.EnrollId == EmployeeCode).SingleOrDefault();
        if (EmployeeObj != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool Check_Username(string Username)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.EmployeeName == Username).SingleOrDefault();
        if (EmployeeObj != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}