using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Calender : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {

        }
        vt_Common.ReloadJS(this.Page, "BindData();");
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var ProbationEmployee = db.vt_tbl_Employee.Where(x => x.EmployeeID == 270).SingleOrDefault();

            //txtEmployeeName.Text = ProbationEmployee.EmployeeName;
            //txtJoiningDate.Text = ProbationEmployee.JoiningDate.ToString();
            //txtProbationPeriod.Text = ProbationEmployee.ProvisionalPeriod;
        }
        vt_Common.ReloadJS(this.Page, "$('#employeeCon').modal();");
        //UpDetail.Update();
        

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}