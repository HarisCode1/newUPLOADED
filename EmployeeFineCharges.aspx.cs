using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmployeeFineCharges : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                 ID = Convert.ToInt32(Request.QueryString["ID"]);
                int UserID = Convert.ToInt32(Session["UserId"]);
                int RoleId = Convert.ToInt32(Session["RoleId"]);
                //if (RoleId ==2)
                //{
                //    BtnAddNew.Visible = true;
                //}
                //else
                //{
                //    BtnAddNew.Visible = false;
                //}
                int CompanyID = Convert.ToInt32(Session["CompanyId"]);
                if (CompanyID != 0)
                {
                    // BindEmployeeDropDown(CompanyID);
                    if (ID !=0)
                    {
                        BindGrid(CompanyID);
                        var query = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).Select(x => new
                        {
                            x.EmployeeID,
                            x.EmployeeName
                        });
                        if (query != null)
                        {
                            //txtemployees.Text=query.EmployeeId.ToString();
                            ddlEmployee.DataSource = query.ToList();
                            ddlEmployee.DataTextField = "EmployeeName";
                            ddlEmployee.DataValueField = "EmployeeID";
                            ddlEmployee.DataBind();
                        }
                        //var checkemp = db.EmployeeFineCharges.Where(x => x.EmployeeId == ID).FirstOrDefault();
                        //if (checkemp != null)
                        //{
                        //   // BtnAddNew.Enabled = false;
                        //}
                        //else
                        //{
                        //    //BtnAddNew.Enabled = true;
                        //}
                    }
                  

                }
                
                //var query = db.EmployeeFineCharges.Where(x => x.EmployeeId == ID).FirstOrDefault();
                //if (query != null)
                //{
                //    ddlEmployee.SelectedValue = query.EmployeeId.ToString();
                //}

            }

        }
    }

    public void BindGrid(int CompanyID)
    {
        //  int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        try
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            if (ID != 0)
            {
                var query = db.vt_sp_getemployeefinechargesdetails(ID);
                grdempfinecharges.DataSource = query;
                grdempfinecharges.DataBind();
                //var checkemp = db.EmployeeFineCharges.Where(x => x.EmployeeId == ID).FirstOrDefault();
                //if (checkemp != null)
                //{
                //    BtnAddNew.Enabled = false;
                //}
                //else
                //{
                //    BtnAddNew.Enabled = true;
                //}
            }
        }
        catch (Exception)
        {

            throw;
        }
       
        


    }
    public void BindEmployeeDropDown(int CompanyID)
    {
        {
            var EmployeeList = db.VT_SP_GetEmployees(CompanyID).ToList();
            ddlEmployee.DataSource = EmployeeList;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
         //   ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(ViewState["hdnID"]);
            int CompanyID = Convert.ToInt32(Session["CompanyId"]);
            EmployeeFineCharge emp = new EmployeeFineCharge();
            if (CompanyID != 0)
            {
                if (id == 0)
                {
                    emp.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                    emp.FineCharges = Convert.ToDecimal(txtfinecharges.Text);
                    emp.Description = txtdescription.Text;
                    emp.CompanyId = CompanyID;
                    emp.CreatedDate = DateTime.Now;
                    db.EmployeeFineCharges.Add(emp);
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, "", "Successfully Done");
                
                ClearForm();
                    UpView.Update();
                    BindGrid(CompanyID);
                }
                else
                {
                    EmployeeFineCharge fnchrg = db.EmployeeFineCharges.Where(x => x.Id == id).FirstOrDefault();
                    fnchrg.Id = id;
                    fnchrg.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                    fnchrg.FineCharges = Convert.ToDecimal(txtfinecharges.Text);
                    fnchrg.Description = txtdescription.Text;
                    fnchrg.CompanyId = CompanyID;
                    fnchrg.CreatedDate = DateTime.Now;
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, "", "Successfully Done");

                    ClearForm();
                    UpView.Update();
                    BindGrid(CompanyID);

                }
           

            }
           
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();

    }
    void ClearForm()
    {
        try
        {
            txtfinecharges.Text = "";
            txtdescription.Text = "";


            vt_Common.ReloadJS(this.Page, "$('#EmpFineChargesForm').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
  


    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        //if ((string)Session["UserName"] == "SuperAdmin")
        //{

        //    vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
        //}
        //else
        //{
        //DataTable Dt = Session["PagePermissions"] as DataTable;
        //foreach (DataRow Row in Dt.Rows)
        //{
        //    if (Row["PageUrl"].ToString() == "Employee_Character.aspx" && Row["Can_Insert"].ToString() == "True")
        //    {

        vt_Common.ReloadJS(this.Page, "$('#EmpFineChargesForm').modal();");


        //}
        //else if (Row["PageUrl"].ToString() == "Employee_Character.aspx" && Row["Can_Insert"].ToString() == "False")
        //{
        //     MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
        //    }
        //    }
        // }

    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if ((string)Session["UserName"] == "SuperAdmin")
            {

                if (e.CommandArgument.ToString() != "")
                {

                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    EmployeeFineCharge fine = db.EmployeeFineCharges.Where(x => x.Id.Equals(id)).FirstOrDefault();
                    ViewState["hdnID"] = fine.Id;
                    ddlEmployee.Enabled = false;
                    ddlEmployee.SelectedValue = fine.EmployeeId.ToString();
                    txtfinecharges.Text = fine.FineCharges.ToString();
                    txtdescription.Text = fine.Description.ToString();
                    vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
                    Update.Update();
                }
                vt_Common.ReloadJS(this.Page, "$('#EmpFineChargesForm').modal();");
            }
            else
            {
                if (e.CommandArgument.ToString() != "")
                {

                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    EmployeeFineCharge fine = db.EmployeeFineCharges.Where(x => x.Id.Equals(id)).FirstOrDefault();
                    ViewState["hdnID"] = fine.Id;
                    ddlEmployee.Enabled = false;
                    ddlEmployee.SelectedValue = fine.EmployeeId.ToString();
                    txtfinecharges.Text = fine.FineCharges.ToString();
                    txtdescription.Text = fine.Description.ToString();
                    vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
                    Update.Update();
                }
                vt_Common.ReloadJS(this.Page, "$('#EmpFineChargesForm').modal();");
                //DataTable Dt = Session["PagePermissions"] as DataTable;
                //foreach (DataRow Row in Dt.Rows)
                //{
                //    if (Row["PageUrl"].ToString() == "EmployeeFineCharges.aspx" && Row["Can_Update"].ToString() == "True")
                //    {
                //        if (e.CommandArgument.ToString() != "")
                //        {

                //            int id = Convert.ToInt32(e.CommandArgument.ToString());
                //            EmployeeFineCharge fine = db.EmployeeFineCharges.Where(x => x.Id.Equals(id)).FirstOrDefault();
                //            ViewState["hdnID"] = fine.Id;
                //            ddlEmployee.Enabled = false;
                //            ddlEmployee.SelectedValue = fine.EmployeeId.ToString();
                //            txtfinecharges.Text = fine.FineCharges.ToString();
                //            txtdescription.Text = fine.Description.ToString();                            
                //            vt_Common.ReloadJS(this.Page, "$('#EmpFineChargesForm').modal();");
                //            Update.Update();

                //        }
                //    }
                //    else if (Row["PageUrl"].ToString() == "EmployeeFineCharges.aspx" && Row["Can_Update"].ToString() == "False")
                //    {
                //        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                //    }
                //}
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                ViewState["hdnAstID"] = Convert.ToInt32(e.CommandArgument);


                vt_Common.ReloadJS(this.Page, "$('#Delete').modal();");

            }
        }
        catch (Exception)
        {

            throw;
        }


    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {

            int id = Convert.ToInt32(ViewState["hdnAstID"]);
            int CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (CompanyID != 0)
            {
                if (id != 0)
                {
                    EmployeeFineCharge fnchr = db.EmployeeFineCharges.Find(id);
                    db.EmployeeFineCharges.Remove(fnchr);
                    db.SaveChanges();
                    vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
                    MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");

                    BindGrid(CompanyID);
                    UpView.Update();

                }

                else
                {
                    MsgBox.Show(Page, MsgBox.danger, "Message ", "Please select company from dashboard!");

                }
            }

        }
        catch (Exception)
        {

            throw;
        }

    }
}