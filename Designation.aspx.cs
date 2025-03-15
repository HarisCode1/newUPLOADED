using Newtonsoft.Json;
using System;
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

public partial class Designation : System.Web.UI.Page
{
    public static int CompanyID = 0;
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
           
             CompanyID = Session["CompanyId"] == null ? 0 : Convert.ToInt32(Session["CompanyId"]);
            var query = db.vt_tbl_Company.Where(x => x.CompanyID == CompanyID).FirstOrDefault();
            if (query != null)
            {
                string CompanyName = query.CompanyName;
                lblcompany.Text = CompanyName;
            }

            if (!IsPostBack)
            {
                if ((string)Session["UserName"] != "SuperAdmin")
                {
                    int ModuleID = 9;
                    int RoleID = (int)Session["RoleId"];

                    DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);

                    string PageName = null;
                    if (Ds != null && Ds.Tables.Count > 0)
                    {
                        DataTable Dt = Ds.Tables[0];
                        foreach (DataRow Row in Dt.Rows)
                        {
                            if (Row["PageName"].ToString() == "Designation")
                            {
                                PageName = Row["PageName"].ToString();
                                break;
                            }
                        }
                        if (PageName == "Designation")
                        {
                            LoadData();
                            BindHeadDepartment();
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                    }
                }
                else
                {
                    BtnAddNew.Visible = false;
                    btnSaveCont.Visible = false;    
                    LoadData();
                    BindHeadDepartment();
                    BindTopDesignation();
                    //MsgBox.Show(Page, MsgBox.danger, "", "You are Loged in by SuperAdmin");
                }



            }
            vt_Common.ReloadJS(this.Page, "binddata();");
        }

    }
    #region Control Event

    protected void grdDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteDesignation":
                try
                {
                    using (vt_EMSEntities db = new vt_EMSEntities())
                    {
                        int ID = vt_Common.CheckInt(e.CommandArgument);
                        vt_tbl_Designation b = db.vt_tbl_Designation.FirstOrDefault(x => x.DesignationID == ID);
                        db.vt_tbl_Designation.Remove(b);
                        db.SaveChanges();
                        LoadData();
                        UpView.Update();
                        MsgBox.Show(Page, MsgBox.success, b.Designation, "Successfully Deleted");
                    }
                }
                catch (DbUpdateException ex)
                {
                    SqlException innerException = ex.GetBaseException() as SqlException;
                    vt_Common.PrintfriendlySqlException(innerException, Page);
                }
                break;
            case "EditDesignation":
                FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                vt_Common.ReloadJS(this.Page, "$('#ModalDesignation').modal();");
                UpDetail.Update();
                break;
            default:
                break;
        }
    }
    //public static bool Delete(int Des)
    //{
    //    vt_EMSEntities db = new vt_EMSEntities();
    //    vt_tbl_Designation Obj = db.vt_tbl_Designation.Where(x => x.DesignationID == Des).FirstOrDefault();
    //    if (Obj != null)
    //    {
    //        Obj.IsActive = false;
    //        db.Entry(Obj).State = System.Data.Entity.EntityState.Deleted;
    //        db.SaveChanges();
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}

    //Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                btnSave.Enabled = false;
                    int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int companyID = 0;
                if (Session["CompanyId"] != null)
                {
                    companyID = (Convert.ToInt32(Session["CompanyId"]));
                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlCompany.SelectedValue);
                }
                int DepID = Convert.ToInt32(ddlDepartment.SelectedValue);
                int TopDes = Convert.ToInt32(ddlDesignation.SelectedValue);
                //int TOE = Convert.ToInt32(DdlTypeOfEmployees.SelectedValue);
                //int TOE = 1;

                //var record = db.vt_tbl_Designation.FirstOrDefault(o => o.DesignationID != recordID && o.CompanyID == companyID && o.Designation.ToLower().Replace(" ", "").Equals(txtDesignationid.Text.ToLower().Replace(" ", "")));
                var record = db.vt_tbl_Designation.Where(x => x.DepartmentID == DepID && x.Designation == txtDesignationid.Text && x.TopDesignationID == TopDes).ToList();

                if (record.Count > 0)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Designation with the same name already exist');");
                }
                else
                {
                    vt_tbl_Designation b = new vt_tbl_Designation();
                    b.CompanyID = companyID;
                    b.Designation = txtDesignationid.Text;
                    b.TypeOfEmployeeID = 1;
                    b.IsActive = true;
                    b.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    b.HeadDepartmentId = Convert.ToInt32(ddlheaddeparment.SelectedValue);
                    if (ddlDesignation.SelectedValue == "0")
                    {
                        b.TopDesignationID = 0;
                        b.ReportTo =Convert.ToInt32(ddltopdesignations.SelectedValue);
                        //b.parentKey = 0;
                    }
                    else
                    {
                        //int parent = 
                        b.TopDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        b.ReportTo=0;
                        var testRecord = db.vt_tbl_Designation.Where(x => x.DesignationID == b.TopDesignationID).ToList();
                        b.parentKey = testRecord[0].parentKey;
                    }
                    vt_tbl_Designation a = new vt_tbl_Designation();
                    if (ViewState["PageID"] != null)
                    {
                        b.DesignationID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        a = db.vt_tbl_Designation.Add(b);
                    }

                    db.SaveChanges();

                    if (ddlDesignation.SelectedValue == "0")
                    {
                        a.parentKey = a.DesignationID;
                        db.Entry(a).State =System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    //}else
                    //{
                    //    int parent = Convert.ToInt32(ddlDesignation.SelectedValue);
                    //    a.parentKey = db.vt_tbl_Designation.Where(x => x.DesignationID.Equals(parent)).SingleOrDefault().parentKey;
                    //}
                    //db.vt_tbl_Designation;

                    MsgBox.Show(Page, MsgBox.success, txtDesignationid.Text, "Successfully Save");
                    ClearForm();
                    //ClearFormAndOper();
                    //Load();
                    ddlCompany.SelectedIndex = 0;
                    UpDetail.Update();
                    UpView.Update();
                    LoadData();


                }
            }
        }
        catch (DbUpdateException ex)
        {
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
        Response.Redirect("Designation.aspx");
    }
    //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        ddlDepartment.Items.Clear();
    //        ddlDesignation.Items.Clear();
    //        if (ddlCompany.SelectedValue != "")
    //        {
    //            int ID = Convert.ToInt32(ddlCompany.SelectedValue);
    //            var Query = db.VT_SP_GetDepartment(ID).ToList();
    //            ddlDepartment.DataSource = Query;
    //            ddlDepartment.DataTextField = "Department";
    //            ddlDepartment.DataValueField = "DepartmentID";
    //            ddlDepartment.DataBind();
    //            ddlDepartment.Items.Insert(0, new ListItem("Please Select", "0"));
    //            BindDesignation(ID);
    //        }
    //        UpView.Update();
    //    }
    //}
    public void BindDep()
    {
        //int companyID = 0;
        //companyID = (Convert.ToInt32(Session["CompanyId"]));
        //SqlParameter[] param =
        //{
        //    new SqlParameter("@CompanyID",companyID)
        //};
        //vt_Common.Bind_DropDown(ddlDepartment, "VT_SP_BindDepart", "Department", "DepartmentID", param);
    }
    public void BindTypeOfEmployee()
    {
        //vt_Common.Bind_DropDown(DdlTypeOfEmployees, "Bind_vt_tbl_TypeofEmployee", "type", "Id");
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDesignation.Enabled = true;

        int DepID = 0;
        DepID = Convert.ToInt32(ddlDepartment.SelectedValue);
        SqlParameter[] param =
        {
            new SqlParameter("@DepID",DepID)
             //new SqlParameter("@CompanyID",CompanyID)

        };
        vt_Common.Bind_DropDown(ddlDesignation, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);
        //BindTopDesignation();
        if (ddlDesignation.Items.Count == 1)
        {
            //for top designation

            ListItem item = new ListItem("Self", "0");
            ddltopdesignations.Visible = true;
            ddlDesignation.Visible = true;

            BindTopDesignation();
            ddlDesignation.Items.Insert(0, item);
            ddlDesignation.Visible = false;
            ddlDesignation.SelectedValue = 0.ToString();
        }

        else
        {
            ddlDesignation.Visible = true;
            ddltopdesignations.Visible = false;
        }
    }
    public void BindTopDesignation()
    {
   
        var query = db.vt_tbl_TopDesignations.ToList();
        if (query != null)
        {
            ddltopdesignations.DataSource = query;
            ddltopdesignations.DataTextField = "TopDesignations";
            ddltopdesignations.DataValueField = "Id";
            ddltopdesignations.DataBind();
            ddltopdesignations.Items.Insert(0, new ListItem("--Select Top Designation--", "0"));

        }

    }

    void Bind_DdlDesignation(int DepID)
    {
        DepID = Convert.ToInt32(ddlDepartment.SelectedValue);
        SqlParameter[] param =
        {
            new SqlParameter("@DepID",DepID)


    };
        vt_Common.Bind_DropDown(ddlDesignation, "VT_SP_BindDesigByDepID", "Designation", "DesignationID", param);
        if (ddlDesignation.Items.Count == 1)
        {
            ListItem item = new ListItem("Self", "0");
            ddlDesignation.Items.Insert(0, item);
            ddlDesignation.Enabled = false;
            ddlDesignation.SelectedValue = 0.ToString();
        }
    }
    private void Bind_GV()
    {
        DataSet Ds = ProcedureCall.SpCall_VT_SP_GetDesignation();
        if (Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null && Dt.Rows.Count > 0)
            {
                grdDesignation.DataSource = Dt;
            }
            else
            {
                grdDesignation.DataSource = null;
            }
        }
        else
        {
            grdDesignation.DataSource = null;
        }
        grdDesignation.DataBind();

    }
    #endregion
    #region Healper Method    
    void LoadData()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (Session["CompanyId"] == null)
            {
                var Query = db.VT_SP_GetDesignation(Convert.ToInt32(ddlComp.SelectedValue)).ToList();
                grdDesignation.DataSource = Query;
                grdDesignation.DataBind();
            }
            else
            {
                grdDesignation.Columns[1].Visible = false;
                var Query = db.VT_SP_GetDesignation(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                grdDesignation.DataSource = Query;
                grdDesignation.DataBind();
                //var Department = db.VT_SP_GetDepartment(Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"]).ToList();
                //ddlDepartment.DataSource = Department;
                //ddlDepartment.DataTextField = "Department";
                //ddlDepartment.DataValueField = "DepartmentID";
                //ddlDepartment.DataBind();
                //BindDesignation(Convert.ToInt32(Session["CompanyId"]));
                trGridCompany.Visible = false;
                //Salman Code
                //SetDdl_value(ddlComp, Session["CompanyId"].ToString());
            }
        }
        BindDep();
        BindTypeOfEmployee();
    }
    void ClearFormAndOper()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        ddlDesignation.Text = null;
        ddlDesignation.Enabled = false;
        vt_Common.ReloadJS(this.Page, "$('#ModalDesignation').modal();");
    }
    void ClearForm()
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#ModalDesignation').modal('hide');");
    }
    void FillDetailForm(int ID)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            vt_tbl_Designation Des = db.vt_tbl_Designation.Where(x => x.DesignationID == ID).FirstOrDefault();
            txtDesignationid.Text = Des.Designation;
            ddlDepartment.SelectedValue = vt_Common.CheckString(Des.DepartmentID);
            Bind_DdlDesignation(Convert.ToInt32(ddlDepartment.SelectedValue));

            ddlDesignation.SelectedValue = vt_Common.CheckString(Des.TopDesignationID);
            if (Des.TopDesignationID == 0)
            {
                //ListItem item = new ListItem("Please Select", "0");
                ListItem item = new ListItem("Self", "0");

                ddlDesignation.Items.Insert(0, item);
                //ddlDesignation.Items.Insert(1, item2);

                ddlDesignation.Enabled = false;
                ddlDesignation.SelectedValue = 0.ToString();
            }

            //vt_tbl_Designation d = db.vt_tbl_Designation.FirstOrDefault(x => x.DesignationID == ID);
            //vt_tbl_Company c = db.vt_tbl_Company.FirstOrDefault(x => x.CompanyID == d.CompanyID);
            //var query = (from D in db.vt_tbl_Designation
            //             join C in db.vt_tbl_Company on D.CompanyID equals C.CompanyID
            //             where D.DesignationID == ID
            //             select new
            //             {
            //                 D.DesignationID,
            //                 D.Designation,
            //                 C.CompanyName,
            //                 D.CompanyID,
            //                 D.DepartmentID,
            //                 D.TopDesignationID
            //             }).SingleOrDefault();

            //txtDesignationid.Text = query.Designation;
            //ddlCompany.SelectedValue = query.CompanyID.ToString();
            //ddlDepartment.Items.Clear();
            //ddlDesignation.Items.Clear();
            //bindDropDown(Convert.ToInt32(ddlCompany.SelectedValue));

            //ddlDepartment.SelectedValue = query.DepartmentID.ToString();
            //ddlDesignation.SelectedValue = query.TopDesignationID.ToString();

            //if (ddlDepartment.Items.FindByValue(query.DepartmentID.ToString()) != null)
            //{
            //    ddlDepartment.SelectedValue = query.DepartmentID.ToString();
            //}
            //if (query.TopDesignationID != null)
            //{
            //    ddlDesignation.SelectedValue = query.TopDesignationID.ToString();
            //}
            ViewState["PageID"] = ID;
        }
    }

    //public void BindDD()
    //{
    //    vt_Common.Bind_DropDown(ddlCompany, "GetAllCompanies", "CompanyName", "CompanyID");
    //}
    #endregion
    protected void btnAddNew_OnClick(object sender, EventArgs e)
    {
        if ((string)Session["UserName"] != "SuperAdmin")
        {


            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Designation.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    ViewState["PageID"] = null;
                    vt_Common.Clear(pnlDetail.Controls);
                    UpDetail.Update();
                    //ddlDepartment.Items.Clear();
                    ddlDesignation.Items.Clear();
                    ddlDesignation.Enabled = false;
                    //if (((EMS_Session)Session["EMS_Session"]).Company == null)
                    if (Session["CompanyId"] == null)
                    {
                        if (ddlCompany.Items.FindByValue(ddlComp.SelectedValue) != null)
                        {
                            ddlCompany.SelectedValue = ddlComp.SelectedValue;
                            if (ddlCompany.SelectedValue != "0")
                            {
                                using (vt_EMSEntities db = new vt_EMSEntities())
                                {
                                    int ID = Convert.ToInt32(ddlCompany.SelectedValue);
                                    //var Query = db.VT_SP_GetDepartment(ID).ToList();
                                    //ddlDepartment.DataSource = Query;
                                    //ddlDepartment.DataTextField = "Department";
                                    //ddlDepartment.DataValueField = "DepartmentID";
                                    //ddlDepartment.DataBind();
                                    ddlDepartment.Items.Insert(0, new ListItem("Please Select", "0"));
                                    //BindDesignation(ID);
                                }
                            }
                        }
                        vt_Common.ReloadJS(this.Page, "$('#ModalDesignation').modal()");
                    }
                    else
                    {
                        trCompany.Visible = true;
                        ddlCompany.Visible = true;
                        vt_Common.ReloadJS(this.Page, "$('#ModalDesignation').modal()");
                    }
                    UpDetail.Update();
                }
                else if (Row["PageUrl"].ToString() == "Designation.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                }
            }
        }
        else
        {
            MsgBox.Show(Page, MsgBox.danger, "", "You are Loged in by SuperAdmin");
        }

    }
    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            var Query = db.VT_SP_GetDesignation(Convert.ToInt32(ddlComp.SelectedValue));
            grdDesignation.DataSource = Query;
            grdDesignation.DataBind();
           // UpView.Update();
        }
    }
    //private void bindDropDown(int CompanyID)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        var Department = db.VT_SP_GetDepartment(CompanyID).ToList();
    //        ddlDepartment.DataSource = Department;
    //        ddlDepartment.DataTextField = "Department";
    //        ddlDepartment.DataValueField = "DepartmentID";
    //        ddlDepartment.DataBind();
    //        ddlDepartment.Items.Insert(0, new ListItem("Please Select", "0"));
    //        BindDesignation(CompanyID);
    //    }
    //}
    //private void BindDesignation(int CompanyID)
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        var Designation = db.VT_SP_GetDesignation(CompanyID).ToList();
    //        ListItem item = new ListItem("Self", "0");
    //        ddlDesignation.Items.Insert(0, item);
    //        ddlDesignation.DataSource = Designation;
    //        ddlDesignation.DataTextField = "Designation";
    //        ddlDesignation.DataValueField = "DesignationID";
    //        ddlDesignation.DataBind();
    //        ddlDesignation.Items.Insert(0, new ListItem("Please Select", "0"));
    //    }
    //}
    protected void grdDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    if (Session["CompanyId"] == null)
                    {
                        Label lblCompany = (Label)e.Row.FindControl("lblCompany");
                        if (lblCompany != null)
                        {
                            int CompanyID = (int)DataBinder.Eval(e.Row.DataItem, "CompanyID");
                            var Query = db.vt_tbl_Company.FirstOrDefault(o => o.CompanyID == CompanyID);
                            if (Query != null)
                            {
                                lblCompany.Text = Query.CompanyName;
                            }
                        }
                    }
                    Label lblDepartment = (Label)e.Row.FindControl("lblDepartment");
                    if (lblDepartment != null)
                    {
                        int DepartmentID = (int)DataBinder.Eval(e.Row.DataItem, "DepartmentID");
                        var Query = db.vt_tbl_Department.FirstOrDefault(o => o.DepartmentID == DepartmentID);
                        if (Query != null)
                        {
                            lblDepartment.Text = Query.Department;
                        }
                    }
                    Label lblTopDesignation = (Label)e.Row.FindControl("lblTopDesignation");
                    if (lblTopDesignation != null)
                    {
                        int DesignationID = (int)DataBinder.Eval(e.Row.DataItem, "DesignationID");
                        var Query = db.vt_tbl_Designation.FirstOrDefault(o => o.DesignationID == DesignationID);
                        if (Query != null)
                        {
                            var Query1 = db.vt_tbl_Designation.FirstOrDefault(o => o.DesignationID == Query.TopDesignationID);
                            if (Query1 != null)
                            {
                                lblTopDesignation.Text = Query1.Designation;
                            }
                        }
                    }
                }
            }
        }
    }

    void SetDdl_value(DropDownList Ddl, string Value)
    {
        for (int i = 0; i < Ddl.Items.Count; i++)
        {
            if (Ddl.Items[i].Value == Value)
            {
                Ddl.SelectedValue = Value;
                break;
            }
            else
            {
                Ddl.SelectedIndex = 0;
            }
        }
    }

    #region Salman Code

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Load()
    {
        int companyID = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
        string List;
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                DataSet Ds = ProcedureCall.SpCall_VT_SP_GetDesignation(companyID);
                //var Query = db.VT_SP_GetDesignation(companyID).ToList();
                List = JsonConvert.SerializeObject(Ds.Tables[0]);
            }

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        return List;
    }

    #endregion


    protected void btnSaveCont_Click(object sender, EventArgs e)
    {
        try
        {
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                int recordID = vt_Common.CheckInt(ViewState["PageID"]);
                int companyID = 0;
                if (Session["CompanyId"] != null)
                {
                    companyID = (Convert.ToInt32(Session["CompanyId"]));
                }
                else
                {
                    companyID = vt_Common.CheckInt(ddlCompany.SelectedValue);
                }
                int DepID = Convert.ToInt32(ddlDepartment.SelectedValue);
                int TopDes = Convert.ToInt32(ddlDesignation.SelectedValue);


                //var record = db.vt_tbl_Designation.FirstOrDefault(o => o.DesignationID != recordID && o.CompanyID == companyID && o.Designation.ToLower().Replace(" ", "").Equals(txtDesignationid.Text.ToLower().Replace(" ", "")));
                var record = db.vt_tbl_Designation.Where(x => x.DepartmentID == DepID && x.Designation == txtDesignationid.Text && x.TopDesignationID == TopDes).ToList();

                if (record.Count > 0)
                {
                    vt_Common.ReloadJS(this.Page, "showMessage('Designation with the same name already exist');");
                }
                else
                {
                    vt_tbl_Designation b = new vt_tbl_Designation();
                    b.CompanyID = companyID;
                    b.Designation = txtDesignationid.Text;
                    b.IsLineManager = ChkIsLineManager.Checked;
                    b.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    b.IsActive = true;
                    if (ddlDesignation.SelectedValue == "0")
                    {
                        b.TopDesignationID = 0;

                        b.ReportTo = Convert.ToInt32(ddltopdesignations.SelectedValue);
                    }
                    else
                    {
                        b.TopDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                        var testRecord = db.vt_tbl_Designation.Where(x => x.DesignationID == b.TopDesignationID).ToList();
                        b.parentKey = testRecord[0].parentKey;
                    }
                    vt_tbl_Designation a = new vt_tbl_Designation();
                    if (ViewState["PageID"] != null)
                    {
                        b.DesignationID = vt_Common.CheckInt(ViewState["PageID"]);
                        db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        a = db.vt_tbl_Designation.Add(b);
                    }
                    db.SaveChanges();
                    if (ddlDesignation.SelectedValue == "0")
                    {
                        a.parentKey = a.DesignationID;
                        db.Entry(a).State =System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    //  db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, txtDesignationid.Text, "Successfully Save");
                    //ClearForm();
                    ClearFormAndOper();
                    //Load();
                    ddlCompany.SelectedIndex = 0;
                    UpDetail.Update();
                    UpView.Update();
                    LoadData();
                    Response.Redirect("Designation.aspx");


                }
            }
        }
        catch (DbUpdateException ex)
        {
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Delete(int DesignationID)
    {
        string List = "";
        try
        {

            if (DesignationID >0 )
            {
                using (vt_EMSEntities db = new vt_EMSEntities())
                {
                    var check = db.vt_tbl_Employee.Where(x => x.DesignationID == DesignationID).FirstOrDefault();
                    if (check == null)
                    {

                        if (DesignationID == 50000 || DesignationID == 60000 || DesignationID == 70000)

                        {
                            //BtnSave.Enabled = false;
                            List = "failed";
                        }
                        else

                        {
                            if (DesignationID != 0)
                            {
                                ProcedureCall.vt_sp_deleteParentandChilddesignationById(DesignationID, CompanyID);
                                List = "success";
                            }
                            else
                            {
                                List = "failed";
                            }
                        }
                    }
                    else
                    {
                        List = "Exist";
                    }
                }
            }
           
        }
        catch (Exception ex)
        {

            
        }
        return List;
        //vt_tbl_Designation Obj = db.vt_tbl_Designation.Where(x => x.DesignationID == DesignationID && x.parentKey==DesignationID).FirstOrDefault();
        //ProcedureCall.vt_sp_deleteParentandChilddesignationById(1,2);
        //if (Obj != null)
        //{
        //    //Obj.IsActive = false;
        //    //db.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
        //    //db.vt_tbl_Designation.Remove(Obj);
        //    //db.SaveChanges();
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

    }
    public void BindHeadDepartment()
    {
        try
        {
            var query = db.vt_tbl_HeadDepartment.ToList();
            if (query != null)
            {

                ddlheaddeparment.DataSource = query;
                ddlheaddeparment.DataTextField = "HeadDeparment";
                ddlheaddeparment.DataValueField = "Id";
                ddlheaddeparment.DataBind();
                ddlheaddeparment.Items.Insert(0, new ListItem("--Select Head Department--", "0"));
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }
    protected void ddlheaddeparment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int companyID = 0;
            companyID = (Convert.ToInt32(Session["CompanyId"]));
            int HeaddeprtId = 0;

            HeaddeprtId = Convert.ToInt32(ddlheaddeparment.SelectedValue);
            var query = db.vt_tbl_Department.Where(x => x.HeadDepartmentId == HeaddeprtId && x.CompanyID==companyID).ToList();
            ddltopdesignations.Visible = false;
            if (query != null)
            {
              
                ddlDepartment.DataSource = query;
                ddlDepartment.DataTextField = "Department";
                ddlDepartment.DataValueField = "DepartmentID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
            }
            
        }
        catch (Exception)
        {

            throw;
        }
        //SqlParameter[] param =
        //{
        //    new SqlParameter("@CompanyID",companyID)
        //};
        //vt_Common.Bind_DropDown(ddlDepartment, "VT_SP_BindDepart", "Department", "DepartmentID", param);

    }
}