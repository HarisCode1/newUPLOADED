using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for TypeOfEmployee_DAl
/// </summary>
public class TypeOfEmployee_DAl
{
   
    public TypeOfEmployee_DAl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //GETDATA
    public virtual DataTable GetEmployeeListById(int Id)
    {
        try
        {
            SqlParameter[] SqlParam = new SqlParameter[] {
                         new SqlParameter("@Id", Id)};
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "sp_TypeOfEmployeeIds", SqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);

            throw ex;
        }
    }
    // INSERT_UPDATE
    public virtual string insertUpdateTypeOfEmp(TypeOfEmployee_BAL emp)
    {
        try
        {
            SqlParameter[] sqlparam =
          {
               new SqlParameter("@Id", emp.Id),
            new SqlParameter("@Type", emp.Type),
             new SqlParameter("@CompanyId", emp.CompanyId),
              new SqlParameter("@CreatedOn", emp.CreatedOn),
               new SqlParameter("@CreatedBy", emp.CreatedBy),
                new SqlParameter("@Active", emp.Active),
                 new SqlParameter("@Deleted", emp.Deleted),
                 new SqlParameter("@UpdatedOn", emp.UpdatedOn),
                new SqlParameter("@UpdatedBy", emp.UpdatedBy),
               
            };

            //return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "Sp_insertUpdateTypeOfEmp_Test", sqlparam).Tables[0];

           return SqlHelper.ExecuteScalar(vt_Common.PayRollConnectionString, "Sp_insertUpdateTypeOfEmp", sqlparam).ToString();
    //return dt;
}
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    //GetEmployeTypeByID
    public virtual TypeOfEmployee_BAL GetEmployeebyID(int Id)
    {
        TypeOfEmployee_BAL TP = new TypeOfEmployee_BAL();

        SqlParameter[] param =
            {
            new SqlParameter("@Id", Id),
        };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "Sp_GetEmployeeTypeByID", param))
        {

            if (dr.Read())
            {
                TP.Id = vt_Common.CheckInt(dr["Id"]);
                TP.Type = vt_Common.CheckString(dr["Type"]);
                TP.CompanyId = vt_Common.CheckInt(dr["CompanyId"]);
                TP.CreatedOn = vt_Common.CheckInt(dr["CreatedOn"]);
                TP.CreatedBy = vt_Common.CheckInt(dr["CreatedBy"]);
                TP.Active = vt_Common.CheckBoolean(dr["Active"]);
                TP.Deleted = vt_Common.CheckDateTime(dr["Deleted"]);
                TP.UpdatedOn = vt_Common.CheckDateTime(dr["UpdatedOn"]);
                TP.UpdatedBy = vt_Common.CheckInt(dr["UpdatedBy"]);
               
            }
        }
        return TP;
    }
    public virtual void DeleteEmployeeType(int Id)
    {

        SqlParameter[] param =
        {
            new SqlParameter("@ID", Id)
        };

        SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "[Sp_DeleteEmployeeType]", param).ToString();
    }



}