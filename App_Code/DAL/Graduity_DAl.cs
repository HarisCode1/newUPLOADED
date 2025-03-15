using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for Graduity_DAl
/// </summary>
public class Graduity_DAl
{
    public Graduity_DAl()
    {
        //
        // TODO: Add constructor logic heres
        //
    }
    public virtual DataTable sp_GetGraduityByID(int Id)
    {
        try
        {
            SqlParameter[] SqlParam = new SqlParameter[] {
                         new SqlParameter("@Id", Id)};
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "sp_GetGraduityByID", SqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);

            throw ex;
        }
    }
    public virtual Graduity_BAl Sp_GetGraduity(int Id)
    {
        Graduity_BAl objgraduity = new Graduity_BAl();

        SqlParameter[] param =
            {
                new SqlParameter("@Id", Id),
            };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "[Sp_GetGraduity]", param))
        {

            if (dr.Read())
            {
                objgraduity.Id = vt_Common.CheckInt(dr["Id"]);
                objgraduity.TypeOfEmployee = vt_Common.CheckInt(dr["TypeOfEmployee"]);
                objgraduity.MaturityOfDays = vt_Common.CheckInt(dr["MaturityOfDays"]);
                objgraduity.NoOfDays = vt_Common.CheckInt(dr["NoOfDays"]);
                objgraduity.CreatedBy = vt_Common.CheckInt(dr["CreatedBy"]);
                objgraduity.CreatedOn = vt_Common.CheckDateTime(dr["Createdon"]);
                objgraduity.ModifiedBy = vt_Common.CheckInt(dr["ModifiedBy"]);
                objgraduity.ModifiedOn = vt_Common.CheckDateTime(dr["ModifiedOn"]);
                objgraduity.IsActive = vt_Common.CheckBoolean(dr["IsActive"]);
            }
        }
        return objgraduity;
    }
    public virtual void DeletedGraduityID(int Id)
    {

        SqlParameter[] param =
        {
            new SqlParameter("@Id", Id)
        };

        SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "[Sp_GraduitydeleteByID]", param);
    }
}