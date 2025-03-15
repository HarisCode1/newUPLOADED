using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class ChangePassword : System.Web.UI.Page
{
    private vt_EMSEntities dbContext = new vt_EMSEntities();
    User_BAL BAL = new User_BAL();
    Company_BAL CP = new Company_BAL();
    RolePermission_BAL RP = new RolePermission_BAL();
    PayRoll_Session goAMLSession = new PayRoll_Session();
    protected void Page_Load(object sender, EventArgs e)
    {


    }



    protected void btnClose_Click(object sender, EventArgs e)
    {
        vt_EMSEntities db = new vt_EMSEntities();
        int EmployeeID = Convert.ToInt32(Session["UserId"]);
        string encryptedOldPassword = vt_Common.Encrypt("123456@");
        vt_tbl_User user = db.vt_tbl_User.FirstOrDefault(x => x.UserId == EmployeeID && x.Password == encryptedOldPassword);
        if (user == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, user.FirstName, "First Change your Password");
        }
  

    }



    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                //EMS_Session s = new EMS_Session();


                int EmployeeID = Convert.ToInt32(Session["UserId"]);
                //int EmployeeID = ((PayRoll_Session)Session["PayRollSession"]).Employee.EmployeeID; Session["UserId"]


                string encryptedOldPassword = vt_Common.Encrypt(txtOldPassword.Text);
                //var emp = dbContext.vt_tbl_Employee.Where(w => w.EmpPassword.Equals(encryptedOldPassword)).SingleOrDefault();


                //vt_tbl_User emp = db.vt_tbl_User.FirstOrDefault(x =>
                //x.UserId == EmployeeID && x.Password == encryptedOldPassword);

                vt_tbl_User user = db.vt_tbl_User.FirstOrDefault(x => x.UserId == EmployeeID && x.Password == encryptedOldPassword);

                //if ((txtNewPassword.Text !=null) && (txtOldPassword.Text !=null) &&(txtReType.Text!=null))
                //{
                //    MsgBox.Show(this, "Passwords can not be null");
                //}

                //else
                //{
                    if (txtNewPassword.Text == "123456@")
                    {

                        MsgBox.Show(this, "New password cannot be the same as the current password");
                    }
                    else
                    {
                        if (txtNewPassword.Text.Length > 4)
                        {
                            if (txtNewPassword.Text == txtReType.Text)
                            {
                                if (user != null )
                                {

                                    var encryptedPassword = vt_Common.Encrypt(txtNewPassword.Text);
                                    var userbal = new User_BAL();
                                    userbal.UserID = EmployeeID;
                                    userbal.Active = user.Active;
                                    userbal.Password = encryptedPassword;
                                    userbal.UserName = user.UserName;
                                    userbal.FirstName = user.FirstName;
                                    userbal.LastName = user.LastName;
                                    userbal.Email = user.Email;
                                    userbal.RoleID = user.RoleId;
                                    userbal.CompanyID = user.CompanyId;
                                    userbal.UpdatedBy = EmployeeID;
                                    userbal.UpdatedOn = DateTime.Now;
                                    BAL.UpdateById(userbal);


                                    MsgBox.Show(Page, MsgBox.success, user.FirstName, "Password Changed Successfully");
                                    Response.Redirect("Default.aspx");
                                }
                                else
                                {

                                    MsgBox.Show(this, "Invalid Old password");

                                }
                            }
                            else
                            {
                                MsgBox.Show(this, "Passwords do not match!");
                            }
                        }
                        else
                        {
                            MsgBox.Show(this, "Minimum 5 characters allowed in password!");
                        }


                    }
                //}


               

                //if (emp != null)
                //{
                //    db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                //    Clear();
                //}
                //else
                //{
                //    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                //    Clear();
                //}
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //}





            }
        }


        catch (Exception ex)
        {
        }


    }


    void Clear()
    {
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtReType.Text = "";
    }
}