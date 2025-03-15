using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employee_ChangeDesignation : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    DateTime EntryDate = DateTime.Now;
    int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            if (!Page.IsPostBack)
            {
                Bind_GVLog();
                Bind_DdlCompany();
                FillDetailForm(ID);
            }
        }
    }
    void FillDetailForm(int ID)
    {
        vt_tbl_Employee Emp = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (Emp != null)
        {
            ViewState["ManagerID"] = Emp.ManagerID; ;
            TxtFirstName.Text = Emp.FirstName;
            TxtLastName.Text = Emp.LastName;
            TxtEmail.Text = Emp.Email;
            DdlComapny1.SelectedValue = Emp.CompanyID.ToString();
            Bind_Department(Convert.ToInt32(Emp.CompanyID));
            DdlDepartment1.SelectedValue = Emp.DepartmentID.ToString();
            Bind_Designation(Convert.ToInt32(Emp.DepartmentID));
            DdlDesignation1.SelectedValue = Emp.DesignationID.ToString();
            DdlDesignationFrom.SelectedValue = Emp.DesignationID.ToString();

            Bind_LineManager(Convert.ToInt32(Emp.DesignationID));
            DdlLineManager1.SelectedValue = Emp.ManagerID.ToString();
            Bind_Type();
            DdlType1.SelectedValue = Emp.Type;
        }
    }
    void Bind_DdlCompany()
    {
        vt_Common.Bind_DropDown(DdlComapny1, "Sp_GetCompany", "CompanyName", "CompanyID");
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
        vt_Common.Bind_DropDown(DdlDesignationFrom, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);

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
            new SqlParameter("@CompanyId",DdlComapny1.SelectedValue)
        };
            vt_Common.Bind_DropDown(DdlLineManager1, "VT_SP_BindHRAdmin", "UserName", "UserId", param1);
        }
        else
        {
            SqlParameter[] param =
            {
            new SqlParameter("@DesignationID",DesignationID)
        };
            vt_Common.Bind_DropDown(DdlLineManager1, "VT_sp_BindLineManager", "EmployeeName", "EmployeeID", param);
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
        int CompanyID = Convert.ToInt32(DdlComapny1.SelectedValue.ToString());
        Bind_Department(CompanyID);
    }
    protected void DdlDepartment1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DepartmentID = Convert.ToInt32(DdlDepartment1.SelectedValue.ToString());
        Bind_Designation(DepartmentID);
    }
    protected void DdlDesignation1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue.ToString());
        Bind_LineManager(DesignationID);
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Update_TransferInfo(ID);
    }
    void Update_TransferInfo(int ID)
    {
        vt_tbl_Employee_Change_DesignationLog empChangeDes = new vt_tbl_Employee_Change_DesignationLog();
        var Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (Data != null)
        {
            Data.CompanyID = Convert.ToInt32(DdlComapny1.SelectedValue);
            Data.DepartmentID = Convert.ToInt32(DdlDepartment1.SelectedValue);
            Data.DesignationID = Convert.ToInt32(DdlDesignation1.SelectedValue);
            Data.ManagerID = Convert.ToInt32(DdlLineManager1.SelectedValue);
            Data.Type = DdlType1.SelectedValue;
            db.Entry(Data).State = System.Data.Entity.EntityState.Modified;

            #region Code
            empChangeDes.EmployeeID = Convert.ToInt32(Data.EmployeeID);
            empChangeDes.FirstName = TxtFirstName.Text;
            empChangeDes.LastName = TxtLastName.Text;
            empChangeDes.CompanyID = Convert.ToInt32(DdlComapny1.SelectedValue);
            empChangeDes.DepartmetID = Convert.ToInt32(DdlDepartment1.SelectedValue);
            empChangeDes.DesignationFromID = Convert.ToInt32(DdlDesignationFrom.SelectedValue);
            empChangeDes.DesignationToID = Convert.ToInt32(DdlDesignation1.SelectedValue);
            empChangeDes.LineManager = Convert.ToInt32(DdlLineManager1.SelectedValue);
            empChangeDes.Type = Convert.ToInt32(DdlType1.SelectedValue);
            empChangeDes.FirstName = Data.FirstName;
            empChangeDes.LastName = Data.LastName;
            empChangeDes.Date = Convert.ToDateTime(txtFromDate.Text);
            empChangeDes.Email = Data.Email;
            empChangeDes.EntryDate = EntryDate;
            empChangeDes.UserID = (int)Session["UserId"];
            db.Entry(empChangeDes).State = System.Data.Entity.EntityState.Added;

            db.SaveChanges();
            Response.Redirect("Employee.aspx");
            #endregion
        }
    }

    protected void BtnLog_Click(object sender, EventArgs e)
    {
        UpDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }
    private void Bind_GVLog()
    {
        DataSet Ds = ProcedureCall.SpCall_Employee_ChangeDesignationLogs();
        if(Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if(Dt != null && Dt.Rows.Count > 0)
            {
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
}