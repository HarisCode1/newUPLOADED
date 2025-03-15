using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Leave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                if ((string)Session["UserName"] != "SuperAdmin")
                {
                    int ModuleID = 9;
                    int RoleID = (int)Session["RoleId"];
                    vt_EMSEntities db = new vt_EMSEntities();
                    DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);

                    string PageName = null;
                    if (Ds != null && Ds.Tables.Count > 0)
                    {
                        DataTable Dt = Ds.Tables[0];
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageName"].ToString() == "Leave")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Leave")
                        {
                            LoadData();
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                    }
                }
                else
                {
                    LoadData();
                }
            }
        }
    }
    #region Control Event
    protected void grdLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        if ((string)Session["UserName"] != "SuperAdmin")
        {
            switch (e.CommandName)
            {
                case "DeleteCompany":
                    try
                    {
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageUrl"].ToString() == "Leave.aspx" && Row["Can_Delete"].ToString() == "True")
                            {
                                using (vt_EMSEntities db = new vt_EMSEntities())
                                {
                                    int ID = vt_Common.CheckInt(e.CommandArgument);
                                    vt_tbl_Leave l = db.vt_tbl_Leave.FirstOrDefault(x => x.LeaveID == ID);
                                    db.vt_tbl_Leave.Remove(l);
                                    db.SaveChanges();
                                    LoadData();
                                    UpView.Update();
                                    MsgBox.Show(Page, MsgBox.success, l.LeaveName, "Successfully Deleted");
                                }
                            }

                            else if (Row["PageUrl"].ToString() == "Leave.aspx" && Row["Can_Delete"].ToString() == "False")
                            {
                                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                            }
                        }

                    }
                    catch (DbUpdateException ex)
                    {
                        SqlException innerException = ex.GetBaseException() as SqlException;
                        vt_Common.PrintfriendlySqlException(innerException, Page);
                    }
                    break;
                case "EditCompany":
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageUrl"].ToString() == "Leave.aspx" && Row["Can_Update"].ToString() == "True")
                        {
                            FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                            vt_Common.ReloadJS(this.Page, "$('#leave').modal();");
                            UpDetail.Update();
                            break;
                        }

                        else if (Row["PageUrl"].ToString() == "Leave.aspx" && Row["Can_Update"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                        }
                    }

                    break;
            }
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "You are Loged in by SuperAdmin");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
        int LeaveID = 0;
        int LastLeaveValue = LeaveID;
        if (!string.IsNullOrWhiteSpace(hdLastLeaveAllow.Value))
        {
            LastLeaveValue = Convert.ToInt32(hdLastLeaveAllow.Value);
        }
        bool isInsert = false;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int companyID = 0;
                if (Session["CompanyId"] != null)
                {
                    companyID = (Convert.ToInt32(Session["CompanyId"]));
                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                var record = db.vt_tbl_Leave.FirstOrDefault(o => o.LeaveID != recordID && o.CompanyID == companyID && o.LeaveName.ToLower().Replace(" ", "").Equals(txtName.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Leave with the same name already exist');");
                }
                else
                {
                    vt_tbl_Leave l = new vt_tbl_Leave();
                    l.CompanyID = companyID;
                    l.LeaveName = txtName.Text;
                    l.LeaveShortName = txtShortName.Text;
                    l.CarryFoward = chkCarryForward.Checked;
                    l.NumberofLeaves = Convert.ToInt32(txtLeaveAllow.Text);
                    if (ViewState["PageID"] != null)
                    {
                        l.LeaveID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                        isInsert = false;
                    }
                    else
                    {
                        db.vt_tbl_Leave.Add(l);
                        isInsert = true;
                    }
                    db.SaveChanges();
                    if (isInsert)
                    {
                        LeaveID = db.vt_tbl_Leave.Where(x => x.LeaveShortName == txtShortName.Text && x.CompanyID == CompanyID).Select(x => x.LeaveID).SingleOrDefault();
                        db.VT_SP_InsertLeaveAllotment(CompanyID, LeaveID, txtLeaveAllow.Text);
                    }
                    else
                    {
                        LeaveID = Convert.ToInt32(ViewState["PageID"]);
                        db.VT_SP_UpdateLeaveAllotment(LastLeaveValue, LeaveID, txtLeaveAllow.Text);
                    }
                    MsgBox.Show(Page, MsgBox.success, txtName.Text, "Successfully Save");
                    ClearForm();
                    LoadData();
                    UpView.Update();
                }
            }
        }
        catch (DbUpdateException ex)
        {
        }
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        foreach (DataRow Row in Dt.Rows)
        {
            if (Row["PageUrl"].ToString() == "Leave.aspx" && Row["Can_Insert"].ToString() == "True")
            {
                ViewState["PageID"] = null;
                vt_Common.Clear(pnlDetail.Controls);
                UpDetail.Update();
                if (Session["CompanyId"] == null)
                {
                    if (ddlcomp.Items.FindByValue(ddlCompany.SelectedValue) != null)
                    {
                        ddlcomp.SelectedValue = ddlCompany.SelectedValue;
                    }
                    vt_Common.ReloadJS(this.Page, "$('#leave').modal();");
                }
                else
                {
                    ddlCompany.Visible = false;
                    trCompany.Visible = false;
                    vt_Common.ReloadJS(this.Page, "$('#leave').modal();");
                }
            }

            else if (Row["PageUrl"].ToString() == "Leave.aspx" && Row["Can_Insert"].ToString() == "False")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            }
        }

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
        UpView.Update();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    #endregion
    #region Healper Method
    void LoadData()
    {
        //if (Session["EMS_Session"] != null)
        //{
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int companyID = 0;
            if (Session["CompanyId"] == null)
            {
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            }
            else
            {
                divCompany.Visible = false;
                grdLeave.Columns[1].Visible = false;
                companyID = vt_Common.CompanyId;
            }
            //riaz
            var Query = db.VT_SP_GetLeaves(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"],1).ToList();
            grdLeave.DataSource = Query;
            grdLeave.DataBind();
        }
        //}
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#leave').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Leave l = db.vt_tbl_Leave.FirstOrDefault(x => x.LeaveID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(l.CompanyID);
            txtName.Text = l.LeaveName;
            txtShortName.Text = l.LeaveShortName;
            hdLastLeaveAllow.Value = l.NumberofLeaves.ToString();
            chkCarryForward.Checked = l.CarryFoward;
            txtLeaveAllow.Text = l.NumberofLeaves.ToString();
        }
        ViewState["PageID"] = ID;
    }
    #endregion
}