using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class StaffBonus : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    StaffBonus_BAL BAL = new StaffBonus_BAL();
    int CompanyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            CompanyID =Convert.ToInt32(Session["CompanyID"]);
            if (!IsPostBack)
            {
                LoadGrid();
                BinEmpType();
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
                if (Row["PageUrl"].ToString() == "StaffBonus.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    ViewState["hdnID"] = BAL.ID;
                    vt_Common.ReloadJS(this.Page, "$('#ModalBonus').modal();");
                }
                else if (Row["PageUrl"].ToString() == "StaffBonus.aspx" && Row["Can_Insert"].ToString() == "False")
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
        int CompanyID =Convert.ToInt32(Session["CompanyID"]);
        string Msg = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int Bonus = 0;
                //DateTime Month = "";
                //int Salary = 0;
                Bonus = vt_Common.CheckInt(ddlBonus.SelectedValue);
                if (Bonus != 0)
                {
                    DateTime Month = vt_Common.CheckDateTime(txtDate.Text);
                    //Salary = vt_Common.CheckInt(ddlSalaryType.SelectedValue);
                    BAL.ID = Convert.ToInt32(ViewState["hdnID"]);
                    var checkmonth = db.vt_tbl_StaffBonus.Where(x => x.Month == Month).FirstOrDefault(); //&& x.CompanyID == CompanyID
                    if (checkmonth == null)
                    {
                        //var query = db.vt_tbl_Bonus.Where(x => x.EmpTypeId == TypeOfEmp && x.IsActive == true && x.CompanyId == CompID && x.SalaryTypeId == Salary && x.ID != BAL.ID).ToList();
                        var query = db.vt_tbl_StaffBonus.Where(x => x.BonusId == Bonus && x.Month == Month && x.IsActive == true && x.Id != BAL.ID).ToList();
                        if (query.Count == 0)
                        {

                            //BAL.ID = ID.Text.Equals("") ? 0 : Convert.ToInt32(ID.Text);
                            BAL.BonusID = Convert.ToInt32(ddlBonus.SelectedValue);
                            BAL.Month = Convert.ToDateTime(txtDate.Text);
                            BAL.CompanyID = CompanyID;
                            //BAL.BonusTitle = txtBonusTitle.Text;
                            //BAL.EmpTypeId = Convert.ToInt32(ddlTypeOfEmp.SelectedValue);
                            //BAL.MatureDays = Convert.ToInt32(txtMatureDays.Text);
                            //BAL.BonusDays = Convert.ToInt32(txtBonusDays.Text);
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
                        }
                        else
                        {
                            Msg = "Record is already exist";
                        }
                        MsgBox.Show(Page, MsgBox.success, "Message ", Msg);
                        ClearForm();
                        LoadGrid();
                        UpView.Update();
                        UpDetail.Update();
                    }
                    else
                    {

                        MsgBox.Show(Page, MsgBox.danger, "Already given Bonus of this month", Msg);
                    }


                }
                else
                {

                    MsgBox.Show(Page, MsgBox.danger, "kindly select Bonus", Msg);
                }

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
            if ((string)Session["UserName"] == "SuperAdmin")
            {
                MsgBox.Show(Page, MsgBox.danger, "Message ", "You can not access");
            }
            else
            {
                DataTable Dt = Session["PagePermissions"] as DataTable;
                foreach (DataRow Row in Dt.Rows)
                {
                    if (Row["PageUrl"].ToString() == "StaffBonus.aspx" && Row["Can_Update"].ToString() == "True")
                    {
                        if (e.CommandArgument.ToString() != "")
                        {
                            if (e.CommandArgument.ToString() != "")
                            {
                                StaffBonus_BAL BAl = BAL.GetBonusByID(Convert.ToInt32(e.CommandArgument));
                                ViewState["hdnID"] = BAl.ID.ToString();
                                ddlBonus.SelectedValue = BAl.BonusID.ToString();
                                txtDate.Text = BAl.Month.ToShortDateString();
                                //ddlTypeOfEmp.SelectedValue = BAl.EmpTypeId.ToString();
                                //txtMatureDays.Text = BAl.MatureDays.ToString();
                                //txtBonusDays.Text = BAl.BonusDays.ToString();
                                chkIsActive.Checked = vt_Common.CheckBoolean(BAl.IsActive);
                                vt_Common.ReloadJS(this.Page, "$('#ModalBonus').modal();");
                                UpDetail.Update();
                            }
                        }

                    }
                    else if (Row["PageUrl"].ToString() == "StaffBonus.aspx" && Row["Can_Update"].ToString() == "False")
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
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
    public void LoadGrid()
    {
        try
        {
            int companyid = Convert.ToInt32(Session["CompanyId"]);
            //EMS_Session s = (EMS_Session)Session["EMS_Session"];
            //ViewState["hdnID"] = s.user.CompanyId;
            //BAL.CompanyID = Convert.ToInt32(ViewState["hdnID"]);
            //vt_Common.Bind_GridView(GridBonus, BAL.GetData(BAL.CompanyID));
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                DataTable dt = ProcedureCall.GetStaffBonus(companyid == 0 ? 0 : companyid).Tables[0];
                if (dt.Rows.Count  > 0)
                {
                    GridBonus.DataSource = dt;
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
            SqlParameter[] param =
                {

            new SqlParameter("@CompanyId", Session["CompanyId"]) };

            vt_Common.Bind_DropDown(ddlBonus, "vt_sp_BindBonus", "BonusTitle", "ID", param);
            //vt_Common.Bind_DropDown(ddlSalaryType, "vt_sp_GetSalaryType", "Type", "Id");
            //vt_Common.Bind_DropDown(ddlTypeOfEmp, "vt_sp_GetType", "Type", "Id");
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
            if ((string)Session["UserName"] == "SuperAdmin")
            {
                MsgBox.Show(Page, MsgBox.danger, "Message ", "You can not access");
            }
            else
            {
                if (e.CommandArgument.ToString() != "")
                {
                    ViewState["BonusId"] = e.CommandArgument;
                    StaffBonus_BAL BAl = BAL.GetBonusByID(Convert.ToInt32(e.CommandArgument));
                    MsgDelete.Text = BAl.ID.ToString();
                    vt_Common.ReloadJS(this.Page, "$('#Delete').modal();");
                }
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
        try
        {
           
                int bonusid = Convert.ToInt32(ViewState["BonusId"]);
                DataTable Dt = Session["PagePermissions"] as DataTable;
                foreach (DataRow Row in Dt.Rows)
                {
                    if (Row["PageUrl"].ToString() == "StaffBonus.aspx" && Row["Can_Delete"].ToString() == "True")
                    {
                        BAL.ID = Convert.ToInt32(MsgDelete.Text);
                        vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
                        BAL.DeleteBonusByID(bonusid);
                        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
                        LoadGrid();
                        UpView.Update();

                    }
                    else if (Row["PageUrl"].ToString() == "StaffBonus.aspx" && Row["Can_Delete"].ToString() == "False")
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                    }
                }
            
                
        }
        catch (Exception)
        {

            throw;
        }
      
    }
}