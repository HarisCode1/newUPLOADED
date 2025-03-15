using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HolidaDelete_DAL
/// </summary>
public class HolidaDelete_DAL
{
    static string result = string.Empty;
    static string recurrencerule = string.Empty;
    static string FullDate = string.Empty;
    static int scheduleid = 0;
    public HolidaDelete_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string DeleteAllDesignationHolidays(string SID, string date, string designationID, string rules)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        if (rules == "undefined")
        {
            var qry = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == date).ToList();
            if (qry.Count > 0)
            {
                foreach (var item in qry)
                {
                    string checkdate1 = item.StartDate;
                     scheduleid = item.ID;
                    Vt_Tbl_HolidaySchedule hds = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == checkdate1).FirstOrDefault();
                    db.Vt_Tbl_HolidaySchedule.Remove(hds);
                    
                    result = "success";
                    DateTime Convertdate = Convert.ToDateTime(checkdate1);
                    string getdatewithTime = Convertdate.ToString();
                    var qry2 = db.Vt_Tbl_HolidaysDates.Where(x => x.HolidayScheduleID == scheduleid).ToList();
                    if (qry2.Count > 0)
                    {
                        List<Vt_Tbl_HolidaysDates> ObjHS = new List<Vt_Tbl_HolidaysDates>(); 
                        foreach (var item2 in qry2)
                        {
                            string checkdate2 = item2.HolidayDate;
                            Vt_Tbl_HolidaysDates hdd = db.Vt_Tbl_HolidaysDates.Where(x => x.HolidayScheduleID == scheduleid).FirstOrDefault();
                            ObjHS.Add(hdd);
                            db.Vt_Tbl_HolidaysDates.RemoveRange(ObjHS);
                           
                        }
                     


                    }
                 //   db.SaveChanges();
                    result = "success";
                }
            }
        }//end if
        return result;

    }


    public static string DeleteAllDesignationSaturday(string SID, string date, string designationID, string rules)
    {
        var month = Convert.ToDateTime(date).Month;
        var day = Convert.ToDateTime(date).Day;
        var Year = Convert.ToDateTime(date).Year;
        vt_EMSEntities db = new vt_EMSEntities();
        List<Vt_Tbl_HolidaysDates> ObjHS = new List<Vt_Tbl_HolidaysDates>();
        Vt_Tbl_HolidaySchedule hs = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == date).FirstOrDefault();
        if (rules.Contains("SA"))
        {
            if (rules.Contains("SA") && rules.Contains("INTERVAL"))
            {
                if (hs != null)
                {
                    recurrencerule = hs.RecurrenceRule;

                }
                FullDate = CheckRecurrenceRule(recurrencerule);
                if (FullDate != "")
                {
                    var qry = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == date).ToList();

                    if (qry.Count > 0)
                    {
                        foreach (var item in qry)
                        {
                            string checkdate = item.StartDate;
                            Vt_Tbl_HolidaySchedule hdd = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == checkdate).FirstOrDefault();
                            db.Vt_Tbl_HolidaySchedule.Remove(hdd);
                            db.SaveChanges();
                            result = "success";
                        }

                    }
                    //Delete Sundays 
                    var GetSaturday = GetAllSaturdays(Year, month, day);
                    foreach (var item in GetSaturday)
                    {
                        if (item.Date <= Convert.ToDateTime(FullDate))
                        {
                            string checkdate = item.Date.ToString();
                            var Qry_hds = db.Vt_Tbl_HolidaysDates.Where(x => x.HolidayDate == checkdate).ToList();
                            foreach (var EachDate in Qry_hds)
                            {
                                ObjHS.Add(EachDate);
                                db.Vt_Tbl_HolidaysDates.RemoveRange(ObjHS);
                                //db.Vt_Tbl_HolidaysDates.Remove(EachDate);
                              
                            }
                            db.SaveChanges();
                            result = "success";

                        }



                    }
                }


            }
            else
            {
                if (hs != null)
                {
                    recurrencerule = hs.RecurrenceRule;

                }
                FullDate = CheckRecurrenceRule(recurrencerule);
                if (FullDate != "")
                {
                    var qry = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == date).ToList();

                    if (qry.Count > 0)
                    {
                        foreach (var item in qry)
                        {
                            string checkdate = item.StartDate;
                            Vt_Tbl_HolidaySchedule hdd = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == checkdate).FirstOrDefault();
                            db.Vt_Tbl_HolidaySchedule.Remove(hdd);
                            db.SaveChanges();
                            result = "success";
                        }

                    }

                    var GetSaturday = GetAllSaturdays(Year, month, day);
                    foreach (var item in GetSaturday)
                    {
                        if (item.Date <= Convert.ToDateTime(FullDate))
                        {
                            string checkdate = item.Date.ToString();
                            var Qry_hds = db.Vt_Tbl_HolidaysDates.Where(x => x.HolidayDate == checkdate).ToList();
                            foreach (var EachDate in Qry_hds)
                            {
                                ObjHS.Add(EachDate);
                                db.Vt_Tbl_HolidaysDates.RemoveRange(ObjHS);
                                //db.Vt_Tbl_HolidaysDates.Remove(EachDate);
                               
                            }
                            db.SaveChanges();
                            result = "success";

                        }



                    }
                }
            }
        }
        return result;
    }

    public static string DeleteAllDesignationSunday(string SID, string date, string designationID, string rules)
    {
        var month = Convert.ToDateTime(date).Month;
        var day = Convert.ToDateTime(date).Day;
        var Year = Convert.ToDateTime(date).Year;
        vt_EMSEntities db = new vt_EMSEntities();
        Vt_Tbl_HolidaySchedule hs = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == date).FirstOrDefault();
        List<Vt_Tbl_HolidaysDates> ObjH = new List<Vt_Tbl_HolidaysDates>();
        if (rules.Contains("SU"))
        {

            if (hs != null)
            {
                recurrencerule = hs.RecurrenceRule;

            }
            FullDate = CheckRecurrenceRule(recurrencerule);
            if (FullDate != "")
            {
                var qry = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == date).ToList();

                if (qry.Count > 0)
                {
                    foreach (var item in qry)
                    {
                        string checkdate = item.StartDate;
                        Vt_Tbl_HolidaySchedule hdd = db.Vt_Tbl_HolidaySchedule.Where(x => x.StartDate == checkdate).FirstOrDefault();
                        db.Vt_Tbl_HolidaySchedule.Remove(hdd);
                        db.SaveChanges();
                        result = "success";
                    }

                }
                //Delete Sundays 
                var Getsundays = GetAllSundays(Year, month, day);
                foreach (var item in Getsundays)
                {
                    if (item.Date <= Convert.ToDateTime(FullDate))
                    {
                        string checkdate = item.Date.ToString();
                        var Qry_hds = db.Vt_Tbl_HolidaysDates.Where(x => x.HolidayDate == checkdate).ToList();
                        foreach (var EachDate in Qry_hds)
                        {
                            ObjH.Add(EachDate);
                            db.Vt_Tbl_HolidaysDates.RemoveRange(ObjH);
                          
                        }
                        db.SaveChanges();
                        result = "success";

                    }



                }
            }
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
    public static List<DateTime> GetAllSundays(int year, int month, int day)
    {
        var result = new List<DateTime>();
        for (var date = new DateTime(year, month, day); date.Year == year; date = date.AddDays(1))
        {
            if (!(new[] { 7, 0 }).Contains((int)date.DayOfWeek)) continue;
            result.Add(date);
            if (date.DayOfWeek == 0) date = date.AddDays(6); // skip mon trough fri
        }
        return result;
    }
    public static string CheckRecurrenceRule(string recurrenceRule)
    {
        string extract = string.Empty;
        string date = string.Empty;
        string year = string.Empty;
        string days = string.Empty;
        string months = string.Empty;
        string fullEndDate = string.Empty;
        string FullDate = string.Empty;
        int count = recurrenceRule.Substring(0).Count();//Count to extract date through subtring
        if (21 <= count)
        {
            extract = recurrenceRule.Substring(21);
            if (extract != "INTERVAL=2")
            {
                if (recurrenceRule.Contains("UNTIL") && recurrenceRule.Contains("INTERVAL"))
                {
                    date = recurrenceRule.Substring(38);
                    year = date.Substring(0, 4);
                    months = date.Substring(4, 2);
                    days = date.Substring(6, 2);
                    fullEndDate = year + "-" + months + "-" + days;
                    FullDate = fullEndDate;
                }
                else
                {
                    if (recurrenceRule.Contains("SA") && recurrenceRule.Contains("UNTIL"))
                    {
                        date = recurrenceRule.Substring(27);
                        year = date.Substring(0, 4);
                        months = date.Substring(4, 2);
                        days = date.Substring(6, 2);
                        fullEndDate = year + "-" + months + "-" + days;
                        FullDate = fullEndDate;
                    }
                    else if (recurrenceRule.Contains("SU") && recurrenceRule.Contains("UNTIL"))
                    {
                        date = recurrenceRule.Substring(27);
                        year = date.Substring(0, 4);
                        months = date.Substring(4, 2);
                        days = date.Substring(6, 2);
                        fullEndDate = year + "-" + months + "-" + days;
                        FullDate = fullEndDate;
                        //date = recurrenceRule.Substring(27);
                        //year = date.Substring(0, 4);
                        //months = date.Substring(4, 2);
                        //days = date.Substring(6, 2);
                        //fullEndDate = year + "-" + months + "-" + days;
                        //FullDate = fullEndDate;
                    }
                    else
                    {
                        FullDate = "";
                    }


                }

            }

        }
        return FullDate;
    }
}