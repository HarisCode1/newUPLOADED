using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class AssetsAssign : System.Web.UI.Page
{
    public static int CompanyID = 0;
    vt_EMSEntities db = new vt_EMSEntities();
    AssetsAssign_BAL BAL = new AssetsAssign_BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
       
            if (Authenticate.Confirm())
            {
                if (!IsPostBack)
                {
                    //DropDownAssets();
                    //DropDownAllEmplaoyees();
                    int Companyid = Convert.ToInt32(Session["CompanyId"]);
                    BindDropDown();
                BindAssignedAssetDropDown();
                    Fill_LstDesignation();
                    int id = Convert.ToInt32(ViewState["hdnAssetparentId"]);
                    LoadData();
                }

            }
        


    }

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {

        if ((string)Session["UserName"] == "SuperAdmin")
        {

            vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "AssetsAssign.aspx" && Row["Can_Insert"].ToString() == "True")
                {
                    vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
                }
                else if (Row["PageUrl"].ToString() == "AssetsAssign.aspx" && Row["Can_Insert"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        }

        //    if ((string)Session["UserName"] == "SuperAdmin")
        //{
        //    //  ViewState["hdnID"] = BAL.UserID;
        //    vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();");
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        try
        {
   
            int Companyid = Convert.ToInt32(Session["CompanyId"]);
            string assignedasst = "assigned";
            int id = Convert.ToInt32(ddlallemployee.SelectedValue);
            int assprntid = Convert.ToInt32(ViewState["hdnAssetparentId"]);
            int asstmapid = Convert.ToInt32(ViewState["hdnID"]);
            if (assprntid == 0)
            {
                vt_tbl_AssetAssignParent asp = new vt_tbl_AssetAssignParent();
                asp.Description = txtdescription.Text;
                asp.CreatedDate = DateTime.Now;
                asp.CompanyId = Companyid;                
                db.vt_tbl_AssetAssignParent.Add(asp);                
                db.SaveChanges();
                LoadData();
                UpView.Update();
                Update.Update();
                
                int pid = asp.AssetAssignParentId;
                ViewState["asstid"] = Convert.ToInt32(pid);
                
              
                foreach (ListItem item in lstassets.Items)
                {
                    if (item.Selected)
                    {
                    //    string[] valuee = item.Selected.ToString().Split(',');
                        //value += Convert.ToInt32(item.Value) + ",";

                        vt_tbl_AssetMapping am = new vt_tbl_AssetMapping();
                        am.EmpId = Convert.ToInt32(ddlallemployee.SelectedValue);
                       
                        am.AssetParentId = pid;
                        am.AssetId = Convert.ToInt32(item.Value);
                        am.Status = assignedasst;
                        db.vt_tbl_AssetMapping.Add(am);
                        db.SaveChanges();
                        int asstid = am.AssetId;
                       // var ast = db.Sp_Asset_QtyUpdate(asstid);
                        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Assigned");
                        ClearForm();
                        LoadData();
                        UpView.Update();
                        Update.Update();
                       // Response.Redirect("AssetsAssign.aspx");
                    }
                }
            }
            else
            {
                int asstasignid = Convert.ToInt32(ViewState["asstid"]);

                var result = db.Sp_DeleteAsset(assprntid, assprntid);
                //vt_tbl_AssetAssignParent aspt = db.vt_tbl_AssetAssignParent.Where(x => x.AssetAssignParentId.Equals(assprntid)).FirstOrDefault();
                //LoadData();
                ////aspt.AssetAssignParentId = asstmapid;   
                //aspt.Description = txtdescription.Text;
                //aspt.CreatedDate = DateTime.Now;
                //db.vt_tbl_AssetAssignParent.Add(aspt);
                //db.SaveChanges();
                vt_tbl_AssetAssignParent asp = new vt_tbl_AssetAssignParent();
                asp.Description = txtdescription.Text;
                asp.CreatedDate = DateTime.Now;
                db.vt_tbl_AssetAssignParent.Add(asp);
                db.SaveChanges();
                int pid = asp.AssetAssignParentId;
                foreach (ListItem item in lstassets.Items)
                {
                    if (item.Selected)
                    {
                        //  int asstmapid = Convert.ToInt32(ViewState["hdnID"]);

                        //value += Convert.ToInt32(item.Value) + ",";
                        vt_tbl_AssetMapping am = new vt_tbl_AssetMapping();
                        am.EmpId = Convert.ToInt32(ddlassetassignedemployee.SelectedValue);
                        am.AssetParentId = pid;
                        am.AssetId = Convert.ToInt32(item.Value);
                        am.Status = assignedasst;
                        db.vt_tbl_AssetMapping.Add(am);
                        db.SaveChanges();
                       
                        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Assigned");
                        ClearForm();
                        
                        LoadData();
                        UpView.Update();
                        Update.Update();
                    }
                }
                Response.Redirect("AssetsAssign.aspx");
            }
           
        }
        catch (DbUpdateException ex)
        {
            MsgBox.Show(Page, MsgBox.danger, "Assets Not Assigned due to this", ex.Message);
        }
        //try
        //{
        //    int id = Convert.ToInt32(ddlallemployee.SelectedValue);
        //    int[] a = lstassets.GetSelectedIndices();
        //    string assignedAssets = "";

        //    for (int i = 0; i < a.Length; i++)
        //    {
        //        assignedAssets += a[i] + ",";

        //    }
        //    assignedAssets = assignedAssets.Remove(assignedAssets.Length - 1, 1);

        //    var asn_asst = db.vt_tbl_Employee.Where(x => x.EmployeeID.Equals(id)).FirstOrDefault();
        //    asn_asst.EmployeeID = vt_Common.CheckInt(ddlallemployee.SelectedValue);
        //    asn_asst.AssestsId = assignedAssets;
        //    db.SaveChanges();
        //    MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Assigned");
        //    ClearForm();
        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.TryCatchException(ex);            
        //}   
    }
    void LoadData()
    {
        using (vt_EMSEntities db = new vt_EMSEntities())
        {

            var qry= BAL.getAssignAssets();            
            grdasset.DataSource = qry;
            grdasset.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearForm();
        Response.Redirect("AssetsAssign.aspx");

    }

   
    void ClearForm()
    {
        try
        {
            //  txtAssestName.Text = "";
            //txtPrice.Text = "";
            //txtDetail.Text = "";
            //lstassets.SelectedValue = "";
            //    vt_Common.Clear(pnlDetail.Controls);
            txtdescription.Text = "";
            vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    //public void DropDownAssets()
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        ddlasset.Items.Clear();
    //        var AllAssets = (from m in db.vt_tbl_Assets
    //                            select new
    //                            {
    //                                m.Name,
    //                                m.AssetId
    //                            }).ToList();

    //        ddlasset.DataSource = AllAssets;
    //        ddlasset.DataTextField = "Name";
    //        ddlasset.DataValueField = "AssetId";
    //        ddlasset.DataBind();
    //        ddlasset.Items.Insert(0, new ListItem("Please Select", "0"));
    //    }

    //}
    //public void DropDownAllEmplaoyees()
    //{
    //    using (vt_EMSEntities db = new vt_EMSEntities())
    //    {
    //        ddlallemployee.Items.Clear();
    //        var AllEmployee = (from m in db.vt_tbl_Employee
    //                            select new
    //                            {
    //                                m.EmployeeName,
    //                                m.EmployeeID
    //                            }).ToList();

    //        ddlallemployee.DataSource = AllEmployee;
    //        ddlallemployee.DataTextField = "EmployeeName";
    //        ddlallemployee.DataValueField = "EmployeeID";          
    //        ddlallemployee.DataBind();         
    //        ddlallemployee.Items.Insert(0, new ListItem("Please Select", "0"));
            
    //    }

    //}
    private void Fill_LstDesignation()
    {
        var Data = db.vt_tbl_Assets.ToList();        
        if (Data != null)
        {
            lstassets.DataValueField = "AssetId";
            lstassets.DataTextField = "Name";
            lstassets.DataSource = Data;
        }
        else
        {
            lstassets.DataSource = null;
        }
        lstassets.DataBind();

    }
    public void BindDropDown()
    {

        try
        {
            int companyID = 0;
            companyID = (Convert.ToInt32(Session["CompanyId"]));
            SqlParameter[] param =
            {
            new SqlParameter("@CompanyID",companyID)
        };
            vt_Common.Bind_DropDown(ddlallemployee, "GetAllEmployees", "EmployeeName", "EmployeeID", param);  
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
    public void BindAssignedAssetDropDown()
    {

        try
        {
            int companyID = 0;
            companyID = (Convert.ToInt32(Session["CompanyId"]));
            SqlParameter[] param =
            {
            new SqlParameter("@CompanyID",companyID)
        };
            vt_Common.Bind_DropDown(ddlassetassignedemployee, "Sp_GetAllAssignedAssetEmployees", "EmployeeName", "EmployeeID", param);
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
   {
        if ((string)Session["UserName"] == "SuperAdmin")
        {
            if (e.CommandArgument.ToString() != "")
            {

                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int empid = Convert.ToInt32(ViewState["empid"]);
                ViewState["hdnAssetparentId"] = Convert.ToInt32(id);
                vt_tbl_AssetAssignParent asp = db.vt_tbl_AssetAssignParent.Where(x => x.AssetAssignParentId.Equals(id)).FirstOrDefault();
                vt_tbl_AssetMapping asst = db.vt_tbl_AssetMapping.Where(x => x.AssetParentId.Equals(id)).FirstOrDefault();

                // var astid = from a in db.vt_tbl_AssetMapping select a.AssetId; 
                int idd = Convert.ToInt32(asst.AssetId);
                var AssetsList = (from m in db.vt_tbl_AssetMapping
                                  where (m.AssetParentId == id)
                                  select m.AssetId).ToList();


                foreach (var abc in AssetsList)
                {
                    foreach (ListItem AssetMainList in lstassets.Items)
                    {
                        if (abc == int.Parse(AssetMainList.Value))
                        {
                            AssetMainList.Selected = true;
                        }
                    }
                }

                ddlassetassignedemployee.Visible = true;
                ddlassetassignedemployee.Enabled = false;
                
                ddlassetassignedemployee.SelectedValue = Convert.ToInt32(asst.EmpId).ToString();
                ViewState["empid"] = Convert.ToInt32(asst.EmpId).ToString();
                ddlallemployee.Visible = false;
                var asset = (asst.AssetId).ToString();
                txtdescription.Text = asp.Description;
                vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
                Update.Update();
            }
            vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
        }
        else
        {
            DataTable Dt = Session["PagePermissions"] as DataTable;
            foreach (DataRow Row in Dt.Rows)
            {
                if (Row["PageUrl"].ToString() == "AssetsAssign.aspx" && Row["Can_Update"].ToString() == "True")
                {
                    if (e.CommandArgument.ToString() != "")
                    {

                        int id = Convert.ToInt32(e.CommandArgument.ToString());
                        int empid = Convert.ToInt32(ViewState["empid"]);
                        ViewState["hdnAssetparentId"] = Convert.ToInt32(id);
                        vt_tbl_AssetAssignParent asp = db.vt_tbl_AssetAssignParent.Where(x => x.AssetAssignParentId.Equals(id)).FirstOrDefault();
                        vt_tbl_AssetMapping asst = db.vt_tbl_AssetMapping.Where(x => x.AssetParentId.Equals(id)).FirstOrDefault();

                        // var astid = from a in db.vt_tbl_AssetMapping select a.AssetId; 
                        int idd = Convert.ToInt32(asst.AssetId);
                        var AssetsList = (from m in db.vt_tbl_AssetMapping
                                          where (m.AssetParentId == id)
                                          select m.AssetId).ToList();


                        foreach (var abc in AssetsList)
                        {
                            foreach (ListItem AssetMainList in lstassets.Items)
                            {
                                if (abc == int.Parse(AssetMainList.Value))
                                {
                                    AssetMainList.Selected = true;
                                }
                            }
                        }
                        ddlassetassignedemployee.Visible = true;
                        ddlassetassignedemployee.Enabled = false;
                        ddlassetassignedemployee.SelectedValue = Convert.ToInt32(asst.EmpId).ToString();
                        ViewState["empid"] = Convert.ToInt32(asst.EmpId).ToString();
                        ddlallemployee.Visible = false;                        
                        var asset = (asst.AssetId).ToString();
                        txtdescription.Text = asp.Description;
                        vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
                        Update.Update();
                    }
                }
                else if (Row["PageUrl"].ToString() == "AssetsAssign.aspx" && Row["Can_Update"].ToString() == "False")
                {
                    MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
                }
            }
        

        //my code
        //if ((string)Session["UserName"] == "SuperAdmin")
        //{
        //    if (e.CommandArgument.ToString() != "")
        //    {
                
        //        int id = Convert.ToInt32(e.CommandArgument.ToString());
        //        int empid = Convert.ToInt32(ViewState["empid"]);
        //        ViewState["hdnAssetparentId"] = Convert.ToInt32(id);
        //        vt_tbl_AssetAssignParent asp = db.vt_tbl_AssetAssignParent.Where(x => x.AssetAssignParentId.Equals(id)).FirstOrDefault();
        //        vt_tbl_AssetMapping asst = db.vt_tbl_AssetMapping.Where(x => x.AssetParentId.Equals(id)).FirstOrDefault();

        //       // var astid = from a in db.vt_tbl_AssetMapping select a.AssetId; 
        //        int idd = Convert.ToInt32(asst.AssetId);
        //        var AssetsList = (from m in db.vt_tbl_AssetMapping where(m.AssetParentId == id )
        //                          select m.AssetId ).ToList();


        //        foreach (var abc in AssetsList)
        //        {
        //            foreach (ListItem AssetMainList in lstassets.Items) {
        //                if (abc == int.Parse(AssetMainList.Value))
        //                {
        //                    AssetMainList.Selected = true;
        //                }
        //            }
        //        }
             
        //        ddlallemployee.SelectedValue = Convert.ToInt32(asst.EmpId).ToString();
        //        ddlallemployee.Enabled = false;
        //        ViewState["empid"]= Convert.ToInt32(asst.EmpId).ToString();
        //        var asset = (asst.AssetId).ToString();
        //        txtdescription.Text = asp.Description;
        //        vt_Common.ReloadJS(this.Page, "$('#AssignAssetForm').modal();$('#ContentPlaceHolder1_lstassets').select2();");
        //        Update.Update();
        //    }
        }

        }

    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                ViewState["hdnAstassignID"] = Convert.ToInt32(e.CommandArgument);
                vt_Common.ReloadJS(this.Page, "$('#Delete').modal();$('#ContentPlaceHolder1_lstassets').select2();");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ViewState["hdnAstassignID"]);
        var result = db.Sp_DeleteAsset(id,id);
        vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide');$('#ContentPlaceHolder1_lstassets').select2();");
        MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Assets Deleted");
        //vt_tbl_AssetMapping deletastmap = db.vt_tbl_AssetMapping.Where(x => x.AssetParentId.Equals(result)).FirstOrDefault();
        //db.vt_tbl_AssetMapping.Remove(deletastmap);
        //db.SaveChanges();
        //vt_Common.ReloadJS(this.Page, "$('#Delete').modal('hide')");
        //MsgBox.Show(Page, MsgBox.success, "Message ", "Successfully Deleted");
        //vt_tbl_AssetAssignParent asp =db.vt_tbl_AssetAssignParent.Where(x => x.AssetAssignParentId.Equals(result)).FirstOrDefault();
        //db.vt_tbl_AssetAssignParent.Remove(asp);
        //db.SaveChanges();

        LoadData();
        UpView.Update();

        Response.Redirect("AssetsAssign.aspx");
    }
}