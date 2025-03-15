<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Other.aspx.cs" Inherits="Other" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function  binddata() {
            if ($("#chkFixMonthDays").prop("checked"))
                $(".fixDay").show();
            $("#chkFixMonthDays").change(function () {
                $(".fixDay").show();
            });

            $("#chkOnlyWorkingDays").change(function () {
                $(".fixDay").hide();
            });            
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="heading">
        PAYROLL SETTING
    </div>


    <div class="row" id="divCompany" runat="server">
        <div class="col-md-6" >
            <asp:UpdatePanel ID="updateCompany" runat="server">
                <ContentTemplate>
                    <table>
                                <tr id="trCompany" runat="server">
                                    <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompany"  ClientIDMode="Static" runat="server" CssClass="form-control"   DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" >
                                        </asp:DropDownList>
                                        <asp:EntityDataSource ID="EDS_Company" runat="server"
                                            ConnectionString="name=vt_EMSEntities"
                                            DefaultContainerName="vt_EMSEntities"
                                            EntitySetName="vt_tbl_Company">
                                        </asp:EntityDataSource>
                                    </td>
                                </tr>
                    </table>
               </ContentTemplate>
           </asp:UpdatePanel>
        </div>
    </div>




    <div id="UpdateModel">
        <asp:UpdatePanel ID="Upview" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

    <div id="pnlDetail" runat="server">
    <div class="row">
        <div class="col-md-5 col-md-offset-1">
            <fieldset>
                <legend>
                    PF/EOBI
                </legend>
            </fieldset>    
            <table class="Shift-det">     
                <tr> 
                    <td style="width:160px;">
                        <asp:checkbox ID="chkPFApplicable" runat="server" text="PF Applicable" cssclass="checkboxbtn"> </asp:checkbox>
                    </td>
                    <td>                        
                        <asp:checkbox ID="chkEOBIApplicable" runat="server" text="EOBI Applicable" cssclass="checkboxbtn"></asp:checkbox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-md-5">
             <fieldset>
                <legend>
                    Salary Rate Type
                </legend>
            </fieldset>    
            <table class="Shift-det">     
                <tr>
                    <td style="width:100px;"><label>Salary Rate :</label></td>
                    <td>
                        <asp:DropDownList ID="ddlSalaryRate" runat="server" CssClass="form-control">
                            <asp:ListItem>Monthly</asp:ListItem>
                            <asp:ListItem>Daily</asp:ListItem>
                        </asp:DropDownList>                        
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-md-offset-1"></div>
    </div>
        <div class="row">
        <div class="col-md-5 col-md-offset-1">
            <fieldset>
                <legend>
                    Monthly Total Days Calcs
                </legend>
            </fieldset>    
            <table class="Shift-det">     
                <tr> 
                    <td style="width:160px;">
                        <asp:RadioButton GroupName="Days" ID="chkFixMonthDays" ClientIDMode="Static" runat="server" text="Fix Month Days" cssclass="checkboxbtn"> </asp:RadioButton>
                    </td>
                    <td class="fixDay" style="display:none;width: 70px;">
                        Fix Days :
                    </td>
                    <td class="fixDay" style="display:none;">
                        <asp:textbox ID="txtFixDays" Width="50" runat="server"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:RadioButton GroupName="Days" ClientIDMode="Static" ID="chkOnlyWorkingDays" runat="server" text="Only Working Days" cssclass="checkboxbtn"> </asp:RadioButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-md-5">
             
        </div>
        <div class="col-md-offset-1"></div>
    </div>
        </div>
                
        

     <div class="row">
        <div class="col-md-10 col-md-offset-1 ">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnSave_Click" />            
        </div>
        <div class="col-md-offset-1"></div>
    </div>
                 
    </ContentTemplate>
    </asp:UpdatePanel>
        </div>
</asp:Content>

