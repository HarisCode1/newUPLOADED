using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for PF_Rules_DAL
/// </summary>
public class PF_Rules_DAL
{
    public PF_Rules_DAL()
    {
        //sssss
        // TODO: Add constructor logic heresssss
        //
    }
    public virtual DataTable GetPF_RulesByID(int Id)
    {
        try
        {
            SqlParameter[] SqlParam = new SqlParameter[] {
                         new SqlParameter("@Id", Id)};
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "sp_GetPFrules", SqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual void DeletePFRules(int Id)
    {

        SqlParameter[] param =
        {
            new SqlParameter("@Id", Id)
        };

        SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "[sp_DeletePFRules]", param);
    }
    public virtual DataTable Sp_insertUpdatePF_Rules(PF_Rules_BAL rules)
    {
        try
        {
            SqlParameter[] sqlparam =
          {
               new SqlParameter("@Id", rules.Id),
            new SqlParameter("@TypeOfEmployee", rules.TypeOfEmployee),
            new SqlParameter("@SalaryType", rules.SalaryType),
            new SqlParameter("@Percent", rules.Percent),
             new SqlParameter("@CompanyId", rules.CompanyId),
              new SqlParameter("@CreatedOn", rules.CreatedOn),
               new SqlParameter("@CreatedBy", rules.CreatedBy),
                new SqlParameter("@Active", rules.Active),
                 new SqlParameter("@Deleted", rules.Deleted),
                 new SqlParameter("@UpdatedOn", rules.UpdatedOn),
                new SqlParameter("@UpdatedBy", rules.UpdatedBy),

            };

            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "Sp_insertUpdatePF_Rules", sqlparam).Tables[0];
            //return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual PF_Rules_BAL GetPF_rules(int Id)
    {
        PF_Rules_BAL PF = new PF_Rules_BAL();

        SqlParameter[] param =
            {
            new SqlParameter("@Id", Id),
        };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "Sp_GetPF_Rules", param))
        {

            if (dr.Read())
            {
                PF.Id = vt_Common.CheckInt(dr["Id"]);
                PF.TypeOfEmployee = vt_Common.CheckString(dr["TypeOfEmployee"]);
                PF.SalaryType = vt_Common.CheckString(dr["SalaryType"]);
                PF.Percent = vt_Common.Checkdecimal(dr["Percent"]);
                PF.CompanyId = vt_Common.CheckInt(dr["CompanyId"]);
                PF.CreatedOn = vt_Common.CheckDateTime(dr["CreatedOn"]);
                PF.CreatedBy = vt_Common.CheckInt(dr["CreatedBy"]);
                PF.Active = vt_Common.CheckBoolean(dr["Active"]);
                PF.Deleted = vt_Common.CheckDateTime(dr["Deleted"]);
                PF.UpdatedOn = vt_Common.CheckDateTime(dr["UpdatedOn"]);
                PF.UpdatedBy = vt_Common.CheckInt(dr["UpdatedBy"]);

            }
        }
        return PF;
    }

}