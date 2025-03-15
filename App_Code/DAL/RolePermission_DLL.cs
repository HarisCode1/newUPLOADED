using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RolePermission_DLL
/// </summary>
public class RolePermission_DLL
{
    public RolePermission_DLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual int InsertUpdateModulePermissionByRoleID(RolePermission_BAL RP, SqlTransaction Tran)
    {
        try
        {
            SqlParameter[] param = {new SqlParameter("@ModulePermissionID",RP.ModulePermissionID)
                                   ,new SqlParameter("@RoleID",RP.RoleID)
                                   ,new SqlParameter("@UserID",RP.UserID)
                                   ,new SqlParameter("@ModuleID",RP.ModuleID)
                                   ,new SqlParameter("@Can_View",RP.Can_View)
                                   ,new SqlParameter("@Can_Insert",RP.Can_Insert)
                                   ,new SqlParameter("@Can_Update",RP.Can_Update)
                                   ,new SqlParameter("@Can_Delete",RP.Can_Delete)
                                   ,new SqlParameter("@Active",RP.Active)
                                   ,new SqlParameter("@CreateBy",RP.CreatedBy)
                                   ,new SqlParameter("@CreateDate",DateTime.Now)
                                   };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(Tran, "[Sp_InsertUpdateModulPermissionByRoleID]", param));
        }
        catch(Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
       
    }
    public virtual int InsertUpdatePagePermission(RolePermission_BAL RP, SqlTransaction Tran)
    {
        try
        {
            SqlParameter[] param ={new SqlParameter("@Permission_Id",RP.PermissionID)
                                 ,new SqlParameter("@RoleId",RP.RoleID)
                                 ,new SqlParameter("@UserId",RP.UserID)
                                 ,new SqlParameter("@PageId",RP.PageID)
                                 ,new SqlParameter("@Can_View",RP.Can_View)
                                 ,new SqlParameter("@Can_Insert",RP.Can_Insert)
                                 ,new SqlParameter("@Can_Update",RP.Can_Update)
                                 ,new SqlParameter("@Can_Delete",RP.Can_Delete)
                                 ,new SqlParameter("@Active",RP.Active)
                                 ,new SqlParameter("@CreateBy",RP.CreatedBy)
                                 ,new SqlParameter("@CreateDate", DateTime.Now)
                                 };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(Tran, "[Sp_InsertUpdatePagePermission]", param));
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }
    public virtual int DeletePagePermissionPagesByRole(int RoleID, int UserID, SqlTransaction Tran)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@RoleID", RoleID)
                                   ,new SqlParameter("@UserID",UserID)
                               };
        return SqlHelper.ExecuteNonQuery(Tran, "[Sp_DeleteRolePagePermission]", param);
    }
    public virtual int DeleteModulePermissionByRoleID(int RoleID, int UserID, SqlTransaction Tran)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@RoleID", RoleID)
                                   ,new SqlParameter("@UserID",UserID)
                               };
        return Convert.ToInt32(SqlHelper.ExecuteNonQuery(Tran, "[Sp_DeleteModulePermission]", param));
        //return SqlHelper.ExecuteNonQuery(Tran, "[Sp_DeleteModulePermission]", param);

    }
    public virtual DataTable GetModuleRightsByRoleID(int RoleID, int UserID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@RoleID", RoleID)
                                   ,new SqlParameter("@UserID",UserID)
                               };
        return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, "[Sp_GetModuleRights]", param).Tables[0];
    }

    public virtual DataTable GetPagePermissionpPagesByRole(int RoleID)
    {
        
            SqlParameter[] param = {
                                    new SqlParameter("@RoleID",RoleID)
                                   //,new SqlParameter("@UserID",UserID)
                               };
        //return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, "sp_GetPagePermissionPages", param).Tables[0];
        return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, "sp_GetPagePermissionByRole", param).Tables[0];




    }
    public virtual DataTable GetMenuByRoleID(int RoleID, SqlTransaction trans)
    {
        SqlParameter[] param = { new SqlParameter("@RoleID", RoleID) };
        return SqlHelper.ExecuteDataset(trans, "Sp_GetMenuByRoleID", param).Tables[0];
    }

}