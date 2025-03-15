<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="SetSalary.aspx.cs" Inherits="SetSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section id="msform" class="content cstm-csform">
          <div id="divpayrollsetting" class="form-horizontal">
                                                    <h2 class="fs-title"><b>Payroll Setting </b></h2>
                                                    <br />
                                                    <div class="row cstm-align-labels">
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12">Basic Salary</label>
                                                            <span id="spnBasicSalary" style="color:red; display:none">Basic Salary Should be greater than zero</span>
                                                            <asp:TextBox ID="txtBasicSalary" ClientIDMode="Static" onblur="calculategrosssalary()" class="is-number-with-decimal" runat="server" placeholder="Basic Salary" validate='vgroup' require="Please Enter" MaxLength="9" max="9"></asp:TextBox>

                                                        </div>
                                                         <div class="col-md-6">
                                                            <label class="col-sm-12">Lunch Allowance</label>
                                                            <asp:TextBox ID="txthouserentallowance" ClientIDMode="Static" class="is-number-with-decimal" onblur="calculategrosssalary()" runat="server" placeholder="House rent" MaxLength="9" max="9"></asp:TextBox>

                                                        </div>
                                                         <div class="col-md-6">
                                                            <label class="col-sm-12">Transport Allowance</label>
                                                            <asp:TextBox ID="txttransportallowance" ClientIDMode="Static" class="is-number-with-decimal" runat="server" onblur="calculategrosssalary()" placeholder="Transport Allowance" MaxLength="9" max="9"></asp:TextBox>

                                                        </div>
                                                         <div class="col-md-6">
                                                            <label class="col-sm-12">Medical Allowance</label>
                                                            <asp:TextBox ID="txtmedicalalowwance"  ClientIDMode="Static" class="is-number-with-decimal" runat="server" onblur="calculategrosssalary()" placeholder="Medical Allowance" MaxLength="9" max="9"></asp:TextBox>
                                                                <label class="col-sm-12">Gross Salary</label>
                                                            <label class="col-sm-12" id="lblgrosssalary" value=""></label>
                                                        </div>
                                                        <div class="col-md-6">
                                                         
                                                            

                                                        </div>
                                                    </div>

                                                    <h2 class="fs-title"><b>Bank Details </b></h2>
               <label class="col-sm-12 ">Payment Mode</label>
                                                            <asp:DropDownList ID="ddlpaymentmethod" ClientIDMode="Static" Width="200px" runat="server" >
                                                      <asp:ListItem>--Select one--</asp:ListItem>
                                                          <asp:ListItem>Cash</asp:ListItem>
                                                              <asp:ListItem>Check</asp:ListItem>
                                                                <asp:ListItem>Bank</asp:ListItem>
                                                                   </asp:DropDownList>
                                                    <div class="row cstm-align-labels"  id="bankdetails" style="display:none;">
                                                          
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12 ">Bank Name</label>
                                                            <asp:TextBox ID="txtbankname" runat="server" placeholder="Bank Name" style="text-transform:capitalize"></asp:TextBox>

                                                              <label class="col-sm-12 ">Branch Name</label>
                                                            <asp:TextBox ID="txtfrombranch"  runat="server" placeholder="Bank Name" style="text-transform:capitalize;"></asp:TextBox>                                                            
                                                        </div>
                                                      </div>
                                                        <div class="col-md-6" id="bankdetail" style="display:none;">
                                                            <label class="col-sm-12 ">Account No</label>
                                                            <asp:TextBox ID="txtaccount"  onkeypress="return isNumberKey(event);"  MaxLength="11" require="Please Enter" runat="server" placeholder="Account NO"></asp:TextBox>
                                                             <label class="col-sm-12 ">Account Title</label>
                                                            <asp:TextBox ID="txtaccountitile"  onkeypress="return isNumberKey(event);"  MaxLength="11" require="Please Enter" runat="server" placeholder="Account Title"></asp:TextBox>
                                   
                                                            <label class="col-sm-12 ">Branch Code</label>
                                                            <asp:TextBox ID="txtbranchcode" Type="number" runat="server" placeholder="Bank Code"></asp:TextBox>
                                                      <label class="col-sm-12 ">From Branch</label>
                                                            <asp:TextBox ID="txtbrachfrom" validate='vgroup' require="Please Enter" runat="server" placeholder="From Branch"></asp:TextBox>

                                                            <label class="col-sm-12 ">To  Branch</label>
                                                            <asp:TextBox ID="txtbranchto" validate='vgroup' require="Please Enter" runat="server" placeholder="To Branch"></asp:TextBox>
                                                            </div>
           
                                                            <asp:TextBox ID="txtaccountitiles"  onkeypress="return isNumberKey(event);"  MaxLength="11" require="Please Enter" runat="server" placeholder="Account Title" style="display:none;"></asp:TextBox>
     </section>
                                               
    <script type="text/javascript">
        function calculategrosssalary()
        {
            var basicsalary = $("#txtBasicSalary").val();
            var houseamount = $("#txthouserentallowance").val();
            var transportamount = $("#txttransportallowance").val();
            var medicalamount = $("#txtmedicalalowwance").val();
            var totalgross = parseInt((basicsalary == "" ? 0 : basicsalary))
                + parseInt((houseamount == "" ? 0 : houseamount))
                + parseInt((transportamount == "" ? 0 : transportamount))
                + parseInt((medicalamount == "" ? 0 : medicalamount));
            $("#lblgrosssalary").html(totalgross);
        }

        $("#<%=ddlpaymentmethod.ClientID%>").change(function () {
        
            if ($("#ddlpaymentmethod") .find("option:selected").text() == "Bank") {
                $("#bankdetail").show();
                $("#bankdetails").show();
            }
            else if ($("#ddlpaymentmethod").find("option:selected").text() == "Cash") {
                $("#bankdetail").hide();
                $("#bankdetails").hide();
              
            }
            else if ($("#ddlpaymentmethod").find("option:selected").text() == "Check") {
                $("#bankdetail").hide();
                $("#bankdetails").hide();
                $("#txtaccountitiles").show();


            }
        })
    </script>
</asp:Content>