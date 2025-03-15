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

public partial class BranchWiseAttendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    #region Control Event


    protected void grdBranchWiseAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_BranchWiseAttendance ba = db.vt_tbl_BranchWiseAttendance.FirstOrDefault(x => x.ID == ID);
                        //db.vt_tbl_BranchWiseAttendance.Remove(ba);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, ba.MonthYear, "Successfully Deleted");
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
                vt_Common.ReloadJS(this.Page, "$('#').modal();binddata();");
                //UpDetail.Update();
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
                vt_tbl_BranchWiseAttendance ba = new vt_tbl_BranchWiseAttendance();               
                ba.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                //ba.EnrollId = "";
                //ba.BranchID = "";
                //ba.MonthYear = "";
                //ba.Present = "";

                if (ViewState["PageID"] != null)
                {
                    ba.ID = vt_Common.CheckInt(ViewState["PageID"]);
                    db.Entry(ba).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    //db.vt_tbl_LoanEntry.Add(ba);
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


    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Clearform();
    }

    #endregion


    #region Healper Method


    void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                var quer = (from ba in db.vt_tbl_BranchWiseAttendance
                            join emp in db.vt_tbl_Employee on ba.EnrollId equals emp.EmployeeID
                            join b in db.vt_tbl_Branch  on ba.CompanyID  equals b.CompanyId 
                            //where ba.CompanyID == vt_Common.CompanyId
                            select new
                            {
                                ba.ID,
                                emp.EnrollId,
                                emp.EmployeeName,
                                ba.Present,
                                b.BranchName,
                                ba.MonthYear,
                                

                            }).ToList();
                grdBranchWiseAttendance.DataSource = (quer);
                grdBranchWiseAttendance.DataBind();
            }
        }

        //if (Session["EMS_Session"] != null)
        //{
        //    EDS_LoanEntry.WhereParameters.Clear();
        //    EDS_LoanEntry.WhereParameters.Add("CompanyId", TypeCode.Int32, ((EMS_Session)Session["EMS_Session"]).Company.CompanyID.ToString());
        //    grdLoanEntry.DataBind();
        //}
    }

    void ClearForm()
    {
        //ViewState["PageID"] = null;
        //vt_Common.Clear(pnlDetail.Controls);
        //vt_Common.ReloadJS(this.Page, "$('#loanentry').modal('hide');$('.confirm').confirm();binddata();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_BranchWiseAttendance ba = db.vt_tbl_BranchWiseAttendance.FirstOrDefault(x => x.EnrollId == ID);
            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ba.EnrollId);
            vt_tbl_Branch b = db.vt_tbl_Branch.FirstOrDefault(x => x.CompanyId == ba.CompanyID);
            //txtEmpID.Text = "";
            //txtEmpName.Text = "";
            //ddlBranch.SelectedValue = "";
            //txtPresent.Text = "";            
            //txtMonthsYear.Text = "";
            
        }
        ViewState["PageID"] = ID;
    }

    #endregion
}