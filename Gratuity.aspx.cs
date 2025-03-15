using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Gratuity : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    AssetsAssign_BAL BAL = new AssetsAssign_BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {

            if (!IsPostBack)
            {
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                BindDropDown();
                Load();
                gratuitycalculate();




            }
        }

    }
    void gratuitycalculate()
    {
        try
        {
            var currentMonthDate = DateTime.Now;

            int CompanyID = Convert.ToInt32(Session["CompanyId"]);
            int EmployeeId = 3168;
            string grosssalaries = string.Empty;
            string basicsalaries = string.Empty;
            string salariesTypecheck = string.Empty;
            int NumberOfDays = 0;
            int yearsId = 0;
            var joioningdate = "";
            var empp = (from emp in db.vt_tbl_Employee
                        join g in db.vt_tbl_Gratuity on emp.DesignationID equals g.EmpId
                        where emp.EmployeeID == EmployeeId
                        select g.EmpId).FirstOrDefault();
            int des = empp;
            vt_tbl_Gratuity gratuity = db.vt_tbl_Gratuity.Where(x => x.EmpId == des).FirstOrDefault();
            salariesTypecheck = gratuity.SalaryType;
          //  NumberOfDays = Convert.ToInt32(gratuity.Days);
            yearsId = gratuity.EligibilityYearsId;

            vt_tbl_EligibilityYears eligibleyear = db.vt_tbl_EligibilityYears.Where(x => x.Id.Equals(yearsId)).FirstOrDefault();
            int elgyr = Convert.ToInt32(eligibleyear.EligibilityYears);
            int yearmultiple = elgyr * 365;
            DataTable dt = ProcedureCall.SP_GetSalaryCustom(CompanyID, EmployeeId).Tables[0];
            grosssalaries = dt.Rows[0]["GrossSalary"].ToString();
            basicsalaries = dt.Rows[0]["BasicSalary"].ToString();
            joioningdate = dt.Rows[0]["JoiningDate"].ToString();

            //Days caluculation
            DateTime joiningdate = DateTime.Parse(joioningdate);
            DateTime d2 = DateTime.Now;
            TimeSpan ts = d2.Subtract(joiningdate);
            var countdays = ts.Days;
            //if (countdays >= yearmultiple)
            //{
            if (salariesTypecheck == "Gross")
            {
                int grosssalary = Convert.ToInt32(grosssalaries);
                int elgyears = Convert.ToInt32(eligibleyear.EligibilityYears);
                decimal cal = (grosssalary / 30m);
                decimal mulbydays = (cal * NumberOfDays);
                decimal mulbyservedyear = (mulbydays * elgyr);
                lblgrt.Text = Convert.ToDecimal(mulbyservedyear).ToString("#,000");

            }
            else if (salariesTypecheck == "Basic")
            {
                int basicsalary = Convert.ToInt32(basicsalaries);
                int elgyears = Convert.ToInt32(eligibleyear.EligibilityYears);
                decimal cal = (basicsalary / 30m);
                decimal mulbydays = (cal * NumberOfDays);
                decimal mulbyservedyear = (mulbydays * elgyr);
                lblgrt.Text= Convert.ToInt32(mulbyservedyear).ToString("#,000");
            }

            //}
            //else
            //{
            //    lblgrt.Text = "Sorry Not Eligible for Gratuity ";
            //}
            //select a;//;
            //salariesType = query.BasicSalary;

            //foreach (var item in query)
            //{

            //    salariesType = item.BasicSalary;
            //    jioningdate = item.JoiningDate;
            //    DateTime joiningdeate = DateTime.Parse(jioningdate);  //jioningdate// ?? DateTime.Now;
            //    DateTime d22 = DateTime.Now;
            //    int tss = d22.Subtract(DateTime.Parse(jioningdate)).Days;
            //    var countsdays = tss;

            //}
            //int id = 3161;
            //vt_tbl_Gratuity grt = db.vt_tbl_Gratuity.FirstOrDefault();
            //int id = grt.EmpId;

            //vt_tbl_Gratuity gratuity = db.vt_tbl_Gratuity.Where(x => x.EmpId == id).FirstOrDefault();
            ////Get Salary Type for check gross or basic
            //string salarytype = gratuity.SalaryType;
            //vt_tbl_Employee salrcd = db.vt_tbl_Employee.Where(r => r.DesignationID == id).FirstOrDefault();
            //var year = gratuity.EligibilityYearsId;
            //vt_tbl_EligibilityYears eligibleyear = db.vt_tbl_EligibilityYears.Where(x => x.Id.Equals(year)).FirstOrDefault();
            //int elgyr = Convert.ToInt32(eligibleyear.EligibilityYears);
            //int yearmultiple = elgyr * 365;
            ////Count Days 
            //DateTime joiningdate = salrcd.JoiningDate ?? DateTime.Now;
            //DateTime d2 = DateTime.Now;
            //TimeSpan ts = d2.Subtract(joiningdate);
            //var countdays = ts.Days;
            //if (countdays >= yearmultiple)
            //{
            //    if (salarytype == "Gross")
            //    {
            //        int grosssalary = Convert.ToInt32(salrcd.BasicSalary);
            //        int elgyears = Convert.ToInt32(eligibleyear.EligibilityYears);
            //        int cal = grosssalary * elgyears;
            //        int formula = (cal * 15) / 26;
            //        lblgrt.Text = Convert.ToInt32(formula).ToString();
            //    }
            //    else if (salarytype == "Basic")
            //    {
            //        int grosssalary = Convert.ToInt32(salrcd.BasicSalary);
            //        int elgyears = Convert.ToInt32(eligibleyear.EligibilityYears);
            //        int cal = grosssalary * elgyears;
            //        int formula = (cal * 15) / 26;
            //        lblgrt.Text = Convert.ToInt32(formula).ToString();
            //    }

            //}
            //else
            //{
            //    lblgrt.Text = "Sorry Not Eligible for Gratuity ";
            //}

        }
        catch (Exception ex)
        {

            throw;
        }

    }
    public void Load()
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                //var query =  db.Sp_GetGratuity(Companyid);
                var qry = from des in db.vt_tbl_Designation
                          join grt in db.vt_tbl_Gratuity on des.DesignationID equals grt.EmpId
                          join elgyr in db.vt_tbl_EligibilityYears on grt.EligibilityYearsId equals elgyr.Id
                          select new
                          {
                              grt.Id,
                              //emp.EmployeeName,
                              des.Designation,
                              elgyr.EligibilityYears,
                              grt.SalaryType,
                              grt.Description,
                            //  grt.Days,
                              grt.CreatedDate
                          };

                grdgratuity.DataSource = qry.ToList();
                grdgratuity.DataBind();




            }

        }
        catch (Exception ex)
        {

            throw;
        }

    }
    public void BindDropDown()
    {
        try
        {
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            SqlParameter[] param =
               { new SqlParameter("@Companyid",CompanyId) };
            vt_Common.Bind_DropDown(ddlalldesignation, "Sp_GetAllDesignation", "Designation", "DesignationId", param);

            //vt_Common.Bind_DropDown(ddlallemployee, "Sp_GetAllGratuityEmployees", "EmployeeName", "EmployeeID", param);
            vt_Common.Bind_DropDown(ddlallassigneddesignation, "Sp_GetAllAssignedDesignationGratuity", "Designation", "DesignationId", param);
            vt_Common.Bind_DropDown(ddlelgyears, "Sp_GetElgyears", "EligibilityYears", "Id");

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int CompanyId = Convert.ToInt32(Session["CompanyId"]);
        int grtid = Convert.ToInt32(ViewState["GrtId"]);
        string salarytype = string.Empty;
        try
        {
            if (grtid == 0)
            {
                if (rbtnbasic.Checked)
                {
                    salarytype = rbtnbasic.Text;
                }
                else if (rbtngross.Checked)
                {
                    salarytype = rbtngross.Text;
                }
                int empid = Convert.ToInt32(ddlalldesignation.SelectedValue);
                int elgyrs = Convert.ToInt32(ddlelgyears.SelectedValue);
                vt_tbl_Gratuity grt = new vt_tbl_Gratuity();
                grt.EmpId = empid;
                grt.EligibilityYearsId = elgyrs;
                grt.SalaryType = salarytype;
                grt.Description = txtdescription.Text;
                grt.CompanyId = CompanyId;
               // grt.Days = Convert.ToInt32(txtnodays.Text);
                grt.CreatedDate = DateTime.Now;
                db.vt_tbl_Gratuity.Add(grt);
                db.SaveChanges();
                vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal('hide');");
                MsgBox.Show(Page, MsgBox.success, "", "Successfully Done");
                Load();
                ClearForm();
                UpView.Update();
                Update.Update();
            }
            else
            {
                if (rbtnbasic.Checked)
                {
                    salarytype = rbtnbasic.Text;
                }
                else if (rbtngross.Checked)
                {
                    salarytype = rbtngross.Text;
                }
                int empid = Convert.ToInt32(ddlallassigneddesignation.SelectedValue);
                int elgyrs = Convert.ToInt32(ddlelgyears.SelectedValue);
                vt_tbl_Gratuity grt = db.vt_tbl_Gratuity.Where(x => x.Id.Equals(grtid)).FirstOrDefault();
                grt.EmpId = empid;
                grt.EligibilityYearsId = elgyrs;
                grt.SalaryType = salarytype;
                grt.Description = txtdescription.Text;
                grt.CompanyId = CompanyId;
                //grt.Days = Convert.ToInt32(txtnodays.Text);
                grt.CreatedDate = DateTime.Now;
                db.SaveChanges();
                vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal('hide');");
                MsgBox.Show(Page, MsgBox.success, "", "Successfully Done");
                Load();
                ClearForm();
                UpView.Update();
                Update.Update();
            }

        }

        catch (Exception ex)
        {

            throw;
        }

    }

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        if ((string)Session["UserName"] == "SuperAdmin")
        {

            vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Gratuity.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
                }
                else if (Row["PageUrl"].ToString() == "Gratuity.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
        vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal('hide')");
        Response.Redirect("Gratuity.aspx");
    }


    void ClearForm()
    {
        try
        {

            txtdescription.Text = "";
            vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        if ((string)Session["UserName"] == "SuperAdmin")
        {
            if (e.CommandArgument.ToString() != "")
            {

                int id = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["GrtId"] = id;
                vt_tbl_Gratuity grt = db.vt_tbl_Gratuity.Where(x => x.Id.Equals(id)).FirstOrDefault();
                ddlallassigneddesignation.Visible = true;
                ddlallassigneddesignation.Enabled = false;
                ddlalldesignation.Visible = false;
                ddlallassigneddesignation.SelectedValue = Convert.ToInt32(grt.EmpId).ToString();
                ddlelgyears.SelectedValue = Convert.ToInt32(grt.EligibilityYearsId).ToString();
               // txtnodays.Text = Convert.ToInt32(grt.Days).ToString();
                if (grt.SalaryType == "Gross")
                {
                    rbtngross.Checked = true;

                }
                else if (grt.SalaryType == "Basic")
                {
                    rbtnbasic.Checked = true;

                }

                txtdescription.Text = grt.Description;


                vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
                Update.Update();
            }
            vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "AssetsAssign.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    if (e.CommandArgument.ToString() != "")
                    {

                        int id = Convert.ToInt32(e.CommandArgument.ToString());
                        ViewState["GrtId"] = id;
                        vt_tbl_Gratuity grt = db.vt_tbl_Gratuity.Where(x => x.Id.Equals(id)).FirstOrDefault();
                        ddlallassigneddesignation.Visible = true;
                        ddlallassigneddesignation.Enabled = false;

                        ddlalldesignation.Visible = false;
                        ddlallassigneddesignation.SelectedValue = Convert.ToInt32(grt.EmpId).ToString();
                        ddlelgyears.SelectedValue = Convert.ToInt32(grt.EligibilityYearsId).ToString();
                        //txtnodays.Text = Convert.ToInt32(grt.Days).ToString();
                        if (grt.SalaryType == "Gross")
                        {
                            rbtngross.Checked = true;

                        }
                        else if (grt.SalaryType == "Basic")
                        {
                            rbtnbasic.Checked = true;

                        }

                        txtdescription.Text = grt.Description;


                        vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
                        Update.Update();
                    }
                    vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
                }
                else if (Row["PageUrl"].ToString() == "AssetsAssign.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }

        }
        //if ((string)Session["UserName"] == "SuperAdmin")
        //{

        //    if (e.CommandArgument.ToString() != "")
        //    {

        //        int id = Convert.ToInt32(e.CommandArgument.ToString());
        //        ViewState["GrtId"] = id;
        //        vt_tbl_Gratuity grt = db.vt_tbl_Gratuity.Where(x => x.Id.Equals(id)).FirstOrDefault();
        //        ddlallemployee.SelectedValue = Convert.ToInt32(grt.EmpId).ToString();
        //        ddlelgyears.SelectedValue = Convert.ToInt32(grt.EligibilityYearsId).ToString();
        //        txtnodays.Text =Convert.ToInt32(grt.Days).ToString();
        //        if (grt.SalaryType == "Gross")
        //        {
        //            rbtngross.Checked = true;

        //        }
        //        else if (grt.SalaryType == "Basic")
        //        {
        //            rbtnbasic.Checked = true;

        //        }

        //        txtdescription.Text = grt.Description;


        //        vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
        //        Update.Update();
        //    }
        //    vt_Common.ReloadJS(this.Page, "$('#GratuityForm').modal()");
        //}
        //else
        //{
        //    DataTable Dt = Session["PagePermissions"] as DataTable;
        //    foreach (DataRow Row in Dt.Rows)
        //    {
        //        if (Row["PageUrl"].ToString() == "Gratuity.aspx" && Row["Can_Update"].ToString() == "True")
        //        {

        //        }
        //        else if (Row["PageUrl"].ToString() == "Gratuity .aspx" && Row["Can_Update"].ToString() == "False")
        //        {
        //            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
        //        }
        //    }
        //}

    }

    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                ViewState["GrtId"] = Convert.ToInt32(e.CommandArgument);
                vt_Common.ReloadJS(this.Page, "$('#Delete').modal()");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }





    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ViewState["GrtId"]);
        vt_tbl_Gratuity grt = db.vt_tbl_Gratuity.Where(x => x.Id.Equals(id)).FirstOrDefault();
        db.vt_tbl_Gratuity.Remove(grt);
        db.SaveChanges();
        vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully  Deleted");
        Load();
        UpView.Update();
        Update.Update();
        Response.Redirect("Gratuity.aspx");
    }
}