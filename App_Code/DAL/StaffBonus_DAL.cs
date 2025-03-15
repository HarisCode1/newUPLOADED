using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for Bonus_DAL
/// </summary>
public class StaffBonus_DAL
{
    public StaffBonus_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable CreateSP(StaffBonus_BAL BAL)
    {
        try
        {
            SqlParameter[] param = {

                new SqlParameter("@ID", BAL.ID),
                new SqlParameter("@BonusId",BAL.BonusID),
                new SqlParameter("@Month", BAL.Month),
                new SqlParameter("@CreatedBy", BAL.CreatedBy),
                new SqlParameter("@CreatedOn", Convert.ToDateTime(BAL.CreatedOn)),
                new SqlParameter("@IsActive", BAL.IsActive),
                 new SqlParameter("@CompanyID", BAL.CompanyID)
            };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "vt_sp_CreateStaffBonus", param).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual DataTable UpdateSP(StaffBonus_BAL BAL)
    {
        try
        {
            SqlParameter[] param = {

                new SqlParameter("@ID", BAL.ID),
                new SqlParameter("@BonusId",BAL.BonusID),
                new SqlParameter("@Month", BAL.Month),
                new SqlParameter("@ModifiedBy", BAL.ModifiedBy),
                new SqlParameter("@Modifiedon", Convert.ToDateTime(BAL.ModifiedOn)),
                new SqlParameter("@IsActive", BAL.IsActive),
                 new SqlParameter("@CompanyID", BAL.CompanyID)
            };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "ModifyStaffBonus", param).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual int DeleteBonusByID(int id)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@ID", id)
        };
        return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "DeleteStaffBonusByID", param);
    }
    public virtual DataTable GetData(int companyId)
    {
        SqlParameter[] param =
            {
                new SqlParameter("@CompanyId", companyId )
            };
        return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "GetBonus", param).Tables[0];
    }
    public virtual StaffBonus_BAL GetBonusByID(int id)
    {
        try
        {
            StaffBonus_BAL BAL = new StaffBonus_BAL();

            SqlParameter[] param =
            {
            new SqlParameter("@ID", id )
        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "GetBonusBonusByID", param))
            {
                if(dr.Read())
                {
                    BAL.ID = vt_Common.CheckInt(dr["Id"]);
                    BAL.BonusID = vt_Common.CheckInt(dr["BonusId"]);
                    BAL.Month = vt_Common.CheckDateTime(dr["Month"]);
                }
               
            }
            return BAL;
        }
        catch(Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
        
    }
}