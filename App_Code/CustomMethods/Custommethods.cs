using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Custommethods
/// </summary>
public class Custommethods
{
    public DateTime? GetDateFromTextBox(string txtdate)
    {
        string[] formats = { "dd/MM/yyyy", "dd/MM", "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy", "MM-dd-yyyy","m/dd/yyyy" };
        DateTime dobDT;
        if (!string.IsNullOrWhiteSpace(txtdate) && DateTime.TryParseExact(txtdate, formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out dobDT))
        {
            //11:34 PM
            string dobtime = DateTime.UtcNow.AddHours(5).ToString("hh:mm tt");
            DateTime timePart = DateTime.ParseExact(dobtime, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            DateTime combinedDateTime = dobDT.Date.Add(timePart.TimeOfDay);
            return combinedDateTime;
        }
        else
        {
            return null;
        }
    }
}