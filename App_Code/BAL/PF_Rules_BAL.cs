using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Viftech;

/// <summary>
/// Summary description for PF_Rules_BAL
/// </summary>
public class PF_Rules_BAL: PF_Rules_DAL
{
    public PF_Rules_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override DataTable GetPF_RulesByID(int Id)
    {
        return base.GetPF_RulesByID(Id);
    }
    public override void DeletePFRules(int Id)
    {

       base.DeletePFRules(Id);
    }
    public override DataTable Sp_insertUpdatePF_Rules(PF_Rules_BAL rules)
    {
        return base.Sp_insertUpdatePF_Rules(rules);
    }
    public override PF_Rules_BAL GetPF_rules(int Id)
    {

        return base.GetPF_rules(Id);
    }

    public int Id { get; set; }

    public string TypeOfEmployee { get; set; }
    public string SalaryType { get; set; }
    public decimal Percent { get; set; }
    public int CompanyId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public Boolean Active { get; set; }
    public DateTime Deleted { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int UpdatedBy { get; set; }

}
   
