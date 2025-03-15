using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            //}        
            //if (Session["EMS_Session"] != null)
            //{
            if (!IsPostBack)
            {

                //// check if password is 123456@ then redirect page to  changePassword.aspx
                //int EmployeeID = Convert.ToInt32(Session["UserId"]);
                //var encryptedPassword = vt_Common.Encrypt("123456@");

                //using (var db = new vt_EMSEntities())
                //{
                //    var user = db.vt_tbl_User.FirstOrDefault(x => x.UserId == EmployeeID);

                //    if (user != null && user.Password == encryptedPassword)
                //    {
                //        Response.Redirect("ChangePassword.aspx");
                //    }
                //}



                UserLabel.Text = Session["UserName"].ToString();

                if ((Session["CompanyId"] != null || Session["CompanyId"] == null) && Session["RoleId"] == null)
                {
                    DesigLbl.Text = "SuperAdmin";
                }
                else
                {
                    DesigLbl.Text = Session["Role"].ToString();
                }

                if (Session["JoiningDate"] != null)
                {
                    DateTime joiningDate = vt_Common.CheckDateTime(Session["JoiningDate"]);
                    DateLbl.Text = String.Format("{0:d}", joiningDate);
                }
                else
                {
                    DateLbl.Text = String.Format("{0:d}", DateTime.Now);
                }

                if (!string.IsNullOrEmpty(Session["ImageName"] as string))
                    Userimg.Src = "~/images/Employees/" + Session["ImageName"].ToString();

                BindMenu();

                /*if (user != null)
                {
                    UserLabel.Text = s.Employee.EmployeeName;

                    NameLbl.Text = s.Employee.FirstName;

                    if (s.Employee.ImageName == "" || s.Employee.ImageName == null)
                    {
                        Userimg.Src = "~/images/user-image.png";
                        Userimg1.Src = "~/images/user-image.png";
                    }
                    else
                    {
                        Userimg.Src = "~/images/Employees/" + s.Employee.ImageName;
                        Userimg1.Src = "~/images/Employees/" + s.Employee.ImageName;
                    }
                    if (((EMS_Session)Session["EMS_Session"]).Company == null)
                    {
                        DesigLbl.Text = "SuperAdmin";


                    }
                    else
                    {
                        using (vt_EMSEntities db = new vt_EMSEntities())
                        {
                            vt_tbl_Designation des = db.vt_tbl_Designation.FirstOrDefault(x => x.DesignationID == s.Employee.DesignationID);
                            string d = des.Designation;
                            DesigLbl.Text = d;
                            if (s.Employee.JoiningDate != null)
                            {

                                DateTime date = vt_Common.CheckDateTime(s.Employee.JoiningDate);
                                DateLbl.Text = String.Format("{0:d}", date);
                            }
                            check();

                            //var roleDetail = db.vt_tbl_Role.Where(x => x.RoleID == s.user.RoleId).FirstOrDefault();

                            //if (roleDetail.RoleID != 1)
                            //{
                            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('._Administration').hide();$('._Employee').hide();$('._Attendance').hide();$('._InputModules').hide();$('._SalaryGen').hide();$('._SalaryGenReport').hide();", true);
                            //}
                        }
                    }
                    checkEmp();
                }
                else if (employee != null)
                {

                }
                else
                {
                    UserLabel.Text = s.user.UserName;
                    CheckAllowPages();
                    //checkEmp();
                    check();


                }*/


                //checkEmp();

                /*this.bindLeveList();
                this.bindProbationList();*/
            }
            else
            {
            }
        }        
    }

    #region Control Events

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (Session["PayrollSess"] != null)
    //    {
    //        if (!IsPostBack)
    //        {
    //            //PayRoll_Session s = (PayRoll_Session)Session["PayrollSess"];

    //            CheckAllowPages();
    //            check();
    //            //checkEmp();
    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("Login.aspx");
    //    }
    //}
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        //Session.Remove("EMS_Session");
        Session.Remove("User");
        Session.Clear();
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
    protected void btnLeaveList_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("LeaveAppNotification.aspx?LeaveApplicationID=" + e.CommandArgument + "");
    }
    protected void btnContractList_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("EmpContractNotif.aspx?EmployeeID=" + e.CommandArgument + "");
    }
    #endregion
    #region HelperMethod
    void bindLeveList()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var query = (from l in db.vt_tbl_LeaveApplication
                         join emp in db.vt_tbl_Employee on l.EnrollId equals emp.EmployeeID
                         where l.isApproved == false && l.isRejected == false
                         select new
                         {
                             l.LeaveApplicationID,
                             emp.EmployeeID,
                             emp.EmployeeName
                         }).ToList();
            LeaveNotifList.DataSource = query;
            LeaveNotifList.DataBind();
        }
    }
    void bindProbationList()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //var query = db.VT_SP_GetEmployeeContract();
            //ProbationList.DataSource = query;
            //ProbationList.DataBind();
        }
    }
    private void CheckAllowPages()
    {
        string PageUrl = string.Empty;
        ModuleClass Module = new ModuleClass();
        PayRoll_Session PayRoll = (PayRoll_Session)Session["PayrollSess"];
        if (PayRoll.UserName != "")
        {
            if (PayRoll.UserName != null)
            {
                PageUrl = Request.Url.Segments.Last();
                DataRow[] dr = PayRoll.PermissionTable.Select("PageUrl='" + PageUrl.ToString() + "'");
                if (dr.Length > 0)
                {
                    if (true)
                    {
                        PayRoll.Can_Insert = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_Insert"]) : false;
                        PayRoll.Can_Update = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_Update"]) : false;
                        PayRoll.Can_Delete = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_Delete"]) : false;
                        PayRoll.Can_View = dr.Length > 0 ? Convert.ToBoolean(dr[0]["Can_View"]) : false;
                    }
                    else
                    {
                        Response.Redirect("" + PayRoll.PageRefrence.ToString() + "");
                    }
                }
            }
        }
    }
    private void checkEmp(bool isVisible)
    {
       
        InputModules.Visible = isVisible;
        SalaryGeneration.Visible = isVisible;
        reportGenerator.Visible = isVisible;
        Administration.Visible = isVisible;
        AttendenceSheet.Visible = isVisible;
        AttendanceModule.Visible = isVisible;
        Roles.Visible = isVisible;
        PayRollMenu.Visible = isVisible;
        Dashboard.Visible = isVisible;
        User.Visible = isVisible;
        CreateEmployeeAssest.Visible = isVisible;
        assetassign.Visible = isVisible;
        approveresign.Visible = isVisible;
        empresign.Visible = isVisible;
        termlist.Visible = IsViewStateEnabled;
    }
    private void BindMenu()
    {
        try
        {
            ModuleClass Module = new ModuleClass();
            DataTable dt = new DataTable();
            if (Session["PagePermissions"] != null)
            {
                dt = (DataTable)Session["PagePermissions"];
            }
            
            //PayRoll_Session PayRoll = (PayRoll_Session)Session["PayrollSess"];
            //EMS_Session EMS = (EMS_Session)Session["EMS_Session"];
            if (DesigLbl.Text == "SuperAdmin")
            {
                checkEmp(true);
            }
            /*if (PayRoll.UserName == "Superadmin")
            {
                checkEmp();
            }*/
            else
            {
                //DataRow[] dr = PayRoll.PermissionTable.Select("PageUrl='" + PageUrl.ToString() + "'");
                //var dt = PayRoll.PermissionTable;
                //List<ModuleClass> studentList = new List<ModuleClass>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Module.ModuleID = Convert.ToInt32(dt.Rows[i]["ModuleID"]);
                        Module.ModuleName = dt.Rows[i]["ModuleName"].ToString();

                        if (Module.ModuleID == 3)
                        {
                            Employes.Visible = true;
                        }
                        if (Module.ModuleID == 6)
                        {
                            InputModules.Visible = true;
                        }
                        if (Module.ModuleID == 7)
                        {
                            SalaryGeneration.Visible = true;
                        }
                        if (Module.ModuleID == 8)
                        {
                            reportGenerator.Visible = true;
                        }
                        if (Module.ModuleID == 9)
                        {
                            Administration.Visible = true;
                        }
                        if (Module.ModuleID == 10)
                        {
                            AttendenceSheet.Visible = true;
                        }
                        if (Module.ModuleID == 11)
                        {
                            AttendanceModule.Visible = true;
                        }
                        if (Module.ModuleID == 1002)
                        {
                            Roles.Visible = true;
                        }
                        if (Module.ModuleID == 1003)
                        {
                            PayRollMenu.Visible = true;
                        }
                        if (Module.ModuleID == 1004)
                        {
                            
                            User.Visible = true;
                        }
                        if (Module.ModuleID == 1005)
                        {

                            CreateEmployeeAssest.Visible = true;
                        }
                        if (Module.ModuleID == 2005)
                        {

                            assetassign.Visible = true;
                        }
                        if (Module.ModuleID == 2006)
                        {

                            approveresign.Visible = true;
                        }
                        if (Module.ModuleID == 2008)
                        {
                            empresign.Visible = true;
                        }
                        if (Module.ModuleID == 2008)
                        {
                            termlist.Visible = true;

                        }
                    }
                }
                else
                {
                    checkEmp(false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    //private void check()
    //{


    //    try
    //    {


    //        ModuleClass Module = new ModuleClass();
    //        PayRoll_Session PayRoll = (PayRoll_Session)Session["PayrollSess"];
    //        EMS_Session EMS = (EMS_Session)Session["EMS_Session"];



    //        if (EMS.Employee.EmployeeName == "Superadmin")
    //        {

    //            Employes.Visible = true;

    //            InputModules.Visible = true;

    //            SalaryGeneration.Visible = true;

    //            reportGenerator.Visible = true;

    //            Administration.Visible = true;

    //            AttendenceSheet.Visible = true;

    //            AttendanceModule.Visible = true;

    //            Roles.Visible = true;
    //        }
    //        else
    //        {
    //            //DataRow[] dr = PayRoll.PermissionTable.Select("PageUrl='" + PageUrl.ToString() + "'");
    //            var dt = PayRoll.PermissionTable;
    //            //List<ModuleClass> studentList = new List<ModuleClass>();
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                Module.ModuleID = Convert.ToInt32(dt.Rows[i]["ModuleID"]);
    //                Module.ModuleName = dt.Rows[i]["ModuleName"].ToString();

    //                if (Module.ModuleID == 3)
    //                {
    //                    Employes.Visible = true;
    //                }
    //                if (Module.ModuleID == 6)
    //                {
    //                    InputModules.Visible = true;
    //                }
    //                if (Module.ModuleID == 7)
    //                {
    //                    SalaryGeneration.Visible = true;
    //                }
    //                if (Module.ModuleID == 8)
    //                {
    //                    reportGenerator.Visible = true;
    //                }
    //                if (Module.ModuleID == 9)
    //                {
    //                    Administration.Visible = true;
    //                }
    //                if (Module.ModuleID == 10)
    //                {
    //                    AttendenceSheet.Visible = true;
    //                }
    //                if (Module.ModuleID == 11)
    //                {
    //                    AttendanceModule.Visible = true;
    //                }
    //                if (Module.ModuleID == 1002)
    //                {
    //                    Roles.Visible = true;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.TryCatchException(ex);
    //        throw ex;
    //    }

    //}
    #endregion
}