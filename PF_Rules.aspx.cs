using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class PF_Rules : System.Web.UI.Page
{
    PF_Rules_BAL PF = new PF_Rules_BAL();
    TypeOfEmployee_BAL TP = new TypeOfEmployee_BAL();
    public void LoadData()
    {
        vt_Common.Bind_GridView(GridEmployee, PF.GetPF_RulesByID(1));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ddSalaryType.SelectedValue = "2";
            LoadData();
            DropdownBind();
        }
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        vt_Common.ReloadJS(this.Page, "ClearFields();");
        vt_Common.ReloadJS(this.Page, "$('#Modal').modal();");
    }
    protected void DropdownBind()
    {

        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ddEmployee.Items.Clear();
            var TypeEmployeeDropdown = (from m in db.tbl_TypeOfEmployee
                                        select new
                                        {
                                            m.Type,
                                            m.Id
                                        }).ToList();

            ddEmployee.DataSource = TypeEmployeeDropdown;
            ddEmployee.DataTextField = "Type";
            ddEmployee.DataValueField = "Id";
            ddEmployee.DataBind();
            // ddEmployee.Items.Insert(0, new ListItem("Please Select", "0"));
            ddEmployee.Items.Insert(1, new ListItem("Please Select", "1"));


        }


    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        btnsaves.Text = "Update";

        try
        {

            vt_Common.ReloadJS(this.Page, "$('#Modal').modal();");
            //btnSaves.Text = "Update";
            if (e.CommandArgument.ToString() != "")
            {
                PF_Rules_BAL rules = PF.GetPF_rules(Convert.ToInt32(e.CommandArgument));
                txtID.Text = rules.Id.ToString();
                ddEmployee.SelectedItem.Text = rules.TypeOfEmployee;
                ddSalaryType.SelectedItem.Text = rules.SalaryType;
                var percent = (txtpercent.Text);
                txtpercent.Text = rules.Percent.ToString();
                chkActive.Checked = rules.Active;
                UpDetail.Update();


            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
        }
    }

    protected void btnsave_PFRules(object sender, EventArgs e)
    {
        try
        {

            if (string.IsNullOrEmpty(txtID.Text))
            {
                SavePf_Rules();
                LoadData();
                UpView.Update();
            }
            else
            {
                UpdatePF_Rules();
                LoadData();
                UpView.Update();

            }


        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
        }
    }
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {

            PF.Id = Convert.ToInt32(TxtIDs.Text);
            PF.DeletePFRules(PF.Id);
            vt_Common.ReloadJS(this.Page, "$('#deleteform').modal('hide')");
            LoadData();
            UpView.Update();
            MsgBox.Show(Page, MsgBox.success, PF.TypeOfEmployee, "Successfully Deleted");


        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    protected void SavePf_Rules()
    {
        try
        {

            PF.TypeOfEmployee = ddEmployee.SelectedItem.Text;
            PF.SalaryType = (ddSalaryType.SelectedItem.Text);
            var percent = (txtpercent.Text);
            PF.Percent = Convert.ToDecimal(percent);
            PF.Active = chkActive.Checked;
            PF.CreatedOn = DateTime.Now;
            PF.Active = chkActive.Checked;
            PF.Deleted = DateTime.Now;
            PF.UpdatedOn = DateTime.Now;

            PF.Sp_insertUpdatePF_Rules(PF);
            vt_Common.ReloadJS(this.Page, "$('#Modal').modal('hide')");
            LoadData();
            MsgBox.Show(Page, MsgBox.success, PF.TypeOfEmployee, "Successfully Saved");
            vt_Common.Clear(pnlDetail.Controls);



        }
        catch (Exception ex)
        {
        }
    }
    private void UpdatePF_Rules()
    {
        try
        {
            PF.Id = Convert.ToInt32(txtID.Text);
            PF.TypeOfEmployee = ddEmployee.SelectedItem.Text;
            PF.SalaryType = (ddSalaryType.SelectedItem.Text);
            var percent = (txtpercent.Text);
            PF.Percent = Convert.ToDecimal(percent);
            PF.Active = chkActive.Checked;
            PF.CreatedOn = DateTime.Now;
            PF.Active = chkActive.Checked;
            PF.Deleted = DateTime.Now;
            PF.UpdatedOn = DateTime.Now;
            PF.Sp_insertUpdatePF_Rules(PF);
            vt_Common.ReloadJS(this.Page, "$('#Modal').modal('hide')");
            LoadData();
            // Alert("running");
            MsgBox.Show(Page, MsgBox.success, PF.TypeOfEmployee, "Successfully Updated");
            vt_Common.Clear(pnlDetail.Controls);
        }
        catch (Exception ex)
        {
        }

    }
    protected void lbtnDelete_Modalshow(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            PF_Rules_BAL rules = PF.GetPF_rules(Convert.ToInt32(e.CommandArgument));
            TxtIDs.Text = rules.Id.ToString();
            vt_Common.ReloadJS(this.Page, "$('#deleteform').modal();");
        }
    }
    protected void lbtnDelete_ModalHide(object sender, CommandEventArgs e)
    {

        vt_Common.ReloadJS(this.Page, "$('#deleteform').modal('hide')");
    }
}