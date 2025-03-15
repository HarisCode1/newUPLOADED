using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class DesignationEdit : System.Web.UI.Page
{
    int ID = 0;
    int companyID = 0;
    vt_EMSEntities db = new vt_EMSEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
       

        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]);
                companyID =Convert.ToInt32(Session["CompanyID"]);
                if (ID > 0)
                {
                    BindTopDesignation();
                    FillForm(ID, sender, e);
                    //if (Session["UserName"].ToString() == "SuperAdmin")
                    //{
                    //    MsgBox.Show(Page, MsgBox.danger," ", "Sorry You have not rights");
                    //}
                    //else
                    //{
                    //    foreach (DataRow Row in Dt.Rows)
                    //    {
                    //        //if (Row["PageUrl"].ToString() == "Designation.aspx" && Row["Can_Update"].ToString() == "True")
                    //        //{

                    //        //}
                    //        //else if (Row["PageUrl"].ToString() == "Designation.aspx" && Row["Can_Update"].ToString() == "False")
                    //        //{
                    //        //    Response.Redirect("Designation.aspx");
                    //        //    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                    //        //}
                    //    }

                    //}
                }
            }
        }
    }
    public void FillForm(int ID, object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            if (ID == 50000 || ID == 60000 || ID == 70000)

            {
                BtnSave.Enabled = false;    
                MsgBox.Show(this.Page, MsgBox.danger, "", "Sorry there is no designation of  main head department You can not edit !");
            }
            else

            {
                vt_tbl_Designation des = db.vt_tbl_Designation.FirstOrDefault(x => x.DesignationID == ID);

                BindDep();
                if (des.TopDesignationID == 0)
                {
                    //ddlDepartment.Enabled = false;
                }
                ddlDepartment.SelectedValue = des.DepartmentID.ToString();
                BindTypeOfEmployee();
                DdlTypeOfEmployees.SelectedValue = des.TypeOfEmployeeID.ToString();
                if (des.TopDesignationID == 0)
                {
                    ListItem item = new ListItem("Self", "0");
                    ddlDesignation.Items.Insert(0, item);
                    //ddlDesignation.Enabled = false;
                    ddlDesignation.SelectedValue = 0.ToString();
                    topdesignaton.Visible = false;
                }
                else
                {
                    ddlDepartment_SelectedIndexChanged(sender, e);
                    ddlDesignation.SelectedValue = des.TopDesignationID.ToString();
                }
                txtDesignationid.Text = des.Designation;
                ChkIsLineManager.Checked = des.IsLineManager == true ? true : false;

            }
          
        }


    }
    public void BindTopDesignation()
    {
        try
        {
            vt_EMSEntities db = new vt_EMSEntities();
            var query = db.vt_tbl_Designation.Where(x => x.DesignationID == ID).FirstOrDefault();
            int ReportToId = 0;
            if (query != null)
            {
                ReportToId =Convert.ToInt32(query.ReportTo);
                if (ReportToId != 0)
                {
                    vt_Common.Bind_DropDown(ddltopdesignation, "vt_sp_gettopdesignation", "topdesignations", "Id");
                    ddltopdesignation.SelectedValue = ReportToId.ToString();
                    ddltopdesignation.Items.RemoveAt(0);


                }
                else
                {
                    reportto.Visible = false;
                }

            }
            else
            {
               
               // vt_Common.Bind_DropDown(ddltopdesignation, "vt_sp_gettopdesignation", "topdesignations", "Id");

            }
  
          



        }
        catch (Exception)
        {

            throw;
        }
    }
   

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Designation.aspx");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            vt_tbl_Designation Data = db.vt_tbl_Designation.Where(x => x.DesignationID == ID).FirstOrDefault();
            if(Data !=null)
            {
                int deptid = Convert.ToInt32(ddlDepartment.SelectedValue);
                var qryheaddeptid = db.vt_tbl_Department.Where(x =>x.DepartmentID ==deptid).FirstOrDefault();
                if (qryheaddeptid !=null)
                {
                    Data.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                    Data.TypeOfEmployeeID = Convert.ToInt32(DdlTypeOfEmployees.SelectedValue);
                    Data.TopDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                    Data.Designation = txtDesignationid.Text;
                    Data.HeadDepartmentId = qryheaddeptid.HeadDepartmentId;
                    Data.IsActive = true;
                    Data.IsLineManager = ChkIsLineManager.Checked;
                    Data.ReportTo = (ddltopdesignation.SelectedValue).ToString() == "" ? 0 : Convert.ToInt32(ddltopdesignation.SelectedValue);
                    db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Response.Redirect("Designation.aspx");
                }
              
            }
         

        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlDesignation.Enabled = true;

        int DepID = 0;
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
            ddlDesignation.SelectedValue = 0.ToString();
            reportto.Visible=true;
            var qrygettopdesignatins = db.vt_tbl_TopDesignations.ToList();
            if (qrygettopdesignatins.Count > 0)
            {
                ddltopdesignation.DataSource = qrygettopdesignatins;
                ddltopdesignation.DataTextField = "Topdesignations";
                ddltopdesignation.DataValueField = "Id";
                ddltopdesignation.DataBind();
                topdesignaton.Visible = false;

            }
            else
            {

            }
            
        }
        else
        {
            reportto.Visible = false;
            ddltopdesignation.Items.Clear();
            topdesignaton.Visible = true;

        }
    }
    public void BindDep()
    {
        int companyID = 0;
        companyID = (Convert.ToInt32(Session["CompanyId"]));
        SqlParameter[] param =
        {
            new SqlParameter("@CompanyID",companyID)
        };
        vt_Common.Bind_DropDown(ddlDepartment, "VT_SP_BindDepart", "Department", "DepartmentID", param);
    }
    public void BindTypeOfEmployee()
    {
        vt_Common.Bind_DropDown(DdlTypeOfEmployees, "Bind_vt_tbl_TypeofEmployee", "type", "Id");
    }
}