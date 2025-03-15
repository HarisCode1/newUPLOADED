using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoanApproval_BAL
/// </summary>
public class LoanApproval_BAL: LoanApproval_DAL
{
    public LoanApproval_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override System.Data.DataTable GetLoanRecord(int id)
    {
        return base.GetLoanRecord(id);
    }
    public override System.Data.DataTable GetLoanRecordHR()
    {
        return base.GetLoanRecordHR();
    }
    public override LoanApproval_BAL GetLoanById(int id)
    {
        return base.GetLoanById(id);
    }
    public override int Approve(int id)
    {
        return base.Approve(id);
    }
    public override int ApproveByHR(int id)
    {
        return base.ApproveByHR(id);
    }
    public override int Reject(int id)
    {
        return base.Reject(id);
    }
    public override int RejectByHR(int id)
    {
        return base.RejectByHR(id);
    }
    public int RoleID { get; set; }

    public int EntryID { get; set; }
    public int CompanyID { get; set; }
    public DateTime AppliedDate { get; set; }
    public int EmployeeID { get; set; } 
    public int LoanID { get; set; }
    public decimal AppliedAmount { get; set; }
    public int MonthDuration { get; set; }
    public int LineManager { get; set; }
    public int HRAdmin { get; set; }
    public string CompanyName { get; set; }
    public string LoanName { get; set; }
    public string EmployeeName { get; set; }
    
    public int EMIType { get; set; }

}