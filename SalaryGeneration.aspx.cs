using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.Globalization;
using System.Data;
using System.IO;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;

public partial class SalaryGeneration : System.Web.UI.Page
{
    string Pagename = string.Empty;
    public static int Companyid = 0;
    public void LoadData()
    {
         Companyid = Convert.ToInt32(Session["CompanyId"]);

        ddlCompany.SelectedValue = Companyid.ToString();
        //if (Session["EMS_Session"] != null)
        //{
        using (vt_EMSEntities db = new vt_EMSEntities())
            {
                //var Query = ProcedureCall.sp_GetEmpSalaries();
                //grdSalaryGen.DataSource = Query.Tables[0];
                //grdSalaryGen.DataBind();
            }
            //Session["myData"] = null;
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadData();
                MonthBind();
                BindLog();
            }
            //if (!IsPostBack)
            //{
            //    RolePermission_BAL RP = new RolePermission_BAL();
            //    DataTable dt = new DataTable();
            //    PayRoll_Session goAMLSess = (PayRoll_Session)Session["goAMLSession"];
            //    //dt = RP.GetPagePermissionpPagesByRole(goAMLSess.RoleID, goAMLSess.UserID);
            //    dt = RP.GetPagePermissionpPagesByRole(Convert.ToInt32(Session["RoleId"]), Convert.ToInt32(Session["UserId"]));

            //    string pageName = null;
            //    bool view = false;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        int row = dt.Rows.IndexOf(dr);
            //        if (dt.Rows[row]["PageUrl"].ToString() == "SalaryRateAllotment.aspx")
            //        {
            //            pageName = dt.Rows[row]["PageUrl"].ToString();
            //            view = Convert.ToBoolean(dt.Rows[row]["Can_View"].ToString());
            //            break;
            //        }
            //    }
            //    if (dt.Rows.Count > 0)
            //    {
            //        if (pageName == "SalaryGeneration.aspx" && view == true)

            //        //if (pageName == "SalaryRateAllotment.aspx" && view == true)
            //        {
            //            LoadData();
            //            MonthBind();
            //            // DropdownBind();
            //            ViewState["PageURL"] = pageName;
            //            ViewState["ModuleId"] = vt_Common.GetModuleId(ViewState["PageURL"].ToString(), goAMLSess.PermissionTable);
            //            ViewState["PageId"] = vt_Common.GetPageId(ViewState["PageURL"].ToString(), goAMLSess.PermissionTable);
            //            Pagename = Path.GetFileNameWithoutExtension(Request.Path);

            //        }
            //        else
            //        {
            //            //Response.Redirect("Default.aspx");
            //        }
            //    }
            //    else
            //    {
            //        //Response.Redirect("Default.aspx");
            //    }
            //}
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTableGrid", "DataTableGrid('GridData');", true);
        }
        //if (!IsPostBack)
        //{
        //    LoadData();
        //    MonthBind();
        //}
    }

    protected void btnSalaryGen_Click(object sender, EventArgs e)
    {
        var companyID = vt_Common.CheckInt(ddlCompany.SelectedValue);
        if (companyID != 0)
        {
            var selectedMonth = vt_Common.CheckInt(DDownMonths.SelectedValue);
            if (selectedMonth != 0)
            {
                int month = selectedMonth;
                int year = DateTime.Now.Year;
                DateTime startDate = new DateTime(year, month, 1);
                if (companyID > 0 && selectedMonth > 0)
                {
                    Response.Redirect("EmpSalaryGenerateNew.aspx?CompanyID=" + companyID + "&month=" + startDate.ToString("MM/dd/yyyy").Trim());
                }
                else
                {
                    MsgBox.Show(Page, MsgBox.success, "Salary Generation", "Please Pick Company Name and Month ...");
                }
            }
            else
            {
                MsgBox.Show(Page, MsgBox.danger, "Salary Generation", "Please Pick Month Name ");
            }

        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "Please Select Company from DashBoard");
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

    protected void grdSalaryGen_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // var companyID = vt_Common.CheckInt(ddlCompany.SelectedValue);
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

            }
        }
        catch (Exception ex)
        {

        }
    }

    #region SalmanCode

    protected void BtnLog_Click(object sender, EventArgs e)
    {
        UpDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }

    void BindLog()
    {
        var CompanyID = vt_Common.CheckInt(Convert.ToInt32(Session["CompanyId"]));
        vt_EMSEntities db = new vt_EMSEntities();
        var Data = db.vt_tbl_CompanyMonthlySalary.Where(x => x.CompanyID == CompanyID).ToList();
        if (Data != null)
        {
            GvLog.DataSource = Data;
        }
        else
        {
            GvLog.DataSource = null;
        }
        GvLog.DataBind();
    }
    #endregion

    protected void btnbankwisesalary_Click(object sender, EventArgs e)
    {
        Response.Redirect("BankWiseSalaryRecord.aspx");
    }    
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    
    public static object Load(int Month)
    {
        string List = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (Companyid == 0)
                {
                }
                else
                {
                    int year = DateTime.Now.Year;
                    DateTime MonthWise;
                    if (year != 0 && Month != 0)
                    {
                        MonthWise = new DateTime(year, Month, 1);
                   
                 
                    DataSet Ds = ProcedureCall.Sp_Call_SetEmpSalariesSummarySheetMonthWise(Companyid, MonthWise);
                        DataTable dt = Ds.Tables[0];
                        var Query = dt;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //Query.Columns.Add("S.no");
                            //for (int i = 0; i < Query.Rows.Count; i++)
                            //{
                            //    Query.Rows[i]["S.no"] = i + 1;
                            //}
                            List = JsonConvert.SerializeObject(Query);
                        }
                        else
                        {
                            List = "Empty1";
                        }
                    }
                    else
                    {
                        List = "Empty";
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

    protected void btnSalary_Click(object sender, EventArgs e)
    {
        try
        {
            var companyID = vt_Common.CheckInt(ddlCompany.SelectedValue);
            if (companyID != 0)
            {
                var selectedMonth = vt_Common.CheckInt(DDownMonths.SelectedValue);
                if (selectedMonth != 0)
                {
                    int month = selectedMonth;
                    int year = DateTime.Now.Year;
                    DateTime startDate = new DateTime(year, month, 1);
                    if (companyID > 0 && selectedMonth > 0)
                    {
                        Response.Redirect("EmployeeSalaryGenerationDemo.aspx?CompanyID=" + companyID + "&month=" + startDate.ToString("MM/dd/yyyy").Trim());
                    }
                    else
                    {
                        MsgBox.Show(Page, MsgBox.success, "Salary Generation", "Please Pick Month ...");
                    }

                }

                }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}