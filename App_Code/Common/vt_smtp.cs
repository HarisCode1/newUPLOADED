using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

public class vt_smtp
{
    public static void vt_smtpSend(string EmailBody, string ToEmailID)
    {
        try
        {
            MailMessage m = new MailMessage("pviftech@gmail.com", "pviftech@gmail.com");
            m.Subject = "Testing";
            m.Body = EmailBody;
            m.IsBodyHtml = true;
            m.From = new MailAddress("pviftech@gmail.com");
            m.To.Add(new MailAddress(ToEmailID)); //"pviftech@gmail.com"
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Host = "smtp.gmail.com";

            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("pviftech@gmail.com", "Viftech123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Port = 587;
            smtp.Send(m);
        }
        catch (Exception ex)
        {

        }
    }
}