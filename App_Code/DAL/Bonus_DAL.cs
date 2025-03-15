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
public class Bonus_DAL
{
    public Bonus_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable CreateSP(Bonus_BAL BAL)
    {
        try
        {
            SqlParameter[] param = {

                new SqlParameter("@ID", BAL.ID),
                new SqlParameter("@EmpTypeId",BAL.EmpTypeId),
                new SqlParameter("@CompanyID",BAL.CompanyID),
                new SqlParameter("@SalaryID",BAL.SalaryID),
                new SqlParameter("@BonusTitle",BAL.BonusTitle),
                new SqlParameter("@MatureDays", BAL.MatureDays),
                new SqlParameter("@BonusDays", BAL.BonusDays),
                new SqlParameter("@Amount", BAL.Amount),
                new SqlParameter("@CreatedBy", BAL.CreatedBy),
                new SqlParameter("@CreatedOn", Convert.ToDateTime(BAL.CreatedOn)),
                new SqlParameter("@IsActive", BAL.IsActive)
            };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "CreateandModifyBonus", param).Tables[0];
        }
        catch(Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual DataTable UpdateSP(Bonus_BAL BAL)
    {
        try
        {
            SqlParameter[] param = {

                new SqlParameter("@ID", BAL.ID),
                new SqlParameter("@EmpTypeId",BAL.EmpTypeId),
                new SqlParameter("@CompanyID",BAL.CompanyID),
                new SqlParameter("@SalaryID",BAL.SalaryID),
                new SqlParameter("@BonusTitle",BAL.BonusTitle),
                new SqlParameter("@MatureDays", BAL.MatureDays),
                new SqlParameter("@BonusDays", BAL.BonusDays),
                new SqlParameter("@Amount", BAL.Amount),
                new SqlParameter("@ModifiedBy", BAL.ModifiedBy),
                new SqlParameter("@ModifiedOn", Convert.ToDateTime(BAL.ModifiedOn)),
                new SqlParameter("@IsActive", BAL.IsActive)
            };
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "ModifyBonus", param).Tables[0];
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
        return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "DeleteBonusByID", param);
    }
    public virtual DataTable GetData(int companyId)
    {
        SqlParameter[] param =
            {
                new SqlParameter("@CompanyId", companyId )
            };
        return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "GetBonus", param).Tables[0];
    }
    public virtual Bonus_BAL GetBonusByID(int id)
    {
        try
        {
            Bonus_BAL BAL = new Bonus_BAL();

            SqlParameter[] param =
            {
            new SqlParameter("@ID", id )
        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "GetBonusByID", param))
            {
                if(dr.Read())
                {
                    BAL.ID = vt_Common.CheckInt(dr["ID"]);
                    BAL.EmpTypeId = vt_Common.CheckInt(dr["EmpTypeId"]);
                    BAL.CompanyID = vt_Common.CheckInt(dr["CompanyId"]);
                    BAL.SalaryID = vt_Common.CheckInt(dr["SalaryTypeId"]);
                    BAL.BonusTitle= vt_Common.CheckString(dr["BonusTitle"]);
                    BAL.MatureDays = vt_Common.CheckInt(dr["MatureDays"]);
                    BAL.BonusDays = vt_Common.CheckInt(dr["BonusDays"]);
                    BAL.Amount = vt_Common.CheckInt(dr["Amount"]);
                    BAL.CreatedBy = vt_Common.CheckInt(dr["CreatedBy"]);
                    BAL.CreatedOn = vt_Common.CheckDateTime(dr["CreatedOn"]);
                    BAL.ModifiedBy = vt_Common.CheckInt(dr["ModifiedBy"]);
                    BAL.ModifiedOn = vt_Common.CheckDateTime(dr["ModifiedOn"]);
                    BAL.IsActive = vt_Common.CheckBoolean(dr["IsActive"]);
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