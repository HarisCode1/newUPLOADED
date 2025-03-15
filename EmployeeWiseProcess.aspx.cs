using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmployeeWiseProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");

    }

    #region Control Event

    protected void grdEmployeeWiseProcess_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_PFMonthlyReturn pmr = db.vt_tbl_PFMonthlyReturn.FirstOrDefault(x => x.PFMonthlyRID == ID);
                        db.vt_tbl_PFMonthlyReturn.Remove(pmr);
                        db.SaveChanges();
                        LoadData();
                        //UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, pmr.DiffEPFAndEPSContribution, "Successfully Deleted");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditCompany":
                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                vt_Common.ReloadJS(this.Page, "$('#employeewiseprocess').modal();binddata();");
                //UpDetail.Update();
                break;
            default:
                break;
        }

        vt_Common.ReloadJS(this.Page, "$('.confirm').confirm();");
    }

    

    

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    #endregion

    #region Healper Method


    void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                var quer = (from pmr in db.vt_tbl_PFMonthlyReturn
                            //join emp in db.vt_tbl_Employee on pmr.EnrollId equals emp.EmployeeID                           
                            //where pmr.CompanyID == vt_Common.CompanyId
                            select new
                            {
                                pmr.EnrollId,
                                //emp.EmployeeName,


                            }).ToList();
                grdEmployeeWiseProcess.DataSource = (quer);
                grdEmployeeWiseProcess.DataBind();
            }
        }


    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        //vt_Common.Clear(pnlDetail.Controls);
        //vt_Common.ReloadJS(this.Page, "$('#employeewiseprocess').modal('hide');$('.confirm').confirm();binddata();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_PFMonthlyReturn pmr = db.vt_tbl_PFMonthlyReturn.FirstOrDefault(x => x.PFMonthlyRID == ID);
            //vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == pmr.EnrollId);
            //txtEnrollid.text = "";
            //txtEmployeeName.text = "";


        }
        ViewState["PageID"] = ID;
    }

    #endregion
}