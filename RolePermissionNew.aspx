<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="RolePermissionNew.aspx.cs" Inherits="RolePermissionNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .row{
            margin-bottom:15px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info custom_input">
                            <div class="box-header with-border">
                                <i class="fa fa-table custom_header_icon admin_icon"></i>
                                <h3 class="box-title">Role Permission</h3>
                                <div class="box-tools pull-right">
                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-horizontal">
                                                <div class="row">
                                                     <div class="form-group">
                                                        <label class="col-sm-2 control-label text-left">Module :</label>
                                                         <div class="col-sm-3">
                                                            <asp:DropDownList ID="DdlModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlModule_SelectedIndexChanged" >
                                                
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                     <div class="form-group">
                                                        <label class="col-sm-2 control-label text-left">Role :</label>
                                                         <div class="col-sm-3">
                                                            <asp:DropDownList ID="DdlRole" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlRole_SelectedIndexChanged" >
                                                
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GvPermission" runat="server" EmptyDataText="No Record Found" 
                                                CssClass="table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="false" OnRowDataBound="GvPermission_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Visible="false" ID="LblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PageName" HeaderText="Page Name"/>
                                                    <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="" AutoPostBack="true" Visible="true" />
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="" AutoPostBack="true" Visible="true" />
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="" AutoPostBack="true" Visible="true" />
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="" AutoPostBack="true" Visible="true" />
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <span class="pull-right">
                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-payroll" Text="Save Permissions" OnClick="BtnSave_Click" />
                                               <%-- <p class="btn btn-primary" onclick="savePermissions()">Save Permission</p>--%>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
    </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function savePermissions() {
            alert('Yes');
        }
    </script>
</asp:Content>

