using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.ComponentModel;
using System.Data.Objects;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

public partial class ExportExcel : System.Web.UI.Page
{
    public static int CompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyID = Convert.ToInt32(Session["CompanyId"]);

    }


    //Load Data
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load()
    {
        string List;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //For CompanyID
                var Query = db.Vt_Sp_GetAttendance_By_CompanyID_OR_EmployeeID(CompanyID, 0,null).ToList();
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



    protected void btnExcportExcel_Click(object sender, EventArgs e)
    {
        //ExcelExport();
        ReadExcel();
        // ExcelExport();
    }
    //public static void ReadExcel_2()
    //{
    //    //HSSFWorkbook hssfwb;
    //    XSSFWorkbook hssfwb;
    //    using (FileStream file = new FileStream(@"D:\parvaiz hussain\tbL_Employee.xlsx", FileMode.Open, FileAccess.Read))
    //    {

    //        //hssfwb = new HSSFWorkbook(file);
    //        hssfwb = new XSSFWorkbook(file); //XSSFWorkBook will read 2007 Excel format  

    //    }
    //    int i = 0;
    //    ISheet sheet = hssfwb.GetSheetAt(0);

    //    int columncount = sheet.GetRow(1).LastCellNum;
    //    for (int row = 2; row <= sheet.LastRowNum; row++)
    //    {
    //        if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
    //        {
    //            if (!String.IsNullOrEmpty(sheet.GetRow(row).GetCell(1).StringCellValue))
    //            {

    //                string test = sheet.GetRow(row).GetCell(1).NumericCellValue.ToString();

    //            }

    //        }
    //        i++;
    //    }
    //}
    //public DataTable GetdesignationByCompanyID()
    //{
    //    vt_EMSEntities db = new vt_EMSEntities();
    //    var objdesignation = db.VT_SP_Getdesignation_ByCompanyID(Convert.ToInt32(Session["CompanyId"]));
    //    List<VT_SP_Getdesignation_ByCompanyID_Result> result = objdesignation.ToList();
    //    DataTable Dt = vt_Common.ConvertToDataTable(result);
    //    return Dt;
    //}

    //public DataTable GetDepartment_ByCompanyID()
    //{
    //    vt_EMSEntities db = new vt_EMSEntities();
    //    var objdepartment = db.VT_SP_GetDepartment_ByCompanyID(Convert.ToInt32(Session["CompanyId"]));
    //    List<VT_SP_GetDepartment_ByCompanyID_Result> result = objdepartment.ToList();
    //    DataTable Dt = vt_Common.ConvertToDataTable(result);
    //    return Dt;
    //}

    public DataTable ReadExcel()
    {

        DataTable dt = new DataTable();
        try
        {
            if (FileUploadExcel.HasFile)
            {
                string extension = Path.GetExtension(FileUploadExcel.PostedFile.FileName);
                string filename = Path.GetFullPath(FileUploadExcel.FileName);
                IWorkbook workbook = null;
                //Stream uploadFileStream = FileUpload1.PostedFile.InputStream;
                HttpPostedFile file = Request.Files[0];
                MemoryStream mem = new MemoryStream();
                mem.SetLength((int)file.ContentLength);
                file.InputStream.Read(mem.GetBuffer(), 0, (int)file.ContentLength);
                //using (MemoryStream file= new MemoryStream())
                //{
                if (extension == ".xlsx")
                {
                    workbook = new XSSFWorkbook(mem);
                }
                else if (extension == ".xls")
                {
                    workbook = new XSSFWorkbook(mem);
                }
                else
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "!This format is not supported");
                }
                //}
                //IWorkbook workbook = WorkbookFactory.Create(uploadFileStream);
                ISheet sheet = workbook.GetSheetAt(0);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    dt.Columns.Add(cell.ToString());
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = dt.NewRow();
                    if (row == null)
                    {
                        break;
                    }
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    dt.Rows.Add(dataRow);
                }
                string sqlConnectionString = vt_Common.PayRollConnectionString;



                // Bulk Copy to SQL Server 

                using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
                {
                    bcp.BatchSize = 100000;
                    bcp.DestinationTableName = "vt_tbl_Attendance";

                  //  bcp.ColumnMappings.Add("EmployeeID", "EmployeeID");
                    bcp.ColumnMappings.Add("EnrollId", "EnrollId");
                    bcp.ColumnMappings.Add("ShiftID", "ShiftID");
                    bcp.ColumnMappings.Add("Date", "Date");
                    bcp.ColumnMappings.Add("InTime", "InTime");
                    bcp.ColumnMappings.Add("OutTime", "OutTime");
                    bcp.ColumnMappings.Add("Late", "Late");
                    bcp.ColumnMappings.Add("Early", "Early");
                    bcp.ColumnMappings.Add("OT", "OT");
                    bcp.ColumnMappings.Add("TotalHrs", "TotalHrs");
                    bcp.ColumnMappings.Add("Status", "Status");
                    bcp.ColumnMappings.Add("Type", "Type");
                    bcp.ColumnMappings.Add("Day", "Day");
                    bcp.ColumnMappings.Add("OutNextDay", "OutNextDay");
                    bcp.ColumnMappings.Add("LunchMin", "LunchMin");
                    bcp.ColumnMappings.Add("LateStatus", "LateStatus");
                    bcp.ColumnMappings.Add("LunchIn", "LunchIn");
                    bcp.ColumnMappings.Add("LunchOut", "LunchOut");
                    bcp.ColumnMappings.Add("BreakIn", "BreakIn");
                    bcp.ColumnMappings.Add("BreakOut", "BreakOut");
                    bcp.ColumnMappings.Add("LeaveDay", "LeaveDay");
                    bcp.ColumnMappings.Add("GatePassHrs", "GatePassHrs");
                    bcp.DestinationTableName = "vt_tbl_Attendance";
                    bcp.BulkCopyTimeout = 0;
                    bcp.WriteToServer(dt);
                    bcp.Close();
                    MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Added");

                }

                return dt;

            }

            else
            {
                return null;
            }
            }

        catch (Exception ex)
        {
            return null;
        }
    }



    //void ExcelExport()
    //{
    //    if (FileUploadExcel.HasFile)

    //    {

    //        try

    //        {

    //            string path = string.Concat(Server.MapPath("~/Excelfiles/" + FileUploadExcel.FileName));

    //            FileUploadExcel.SaveAs(path);


    //            // Connection String to Excel Workbook

    //            string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);

    //            OleDbConnection connection = new OleDbConnection();

    //            connection.ConnectionString = excelConnectionString;

    //            OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);

    //            connection.Open();

    //            // Create DbDataReader to Data Worksheet

    //            OleDbDataReader dr = command.ExecuteReader();



    //            // SQL Server Connection String

    //            string sqlConnectionString = @"Data Source=192.168.0.48\SQL2014;initial catalog=vt_EMS_PhaseII;user id=SA;password=viftech;MultipleActiveResultSets=True;";



    //            // Bulk Copy to SQL Server 

    //            SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);

    //            bulkInsert.DestinationTableName = "vt_tbl_Attendance";

    //            bulkInsert.WriteToServer(dr);
    //            connection.Close();


    //        }

    //        catch (Exception ex)

    //        {



    //        }

    //    }
    //}



}