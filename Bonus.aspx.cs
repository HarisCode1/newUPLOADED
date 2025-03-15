using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Bonus : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    Bonus_BAL BAL = new Bonus_BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                LoadGrid();
                BinEmpType();
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                ddlComp.SelectedValue = Companyid.ToString();
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;

            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Bonus.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    ViewState["hdnID"] = BAL.ID;
                    vt_Common.ReloadJS(this.Page, "$('#ModalBonus').modal();");
                }
                else if (Row["PageUrl"].ToString() == "Bonus.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int TypeOfEmp = 0;
                int CompID = 0;
                int Salary = 0;
                TypeOfEmp = vt_Common.CheckInt(ddlTypeOfEmp.SelectedValue);
                CompID = vt_Common.CheckInt(ddlComp.SelectedValue);
                Salary = vt_Common.CheckInt(ddlSalaryType.SelectedValue);
                BAL.ID = Convert.ToInt32(ViewState["hdnID"]);
                //var query = db.vt_tbl_Bonus.Where(x => x.EmpTypeId == TypeOfEmp && x.IsActive == true && x.CompanyId == CompID && x.SalaryTypeId == Salary && x.ID != BAL.ID).ToList();

                //if (query.Count == 0)
                //{
                //BAL.ID = ID.Text.Equals("") ? 0 : Convert.ToInt32(ID.Text);
                BAL.CompanyID = Convert.ToInt32(ddlComp.SelectedValue);
                    BAL.SalaryID = Convert.ToInt32(ddlSalaryType.SelectedValue);
                    BAL.BonusTitle = txtBonusTitle.Text;
                    BAL.EmpTypeId = Convert.ToInt32(ddlTypeOfEmp.SelectedValue);
                    BAL.MatureDays = Convert.ToInt32(txtMatureDays.Text);
                    BAL.BonusDays = Convert.ToInt32(txtBonusDays.Text);
                    BAL.Amount = Convert.ToInt32(txtAmount.Text);
                    BAL.IsActive = true;
                    if (BAL.ID > 0)
                    {
                        BAL.ModifiedOn = DateTime.Now;
                        BAL.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                        BAL.UpdateSP(BAL);
                    }
                    else
                    {
                        BAL.CreatedOn = DateTime.Now;
                        BAL.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        BAL.CreateSP(BAL);
                    }
                    Msg = "Record Save Successfully";
                    MsgBox.Show(Page, MsgBox.success, "Message ", Msg);
                ClearForm();
                LoadGrid();
                UpView.Update();
                UpDetail.Update();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
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
    void ClearForm()
    {
        try
        {
            ViewState["PageID"] = null;
            vt_Common.Clear(pnlDetail.Controls);
            vt_Common.ReloadJS(this.Page, "$('#ModalBonus').modal('hide');");
            divBonus.Visible = false;
            divother.Visible = false;
            int Companyid = Convert.ToInt32(Session["CompanyId"]);
            ddlComp.SelectedValue = Companyid.ToString();

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
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Bonus.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    if (e.CommandArgument.ToString() != "")
                    {
                        Bonus_BAL BAl = BAL.GetBonusByID(Convert.ToInt32(e.CommandArgument));
                        ViewState["hdnID"] = BAl.ID.ToString();
                     //   ddlComp.SelectedValue = BAl.CompanyID.ToString();
                        ddlSalaryType.SelectedValue = BAl.SalaryID.ToString();
                        if (ddlSalaryType.SelectedItem.Text == "Fixed")
                        {
                            divother.Visible = true;
                            txtAmount.Text = BAl.Amount.ToString();
                            txtBonusDays.Text = "0";
                        }
                        else
                        {
                            divBonus.Visible = true;
                            txtBonusDays.Text = BAl.BonusDays.ToString();
                            txtAmount.Text = "0";
                        }
                        txtBonusTitle.Text = BAl.BonusTitle;
                        ddlTypeOfEmp.SelectedValue = BAl.EmpTypeId.ToString();
                        txtMatureDays.Text = BAl.MatureDays.ToString();
                        chkIsActive.Checked = vt_Common.CheckBoolean(BAl.IsActive);
                        vt_Common.ReloadJS(this.Page, "$('#ModalBonus').modal();");
                        UpDetail.Update();
                    }

                }
                else if (Row["PageUrl"].ToString() == "Bonus.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                }
            }

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public void LoadGrid()
    {
        try
        {

            //EMS_Session s = (EMS_Session)Session["EMS_Session"];
            //ViewState["hdnID"] = s.user.CompanyId;
            //BAL.CompanyID = Convert.ToInt32(ViewState["hdnID"]);
            //vt_Common.Bind_GridView(GridBonus, BAL.GetData(BAL.CompanyID));
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int CompanyId =Convert.ToInt32(Session["CompanyId"]);
                if (CompanyId != 0)
                {
                    var Query = db.GetBonus(CompanyId).ToList();
                    GridBonus.DataSource = Query;
                    GridBonus.DataBind();
                }
              
            }

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public void BinEmpType()
    {
        try
        {
            vt_Common.Bind_DropDown(ddlComp, "vt_sp_GetCompanies", "CompanyName", "CompanyID");
            vt_Common.Bind_DropDown(ddlSalaryType, "vt_sp_GetSalaryType", "Type", "Id");
            vt_Common.Bind_DropDown(ddlTypeOfEmp, "vt_sp_GetType", "Type", "Id");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    //protected void bntDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        btnSave.Visible = false;

    //        PayRoll_Session PayRoll = (PayRoll_Session)Session["PayrollSess"];
    //        //BAL.ID = hdnID.Value.Equals("") ? 0 : Convert.ToInt32(ID.Text);
    //        BAL.CompanyID = Convert.ToInt32(ddlComp.SelectedValue);
    //        BAL.SalaryID = Convert.ToInt32(ddlSalaryType.SelectedValue);
    //        BAL.BonusTitle = txtBonusTitle.Text;
    //        BAL.EmpTypeId = Convert.ToInt32(ddlTypeOfEmp.Text);
    //        BAL.MatureDays = Convert.ToInt32(txtMatureDays.Text);
    //        BAL.BonusDays = Convert.ToInt32(txtBonusDays.Text);
    //        BAL.ModifiedOn = DateTime.Now;
    //        BAL.ModifiedBy = PayRoll.UserID;
    //        BAL.IsActive = false;
    //        BAL.UpdateSP(BAL);
    //        ClearForm();
    //        LoadGrid();
    //        UpView.Update();
    //        btnSave.Visible = true;

    //        MsgBox.Show(Page, MsgBox.success, txtMatureDays.Text, "Record Delete Successfully");
    //        ddlComp.Enabled = false;
    //        ddlSalaryType.Enabled = false;
    //        ddlTypeOfEmp.Enabled = false;
    //        txtBonusTitle.Enabled= false;
    //        txtMatureDays.Enabled = false;

    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.TryCatchException(ex);
    //        throw ex;
    //    }
    //}
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                Bonus_BAL BAl = BAL.GetBonusByID(Convert.ToInt32(e.CommandArgument));
                MsgDelete.Text = BAl.ID.ToString();
                vt_Common.ReloadJS(this.Page, "$('#Delete').modal();");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void btndelete_Command(object sender, CommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        foreach (DataRow Row in Dt.Rows)
        {
            int ID = Convert.ToInt32(MsgDelete.Text);
            if (ID > 0)
            {
                var qry = db.vt_tbl_StaffBonus.Where(x => x.BonusId == ID).FirstOrDefault();
                if (qry == null)
                {
                    if (Row["PageUrl"].ToString() == "Bonus.aspx" && Row["Can_Delete"].ToString() == "True")
                    {
                        BAL.ID = Convert.ToInt32(MsgDelete.Text);
                        vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
                        BAL.DeleteBonusByID(BAL.ID);
                        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
                        LoadGrid();
                        UpView.Update();
                    }
                    else if (Row["PageUrl"].ToString() == "Bonus.aspx" && Row["Can_Delete"].ToString() == "False")
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                    }
                }
                else
                {
                    vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
                    MsgBox.Show(this.Page, MsgBox.danger, "", "You can not delete it exist in satff bonus");
                    LoadGrid();
                    UpView.Update();
                }
            }
          
        }

    }

    //protected void chkBonus_CheckedChanged(object sender, EventArgs e)
    //{
    //    if(divBonus.Visible == false)
    //    {
    //        divBonus.Visible = true;
    //    }
    //    else
    //    {
    //        divBonus.Visible = false;

    //    }
    //}
    //protected void chkOthers_CheckedChanged(object sender, EventArgs e)
    //{
    //    if(divother.Visible==false)
    //    {
    //        divother.Visible = true;
    //    }
    //    else
    //    {
    //        divother.Visible = false;
    //    }
    //}

    protected void ddlSalaryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalaryType.SelectedItem.Text == "Fixed")
        {
            divother.Visible = true;
            divBonus.Visible = false;
            txtBonusDays.Text = "0";
        }
        else
        {
            divother.Visible = false;
            divBonus.Visible = true;
            txtAmount.Text = "0";
        }
    }
}