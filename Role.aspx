<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Role</h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <section class="content cstm-content">
        <div class="col-sm-12" style="padding-bottom: 20px;">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-payroll pull-right" OnClick="btnAddNew_Click" Visible="false" />
                     <asp:Button ID="lbtnRolePermissions" runat="server" CssClass="btn btn-payroll pull-right" OnClick="lbtnRolePermissions_Click" Text="Role Permission"/>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </section>

    <div class="modal fade" id="myModalBranch" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="myModalLabel">Role Add/Edit</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">
                                    <%--<tr id="trCompany" runat="server">
                                            <td style="width: 130px;">
                                                <label>Company :</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' require='Please select company' customFn='var r = parseInt(this.value); return r > 0;' AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:EntityDataSource ID="EDS_Comp" runat="server"
                                                    ConnectionString="name=vt_EMSEntities"
                                                    DefaultContainerName="vt_EMSEntities"
                                                    EntitySetName="vt_tbl_Company">
                                                </asp:EntityDataSource>
                                            </td>
                                        </tr>--%>
                                    <asp:HiddenField runat="server" ID="hdnID" />
                                    <tr>
                                        <%--<td>
                                            <asp:TextBox ID="ID" runat="server" Visible="false" validate='vgroup' require='Please enter a branch name' CssClass="form-control"></asp:TextBox>
                                        </td>--%>
                                        <td style="width: 120px;">
                                            <label>Role :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" validate='vgroup' require='Please enter a role name' CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">
                                            <label>Active :</label></td>
                                        <td>
                                            <asp:CheckBox ID="chkISA" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" class="btn btn-primary" OnClick="btnClose_Click1" Text="Close"></asp:Button>
                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" OnClick="btnSave_Click" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="Delete" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel2">Delete</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12" id="mybtn">

                                        <fieldset>
                                            <label>Are you sure you want to delete this record?</label>
                                            <div class="form-group">

                                                <asp:TextBox ID="MsgDelete" Visible="false" runat="server"></asp:TextBox>

                                                
                                            </div>

                                        </fieldset>
                                        <%-- End Account Setup--%>

                                        <fieldset>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:TextBox ID="TextBox4" Visible="false" runat="server"></asp:TextBox>
                               <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" Text="No" Style="width: 130px;" CssClass="submit action-button"></asp:Button>
                                                <asp:Button ID="btndelete" ClientIDMode="Static" runat="server" Text="Yes" CommandArgument='<%#Eval("ID")%>' Style="width: 130px;" CssClass="submit action-button" OnCommand="btndelete_Command"></asp:Button>

                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- /.modal -->
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Role </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <%--OnPageIndexChanging="GridUser_PageIndexChanging" OnRowDataBound="GridUser_RowDataBound"--%>
                                    <asp:GridView ID="GridRole" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="RoleID">
                                        <Columns>
                                            <asp:BoundField DataField="RoleID" HeaderText="Role ID" SortExpression="RoleID" Visible="true" />
                                            <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("RoleID") %>' OnCommand="lbtnEdit_Command"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" 
                                                        CommandArgument='<%# Eval("RoleID") %>' OnCommand="lbtnDelete_Command"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                                    <%--<asp:LinkButton ID="lbtnRolePermissions" runat="server" 
                                                        CommandArgument='<%#Eval("RoleID") + "," +Eval("Role") %>' OnCommand="lbtnRolePermissions_Command"><i class="fa fa-reorder"></i></asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                    </asp:GridView>
                                    <%--<asp:GridView ID="grdDesignation" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID">
                                                <HeaderStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("RoleID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="Roles">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Role")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="5%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("RoleID")%>' CommandName="Edit" OnCommand="lnkEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("RoleID")%>' CommandName="Delete" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        });
        function binddata() {
            debugger;
            $("[id$=GridRole]").prepend($("<thead></thead>").append($("[id$=GridRole]").find("tr:first"))).dataTable();
        }
    </script>
</asp:Content>

