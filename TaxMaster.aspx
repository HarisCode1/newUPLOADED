<%@ Page Title="" Language="C#" MasterPageFile="NewMain.master" AutoEventWireup="true" CodeFile="TaxMaster.aspx.cs" Inherits="EmailSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Tax Master</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Tax Master</li>
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
                        <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="EmailSetting" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Tax Master Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <table class="all-tables">
                                    <tr>
                                        <td>
                                            <label>Range From :</label></td>
                                        <td>
                                            <asp:HiddenField ID="txtTaxRangeID" runat="server" />
                                            <asp:TextBox type="text" ID="txtRangeFrom" runat="server" CssClass="form-control auto-numeric" validate='vgroup' require='Please Enter Range From'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Range To :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox type="text" ID="txtRangeTo" runat="server" CssClass="form-control auto-numeric" validate='vgroup' require='Please Enter Range To'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Percentage %:</label></td>
                                        <td>
                                            <asp:TextBox type="text" ID="txtPer" runat="server" CssClass="form-control auto-numeric" validate='vgroup' require='Please Enter Percentage %'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Fixed Amount :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control auto-numeric"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Exceeding Amount Condition :</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtExeCond" runat="server" CssClass="form-control auto-numeric"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>% Calculation on Exceeding Amount :</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtExeAmt" runat="server" CssClass="form-control auto-numeric"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_OnClick" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
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
                        <h3 class="box-title">Tax Master </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdEmailSetting" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdEmailSetting_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="TaxRangeID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="FirstCalculationRangeFrom" HeaderText="Range From" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="FirstCalculationRageTo" HeaderText="Range To" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="FirstCalculationPercentageValue" HeaderText="Percentage %" HeaderStyle-Width="15%" />

                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" ToolTip="Edit" runat="server" CommandArgument='<%#Eval("TaxRangeID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("TaxRangeID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:EntityDataSource ID="EDS_EmailSettings" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_EmailSettings">
                                    </asp:EntityDataSource>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
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

            $("[id$=grdEmailSetting]").prepend($("<thead></thead>").append($("[id$=grdEmailSetting]").find("tr:first"))).dataTable().fnDestroy();

            $("[id$=grdEmailSetting]").prepend($("<thead></thead>").append($("[id$=grdEmailSetting]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });
        }
    </script>
</asp:Content>