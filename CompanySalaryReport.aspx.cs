using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;




public partial class EmailSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }


    #region ControlEvents



    # endregion

    #region Healper Method

    public void loadData()
    {

        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

               
            }
        }

    }

    #endregion




    protected void submit_Click(object sender, EventArgs e)
    {

        
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
    protected void btnEmploySalaryGenClick(object sender, EventArgs e)
    {
        var companyID =vt_Common.CheckInt(Request.QueryString["CompanyID"]);
        var queryStrMonth = Request.QueryString["month"];
        var credtbyEmp = ((EMS_Session)Session["EMS_Session"]).Employee.EmployeeID;

        int selectedMonth = vt_Common.CheckInt(queryStrMonth.Substring(0,2));
        using (vt_EMSEntities db = new vt_EMSEntities())
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
                    var runSalaryGen = db.VT_SP_SetEmpSalaries(startDate, companyID, credtbyEmp, DateTime.Now);
                    MsgBox.Show(Page, MsgBox.success, "Salary Generation", "Successfully Run ...");
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


    [WebMethod]
    [ScriptMethod]
    public static dynamic getEmpList()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            return db.vt_tbl_Employee.ToList().Select(x => new string[] { x.EmployeeID.ToString(), x.EmployeeName });
        }
    }
}