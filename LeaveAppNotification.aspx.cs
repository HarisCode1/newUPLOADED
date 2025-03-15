using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class LeaveAppNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillLeaveDetailForm();
            LeaveGridBind();
           
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        update();
        
       
    }


    void LeaveGridBind()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int id = Convert.ToInt32(Request.QueryString.Get("LeaveApplicationID"));
            vt_tbl_LeaveApplication la = db.vt_tbl_LeaveApplication.FirstOrDefault(x => x.LeaveApplicationID == id);
            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == la.EnrollId);
            var leavequery = db.VT_SP_GetOtherLeaves(la.EnrollId).ToList();
            
            grdOtherLeave.DataSource = leavequery;
            grdOtherLeave.DataBind();
        }
    }


    public void update()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())

        {

        int id = Convert.ToInt32(Request.QueryString.Get("LeaveApplicationID"));
        var comments = lblComments.Text;


        if (rdbApprove.Checked == true)
        {
            var quer = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID == id).ToList();
            quer.ForEach(a =>
                            {
                                a.isApproved = true;
                                a.isRejected = false;
                                a.Comments = comments;
                            });
                        
        }


        else if (rdbReject.Checked == true)
        {
            var quer = db.vt_tbl_LeaveApplication.Where(x => x.LeaveApplicationID == id).ToList();
            quer.ForEach(a =>
            {
                a.isApproved = false;
                a.isRejected = true;
                a.Comments = comments;
            });
                        
        }



        db.SaveChanges();
        MsgBox.Show(this.Page, MsgBox.success, "  ", "Record Updated");
        Response.Redirect("Default.aspx");
    }

    }




    void FillLeaveDetailForm()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {

            int LeaveApId =  Convert.ToInt32(Request.QueryString.Get("LeaveApplicationID"));

            
            var query = (from la in db.vt_tbl_LeaveApplication
                         join l in db.vt_tbl_Leave on la.LeaveID equals l.LeaveID
                         join emp in db.vt_tbl_Employee on la.EnrollId equals emp.EmployeeID
                         join des in db.vt_tbl_Designation on emp.DesignationID equals des.DesignationID
                         where la.LeaveApplicationID == LeaveApId

                         select new
                         {
                             la.LeaveApplicationID,
                             emp.FirstName,
                             des.Designation,
                             la.EnrollId,
                             l.LeaveName,
                             la.FromDate,
                             la.ToDate,
                             la.Reason,
                             
                         }).SingleOrDefault();



            var record = db.vt_tbl_LeaveApplication.Where(o => o.EnrollId == query.EnrollId).Count();

            int LeaveRequest = Convert.ToInt32(record);
            lblLeaveRequest.Text = LeaveRequest.ToString();

            DateTime Frmdate = vt_Common.CheckDateTime(query.FromDate);
            DateTime Todate = vt_Common.CheckDateTime(query.ToDate);


            lblName.Text = query.FirstName;
            lblDesignation.Text = Convert.ToString(query.Designation);
            lblFromLeaveDate.Text = String.Format("{0:d}", Frmdate);
            lblToLeaveDate.Text = String.Format("{0:d}", Todate);
            lblLeaveType.Text = query.LeaveName;
            lblReason.Text = query.Reason;
           
            
            ViewState["LeaveAppID"] = query.LeaveApplicationID;
        }

    }



    protected void btnOtherLeave_Click(object sender, EventArgs e)
    {
        vt_Common.ReloadJS(this.Page, "(OtherLeave).modal();");
    }
}





