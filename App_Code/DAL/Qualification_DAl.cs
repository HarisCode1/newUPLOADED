using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for Qualification_DAl
/// </summary>
public class Qualification_DAl
{
    public Qualification_DAl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual void DeleteQualification(int Id)
    {

        SqlParameter[] param =
        {
            new SqlParameter("@Id", Id)
        };

        SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "[sp_DeleteQualification]", param);
    }
}