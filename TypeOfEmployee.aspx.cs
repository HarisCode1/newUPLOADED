using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;
using System.Data;
using System.IO;
public partial class TypeOfEmployee : System.Web.UI.Page
{
    string Pagename = string.Empty;
    TypeOfEmployee_BAL TypeEmp = new TypeOfEmployee_BAL();
    protected void LaodData()
    {
        vt_Common.Bind_GridView(GridEmployee, TypeEmp.GetEmployeeListById(1));
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        LaodData();
//        DropdownBind();

    }
    //protected void DropdownBind()
    //{

    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        ddType.Items.Clear();
    //        var Typedropdown = (from m in db.vt_tbl_Role
    //                               select new
    //                               {
    //                                   m.RoleID,
    //                                   m.Role
    //                               }).ToList();

    //        ddType.DataSource = Typedropdown;
    //        ddType.DataTextField = "RoleID";
    //        ddType.DataValueField = "Role";
    //        ddType.DataBind();


    //    }
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        ddCompanyID.Items.Clear();
    //        var CompanyDropdown = (from m in db.vt_tbl_Company
    //                               select new
    //                            {
    //                                m.CompanyID,
    //                                m.CompanyName,
    //                            }).ToList();

    //        ddCompanyID.DataSource = CompanyDropdown;
    //        ddCompanyID.DataTextField = "CompanyID";
    //        ddCompanyID.DataValueField = "CompanyName";
    //        ddCompanyID.DataBind();

    //    }

    //    //CP.GetCompanyID(1099);

    //}
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        vt_Common.ReloadJS(this.Page, "ClearFields();");
        vt_Common.ReloadJS(this.Page, "$('#Modal').modal();");
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            btnsaves.Text = "Update";

            //vt_Common.ReloadJS(this.Page, "$('#Modal').modal();");
            //btnSaves.Text = "Update";
            if (e.CommandArgument.ToString() != "")
            {
                TypeOfEmployee_BAL TP = TypeEmp.GetEmployeebyID(Convert.ToInt32(e.CommandArgument));
                TxtEmpID.Text = TP.Id.ToString();
                TxtEmployee.Text = TP.Type;
                chkActive.Checked = TP.Active;
                vt_Common.ReloadJS(this.Page, "$('#Modal').modal();");
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

            //TypeEmp.Id = TypeEmp.Id.Equals("") ? 0 : Convert.ToInt32(TypeEmp.Id);
            // TypeEmp.Id = Convert.ToInt32(TxtEmpID.Text);
            //  TxtEmpID.Text = TypeEmp.Id.ToString();
            TypeEmp.Id = Convert.ToInt32(TxtEmpID.Text);
            TypeEmp.DeleteEmployeeType(TypeEmp.Id);
            //TypeEmp.Type = TypeEmp.Id.ToString();

            //chkActive.Checked = Convert.ToBoolean(TypeEmp.Active);
            vt_Common.ReloadJS(this.Page, "$('#deleteform').modal('hide')");
            LaodData();
            UpView.Update();
            vt_Common.Bind_GridView(GridEmployee, TypeEmp.GetEmployeeListById(1));
            MsgBox.Show(Page, MsgBox.success, TypeEmp.Type, "Successfully Deleted");

          
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }
    protected void btnsave_TypeEmployee(object sender, EventArgs e)
    {
        try
        {

            if (btnsaves.Text == "Save")
            {
                if (TypeEmp.Id == 0)
                {
                    btnsaves.Visible = true;
                    SaveEmployeType();
                    LaodData();
                    UpView.Update();
                }
            }
            if (TypeEmp.Id <= 0)
            {
                UpdateEmployeeType();
                LaodData();
                UpView.Update();

            }


        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
        }
    }
    protected void SaveEmployeType()
    {
        try
        {

            // TypeEmp.CompanyId = Convert.ToInt32(ComapnyId.Text);
            // TypeEmp.Id = Convert.ToInt32(TxtEmpID); 
            TypeEmp.Type = TxtEmployee.Text;
            TypeEmp.Active = chkActive.Checked;
            TypeEmp.Deleted = DateTime.Now;
            TypeEmp.UpdatedOn = DateTime.Now;

            string Message = TypeEmp.insertUpdateTypeOfEmp(TypeEmp);
            if (Message == "Record already exists")
            {
                MsgBox.Show(Page, MsgBox.danger, TypeEmp.Type, "already exists");
            }
            else
            {
                vt_Common.ReloadJS(this.Page, "$('#Modal').modal('hide')");
                //LaodData();
                MsgBox.Show(Page, MsgBox.success, TypeEmp.Type, "Successfully Saved");
                vt_Common.Clear(pnlDetail.Controls);
            }




        }
        catch (Exception ex)
        {
        }
    }
    private void UpdateEmployeeType()
    {

        try
        {
            // TypeEmp.Id = TypeEmp.Id.Equals("") ? 0 : Convert.ToInt32(TypeEmp.Id);
            //TypeEmp.Id = Convert.ToInt32(TypeEmp.Id);
            TypeEmp.Id = Convert.ToInt32(TxtEmpID.Text);
            TypeEmp.Type = TxtEmployee.Text;
            TypeEmp.Active = chkActive.Checked;
            TypeEmp.Deleted = DateTime.Now;
            TypeEmp.UpdatedOn = DateTime.Now;



            string Message = TypeEmp.insertUpdateTypeOfEmp(TypeEmp);
            if (Message == "Record Updated")
            {
                MsgBox.Show(Page, MsgBox.success, TypeEmp.Type, "Record Updated");
                vt_Common.ReloadJS(this.Page, "$('#Modal').modal('hide')");

                // MsgBox.Show(Page, MsgBox.success, TypeEmp.Type, "Successfully Saved");
                vt_Common.Clear(pnlDetail.Controls);
            }
            else
            {
                //vt_Common.ReloadJS(this.Page, "$('#Modal').modal('hide')");
                //LaodData();
                // MsgBox.Show(Page, MsgBox.success, TypeEmp.Type, "Successfully Saved");
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void lbtnDelete_Modalshow(object sender, CommandEventArgs e)
    {

        if (e.CommandArgument.ToString() != "")
        {
            TypeOfEmployee_BAL TP = TypeEmp.GetEmployeebyID(Convert.ToInt32(e.CommandArgument));
            TxtEmpID.Text = TP.Id.ToString();
            LaodData();
            vt_Common.ReloadJS(this.Page, "$('#deleteform').modal();");
        }
    }

    protected void ModalHide(object sender, CommandEventArgs e)
    {
        vt_Common.ReloadJS(this.Page, "$('#deleteform').modal('hide')");

    }
    protected void ModalShow()
    {
        vt_Common.ReloadJS(this.Page, "$('#deleteform').modal()");
    }
}

