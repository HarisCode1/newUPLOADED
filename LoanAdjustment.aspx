<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="LoanAdjustment.aspx.cs" Inherits="LoanAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-migrate-1.2.1.js"></script> 
    <script src="js/jquery.plugin.js"></script>
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    
      <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
			  <h1>
				Loan Adjustment
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Loan Adjustment</li>
			  </ol>
			</section>
			<section class="content">


    <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
            <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-primary" Text="Add New" OnClick="btnAddnew_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>


    
    <div class="modal fade" id="loanentry" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">LOAN ADJUSTMENT</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-6">
                                    <fieldset>
                                        <legend>Details</legend>
                                    </fieldset>
                                    <table>
                                        <tr id="trCompany" runat="server">
                                            <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;'  AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Please Select Company"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:EntityDataSource ID="EDS_Comp" runat="server"
                                            ConnectionString="name=vt_EMSEntities"
                                            DefaultContainerName="vt_EMSEntities"
                                            EntitySetName="vt_tbl_Company">
                                        </asp:EntityDataSource>
                                    </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Employee :</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="form-control" runat="server" validate='vgroup' AutoPostBack="True" require='Please Select Employee' OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                               <asp:ListItem Value="0" Text="Select Employee..."></asp:ListItem>
                                                     </asp:DropDownList>
                                          
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Loan Type :</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlLoanType" ClientIDMode="Static" CssClass="form-control"  runat="server"  validate='vgroup' AutoPostBack="True" require='Please Select Loan Type' OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Please Select Loan Type..."></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td>
                                                <label>Installment Amount :</label></td>
                                            <td>
                                                <asp:Label ID="LblInstallmentAmt" CssClass="form-control" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Loan Adjustment Amount :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control" validate="vgroup" require="please enter loan amount"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Months :</label></td>
                                            <td>
                                                <div class="pull-left leavapp-from">
                                                     <asp:Label ID="lblMonths" CssClass="form-control" runat="server" Text=""></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.modal-dialog -->
    </div>




    <div class="row" id="divCompany" runat="server">
        <div class="col-md-6" >
            <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                                <tr>
                                    <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
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





    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Loan Adjustment</h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdLoanEntry" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdLoanEntry_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Enroll Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnrollId" runat="server" Text='<%#Eval("Enrollid")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblLoanName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loan Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblLoanAmount" runat="server" Text='<%#Eval("LoanAmount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMI Type" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblEMIType" runat="server" Text='<%#Eval("EMIType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Number Of Month">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmountMonth" runat="server" Text='<%#Eval("AmountMonth")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                   <%-- <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("EntryID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>--%>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("EntryID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource  runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_LoanEntry"
                        Where="it.CompanyId = @CompanyId">
                    </asp:EntityDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
                 </div>
    </div>
        </div>
                </section>
    
        

    <script src="js/bootstrap-datetimepicker.min.js"></script>       
    <script src="js/bootstrap-typeahead.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        });

        function binddata() {
            $('#dtpDate').datetimepicker({ pickTime: false, autoclose: true });                     
            PageMethods.getEmpList(OnRequestComplete, OnRequestError);

            $("[id$=grdLoanEntry]").prepend($("<thead></thead>").append($("[id$=grdLoanEntry]").find("tr:first"))).dataTable();


        }

        function OnRequestComplete(res, userContext, methodName) {
            $("#txtEmpName").typeahead({
                source: function (typeahead, query) {
                    $("#txtEmpID").val("");
                    typeahead.process($.map(res, function (item) {
                        return {
                            value: item[1],
                            EmpID: item[0]
                        }
                    }));
                },
                minLength: 1,
                property: 'value',
                appendtoSelector: "[id $= pnlDetail]",
                SubTop: 90,
                SubLeft: 182.5,
                onselect: function (obj) {
                    $("#txtEmpID").val(obj.EmpID);
                }
            });
        }

        function OnRequestError(error, userContext, methodName) {
            if (error != null) {
                $("#txtEmpID").val("");
            }
        }
    </script>
        



</asp:Content>

