using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmployeeSalaryGenerationDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_submitdemo_Click(object sender, EventArgs e)
    {
        var companyID = vt_Common.CheckInt(Session["CompanyId"]);
        DateTime queryStrMonth = Convert.ToDateTime(Request.QueryString["month"]);
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            string Obj_SalaryGenerated = null;// db.sala.Where(x => x.CompanyID == companyID && x.MonthOfSalaryGen == selectedMonth).FirstOrDefault();
            if (Obj_SalaryGenerated != null)
            {
                MsgBox.Show(Page, MsgBox.danger, "Salary Generation", "Salary already been generated for this month");
            }
            else
            {
                var gen = ProcedureCall.SP_GenerateSalaryDemoLink(queryStrMonth, companyID);
                MsgBox.Show(Page, MsgBox.success, "Salary ", "Generated Successfully");
            }
        }
    }
}