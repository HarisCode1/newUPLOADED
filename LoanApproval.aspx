<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="LoanApproval.aspx.cs" Inherits="LoanApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="modal fade" id="ApprovalModal" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel runat="server" ID="Update" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">Loan Request </h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">
                                    <asp:HiddenField ID="hdnID" runat="server" />
                                    <asp:HiddenField ID="hdnID2" runat="server" />

                                    <tr>
                                        <td>
                                            <label>Name</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" type="text" runat="server" validate='vgroup' require='' CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Loan Type</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLoanType" runat="server" validate='vgroup' require='' CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Company</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompany" runat="server" validate="vgroup" require='' CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Applied Date</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAppliedDate" runat="server" validate="vgroup" require='' CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Applied Amount</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" validate="vgroup" require='' CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td>
                                            <label>Approval By Line Manager</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAppRej" runat="server">
                                                <asp:ListItem Value="0" Text="Applied"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <div runat="server" id="divHRAdmin" visible="false">
                                        <tr>
                                        <td>
                                            <label>Approval By Account</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlHRAdmin" runat="server">
                                                <asp:ListItem Value="0" Text="Applied"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    </div>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" class="btn btn-primary" OnClick="btnClose_Click" Text="Close"></asp:Button>
                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" OnClick="btnSave_Click" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Loan Approval </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" ID="UpdateGrid" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="EntryID" HeaderText="EntryID" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                            <asp:BoundField DataField="Name" HeaderText="Loan Type" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                            <asp:BoundField DataField="AppliedDate" HeaderText="Applied Date" />
                                            <asp:BoundField DataField="AppliedAmount" HeaderText="Applied Amount" />
                                            <asp:BoundField DataField="Status" HeaderText="Manager Status" />
                                            <asp:BoundField DataField="HRStatus" HeaderText="HR Status" />


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" ToolTip="Edit" runat="server" Text="Edit" CommandArgument='<%# Eval("EntryID") %>' OnCommand="lbtnEdit_Command"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

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
            $("[id$=GridView]").prepend($("<thead></thead>").append($("[id$=GridView]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=GridView]").prepend($("<thead></thead>").append($("[id$=GridView]").find("tr:first"))).dataTable({
                "order":[[0,"decs"]]
            });

        }
    </script>

</asp:Content>

