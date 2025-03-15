using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RolePermission
/// </summary>
public class RolePermission
{
    public RolePermission()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable GetPagePermissionPagesByRole(string UserName, int RoleID, int UserID, SqlTransaction trans)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@RoleID",RoleID)
                                   ,new SqlParameter("@UserID",UserID)
                               };
        return SqlHelper.ExecuteDataset(trans, "sp_GetPagePermissionPage", param).Tables[0];
    }
    public virtual DataTable GetMenuByRoleID(int RoleID, SqlTransaction trans)
    {
        SqlParameter[] param = { new SqlParameter("@RoleID", RoleID) };
        return SqlHelper.ExecuteDataset(trans, "Sp_GetMenuByRoleID", param).Tables[0];
    }

    public int PermissionID { get; set; }
    public int RoleID { get; set; }
    public int UserID { get; set; }
    public int PageID { get; set; }
    public bool Can_View { get; set; }
    public bool Can_Insert { get; set; }
    public bool Can_Update { get; set; }
    public bool Can_Delete { get; set; }
    public bool Active { get; set; }
    public DateTime CreateDate { get; set; }
    public int CreateBy { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateBy { get; set; }
}