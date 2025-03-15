using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

/// <summary>
/// Summary description for ErrHandler
/// </summary>
public class ErrHandler
{
    public ErrHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// Handles error by accepting the error message 
    /// Displays the page on which the error occured
    public static void WriteError(string errorMessage)
    {
        string path = "";
        try
        {
           // path = "~/Error/" + Guid.NewGuid() + ".txt";
            path = "~/Error/" + DateTime.Today.ToString("MM-dd-yy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                              ". Error Message:" + errorMessage;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }

            //SendEmail("shahbaz@viftech.com.pk", "eCTS Errors", "LogPath : " + path + "<br>ERROR: " + errorMessage);
        }
        catch (Exception ex)
        {
            //SendEmail("shahbaz@viftech.com.pk", "eCTS Errors", "LogPath : " + path + "<br>ERROR: " + errorMessage + "<br>ERROR: " + ex.Message);
        }

    }

    public static bool SendEmail(string to, string subject, string body)
    {
        MailMessage msg = new MailMessage();
        //Sender email and displayName
        msg.From = new MailAddress("info@vif-tech.com", "Viftech Solutions");
        msg.To.Add(to);
        msg.Subject = subject;
        msg.Body = body;
        msg.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.vif-tech.com";
        smtp.Credentials = new NetworkCredential("info@vif-tech.com", "inter123net");
        smtp.Port = 25;
        smtp.EnableSsl = false;
        try
        {
            smtp.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw ex;
        }
    }

    public static void TryCatchException(Exception ex)
    {
        StackFrame sf = new StackFrame(true);
        string methodName = sf.GetMethod().ToString();
        int lineNumber = sf.GetFileLineNumber();
        HttpContext ctx = HttpContext.Current;
        Exception exception = ctx.Server.GetLastError();
        string errorInfo =
        Environment.NewLine + "  [Offending URL]: " + ctx.Request.Url.ToString() +
        Environment.NewLine + "  [Source]: " + ex.Source +
        Environment.NewLine + "  [Message]: " + ex.Message +
        Environment.NewLine + "  [Method]: " + methodName.ToString() +
        Environment.NewLine + "  [LineNumber]: " + lineNumber.ToString() +
        Environment.NewLine + "  [Stack trace]: " + ex.StackTrace;
        ErrHandler.WriteError(errorInfo);
        HttpContext.Current.Server.ClearError();
    }
}