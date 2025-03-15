using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TypeOfEmployee_BAL
/// </summary>
public class TypeOfEmployee_BAL:TypeOfEmployee_DAl
{
    public TypeOfEmployee_BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override DataTable GetEmployeeListById(int Id)
    {
        return base.GetEmployeeListById(Id);
    }
    public override string insertUpdateTypeOfEmp(TypeOfEmployee_BAL emp)
    {
        return base.insertUpdateTypeOfEmp(emp);
    }
    public override TypeOfEmployee_BAL GetEmployeebyID(int Id)
    {

        return base.GetEmployeebyID(Id);
    }
    public override void DeleteEmployeeType(int Id)
    {

         base.DeleteEmployeeType(Id);
    }
    public int  Id{ get; set; }
    public string Type { get; set; }
    public int CompanyId { get; set; }
    public int CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public Boolean Active { get; set; }
    public DateTime Deleted { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int UpdatedBy { get; set; }
}