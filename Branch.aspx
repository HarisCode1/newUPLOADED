<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Branch.aspx.cs" Inherits="Branch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Branch </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Branch</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-payroll pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row" id="divCompany" runat="server" visible="false">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updateCompany" runat="server">
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
                        <h3 class="box-title">Branch Details</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdBranch" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdBranch_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="BranchID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="BranchShortName" HeaderText="Branch Short Name" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone" HeaderStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit"  ToolTip="Edit" runat="server" CommandArgument='<%#Eval("BranchID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("BranchID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
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
        <div class="modal fade" id="myModalBranch" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog" role="document">
                <div id="pnlDetail" runat="server">
                    <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalLabel">Branch Add/Edit</h4>
                                </div>
                                <div class="modal-body">
                                    <table class="all-table all-tables">
                                        <tr id="trCompany" runat="server">
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
                                        </tr>
                                        <tr>
                                            <td style="width: 120px;">
                                                <label>Name :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" validate='vgroup' require='Please enter a branch name' CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Short Name :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtShortName" runat="server" validate='vgroup' CssClass="form-control" require='Please enter a Short name'></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Phone :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtPhone" runat="server" validate='vgroup' CssClass="form-control only-number" require='Please enter a branch phone'></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Email :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" validate='vgroup' CssClass="form-control" require='Please enter a branch e-mail' email='invalid email'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Contact Person :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtCPerson" runat="server" validate='vgroup' CssClass="form-control" require='Please enter a contact person'></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Address :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control" Height="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-payroll" OnClick="btnSave_Click" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
                                    <asp:Button ID="btnClose" runat="server" class="btn" OnClick="btnClose_Click" Text="Close"></asp:Button>

                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
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
            $("[id$=grdBranch]").prepend($("<thead></thead>").append($("[id$=grdBranch]").find("tr:first"))).dataTable().fnDestroy();

            $("[id$=grdBranch]").prepend($("<thead></thead>").append($("[id$=grdBranch]").find("tr:first"))).dataTable({
                "order": [[0, "desc"]]
            });
        }
    </script>
</asp:Content>
