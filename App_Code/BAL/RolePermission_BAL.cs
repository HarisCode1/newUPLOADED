using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RolePermission_BAL
/// </summary>
public class RolePermission_BAL :RolePermission_DLL
{
    public RolePermission_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override int InsertUpdatePagePermission(RolePermission_BAL RoleBAL, SqlTransaction Tran)
    {
        return base.InsertUpdatePagePermission(RoleBAL, Tran);
    }

    public override DataTable GetModuleRightsByRoleID(int RoleID, int UserID)
    {
        return base.GetModuleRightsByRoleID(RoleID, UserID);
    }
    public override DataTable GetPagePermissionpPagesByRole(int RoleID)
    {
        return base.GetPagePermissionpPagesByRole(RoleID);
    }
   
    public  int PermissionID { get; set; }
    public int ModulePermissionID { get; set; }
    public int UserID { get; set; }
    public int CreatedBy { get; set; }

    public int PageID { get; set; }
    public int RoleID { get; set; }
    public int ModuleID { get; set; }
    public bool Can_View { get; set; }
    public bool Can_Insert { get; set; }
    public bool Can_Update { get; set; }
    public bool Can_Delete { get; set; }
    public bool Active { get; set; }
}