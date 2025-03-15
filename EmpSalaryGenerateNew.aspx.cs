using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmpSalaryGenerateNew : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
       // MonthBind();
    }

    protected void btnEmploySalaryGenClick(object sender, EventArgs e)
    {
        //var companyID =vt_Common.CheckInt(Request.QueryString["CompanyID"]);
        //var queryStrMonth = Request.QueryString["month"];
        //var credtbyEmp = ((EMS_Session)Session["EMS_Session"]).Employee.EmployeeID;
        try
        {

         
                var companyID = vt_Common.CheckInt(Session["CompanyId"]);
                var queryStrMonth = Request.QueryString["month"];
                var credtbyEmp = Convert.ToInt32(Session["UserId"]);
                int selectedMonth = vt_Common.CheckInt(queryStrMonth.Substring(0, 2));
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    var Obj_SalaryGenerated = db.vt_tbl_CompanyMonthlySalary.Where(x => x.CompanyID == companyID && x.MonthOfSalaryGen == selectedMonth).FirstOrDefault();
                    if (Obj_SalaryGenerated != null)
                    {
                        MsgBox.Show(Page, MsgBox.danger, "Salary Generation", "Salary already been generated for this month");
                    }
                    else
                    {
                        if (companyID > 0)
                        {
                            int month = selectedMonth;
                            int year = DateTime.Now.Year;
                            DateTime startDate = new DateTime(year, month, 1);
                            var getRunSalaries = db.vt_tbl_salaryRecords.Where(x => x.CompanyID == companyID && x.SalaryMonth == startDate).ToList();

                            if (getRunSalaries.Count == 0)
                            {
                                string sdate = startDate.ToString("MM/dd/yyyy");
                                startDate = DateTime.ParseExact(sdate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                                /* @currentMonthDate datetime = '10/01/2018',
                                  @CompanyID int = 1,
                                  @CreatedBy int = 2,
                                  @CreatedDate datetime 
                                  pass these values */
                                var runSalaryGen = ProcedureCall.Sp_Call_SetEmpSalaries(startDate, companyID.ToString(), credtbyEmp, DateTime.Now);
                                if (runSalaryGen != 0)
                                {
                                    MsgBox.Show(Page, MsgBox.success, "Salary Generation", "Successfully Run ...");
                                    SendEmails();
                                }
                                else
                                {
                                    DateTime dtDate = new DateTime(2000, month, 1);
                                    string sMonthName = dtDate.ToString("MMM");
                                    string sMonthFullName = dtDate.ToString("MMMM");
                                    MsgBox.Show(Page, MsgBox.danger, "!Sorry " + sMonthFullName, "Attendace not exist Salary could not be generated...");
                                }



                            }
                            else
                            {
                                MsgBox.Show(Page, MsgBox.success, "Salary Generation", "Already Run on " + queryStrMonth + " ...");
                            }
                        }
                        else
                        {
                            MsgBox.Show(Page, MsgBox.success, "Salary Generation", "Please Pick Company Name and Month ...");
                        }
                    }
                }

            
          
        }
        catch (Exception ex)
        {

            throw;
        }
     
    }

    bool SendEmails()
    {
        string ToEmail = String.Empty;
        var CompanyID = vt_Common.CheckInt(Session["CompanyId"]);
        vt_EMSEntities db = new vt_EMSEntities();
        var Data = db.vt_tbl_Employee.Where(x => x.CompanyID == CompanyID && x.Email != null).ToList();
        if (Data.Count > 0)
        {
            foreach (var item in Data)
            {
                if (item.Email != null)
                {
                    ToEmail += item.Email + ",";
                }
            }

            ToEmail = ToEmail.Substring(0, ToEmail.Length - 1);
        }
        if (!String.IsNullOrWhiteSpace(ToEmail))
        {
           // return EmailSender.SendEmail("salman.tahir@viftech.com.pk", ToEmail, "", "", "Salary Notification", "<h2>Your monthly salary has been released</h2>", null);
        }

        return false;



    }

}