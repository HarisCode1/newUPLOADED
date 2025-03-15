using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SetSalary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
              int  ID = Convert.ToInt32(Request.QueryString["ID"]);
            }
        }

    }
}