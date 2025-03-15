using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Company_BAL
/// </summary>
public class Company_BAL: Company_DAL
{
    public Company_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override DataTable GetCompanyID(int CompanyID)
    {
        return base.GetCompanyID(CompanyID);
    }

    public int CompanyID { get; set; }
    public string CompanyName { get; set; }
    public string CompanyShortName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string Address { get; set; }

    public int HoursInDay { get; set; }
    public int EOBI { get; set; }
    public DateTime BreakStartTime { get; set; }
    public DateTime BreakEndTime { get; set; }

}