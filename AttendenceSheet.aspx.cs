using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class AttendenceSheet : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    DateTime enterydate = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                loadData();
                BindCompany();
                BindDepartment();
                txtDate.Text = enterydate.ToString("01/MM/yyyy");
            }
            vt_Common.ReloadJS(this.Page, "binddata();");
        }

    }
    public void BindDepartment()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        if (Companyid != 0)
        {
            string constr = vt_Common.PayRollConnectionString;
            DataTable dt = SqlHelper.ExecuteDataset(constr, CommandType.Text, "select * from vt_tbl_Department where CompanyID='" + Companyid + "'").Tables[0];
            DDldepartment.DataSource = dt;
            DDldepartment.DataValueField = "DepartmentID";
            DDldepartment.DataTextField = "Department";
            DDldepartment.DataBind();
            DDldepartment.Items.Insert(0, new ListItem("Please Select Department", "0"));

        }
   
    }
    public void BindCompany()
    {
        int CompanyId = Convert.ToInt32(Session["CompanyID"]);
        if (CompanyId != 0)
        {
            var query = db.vt_tbl_Company.Where(x => x.CompanyID == CompanyId).Select(x => new
            {
                x.CompanyID,
                x.CompanyName
            });
            if (query != null)
            {
                ddlCompany.DataSource = query.ToList();
                ddlCompany.DataValueField = "CompanyID";
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataBind();
            }

        }
    
    }
    #region ControlEvents
    # endregion
    #region Healper Method
    public void loadData()
    {
        //if (Session["EMS_Session"] != null)
        //{
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //var data = db.VT_SP_GetEmpSalary_Slip(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue)).ToList();
            ////string strQuery = "SELECT * FROM tblStudent";
            ////SqlDataAdapter da = newSqlDataAdapter(strQuery, con);
            ////DataTable dt = newDataTable();
            ////da.Fill(dt);
            ////RDLC ds = newRDLC();
            ////ds.Tables["tblStudent"].Merge(dt);
            //ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("SalarySlip.rdlc");
            //ReportDataSource datasource = new ReportDataSource("SalaryData", data);
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(datasource);  
            //if (((EMS_Session)Session["EMS_Session"]).Company == null)
            //{
            //    var Query = db.vt_tbl_EmployeeAdvSalary.Select(x => new
            //    {
            //        EmployeeName = x.vt_tbl_Employee.EmployeeName,
            //        SalaryOfMonth = x.SalaryOfMonth,
            //        x.SalaryAmount,
            //        x.AdvSalaryReleaseDate,
            //        x.AdvSalaryID
            //    }).ToList();
            //}
            //else
            //{
            //    divCompany.Visible = false;
            //    var Query = db.vt_tbl_EmployeeAdvSalary.Where(x => x.CompanyID == vt_Common.CompanyId).Select(x => new
            //    {
            //        EmployeeName = x.vt_tbl_Employee.EmployeeName,
            //        SalaryOfMonth = x.SalaryOfMonth,
            //        x.SalaryAmount,
            //        x.AdvSalaryReleaseDate,
            //        x.AdvSalaryID
            //    }).ToList();
            //    grdEmailSetting.DataSource = Query;
            //    grdEmailSetting.DataBind();
            //}
        }
        //}
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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //int Employee = -1;
            //if (!string.IsNullOrEmpty(txtEmpID.Text))
            //{
                //Employee = Convert.ToInt32(txtEmpID.Text);
            //}
           
            int EmployeeId = Convert.ToInt32(Session["UserId"]);
            //var QryEmployeID = db.vt_tbl_User.Where(x => x.UserId == EmployeeId).FirstOrDefault();
            int roleID = Convert.ToInt32(Session["RoleId"]);

            
            var roleDetail = db.vt_tbl_Role.Where(x => x.RoleID == roleID).FirstOrDefault();
            var data = (dynamic)null;


            //if (EmployeeId != 0)
            //{

                if (roleID == 0)
                {
                    data = db.VT_SP_GetEmpAttendanceReport_SuperAdmin(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(DDldepartment.SelectedValue)).ToList();
                }
                else
                {
                    data = db.VT_SP_GetEmpAttendanceReport(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), EmployeeId).ToList();

                }


            //    //data = db.VT_SP_GetEmpAttendanceReport(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(Session["UserId"])).ToList();
            //}
            //else if (roleID == 0)
            //{
            //    data = db.VT_SP_GetEmpAttendanceReport_SuperAdmin(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(DDldepartment.SelectedValue)).ToList();
            //}
            //else
            //{
            //    //data = db.VT_SP_GetEmpAttendanceReport(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(Session["UserId"])).ToList();

            //    data = db.VT_SP_GetEmpAttendanceReport_SuperAdmin(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(DDldepartment.SelectedValue)).ToList();
            //}




            //if (EmployeeId == 0)
            //{
            //    data = db.VT_SP_GetEmpAttendanceReport(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(Session["UserId"])).ToList();
            //}
            //else if (roleID == 0)
            //{
            //    data = db.VT_SP_GetEmpAttendanceReport_SuperAdmin(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(DDldepartment.SelectedValue)).ToList();
            //}
            //else
            //{
            //    data = db.VT_SP_GetEmpAttendanceReport(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(Session["UserId"])).ToList();

            //    //data = db.VT_SP_GetEmpAttendanceReport_SuperAdmin(Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(DDldepartment.SelectedValue)).ToList();
            //}

            //var
            if (data != null)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                LocalReport localReport = ReportViewer1.LocalReport;
                /* Get Path of RDLC */
                //string rdlPath = @"D:\Wasif Ali\Projects\Payrollproject\Payroll\SalarySlip.rdlc";
                //localReport.ReportPath = rdlPath;
                DataSet dataset = new DataSet();
                /*Get this from RDL file DataSource Name 
                 This will be in loop*/
                string DataSetName = "DataSet1";
                ReportDataSource rptDataSource = new ReportDataSource();
                rptDataSource.Name = DataSetName;
                rptDataSource.Value = data;
                localReport.DataSources.Clear();
                localReport.DataSources.Add(rptDataSource);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('._Administration').hide();$('._Employee').hide();$('._Attendance').hide();$('._InputModules').hide();$('._SalaryGen').hide();$('._SalaryGenReport').hide();", false);
                
            }

        }
    }

}