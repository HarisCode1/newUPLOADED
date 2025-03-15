using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ProcedureCall
/// </summary>
public class ProcedureCall
{
    public static object sp_SingleValue(string TableName, string ColumnName, string IDTitle, int? ID)
    {
        string Query = "SELECT " + ColumnName + " FROM " + TableName + " WHERE " + IDTitle + " = " + ID;
        using (var db = new vt_EMSEntities())
        {
            return SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, Query);
        }
    }

    public static object sp_IsPresent(string TableName, string ColumnName, string Value, string ColumnName2, int Value2)
    {
        string Query = "SELECT " + ColumnName + " FROM " + TableName + " WHERE " + ColumnName + " = '" + Value + "' AND " + ColumnName2 + " = " + Value2 + "";
        using (var db = new vt_EMSEntities())
        {
            return SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, Query);
        }
    }
    public static DataSet SpCall_BindTypeOfEmployee()
    {
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[BindTypeOfEmployee]");
            return Ds;
        }
    }
    public static DataSet SpCall_VT_SP_GetDesignation()
    {
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_SP_GetDesignation]");
            return Ds;
        }
    }

    public static DataSet sp_GetEmpSalaries()
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_SP_GetCompanySalaryDetail]");
            return ds;
        }
    }
    //Get Deactive Employees
    public static DataSet sp_GetDeactiveEmp(int CompanyID)
    {
        DataSet ds = null;
        SqlParameter[] param =

           {
                        new SqlParameter("@CompanyID",CompanyID)
            };
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_SP_GetDeactiveEmployees]",param);
            return ds;
        }
    }


    //update default password
    public static DataSet Sp_SetUserDefaultPass(int CompanyID, int  ModifiedBy, int UserID, string  Passsword  )
    {
        DataSet ds = null;
        SqlParameter[] param =
            {
                           new SqlParameter("@UserID",    UserID),
                           new SqlParameter("@Passsword", Passsword),
                           new SqlParameter("@CompanyID", CompanyID),
                           new SqlParameter("@ModifiedBy",ModifiedBy)
            };
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[Sp_SetUserDefaultPass]", param);
            return ds;
        }
    }

    public static DataSet GetLineManager(int DesignationID,int DepartmentID,int CompanyID)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {
                  new SqlParameter("@DesignationID",DesignationID),
                new SqlParameter("@DepartmentID", DepartmentID),
                        new SqlParameter("@CompanyID",CompanyID)
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_sp_BindLineManager]",param);
            return ds;
        }
    }
    //For reigned employee
    public static DataSet GetSp_GetResignedEmployeeByCompanyID(int CompanyID)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {
                        new SqlParameter("@CompanyID",CompanyID)
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[Sp_GetResignedEmployeeByCompanyID]", param);
            return ds;
        }
    }
    public static DataSet GetSp_GetResignedEmployeeByEmployeeID(int CompanyID)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {
                        new SqlParameter("@CompanyID",CompanyID)
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[Sp_GetResignedEmployeeByEmployeeID]", param);
            return ds;
        }
    }
    public static DataSet GetElg()
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[getelgyears]");
            return ds;
        }
    }
    public static DataSet GetHRManager(int RoleID, int CompanyID)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {

                new SqlParameter("@RoleId",RoleID),
                        new SqlParameter("@CompanyID",CompanyID)
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_SP_BindHRAdmin]", param);
            return ds;
        }
    }

    public static DataSet sp_GetRoles()
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_SP_GetRoles]");
            return ds;
        }
    }

    public static DataSet SpCall_Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID(int CompanyID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@CompanyID",CompanyID)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Vt_Sp_GetEmployeeID_By_EmployeeName_AND_CompanyID", param);
            return ds;
        }
    }
    public static DataSet SpCall_Sp_Get_EmpAssets(int EmpID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmpID",EmpID)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Sp_Get_EmpAssets", param);
            return ds;
        }
    }

    public static DataSet SpCall_Sp_Get_Employee_TransferLog()
    {
        //SqlParameter[] param = {
        //                            new SqlParameter("@CompanyID",CompanyID)
        //                       };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Sp_Get_Employee_TransferLog", null);
            return ds;
        }
    }

    //add leaves after confirmation
    public static DataSet VT_SP_AddLeavesAfterConfirmation(int EmployeeID, int companayid , DateTime confirmationdate, string gender)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeID", EmployeeID),
                                    new SqlParameter("@CompanyID", companayid),
                                     new SqlParameter("@ConfirmDate", confirmationdate),
                                     new SqlParameter("@Gender", gender)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "VT_SP_AddLeavesAfterConfirmation", param);
            return ds;
        }
    }

    public static DataSet SpCall_Vt_Sp_Employee_Qualifications(int EmployeeID, int QualificationType)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeID", EmployeeID),
                                    new SqlParameter("@QualificationType", QualificationType),
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Vt_Sp_Employee_Qualifications", param);
            return ds;
        }
    }
    //Previous Job Info
    public static DataSet SpCall_Vt_Sp_Employee_PreviousJobInfo(int EmployeeID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeID", EmployeeID),                                    
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Vt_Sp_Employee_PreviousJobInfo", param);
            return ds;
        }
    }

    //Employee Promotion Details
    public static DataSet SpCall_Sp_Get_Employee_PromotionLog()
    {
        //SqlParameter[] param = {
        //                            new SqlParameter("@CompanyID",CompanyID)
        //                       };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Sp_Get_Employee_PromotionLog", null);
            return ds;
        }
    }

    public static DataSet SpCall_Sp_Get_Employee_PromotionLog_New(int EmployeeID)
    {
        //SqlParameter[] param = {
        //                            new SqlParameter("@CompanyID",CompanyID)
        //                       };
        DataSet ds = null;
        SqlParameter[] param = { new SqlParameter("@EmployeeID", EmployeeID) };
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Sp_Get_Employee_PromotionLog_New", param);
            return ds;
        }
    }
    public static DataSet SpCall_Sp_Get_Employee_JobLog(int EmployeeID)
    {
        DataSet ds = null;
        SqlParameter[] param = { new SqlParameter("@EmployeeID", EmployeeID) };
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Sp_Get_Employee_JobLog", param);
            return ds;
        }
    }
    public static DataSet SpCall_sp_GetMenuByUserNew(int ModuleID, int RoleID)
    {
        SqlParameter[] param = {
            new SqlParameter("@ModuleId", ModuleID),
            new SqlParameter("@RoleId",RoleID)
        };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "sp_GetMenuByUserNew", param);
            return ds;
        }
    }
    public static bool SpCall_sp_CheckLineManagerw(int EmployeeID)
    {
        bool IsLineManager = false;
        SqlParameter[] param = {
            new SqlParameter("@EmployeeID", EmployeeID)
        };
        using (var db = new vt_EMSEntities())
        {
            IsLineManager = Convert.ToBoolean(SqlHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "sp_CheckLineManager", param));
        }
        return IsLineManager;
    }

    public static DataSet SpCall_Employee_ChangeDesignationLogs()
    {
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Employee_ChangeDesignationLogs", null);
            return Ds;
        }
    }

    public static DataSet SpCall_sp_get_Tax_Result()
    {
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "sp_get_Tax", null);
            return Ds;
        }
    }

    public static DataSet SpCall_Job_SP_UpdateEmployeesPromotion()
    {
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Job_SP_UpdateEmployeesPromotion", null);
            return Ds;
        }
    }

    public static DataSet SpCall_Vt_Sp_GetAttendance(int EmployeeID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeID", EmployeeID)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Vt_Sp_GetAttendance", param);
            return ds;
        }
    }

    public static DataSet SpCall_VT_SP_GetDesignation(int CompanyID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@CompanyID", CompanyID)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "VT_SP_GetDesignation", param);
            return ds;
        }
    }
    public static DataSet SpCall_BindLeaveApplicationToHR(int compID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@CompanyID", compID)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "BindLeaveApplicationToHR", param);
            return ds;
        }
    }
    public static DataSet SpCall_BindLeaveApplicationToLM( int ManagerID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@ManagerID", ManagerID)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "BindLeaveApplicationToLM", param);
            return ds;
        }
    }
    public static DataSet SpCall_VT_SP_GetLeaveApplications(int CompanyID,int EmployeeID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@CompanyID", CompanyID),
                                    new SqlParameter("@EmployeeID", EmployeeID)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "VT_SP_GetLeaveApplications", param);
            return ds;
        }
    }

    public static DataSet SpCall_Vt_Sp_SalaryNotification(int EmployeeID, int Month, int Year)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeID", EmployeeID),
                                    new SqlParameter("@Month", Month),
                                    new SqlParameter("@Year", Year)
                               };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Vt_Sp_SalaryNotification", param);
            return ds;
        }
    }

    public static DataSet SpCall_VT_SP_GetLoanEntry(int CompanyID)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@CompanyID", CompanyID)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "VT_SP_GetLoanEntry", param);
            return Ds;
        }
    }
    public static DataSet Sp_Call_vt_Sp_BindLoanEntryLog()
    {
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "dbo.vt_Sp_BindLoanEntryLog", null);
            return Ds;
        }
    }

    public static int Sp_Call_SetEmpSalaries(System.DateTime startDate, string companyID, int credtbyEmp, System.DateTime NowDate)
    {
        NowDate = System.DateTime.Now;
        int Result = 0;

        SqlParameter[] param = {
                                    new SqlParameter("@currentMonthDate", startDate),
                                    new SqlParameter("@CompanyID", companyID),
                                    new SqlParameter("@CreatedBy", credtbyEmp),
                                    new SqlParameter("@CreatedDate", NowDate)
                               };
        using (var db = new vt_EMSEntities())
        {
            Result = SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_SP_SetEmpSalaries8Feb2019]", param);
            return Result;
        }
    }
    public static DataSet Sp_Call_SetEmpSalariesBankWise(System.DateTime startDate, int companyID, string BankName)
    {
      //  NowDate = System.DateTime.Now;
        int Result = 0;

        SqlParameter[] param = {
                                    new SqlParameter("@currentMonthDate", startDate),
                                    new SqlParameter("@CompanyID", companyID),
                                    new SqlParameter("@BankName", BankName)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "VT_SP_GetEmpSalariesBankWise", param);
            return Ds;
        }
   
        //using (var db = new vt_EMSEntities())
        //{
        //    Result = SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "dbo.VT_SP_GetEmpSalariesBankWise", param);
        //    return Result;
        //}
    }
    public static DataSet Sp_Call_SetEmpSalariesyearlySummarySheet(int Companyid, int EmployeeID,int year)
    {
        //  NowDate = System.DateTime.Now;
        int Result = 0;

        SqlParameter[] param = {
                                    new SqlParameter("@Companyid", Companyid),
                                    new SqlParameter("@EmployeeID", EmployeeID),
                                    new SqlParameter("@year", year)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "vt_sp_DepartmentyearlyWieSummarySheet", param);
            return Ds;
        }

        //using (var db = new vt_EMSEntities())
        //{
        //    Result = SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "dbo.VT_SP_GetEmpSalariesBankWise", param);
        //    return Result;
        //}
    }
    public static DataSet Sp_Call_SetEmpSalariesCashWise(System.DateTime startDate, int companyID, string BankName)
    {
        //  NowDate = System.DateTime.Now;
        int Result = 0;

        SqlParameter[] param =
        {
                                    new SqlParameter("@currentMonthDate", startDate),
                                    new SqlParameter("@CompanyID", companyID),
                                    new SqlParameter("@BankName", BankName)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "VT_SP_GetEmpSalariesCashWise", param);
            return Ds;
        }

        //using (var db = new vt_EMSEntities())
        //{
        //    Result = SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "dbo.VT_SP_GetEmpSalariesBankWise", param);
        //    return Result;
        //}
    }
    public static DataSet Sp_Call_SetEmpSalariesSummarySheetMonthWise(int Companyid, System.DateTime MonthWise)
    {
        //  NowDate = System.DateTime.Now;
        int Result = 0;

        SqlParameter[] param = {
                                    new SqlParameter("@Companyid", Companyid),
                                    new SqlParameter("@MonthWise", MonthWise)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "vt_sp_DepartmentWieSummarySheet", param);
            return Ds;
        }

        //using (var db = new vt_EMSEntities())
        //{
        //    Result = SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "dbo.VT_SP_GetEmpSalariesBankWise", param);
        //    return Result;
        //}
    }
    public static DataSet Sp_Call_SetEmployeeTranferRecords(int EmployeeID)
    {        
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeID", @EmployeeID)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "vt_sp_GetEmptranferrecord", param);
            return Ds;
        }

        //using (var db = new vt_EMSEntities())
        //{
        //    Result = SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "dbo.VT_SP_GetEmpSalariesBankWise", param);
        //    return Result;
        //}
    }
    public static DataSet Sp_Call_SetEmpSalariesChequeWise(System.DateTime startDate, int companyID, string BankName)
    {
        //  NowDate = System.DateTime.Now;
        int Result = 0;

        SqlParameter[] param = {
                                    new SqlParameter("@currentMonthDate", startDate),
                                    new SqlParameter("@CompanyID", companyID),
                                    new SqlParameter("@BankName", BankName)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "VT_SP_GetEmpSalariesChequeWise", param);
            return Ds;
        }

        //using (var db = new vt_EMSEntities())
        //{
        //    Result = SqlHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "dbo.VT_SP_GetEmpSalariesBankWise", param);
        //    return Result;
        //}
    }    
    public static DataSet SpCall_Vt_Sp_GetCoreManagement(int CompanyID)
    {
        SqlParameter[] param =
        {
            new SqlParameter("@CompanyID", CompanyID)
        };
        DataSet Ds = null;
        using (var db = new vt_EMSEntities())
        {
            Ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "Vt_Sp_GetCoreManagement", param);
            return Ds;
        }
    }
    public static DataSet SpCall_VT_SP_GetEmpSalaries6Feb2019(int EmployeeId, int Companyid)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@currentMonthDate", System.DateTime.Now),
                                    new SqlParameter("@CompanyID", Companyid),
                                    new SqlParameter("@CompanyID", EmployeeId)
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[VT_SP_GetEmpSalaries6Feb2019]", param);
            DataTable dt = a.Tables[0];
            return a;
        }
    }
    public static DataSet SP_GetSalaryCustom(int Companyid,int EmployeeId)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@currentMonthDate", System.DateTime.Now),
                                    new SqlParameter("@CompanyID", Companyid),
                                    new SqlParameter("@EmployeeId", EmployeeId)
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[SP_GetSalaryCustom]", param);
            return a;
        }
    }

    public static DataSet SP_GetDeletedEmp(int Companyid)
    {
        SqlParameter[] param = {
                                
                                    new SqlParameter("@CompanyID", Companyid),
                                  
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[SP_GetDeletedEmp]", param);
            return a;
        }
    }
    public static DataSet VT_SP_GetEmployee_ByID(int EmployeeId)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeId", EmployeeId)
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[VT_SP_GetEmployee_ByID]", param);
            return a;
        }
    }
    public static DataSet vt_AttendanceAdd()
    {
        SqlParameter[] param = {};
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[vt_AttendanceAdd]", param);
            return a;
        }
    }


    public static DataSet VT_Sp_UserbyCompanyIDandUserId(int CompanyID,int UserID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@CompanyID", CompanyID),
                                    new SqlParameter("@UserID", UserID)
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[Sp_UserbyCompanyIDandUserId]", param);
            return a;
        }
    }

    //Delete sp for designation
    public static DataSet vt_sp_deleteParentandChilddesignationById(int DesignationID , int CompanyID)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@DesignationID", DesignationID),
                                    new SqlParameter("@CompanyID", CompanyID)
                                    
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[vt_sp_deletedesignationById]", param);
            return a;
        }
    }
    //Delete sp for Employee
    public static DataSet Vt_Sp_DeleteEmployeePermanent(int EmployeeID, int userid )
    {
        SqlParameter[] param = {
                                     new SqlParameter("@EmployeeID", EmployeeID),
                                     new SqlParameter("@userid", userid)

                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[Sp_DeleteEmployeePermanent]", param);
            return a;
        }
    }

    public static DataSet Vt_Sp_UpdateEmpType(int EmployeeID,  int companyid)
    {
        SqlParameter[] param = {
                                     new SqlParameter("@EmployeeID", EmployeeID),
                                     new SqlParameter("@companyid", companyid)

                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[Sp_DeleteEmployeePermanent]", param);
            return a;
        }
    }

    public static DataSet Sp_activeEmployee(int EmployeeID )
    {
        SqlParameter[] param = {
                                    new SqlParameter("@EmployeeID", EmployeeID)

                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[Sp_activeEmployee]", param);
            return a;
        }
    }


    public static DataSet GetAttendance()
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[Sp_GetAttendanceState]");
            return ds;
        }
    }
    public static DataSet Get_SP_GetEmployees(int CompanyID)
    {
        DataSet ds = null;
        SqlParameter[] param = { new SqlParameter("@CompanyID", CompanyID)};
        using (var db = new vt_EMSEntities())
        {

            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_SP_GetEmployees]", param);
            return ds;
        }
    }
    
    public static DataSet vt_AttendanceAddByTemptable()
    {
        SqlParameter[] param = { };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[sp_InsertAttendanceFromTempTable]", param);
            return a;
        }
    }

    public static DataSet GetAttendanceMonthWise(int CompanyID, int employeeID, Nullable<DateTime> SearchByDate)
    {
        SqlParameter[] param = {
                 new SqlParameter("@companyID", CompanyID),
                  new SqlParameter("@employeeID", CompanyID),
                   new SqlParameter("@SearchByDate", SearchByDate),
        };
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {

            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[Vt_Sp_GetAttendance_By_CompanyID_OR_EmployeeID]", param);
            return ds;
        }
    }


        //For demo link
    public static DataSet SP_GenerateSalaryDemoLink(DateTime currentMonthDate, int Companyid)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@currentMonthDate", currentMonthDate),
                                    new SqlParameter("@CompanyID", Companyid),
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[VT_SP_SetEmpSalariesDemoLink]", param);
            return a;
        }
    }


    public static DataSet SP_GetTimout(string currentMonthDate, int Companyid)
    {
        SqlParameter[] param = {
                                    new SqlParameter("@currentMonthDate", currentMonthDate),
                                    new SqlParameter("@CompanyID", Companyid),
                               };
        using (var db = new vt_EMSEntities())
        {
            DataSet a = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, "[dbo].[VT_SP_GetTimeOUt]", param);
            return a;
        }
    }


    public static DataSet UpdateOutTime(int DesignationID, int DepartmentID, int CompanyID)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {
                  new SqlParameter("@DesignationID",DesignationID),
                new SqlParameter("@DepartmentID", DepartmentID),
                        new SqlParameter("@CompanyID",CompanyID)
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[VT_sp_BindLineManager]", param);
            return ds;
        }
    }


    public static DataSet GetHolidays(string Date, int CompanyID, string DesignationID, string Rules)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {
                  new SqlParameter("@Date",Date),
                        new SqlParameter("@CompanyID",@CompanyID),
                           new SqlParameter("@DesignationID",@DesignationID),
                        new SqlParameter("@Rules",@Rules)
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[SP_DeleteHolidays]", param);
            return ds;
        }
    }

    //FOR staff bonus

    public static DataSet GetStaffBonus(int CompanyID)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {
                        new SqlParameter("@CompanyID",CompanyID)
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[GetStaffBonus]", param);
            return ds;
        }
    }
    public static DataSet DeleteLoanById(int ID, int CompanyID)
    {
        DataSet ds = null;
        using (var db = new vt_EMSEntities())
        {
            SqlParameter[] param =

            {
                  new SqlParameter("@ID",ID),
                new SqlParameter("@CompanyID", CompanyID),
            };
            ds = SqlHelper.ExecuteDataset(db.Database.Connection.ConnectionString, CommandType.StoredProcedure, "[dbo].[Sp_DeleteLoanByID]", param);
            return ds;
        }
    }


}