using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

/// <summary>
/// Summary description for PostImage
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PostImage : System.Web.Services.WebService
{

    public PostImage()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string PostFiles(string Reason)
    {
        string retunjson = "";
       // HttpContext context = HttpContext.Current;

     

        if (HttpContext.Current.Request.Files["ProfileImg"] != null)
        {

            HttpPostedFile ProfileImg = HttpContext.Current.Request.Files["ProfileImg"];
          
            string Extenion = Path.GetExtension(ProfileImg.FileName);
            if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                Extenion.ToLower() == ".bmp")
            {
                retunjson = ProfileImg.FileName;
                string ext = Extenion.Substring(1);
                //ViewState["ContenType"] = "Image/" + ext;
              //  ProfileImg.SaveAs(Server.MapPath("~/images/Employees/" + retunjson));
            }
        }

       
        vt_EMSEntities db = new vt_EMSEntities();
        vt_tbl_terminatedemployees termEmp = new vt_tbl_terminatedemployees();
        //vt_tbl_Employee Obj = db.vt_tbl_Employee.Where(x => x.EmployeeID == EmployeeID).FirstOrDefault();
        //if (Obj != null)
        //{

        //    Obj.JobStatus = "InActive";
        //    db.Entry(Obj).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    termEmp.EmployeeID = EmployeeID;
        //    termEmp.CompanyID = CompanyID;
        //    termEmp.Reason = Reason;
        //    termEmp.Status = true;
        //    termEmp.TerminatedDate = Convert.ToDateTime(date);
        //    // db.vt_tbl_terminatedemployees.Add(termEmp);
        //    //  db.SaveChanges();
        //    return true;
        return retunjson;
      

    }
    [WebMethod]
    public string PostFile()
    {
        string retunjson = "";
        if (HttpContext.Current.Request.Files["ProfileImg"] != null)
        {

            HttpPostedFile ProfileImg = HttpContext.Current.Request.Files["ProfileImg"];
            string Extenion = Path.GetExtension(ProfileImg.FileName);
            if (Extenion.ToLower() == ".jpg" || Extenion.ToLower() == ".png" || Extenion.ToLower() == ".gif" || Extenion.ToLower() == ".jpeg" ||
                Extenion.ToLower() == ".bmp")
            {
                retunjson = ProfileImg.FileName;
                string ext = Extenion.Substring(1);
                //ViewState["ContenType"] = "Image/" + ext;
                ProfileImg.SaveAs(Server.MapPath("~/images/Employees/" + retunjson));
            }
        }

        return retunjson;
    }

    [WebMethod]
    public string PostResumeFile()
    {
        string returnjson = "";
        if (HttpContext.Current.Request.Files["ResumeUploadFile"] != null)
        {

            HttpPostedFile PostResumeFile = HttpContext.Current.Request.Files["ResumeUploadFile"];
            string Extenion = Path.GetExtension(PostResumeFile.FileName);
            if (Extenion.ToLower() == ".pdf" || Extenion.ToLower() == ".PDF" || Extenion.ToLower() == ".docx" || Extenion.ToLower() == ".doc" || Extenion.ToLower() == ".DOCX" || Extenion.ToLower() == ".DOC")
            {
                
                returnjson = PostResumeFile.FileName;
                string ext = Extenion.Substring(1);
                //ViewState["ContenType"] = "Image/" + ext;
                PostResumeFile.SaveAs(Server.MapPath("~/ResumeFiles/" + returnjson));
            }
        }

        return returnjson;

    }
    [WebMethod]
    public string DocumentsUpload()
    {
        string returnjson = "";
        if (HttpContext.Current.Request.Files["DocumentsUpload"] != null)
        {

            HttpPostedFile DocumentsUpload = HttpContext.Current.Request.Files["DocumentsUpload"];
            string Extenion = Path.GetExtension(DocumentsUpload.FileName);
            if (Extenion.ToLower() == ".pdf" || Extenion.ToLower() == ".PDF" || Extenion.ToLower() == ".docx" || Extenion.ToLower() == ".doc" || Extenion.ToLower() == ".DOCX" || Extenion.ToLower() == ".DOC")
            {
                returnjson = DocumentsUpload.FileName;
                string ext = Extenion.Substring(1);
                //ViewState["ContenType"] = "Image/" + ext;
                DocumentsUpload.SaveAs(Server.MapPath("~/ResumeFiles/" + returnjson));
            }
        }
        return returnjson;

    } 
    

    [WebMethod]
    public string OtherDocumetsUpload()
    {
        string returnjson = "";
        if (HttpContext.Current.Request.Files["OtherDocumetsUpload"] != null)
        {

            HttpPostedFile OtherDocumetsUpload = HttpContext.Current.Request.Files["OtherDocumetsUpload"];
            string Extenion = Path.GetExtension(OtherDocumetsUpload.FileName);
            if (Extenion.ToLower() == ".pdf" || Extenion.ToLower() == ".PDF" || Extenion.ToLower() == ".docx" || Extenion.ToLower() == ".doc" || Extenion.ToLower() == ".DOCX" || Extenion.ToLower() == ".DOC")
            {
                returnjson = OtherDocumetsUpload.FileName;
                string ext = Extenion.Substring(1);
                //ViewState["ContenType"] = "Image/" + ext;
                OtherDocumetsUpload.SaveAs(Server.MapPath("~/ResumeFiles/" + returnjson));
            }
        }

        return returnjson;
    }
    [WebMethod]
    public string PostExcelFile()
    {
        string returnjson = "";
        if (HttpContext.Current.Request.Files["FileUploadExcel"] != null)
        {

            HttpPostedFile PostExcelFile = HttpContext.Current.Request.Files["FileUploadExcel"];
            string Extenion = Path.GetExtension(PostExcelFile.FileName);
            if (Extenion.ToLower() == ".xlsx")
            {

                returnjson = PostExcelFile.FileName;
                string ext = Extenion.Substring(1);
                //ViewState["ContenType"] = "Image/" + ext;
                PostExcelFile.SaveAs(Server.MapPath("~/ResumeFiles/" + returnjson));
            }
        }

        return returnjson;

    }

}
