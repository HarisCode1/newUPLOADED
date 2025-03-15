using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangeCompbtn_Click(object sender, EventArgs e)
    {
        //Response.Redirect("");
    }
    protected void Backupbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackupDatabase.aspx");
    }
    protected void Restorebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("RestoreDatabase.aspx");
    }
    protected void Settingbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("DatabaseSetting.aspx");
    }
    protected void ChangePwdbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
    protected void Reminderbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reminder.aspx");
    }
    protected void DeviceMgmntbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackupDatabase.aspx");
    }
    protected void ReportGenbtn_Click(object sender, EventArgs e)
    {
        //Response.Redirect("BackupDatabase.aspx");
    }
    protected void PayslipMailbtn_Click(object sender, EventArgs e)
    {
        //Response.Redirect("BackupDatabase.aspx");
    }
    protected void EmailSettingbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmailSetting.aspx");
    }
    protected void PagePermissionbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PagePermission.aspx");
    }
}