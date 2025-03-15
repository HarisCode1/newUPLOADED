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

public partial class EmployeeType : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                if ((string)Session["UserName"]!="SuperAdmin")
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
                            if (Row["PageName"].ToString() == "Employee Type")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Employee Type")
                        {
                            //   LoadData();
                            Bind_GV();
                            BindSalaryType();
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                    }
                }
                else
                {
                    Bind_GV();
                    BindSalaryType();
                    //MsgBox.Show(Page, MsgBox.danger, "", "You are Loged in by SuperAdmin");
                }

            }
        }

    }

    #region Click
    protected void btnAddNew_OnClick(object sender, EventArgs e)
    {
        ClearForm();

        DataTable Dt = Session["PagePermissions"] as DataTable;
        if ((string)Session["UserName"] != "SuperAdmin")
        {
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "EmployeeType.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    vt_Common.ReloadJS(this.Page, "$('#ModalDesignation').modal()");
                }

                else if (Row["PageUrl"].ToString() == "EmployeeType.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to insert");
                }
            }
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "You are Loged in by SuperAdmin");
        }
    }

    protected void Btn_OnClick(object sender, EventArgs e)
    {

        Response.Write("<script>alert()</script>");
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        int ID = 0;
        if (HdnID.Value != "")
        {
            ID = Convert.ToInt32(HdnID.Value);
        }
        var Data = db.vt_tbl_TypeofEmployee.Where(x => x.Id == ID).FirstOrDefault();
        if (Data != null)
        {
            Data.Type = TxtEmployementType.Text;
            Data.IsOvertime = ChkOvertime.Checked;
            Data.Description = TxtDescription.Text;
            Data.SalaryType = Convert.ToInt32(DdlSalaryType.SelectedItem.Value);
            if (TxtAmount.Text != "")
            {
                Data.Amount = Convert.ToDecimal(TxtAmount.Text);
            }
            db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
        }
        else
        {
            vt_tbl_TypeofEmployee TypeEmp = new vt_tbl_TypeofEmployee();
            TypeEmp.Type = TxtEmployementType.Text;
            TypeEmp.IsOvertime = ChkOvertime.Checked;
            TypeEmp.Description = TxtDescription.Text;
            TypeEmp.SalaryType = Convert.ToInt32(DdlSalaryType.SelectedItem.Value);
            if (TxtAmount.Text != "")
            {
                TypeEmp.Amount = Convert.ToDecimal(TxtAmount.Text);
            }
            db.Entry(TypeEmp).State = System.Data.Entity.EntityState.Added;
        }
        db.SaveChanges();
        Response.Redirect("EmployeeType.aspx");
    }
    #endregion

    #region Data Fill

    void LoadData()
    {
        var Data = db.vt_tbl_TypeofEmployee.ToList();
        if (Data != null)
        {
            GvEmployeeType.DataSource = Data;
        }
        else
        {

            GvEmployeeType.DataSource = null;
        }
        GvEmployeeType.DataBind();
        
    }
    private void Bind_GV()
    {
        DataSet Ds = ProcedureCall.SpCall_BindTypeOfEmployee();
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                GvEmployeeType.DataSource = Dt;
            }
            else
            {
                GvEmployeeType.DataSource = null;
            }
        }
        else
        {
            GvEmployeeType.DataSource = null;
        }
        GvEmployeeType.DataBind();
    }
    #endregion

    #region Change
    protected void ChkOvertime_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkOvertime.Checked)
        {
            DivSalartType.Visible = true;
        }
        else
        {
            DivSalartType.Visible = false;
        }
    }
    #endregion

    public void BindSalaryType()
    {
        vt_Common.Bind_DropDown(DdlSalaryType, "Bind_SalaryType", "Type", "ID");
    }

    protected void GvEmployeeType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        if ((string)Session["UserName"] != "SuperAdmin")
        {


            switch (e.CommandName)
            {
                case "DeleteEmployeeType":
                    try
                    {
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageUrl"].ToString() == "EmployeeType.aspx" && Row["Can_Delete"].ToString() == "True")
                            {
                                using (vt_EMSEntities db = new vt_EMSEntities())
                                {
                                    int ID = vt_Common.CheckInt(e.CommandArgument);
                                    vt_tbl_TypeofEmployee b = db.vt_tbl_TypeofEmployee.FirstOrDefault(x => x.Id == ID);
                                    db.vt_tbl_TypeofEmployee.Remove(b);
                                    db.SaveChanges();
                                    Response.Redirect("EmployeeType.aspx");
                                    LoadData();
                                    UpView.Update();
                                    MsgBox.Show(Page, MsgBox.success, "", "Successfully Deleted");
                                }
                            }
                            else if (Row["PageUrl"].ToString() == "EmployeeType.aspx" && Row["Can_Delete"].ToString() == "False")
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
                case "EditEmployeeType":
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageUrl"].ToString() == "EmployeeType.aspx" && Row["Can_Update"].ToString() == "True")
                        {
                            FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                            vt_Common.ReloadJS(this.Page, "$('#ModalDesignation').modal();");
                            UpDetail.Update();
                        }
                        else if (Row["PageUrl"].ToString() == "EmployeeType.aspx" && Row["Can_Update"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                        }
                    }

                    break;
                default:
                    break;
            }
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "You are Loged in by SuperAdmin");
        }
    }
    void FillDetailForm(int ID) 
    {
        DivSalartType.Visible = false;

        HdnID.Value = ID.ToString();
        var Data = db.vt_tbl_TypeofEmployee.Where(x => x.Id == ID).FirstOrDefault();
        if (Data != null)
        {
            TxtEmployementType.Text = Data.Type;
            ChkOvertime.Checked = Convert.ToBoolean(Data.IsOvertime);
            TxtDescription.Text = Data.Description;
            DdlSalaryType.SelectedValue = Convert.ToInt32(Data.SalaryType).ToString();
            TxtAmount.Text = Convert.ToDecimal(Data.Amount).ToString();

            if (ChkOvertime.Checked)
            {
                DivSalartType.Visible = true;
            }
        }
    }

    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#department').modal('hide');");
    }

}