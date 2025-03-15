using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Page_DLL
/// </summary>
public class Page_DLL
{
    public Page_DLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public virtual DataTable GetAllPage(int? PageID)
    {
        SqlParameter[] Gparam = {
                                    new SqlParameter("@PageID",PageID)
                               };
        return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "Sp_GetAllPageByPageID", Gparam).Tables[0];
    }

    public virtual DataTable GetAllPageByModuleID(int? ModuleID)
    {
        SqlParameter[] Gparam = {
                                    new SqlParameter("@ModuleID",ModuleID)
                               };
        return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "Sp_GetAllPageByModuleID", Gparam).Tables[0];
    }

    public virtual DataTable GetPermissions(int? RoleID, int? ModuleID)
    {
        SqlParameter[] Gparam = {
                                    new SqlParameter("@RoleID",RoleID),
                                    new SqlParameter("@ModuleID",ModuleID)
                               };
        return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "GetPermissions", Gparam).Tables[0];
    }
}