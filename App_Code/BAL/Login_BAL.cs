using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Login_BAL
/// </summary>
public class Login_BAL: Login_DAL
{
    public Login_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
     public override DataTable CheckPSIDAndPassword(string PSID, string Password)
    {
        return base.CheckPSIDAndPassword(PSID, Password);
    }

    public override DataTable CheckPSIDAndPassword(string PSID, string Password, SqlTransaction tran)
    {
        return base.CheckPSIDAndPassword(PSID, Password, tran);
    }
    public override DataTable CheckPSIDAndPasswordUser(string PSID, string Password, SqlTransaction tran)
    {
        return base.CheckPSIDAndPasswordUser(PSID, Password, tran);
    }
}