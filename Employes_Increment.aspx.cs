using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Viftech;

public partial class Employes_Increment : System.Web.UI.Page
{
    vt_EMSEntities db = new vt_EMSEntities();
    private int ID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Authenticate.Confirm())
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            if (!Page.IsPostBack)
            {
                if (ID > 0)
                {
                    FillData(ID);
                    BindGrid();
                    //GetEmpDetails();
                }
            }
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //Save();
        var Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (Data != null)
        {
            if (TxtIncrement.Text == "")
            {
                //TxtIncrement.Text="0"

                MsgBox.Show(Page, MsgBox.danger, "", "Please Select Increment");
            }
            else if(txtcurrenttax.Text == "")
            {
                MsgBox.Show(Page, MsgBox.danger, "", "Please Apply Tax Increment");

            }
            else
            {
                Data.BasicSalary = Data.BasicSalary + Convert.ToDecimal(TxtIncrement.Text);
                Data.Tax = Data.Tax + Convert.ToDecimal(txtcurrenttax.Text);
                db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //MsgBox.Show(Page, MsgBox.success, "", "Succesfully Done");
                vt_Common.ReloadJS(Page, "msgbox(1,' ' ,'Succesfully Done'); setTimeout(function(){window.location.href='Employes_Increment.aspx?ID=" + ID + "';},1000)");
                TxtIncrement.Text = "";
            }

        }
        //MsgBox.Show(Page, MsgBox.success, "", "Successfully Done");
        
        
        //Response.Redirect("Employes_Increment.aspx?ID="+ID);
        
        //UpdatePanel2.Update();
    }

    void Save()
    {
        //var Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        //if (Data != null)
        //{
        //    if (TxtIncrement.Text == "" || txtcurrenttax.Text == "")
        //    {
        //        //TxtIncrement.Text="0"

        //        MsgBox.Show(Page, MsgBox.danger, "", "Please Select Increment");
        //    }
        //    else
        //    {
        //        Data.BasicSalary = Data.BasicSalary + Convert.ToDecimal(TxtIncrement.Text);
        //        Data.Tax = Data.Tax + Convert.ToDecimal(txtcurrenttax.Text);                
        //        db.Entry(Data).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        MsgBox.Show(Page, MsgBox.success, "", "Succesfully Done");
        //        TxtIncrement.Text = "";
        //    }
          
        //}
    }
    void FillData(int ID)
    {
        var Data = db.vt_tbl_Employee.Where(x => x.EmployeeID == ID).FirstOrDefault();
        if (Data != null)
        {
            hdnbtn.Value = Data.EmployeeID.ToString();
            TxtFirstName.Text = Data.FirstName;
            TxtLastName.Text = Data.LastName;
            TxtEmail.Text = Data.Email;
            txtoldtax.Text = Data.Tax.ToString();

            TxtBasicSalary.Text = Data.BasicSalary.ToString();
        }
    }
    
    public void BindGrid()
    {
        //try
        //{
           
        //    var query = db.Sp_GetEmployeePerformanceEvaluation(ID,DateTime.Now );
        //    grdempperformanceevaluation.DataSource = query.ToList();
        //    grdempperformanceevaluation.DataBind();
        //}
        //catch (Exception)
        //{

        //    throw;
        //}
    }
    [WebMethod]
    public static string SearchLeaves(string ID,string Year)
    {
        try
        {
            vt_EMSEntities db = new vt_EMSEntities();
            var json = "";

            var query = db.vt_sp_getempdetail_forappraisal(Convert.ToInt32(ID), Year).ToList();
            if (query.Count != 0)
            {
                var jsonSerialiser = new  JavaScriptSerializer();
                json = jsonSerialiser.Serialize(query);
                // HttpContext.Current.UpdatePanel1.Update();
            }
            else
            {
                //   Page page = HttpContext.Current.Handler as Page;
                //MsgBox.Show(new Page(), MsgBox.success, "", "Sorry No Leave this year");
                //string empty = "";
                json = "Empty";

                }



            return json;
        }
        catch (Exception)
        {

            throw;
        }
     
    }



    //protected void SearchRecord_Click(object sender, EventArgs e)
    //{
    //    GetEmpDetails(1089, "2019");
    //}

  



    protected void btnsearchperformance_Click(object sender, EventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            if (ID != 0)
            {
                string year = txtsearchperformance.Text;
                //if (DateTime.Now.Year.ToString() == year)
                //{
                    var query = db.Sp_GetEmployeePerformanceEvaluation(ID, Convert.ToDateTime(year + "-01-01"));
                grdempperformanceevaluation.DataSource = query.ToList();
                    grdempperformanceevaluation.DataBind();
                //}
            }
        
           
        }
        catch (Exception)
        {

            throw;
        }
    }
}