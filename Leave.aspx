<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Leave.aspx.cs" Inherits="Leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Leave</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Leave</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <% if (Session["Username"].ToString() == "SuperAdmin")
                            { %>

                        <% }
                            else
                            { %>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-payroll" Text="Add New" OnClick="btnAddnew_Click" />
                        <%} %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
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
        <div class="modal fade" id="leave" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Leave Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <table class="all-tables">
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
                                        <td>
                                            <label>Name :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a leave name'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Short Name :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a leave short name'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Leaves Allow</label></td>
                                        <td>
                                            <asp:TextBox ID="txtLeaveAllow" runat="server" CssClass="form-control only-number not-zero" validate='vgroup' require='Please enter a Value' type="text"></asp:TextBox></td>
                                        <asp:HiddenField ID="hdLastLeaveAllow" runat="server" />
                                    </tr>
                                    <tr style="display: none;">
                                        <td>
                                            <label>Is Carry Forward ?</label></td>
                                        <td>
                                            <asp:CheckBox ID="chkCarryForward" runat="server" CssClass="checkboxbtn" Text="" />
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
                        <h3 class="box-title">Leave </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdLeave" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdLeave_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="LeaveID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="LeaveName" HeaderText="Name" HeaderStyle-Width="30%" />
                                            <asp:BoundField DataField="LeaveShortName" HeaderText="Short Name" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="NumberofLeaves" HeaderText="Leaves Allow" HeaderStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("LeaveID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("LeaveID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:EntityDataSource ID="EDS_Leave" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Leave"
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
            $("[id$=grdLeave]").prepend($("<thead></thead>").append($("[id$=grdLeave]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=grdLeave]").prepend($("<thead></thead>").append($("[id$=grdLeave]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });

        }
    </script>
</asp:Content>
