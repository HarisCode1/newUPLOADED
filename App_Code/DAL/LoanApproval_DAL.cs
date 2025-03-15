using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Viftech;

/// <summary>
/// Summary description for LoanApproval_DAL
/// </summary>
public class LoanApproval_DAL
{
    public LoanApproval_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public virtual System.Data.DataTable GetLoanRecord(int id)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@RoleID",id)
        };
        return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "GetLoanRequestRecord", param).Tables[0];
    }
    public virtual System.Data.DataTable GetLoanRecordHR()
    {
        return SqlHelper.ExecuteDataset(vt_Common.PayRollConnectionString, "GetLoanApprovedRecord").Tables[0];
    }
    public virtual LoanApproval_BAL GetLoanById(int id)
    {
        LoanApproval_BAL BAL = new LoanApproval_BAL();
        SqlParameter[] param =
        {
            new SqlParameter("@ID",id)
        };
        using (SqlDataReader dr = SqlHelper.ExecuteReader(vt_Common.PayRollConnectionString, "GetLoanRequestRecordByID", param))
        {
            if(dr.Read())
            {
                BAL.EntryID = Convert.ToInt32(dr["EntryID"]);
                BAL.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                BAL.AppliedDate = Convert.ToDateTime(dr["AppliedDate"]);
                BAL.EmployeeID = Convert.ToInt32(dr["EmployeeId"]);
                BAL.LoanID = Convert.ToInt32(dr["LoanId"]);
                BAL.AppliedAmount = Convert.ToDecimal(dr["AppliedAmount"]);
                BAL.EMIType = Convert.ToInt32(dr["EMIType"]);
                BAL.MonthDuration = Convert.ToInt32(dr["MonthDuration"]);
                BAL.LineManager = Convert.ToInt32(dr["LineManager"]);
                BAL.HRAdmin = Convert.ToInt32(dr["HRAdmin"]);
                BAL.CompanyName = Convert.ToString(dr["CompanyName"]);
                BAL.LoanName = Convert.ToString(dr["Name"]);
                BAL.EmployeeName = Convert.ToString(dr["EmployeeName"]);

            }
        }
        return BAL;
    }
    public virtual int Approve(int id)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@ID", id)
        };
        return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, System.Data.CommandType.StoredProcedure, "ApproveLoanRequest", param);
    }
    public virtual int ApproveByHR(int id)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@ID", id)
        };
        return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, System.Data.CommandType.StoredProcedure, "ApproveLoanRequestByHR", param);
    }
    public virtual int Reject(int id)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@ID", id)
        };
        return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, System.Data.CommandType.StoredProcedure, "RejectLoanRequest", param);
    }
    public virtual int RejectByHR(int id)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@ID", id)
        };
        return SqlHelper.ExecuteNonQuery(vt_Common.PayRollConnectionString, System.Data.CommandType.StoredProcedure, "RejectLoanRequestByHR", param);
    }
}