using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Viftech;

public partial class _Default : System.Web.UI.Page
{
    public DateTime EntryDate = DateTime.Now;
    vt_EMSEntities db = new vt_EMSEntities();
   
    protected void Page_Load(object sender, EventArgs e)

    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                if (Session["CompanyId"] != null)
                {
                    TotalEmployees();
                    TotalDepartment();
                    TotalAttendance();
                    TotalDesignation();
                    var query = db.vt_tbl_Company.Where(x => x.CompanyID == Companyid).FirstOrDefault();
                    string CompanyName = query.CompanyName;
                    lblcompany.Text = CompanyName;
                }
                LblDate.Text = DateTime.Now.ToString("MMMM yyyy");
                LblEmpName.Text = (string)Session["UserName"];
                int EmployeeID = (int)Session["UserID"];
                HdnEmployeeID.Value = EmployeeID.ToString();
                GetData();
               // int Companyid = Convert.ToInt32(Session["CompanyId"]);

                if ((Session["CompanyId"] != null || Session["CompanyId"] == null) && Session["RoleId"] != null)
                {
                    divCompany.Visible = false;
                    if (Convert.ToInt32(Session["RoleId"]) == 4)
                    {
                        //SpnNotification.Visible = true;
                       
                        SalaryNotification(EmployeeID, EntryDate.Month, EntryDate.Year);
                    }
                }

