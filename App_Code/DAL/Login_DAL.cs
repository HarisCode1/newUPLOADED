using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Login_DAL
/// </summary>
public class Login_DAL
{
    public Login_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable CheckPSIDAndPassword(string PSID, string Password)
    {
        try
        {

            SqlParameter[] param = {
                new SqlParameter("@UserName", Password),
                                 new SqlParameter("@Password", Password)

                               };

            return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, "[Sp_CheckPSIDAndPassword]", param).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;

        }
    }
        public virtual DataTable CheckPSIDAndPassword(string PSID, string Password,  SqlTransaction trans)
    {
        try
        {
            SqlParameter[] param = { new SqlParameter("@UserName", PSID),
                                 new SqlParameter("@Password", Password)
                               };
            //return SqlHelper.ExecuteDataset(trans, "[Sp_CheckPSIDAndPassword]", param).Tables[0];

            return SqlHelper.ExecuteDataset(trans, "[Sp_LoginFromEmp]", param).Tables[0];
        }
       catch(Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual DataTable CheckPSIDAndPasswordUser(string PSID, string Password, SqlTransaction trans)
    {
        try
        {
            SqlParameter[] param = { new SqlParameter("@UserName", PSID),
                                 new SqlParameter("@Password", Password)
                               };
            return SqlHelper.ExecuteDataset(trans, "[Sp_CheckPSIDAndPassword]", param).Tables[0];

            //return SqlHelper.ExecuteDataset(trans, "[Sp_LoginFromEmp]", param).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
}
