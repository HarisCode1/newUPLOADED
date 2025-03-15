using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmployeeLeavesView : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadData();
                int RoleID;
                RoleID = Convert.ToInt32(Session["RoleId"]);
                int lid = Convert.ToInt32(lblid.Value);
                var query = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID.Equals(lid)).FirstOrDefault();
                if (query != null)
                {
                    if (query.isApproved == true && RoleID == 2)
                    {
                        MsgBox.Show(Page, MsgBox.success, "", "This Application Has been approved!");
                      //  btnSaveLeaveApproval.Visible = false;
                        lblapprove.Text = "Application Has been approved!";
                        DdlHrApproval.Enabled = false;

                    }
                    if (query.isRejected == true && RoleID == 2)
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "This Application Has been Rejected!");
                      //  btnSaveLeaveApproval.Visible = false;
                        lblapprove.Text = "Application Has been Rejected!";
                        DdlHrApproval.SelectedValue = "1";
                    }

                }


                if (RoleID == 2)
                {
                    divHR.Visible = true;
                }
            }
        }
    }
    private int LeavesAppID
    {
        get
        {
            return vt_Common.CheckInt(Request.QueryString["LOA"].Trim());
        }
    }

    private string LeaveViewID
    {
        get
        {
            return Request.QueryString["v2s"].Trim();
        }
    }
    void LoadData()
    {
        if (LeavesAppID <= 0 || string.IsNullOrEmpty(LeaveViewID))
        {
            Response.Redirect("EmpLeaveAproval.aspx");
        }
        int RoleID;
        RoleID = Convert.ToInt32(Session["RoleId"]);

        if (LeavesAppID > 0)
        {
            //if (Session["EMS_Session"] != null)
            //{
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                var getLeavesDetail = (from L in db.vt_tbl_LeaveApplication
                                       join C in db.vt_tbl_Company on L.CompanyID equals C.CompanyID
                                       join E in db.vt_tbl_Employee on L.EnrollId equals E.EmployeeID
                                       where L.LeaveApplicationID == LeavesAppID
                                       select new {E.RoleID, C.CompanyName, E.EmployeeName, L.Date, L.ToDate, L.FromDate, L.TotalLeave, L.Reason, L.isApproved, L.Comments, L.isManagerMark, L.isRejected }).FirstOrDefault();
                lblid.Value = LeavesAppID.ToString();
                LblCompanyName.Text = getLeavesDetail.CompanyName;
                LblEmployeeName.Text = getLeavesDetail.EmployeeName;
                LblOnApplyLeaves.Text = String.Format("{0:dd/MM/yyyy}", getLeavesDetail.Date);
                LblTotalNoLeaves.Text = getLeavesDetail.TotalLeave;
                LblLeavesFromDate.Text = String.Format("{0:dd/MM/yyyy}", getLeavesDetail.FromDate);
                LblLeavesTodate.Text = String.Format("{0:dd/MM/yyyy}", getLeavesDetail.ToDate);
                TxtBxReasonOfLeaves.Text = getLeavesDetail.Reason;
                //ddldecision.SelectedValue = getLeavesDetail.isApproved;

                if (getLeavesDetail.isManagerMark == false && RoleID == 2)
                {
                    DdlHrApproval.Enabled = false;
                    ddldecision.Enabled = false;
                }
                else
                {
                   
                    if (getLeavesDetail.isManagerMark == true || LeaveViewID == "y")
                    {
                        TxtBxComments.Text = getLeavesDetail.Comments;
                        if (ddldecision.SelectedValue != "0")
                        {

                            ddldecision.SelectedValue = getLeavesDetail.isManagerMark == false ? "1" : "2";
                        }
                        TxtBxComments.Enabled = false;
                        ddldecision.Enabled = false;

                    }

                    if (getLeavesDetail.isApproved == true)
                    {
                        ddldecision.SelectedValue = getLeavesDetail.isManagerMark == false ? "1" : "2";
                        DdlHrApproval.SelectedValue = getLeavesDetail.isApproved == false ? "1" : "2";
                        DdlHrApproval.Enabled = false;
                        ddldecision.Enabled = false;
                    }
                    else if (getLeavesDetail.isRejected == true)
                    {
                        ddldecision.SelectedValue = getLeavesDetail.isManagerMark == false ? "1" : "2";
                        DdlHrApproval.SelectedValue = getLeavesDetail.isApproved == false ? "1" : "2";
                        DdlHrApproval.Enabled = false;
                        ddldecision.Enabled = false;
                    }
                    else
                    {
                        {
                            DdlHrApproval.SelectedValue = "0";
                        }
                    }

                }
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        //ClearForm();
        Response.Redirect("EmpLeaveAproval.aspx");
    }
}