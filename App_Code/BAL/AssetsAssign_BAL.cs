using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for User_BAL
/// </summary>
public class AssetsAssign_BAL : AssetsAssign_DLL
{ 
    public AssetsAssign_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>s
    /// Get UserList By CompanyID
    /// </summary>
    /// <returns></returns>
    public override DataTable getAssignAssets()
    {
        return base.getAssignAssets();
    }
    public override DataTable getgratuity()
    {
        return base.getgratuity();
    }




    public override int DeleteUser(int UserID)
    {

         return base.DeleteUser(UserID);
    }
 
    public override System.Data.DataTable Sp_Insert(User_BAL user)
    {
        return base.Sp_Insert(user);
    }

    public override System.Data.DataTable UpdateById(User_BAL user)
    {
        return base.UpdateById(user);
    }
    public override User_BAL GetUserbyID(int UserID)
    {

        return base.GetUserbyID(UserID);
    }

    public DataTable PermissionTable = new DataTable();
    public DataTable MenuTable = new DataTable();
    public int UserID { get; set; }
    public int CompanyID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int RoleID { get; set; }
    public string LastName { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime Deleted { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int UpdatedBy { get; set; }
    public int DeletedBy { get; set; }


    //public int UserID { get; set; }
    //public Nullable<int> CompanyID { get; set; }
    //public string UserName { get; set; }
    //public string Password { get; set; }
    //public Nullable<int> RoleID { get; set; }
    //public string LastName { get; set; }
    //public Nullable<bool> Active { get; set; }
    //public Nullable<System.DateTime> CreatedDate { get; set; }
    //public string Email { get; set; }
    //public string FirstName { get; set; }
    //public Nullable<System.DateTime> CreatedOn { get; set; }
    //public Nullable<int> CreatedBy { get; set; }
    //public Nullable<System.DateTime> Deleted { get; set; }
    //public Nullable<System.DateTime> UpdatedOn { get; set; }
    //public Nullable<int> UpdatedBy { get; set; }

 
}

