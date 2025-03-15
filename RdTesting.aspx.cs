using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RdTesting : System.Web.UI.Page
{
    public static int CompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        CompanyID = Convert.ToInt32(Session["CompanyId"]);
        //if (!Page.IsPostBack)
        //{
        //    LoadData();
        //}
    }

    //void LoadData()
    //{
    //    try
    //    {
    //        using (vt_EMSEntities db = new vt_EMSEntities())
    //        {
    //            var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
    //            GvTest.DataSource = Query;
    //            GvTest.DataBind();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.TryCatchException(ex);
    //        throw ex;
    //    }
    //}


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Test()
    {
        string List;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //var Query = db.VT_SP_GetEmployees(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                var Query = db.VT_SP_GetEmployees(CompanyID).ToList();

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


}