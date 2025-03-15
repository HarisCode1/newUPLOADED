using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Viftech;

public partial class WhMonitor : System.Web.UI.Page
{
    static int CompanyID = 0;
    static int Hours = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                CompanyID =Convert.ToInt32(Session["CompanyID"]);
            }
        }
    }

    private bool SendEmail()
    {
        string Subject = "Test Email";
        string ToEmail = "riaz.viftech@gmail.com";
        string BccEmail = string.Empty;

        List<string> AttachedFiles = new List<string>();
        AttachedFiles.Add(Server.MapPath(@"Uploads/Attachments/Employees.xlsx"));
        AttachedFiles.Add(Server.MapPath(@"Uploads/Attachments/Test.xlsx"));

        string Body = string.Empty;
        using (StreamReader Reader = new StreamReader(Server.MapPath("~/Uploads/EmailTemplates/TestEmail.html")))
        {
            Body = Reader.ReadToEnd();
        }

        return EmailSender.SendEmail(ToEmail, "", BccEmail, Subject, Body, AttachedFiles);
    }

    //[ScriptMethod]
    //[WebMethod]
    //public static List<vt_tbl_Attendance> getAttendance(int? id)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        //if (id == 0)
    //        {
    //            List<vt_tbl_Attendance> data = db.vt_tbl_Attendance.ToList().Select(h => new vt_tbl_Attendance
    //            {
    //                TotalHrs = h.TotalHrs,
    //                Date = h.Date
    //                //title = h.HolidayName,
    //                //start = h.FromDate == null ? "" : vt_Common.CheckDateTime(h.FromDate).ToString(),
    //                //end = h.ToDate == null ? "" : vt_Common.CheckDateTime(h.ToDate).ToString().Replace("AM", "PM")
    //            }).ToList();

    //            return data;
    //        }

    //    }
    //}

    [ScriptMethod]
    [WebMethod]
    public static List<WorkingHours> getAttendance(int? id)
    {
        
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            {
                var qrycompanyHrs = db.vt_tbl_Company.Where(x => x.CompanyID == CompanyID).FirstOrDefault();
                if (qrycompanyHrs != null)
                {
                    Hours =Convert.ToInt32(qrycompanyHrs.HoursInDay);
                }
                WorkingHours Obj = new WorkingHours();
                List<WorkingHours> LstIn = db.vt_tbl_Attendance.Where(x => x.EmployeeID == id).ToList().Select(h => new WorkingHours
                {
                    title = "In Time: " + vt_Common.CheckDateTime(h.InTime).ToString("hh:mm tt"),
                    start = h.Date == null ? "" : vt_Common.CheckDateTime(h.Date).ToString(),
                }).ToList();
                List<WorkingHours> LstOut = db.vt_tbl_Attendance.Where(x => x.EmployeeID == id).ToList().Select(h => new WorkingHours
                {
                    title = "Out Time: " + vt_Common.CheckDateTime(h.OutTime).ToString("hh:mm tt"),
                    start = h.Date == null ? "" : vt_Common.CheckDateTime(h.Date).ToString(),
                }).ToList();

                List<WorkingHours> LstStatus = db.vt_tbl_Attendance.Where(x => x.EmployeeID == id).ToList().Select(h => new WorkingHours
                {
                    title = Convert.ToInt32(vt_Common.CheckDateTime(h.OutTime).ToString("HH")) - Convert.ToInt32(vt_Common.CheckDateTime(h.InTime).ToString("HH")) - Hours > 0 ? "Status : Overtime" :
                    Convert.ToInt32(vt_Common.CheckDateTime(h.OutTime).ToString("HH")) - Convert.ToInt32(vt_Common.CheckDateTime(h.InTime).ToString("HH")) - Hours < 0 ? "Status : Early Leave" :
                    "Status : Normal",
                    //title = Convert.ToInt32(vt_Common.CheckDateTime(h.OutTime).ToString("HH")) - Convert.ToInt32(vt_Common.CheckDateTime(h.InTime).ToString("HH")) - 9 > 0 ? "Status : Overtime" : 
                    //"Status : Normal",
                    start = h.Date == null ? "" : vt_Common.CheckDateTime(h.Date).ToString(),
                    //color = Convert.ToInt32(vt_Common.CheckDateTime(h.OutTime).ToString("HH")) - Convert.ToInt32(vt_Common.CheckDateTime(h.InTime).ToString("HH")) - 9 > 0 ? "#de8380" : "#77acda",
                    color = Convert.ToInt32(vt_Common.CheckDateTime(h.OutTime).ToString("HH")) - Convert.ToInt32(vt_Common.CheckDateTime(h.InTime).ToString("HH")) - Hours > 0 ? "#de8380" :
                    Convert.ToInt32(vt_Common.CheckDateTime(h.OutTime).ToString("HH")) - Convert.ToInt32(vt_Common.CheckDateTime(h.InTime).ToString("HH")) - Hours < 0 ? "#818c96" :
                    "#77acda",
                }).ToList();

                List<WorkingHours> LstWorkingHours = db.vt_tbl_Attendance.Where(x => x.EmployeeID == id).ToList().Select(h => new WorkingHours
                {
                    title = "Working Hours : " + (Convert.ToInt32(vt_Common.CheckDateTime(h.OutTime).ToString("HH")) - Convert.ToInt32(vt_Common.CheckDateTime(h.InTime).ToString("HH"))).ToString(),
                    start = h.Date == null ? "" : vt_Common.CheckDateTime(h.Date).ToString(),
                }).ToList();

                var result = LstIn.Concat(LstOut);
                var result2 = result.Concat(LstStatus);
                var result3 = result2.Concat(LstWorkingHours);                
                return result3.ToList();
            }
        }
    }
}

public class WorkingHours
{
    public string title { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string color { get; set; }
}