using System.Web;
public class Authenticate
{
    private static bool isLogin { get; set; }

    public static bool Confirm()
    {
        if (HttpContext.Current.Session["UserId"] == null)
        {
            isLogin = false;
            HttpContext.Current.Response.Redirect("login.aspx", true);
        }
        else
        {
            isLogin = true;
        }
        return isLogin;
    }
}