using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Module_DLL
/// </summary>
public class Module_DLL
{
    public Module_DLL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable GetAllModule(int? ModuleID)
    {
        SqlParameter[] Gparam = {
                                    new SqlParameter("@ModuleID",ModuleID)
                               };
        return SqlHelper.ExecuteDataset(Viftech.vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "Sp_GetAllModuleByModuleID", Gparam).Tables[0];
    }
}