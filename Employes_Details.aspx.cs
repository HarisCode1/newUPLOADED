using HiQPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Viftech;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;

using System.Data.SqlClient;

using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System.Collections;
using System.ComponentModel.Design;
using System.Configuration;

public partial class Employes_Details : System.Web.UI.Page
{
    private int ID = 0;
    int Companyid =0;
    private string employeeStatus = "";
    string emptype = "";
    Custommethods customMethods = new Custommethods();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            if (!IsPostBack)
            {
                int RoleID = Convert.ToInt32(Session["RoleID"]);
                 Companyid= Convert.ToInt32(Session["CompanyID"]);
                if (RoleID ==2)
                {
                    divpayroll.Visible = false;
                    divbankmode.Visible = false;
                }
                Bind_GvTransferLog();
                if (ID > 0)
                {
                    FillDetails(ID);
                    Bind_GvLog();
                    Bind_JbLog();
                }
            }
        }
    }
    public DateTime? GetDateFromTextBox(string txtdate)
    {
        string[] formats = { "dd/MM/yyyy", "dd/MM", "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy" };

        DateTime dobDT;

        if (!string.IsNullOrWhiteSpace(txtdate) && DateTime.TryParseExact(txtdate, formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out dobDT))
        {
            //11:34 PM
            string dobtime = DateTime.UtcNow.AddHours(4).ToString("hh:mm tt");
            DateTime timePart = DateTime.ParseExact(dobtime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            DateTime combinedDateTime = dobDT.Date.Add(timePart.TimeOfDay);

            return combinedDateTime;

            // return dobDT;

        }
        else
        {
            return null;
        }
    }
    public string GetEmployeeStatus(string emptype, DateTime empjoiningdate, bool IsConfirmed)
    {
        string employeeStatus = string.Empty;

        // Type 1: Permanent
        if (emptype == "1")
        {
            employeeStatus = (IsConfirmed) ? "Permanent" : 
                             (empjoiningdate.AddMonths(3) <= DateTime.Now && IsConfirmed) ? "Permanent" :
                             (empjoiningdate.AddMonths(3) > DateTime.Now) ? "Probation" :
                             (empjoiningdate.AddMonths(3) <= DateTime.Now && !IsConfirmed) ? "Probation Completed" :
                              string.Empty;
        }
        // Type 5: Internship
        else if (emptype == "5")
        {
            employeeStatus = (IsConfirmed) ? "Permanent" :
                             (empjoiningdate.AddMonths(6) <= DateTime.Now && !IsConfirmed) ? "Probation Completed" :
                             (empjoiningdate.AddMonths(6) > DateTime.Now && !IsConfirmed) ? "Internship" :
                             (empjoiningdate.AddMonths(6) <= DateTime.Now && IsConfirmed) ? "Permanent" :
                             string.Empty;
        }
        // Type 8: Contract
        else if (emptype == "8")
        {
            employeeStatus = IsConfirmed ? "Permanent" : "Contract";
        }
        else
        {
            // Default case when no type matches
            employeeStatus = null;
        }

        return employeeStatus;
    }
    private void Bind_GvLog()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        DataSet Ds = ProcedureCall.SpCall_Sp_Get_Employee_PromotionLog_New(ID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Dt.Columns.Add("FileClass", typeof(string));
                foreach (DataRow dr in Dt.Rows)
                {
                    dr["FileClass"] = (dr["PromotionDocxPath"].ToString() != "") ? "btn btn-link" : "btn btn-link btn-dark disabled";
                }
                GvLog.DataSource = Dt;
            }
            else
            {
                GvLog.DataSource = null;
            }
        }
        else
        {
            GvLog.DataSource = null;
        }
        GvLog.DataBind();
    }
    private void Bind_JbLog()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        DataSet Ds = ProcedureCall.SpCall_Sp_Get_Employee_JobLog(ID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                //Dt.Columns.Add("FileClass", typeof(string));
                //foreach (DataRow dr in Dt.Rows)
                //{
                //    dr["FileClass"] = (dr["PromotionDocxPath"].ToString() != "") ? "btn btn-link" : "btn btn-link btn-dark disabled";
                //}
                JLGrid.DataSource = Dt;
            }
            else
            {
                JLGrid.DataSource = null;
            }
        }
        else
        {
            JLGrid.DataSource = null;
        }
        JLGrid.DataBind();
    }
    private void FillDetails(int ID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_Employee EmpData = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        int Companyid = Convert.ToInt32(Session["CompanyID"]);
        int Userid = Convert.ToInt32(Session["UserId"]);
        int desigid = 0;
        int dept = 0;
        int reportto = 0;
        if (EmpData != null)
        {
            
            dept =Convert.ToInt32(EmpData.DepartmentID);
            var qryfordesigid = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
            if (qryfordesigid != null)
            {
                desigid =Convert.ToInt32(qryfordesigid.DesignationID);
            }
            var query = db.vt_tbl_Designation.Where(x => x.DesignationID.Equals(desigid) && x.TopDesignationID == 0).FirstOrDefault();
            if (query != null)
            {
                reportto =Convert.ToInt32(query.ReportTo);
            }
            var qryfortopdesignation = db.vt_tbl_TopDesignations.Where(x => x.Id == reportto).FirstOrDefault();
            if (qryfortopdesignation != null)
            {
                lblreportto.InnerText = qryfortopdesignation.TopDesignations;
                repotto.Visible = true;
            }
            else
            {
                repotto.Visible = false;
            }
            int DesignationID = 0;
            DesignationID =Convert.ToInt32(EmpData.DesignationID);
            int DepartmentID = 0;
            DepartmentID = Convert.ToInt32(EmpData.DepartmentID);
            int RoleID = 0;
            int managerid =Convert.ToInt32(EmpData.ManagerID);
            RoleID =Convert.ToInt32(Session["RoleId"]);           
            DataTable dt = ProcedureCall.GetLineManager(DesignationID, DepartmentID, Companyid).Tables[0];
            if (dt.Rows.Count > 0)
            {
                LblDesignation.InnerText = dt.Rows[0]["Designation"].ToString();
            }
            else
            {
                DataTable dthradmin = ProcedureCall.GetHRManager(RoleID, Companyid).Tables[0];
                if (dthradmin.Rows.Count > 0)
                {
                    LblDesignation.InnerText = dthradmin.Rows[0]["UserName"].ToString();
                }
            }        
            LblName1.Text = vt_Common.IsStringEmpty(EmpData.FirstName);
            LblName2.Text = vt_Common.IsStringEmpty(EmpData.LastName);
            int desgid =Convert.ToInt32(EmpData.DesignationID);
            if (EmpData.DesignationID != null)
            {
                LblDesignation1.Text = ProcedureCall.sp_SingleValue("vt_tbl_designation", "Designation", "DesignationID", EmpData.DesignationID).ToString();
            }
            
            LblBasicSalary.InnerText = vt_Common.IsStringEmpty(EmpData.BasicSalary.ToString());
            LblHouseRent.InnerText = vt_Common.IsStringEmpty(EmpData.HouseRentAllownce.ToString());
            LblMedical.InnerText = vt_Common.IsStringEmpty(EmpData.MedicalAllowance.ToString());
            LblTransport.InnerText = vt_Common.IsStringEmpty(EmpData.TransportAllownce.ToString());
            LblFuel.InnerText = vt_Common.IsStringEmpty(EmpData.FuelAllowance.ToString());
            LblSpecial.InnerText = vt_Common.IsStringEmpty(EmpData.SpecialAllowance.ToString());
            //LblPfType.InnerText = vt_Common.IsStringEmpty(EmpData.PFType.ToString());
            //LblPf.InnerText = EmpData.PFApplicable == null || Convert.ToInt16(EmpData.PFApplicable) == 0 ? "Not Applicable" : "Applicable";
            if (EmpData.FromBank == "")
            {
            
            }
            else
            {
                LblFromBank.InnerHtml = vt_Common.IsStringEmpty(EmpData.FromBank);
            }
            
           
            lblpaymentmethod.InnerText= vt_Common.IsStringEmpty(EmpData.PaymentMethod);
            LblFirstName.InnerText = vt_Common.IsStringEmpty(EmpData.FirstName);
            LblLastName.InnerText = vt_Common.IsStringEmpty(EmpData.LastName);

            if (EmpData.ImageName != null || EmpData.ImageName != "")
            {
                bool IsFile = File.Exists(Server.MapPath("~/images/Employees/ProfileImage/" + EmpData.ImageName));
                if (IsFile)
                {
                    empImageView.ImageUrl = "~/images/Employees/ProfileImage/" + EmpData.ImageName;
                    linkImage.Attributes.Add("href", "~/images/Employees/ProfileImage/" + EmpData.ImageName);
                }
                else
                {
                    empImageView.ImageUrl = ("../../images/user-image.png");
                    linkImage.Attributes.Add("href", "../../images/user-image.png");
                }
            }
            else
            {
                empImageView.ImageUrl = ("../../images/user-image.png");
                linkImage.Attributes.Add("href", "../../images/user-image.png");
            }
            lblPEmail.InnerText = vt_Common.IsStringEmpty(EmpData.Email);
            //  LblDesignation.InnerText = ProcedureCall.sp_SingleValue("vt_tbl_designation", "Designation", "DesignationID", EmpData.DesignationID).ToString();
            if (EmpData.Type != null)
            {
                LblType.InnerText = ProcedureCall.sp_SingleValue("vt_tbl_TypeofEmployee", "Type", "ID", Convert.ToInt32(EmpData.Type)).ToString();
            }
            if (EmpData.FatherHusbandName !=null ||EmpData.RelationWithEmployee !=null  || EmpData.DOB !=null || EmpData.MartialStatus !=null || EmpData.Sex !=null || EmpData.Phone != null || EmpData.Mobile !=null || EmpData.HomePhone != null || EmpData.CurrentAddress != null || EmpData.ParmanantAddress !=null || EmpData.JobPeriod !=null || EmpData.ConfirmationDate != null || EmpData.JoiningDate !=null || EmpData.JobStatus !=null || EmpData.ToBranch !=null || EmpData.FromBranch !=null || EmpData.AccountNo !=null )
            {
                try
                {
                    if (EmpData.DepartmentID != null)
                    {
                        LblDepartment.InnerText = ProcedureCall.sp_SingleValue("vt_tbl_department", "Department", "DepartmentID", EmpData.DepartmentID).ToString();
                    }
                    LblFatherName.InnerText = vt_Common.IsStringEmpty(EmpData.FatherHusbandName);
                    LblRelation.InnerText = vt_Common.IsStringEmpty(EmpData.RelationWithEmployee);
                    Label1.Text = !string.IsNullOrEmpty(EmpData.FatherHusbandName) && EmpData.FatherHusbandName != "--Select One--"
                         ? EmpData.FatherHusbandName + "'s Name"
                         : "Husband's / Father's Name";
                    LblDOB.InnerText = vt_Common.IsStringEmpty(Convert.ToDateTime(EmpData.DOB).ToString("dd MMM yyyy"));
                    LblMaritalStatus.InnerText = vt_Common.IsStringEmpty(EmpData.MartialStatus);
                    LblGender.InnerText = vt_Common.IsStringEmpty(EmpData.Sex);
                    LblPhone.InnerText = vt_Common.IsStringEmpty(EmpData.Phone);
                    LblMobile.InnerText = vt_Common.IsStringEmpty(EmpData.HomePhone);
                    lblcnic.InnerText = vt_Common.IsStringEmpty(EmpData.CNIC);
                    sreligion.InnerText = vt_Common.IsStringEmpty(EmpData.Religion);
                    LblCurrentAddress.InnerText = EmpData.Current_Address1 + " " + EmpData.Current_City + " " + EmpData.Current_State + " " + EmpData.Current_Zip + " " + EmpData.Current_Country;
                    LblPermenantAddress.InnerText = EmpData.Permenant_Address1 + " " + EmpData.Permenant_Address2 + " " + EmpData.Permenant_Address3 + " " + EmpData.Permenant_City + " " + EmpData.Permenant_State + " " + EmpData.Permenant_Zip + " " + EmpData.Permenant_Country;
                    
                    //binding grid and manipulating local link to server link for downlaod files
                    User_BAL usr = new User_BAL();
                    var documents = usr.Getuploadeddocuments(Companyid, ID);
                    string serverBaseUrl = ConfigurationManager.AppSettings["ServerBaseUrl"];
                    string localPathPrefix = ConfigurationManager.AppSettings["LocalPathPrefix"];
                    if (documents != null)
                    {
                        foreach (DataRow row in documents.Rows)
                        {
                            string documentPath = row["DocumentPath"].ToString();
                            documentPath = documentPath.Replace(localPathPrefix, serverBaseUrl);
                            row["DocumentPath"] = documentPath;
                        }
                        UPGridView.DataSource = documents;
                        UPGridView.DataBind();
                    }
                    else
                    {
                        UPGridView.DataSource = null;
                        UPGridView.DataBind();
                    }
                    Console.WriteLine(documents+ "documents");

                    string emptype = EmpData.Type;
                    DateTime empjoiningdate = Convert.ToDateTime(EmpData.JoiningDate);
                    bool IsConfirmed = EmpData.IsConfirmed.HasValue && EmpData.IsConfirmed.Value;
                    employeeStatus = GetEmployeeStatus(emptype, empjoiningdate, IsConfirmed);
                    LblProbationPeriod.Text = employeeStatus;
                    
                    //Note:   this field is for bank branch code and its name in dabase is tobank
                    lblbranchcode.InnerText = EmpData.ToBranch;
                    lblbranchname.InnerText = EmpData.FromBranch;
                    lblaccountno.InnerHtml = vt_Common.IsStringEmpty(EmpData.AccountNo);
                    lblaccountitile.InnerText = vt_Common.IsStringEmpty(EmpData.AccountTitle);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
              
            }
            
        }
        DataSet Ds_Academics = ProcedureCall.SpCall_Vt_Sp_Employee_Qualifications(ID, 1);
        DataSet Ds_Certificates = ProcedureCall.SpCall_Vt_Sp_Employee_Qualifications(ID, 2);
        DataSet Ds_PreviousJobInfo = ProcedureCall.SpCall_Vt_Sp_Employee_PreviousJobInfo(ID);
        var ds_emptranfer=ProcedureCall.Sp_Call_SetEmployeeTranferRecords(ID);
        if (ds_emptranfer != null)
        {
            grdtranferemp.DataSource = ds_emptranfer;
            grdtranferemp.DataBind();
        }
        else
        {
            grdtranferemp.DataSource = null;

        }
        if ((Ds_Academics != null && Ds_Academics.Tables.Count > 0) && (Ds_Certificates != null && Ds_Certificates.Tables.Count > 0) && (Ds_PreviousJobInfo != null && Ds_PreviousJobInfo.Tables.Count > 0))
        {
            DataTable Dt_Academics = Ds_Academics.Tables[0];
            DataTable Dt_Certificates = Ds_Certificates.Tables[0];
            DataTable Dt_PjobInfo = Ds_PreviousJobInfo.Tables[0];
            if (Dt_Academics != null && Dt_Academics.Rows.Count > 0)
            {
                GvAcademics.DataSource = Dt_Academics;
            }
            else
            {
                GvAcademics.DataSource = null;
            }
            if (Dt_Certificates != null && Dt_Certificates.Rows.Count > 0)
            {
                GvCertificates.DataSource = Dt_Certificates;
            }
            else
            {
                GvCertificates.DataSource = null;
            }
            if (Dt_PjobInfo != null && Dt_PjobInfo.Rows.Count > 0)
            {
                //var a  = Dt_PjobInfo.
                //var data = Dt_PjobInfo.Rows[0]["JoiningDate"].ToString()
                for (int i = 0; i < Dt_PjobInfo.Rows.Count; i++)
                {

                    string JoiningDate1 = Dt_PjobInfo.Rows[i]["JoiningDate"].ToString();
                    string EndDate = Dt_PjobInfo.Rows[i]["EndDate"].ToString();
                    string JoiningDate = Dt_PjobInfo.Rows[i]["JoiningDate"].ToString();
                    Dt_PjobInfo.Rows[i]["JoiningDate"] = JoiningDate;
                    Dt_PjobInfo.Rows[i]["EndDate"] = EndDate;
                }
                grdpjobinfo.DataSource = Dt_PjobInfo;
            }
            
        else
        {
            grdpjobinfo.DataSource = null;
        }
            if (ds_emptranfer != null)
            {
                grdtranferemp.DataSource = ds_emptranfer;
                grdtranferemp.DataBind();
            }
            else
            {
                grdtranferemp.DataSource = null;
            }
        }
        else
        {
            GvAcademics.DataSource = null;
            GvCertificates.DataSource = null;
            grdpjobinfo.DataSource = null;
        }

        GvAcademics.DataBind();
        grdpjobinfo.DataBind();
        GvCertificates.DataBind();
    }
    protected void BtnPrintPdf_Click(object sender, EventArgs e)
    {
        string Body = string.Empty;
        int desigid = 0;
        int reportto = 0;
        using (StreamReader Reader = new StreamReader(Server.MapPath("~/Report/EmployeeDetails.html")))
        {
            Body = Reader.ReadToEnd();
        }
        #region HTML REPLACE
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_Employee Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        DataTable emptrans = ProcedureCall.Sp_Call_SetEmployeeTranferRecords(ID).Tables[0];
        //Employee Transfer Information
        var qrylinemanager = (from emp in db.vt_tbl_Employee
                              join des in db.vt_tbl_Designation on emp.ManagerID equals des.DesignationID
                              where emp.EmployeeID == ID
                              select des).FirstOrDefault();

        if (qrylinemanager != null)
        {
            //for line manage
            Body = Body.Replace("{recordmanager}", qrylinemanager.Designation);
        }
        else
        {
            var qryforhr = (from emp in db.vt_tbl_Employee
                            join usr in db.vt_tbl_User on emp.ManagerID equals usr.UserId
                            where emp.EmployeeID == ID
                            select usr).FirstOrDefault();
            if (qryforhr != null)
            {
                Body = Body.Replace("{recordmanager}", qryforhr.UserName);
            }
            else
            {
                Body = Body.Replace("{recordmanager}", "");
            }
        }
        if (emptrans.Rows.Count > 0)
        {
            var str = "";
            for (int i = 0; i < emptrans.Rows.Count; i++)
            {
                str += "<tr><td>" + emptrans.Rows[i]["EmployeeCode"].ToString() + "</td>" +
               "<td>" + emptrans.Rows[i]["EmployeeName"].ToString() + "</td>" +
               "<td>" + emptrans.Rows[i]["Department"].ToString() + "</td>" +
               "<td>" + emptrans.Rows[i]["Designation"].ToString() + "</td>" +
               "<td>" + emptrans.Rows[i]["LineManager"].ToString() + "</td>" +
               "</td>";




                string transferDateStr = emptrans.Rows[i]["Transferdate"].ToString();
                if (!string.IsNullOrEmpty(transferDateStr))
                {
                    try
                    {
                        str += "<td>" + emptrans.Rows[i]["Transferdate"].ToString() + "</td>";
                    }
                    catch
                    {
                        str += "<td>Invalid Date</td>"; 
                    }
                }
                else
                {
                    str += "<td>N/A</td>"; 
                }
                str += "</tr>";
            }
            Body = Body.Replace("{EmlpoyeeTransferRows}", str);
        }
        else
        {
            Body = Body.Replace("{EmlpoyeeTransferRows}", "");
        }
        int emptype = 0;
        //Job Info
        string emptype1 = Data.Type;
        DateTime empjoiningdate = Convert.ToDateTime(Data.JoiningDate);
        bool IsConfirmed = Data.IsConfirmed.HasValue && Data.IsConfirmed.Value;
        string employeeStatus = GetEmployeeStatus(emptype1, empjoiningdate, IsConfirmed);
        if (Data != null)
        {
            emptype = Convert.ToInt32(Data.Type);
            //Basic Info
            Body = Body.Replace("{empimage}", Server.MapPath("~/images/Employees/ProfileImage/" + Data.ImageName));
            Body = Body.Replace("{FirstName}", Data.FirstName);
            Body = Body.Replace("{LastName}", Data.LastName);
            //Personal Info
            Body = Body.Replace("{FatherHusbandName}", !string.IsNullOrEmpty(Data.FatherHusbandName) && Data.FatherHusbandName != "--Select One--"
            ? Data.FatherHusbandName + "'s Name"
            : "Husband's / Father's Name");

            Body = Body.Replace("{RelationWithEmployee}", Data.RelationWithEmployee);
            Body = Body.Replace("{DOB}", Convert.ToDateTime(Data.DOB).ToString("dd/MMM/yyyy"));
            Body = Body.Replace("{Sex}", Data.Sex);
            Body = Body.Replace("{Status}", Data.MartialStatus);
            Body = Body.Replace("{JobStatus}", employeeStatus);
            Body = Body.Replace("{Phone}", Data.Phone);
            Body = Body.Replace("{Mobile}", Data.Mobile);
            Body = Body.Replace("{HomePhone}", Data.HomePhone);
            Body = Body.Replace("{Phone}", Data.Phone);
            Body = Body.Replace("{CurrentAddress}", Data.Current_Address1 + " " + Data.Current_City + " " + Data.Current_State + " " + Data.Current_Zip + " " + Data.Current_Country);
            Body = Body.Replace("{Email}", Data.Email);
            Body = Body.Replace("{PEmail}", Data.Current_Address3);//Current_Address3 is used for personal email;
            Body = Body.Replace("{CNIC}", Data.CNIC);

           

            //Body = Body.Replace("{Probation}", employeeStatus);
            //Body = Body.Replace("{Probation}", Data.ProvisionalPeriod);
            // Body = Body.Replace("{ConfimationDate}", Convert.ToDateTime(Data.ConfirmationDate).ToString("d/M/yyyy"));
            Body = Body.Replace("{JoiningDate}", Convert.ToDateTime(Data.JoiningDate).ToString("dd/MMM/yyyy"));
            Body = Body.Replace("{jobStatus}", Data.JobStatus);

            ////Payroll Info
            //Body = Body.Replace("{BasicSalary}", Data.BasicSalary.ToString());
            //Body = Body.Replace("{HouseRentAllowance}", Data.HouseRentAllownce.ToString());
            //Body = Body.Replace("{MedicalAllowance}", Data.MedicalAllowance.ToString());
            //Body = Body.Replace("{TransportAllowance}", Data.TransportAllownce.ToString());
            //Body = Body.Replace("{FuelAllowance}", Data.FuelAllowance.ToString());
            //Body = Body.Replace("{SpecialAllowance}", Data.SpecialAllowance.ToString());
            //Body = Body.Replace("{TypeOfPF}", Data.PFType.ToString());
            //// Body = Body.Replace("{PF}", Data.PFApplicable == null || Convert.ToBoolean(Data.PFApplicable) == false ? "Not Applicable" : "Applicable");

            ////Bank Info
            //Body = Body.Replace("{paymentmethod}", Data.PaymentMethod);
            //Body = Body.Replace("{FromBank}", Data.FromBank);
            //Body = Body.Replace("{FromBranch}", Data.FromBranch);
            //Body = Body.Replace("{ToBank}", Data.ToBank);
            //Body = Body.Replace("{ToBranch}", Data.ToBranch);
            //Body = Body.Replace("{AccountNo}", Data.AccountNo == "" ? "0" : Data.AccountNo);
            //Body = Body.Replace("{AccountTitles}", Data.AccountTitle == "" ? "0" : Data.AccountTitle);
        }
        else
        {
            //emptype = Convert.ToInt32(Data.Type);
            //Basic Info
            Body = Body.Replace("{empimage}", "");
            Body = Body.Replace("{FirstName}", "");
            Body = Body.Replace("{LastName}", "");

            //Personal Info
            Body = Body.Replace("{FatherHusbandName}", "");
            Body = Body.Replace("{RelationWithEmployee}","");
            Body = Body.Replace("{DOB}", "");
            Body = Body.Replace("{Sex}", "");
            Body = Body.Replace("{Status}","");
            Body = Body.Replace("{Phone}", "");
            Body = Body.Replace("{Mobile}", "");
            Body = Body.Replace("{HomePhone}","");
            Body = Body.Replace("{Phone}", "");
            Body = Body.Replace("{CurrentAddress}", "");
            Body = Body.Replace("{Email}", "");
            Body = Body.Replace("{PEmail}", "");
            Body = Body.Replace("{CNIC}", "");


            //Job Info
            Body = Body.Replace("{Probation}", "");
            // Body = Body.Replace("{ConfimationDate}", Convert.ToDateTime(Data.ConfirmationDate).ToString("d/M/yyyy"));
            Body = Body.Replace("{JoiningDate}", "");
            Body = Body.Replace("{Status}", "");

            //Payroll Info
            Body = Body.Replace("{BasicSalary}","");
            Body = Body.Replace("{HouseRentAllowance}", "");
            Body = Body.Replace("{MedicalAllowance}", "");
            Body = Body.Replace("{TransportAllowance}", "");
            Body = Body.Replace("{FuelAllowance}", "");
            Body = Body.Replace("{SpecialAllowance}","");
            Body = Body.Replace("{TypeOfPF}", "");
            // Body = Body.Replace("{PF}", Data.PFApplicable == null || Convert.ToBoolean(Data.PFApplicable) == false ? "Not Applicable" : "Applicable");

            //Bank Info
            Body = Body.Replace("{paymentmethod}","");
            Body = Body.Replace("{FromBank}", "");
            Body = Body.Replace("{FromBranch}","");
            Body = Body.Replace("{ToBank}", "");
            Body = Body.Replace("{ToBranch}", "");
            Body = Body.Replace("{AccountNo}", "");
            Body = Body.Replace("{AccountTitles}", "");

        }
   
        //designation
        var queryy = (from emp in db.vt_tbl_Employee
                      join des in db.vt_tbl_Designation on emp.DesignationID equals des.DesignationID
                      join deprt in db.vt_tbl_Department on emp.DepartmentID equals deprt.DepartmentID

                      where emp.EmployeeID == ID
                      select new
                      {
                          des.Designation,
                          deprt.Department,
                      }).FirstOrDefault();
        //end
        //get emp type
        var emptyp = db.vt_tbl_TypeofEmployee.Where(x => x.Id == emptype).FirstOrDefault();
        if (queryy !=null)
        {
            desigid = Convert.ToInt32(Data.DesignationID);
            var qryforreporttoid = db.vt_tbl_Designation.Where(x => x.DesignationID == desigid).FirstOrDefault();
            if (qryforreporttoid != null)
            {
                reportto =Convert.ToInt32(qryforreporttoid.ReportTo);

            }
            var qryfortopdesignation = db.vt_tbl_TopDesignations.Where(x =>x.Id == reportto).FirstOrDefault();
            if (qryfortopdesignation != null)
            {
                Body = Body.Replace("{ReportTo}", qryfortopdesignation.TopDesignations);
            }
            else
            {
                Body = Body.Replace("{ReportTo}"," ");
                Body = Body.Replace("Report To", " ");
            }
            Body = Body.Replace("{Designation}", queryy.Designation);
            Body = Body.Replace("{Department}", queryy.Department);
        }
        else
        {
            Body = Body.Replace("{Designation}", "");
            Body = Body.Replace("{Department}", "");
            Body = Body.Replace("{ReportTo}", "");

        }

        //GET EMP TYPE

        if (emptyp != null)
        {
            Body = Body.Replace("{Type}", emptyp.Type);
        }
        else
        {
            Body = Body.Replace("{Type}", "");
        }        
       // Body = Body.Replace("{PermenantAddress}", Data.Permenant_Address1 + " " + Data.Permenant_Address2 + " " + Data.Permenant_Address3 + " " + Data.Permenant_City + " " + Data.Permenant_State + " " + Data.Permenant_Zip + " " + Data.Permenant_Country);

      
        //ACADEMIC INFO
        var query = (from emp in db.vt_tbl_Employee
                     join qual in db.vt_tbl_QualificationDetails on emp.EmployeeID equals qual.EmployeeId
                     where emp.EmployeeID == ID && qual.Type == 1
                     select qual).ToList();
        if (query.Count >0)
        {
            var Academic = "";
            foreach (var item in query)
            {
                Academic += "<tr><td>" + item.InstituteName + "</td><td>" + item.Qualification+ "</td ><td>" + item.Year+ "</td><td>" + item.Marks + "</td></tr> ";

            }
            Body = Body.Replace("{AcademicInformation}", Academic);
            // Body = Body.Replace("{InstituteName}",vt_Common.IsStringEmpty(query.InstituteName));
            //Body = Body.Replace("{Qualification}", vt_Common.IsStringEmpty(query.Qualification));
            //Body = Body.Replace("{Years}", vt_Common.IsStringEmpty(Convert.ToInt32(query.Year).ToString()));
            //Body = Body.Replace("{GPA}", vt_Common.IsStringEmpty(query.Marks.ToString()));
        }
        else

        {
            Body = Body.Replace("{AcademicInformation}", "");
            //Body = Body.Replace("{InstituteName}", "");
            //Body = Body.Replace("{Qualification}", "");
            //Body = Body.Replace("{Years}","");
            //Body = Body.Replace("{GPA}", "");


        }

        //PROMOTION INFO
        DataSet Ds = ProcedureCall.SpCall_Sp_Get_Employee_PromotionLog_New(ID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            var str = "";
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Dt.Columns.Add("FileClass", typeof(string));
                foreach (DataRow dr in Dt.Rows)
                {
                    //string EffectiveDate = dr["EffectiveDate"].ToString("dd/MMM/yyyy");


                    //    DateTime transferDate = Convert.ToDateTime(transferDateStr);
                    //    str += "<td>" + transferDate.ToString("dd/MMM/yyyy") + "</td>";


                    str += "<tr>" +
      "<td>" + dr["EmployeeID"].ToString() + "</td>" +
      "<td>" + dr["EmployeeName"].ToString() + "</td>" +
      "<td>" + dr["Department"].ToString() + "</td>" +
      "<td>" + dr["Designation"].ToString() + "</td>" +
      "<td>" + dr["BasicSalary"].ToString() + "</td>" +
      "<td>" + dr["HouseRentAllownce"].ToString() + "</td>" +
      "<td>" + dr["TransportAllownce"].ToString() + "</td>" +
      "<td>" + dr["MedicalAllowance"].ToString() + "</td>" +
      "<td>" + dr["FuelAllowance"].ToString() + "</td>";

                    string effectiveDateStr = dr["EffectiveDate"].ToString();

                    if (!string.IsNullOrEmpty(effectiveDateStr))
                    {
                        try
                        {
                            DateTime effectiveDate = Convert.ToDateTime(effectiveDateStr);
                            str += "<td>" + effectiveDate.ToString() + "</td>";
                        }
                        catch
                        {
                            str += "<td>Invalid Date</td>";
                        }
                    }
                    else
                    {
                        str += "<td>N/A</td>";
                    }

                    str += "<td>" + dr["Tax"].ToString() + "</td></tr>";








                }
                Body = Body.Replace("{EmlpoyeePromotionRows}", str);
            }
            else
            {
                Body = Body.Replace("{EmlpoyeePromotionRows}", null);
            }
        }


        //JOB LOG INFO
        DataSet JB = ProcedureCall.SpCall_Sp_Get_Employee_JobLog(ID);
        if (JB != null && JB.Tables.Count > 0)
        {
            DataTable Dt = JB.Tables[0];
            var str = "";
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Dt.Columns.Add("FileClass", typeof(string));
                foreach (DataRow dr in Dt.Rows)
                {


                    str += "<tr>";

                    string joiningDateStr = dr["joiningdate"].ToString();

                    if (!string.IsNullOrEmpty(joiningDateStr))
                    {
                        try
                        {
                            DateTime joiningDate = Convert.ToDateTime(joiningDateStr);
                            str += "<td>" + joiningDate.ToString() + "</td>";
                        }
                        catch
                        {
                            str += "<td>Invalid Date</td>";
                        }
                    }
                    else
                    {
                        str += "<td>N/A</td>";
                    }

                    str += "<td>" + dr["jobstatus"].ToString() + "</td>" +
                           "<td>" + dr["type"].ToString() + "</td>" +
                           "<td>" + dr["Designation"].ToString() + "</td>"+

                            "<td>" + dr["Department"].ToString() +
                           "</td></tr>";


                }
                Body = Body.Replace("{EmlpoyeeJobLogRows}", str);
            }
            else
            {
                Body = Body.Replace("{EmlpoyeeJobLogRows}", null);
            }
        }


        //Certificate Info
        var certificatquery = (from emp in db.vt_tbl_Employee
                               join cert in db.vt_tbl_QualificationDetails on emp.EmployeeID equals cert.EmployeeId
                               where emp.EmployeeID == ID && cert.Type == 2
                               select cert).ToList();
        if (certificatquery !=null)
        {
            var Certificate = "";
            foreach (var item in certificatquery)
            {
                Certificate += "<tr><td>" + item.InstituteName + "</td><td>" + item.Qualification + "</td ><td>" + item.Year + "</td><td>" + item.Grade + "</td></tr> ";

            }
            Body = Body.Replace("{CertificateInformation}", Certificate);
            //Body = Body.Replace("{CertificationsInstituteName}", certificatquery.InstituteName);
            //Body = Body.Replace("{CertificationsQualification}", certificatquery.Qualification);
            //Body = Body.Replace("{CertificationsYears}", Convert.ToInt32(certificatquery.Year ).ToString());
            //Body = Body.Replace("{CertificationsGPA}", certificatquery.Grade).ToString();
        }
        else
        {
            Body = Body.Replace("{CertificateInformation}", "");
            //Body = Body.Replace("{CertificationsInstituteName}", "");
            //Body = Body.Replace("{CertificationsQualification}", "");
            //Body = Body.Replace("{CertificationsYears}","");
            //Body = Body.Replace("{CertificationsGPA}", "");
        }


        //PREVIOUS JOB INFO
        var PjobInfo = db.vt_tbl_PreviousJobDetails.Where(x => x.EmployeeID == ID).ToList();
        if (PjobInfo.Count  != 0 )
        {

            var previousJob = "";
            foreach (var item in PjobInfo)
            {
                string EndDate = item.EndDate.ToString();
                //DateTime EndDate1 = Convert.ToDateTime(EndDate);

                string JoiningDate = item.JoiningDate.ToString();
                //DateTime JoiningDate1 = Convert.ToDateTime(JoiningDate);

                previousJob +=  
                    "<tr><td>" + item.PreviousCompanyName + "</td>" +
                    "<td>" + item.PreviousDesignation+ "</td >" + 
                    "<td>" + JoiningDate + "</td>" +
                    "<td>" + EndDate + "</td></tr> ";
     

            }

            Body = Body.Replace("{PreviousJobInformation}", previousJob);
            
        }
        else
        {
            Body = Body.Replace("{PreviousJobInformation}", "");
        }


        #endregion HTML REPLACE









        // instantiate the HiQPdf HTML to PDF converter
        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
        //htmlToPdfConverter.HtmlLoadedTimeout = 1200;
        htmlToPdfConverter.HtmlLoadedTimeout = 9999999;
        htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

        htmlToPdfConverter.SerialNumber = "CEBhWVhs-bkRhanpp-enE5OCY4-KDkoOigw-ODAoOzkm-OTomMTEx-MQ==";
        byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(Body, "");
        HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

        // let the browser know how to open the PDF document and the file name
        HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("attachment; filename=EmployeeDetails.pdf; size={0}",
                    pdfBuffer.Length.ToString()));

        // write the PDF buffer to HTTP response
        HttpContext.Current.Response.BinaryWrite(pdfBuffer);

        // call End() method of HTTP response to stop ASP.NET page processing
        HttpContext.Current.Response.End();
    }
    protected void BtnLog_Click(object sender, EventArgs e)
    {
        UpDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }
    private void Bind_GvTransferLog()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        DataSet Ds = ProcedureCall.SpCall_Sp_Get_Employee_TransferLog();
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                GvTransferLog.DataSource = Dt;
            }
            else
            {
                GvTransferLog.DataSource = null;
            }
        }
        else
        {
            GvTransferLog.DataSource = null;
        }
        GvTransferLog.DataBind();
    }
}



   
