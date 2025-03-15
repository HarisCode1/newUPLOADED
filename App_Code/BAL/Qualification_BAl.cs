using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Qualification_BAl
/// </summary>
public class Qualification_BAl: Qualification_DAl
{
    public Qualification_BAl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override void DeleteQualification(int Id)
    {

        base.DeleteQualification(Id);
    }

    public int Id { get; set; }
    public Nullable<int> EmployeeId { get; set; }
    public string InstituteName { get; set; }
    public string Qualification { get; set; }
    public int Year { get; set; }
    public string Grade { get; set; }
    public int Type { get; set; }
    public decimal Marks { get; set; }
    public System.DateTime Createdon { get; set; }
    public int Creadetby { get; set; }
    public Nullable<System.DateTime> Modifiedon { get; set; }
    public Nullable<int> Modifiedby { get; set; }
    public bool IsActive { get; set; }
}