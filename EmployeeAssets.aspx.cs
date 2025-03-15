using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class EmployeeAssets : System.Web.UI.Page
{
    string PageUrl = "";
    vt_EMSEntities db = new vt_EMSEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
        PageUrl = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        if (Authenticate.Confirm())
        {
            if (!IsPostBack)
            {
                int Companyid = Convert.ToInt32(Session["CompanyId"]);
                ddlComp.SelectedValue = Companyid.ToString();


            }
        }

    }
    void LoadData()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {
            //    if (Session["CompanyId"] == null)
            //    {
            //grdCompany.DataBind();
            //  UpView.Update();
            //}
            //else
            //{
            //    BtnAddNew.Visible = false;
            //    companygrid.Visible = false;EditAsset;
            //}
            var query = db.vt_tbl_Assets.Where(x => x.IsActive == true);
            grdempasset.DataSource = query.ToList();
            grdempasset.DataBind();
        }
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {


        if ((string)Session["UserName"] == "SuperAdmin")
        {
       
            vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal();");
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "EmployeeAssets.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                   vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal();");
                }
                else if (Row["PageUrl"].ToString() == "EmployeeAssets.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }


        //if ((string)Session["UserName"] == "SuperAdmin")
        //{
        //    //  ViewState["hdnID"] = BAL.UserID;
        //    vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal();");
        //}
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
    void ClearForm()
    {
        try
        {
            txtAssestName.Text = "";
            txtPrice.Text = "";
            txtDetail.Text = "";
            vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool IsActive = true;
            int Companyid = Convert.ToInt32(Session["CompanyId"]);
            //EMS_Session EMS = (EMS_Session)Session["EMS_Session"];
            int id = Convert.ToInt32(ViewState["hdnID"]);
            using (vt_EMSEntities db = new vt_EMSEntities())
            {
                string msg = "Succesfully Done";
                vt_tbl_Assets EmpAsst = new vt_tbl_Assets();

                if (id == 0)
                {
                    EmpAsst.Name = txtAssestName.Text;
                    EmpAsst.Price = Convert.ToInt32(txtPrice.Text);
                    EmpAsst.Detail = txtDetail.Text;
                    EmpAsst.CreatedDate = DateTime.Now;
                    EmpAsst.CompanyId = Companyid;
                    EmpAsst.IsActive = IsActive;
                    EmpAsst.Qty =0;
                    db.vt_tbl_Assets.Add(EmpAsst);
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, "Message ", msg);
                    ClearForm();



                }
                else
                {
                    var updatequery = db.vt_tbl_Assets.Where(x => x.AssetId.Equals(id)).FirstOrDefault();
                    updatequery.AssetId = id;
                    updatequery.Name = txtAssestName.Text;
                    updatequery.Price = Convert.ToInt32(txtPrice.Text);
                    updatequery.Detail = txtDetail.Text;
                    updatequery.Qty = 0;
                    updatequery.IsActive = IsActive;
                    updatequery.CreatedDate = DateTime.Now;
                    db.SaveChanges();
                    MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Updated");
                }
            }


            ClearForm();
            LoadData();
            UpView.Update();
            Update.Update();

        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
        }
    }
    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
    //    if ((string)Session["UserName"] == "SuperAdmin")
    //    {
    //        if (e.CommandArgument.ToString() != "")
    //        {

    //            int id = Convert.ToInt32(e.CommandArgument.ToString());
    //            vt_tbl_Assets asst = db.vt_tbl_Assets.Where(x => x.AssetId.Equals(id)).FirstOrDefault();
    //            ViewState["hdnID"] = asst.AssetId;
    //            txtAssestName.Text = asst.Name;
    //            txtPrice.Text = Convert.ToInt32(asst.Price).ToString();
    //            txtDetail.Text = asst.Detail;               
    //            vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal();");
    //            Update.Update();
    //        }
    //    }





        if ((string)Session["UserName"] == "SuperAdmin")
        {

            if (e.CommandArgument.ToString() != "")
            {

                int id = Convert.ToInt32(e.CommandArgument.ToString());
                vt_tbl_Assets asst = db.vt_tbl_Assets.Where(x => x.AssetId.Equals(id)).FirstOrDefault();
                ViewState["hdnID"] = asst.AssetId;
                txtAssestName.Text = asst.Name;
                txtPrice.Text = Convert.ToInt32(asst.Price).ToString();
                txtDetail.Text = asst.Detail;
                vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal();");
                Update.Update();
            }
            vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal();");
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "EmployeeAssets.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    if (e.CommandArgument.ToString() != "")
                    {

                        int id = Convert.ToInt32(e.CommandArgument.ToString());
                        vt_tbl_Assets asst = db.vt_tbl_Assets.Where(x => x.AssetId.Equals(id)).FirstOrDefault();
                        ViewState["hdnID"] = asst.AssetId;
                        txtAssestName.Text = asst.Name;
                        txtPrice.Text = Convert.ToInt32(asst.Price).ToString();
                        txtDetail.Text = asst.Detail;
                        vt_Common.ReloadJS(this.Page, "$('#EmpAssetForm').modal();");
                        Update.Update();
                    }
                }
                else if (Row["PageUrl"].ToString() == "EmployeeAssets.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }
    }

    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                ViewState["hdnAstID"] = Convert.ToInt32(e.CommandArgument);


                vt_Common.ReloadJS(this.Page, "$('#Delete').modal();");

            }
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }

    }

    //protected void btndelete_Command(object sender, CommandEventArgs e)
    //{

    //}

    protected void btndelete_Click(object sender, EventArgs e)
    {

        int id = Convert.ToInt32(ViewState["hdnAstID"]);
        vt_tbl_Assets deleteast = db.vt_tbl_Assets.Find(id);
        db.vt_tbl_Assets.Remove(deleteast);
        db.SaveChanges();
        vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
        LoadData();
        UpView.Update();
    }

}
