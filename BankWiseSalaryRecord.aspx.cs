using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BankWiseSalaryRecord : System.Web.UI.Page
{
    public static int CompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                CompanyID = Convert.ToInt32(Session["CompanyId"]);
                MonthBind();
       


            }
        }
    }
    private void MonthBind()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        for (int i = 1; i < 13; i++)
        {
            
            DDownMonths.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load(int year, string bankname,int month)
    {
        string List = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {                
                if (CompanyID == 0)
                {
                }
                else
                {
                    if (bankname =="Cash")
                    {
                        DateTime startDate = new DateTime(year, month, 1);
                        // var query = ProcedureCall.Sp_Call_SetEmpSalariesBankWise(startDate, Companyid, bankname);//db.VT_SP_GetEmpSalariesBankWise(DateTime.Now, Companyid, bankname);
                        DataSet Ds = ProcedureCall.Sp_Call_SetEmpSalariesCashWise(startDate, CompanyID, bankname);
                        DataTable dt = Ds.Tables[0];
                        var Query = dt;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                          
                            List = JsonConvert.SerializeObject(Query);
                        }
                        else
                        {
                        }

                    }
                    else if(bankname == "Cheque")
                    {
                        DateTime startDate = new DateTime(year, month, 1);
                        // var query = ProcedureCall.Sp_Call_SetEmpSalariesBankWise(startDate, Companyid, bankname);//db.VT_SP_GetEmpSalariesBankWise(DateTime.Now, Companyid, bankname);
                        DataSet Ds = ProcedureCall.Sp_Call_SetEmpSalariesChequeWise(startDate, CompanyID, bankname);
                        DataTable dt = Ds.Tables[0];
                        var Query = dt;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                           
                            List = JsonConvert.SerializeObject(Query);
                        }
                        else
                        {
                        }

                    }
                    else
                    {
                        DateTime startDate = new DateTime(year, month, 1);
                        // var query = ProcedureCall.Sp_Call_SetEmpSalariesBankWise(startDate, Companyid, bankname);//db.VT_SP_GetEmpSalariesBankWise(DateTime.Now, Companyid, bankname);
                        DataSet Ds = ProcedureCall.Sp_Call_SetEmpSalariesBankWise(startDate, CompanyID, bankname);
                        DataTable dt = Ds.Tables[0];
                        var Query = dt;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                           
                            List = JsonConvert.SerializeObject(Query);
                        }
                        else
                        {
                        }

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
    protected void btnsearchbankwisedetail_Click(object sender, EventArgs e)
    {
        try
        {
            int Companyid = Convert.ToInt32(Session["CompanyId"]);
            if (Companyid != 0)
            {
                int year = DateTime.Now.Year;
                string bankname = ddlsearchbankwisedetail.Text;
                int month = Convert.ToInt32(DDownMonths.SelectedValue);
              // Load(year, bankname, month);
                //DateTime startDate = new DateTime(year, month, 1);
                //// var query = ProcedureCall.Sp_Call_SetEmpSalariesBankWise(startDate, Companyid, bankname);//db.VT_SP_GetEmpSalariesBankWise(DateTime.Now, Companyid, bankname);
                //DataSet Ds = ProcedureCall.Sp_Call_SetEmpSalariesBankWise(startDate, Companyid, bankname);
                //if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                //{
                //     grdempperformanceevaluation.DataSource = Ds.Tables[0];
                //}
                //else
                //{
                //    grdempperformanceevaluation.DataSource = null;
                //    MsgBox.Show(Page, MsgBox.danger, "Sorry" +" " + bankname, "Record Not Exist");
                //}
                //grdempperformanceevaluation.DataBind();

            }

        }
        catch (Exception)
        {

            throw;
        }

    }


}