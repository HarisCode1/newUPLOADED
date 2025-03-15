using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employes_Transfer : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    Custommethods custom = new Custommethods();
    DateTime EntryDate = DateTime.Now;
    public static int Companyid = 0;
    public static int EmployeeID = 0;
    public static int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            Companyid = Convert.ToInt32(Session["CompanyId"]);
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            txtempcode.ReadOnly = true;
            if (!Page.IsPostBack)
            {
                FillDetailForm(ID);
                txtEntryDate.Text = EntryDate.ToString("dd/MM/yyyy");
            }
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static object Load()
    {
       
        string List = "";
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (Companyid == 0)
                {
                }
                else
                {
                    int year = DateTime.Now.Year;
                
                    if (Companyid != 0 && ID != 0)
                    {
                        DataSet Ds = ProcedureCall.Sp_Call_SetEmployeeTranferRecords(ID);
                        DataTable dt = Ds.Tables[0];
                        var Query = dt;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //Query.Columns.Add("S.no");
                            //for (int i = 0; i < Query.Rows.Count; i++)
                            //{
                            //    Query.Rows[i]["S.no"] = i + 1;
                            //}
                            List = JsonConvert.SerializeObject(Query);
                        }
                        else
                        {
                            List = "Empty1";
                        }
                    }
                    else
                    {
                        List = "Empty";
                    }


                }

            

            }


        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        return List;
    }


    //public DateTime? GetDateFromTextBox(string txtdate)
    //{
    //    string[] formats = { "dd/MM/yyyy", "dd/MM", "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy" , "MM-dd-yyyy" };

    //    DateTime dobDT;

    //    if (!string.IsNullOrWhiteSpace(txtdate) && DateTime.TryParseExact(txtdate, formats,
    //            System.Globalization.CultureInfo.InvariantCulture,
    //            System.Globalization.DateTimeStyles.None,
    //            out dobDT))
    //    {
    //        //11:34 PM
    //        string dobtime = DateTime.UtcNow.AddHours(5).ToString("hh:mm tt");
    //        DateTime timePart = DateTime.ParseExact(dobtime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
    //        DateTime combinedDateTime = dobDT.Date.Add(timePart.TimeOfDay);

    //        return combinedDateTime;

    //        // return dobDT;

    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
    void FillDetailForm(int ID)
    {
        vt_tbl_Employee Emp = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (Emp != null)
        {
            ViewState["ManagerID"] = Emp.ManagerID; ;
            TxtFirstName.Text = Emp.FirstName;
            TxtLastName.Text = Emp.LastName;
            TxtEmail.Text = Emp.Email;
            txtempcode.Text = Emp.EnrollId;
            // DdlComapny1.SelectedValue = Emp.CompanyID.ToString();
            Bind_Department(Convert.ToInt32(Emp.CompanyID));
            DdlDepartment1.SelectedValue = Emp.DepartmentID.ToString();
            Bind_Designation(Convert.ToInt32(Emp.DepartmentID));
            DdlDesignation1.SelectedValue = Emp.DesignationID.ToString();
            //BindLineManager(Convert.ToInt32(Emp.DesignationID));
            //Bind_LineManager(Convert.ToInt32(Emp.DesignationID));
            //DdlLineManager1.SelectedValue = Emp.ManagerID.ToString();
            int DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue.ToString());
            var query = db.vt_tbl_Designation.Where(x => x.DesignationID == DesignationID).FirstOrDefault();
            if (query != null)
            {
                var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query.ReportTo).ToList();
                if (qry.Count > 0)
                {
                    DdlLineManager1.DataSource = qry;
                    DdlLineManager1.DataTextField = "Topdesignations";
                    DdlLineManager1.DataValueField = "Id";
                    DdlLineManager1.DataBind();
                    DdlLineManager1.Visible = true;
                }
                else
                {
                    Bind_LineManager(DesignationID);
                }
            }
            Bind_Type();
            DdlType1.SelectedValue = Emp.Type;
            DateTime joiningDate = (DateTime)Emp.JoiningDate;
            HiddenField1.Value = joiningDate.ToString("yyyy-MM-dd");
        
        }

    }
  
    void Bind_Department(int CompanyID)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@CompanyID", CompanyID)
        };
        vt_Common.Bind_DropDown(DdlDepartment1, "VT_SP_BindDepart", "Department", "DepartmentID", param);
    }
    void Bind_Designation(int DepartmentID)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@DepartmentID", DepartmentID)
        };
        vt_Common.Bind_DropDown(DdlDesignation1, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);
    }
    public void BindLineManager(int DesignationID)
    {

        int Des = 0;
        int Dept = 0;
        Des = (Convert.ToInt32(DdlDesignation1.SelectedValue));
        Dept = (Convert.ToInt32(DdlDepartment1.SelectedValue));
        int CompanyId = Convert.ToInt32(Session["CompanyId"]);
        //int CompanyId = Convert.ToInt32(DdlComapny1.SelectedValue) == 0 ?0: Convert.ToInt32(DdlComapny1.SelectedValue);
        SqlParameter[] param =
        {
            new SqlParameter("@DesignationID",Des),
            new SqlParameter("@DepartmentID",Dept),
            new SqlParameter("@CompanyID",CompanyId)

        };
        var query = db.vt_tbl_Designation.Where(x => x.DesignationID.Equals(Des) && x.TopDesignationID == 0).FirstOrDefault();
        int id = 0;
        if (query != null)
        {
            id = Des;
        }
        vt_Common.Bind_DropDown(DdlLineManager1, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
        if (DdlLineManager1.Items.Count == 1 && Des == id)
        {
            int RoleID = Convert.ToInt32(Session["RoleId"]);
            int ComID = Convert.ToInt32(Session["CompanyId"]);
            SqlParameter[] param1 =
            {
                new SqlParameter("@RoleId",RoleID),
                new SqlParameter("@CompanyId",ComID)
            };
            vt_Common.Bind_DropDown(DdlLineManager1, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
        }
        else if (DdlLineManager1.Items.Count == 1 && Des > 1)
        {
            vt_Common.Bind_DropDown(DdlLineManager1, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
        }

        //Previous One


    }
    void Bind_LineManager(int DesignationID)
    {
        int ManagerID = Convert.ToInt32(ViewState["ManagerID"]);
        int RoleID = Convert.ToInt32(Session["RoleId"]);
        int ComID = Convert.ToInt32(Session["CompanyId"]);
        var query = db.vt_tbl_User.Where(x => x.UserId == ManagerID).ToList();
        if (query.Count > 0)
        {
            SqlParameter[] param1 =
            {
            new SqlParameter("@RoleId",RoleID),
            //new SqlParameter("@CompanyId",DdlComapny1.SelectedValue)
             new SqlParameter("@CompanyId",ComID)
        };
            vt_Common.Bind_DropDown(DdlLineManager1, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
        }
        else
        {
            int Des = 0;
            int Dept = 0;
            Des = (Convert.ToInt32(DdlDesignation1.SelectedValue));
            Dept = (Convert.ToInt32(DdlDepartment1.SelectedValue));
            int CompanyId = (Convert.ToInt32(Session["CompanyId"]));
            SqlParameter[] param =
            {
            new SqlParameter("@DesignationID",Des),
            new SqlParameter("@DepartmentID",Dept),
            new SqlParameter("@CompanyID",CompanyId)


        };
            //vt_Common.Bind_DropDown(DdlLineManager1, "VT_sp_BindLineManager", "EmployeeName", "EmployeeID", param);
            vt_Common.Bind_DropDown(DdlLineManager1, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
        }
    }
    void Bind_Type()
    {
        DdlType1.Items.Clear();
        var Typedropdown = (from m in db.vt_tbl_TypeofEmployee
                            select new
                            {
                                m.Type,
                                m.Id
                            }).ToList();
        DdlType1.DataSource = Typedropdown;
        DdlType1.DataTextField = "Type";
        DdlType1.DataValueField = "Id";
        DdlType1.DataBind();
        DdlType1.Items.Insert(0, new ListItem("Please Select", "0"));
    }
    protected void DdlComapny1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int CompanyID = Convert.ToInt32(DdlComapny1.SelectedValue.ToString());
        int CompanyID = Convert.ToInt32(Session["CompanyId"]);

        Bind_Department(CompanyID);
        DdlDesignation1.SelectedIndex = 0;
        Bind_Designation(0);
        DdlLineManager1.SelectedIndex = 0;
        BindLineManager(0);
        ScriptManager.RegisterStartupScript(this, GetType(), "InitializeDatepicker", "$(document).ready(function () { $('.datepicker').datepicker({format: 'dd-mm-yyyy', autoclose: true, todayHighlight: true}); });", true);

    }
    protected void DdlDepartment1_SelectedIndexChanged(object sender, EventArgs e)
    {



        int DepartmentID = Convert.ToInt32(DdlDepartment1.SelectedValue.ToString());
        Bind_Designation(DepartmentID);
        int DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue.ToString());
        BindLineManager(DesignationID);
        if (DesignationID != 0)
        {

            var query = db.vt_tbl_Designation.Where(x => x.DesignationID == DesignationID).FirstOrDefault();
            if (query != null)
            {
                var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query.ReportTo).ToList();
                if (qry.Count > 0)
                {
                    DdlLineManager1.DataSource = qry;
                    DdlLineManager1.DataTextField = "Topdesignations";
                    DdlLineManager1.DataValueField = "Id";
                    DdlLineManager1.DataBind();
                    DdlLineManager1.Visible = true;
                }
                else
                {
                    BindLineManager(DesignationID);
                }
            }
        }

        vt_tbl_Employee Emp = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        DateTime joiningDate = (DateTime)Emp.JoiningDate;
        HiddenField1.Value = joiningDate.ToString("yyyy-MM-dd");

        ScriptManager.RegisterStartupScript(this, GetType(), "InitializeDatepicker", "$(document).ready(function () { $('.datepicker').datepicker({format: 'dd-mm-yyyy', autoclose: true, todayHighlight: true}); });", true);

    }
    protected void DdlDesignation1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue.ToString());
        BindLineManager(DesignationID);
        if (DesignationID != 0)
        {

            var query = db.vt_tbl_Designation.Where(x => x.DesignationID == DesignationID).FirstOrDefault();
            if (query != null)
            {
                var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query.ReportTo).ToList();
                if (qry.Count > 0)
                {
                    DdlLineManager1.DataSource = qry;
                    DdlLineManager1.DataTextField = "Topdesignations";
                    DdlLineManager1.DataValueField = "Id";
                    DdlLineManager1.DataBind();
                    DdlLineManager1.Visible = true;
                }
                else
                {
                    BindLineManager(DesignationID);
                }
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "InitializeDatepicker", "$(document).ready(function () { $('.datepicker').datepicker({format: 'dd-mm-yyyy', autoclose: true, todayHighlight: true}); });", true);

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        AddPreviousInfoOfEmployee(ID);
        Update_TransferInfo(ID);


    }
    void AddPreviousInfoOfEmployee(int ID)
    {
        try
        {
            if (ID != 0)
            {
                var Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
                var check = db.vt_tbl_Employee_TransferLogRecord.Where(x => x.EmployeeID == ID).ToList();
                int companyid = Convert.ToInt32(Data.CompanyID);
                int deprtid = Convert.ToInt32(Data.DepartmentID);
                int desigid = Convert.ToInt32(DdlDesignation1.SelectedValue);
                if (Data != null)
                {
                    if (UploadDocImage.HasFile)
                    {
                        string Extenion = System.IO.Path.GetExtension(UploadDocImage.PostedFile.FileName).ToString().ToLower();

                        UploadDocImage.SaveAs(Server.MapPath("/images/TransferEmployee/" + ID + "-" + UploadDocImage.PostedFile.FileName));

                    }

                    vt_tbl_Employee_TransferLogRecord empTrans = new vt_tbl_Employee_TransferLogRecord();
                 
                    empTrans.EmployeeID = Data.EmployeeID;
                    empTrans.CompanyID = Convert.ToInt32(Data.CompanyID);
                    empTrans.DepartmentID = Convert.ToInt32(DdlDepartment1.SelectedValue);
                    empTrans.DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue);
                    empTrans.ManagerID = Convert.ToInt32(DdlLineManager1.SelectedValue);
                    empTrans.EmployeeType = DdlType1.SelectedValue;
                    empTrans.FirstName = TxtFirstName.Text;
                    empTrans.LastName = TxtLastName.Text;
                    empTrans.Email = TxtEmail.Text;

                    if (UploadDocImage.HasFile)
                    {
                        empTrans.Image = "/images/TransferEmployee/" + ID + "-" + UploadDocImage.PostedFile.FileName;
                    }
                    else
                    {
                        empTrans.Image = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
                    }
                    empTrans.Action = "PreviousRecord";

                    if (!string.IsNullOrEmpty(txtEntryDate.Text))
                    {
                        string txtDateValue = txtEntryDate.Text;
                        DateTime? resultDate = custom.GetDateFromTextBox(txtDateValue);
                        empTrans.EntryDate = resultDate;
                    }

                    db.vt_tbl_Employee_TransferLogRecord.Add(empTrans);
                    //db.vt_tbl_Employee.Add(Data);
                    db.SaveChanges();


                }
                else
                {
                    vt_tbl_Employee_TransferLogRecord empTrans = new vt_tbl_Employee_TransferLogRecord();
                    empTrans.EmployeeID = Data.EmployeeID;
                    empTrans.CompanyID = Convert.ToInt32(Data.CompanyID);
                    empTrans.DepartmentID = Convert.ToInt32(Data.DepartmentID);
                    empTrans.DesignationID = Convert.ToInt32(Data.DesignationID);
                    empTrans.ManagerID = Convert.ToInt32(Data.ManagerID);
                    empTrans.EmployeeType = Data.Type;
                    empTrans.FirstName = Data.FirstName;
                    empTrans.LastName = Data.LastName;
                    empTrans.Email = Data.Email;
                    if (UploadDocImage.HasFile)
                    {
                        empTrans.Image = "/images/TransferEmployee/" + ID + "-" + UploadDocImage.PostedFile.FileName;
                    }
                    else
                    {
                        empTrans.Image = hdImageName.Value == "" ? hdEmpPhotoID.Value : hdImageName.Value;
                    }
                    //empTrans.EnrollID = (Data.EnrollId);

                    empTrans.Action = "PreviousRecord";

                    if (!string.IsNullOrEmpty(txtEntryDate.Text))
                    {
                        string txtDateValue = txtEntryDate.Text;
                        DateTime? resultDate = custom.GetDateFromTextBox(txtDateValue);
                        empTrans.EntryDate = resultDate;
                    }



                    db.vt_tbl_Employee_TransferLogRecord.Add(empTrans);
                    db.SaveChanges();
                }
                //db.vt_tbl_Employee.Add(Data);

                ////db.vt_tbl_Employee_TransferLogRecord.Add(empTrans);
                //db.SaveChanges();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    


    void Update_TransferInfo(int ID)
    {
        int CompanyID = Convert.ToInt32(Session["CompanyId"]);
        vt_tbl_Employee_TransferLog empTrans = new vt_tbl_Employee_TransferLog();
        //var Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();

        //if (Data != null)
        //{
        //    DateTime dt = DateTime.UtcNow;
        //    Data.EmployeeID = ID;
        //    Data.CompanyID = CompanyID;
        //    Data.DepartmentID =  Convert.ToInt32(DdlDepartment1.SelectedValue);
        //    Data.DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue);
        //    Data.ManagerID =     Convert.ToInt32(DdlLineManager1.SelectedValue);
        //    Data.Type = DdlType1.SelectedValue;
        //    //Data.EnrollId = txtempcode.Text;
        //    Data.Email = TxtEmail.Text;
        //    Data.TransferDate = dt;
        //    //Convert.ToDateTime(txtEntryDate.Text).Date;
        //    // Data.TransferDepartment = Convert.ToInt32(DdlDepartment1.SelectedValue);
        //    // db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
        //    #region Code
        //    //empTrans.EmployeeID = Convert.ToInt32(Data.EmployeeID);
        //    //empTrans.CompanyID = Convert.ToInt32(DdlComapny1.SelectedValue);
        //    //empTrans.DepartmentID = Convert.ToInt32(DdlDepartment1.SelectedValue);
        //    //empTrans.DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue);
        //    //empTrans.ManagerID = Convert.ToInt32(DdlLineManager1.SelectedValue);
        //    //empTrans.EmployeeType = DdlType1.SelectedValue;
        //    //empTrans.FirstName = Data.FirstName;
        //    //empTrans.LastName = Data.LastName;
        //    //empTrans.Email = Data.Email;
        //    //empTrans.EntryDate = EntryDate;
        //    //empTrans.Action = "Update";
        //    //db.Entry(empTrans).State = System.Data.Entity.EntityState.Added;
        //    db.vt_tbl_Employee.Add(Data);
        //    db.SaveChanges();

        //    Response.Redirect("Employee.aspx");
        //    #endregion
        //}

        //using (var transaction = db.Database.BeginTransaction())
        //{
            try
            {
                var Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();

                if (Data != null)
                {
                    DateTime dt = DateTime.UtcNow;
                    // Update the data
                    Data.EmployeeID = ID;
                    Data.CompanyID = CompanyID;
                    Data.DepartmentID = Convert.ToInt32(DdlDepartment1.SelectedValue);
                    Data.DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue);
                    Data.ManagerID = Convert.ToInt32(DdlLineManager1.SelectedValue);
                    Data.Type = DdlType1.SelectedValue;
                    Data.Email = TxtEmail.Text;

                if (!string.IsNullOrEmpty(txtEntryDate.Text))
                {
                    string txtDateValue = txtEntryDate.Text;
                    DateTime? resultDate = custom.GetDateFromTextBox(txtDateValue);
                    Data.TransferDate = resultDate;
                }

                //Data.TransferDate = DateTime.Today.Add(DateTime.Now.TimeOfDay);

                // Mark the entity as modified
                // Mark the entity as modified
                db.Entry(Data).State = System.Data.Entity.EntityState.Modified;

                // Save changes
                db.SaveChanges();

                // Commit the transaction
                //  transaction.Commit();

                Response.Redirect("Employee.aspx");
                }
            }
            catch (Exception ex)
            {
              //  transaction.Rollback();
                // Log the exception or handle it accordingly
                Console.WriteLine(ex.Message);
                throw;
            }
       // }

    }
}