using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;
using System.Linq;
using System.ComponentModel;

namespace Viftech
{
    /// <summary>
    /// Summary description for Common
    /// </summary>
    /// 
    public class vt_Common
    {
        public vt_Common()
        {

        }

        public static string SendStatus;
        
        public static string ConStr
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["vt_ConnectionString"] == null)
                {
                    return null;
                }
                string myCon = ConfigurationManager.ConnectionStrings["vt_ConnectionString"].ToString();
                return myCon;
            }
        }

        public static string PayRollConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["vt_PayRollConnection"] == null)
                {
                    return null;
                }
                string myCon = ConfigurationManager.ConnectionStrings["vt_PayRollConnection"].ToString();
                return myCon;
            }
        }

        public static int CompanyId
        {
            get
            {
                if (HttpContext.Current.Session["EMS_Session"] != null)
                {
                    return ((EMS_Session)HttpContext.Current.Session["EMS_Session"]).Company.CompanyID;
                }
                else
                    return -1;
            }
        }

        public static int RoleID
        {
            get
            {
                if (HttpContext.Current.Session["EMS_Session"] != null)
                {
                    return Convert.ToInt32(((EMS_Session)HttpContext.Current.Session["EMS_Session"]).user.RoleId);
                }
                else
                    return -1;
            }
        }

        public static byte[] getKey
        {
            get
            {
                return ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["EncryptKey"].ToString());
            }
        }
        //here
        public static int GetModuleId(string Page_Url, DataTable PermissionTable)
        {

            int moduleID = 0;
            DataTable dt = PermissionTable;
            DataRow[] dr = dt.Select("PageUrl = '" + Page_Url + "'");

            foreach (DataRow r in dr)
            {
                moduleID = Convert.ToInt32(r["ModuleId"].ToString());
            }

            return moduleID;
        }
        public static int GetPageId(string Page_Url, DataTable PermissionTable)
        {

            int PageId = 0;
            DataTable dt = PermissionTable;
            DataRow[] dr = dt.Select("PageUrl = '" + Page_Url + "'");

            foreach (DataRow r in dr)
            {
                PageId = Convert.ToInt32(r["PageId"].ToString());
            }

            return PageId;
        }

        public static string RenderUserControl(string path, string propertyName, object propertyValue)
        {
            Page pageHolder = new Page();
            UserControl viewControl =
               (UserControl)pageHolder.LoadControl(path);

            if (propertyValue != null)
            {
                Type viewControlType = viewControl.GetType();
                PropertyInfo property =
                   viewControlType.GetProperty(propertyName);

                if (property != null)
                {
                    property.SetValue(viewControl, propertyValue, null);
                }
                else
                {
                    throw new Exception(string.Format("UserControl: {0} does not have a public {1} property.", path, propertyName));
                }
            }

            pageHolder.Controls.Add(viewControl);
            StringWriter output = new StringWriter();
            HttpContext.Current.Server.Execute(pageHolder, output, false);
            return output.ToString();
        }

        public static string GetByteContent(byte[] content)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(content);
        }               

        public static bool checkEmail(string EmailAddress)
        {
            string pattern = @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(EmailAddress);
        }

        public static int GetColumnIndexByName(GridView grid, string name)
        {
            foreach (DataControlField col in grid.Columns)
            {
                if (col.HeaderText.ToLower().Trim() == name.ToLower().Trim())
                {
                    return grid.Columns.IndexOf(col);
                }
            }

            return -1;
        }

        public static string RemoveHTML(string StringWithHTML)
        {
            if (string.IsNullOrEmpty(StringWithHTML)) return string.Empty;
            return Regex.Replace(StringWithHTML, @"<(.|\n)*?>", string.Empty);
        }

        public static string Find(string input, string StartStr, string LastStr)
        {
            int Start = input.IndexOf(StartStr);
            int length = (input.LastIndexOf(LastStr) - Start) + LastStr.Length;
            return input.Substring(Start, length);
        }

        public static string UppercaseFirst(string s, char separator)
        {
            s = s.ToLower().Trim();
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            string[] word = s.Split(separator);
            string NewStr = "";
            for (int i = 0; i < word.Length; i++)
            {
                char[] a = word[i].ToCharArray();
                if (a.Length > 0)
                {
                    a[0] = char.ToUpper(a[0]);
                    if (i == word.Length - 1)
                        NewStr += new string(a);
                    else
                        NewStr += new string(a) + separator;
                }
            }
            return NewStr;
        }

        public static string getVisitorsIP()
        {
            string VisitorsIPAddr = string.Empty;
            //Users IP Address.                
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                //To get the IP address of the machine and not the proxy
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;

            return VisitorsIPAddr;
        }

        public static string getVisitorsBrowserInfo()
        {
            string VisitorsBrowserInfo = string.Empty;
            HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            string UserAgent = HttpContext.Current.Request.UserAgent;
            VisitorsBrowserInfo = "{Browser-Capabilities "
                     + "(Type = '" + browser.Type + "' "
                     + "Name = '" + browser.Browser + "' "
                     + "Version = '" + browser.Version + "' "
                     + "Major Version = '" + browser.MajorVersion + "' "
                     + "Minor Version = '" + browser.MinorVersion + "' "
                     + "Platform = '" + browser.Platform + "' "
                     + "Is Win32 = '" + browser.Win32 + "' "
                     + "Is Beta = '" + browser.Beta + "' "
                     + "Supports Cookies = '" + browser.Cookies + "' "
                     + "Supports ECMAScript = '" + browser.EcmaScriptVersion.ToString() + "' "
                     + "Supports JavaScript Version = '" + browser.JScriptVersion + "' "
                     + "UserAgent = '" + UserAgent + "')}";
            return VisitorsBrowserInfo;
        }

        public static string get_setting(string name, string default_value)
        {
            NameValueCollection name_values = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("appSettings");
            if (string.IsNullOrEmpty(name_values[name]))
            {
                return default_value;
            }
            else
            {
                return name_values[name];
            }
        }

        public static string SubStr(string str, int length)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (str.Length > length)
                return str.Substring(0, length) + "...";
            else
                return str;
        }

        public static string SubStrSimple(string str, int length)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            if (str.Length > length)
                return str.Substring(0, length);
            else
                return str;
        }

        public static object ValidateValue(int intValue)
        {
            if (intValue == 0)
            {
                return DBNull.Value;
            }
            else
            {
                return intValue;
            }
        }

        public static object ValidateValue(string strValue)
        {
            if (strValue == null)
            {
                return DBNull.Value;
            }
            else
            {
                return strValue;
            }
        }

        public static object ValidateValue(DateTime dtValue)
        {
            if ((dtValue == null) || (dtValue == DateTime.MinValue))
            {
                return DBNull.Value;
            }
            else
            {
                return dtValue;
            }
        }

        public static object ValidateValue(double dblValue)
        {
            if ((dblValue == null))
            {
                return DBNull.Value;
            }
            else
            {
                return dblValue;
            }
        }

        public static int CheckInt(object value)
        {
            int parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : int.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }
        public static Int64 CheckInt64(object value)
        {
            Int64 parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : Int64.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }

        public static double CheckDouble(object value)
        {
            double parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : double.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }

        public static decimal Checkdecimal(object value)
        {
            decimal parseVal;
            return ((value == null) || (value == DBNull.Value)) ? 0 : decimal.TryParse(value.ToString(), out parseVal) ? parseVal : 0;
        }

        public static DateTime CheckDateTime(object value)
        {
            DateTime parseVal;
            return ((value == null) || (value == DBNull.Value)) ? GetDefaultDate() : DateTime.TryParse(value.ToString(), out parseVal) ? parseVal : GetDefaultDate();
        }

        public static string CheckString(object value)
        {
            return ((value == null) || (value == DBNull.Value)) ? GetDefaultString() : value.ToString();
        }

        public static bool CheckBoolean(object value)
        {
            bool parseVal;
            return ((value == null) || (value == DBNull.Value)) ? GetDefaultBoolean() : bool.TryParse(value.ToString(), out parseVal) ? parseVal : GetDefaultBoolean();
        }

        public static DateTime GetDefaultDate()
        {
            return new DateTime(1900, 1, 1);
        }

        public static bool GetDefaultBoolean()
        {
            return false;
        }

        public static string GetDefaultString()
        {
            return string.Empty;
        }

        public static string GetUniqueID()
        {
            int maxSize = 30;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            System.Security.Cryptography.RNGCryptoServiceProvider crypto = new System.Security.Cryptography.RNGCryptoServiceProvider();
            //crypto.GetNonZeroBytes(data);

            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            System.Text.StringBuilder result = new System.Text.StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        public static bool WriteErrorFile(string contents, string strPath)
        {
            StreamWriter writer = new StreamWriter(strPath, true);
            writer.Write(contents);
            writer.Flush();
            writer.Close();
            writer.Dispose();
            writer = null;
            return true;
        }

        /// <summary>
        /// Reloads current page
        /// </summary>
        /// <param name="UseSSL">Use SSL</param>
        public static void ReloadCurrentPage(bool UseSSL)
        {
            string result = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_HOST"] != null)
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
            }
            result = "http://" + result;
            if (!result.EndsWith("/"))
            {
                result += "/";
            }

            if (UseSSL)
            {
                result = result.Replace("http:/", "https:/");
                result = result.Replace("www.www", "www");
            }



            if (result.EndsWith("/"))
            {
                result = result.Substring(0, result.Length - 1);
            }
            string URL = result + HttpContext.Current.Request.RawUrl;
            HttpContext.Current.Response.Redirect(URL);
        }

        /// <summary>
        /// Ensures that requested page is secured (https://)
        /// </summary>
        public static void EnsureSSL()
        {
            if (!HttpContext.Current.Request.IsSecureConnection)
            {
                if (!HttpContext.Current.Request.Url.IsLoopback)
                {
                    ReloadCurrentPage(true);
                }
            }
        }

        /// <summary>
        /// Ensures that requested page is not secured (http://)
        /// </summary>
        public static void EnsureNonSSL()
        {
            if (HttpContext.Current.Request.IsSecureConnection)
            {
                ReloadCurrentPage(false);
            }
        }

        public static string ConvertCurrency(string To, string Amount, HttpRequest Request)
        {
            string Expression = Amount + "USD" + "=?" + To;
            string url = "http://www.google.com/ig/calculator?hl=en&q=" + Expression;

            string response = "";
            string responseMsg = Request.Params.ToString();
            string post = responseMsg;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = post.Length;

            StreamWriter writer = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            writer.Write(post);
            writer.Close();

            StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream());
            response = reader.ReadToEnd();
            reader.Close();

            char[] cChar = new char[3];
            string[] _params = new string[100];

            cChar[0] = ',';
            _params = response.Split(cChar[0]);

            string ConvertedAmount = "";

            ConvertedAmount = _params[1];
            ConvertedAmount = ConvertedAmount.Replace("\"", "");
            ConvertedAmount = ConvertedAmount.Replace("rhs", "");
            ConvertedAmount = ConvertedAmount.Replace(":", "");
            ConvertedAmount = ConvertedAmount.Trim();
            ConvertedAmount = ConvertedAmount.Remove(ConvertedAmount.IndexOf(' '), ConvertedAmount.Length - ConvertedAmount.IndexOf(' '));

            return ConvertedAmount;

        }

        // Detact Bots And Crawlers
        public static bool IsBot
        {
            get
            {
                // If this method can't access the current context that means the executing thread doesn't have access
                // to the current request's properties ... since we can't pull any agent information we have to assume
                // this is not a bot.
                if (HttpContext.Current == null)
                    return false;

                string HTTP_USER_AGENT = "";
                if (HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null)
                    HTTP_USER_AGENT = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToLower();

                // Check to see if the user agent field contains any of the terms in the botRegex set in the web.config
                string expression = ConfigurationManager.AppSettings["botRegex"];
                Regex botRegex = new Regex(expression);
                return botRegex.IsMatch(HTTP_USER_AGENT);
            }
        }

        public static void ReloadJS(Page page, string Function)
        {
            ScriptManager.RegisterStartupScript(page, typeof(string), Guid.NewGuid().ToString(), Function, true);
        }

        public static string Encrypt(string originalString)
        {
            return Encrypt(originalString, getKey);
        }

        public static string Encrypt(string originalString, byte[] bytes)
        {
            try
            {
                if (String.IsNullOrEmpty(originalString))
                {
                    throw new ArgumentNullException("The string which needs to be encrypted can not be null.");
                }

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

                StreamWriter writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Decrypt(string originalString)
        {
            return Decrypt(originalString, getKey);
        }

        public static string Decrypt(string cryptedString, byte[] bytes)
        {
            try
            {
                if (String.IsNullOrEmpty(cryptedString))
                {
                    throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
                }

                cryptedString = Regex.Replace(cryptedString, "[ ]", "+");

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);

                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static void Clear(ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl.HasControls())
                {
                    Clear(ctrl.Controls);
                }

                if (ctrl is TextBox)
                {
                    if (((TextBox)ctrl).TextMode == TextBoxMode.Password)
                    {
                        ((TextBox)ctrl).Attributes.Add("value", "");
                    }
                    else
                    {
                        ((TextBox)ctrl).Text = string.Empty;
                    }
                }
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Checked = false;
                }
                if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).Checked = false;
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).SelectedIndex = -1;   // -1 is the value to use for none selected in a drop down list
                }
                else if (ctrl is CheckBoxList)
                {
                    ((CheckBoxList)ctrl).SelectedIndex = 0;
                }
                else if (ctrl is ListBox)
                {
                    ((ListBox)ctrl).SelectedIndex = -1;     // -1 is the value to use for none selected in a list box
                }
                else if (ctrl is RadioButtonList)
                {
                    ((RadioButtonList)ctrl).SelectedIndex = 0;
                }
                else if (ctrl is HtmlInputRadioButton)
                {
                    ((HtmlInputRadioButton)ctrl).Checked = false;
                }
                else if (ctrl is HtmlTextArea)
                {
                    ((HtmlTextArea)ctrl).Value = "";
                }
             
            }
        }

        public static void ChangeControlStatus(bool status, ControlCollection Controls)
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = status;
                else if (c is CheckBox)
                    ((CheckBox)c).Enabled = status;
                else if (c is RadioButton)
                    ((RadioButton)c).Enabled = status;
                else if (c is Button)
                    ((Button)c).Enabled = status;
                else if (c is DropDownList)
                    ((DropDownList)c).Enabled = status;
                else if (c is HtmlInputRadioButton)
                    ((HtmlInputRadioButton)c).Disabled = !status;
                else if (c is HtmlTextArea)
                    ((HtmlTextArea)c).Disabled = !status;
            }
        }

        public static void Success_Message(Page page)
        {
            vt_Common.ReloadJS(page, "$('#Notification_Success').fadeIn('slow').delay('4000').fadeOut('slow');");
        }

        public static void PrintfriendlySqlException(SqlException ex, Page page)
        {
            switch (ex.Number)
            {
                case SQLErrorCode.DELETE_Statement_Conflicted_With_The_FOREIGN_KEY_Constraint:
                    MsgBox.Show(page, "Unable to delete entry dependent data found!");
                    break;
                default:
                    break;
            }
        }

        public static void MessageSecurity(Page page, EMS_Session s, string msg)
        {
            MsgBox.Show(page, MsgBox.info, "Security", string.Format("User not Allowed to ({0})", msg));
        }
        public static void Bind_GridView(GridView GridViewName, DataTable DataSource)
        {
            GridViewName.DataSource = DataSource;
            GridViewName.DataBind();
        }
        public static void Bind_DropDown(DropDownList ComboboxName, string SpName, string DataTextField, string DataValueField, params object[] parameterValues)

        {
            DataTable dt1 = SqlHelper.ExecuteDataset(PayRollConnectionString, SpName, parameterValues).Tables[0];
            DataTable dt = new DataTable();

            //if (dt1.Rows.Count > 0)
            //{
            //    // Debugging: Verify the data
            //    foreach (DataRow row in dt1.Rows)
            //    {
            //        Console.WriteLine(row["DesignationID"] + " - " + row["Designation"]);
            //    }
            //}

            dt = SqlHelper.ExecuteDataset(PayRollConnectionString, SpName, parameterValues).Tables[0];
            ComboboxName.DataTextField = DataTextField;
            ComboboxName.DataValueField = DataValueField;
            DataRow dr = dt.NewRow();

            if (SpName!= "VT_sp_BindLineManager" && SpName != "VT_sp_BindLineManagerName" && SpName != "VT_SP_BindHRAdmin" && SpName != "VT_SP_BindDesigByDepID" && SpName!= "VT_SP_BindDepart")
            {
                dr[DataTextField] = "Select One"; // Yeh woh text hoga jo combobox mein dikhai dega
                dr[DataValueField] = "0";         // Default value jo select hone par milegi
                dt.Rows.InsertAt(dr, 0);
            }
            

  
            ComboboxName.DataSource = dt;
            ComboboxName.DataBind();
            //ComboboxName.SelectedIndex = 0;
        }

        //Parvaiz Code
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        //IF NULL STRING THEN RETURNS -
        public static string IsStringEmpty(string Val)
        {
            if (Val == null || string.IsNullOrEmpty(Val) || string.IsNullOrWhiteSpace(Val))
            {
                return "-";
            }
            else
            {
                return Val;
            }
        }
        //IF NULL STRING THEN RETURNS 0
        public static int? IsNumber(int? Val)
        {
            if (Val > 0)
            {
                return Val;
            }
            else
            {
                return 0;
            }
        }

        public static bool IsAuthenticated(string PageUrl,string UserName, DataTable Dt)
        {
            
            bool isAuthenticate = false;
            if (UserName == "SuperAdmin")
            {
                isAuthenticate = true;
            }
            else
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    if (PageUrl == Dt.Rows[i]["PageUrl"].ToString())
                    {
                        isAuthenticate = true;
                        break;
                    }
                    else
                    {
                        isAuthenticate = false;
                    }
                }
            }
            
            return isAuthenticate;
        }
    }
}