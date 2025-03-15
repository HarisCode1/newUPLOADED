using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class RolePermissionNew : System.Web.UI.Page
{
    Module_BAL ModuleBal = new Module_BAL();
    Page_BAL PageBal = new Page_BAL();
    public DataTable Dt;
    public int RoleID = 0;
    public int ModuleID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            GvPermission_Fill(0);
            DdlRole_Fill();
            Ddl_DdlModule();
        }
    }

    #region Ddl_Fill
    void DdlRole_Fill()
    {
        vt_Common.Bind_DropDown(DdlRole, "BindGetRoles", "Role", "RoleID");
    }

    void Ddl_DdlModule()
    {
        vt_Common.Bind_DropDown(DdlModule, "BindGetModules", "ModuleName", "ModuleID");
    }
    #endregion

    #region Grid_Fill
    void GvPermission_Fill(int ModuleID)
    {
        if (ModuleID > 0)
        {
            GvPermission.DataSource = PageBal.GetAllPageByModuleID(ModuleID);
        }
        else
        {
            GvPermission.DataSource = null;
        }
        GvPermission.DataBind();
    }


    #endregion

    

    protected void DdlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RoleID = Convert.ToInt32(DdlRole.SelectedValue.ToString());
        int ModuleID = Convert.ToInt32(DdlModule.SelectedValue.ToString());
        Dt = PageBal.GetPermissions(RoleID, ModuleID);
        GvPermission_Fill(ModuleID);
    }

    protected void DdlRole_SelectedIndexChanged(object sender, EventArgs e)
    {

        int RoleID = Convert.ToInt32(DdlRole.SelectedValue.ToString());
        int ModuleID = Convert.ToInt32(DdlModule.SelectedValue.ToString());
        Dt = PageBal.GetPermissions(RoleID, ModuleID);
        GvPermission_Fill(ModuleID);

    }

    protected void GvPermission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Dt != null && Dt.Rows.Count > 0)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblPageID = (Label)e.Row.FindControl("LblPageID");
                CheckBox ChkView = (CheckBox)e.Row.FindControl("ChkView");
                CheckBox ChkInsert = (CheckBox)e.Row.FindControl("ChkInsert");
                CheckBox ChkUpdate = (CheckBox)e.Row.FindControl("ChkUpdate");
                CheckBox ChkDelete = (CheckBox)e.Row.FindControl("ChkDelete");
                int PageID = Convert.ToInt32(LblPageID.Text);


                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    if (PageID == Convert.ToInt32(Dt.Rows[i][3].ToString()))
                    {
                        //Check View
                        if (Convert.ToBoolean(Dt.Rows[i][4]))
                        {
                            ChkView.Checked = true;
                        }
                        else
                        {
                            ChkView.Checked = false;
                        }

                        //Insert View
                        if (Convert.ToBoolean(Dt.Rows[i][5]))
                        {
                            ChkInsert.Checked = true;
                        }
                        else
                        {
                            ChkInsert.Checked = false;
                        }

                        //Update View
                        if (Convert.ToBoolean(Dt.Rows[i][6]))
                        {
                            ChkUpdate.Checked = true;
                        }
                        else
                        {
                            ChkUpdate.Checked = false;
                        }

                        //Delete View
                        if (Convert.ToBoolean(Dt.Rows[i][7]))
                        {
                            ChkDelete.Checked = true;
                        }
                        else
                        {
                            ChkDelete.Checked = false;
                        }
                    }
                }
            }
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (DdlRole.SelectedIndex == 0 || DdlModule.SelectedIndex == 0)
        {
            MsgBox.Show(Page, MsgBox.danger, "Error", "Please select a Module/Role");
        }
        else
        {
            vt_EMSEntities db = new vt_EMSEntities();
            int RoleID = Convert.ToInt32(DdlRole.SelectedValue.ToString());
            int ModuleID = Convert.ToInt32(DdlModule.SelectedValue.ToString());

            List<vt_tbl_Permission> DataPermission = db.vt_tbl_Permission.Where(x => x.ModuleID == ModuleID && x.RoleID == RoleID).ToList();
            if (DataPermission != null)
            {
                foreach (var item in DataPermission)
                {
                    db.vt_tbl_Permission.Remove(item);
                    db.SaveChanges();
                }
            }

            vt_tbl_Permission ObjPermission = new vt_tbl_Permission();
            for (int i = 0; i < GvPermission.Rows.Count; i++)
            {
                Label LblPageID = (Label)GvPermission.Rows[i].FindControl("LblPageID");
                CheckBox ChkView = (CheckBox)GvPermission.Rows[i].FindControl("ChkView");
                CheckBox ChkInsert = (CheckBox)GvPermission.Rows[i].FindControl("ChkInsert");
                CheckBox ChkUpdate = (CheckBox)GvPermission.Rows[i].FindControl("ChkUpdate");
                CheckBox ChkDelete = (CheckBox)GvPermission.Rows[i].FindControl("ChkDelete");
                if (ChkView.Checked || ChkInsert.Checked || ChkUpdate.Checked || ChkDelete.Checked)
                {
                    ObjPermission.RoleID = RoleID;
                    ObjPermission.ModuleID = ModuleID;
                    ObjPermission.PageID = Convert.ToInt32(LblPageID.Text);
                    ObjPermission.Can_View = ChkView.Checked;
                    ObjPermission.Can_Insert = ChkInsert.Checked;
                    ObjPermission.Can_Update = ChkUpdate.Checked;
                    ObjPermission.Can_Delete = ChkDelete.Checked;
                    db.vt_tbl_Permission.Add(ObjPermission);
                    db.SaveChanges();
                }
                
            }
            MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Saved!");
        }
    }
}

class Permission
{
    public int RoleID { get; set; }
    public int ModuleID { get; set; }
    public int PageID { get; set; }

    public bool Can_View { get; set; }

    public bool Can_Insert { get; set; }

    public bool Can_Update { get; set; }
    public bool Can_Delete { get; set; }
}