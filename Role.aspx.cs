using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Role : System.Web.UI.Page
{
    Role_BAl BAl = new Role_BAl();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Authenticate.Confirm())
        {
            if(!IsPostBack)
            {
                LoadGrid();
            }
        }
    }
    public void LoadGrid()
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                var Query = db.GetRoles();
                GridRole.DataSource = Query;
                GridRole.DataBind(); 

            }

            //vt_Common.Bind_GridView(GridRole, BAl.GetRoles());
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["hdnID"] = BAl.RoleID;
             vt_Common.ReloadJS(this.Page, "$('#myModalBranch').modal();");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            string msg = "";
            //EMS_Session EMS = (EMS_Session)Session["EMS_Session"];

            BAl.RoleID = Convert.ToInt32(ViewState["hdnID"]);
            
            var query = db.vt_tbl_Role.Where(x => x.Role == txtName.Text && x.RoleID != BAl.RoleID).ToList();
            if (query.Count == 0)
            {
                BAl.Role = txtName.Text;
                BAl.IsActive = true;
                if (BAl.RoleID > 0)
                {
                    BAl.UpdatedOn = DateTime.Now;
                    BAl.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                    BAl.ModifiedByID(BAl);
                }
                else
                {
                    BAl.CreatedOn = DateTime.Now;
                    BAl.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    BAl.CreateModifyRoles(BAl);
                }
                msg = "Record Save Successfully";
            }
            else
            {
                msg = "Record Already Exist Successfully";
            }
            MsgBox.Show(Page, MsgBox.success, txtName.Text, msg);

            ClearForm();
            LoadGrid();
            UpView.Update();
        }
    }
    void ClearForm()
    {
        try
        {
            vt_Common.Clear(pnlDetail.Controls);
            vt_Common.ReloadJS(this.Page, "$('#myModalBranch').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                Role_BAl BAL = BAl.GetRoleInfo(Convert.ToInt32(e.CommandArgument));
                ViewState["hdnID"] = BAL.RoleID.ToString();
                txtName.Text = BAL.Role.ToString();
                hdnID.Value = BAL.RoleID.ToString();
                chkISA.Checked = Convert.ToBoolean(BAL.IsActive);
                vt_Common.ReloadJS(this.Page, "$('#myModalBranch').modal();");
                UpDetail.Update();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if(e.CommandArgument.ToString()!="")
            {
                Role_BAl BAL = BAl.GetRoleInfo(Convert.ToInt32(e.CommandArgument));
                MsgDelete.Text = BAL.RoleID.ToString();
                vt_Common.ReloadJS(this.Page, "$('#Delete').modal();");

            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void btnClose_Click1(object sender, EventArgs e)
    {
        try
        {
            ClearForm();
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void lbtnRolePermissions_Command(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string RoleID = commandArgs[0];
        string RoleName = commandArgs[1];
        var url = String.Format("RolePermission.aspx?ID={0}&Name={1}", Viftech.vt_Common.Encrypt(RoleID), Viftech.vt_Common.Encrypt(RoleName));
        Response.Redirect(url);
    }
    protected void btndelete_Command(object sender, CommandEventArgs e)
    {
        BAl.RoleID= Convert.ToInt32(MsgDelete.Text);
        BAl.DeleteRoleByID(BAl.RoleID);
        vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");

        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
        LoadGrid();
        UpView.Update();
    }

    protected void lbtnRolePermissions_Click(object sender, EventArgs e)
    {
        Response.Redirect("RolePermissionNew.aspx");
    }
}