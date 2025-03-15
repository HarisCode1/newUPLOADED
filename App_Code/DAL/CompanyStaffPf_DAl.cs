using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for CompanyStaffPf_DAl
/// </summary>
public class CompanyStaffPf_DAl
{
    public CompanyStaffPf_DAl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual DataTable GetPF_RulesByID(int StaffPFID)
    {
        try
        {
            SqlParameter[] SqlParam = new SqlParameter[] {
                         new SqlParameter("@StaffPFID", StaffPFID)};
            return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "sp_GetCompanyPFrules", SqlParam).Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public virtual CompanyStaffpf_BAl GetCompanyStaffPFID(int StaffPFID)
    {
        CompanyStaffpf_BAl CompanystaffPF = new CompanyStaffpf_BAl();

        SqlParameter[] param =
            {
            new SqlParameter("@StaffPFID", StaffPFID),
        };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "[Sp_GetCompanyStaffPFID]", param))
        {

            if (dr.Read())
            {
                CompanystaffPF.StaffPFID = vt_Common.CheckInt(dr["StaffPFID"]);
                CompanystaffPF.CompanyID = vt_Common.CheckInt(dr["CompanyID"]);
                CompanystaffPF.EmployeeID = vt_Common.CheckInt(dr["EmployeeID"]);
                CompanystaffPF.EmployeeTypeId = vt_Common.CheckInt(dr["EmployeeTypeId"]);
                CompanystaffPF.SalaryTypeID = vt_Common.CheckInt(dr["SalaryTypeID"]);
                CompanystaffPF.Percentage = vt_Common.Checkdecimal(dr["Percentage"]);
                CompanystaffPF.NatureOfAppointment = vt_Common.CheckString(dr["NatureOfAppointment"]);
                CompanystaffPF.JoiningDate = vt_Common.CheckDateTime(dr["JoiningDate"]);
                CompanystaffPF.FundJoiningDate = vt_Common.CheckDateTime(dr["FundJoiningDate"]);
                CompanystaffPF.MonthlySalary = vt_Common.Checkdecimal(dr["MonthlySalary"]);
                CompanystaffPF.WitnesName1 = vt_Common.CheckString(dr["WitnesName1"]);
                CompanystaffPF.WitnesCNIC1 = vt_Common.CheckString(dr["WitnesCNIC1"]);
                CompanystaffPF.WitnesName2 = vt_Common.CheckString(dr["WitnesName2"]);
                CompanystaffPF.WitnesCNIC2 = vt_Common.CheckString(dr["WitnesCNIC2"]);
                CompanystaffPF.Createdby = vt_Common.CheckInt(dr["Createdby"]);
                CompanystaffPF.Createdon = vt_Common.CheckDateTime(dr["Createdon"]);
                CompanystaffPF.Modifiedby = vt_Common.CheckInt(dr["Modifiedby"]);
                CompanystaffPF.Modifiedon = vt_Common.CheckDateTime(dr["Modifiedon"]);
                CompanystaffPF.IsActive = vt_Common.CheckBoolean(dr["IsActive"]);
            }
        }
        return CompanystaffPF;
    }
    public virtual void DeleteStaffID(int StaffPFID)
    {

        SqlParameter[] param =
        {
            new SqlParameter("@StaffPFID", StaffPFID)
        };

        SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, CommandType.StoredProcedure, "[Sp_deletestaff]", param);
    }
}