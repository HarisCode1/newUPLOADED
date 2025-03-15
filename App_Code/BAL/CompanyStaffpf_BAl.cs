using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CompanyStaffpf_BAl
/// </summary>
public class CompanyStaffpf_BAl : CompanyStaffPf_DAl
{
    public CompanyStaffpf_BAl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override DataTable GetPF_RulesByID(int StaffPFID) 
    {
        return base.GetPF_RulesByID(StaffPFID);
    }
    public override CompanyStaffpf_BAl GetCompanyStaffPFID(int StaffPFID)
    {
        return base.GetCompanyStaffPFID(StaffPFID);
    }
    public override void DeleteStaffID(int StaffPFID)
    {

        base.DeleteStaffID(StaffPFID);
    }
    public int StaffPFID { get; set; }
    public int CompanyID { get; set; }
    public int EmployeeID { get; set; }
    public int MyProperty { get; set; }
    public int EmployeeTypeId { get; set; }
    public int SalaryTypeID { get; set; }
    public decimal Percentage { get; set; }
    public string NatureOfAppointment { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime FundJoiningDate { get; set; }
    public decimal MonthlySalary { get; set; }
    public string WitnesName1 { get; set; }
    public string WitnesCNIC1 { get; set; }
    public string WitnesName2 { get; set; }
    public string WitnesCNIC2 { get; set; }
    public int Createdby { get; set; }
    public DateTime Createdon { get; set; }
    public int Modifiedby { get; set; }
    public DateTime Modifiedon { get; set; }
    public Boolean IsActive { get; set; }

}