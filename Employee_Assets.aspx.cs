using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_Assets : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    private static int EmployeeID = 0;
    private static int CompanyID = 0;
    private static int UserID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            EmployeeID = Convert.ToInt32(Request.QueryString["ID"]);
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            UserID = Convert.ToInt32(Session["UserId"]);

            if (!IsPostBack)
            {

            }
        }
    }
    

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load()
    {
        string List;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                //var Query = db.VT_SP_GetEmployees(CompanyID).ToList();
                var Query = db.vt_tbl_EmployeeAssets.Where(x => x.Given_To == EmployeeID).ToList();
                List = JsonConvert.SerializeObject(Query);
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
    public static bool Insert(string Title, string Description, string Given_Value, string Given_Condition)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_EmployeeAssets Obj = new vt_tbl_EmployeeAssets();
        Obj.Title = Title;
        Obj.Description = Description;
        Obj.Given_Value = Convert.ToDecimal(Given_Value);
        Obj.Given_Condition = Given_Condition;
        Obj.Given_To = EmployeeID;

        Obj.CreatedDate = DateTime.Now;
        Obj.CreatedBy = UserID;
        db.Entry(Obj).State = System.Data.Entity.EntityState.Added;
        db.SaveChanges();

        return true;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool Update(string ID,string Title, string Description, string Given_Value, string Given_Condition)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        int id = Convert.ToInt32(ID);
        vt_tbl_EmployeeAssets Obj = db.vt_tbl_EmployeeAssets.Where(x => x.ID.Equals(id)).SingleOrDefault();
        if (Obj != null)
        {
            Obj.Title = Title;
            Obj.Description = Description;
            Obj.Given_Value = Convert.ToDecimal(Given_Value);
            Obj.Given_Condition = Given_Condition;
            Obj.Given_To = EmployeeID;

            Obj.CreatedDate = DateTime.Now;
            Obj.CreatedBy = UserID;
            db.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        return true;
    }
}
