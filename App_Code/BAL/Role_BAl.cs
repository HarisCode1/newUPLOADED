using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Role_BAl
/// </summary>
public class Role_BAl: Role_DAL
{
    public Role_BAl()
    {
        //a
        // TODO: Add constructor logic here
        //
    }
    public override System.Data.DataTable CreateModifyRoles(Role_BAl RoleBAL)
    {
        return base.CreateModifyRoles(RoleBAL);
    }
    public override System.Data.DataTable ModifiedByID(Role_BAl RoleBAL)
    {
        return base.ModifiedByID(RoleBAL);
    }
    public override System.Data.DataTable GetRoles()
    {
        return base.GetRoles();
    }
    public override Role_BAl GetRoleInfo(int Id)
    {
        return base.GetRoleInfo(Id);
    }
    public override int DeleteRoleByID(int id)
    {
        return base.DeleteRoleByID(id);
    }
    public int RoleID { get; set; }
    public string Role { get; set; }
    public bool IsActive   { get; set; }
    public int CompanyId  { get; set; }
    public DateTime CreatedOn  { get; set; }
    public int CreatedBy  { get; set; }
    public int DeletedBy  { get; set; }
    public DateTime DeletedOn  { get; set; }
    public DateTime UpdatedOn  { get; set; }
    public int UpdatedBy  { get; set; }

}