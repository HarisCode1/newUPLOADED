using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class LoanEntry : System.Web.UI.Page
{
    DateTime EntryDate = DateTime.Now;
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)

    {

        int ModuleID = 6;
        int RoleID =Convert.ToInt32(Session["RoleId"]);
        DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);
        //DataTable DtPer = Session["PagePermissions"] as DataTable;    

        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                string PageName = null;
              

                //foreach (DataRow Row in DtPer.Rows)
                //{
                //    if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_View"].ToString() == "True")
                //    {
                //        Bind_GV();
                //        break;
                //    }
                //    else if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_View"].ToString() == "False")
                //    {
                //        Response.Redirect("Default.aspx");
                //    }
                //}
                if (Ds != null && Ds.Tables.Count > 0)
                {
                    DataTable Dt = Ds.Tables[0];
                    DataTable DtPer = Session["PagePermissions"] as DataTable;

                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageName"].ToString() == "Loan Input")
                        {
                            PageName = Row["PageName"].ToString();
                            break;
                        }
                    }
                    if (PageName == "Loan Input")
                    {
                        Bind_GV();
                    }
                    else
                    {
                        Response.Redirect("default.aspx");

                    }
                }
                //if (PageName == null)
                //{
                //    Response.Redirect("default.aspx");
                //}
                //LoadData();

            }
            Bind_GVLLog();
        }
    }


    public  void BindGrid(int EntryID)
    {
        try
        {
            
            var list = db.vt_tbl_IsnotLoanDecution.Where(x => x.EntryId == EntryID).ToList();
            if (list.Count > 0)
            {
                grdnotdeductionmonth.DataSource = list;
                grdnotdeductionmonth.DataBind();
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #region Control Event
    protected void grdLoanEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        switch (e.CommandName)
        {
            case "DeleteCompany":
                try
                {
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Delete"].ToString() == "True")
                        {
                            using (vt_EMSEntities db = new vt_EMSEntities())
                            {
                                int ID = vt_Common.CheckInt(e.CommandArgument);
                                ViewState["hdnID"] = ID;
                                vt_Common.ReloadJS(this.Page, "$('#Delete-Modal').modal('show')");

                            }
                        }
                        else if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Delete"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break; ;

            case "EditCompany":
                foreach (DataRow Row in Dt.Rows)
                {
                    if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Update"].ToString() == "True")
                    {
                        using (vt_EMSEntities db = new vt_EMSEntities())
                        {
                            int id = vt_Common.CheckInt(e.CommandArgument);

                            vt_tbl_LoanEntry objLoanEntry = db.vt_tbl_LoanEntry.Find(id);
                            var Query = db.vt_tbl_LoanEntry.Where(x => x.EntryID == objLoanEntry.EntryID).FirstOrDefault();
                            int EntryID = Convert.ToInt32(e.CommandArgument);
                            BindGrid(EntryID);
                            if (Query != null)
                            {
                                ViewState["EntryID"] = vt_Common.CheckInt(e.CommandArgument);
                                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                                UpDetail.Update();
                                if (Query.HRAdmin ==2)
                                {
                                    ddlEmployee.Enabled = false;
                                    TxtDescription.Enabled = false;
                                    txtDate.Enabled = false;
                                    txtLoanAmount.Enabled = false;
                                    txtMonths.Enabled = false;
                                    ddlLoanType.Enabled = false;
                                    divloan.Visible = true;
                                    
                                }
                              else
                                {
                                    divloan.Visible = false;
                                }

                                vt_Common.ReloadJS(this.Page, "$('#loanentry').modal();");
                            }
                            //else
                            //{
                            //    MsgBox.Show(Page, MsgBox.danger, "Record", "Action has been performed you can not edit!");
                            //}
                        }
                    }
                    else if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Update"].ToString() == "False")
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to edit this record");
                    }
                }
                break;

            default:
                break;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                if (txtnotdedctiondate.Text !=null)
                {
                    int CompanyID= Convert.ToInt32(Session["CompanyId"]); 
                    vt_tbl_LoanEntry objLoanEntry = new vt_tbl_LoanEntry();
                    vt_tbl_IsnotLoanDecution objloan = new vt_tbl_IsnotLoanDecution();
                    if (Session["CompanyId"] != null)
                    {
                        objLoanEntry.CompanyID = CompanyID;
                    }
                    else
                    {
                        objLoanEntry.CompanyID = vt_Common.CheckInt(ddlcomp.SelectedValue);
                    }
                    int EmpID = (int)Session["UserId"];
                    int LoanID = Convert.ToInt32(ddlLoanType.SelectedValue);
                    int EntryID = Convert.ToInt32(ViewState["EntryID"]);
                    var Query = db.vt_tbl_LoanEntry.Where(x => x.EmployeeId == EmpID && x.LoanId == LoanID && x.EntryID != EntryID).ToList();
                    if (Query.Count == 0)
                    {
                        objLoanEntry.AppliedDate = vt_Common.CheckDateTime(txtDate.Text);
                        objLoanEntry.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue); //(int)Session["UserId"];
                        objLoanEntry.LoanId = vt_Common.CheckInt(ddlLoanType.SelectedValue);
                        objLoanEntry.AppliedAmount = vt_Common.Checkdecimal(txtLoanAmount.Text);
                        objLoanEntry.EMIType = ddlEMIType.SelectedValue;
                        objLoanEntry.MonthDuration = vt_Common.CheckInt(txtMonths.Text);
                        objLoanEntry.LineManager = 1;
                        objLoanEntry.Description = TxtDescription.Text;

                        var CheckHR = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmpID && x.IsHRLineManager == true).ToList();
                        if (CheckHR.Count > 0)
                        {
                            objLoanEntry.IsLineManager = true;
                            objLoanEntry.HRAdmin = 1;
                        }
                        else
                        {
                            objLoanEntry.IsLineManager = true;
                            objLoanEntry.HRAdmin = 1;

                        }
                        if (chknloanotdeduction.Checked)
                        {
                            objLoanEntry.LineManager = 2;
                            objLoanEntry.HRAdmin = 2;
                            objLoanEntry.LoanNotDeductionCheck = true;
                            objLoanEntry.DateDeductionMonth = Convert.ToDateTime(txtnotdedctiondate.Text);
                            DateTime isnotdedctiondate = Convert.ToDateTime(txtnotdedctiondate.Text);
                            var loancheck = db.vt_tbl_IsnotLoanDecution.Where(x =>x.EntryId ==EntryID && x.CompanyID==CompanyID && x.IsnotDeductionMonth== isnotdedctiondate).ToList();
                            if (loancheck.Count != 0)
                            {
                                foreach (var item in loancheck)
                                {
                                    if (Convert.ToDateTime(item.IsnotDeductionMonth).Date.ToShortDateString() ==txtnotdedctiondate.Text.ToString())
                                    {
                                      

                                        MsgBox.Show(Page, MsgBox.danger, "Sorry", "Months already exist");
                                       // vt_Common.ReloadJS(Page, "msgbox(1,' ' ,'Succesfully Done'); setTimeout(function(){window.location.href='LoanEntry.aspx';},1000)");                                        

                                    }
                                    else
                                    {
                                        //objloan.CompanyID = CompanyID;
                                        //objloan.EntryId = EntryID;
                                        //objloan.IsnotDeductionMonth = Convert.ToDateTime(txtnotdedctiondate.Text);
                                        //objloan.CreatedDate = DateTime.Now;
                                        //db.vt_tbl_IsnotLoanDecution.Add(objloan);
                                        //db.SaveChanges();

                                    }

                                }

                            }
                            else
                            {
                                //Insert in isnotloaddeduct table
                                objloan.CompanyID = CompanyID;
                                objloan.EntryId = EntryID;
                                objloan.IsnotDeductionMonth = Convert.ToDateTime(txtnotdedctiondate.Text);
                                objloan.CreatedDate = DateTime.Now;
                                db.vt_tbl_IsnotLoanDecution.Add(objloan);
                                db.SaveChanges();
                            }
                           
                     

                        }
                        else if (EntryID > 0 || chknloanotdeduction.Checked == false)

                        {
                            objLoanEntry.LoanNotDeductionCheck = false;
                            objLoanEntry.LineManager = 1;
                            objLoanEntry.HRAdmin = 1;
                            objLoanEntry.DateDeductionMonth = null;
                        }
                    
                      
                        else
                        {
                            objLoanEntry.LoanNotDeductionCheck = false;
                            objLoanEntry.LineManager = 2;
                            objLoanEntry.HRAdmin = 2;
                            objLoanEntry.DateDeductionMonth = null;
                        }
                        if (ViewState["PageID"] != null)
                        {
                            objLoanEntry.EntryID = vt_Common.CheckInt(ViewState["PageID"]);
                            db.Entry(objLoanEntry).State = System.Data.Entity.EntityState.Modified;
                            var loanMonthRecords = db.vt_tbl_LoanMonthAmount.Where(o => o.EntryID == objLoanEntry.EntryID).ToList();
                            loanMonthRecords.ForEach(f => db.vt_tbl_LoanMonthAmount.Remove(f));
                        }
                        else
                        {
                            db.vt_tbl_LoanEntry.Add(objLoanEntry);
                        }
                        db.SaveChanges();
                        double installmentAmt = Convert.ToDouble(txtLoanAmount.Text) / Convert.ToInt32(txtMonths.Text);
                        int numberOfMonths = 0;
                        numberOfMonths= Convert.ToInt32(txtMonths.Text);
                       // int noofmonthadd = 0;
                        if (chknloanotdeduction.Checked)
                        {
                             numberOfMonths = Convert.ToInt32(txtMonths.Text) + 1;

                        }
                        var checkcount = db.vt_tbl_IsnotLoanDecution.Where(x => x.EntryId == EntryID).ToList();
                        if (checkcount.Count >0)
                        {
                            numberOfMonths = Convert.ToInt32(txtMonths.Text) + checkcount.Count;
                            
                        }
                            DateTime currentDate = vt_Common.CheckDateTime(txtDate.Text);
                        DateTime notdedctiondate = vt_Common.CheckDateTime(txtnotdedctiondate.Text);
                        for (int i = 1; i <= numberOfMonths; i++)
                        {
                            vt_tbl_LoanMonthAmount objLoanMonth = new vt_tbl_LoanMonthAmount();
                            objLoanMonth.CompanyID = objLoanEntry.CompanyID;
                            //objLoanMonth.EnrollId = vt_Common.CheckString(ddlEmployee.SelectedValue);
                            objLoanMonth.EnrollId = objLoanEntry.EmployeeId.ToString();
                            objLoanMonth.LoanID = objLoanEntry.LoanId;
                            objLoanMonth.Amount = installmentAmt.ToString();
                            objLoanMonth.CurrentAmount = "0";
                            objLoanMonth.EntryID = objLoanEntry.EntryID;
                            //if (currentDate.AddMonths(i).AddDays(-1).ToString() == vt_Common.CheckDateTime(txtnotdedctiondate.Text).AddMonths(1).AddDays(-1) .ToString())
                            //{
                            //    objLoanMonth.MonthYear = vt_Common.CheckDateTime(txtnotdedctiondate.Text).AddMonths(numberOfMonths).AddDays(-1).ToString();
                            //}
                            //else
                            //{
                                objLoanMonth.MonthYear = currentDate.AddMonths(i).AddDays(-1).ToString();

                           // }
                           
                            db.vt_tbl_LoanMonthAmount.Add(objLoanMonth);
                            db.SaveChanges();

                        }
                        #region LoanEntry Log
                        var loneentry = db.vt_tbl_LoanEntryLog.Where(x => x.LogEntryID == EntryID).FirstOrDefault();
                        if (loneentry == null)
                        {
                            vt_tbl_LoanEntryLog LELog = new vt_tbl_LoanEntryLog();
                            LELog.CompanyID = Convert.ToInt32(Session["CompanyId"]);
                            LELog.AppliedDate = vt_Common.CheckDateTime(txtDate.Text);
                            LELog.EmployeeId = (int)Session["UserId"];
                            LELog.LoanId = vt_Common.CheckInt(ddlLoanType.SelectedValue);
                            LELog.AppliedAmount = vt_Common.Checkdecimal(txtLoanAmount.Text);
                            LELog.Description = TxtDescription.Text;
                            LELog.EMIType = ddlEMIType.SelectedValue;
                            LELog.MonthDuration = vt_Common.CheckInt(txtMonths.Text);
                            LELog.LineManager = 1;
                            LELog.EntryDate = EntryDate;
                            var Checkdata = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmpID && x.IsHRLineManager == true).ToList();
                            if (Checkdata.Count > 0)
                            {
                                LELog.IsLineManager = true;
                                LELog.HRAdmin = 1;
                            }
                            else
                            {
                                LELog.IsLineManager = false;
                                LELog.HRAdmin = 0;
                            }
                            if (EntryID > 0)
                            {
                                LELog.Action = "Update";
                            }
                            else
                            {
                                LELog.Action = "Insert";
                            }



                            db.vt_tbl_LoanEntryLog.Add(LELog);
                            db.SaveChanges();
                            #endregion

                        }
                        else
                        {
                            loneentry.LogEntryID = EntryID;
                            loneentry.CompanyID = Convert.ToInt32(Session["CompanyId"]);
                            loneentry.AppliedDate = vt_Common.CheckDateTime(txtDate.Text);
                            loneentry.EmployeeId = (int)Session["UserId"];
                            loneentry.LoanId = vt_Common.CheckInt(ddlLoanType.SelectedValue);
                            loneentry.AppliedAmount = vt_Common.Checkdecimal(txtLoanAmount.Text);
                            loneentry.Description = TxtDescription.Text;
                            loneentry.EMIType = ddlEMIType.SelectedValue;
                            loneentry.MonthDuration = vt_Common.CheckInt(txtMonths.Text);
                            loneentry.LineManager = 1;
                            loneentry.EntryDate = EntryDate;
                            db.SaveChanges();

                        }



                        ClearForm();
                        //LoadData();
                        Bind_GV();
                        Bind_GVLLog();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, "Record", "Successfully Saved!");
                    }
                    else
                    {
                        vt_Common.ReloadJS(this.Page, "showMessage('You already take loan');");
                    }
                }
                else
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "Please select dec!");
                }
            }



        }
        catch (DbUpdateException ex)
        {
        }
    }
    protected void btnAddnew_ServerClick(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        foreach (DataRow Row in Dt.Rows)
        {
            if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Insert"].ToString() == "True")
            {
                ViewState["PageID"] = null;
                vt_Common.Clear(pnlDetail.Controls);
                UpDetail.Update();
            }
            else if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Insert"].ToString() == "False")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ddlEmployee.Enabled = true;
        TxtDescription.Enabled = true;
        txtDate.Enabled = true;
        txtLoanAmount.Enabled = true;
        txtMonths.Enabled = true;
        ddlLoanType.Enabled = true;
        divloan.Visible = false;
        txtnotdedctiondate.Visible = false;
        DataTable ds = new DataTable();
        ds = null;
        grdnotdeductionmonth.DataSource = ds;
        grdnotdeductionmonth.DataBind();
        ClearForm();
    }
    private void Bind_GVLLog()
    {
        DataSet Ds = ProcedureCall.Sp_Call_vt_Sp_BindLoanEntryLog();
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
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
    private void Bind_GV()
    {
        DataSet Ds = ProcedureCall.SpCall_VT_SP_GetLoanEntry(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]);
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                grdLoanEntry.DataSource = Dt;
            }
            else
            {
                grdLoanEntry.DataSource = null;
            }
        }
        else
        {
            grdLoanEntry.DataSource = null;
        }
        grdLoanEntry.DataBind();
    }
    #endregion
    #region Healper Method
    void LoadData()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //int companyID = 0;
            //if (Session["CompanyId"] == null)
            //{
            //    companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            //}
            //else
            //{
            //    divCompany.Visible = false;
            //    grdLoanEntry.Columns[1].Visible = false;
            //    companyID = vt_Common.CompanyId;

            var Query = db.VT_SP_GetLoanEntry(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"], 0,4).ToList();
            grdLoanEntry.DataSource = Query;
            grdLoanEntry.DataBind();
            // }
        }

    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#loanentry').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_LoanEntry objloanEntry = db.vt_tbl_LoanEntry.FirstOrDefault(x => x.EntryID == ID);
            vt_tbl_Employee emp = db.vt_tbl_Employee.FirstOrDefault(x => x.EmployeeID == objloanEntry.EmployeeId);
            //ddlcomp.SelectedValue = vt_Common.CheckString(objloanEntry.CompanyID);
            txtDate.Text = vt_Common.CheckString(objloanEntry.AppliedDate);
            BindEmployeeGrid(vt_Common.CheckInt(objloanEntry.CompanyID));
            ddlEmployee.SelectedValue = vt_Common.CheckString(objloanEntry.EmployeeId);
            ddlLoanType.SelectedValue = vt_Common.CheckString(objloanEntry.LoanId);
            txtLoanAmount.Text = objloanEntry.AppliedAmount.ToString();
            ddlEMIType.SelectedValue = objloanEntry.EMIType;
            txtMonths.Text = objloanEntry.MonthDuration.ToString();
            txtEMI.Text = objloanEntry.EMIType;
            TxtDescription.Text = objloanEntry.Description.ToString();
            if (objloanEntry.LoanNotDeductionCheck == true)
            {
                chknloanotdeduction.Checked = true;
                txtnotdedctiondate.Visible = true;
                txtnotdedctiondate.Text =Convert.ToDateTime(objloanEntry.DateDeductionMonth).Date.ToShortDateString();
                    
            }
            else
            {
                chknloanotdeduction.Checked = false;
                txtnotdedctiondate.Visible = false  ;
            }
        }
        ViewState["PageID"] = ID;
    }
    #endregion
    protected void btnAddnew_Click(object sender, EventArgs e)
    {

        DataTable Dt = Session["PagePermissions"] as DataTable;
        foreach (DataRow Row in Dt.Rows)
        {
            if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Insert"].ToString() == "True")
            {
                ViewState["PageID"] = null;
                vt_Common.Clear(pnlDetail.Controls);
                UpDetail.Update();
                //if (Session["CompanyId"] == null)
                /// {
                //if (ddlcomp.Items.FindByValue(ddlCompany.SelectedValue) != null)
                //{
                //  ddlcomp.SelectedValue = ddlCompany.SelectedValue;
                //int EntryID = vt_Common.CheckInt(ddlCompany.SelectedValue);
                BindEmployeeGrid(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]);
                //}
                //vt_Common.ReloadJS(this.Page, "$('#loanentry').modal();");
                //  }
                //  else
                //  {
                // trCompany.Visible = false;
                //  BindEmployeeGrid(vt_Common.CompanyId);
                //trCompany.Visible = false;
                vt_Common.ReloadJS(this.Page, "$('#loanentry').modal();");
                // }
            }
            else if (Row["PageUrl"].ToString() == "LoanEntry.aspx" && Row["Can_Insert"].ToString() == "False")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            }
        }

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadData();
        Bind_GV();
        Bind_GVLLog();
        UpView.Update();
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEmployee.Items.Clear();
        ddlLoanType.Items.Clear();
        if (ddlcomp.SelectedValue != "")
        {
            BindEmployeeGrid(Convert.ToInt32(ddlcomp.SelectedValue));
        }
        else
        {
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
            ddlLoanType.Items.Insert(0, new ListItem("Select Loan Type", ""));
        }
        UpDetail.Update();
    }
    public void BindEmployeeGrid(int CompanyID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var EmployeeList = db.VT_SP_GetEmployees(CompanyID).ToList();
            ddlEmployee.DataSource = EmployeeList;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));

            var LoanRecords = db.vt_tbl_Loan.Where(x => x.CompanyID == CompanyID).ToList();
            ddlLoanType.DataSource = LoanRecords;
            ddlLoanType.DataTextField = "Name";
            ddlLoanType.DataValueField = "LoanID";
            ddlLoanType.DataBind();
            ddlLoanType.Items.Insert(0, new ListItem("Select Loan Type", ""));
        }
    }
    protected void BtnLog_Click(object sender, EventArgs e)
    {
        //Bind_GVLLog();
        Bind_GVLLog();
        UpDetail.Update();
        //UpPanelLog.Update();
        vt_Common.ReloadJS(this.Page, "$('#LoanEntryModal').modal();");
    }

    protected void chknloanotdeduction_CheckedChanged(object sender, EventArgs e)
    {
        if (chknloanotdeduction.Checked ==true)
        {
            txtnotdedctiondate.Visible = true;

        }
        else
        {
            txtnotdedctiondate.Visible = false;

        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {



            int ID = 0;
            ID = Convert.ToInt32(ViewState["hdnID"]);
            int CompanyID = Session["CompanyID"] == null ? 0 : Convert.ToInt32(Session["CompanyID"]);
            string result = string.Empty;
            if (ID > 0)
            {
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    var checkDeprt = db.vt_tbl_Designation.Where(x => x.DepartmentID == ID).FirstOrDefault();
                    var LoanLogs = db.vt_tbl_LoanMonthAmount.Where(x => x.EntryID == ID).ToList();
                    DataTable dt_check = ProcedureCall.DeleteLoanById(ID, CompanyID).Tables[0];

                    //transaction.Commit();
                    if (dt_check.Rows.Count > 0)
                    {


                        foreach (DataRow rows in dt_check.Rows)
                        {
                            result = rows["result"].ToString();
                            if (result == "Success")
                            {
                                MsgBox.Show(Page, MsgBox.success, "", "Successfully Delete");
                                vt_Common.ReloadJS(this.Page, "$('#Delete-Modal').modal('hide');");
                                ClearForm();
                                Bind_GV();
                                Bind_GVLLog();
                                UpView.Update();

                            }
                           
                            else if (result == "CheckApproval")
                            {

                                MsgBox.Show(Page, MsgBox.danger, "!", "You can not delete Approved Loan");
                            }

                            else if (result == "Failed")
                            {
                                MsgBox.Show(Page, MsgBox.danger, "!", "SomeThing went wrong");
                            }
                        }



                    }
                    else
                    {
                        MsgBox.Show(Page, MsgBox.danger, "", "Sorry this department can not be deleted because designation exist");

                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}