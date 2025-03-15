using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PayRoll_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                if ((string)Session["UserName"] == "SuperAdmin")
                {
                    checkEmp(true);
                }
                else
                {
                    Bind_Pages();
                    //using (vt_EMSEntities db = new vt_EMSEntities())
                    // {
                    //    var Query = db.sp_GetMenuByUserNew(1003, (int)Session["RoleId"]).ToList();

                    //    //var Query = db.sp_GetMenuByUser(1003, (int)Session["UserId"]).ToList();
                    //    foreach (var item in Query)
                    //    {
                    //        if (item.PageName == "Bonus")
                    //        {
                    //            btnbonus.Visible = true;
                    //        }
                    //        else if (item.PageName == "PF")
                    //        {
                    //            btnpf.Visible = true;
                    //        }
                    //        else if (item.PageName == "Graduity")
                    //        {
                    //            btngraduity.Visible = true;
                    //        }
                    //        else if (item.PageName == "Staff Bonus")
                    //        {
                    //            btnStaffBonus.Visible = true;
                    //        }
                    //    }
                    //}
                } 
            }
        }
    }
    private void Bind_Pages()
    {
        int ModuleID = 1003;
        int RoleID = (int)Session["RoleId"];
        DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID,RoleID);
        if(Ds!= null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageName"].ToString() == "Bonus")
                {
                    btnbonus.Visible = true;
                }
                else if (Row["PageName"].ToString() == "PF")
                {
                    btnpf.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Graduity")
                {
                    btngraduity.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Staff Bonus")
                {
                    btnStaffBonus.Visible = true;
                }
            }
        }

    }
    private void checkEmp(bool isVisible)
    {
        btngraduity.Visible = isVisible;
        btnpf.Visible = isVisible;
        btnbonus.Visible = isVisible;
        btnStaffBonus.Visible = true;
    }

    protected void btnbonus_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bonus.aspx");
    }

    protected void btnpf_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyStaffPF.aspx");
    }

    protected void btngraduity_Click(object sender, EventArgs e)
    {
        Response.Redirect("Gratuity.aspx");
    }

    protected void btnStaffBonus_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffBonus.aspx");
    }
}