using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class RolePermission : System.Web.UI.Page
{
    Module_BAL Mod = new Module_BAL();
    Page_BAL Pag = new Page_BAL();
    RolePermission_BAL RPermission = new RolePermission_BAL();
    string Pagename = string.Empty;
    private int RoleID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if(Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                BindAllGrid();
                BindDropDown();
            }
        }
        //if (Session["PayrollSess"] != null)
        //{

        //    Response.Redirect("Login.aspx");
        //}
    }

    public void BindAllGrid()
    {
        try
        {
            GridAllModule.DataSource = Mod.GetAllModule(null);
            GridAllModule.DataBind();
            GridAdministrations.DataSource = Pag.GetAllPageByModuleID(9);
            GridAdministrations.DataBind();
            GridEmployeeCreation.DataSource = Pag.GetAllPageByModuleID(3);
            GridEmployeeCreation.DataBind();
            GridAttendances.DataSource = Pag.GetAllPageByModuleID(11);
            GridAttendances.DataBind();
            GridSalariesGeneration.DataSource = Pag.GetAllPageByModuleID(4);
            GridSalariesGeneration.DataBind();
            Gridinputmodules.DataSource = Pag.GetAllPageByModuleID(6);
            Gridinputmodules.DataBind();
            GridAttendenceSheet.DataSource = Pag.GetAllPageByModuleID(10);
            GridAttendenceSheet.DataBind();
            GridSalariesGeneration.DataSource = Pag.GetAllPageByModuleID(7);
            GridSalariesGeneration.DataBind();
            GridSalarySlip.DataSource = Pag.GetAllPageByModuleID(8);
            GridSalarySlip.DataBind();
            GridRoles.DataSource = Pag.GetAllPageByModuleID(1002);
            GridRoles.DataBind();
            Gridpayroll.DataSource = Pag.GetAllPageByModuleID(1003);
            Gridpayroll.DataBind();
            GridUserCreation.DataSource = Pag.GetAllPageByModuleID(1004);
            GridUserCreation.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    private void GetModulPermission(int RoleID)
    {
        try
        {
            DataTable dt = RPermission.GetModuleRightsByRoleID(RoleID, 0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < GridAllModule.Rows.Count; j++)
                {
                    Label lblModulID = (Label)GridAllModule.Rows[j].FindControl("lblModuleID");
                    if (lblModulID.Text == dt.Rows[i]["ModuleID"].ToString())
                    {
                        CheckBox chkAllSelect = (CheckBox)GridAllModule.Rows[j].FindControl("chkAllSelect");
                        chkAllSelect.Checked = true;
                        CheckBox ChkView = (CheckBox)GridAllModule.Rows[j].FindControl("ChkView");
                        ChkView.Checked = Convert.ToBoolean(dt.Rows[i]["Can_View"]);
                        CheckBox Can_Insert = (CheckBox)GridAllModule.Rows[j].FindControl("ChkInsert");
                        Can_Insert.Checked = Convert.ToBoolean(dt.Rows[i]["Can_Insert"]);
                        CheckBox Can_Update = (CheckBox)GridAllModule.Rows[j].FindControl("ChkUpdate");
                        Can_Update.Checked = Convert.ToBoolean(dt.Rows[i]["Can_Update"]);
                        CheckBox Can_Delete = (CheckBox)GridAllModule.Rows[j].FindControl("ChkDelete");
                        Can_Delete.Checked = Convert.ToBoolean(dt.Rows[i]["Can_Delete"]);
                    }
                    else
                    {
                        //CheckBox chkAllSelect = (CheckBox)GridAllModule.Rows[j].FindControl("chkAllSelect");
                        //chkAllSelect.Enabled = false;
                        //CheckBox ChkView = (CheckBox)GridAllModule.Rows[j].FindControl("ChkView");
                        //ChkView.Enabled = false;
                        //CheckBox Can_Insert = (CheckBox)GridAllModule.Rows[j].FindControl("ChkInsert");
                        //Can_Insert.Enabled = false;
                        //CheckBox Can_Update = (CheckBox)GridAllModule.Rows[j].FindControl("ChkUpdate");
                        //Can_Update.Enabled = false;
                        //CheckBox Can_Delete = (CheckBox)GridAllModule.Rows[j].FindControl("ChkDelete");
                        //Can_Delete.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }

    private void GetRolePagePermission(int RoleID)
    {
        try
        {
            DataTable dt = RPermission.GetPagePermissionpPagesByRole(RoleID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool Continue;
                Continue = SetRolePagePermissionOnGrid(GridAdministrations, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridEmployeeCreation, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridAttendances, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridAttendenceSheet, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(Gridinputmodules, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridSalariesGeneration, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridSalarySlip, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridRoles, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(Gridpayroll, dt.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridUserCreation, dt.Rows[i]);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    private bool SetRolePagePermissionOnGrid(GridView Grid, DataRow Drow)
    {
        try
        {
            hddTabID.Value = "ALLMODULE";
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                Label lblPageID = (Label)Grid.Rows[i].FindControl("lblPageID");
                if (lblPageID.Text == Drow["PageID"].ToString())
                {
                    CheckBox chkAllSelect = (CheckBox)Grid.Rows[i].FindControl("chkAllSelect");
                    chkAllSelect.Checked = true;
                    CheckBox ChkView = (CheckBox)Grid.Rows[i].FindControl("ChkView");
                    ChkView.Checked = Convert.ToBoolean(Drow["Can_View"]);
                    CheckBox Can_Insert = (CheckBox)Grid.Rows[i].FindControl("ChkInsert");
                    Can_Insert.Checked = Convert.ToBoolean(Drow["Can_Insert"]);
                    CheckBox Can_Update = (CheckBox)Grid.Rows[i].FindControl("ChkUpdate");
                    Can_Update.Checked = Convert.ToBoolean(Drow["Can_Update"]);
                    CheckBox Can_Delete = (CheckBox)Grid.Rows[i].FindControl("ChkDelete");
                    Can_Delete.Checked = Convert.ToBoolean(Drow["Can_Delete"]);

                    return true;
                }
            }
            return false;
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
            if (ddlUserAndEmp.SelectedItem.Text != "--Please Select--")
            {
                //PayRoll_Session PayRoll = (PayRoll_Session)Session["PayrollSess"];

                if (SaveUserRolePermission(Convert.ToInt32(ddlUserType.SelectedValue),Convert.ToInt32(ddlUserAndEmp.SelectedValue)) == true)
                {
                    //        //JQ.ShowStatusMsg(this, "1", "User Permission Save");
                    //JQ.ToastMsg(this.Page, "1", "Role Permission Save", "bottom-right");
                    string Pagename = Path.GetFileNameWithoutExtension(Request.Path);
                    //Log.InsertLogs(PayRoll.UserID, 0, 0, "Role " + txtRoleName.Text + " Permission Saved");


                }
                else
                {
                    //        JQ.ToastMsg(this.Page, Constants.Error, "An error occurred while giving user permission", Constants.bottom_right);

                }
                MsgBox.Show(Page, MsgBox.success, txtRoleName.Text, "Successfully Save");
                ddlUserType.SelectedValue = 0.ToString();
                ddlUserAndEmp.SelectedValue = 0.ToString();
                ddlUserAndEmp.Enabled = false;
                BindAllGrid();
                UpdatePanel1.Update();



                //    //    }
                //    //    else
                //    //    {
                //    //        JQ.showStatusMsg(this, "3", "User not Allowed to Save Permission");
                //    //    }
                //    //}
            }
            else
            {
                MsgBox.Show(Page, MsgBox.danger, txtRoleName.Text, "Please Try Again");
            }
        }

        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }


    }

    protected void chkAllSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            UpdatePanel1.Update();
            hddTabID.Value = "Module";
            CheckBox chk = (CheckBox)sender;
            GridViewRow dr = (GridViewRow)chk.Parent.Parent;
            Label lblModID = (Label)dr.FindControl("lblModuleID");
            CheckBox ChkSelected = (CheckBox)dr.FindControl("chkAllSelect");
            CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
            (dr.FindControl("ChkView") as CheckBox).Checked = ChkSelected.Checked;
            CheckBox chkView = (CheckBox)dr.FindControl("ChkView");
            CheckApply(Convert.ToInt32(lblModID.Text), chkView.ID, chk.Checked);
            (dr.FindControl("ChkInsert") as CheckBox).Checked = ChkSelected.Checked;
            CheckBox ChkInsert = (CheckBox)dr.FindControl("ChkInsert");
            CheckApply(Convert.ToInt32(lblModID.Text), ChkInsert.ID, chk.Checked);
            (dr.FindControl("ChkUpdate") as CheckBox).Checked = ChkSelected.Checked;
            CheckBox ChkUpdate = (CheckBox)dr.FindControl("ChkUpdate");
            CheckApply(Convert.ToInt32(lblModID.Text), ChkUpdate.ID, chk.Checked);
            (dr.FindControl("ChkDelete") as CheckBox).Checked = ChkSelected.Checked;
            CheckBox ChkDelete = (CheckBox)dr.FindControl("ChkDelete");
            CheckApply(Convert.ToInt32(lblModID.Text), ChkDelete.ID, chk.Checked);

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }

    protected void ChkView_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow dr = (GridViewRow)chk.Parent.Parent;
            Label lblModID = (Label)dr.FindControl("lblModuleID");
            CheckBox ChkSelected = (CheckBox)dr.FindControl("chkAllSelect");
            if (ChkSelected.Checked)
                CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    protected void ChkInsert_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow dr = (GridViewRow)chk.Parent.Parent;
            Label lblModID = (Label)dr.FindControl("lblModuleID");
            CheckBox ChkSelected = (CheckBox)dr.FindControl("chkAllSelect");
            if (ChkSelected.Checked)
                CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }

    protected void ChkUpdate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            CheckBox chk = (CheckBox)sender;
            GridViewRow dr = (GridViewRow)chk.Parent.Parent;
            Label lblModID = (Label)dr.FindControl("lblModuleID");
            CheckBox ChkSelected = (CheckBox)dr.FindControl("chkAllSelect");
            if (ChkSelected.Checked)
                CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    protected void ChkDelete_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow dr = (GridViewRow)chk.Parent.Parent;
            Label lblModID = (Label)dr.FindControl("lblModuleID");
            CheckBox ChkSelected = (CheckBox)dr.FindControl("chkAllSelect");
            if (ChkSelected.Checked)
                CheckApply(Convert.ToInt32(lblModID.Text), chk.ID, chk.Checked);
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }

    protected void chkAllSelect_CheckedChanged1(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow dr = (GridViewRow)chk.Parent.Parent;
            CheckBox ChkSelected = (CheckBox)dr.FindControl("chkAllSelect");
            (dr.FindControl("ChkView") as CheckBox).Checked = ChkSelected.Checked;
            (dr.FindControl("ChkInsert") as CheckBox).Checked = ChkSelected.Checked;
            (dr.FindControl("ChkUpdate") as CheckBox).Checked = ChkSelected.Checked;
            (dr.FindControl("ChkDelete") as CheckBox).Checked = ChkSelected.Checked;
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }

    private void CheckApply(int ModuleID, string CheckBoxName, bool IsCheck)
    {
        try
        {
            GridView Grid = GetGridName(ModuleID);
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)Grid.Rows[i].FindControl(CheckBoxName);
                chk.Checked = IsCheck;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    private GridView GetGridName(int ModuleID)
    {
        try
        {
            GridView Grid = null;

            if (ModuleID == (int)3)
            {
                Grid = GridEmployeeCreation;
            }
            else if (ModuleID == (int)6)
            {
                Grid = Gridinputmodules;
            }
            else if (ModuleID == (int)7)
            {
                Grid = GridSalariesGeneration;
            }
            else if (ModuleID == (int)8)
            {
                Grid = GridSalarySlip;
            }
            else if (ModuleID == (int)9)
            {
                Grid = GridAdministrations;
            }
            else if (ModuleID == (int)10)
            {
                Grid = GridAttendenceSheet;
            }
            else if (ModuleID == (int)11)
            {
                Grid = GridAttendances;
            }
            else if (ModuleID == (int)1002)
            {
                Grid = GridRoles;
            }
            else if (ModuleID == (int)1003)
            {
                Grid = Gridpayroll;
            }
            else if (ModuleID == (int)1004)
            {
                Grid = GridUserCreation;
            }
            return Grid;

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    private bool SaveUserRolePermission(int RoleID, int UserID)
        {
        bool IsTrue = false;
        

        //    if (DITSess != null)
        //    {
        SqlConnection con = new SqlConnection(Viftech.vt_Common.PayRollConnectionString);
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        try
        {
            RoleID = Convert.ToInt32(ddlUserType.SelectedValue);
            UserID = Convert.ToInt32(ddlUserAndEmp.SelectedValue);
            //RPermission.DeletePagePermissionPagesByRole(RoleID, UserID, trans);
            //RPermission.DeleteModulePermissionByRoleID(RoleID, UserID, trans);

            //RPermission.DeletePagePermissionPagesByRole(RoleID, UserID, trans);
            RPermission.CreatedBy = Convert.ToInt32(Session["UserId"]);
            InsertUpdateModulePermission(RoleID, UserID, trans);
            //InsertUpdateRolePagePermission(GridAllModule, RoleID, trans, DITSess);
            InsertUpdateRolePagePermission(GridAdministrations, RoleID, trans);
            InsertUpdateRolePagePermission(GridEmployeeCreation, RoleID, trans);
            InsertUpdateRolePagePermission(GridAttendances, RoleID, trans);
            InsertUpdateRolePagePermission(Gridinputmodules, RoleID, trans);
            InsertUpdateRolePagePermission(GridAttendenceSheet, RoleID, trans);
            InsertUpdateRolePagePermission(GridSalarySlip, RoleID, trans);
            InsertUpdateRolePagePermission(GridSalariesGeneration, RoleID, trans);
            InsertUpdateRolePagePermission(GridRoles, RoleID, trans);
            InsertUpdateRolePagePermission(Gridpayroll, RoleID, trans);
            InsertUpdateRolePagePermission(GridUserCreation, RoleID, trans);

            trans.Commit();
            IsTrue = true;
        }
        catch (Exception)
        {
            trans.Rollback();
            IsTrue = false;
        }
        //    }
        return IsTrue;
    }

    #region InsertUpdateModulePermission
    private void InsertUpdateModulePermission(int RoleID, int UserID, SqlTransaction Tran)
    {
        try
        {
            SqlConnection con = new SqlConnection(Viftech.vt_Common.PayRollConnectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            RPermission.DeletePagePermissionPagesByRole(RoleID, UserID, trans);
            RPermission.DeleteModulePermissionByRoleID(RoleID, UserID, trans);

            //RPermission.DeleteModulePermissionByRoleID(RoleID, UserID, trans);
            trans.Commit();
            PayRoll_Session Sessa = (PayRoll_Session)Session["PayrollSess"];
            for (int i = 0; i < GridAllModule.Rows.Count; i++)
            {
                CheckBox chkAllSelect = (CheckBox)GridAllModule.Rows[i].FindControl("chkAllSelect");
                CheckBox ChkView = (CheckBox)GridAllModule.Rows[i].FindControl("ChkView");
                CheckBox ChkInsert = (CheckBox)GridAllModule.Rows[i].FindControl("ChkInsert");
                CheckBox ChkUpdate = (CheckBox)GridAllModule.Rows[i].FindControl("ChkUpdate");
                CheckBox ChkDelete = (CheckBox)GridAllModule.Rows[i].FindControl("ChkDelete");
                if (chkAllSelect.Checked)
                {
                    if (ChkView.Checked || ChkInsert.Checked || ChkUpdate.Checked || ChkDelete.Checked)
                    {
                        //RPermission.UserID = Sessa.UserID;
                        RPermission.CreatedBy = Convert.ToInt32(Session["UserId"]);


                        RPermission.UserID = Convert.ToInt32(ddlUserAndEmp.SelectedValue);

                        RPermission.RoleID = Convert.ToInt32(ddlUserType.SelectedValue);

                        //RPermission.RoleID = RoleID;
                        RPermission.ModuleID = Convert.ToInt32(((Label)GridAllModule.Rows[i].FindControl("lblModuleID")).Text);
                        RPermission.Can_View = ChkView.Checked;
                        RPermission.Can_Insert = ChkInsert.Checked;
                        RPermission.Can_Update = ChkUpdate.Checked;
                        RPermission.Can_Delete = ChkDelete.Checked;
                        RPermission.Active = true;
                        RPermission.InsertUpdateModulePermissionByRoleID(RPermission, Tran);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    #endregion

    #region InsertUpdateRolePagePermission
    private void InsertUpdateRolePagePermission(GridView Grid, int RoleID, SqlTransaction Tran)
    {
        PayRoll_Session Sessa = (PayRoll_Session)Session["PayrollSess"];
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            CheckBox chkAllSelect = (CheckBox)Grid.Rows[i].FindControl("chkAllSelect");
            CheckBox ChkView = (CheckBox)Grid.Rows[i].FindControl("ChkView");
            CheckBox ChkInsert = (CheckBox)Grid.Rows[i].FindControl("ChkInsert");
            CheckBox ChkUpdate = (CheckBox)Grid.Rows[i].FindControl("ChkUpdate");
            CheckBox ChkDelete = (CheckBox)Grid.Rows[i].FindControl("ChkDelete");

            if (chkAllSelect.Checked)
            {
                if (ChkView.Checked || ChkInsert.Checked || ChkUpdate.Checked || ChkDelete.Checked)
                {
                    RPermission.UserID = Convert.ToInt32(ddlUserAndEmp.SelectedValue);

                    RPermission.RoleID = Convert.ToInt32(ddlUserType.SelectedValue);
                    //RPermission.UserID = Sessa.UserID;
                    //RPermission.RoleID = RoleID;
                    RPermission.PageID = Convert.ToInt32(((Label)Grid.Rows[i].FindControl("lblPageID")).Text);
                    RPermission.Can_View = ChkView.Checked;
                    RPermission.Can_Insert = ChkInsert.Checked;
                    RPermission.Can_Update = ChkUpdate.Checked;
                    RPermission.Can_Delete = ChkDelete.Checked;
                    RPermission.Active = true;
                    RPermission.InsertUpdatePagePermission(RPermission, Tran);
                }
            }
        }
    }
    #endregion

    protected void GridAllModule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("ChkView") as CheckBox;
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(chk);
        }
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlUserAndEmp.Enabled = true;
        EMS_Session EMS = (EMS_Session)Session["EMS_Session"];
        PayRoll_Session Sessa = (PayRoll_Session)Session["PayrollSess"];
        if (ddlUserType.SelectedItem.Text == "--Please Select--")
        {

            ddlUserAndEmp.Enabled = false;
        }
        if (Session["CompanyId"] == null)
        {
            if (ddlUserType.SelectedItem.Text == "User")
            {
                SqlParameter[] param =
                {
                new SqlParameter("@RoleID",ddlUserType.SelectedValue),
            new SqlParameter("@CompanyId", null)
        };
                vt_Common.Bind_DropDown(ddlUserAndEmp, "BindAllUsersAndEmployeesForAdmin", "EmployeeName", "EmployeeID", param);
            }
            else
            {
                SqlParameter[] param =
               {
                new SqlParameter("@RoleID",ddlUserType.SelectedValue),
            new SqlParameter("@CompanyId", null)
            //new SqlParameter("@CompanyId", ddlUserAndEmp.SelectedValue)

        };
                vt_Common.Bind_DropDown(ddlUserAndEmp, "BindAllUsersAndEmployeesForAdmin", "UserName", "UserId", param);
            }
        }
        else
        {
            if (ddlUserType.SelectedItem.Text == "User")
            {
                SqlParameter[] param =
                {
                new SqlParameter("@RoleID",ddlUserType.SelectedValue),
            new SqlParameter("@CompanyId", Session["CompanyId"])
        };
                vt_Common.Bind_DropDown(ddlUserAndEmp, "BindUsersAndEmployees", "EmployeeName", "EmployeeID", param);
            }
            else
            {
                SqlParameter[] param =
               {
                new SqlParameter("@RoleID",ddlUserType.SelectedValue),
            new SqlParameter("@CompanyId", Session["CompanyId"])
        };
                vt_Common.Bind_DropDown(ddlUserAndEmp, "BindUsersAndEmployees", "UserName", "UserId", param);
            }
        }
        BindAllGrid();
        //UpdatePanel1.Update();



    }
    public void BindDropDown()
    {
        vt_Common.Bind_DropDown(ddlUserType, "BindGetRoles", "Role", "RoleID");
    }

    protected void ddlUserAndEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindAllGrid();

            //UpdatePanel1.Update();
            int RoleID = Convert.ToInt32(ddlUserType.SelectedValue);

            int UserId = Convert.ToInt32(ddlUserAndEmp.SelectedValue);
            

            DataTable dt = RPermission.GetModuleRightsByRoleID(RoleID, UserId);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < GridAllModule.Rows.Count; j++)
                {
                    Label lblModulID = (Label)GridAllModule.Rows[j].FindControl("lblModuleID");
                    if (lblModulID.Text == dt.Rows[i]["ModuleID"].ToString())
                    {
                        CheckBox chkAllSelect = (CheckBox)GridAllModule.Rows[j].FindControl("chkAllSelect");
                        chkAllSelect.Checked = true;
                        CheckBox ChkView = (CheckBox)GridAllModule.Rows[j].FindControl("ChkView");
                        ChkView.Checked = Convert.ToBoolean(dt.Rows[i]["Can_View"]);
                        CheckBox Can_Insert = (CheckBox)GridAllModule.Rows[j].FindControl("ChkInsert");
                        Can_Insert.Checked = Convert.ToBoolean(dt.Rows[i]["Can_Insert"]);
                        CheckBox Can_Update = (CheckBox)GridAllModule.Rows[j].FindControl("ChkUpdate");
                        Can_Update.Checked = Convert.ToBoolean(dt.Rows[i]["Can_Update"]);
                        CheckBox Can_Delete = (CheckBox)GridAllModule.Rows[j].FindControl("ChkDelete");
                        Can_Delete.Checked = Convert.ToBoolean(dt.Rows[i]["Can_Delete"]);
                    }
                    else
                    {
                        //CheckBox chkAllSelect = (CheckBox)GridAllModule.Rows[j].FindControl("chkAllSelect");
                        //chkAllSelect.Enabled = false;
                        //CheckBox ChkView = (CheckBox)GridAllModule.Rows[j].FindControl("ChkView");
                        //ChkView.Enabled = false;
                        //CheckBox Can_Insert = (CheckBox)GridAllModule.Rows[j].FindControl("ChkInsert");
                        //Can_Insert.Enabled = false;
                        //CheckBox Can_Update = (CheckBox)GridAllModule.Rows[j].FindControl("ChkUpdate");
                        //Can_Update.Enabled = false;
                        //CheckBox Can_Delete = (CheckBox)GridAllModule.Rows[j].FindControl("ChkDelete");
                        //Can_Delete.Enabled = false;
                    }
                }
            }

            DataTable dtM = RPermission.GetPagePermissionpPagesByRole(RoleID);
            for (int i = 0; i < dtM.Rows.Count; i++)
            {
                bool Continue;
                Continue = SetRolePagePermissionOnGrid(GridAdministrations, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridEmployeeCreation, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridAttendances, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridSalariesGeneration, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(Gridinputmodules, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridAttendenceSheet, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridSalarySlip, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridRoles, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(Gridpayroll, dtM.Rows[i]);
                if (Continue)
                    continue;
                Continue = SetRolePagePermissionOnGrid(GridUserCreation, dtM.Rows[i]);
            }       
        }

        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
}