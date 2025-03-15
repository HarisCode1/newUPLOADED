using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Promotion_SpCall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet Ds = ProcedureCall.SpCall_Job_SP_UpdateEmployeesPromotion();
        if (Ds != null)
        {
            LblResult.Text = "Promotion Table Updated";
        }
    }
}