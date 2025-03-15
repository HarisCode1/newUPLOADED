using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Terminate_Employee : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    DateTime EntryDate = DateTime.Now;
    public static int Companyid = 0;
    public static int EmployeeID = 0;
    public static int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Authenticate.Confirm())
        {
            Companyid = Convert.ToInt32(Session["CompanyId"]);
            ID = Convert.ToInt32(Request.QueryString["ID"]);
           
            if (!Page.IsPostBack)
            {
                //Bind_DdlCompany();
          
            }


            //  Check_EmployeeCodeInEdit(Convert.ToInt32(Request.QueryString["ID"]));

        }
    }
}