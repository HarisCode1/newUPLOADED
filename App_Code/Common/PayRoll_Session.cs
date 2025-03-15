using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PayRoll_Session
/// </summary>
public class PayRoll_Session
{
    public PayRoll_Session()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   
    public DataTable PermissionTable = new DataTable();
    public DataTable MenuTable = new DataTable();
    public int EmployeeID { get; set; }

    public int UserID { get; set; }
    public string UserName { get; set; }
    public string EmployeeName { get; set; }
    public int ActionID { get; set; }
    public string Email { get; set; }
    public string ImageID { get; set; }
    public int RoleID { get; set; }
    public int? CompanyID { get; set; }
    public string RoleName { get; set; }
    public string Password { get; set; }
    public bool UserActive { get; set; }
    public bool RoleActive { get; set; }
    public bool Can_Insert { get; set; }
    public bool Can_Update { get; set; }
    public bool Can_Delete { get; set; }
    public bool Can_View { get; set; }
    public string Permission { get; set; }
    public string PageRefrence { get; set; }
    public int LoginHistoryID { get; set; }
    public bool IsLock { get; set; }
    public string LockReason { get; set; }
    public int LoginAttemptFailed { get; set; }
    public int LoginCounter { get; set; }
    public string Designation { get; set; }
    public DateTime LastLoginDate { get; set; }
    public DateTime? LastUnsuccessfulLoginDate { get; set; }
    public vt_tbl_User User { get; set; }
    public vt_tbl_Employee Employee { get; set; }

}