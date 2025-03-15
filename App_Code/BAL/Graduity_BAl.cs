using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Graduity_BAl
/// </summary>
public class Graduity_BAl:Graduity_DAl
{
    public Graduity_BAl()
    {
        //
        // TODO: Add constructor logic heres
        //
    }
    public override DataTable sp_GetGraduityByID(int Id)
    {
        return base.sp_GetGraduityByID(Id);
    }
    public override Graduity_BAl Sp_GetGraduity(int Id)
    {

        return base.Sp_GetGraduity(Id);
    }
    public override void DeletedGraduityID(int Id)
    {

        base.DeletedGraduityID(Id);
    }
    public int Id { get; set; }
    public int TypeOfEmployee { get; set; }
    public int MaturityOfDays { get; set; } 
    public int NoOfDays { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Boolean IsActive { get; set; }
}