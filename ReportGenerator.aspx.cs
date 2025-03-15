using HiQPdf;
using Ionic.Zip;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
//using itextsharp;

public partial class EmailSetting : System.Web.UI.Page
{
    public static int Companyid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                loadData();
                GetData(sender, e);
                BindLog();
               
               
            }
        }
    }

    public void GetData(object sender, EventArgs e)
    {
        string UserName = (string)Session["UserName"];
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            DataTable dt = new DataTable();
            var Query = db.GetAllCompanies().ToArray();
            DataTable dt1 = LINQResultToDataTable(Query);
            DataView view = new DataView(dt1);
            DataTable Distnctgrade = view.ToTable(true, "CompanyName", "CompanyID");
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataSource = Distnctgrade;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("Please Select", ""));

             Companyid = Convert.ToInt32(Session["CompanyId"]);
            ddlCompany.SelectedValue = Companyid.ToString();


            int Userid = Convert.ToInt32(Session["UserId"]);

            var activeuser = db.vt_tbl_User
                .Where(w => (w.UserId.Equals(Userid) && (w.RoleId == 4))).SingleOrDefault();

            string EmplEmail = activeuser != null ? activeuser.Email.ToString() : null;
            if (EmplEmail != null)
            {
                var EmployeeList = db.vt_tbl_Employee
                .Where(w => w.Email == EmplEmail && w.RoleID == 4)
               .Select(r => new
               {
                   EmployeeName = r.EmployeeName,     // Change to the actual property name
                   EmployeeID = r.EmployeeID          // Change to the actual property name
               })
                .ToList();
                ddlEmployee.DataSource = EmployeeList;
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();
            }


           // ddlCompany_SelectedIndexChanged(sender, e);
            if (UserName == "SuperAdmin")
            {
                ddlEmployee.Enabled = true;
            }
           else
            {
                ddlEmployee.Enabled = true;
            }
        }
    }

    public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
    {
        DataTable dt = new DataTable();

        PropertyInfo[] columns = null;

        if (Linqlist == null) return dt;

        foreach (T Record in Linqlist)
        {
            if (columns == null)
            {
                columns = ((Type)Record.GetType()).GetProperties();
                foreach (PropertyInfo GetProperty in columns)
                {
                    Type colType = GetProperty.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                    == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                }
            }

            DataRow dr = dt.NewRow();

            foreach (PropertyInfo pinfo in columns)
            {
                dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                (Record, null);
            }

            dt.Rows.Add(dr);
        }
        return dt;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static object Load(string Month,int EmpId)
    {
        string List = "";
       
        try
        {
            var month =Convert.ToDateTime(Month).Month;
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (Companyid == 0)
                {
                }
                else
                {
                    int year = DateTime.Now.Year;
                    DateTime MonthWise;
                    if (year != 0 && month != 0)
                    {
                        MonthWise = new DateTime(year, month, 1);                        
                        DataSet Ds = ProcedureCall.Sp_Call_SetEmpSalariesyearlySummarySheet(Companyid, EmpId, year);
                        DataTable dt = Ds.Tables[0];
                        var Query = dt;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //Query.Columns.Add("S.no");
                            //for (int i = 0; i < Query.Rows.Count; i++)
                            //{
                            //    Query.Rows[i]["S.no"] = i + 1;
                            //}
                            List = JsonConvert.SerializeObject(Query);
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        List = "Empty";
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
    public void loadData()
    {
        //int Companyid = Convert.ToInt32(Session["CompanyId"]);

        //ddlCompany.SelectedValue = Companyid.ToString();
    }

    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name, type);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var data = db.VT_SP_GetEmpSalary_Slip(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue)).ToList();

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = ReportViewer1.LocalReport;

            /* Get Path of RDLC */
            //string rdlPath = @"D:\Wasif Ali\Projects\Payrollproject\Payroll\SalarySlip.rdlc";
            //localReport.ReportPath = rdlPath;
            DataSet dataset = new DataSet();

            /*Get this from RDL file DataSource Name
             This will be in loop*/
            string DataSetName = "SalaryData";

            ReportDataSource rptDataSource = new ReportDataSource();
            rptDataSource.Name = DataSetName;
            rptDataSource.Value = data;

            localReport.DataSources.Clear();
            localReport.DataSources.Add(rptDataSource);

            int roleID = Convert.ToInt32(Session["RoleId"]);
            var roleDetail = db.vt_tbl_Role.Where(x => x.RoleID == roleID).FirstOrDefault();

            if (Convert.ToInt32(Session["RoleId"]) != 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('._Administration').hide();$('._Employee').hide();$('._Attendance').hide();$('._InputModules').hide();$('._SalaryGen').hide();$('._SalaryGenReport').hide();", true);
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {


        //    ddlEmployee.Items.Clear();
        //    using (vt_EMSEntities db = new vt_EMSEntities())
        //    {

        //        int Userid = Convert.ToInt32(Session["UserId"]);

        //        var activeuser = db.vt_tbl_User
        //            .Where(w => (w.UserId.Equals(Userid) && (w.RoleId == 4))).SingleOrDefault();

        //        string EmplEmail = activeuser != null ? activeuser.Email.ToString() : null;
        //        if (EmplEmail != null)
        //        {

        //            var EmployeeList = db.vt_tbl_Employee
        //            .Where(w => w.Email == EmplEmail && w.RoleID == 4)
        //           .Select(r => new
        //           {
        //               EmployeeName = r.EmployeeName,     // Change to the actual property name
        //               EmployeeID = r.EmployeeID          // Change to the actual property name
        //           })
        //            .ToList();

        //            ddlEmployee.DataSource = EmployeeList;
        //            ddlEmployee.DataTextField = "EmployeeName";
        //            ddlEmployee.DataValueField = "EmployeeID";
        //            ddlEmployee.DataBind();
        //        }
        //        else
        //        {
        //            //var EmployeeList = db.VT_SP_GetEmployees(Convert.ToInt32(UpdatePanel1SelectedValue)).ToList();
        //            var EmployeeList = db.VT_SP_GetEmployees(Convert.ToInt32(Session["CompanyId"])).ToList();

        //            ddlEmployee.DataSource = EmployeeList;
        //            ddlEmployee.DataTextField = "EmployeeName";
        //            ddlEmployee.DataValueField = "EmployeeID";
        //            ddlEmployee.DataBind();
        //            int empid = (Convert.ToInt32(Session["UserId"]));
        //            ddlEmployee.SelectedValue = empid.ToString();
        //        }
        //    }
        }

        #region Salman Code

        protected void BtnMonthly_Click(object sender, EventArgs e)
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        if (Companyid != 0)
        {
            HtmlTemplateToString();
        }
        else
        {
            MsgBox.Show(this.Page, MsgBox.danger, "", "Please Select Company First from DashBoard");
        }

    }

    protected void BtnYearly_Click(object sender, EventArgs e)
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        if (Companyid != 0)
        {
            if (HtmlTemplateToString_Zip())
            {
                ZipFolder();
                clearFolder(Server.MapPath("~/Uploads/SalarySlips/"));
            }
            else { MsgBox.Show(this.Page, MsgBox.warning, "", "No Record found"); }
        }
        else
        {
            MsgBox.Show(this.Page, MsgBox.danger, "", "Please Select Company First from DashBoard");
        }
    }

    private bool HtmlTemplateToString_Zip()
    {
        bool SlipGenerated = false;
        int PaySlip_Month = Convert.ToDateTime(txtDate.Text).Month;
        int PaySlip_Year = Convert.ToDateTime(txtDate.Text).Year;
        string TempDate = "";
        for (int i = PaySlip_Month; i > 0; i--)
        {
            TempDate = i + "/01/" + PaySlip_Year;
            if (Convert.ToBoolean(HtmlTemplateToString(TempDate, i, PaySlip_Year)))
            {
                SlipGenerated = true;
            }
        }
        return SlipGenerated;
    }

    private void HtmlTemplateToString()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        var Data = db.VT_SP_GetEmpSalary_Slip(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue == "" ? "0" : ddlCompany.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue)).ToList();
        string Body = string.Empty;
        //using streamreader for reading my htmltemplate

        //using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemplate.html")))
        using (StreamReader Reader = new StreamReader(Server.MapPath("~/Report/PaySlipHtml.html")))
        {
            Body = Reader.ReadToEnd();
        }
        //My Code
        int EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
        DateTime checkdate = Convert.ToDateTime(txtDate.Text);
        vt_tbl_salaryRecords sl = db.vt_tbl_salaryRecords.Where(x => x.EmployeeID == EmployeeId && x.SalaryMonth.Month ==checkdate.Month && x.SalaryMonth.Year== checkdate.Year).FirstOrDefault();
        if (sl != null)
        {
            int empid = sl.EmployeeID;
            var query = db.vt_tbl_Employee.Where(x => x.EmployeeID == empid).FirstOrDefault();
            var deprt = (from emp in db.vt_tbl_Employee
                         join dprt in db.vt_tbl_Department on emp.DepartmentID equals dprt.DepartmentID
                         where emp.EmployeeID == EmployeeId
                         select dprt).FirstOrDefault();
                       
            int checkmonth = Convert.ToDateTime(sl.SalaryMonth).Month;
            if (query != null)
            {
                //DateTime month = sl.SalaryMonth;
                int Month = Convert.ToDateTime(txtDate.Text).Month;
                if (Month == checkmonth)
                {
                    int Year = Convert.ToDateTime(txtDate.Text).Year;
                    MonthsEnum wdEnum;
                    Enum.TryParse<MonthsEnum>(Month.ToString(), out wdEnum);
                    Body = Body.Replace("{Date}", wdEnum.ToString() + " " + Year);

                    Body = Body.Replace("{EmployeeID}", Data[0].EmployeeID.ToString());
                    Body = Body.Replace("{EmployeeName}", Data[0].EmployeeName);

                    //Get Deprtment
                    if (deprt !=null)
                    {
                        Body = Body.Replace("{DepartmentID}", deprt.Department);
                    }
                   
                    Body = Body.Replace("{Designation}", Data[0].Position);

                    //Futher Employee Field 
                    Body = Body.Replace("{EmployeeCNIC}", query.CNIC);
                    Body = Body.Replace("{EmployeeAddress}", query.Current_Address1);
                    Body = Body.Replace("{PaidThrough}", query.PaymentMethod);
                    Body = Body.Replace("{BankName}", query.FromBank);
                    Body = Body.Replace("{AccountNumber}", query.AccountNo);
                    Body = Body.Replace("{HomePhone}", query.HomePhone);



                    Body = Body.Replace("{Basic}", Data[0].BasicSalary.ToString());
                    Body = Body.Replace("{MedicalAllowance}", Data[0].MedicalAllowance.ToString());
                    Body = Body.Replace("{HouseRentAllowance}", Data[0].HouseRentAllownce.ToString());
                    Body = Body.Replace("{TransportAllowance}", Data[0].TransportAllownce.ToString());
                    Body = Body.Replace("{FuelAllowance}", Data[0].FuelAllowance.ToString());
                    Body = Body.Replace("{Overtime}", Data[0].Overtime.ToString());
                    Body = Body.Replace("{Bonus}", Data[0].Bonus.ToString());

                    double SubTotal = Convert.ToDouble(Data[0].BasicSalary.ToString()) + Convert.ToDouble(Data[0].MedicalAllowance.ToString()) + Convert.ToDouble(Data[0].HouseRentAllownce.ToString()) + Convert.ToDouble(Data[0].TransportAllownce.ToString()) + Convert.ToDouble(Data[0].FuelAllowance.ToString())  + Convert.ToDouble(Data[0].Bonus.ToString());
                    Body = Body.Replace("{SubTotal}", SubTotal.ToString());

                    Body = Body.Replace("{finecharges}", sl.FineCharges.ToString());
                    string taxcalpermonth = sl.Tax.ToString() == "" ? "0.00" : sl.Tax.ToString();//= double.Parse((query.Tax / 12).ToString()).ToString("0.00");
                    Body = Body.Replace("{IncomeTax}", taxcalpermonth.ToString());
                    Body = Body.Replace("{AdvanceSalary}", Data[0].Advance.ToString());
                    Body = Body.Replace("{Loan}", Data[0].Loan.ToString());
                    Body = Body.Replace("{EOBI}", Data[0].EOBI.ToString());
                    Body = Body.Replace("{LeaveOnJoining}", Data[0].LeaveonJoining.ToString());
                    //Convert.ToDouble(Data[0].PF.ToString()) +
                    double SubTotal2 = Convert.ToDouble(Data[0].Tax.ToString()) + Convert.ToDouble(Data[0].Advance.ToString()) + Convert.ToDouble(sl.FineCharges.ToString())  + Convert.ToDouble(Data[0].Loan.ToString()) + Convert.ToDouble(Data[0].EOBI.ToString()) + Convert.ToDouble(Data[0].LeaveonJoining.ToString());
                    Body = Body.Replace("{SubTotal2}", SubTotal2.ToString());

                    Body = Body.Replace("{NetPay}", Data[0].NetSalary.ToString());

                    // instantiate the HiQPdf HTML to PDF converter
                    HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
                    //htmlToPdfConverter.HtmlLoadedTimeout = 1200;
                    htmlToPdfConverter.HtmlLoadedTimeout = 9999999;
                    htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
                    htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
                    //htmlToPdfConverter.BrowserZoom = 150;
                    // hide the button in the created PDF
                    htmlToPdfConverter.HiddenHtmlElements = new string[] { "#convertThisPageButtonDiv" };
                    htmlToPdfConverter.SerialNumber = "CEBhWVhs-bkRhanpp-enE5OCY4-KDkoOigw-ODAoOzkm-OTomMTEx-MQ==";

                    // render the HTML code as PDF in memory
                    //byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);
                    byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(Body, "");

                    // send the PDF file to browser

                    HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

                    // let the browser know how to open the PDF document and the file name
                    HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("attachment; filename=PaySlip.pdf; size={0}",
                                pdfBuffer.Length.ToString()));

                    // write the PDF buffer to HTTP response
                    HttpContext.Current.Response.BinaryWrite(pdfBuffer);

                    // call End() method of HTTP response to stop ASP.NET page processing
                    HttpContext.Current.Response.End();
                }
                else
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "Sorry  this month salary does not exist!");
                }
            }

            else
            {
                MsgBox.Show(Page, MsgBox.danger, "", "Sorry  this month salary does not exist!");
            
        }
        }



    }

    private bool HtmlTemplateToString(string date, int Month, int Year)
    {
        bool SlipGenerated = false;
        int EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_salaryRecords sl = db.vt_tbl_salaryRecords.Where(x => x.EmployeeID == EmployeeId).FirstOrDefault();
        if (sl == null)
        {
            return SlipGenerated;
        }
        int empid = sl.EmployeeID;
        var query = db.vt_tbl_Employee.Where(x => x.EmployeeID == empid).Select(x => new { x.CNIC, x.Current_Address1, x.PaymentMethod, x.FromBank, x.HomePhone, x.AccountNo }).FirstOrDefault();
        var deprtid = (from emp in db.vt_tbl_Employee
                       join dep in db.vt_tbl_Department on emp.DepartmentID equals dep.DepartmentID
                       where emp.EmployeeID == EmployeeId
                       select dep).FirstOrDefault();
      
        Companyid = Convert.ToInt32(Session["CompanyID"]);
        var Data = db.VT_SP_GetEmpSalary_Slip(Convert.ToDateTime(date), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue)).ToList();
        var ds = ProcedureCall.Sp_Call_SetEmpSalariesyearlySummarySheet(Companyid, Convert.ToInt32(ddlEmployee.SelectedValue), Year);
       
        //var list = ds.Tables[0].Select().ToList();
        var html = ConvertDataTableToHTML(ds.Tables[0]);
        html = html.Replace("TransportAllowance", "TransportAllow");
        html = html.Replace("FoodAllowance", "FoodAllow");
        //html = html.Insert(6, "='table table-bordered'");
        if (Data != null && Data.Count > 0)
        {
            string Body = string.Empty;
            //using streamreader for reading my htmltemplate

            //using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemplate.html")))
            using (StreamReader Reader = new StreamReader(Server.MapPath("~/Report/YearlyPaySlipHtml.html")))
            {
                Body = Reader.ReadToEnd();
            }
            MonthsEnum wdEnum;
            //if ()
            //{

            //}
            Enum.TryParse<MonthsEnum>(Month.ToString(), out wdEnum);
            Body = Body.Replace("{Date}", wdEnum.ToString() + " " + Year);
            Body = Body.Replace("{EmployeeID}", Data[0].EmployeeID.ToString());
            Body = Body.Replace("{EmployeeName}", Data[0].EmployeeName);
            if (deprtid != null)
            {
                Body = Body.Replace("{DepartmentID}", deprtid.Department.ToString());

            }
            else
            {
                Body = Body.Replace("{DepartmentID}","");
            }
          
            Body = Body.Replace("{Designation}", Data[0].Position);
            Body = Body.Replace("{EmployeeCNIC}", query.CNIC);
            Body = Body.Replace("{EmployeeAddress}", query.Current_Address1);
            Body = Body.Replace("{PaidThrough}", query.PaymentMethod);
            Body = Body.Replace("{BankName}", query.FromBank);
            Body = Body.Replace("{AccountNumber}", query.AccountNo);
            Body = Body.Replace("{HomePhone}", query.HomePhone);
            Body = Body.Replace("{datagridData}", html);

            // instantiate the HiQPdf HTML to PDF converter

            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            //htmlToPdfConverter.HtmlLoadedTimeout = 1200;
            htmlToPdfConverter.HtmlLoadedTimeout = 9999999;
            htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
            htmlToPdfConverter.BrowserZoom = 115;
            // hide the button in the created PDF
            htmlToPdfConverter.HiddenHtmlElements = new string[] { "#convertThisPageButtonDiv" };
            htmlToPdfConverter.SerialNumber = "CEBhWVhs-bkRhanpp-enE5OCY4-KDkoOigw-ODAoOzkm-OTomMTEx-MQ==";

            // render the HTML code as PDF in memory
            //byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(Body, "");

            string path = AppDomain.CurrentDomain.BaseDirectory + "/Uploads/SalarySlips/";

            System.IO.File.WriteAllBytes(System.IO.Path.Combine(path, "SalarySlip-" + wdEnum + "-" + Year + ".pdf"), pdfBuffer);
            SlipGenerated = true;
        }
        return SlipGenerated;
    }


    public static string ConvertDataTableToHTML(DataTable dt)
    {
        string html = "";
        //add header row
        html += "<tr>";
        for (int i = 0; i < dt.Columns.Count - 1; i++)
            html += "<th id='thabc'>" + dt.Columns[i].ColumnName + "</th>";
        html += "</tr>";
        //add rows
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";
            for (int j = 0; j < dt.Columns.Count - 1; j++)
                html += "<td id='tdabc'>" + dt.Rows[i][j].ToString() + "</td>";
            html += "</tr>";
        }
        html += "";
        return html;
    }

    //Previous One
    //private void HtmlTemplateToString(string date, int Month, int Year)
    //{
    //    vt_EMSEntities db = new vt_EMSEntities();
    //    var Data = db.VT_SP_GetEmpSalary_Slip(Convert.ToDateTime(date), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue)).ToList();
    //    if (Data != null && Data.Count > 0)
    //    {
    //        string Body = string.Empty;
    //        //using streamreader for reading my htmltemplate

    //        //using (StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemplate.html")))
    //        using (StreamReader Reader = new StreamReader(Server.MapPath("~/Report/PaySlipHtml.html")))
    //        {
    //            Body = Reader.ReadToEnd();
    //        }
    //        MonthsEnum wdEnum;
    //        //if ()
    //        //{

    //        //}
    //        Enum.TryParse<MonthsEnum>(Month.ToString(), out wdEnum);
    //        Body = Body.Replace("{Date}", wdEnum.ToString() + " " + Year);
    //        Body = Body.Replace("{EmployeeID}", Data[0].EmployeeID.ToString());
    //        Body = Body.Replace("{EmployeeName}", Data[0].EmployeeName);
    //        Body = Body.Replace("{DepartmentID}", Data[0].DepartmentID.ToString());
    //        Body = Body.Replace("{Designation}", Data[0].Position);

    //        Body = Body.Replace("{Basic}", Data[0].BasicSalary.ToString());
    //        Body = Body.Replace("{MedicalAllowance}", Data[0].MedicalAllowance.ToString());
    //        Body = Body.Replace("{HouseRentAllowance}", Data[0].HouseRentAllownce.ToString());
    //        Body = Body.Replace("{TransportAllowance}", Data[0].TransportAllownce.ToString());
    //        Body = Body.Replace("{FuelAllowance}", Data[0].FuelAllowance.ToString());
    //        Body = Body.Replace("{Overtime}", Data[0].Overtime.ToString());
    //        Body = Body.Replace("{Bonus}", Data[0].Bonus.ToString());

    //        double SubTotal = Convert.ToDouble(Data[0].BasicSalary.ToString()) + Convert.ToDouble(Data[0].MedicalAllowance.ToString()) + Convert.ToDouble(Data[0].HouseRentAllownce.ToString()) + Convert.ToDouble(Data[0].TransportAllownce.ToString()) + Convert.ToDouble(Data[0].FuelAllowance.ToString()) + Convert.ToDouble(Data[0].Overtime.ToString()) + Convert.ToDouble(Data[0].Bonus.ToString());
    //        Body = Body.Replace("{SubTotal}", SubTotal.ToString());

    //        Body = Body.Replace("{ProvidentFund}", Data[0].PF.ToString());
    //        Body = Body.Replace("{IncomeTax}", Data[0].Tax.ToString());
    //        Body = Body.Replace("{AdvanceSalary}", Data[0].Advance.ToString());
    //        Body = Body.Replace("{Loan}", Data[0].Loan.ToString());
    //        Body = Body.Replace("{EOBI}", Data[0].EOBI.ToString());
    //        Body = Body.Replace("{LeaveOnJoining}", Data[0].LeaveonJoining.ToString());
    //        // Convert.ToDouble(Data[0].PF.ToString()) + 
    //        double SubTotal2 =Convert.ToDouble(Data[0].Tax.ToString()) + Convert.ToDouble(Data[0].Advance.ToString()) + Convert.ToDouble(Data[0].Loan.ToString()) + Convert.ToDouble(Data[0].EOBI.ToString()) + Convert.ToDouble(Data[0].LeaveonJoining.ToString());
    //        Body = Body.Replace("{SubTotal2}", SubTotal2.ToString());

    //        Body = Body.Replace("{NetPay}", Data[0].NetSalary.ToString());

    //        // instantiate the HiQPdf HTML to PDF converter
    //        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
    //        //htmlToPdfConverter.HtmlLoadedTimeout = 1200;
    //        htmlToPdfConverter.HtmlLoadedTimeout = 9999999;
    //        htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
    //        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
    //        // hide the button in the created PDF
    //        htmlToPdfConverter.HiddenHtmlElements = new string[] { "#convertThisPageButtonDiv" };
    //        htmlToPdfConverter.SerialNumber = "CEBhWVhs-bkRhanpp-enE5OCY4-KDkoOigw-ODAoOzkm-OTomMTEx-MQ==";

    //        // render the HTML code as PDF in memory
    //        //byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);
    //        byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(Body, "");

    //        string path = AppDomain.CurrentDomain.BaseDirectory + "/Uploads/SalarySlips/";

    //        System.IO.File.WriteAllBytes(System.IO.Path.Combine(path, "SalarySlip-" + wdEnum + "-" + Year + ".pdf"), pdfBuffer);
    //    }
    //}

    private void clearFolder(string FolderName)
    {
        DirectoryInfo dir = new DirectoryInfo(FolderName);

        foreach (FileInfo fi in dir.GetFiles())
        {
            fi.Delete();
        }

        foreach (DirectoryInfo di in dir.GetDirectories())
        {
            clearFolder(di.FullName);
            di.Delete();
        }
    }

    private void ZipFolder()
    {
        Response.Clear();
        Response.BufferOutput = false;
        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "attachment; filename=salaryslip.zip");

        using (ZipFile zip = new ZipFile())
        {
            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
            zip.AddDirectory(Server.MapPath("~/Uploads/SalarySlips/"));
            zip.Save(Response.OutputStream);
        }

        Response.Close();
    }

    protected void BtnLog_Click(object sender, EventArgs e)
    {
        //UpDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }

    private void BindLog()
    {
        int EmployeeID = Convert.ToInt32((ddlEmployee.SelectedValue == "" ? "0" : ddlEmployee.SelectedValue));
        vt_EMSEntities db = new vt_EMSEntities();
        var Data = db.vt_tbl_salaryRecords.Where(x => x.EmployeeID == EmployeeID).ToList();
        if (Data.Count == 0)
        {
            GvLog.DataSource = null;

        }
        else
        {
            GvLog.DataSource = Data;

        }
        GvLog.DataBind();
    }

    #endregion Salman Code

    protected void btnYearlySlip_Click(object sender, EventArgs e)
    {

        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        if (Companyid != 0)
        {
            if (HtmlTemplateToString_Zip())
            {
                ZipFolder();
                clearFolder(Server.MapPath("~/Uploads/SalarySlips/"));
            }
            else { MsgBox.Show(this.Page, MsgBox.warning, "", "No Record found"); }
        }
        else
        {
            MsgBox.Show(this.Page, MsgBox.danger, "", "Please Select Company First from DashBoard");
        }

    }
}




internal enum MonthsEnum
{
    Jan = 1,
    Feb = 2,
    Mar = 3,
    Apr = 4,
    May = 5,
    Jun = 6,
    Jul = 7,
    Aug = 8,
    Sep = 9,
    Oct = 10,
    Nov = 11,
    Dec = 12
}