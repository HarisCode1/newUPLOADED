using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using NPOI.SS.Formula.Functions;
using System.Globalization;

public partial class Userform : System.Web.UI.Page
{
    User_BAL BAL = new User_BAL();
    Company_BAL CP = new Company_BAL();
    RolePermission_BAL RP = new RolePermission_BAL();
    PayRoll_Session goAMLSession = new PayRoll_Session();
    string PageUrl = "";

    string Pagename = string.Empty;

    public void Page_Load(object sender, EventArgs e)
    {

        PageUrl = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadGrid();
                BindDropDown();
                lblErrorMessage.Visible = false;
                txtEmailerror.Visible = false;
               
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                ddlComp.SelectedValue = Companyid.ToString();


                //#region IsAuthenticated To Page
                //string UserName = Session["UserName"].ToString();
                //DataTable Dt = Session["PagePermissions"] as DataTable;
                //if (!vt_Common.IsAuthenticated(PageUrl, UserName, Dt))
                //{
                //    Response.Redirect("Default.aspx");
                //}
                //#endregion

                //#region Authorized Controls
                //if (UserName == "SuperAdmin")
                //{
                //    BtnAddNew.Enabled = true;
                //}
                //else
                //{
                //    for (int i = 0; i < Dt.Rows.Count; i++)
                //    {
                //        if (Convert.ToBoolean(Dt.Rows[i]["Can_Insert"].ToString()) == true)
                //        {
                //            BtnAddNew.Enabled = true;
                //        }
                //        else
                //        {

                //            BtnAddNew.Enabled = false;
                //        }
                //    }

                //}
                //#endregion
            }
        }

    }

    public void LoadGrid()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            int RoleId = 0;
            RoleId = Convert.ToInt32(Session["RoleID"]);
            int UserID =0;
                UserID =Convert.ToInt32(Session["UserID"]);
            if (CompanyId != 0)
            {
                if (RoleId == 0)
                {
                    var Query = db.Sp_UserbyCompanyID(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                    if (Query != null)
                    {
                        GridUser.DataSource = Query;
                        GridUser.DataBind();
                    }


                }
                else
                {
                    DataSet ds = ProcedureCall.VT_Sp_UserbyCompanyIDandUserId(CompanyId, UserID);
                    if (ds != null)
                    {
                        GridUser.DataSource = ds;
                        GridUser.DataBind();
                    }


                }


            }
            else
            {

            }

        }
        //vt_Common.Bind_GridView(GridUser, BAL.GetUserListByCompanyID());

        //Previous One
        //using (vt_EMSEntities db = new vt_EMSEntities())
        //{
        //    var Query = db.Sp_UserbyCompanyID(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
        //    GridUser.DataSource = Query;
        //    GridUser.DataBind();

        //}

    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        int CompanyId = Convert.ToInt32(Session["CompanyId"]);
        if (CompanyId != 0)
        {
            if ((string)Session["UserName"] == "SuperAdmin")
            {
                ViewState["hdnID"] = BAL.UserID;
                vt_Common.ReloadJS(this.Page, "$('#UserForm').modal();");
            }
            else
            {
                DataTable Dt = Session["PagePermissions"] as DataTable;
                foreach (DataRow Row in Dt.Rows)
                {
                    if (Row["PageUrl"].ToString() == "userform.aspx" && Row["Can_Insert"].ToString() == "True")
                    {
                        ViewState["hdnID"] = BAL.UserID;
                        vt_Common.ReloadJS(this.Page, "$('#UserForm').modal();");
                    }
                    else if (Row["PageUrl"].ToString() == "userform.aspx" && Row["Can_Insert"].ToString() == "False")
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                    }
                }
            }
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "Please Select Company First From Dashboard");
        }
      

    }
    public void BindDropDown()
    {
        try
        {
            vt_Common.Bind_DropDown(ddlComp, "GetAllCompanies", "CompanyName", "CompanyID");
            vt_Common.Bind_DropDown(ddlRole, "BindRoles", "Role", "RoleID");

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    void ClearForm()
    {
        try
        {
         
            //int Companyid = Convert.ToInt32(Session["CompanyId"]);
            //ddlComp.SelectedValue = Companyid.ToString();
            vt_Common.ReloadJS(this.Page, "$('#UserForm').modal('hide');");
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            ddlRole.SelectedIndex =-1;


            //ddlRole.Items.Clear();
     //    vt_Common.Clear(pnlDetail.Controls);
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {

            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                string msg = "";
                int CompID = 0;
                CompID = vt_Common.CheckInt(ddlComp.SelectedValue);
              

                BAL.UserID = Convert.ToInt32(ViewState["hdnID"]);
                int roleId = Convert.ToInt32(ddlRole.SelectedValue);

                //var query = db.vt_tbl_User.Where(x => x.UserName == txtUserName.Text && x.Active == true && x.CompanyId == CompID && x.UserId != BAL.UserID).ToList();
                //var query1 = db.vt_tbl_Employee.Where(x => x.EmployeeName == txtUserName.Text && x.CompanyID == CompID).ToList();
                var checkAdmins = db.vt_tbl_User.Where(x => x.CompanyId == CompID && x.RoleId == roleId).ToList();
                var checkEmail = db.vt_tbl_User.Where(x => x.CompanyId == CompID && x.Email.Equals(txtEmail.Text)).ToList();

                //if (roleId != 4)
                //{
                //    if (checkAdmins.Count > 0)
                //    {
                //        lblErrorMessage.Visible = true;
                //        return;
                //    }

                //    else
                //    {
                      
                        if (checkEmail.Count > 0)
                        {
                            txtEmailerror.Visible = true;
                    return;
                        }
                        else
                        {

                            //if (query.Count == 0 && query1.Count == 0)
                            //{
                                BAL.UserName = Convert.ToInt32(ddlRole.SelectedValue) == 2 ? "Human Resource" : "Accounts Executive";
                                BAL.Password = vt_Common.Encrypt("123456@");
                                BAL.FirstName = txtFirstName.Text;
                                BAL.LastName = txtLastName.Text;
                                BAL.Email = txtEmail.Text;
                                BAL.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                                BAL.CompanyID = Convert.ToInt32(ddlComp.SelectedValue);
                             
                                if (BAL.UserID > 0)
                                {
                                    BAL.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                                    BAL.UpdatedOn = DateTime.Now;
                                    BAL.Active = true;
                                    BAL.UpdateById(BAL);
                                }
                                else
                                {
                                    BAL.CreatedOn = DateTime.Now;
                                    BAL.CreatedBy = Convert.ToInt32(Session["UserId"]);
                                    BAL.Active = false;
                                    BAL.Sp_Insert(BAL);
                                }

                                msg = "Record Save Successfully";
                                MsgBox.Show(Page, MsgBox.success, "Message ", msg);

                    txtEmailerror.Visible = false;



                    ClearForm();
                            LoadGrid();
                            UpView.Update();
                            Update.Update();
                            //Response.Redirect("userform.aspx");

                        }
                    }
               


            }
        
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
        }
    }
    protected void lbtnDelete_Modalshow(object sender, CommandEventArgs e)
    {

        if (e.CommandArgument.ToString() != "")
        {
            User_BAL us = BAL.GetUserbyID(Convert.ToInt32(e.CommandArgument));
            MsgDelete.Text = us.UserID.ToString();

            vt_Common.ReloadJS(this.Page, "$('#Delete').modal();");
        }

    }



    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        if ((string)Session["UserName"] == "SuperAdmin")
        {
            if (e.CommandArgument.ToString() != "")
            {
                User_BAL UserBAL = BAL.GetUserbyID(Convert.ToInt32(e.CommandArgument));
                ViewState["hdnID"] = UserBAL.UserID;
                ddlComp.SelectedValue = UserBAL.CompanyID.ToString();
                ddlRole.SelectedValue = UserBAL.RoleID.ToString();
                txtFirstName.Text = UserBAL.FirstName;
                txtLastName.Text = UserBAL.LastName;
                txtEmail.Text = UserBAL.Email;
                txtPassword.Text = Convert.ToInt32(ddlRole.SelectedValue) == 2 ? "Human Resource" : "Accounts Executive";
                txtUserName.Text = UserBAL.UserName;
                vt_Common.ReloadJS(this.Page, "$('#UserForm').modal();");
                Update.Update();
            }
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "userform.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    if (e.CommandArgument.ToString() != "")
                    {
                        User_BAL UserBAL = BAL.GetUserbyID(Convert.ToInt32(e.CommandArgument));
                        ViewState["hdnID"] = UserBAL.UserID;
                        ddlComp.SelectedValue = UserBAL.CompanyID.ToString();
                        ddlRole.SelectedValue = UserBAL.RoleID.ToString();
                        txtFirstName.Text = UserBAL.FirstName;
                        txtLastName.Text = UserBAL.LastName;
                        txtEmail.Text = UserBAL.Email;
                        txtPassword.Text = vt_Common.Decrypt(UserBAL.Password);
                        txtUserName.Text = UserBAL.UserName;
                        vt_Common.ReloadJS(this.Page, "$('#UserForm').modal();");
                        Update.Update();
                    }
                }
                else if (Row["PageUrl"].ToString() == "userform.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                }
            }
        }
            
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
        lblErrorMessage.Visible = false;
        txtEmailerror.Visible = false;
       

        //Response.Redirect("userform.aspx");
    }
    protected void btndelete_Command(object sender, CommandEventArgs e)
    {
        //BAL.UserID = Convert.ToInt32(MsgDelete.Text);
        //vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
        //BAL.DeleteUser(BAL.UserID);
        //MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
        //LoadGrid();
        //UpView.Update();
        if ((string)Session["UserName"] == "SuperAdmin")
        {
            BAL.UserID = Convert.ToInt32(MsgDelete.Text);
            vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
            BAL.DeleteUser(BAL.UserID);
            MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
            LoadGrid();
            UpView.Update();
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "userform.aspx" && Row["Can_Delete"].ToString() == "True")
                {
                    BAL.UserID = Convert.ToInt32(MsgDelete.Text);
                    vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
                    BAL.DeleteUser(BAL.UserID);
                    MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
                    LoadGrid();
                    UpView.Update();
                }
                else if (Row["PageUrl"].ToString() == "userform.aspx" && Row["Can_Delete"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                }
            }

        }
           
    }
    protected void GridUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LinkButton LnkEdit = e.Row.FindControl("lbtnEdit") as LinkButton;
            //LinkButton LnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;

            //if (Session["Username"].ToString() != "SuperAdmin")
            //{
            //    if (Dt != null && Dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < Dt.Rows.Count; i++)
            //        {
            //            if (PageUrl.ToUpper() == Dt.Rows[i]["PageUrl"].ToString().ToUpper())
            //            {
            //                if (Convert.ToBoolean(Dt.Rows[i]["Can_Update"].ToString()) == true)
            //                {
            //                    LnkEdit.Enabled = true;
            //                }
            //                else
            //                {
            //                    LnkEdit.Enabled = false;
            //                }


            //                if (Convert.ToBoolean(Dt.Rows[i]["Can_Delete"].ToString()) == true)
            //                {
            //                    LnkDelete.Enabled = true;
            //                }
            //                else
            //                {
            //                    LnkDelete.Enabled = false;
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static bool Check_Username(string Username)
    {
        //var a = HttpContext.Current.Session["Companyid"];

        int companyId = Convert.ToInt32(HttpContext.Current.Session["Companyid"]);
        //int CompanyId = Convert.ToInt32(Session["Companyid"]);

        vt_EMSEntities db = new vt_EMSEntities();
        
        vt_tbl_User EmployeeObj = db.vt_tbl_User.Where(x => x.UserName == Username && x.CompanyId==companyId).FirstOrDefault();
        if (EmployeeObj != null)
        {
            return true;
          
        }
        else
        {
            return false;
        }

    }
}

