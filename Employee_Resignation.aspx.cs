using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Viftech;
using System.Data;
using System.IO;

public partial class Default2 : System.Web.UI.Page
{
    vt_EMSEntities dbContext = new vt_EMSEntities();

    int UserId;
    vt_tbl_Resignations user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            int Companyid = Convert.ToInt32(Session["CompanyId"]);
             if (!IsPostBack)
            {
                BindEmployeeGrid();
                BindResignGrid();
                user = dbContext.vt_tbl_Resignations.Where(w => w.EmployeeId.Equals(UserId)).Where(w => w.Status != "Revert").SingleOrDefault();
                if (user != null)
                {

                    //RevertSection.Visible = true;
                    //if (RevertSection.Visible ==true)
                    //{
                    //    ddlEmployee.Visible = false;
                    //    //ddlEmployee.SelectedValue = user.EmployeeId.ToString();  

                    //}
                    ApplySection.Visible = true;
                    //lblAppliedDate.Text = user.AppliedDate.ToShortDateString();
                    //lblReason.Text = user.Reason;
                    //lblRemarks.Text = user.Remarks;
                    //lblstatus.Text = user.Status;
                    //if (lblstatus.Text == "Approved" ||  lblstatus.Text == "Resigned")
                    //{
                    //    btnRevert.Visible = true;
                    //}
                    //else
                    //{
                    //    btnRevert.Visible = true;
                    //}
                }
                else
                {
                    ApplySection.Visible = true;
                   // RevertSection.Visible = false;
                }
            }
        }
    }
    public void BindResignGrid()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        DataTable dt = ProcedureCall.GetSp_GetResignedEmployeeByCompanyID(Companyid).Tables[0];
        if (dt.Rows.Count > 0)
        {
            grdresignation.DataSource = dt;
            grdresignation.DataBind();
        }

          

        //var queryapvd = (from r in db.vt_tbl_Resignations
        //                 join e in db.vt_tbl_Employee on r.EmployeeId equals e.EmployeeID
        //                 where r.CompanyId == Companyid && r.Status == "Approved"
        //                 select new
        //                 {
        //                     r.ResignationId,
        //                     r.EmployeeId,
        //                     e.EmployeeName,
        //                     r.Reason,
        //                     r.Remarks,
        //                     r.Status,
        //                     r.AppliedDate,
        //                     r.EOSDate
        //                 });
        //var queryrevert = (from r in db.vt_tbl_Resignations
        //                   join e in db.vt_tbl_Employee on r.EmployeeId equals e.EmployeeID
        //                   where r.CompanyId == Companyid && r.Status == "Revert"
        //                   select new
        //                   {
        //                       r.ResignationId,
        //                       r.EmployeeId,
        //                       e.EmployeeName,
        //                       r.Reason,
        //                       r.Remarks,
        //                       r.Status,
        //                       r.AppliedDate
        //                   });

        //grdapvdresg.DataSource = queryapvd.ToList();
        //grdapvdresg.DataBind();
        //grdrevertresignation.DataSource = queryrevert.ToList();
        //grdrevertresignation.DataBind();
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmployee.SelectedIndex ==0)
            {
                MsgBox.Show(Page, MsgBox.danger, "", "Please Select Employee!");
            }
            else
            {
                int EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                if (EmployeeId > 0)
                {
                    var query = dbContext.vt_tbl_Resignations.Where(x => x.EmployeeId == EmployeeId).FirstOrDefault();
                    if (query == null)
                    {
                        if ((string)Session["UserName"] == "SuperAdmin")
                        {

                            MsgBox.Show(Page, MsgBox.danger, "", "Sorry You can not Apply!");
                        }
                        else
                        {
                            DataTable Dt = Session["PagePermissions"] as DataTable;
                            if (UploadDocImage.HasFile)
                            {
                                string Extenion = Path.GetExtension(UploadDocImage.PostedFile.FileName).ToString().ToLower();
                                //if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                                //    Extenion.ToLower() == ".bmp")
                                //{
                                //    ViewState["ImageName"] = UploadDocImage.PostedFile.FileName;
                                //string imagename = ViewState["ImageName"].ToString();
                                //  string ext = Extenion.Substring(1);
                                UploadDocImage.SaveAs(Server.MapPath("~/images/ReceivingLetter/" + UserId + "-" + UploadDocImage.PostedFile.FileName));
                                //empImageView.ImageUrl = "~/images/Employees/" + ViewState["ImageName"].ToString();
                                lblmsg.Text = "";
                                //}
                                //else
                                //{
                                //    lblmsg.Text = Extenion + " Not Supported";
                                //}
                            }
                            foreach (DataRow Row in Dt.Rows)
                            {

                                if (Row["PageUrl"].ToString() == "Employee_Resignation.aspx" && Row["Can_Insert"].ToString() == "True")
                                {

                                    if (this.Insert())
                                    {
                                        MsgBox.Show(this, 1, "Successfully ", "Resignation Submitted!");
                                        vt_Common.Clear(ApplySection.Controls);
                                        user = dbContext.vt_tbl_Resignations.Where(w => w.EmployeeId.Equals(UserId)).SingleOrDefault();
                                        // RevertSection.Visible = true;
                                        ApplySection.Visible = true;
                                        //lblReason.Text = user.Reason;
                                        //lblRemarks.Text = user.Remarks;

                                        //lblAppliedDate.Text = user.AppliedDate.ToShortDateString();
                                        //lblstatus.Text = user.Status;
                                    }
                                    else
                                    {
                                        user = dbContext.vt_tbl_Resignations.Where(w => w.EmployeeId.Equals(UserId)).SingleOrDefault();
                                        MsgBox.Show(this, 1, "", "You Already applied for resignation.");
                                        // RevertSection.Visible = true;
                                        ApplySection.Visible = false;
                                        //lblReason.Text = user.Reason;
                                        //lblRemarks.Text = user.Remarks;
                                        //lblAppliedDate.Text = user.AppliedDate.ToShortDateString();
                                        //lblstatus.Text = user.Status;
                                    }

                                }
                                else if (Row["PageUrl"].ToString() == "Employee_Resignation.aspx" && Row["Can_Insert"].ToString() == "False")
                                {
                                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                                }
                            }
                        }

                    }
                    else
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "Already Resigned!");

                    }


                }
            }
         

        }
        catch (Exception)
        {

            throw;
        }
       
            
            //faraz code
        
    }
    public void BindEmployeeGrid()
    {
        int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var EmployeeList = db.VT_SP_GetEmployees(CompanyID).ToList();
            ddlEmployee.DataSource = EmployeeList;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
        }
    }
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool Insert()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        int EmployeeID =ddlEmployee.SelectedValue == null?0: Convert.ToInt32(ddlEmployee.SelectedValue);
        if (EmployeeID > 0)
        {
            user = dbContext.vt_tbl_Resignations.Where(w => w.EmployeeId == EmployeeID && w.CompanyId == Companyid).FirstOrDefault();
        }
       
        if (user != null) {
            string fullPath = Request.MapPath(user.Image);
            
            //  string file_name = DropDownList1.SelectedItem.Text;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                Label1.Text = "File succefully deleted";
            }
            else
            {
                Label1.Text = "Failed";
            }
            dbContext.vt_tbl_Resignations.Remove(user);
            //  File.Delete(Server.MapPath(user.Image));    
            dbContext.SaveChanges();                                                
        }
         if (user == null)
        {
            //string fullPath = Request.MapPath(user.Image);

            ////  string file_name = DropDownList1.SelectedItem.Text;
            //if (System.IO.File.Exists(fullPath))
            //{
            //    System.IO.File.Delete(fullPath);
            //    Label1.Text = "File succefully deleted";
            //}
            //else
            //{
            //    Label1.Text = "Failed";
            //}
            vt_tbl_Resignations Obj = new vt_tbl_Resignations();
            Obj.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue); //this.UserId;
            Obj.AppliedDate = DateTime.Now;
            Obj.EOSDate = null;
            Obj.Reason = txtReason.Text;
            Obj.Remarks = txtRemarks.Text;
            //  Obj.Status = "Applied";
            Obj.Status = "Approved";
            Obj.CompanyId = Companyid;
            if (UploadDocImage.HasFile)
            {
                Obj.Image= "/images/ReceivingLetter/" + UserId +"-"+ UploadDocImage.PostedFile.FileName;
            }
            else
            {
                Obj.Image = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
            }
            dbContext.Entry(Obj).State = System.Data.Entity.EntityState.Added;
            dbContext.SaveChanges();
            int empid = Convert.ToInt32(ddlEmployee.SelectedValue);
            bool isresigned = true;
            vt_tbl_Employee emp = dbContext.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(empid)).FirstOrDefault();
            emp.IsResigned = isresigned;
            dbContext.SaveChanges();
            ddlEmployee.Items.Clear();
            BindResignGrid();
            UpdateResign.Update();
         
            //int EmpId = Obj.EmployeeId;
            //vt_tbl_Employee emp = dbContext.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(EmpId)).FirstOrDefault();

            //bool isresigned = true;
            //emp.IsResigned = isresigned;
            //dbContext.SaveChanges();
            return true;
        }
        else
        {
          
            vt_tbl_Resignations Obj = new vt_tbl_Resignations();
            Obj.EmployeeId = this.UserId;
            Obj.AppliedDate = DateTime.Now;
            Obj.EOSDate = null;
            Obj.Reason = txtReason.Text;
            Obj.Remarks = txtRemarks.Text;
            Obj.Status = "Applied";
            Obj.CompanyId = Companyid;
            if (UploadDocImage.HasFile)
            {
                Obj.Image = "/images/ReceivingLetter/" + UserId + "-" + UploadDocImage.PostedFile.FileName;
            }
            else
            {
                Obj.Image = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
            }
            dbContext.Entry(Obj).State = System.Data.Entity.EntityState.Added;
            dbContext.SaveChanges();
            return true;
        }
        return false;
    }

    protected void btnRevert_Click(object sender, EventArgs e)
    {
        if (this.Update("Revert"))
        {
            MsgBox.Show(this, 1, "Database Response: ", "Resignation Revert!");
            ApplySection.Visible = true;
          //  RevertSection.Visible = false;
        }

    }

    public bool Update(string status)
    {
        vt_tbl_Resignations Obj = dbContext.vt_tbl_Resignations.Where(x => x.EmployeeId.Equals(UserId)).SingleOrDefault();
        if (Obj != null)
        {
            Obj.Status = status;
            dbContext.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }
        return true;
    }
    public bool Update(string status, string reason, string remarks)
    {
        vt_tbl_Resignations Obj = dbContext.vt_tbl_Resignations.Where(x => x.EmployeeId.Equals(UserId)).SingleOrDefault();
        if (Obj != null)
        {
            Obj.Reason = reason;
            Obj.Remarks = remarks;
            Obj.Status = status;
            dbContext.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }
        return true;
    }

}