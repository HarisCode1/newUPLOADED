using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class Default2 : System.Web.UI.Page
{
    private vt_EMSEntities dbContext = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                int Employeeid = Convert.ToInt32(Request.QueryString["ID"]);
                if (Employeeid == 0)
                {
                    Response.Redirect("default.aspx");
                }
                DataTable dt1 = ProcedureCall.VT_SP_GetEmployee_ByID(Employeeid).Tables[0];
                lblid.Text = dt1.Rows[0]["EmployeeID"].ToString();
                lblname.Text = dt1.Rows[0]["FirstName"].ToString() + " " + dt1.Rows[0]["LastName"].ToString();
                lbldesignation.Text = dt1.Rows[0]["Designation"].ToString();
                lbldepartment.Text = dt1.Rows[0]["Department"].ToString();
                //lblleavededuction.Text= dt1.Rows[0]["LeaveDeduction"].ToString();
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                DataTable dt = ProcedureCall.SP_GetSalaryCustom(Companyid, Employeeid).Tables[0];
                if (dt.Rows.Count >0)
                {
                    lblSalary.Text = dt.Rows[0]["GrossSalary"].ToString() == ""?"0.00":dt.Rows[0]["GrossSalary"].ToString();
                    lblleavededuction.Text = dt.Rows[0]["LeaveDeduction"].ToString() == ""?"0.00": dt.Rows[0]["LeaveDeduction"].ToString();

                    vt_tbl_EndOfServices endofservice = dbContext.vt_tbl_EndOfServices.Where(x => x.EmpId == Employeeid).FirstOrDefault();
                    // lblassetcondition.Text = endofservice.Description;
                    //  int grosssalary =Convert.ToInt32(lblSalary.Text);

                    //Gratuity Calculation Starts

                    var currentMonthDate = DateTime.Now;

                    int CompanyID = Convert.ToInt32(Session["CompanyId"]);
                    //   int EmployeeId = 3168;
                    string grosssalaries = string.Empty;
                    string basicsalaries = string.Empty;
                    string salariesTypecheck = string.Empty;
                    var joioningdate = "";
                    grosssalaries = dt.Rows[0]["GrossSalary"].ToString() == ""?"0.00":dt.Rows[0]["GrossSalary"].ToString();
                    basicsalaries = dt.Rows[0]["BasicSalary"].ToString() == ""? "0.00": dt.Rows[0]["BasicSalary"].ToString();
                    joioningdate = dt.Rows[0]["JoiningDate"].ToString();
                    // lblPF.Text = dt.Rows[0]["TotalPF"].ToString();
                    lblBonus.Text = dt.Rows[0]["Bonus"].ToString();
                    var totalsalry_pf_bonu = ((int.Parse(lblSalary.Text) + double.Parse(lblBonus.Text)));
                    int sal = Convert.ToInt32(totalsalry_pf_bonu);
                    //decimal gratuities = Convert.ToDecimal(lblgratuity.Text);
                    var totalsalry_pf_bonu_gratuity = sal;// + gratuities;
                    lbltotalsalary.Text = Convert.ToDecimal(totalsalry_pf_bonu_gratuity).ToString();

                    //Deduction Calculation
                    decimal totalsalarywithgratuity = Convert.ToDecimal(lbltotalsalary.Text);
                    lblGrossForDeduction.Text = Convert.ToDecimal(totalsalarywithgratuity).ToString("#,000");
                    decimal grossfordeducttion = Convert.ToDecimal(lblGrossForDeduction.Text);
                    lblLoan.Text = dt.Rows[0]["ReamainingLoanAmount"].ToString();
                    vt_tbl_EndOfServices eos = dbContext.vt_tbl_EndOfServices.Where(w => w.EmpId.Equals(Employeeid)).FirstOrDefault();
                    //   lblDepreciation.Text = eos.DepreciationAmount.ToString();
                    decimal leavededuction = Convert.ToDecimal(lblleavededuction.Text);
                    //decimal depreciationamount = Convert.ToDecimal(lblDepreciation.Text);
                    decimal TotalSalary_AfterDeduction = totalsalarywithgratuity - leavededuction;
                    LblTotal.Text = TotalSalary_AfterDeduction.ToString("0 , 0.00");
                    LblTotal.Text = (grossfordeducttion - decimal.Parse(lblLoan.Text)).ToString();
                    ////int Employeeid = Convert.ToInt32(Session["UserId"]);
                    //DataTable dt = ProcedureCall.SpCall_VT_SP_GetEmpSalaries6Feb2019(Employeeid, Companyid).Tables[0];
                    //s = dt.Rows[0][""];
                }



            }
        }
    }
    void GratyuityCalculation()
    {
        //        //Gratuity Calculation
        //        int Employeeid = Convert.ToInt32(Request.QueryString["ID"]);
        //        var currentMonthDate = DateTime.Now;

        //        int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        //        //   int EmployeeId = 3168;
        //        string grosssalaries = string.Empty;
        //        string basicsalaries = string.Empty;
        //        string salariesTypecheck = string.Empty;
        //        int NumberOfDays = 0;
        //        int yearsId = 0;
        //        var joioningdate = "";
        //        var empp = (from emp in dbContext.vt_tbl_Employee
        //                    join g in dbContext.vt_tbl_Gratuity on emp.DesignationID equals g.EmpId
        //                    where emp.EmployeeID == Employeeid
        //                    select g.EmpId).FirstOrDefault();
        //        int des = empp;
        //        vt_tbl_Gratuity gratuity = dbContext.vt_tbl_Gratuity.Where(x => x.EmpId == des).FirstOrDefault();
        //        salariesTypecheck = gratuity.SalaryType;
        //      //  NumberOfDays = Convert.ToInt32(gratuity.Days);
        //        yearsId = gratuity.EligibilityYearsId;

        //        vt_tbl_EligibilityYears eligibleyear = dbContext.vt_tbl_EligibilityYears.Where(x => x.Id.Equals(yearsId)).FirstOrDefault();
        //        int elgyr = Convert.ToInt32(eligibleyear.EligibilityYears);
        //        int yearmultiple = elgyr * 365;
        //          DataTable dt = ProcedureCall.SP_GetSalaryCustom(CompanyID, Employeeid).Tables[0];
        //        grosssalaries = dt.Rows[0]["GrossSalary"].ToString();
        //        basicsalaries = dt.Rows[0]["BasicSalary"].ToString();
        //        joioningdate = dt.Rows[0]["JoiningDate"].ToString();

        //        //Days caluculation
        //        DateTime joiningdate = DateTime.Parse(joioningdate);
        //        DateTime d2 = DateTime.Now;
        //        TimeSpan ts = d2.Subtract(joiningdate);
        //        var countdays = ts.Days;
        //        //if (countdays >= yearmultiple)
        //        //{
        //        if (salariesTypecheck == "Gross")
        //        {
        //            int grosssalary = Convert.ToInt32(grosssalaries);
        //            int elgyears = Convert.ToInt32(eligibleyear.EligibilityYears);
        //            decimal cal = (grosssalary / 30m);
        //            decimal mulbydays = (cal * NumberOfDays);
        //            decimal mulbyservedyear = (mulbydays * elgyr);
        //            lblgratuity.Text = Convert.ToDecimal(mulbyservedyear).ToString("0 , 0.00");

        //        }
        //        else if (salariesTypecheck == "Basic")
        //        {
        //            int basicsalary = Convert.ToInt32(basicsalaries);
        //            int elgyears = Convert.ToInt32(eligibleyear.EligibilityYears);
        //            decimal cal = (basicsalary / 30m);
        //            decimal mulbydays = (cal * NumberOfDays);
        //            decimal mulbyservedyear = (mulbydays * elgyr);
        //            lblgratuity.Text = Convert.ToInt32(mulbyservedyear).ToString("0 , 0.00");
        //        }

        //}
    }
  protected void btnValidate_Click(object sender, EventArgs e)

     {
        int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        bool isresigned = true; 
        string status = "Resigned";
        string jobstatus = "InActive";
        int Employeeid = Convert.ToInt32(Request.QueryString["ID"]);
        if (Employeeid != 0)
        {
            vt_tbl_Employee employee = dbContext.vt_tbl_Employee.Where(w => w.EmployeeID.Equals(Employeeid)).SingleOrDefault();
            employee.IsLeft = true;
            employee.JobStatus = jobstatus;
            dbContext.Entry(employee).State =System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            vt_tbl_Resignations resg = dbContext.vt_tbl_Resignations.Where(x => x.EmployeeId.Equals(Employeeid)).FirstOrDefault();
            resg.Status = status;
            dbContext.SaveChanges();
            vt_tbl_EndOfServices eos = new vt_tbl_EndOfServices();
            eos.EmpId = Employeeid;
            eos.Name = lblname.Text;
            eos.Designation = lbldesignation.Text;
            eos.Department = lbldepartment.Text;
            eos.CompanyId = CompanyID;
            //eos.Description = txtdescription.InnerText;
            //eos.DepreciationAmount = Convert.ToDecimal(txtdepreciation.Text);
            eos.TerminationDate = DateTime.Now;
            eos.CreatedDate = DateTime.Now;
            dbContext.vt_tbl_EndOfServices.Add(eos);
           dbContext.SaveChanges();
            vt_tbl_Employee emp = dbContext.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(Employeeid)).FirstOrDefault();

            emp.IsResigned = isresigned;
            dbContext.SaveChanges();
            Response.Redirect("ResignedEmployee.aspx");

            MsgBox.Show(Page, MsgBox.success, "Message ", "EOS Terminated");
        }
    
    }

}