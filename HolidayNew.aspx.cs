using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Viftech;

public partial class HolidayNew : System.Web.UI.Page
{
    public static int CompanyID = 0;
      
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                CompanyID = Convert.ToInt32(Session["CompanyID"]);
               // Bind_DdlDesignation();
            }
        }


    }

    //void Bind_DdlDesignation()
    //{
    //    int CompanyID = Convert.ToInt32(Session["CompanyId"]);
    //    SqlParameter[] param =
    //        {
    //            new SqlParameter("@CompanyID",CompanyID)
    //        };
    //    vt_Common.Bind_DropDown(DdlDesignation, "vt_sp_BindDesignation_byCompanyID", "Designation", "DesignationID", param);
    //    DdlDesignation.Items.Insert(1, new ListItem("All", "0"));
    //    //DdlDesignation.Items.Add(new ListItem("All", "0"));
    //}


    [ScriptMethod]
    [WebMethod]
    public static string getData(int DesignationID)
    {
        if (DesignationID > 0)
        {
            vt_EMSEntities db = new vt_EMSEntities();

            List<DxSchedular> Lst = new List<DxSchedular>();
            //var Data = db.Vt_Tbl_HolidaySchedule.Where(x => x.DesignationID == DesignationID).FirstOrDefault();
            var Data = db.Vt_Tbl_HolidaySchedule.Where(x => x.DesignationID == DesignationID && x.ForAllDesignation ==null).ToList();
            if (Data != null)
            {
                foreach (var item in Data)
                {
                    DxSchedular obj = new DxSchedular();
                    if (item.RecurrenceRule == "undefined" || item.RecurrenceRule == "")
                    {
                        obj.ID = item.ID;
                        obj.allDay = item.AllDay;
                        obj.text = item.LeaveTitle;
                        obj.startDate = Convert.ToDateTime(item.StartDate);
                        obj.endDate = Convert.ToDateTime(item.EndDate).AddDays(1);
                        obj.recurrenceRule = item.RecurrenceRule;
                        obj.repeatOnOff = item.RepeartOnOff;
                    }
                    else
                    {
                        
                        obj.ID = item.ID;
                        obj.allDay = item.AllDay;
                        obj.text = item.LeaveTitle;
                        obj.startDate = Convert.ToDateTime(item.StartDate);
                        obj.endDate = Convert.ToDateTime(item.EndDate);
                        obj.recurrenceRule = item.RecurrenceRule;
                        obj.repeatOnOff = item.RepeartOnOff;
                    }
                   
                    Lst.Add(obj);
                }
               

            }
            var arry = Lst.ToArray();
            return JsonConvert.SerializeObject(arry);

        }
        else
        {
            vt_EMSEntities db = new vt_EMSEntities();
            
            List<DxSchedular> Lst = new List<DxSchedular>();
            var Data = db.Vt_Tbl_HolidaySchedule.Where(x =>x.ForAllDesignation==true && x.CompanyID ==CompanyID).ToList();
            if (Data != null)
            {
                foreach (var item in Data)
                {
                    DxSchedular obj = new DxSchedular();
                    
                    if (item.RecurrenceRule == "undefined" || item.RecurrenceRule == "")
                    {
                        obj.allDay = item.AllDay;
                        obj.text = item.LeaveTitle;
                        obj.startDate = Convert.ToDateTime(item.StartDate);
                        obj.endDate = Convert.ToDateTime(item.EndDate).AddDays(1);
                        obj.recurrenceRule = item.RecurrenceRule;
                        obj.repeatOnOff = item.RepeartOnOff;
                    }
                    else
                    {
                        obj.allDay = item.AllDay;
                        obj.text = item.LeaveTitle;
                        obj.startDate = Convert.ToDateTime(item.StartDate);
                        obj.endDate = Convert.ToDateTime(item.EndDate);
                        obj.recurrenceRule = item.RecurrenceRule;
                        obj.repeatOnOff = item.RepeartOnOff;

                    }
                   
                    Lst.Add(obj);
                }
            }
            var arry = Lst.ToArray();
            return JsonConvert.SerializeObject(arry);
        }
    }

    [ScriptMethod]
    [WebMethod]
    public static string Create(string text, string startDate, string endDate, string allDay, string recurrenceRule, string repeatOnOff, int designationID)
    {

        string Start_Date = Convert.ToDateTime(startDate).ToString("MM/dd/yyyy");
        string End_Date = Convert.ToDateTime(endDate).ToString("MM/dd/yyyy");
        vt_EMSEntities db = new vt_EMSEntities();
        var day = Convert.ToDateTime(startDate).Day;
        var month = Convert.ToDateTime(startDate).Month;
        string result = string.Empty;
        string extract = string.Empty;
        string date = string.Empty;
        string year = string.Empty;
        string days = string.Empty;
        string months = string.Empty;
        string fullEndDate = string.Empty;
        int Year = Convert.ToDateTime(startDate).Year;
        string FullEndDateForRecurrence =HolidaDelete_DAL.CheckRecurrenceRule(recurrenceRule);
        Vt_Tbl_HolidaySchedule Data = new Vt_Tbl_HolidaySchedule();
        var desg = db.vt_tbl_Designation.Where(x => x.CompanyID == CompanyID && x.IsActive == true).ToList();
        int desdid = 0;
        int HolidayScheduleId = 0;
        //For ALl designation
        if (designationID == 0)
        {
            if (desg.Count > 0)
            {
                if (recurrenceRule == "undefined")
                {
                    var check = OneDayHolidayForALl(text, startDate, endDate, allDay, recurrenceRule, repeatOnOff, designationID);
                    result = check;

                }
                else if (recurrenceRule.Contains("SA"))
                {


                    if (recurrenceRule.Contains("INTERVAL=2"))//for alternate
                    {
                        //for alterna saturday for all designation
                        if (FullEndDateForRecurrence == "")
                        {
                            var CHECK = SaturdayForALl(text, startDate, endDate, allDay, recurrenceRule, repeatOnOff, designationID, FullEndDateForRecurrence);
                            result = CHECK;
                        }
                        else
                        {

                            var CHECK = SaturdayForALl(text, startDate, endDate, allDay, recurrenceRule, repeatOnOff, designationID, FullEndDateForRecurrence);
                            result = CHECK;
                        }

                    }
                    else
                    {

                        if (FullEndDateForRecurrence == "")
                        {

                        }
                        else
                        {
                            var CHECK = SaturdayForALl(text, startDate, endDate, allDay, recurrenceRule, repeatOnOff, designationID, FullEndDateForRecurrence);
                            result = CHECK;
                        }

                    }

                }
                else if (recurrenceRule.Contains("SU"))
                {

                    var CHECK = GetSundayAllDesignation(text, startDate, endDate, allDay, recurrenceRule, repeatOnOff, designationID);
                    result = CHECK.ToString();
                }
            }
        }
        //===========================//



        //For Individual designation
        else
        {
            if (desg != null)
            {

                Data.LeaveTitle = text;
                Data.StartDate = Start_Date;
                Data.EndDate = End_Date;
                // Data.StartDate = Convert.ToDateTime(startDate).ToString("mm/dd/yyyy");
                // Data.EndDate = Convert.ToDateTime(endDate).ToString("mm/dd/yyyy");
                Data.AllDay = allDay;
                Data.CompanyID = CompanyID;
                Data.RecurrenceRule = recurrenceRule;
                Data.RepeartOnOff = repeatOnOff;
                Data.DesignationID = designationID;
                db.Entry(Data).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
            Vt_Tbl_HolidaySchedule EnteredData = db.Vt_Tbl_HolidaySchedule.OrderByDescending(x => x.ID).FirstOrDefault();
            List<Vt_Tbl_HolidaysDates> ObjH = new List<Vt_Tbl_HolidaysDates>();
            //code
            var NumberOfDays = (vt_Common.CheckDateTime(endDate) - vt_Common.CheckDateTime(startDate)).TotalDays;
            Vt_Tbl_HolidaySchedule hid = db.Vt_Tbl_HolidaySchedule.OrderByDescending(x => x.ID).FirstOrDefault();
            Vt_Tbl_HolidaySchedule hsid = db.Vt_Tbl_HolidaySchedule.Where(x => x.ID == hid.ID).FirstOrDefault();
            Vt_Tbl_HolidaySchedule holid = db.Vt_Tbl_HolidaySchedule.Where(y => y.ID == hsid.ID).FirstOrDefault();
            bool firstDayOfLeave = true;
            if (recurrenceRule == "undefined" || recurrenceRule == "")
            {
                for (int i = 1; i <= NumberOfDays + 1; i++)
                {

                    Vt_Tbl_HolidaysDates HolidayDates = new Vt_Tbl_HolidaysDates();
                    HolidayDates.HolidayScheduleID = EnteredData.ID;
                    HolidayDates.HolidayDate = Start_Date;// startDate.ToString("MM/DD/YYYY");
                    HolidayDates.DesignationID = designationID;
                    if (firstDayOfLeave)
                    {
                        HolidayDates.HolidayDate = Start_Date;
                    }
                    else
                    {
                        HolidayDates.HolidayDate = vt_Common.CheckDateTime(holid.StartDate).AddDays(i - 1).ToString("MM/dd/yyyy");
                    }
                    DateTime? dtDate = DateTime.Parse(HolidayDates.HolidayDate);
                    firstDayOfLeave = false;
                    ObjH.Add(HolidayDates);
                    db.Vt_Tbl_HolidaysDates.AddRange(ObjH);
                   
                }
                db.SaveChanges();
                result = "Success";
            }
            else
            {

                //For ALternate Saturday for individual employees
                if (recurrenceRule.Contains("SA"))
                {
                    var check = InsertSaturdaySingleDesignation(text, startDate, endDate, allDay, recurrenceRule, repeatOnOff, designationID, EnteredData.ID);
                    if (check == "Success")
                    {
                        result = check;

                    }
                    else
                    {
                        result = "false";
                    }
                                          

                }
               else if (recurrenceRule.Contains("SU"))
                {
                    var check= InsertSundaySingleDesignation(text, startDate, endDate, allDay, recurrenceRule, repeatOnOff, designationID, EnteredData.ID);
                    result = check;
                }
                else
                {
                    //int ID = EnteredData.ID;
                    //Vt_Tbl_HolidaySchedule objholidayschedule = db.Vt_Tbl_HolidaySchedule.Where(x => x.ID == ID).FirstOrDefault();
                    ////db.Entry(Data).State = System.Data.Entity.EntityState.Deleted;
                    //db.Vt_Tbl_HolidaySchedule.Remove(objholidayschedule);
                    //db.SaveChanges();
                    //result = "Delete";
                }

            }

        }
        return result;
    }
    [ScriptMethod]
    [WebMethod]
    public static bool Delete(string text, string date,string designationID,string rules)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        string StartDate = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
        int ID = 0;
        int schedulerid = Convert.ToInt32(text);
        if (designationID == "0")
        {
            //var check = DeleteAllHolidays(text, StartDate, designationID, rules);
            var check = ProcedureCall.GetHolidays(StartDate, CompanyID, designationID, rules).Tables[0];
            if (check.Rows.Count > 0)
            {
                if (check.Rows[0]["Result"].ToString().ToLower() == "failed")
                {
                    return false;
                }

            }
         
        }
       else
        {
          var check =ProcedureCall.GetHolidays(StartDate, CompanyID,designationID, rules);
        }

        return true;
    }


    public static string DeleteAllHolidays(string SID, string date, string designationID, string rules)
    {
        string result = string.Empty;
        try
        {
           
            DataTable dt_check = ProcedureCall.GetHolidays(date, CompanyID, designationID, rules).Tables[0];
            if (dt_check.Rows.Count > 0)
            {
                result = "Success";
            }
            
        }
        catch (Exception)
        {

            //throw;
        }
        return result;

        //string result = string.Empty;
        //string recurrencerule = string.Empty;
        //string FullDate = string.Empty;
        //var month =Convert.ToDateTime(date).Month;
        //var day    = Convert.ToDateTime(date).Day;
        //var Year = Convert.ToDateTime(date).Year;

        //vt_EMSEntities db = new vt_EMSEntities();
        //try
        //{
        //    Vt_Tbl_HolidaySchedule hs = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == date).FirstOrDefault();
        //            Vt_Tbl_HolidaysDates hd = db.Vt_Tbl_HolidaysDates.Where(x => x.HolidayDate == date).FirstOrDefault();
        //    if (designationID =="0")
        //    {
        //        if (rules =="undefined")
        //        {

        //            var check =HolidaDelete_DAL.DeleteAllDesignationHolidays(SID, date, designationID, rules);
        //            if (check == "success")
        //            {
        //                result = check;
        //            }
        //            else
        //            {
        //                result = "false";
        //            }

        //            }



        //        else if (rules.Contains("SA"))
        //        {

        //            var check = HolidaDelete_DAL.DeleteAllDesignationSaturday(SID, date, designationID, rules);
        //            if (check =="success")
        //            {
        //                result = check;

        //            }
        //            else
        //            {
        //                result = "false";
        //            }
        //        }
        //        else if(rules.Contains("SU"))
        //        {

        //            var check = HolidaDelete_DAL.DeleteAllDesignationSunday(SID, date, designationID, rules);
        //            if (check == "success")
        //            {
        //                result = check;
        //            }
        //            else
        //            {
        //                result = "false";
        //            }
        //        }


        //    }

        //}
        //catch (Exception ex)
        //{

        //    throw;
        //}

    }
    
    public static string OneDayHolidayForALl(string text, string startDate, string endDate, string allDay, string recurrenceRule, string repeatOnOff, int designationID)
    {
        try
        {
          
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["vt_PayRollConnection"].ConnectionString);
            vt_EMSEntities db = new vt_EMSEntities();
            var desg = db.vt_tbl_Designation.Where(x => x.CompanyID == CompanyID && x.IsActive == true).ToList();
            int desdid = 0;
            int HolidayScheduleId = 0;
            string result = string.Empty;
            List<Vt_Tbl_HolidaySchedule> LstHolidaysS = new List<Vt_Tbl_HolidaySchedule>();
            //foreach (var itemholidayschedule in desg)
            //{
               
                Vt_Tbl_HolidaySchedule Data = new Vt_Tbl_HolidaySchedule();
                //desdid = itemholidayschedule.DesignationID;
                Data.LeaveTitle = text;                
                Data.StartDate =Convert.ToDateTime(startDate).ToString("MM/dd/yyyy");
                Data.EndDate = Convert.ToDateTime(endDate).ToString("MM/dd/yyyy");
                Data.AllDay = allDay;
                Data.CompanyID = CompanyID;
                Data.RecurrenceRule = recurrenceRule;
                Data.RepeartOnOff = repeatOnOff;
                Data.DesignationID = 0;
                Data.ForAllDesignation = true;
                LstHolidaysS.Add(Data);

            //}
            // for bulk import into Holiday schedule
            DataTable dt_holidaySchedule = ToDataTable(LstHolidaysS);
            if (dt_holidaySchedule.Rows.Count > 0)
            {
                using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                { 
                    connection.Open();

                    bcp.DestinationTableName = "[Vt_Tbl_HolidaySchedule]";

                    bcp.ColumnMappings.Add("CompanyID", "CompanyID");
                    bcp.ColumnMappings.Add("DesignationID", "DesignationID");
                    bcp.ColumnMappings.Add("LeaveTitle", "LeaveTitle");
                    bcp.ColumnMappings.Add("StartDate", "StartDate");
                    bcp.ColumnMappings.Add("EndDate", "EndDate");
                    bcp.ColumnMappings.Add("AllDay", "AllDay");
                    bcp.ColumnMappings.Add("RepeartOnOff", "RepeartOnOff");
                    bcp.ColumnMappings.Add("RecurrenceRule", "RecurrenceRule");
                    bcp.ColumnMappings.Add("ForAllDesignation", "ForAllDesignation");

                    bcp.WriteToServer(dt_holidaySchedule);
                    connection.Close();
                }
            }
                

                //code
                var NumberOfDays = (vt_Common.CheckDateTime(endDate) - vt_Common.CheckDateTime(startDate)).TotalDays;
                Vt_Tbl_HolidaySchedule hid = db.Vt_Tbl_HolidaySchedule.OrderByDescending(x => x.ID).FirstOrDefault();
                Vt_Tbl_HolidaySchedule hsid = db.Vt_Tbl_HolidaySchedule.Where(x => x.ID == hid.ID).FirstOrDefault();

                bool firstDayOfLeave = true;
                Vt_Tbl_HolidaySchedule holid = db.Vt_Tbl_HolidaySchedule.Where(y => y.ID == hsid.ID).FirstOrDefault();
                Vt_Tbl_HolidaySchedule EnteredData1 = db.Vt_Tbl_HolidaySchedule.Where(x => x.ID == HolidayScheduleId && x.CompanyID == CompanyID).FirstOrDefault();


                //for holidaysdates
                if (recurrenceRule == "undefined")
                {
                string SDate = Convert.ToDateTime(startDate).ToString("MM/dd/yyyy");
                string EDate = Convert.ToDateTime(endDate).ToString("MM/dd/yyyy");
                if (desg != null)
                    {
                    var Holidays = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == SDate && x.EndDate == EDate && x.CompanyID == CompanyID).ToList();
                    List<Vt_Tbl_HolidaysDates> VtHl = new List<Vt_Tbl_HolidaysDates>();
                    foreach (var Holidayitem in Holidays)
                    {
                       
                        for (int i = 1; i <= NumberOfDays + 1; i++)
                        {
                            //desdid = item.DesignationID;

                            Vt_Tbl_HolidaysDates HolidayDates = new Vt_Tbl_HolidaysDates();
                            HolidayDates.HolidayScheduleID = Holidayitem.ID;
                            HolidayDates.HolidayDate = vt_Common.CheckDateTime(holid.StartDate).ToString("MM/dd/yyyy");
                            HolidayDates.DesignationID = Holidayitem.DesignationID;
                            if (firstDayOfLeave)
                            {
                                HolidayDates.HolidayDate = vt_Common.CheckDateTime(holid.StartDate).ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                HolidayDates.HolidayDate = Convert.ToDateTime(holid.StartDate).AddDays(i - 1).ToString("MM/dd/yyyy");
                            }
                            DateTime? dtDate = DateTime.Parse(HolidayDates.HolidayDate);
                            firstDayOfLeave = false;
                            VtHl.Add(HolidayDates);

                        }
                    }
                        
                    DataTable dt = ToDataTable(VtHl);
                    if (dt.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                        {
                            connection.Open();

                            bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                            bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                            bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                            bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                            bcp.WriteToServer(dt);
                            connection.Close();
                        }
                        result = "Success";

                    }

                    //db.Vt_Tbl_HolidaysDates.AddRange(VtHl);
                    // db.SaveChanges();
                }
                }
            
  
            return "Success";
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    //Saturday For All Designation
    public static string SaturdayForALl(string text, string startDate, string endDate, string allDay, string recurrenceRule, string repeatOnOff, int designationID,string FullEndDate)
    {
        string Start_Date = Convert.ToDateTime(startDate).ToString("MM/dd/yyyy");
        string End_Date = Convert.ToDateTime(endDate).ToString("MM/dd/yyyy");
        var day = Convert.ToDateTime(startDate).Day;
        var month = Convert.ToDateTime(startDate).Month;
        List<Vt_Tbl_HolidaysDates> LstHolidays = new List<Vt_Tbl_HolidaysDates>();
        List<Vt_Tbl_HolidaySchedule> LstHolidaysS = new List<Vt_Tbl_HolidaySchedule>();
        var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["vt_PayRollConnection"].ConnectionString);
        string result = string.Empty;
        if (recurrenceRule.Contains("SA"))
        {
            if (recurrenceRule.Contains("INTERVAL=2"))
            {
                List<Vt_Tbl_HolidaysDates> objHS = new List<Vt_Tbl_HolidaysDates>();
                vt_EMSEntities db = new vt_EMSEntities();
                var desg = db.vt_tbl_Designation.Where(x => x.CompanyID == CompanyID && x.IsActive == true).ToList();
                int desdid = 0;
                int HolidayScheduleId = 0;
             

                foreach (var itemholidayschedule in desg)
                {
                    Vt_Tbl_HolidaySchedule Data = new Vt_Tbl_HolidaySchedule();
                    desdid = itemholidayschedule.DesignationID;
                    Data.LeaveTitle = text;
                    Data.StartDate = Start_Date;
                    Data.EndDate = End_Date;
                    Data.AllDay = allDay;
                    Data.CompanyID = CompanyID;
                    Data.RecurrenceRule = recurrenceRule;
                    Data.RepeartOnOff = repeatOnOff;
                    Data.DesignationID = desdid;
                    Data.ForAllDesignation = true;
                    LstHolidaysS.Add(Data);

                }
                // for bulk import into Holiday schedule
                DataTable dt_holidaySchedule = ToDataTable(LstHolidaysS);
                if (dt_holidaySchedule.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaySchedule]";

                        bcp.ColumnMappings.Add("CompanyID", "CompanyID");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");
                        bcp.ColumnMappings.Add("LeaveTitle", "LeaveTitle");
                        bcp.ColumnMappings.Add("StartDate", "StartDate");
                        bcp.ColumnMappings.Add("EndDate", "EndDate");
                        bcp.ColumnMappings.Add("AllDay", "AllDay");
                        bcp.ColumnMappings.Add("RepeartOnOff", "RepeartOnOff");
                        bcp.ColumnMappings.Add("RecurrenceRule", "RecurrenceRule");
                        bcp.ColumnMappings.Add("ForAllDesignation", "ForAllDesignation");

                        bcp.WriteToServer(dt_holidaySchedule);
                        connection.Close();
                    }

                    var Holidays = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == Start_Date && x.EndDate == End_Date && x.CompanyID == CompanyID).ToList();
                    foreach (var Holidayitem in Holidays)
                    {
                        //Vt_Tbl_HolidaysDates record
                        int Year = DateTime.Now.Year;
                        List<DateTime> LstStartDate = GetAllAlternateSaturdays(Year, month, day);

                        foreach (var item in LstStartDate)
                        {
                            if (FullEndDate == "")
                            {
                                Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                                HolidayDate.HolidayScheduleID = Holidayitem.ID;
                                HolidayDate.HolidayDate = item.ToString("MM/dd/yyyy");
                                HolidayDate.DesignationID = Holidayitem.DesignationID;
                                // LstHolidays.Add(HolidayDate);
                                LstHolidays.Add(HolidayDate);
                                //  db.Vt_Tbl_HolidaysDates.AddRange(LstHolidays);

                            }
                            else
                            {
                                if (item.Date <= Convert.ToDateTime(FullEndDate))
                                {
                                    Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                                    HolidayDate.HolidayScheduleID = Holidayitem.ID;
                                    HolidayDate.HolidayDate = item.ToString("MM/dd/yyyy");
                                    HolidayDate.DesignationID = Holidayitem.DesignationID;
                                    LstHolidays.Add(HolidayDate);
                                }
                                else
                                {
                                    break;

                                }

                            }

                        }
                    }
                    /// check for holidays dates list record throgh bulk import

                    DataTable dt = ToDataTable(LstHolidays);
                    if (dt.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                        {
                            connection.Open();

                            bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                            bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                            bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                            bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                            bcp.WriteToServer(dt);
                            connection.Close();
                        }
                        result = "Success";

                    }

                }
            }
            else
            {
               
               
                vt_EMSEntities db = new vt_EMSEntities();
                var desg = db.vt_tbl_Designation.Where(x => x.CompanyID == CompanyID && x.IsActive == true).ToList();
                int desdid = 0;
                int HolidayScheduleId = 0;
              
                foreach (var itemholidayschedule in desg)
                {
                    Vt_Tbl_HolidaySchedule Data = new Vt_Tbl_HolidaySchedule();
                    desdid = itemholidayschedule.DesignationID;
                    Data.LeaveTitle = text;
                    Data.StartDate = Start_Date;
                    Data.EndDate = End_Date;
                    Data.AllDay = allDay;
                    Data.CompanyID = CompanyID;
                    Data.RecurrenceRule = recurrenceRule;
                    Data.RepeartOnOff = repeatOnOff;
                    Data.DesignationID = desdid;
                    Data.ForAllDesignation = true;
                    LstHolidaysS.Add(Data);

                }
                // for bulk import into Holiday schedule
                DataTable dt_holidaySchedule = ToDataTable(LstHolidaysS);
                if (dt_holidaySchedule.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaySchedule]";

                        bcp.ColumnMappings.Add("CompanyID", "CompanyID");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");
                        bcp.ColumnMappings.Add("LeaveTitle", "LeaveTitle");
                        bcp.ColumnMappings.Add("StartDate", "StartDate");
                        bcp.ColumnMappings.Add("EndDate", "EndDate");
                        bcp.ColumnMappings.Add("AllDay", "AllDay");
                        bcp.ColumnMappings.Add("RepeartOnOff", "RepeartOnOff");
                        bcp.ColumnMappings.Add("RecurrenceRule", "RecurrenceRule");
                        bcp.ColumnMappings.Add("ForAllDesignation", "ForAllDesignation");

                        bcp.WriteToServer(dt_holidaySchedule);
                        connection.Close();
                    }

                    var Holidays = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == Start_Date && x.EndDate == End_Date && x.CompanyID == CompanyID).ToList();
                    foreach (var Holidayitem in Holidays)
                    {
                        //Vt_Tbl_HolidaysDates record
                        int Year = DateTime.Now.Year;
                        List<DateTime> LstStartDate = GetAllSaturdays(Year, month, day);

                        foreach (var item in LstStartDate)
                        {
                            if (FullEndDate == "")
                            {
                                Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                                HolidayDate.HolidayScheduleID = Holidayitem.ID;
                                HolidayDate.HolidayDate = item.ToString("MM/dd/yyyy");
                                HolidayDate.DesignationID = Holidayitem.DesignationID;
                                // LstHolidays.Add(HolidayDate);
                                LstHolidays.Add(HolidayDate);
                                //  db.Vt_Tbl_HolidaysDates.AddRange(LstHolidays);

                            }
                            else
                            {
                                if (item.Date <= Convert.ToDateTime(FullEndDate))
                                {
                                    Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                                    HolidayDate.HolidayScheduleID = Holidayitem.ID;
                                    HolidayDate.HolidayDate = item.ToString("MM/dd/yyyy");
                                    HolidayDate.DesignationID = Holidayitem.DesignationID;
                                    LstHolidays.Add(HolidayDate);
                                }
                                else
                                {
                                    break;

                                }

                            }

                        }
                    }
                    /// check for holidays dates list record throgh bulk import

                    DataTable dt = ToDataTable(LstHolidays);
                    if (dt.Rows.Count > 0)
                    {
                           using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                            connection.Close();
                        }
                    result = "Success";

                    }

                }
            }

        }
        return result;
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
    //Sunday for all designation
    public static string GetSundayAllDesignation(string text, string startDate, string endDate, string allDay, string recurrenceRule, string repeatOnOff, int designationID)
    {
        string Start_Date = Convert.ToDateTime(startDate).ToString("MM/dd/yyyy");
        string End_Date = Convert.ToDateTime(endDate).ToString("MM/dd/yyyy");
        var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["vt_PayRollConnection"].ConnectionString);
        var day = Convert.ToDateTime(startDate).Day;
        var month = Convert.ToDateTime(startDate).Month;
        var result = string.Empty;
        vt_EMSEntities db = new vt_EMSEntities();
        var desg = db.vt_tbl_Designation.Where(x => x.CompanyID == CompanyID && x.IsActive == true).ToList();
        int desdid = 0;
        int HolidayScheduleId = 0;
        List<Vt_Tbl_HolidaySchedule> LstHolidaysSchedule = new List<Vt_Tbl_HolidaySchedule>();

        foreach (var itemholidayschedule in desg)
        {
            Vt_Tbl_HolidaySchedule Data = new Vt_Tbl_HolidaySchedule();
            desdid = itemholidayschedule.DesignationID;
            Data.LeaveTitle = text;
            Data.StartDate = Start_Date;
            Data.EndDate = End_Date;
            // Data.StartDate = Convert.ToDateTime(startDate).ToString("mm/dd/yyyy");
            // Data.EndDate = Convert.ToDateTime(endDate).ToString("mm/dd/yyyy");
            Data.AllDay = allDay;
            Data.CompanyID = CompanyID;
            Data.RecurrenceRule = recurrenceRule;
            Data.RepeartOnOff = repeatOnOff;
            Data.DesignationID = desdid;
            Data.ForAllDesignation = true;
            LstHolidaysSchedule.Add(Data);
            //db.Entry(Data).State = System.Data.Entity.EntityState.Added;
            //db.SaveChanges();
            //result = "Success";
        }
        DataTable dt_holidaySchedule = ToDataTable(LstHolidaysSchedule);
        if (dt_holidaySchedule.Rows.Count > 0)
        {
            using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
            {
                connection.Open();

                bcp.DestinationTableName = "[Vt_Tbl_HolidaySchedule]";

                bcp.ColumnMappings.Add("CompanyID", "CompanyID");
                bcp.ColumnMappings.Add("DesignationID", "DesignationID");
                bcp.ColumnMappings.Add("LeaveTitle", "LeaveTitle");
                bcp.ColumnMappings.Add("StartDate", "StartDate");
                bcp.ColumnMappings.Add("EndDate", "EndDate");
                bcp.ColumnMappings.Add("AllDay", "AllDay");
                bcp.ColumnMappings.Add("RepeartOnOff", "RepeartOnOff");
                bcp.ColumnMappings.Add("RecurrenceRule", "RecurrenceRule");
                bcp.ColumnMappings.Add("ForAllDesignation", "ForAllDesignation");

                bcp.WriteToServer(dt_holidaySchedule);
                connection.Close();
            }

            //HolidayScheduleId = Data.ID;
            List<Vt_Tbl_HolidaysDates> LstHolidays = new List<Vt_Tbl_HolidaysDates>();
            int Year = DateTime.Now.Year;
            List<DateTime> LstStartDate = HolidaDelete_DAL.GetAllSundays(Year, month, day);
            var Holidays = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == Start_Date && x.EndDate == End_Date && x.CompanyID == CompanyID).ToList();
            foreach (var Holidayitem in Holidays)
            {
                foreach (var item in LstStartDate)
                {
                    Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                    HolidayDate.HolidayScheduleID = Holidayitem.ID;
                    HolidayDate.HolidayDate = item.ToString("MM/dd/yyyy");
                    HolidayDate.DesignationID = Holidayitem.DesignationID;
                    LstHolidays.Add(HolidayDate);
                    //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);
                    //db.SaveChanges();

                }
              
            }
            DataTable dt = ToDataTable(LstHolidays);
            if (dt.Rows.Count > 0)
            {
                using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                {
                    connection.Open();

                    bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                    bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                    bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                    bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                    bcp.WriteToServer(dt);
                    connection.Close();
                }

                result = "Success";
            }



        }


        return result;
    }


    public static List<DateTime> GetAllAlternateSaturdays(int year, int month, int day)
    {

        var Count = 0;
        var result = new List<DateTime>();
        for (var date = new DateTime(year, month, day); date.Year == year; date = date.AddDays(1))
        {
            Count++;
            if (Count % 2 == 0)
            {
                continue;
            }
            if (!(new[] { 6, 6 }).Contains((int)date.DayOfWeek)) continue;
            result.Add(date);
            if (date.DayOfWeek == 0) date = date.AddDays(5); // skip mon trough fri
        }
        return result;
    }

    public static List<DateTime> GetAllSaturdays(int year, int month, int day)
    {

        var result = new List<DateTime>();
        DateTime start = new DateTime(year, month, day);
        DateTime end = new DateTime(year, 12, 31);
        List<DateTime> saturdays = new List<DateTime>();
        while (start < end)
        {
            if (start.DayOfWeek == DayOfWeek.Saturday)
            {
                saturdays.Add(new DateTime(start.Year, start.Month, start.Day));
                result.Add(start);
                start = start.AddDays(7);
               
            }
            else
            {
                start = start.AddDays(1);
            }
        }
        return result;
    }

    public static string InsertSaturdaySingleDesignation(string text, string startDate, string endDate, string allDay, string recurrenceRule, string repeatOnOff, int designationID,int EnteredDataID)
    {

        var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["vt_PayRollConnection"].ConnectionString);
        var day = Convert.ToDateTime(startDate).Day;
        var month = Convert.ToDateTime(startDate).Month;
        int Year = Convert.ToDateTime(startDate).Year;
        var result = new List<DateTime>();
        vt_EMSEntities db = new vt_EMSEntities();
        var desg = db.vt_tbl_Designation.Where(x => x.CompanyID == CompanyID && x.IsActive == true).ToList();
        int desdid = 0;
        int HolidayScheduleId = 0;
       string FullEndDateForRecurrence = HolidaDelete_DAL.CheckRecurrenceRule(recurrenceRule);
      
        List<Vt_Tbl_HolidaysDates> LstHolidaysSat = new List<Vt_Tbl_HolidaysDates>();
        if (recurrenceRule.Contains("SA") && recurrenceRule.Contains("INTERVAL=2"))
        {
            List<DateTime> LstAllSat = GetAllAlternateSaturdays(Year, month, day);
            if (FullEndDateForRecurrence == "")
            {
                foreach (var items in LstAllSat)
                {
                    Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                    HolidayDate.HolidayScheduleID = EnteredDataID;
                    HolidayDate.HolidayDate = items.ToString("MM/dd/yyyy");
                    HolidayDate.DesignationID = designationID;                    
                    LstHolidaysSat.Add(HolidayDate);
                  

                    //db.Vt_Tbl_HolidaysDates.AddRange(LstHolidaysSat);
                    //db.Vt_Tbl_HolidaysDates.AddRange(LstHolidaysSat);
                    
                 
                    //For all saturday
                }
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }
                 

                }
                //db.SaveChanges();
            }
            else
            {
                foreach (var items in LstAllSat)
                {
                    if (items.Date <= Convert.ToDateTime(FullEndDateForRecurrence))
                    {
                        Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                        HolidayDate.HolidayScheduleID = EnteredDataID;
                        HolidayDate.HolidayDate = items.ToString("MM/dd/yyyy");
                        HolidayDate.DesignationID = designationID;

                        LstHolidaysSat.Add(HolidayDate);

                        //db.Vt_Tbl_HolidaysDates.AddRange(LstHolidaysSat);
                        //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);
                        //LstHolidaysSat.Add(HolidayDate);
                        //LstHolidaysSat.ForEach(n => db.Vt_Tbl_HolidaysDates.Add(n));
                     
                      

                    }
                    else
                    {
                        break;
                    }
                    //For all saturday
                }
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }


                }
                //db.SaveChanges();


            }

        }


        //Second else  
        else
        {
            List<DateTime> LstAllSat = GetAllSaturdays(Year, month, day);
            if (FullEndDateForRecurrence == "")
            {
                foreach (var items in LstAllSat)
                {
                    Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                    HolidayDate.HolidayScheduleID = EnteredDataID;
                    HolidayDate.HolidayDate = items.ToString("MM/dd/yyyy");
                    HolidayDate.DesignationID = designationID;

                    LstHolidaysSat.Add(HolidayDate);
                    //db.Vt_Tbl_HolidaysDates.AddRange(LstHolidaysSat);
                    //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);

                    //LstHolidaysSat.Add(HolidayDate);
                    //LstHolidaysSat.ForEach(n => db.Vt_Tbl_HolidaysDates.Add(n));
                  
                    //For all saturday
                }
                //db.SaveChanges();
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }


                }

            }
            else
            {
                foreach (var items in LstAllSat)
                {
                    if (items.Date <= Convert.ToDateTime(FullEndDateForRecurrence))
                    {
                        Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                        HolidayDate.HolidayScheduleID = EnteredDataID;
                        HolidayDate.HolidayDate = items.ToString("MM/dd/yyyy");
                        HolidayDate.DesignationID = designationID;
                        LstHolidaysSat.Add(HolidayDate);
                        //db.Vt_Tbl_HolidaysDates.AddRange(LstHolidaysSat);
                        //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);
                        //LstHolidaysSat.Add(HolidayDate);
                        //LstHolidaysSat.ForEach(n => db.Vt_Tbl_HolidaysDates.Add(n));
                        

                    }
                    else
                    {
                        break;
                    }

                    //For all saturday
                }
                //db.SaveChanges();
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }


                }

            }

        }

        return "Success";
    }


    public static string InsertSundaySingleDesignation(string text, string startDate, string endDate, string allDay, string recurrenceRule, string repeatOnOff, int designationID, int EnteredDataID)
    {
        var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["vt_PayRollConnection"].ConnectionString);
        var day = Convert.ToDateTime(startDate).Day;
        var month = Convert.ToDateTime(startDate).Month;
        int Year = Convert.ToDateTime(startDate).Year;
        var result = new List<DateTime>();
        vt_EMSEntities db = new vt_EMSEntities(); 
        int HolidayScheduleId = 0;
        string FullEndDateForRecurrence = HolidaDelete_DAL.CheckRecurrenceRule(recurrenceRule);

        List<Vt_Tbl_HolidaysDates> LstHolidaysSat = new List<Vt_Tbl_HolidaysDates>();
        if (recurrenceRule.Contains("SU") && recurrenceRule.Contains("INTERVAL=2"))
        {
            List<DateTime> LstAllSat =HolidaDelete_DAL.GetAllSundays(Year, month, day);
            if (FullEndDateForRecurrence == "")
            {
                foreach (var items in LstAllSat)
                {
                    Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                    HolidayDate.HolidayScheduleID = EnteredDataID;
                    HolidayDate.HolidayDate = items.ToString();
                    HolidayDate.DesignationID = designationID;
                    LstHolidaysSat.Add(HolidayDate);
                    //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);
                    ////LstHolidaysSat.Add(HolidayDate);
                    ////LstHolidaysSat.ForEach(n => db.Vt_Tbl_HolidaysDates.Add(n));
                    //db.SaveChanges();
                    
                }
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }


                }

            }
            else if (recurrenceRule.Contains("SU"))
            {
                foreach (var items in LstAllSat)
                {
                    if (items.Date <= Convert.ToDateTime(FullEndDateForRecurrence))
                    {
                        Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                        HolidayDate.HolidayScheduleID = EnteredDataID;
                        HolidayDate.HolidayDate = items.ToString();
                        HolidayDate.DesignationID = designationID;
                        LstHolidaysSat.Add(HolidayDate);
                        //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);
                        ////LstHolidaysSat.Add(HolidayDate);
                        ////LstHolidaysSat.ForEach(n => db.Vt_Tbl_HolidaysDates.Add(n));
                        //db.SaveChanges();

                    }

                }
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }


                }

            }

        }


        //Second else  
        else
        {
            List<DateTime> LstAllSat = HolidaDelete_DAL.GetAllSundays(Year, month, day);
            if (FullEndDateForRecurrence == "")
            {
                foreach (var items in LstAllSat)
                {
                    Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                    HolidayDate.HolidayScheduleID = EnteredDataID;
                    HolidayDate.HolidayDate = items.ToString();
                    HolidayDate.DesignationID = designationID;
                    LstHolidaysSat.Add(HolidayDate);
                    //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);
                    ////LstHolidaysSat.Add(HolidayDate);
                    ////LstHolidaysSat.ForEach(n => db.Vt_Tbl_HolidaysDates.Add(n));
                    //db.SaveChanges();
                    //For all saturday
                }
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }


                }

            }
            else
            {
                foreach (var items in LstAllSat)
                {
                    if (items.Date <= Convert.ToDateTime(FullEndDateForRecurrence))
                    {
                        Vt_Tbl_HolidaysDates HolidayDate = new Vt_Tbl_HolidaysDates();
                        HolidayDate.HolidayScheduleID = EnteredDataID;
                        HolidayDate.HolidayDate = items.ToString();
                        HolidayDate.DesignationID = designationID;
                        LstHolidaysSat.Add(HolidayDate);
                        //db.Vt_Tbl_HolidaysDates.Add(HolidayDate);

                        ////LstHolidaysSat.Add(HolidayDate);
                        ////LstHolidaysSat.ForEach(n => db.Vt_Tbl_HolidaysDates.Add(n));
                        //db.SaveChanges();

                    }

                    //For all saturday
                }
                DataTable dt = ToDataTable(LstHolidaysSat);
                if (dt.Rows.Count > 0)
                {
                    using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                    {
                        connection.Open();

                        bcp.DestinationTableName = "[Vt_Tbl_HolidaysDates]";

                        bcp.ColumnMappings.Add("HolidayScheduleID", "HolidayScheduleID");
                        bcp.ColumnMappings.Add("HolidayDate", "HolidayDate");
                        bcp.ColumnMappings.Add("DesignationID", "DesignationID");

                        bcp.WriteToServer(dt);
                        connection.Close();
                    }


                }


            }

        }

        return "Success";
    }



}

public class DxSchedular
{
    public int ID { get; set; }
    public string allDay { get; set; }
    public string text { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string recurrenceRule { get; set; }
    public string repeatOnOff { get; set; }
}