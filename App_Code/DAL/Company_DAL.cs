using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for Company_DAL
/// </summary>
public class Company_DAL
{
    public Company_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable GetCompanyID(int CompanyID)
    {
        try
        {
            SqlParameter[] SqlParam = new SqlParameter[] {
                         new SqlParameter("@CompanyID", CompanyID)};
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "Sp_GetCompany", SqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

}