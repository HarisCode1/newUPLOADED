using NPOI.SS.Formula.Functions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employee_PromotionNew : System.Web.UI.Page
{
    private int ID = 0;
    private int CompanyID = 0;
    private int RoleID = 0;
    private DateTime EntryDate = DateTime.Now;
    private vt_EMSEntities db = new vt_EMSEntities();
    Custommethods custom = new Custommethods();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            CompanyID = Convert.ToInt32(Session["CompanyId"]);
            RoleID = Convert.ToInt32(Session["RoleId"]);
            if (!IsPostBack)
            {
                TxtDate.Text = EntryDate.ToString("dd/MM/yyyy");
                TxtEffectiveDate.Text = EntryDate.ToString("dd/MM/yyyy");

                Bind_GvLog();
                if (ID > 0)
                {
                    FillDetails(ID);
                }
            }
        }
    }

    #region Fill

    private void Bind_GvLog()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        DataSet Ds = ProcedureCall.SpCall_Sp_Get_Employee_PromotionLog_New(ID);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                Dt.Columns.Add("FileClass", typeof(string));
                foreach (DataRow dr in Dt.Rows)
                {
                    dr["FileClass"] = (dr["PromotionDocxPath"].ToString() != "") ? "btn btn-link" : "btn btn-link btn-dark disabled";                    
                }
                GvLog.DataSource = Dt;
            }
            else
            {
                GvLog.DataSource = null;
            }
        }
        else
        {
            GvLog.DataSource = null;
        }
        GvLog.DataBind();
    }

    private void FillDetails(int ID)
    {
        vt_tbl_Employee EmpData = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (EmpData != null)
        {

            TxtFirstName.Text = EmpData.FirstName;
            TxtLastName.Text = EmpData.LastName;
            TxtEmail.Text = EmpData.Email;
            txtoldtax.Text = EmpData.Tax.ToString();
            //object val = ProcedureCall.sp_SingleValue("vt_tbl_department", "Department", "DepartmentID", 3121);
            TxtCurrentDepartment.Text = ProcedureCall.sp_SingleValue("vt_tbl_department", "Department", "DepartmentID", EmpData.DepartmentID).ToString();
            TxtCurrentDesignation.Text = ProcedureCall.sp_SingleValue("vt_tbl_designation", "Designation", "DesignationID", EmpData.DesignationID).ToString();
            var query = db.vt_tbl_User.Where(x => x.UserId == EmpData.ManagerID).ToList();
            if (query.Count > 0)
            {
                TxtCurrentLineManager.Text = ProcedureCall.sp_SingleValue("vt_tbl_User", "UserName", "UserId", EmpData.ManagerID).ToString();
                //TxtCurrentLineManager.Text = EmpData.ManagerID.ToString();
            }
            else
            {
                if (EmpData.ManagerID > 0)
                {
                    var queryformanagerid = (from emp in db.vt_tbl_Employee
                                             join des in db.vt_tbl_Designation on emp.ManagerID equals des.DesignationID
                                             where emp.ManagerID == EmpData.ManagerID
                                             select new
                                             {
                                                 des.Designation
                                             }).FirstOrDefault();
                    if (queryformanagerid != null)
                    {
                        TxtCurrentLineManager.Text = queryformanagerid.Designation;
                    }
                        //db.vt_tbl_Employee.Where(x => x.ManagerID == EmpData.ManagerID).Select(x => new                    
                    //TxtCurrentLineManager.Text = ProcedureCall.sp_SingleValue("vt_tbl_Employee", "EmployeeName", "EmployeeID", EmpData.ManagerID).ToString();
                    
                }
                else
                {
                    TxtCurrentLineManager.Text = "";

                }
            }

            if (EmpData.Type != null)
            {
                TxtCurrentEmploymentType.Text = ProcedureCall.sp_SingleValue("vt_tbl_TypeofEmployee", "Type", "ID", Convert.ToInt32(EmpData.Type)).ToString();
            }
            TxtCurrentBasicSalary.Text = EmpData.BasicSalary.ToString();
            TxtCurrentHouseRentAllowance.Text = EmpData.HouseRentAllownce.ToString();
            TxtCurrentMedicalRentAllowance.Text = EmpData.MedicalAllowance.ToString();
            TxtCurrentTransportAllowance.Text = EmpData.TransportAllownce.ToString();
            TxtCurrentFuelAllowance.Text = EmpData.FuelAllowance.ToString();
            TxtCurrentSpecialAllowance.Text = EmpData.SpecialAllowance.ToString();
            TxtCurrentPFStatus.Text = Convert.ToInt16(EmpData.PFApplicable).ToString();
            TxtCurrentPFType.Text = EmpData.PFType.ToString();

            //Set Current Designation
            Bind_Department(CompanyID);
            Bind_EmployementType();
            Bind_DdlPFType();
            DateTime joiningDate = (DateTime)EmpData.JoiningDate;
            HiddenField1.Value = joiningDate.ToString("yyyy-MM-dd");

        }
    }

    private void Bind_Department(int CompanyID)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@CompanyID", CompanyID)
        };
        vt_Common.Bind_DropDown(DdlNewDepartment, "VT_SP_BindDepart", "Department", "DepartmentID", param);
    }

    private void Bind_Designation(int DepartmentID)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@DepartmentID", DepartmentID)
        };
        vt_Common.Bind_DropDown(DdlNewDesignation, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);
    }

    public void BindLineManager(int DesignationID)
    {
        //int Des = 0;
        //Des = (Convert.ToInt32(DdlLineManager1.SelectedValue));
        int Des = 0;
        int Dept = 0;
        Des = Convert.ToInt32(DdlNewDesignation.SelectedValue);
        Dept = Convert.ToInt32(DdlNewDepartment.SelectedValue);
        int CompanyId = Convert.ToInt32(Session["CompanyID"]);

        if (DesignationID != 0)
        {
            SqlParameter[] param =
            {

            new SqlParameter("@DesignationID",Des),
            new SqlParameter("@DepartmentID",Dept),
            new SqlParameter("@CompanyID",CompanyId)
        };
            var query = db.vt_tbl_Designation.Where(x => x.DesignationID == Des && x.TopDesignationID==0).FirstOrDefault();
            int id = 0;
            if (query != null)
            {
                id = Des;
            }
            vt_Common.Bind_DropDown(DdlNewLineManager, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
            if (DdlNewLineManager.Items.Count==1 &&  Des ==id)
            {
                int RoleID = Convert.ToInt32(Session["RoleId"]);
                int ComID = Convert.ToInt32(Session["CompanyId"]);
                SqlParameter[] param1 =
                {
                new SqlParameter("@RoleId",RoleID),
                new SqlParameter("@CompanyId",ComID)
            };
                vt_Common.Bind_DropDown(DdlNewLineManager, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
            }
            else if (DdlNewLineManager.Items.Count == 1 && Des > 1)
            {
                vt_Common.Bind_DropDown(DdlNewLineManager, "VT_sp_BindLineManager", "Designation", "DesignationID", param);
            }
            //    vt_Common.Bind_DropDown(DdlNewLineManager, "VT_sp_BindLineManager", "EmployeeName", "EmployeeID", param);
            //    if (DdlNewLineManager.Items.Count == 1)
            //    {
            //        int RoleID = Convert.ToInt32(Session["RoleId"]);
            //        int Company = Convert.ToInt32(Session["CompanyId"]);

            //        if (RoleID == 0)
            //        {
            //            RoleID = 2;
            //        }
            //        int ComID = Convert.ToInt32(DdlNewLineManager.SelectedValue);
            //        SqlParameter[] param1 =
            //        {
            //    new SqlParameter("@RoleId",RoleID),
            //    new SqlParameter("@CompanyId",Company)
            //};
            //        vt_Common.Bind_DropDown(DdlNewLineManager, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
            //    }
            //}
            //else
            //{
            //    SqlParameter[] param =
            //   {
            //    new SqlParameter("@DesignationID",DesignationID)
            //};
            //    vt_Common.Bind_DropDown(DdlNewLineManager, "VT_sp_BindLineManager", "EmployeeName", "EmployeeID", param);
        }
    }

    private void Bind_LineManager(int DesignationID)
    {
        int ManagerID = Convert.ToInt32(ViewState["ManagerID"]);
        int Company = Convert.ToInt32(Session["CompanyId"]);
        var query = db.vt_tbl_User.Where(x => x.UserId == ManagerID).ToList();
        if (query.Count > 0)
        {
            SqlParameter[] param1 =
            {
            new SqlParameter("@RoleId", RoleID),
            new SqlParameter("@CompanyId", CompanyID)
        };
            vt_Common.Bind_DropDown(DdlNewLineManager, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
        }
        else
        {
            SqlParameter[] param =
            {
            new SqlParameter("@DesignationID",DesignationID)
        };
            vt_Common.Bind_DropDown(DdlNewLineManager, "VT_sp_BindLineManager", "EmployeeName", "EmployeeID", param);
        }
    }

    private void Bind_EmployementType()
    {
        DdlNewEmploymentType.Items.Clear();
        var Typedropdown = (from m in db.vt_tbl_TypeofEmployee
                            select new
                            {
                                m.Type,
                                m.Id
                            }).ToList();

        DdlNewEmploymentType.DataSource = Typedropdown;
        DdlNewEmploymentType.DataTextField = "Type";
        DdlNewEmploymentType.DataValueField = "Id";
        DdlNewEmploymentType.DataBind();
        DdlNewEmploymentType.Items.Insert(0, new ListItem("Please Select", "0"));
    }

    private void Bind_DdlPFType()
    {
       //// DdlPFType.Items.Clear();
       // var PFtype = (from m in db.vt_tbl_TypeofSalary
       //               select new
       //               {
       //                   m.Type,
       //                   m.Id
       //               }).ToList();

       // DdlPFType.DataSource = PFtype;
       // DdlPFType.DataTextField = "Type";
       // DdlPFType.DataValueField = "Id";
       // DdlPFType.DataBind();
       // DdlPFType.Items.Insert(0, new ListItem("Please Select", "0"));
    }

    #endregion Fill

    #region Change

    protected void DdlNewDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DepartmentID = Convert.ToInt32(DdlNewDepartment.SelectedValue);
        Bind_Designation(Convert.ToInt32(DepartmentID));
    }

    protected void DdlNewDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DesignationID = Convert.ToInt32(DdlNewDesignation.SelectedValue);
        DdlNewLineManager.Items.Clear();
        //Bind_LineManager(Convert.ToInt32(DesignationID));
        BindLineManager(Convert.ToInt32(DesignationID));



        int desiId = Convert.ToInt32(DdlNewDesignation.SelectedValue);
        if (desiId != 0)
        {

            var query1 = db.vt_tbl_Designation.Where(x => x.DesignationID == desiId).FirstOrDefault();
            if (query1 != null)
            {
                var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query1.ReportTo).ToList();
                if (qry.Count > 0)
                {

                    DdlNewLineManager.DataSource = qry;
                    DdlNewLineManager.DataTextField = "Topdesignations";
                    DdlNewLineManager.DataValueField = "Id";
                    DdlNewLineManager.DataBind();
                    DdlNewLineManager.Visible = true;
                    //DdlNewLineManager.Items.Clear();

                }
                //else
                //{
                //    ddltopdesignation.Visible = false;
                //    div2.Visible = false;
                //}
            }
        }



    }

    #endregion Change

    #region Save

    private bool Save()
    {
        vt_tbl_Employee_PromotionNewLog Log = new vt_tbl_Employee_PromotionNewLog();
        var query = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID && x.CompanyID == CompanyID).FirstOrDefault();
        if (query != null)
        {
            Log.EmployeeID = ID;
            Log.EmployeeID = ID;
            Log.DepartmentID = Convert.ToInt32(query.DepartmentID);
            Log.DesignationID = Convert.ToInt32(query.DesignationID);
            Log.LineManagerID = Convert.ToInt32(query.ManagerID);
            Log.EmploymentType = Convert.ToInt32(query.Type);
            //Log.BasicSalary = Convert.ToDecimal(txtne);
            Log.BasicSalary = Convert.ToDecimal(query.BasicSalary);
            Log.Tax = Convert.ToDecimal(query.Tax);
            Log.HouseRentAllownce = Convert.ToDecimal(query.HouseRentAllownce);
            Log.TransportAllownce = Convert.ToDecimal(query.TransportAllownce);
            Log.MedicalAllowance = Convert.ToDecimal(query.MedicalAllowance);
            Log.FuelAllowance = Convert.ToDecimal(query.FuelAllowance);
            Log.SpecialAllowance = Convert.ToDecimal(query.SpecialAllowance);
            //Log.PFApplicable = Convert.ToBoolean(ChkPFStatus.Checked);
            Log.PFType = "";

            if (!string.IsNullOrEmpty(TxtEffectiveDate.Text))
            {
                string txtDateValue = TxtEffectiveDate.Text;
                DateTime? resultDate = custom.GetDateFromTextBox(txtDateValue);
                Log.EffectiveDate = resultDate;
            }



            //Log.EffectiveDate = DateTime.ParseExact(TxtEffectiveDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Log.EntryDate = EntryDate;
            Log.CreatedBy = Convert.ToInt32(Session["UserId"]);
            //Log.PFType = DdlPFType.SelectedValue;
            Log.Action = "Inserted";
           // Log.PromotionDocxPath = query.OtherDocuments;
            if (fluploadPromotionDocx.HasFile)
            {
                string extension = Path.GetExtension(fluploadPromotionDocx.PostedFile.FileName);
                string filename = Path.GetFullPath(fluploadPromotionDocx.FileName);
                string folderPath = AppDomain.CurrentDomain.BaseDirectory + "images\\PromotionUploadDocx"; // Your path Where you want to save other than Server.MapPath
                                                                                                           //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                string newFileName = Log.EmployeeID + "_" + DateTime.Now.ToString("ddmmyyyyHHmmss") + "_" + Path.GetFileName(fluploadPromotionDocx.FileName);
                string path = Path.Combine(Server.MapPath("~/images/PromotionUploadDocx"), newFileName);

                //Save the File to the Directory (Folder).
                fluploadPromotionDocx.SaveAs(path);
                //Upload File Code block ends..
              //  Log.PromotionDocxPath = "images/PromotionUploadDocx/" + newFileName;

            }

            db.Entry(Log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

        //Previous record end
        //Log.EmployeeID = ID;
        //Log.DepartmentID = Convert.ToInt32(DdlNewDepartment.SelectedValue);
        //Log.DesignationID = Convert.ToInt32(DdlNewDesignation.SelectedValue);
        //Log.LineManagerID = Convert.ToInt32(DdlNewLineManager.SelectedValue);
        //Log.EmploymentType = Convert.ToInt32(DdlNewEmploymentType.SelectedValue);
        ////Log.BasicSalary = Convert.ToDecimal(txtne);
        //Log.BasicSalary = Convert.ToDecimal(TxtNewBasicSalary.Text);
        //Log.Tax = Convert.ToDecimal(txtcurrenttax.Text);
        //Log.HouseRentAllownce = Convert.ToDecimal(TxtNewHouseRentAllowance.Text == "" ? "0.00" : TxtNewHouseRentAllowance.Text);
        //Log.TransportAllownce = Convert.ToDecimal(TxtNewTransportAllowance.Text == "" ? "0.00" : TxtNewTransportAllowance.Text);
        //Log.MedicalAllowance = Convert.ToDecimal(TxtNewMedicalAllowance.Text == "" ? "0.00" : TxtNewMedicalAllowance.Text);
        //Log.FuelAllowance = Convert.ToDecimal(TxtNewFuelAllowance.Text == "" ? "0.00" : TxtNewFuelAllowance.Text);
        //Log.SpecialAllowance = Convert.ToDecimal(TxtNewSpecialAllowance.Text == "" ? "0.00" : TxtNewSpecialAllowance.Text);
        ////Log.PFApplicable = Convert.ToBoolean(ChkPFStatus.Checked);
        //Log.PFType = "";
        //Log.EffectiveDate = DateTime.ParseExact(TxtEffectiveDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        //Log.EntryDate = EntryDate;
        //Log.CreatedBy = Convert.ToInt32(Session["UserId"]);
        ////Log.PFType = DdlPFType.SelectedValue;
        //Log.Action = "Inserted";
        //if (fluploadPromotionDocx.HasFile)
        //{
        //    string extension = Path.GetExtension(fluploadPromotionDocx.PostedFile.FileName);
        //    string filename = Path.GetFullPath(fluploadPromotionDocx.FileName);
        //    string folderPath = AppDomain.CurrentDomain.BaseDirectory + "images\\PromotionUploadDocx"; // Your path Where you want to save other than Server.MapPath
        //                                                                                               //Check whether Directory (Folder) exists.
        //    if (!Directory.Exists(folderPath))
        //    {
        //        //If Directory (Folder) does not exists. Create it.
        //        Directory.CreateDirectory(folderPath);
        //    }

        //    string newFileName = Log.EmployeeID + "_" + DateTime.Now.ToString("ddmmyyyyHHmmss") + "_" + Path.GetFileName(fluploadPromotionDocx.FileName);
        //    string path = Path.Combine(Server.MapPath("~/images/PromotionUploadDocx"), newFileName);

        //    //Save the File to the Directory (Folder).
        //    fluploadPromotionDocx.SaveAs(path);
        //    //Upload File Code block ends..
        //    Log.PromotionDocxPath = "images/PromotionUploadDocx/" + newFileName;
        //}
        //db.Entry(Log).State = System.Data.Entity.EntityState.Added;

        vt_tbl_Employee Employee = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (Employee != null)
        {
            Employee.DepartmentID = Convert.ToInt32(DdlNewDepartment.SelectedValue);
            Employee.DesignationID = Convert.ToInt32(DdlNewDesignation.SelectedValue);
            int desiId = Convert.ToInt32(DdlNewDesignation.SelectedValue);
            if (desiId != 0)
            {

                var query1 = db.vt_tbl_Designation.Where(x => x.DesignationID == desiId).FirstOrDefault();
                if (query1 != null)
                {
                    var qry = db.vt_tbl_TopDesignations.Where(x => x.Id == query1.ReportTo ).ToList();
                    if (qry.Count > 0)
                    {

                        DdlNewLineManager.DataSource = qry;
                        DdlNewLineManager.DataTextField = "Topdesignations";
                        DdlNewLineManager.DataValueField = "Id";
                        DdlNewLineManager.DataBind();
                        DdlNewLineManager.Visible = true;
                        //DdlNewLineManager.Items.Clear();
                       
                    }
                    //else
                    //{
                    //    ddltopdesignation.Visible = false;
                    //    div2.Visible = false;
                    //}
                }
            }




            Employee.ManagerID = Convert.ToInt32(DdlNewLineManager.SelectedValue);
            Employee.Type = (DdlNewEmploymentType.SelectedValue);

            Employee.IsPromoted = true;
            Employee.PromotionDate = EntryDate;

            if (!string.IsNullOrEmpty(TxtEffectiveDate.Text))
            {
                string txtDateValue = TxtEffectiveDate.Text;
                DateTime? resultDate = custom.GetDateFromTextBox(txtDateValue);
                Employee.Promotion_EffictiveDate = resultDate;
            }

            //Employee.Promotion_EffictiveDate = DateTime.ParseExact(TxtEffectiveDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (fluploadPromotionDocx.HasFile)
            {
                string extension = Path.GetExtension(fluploadPromotionDocx.PostedFile.FileName);
                string filename = Path.GetFullPath(fluploadPromotionDocx.FileName);
                string folderPath = AppDomain.CurrentDomain.BaseDirectory + "images\\PromotionUploadDocx"; // Your path Where you want to save other than Server.MapPath
                                                                                                           //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {

                    Directory.CreateDirectory(folderPath);
                }

                string newFileName = Log.EmployeeID + "_" + DateTime.Now.ToString("ddmmyyyyHHmmss") + "_" + Path.GetFileName(fluploadPromotionDocx.FileName);
                string path = Path.Combine(Server.MapPath("~/images/PromotionUploadDocx"), newFileName);

                //Save the File to the Directory (Folder).
                fluploadPromotionDocx.SaveAs(path);
                //Upload File Code block ends..
                Employee.OtherDocuments = "images/PromotionUploadDocx/" + newFileName;
            }
            db.Entry(Employee).State = System.Data.Entity.EntityState.Modified;
        }
        db.SaveChanges();
        //Response.Redirect("Employee_PromotionNew.aspx?ID=" + Employee.EmployeeID);
        Response.Redirect("Employee.aspx");

        return true;
    }
    private void PreviousPromotionSave()
    {
        vt_tbl_Employee_PromotionNewLog Log = new vt_tbl_Employee_PromotionNewLog();
      
    }
    #endregion Save

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      
       Save();
        vt_Common.ReloadJS(Page, "msgbox(1,' ' ,'Succesfully Done'); setTimeout(function(){window.location.href='Employee_PromotionNew.aspx?ID=" + ID + "';},1000)");
    }

    protected void BtnLog_Click(object sender, EventArgs e)
    {
        UpDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Employee.aspx");
    }

    protected void ChkPFStatus_CheckedChanged(object sender, EventArgs e)
    {
        //if (ChkPFStatus.Checked)
        //{
        //    //DdlPFType.Enabled = true;
        //}
        //else
        //{
        //    //DdlPFType.Enabled = false;
        //}
    }
}