<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Department </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Department</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-payroll" Text="Add New" OnClick="BtnAddNew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="department" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Department Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <table class="all-table department-table all-tables">
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
                                                                    <label> HeadDepartment:</label></td>

                                                             </td>
                                        <td>
                                            <asp:DropDownList ID="ddlheaddeparment" ClientIDMode="Static" CssClass="form-control" runat="server">

                                                    <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                                                  </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                   
                                        <td style="width: 120px;">
                                            <label>Name :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" validate='vgroup' require='please enter a Name' style="text-transform:capitalize;"></asp:TextBox></td>
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
        <!-- /.modal -->
        <div class="row" id="divCompany" runat="server">
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
                        <h3 class="box-title">Department</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grddepartment" runat="server" EmptyDataText="No Record Found" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grddepartment_RowCommand">
                                        <Columns>
                                             <asp:TemplateField HeaderText="Sno" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                     <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="50%" />
                                            <asp:BoundField DataField="HeadDeparment" HeaderText="Head Department" HeaderStyle-Width="50%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" ToolTip="Edit" runat="server" CommandArgument='<%#Eval("DepartmentID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("DepartmentID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="test" />
                                    </asp:GridView>
                                    <asp:EntityDataSource ID="EDS_department" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Department"
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
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
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

            //$("[id$=grddepartment]").prepend($("<thead></thead>").append($("[id$=grddepartment]").find("tr:first"))).dataTable().fnDestroy();
            //$("[id$=grddepartment]").prepend($("<thead></thead>").append($("[id$=grddepartment]").find("tr:first"))).dataTable({
            //    "order": [[0, "desc"]]
            //});
        }
    </script>
</asp:Content>