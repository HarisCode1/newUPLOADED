using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeactiveEmployees : System.Web.UI.Page
{
    int Count = 0;
    public static int CompanyID = 0;
    DataTable Dt = new DataTable();
    public vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (!Page.IsPostBack)
            {

            }

            }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load()
    {
        string List = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (CompanyID == 0)
                {
                    // MsgBox.Show(Page, MsgBox.danger, "", "Please Select Company First from dashboard");
                    //HttpContext.Current.Response.Write("<script>alert('Please Select Company First from dashboard');</script>");
                }
                else
                {

                    DataTable dt = ProcedureCall.sp_GetDeactiveEmp(CompanyID).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.SerializeObject(dt);
                    }
                    else
                    {

                    }
                    //var Query = db.VT_SP_GetEmployees(CompanyID == 0 ? 0 : CompanyID).ToList();
                    //if (Query != null)
                    //{
                    //    List = JsonConvert.SerializeObject(Query);

                    //}
                    //else
                    //{
                    //    //HttpContext.Current.Response.Write("<script>alert('Please Select Company First from dashboard');</script>");
                    //}


                }


            }


        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        return List;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string EmployeeInctive(int EmployeeID)
    {

        string message;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                
                vt_tbl_Employee Obj = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmployeeID).FirstOrDefault();

                if (EmployeeID != 0)
                {
                       
                        DataSet dt = ProcedureCall.Sp_activeEmployee(EmployeeID);
                        message = "User activated succesfully";
                        return message;
                }
                else
                {
                    message = "EmployeID not found";
                }
                return message;
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }


    }

}