<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Tax.aspx.cs" Inherits="Tax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Tax</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Tax</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-payroll pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="ModalTax" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Tax Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <table class="all-tables">
                                    <tr>
                                        <td>
                                            <label>Title :</label></td>
                                        <td>
                                            <asp:HiddenField ID="txtTaxRangeID" runat="server"/>
                                            <asp:TextBox type="text" ID="TxtTitle" runat="server" CssClass="form-control" validate='vgroup' require='Please Enter Range From'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Description :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox type="text" ID="TxtDescription" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" validate='vgroup' require='Please Enter Range To'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Amount From:</label></td>
                                        <td>
                                            <asp:TextBox type="text" ID="TxtAmountFrom" Enabled="false"  runat="server" CssClass="form-control auto-numeric" validate='vgroup' require='Please Enter Percentage %'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Amount To:</label></td>
                                        <td>
                                            <asp:TextBox type="text" ID="TxtAmountTo" runat="server" CssClass="form-control auto-numeric" validate='vgroup' require='Please Enter Percentage %'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Percentage : (%)</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtPercentage" runat="server" CssClass="form-control" validate='vgroup' require='Please Enter Percentage %'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Applicable From :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtApplicableFrom" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" require='Please select from date' ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Applicable To :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtApplicableTo" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" require='Please select from date' ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClick="btnSave_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
                                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" OnClick="btnClose_Click" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Tax</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <%--OnPageIndexChanging="GridUser_PageIndexChanging" OnRowDataBound="GridUser_RowDataBound"--%>
                                    <asp:GridView ID="GridTax" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridDataTable table table-striped table-bordered dataTable " AutoGenerateColumns="False" OnRowCommand="GridTax_RowCommand">
                                        <Columns>

                                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                            <asp:BoundField DataField="AmountFrom" HeaderText="Amount From" SortExpression="Amount" />
                                            <asp:BoundField DataField="AmountTo" HeaderText="Amount To" SortExpression="Amount" />
                                            <asp:BoundField DataField="Percentage" HeaderText="Per(%)" SortExpression="Percentage" />



                                            <asp:TemplateField HeaderText="Action">

                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" ToolTip="Edit" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("ID")%>' CommandName="lbtnEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lnkDelete" ToolTip="Delete" runat="server" Text="Delete" CommandArgument='<%#Eval("ID")%>' CommandName="lnkDelete_Command" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                             <i class="fa fa-times-circle"></i>
                                                    </asp:LinkButton>
                                                    <h2 id="titles"></h2>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="js/jquery.validator.js"></script>

    <script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function binddata() {
            $('#txtDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1
            });
            $("[id$=GridTax]").prepend($("<thead></thead>").append($("[id$=GridTax]").find("tr:first"))).dataTable();
        }
        function binddata() {
            
            $('#TxtApplicableFrom').datepicker({
                format: 'dd/m/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1
            });
            $('#TxtApplicableTo').datepicker({
                format: 'dd/m/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1,
            });
        }
        
        <%-- $(document).ready(function () {

            $("#<%=ddlComp.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

        });
         $(document).ready(function () {

            $("#<%=ddlRole.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

      });--%>
    </script>
</asp:Content>

