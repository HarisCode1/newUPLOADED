using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Module_BAL
/// </summary>
public class Module_BAL :Module_DLL
{
    public Module_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override DataTable GetAllModule(int? ModuleID)
    {
        return base.GetAllModule(ModuleID);
    }
}