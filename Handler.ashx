<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Data.SqlClient;
using System.Web;

public class Handler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {

        SqlDataReader rdr = null;
        SqlConnection conn = null;
        SqlCommand selcmd = null;
        try
        {
            conn =
                new SqlConnection(
                    @"Server=VIFTECH-SERVER\SQLEXPRESS; DataBase=vt_EMS; User ID=sajjad; Password=SrDev2123net!");
            selcmd = new SqlCommand("select photo from vt_tbl_EmployeePhoto where enrollid=3",
                conn);
            conn.Open();
            rdr = selcmd.ExecuteReader();
            while (rdr.Read())
            {
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite((byte[]) rdr["Photo"]);
            }
            rdr.Close();
        }
        finally
        {
            conn.Close();
        }


        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    
    

}