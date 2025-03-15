using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class PagePermission : System.Web.UI.Page
{
    #region Helper Method

    public void LoadData()
    {
        if (ViewState["RoleID"] != null)
        {
            FillGrid(Convert.ToInt32(ViewState["PageID"]));
        }
        else
        {
            FillGrid(0);
        }
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.vt_tbl_Role.Where(x => x.IsActive == true).ToList();
            grdRolesPermission.DataSource = Query;
            grdRolesPermission.DataBind();
        }
    }

    public void FillGrid(int selectedRoleID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int RoleID = vt_Common.CheckInt(selectedRoleID);//will retrun zero if querystring not found
            var query = db.vt_tbl_Role.Where(x => x.RoleID == selectedRoleID).FirstOrDefault();
            if (query != null)
            {
                txtRoleName.Text = query.Role;
            }
            var Role_Page_Data = (from p in db.vt_tbl_SitePages
                                  join prm in db.vt_tbl_Page_Role_Mapping on new { p.PageID, RoleID } equals new { prm.PageID, prm.RoleID } into prmj
                                  from prm_d in prmj.DefaultIfEmpty()
                                  where p.IsActive == true && p.ParentID != -1
                                  select new
                                   {
                                       Parent=p.ParentID,
                                       PageID = p.PageID,
                                       PageName = p.PageName,
                                       Can_View = prm_d.Equals(null) ? false : prm_d.Can_View,
                                       Can_Insert = prm_d.Equals(null) ? false : prm_d.Can_Insert,
                                       Can_Update = prm_d.Equals(null) ? false : prm_d.Can_Update,
                                       Can_Delete = prm_d.Equals(null) ? false : prm_d.Can_Delete,
                                   }
                                   
                         ).OrderBy(x=>x.Parent).ToList();
            grdPagePermission.DataSource = Role_Page_Data;
            grdPagePermission.DataBind();
        }
    }

    public void SavePermitions(int RoleID, vt_EMSEntities db)
    {
        {
            vt_tbl_Page_Role_Mapping prm = new vt_tbl_Page_Role_Mapping();
            foreach (GridViewRow item in grdPagePermission.Rows)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                HiddenField hdnPageID = (HiddenField)item.FindControl("hdnPageID");

                if (chkSelect.Checked)
                {
                    prm.RoleID = RoleID;
                    prm.Can_Insert = true;
                    prm.Can_Update = true;
                    prm.Can_Delete = true;
                    prm.Can_View = true;
                    prm.Can_ApproveOrReject = true;
                    prm.PageID = vt_Common.CheckInt(hdnPageID.Value);
                    db.vt_tbl_Page_Role_Mapping.Add(prm);
                }
                else
                {
                    prm.RoleID = RoleID;
                    prm.Can_Insert = ((CheckBox)item.FindControl("chkInsert")).Checked;
                    prm.Can_Update = ((CheckBox)item.FindControl("chkUpdate")).Checked;
                    prm.Can_Delete = ((CheckBox)item.FindControl("chkDelete")).Checked;
                    prm.Can_View = ((CheckBox)item.FindControl("chkView")).Checked;
                    prm.PageID = vt_Common.CheckInt(hdnPageID.Value);
                    db.vt_tbl_Page_Role_Mapping.Add(prm);
                }
                db.SaveChanges();
            }
        }
    }

    public void ClearForm()
    {
        ViewState["RoleID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#PagePermission').modal('hide');");
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }

    #region Control Event
    protected void btnAddNew_ServerClick(object sender, EventArgs e)
    {
        ViewState["RoleID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        upPagePermission.Update();
    }

    protected void grdRolesPermission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        db.vt_tbl_Page_Role_Mapping.Where(p => p.RoleID == ID).ToList().ForEach(p => db.vt_tbl_Page_Role_Mapping.Remove(p));
                        db.SaveChanges();
                        vt_tbl_Role r = db.vt_tbl_Role.FirstOrDefault(x => x.RoleID == ID);
                        db.vt_tbl_Role.Remove(r);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, r.Role, "Successfully Deleted");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditCompany":
                FillGrid(vt_Common.CheckInt(e.CommandArgument));
                ViewState["RoleID"] = vt_Common.CheckInt(e.CommandArgument);
                vt_Common.ReloadJS(this.Page, "$('#PagePermission').modal();");
                upPagePermission.Update();
                UpView.Update();
                break;
            default:
                break;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();   
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool issave = false;
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Role R = new vt_tbl_Role();
            R.Role = txtRoleName.Text;
            R.IsActive = true;

            int RoleID = vt_Common.CheckInt(ViewState["RoleID"]);//will retrun zero if querystring not found

            if (RoleID == 0)
            {
                db.vt_tbl_Role.Add(R);
                db.SaveChanges();
                SavePermitions(R.RoleID, db);
                issave = true;
            }

            if (RoleID != 0)
            {
                R.RoleID = RoleID;
                db.vt_tbl_Page_Role_Mapping.Where(p => p.RoleID == RoleID).ToList().ForEach(p => db.vt_tbl_Page_Role_Mapping.Remove(p));
                db.SaveChanges();
                SavePermitions(RoleID, db);
                issave = true;
            }
        }
            vt_Common.ReloadJS(this.Page, "$('#PagePermission').modal('hide');");
            LoadData();
            UpView.Update();
    }

    #endregion

    
}