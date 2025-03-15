using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Page_BAL
/// </summary>
public class Page_BAL :Page_DLL
{
    public Page_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override DataTable GetAllPageByModuleID(int? ModuleID)
    {
        return base.GetAllPageByModuleID(ModuleID);
    }

    public override DataTable GetAllPage(int? PageID)
    {
        return base.GetAllPage(PageID);
    }

    public override DataTable GetPermissions(int? RoleID, int? ModuleID)
    {
        return base.GetPermissions(RoleID, ModuleID);
    }

    //public virtual int Insert_Permission(RolePermission_BAL RP, SqlTransaction Tran)
    //{
    //    try
    //    {
    //        SqlParameter[] param = {new SqlParameter("@ModulePermissionID",RP.ModulePermissionID)
    //                               ,new SqlParameter("@RoleID",RP.RoleID)
    //                               ,new SqlParameter("@UserID",RP.UserID)
    //                               ,new SqlParameter("@ModuleID",RP.ModuleID)
    //                               ,new SqlParameter("@Can_View",RP.Can_View)
    //                               ,new SqlParameter("@Can_Insert",RP.Can_Insert)
    //                               ,new SqlParameter("@Can_Update",RP.Can_Update)
    //                               ,new SqlParameter("@Can_Delete",RP.Can_Delete)
    //                               ,new SqlParameter("@Active",RP.Active)
    //                               ,new SqlParameter("@CreateBy",RP.CreatedBy)
    //                               ,new SqlParameter("@CreateDate",DateTime.Now)
    //                               };
    //        return Convert.ToInt32(SqlHelper.ExecuteScalar(Tran, "[Insert_Permission]", param));
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.TryCatchException(ex);
    //        throw ex;
    //    }

    //}
}