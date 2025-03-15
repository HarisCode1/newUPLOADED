using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AttendanceModule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                if((string)Session["UserName"] == "SuperAdmin")
                {
                    checkEmp(true);
                    LeavingApplicationbtn.Visible = false;
                    ManualAttendancebtn.Visible = false;
                    EmployeeAttendancebtn.Visible = false;
                }
                else
                {
                    Bind_Pages();
                    //using (vt_EMSEntities db = new vt_EMSEntities())
                    //{
                    //    var Query = db.sp_GetMenuByUserNew(11,(int)Session["RoleId"]).ToList();
                    //    //var Query = db.sp_GetMenuByUser(11, (int)Session["RoleId"]).ToList();
                    //    foreach (var item in Query)
                    //    {
                    //        if (item.PageName == "Leave Approval")
                    //        {
                    //            EmpLeaveAproval.Visible = true;
                    //        }
                    //        else if (item.PageName == "Manual Attendance")
                    //        {
                    //            ManualAttendancebtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Employee Attendance")
                    //        {
                    //            EmployeeAttendancebtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Leave Application")
                    //        {
                    //            LeavingApplicationbtn.Visible = true;
                    //        }
                    //        else if (item.PageName == "Bulk Attendance")
                    //        {
                    //            BtnEmployeeAttendance.Visible = true;
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
    private void Bind_Pages()
    {
        int ModuleID = 11;
        int RoleID = (int)Session["RoleId"];
        vt_EMSEntities db = new vt_EMSEntities();
        DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageName"].ToString() == "Leave Approval")
                {
                    EmpLeaveAproval.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Manual Attendance")
                {

                    //ManualAttendancebtn.Visible = false;
                    ManualAttendancebtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Employee Attendance")
                {
                    //EmployeeAttendancebtn.Visible = false;
                     EmployeeAttendancebtn.Visible = true;
                }
                else if (Row["PageName"].ToString() == "Leave Application")
                {
                    int EmpID = Convert.ToInt32(Session["EmployeeID"]);
                    if (RoleID == 4)
                    {
                        LeavingApplicationbtn.Visible = true;
                    }
                    if (ProcedureCall.SpCall_sp_CheckLineManagerw(EmpID))
                    {
                        EmpLeaveAproval.Visible = true;
                    }

                }
                else if (Row["PageName"].ToString() == "Bulk Attendance")
                {
                    BtnEmployeeAttendance.Visible = true;
                }
            }
        }
    }
    private void checkEmp(bool isVisible)
    {
        EmpLeaveAproval.Visible = isVisible;
        ManualAttendancebtn.Visible = isVisible;
        EmployeeAttendancebtn.Visible = isVisible;
        LeavingApplicationbtn.Visible = isVisible;
        BtnEmployeeAttendance.Visible = isVisible;
    }
    protected void LeaveYearbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("LeaveYear.aspx");
    }
    protected void LeaveAllotmentbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("LeaveAllotment.aspx");
    }
    protected void ShiftSchedulingbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShiftAllotment.aspx");
    }
    protected void LeavingApplicationbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("LeaveApplication.aspx");
    }
    protected void COFFAppbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CoffApplication.aspx");
    }
    protected void ManualAttendancebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManualAttendance.aspx");
    }

    protected void EmpLeaveAproval_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmpLeaveAproval.aspx");
    }
    protected void GatePassEntrybtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("GetPassEntry.aspx");
    }
    protected void MissingLogEntrybtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("MissingLogEntry.aspx");
    }
    protected void BtnEmployeeAttendance_Click(object sender, EventArgs e)
    {
        Response.Redirect("Attendance_Sheet.aspx");
    }
    protected void BranchWAttbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchWiseAttendance.aspx");
    }
    protected void ProcessLogbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeWiseProcess.aspx");
    }
    protected void DeviceUSBbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("DownloadLogsByDevice.aspx");
    }
    protected void USBbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("DownloadLogsFromUSB.aspx");
    }
    protected void EmployeeAttendancebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeAttendance.aspx");
    }
}