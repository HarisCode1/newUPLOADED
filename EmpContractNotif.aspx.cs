using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmployeeContractNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDetailForm();
        }
        vt_Common.ReloadJS(this.Page, "BindData();");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        update();

    }



    public void update()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            

            int id = Convert.ToInt32(Request.QueryString.Get("EmployeeID"));
            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == id);
            int ExtProbPer = txtExtendProbation.Text.Equals("") ? 0 : Convert.ToInt32(txtExtendProbation.Text);
            int PrevPeriod = vt_Common.CheckInt(emp.ProvisionalPeriod);
            int NewPer = ExtProbPer + PrevPeriod;
            var JobConfirm = "Confirmation";
            var ConfDate = txtConfrDate.Text;



            if (rdoProb.Checked == true)
            {
                var quer = db.vt_tbl_Employee.Where(x => x.EmployeeID == id).ToList();
                quer.ForEach(a =>
                {
                    a.ProvisionalPeriod = NewPer.ToString();

                });

            }


            else if (rdoConfirmation.Checked == true)
            {
                var quer = db.vt_tbl_Employee.Where(x => x.EmployeeID == id).ToList();
                quer.ForEach(a =>
                {
                    a.JobPeriod = JobConfirm;
                    a.ConfirmationDate = vt_Common.CheckDateTime(ConfDate);
                });

            }



            db.SaveChanges();
            MsgBox.Show(this.Page, MsgBox.success, "  ", "Record Updated");
            Response.Redirect("Default.aspx");
        }

    }






    void FillDetailForm()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {

            int Employeeid = Convert.ToInt32(Request.QueryString.Get("EmployeeID"));

            var quer = db.VT_SP_GetEmployeeContract().Where(x=> x.EmployeeID == Employeeid).SingleOrDefault();

            var query = (from emp in db.vt_tbl_Employee
                         where emp.EmployeeID == Employeeid

                         select new
                         {  
                             emp.EmployeeID,
                             emp.FirstName,
                             emp.JoiningDate,
                             emp.ProvisionalPeriod
                         }).SingleOrDefault();


            DateTime JoinDate = vt_Common.CheckDateTime(query.JoiningDate);

            lblEmployeeName.Text = query.FirstName;
            lblRemDays.Text = vt_Common.CheckString(quer.NumberofDaysRemaining);
            lblJoiningDate.Text = String.Format("{0:d}", JoinDate);
            lblProbationPeriod.Text = query.ProvisionalPeriod+ "  Month";


            ViewState["LeaveAppID"] = query.EmployeeID;
        }

    }


}