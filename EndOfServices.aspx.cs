using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EndOfServices : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32(Request.QueryString["ID"]);

        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                DataBindTable();
                BindListItems();
                //  Fill_LstDesignation();
                if (ID > 0)
                {
                    //       FillDetails(ID);
                    //  Label1.Text =Convert.ToInt32(ID).ToString();
                }
            }
        }


    }

    void DataBindTable()
    {
        int ID = Convert.ToInt32(Request.QueryString["ID"]);
        //var qry = from e in db.vt_tbl_Employee
        //          join d in db.vt_tbl_Designation on e.EmployeeID equals d.TypeOfEmployeeID
        //          join dpt in db.vt_tbl_Department on e.DepartmentID equals dpt.DepartmentID

        //          select new
        //          {
        //              e.EmployeeID,
        //              e.EmployeeName,
        //              d.Designation,
        //              dpt.Department,

        //          };
        var query = from e in db.vt_tbl_Employee
                    join d in db.vt_tbl_Designation on e.DesignationID equals d.DesignationID
                    join dpt in db.vt_tbl_Department on e.DepartmentID equals dpt.DepartmentID
                    join r in db.vt_tbl_Resignations on e.EmployeeID equals r.EmployeeId
                    where e.EmployeeID.Equals(ID)
                    select new
                    {
                        e.EmployeeID,
                        e.EmployeeName,
                        d.Designation,
                        dpt.Department,
                        r.EOSDate

                    };
        foreach (var item in query)
        {
            lblid.Text = Convert.ToInt32(item.EmployeeID).ToString();
            lblname.Text = item.EmployeeName;
            lbldesignation.Text = item.Designation;
            lbldepartment.Text = item.Department;
            lblterminationdate.Text = item.EOSDate.ToString();

        }

    }
    void BindListItems()
    {
        int Id = Convert.ToInt32(Request.QueryString["Id"]);
        DataSet Ds = ProcedureCall.SpCall_Sp_Get_EmpAssets(Id);
        //   DataTable Dt = Ds.Tables[0];
        if (Ds != null)
        {
            DataTable Dt = Ds.Tables[0];
            if (Dt != null)
            {
                //ddlassigneditems.DataSource = Dt;
                //ddlassigneditems.DataValueField = "AssetId";
                //ddlassigneditems.DataTextField = "Name";
            }
            else
            {
                //ddlassigneditems.DataSource = null;
            }
        }
        else
        {
           // ddlassigneditems.DataSource = null;
        }
     //   ddlassigneditems.DataBind();


        //if (ds != null)
        //{
        //    ddlassigneditems.DataValueField = "EmployeeId";
        //    ddlassigneditems.DataTextField = "Name";
        //}
        //else
        //{
        //    ddlassigneditems.DataSource = null;
        //}
        //ddlassigneditems.DataBind();
        //vt_Common.Bind_DropDown(dll, "Sp_Get_AllAssignedAssets", "List_Output", "EmployeeID");
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            bool isresigned = false;
            if (id == 0)
            {
                vt_tbl_EndOfServices eos = new vt_tbl_EndOfServices();
                eos.EmpId = id;
                eos.Name = lblname.Text;
                eos.Designation = lbldesignation.Text;
                eos.Department = lbldepartment.Text;
                //eos.Description = txtdescription.InnerText;
                //eos.DepreciationAmount = Convert.ToDecimal(txtdepreciation.Text);
                eos.TerminationDate = DateTime.Parse(lblterminationdate.Text);
                eos.CreatedDate = DateTime.Now;
                db.vt_tbl_EndOfServices.Add(eos);
                db.SaveChanges();
                vt_tbl_Employee emp = db.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(id)).FirstOrDefault();
                emp.IsResigned = isresigned;
                db.SaveChanges();

            }
            else
            {
                vt_tbl_EndOfServices eos = new vt_tbl_EndOfServices();
                eos.EmpId = id;
                eos.Name = lblname.Text;
                eos.Designation = lbldesignation.Text;
                eos.Department = lbldepartment.Text;
                //eos.Description = txtdescription.InnerText;
                //eos.DepreciationAmount = Convert.ToDecimal(txtdepreciation.Text);
                eos.TerminationDate = DateTime.Parse(lblterminationdate.Text);
                eos.CreatedDate = DateTime.Now;
                db.vt_tbl_EndOfServices.Add(eos);
                db.SaveChanges();
                // Response.Redirect("EndOfServices.aspx");
                //var result = db.Sp_DeleteAsset(assprntid, assprntid);
                //foreach (ListItem item in ddlassigneditems.Items)
                //{

                //    if (item.Selected)
                //    {
                //        bool IsActive = true;

                //        string asstreturn = "Returned";
                //        int asstid = Convert.ToInt32(item.Value);
                //        vt_tbl_AssetMapping am = db.vt_tbl_AssetMapping.Where(x => x.EmpId.Equals(id) && x.AssetId.Equals(asstid)).FirstOrDefault();
                //        am.AssetId = asstid;
                //        am.Status = asstreturn;
                //        db.SaveChanges();
                //        vt_tbl_Assets ast = db.vt_tbl_Assets.Where(x => x.AssetId.Equals(asstid)).FirstOrDefault();
                //        ast.IsActive = IsActive;
                //        db.SaveChanges();
                //        //vt_tbl_Employee emp = db.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(id)).FirstOrDefault();
                //        //emp.IsResigned = isresigned;
                //        //db.SaveChanges();
                //        Response.Redirect("Default.aspx");
                //        MsgBox.Show(Page, MsgBox.success, "Message ", "EOS Terminated");
                //      //  UpdateEos.Update();
                //        ClearForm();
                //        // item ...
                //    }
                //    else
                //    {
                //        string asstreturn = "Not Returned";
                //        bool IsActive = false;
                //        int asstid = Convert.ToInt32(item.Value);
                //        vt_tbl_AssetMapping am = db.vt_tbl_AssetMapping.Where(x => x.EmpId.Equals(id) && x.AssetId.Equals(asstid)).FirstOrDefault();

                //        am.Status = asstreturn;
                //        db.SaveChanges();
                //        vt_tbl_Assets ast = db.vt_tbl_Assets.Where(x => x.AssetId.Equals(asstid)).FirstOrDefault();
                //        ast.IsActive = IsActive;
                //        db.SaveChanges();

                //        Response.Redirect("Default.aspx");
                //        MsgBox.Show(Page, MsgBox.success, "Message ", "EOS Terminated");
                //        UpdateEos.Update();
                //        ClearForm();

                //    }

               





                //else
                //{
                //      foreach (ListItem item in ddlassigneditems.Items)
                //    {
                //        string asstreturn = "Not Returned";
                //        string asststatus = "Inactive";
                //        //int asstid = Convert.ToInt32(ddlassigneditems.SelectedValue);
                //        vt_tbl_AssetMapping am = db.vt_tbl_AssetMapping.Where(x => x.EmpId.Equals(id)).FirstOrDefault();
                //        //am.AssetId = Convert.ToInt32(ddlassigneditems.SelectedValue);
                //        am.Status = asstreturn;
                //        db.SaveChanges();
                //        vt_tbl_Assets asst = db.vt_tbl_Assets.Where(x => x.AssetId.Equals(assi)).FirstOrDefault();
                //        MsgBox.Show(Page, MsgBox.success, "Message ", "EOS Terminated");
                //        UpdateEos.Update();
                //    }

                //}

            }
        }
        catch (Exception ex)
        {

            ErrHandler.TryCatchException(ex);
        }
    }
    void ClearForm()
    {
        //txtdescription.InnerText = "";
        //txtdepreciation.Text = "";
    }

    //private void Fill_LstDesignation()
    //{
    //    var data = from am in db.vt_tbl_AssetMapping
    //               join asst in db.vt_tbl_Assets on am.AssetId equals asst.AssetId
    //               where am.EmpId == 19
    //               select new
    //               {
    //                   am.EmpId,
    //                   asst.Name

    //               };
    //    //var Data = db.vt_tbl_Assets.Where(x =>x.).ToList();

    //    if (data != null)
    //    {
    //        ddlassigneditems.DataValueField = "EmployeeId";
    //        ddlassigneditems.DataTextField = "Name";
    //        ddlassigneditems.DataSource = data;
    //    }
    //    else
    //    {
    //        ddlassigneditems.DataSource = null;
    //    }
    //    ddlassigneditems.DataBind();
    //}


    //private void Bind_Pages()
    //{
    //    int ModuleID = 9;
    //    int RoleID = (int)Session["RoleId"];
    //    vt_EMSEntities db = new vt_EMSEntities();
    //    DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);
    //    if (Ds != null && Ds.Tables.Count > 0)
    //    {
    //        DataTable Dt = Ds.Tables[0];


    //        foreach (DataRow Row in Dt.Rows)
    //        {

    //            if (Row["PageName"].ToString() == "PF")
    //            {
    //                //PFbtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Company")
    //            {
    //                Companybtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Designation")
    //            {
    //                Designation.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Loan Category")
    //            {
    //                Loanbtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Branch")
    //            {
    //                Branchbtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Tax")
    //            {
    //                TaxMasterBtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Leave")
    //            {
    //                Leavebtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Department")
    //            {
    //                Departmentbtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Holiday")
    //            {
    //                Holidaybtn.Visible = true;
    //            }
    //            else if (Row["PageName"].ToString() == "Employee Type")
    //            {
    //                EmployeeTypebtn.Visible = true;
    //            }
    //            //else if (Row["PageName"].ToString() == "Type Of Employee")
    //            //{
    //            //    EmployeeTypebtn.Visible = true;
    //            //}
    //        }
    //    }
    //}
}