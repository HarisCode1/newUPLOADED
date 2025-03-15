using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for User_DLL
/// </summary>
public class User_DLL
{
    public User_DLL()
    {
        //
        // TODO: Add constructor logic here
        // 
    }

    public virtual DataTable Sp_Insert(User_BAL user)
    {
        try
        {
            SqlParameter[] sqlparam =
          {
                new SqlParameter("@EmployeeID", user.EmployeIeD),
                new SqlParameter("@EmployeeEnrollId", user.EmployeeEnrollId),
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Passsword", user.Password),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@CompanyID", user.CompanyID),
                new SqlParameter("@RoleID", user.RoleID),
                new SqlParameter("@CreatedOn", Convert.ToDateTime(user.CreatedOn)),
                new SqlParameter("@CreatedBy", user.CreatedBy),
                new SqlParameter("@Active", user.Active),
            };

            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "CreateUsersp", sqlparam).Tables[0];
            //return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual DataTable UpdateById(User_BAL user)
    {
        try
        {
            SqlParameter[] sqlparam =
          {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Passsword", user.Password),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@RoleID", user.RoleID),
                new SqlParameter("@CompanyID", user.CompanyID),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@ModifiedOn", user.UpdatedOn),
                new SqlParameter("@ModifiedBy", user.UpdatedBy),
                new SqlParameter("@Active", user.Active),
            };

           var dt =SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "UpdateUserByID", sqlparam).Tables[0];

           return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual DataTable SetDefaultPassword(User_BAL user)
    {
        try
        {
            SqlParameter[] sqlparam =
          {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Passsword", user.Password),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@RoleID", user.RoleID),
                new SqlParameter("@CompanyID", user.CompanyID),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@ModifiedOn", user.UpdatedOn),
                new SqlParameter("@ModifiedBy", user.UpdatedBy),
                new SqlParameter("@Active", user.Active),
            };

            var dt = SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "UpdateUserByID", sqlparam).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }









    public virtual DataTable GetUserListByCompanyID()
    {
        try
        {
            
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "Sp_UserbyCompanyID").Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    public virtual int DeleteUser(int id)
    {
        try
        {
            SqlParameter[] param =
           {
            new SqlParameter("@UserID", id)
        };
            return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "Sp_deleteUSer", param);

        }
        catch(Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        
    }
    public virtual DataTable GetTerminatedEmployeeCompanyID(int CompanyID)
    {
        try
        {
            SqlParameter[] param = {

                new SqlParameter("@CompanyID", CompanyID)
            };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "[dbo].[Sp_GetTerminatedEmployeeByCompanyID]", param).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    public virtual DataTable Getuploadeddocuments(int CompanyID, int employeeid)
    {
        try
        {
            SqlParameter[] param = {
                new SqlParameter("@employeeid", employeeid),
                new SqlParameter("@CompanyID", CompanyID)
                   
            };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "[dbo].[VT_sp_GetUploadDocument]", param).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual User_BAL GetUserbyID(int userID)
    {
        User_BAL user = new User_BAL();

        SqlParameter[] param =
            {
            new SqlParameter("@UserID", userID),
        };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "Sp_GetUserbyID", param))
        {

            if (dr.Read())
            {
                user.UserID = vt_Common.CheckInt(dr["UserID"]);
                user.UserName = vt_Common.CheckString(dr["UserName"]);
                user.Password = vt_Common.CheckString(dr["Password"]);
                user.FirstName = vt_Common.CheckString(dr["FirstName"]);
                user.LastName = vt_Common.CheckString(dr["LastName"]);
                user.Email = vt_Common.CheckString(dr["Email"]);
                user.CompanyID = vt_Common.CheckInt(dr["CompanyID"]);
                user.RoleID = vt_Common.CheckInt(dr["RoleID"]);
                user.Active = vt_Common.CheckBoolean(dr["Active"]);
                user.Email = vt_Common.CheckString(dr["Email"]);
            }
        }
        return user;
    }


    //public virtual int UpdateUser(User_BAL user)
    //{
    //    try
    //    {
    //        int i;
    //        SqlParameter[] sqlparam =
    //        {
    //           new SqlParameter("@UserID", user.UserID),
    //        new SqlParameter("@CompanyID", user.CompanyID),
    //         new SqlParameter("@UserName", user.UserName),
    //          new SqlParameter("@Passsword", user.Password),
    //           new SqlParameter("@RoleID", user.RoleID),
    //            new SqlParameter("@LastName", user.LastName),
    //             new SqlParameter("@Active", user.Active),
    //             new SqlParameter("@CreatedDate", user.CreatedDate),
    //            new SqlParameter("@Email", user.Email),
    //             new SqlParameter("@FirstName", user.FirstName),
    //             new SqlParameter("@CreatedOn", user.CreatedOn),
    //            new SqlParameter("@CreatedBy", user.CreatedBy),
    //             new SqlParameter("@Deleted", user.Deleted),
    //             new SqlParameter("@UpdatedOn", user.UpdatedOn),
    //             new SqlParameter("@UpdatedBy", user.UpdatedBy),
    //        };
    //        i = SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "Sp_UpdateUser", sqlparam);
    //        return i;

    //        // return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "CreateandModifyRoles", param).Tables[0];

    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.TryCatchException(ex);
    //        throw ex;
    //    }

    //}


    


    //GetCOmpanyID 


}