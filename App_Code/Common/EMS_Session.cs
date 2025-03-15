using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for EMS_Session
/// </summary>
public class EMS_Session
{
	public EMS_Session()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable PermissionTable = new DataTable();
    public string UserName { get; set; }
    public string EmpName { get; set; }

    public bool Can_Insert { get; set; }
    public bool Can_View { get; set; }
    public bool Can_Update { get; set; }
    public bool Can_Delete { get; set; }

    public bool CanInsert(System.Web.UI.Page Page)
    {
        if (!Can_Insert)
        {
            vt_Common.MessageSecurity(Page, this, "Add New/Save");
        }

        return Can_Insert;
    }

    public bool CanView(System.Web.UI.Page Page)
    {
        if (!Can_View)
        {
            vt_Common.MessageSecurity(Page, this, "View");
        }

        return Can_View;
    }

    public bool CanUpdate(System.Web.UI.Page Page)
    {
        if (!Can_Update)
        {
            vt_Common.MessageSecurity(Page, this, "Update");
        }

        return Can_Update;
    }

    public bool CanDelete(System.Web.UI.Page Page)
    {
        if (!Can_Delete)
        {
            vt_Common.MessageSecurity(Page, this, "Delete");
        }

        return Can_Delete;
    }

    public vt_tbl_Company Company { get; set; }
    public vt_tbl_Employee Employee { get; set; }
    public vt_tbl_User user { get; set; }

    public vt_tbl_Designation Designation { get; set; }

}