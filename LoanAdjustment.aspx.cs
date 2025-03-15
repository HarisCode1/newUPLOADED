using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using Viftech;
public partial class LoanAdjustment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        vt_Common.ReloadJS(this.Page, "binddata();");
    }

    protected void grdLoanEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_LoanEntry le = db.vt_tbl_LoanEntry.FirstOrDefault(x => x.EntryID == ID);
                        db.vt_tbl_LoanEntry.Remove(le);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, "Record", "Deleted Successfully!");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditCompany":
                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                vt_Common.ReloadJS(this.Page, "$('#loanentry').modal();binddata();");
                UpDetail.Update();
                break;
            default:
                break;
        }

        vt_Common.ReloadJS(this.Page, "$('.confirm').confirm();");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_LoanMonthAmount le = new vt_tbl_LoanMonthAmount();
                if (((EMS_Session)Session["EMS_Session"]).Company != null)
                {
                    le.CompanyID = ((EMS_Session)Session["EMS_Session"]).Company.CompanyID;
                }
                else
                {
                    le.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                }

                int LoanId = Convert.ToInt32(ddlLoanType.SelectedValue);
                string EmpId = ddlEmployee.SelectedValue;
                DateTime dt = DateTime.Now;
                string mon = dt.Month.ToString();
                string yy = dt.Year.ToString().Substring(2, 2);
                var LoanMonthAMT = db.vt_tbl_LoanMonthAmount.Where(x => x.CompanyID == le.CompanyID && x.LoanID == LoanId && x.EnrollId == EmpId && x.MonthYear.Substring(0, 2) == mon && x.MonthYear.Substring(7, 2) == yy).Select(x => x.LMAID).FirstOrDefault();
                var InstallCount = db.vt_tbl_LoanMonthAmount.Where(x => x.CompanyID == le.CompanyID && x.LoanID == LoanId && x.EnrollId == EmpId && x.LMAID >= LoanMonthAMT).Count();
                var installDetail = db.VT_SP_getTotalLoanInstall(le.CompanyID, Convert.ToInt32(EmpId), LoanId, LoanMonthAMT).FirstOrDefault();
                double install = Convert.ToDouble(txtLoanAmount.Text);
                db.VT_SP_UpdateLoanInstallment(le.CompanyID, Convert.ToInt32(EmpId), LoanMonthAMT, install.ToString());
                double AmtRemaining = Convert.ToDouble(installDetail) - install;
                InstallCount = InstallCount + 1;
                var MonthlyInstallment = AmtRemaining / InstallCount;
                string LoanInstallAMT = MonthlyInstallment.ToString();
                var getTotalAMOUNT = db.VT_SP_UpdateLoanInstallment(le.CompanyID, Convert.ToInt32(EmpId), LoanMonthAMT, LoanInstallAMT);
            }

            MsgBox.Show(Page, MsgBox.success, "", "Successfully Save");
            ClearForm();
            LoadData();
            UpView.Update();
        }
        catch (DbUpdateException ex)
        {

        }
    }

    protected void btnAddnew_ServerClick(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    [WebMethod]
    [ScriptMethod]
    public static dynamic getEmpList()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            return db.vt_tbl_Employee.ToList().Select(x => new string[] { x.EmployeeID.ToString(), x.EmployeeName });
        }
    }

    internal void LoadData()
    {
        if (Session["EMS_Session"] != null)
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {

                if (((EMS_Session)Session["EMS_Session"]).Company == null)
                {
                    var Query = db.VT_SP_GetLoanEntry(0,1,1).ToList();
                    grdLoanEntry.DataSource = Query;
                    grdLoanEntry.DataBind();
                }
                else
                {
                    divCompany.Visible = false;
                    var Query = db.VT_SP_GetLoanEntry(vt_Common.CompanyId,1,2).ToList();
                    grdLoanEntry.DataSource = Query;
                    grdLoanEntry.DataBind();
                }
            }
        }
    }

    internal void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#loanentry').modal('hide');$('.confirm').confirm();binddata();");
    }

    internal void FillDetailForm(int ID)
    {
        try
        {

            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                vt_tbl_LoanEntry le = db.vt_tbl_LoanEntry.FirstOrDefault(x => x.EntryID == ID);
                ddlcomp.SelectedValue = vt_Common.CheckString(le.CompanyID);
                BindEmployeeGrid((int)le.CompanyID);


                int empID = (int)le.EmployeeId;
                BindLoanType(empID, (int)le.CompanyID);
                vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == empID);
                ddlEmployee.SelectedValue = vt_Common.CheckString(emp.EmployeeID);
                vt_tbl_Loan LoanTypeList = db.vt_tbl_Loan.Where(x => x.LoanID == le.LoanId).FirstOrDefault();
                ddlLoanType.SelectedValue = vt_Common.CheckString(LoanTypeList.LoanID);
                //    ddlLoanType.SelectedValue = vt_Common.CheckString(le.LoanID);

                string empStr = Convert.ToString(empID);
                DateTime dt = DateTime.Now;
                string mm = dt.Month.ToString();
                string yy = dt.Year.ToString().Substring(2, 2);
                var LoanMonthAMT = db.vt_tbl_LoanMonthAmount.Where(x => x.CompanyID == le.CompanyID && x.LoanID == le.LoanId && x.EnrollId == empStr).ToList();
                if (LoanMonthAMT.Count > 0)
                {
                    LblInstallmentAmt.Text = String.Format("{0:0.00}", LoanMonthAMT[0].Amount);
                }

                txtLoanAmount.Text = String.Format("{0:0.00}", LoanMonthAMT[0].Amount);
                lblMonths.Text = dt.ToString("MMMM");

            }
            ViewState["PageID"] = ID;

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        UpDetail.Update();


        if (((EMS_Session)Session["EMS_Session"]).Company == null)
        {
            vt_Common.ReloadJS(this.Page, "$('#loanentry').modal();");
        }

        else
        {
            ddlCompany.Visible = false;
            //   trCompany.Visible = false;
            vt_Common.ReloadJS(this.Page, "$('#loanentry').modal();");

        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetLoanEntry(Convert.ToInt32(ddlCompany.SelectedValue),0,4);
            grdLoanEntry.DataSource = Query;
            grdLoanEntry.DataBind();
            UpView.Update();


        }
    }

    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {

            if (ddlcomp.SelectedValue != "0")
            {

                BindEmployeeGrid(Convert.ToInt32(ddlcomp.SelectedValue));

            }
            else
            {
                ddlEmployee.Items.Clear();

            }
        }
    }

    public void BindEmployeeGrid(int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var EmployeeList = db.VT_SP_GetLoanEmployees(CompanyID);
            ddlEmployee.DataSource = EmployeeList;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        int EmpId = Convert.ToInt32(ddlEmployee.SelectedValue);
        int CompanyID = Convert.ToInt32(ddlcomp.SelectedValue);
        if (EmpId > 0 && CompanyID > 0)
        {
            BindLoanType(EmpId, CompanyID);
        }
    }

    public void BindLoanType(int EmployeeID, int CompanyId)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var LoanType = db.VT_SP_GetEmployeeLoan(CompanyId, EmployeeID).ToList();

            var LoanTypeList = db.vt_tbl_Loan.Where(x => LoanType.Contains(x.LoanID)).ToList();

            ddlLoanType.DataSource = LoanTypeList;
            ddlLoanType.DataTextField = "Name";
            ddlLoanType.DataValueField = "LoanID";
            ddlLoanType.DataBind();
            ddlLoanType.Items.Insert(0, new ListItem("Please Select Loan Type...", "0"));
        }
    }

    protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int LoanId = Convert.ToInt32(ddlLoanType.SelectedValue);
        string EmpId = ddlEmployee.SelectedValue;

        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            DateTime dt = DateTime.Now;
            string mon = dt.Month.ToString();
            string yy = dt.Year.ToString().Substring(2, 2);
            var LoanMonthAMT = db.VT_SP_GetLoanMonthAmount(LoanId, Convert.ToInt32(EmpId), dt).ToList();
            if (LoanMonthAMT.Count > 0)
                LblInstallmentAmt.Text = LoanMonthAMT[0].Amount;
            lblMonths.Text = DateTime.Now.ToString("MMMM");
        }
    }
}
