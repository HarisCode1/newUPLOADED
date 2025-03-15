using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Viftech;

public partial class login : System.Web.UI.Page
{
    private vt_EMSEntities dbContext = new vt_EMSEntities();

    private Login_BAL UserLogin = new Login_BAL();
    private User_BAL us = new User_BAL();
    private RolePermission_BAL RP = new RolePermission_BAL();
    private DataTable dtLogin = new DataTable();
    private PayRoll_Session PayRollSession = new PayRoll_Session();

    // Log_BLL Log = new Log_BLL();gghhh
    private static int _UserID = 0;

    public static int UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }

    private static string RoleName;

    private void ShowLoading()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Show_Loading", "Show_Loading('btn_Login');", true);
    }

    private void HideLoading()
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide_Loading", "Hide_Loading('btnLogin');", true);
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
        {
            vt_tbl_User user = null;
            string encryptPassword = vt_Common.Encrypt(txtPassword.Text);
            string EmpCode = txtUserName.Text;
            if (EmpCode == "SuperAdmin")
            {
                user = dbContext.vt_tbl_User
                      .SingleOrDefault(w => w.UserName == "SuperAdmin" && w.Password == encryptPassword);

            }
            else
            {

                var user1 = dbContext.vt_tbl_User.Where(w => w.EmployeeEnrollId
                         .Equals(EmpCode)).SingleOrDefault();

                string encrippas = vt_Common.Decrypt(user1.Password);

                user = dbContext.vt_tbl_User.Where(w => w.EmployeeEnrollId
                       .Equals(EmpCode) && w.Password.Equals(encryptPassword) && w.Active == true).SingleOrDefault();

                //string encrippas= vt_Common.Decrypt(user.Password);
            }

            DataTable dt = new DataTable();

            if (user != null)
            {

                Session["UserId"] = user.UserId;
                Session["EmployeeID"] = user.EmployeeID;
                Session["UserName"] = user.UserName;
                Session["Password"] = vt_Common.Decrypt(user.Password);
                Session["FirstName"] = user.FirstName;
                Session["LastName"] = user.LastName;
                Session["Email"] = user.Email;
                Session["JoiningDate"] = user.CreatedOn;
                Session["CompanyId"] = !string.IsNullOrEmpty(user.CompanyId.ToString()) == false ? null : user.CompanyId;
                Session["CompanyName"] = Session["CompanyId"] == null ? null : user.vt_tbl_Company.CompanyName;
                Session["RoleId"] = !string.IsNullOrEmpty(user.RoleId.ToString()) == false ? null : user.RoleId;
                Session["Role"] = Session["RoleId"] == null ? null : user.vt_tbl_Role.Role;
                if (Session["CompanyId"] != null && Session["RoleId"] != null)
                {
                    dt = RP.GetPagePermissionpPagesByRole((int)user.RoleId);
                }

                if (dt.Rows.Count > 0)
                    Session["PagePermissions"] = dt;
                else
                    Session["PagePermissions"] = null;
                //check if password is 123456@ it should be change
                string encryptOldPassword = vt_Common.Encrypt("123456@");
                string roleValue = "4";
                if (encryptOldPassword == encryptPassword)
                {
                    var resetpas = dbContext.vt_tbl_User
                    .Where(w => w.Password != null && w.Password.Equals(encryptOldPassword) && w.RoleId.ToString() == roleValue)
                    .FirstOrDefault();
                    if (resetpas != null)
                    {
                        Response.Redirect("ChangePassword.aspx");
                        //Response.Redirect("Default.aspx");
                    }
                }
                Response.Redirect("Default.aspx");
            }
          
            else
            {
            lblErrorMsg.Text = "Invalid UserName or Password !";
            lblErrorMsg.Visible = true;
            }
            
           

            //else
            //{    
            //    lblErrorMsg.Text = "UserName and Password cannot be Empty !";
            //    lblErrorMsg.Visible = true;
            //}
            //}
            //else
            //{
            //    lblErrorMsg.Text = "UserName and Password cannot be Empty !";
        }
    }

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //if (!IsPostBack)
    //    //{
    //    //    txtUserName.Text = "";
    //    //    txtPassword.Text = "";
    //    //    FillDomains();
    //    //}


    //    int EmployeeID = Convert.ToInt32(Session["UserId"]);
    //    var encryptedPassword = vt_Common.Encrypt("123456@");

    //    using (var db = new vt_EMSEntities())
    //    {
    //        var user = db.vt_tbl_User.FirstOrDefault(x => x.UserId == EmployeeID);

    //        if (user != null && user.Password == encryptedPassword)
    //        {
    //            Session["IsDefaultPassword"] = true;  // Indicates default password
    //        }
    //        else
    //        {
    //            Session["IsDefaultPassword"] = false;
    //        }
    //    }
    //}
    private bool SendEmail()
    {
        string Subject = "Test Email";
        string ToEmail = "riaz.viftech@gmail.com";
        string BccEmail = string.Empty;

        //List<string> AttachedFiles = new List<string>();
        //AttachedFiles.Add(Server.MapPath(@"Uploads/Attachments/Employees.xlsx"));
        //AttachedFiles.Add(Server.MapPath(@"Uploads/Attachments/Test.xlsx"));

        string Body = string.Empty;
        using (StreamReader Reader = new StreamReader(Server.MapPath("~/Uploads/EmailTemplates/WelcomeEmail.html")))
        {
            Body = Reader.ReadToEnd();
        }

        return EmailSender.SendEmail(ToEmail, "", BccEmail, Subject, Body, null);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string encryptPassword = vt_Common.Decrypt("T+DTiJJoii0=");
            //SendEmail();
            FillDomains();
        }
    }

    private void CheckDB()
    {
        SqlConnection con = new SqlConnection(vt_Common.PayRollConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlTransaction trans = con.BeginTransaction();
        try
        {
            WriteLogfile("Check PSID From Database");
            dtLogin = UserLogin.CheckPSIDAndPassword(txtUserName.Text, vt_Common.Encrypt(txtPassword.Text), trans); //Check PSID
            WriteLogfile("Check PSID");
            if (dtLogin.Rows.Count > 0)
            {
                var Password = from SN in dtLogin.AsEnumerable()
                                   //where SN.Field<string>("UserName") == dtLogin.Rows[0]["UserName"].ToString() && SN.Field<string>("Password") == vt_Common.Encrypt(txtPassword.Text)
                               where SN.Field<string>("EmployeeName") == dtLogin.Rows[0]["EmployeeName"].ToString() && SN.Field<string>("EmpPassword") == vt_Common.Encrypt(txtPassword.Text)
                               select SN;
                if (Password != null && Password.Count() != 0)
                {
                    PayRollSession.EmployeeID = Convert.ToInt32(dtLogin.Rows[0]["EmployeeID"]);
                    PayRollSession.RoleID = Convert.ToInt32(dtLogin.Rows[0]["RoleID"]);
                    PayRollSession.RoleName = (dtLogin.Rows[0]["Role"]).ToString();
                    PayRollSession.RoleActive = Convert.ToBoolean(dtLogin.Rows[0]["RoleStatus"]);
                    PayRollSession.UserActive = Convert.ToBoolean(dtLogin.Rows[0]["RoleStatus"]);
                    PayRollSession.Password = (dtLogin.Rows[0]["EmpPassword"]).ToString();
                    PayRollSession.CompanyID = Convert.ToInt32(dtLogin.Rows[0]["CompanyID"]);
                    PayRollSession.EmployeeName = dtLogin.Rows[0]["EmployeeName"].ToString();
                    PayRollSession.Email = dtLogin.Rows[0]["Email"].ToString();
                    if (PayRollSession.UserActive == true)
                    {
                        if (PayRollSession.UserActive == true)
                        {
                            //PayRollSession.PermissionTable = RP.GetPagePermissionpPagesByRole(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), 0);
                            PayRollSession.PermissionTable = RP.GetPagePermissionpPagesByRole(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]));

                            PayRollSession.MenuTable = RP.GetMenuByRoleID(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), trans);
                            Session["PayrollSess"] = PayRollSession;
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                        }
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                else
                {
                    trans.Rollback();
                }
                //User_BAL user = new User_BAL();
                //Role_BAl role = new Role_BAl();

                // user.UserName = (dtLogin.Rows[0]["UserName"]).ToString();
                //user.CreatedDate = Convert.ToDateTime(dtLogin.Rows[0]["CreatedDate"]);
                //user.Email = (dtLogin.Rows[0]["Email"]).ToString();
                //user.FirstName = (dtLogin.Rows[0]["FirstName"]).ToString();
                //user.CreatedOn = Convert.ToDateTime(dtLogin.Rows[0]["CreatedOn"]);
                //user.CreatedBy = Convert.ToInt32(dtLogin.Rows[0]["CreatedBy"]);
                //user.Deleted = Convert.ToDateTime(dtLogin.Rows[0]["Deleted"]);
                //user.UpdatedOn = Convert.ToDateTime(dtLogin.Rows[0]["UpdatedOn"]);
                //user.UpdatedBy = Convert.ToInt32(dtLogin.Rows[0]["UpdatedBy"]);
                //user.DeletedBy = Convert.ToInt32(dtLogin.Rows[0]["DeletedBy"]);

                //User_BAL Sess = (User_BAL)Session["PayrollSess"];
                ////Sess.UserName

                //RoleName = dtLogin.Rows[0]["RoleName"].ToString();
                //goAMLSession.IsLock = dtLogin.Rows[0]["IsLock"].ToString() == "" || dtLogin.Rows[0]["IsLock"] == null ? false : Convert.ToBoolean(dtLogin.Rows[0]["IsLock"]);
                //goAMLSession.LockReason = dtLogin.Rows[0]["LockReason"].ToString();
                //goAMLSession.LoginAttemptFailed = dtLogin.Rows[0]["LoginAttemptFailed"].ToString() == "" || dtLogin.Rows[0]["LoginAttemptFailed"] == null ? 0 : Convert.ToInt32(dtLogin.Rows[0]["LoginAttemptFailed"]);
                ////goAMLSession.ActionID = Convert.ToInt32(PM.UserAction.SystemUsers);

                //var Password = from SN in dtLogin.AsEnumerable()
                //               where SN.Field<string>("UserName") == dtLogin.Rows[0]["UserName"].ToString() && SN.Field<string>("Password") == vt_Common.Encrypt(txtPassword.Text)
                //               select SN;
                //if (Password != null && Password.Count() != 0)
                //{
                //    WriteLogfile("Password Ok ");
                //    dtLogin = Password.CopyToDataTable();
                //    WriteLogfile("Copy Password TO dataTable");
                //    if (dtLogin.Rows.Count > 0)
                //    {
                //        WriteLogfile("DTlogin Rows is greater");
                //        goAMLSession.UserID = UserID;
                //        WriteLogfile("Set values In session");
                //        goAMLSession.PSID = dtLogin.Rows[0]["PSID"].ToString();
                //        goAMLSession.LoginName = dtLogin.Rows[0]["LoginName"].ToString();
                //        goAMLSession.Designation = dtLogin.Rows[0]["Designation"].ToString();
                //        goAMLSession.LastLoginDate = dtLogin.Rows[0]["LastLoginDate"].ToString() == "" || dtLogin.Rows[0]["LastLoginDate"] == null ? DateTime.Now : Convert.ToDateTime(dtLogin.Rows[0]["LastLoginDate"]);
                //        //goAMLSession.LastChangePasswordDate = dtLogin.Rows[0]["LastChangePasswordDate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(dtLogin.Rows[0]["LastChangePasswordDate"]);
                //        goAMLSession.RoleID = Convert.ToInt32(dtLogin.Rows[0]["RoleID"]);
                //        //goAMLSession.UserType = Convert.ToInt32(dtLogin.Rows[0]["UserType"]);
                //        //goAMLSession.Password = EncryptDecrypt.Encrypt(txtPassword.Text);
                //        goAMLSession.UserActive = Convert.ToBoolean(dtLogin.Rows[0]["UserActive"]);
                //        goAMLSession.RoleActive = Convert.ToBoolean(dtLogin.Rows[0]["RoleActive"]);
                //        goAMLSession.RoleName = RoleName;
                //        goAMLSession.Permission = "Checked";
                //        goAMLSession.LastUnsuccessfulLoginDate = dtLogin.Rows[0]["LastUnsuccessfulLogin"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dtLogin.Rows[0]["LastUnsuccessfulLogin"]);
                //        //goAMLSession.Domain = ddlDomain.SelectedItem.Text;
                //        WriteLogfile("Set values In session");
                //        if (goAMLSession.RoleActive == true)
                //        {
                //            WriteLogfile("Role Active is True");
                //            if (goAMLSession.UserActive == true)
                //            {
                //                WriteLogfile("User Active is True");
                //                if (goAMLSession.IsLock == false)
                //                {
                //                    WriteLogfile("IsLock is True");
                //                    goAMLSession.PermissionTable = RP.GetPagePermissionpPagesByRole(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), 0);
                //                    goAMLSession.MenuTable = RP.GetMenuByRoleID(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), trans);
                //                    goAMLSession.LoginCounter = dtLogin.Rows[0]["LoginCounter"].ToString() == "" || dtLogin.Rows[0]["LoginCounter"] == null ? 0 : Convert.ToInt32(dtLogin.Rows[0]["LoginCounter"]);

                //                    //Login History
                //                    //Log.UserID = UserID;
                //                    //Log.LoginDateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss"));
                //                    //goAMLSession.LoginHistoryID = Log.CreateLoginHistory(Log, trans);
                //                    //Login Attempt Failed Default Value 0
                //                    //Log.UserID = UserID;

                //                    //Log.LoginAttemptFailed = 0;
                //                    //WriteLogfile("Modify Attempt Failed");
                //                    //Log.ModifyAttemptFailed(Log, trans);
                //                    //goAMLSession.LoginAttemptFailed = dtLogin.Rows[0]["LoginAttemptFailed"].ToString() == "" || dtLogin.Rows[0]["LoginAttemptFailed"] == null ? 0 : Convert.ToInt32(dtLogin.Rows[0]["LoginAttemptFailed"]);
                //                    //IsLogin Update
                //                    //Log.UserID = UserID;
                //                    //Log.IsLogin = true;
                //                    //WriteLogfile("Update IsLogin Started");
                //                    //Log.UpdateIsLogin(Log, trans);
                //                    //WriteLogfile("Update IsLogin Completed");
                //                    //Login Counter Update
                //                    //Log.UserID = UserID;
                //                    //Log.LoginCounter = goAMLSession.LoginCounter + 1;
                //                    //Log.LastLoginDate = DateTime.Now;
                //                    //WriteLogfile("Update LoginCounter Started");
                //                    //Log.UpdateLoginCounter(Log, trans);
                //                    //WriteLogfile("Update LoginCounter Completed");
                //                    //trans.Commit();
                //                    //WriteLogfile("Trans Committed");
                //                    //Session["goAMLSession"] = goAMLSession;
                //                    //WriteLogfile("Insert Logs started");
                //                    //Log.InsertLogs(UserID, 0, 0, "Login Successfully");

                //                    //WriteLogfile("Insert Logs COmpleted");
                //                    //JQ.ToastMsg(this.Page, "1", "Login Successfully", "top-right");
                //                    //WriteLogfile("Toast Msg Shown");
                //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectAfterDelayFn", "RedirectAfterDelayFn('Default.aspx');", true);// Respone Redirect Default Page
                //                }
                //                else
                //                {
                //                    WriteLogfile("Your account has been locked out, contact administrator");
                //                    //JQ.ToastMsg(this.Page, "4", "Your account has been locked out, contact administrator", "top-right");
                //                    WriteLogfile("Transaction Rollbacked");
                //                    trans.Rollback();
                //                    HideLoading();
                //                }
                //            }
                //            else
                //            {
                //                WriteLogfile("User not Active");
                //                //JQ.ToastMsg(this.Page, "4", "User not Active", "top-right");
                //                WriteLogfile("Transaction Rollbacked");
                //                trans.Rollback();
                //                HideLoading();
                //                //User not Active
                //            }
                //        }
                //        else
                //        {
                //            WriteLogfile("User not Active");
                //            //JQ.ToastMsg(this.Page, "4", "Role not Active", "top-right");
                //            WriteLogfile("Transaction Rollbacked");
                //            trans.Rollback();
                //            //Role not Active
                //        }
                //    }
                //}
                //else
                //{
                //    if (RoleName != "SuperAdmin")
                //    {
                //        HideLoading();

                //        //Log.UserID = UserID;
                //        //Log.LoginAttemptFailed = goAMLSession.LoginAttemptFailed + 1;
                //        //Log.ModifyAttemptFailed(Log, trans);

                //        //if (Log.LoginAttemptFailed >= 3)
                //        //{
                //        //    if (Log.LoginAttemptFailed == 3)
                //        //    {
                //        //        Log.UserID = UserID;
                //        //        Log.IsLock = true;
                //        //        Log.LockDate = DateTime.Now;
                //        //        Log.LockReason = "Three Fail Login Attemptes";
                //        //        Log.UserLock(Log, trans);
                //        //        trans.Commit();
                //        //        JQ.ToastMsg(this.Page, "4", "Three Fail Login Attemptes", "top-right");
                //        //        HideLoading();
                //        //        //Lock User
                //        //    }
                //        //    else
                //        //    {
                //        //        JQ.ToastMsg(this.Page, "4", "Your account has been locked out, contact administrator", "top-right");
                //        //        trans.Commit();
                //        //        HideLoading();
                //        //    }
                //        //}
                //        //else
                //        //{
                //        //    JQ.ToastMsg(this.Page, "4", "Invaild Password", "top-right");
                //        //    Log.UpdateUnsuccessfullLogin(UserID, trans);
                //        //    Log.InsertLogs(UserID, 0, 0, "Invaild Password");
                //        //    trans.Commit();
                //        //    HideLoading();
                //        //    //Invaild Password
                //        //}
                //    }
                //    else
                //    {
                //        //JQ.ToastMsg(this.Page, "4", "Invaild Password", "top-right");
                //        //Log.UpdateUnsuccessfullLogin(UserID, trans);
                //        //Log.InsertLogs(UserID, 0, 0, "Invaild Password");
                //        //trans.Commit();
                //        HideLoading();
                //        //InsertLogs(UserID, 0, "Invaild Password", Convert.ToInt32(PM.UserAction.SystemUsers));
                //        //Invaild Password
                //    }
                //}
            }
            else
            {
                MsgBox.Show(Page, MsgBox.success, us.UserName, "Successfully Saved");
                //JQ.ToastMsg(this.Page, "3", "Invaild username", "top-right");
                trans.Rollback();
                //HideLoading();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            trans.Rollback();
            throw ex;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    private void CheckDBUser()
    {
        SqlConnection con = new SqlConnection(vt_Common.PayRollConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlTransaction trans = con.BeginTransaction();
        try
        {
            WriteLogfile("Check PSID From Database");
            dtLogin = UserLogin.CheckPSIDAndPasswordUser(txtUserName.Text, vt_Common.Encrypt(txtPassword.Text), trans); //Check PSID
            WriteLogfile("Check PSID");
            if (dtLogin.Rows.Count > 0)
            {
                var Password = from SN in dtLogin.AsEnumerable()
                               where SN.Field<string>("UserName") == dtLogin.Rows[0]["UserName"].ToString() && SN.Field<string>("Password") == vt_Common.Encrypt(txtPassword.Text)
                               //where SN.Field<string>("UserName") == dtLogin.Rows[0]["UserName"].ToString() && SN.Field<string>("EmpPassword") == vt_Common.Encrypt(txtPassword.Text)
                               select SN;
                if (Password != null && Password.Count() != 0)
                {
                    PayRollSession.UserID = Convert.ToInt32(dtLogin.Rows[0]["UserID"]);
                    PayRollSession.RoleID = Convert.ToInt32(dtLogin.Rows[0]["RoleID"]);
                    PayRollSession.RoleName = (dtLogin.Rows[0]["Role"]).ToString();
                    PayRollSession.RoleActive = Convert.ToBoolean(dtLogin.Rows[0]["RoleStatus"]);
                    PayRollSession.UserActive = Convert.ToBoolean(dtLogin.Rows[0]["Active"]);
                    PayRollSession.Password = (dtLogin.Rows[0]["Password"]).ToString();
                    PayRollSession.CompanyID = Convert.ToInt32(dtLogin.Rows[0]["CompanyID"]);
                    PayRollSession.UserName = dtLogin.Rows[0]["UserName"].ToString();
                    PayRollSession.Email = dtLogin.Rows[0]["Email"].ToString();
                    if (PayRollSession.UserActive != false)
                    {
                        if (PayRollSession.UserActive != false)
                        {
                            //PayRollSession.PermissionTable = RP.GetPagePermissionpPagesByRole(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), Convert.ToInt32(dtLogin.Rows[0]["UserID"]));

                            PayRollSession.PermissionTable = RP.GetPagePermissionpPagesByRole(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]));
                            PayRollSession.MenuTable = RP.GetMenuByRoleID(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), trans);
                            Session["PayrollSess"] = PayRollSession;
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                        }
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                else
                {
                    trans.Rollback();
                }
                //User_BAL user = new User_BAL();
                //Role_BAl role = new Role_BAl();

                // user.UserName = (dtLogin.Rows[0]["UserName"]).ToString();
                //user.CreatedDate = Convert.ToDateTime(dtLogin.Rows[0]["CreatedDate"]);
                //user.Email = (dtLogin.Rows[0]["Email"]).ToString();
                //user.FirstName = (dtLogin.Rows[0]["FirstName"]).ToString();
                //user.CreatedOn = Convert.ToDateTime(dtLogin.Rows[0]["CreatedOn"]);
                //user.CreatedBy = Convert.ToInt32(dtLogin.Rows[0]["CreatedBy"]);
                //user.Deleted = Convert.ToDateTime(dtLogin.Rows[0]["Deleted"]);
                //user.UpdatedOn = Convert.ToDateTime(dtLogin.Rows[0]["UpdatedOn"]);
                //user.UpdatedBy = Convert.ToInt32(dtLogin.Rows[0]["UpdatedBy"]);
                //user.DeletedBy = Convert.ToInt32(dtLogin.Rows[0]["DeletedBy"]);

                //User_BAL Sess = (User_BAL)Session["PayrollSess"];
                ////Sess.UserName

                //RoleName = dtLogin.Rows[0]["RoleName"].ToString();
                //goAMLSession.IsLock = dtLogin.Rows[0]["IsLock"].ToString() == "" || dtLogin.Rows[0]["IsLock"] == null ? false : Convert.ToBoolean(dtLogin.Rows[0]["IsLock"]);
                //goAMLSession.LockReason = dtLogin.Rows[0]["LockReason"].ToString();
                //goAMLSession.LoginAttemptFailed = dtLogin.Rows[0]["LoginAttemptFailed"].ToString() == "" || dtLogin.Rows[0]["LoginAttemptFailed"] == null ? 0 : Convert.ToInt32(dtLogin.Rows[0]["LoginAttemptFailed"]);
                ////goAMLSession.ActionID = Convert.ToInt32(PM.UserAction.SystemUsers);

                //var Password = from SN in dtLogin.AsEnumerable()
                //               where SN.Field<string>("UserName") == dtLogin.Rows[0]["UserName"].ToString() && SN.Field<string>("Password") == vt_Common.Encrypt(txtPassword.Text)
                //               select SN;
                //if (Password != null && Password.Count() != 0)
                //{
                //    WriteLogfile("Password Ok ");
                //    dtLogin = Password.CopyToDataTable();
                //    WriteLogfile("Copy Password TO dataTable");
                //    if (dtLogin.Rows.Count > 0)
                //    {
                //        WriteLogfile("DTlogin Rows is greater");
                //        goAMLSession.UserID = UserID;
                //        WriteLogfile("Set values In session");
                //        goAMLSession.PSID = dtLogin.Rows[0]["PSID"].ToString();
                //        goAMLSession.LoginName = dtLogin.Rows[0]["LoginName"].ToString();
                //        goAMLSession.Designation = dtLogin.Rows[0]["Designation"].ToString();
                //        goAMLSession.LastLoginDate = dtLogin.Rows[0]["LastLoginDate"].ToString() == "" || dtLogin.Rows[0]["LastLoginDate"] == null ? DateTime.Now : Convert.ToDateTime(dtLogin.Rows[0]["LastLoginDate"]);
                //        //goAMLSession.LastChangePasswordDate = dtLogin.Rows[0]["LastChangePasswordDate"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(dtLogin.Rows[0]["LastChangePasswordDate"]);
                //        goAMLSession.RoleID = Convert.ToInt32(dtLogin.Rows[0]["RoleID"]);
                //        //goAMLSession.UserType = Convert.ToInt32(dtLogin.Rows[0]["UserType"]);
                //        //goAMLSession.Password = EncryptDecrypt.Encrypt(txtPassword.Text);
                //        goAMLSession.UserActive = Convert.ToBoolean(dtLogin.Rows[0]["UserActive"]);
                //        goAMLSession.RoleActive = Convert.ToBoolean(dtLogin.Rows[0]["RoleActive"]);
                //        goAMLSession.RoleName = RoleName;
                //        goAMLSession.Permission = "Checked";
                //        goAMLSession.LastUnsuccessfulLoginDate = dtLogin.Rows[0]["LastUnsuccessfulLogin"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dtLogin.Rows[0]["LastUnsuccessfulLogin"]);
                //        //goAMLSession.Domain = ddlDomain.SelectedItem.Text;
                //        WriteLogfile("Set values In session");
                //        if (goAMLSession.RoleActive == true)
                //        {
                //            WriteLogfile("Role Active is True");
                //            if (goAMLSession.UserActive == true)
                //            {
                //                WriteLogfile("User Active is True");
                //                if (goAMLSession.IsLock == false)
                //                {
                //                    WriteLogfile("IsLock is True");
                //                    goAMLSession.PermissionTable = RP.GetPagePermissionpPagesByRole(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), 0);
                //                    goAMLSession.MenuTable = RP.GetMenuByRoleID(Convert.ToInt32(dtLogin.Rows[0]["RoleID"]), trans);
                //                    goAMLSession.LoginCounter = dtLogin.Rows[0]["LoginCounter"].ToString() == "" || dtLogin.Rows[0]["LoginCounter"] == null ? 0 : Convert.ToInt32(dtLogin.Rows[0]["LoginCounter"]);

                //                    //Login History
                //                    //Log.UserID = UserID;
                //                    //Log.LoginDateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss"));
                //                    //goAMLSession.LoginHistoryID = Log.CreateLoginHistory(Log, trans);
                //                    //Login Attempt Failed Default Value 0
                //                    //Log.UserID = UserID;

                //                    //Log.LoginAttemptFailed = 0;
                //                    //WriteLogfile("Modify Attempt Failed");
                //                    //Log.ModifyAttemptFailed(Log, trans);
                //                    //goAMLSession.LoginAttemptFailed = dtLogin.Rows[0]["LoginAttemptFailed"].ToString() == "" || dtLogin.Rows[0]["LoginAttemptFailed"] == null ? 0 : Convert.ToInt32(dtLogin.Rows[0]["LoginAttemptFailed"]);
                //                    //IsLogin Update
                //                    //Log.UserID = UserID;
                //                    //Log.IsLogin = true;
                //                    //WriteLogfile("Update IsLogin Started");
                //                    //Log.UpdateIsLogin(Log, trans);
                //                    //WriteLogfile("Update IsLogin Completed");
                //                    //Login Counter Update
                //                    //Log.UserID = UserID;
                //                    //Log.LoginCounter = goAMLSession.LoginCounter + 1;
                //                    //Log.LastLoginDate = DateTime.Now;
                //                    //WriteLogfile("Update LoginCounter Started");
                //                    //Log.UpdateLoginCounter(Log, trans);
                //                    //WriteLogfile("Update LoginCounter Completed");
                //                    //trans.Commit();
                //                    //WriteLogfile("Trans Committed");
                //                    //Session["goAMLSession"] = goAMLSession;
                //                    //WriteLogfile("Insert Logs started");
                //                    //Log.InsertLogs(UserID, 0, 0, "Login Successfully");

                //                    //WriteLogfile("Insert Logs COmpleted");
                //                    //JQ.ToastMsg(this.Page, "1", "Login Successfully", "top-right");
                //                    //WriteLogfile("Toast Msg Shown");
                //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectAfterDelayFn", "RedirectAfterDelayFn('Default.aspx');", true);// Respone Redirect Default Page
                //                }
                //                else
                //                {
                //                    WriteLogfile("Your account has been locked out, contact administrator");
                //                    //JQ.ToastMsg(this.Page, "4", "Your account has been locked out, contact administrator", "top-right");
                //                    WriteLogfile("Transaction Rollbacked");
                //                    trans.Rollback();
                //                    HideLoading();
                //                }
                //            }
                //            else
                //            {
                //                WriteLogfile("User not Active");
                //                //JQ.ToastMsg(this.Page, "4", "User not Active", "top-right");
                //                WriteLogfile("Transaction Rollbacked");
                //                trans.Rollback();
                //                HideLoading();
                //                //User not Active
                //            }
                //        }
                //        else
                //        {
                //            WriteLogfile("User not Active");
                //            //JQ.ToastMsg(this.Page, "4", "Role not Active", "top-right");
                //            WriteLogfile("Transaction Rollbacked");
                //            trans.Rollback();
                //            //Role not Active
                //        }
                //    }
                //}
                //else
                //{
                //    if (RoleName != "SuperAdmin")
                //    {
                //        HideLoading();

                //Log.UserID = UserID;
                //        //Log.LoginAttemptFailed = goAMLSession.LoginAttemptFailed + 1;
                //        //Log.ModifyAttemptFailed(Log, trans);

                //        //if (Log.LoginAttemptFailed >= 3)
                //        //{
                //        //    if (Log.LoginAttemptFailed == 3)
                //        //    {
                //        //        Log.UserID = UserID;
                //        //        Log.IsLock = true;
                //        //        Log.LockDate = DateTime.Now;
                //        //        Log.LockReason = "Three Fail Login Attemptes";
                //        //        Log.UserLock(Log, trans);
                //        //        trans.Commit();
                //        //        JQ.ToastMsg(this.Page, "4", "Three Fail Login Attemptes", "top-right");
                //        //        HideLoading();
                //        //        //Lock User
                //        //    }
                //        //    else
                //        //    {
                //        //        JQ.ToastMsg(this.Page, "4", "Your account has been locked out, contact administrator", "top-right");
                //        //        trans.Commit();
                //        //        HideLoading();
                //        //    }
                //        //}
                //        //else
                //        //{
                //        //    JQ.ToastMsg(this.Page, "4", "Invaild Password", "top-right");
                //        //    Log.UpdateUnsuccessfullLogin(UserID, trans);
                //        //    Log.InsertLogs(UserID, 0, 0, "Invaild Password");
                //        //    trans.Commit();
                //        //    HideLoading();
                //        //    //Invaild Password
                //        //}
                //    }
                //    else
                //    {
                //        //JQ.ToastMsg(this.Page, "4", "Invaild Password", "top-right");
                //        //Log.UpdateUnsuccessfullLogin(UserID, trans);
                //        //Log.InsertLogs(UserID, 0, 0, "Invaild Password");
                //        //trans.Commit();
                //        HideLoading();
                //        //InsertLogs(UserID, 0, "Invaild Password", Convert.ToInt32(PM.UserAction.SystemUsers));
                //        //Invaild Password
                //    }
                //}
            }
            else
            {
                MsgBox.Show(Page, MsgBox.success, us.UserName, "Successfully Saved");
                //JQ.ToastMsg(this.Page, "3", "Invaild username", "top-right");
                trans.Rollback();
                //HideLoading();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            trans.Rollback();
            throw ex;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //int cid = vt_Common.CheckInt(ddlCompany.SelectedValue);
            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                string Pwd = vt_Common.Encrypt(txtPassword.Text);
                //if (cid > 0 && !(Pwd.Equals(string.Empty)))
                string dyc = vt_Common.Decrypt("f1nqOHphoy4=");

                if (!(Pwd.Equals(string.Empty)))
                {
                    //if (ddlUserType.SelectedValue == "Admin")
                    //{
                    //    vt_tbl_Company c = db.vt_tbl_Company.Where(x => x.CompanyID == cid && x.UserName.Equals(txtUserName.Text) && x.Password.Equals(Pwd)).SingleOrDefault();
                    //    if (c != null)
                    //    {
                    //        EMS_Session s = new EMS_Session();
                    //        s.Company = c;
                    //        Session["EMS_Session"] = s;
                    //        Response.Redirect("Default.aspx");
                    //    }
                    //    else
                    //    {
                    //        MsgBox.Show(this, "Invalid email or password");
                    //    }
                    //}
                    //else
                    {
                        vt_tbl_User user = db.vt_tbl_User.Where(x => x.UserName.Equals(txtUserName.Text.Trim()) && x.Password.Equals(Pwd)).SingleOrDefault();
                        vt_tbl_Employee emp = db.vt_tbl_Employee.Where(x => x.EmployeeName.Equals(txtUserName.Text.Trim()) && x.EmpPassword.Equals(Pwd)).SingleOrDefault();
                        if (emp != null)
                        {
                            CheckDB();
                            vt_tbl_Company c = db.vt_tbl_Company.Where(x => x.CompanyID == emp.CompanyID).SingleOrDefault();
                            EMS_Session s = new EMS_Session();
                            //PayRoll_Session p = new PayRoll_Session();
                            //EMS_Session s = new EMS_Session();
                            s.Employee = emp;
                            s.Company = c;
                            Session["EMS_Session"] = s;
                            //Session["PayrollSess"] = p;
                            Response.Redirect("Default.aspx");
                        }
                        else if (user != null)
                        {
                            CheckDBUser();
                            vt_tbl_Company c = db.vt_tbl_Company.Where(x => x.CompanyID == user.CompanyId).SingleOrDefault();
                            EMS_Session s = new EMS_Session();
                            //PayRoll_Session p = new PayRoll_Session();
                            //EMS_Session s = new EMS_Session();
                            s.user = user;
                            s.Company = c;
                            Session["EMS_Session"] = s;
                            //Session["PayrollSess"] = p;
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            lblErrorMsg.Text = "Invalid email or password";
                            lblErrorMsg.Visible = true;
                        }
                    }
                }
            }
            else
            {
                lblErrorMsg.Text = "Please Enter Username or Password Correctly";
                lblErrorMsg.Visible = true;
            }
        }
    }

    public static void WriteLogfile(string mAction)
    {
        try
        {
            string pa = ConfigurationManager.AppSettings["pa"].ToString();
            string Narration = DateTime.Now.ToString() + " : " + mAction;
            using (StreamWriter write = new StreamWriter(new FileStream(pa, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                write.WriteLine(Narration);
            }
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }
    }

    private void FillDomains()
    {
        string Domains = ConfigurationManager.AppSettings["DomainName"];
        if (Domains != null)
        {
            string[] DomainName = Domains.Split(',').ToArray();
            for (int i = 0; i < DomainName.Length; i++)
            {
                if (DomainName[i] != "" && DomainName[i] != null)
                {
                    string Name = DomainName[i];
                    ListItem Litem = new ListItem();
                    Litem.Text = Name;
                    Litem.Value = Name;
                    //ddlDomain.Items.Add(Litem);
                }
            }
            ListItem Litema = new ListItem();
            Litema.Text = "Database";
            Litema.Value = "Database";
            //ddlDomain.Items.Insert(0, Litema);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CheckEmail(string Email)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        UserCredentials Obj = new UserCredentials();

        vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.Email.Equals(Email)).SingleOrDefault();
        if (EmployeeObj == null)
        {
            Obj.Status = false;
        }
        else
        {
            if (EmployeeObj.EmpPassword != null && !string.IsNullOrEmpty(EmployeeObj.EmpPassword) && !string.IsNullOrWhiteSpace(EmployeeObj.EmpPassword))
            {
                Obj.Status = false;
                Obj.PasswordSet = true;
            }
            else if (EmployeeObj.Email == Email)
            {
                Obj.Status = true;
                Obj.PasswordSet = false;
                Obj.Username = EmployeeObj.EmployeeName;
            }
        }
        return JsonConvert.SerializeObject(Obj);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool SetPassword(string Email, string Password)
    {
        vt_EMSEntities db = new vt_EMSEntities();

        vt_tbl_Employee EmployeeObj = db.vt_tbl_Employee.Where(x => x.Email.Equals(Email)).SingleOrDefault();
        if (EmployeeObj != null)
        {
            EmployeeObj.EmpPassword = vt_Common.Encrypt(Password);
            db.Entry(EmployeeObj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        return true;
    }

    //protected void saveButton_Click(object sender, EventArgs e)
    //{
    //    var user = dbContext.vt_tbl_User
    //   .Where(w => ((w.Email.Equals(txtUserName.Text.Trim()) || w.UserName.Equals(txtUserName.Text.Trim())) || (c != null && c.Equals(txtUserName.Text.Trim())))
    //       && w.Password.Equals()
    //       && w.Active == true)
    //   .FirstOrDefault();


    //}
}

public class UserCredentials
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
    public bool PasswordSet { get; set; }
}