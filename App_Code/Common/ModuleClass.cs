using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModuleClass
/// </summary>
public class ModuleClass
{
    public ModuleClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int ModuleID { get; set; }
    public int UserID { get; set; }
    public int RoleID { get; set; }
    public int PageID { get; set; }
    public bool CanView { get; set; }
    public bool CanInsert { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
    public bool Active { get; set; }
    public string  PageUrl { get; set; }
    public string ModuleName { get; set; }
}