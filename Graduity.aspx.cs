using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Graduity : System.Web.UI.Page
{
    Graduity_BAl ObjGraduity = new Graduity_BAl();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                DropdownBind();
                LoadData();
            }
        }
    }
    protected void LoadData()
    {
         vt_Common.Bind_GridView(GridEmployee, ObjGraduity.sp_GetGraduityByID(1));
    }
    protected void DropdownBind()
    {

        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ddlEmployeeType.Items.Clear();
            var EmployeeType = (from m in db.vt_tbl_TypeofEmployee
                                select new
                                {
                                    m.Type,
                                    m.Id
                                }).ToList();

            ddlEmployeeType.DataSource = EmployeeType;
            ddlEmployeeType.DataTextField = "Type";
            ddlEmployeeType.DataValueField = "Id";
            ddlEmployeeType.DataBind();
            ddlEmployeeType.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        
         vt_Common.ReloadJS(this.Page, "ClearFields();");
        vt_Common.ReloadJS(this.Page, "$('#Modal').modal();");
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        DropdownBind();

        try
        {
            vt_Common.ReloadJS(this.Page, "$('#Modal').modal('show');");

            if (e.CommandArgument.ToString() != "")
            {
                Graduity_BAl graduity = ObjGraduity.Sp_GetGraduity(Convert.ToInt32(e.CommandArgument));
                ddlEmployeeType.SelectedValue = graduity.TypeOfEmployee.ToString();
                txtmaturedays.Text = Convert.ToString(graduity.MaturityOfDays);
                txtnodays.Text = Convert.ToString(graduity.NoOfDays);
                chkactive.Checked = graduity.IsActive;
                ViewState["PfId"] = graduity.Id.ToString();
                //ddlcomp.SelectedValue = companystaff.CompanyID.ToString();
                UpDetail.Update();
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
            ObjGraduity.Id = Convert.ToInt32(TxtIDs.Text);
            ObjGraduity.DeletedGraduityID(ObjGraduity.Id);
            vt_Common.ReloadJS(this.Page, "$('#deleteform').modal('hide')");
            vt_Common.Bind_GridView(GridEmployee, ObjGraduity.sp_GetGraduityByID(1));
            MsgBox.Show(Page, MsgBox.success, ObjGraduity.MaturityOfDays.ToString(), "Successfully Deleted");
            UpView.Update();
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }
    protected void lbtnDelete_Modalshow(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            Graduity_BAl graduity = ObjGraduity.Sp_GetGraduity(Convert.ToInt32(e.CommandArgument));
            TxtIDs.Text = graduity.Id.ToString();

            vt_Common.ReloadJS(this.Page, "$('#deleteform').modal();");
        }
    }
    protected void btnsave_TypeEmployee(object sender, EventArgs e)
    {

        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PfId"]);

                int EmployeeID = vt_Common.CheckInt(ddlEmployeeType.SelectedValue);
                var record = db.tbl_Graduity.Where(x => x.Id != recordID && x.TypeOfEmployee == EmployeeID).FirstOrDefault();
                tbl_Graduity objgraduity = new tbl_Graduity();
                objgraduity.TypeOfEmployee = Convert.ToInt32(ddlEmployeeType.SelectedValue);
                objgraduity.MaturityOfDays = Convert.ToInt32(txtmaturedays.Text);
                objgraduity.NoOfDays = Convert.ToInt32(txtnodays.Text);
                objgraduity.IsActive = chkactive.Checked;

                if (ViewState["PfId"] != null)
                {
                    objgraduity.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                    objgraduity.ModifiedOn = DateTime.Now;
                    objgraduity.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    objgraduity.CreatedOn = DateTime.Now;
                    objgraduity.Id = vt_Common.CheckInt(ViewState["PfId"]);
                    db.Entry(objgraduity).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    objgraduity.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    objgraduity.CreatedOn = DateTime.Now;
                     db.tbl_Graduity.Add(objgraduity);
                }
                //if (objgraduity.Id > 0)
                //{
                //    db.Entry(objgraduity).State = System.Data.Entity.EntityState.Modified;
                //}
                UpView.Update();
                vt_Common.ReloadJS(this.Page, "$('#Modal').modal('hide');");
                MsgBox.Show(Page, MsgBox.success, objgraduity.TypeOfEmployee.ToString(), "Record Saved Successfully.");
                db.SaveChanges();
                //  ClearForm(false);
                LoadData();
            }
        }
        catch (Exception ex)
        {
            MsgBox.Show(Page, MsgBox.danger, "Employee Not Saving due to this", ex.Message);
        }
    }
}