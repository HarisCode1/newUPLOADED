using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class LoanApproval : System.Web.UI.Page
{
    LoanApproval_BAL BAL = new LoanApproval_BAL();
    EMS_Session EM = new EMS_Session();
    vt_EMSEntities db = new vt_EMSEntities();
    DateTime EntryDate = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }


        //if (Session["EMS_Session"] != null)
        //{
        //    if (!IsPostBack)
        //    {
        //        EMS_Session s = (EMS_Session)Session["EMS_Session"];
        //        ViewState["hdnID2"] = s.user.RoleId;
        //        LoadGrid();
        //    }
        //}
    }
    public void LoadGrid()
    {
        BAL.RoleID = Convert.ToInt32(Session["RoleId"]);
        int CompanyId = Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];

        if (BAL.RoleID == 1)
        {
            divHRAdmin.Visible = true;
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                var Query = db.GetLoanRequestRecord(BAL.RoleID, CompanyId).ToList();
                GridView.DataSource = Query;
                GridView.DataBind();
            }
        }
        if (BAL.RoleID == 4)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                var Query = db.GetLoanRequestRecord(BAL.RoleID, CompanyId).ToList();
                GridView.DataSource = Query;
                GridView.DataBind();
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BAL.RoleID = (int)Session["RoleId"];
        if (BAL.RoleID == 1)
        {
            if (ddlHRAdmin.SelectedItem.Text != "Applied")
            {
                if (ddlHRAdmin.SelectedItem.Text == "Approved")
                {
                    BAL.EntryID = Convert.ToInt32(ViewState["hdnID"]);
                    vt_Common.ReloadJS(this.Page, "$('#ApprovalModal').modal('hide')");
                    BAL.ApproveByHR(BAL.EntryID);
                    MsgBox.Show(Page, MsgBox.success, "Message ", "Approved");
                }
                else
                {
                    BAL.EntryID = Convert.ToInt32(ViewState["hdnID"]);
                    vt_Common.ReloadJS(this.Page, "$('#ApprovalModal').modal('hide')");
                    BAL.RejectByHR(BAL.EntryID);
                    MsgBox.Show(Page, MsgBox.danger, "Message ", "Rejected");
                }
                #region LoanEntry Log
                int ID = Convert.ToInt32(ViewState["hdnID"]);
                vt_tbl_LoanEntry LoanEntry = db.vt_tbl_LoanEntry.Where(x => x.EntryID == ID).FirstOrDefault();
                vt_tbl_LoanEntryLog LELog = new vt_tbl_LoanEntryLog();
                LELog.CompanyID = LoanEntry.CompanyID; ;
                LELog.AppliedDate = LoanEntry.AppliedDate;
                LELog.EmployeeId = LoanEntry.EmployeeId;
                LELog.LoanId = LoanEntry.LoanId;
                LELog.AppliedAmount = LoanEntry.AppliedAmount;
                LELog.Description = LoanEntry.Description;
                LELog.EMIType = LoanEntry.EMIType;
                LELog.MonthDuration = LoanEntry.MonthDuration;
                LELog.LineManager = LoanEntry.LineManager;
                LELog.EntryDate = EntryDate;
                LELog.Action = "Update";

                //if (EntryID > 0)
                //{
                //    LELog.Action = "Update";
                //}
                //else
                //{
                //    LELog.Action = "Insert";
                //}
                var Checkdata = db.vt_tbl_Employee.Where(x => x.EmployeeID == LoanEntry.EmployeeId && x.IsHRLineManager == true).ToList();
                if (Checkdata.Count > 0)
                {
                    LELog.IsLineManager = true;
                    //LELog.HRAdmin = 1;
                }
                else
                {
                    LELog.IsLineManager = false;
                    //LELog.HRAdmin = 0;
                }
                if (ddlHRAdmin.SelectedItem.Text == "Approved")
                {
                    LELog.HRAdmin = 2;
                }
                else
                {
                    LELog.HRAdmin = 3;
                }


                db.vt_tbl_LoanEntryLog.Add(LELog);
                db.SaveChanges();
                #endregion
                ClearForm();
                LoadGrid();
                Update.Update();
                UpdateGrid.Update();
            }
            else
            {
                MsgBox.Show(Page, MsgBox.danger, "Message ", "Please select Approved or Rejected");
                Update.Update();
                UpdateGrid.Update();
            }

        }
        else
        {
            if (ddlAppRej.SelectedItem.Text != "Applied")
            {
                if (ddlAppRej.SelectedItem.Text == "Approved")
                {
                    BAL.EntryID = Convert.ToInt32(ViewState["hdnID"]);
                    vt_Common.ReloadJS(this.Page, "$('#ApprovalModal').modal('hide')");
                    BAL.Approve(BAL.EntryID);
                    MsgBox.Show(Page, MsgBox.success, "Message ", "Approved");
                }
                else
                {
                    BAL.EntryID = Convert.ToInt32(ViewState["hdnID"]);
                    vt_Common.ReloadJS(this.Page, "$('#ApprovalModal').modal('hide')");
                    BAL.Reject(BAL.EntryID);
                    MsgBox.Show(Page, MsgBox.danger, "Message ", "Rejected");
                }
                #region LoanEntry Log
                int ID = Convert.ToInt32(ViewState["hdnID"]);
                vt_tbl_LoanEntry LoanEntry = db.vt_tbl_LoanEntry.Where(x => x.EntryID == ID).FirstOrDefault();
                vt_tbl_LoanEntryLog LELog = new vt_tbl_LoanEntryLog();
                LELog.CompanyID = LoanEntry.CompanyID; ;
                LELog.AppliedDate = LoanEntry.AppliedDate;
                LELog.EmployeeId = LoanEntry.EmployeeId;
                LELog.LoanId = LoanEntry.LoanId;
                LELog.AppliedAmount = LoanEntry.AppliedAmount;
                LELog.Description = LoanEntry.Description;
                LELog.EMIType = LoanEntry.EMIType;
                LELog.MonthDuration = LoanEntry.MonthDuration;
                LELog.LineManager = LoanEntry.LineManager;
                LELog.EntryDate = EntryDate;
                LELog.Action = "Update";

                //if (EntryID > 0)
                //{
                //    LELog.Action = "Update";
                //}
                //else
                //{
                //    LELog.Action = "Insert";
                //}
                var Checkdata = db.vt_tbl_Employee.Where(x => x.EmployeeID == LoanEntry.EmployeeId && x.IsHRLineManager == true).ToList();
                if (Checkdata.Count > 0)
                {
                    LELog.IsLineManager = true;
                    //LELog.HRAdmin = 1;
                }
                else
                {
                    LELog.IsLineManager = false;

                }
                if (ddlAppRej.SelectedItem.Text == "Approved")
                {
                    LELog.HRAdmin = 1;
                }
                else
                {
                    LELog.HRAdmin = 0;
                }


                db.vt_tbl_LoanEntryLog.Add(LELog);
                db.SaveChanges();
                #endregion


                ClearForm();
                LoadGrid();
                Update.Update();
                UpdateGrid.Update();
            }
            else
            {
                MsgBox.Show(Page, MsgBox.danger, "Message ", "Please select Approved or Rejected");
                Update.Update();
                UpdateGrid.Update();
            }

        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            //EMS_Session s = (EMS_Session)Session["EMS_Session"];
            ViewState["hdnID2"] = Convert.ToInt32(Session["RoleId"]);
            LoanApproval_BAL LA_BAL = BAL.GetLoanById(Convert.ToInt32(e.CommandArgument));
            ViewState["hdnID"] = LA_BAL.EntryID;
            txtName.Text = LA_BAL.EmployeeName;
            txtCompany.Text = LA_BAL.CompanyName;
            txtLoanType.Text = LA_BAL.LoanName;
            txtAppliedDate.Text = LA_BAL.AppliedDate.ToShortDateString();
            txtAmount.Text = LA_BAL.AppliedAmount.ToString();
            if (LA_BAL.LineManager == 2)
            {
                ddlAppRej.SelectedValue = 1.ToString();
            }
            else if (LA_BAL.LineManager == 1)
            {
                ddlAppRej.SelectedValue = 0.ToString();
            }
            else
            {
                ddlAppRej.SelectedValue = 2.ToString();
            }
            if (LA_BAL.HRAdmin == 1)
            {
                ddlHRAdmin.SelectedValue = 0.ToString();
            }
            else if (LA_BAL.HRAdmin == 3)
            {
                ddlHRAdmin.SelectedValue = 2.ToString();
            }
            else
            {
                ddlHRAdmin.SelectedValue = 1.ToString();
            }
            vt_Common.ReloadJS(this.Page, "$('#ApprovalModal').modal();");
            Update.Update();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    void ClearForm()
    {
        try
        {
            ViewState["PageID"] = null;
            vt_Common.Clear(pnlDetail.Controls);
            vt_Common.ReloadJS(this.Page, "$('#ApprovalModal').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
}