                ddlCompany.SelectedValue = Companyid.ToString();
            }
            //else
            //{
            //    GetData();

            //}
            vt_Common.ReloadJS(this.Page, "BindCalender();");
            //updateCompany.Update();
        }
    }
    void  TotalEmployees()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        var query = db.vt_tbl_Employee.Where(x => x.JobStatus=="Active" && x.CompanyID ==Companyid).ToList();
        lbltotalemployee.Text = query.Count().ToString();
    }
    void TotalDepartment()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        var query = db.vt_tbl_Department.Where(x =>x.CompanyID == Companyid).ToList();
        lbldep.Text = query.Count().ToString();
    }
    void TotalAttendance()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        var query = db.vt_tbl_Attendance.Where(x => x.CompanyId == Companyid).ToList();
        lblattendance.Text = query.Count().ToString();
    }
    void TotalDesignation()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        var query = db.vt_tbl_Designation.Where(x => x.CompanyID == Companyid).ToList();
        lbldesg.Text = query.Count().ToString();
    }
    void SalaryNotification(int EmployeeID, int Month, int Year)
    {
        DataSet Ds = ProcedureCall.SpCall_Vt_Sp_SalaryNotification(EmployeeID, Month, Year);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                if ((bool)Dt.Rows[0]["IsNotif_Checked"] == false)
                {
                    SpnNotification.Visible = true;
                }
                else
                {
                    SpnNotification.Visible = false;
                }
            }
            else
            {
                SpnNotification.Visible = false;
            }
        }
    }

    [ScriptMethod]
    [WebMethod]
    public static void Update_SalaryNotification(int EmployeeID)
    {
        DateTime TodayDate = DateTime.Now;
        vt_EMSEntities db = new vt_EMSEntities();
        var Data = db.vt_tbl_salaryRecords.Where(x => x.EmployeeID == EmployeeID && x.SalaryMonth.Month == TodayDate.Month && x.SalaryMonth.Year == TodayDate.Year).FirstOrDefault();
        if (Data != null)
        {
            Data.IsNotif_Checked = true;
            db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        
    }
    public class calholiday
    {
        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }
    }

    [ScriptMethod]
    [WebMethod]
    public static List<calholiday> GetHoliday(int? id)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (id == 0)
            {
                List<calholiday> data = db.vt_tbl_Holiday.ToList().Select(h => new calholiday
                {
                    title = h.HolidayName,
                    start = h.FromDate == null ? "" : vt_Common.CheckDateTime(h.FromDate).ToString(),
                    end = h.ToDate == null ? "" : vt_Common.CheckDateTime(h.ToDate).ToString().Replace("AM", "PM")
                }).ToList();

                return data;
            }
            else
            {
                List<calholiday> data = db.vt_tbl_Holiday.Where(o => o.CompanyID == id).ToList().Select(h => new calholiday
                {
                    title = h.HolidayName,
                    start = h.FromDate == null ? "" : vt_Common.CheckDateTime(h.FromDate).ToString(),
                    end = h.ToDate == null ? "" : vt_Common.CheckDateTime(h.ToDate).ToString().Replace("AM", "PM")
                }).ToList();

                return data;
            }
        }
    }

    public void LoadData()
    {
    }

    public int TotalEmployee(int? CompanyID)
    {
        int NumberofEmployee = 0;
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (CompanyID == null)
            {
                NumberofEmployee = db.vt_tbl_Employee.Count();
            }
            else
            {
                NumberofEmployee = db.vt_tbl_Employee.Where(x => x.CompanyID == CompanyID).Count();
            }
        }
        return NumberofEmployee;
    }

    public int TotalDepartment(int? CompanyID)
    {
        int TotalDepartment = 0;
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (CompanyID == null)
            {
                TotalDepartment = db.vt_tbl_Department.Count();
            }
            else
            {
                TotalDepartment = db.vt_tbl_Department.Where(x => x.CompanyID == CompanyID).Count();
            }
        }
        return TotalDepartment;
    }

    public class DesignationTree
    {
        public string Designation { set; get; }

        public string Department { set; get; }

        public int DesignationID { set; get; }

        public int? TopDesignationID { set; get; }
    }

    private void PopulateDesignation(int? CompanyID)
    {
        /*List<vt_tbl_Designation> treeDesig = new List<vt_tbl_Designation>();
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (CompanyID == null)
            {
                treeDesig = db.vt_tbl_Designation.ToList();
            }
            else
            {
                treeDesig = db.vt_tbl_Designation.Where(x => x.CompanyID == CompanyID).ToList();
            }
        }*/

        List<DesignationTree> treeDesig = new List<DesignationTree>();
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (CompanyID == null)
            {
                treeDesig = db.vt_tbl_Designation.Join(db.vt_tbl_Department, D => D.DepartmentID, DE => DE.DepartmentID, (D, DE) => new { D, DE }).ToList().Select(h => new DesignationTree
                {
                    DesignationID = h.D.DesignationID,
                    Department = h.DE.Department,
                    Designation = h.D.Designation,
                    TopDesignationID = h.D.TopDesignationID
                }).ToList();
            }
            else
            {
                treeDesig = db.vt_tbl_Designation.Join(db.vt_tbl_Department, D => D.DepartmentID, DE => DE.DepartmentID, (D, DE) => new { D, DE }).ToList().Where(x => x.D.CompanyID == CompanyID).Select(h => new DesignationTree
                {
                    DesignationID = h.D.DesignationID,
                    Department = h.DE.Department,
                    Designation = h.D.Designation,
                    TopDesignationID = h.D.TopDesignationID
                }).ToList();
            }
        }
        // Call function here for bind treeview
        CreateTreeView(treeDesig, 0, null);
    }

    private void CreateTreeView(List<DesignationTree> source, int parentID, TreeNode parentNode)
    {
        List<DesignationTree> newSource = source.Where(a => a.TopDesignationID.Equals(parentID)).ToList();

        foreach (var i in newSource)
        {
            //TreeNode newnode = new TreeNode(i.Designation, i.DesignationID.ToString());
            TreeNode newnode = new TreeNode();
            if (parentNode == null)
            {
                newnode.Text = i.Designation + " (" + i.Department + ")";
                newnode.Value = i.DesignationID.ToString();
                treeDesignation.Nodes.Add(newnode);
            }
            else
            {
                newnode.Text = i.Designation + " (" + i.Department + ")";
                newnode.Value = i.DesignationID.ToString();
                parentNode.ChildNodes.Add(newnode);
            }

            CreateTreeView(source, i.DesignationID, newnode);
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            Session["CompanyId"] = null;
        }
        else
        {
            if (Session["CompanyId"] == null && Session["RoleId"] == null)
            {
                if (ddlCompany.SelectedValue == "0")
                {
                    Session["CompanyId"] = null;
                }
                else
                {
                    Session["CompanyId"] = Convert.ToInt32(ddlCompany.SelectedValue);
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                Session["CompanyId"] = Convert.ToInt32(ddlCompany.SelectedValue);
            }
        }
    }

    public void GetData()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            DataTable dt = new DataTable();
            var Query = db.GetAllCompanies().ToArray();
            DataTable dt1 = LINQResultToDataTable(Query);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                DataView view = new DataView(dt1);
                DataTable Distnctgrade = view.ToTable(true, "CompanyName", "CompanyID");
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "CompanyID";
                ddlCompany.DataSource = Distnctgrade;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Please Select", ""));
            }
            else
            {
                ddlCompany.DataSource = null;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Please Select", ""));
            }
        }
    }

    public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
    {
        DataTable dt = new DataTable();

        PropertyInfo[] columns = null;

        if (Linqlist == null) return dt;

        foreach (T Record in Linqlist)
        {
            if (columns == null)
            {
                columns = ((Type)Record.GetType()).GetProperties();
                foreach (PropertyInfo GetProperty in columns)
                {
                    Type colType = GetProperty.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                    == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                }
            }

            DataRow dr = dt.NewRow();

            foreach (PropertyInfo pinfo in columns)
            {
                dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                (Record, null);
            }

            dt.Rows.Add(dr);
        }
        return dt;
    }
}