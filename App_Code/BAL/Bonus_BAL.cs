using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Bonus_BAL
/// </summary>
public class Bonus_BAL:Bonus_DAL
{
    public Bonus_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    public override DataTable CreateSP(Bonus_BAL BAL)
    {
        return base.CreateSP(BAL);
    }
    public override DataTable UpdateSP(Bonus_BAL BAL)
    {
        return base.UpdateSP(BAL);
    }
    public override int DeleteBonusByID(int id)
    {
        return base.DeleteBonusByID(id);
    }
    public override DataTable GetData(int companyId)
    {
        return base.GetData(companyId);
    }
    public override Bonus_BAL GetBonusByID(int id)
    {
        return base.GetBonusByID(id);
    }

    public int ID { get; set; }
    public int CompanyID { get; set; }
    public int SalaryID { get; set; }
    public string BonusTitle { get; set; }

    public int EmpTypeId { get; set; }
    public int MatureDays { get; set; }
    public int BonusDays { get; set; }
    public decimal Amount { get; set; }

    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public bool IsActive { get; set; }
}