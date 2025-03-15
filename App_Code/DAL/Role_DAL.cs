using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Viftech;

/// <summary>
/// Summary description for Role_DAL
/// </summary>
public class Role_DAL
{
    public Role_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable CreateModifyRoles(Role_BAl RoleBAL)
    {
        try
        {
            SqlParameter[] param = {
                new SqlParameter("@RoleID", RoleBAL.RoleID),
                new SqlParameter("@Role", RoleBAL.Role),
                new SqlParameter("@CreatedOn", RoleBAL.CreatedOn),
                new SqlParameter("@CreatedBy", RoleBAL.CreatedBy),
                new SqlParameter("@IsActive", RoleBAL.IsActive),
                };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "CreateandModifyRoles", param).Tables[0];
            //return dt;
        }
        catch(Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    public virtual DataTable ModifiedByID(Role_BAl RoleBAL)
    {
        try
        {
            SqlParameter[] param = {
               new SqlParameter("@RoleID", RoleBAL.RoleID),
                new SqlParameter("@Role", RoleBAL.Role),
                new SqlParameter("@UpdatedOn", RoleBAL.UpdatedOn),
                new SqlParameter("@UpdatedBy", RoleBAL.UpdatedBy),
                new SqlParameter("@IsActive", RoleBAL.IsActive),
                };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "ModifyRoles", param).Tables[0];
            //return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual DataTable GetRoles()
    {
        try
        {
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "GetRoles").Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual int DeleteRoleByID(int id)
    {
        SqlParameter[] param =
        {
            new SqlParameter("ID",id)
        };
        return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, "RoleDeleteById",param);
    }
    public virtual Role_BAl GetRoleInfo(int InventId)
    {
        try
        {
            Role_BAl BAL = new Role_BAl();
            SqlParameter[] param = { new SqlParameter("@RoleID", InventId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "GetRolesDetails", param))
            {
                if (dr.Read())
                {
                    BAL.RoleID = vt_Common.CheckInt(dr["RoleID"]);
                    BAL.Role = vt_Common.CheckString(dr["Role"]);
                    BAL.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    BAL.CreatedOn = vt_Common.CheckDateTime(dr["CreatedOn"]);
                    BAL.CreatedBy = vt_Common.CheckInt(dr["CreatedBy"]);
                    BAL.UpdatedOn = vt_Common.CheckDateTime(dr["ModifiedOn"]);
                    BAL.UpdatedBy = vt_Common.CheckInt(dr["ModifiedBy"]);
                }
                return BAL;
            }
        }
        catch(Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
}