using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Viftech;
using System.IO;

public partial class User : System.Web.UI.Page
{
    string Pagename = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();

        }
        vt_Common.ReloadJS(this.Page, "binddata();");



    }

    #region Control Event

    protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ID);
                        db.vt_tbl_Employee.Remove(emp);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, emp.EmployeeName, "Successfully Deleted");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditCompany":
                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                vt_Common.ReloadJS(this.Page, "$('#createuser').modal();loadRoleData();");
                UpDetail.Update();
                break;
            default:
                break;
        }

        
    }

    protected void btnSave_Click(object sender, EventArgs e)
        {
        int CompanyID;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                //int CompanyId = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;



                if (ddlCompany.SelectedValue == "0")
                {
                    CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                }
                else 
                {
                    CompanyID = vt_Common.CompanyId;
                }

                var query = db.vt_tbl_Employee.Where(x => x.EmployeeName == txtUserName.Text).ToList();

                if(query.Count==0)
                {
                    vt_tbl_Employee emp = new vt_tbl_Employee();
                    emp.CompanyID = CompanyID;
                    emp.EmployeeName = txtUserName.Text;
                    emp.EmpPassword = vt_Common.Encrypt(txtPwd.Text);
                    emp.RoleID = vt_Common.CheckInt(ddlRole.SelectedValue);

                    if (ViewState["PageID"] != null)
                    {
                        emp.EmployeeID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_Employee.Add(emp);
                    }

                    db.SaveChanges();
                }
                else
                {
                    MsgBox.Show(Page, MsgBox.success, txtUserName.Text, "Already Exist");
                    ClearForm();
                    return;
                }
            }

            MsgBox.Show(Page, MsgBox.success, txtUserName.Text, "Successfully Save");
            ClearForm();
            LoadData();
            UpView.Update();
        }
        catch (DbUpdateException ex)
        {

        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    class pagePermission
    {
        public int ID { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }

    protected void btnrSave_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int RoleID = vt_Common.CheckInt(hdnRoleID.Value);
            vt_tbl_Role r = new vt_tbl_Role();
            if (RoleID > 0)
            {
                r.Role = txtRoleName.Text;
                r.IsActive = chkActive.Checked;
                r.RoleID = RoleID;
                db.Entry(r).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                r.Role = txtRoleName.Text;
                r.IsActive = chkActive.Checked;
                db.vt_tbl_Role.Add(r);
            }

            List<pagePermission> obj = JsonConvert.DeserializeObject<List<pagePermission>>(hdnSelectedVal.Value);
            List<vt_tbl_Page_Role_Mapping> list = db.vt_tbl_Page_Role_Mapping.Where(x => x.RoleID == r.RoleID).ToList();
            if (list.Count > 0)
            {
                foreach (var i in list)
                {
                    db.vt_tbl_Page_Role_Mapping.Remove(i);
                }
            }

            foreach (var item in obj)
            {
                vt_tbl_Page_Role_Mapping prm = new vt_tbl_Page_Role_Mapping();
                prm.RoleID = r.RoleID;
                prm.PageID = item.ID;
                prm.Can_View = item.View;
                prm.Can_Insert = item.Insert;
                prm.Can_Update = item.Update;
                prm.Can_Delete = item.Delete;
                db.vt_tbl_Page_Role_Mapping.Add(prm);
            }
            db.SaveChanges();
        }

        vt_Common.ReloadJS(this.Page, "$('#createrole').modal('hide');loadRoleData();");
        ddlRole.DataBind();
        UpDetail.Update();
    }

    protected void btnrClose_Click(object sender, EventArgs e)
    {
        ClearRoleForm();
    }

    protected void btnEditRole_Click(object sender, EventArgs e)
    {
        LoadRoleData();
        UproleDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#createrole').modal();loadRoleData();");
    }

    protected void btnDeleteRole_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int RoleID = vt_Common.CheckInt(ddlRole.SelectedValue);
                if (RoleID > 0)
                {
                    List<vt_tbl_Page_Role_Mapping> list = db.vt_tbl_Page_Role_Mapping.Where(x => x.RoleID == RoleID).ToList();
                    if (list.Count > 0)
                    {
                        foreach (var i in list)
                        {
                            db.vt_tbl_Page_Role_Mapping.Remove(i);
                        }
                    }
                    db.vt_tbl_Role.Remove(db.vt_tbl_Role.Where(x => x.RoleID == RoleID).FirstOrDefault());
                    db.SaveChanges();
                    ddlRole.DataBind();
                    UpDetail.Update();
                }
                ClearRoleForm();
            }
        }
        catch (DbUpdateException ex)
        {
            SqlException innerException = ex.GetBaseException() as SqlException;
            vt_Common.PrintfriendlySqlException(innerException, Page);
        }
    }

    #endregion

    #region Healper Method

    void LoadRoleData()
    {
        int RoleID = vt_Common.CheckInt(hdnRoleID.Value);

        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (RoleID > 0)
            {
                vt_tbl_Role objr = db.vt_tbl_Role.Where(x => x.RoleID == RoleID).FirstOrDefault();
                txtRoleName.Text = objr.Role;
                chkActive.Checked = objr.IsActive;
            }
            else
            {
                txtRoleName.Text = string.Empty;
                chkActive.Checked = false;
            }

            var data = (from x in db.vt_tbl_SitePages
                        join y in db.vt_tbl_Page_Role_Mapping on new { PID = x.PageID, RID = RoleID } equals new { PID = (int)y.PageID, RID = y.RoleID } into jointable
                        from z in jointable.DefaultIfEmpty()
                        select new
                        {
                            Can_View = z.Equals(null) ? false : z.Can_View,
                            Can_Insert = z.Equals(null) ? false : z.Can_Insert,
                            Can_Update = z.Equals(null) ? false : z.Can_Update,
                            Can_Delete = z.Equals(null) ? false : z.Can_Delete,
                            PageID = x.PageID,
                            x.PageName,
                            x.ParentID
                        }).Where(x => x.ParentID.Equals(-1)).ToList();

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            foreach (var item in data)
            {
                createUl(ul, item, db);
            }
            Mytree.Controls.Add(ul);
        }
    }

    void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {



            if (Session["EMS_Session"] != null)
            {
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    if (((EMS_Session)Session["EMS_Session"]).Company == null)
                    {
                        var Query = db.VT_SP_GetEmployees(0).ToList();
                        //var Query = db.VT_SP_GetUser(0).ToList();
                        grdUser.DataSource = Query;
                        grdUser.DataBind();
                    }
                    else
                    {
                        divCompany.Visible = false;
                        var Query = db.VT_SP_GetEmployees(vt_Common.CompanyId).ToList();
                        grdUser.DataSource = Query;
                        grdUser.DataBind();
                    }
                }
            }
           
        }
    }

    void createUl(HtmlGenericControl ul, dynamic item, vt_EMSEntities db)
    {
        HtmlGenericControl li = new HtmlGenericControl("li");

        HtmlGenericControl lblall = new HtmlGenericControl("span");
        lblall.Attributes.Add("class", "label label-default");
        HtmlGenericControl chkall = new HtmlGenericControl("input");
        chkall.Attributes.Add("type", "checkbox");
        chkall.Attributes.Add("id", "chkAll_" + item.PageID);
        if (item.Can_View && item.Can_Insert && item.Can_Update && item.Can_Delete)
            chkall.Attributes.Add("checked", "checked");
        lblall.Controls.Add(chkall);
        HtmlGenericControl spnall = new HtmlGenericControl("label");
        spnall.InnerText = item.PageName;
        spnall.Attributes.Add("for", "chkAll_" + item.PageID);
        lblall.Controls.Add(spnall);
        li.Controls.Add(lblall);

        HtmlGenericControl spnView = new HtmlGenericControl("span");
        spnView.Attributes.Add("class", "label label-primary");
        HtmlGenericControl chkView = new HtmlGenericControl("input");
        chkView.Attributes.Add("type", "checkbox");
        chkView.Attributes.Add("id", "chkView_" + item.PageID);
        if (item.Can_View)
            chkView.Attributes.Add("checked", "checked");
        spnView.Controls.Add(chkView);
        HtmlGenericControl lblView = new HtmlGenericControl("label");
        lblView.InnerText = "View";
        lblView.Attributes.Add("for", "chkView_" + item.PageID);
        spnView.Controls.Add(lblView);
        lblall.Controls.Add(spnView);

        HtmlGenericControl spnInsert = new HtmlGenericControl("span");
        spnInsert.Attributes.Add("class", "label label-success");
        HtmlGenericControl chkInsert = new HtmlGenericControl("input");
        chkInsert.Attributes.Add("type", "checkbox");
        chkInsert.Attributes.Add("id", "chkInsert_" + item.PageID);
        if (item.Can_Insert)
            chkInsert.Attributes.Add("checked", "checked");
        spnInsert.Controls.Add(chkInsert);
        HtmlGenericControl lblinsert = new HtmlGenericControl("label");
        lblinsert.InnerText = "Insert";
        lblinsert.Attributes.Add("for", "chkInsert_" + item.PageID);
        spnInsert.Controls.Add(lblinsert);
        lblall.Controls.Add(spnInsert);

        HtmlGenericControl spnUpdate = new HtmlGenericControl("span");
        spnUpdate.Attributes.Add("class", "label label-info");
        HtmlGenericControl chkUpdate = new HtmlGenericControl("input");
        chkUpdate.Attributes.Add("type", "checkbox");
        chkUpdate.Attributes.Add("id", "chkUpdate_" + item.PageID);
        if (item.Can_Update)
            chkUpdate.Attributes.Add("checked", "checked");
        spnUpdate.Controls.Add(chkUpdate);
        HtmlGenericControl lblUpdate = new HtmlGenericControl("label");
        lblUpdate.InnerText = "Update";
        lblUpdate.Attributes.Add("for", "chkUpdate_" + item.PageID);
        spnUpdate.Controls.Add(lblUpdate);
        lblall.Controls.Add(spnUpdate);

        HtmlGenericControl spnDelete = new HtmlGenericControl("span");
        spnDelete.Attributes.Add("class", "label label-danger");
        HtmlGenericControl chkDelete = new HtmlGenericControl("input");
        chkDelete.Attributes.Add("type", "checkbox");
        chkDelete.Attributes.Add("id", "chkDelete_" + item.PageID);
        if (item.Can_Delete)
            chkDelete.Attributes.Add("checked", "checked");
        spnDelete.Controls.Add(chkDelete);
        HtmlGenericControl lblDelete = new HtmlGenericControl("label");
        lblDelete.InnerText = "Delete";
        lblDelete.Attributes.Add("for", "chkDelete_" + item.PageID);
        spnDelete.Controls.Add(lblDelete);
        lblall.Controls.Add(spnDelete);

        ul.Controls.Add(li);

        int RoleID = vt_Common.CheckInt(hdnRoleID.Value);
        int PageID = item.PageID;

        var childPages = (from x in db.vt_tbl_SitePages
                          join y in db.vt_tbl_Page_Role_Mapping on new { PID = x.PageID, RID = RoleID } equals new { PID = (int)y.PageID, RID = y.RoleID } into jointable
                          from z in jointable.DefaultIfEmpty()
                          select new
                          {
                              Can_View = z.Equals(null) ? false : z.Can_View,
                              Can_Insert = z.Equals(null) ? false : z.Can_Insert,
                              Can_Update = z.Equals(null) ? false : z.Can_Update,
                              Can_Delete = z.Equals(null) ? false : z.Can_Delete,
                              PageID = x.PageID,
                              x.PageName,
                              x.ParentID
                          }).Where(x => x.ParentID.Equals(PageID)).ToList();

        if (childPages.Count > 0)
        {
            HtmlGenericControl divexpand = new HtmlGenericControl("div");
            divexpand.Attributes.Add("class", "divexpand");
            HtmlGenericControl expand = new HtmlGenericControl("span");
            expand.Attributes.Add("class", "glyphicon glyphicon-chevron-down");
            divexpand.Controls.Add(expand);
            lblall.Controls.Add(divexpand);
            li.Attributes.Add("class", "parent_li");
            HtmlGenericControl subul = new HtmlGenericControl("ul");
            li.Controls.Add(subul);
            foreach (var subitem in childPages)
            {
                createUl(subul, subitem, db);
            }
        }
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#createuser').modal('hide');loadRoleData();");
    }

    void ClearRoleForm()
    {
        vt_Common.Clear(pnlrDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#createrole').modal('hide');loadRoleData();");
    }

    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {

            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == ID);
            txtUserName.Text = emp.EmployeeName;
            txtPwd.Attributes.Add("value", vt_Common.Decrypt(emp.EmpPassword));
            txtRePwd.Attributes.Add("value", vt_Common.Decrypt(emp.EmpPassword));
            ddlRole.SelectedValue = emp.RoleID.ToString();
        }
        ViewState["PageID"] = ID;
    }



    //void  Users(int CompanyId)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        vt_tbl_User e = db.vt_tbl_User.FirstOrDefault(x => x.CompanyID == CompanyId);
    //        if (CompanyId == 0)
    //        {
    //            var query = (from E in db.vt_tbl_User
    //                         select new
    //                         {
    //                             E.UserID,
    //                             E.UserName,
    //                             E.RoleID
                                 
    //                         }).ToList();
    //        }

    //        else
    //        {
    //            var query = (from E in db.vt_tbl_User
    //                         join C in db.vt_tbl_Company on E.CompanyID equals C.CompanyID
    //                         where E.UserID == CompanyId
    //                         select new
    //                         {
    //                             E.UserID,
    //                             E.UserName,
    //                             E.RoleID

    //                         }).ToList();

    //        }
    //    }

    //}







    #endregion

    
    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetEmployees(Convert.ToInt32(ddlComp.SelectedValue));
            grdUser.DataSource = Query;
            grdUser.DataBind();
            UpView.Update();
        }

    }
}