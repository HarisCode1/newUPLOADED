using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Tax : System.Web.UI.Page
{
    DateTime EntryDate = DateTime.Now;
    vt_EMSEntities db = new vt_EMSEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((string)Session["UserName"] != "SuperAdmin")
            {
                int ModuleID = 9;
                int RoleID = (int)Session["RoleId"];
                vt_EMSEntities db = new vt_EMSEntities();
                DataSet Ds = ProcedureCall.SpCall_sp_GetMenuByUserNew(ModuleID, RoleID);

                
                string PageName = null;
                if (Ds != null && Ds.Tables.Count > 0)
                {
                    DataTable Dt = Ds.Tables[0];
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageName"].ToString() == "Tax")
                        {
                            PageName = Row["PageName"].ToString();
                            break;
                        }
                    }
                    if (PageName == "Tax")
                    {
                        Bind_GV();
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }
            }
            else
            {
                BtnAddNew.Visible = false;
                Bind_GV();
            }
        }
        
    }
    private void Bind_GV()
    {
        DataSet Ds = ProcedureCall.SpCall_sp_get_Tax_Result();
        if(Ds != null && Ds.Tables.Count > 0)
        {
            DataTable Dt = Ds.Tables[0];
            if(Dt != null && Dt.Rows.Count > 0)
            {
                GridTax.DataSource = Dt;
            }
            else
            {
                GridTax.DataSource = null;
            }
        }
        else
        {
            GridTax.DataSource = null;
        }
        GridTax.DataBind();

    }
    #region Click
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        foreach (DataRow Row in Dt.Rows)
        {
            if (Row["PageUrl"].ToString() == "Tax.aspx" && Row["Can_Insert"].ToString() == "True")
            {
                ViewState["PageID"] = null;
                var LastRecord = db.vt_tbl_Tax.OrderByDescending(x => x.ID).FirstOrDefault();
                ViewState["TaxID"] = 0;

                vt_Common.Clear(pnlDetail.Controls);
                UpDetail.Update();
                if (LastRecord != null)
                {
                    TxtAmountFrom.Text = Convert.ToString(LastRecord.AmountTo);
                }
                else
                {
                    TxtAmountFrom.Text = Convert.ToString("0.00");
                    TxtAmountTo.Text = Convert.ToString("0.00");

                    TxtAmountFrom.Enabled = true;
                }

                vt_Common.ReloadJS(this.Page, "$('#ModalTax').modal();");
            }
            else if (Row["PageUrl"].ToString() == "Tax.aspx" && Row["Can_Insert"].ToString() == "False")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to Insert");
            }
        }
    
          
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ViewState["PageID"] = null;
        vt_Common.Clear(pnlDetail.Controls);
        vt_Common.ReloadJS(this.Page, "$('#ModalTax').modal('hide');");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime ApplicableFrom;
        DateTime ApplicableTo;
        string Title = TxtTitle.Text;
        string Description = TxtDescription.Text;
        Decimal AmountFrom = Convert.ToDecimal(TxtAmountFrom.Text);
        Decimal AmountTo = Convert.ToDecimal(TxtAmountTo.Text);
        //string ApplicableFrom_String = TxtApplicableFrom.Text;
        //string ApplicableTo_String = TxtApplicableTo.Text;
        decimal Percentage = Convert.ToDecimal(TxtPercentage.Text);
        bool IsActive = true;
        // ApplicableFrom = Convert.ToDateTime(ApplicableFrom_String);
        //ApplicableTo = Convert.ToDateTime(ApplicableTo_String);
        int ID = Convert.ToInt32(ViewState["TaxID"]);
        var query = db.vt_tbl_Tax.Where(x => x.ID == ID && x.AmountFrom > AmountFrom && x.AmountTo < AmountTo).ToList();
        if(AmountFrom > AmountTo)
        {
            vt_Common.ReloadJS(this.Page, "showMessage('Ammount from should be grater then Ammount to');");
        }
        else
        {
            vt_tbl_Tax Tax = new vt_tbl_Tax();
            vt_tbl_Tax Data = db.vt_tbl_Tax.Where(x => x.ID == ID).FirstOrDefault();
            if (Data == null)
            {
                Tax.Title = Title;
                Tax.AmountFrom = AmountFrom;
                Tax.AmountTo = AmountTo;
                Tax.Description = Description;
                Tax.Percentage = Percentage;
                Tax.EntryDate = EntryDate;
                Tax.IsActive = IsActive;
                Tax.ApplicableFrom = Convert.ToDateTime(TxtApplicableFrom.Text);
                Tax.ApplicableTo = Convert.ToDateTime(TxtApplicableTo.Text);
                db.Entry(Tax).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                //Tax.ID = ID;
                Data.Title = Title;
                Data.AmountFrom = AmountFrom;
                Data.AmountTo = AmountTo;
                Data.Description = Description;
                Data.Percentage = Percentage;
                Data.EntryDate = EntryDate;
                Data.IsActive = IsActive;
                Data.ApplicableFrom = Convert.ToDateTime(TxtApplicableFrom.Text);
                Data.ApplicableTo = Convert.ToDateTime(TxtApplicableTo.Text);
                db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            ClearForm();
            Bind_GV();
            UpView.Update();
            UpDetail.Update();

        }


       
    }
    #endregion
    //void LoadData()
    //{

    //    var Query = db.sp_get_Tax().ToList();
    //    GridTax.DataSource = Query;
    //    GridTax.DataBind();
    //}
    protected void GridTax_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable Dt = Session["PagePermissions"] as DataTable;
        ViewState["TaxID"] = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "lnkDelete_Command":
                {
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageUrl"].ToString() == "Tax.aspx" && Row["Can_Delete"].ToString() == "True")
                        {
                            int ID = Convert.ToInt32(e.CommandArgument);
                            vt_tbl_Tax tax = db.vt_tbl_Tax.FirstOrDefault(x => x.ID == ID);
                            db.Entry(tax).State = System.Data.Entity.EntityState.Deleted;
                            db.SaveChanges();
                            Bind_GV();
                            UpView.Update();
                            UpDetail.Update();
                        }

                        else if (Row["PageUrl"].ToString() == "Tax.aspx" && Row["Can_Delete"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to delete this record");
                        }
                    }
                   
                }
                break;
            case "lbtnEdit_Command":
                {
                    foreach (DataRow Row in Dt.Rows)
                    {
                        if (Row["PageUrl"].ToString() == "Tax.aspx" && Row["Can_Update"].ToString() == "True")
                        {
                            FillDetailForm(vt_Common.CheckInt(e.CommandArgument));
                            UpDetail.Update();
                            vt_Common.ReloadJS(this.Page, "$('#ModalTax').modal();");
                            
                        }

                        else if (Row["PageUrl"].ToString() == "Tax.aspx" && Row["Can_Update"].ToString() == "False")
                        {
                            MsgBox.Show(Page, MsgBox.danger, "", "You Dont have Permission to update this record");
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
    public void FillDetailForm(int ID)
    {
        vt_tbl_Tax Tax = db.vt_tbl_Tax.FirstOrDefault(x=>x.ID == ID);
        TxtTitle.Text = Tax.Title;
        TxtDescription.Text = Tax.Description;
        TxtAmountTo.Text = Convert.ToString(Tax.AmountTo);
        TxtAmountFrom.Text = Convert.ToString(Tax.AmountFrom);
        TxtPercentage.Text = Convert.ToString(Tax.Percentage);
        TxtApplicableFrom.Text = Convert.ToString(Tax.ApplicableFrom);
        TxtApplicableTo.Text = Convert.ToString(Tax.ApplicableTo);
    }
    void ClearForm()
    {
        try
        {
            vt_Common.Clear(pnlDetail.Controls);
            vt_Common.ReloadJS(this.Page, "$('#ModalTax').modal('hide');");
        }
        catch (Exception ex)
        {
            ErrHandler.TryCatchException(ex);
            throw ex;
        }
    }
}