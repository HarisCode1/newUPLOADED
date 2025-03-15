using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TerminatedEmployee : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    public static int CompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (!IsPostBack)
            {
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                //LoadData();

            }

        }
        if (!IsPostBack)
        {
            //LoadData();

        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object LoadData()
    {
        //try
        //{

        //    //int pageSize = grdtermemp.PageSize;
        //    DataTable dt = new DataTable();
        //    User_BAL usr = new User_BAL();
        //    int Companyid = Convert.ToInt32(Session["CompanyId"]);
        //    dt = usr.GetTerminatedEmployeeCompanyID(Companyid);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    grdtermemp.DataSource = dt;
        //    //    grdtermemp.DataBind();
        //    //}
        //}
        //catch (Exception ex)
        //{

        //    throw;
        //}







        string List = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //DataTable dt = new DataTable();
                User_BAL usr = new User_BAL();
                
                //dt = usr.GetTerminatedEmployeeCompanyID(Companyid);

                // Page Page = HttpContext.Current.Handler as Page;
                //var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                //var Query = db.VT_SP_GetEmployees(CompanyID).ToList();
                if (CompanyID == 0)
                {
                    // MsgBox.Show(Page, MsgBox.danger, "", "Please Select Company First from dashboard");
                    //HttpContext.Current.Response.Write("<script>alert('Please Select Company First from dashboard');</script>");
                }
                else
                {

                    DataTable dt = new DataTable();
                    dt = usr.GetTerminatedEmployeeCompanyID(CompanyID);/// db.VT_SP_GetEmployees(CompanyID == 0 ? 0 : CompanyID).ToList();
                    if (dt != null)
                    {
                        List = JsonConvert.SerializeObject(dt);

                    }
                    else
                    {
                        //HttpContext.Current.Response.Write("<script>alert('Please Select Company First from dashboard');</script>");
                    }


                }


            }


        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        return List;
        //Previous One
        //string List;
        //try
        //{
        //    using (vt_EMSEntities db = new vt_EMSEntities())
        //    {
        //        //var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
        //        //var Query = db.VT_SP_GetEmployees(CompanyID).ToList();
        //        var Query = db.VT_SP_GetEmployees(CompanyID == 0 ? 0 : CompanyID).ToList();
        //        List = JsonConvert.SerializeObject(Query);

        //    }


        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.TryCatchException(ex);
        //    throw ex;
        //}
        //return List;  











    }





    protected void lbtndetail_Command(object sender, CommandEventArgs e)
    {
        try
        {
            // Attempt to parse the CommandArgument
            int EmpID;
            if (e.CommandArgument != null && int.TryParse(e.CommandArgument.ToString(), out EmpID))
            {
                if (EmpID != 0)
                {
                    Response.Redirect("Employes_Details.aspx?ID=" + EmpID);
                }
                else
                {
                    // Handle the case where EmpID is 0, if necessary
                    // For example, show an error or do nothing
                }
            }
            else
            {
                // Handle invalid EmpID or CommandArgument (not a valid integer)
                // For example, log the error or show a user-friendly message
                throw new ArgumentException("Invalid Employee ID");
            }
        }
        catch (Exception ex)
        {
            // Log the exception or show a message to the user if needed
            // For now, just rethrow the exception
            throw;
        }

    }




    protected void grdtermemp_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //grdtermemp.PageIndex = e.NewPageIndex; // Set the new page index
        //LoadData();
    }
}