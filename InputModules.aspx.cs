using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InputModules : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                if ((string)Session["UserName"] == "SuperAdmin")
                {
                    checkEmp(true);
                }
                else
                {
                    Bind_Pages();
                    //using (vt_EMSEntities db = new vt_EMSEntities())
                    //{
                    //    var Query = db.sp_GetMenuByUserNew(6, (int)Session["RoleId"]).ToList();

                    //    //var Query = db.sp_GetMenuByUser(6, (int)Session["UserId"]).ToList();
                    //    foreach (var item in Query)
                    //    {
                    //        if (item.PageName == "Loan Input")
                    //        {
                    //            LoanInputbtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Advance Salary")
                    //        {
                    //            AdvanceSalarybtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Loan Approval")
                    //        {
                    //            LoanApproval.Visible = true;
                    //        }
                    //    }
                    //}
                }
                //if((int)Session["RoleId"] == 2 || (int)Session["RoleId"] == 1)
                //{
                //    AdvanceSalarybtn.Visible = false;
                //    LoanInputbtn.Visible = false;
                //}
            }
        }
    }
    private void Bind_Pages()
    {
        int ModuleID = 6;
        int RoleID = (int)Session["RoleId"];
        DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);
        if(Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageName"].ToString() == "Loan Input")
                {
                    if (RoleID == 1)
                    {
                        LoanInputbtn.Visible = false;
                    }
                    else
                    {
                        LoanInputbtn.Visible = true;
                    }
                    
                }
                else if (Row["PageName"].ToString() == "Advance Salary")
                {
                    AdvanceSalarybtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Loan Approval")
                {
                    if (RoleID ==2 )
                    {
                        LoanApproval.Visible = false;
                    }
                    else if(RoleID ==1)
                    {
                        LoanApproval.Visible = true;
                    }
                    
                }
            }
        }
    }
    private void checkEmp(bool isVisible)
    {
        AdvanceSalarybtn.Visible = isVisible;
        LoanInputbtn.Visible = isVisible;
        LoanApproval.Visible = isVisible;

    }
    protected void LoanInputbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoanEntry.aspx");
    }
    protected void AdvanceSalarybtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdvanceSalary.aspx");
    }
    protected void BtnLoanAdjustment_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoanAdjustment.aspx");
    }

    protected void LoanApproval_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoanApproval.aspx");
    }
}