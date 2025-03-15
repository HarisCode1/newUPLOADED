using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Reminder : System.Web.UI.Page
{
    public void LoadData() 
    {
        string birthDate = txtDate.Text;

        DateTime date = Convert.ToDateTime(txtDate.Text);
        
        using (vt_EMSEntities db = new vt_EMSEntities())
        {

            var data = from v in db.vt_tbl_Employee
                    where (v.DOB.Value.Month == date.Month && v.DOB.Value.Day == date.Day)
                    orderby v.DOB.Value.Month, v.DOB.Value.Day
                    select v.EmployeeName;
            
            foreach (var item in data)
            {
                lsbEmployeeBirthDay.Items.Add(item.ToString());
            }

            var dataa = from v in db.vt_tbl_Employee
                        where (v.WeddDate.Value.Month == date.Month && v.WeddDate.Value.Day == date.Day)
                       orderby v.WeddDate.Value.Month, v.WeddDate.Value.Day
                       select v.EmployeeName;

            foreach (var item in dataa)
            {
                lsbEmployeeWedding.Items.Add(item.ToString());
            }


        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            txtDate.Text = DateTime.Now.ToString();
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "BindData();");
    }

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        lsbEmployeeBirthDay.Items.Clear();
        lsbEmployeeWedding.Items.Clear();
        DateTime date = Convert.ToDateTime(txtDate.Text);

        LoadData();
        //using (vt_EMSEntities db = new vt_EMSEntities())
        //{
        //    var data = (from x in db.vt_tbl_Employee
        //                where x.DOB == date
        //                select x.EmployeeName);
           
        //    foreach (var item in data)
        //    {
        //        lsbEmployeeBirthDay.Items.Add(item.ToString());
        //    }

        //    var dataa = (from x in db.vt_tbl_Employee
        //                where x.WeddDate == date
        //                select x.EmployeeName);

        //    foreach (var item in dataa)
        //    {
        //        lsbEmployeeWedding.Items.Add(item.ToString());
        //    }


        //}
    }
}