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

public partial class PFMonthlyReturn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }

    }


    #region Control Event

    protected void grdLoanEntry_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        UpView.Update();
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
                vt_Common.ReloadJS(this.Page, "$('#pfmonthlyreturn').modal();binddata();");
                UpDetail.Update();
                break;
            default:
                break;
        }

        vt_Common.ReloadJS(this.Page, "$('.confirm').confirm();");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_PFMonthlyReturn pmr = new vt_tbl_PFMonthlyReturn();
                //vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == pmr.EnrollId);
                pmr.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                //pmr.EnrollId = "";                
                //pmr.EPFContribution = "";
                //pmr.EPFWages = "";
                //pmr.EPSContribution = "";
                //pmr.EPSWages = "";
                //pmr.DiffEPFAndEPSContribution = "";
                //pmr.AbsentDays = "";

                if (ViewState["PageID"] != null)
                {
                    //pmr.EntryID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(pmr).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    //db.vt_tbl_LoanEntry.Add(pmr);
                }

                db.SaveChanges();
            }

            MsgBox.Show(Page, MsgBox.success, "", "Successfully Save");
            ClearForm();
            LoadData();
            UpView.Update();
        }
        catch (DbUpdateException ex)
        {

        }
    }

    protected void btnAddnew_ServerClick(object sender, EventArgs e)
    {
        //ViewState["PageID"] = null;
        //vt_Common.Clear(pnlDetail.Controls);
        //UpDetail.Update();
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
                            where pmr.CompanyID == vt_Common.CompanyId
                            select new
                            {
                                pmr.EnrollId,
                                //emp.EmployeeName,
                                pmr.PFMonthlyRID,
                                pmr.EPFContribution,
                                pmr.EPFWages,
                                pmr.EPSContribution,
                                pmr.EPSWages,
                                pmr.DiffEPFAndEPSContribution
                                

                            }).ToList();
                grdPFMonthlyReturn.DataSource = (quer);
                grdPFMonthlyReturn.DataBind();
            }
        }

      
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        //vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#pfmonthlyreturn').modal('hide');$('.confirm').confirm();binddata();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_PFMonthlyReturn pmr = db.vt_tbl_PFMonthlyReturn.FirstOrDefault(x => x.PFMonthlyRID == ID);
            //vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == pmr.EnrollId);
            //txtEnrollid.text = "";
            //txtEmployeeName.text = "";
            //txtEPFContribution.Text = "";
            //txtEPFWages.Text = "";
            //txtEPSContribution.Text = "";
            //txtEPSWages.Text = "";
            //txtEPSAndEPFCont.Text = "";
            //txtAbsentDays.Text = "";

            
        }
        ViewState["PageID"] = ID;
    }

    #endregion

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {

    }
}