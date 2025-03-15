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

public partial class Holiday : System.Web.UI.Page
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
                            if (Row["PageName"].ToString() == "Holiday")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Holiday")
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
    protected void grdHoliday_RowCommand(object sender, GridViewCommandEventArgs e)
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
                            if (Row["PageUrl"].ToString() == "Holiday.aspx" && Row["Can_Delete"].ToString() == "True")
                            {
                                using (vt_EMSEntities db = new vt_EMSEntities())
                                {
                                    int ID = vt_Common.CheckInt(e.CommandArgument);
                                    vt_tbl_Holiday h = db.vt_tbl_Holiday.FirstOrDefault(x => x.HolidayID == ID);
                                    db.vt_tbl_Holiday.Remove(h);
                                    db.SaveChanges();
                                    LoadData();
                                    UpView.Update();
                                    MsgBox.Show(Page, MsgBox.success, h.HolidayName, "Successfully Deleted");
                                }
                            }

                            else if (Row["PageUrl"].ToString() == "Holiday.aspx" && Row["Can_Delete"].ToString() == "False")
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
                        if (Row["PageUrl"].ToString() == "Holiday.aspx" && Row["Can_Update"].ToString() == "True")
                        {
                            FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                            vt_Common.ReloadJS(this.Page, "$('#Holiday').modal();");
                            UpDetail.Update();
                            break;
                        }
                        else if (Row["PageUrl"].ToString() == "Holiday.aspx" && Row["Can_Update"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Update this record");
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
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int companyID = 0;
                if (Session["CompanyId"] != null)
                {
                    //companyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                    companyID = Convert.ToInt32(Session["CompanyId"]);
                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }
                var record = db.vt_tbl_Holiday.FirstOrDefault(o => o.HolidayID != recordID && o.CompanyID == companyID && o.HolidayName.ToLower().Replace(" ", "").Equals(txtName.Text.ToLower().Replace(" ", "")));
                if (record != null)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Holiday with the same name already exist');");
                }
                else
                {
                    vt_tbl_Holiday h = new vt_tbl_Holiday();
                    h.CompanyID = companyID;
                    h.HolidayName = txtName.Text;
                    h.FromDate = vt_Common.CheckDateTime(txtFromDate.Text);
                    h.ToDate = vt_Common.CheckDateTime(txtToDate.Text);
                    h.Payble = chkPayble.Checked;
                    if (ViewState["PageID"] != null)
                    {
                        h.HolidayID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(h).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.vt_tbl_Holiday.Add(h);
                    }
                    db.SaveChanges();
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
            if (Row["PageUrl"].ToString() == "Holiday.aspx" && Row["Can_Insert"].ToString() == "True")
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
                    vt_Common.ReloadJS(this.Page, "$('#Holiday').modal();");
                }
                else
                {
                    ddlCompany.Visible = false;
                    trCompany.Visible = false;
                    vt_Common.ReloadJS(this.Page, "$('#Holiday').modal();");
                }
            }

            else if (Row["PageUrl"].ToString() == "Holiday.aspx" && Row["Can_Insert"].ToString() == "False")
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
                grdHoliday.Columns[1].Visible = false;
                companyID = Convert.ToInt32(Session["CompanyId"]);
            }
            var Query = db.VT_SP_GetHolidays(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
            grdHoliday.DataSource = Query;
            grdHoliday.DataBind();
        }
        //}
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#Holiday').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Holiday h = db.vt_tbl_Holiday.FirstOrDefault(x => x.HolidayID == ID);
            ddlcomp.SelectedValue = vt_Common.CheckString(h.CompanyID);
            txtName.Text = h.HolidayName;
            txtFromDate.Text = ((DateTime)h.FromDate).ToString("MM/dd/yyyy");
            txtToDate.Text = ((DateTime)h.ToDate).ToString("MM/dd/yyyy");
            chkPayble.Checked = h.Payble;
        }
        ViewState["PageID"] = ID;
    }
    #endregion 
}