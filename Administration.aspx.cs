using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administration : System.Web.UI.Page
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
                    //{
                    //    var Query = db.sp_GetMenuByUserNew(9, (int)Session["RoleId"]).ToList();

                    //    //var Query = db.sp_GetMenuByUser(9, (int)Session["UserId"]).ToList();
                    //    foreach (var item in Query)
                    //    {
                    //        if (item.PageName == "PF")
                    //        {
                    //            //PFbtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Company")
                    //        {
                    //            Companybtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Designation")
                    //        {
                    //            Designation.Visible = true;
                    //        }
                    //        else if (item.PageName == "Loan Category")
                    //        {
                    //            Loanbtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Branch")
                    //        {
                    //            Branchbtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Tax")
                    //        {
                    //            TaxMasterBtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Leave")
                    //        {
                    //            Leavebtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Department")
                    //        {
                    //            Departmentbtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Holiday")
                    //        {
                    //            Holidaybtn.Visible = true;
                    //        }
                    //    }
                    //}
                }

            }
        }

    }

    private void Bind_Pages()
    {
        int ModuleID = 9;
        int RoleID = (int)Session["RoleId"];
        vt_EMSEntities db = new vt_EMSEntities();
        DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];


            foreach (DataRow Row in Dt.Rows)
            {

                if (Row["PageName"].ToString() == "PF")
                {
                    //PFbtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Company")
                {
                    if (RoleID ==2 || RoleID ==1)
                    {
                        Companybtn.Visible = false;
                    }
                    else
                    {
                        Companybtn.Visible = true;
                    }
                    
                }
                else if (Row["PageName"].ToString() == "Designation")
                {
                    Designation.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Loan Category")
                {
                    Loanbtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Branch")
                {
                    Branchbtn.Visible = false;
                }
                else if (Row["PageName"].ToString() == "Tax")
                {
                    TaxMasterBtn.Visible = false;
                }
                else if (Row["PageName"].ToString() == "Leave")
                {
                    Leavebtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Department")
                {
                    Departmentbtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Holiday")
                {
                    Holidaybtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Employee Type")
                {
                    EmployeeTypebtn.Visible = true;
                }
                //else if (Row["PageName"].ToString() == "Type Of Employee")
                //{
                //    EmployeeTypebtn.Visible = true;
                //}
            }
        }
    }

    private void checkEmp(bool isVisible)
    {
        //PFbtn.Visible = isVisible;
        Companybtn.Visible = isVisible;
        Designation.Visible = isVisible;
        Loanbtn.Visible = isVisible;
        Branchbtn.Visible = false;
        //Branchbtn.Visible = isVisible;
        TaxMasterBtn.Visible = isVisible;
        Leavebtn.Visible = isVisible;
        Departmentbtn.Visible = isVisible;
        Holidaybtn.Visible = isVisible;
        //EmployeeTypebtn.Visible = isVisible;
        EmployeeTypebtn.Visible = false;
    }

    protected void Companybtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Company.aspx");
    }

    protected void Branchbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branch.aspx");
    }
    protected void Departmentbtn_Click(object sender, EventArgs e)
    {
        //DataTable Dt = Session["PagePermissions"] as DataTable;
        //foreach (DataRow Row in Dt.Rows)
        //{
        //    if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_View"].ToString() == "True")
        //    {
                Response.Redirect("Department.aspx");
        //    }
        //    else if (Row["PageUrl"].ToString() == "Department.aspx" && Row["Can_Update"].ToString() == "False")
        //    {
        //        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to view this record");
        //    }
        //}
    }
    protected void Categorybtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Category.aspx");
    }
    protected void Bankbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("bankinformation.aspx");
    }
    protected void Employeebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Employes.aspx");
    }
    protected void Designation_Click(object sender, EventArgs e)
    {
        Response.Redirect("Designation.aspx");
    }
    protected void Salarybtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("SaleryHead.aspx");
    }
    protected void Loanbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoanInformation.aspx");
    }
    protected void Shiftbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Shift.aspx");
    }
    protected void Leavebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Leave.aspx");
    }
    protected void Holidaybtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("HolidayNew.aspx");
    }
    protected void PFbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyStaffPF.aspx");
    }
    protected void PTbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PT.aspx");
    }
    protected void EOBIbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EOBI.aspx");
    }
    protected void Otherbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Other.aspx");
    }
    protected void Devicebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Device.aspx");
    }
    protected void DatabaseDevbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("DatebaseDevice.aspx");
    }

    protected void TaxMasterbtn_Click(object sender, EventArgs e)
    {
        //Response.Redirect("TaxMaster.aspx");
        Response.Redirect("Tax.aspx");
    }

    protected void BtnEmployeeTaxAdjustment_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeTaxAdjustments.aspx");
    }
    

    protected void EmployeeTypebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeType.aspx");
    }

    protected void Canlenderbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Calender.aspx");
    }

    protected void Btn_CoreManagement_Click(object sender, EventArgs e)
    {
        Response.Redirect("CoreManagement_Add.aspx");
    }
}