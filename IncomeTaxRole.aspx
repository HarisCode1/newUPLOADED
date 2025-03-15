<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="IncomeTaxRole.aspx.cs" Inherits="IncomeTaxRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">        
    <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
			  <h1>
				Income Tax Role
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Income Tax Role</li>
			  </ol>
			</section>
			<section class="content">


     <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
            <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-primary" Text="Add New" OnClick="BtnAddNew_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>



    <div class="modal fade" id="IncomeTax" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">LEAVE YEAR</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table>
                                        <tr>
                                            <td style="width: 100px;">
                                                <label>From :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFROMTaxableIncome" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td style="width: 50px;padding-left:5px;">
                                                <label>To :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtToTaxableIncome" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px;">
                                                <label>RateOfTax :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRateOfTax" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                           <td style="width: 50px;">                                                
                                            </td>
                                            <td>
                                                <asp:CheckBox id="chkActive" Text="Active" runat="server"></asp:CheckBox>
                                            </td>                                            
                                        </tr>                                        
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
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
                        <h3 class="box-title">Income Tax Role </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdInComeTaxRole" runat="server" DataSourceID="EDS_InComeTaxRole" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdLeaveYear_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                            <asp:BoundField DataField="FROMTaxableIncome" HeaderText="FROM" ReadOnly="True" SortExpression="FROMTaxableIncome"></asp:BoundField>
                            <asp:BoundField DataField="ToTaxableIncome" HeaderText="To" ReadOnly="True" SortExpression="ToTaxableIncome"></asp:BoundField>
                            <asp:BoundField DataField="RateOfTax" HeaderText="RateOfTax" ReadOnly="True" SortExpression="RateOfTax"></asp:BoundField>
                            <asp:BoundField DataField="IsActive" HeaderText="Active" ReadOnly="True" SortExpression="IsActive"></asp:BoundField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="EditTaxableIncome">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("ID")%>' CommandName="DeleteTaxableIncome" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_InComeTaxRole" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Tax_Rate_Role"
                        EnableFlattening="False" EntityTypeFilter="vt_tbl_Tax_Rate_Role" Select="it.[ID], it.[FROMTaxableIncome], it.[ToTaxableIncome], it.[RateOfTax],it.IsActive">
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

        function binddata() {
            $("[id$=grdInComeTaxRole]").prepend($("<thead></thead>").append($("[id$=grdInComeTaxRole]").find("tr:first"))).dataTable();
        }
    </script>
</asp:Content>

