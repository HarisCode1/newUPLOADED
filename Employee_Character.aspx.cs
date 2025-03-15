using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employee_Character : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Authenticate.Confirm())
        {
            if (!Page.IsPostBack)
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
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
               // BindEmployeeDropDown(CompanyID);
                BindGrid(CompanyID);
                var query = db.vt_tbl_Employee.Where(x => x.EmployeeID== ID).Select(x => new
                {
                    x.EmployeeID,
                    x.EmployeeName
                });
                if (query !=null)
                {
                    //txtemployees.Text=query.EmployeeId.ToString();
                    ddlEmployee.DataSource = query.ToList();
                    ddlEmployee.DataTextField = "EmployeeName";
                          ddlEmployee.DataValueField = "EmployeeID";
                    ddlEmployee.DataBind();
                }
                var checkrecordexist = db.EmployeeCharacters.Where(x => x.EmployeeId == ID).FirstOrDefault();
                if (checkrecordexist != null)
                {
                    BtnAddNew.Enabled = false;
                }
                else
                {
                    BtnAddNew.Enabled = true;
                }

            }
        }
    }
    //public void BindEmployeeDropDown(int CompanyID)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        var EmployeeList = db.VT_SP_GetEmployees(CompanyID).ToList();
    //        ddlEmployee.DataSource = EmployeeList;
    //        ddlEmployee.DataTextField = "EmployeeName";
    //        ddlEmployee.DataValueField = "EmployeeID";
    //        ddlEmployee.DataBind();
    //      //  ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
    //    }
    //}
    public void BindGrid(int CompanyID)
    {
        //  int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        int ID = Convert.ToInt32(Request.QueryString["ID"]);
        if (ID !=0)
        {
            var query = (from emp in db.vt_tbl_Employee
                         join empc in db.EmployeeCharacters on emp.EmployeeID equals empc.EmployeeId
                         where empc.CompanyId == CompanyID && empc.EmployeeId == ID
                         select new
                         {
                             empc.Id,
                             emp.FirstName,
                             emp.LastName,

                             emp.EmployeeName,
                             empc.BehaviorwithColleagues,
                             empc.BehaviorwithCustomers,
                             empc.Honesty,
                             empc.TimeReporting,
                             empc.CreatedDate
                         });
            grdempcharacter.DataSource = query.ToList();
            grdempcharacter.DataBind();
            var checkrecordexist = db.EmployeeCharacters.Where(x => x.EmployeeId == ID).FirstOrDefault();
            if (checkrecordexist != null)
            {
                BtnAddNew.Enabled = false;
            }
            else
            {
                BtnAddNew.Enabled = true;
            }
        }



    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {

        if ((string)Session["UserName"] == "SuperAdmin")
        {

            if (e.CommandArgument.ToString() != "")
            {

                int id = Convert.ToInt32(e.CommandArgument.ToString());
                EmployeeCharacter character = db.EmployeeCharacters.Where(x => x.Id.Equals(id)).FirstOrDefault();
                ViewState["hdnID"] = character.Id;
                txtBxColleagues.Text = character.BehaviorwithColleagues.ToString();
                txtBxCustomers.Text = character.BehaviorwithCustomers.ToString();
                txthonesty.Text = character.Honesty.ToString();
                txttimereporting.Text = character.TimeReporting.ToString();
                vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
                Update.Update();
            }
            vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
        }
        else
        {
            if (e.CommandArgument.ToString() != "")
            {

                int id = Convert.ToInt32(e.CommandArgument.ToString());
                EmployeeCharacter character = db.EmployeeCharacters.Where(x => x.Id.Equals(id)).FirstOrDefault();
                ViewState["hdnID"] = character.Id;
                //ddlEmployee.Enabled = false;
                //ddlEmployee.SelectedValue = character.EmployeeId.ToString();
                txtBxColleagues.Text = character.BehaviorwithColleagues.ToString();
                txtBxCustomers.Text = character.BehaviorwithCustomers.ToString();
                txthonesty.Text = character.Honesty.ToString();
                txttimereporting.Text = character.TimeReporting.ToString();
                vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
                Update.Update();

            }
            //DataTable Dt = Session["PagePermissions"] as DataTable;
            //foreach (DataRow Row in Dt.Rows)
            //{
            //    if (Row["PageUrl"].ToString() == "Employee_Character.aspx" && Row["Can_Update"].ToString() == "True")
            //    {
            //        if (e.CommandArgument.ToString() != "")
            //        {

            //            int id = Convert.ToInt32(e.CommandArgument.ToString());
            //            EmployeeCharacter character= db.EmployeeCharacters.Where(x => x.Id.Equals(id)).FirstOrDefault();
            //            ViewState["hdnID"] = character.Id;
            //            ddlEmployee.Enabled = false;
            //            ddlEmployee.SelectedValue =character.EmployeeId.ToString();
            //            txtBxColleagues.Text = character.BehaviorwithColleagues.ToString();
            //            txtBxCustomers.Text = character.BehaviorwithCustomers.ToString();
            //            txthonesty.Text = character.Honesty.ToString();
            //            txttimereporting.Text = character.TimeReporting.ToString();
            //            vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
            //            Update.Update();

            //        }
            //    }
            //    else if (Row["PageUrl"].ToString() == "Employee_Character.aspx" && Row["Can_Update"].ToString() == "False")
            //    {
            //        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            //    }
            //}
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
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int EmpID = Convert.ToInt32(Request.QueryString["ID"]);
            int id = Convert.ToInt32(ViewState["hdnID"]);
            int CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (CompanyID != 0)
            {
                if (id == 0)
                {
                    EmployeeCharacter emp = new EmployeeCharacter();

                    emp.EmployeeId = EmpID;//Convert.ToInt32(ddlEmployee.SelectedValue);
                    emp.BehaviorwithColleagues = Convert.ToDecimal(txtBxColleagues.Text);
                    emp.BehaviorwithCustomers = Convert.ToDecimal(txtBxCustomers.Text);
                    emp.Honesty = Convert.ToDecimal(txthonesty.Text);
                    emp.TimeReporting = Convert.ToDecimal(txttimereporting.Text);
                    emp.CreatedDate = DateTime.Now;
                    emp.CompanyId = CompanyID;
                    db.EmployeeCharacters.Add(emp);
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Updated");
                    ClearForm();
                      UpView.Update();
                    BindGrid(CompanyID);


                }
                else
                {
                    EmployeeCharacter empupdate = db.EmployeeCharacters.Where(x => x.Id == id).FirstOrDefault();
                    empupdate.Id = id;
                    empupdate.EmployeeId = EmpID;// Convert.ToInt32(ddlEmployee.SelectedValue);
                    empupdate.BehaviorwithColleagues = Convert.ToDecimal(txtBxColleagues.Text);
                    empupdate.BehaviorwithCustomers = Convert.ToDecimal(txtBxCustomers.Text);
                    empupdate.Honesty = Convert.ToDecimal(txthonesty.Text);
                    empupdate.TimeReporting = Convert.ToDecimal(txttimereporting.Text);
                    empupdate.CreatedDate = DateTime.Now;
                    empupdate.CompanyId = CompanyID;
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Updated");
                    ClearForm();
                    UpView.Update();
                    BindGrid(CompanyID);
                }
            

            }
            else
            {
                MsgBox.Show(Page, MsgBox.success, "Message ", "Please select Company from dashboard");
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
            txtBxColleagues.Text = "";
            txtBxCustomers.Text = "";
            txthonesty.Text = "";
            txttimereporting.Text = "";
            
            vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int CompanyID = Convert.ToInt32(Session["CompanyId"]);
            if (CompanyID != 0)
            {
                int id = Convert.ToInt32(ViewState["hdnAstID"]);
                EmployeeCharacter deletecharacter = db.EmployeeCharacters.Find(id);
                db.EmployeeCharacters.Remove(deletecharacter);
                db.SaveChanges();
                vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
                MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");

                BindGrid(CompanyID);
                UpView.Update();

            }
        }
        catch (Exception)
        {

            throw;
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
      
            vt_Common.ReloadJS(this.Page, "$('#EmpcharacterForm').modal();");
       
                    
                //}
                //else if (Row["PageUrl"].ToString() == "Employee_Character.aspx" && Row["Can_Insert"].ToString() == "False")
                //{
                //     MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                //    }
                //    }
               // }


    }
}