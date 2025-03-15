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
using Viftech;

public partial class DeletedEmployee : System.Web.UI.Page
{
    int Count = 0;
    public static int CompanyID = 0;
    DataTable Dt = new DataTable(); 
    public vt_EMSEntities db = new vt_EMSEntities();

    User_BAL BAL = new User_BAL();
    vt_tbl_Employee emp = new vt_tbl_Employee();
    protected void Page_Load(object sender, EventArgs e)
    {

        
            if (Authenticate.Confirm())
            {
                // Fill();
                CompanyID = Convert.ToInt32(Session["CompanyId"]);
                if (!Page.IsPostBack)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                dt = ProcedureCall.VT_SP_GetEmployee_ByID(5132).Tables[0];



                #region IsAuthenticated To Page
                int UserID = Convert.ToInt32(Session["UserId"]);
                    int RoleId = Convert.ToInt32(Session["RoleId"]);
                 
                    string UserName = Session["UserName"].ToString();
                    string PageUrl = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                DataTable Dt = Session["PagePermissions"] as DataTable;
                if (!vt_Common.IsAuthenticated(PageUrl, UserName, Dt))
                {
                    Response.Redirect("Default.aspx");
                }

                var query = db.vt_tbl_User.Where(x => x.UserId == UserID).ToList();
                if (query.Count > 0 || UserName == "SuperAdmin")
                {
                    //BtnAddNew.Visible = false;
                }
                #endregion
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

                    DataTable dt = new DataTable();
                    dt = ProcedureCall.SP_GetDeletedEmp(CompanyID).Tables[0];/// db.VT_SP_GetEmployees(CompanyID == 0 ? 0 : CompanyID).ToList();
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
               
    }
}