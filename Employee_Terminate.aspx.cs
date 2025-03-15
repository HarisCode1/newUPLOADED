using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.Design;
using NPOI.SS.Formula.Functions;
public partial class Employee_Terminate : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    DateTime EntryDate = DateTime.Now;
    Custommethods customMethods = new Custommethods();
    public static int Companyid = 0;
    public static int EmployeeID = 0;
    public static int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        Companyid = Convert.ToInt32(Session["CompanyId"]);
        ID = Convert.ToInt32(Request.QueryString["ID"]);

        if (!Page.IsPostBack)
        {
                //Bind_DdlCompany();
                string empId = Request.QueryString["empid"];

               //Assigned the JoiningDate to HiddenField1, which is used to filter the Terminate Date calendar for joining dates after the current date.

                vt_tbl_Employee Emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ID);
                if (Emp != null)
                {
                    DateTime joiningDate = (DateTime)Emp.JoiningDate;
                    HiddenField1.Value = joiningDate.ToString("yyyy-MM-dd");
                }
                txtEntryDate.Text = EntryDate.ToString("dd/MM/yyyy");
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            int compID = Convert.ToInt32(Session["CompanyId"]);
            int EmployeeID = ID;
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
                termEmp.CompanyID = compID;
                termEmp.Reason = reason;
                if (UploadDocImage.HasFile)
                {
                    termEmp.Documents = "/images/TerminatedEmployees/" + EmployeeID + "-" + UploadDocImage.PostedFile.FileName;

                }
                else

                {
                    termEmp.Documents = null;
                }
                termEmp.Status = true;

                string entrydate = txtEntryDate.Text;
                DateTime? eEdate = customMethods.GetDateFromTextBox(entrydate);
                termEmp.TerminatedDate = eEdate;
                db.vt_tbl_terminatedemployees.Add(termEmp);
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessModal", "$('#successModal').modal('show');", true);
                Response.Redirect("Employee.aspx");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}