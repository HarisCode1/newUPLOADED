using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Resignation : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                LoadData();
                
            }

        }
        if (!IsPostBack)
        {
            LoadData();

        }
    }
    void LoadData()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            int Companyid = Convert.ToInt32(Session["CompanyId"]);
            var query = (from r in db.vt_tbl_Resignations
                         join e in db.vt_tbl_Employee on r.EmployeeId equals e.EmployeeID
                         where r.CompanyId == Companyid && r.Status == "Applied"
                         select new
                         {
                             r.ResignationId,
                             r.EmployeeId,
                             e.EmployeeName,
                             r.Reason,
                             r.Remarks,
                             r.Status,
                             r.Image,
                             r.AppliedDate
                         });
            var queryapvd = (from r in db.vt_tbl_Resignations
                             join e in db.vt_tbl_Employee on r.EmployeeId equals e.EmployeeID
                             where r.CompanyId == Companyid && r.Status == "Approved"
                             select new
                             {
                                 r.ResignationId,
                                 r.EmployeeId,
                                 e.EmployeeName,
                                 r.Reason,
                                 r.Remarks,
                                 r.Status,
                                 r.AppliedDate
                             });
            var queryrevert = (from r in db.vt_tbl_Resignations
                               join e in db.vt_tbl_Employee on r.EmployeeId equals e.EmployeeID
                               where r.CompanyId == Companyid && r.Status == "Revert"
                               select new
                               {
                                   r.ResignationId,
                                   r.EmployeeId,
                                   e.EmployeeName,
                                   r.Reason,
                                   r.Remarks,
                                   r.Status,
                                   r.AppliedDate
                               });

            grdapvdresg.DataSource = queryapvd.ToList();
            grdapvdresg.DataBind();
            grdrevertresignation.DataSource = queryrevert.ToList();
            grdrevertresignation.DataBind();
            grdresignation.DataSource = query.ToList();
            grdresignation.DataBind();
        }
    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {

        if ((string)Session["UserName"] == "SuperAdmin")
        {
            if (e.CommandArgument.ToString() != "")
            {

                int id = Convert.ToInt32(e.CommandArgument.ToString());
                vt_tbl_Resignations resg = db.vt_tbl_Resignations.Where(x => x.ResignationId.Equals(id)).FirstOrDefault();
                ViewState["hdnID"] = resg.ResignationId;
                ViewState["EmpId"] = resg.EmployeeId;
                lbldate.Text = resg.AppliedDate.ToString();
                vt_Common.ReloadJS(this.Page, "$('#ResignationForm').modal();");
                vt_Common.ReloadJS(this.Page, "settimeout(function(){$('.date - picker').datepicker({dateFormat: 'dd-M-yy'})},1000); ");



                Update.Update();
            }

            vt_Common.ReloadJS(this.Page, "$('#ResignationForm').modal();");
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "Resignation.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    if (e.CommandArgument.ToString() != "")
                    {

                        int id = Convert.ToInt32(e.CommandArgument.ToString());
                        vt_tbl_Resignations resg = db.vt_tbl_Resignations.Where(x => x.ResignationId.Equals(id)).FirstOrDefault();
                        ViewState["hdnID"] = resg.ResignationId;
                        ViewState["EmpId"] = resg.EmployeeId;
                        lbldate.Text = resg.AppliedDate.ToString();
                        vt_Common.ReloadJS(this.Page, "$('#ResignationForm').modal();");
                        vt_Common.ReloadJS(this.Page, "settimeout(function(){$('.date - picker').datepicker({dateFormat: 'dd-M-yy'})},1000); ");



                        Update.Update();
                    }
                }
                else if (Row["PageUrl"].ToString() == "Resignation.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        int id = Convert.ToInt32(ViewState["hdnID"]);
        string msg = "Succesfully Done";
        string status = "Approved";
        Resignation resig = new Resignation();
        try
        {
            if (id > 0)
            {
                int empid = Convert.ToInt32(ViewState["EmpId"]);
                bool isresigned = true;
                var updatequery = db.vt_tbl_Resignations.Where(x => x.ResignationId.Equals(id)).FirstOrDefault();
                updatequery.ResignationId = id;
                updatequery.EOSDate = DateTime.Parse(txtFromDate.Text);
                updatequery.Status = "Approved";
                db.SaveChanges();
                MsgBox.Show(Page, MsgBox.success, "Message ", msg);
                ClearForm();
                vt_tbl_Employee emp = db.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(empid)).FirstOrDefault();
                emp.IsResigned = isresigned;
                db.SaveChanges();
                vt_tbl_Resignations resg = db.vt_tbl_Resignations.Where(x => x.EmployeeId.Equals(empid)).FirstOrDefault();
                resg.Status = status;
                db.SaveChanges();
                Response.Redirect("Resignation.aspx", false);



            }
            ClearForm();
            LoadData();
            UpdateResign.Update();
            Update.Update();
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);

        }



    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

        ClearForm();
    }


    void ClearForm()
    {
        try
        {


            vt_Common.ReloadJS(this.Page, "$('#ResignationForm').modal('hide');");
        }
        catch (Exception ex)
        {

            throw;
        }
    }





    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                ViewState["hdnAstassignID"] = Convert.ToInt32(e.CommandArgument);
                vt_Common.ReloadJS(this.Page, "$('#Delete').modal();");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        //try
        ////{
        ////    int id = Convert.ToInt32(ViewState["hdnID"]);
        ////    string status = "Canceled";         
        ////    var resg = db.vt_tbl_Resignations.Where(x => x.ResignationId.Equals(id)).Select(p => p.EmployeeId)
        ////    resg.
        //}
        //catch (Exception ex)
        //{

        //    throw;
        //}

    }

    //protected void lnkView_Command(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandArgument.ToString() != "")
    //        {
    //            ViewState["hdnAstassignID"] = Convert.ToInt32(e.CommandArgument);
    //            int resignationid =Convert.ToInt32(e.CommandArgument);
    //            int Companyid = Convert.ToInt32(Session["CompanyId"]);
    //            var query = (from r in db.vt_tbl_Resignations
    //                         join ee in db.vt_tbl_Employee on r.EmployeeId equals ee.EmployeeID
    //                         where r.CompanyId == Companyid  && r.Status == "Applied"
    //                         select new
    //                         {
    //                             r.ResignationId,
    //                             r.EmployeeId,
    //                        ee.EmployeeName,
    //                             r.Reason,
    //                             r.Remarks,
    //                             r.Status,
    //                             r.AppliedDate,
    //                             r.Image
    //                         });
    //            foreach (var item in query)
    //            {
    //                lblempname.Text = item.EmployeeName;
    //                lblreason.Text = item.Reason;
    //                lblremarks.Text = item.Remarks;
    //                lblstatus.Text = item.Status;
    //                lblapplieddate.Text =item.AppliedDate.ToString();
    //                //recvltr =Convert.ToInt32(item.Image);
    //            }
    //            vt_Common.ReloadJS(this.Page, "$('#ViewForm').modal();");
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }

    //}

    protected void btncloseviewmodal_Click(object sender, EventArgs e)
    {

        vt_Common.ReloadJS(this.Page, "$('#ViewForm').modal('hide');");
    }





   


    //protected void lnkDownload_Command(object sender, CommandEventArgs e)
    //{
    //    if (e.CommandArgument != null)
    //    {
    //        int id = Convert.ToInt32(e.CommandArgument);
    //        vt_tbl_Resignations res = db.vt_tbl_Resignations.Where(x => x.ResignationId == id).FirstOrDefault();
    //        string path = res.Image;
    //        Response.ContentType = "Application/pdf";
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=Test_PDF.pdf");
    //        Response.TransmitFile(Server.MapPath("~"+ path));
    //        Response.End();
    //        //string file = "~" + path;
    //        //if (file != string.Empty)
    //        //{
    //        //    WebClient req = new WebClient();
    //        //    HttpResponse response = HttpContext.Current.Response;
    //        //    string filePath = file;
    //        //    response.Clear();
    //        //    response.ClearContent();
    //        //    response.ClearHeaders();
    //        //    response.Buffer = true;
    //        //    response.AddHeader("Content-Disposition", "attachment;filename=Filename.extension");
    //        //    byte[] data = req.DownloadData(Server.MapPath(filePath));
    //        //    response.BinaryWrite(data);
    //        //    response.End();
    //        //}
    //    }

    //}

    //protected void lnkDownload_Click(object sender, EventArgs e)
    //{
    //    //string filePath = (sender as LinkButton).CommandArgument;
    //    string filePath = Server.MapPath((sender as LinkButton).CommandArgument);
    //    Response.ContentType = ContentType;
    //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
    //    Response.WriteFile(filePath);
    //    Response.End();

    //    //Response.Clear();
    //    //Response.ContentType = "application/octet-stream";
    //    //Response.AppendHeader("Content-Disposition", "filename=" + ~/images/ReceivingLetter/inverex-logo.png);
    //    //Response.TransmitFile(Server.MapPath("~/images/ReceivingLetter/inverex-logo.png"));
    //    //Response.End();
    //}

    //protected void lnkDownload_Command(object sender, CommandEventArgs e)
    //{
    //    if (e.CommandArgument != null)
    //    {
    //        int id = Convert.ToInt32(e.CommandArgument);
    //        vt_tbl_Resignations res = db.vt_tbl_Resignations.Where(x => x.ResignationId == id).FirstOrDefault();
    //        string filePath = res.Image;
    //        Response.ContentType = "application/octet-stream"; 
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
    //        Response.WriteFile(filePath);
    //        Response.End();
    //        //string file = "~" + path;
    //        //if (file != string.Empty)
    //        //{
    //        //    WebClient req = new WebClient();
    //        //    HttpResponse response = HttpContext.Current.Response;
    //        //    string filePath = file;
    //        //    response.Clear();
    //        //    response.ClearContent();
    //        //    response.ClearHeaders();
    //        //    response.Buffer = true;
    //        //    response.AddHeader("Content-Disposition", "attachment;filename=Filename.extension");
    //        //    byte[] data = req.DownloadData(Server.MapPath(filePath));
    //        //    response.BinaryWrite(data);
    //        //    response.End();
    //        //}
    //    }

    //}
}