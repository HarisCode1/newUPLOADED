using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employee_Promotion : System.Web.UI.Page
{
    private int ID = 0;
    DateTime EntryDate = DateTime.Now;
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            if (!IsPostBack)
            {
                TxtEffectiveDate.Text = EntryDate.ToString("MM/dd/yyyy");
                Bind_GvLog();
                if (ID > 0)
                {
                    FillDetails(ID);
                }
            }
        }
    }

    protected void BtnLog_Click(object sender, EventArgs e)
    {
        UpDetail.Update();
        vt_Common.ReloadJS(this.Page, "$('#employes').modal();");
    }

    private void Bind_GvLog()
    {
        vt_EMSEntities db = new vt_EMSEntities();
        DataSet Ds = ProcedureCall.SpCall_Sp_Get_Employee_PromotionLog();
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

    private void FillDetails(int ID)
    {
        vt_tbl_Employee EmpData = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (EmpData != null)
        {
            //if (EntryDate > EmpData.Promotion_EffictiveDate)
            //{
            //    TxtBasicSalary.Text = Convert.ToDecimal(EmpData.Promotion_BasicSalary).ToString();
            //    TxtHouseRentAllowance.Text = Convert.ToDecimal(EmpData.Promotion_HouseRentAllowance).ToString();
            //    TxtMedicalAllowance.Text = Convert.ToDecimal(EmpData.Promotion_MedicalAllowance).ToString();
            //    TxtTransportAllowance.Text = Convert.ToDecimal(EmpData.Promotion_TransportAllowance).ToString();
            //    TxtFuelAllowance.Text = Convert.ToDecimal(EmpData.Promotion_FuelAllowance).ToString();
            //    TxtSpecialAllowance.Text = Convert.ToDecimal(EmpData.Promotion_SpecialAllowance).ToString();
            //}
            //else
            //{
            //    TxtBasicSalary.Text = Convert.ToDecimal(EmpData.BasicSalary).ToString();
            //    TxtHouseRentAllowance.Text = Convert.ToDecimal(EmpData.HouseRentAllownce).ToString();
            //    TxtMedicalAllowance.Text = Convert.ToDecimal(EmpData.MedicalAllowance).ToString();
            //    TxtTransportAllowance.Text = Convert.ToDecimal(EmpData.TransportAllownce).ToString();
            //    TxtFuelAllowance.Text = Convert.ToDecimal(EmpData.FuelAllowance).ToString();
            //    TxtSpecialAllowance.Text = Convert.ToDecimal(EmpData.SpecialAllowance).ToString();
            //}
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Vt_Tbl_Employee_PromotionLog PromotionLog = new Vt_Tbl_Employee_PromotionLog();
        vt_tbl_Employee Employee = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (Employee != null)
        {
            //Employee.IsPromoted = true;
            //Employee.Promotion_Type = DdlType.SelectedValue;
            //Employee.PromotionDate = EntryDate;
            //Employee.Promotion_EffictiveDate = DateTime.ParseExact(TxtEffectiveDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //Employee.Promotion_BasicSalary = Convert.ToDecimal(TxtBasicSalary.Text);
            //Employee.Promotion_HouseRentAllowance = Convert.ToDecimal(TxtHouseRentAllowance.Text);
            //Employee.Promotion_MedicalAllowance = Convert.ToDecimal(TxtMedicalAllowance.Text);
            //Employee.Promotion_TransportAllowance = Convert.ToDecimal(TxtTransportAllowance.Text);
            //Employee.Promotion_FuelAllowance = Convert.ToDecimal(TxtFuelAllowance.Text);
            //Employee.Promotion_SpecialAllowance = Convert.ToDecimal(TxtSpecialAllowance.Text);
            //db.Entry(Employee).State = System.Data.Entity.EntityState.Modified;

            //Promotion Log
            PromotionLog.EmployeeID = ID;
            PromotionLog.PromotionType = DdlType.SelectedValue;
            PromotionLog.BasicSalary = Convert.ToDecimal(TxtBasicSalary.Text);
            PromotionLog.HouseRentAllownce = Convert.ToDecimal(TxtBasicSalary.Text);
            PromotionLog.TransportAllownce = Convert.ToDecimal(TxtBasicSalary.Text);
            PromotionLog.MedicalAllowance = Convert.ToDecimal(TxtBasicSalary.Text);
            PromotionLog.FuelAllowance = Convert.ToDecimal(TxtBasicSalary.Text);
            PromotionLog.ProvidentFund = true;
            PromotionLog.SpecialAllowance = Convert.ToDecimal(TxtBasicSalary.Text);
            PromotionLog.EffectiveDate = DateTime.ParseExact(TxtEffectiveDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            PromotionLog.EntryDate = EntryDate;
            PromotionLog.CreatedBy = Convert.ToInt32(Session["UserId"]); 
            db.Entry(PromotionLog).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }
    }
    
}