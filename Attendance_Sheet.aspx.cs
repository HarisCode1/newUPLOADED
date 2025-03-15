using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Viftech;

public partial class Attendance_Sheet : System.Web.UI.Page
{
    private int Count = 0;
    vt_EMSEntities db = new vt_EMSEntities();
    int UseriD = 0;
    int ShiftID = 0;
    private vt_tbl_Attendance ObjAttendance = new vt_tbl_Attendance();
    public static int CompanyID = 0;
    public static int EmployeeID = 0;
    public static string Role = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                CompanyID = Convert.ToInt32(Session["CompanyId"]);
                BindEmployeeGrid(CompanyID);
            }
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            EmployeeID = Convert.ToInt32(Session["UserId"]);
            Role = Session["Role"] == null ? "" : Session["Role"].ToString();
        }
    }

    protected void btnExcportExcel_Click(object sender, EventArgs e)
    {
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

 
    public bool addTempAttendance()
    {
        try
        {
            //  var query = ProcedureCall.GetAttendance();
          
             UseriD= Convert.ToInt32(Session["UserId"]); 
            vt_tbl_SheetRecord sr = new vt_tbl_SheetRecord();
            DataTable dtt;
            if (FileExportExcel.HasFile)
            {
                string extension = Path.GetExtension(FileExportExcel.PostedFile.FileName);
                string filename = Path.GetFullPath(FileExportExcel.FileName);
                string format = DateTime.Now.ToString("ddmmyyyyHHmmss");
                string folderPath = "~/Sheet";// AppDomain.CurrentDomain.BaseDirectory + "Sheet";
              // + fromat;
                if (extension == ".csv")
                {
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(Server.MapPath(folderPath));
                    }
                    string fullpath = "~/Sheet/" + format + Path.GetFileName(FileExportExcel.FileName);
                    FileExportExcel.SaveAs(MapPath(fullpath));
                    
                    dtt = ConvertCSVtoDataTable(MapPath(fullpath), true);

                    string sqlConnectionString = vt_Common.PayRollConnectionString;

                    sr.SheetName = format + Path.GetFileName(FileExportExcel.FileName);
                    sr.SheetUrl = fullpath;
                    sr.CompanyID = CompanyID;
                    sr.UserID = UseriD;
                    sr.CreatedDate = DateTime.Now;
                   // db.vt_tbl_SheetRecord.Add(sr);
                    db.SaveChanges();
                    ShiftID = sr.SheetID;
                    if (ShiftID > 0)
                    {
                        // Bulk Copy to SQL Server
                        using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
                        {
                            bcp.BatchSize = 100000;
                            bcp.DestinationTableName = "tbl_TempAttendance";
                            bcp.ColumnMappings.Add("EmpID", "EmpID");
                            bcp.ColumnMappings.Add("Name", "Name");
                            bcp.ColumnMappings.Add("Time", "Time");
                            bcp.ColumnMappings.Add("WorkCode", "WorkCode");
                            bcp.ColumnMappings.Add("WorkState", "WorkState");
                            bcp.ColumnMappings.Add("TerminalName", "TerminalName");
                            bcp.BulkCopyTimeout = 0;
                            bcp.WriteToServer(dtt);
                            bcp.Close();
                        }
                        //  return true;

                    }

                }
                else
                {
                    throw new Exception("This format is not supported");
                }





                //for temp attendance bulk import through sp
                DataTable dt;
                if (FileExportExcel.HasFile)
                {
                    //string extension = Path.GetExtension(FileExportExcel.PostedFile.FileName);
                    //string filename = Path.GetFullPath(FileExportExcel.FileName);
                    //string folderPath = AppDomain.CurrentDomain.BaseDirectory;
                    if (extension == ".csv")
                    {
                        //if (!Directory.Exists(folderPath))
                        //{
                        //    Directory.CreateDirectory(folderPath);
                        //}
                        //FileExportExcel.SaveAs(folderPath + Path.GetFileName(FileExportExcel.FileName));
                        //dt = ConvertCSVtoDataTable(folderPath + Path.GetFileName(FileExportExcel.FileName), true);
                        dt = ProcedureCall.GetAttendance().Tables[0];
                        if (dt.Rows.Count > 0)

                        {
                            string sqlConnectionString = vt_Common.PayRollConnectionString;
                            // Bulk Copy to SQL Server
                            using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
                            {
                                bcp.BatchSize = 100000;
                                bcp.DestinationTableName = "vt_tbl_TempAtt";

                                bcp.ColumnMappings.Add("EmpID", "EmpID");
                                bcp.ColumnMappings.Add("Name", "Name");
                                bcp.ColumnMappings.Add("Time", "Time");
                                bcp.ColumnMappings.Add("WorkCode", "WorkCode");
                                bcp.ColumnMappings.Add("AttendanceState", "WorkState");
                                bcp.ColumnMappings.Add("DeviceName", "TerminalName");
                                bcp.BulkCopyTimeout = 0;
                                bcp.WriteToServer(dt);
                                bcp.Close();

                            }
                 
                        }
                        return true;
                    }
                    else
                    {
                        throw new Exception("This format is not supported");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return false;
    }
    public DataTable ConvertCSVtoDataTable(string strFilePath, bool removeHeaderSpaces = false)
    {
        StreamReader sr = new StreamReader(strFilePath);
        string[] headers = sr.ReadLine().Split(',');
        DataTable dt = new DataTable();
        foreach (string header in headers)
        {
            if (removeHeaderSpaces)
            {
                dt.Columns.Add(header.Replace(" ", string.Empty));
            }
            else {
                dt.Columns.Add(header);
            }
        }
        while (!sr.EndOfStream)
        {
            string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            DataRow dr = dt.NewRow();
            for (int i = 0; i < headers.Length; i++)
            {
                dr[i] = rows[i];
            }
            dt.Rows.Add(dr);
        }
        sr.Close();
        return dt;
    }

    public DataTable ReadExcel()
    {
     addTempAttendance();
        //DataTable dt = ProcedureCall.vt_AttendanceAdd().Tables[0]; Comment for testing
        DataTable dt = ProcedureCall.vt_AttendanceAddByTemptable().Tables[0];
        DataColumn[] sd = { new DataColumn("CompanyId"), new DataColumn("ShiftID"), new DataColumn("Late"), new DataColumn("Early"), new DataColumn("OT"), new DataColumn("TotalHrs"), new DataColumn("Status"), new DataColumn("Type"), new DataColumn("Day"),
                            new DataColumn("OutNextDay"), new DataColumn("LunchMin"), new DataColumn("LateStatus"), new DataColumn("LunchIn"), new DataColumn("LunchOut"), new DataColumn("BreakIn"), new DataColumn("BreakOut"), new DataColumn("LeaveDay"), new DataColumn("GatePassHrs")};
        dt.Columns.AddRange(sd);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["CompanyId"] = Convert.ToInt32(Session["CompanyId"]);
            dt.Rows[i]["ShiftID"] =ShiftID;
            dt.Rows[i]["Status"] = "P";
            dt.Rows[i]["GatePassHrs"] = 10;
        }
        string sqlConnectionString = vt_Common.PayRollConnectionString;
        // Bulk Copy to SQL Server
        using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
        {
            bcp.BatchSize = 100000;
            bcp.DestinationTableName = "vt_tbl_Attendance";

            bcp.ColumnMappings.Add("CompanyId", "CompanyId");
            bcp.ColumnMappings.Add("EmployeeID", "EmployeeID");
            bcp.ColumnMappings.Add("EmpID", "EnrollId");
            bcp.ColumnMappings.Add("ShiftID", "ShiftID");
            bcp.ColumnMappings.Add("date", "Date");
            bcp.ColumnMappings.Add("CheckinTime", "InTime");
            bcp.ColumnMappings.Add("CheckOutTime", "OutTime");
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
            bcp.ColumnMappings.Add("GatePassHrs", "GatePassHrs");
            bcp.ColumnMappings.Add("LeaveDay", "LeaveDay");                
            bcp.BulkCopyTimeout = 0;
            bcp.WriteToServer(dt);
            bcp.Close();
            Load();
            MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Added");
        }
        MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Added");
        return dt;


        //List<vt_sp_GetComapanyID_ByCompanyName_Result> CompanyNameList = new List<vt_sp_GetComapanyID_ByCompanyName_Result>();
        ////CompanyNameList = GetCompanyID_ByCompanyName(Convert.ToInt32(Session["CompanyId"]).ToString());

        //List<Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID_Result> EmployeeID_List = new List<Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID_Result>();

        //DataTable dt = new DataTable();
        //try
        //{
        //    if (FileExportExcel.HasFile)
        //    {
        //        string extension = Path.GetExtension(FileExportExcel.PostedFile.FileName);
        //        string filename = Path.GetFullPath(FileExportExcel.FileName);
        //        IWorkbook workbook = null;
        //        //Stream uploadFileStream = FileUpload1.PostedFile.InputStream;
        //        HttpPostedFile file = Request.Files[0];
        //        MemoryStream mem = new MemoryStream();
        //        mem.SetLength((int)file.ContentLength);
        //        file.InputStream.Read(mem.GetBuffer(), 0, (int)file.ContentLength);
        //        //using (MemoryStream file= new MemoryStream())
        //        //{
        //        if (extension == ".xlsx")
        //        {
        //            workbook = new XSSFWorkbook(mem);
        //        }
        //        else if (extension == ".xls")
        //        {
        //            workbook = new XSSFWorkbook(mem);
        //        }
        //        else
        //        {
        //            throw new Exception("This format is not supported");
        //        }
        //        //}
        //        //IWorkbook workbook = WorkbookFactory.Create(uploadFileStream);
        //        ISheet sheet = workbook.GetSheetAt(0);
        //        System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

        //        IRow headerRow = sheet.GetRow(0);
        //        int cellCount = headerRow.LastCellNum;
        //        int rowCount = headerRow.RowNum;

        //        for (int j = 0; j < cellCount; j++)
        //        {
        //            ICell cell = headerRow.GetCell(j);
        //            dt.Columns.Add(cell.ToString());
        //        }

        //        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
        //        {
        //            IRow row = sheet.GetRow(i);
        //            DataRow dataRow = dt.NewRow();
        //            if (row == null)
        //            {
        //                break;
        //            }
        //            for (int j = row.FirstCellNum; j < cellCount; j++)
        //            {
        //                //if(row.GetCell(j))
        //                if (j == 0)
        //                {
        //                    CompanyNameList = GetCompanyID_ByCompanyName(row.GetCell(j).ToString());
        //                    dataRow[j] = CompanyNameList[0].CompanyID;
        //                }

        //                #region Working Code

        //                if (j == 1)
        //                {
        //                    EmployeeID_List = GetEmployeeID_By_EmployeeName_AND_CompanyID(CompanyNameList[0].CompanyID.ToString());
        //                    dataRow[j] = EmployeeID_List.Where(x => x.EnrollID == row.GetCell(j).ToString()).Select(x => x.EmployeeID).First();
        //                }

        //                if (j >= 2 && row.GetCell(j) != null)
        //                {
        //                    if (j == 5 || j == 6)
        //                    {
        //                        dataRow[j] = row.GetCell(j).DateCellValue;
        //                    }
        //                    else
        //                    {
        //                        dataRow[j] = row.GetCell(j).ToString();
        //                    }
        //                }

        //                #endregion Working Code
        //            }
        //            //dataRow["CompanyID"] = GET_CompanyId(CompanyNameList, dataRow);

        //            dt.Rows.Add(dataRow);
        //        }
        //        string sqlConnectionString = vt_Common.PayRollConnectionString;
        //        // Bulk Copy to SQL Server
        //        using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
        //        {
        //            bcp.BatchSize = 100000;
        //            bcp.DestinationTableName = "vt_tbl_Attendance";

        //            bcp.ColumnMappings.Add("CompanyId", "CompanyId");
        //            bcp.ColumnMappings.Add("EmployeeID", "EmployeeID");
        //            bcp.ColumnMappings.Add("EnrollId", "EnrollId");
        //            bcp.ColumnMappings.Add("ShiftID", "ShiftID");
        //            bcp.ColumnMappings.Add("Date", "Date");
        //            bcp.ColumnMappings.Add("InTime", "InTime");
        //            bcp.ColumnMappings.Add("OutTime", "OutTime");
        //            bcp.ColumnMappings.Add("Late", "Late");
        //            bcp.ColumnMappings.Add("Early", "Early");
        //            bcp.ColumnMappings.Add("OT", "OT");
        //            bcp.ColumnMappings.Add("TotalHrs", "TotalHrs");
        //            bcp.ColumnMappings.Add("Status", "Status");
        //            bcp.ColumnMappings.Add("Type", "Type");
        //            bcp.ColumnMappings.Add("Day", "Day");
        //            bcp.ColumnMappings.Add("OutNextDay", "OutNextDay");
        //            bcp.ColumnMappings.Add("LunchMin", "LunchMin");
        //            bcp.ColumnMappings.Add("LateStatus", "LateStatus");
        //            bcp.ColumnMappings.Add("LunchIn", "LunchIn");
        //            bcp.ColumnMappings.Add("LunchOut", "LunchOut");
        //            bcp.ColumnMappings.Add("BreakIn", "BreakIn");
        //            bcp.ColumnMappings.Add("BreakOut", "BreakOut");
        //            bcp.ColumnMappings.Add("LeaveDay", "LeaveDay");
        //            bcp.ColumnMappings.Add("GatePassHrs", "GatePassHrs");
        //            bcp.BulkCopyTimeout = 0;
        //            bcp.WriteToServer(dt);
        //            bcp.Close();
        //            MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Added");
        //        }

        //        return dt;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MsgBox.Show(Page, MsgBox.danger, "", "Please Enter Correct Data");
        //    return null;
        //}
    }

    //Load Data
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load()
    {
       
        string List = "";
        try
        {
            DateTime currentDate = DateTime.Now;


            using ( vt_EMSEntities db = new vt_EMSEntities())
            {
                DataTable dt = new DataTable();
                //For CompanyID
                if (Role != null && Role == "User")
                {
                 
                  
                    dt = ProcedureCall.GetAttendanceMonthWise(0, EmployeeID, currentDate).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                      
                        List = JsonConvert.SerializeObject(dt);
                    }
                }
                else if (CompanyID > 0)
                {

                    dt = ProcedureCall.GetAttendanceMonthWise(CompanyID, EmployeeID, currentDate).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                       
                      
                        List = JsonConvert.SerializeObject(dt);


                    }
                }
                else
                {
                    List = "";
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

        //JavaScriptSerializer s = new JavaScriptSerializer();
        //s.MaxJsonLength = 2147483644;

        //var str = s.Serialize(att);// Newtonsoft.Json.JsonConvert.SerializeObject(List);
        return List;

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object LoadBySearch(string date)
    {
        string List = "";

        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //For CompanyID
                if (date != "")
                {

                    DataTable dt = new DataTable();
                    if (Role != null && Role == "User")
                    {
                        dt = ProcedureCall.GetAttendanceMonthWise(0, EmployeeID, Convert.ToDateTime(date)).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            List = JsonConvert.SerializeObject(dt);
                        }

                    }
                    else if (CompanyID > 0)
                    {
                        dt = ProcedureCall.GetAttendanceMonthWise(CompanyID, 0, Convert.ToDateTime(date)).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            List = JsonConvert.SerializeObject(dt);
                        }

                    }
                    else
                    {
                        List = "";
                    }
                }
                else
                {

                    List = "";
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

    public List<Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID_Result> GetEmployeeID_By_EmployeeName_AND_CompanyID(string CompanyID)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        var obj = db.Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID(Convert.ToInt32(CompanyID));
        List<Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID_Result> result = obj.ToList();
        DataTable Dt = vt_Common.ConvertToDataTable(result);
        return result;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string Date = DateTime.Now.ToShortDateString();
        txtFromDate.Text = Date;
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        upMAttendance.Update();
        vt_Common.ReloadJS(this.Page, "$('#manualattendance').modal();");
    }

    public void BindEmployeeGrid(int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var EmployeeList = db.VT_SP_Get_Active_Employees(CompanyID).ToList();
            ddlEmployee.DataSource = EmployeeList;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();

            ddlUEmp.DataSource = EmployeeList;
            ddlUEmp.DataTextField = "EmployeeName";
            ddlUEmp.DataValueField = "EmployeeID";
            ddlUEmp.DataBind();

        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            DateTime entryDate = vt_Common.CheckDateTime(txtUdate.Text);
            long id = long.Parse(hdnAttID.Value);
            var attendance = db.vt_tbl_Attendance.Where(x => x.AttendanceID.Equals(id)).SingleOrDefault();
            string format = "MM/dd/yyyy";
            var date = entryDate.Date.ToShortDateString();
            //var date = dateAndTime.ToString(format);
            //var date = DateTime.ParseExact(attendance.Date.ToString(), format,CultureInfo.InvariantCulture);
            attendance.InTime = date + " " + txtUIN.Text;
            attendance.OutTime = date + " " + txtUOUT.Text;
            attendance.OT = chkUOvertime.Checked == true ? "1" : null;
            db.Entry(attendance).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            vt_Common.ReloadJS(this.Page, "$('#manualattendanceUpdate').modal('hide');");
            vt_Common.ReloadJS(this.Page, "reload()");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int recordID = vt_Common.CheckInt(ViewState["PageID"]);
            int employeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
            DateTime entryDate = vt_Common.CheckDateTime(txtFromDate.Text);
            int companyID = 0;

            companyID = Convert.ToInt32(Session["CompanyId"]);
            //}
            //else
            //{
            //    companyID = vt_Common.CheckInt(ddlModalCompany.SelectedValue);
            //}
            var record = db.vt_tbl_Attendance.FirstOrDefault(o => o.AttendanceID != recordID && o.CompanyId == companyID && o.EmployeeID == employeeID && System.Data.Entity.DbFunctions.TruncateTime(o.Date) == System.Data.Entity.DbFunctions.TruncateTime(entryDate));
            if (record != null)
            {
                vt_Common.ReloadJS(this.Page, "showMessage('Employee Attendance on this date already exist');");
            }
            else
            {
                vt_tbl_Attendance mAttendance = new vt_tbl_Attendance();
                if (employeeID != 0)
                {
                    var query = db.vt_tbl_Employee.Where(x => x.EmployeeID == employeeID).FirstOrDefault();
                    if (query !=null)
                    {
                        string format = "MM/dd/yyyy";
                         var date = entryDate.Date.ToShortDateString();

                        //var dateAndTime2 = dateAndTime;

                        //var date = dateAndTime.ToString(format);
                        mAttendance.CompanyId = companyID;
                        mAttendance.Date = entryDate;
                        mAttendance.EmployeeID = employeeID;
                        mAttendance.EnrollId = query.EnrollId;

                        mAttendance.InTime = date +" "+ txtInTime.Text;
                        mAttendance.OutTime = date + " " + txtOutTime.Text;
                        mAttendance.OT = chkOvertime.Checked == true ? 1.ToString() : null;
                        //mAttendance.ShiftID = Convert.ToInt32(ddlShift.SelectedValue);
                        mAttendance.Status = ddlStatus.SelectedValue;
                     
                        //mAttendance.OTHrs = txtOTHrs.Text;
                        if (ViewState["PageID"] != null)
                        {
                            mAttendance.AttendanceID = vt_Common.CheckInt(ViewState["PageID"]);
                            db.Entry(mAttendance).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.vt_tbl_Attendance.Add(mAttendance);
                        }
                        //var empRecord = db.vt_tbl_EmpAttendance.FirstOrDefault(o => o.CompanyID == companyID && o.EmployeeID == employeeID && o.EntryDate == entryDate);
                        //if (empRecord != null)
                        //{
                        //    empRecord.InTime = txtInTime.Text;
                        //    empRecord.OutTime = txtOutTime.Text;
                        //    if (ddlStatus.SelectedValue.Equals("P"))
                        //    {
                        //        empRecord.Status = true;
                        //    }
                        //    else
                        //    {
                        //        empRecord.Status = false;
                        //    }
                        //}
                        db.SaveChanges();
                        string empId = employeeID.ToString();
                        var leaveRecord = db.vt_tbl_LeaveApplicationDates.FirstOrDefault(o => o.EnrollId == empId && System.Data.Entity.DbFunctions.TruncateTime(o.Date) == System.Data.Entity.DbFunctions.TruncateTime(entryDate));
                        if (leaveRecord != null)
                        {
                            int? LeaveAppID = leaveRecord.LeaveAppID;
                            db.vt_tbl_LeaveApplicationDates.Remove(leaveRecord);
                            db.SaveChanges();
                            var recordCount = db.vt_tbl_LeaveApplicationDates.Where(o => o.LeaveAppID == LeaveAppID).Count();
                            if (recordCount == 0)
                            {
                                var leaveAppRecord = db.vt_tbl_LeaveApplication.FirstOrDefault(o => o.LeaveApplicationID == LeaveAppID);
                                if (leaveAppRecord != null)
                                {
                                    db.vt_tbl_LeaveApplication.Remove(leaveAppRecord);
                                    db.SaveChanges();
                                }
                            }
                        }
                        ClearForm();
                        Load();
                        Response.Redirect("Attendance_Sheet.aspx");
                        MsgBox.Show(Page, MsgBox.success, ddlStatus.SelectedValue, "Successfully Save");

                        //LoadData();
                        //UpView.Update();

                    }
                }
             
            }
        }
    }
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string OpenUpdateModel(string id)
    {
        var RecordId = int.Parse(id);
        vt_tbl_Attendance att;
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
             att = db.vt_tbl_Attendance.Where(x => x.AttendanceID.Equals(RecordId)).SingleOrDefault();
        }
        return new JavaScriptSerializer().Serialize(att);
    }
    public void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls); 
        vt_Common.Clear(pnlDetail2.Controls);
        hdnAttID.Value = 0.ToString();
        vt_Common.ReloadJS(this.Page, "$('#manualattendance').modal('hide');");
        vt_Common.ReloadJS(this.Page, "$('#manualattendanceUpdate').modal('hide');");
    }



    protected void btntimeout_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime entryDate = vt_Common.CheckDateTime(txtDate.Text);
            string date = txtDate.Text;// entryDate.Date.ToShortDateString();// .ToString();
            string Result = string.Empty;
            int companyID = Convert.ToInt32(Session["CompanyId"]);
            if (companyID > 0)
            {
                if (txtDate.Text != "")
                {
                    DataTable dt = ProcedureCall.SP_GetTimout(date, companyID).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        List<string> outTime = new List<string>();
                        foreach (DataRow rows in dt.Rows)
                        {
                            Result = rows["Result"].ToString();
                            if (Result == "Success")
                            {
                                MsgBox.Show(this.Page, MsgBox.success, " ", "Successfully done");

                            }
                            else

                            {
                                MsgBox.Show(this.Page, MsgBox.danger, "Sorry", "No absent record found of this month");
                            }                            
                        }
                        
                    }
                    else
                    {
                        MsgBox.Show(this.Page, MsgBox.danger, "Sorry", "No absent record found of this month");
                    }
                }
                else
                {
                    MsgBox.Show(this.Page, MsgBox.danger, " ", "Please select month");
                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }



}
   