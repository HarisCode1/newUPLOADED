using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for EmailSender
/// </summary>
public class EmailSender
{
    public EmailSender()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    public static bool SendEmail(string to, string cc, string bcc, string subject, string body, List<string> Attachements)
    {
        MailMessage msg = new MailMessage();
        //Sender email and displayName
        msg.From = new MailAddress("", "Payroll Viftech");
        msg.To.Add(to);
        if (cc != null && cc != "")
        {
            msg.CC.Add(cc);
        }

        if (bcc != null && bcc != "")
        {
            msg.CC.Add(bcc);
        }

        if (Attachements != null)
        {
            foreach (var item in Attachements)
            {
                msg.Attachments.Add(new Attachment(item));
            }
        }
        //msg.Bcc.Add(new MailAddress(bcc));
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
}