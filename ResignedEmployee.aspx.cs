using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResignedEmployee : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                LoadData();

            }

        }
        if (!IsPostBack)
        {
            LoadData();

        }

    }

    void LoadData()
    {
        int Companyid = Convert.ToInt32(Session["CompanyId"]);
        DataTable dt = ProcedureCall.GetSp_GetResignedEmployeeByCompanyID(Companyid).Tables[0];
        if (dt.Rows.Count > 0)
        {
            grdresgemp.DataSource = dt;
            grdresgemp.DataBind();

        }
        //var query = from r in db.vt_tbl_Resignations
        //            join e in db.vt_tbl_Employee on r.EmployeeId equals e.EmployeeID
        //            join eos in db.vt_tbl_EndOfServices on e.EmployeeID equals eos.EmpId
        //            where r.CompanyId == Companyid && r.Status == "Resigned"
        //            select new
        //            {
        //                r.ResignationId,
        //                r.EmployeeId,
        //                e.EnrollId,
        //                EmployeeName = e.FirstName +' '+e.LastName,
        //                r.Reason,
        //                r.Remarks,
        //                r.Status,
        //                r.AppliedDate,
        //                r.Image,
        //                eos.TerminationDate

        //            };
  

    }

    protected void lbtndetail_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int EmpID = Convert.ToInt32(e.CommandArgument);
            if (EmpID != 0)
            {
                Response.Redirect("Employes_Details.aspx?ID=" + EmpID);
            }
           
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}