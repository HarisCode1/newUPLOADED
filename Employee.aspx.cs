    using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employee : System.Web.UI.Page
{
    int Count = 0;
    public static int CompanyID = 0;
    DataTable Dt = new DataTable();
    public vt_EMSEntities db = new vt_EMSEntities();
    
    User_BAL BAL = new User_BAL();
    vt_tbl_Employee emp = new vt_tbl_Employee();    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
           // Fill();
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (!Page.IsPostBack)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = ProcedureCall.VT_SP_GetEmployee_ByID(5132).Tables[0];
               
                

                #region IsAuthenticated To Page
                int UserID = Convert.ToInt32(Session["UserId"]);
                int RoleId = Convert.ToInt32(Session["RoleId"]);
                if (RoleId ==1)
                {
                    chechempright.Visible = false;
                }
                string UserName = Session["UserName"].ToString();
                string PageUrl = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                DataTable Dt = Session["PagePermissions"] as DataTable;
                if (!vt_Common.IsAuthenticated(PageUrl, UserName, Dt))
                {
                    Response.Redirect("Default.aspx");
                }
                
                var query = db.vt_tbl_User.Where(x => x.UserId == UserID).ToList();
                if (query.Count > 0 || UserName == "SuperAdmin")
                {
                    //BtnAddNew.Visible = false;
                }
                #endregion
            }
        }
    }

    #region Tuaha Code

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load()
    {
        string List = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {


                // Page Page = HttpContext.Current.Handler as Page;
                //var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                //var Query = db.VT_SP_GetEmployees(CompanyID).ToList();
                if (CompanyID == 0)
                {
                    // MsgBox.Show(Page, MsgBox.danger, "", "Please Select Company First from dashboard");
                    //HttpContext.Current.Response.Write("<script>alert('Please Select Company First from dashboard');</script>");
                }
                else
                {

                    DataTable dt = new DataTable();
                    dt= ProcedureCall.Get_SP_GetEmployees(CompanyID).Tables[0];/// db.VT_SP_GetEmployees(CompanyID == 0 ? 0 : CompanyID).ToList();
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
        //Previous One
        //string List;
        //try
        //{
        //    using (vt_EMSEntities db = new vt_EMSEntities())
        //    {
        //        //var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
        //        //var Query = db.VT_SP_GetEmployees(CompanyID).ToList();
        //        var Query = db.VT_SP_GetEmployees(CompanyID == 0 ? 0 : CompanyID).ToList();
        //        List = JsonConvert.SerializeObject(Query);

        //    }


        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.TryCatchException(ex);
        //    throw ex;
        //}
        //return List;        
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool Terminate(int EmployeeID, string Reason,string date)
    {

        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_terminatedemployees termEmp = new vt_tbl_terminatedemployees();
        vt_tbl_Employee Obj = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmployeeID).FirstOrDefault();
        if (Obj != null)
        {

            Obj.JobStatus = "InActive";
            db.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            termEmp.EmployeeID = EmployeeID;
            termEmp.CompanyID = CompanyID;
            termEmp.Reason = Reason;
            termEmp.Status = true;
            termEmp.TerminatedDate = Convert.ToDateTime(date);
            // db.vt_tbl_terminatedemployees.Add(termEmp);
            //  db.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }

    }
    #endregion
    #region Bulk Upload
    protected void Btnexcelimport_Click(object sender, EventArgs e)
    {
        #region Comment
        // List<VT_SP_GetDepartment_ByCompanyID_Result> Departlist = new List<VT_SP_GetDepartment_ByCompanyID_Result>();

        //  Departlist = GetDepartment_ByCompanyID(Convert.ToInt32(Session["CompanyId"]));



        //   GETDepartID(Departdt, dt);
        #endregion
        if (FileExportExcel.HasFile)
        {
            Count++;
            if (Count > 0)
            {
                Btnexcelimport.Enabled = false;
            }
            ReadExcel();
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "Please upload a file");
        }

    }

    public DataTable ReadExcel()
    {
        DataTable dt = new DataTable();
        //try
        //{
        string extension = Path.GetExtension(FileExportExcel.PostedFile.FileName);
        string filename = Path.GetFullPath(FileExportExcel.FileName);
        string folderPath = AppDomain.CurrentDomain.BaseDirectory; // Your path Where you want to save other than Server.MapPath
                                                                   //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileExportExcel.SaveAs(folderPath + Path.GetFileName(FileExportExcel.FileName));

        string connString = "";
        if (extension == ".xlsx")
        {
            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + folderPath + Path.GetFileName(FileExportExcel.FileName) + ";Extended Properties=Excel 12.0";
            //workbook = new XSSFWorkbook(mem);

        }
        else if (extension == ".xls")
        {
            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + folderPath + Path.GetFileName(FileExportExcel.FileName) + ";Extended Properties=Excel 8.0";
            //workbook = new XSSFWorkbook(mem);
        }
        else
        {
            throw new Exception("This format is not supported");
        }

        OleDbConnection oledbConn = new OleDbConnection(connString);
        try
        {
            // Open connection
            oledbConn.Open();

            // Create OleDbCommand object and select data from worksheet Sheet1
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

            // Create new OleDbDataAdapter
            OleDbDataAdapter oleda = new OleDbDataAdapter();

            oleda.SelectCommand = cmd;

            DataSet ds = new DataSet();
            // Fill the DataSet from the data extracted from the worksheet.
            oleda.Fill(ds, "Employees");
            dt = ds.Tables[0];

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            // Close connection
            oledbConn.Close();
            File.Delete(folderPath + Path.GetFileName(FileExportExcel.FileName));
        }
        //dt.Columns.Add("EmployeeName");
        //dt.Columns.Add("EmpPassword");
        List<int> errIndexes = new List<int>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //string companyName = dt.Rows[i]["CompanyID"].ToString();
            string DepartName = dt.Rows[i]["DepartmentID"].ToString();
            string DesignationName = dt.Rows[i]["DesignationID"].ToString();
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int compID = Convert.ToInt32(Session["CompanyId"]);
                dt.Rows[i]["CompanyID"] = compID;               
                if (dt.Rows[i]["DOB"].ToString() == "")
                {
                    dt.Rows[i]["DOB"] = DateTime.Now.Date.ToString("MM/dd/yyyy");
                }
                if (dt.Rows[i]["JoiningDate"].ToString() == "")
                {
                    dt.Rows[i]["JoiningDate"] = DateTime.Now.Date.ToString("MM/dd/yyyy");
                }
                string code = (Get_EmployeeCode() + i).ToString();
                //dt.Rows[i]["EmployeeName"] = (dt.Rows[i]["FirstName"].ToString() + code).Trim().Replace(" ", string.Empty);
              //  dt.Rows[i]["EmpPassword"] = vt_Common.Encrypt((dt.Rows[i]["FirstName"].ToString() + code).Trim().Replace(" ", string.Empty));
                dt.Rows[i]["EnrollId"] = (code).ToString();
            }
        }        
        dt.CaseSensitive = false;

        try
        {
            string sqlConnectionString = vt_Common.PayRollConnectionString;
            using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
            {
                bcp.BatchSize = 100000;
                bcp.DestinationTableName = "vt_tbl_Employee";

                // bcp.ColumnMappings.Add("EmployeeID", "EmployeeID");
                bcp.ColumnMappings.Add("CompanyID", "CompanyID");

                // bcp.ColumnMappings.Add("BranchID", "BranchID");
                //bcp.ColumnMappings.Add("DepartmentID", "DepartmentID");
                //bcp.ColumnMappings.Add("DesignationID", "DesignationID");
                bcp.ColumnMappings.Add("EnrollId", "EnrollId");
                bcp.ColumnMappings.Add("FirstName", "FirstName");
                bcp.ColumnMappings.Add("LastName", "LastName");
                bcp.ColumnMappings.Add("PFType", "PFType");
                bcp.ColumnMappings.Add("RoleID", "RoleID");
                bcp.ColumnMappings.Add("BloodGroup", "BloodGroup");
                bcp.ColumnMappings.Add("Sex", "Sex");
                bcp.ColumnMappings.Add("DOB", "DOB");
                bcp.ColumnMappings.Add("MartialStatus", "MartialStatus");
                bcp.ColumnMappings.Add("Phone", "Phone");
                bcp.ColumnMappings.Add("Email", "Email");
                bcp.ColumnMappings.Add("CurrentAddress", "CurrentAddress");
                bcp.ColumnMappings.Add("JoiningDate", "JoiningDate");
                bcp.ColumnMappings.Add("ConfirmationDate", "ConfirmationDate");
                bcp.ColumnMappings.Add("JobStatus", "JobStatus");
                bcp.ColumnMappings.Add("BasicSalary", "BasicSalary");
                bcp.ColumnMappings.Add("HouseRentAllownce", "HouseRentAllownce");
                bcp.ColumnMappings.Add("TransportAllownce", "TransportAllownce");
                bcp.ColumnMappings.Add("MedicalAllowance", "MedicalAllowance");
                bcp.ColumnMappings.Add("FuelAllowance", "FuelAllowance");
                bcp.ColumnMappings.Add("ProvidentFund", "ProvidentFund");
                bcp.ColumnMappings.Add("SpecialAllowance", "SpecialAllowance");
                bcp.ColumnMappings.Add("FromBank", "FromBank");
                bcp.ColumnMappings.Add("FromBranch", "FromBranch");
                bcp.ColumnMappings.Add("ToBank", "ToBank");
                bcp.ColumnMappings.Add("ToBranch", "ToBranch");
                bcp.ColumnMappings.Add("AccountNo", "AccountNo");
                // bcp.ColumnMappings.Add("EmployeeName", "EmployeeName");
                //bcp.ColumnMappings.Add("EmpPassword", "EmpPassword");

                bcp.BulkCopyTimeout = 0;

                bcp.WriteToServer(dt);
                bcp.Close();
                MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Added");
            }

        }
        catch (SqlException esql)
        {

            Console.WriteLine(esql.Message);
        }

      

        //}
        //catch (Exception ex)
        //{

        //    throw;
        //}
        return dt;
    }


    //protected void Btnexcelimport_Click(object sender, EventArgs e)
    //{
    //    int CompanyID = Convert.ToInt32(Session["CompanyId"]);
    //    if (FileExportExcel.HasFile)
    //    {
    //        //Count++;
    //        //if (Count > 0)
    //        //{
    //            String Companyid;
    //            String DepartmentID;
    //            String DesignationID;
    //            String EnrollId;
    //            String FirstName;
    //            String LastName;
    //            String RoleID;
    //            String Sex;
    //            String DOB;
    //            String MartialStatus;
    //            String Phone;
    //            String Email;
    //            String CurrentAddress;
    //            String Current_City;
    //            String Current_State;
    //            String Current_Zip;
    //            String Current_Country;
    //            String JoiningDate;
    //            String JobStatus;
    //            String BasicSalary;
    //            String HouseRentAllownce;
    //            String TransportAllownce;
    //            String MedicalAllowance;
    //            String FuelAllowance;
    //            String FromBank;
    //            String FromBranch; 
    //             String ToBank;
    //            String ToBranch;
    //            String AccountNo;
    //            string path = Path.GetFileName(FileExportExcel.FileName);
    //        //string getpath = Server.MapPath("~/ExcelFile/") + path;
    //        //if (System.IO.File.Exists(getpath))
    //        //{
    //        //    File.Delete(getpath);
    //        //}
    //        //string path2 = Path.GetFileName(FileExportExcel.FileName);

    //        //Random rand = new Random((int)DateTime.Now.Ticks);
    //        //int RandomNumber;
    //        //RandomNumber = rand.Next(100000, 999999);

    //        FileExportExcel.SaveAs(Server.MapPath("~/ExcelFile/")  + path);
    //        String ExcelPath = Server.MapPath("~/ExcelFile/")+ path;
    //        OleDbConnection mycon = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + ExcelPath + "; Extended Properties=Excel 8.0; Persist Security Info = False");
    //        try
    //        {
    //            mycon.Open();
    //            OleDbCommand cmd = new OleDbCommand("select * from [Sheet$]", mycon);
    //            OleDbDataReader dr = cmd.ExecuteReader();
    //            while (dr.Read())
    //            {
    //                // Response.Write("<br/>"+dr[0].ToString());
    //                Companyid = dr[0].ToString();
    //                DepartmentID = dr[1].ToString();
    //                DesignationID = dr[2].ToString();
    //                EnrollId = dr[3].ToString();
    //                FirstName = dr[4].ToString();
    //                LastName = dr[5].ToString();
    //                RoleID = dr[6].ToString();
    //                Sex = dr[7].ToString();
    //                DOB = dr[8].ToString();
    //                MartialStatus = dr[9].ToString();
    //                Phone = dr[10].ToString();
    //                Email = dr[11].ToString();
    //                CurrentAddress = dr[12].ToString();
    //                Current_City = dr[13].ToString();
    //                Current_State = dr[14].ToString();
    //                Current_Zip = dr[15].ToString();
    //                Current_Country = dr[16].ToString();
    //                JoiningDate = dr[17].ToString();
    //                JobStatus = dr[18].ToString();
    //                BasicSalary = dr[19].ToString();
    //                HouseRentAllownce = dr[20].ToString();
    //                TransportAllownce = dr[21].ToString();
    //                MedicalAllowance = dr[22].ToString();
    //                FuelAllowance = dr[23].ToString();
    //                FromBank = dr[24].ToString();
    //                FromBranch = dr[25].ToString();
    //                ToBank = dr[26].ToString();
    //                ToBranch = dr[27].ToString();
    //                AccountNo = dr[28].ToString();
    //                savedata(CompanyID, DepartmentID, DesignationID, EnrollId, FirstName, LastName, RoleID, Sex, DOB, MartialStatus, Phone, Email, CurrentAddress, Current_City, Current_State, Current_Zip, Current_Country, JoiningDate, JobStatus, BasicSalary, HouseRentAllownce, TransportAllownce, MedicalAllowance, FuelAllowance, FromBank, FromBranch, ToBank, ToBranch, AccountNo);

    //            }

    //        }
    //        catch (Exception ex)
    //        {

    //            throw;
    //        }
    //        finally
    //        {
    //            mycon.Close();
    //        }
    //        string getpath = Server.MapPath("~/ExcelFile/") + path;
    //        File.Delete(getpath);

    //        //if ((System.IO.File.Exists(path)))
    //        //{
    //        //    System.IO.File.Delete(path);
    //        //}
    //        //}
    //        //Btnexcelimport.Enabled = false;

    //        //ReadExcel();
    //    }
    //    else
    //    {
    //        MsgBox.Show(Page, MsgBox.danger, "", "Please upload a file");
    //    }

    //}
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
    private void savedata(int CompanyID,string DepartmentID, string DesignationID, string EnrollId, string FirstName, string LastName, string RoleID, string Sex, string DOB, string MartialStatus, string Phone, string Email, string CurrentAddress, string Current_City, string Current_State, string Current_Zip, string Current_Country, string JoiningDate, string JobStatus, string BasicSalary, string HouseRentAllownce, string TransportAllownce, string MedicalAllowance,string FuelAllowance, string FromBank, string FromBranch, string ToBank, string ToBranch, string AccountNo)
    {
        try
        {
            vt_tbl_Employee emp = new vt_tbl_Employee();
            var checkenrollid = db.vt_tbl_Employee.Where(x => x.EnrollId == EnrollId).FirstOrDefault();
            if (checkenrollid != null)
            {
                MsgBox.Show(Page, MsgBox.danger, "", "Change Enroll Id from Excel Sheet Because It Already Exist");
            }
            else
            {
                //Get DepartementId
                int departmentid = 0;
                vt_tbl_Department dept = db.vt_tbl_Department.Where(x => x.Department == DepartmentID).FirstOrDefault();
                departmentid = dept.DepartmentID;
                //Get Designmation Id
                int designationid = 0;
                vt_tbl_Designation desig = db.vt_tbl_Designation.Where(x => x.Designation == DesignationID).FirstOrDefault();
                designationid = desig.DesignationID;

                emp.CompanyID = CompanyID;
                emp.DepartmentID = departmentid;
                emp.DesignationID = designationid;
             //   emp.EnrollId = EnrollId;
                emp.FirstName = FirstName;
                emp.LastName = LastName;
                emp.RoleID = Convert.ToInt32(RoleID);
                emp.Sex = Sex;
                emp.DOB = DateTime.Parse(DOB);
                emp.MartialStatus = MartialStatus;
                emp.Phone = Phone;
                emp.Email = Email;
                emp.CurrentAddress = CurrentAddress;
                emp.Current_City = Current_City;
                emp.Current_State = Current_State;
                emp.Current_Zip = Current_Zip;
                emp.Current_Country = Current_Country;
                emp.JoiningDate = DateTime.Now;
                emp.JobStatus = JobStatus;
                emp.BasicSalary = Convert.ToDecimal(BasicSalary);
                emp.HouseRentAllownce = Convert.ToDecimal(HouseRentAllownce);
                emp.TransportAllownce = Convert.ToDecimal(TransportAllownce);
                emp.MedicalAllowance = Convert.ToDecimal(MedicalAllowance);
                emp.FuelAllowance = Convert.ToDecimal(FuelAllowance);
                emp.FromBank = FromBank;
                emp.ToBank = ToBank;
                emp.ToBranch = ToBranch;
                emp.AccountNo = AccountNo;
                db.vt_tbl_Employee.Add(emp);
                db.SaveChanges();
                MsgBox.Show(Page, MsgBox.success, "", "Successfully Imported");
            }
        



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

    #endregion

    //void Fill()
    //{
    //    DataSet Ds = ProcedureCall.SpCall_Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID(1122);
    //}



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string   SetDefaultPassword(int EmployeeID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        User_BAL BAL = new User_BAL();
        vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == EmployeeID);
        if (emp == null)
        {
            return "Employee not found";
        }
        string email =emp.Email;
        var encryptedPassword = vt_Common.Encrypt("123456@");
        int ModifiedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
        vt_tbl_User user = db.vt_tbl_User.FirstOrDefault(x => x.Email == email && x.Active == true);


        if (user == null)
        {
            return "User not found";
        }
        DataSet dt = ProcedureCall.Sp_SetUserDefaultPass(CompanyID, ModifiedBy, EmployeeID, encryptedPassword);
        //var userbal = new User_BAL();
        //userbal.UserID = user.UserId;
        //userbal.Active = user.Active;
        //userbal.Password = encryptedPassword;
        //userbal.UserName = user.UserName;
        //userbal.FirstName = user.FirstName;
        //userbal.LastName = user.LastName;
        //userbal.Email = user.Email;
        //userbal.RoleID = user.RoleId;
        //userbal.CompanyID = user.CompanyId;
        //userbal.UpdatedBy = EmployeeID;
        //userbal.UpdatedOn = DateTime.Now;
        //BAL.UpdateById(userbal);

        string message = "success";
        return message;
       
    }






    [WebMethod]
    public static bool CheckRights(string data)
    {
        int ID = Convert.ToInt32(data);
        bool ispermit = false;
        //int RoleId = Convert.ToInt32(HttpContext.Current.Session["RoleId"]);
        string name = HttpContext.Current.Session["UserName"].ToString();
        if ((string)HttpContext.Current.Session["UserName"] == "SuperAdmin")
        {
            ispermit = false;

            // vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
        }
        else
        {

            DataTable Dt = HttpContext.Current.Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Employee.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    ispermit = true;
                    //  HttpContext.Current.Response.Redirect("~/Employes_Edit.aspx?ID=" + ID);
                }
                else if (Row["PageUrl"].ToString() == "Employee.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    ispermit = false;
                    //HttpContext.Current.Response.Write("<script language='javascript'>window.alert('Your Message');window.location='Employee.aspx';</script>");
                    //MsgBox.Show(Employee., MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }
        return ispermit;
    }
    [WebMethod]
    public static bool CheckRightsofDelete(string data)
    {
        int ID = Convert.ToInt32(data);

        bool ispermit = false;
        string name = HttpContext.Current.Session["UserName"].ToString();
        if ((string)HttpContext.Current.Session["UserName"] == "SuperAdmin")
        {


            // vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
        }
        else
        {

            DataTable Dt = HttpContext.Current.Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Employee.aspx" && Row["Can_Delete"].ToString() == "True")
                {
                    ispermit = true;
                    //  HttpContext.Current.Response.Redirect("~/Employes_Edit.aspx?ID=" + ID);
                }
                else if (Row["PageUrl"].ToString() == "Employee.aspx" && Row["Can_Delete"].ToString() == "False")
                {
                    ispermit = false;
                    //HttpContext.Current.Response.Write("<script language='javascript'>window.alert('Your Message');window.location='Employee.aspx';</script>");
                    //MsgBox.Show(Employee., MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }
        return ispermit;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int EmployeeID =Convert.ToInt32(hdnempid.Value);
            string reason = txtreason.Text;
            // string date = txtFromDate.Text;
            if (UploadDocImage.HasFile)
            {
                string Extenion = System.IO.Path.GetExtension(UploadDocImage.PostedFile.FileName).ToString().ToLower();

                UploadDocImage.SaveAs(Server.MapPath("/images/TerminatedEmployees/" + EmployeeID + "-" + UploadDocImage.PostedFile.FileName));

            }
            vt_EMSEntities db = new vt_EMSEntities();
            vt_tbl_terminatedemployees termEmp = new vt_tbl_terminatedemployees();
            vt_tbl_Employee Obj = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmployeeID).FirstOrDefault();
            if (Obj != null)
            {

                Obj.JobStatus = "InActive";
                db.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                termEmp.EmployeeID = EmployeeID;
                termEmp.CompanyID = CompanyID;
                termEmp.Reason = reason;
                if (UploadDocImage.HasFile)
                {
                    termEmp.Documents = "/images/TerminatedEmployees/" + EmployeeID + "-"+ UploadDocImage.PostedFile.FileName;

                }
                else

                {
                    termEmp.Documents = null;
                }
                  
              
                termEmp.Status = true;
                termEmp.TerminatedDate = Convert.ToDateTime(txtEntryDate.Text);
                db.vt_tbl_terminatedemployees.Add(termEmp);
                db.SaveChanges();

            }
            }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {

            int EmpId = 0;
            EmpId =hdnempid2.Value == "" ? 0: Convert.ToInt32(hdnempid2.Value);
            int Userid = Convert.ToInt32(Session["UserId"]);
            if (EmpId != 0)
            {
               DataSet dt = ProcedureCall.Vt_Sp_DeleteEmployeePermanent(EmpId, Userid);
                vt_Common.ReloadJS(this.Page, " $('#Delete').modal('hide');");
                //vt_Common.ReloadJS(Page, "msgbox(1,' ' ,'Succesfully Done'); setTimeout(function(){window.location.href='Employee.aspx?ID=" + ID + "';},1000)");
                vt_Common.ReloadJS(this.Page, "msgbox(1,' ' ,'Succesfully Done'); setTimeout(function(){ window.location.href = 'Employee.aspx' }, 1000)");
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }


    


    [WebMethod]
    public static string GetPersonalEmail(string data)
    {
        string List = string.Empty;
        int ID = 0;
        if (data != "")
        {
            ID = Convert.ToInt32(data);
        }

        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var qry = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
            if (qry != null)
            {
                List = qry.Current_Address3;
            }
        }

        return List;
    }
    protected void btnPEmail_Click(object sender, EventArgs e)
    {
        try
        {
            int EmpId = 0;
            EmpId = Convert.ToInt32(hdnempid.Value);
            if (EmpId > 0)
            {
                vt_tbl_Employee emp = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmpId).FirstOrDefault();
                emp.Current_Address3 = txtPersonaEmail.Text;//Current_Address3 is used for personal email
                db.SaveChanges();
                txtPersonaEmail.Text = "";
                vt_Common.ReloadJS(this.Page, "$('#Email-Modal').modal('hide')");
                vt_Common.ReloadJS(this.Page, "$(document).ready(function(){ setInterval(function () {window.location.href = 'Employee.aspx';}, 1000);})");
                MsgBox.Show(Page, MsgBox.success, "", "Successfully save");
                



            }
        }
        catch (Exception ex)
        {


        }
    }

   
}