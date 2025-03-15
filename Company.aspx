<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Company</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Company</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass="pull-right btn btn-payroll" Text="Add New" OnClick="BtnAddNew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="CompanyModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upCompany" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Company Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <table class="all-table all-tables">
                                    <tr>
                                        <td style="width: 140px;">
                                            <label>Name :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control SentenseCase" validate='vgroup' require='Please enter a Company Name'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Short Name :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a Company Short name'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Phone :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtPhone" MaxLength="11" runat="server" CssClass="form-control only-number" validate='vgroup' require='Please enter a Company phone'></asp:TextBox> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Email :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a Company e-mail' email='invalid email'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Working Hour (Included Lunch Hour)</label></td>
                                        <td>
                                            <asp:TextBox ID="txtWorkingHour" CssClass="form-control only-number" validate='vgroup' require='Please enter a Company working hour' runat="server"></asp:TextBox>  
                                        </td>
                                    </tr>
                                    <div runat="server" visible="false">
                                        <tr>
                                        <td>
                                            <label>EOBI Amount</label></td>
                                        <td>
                                            <asp:TextBox ID="txtEOBIAmount" CssClass="form-control only-number" validate='vgroup' require='Please enter a Company EOBI' runat="server"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Break Time Start :</label>
                                        </td>
                                        <td>
                                            <div class="controls">
                                                <div class="input-group bootstrap-timepicker">
                                                    <asp:TextBox ID="txtBreakStartTime" ClientIDMode="Static" runat="server" CssClass="form-control input-small" validate='vgroup' require='Please enter break start time'></asp:TextBox>
                                                    <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Break Time End :</label>
                                        </td>
                                        <td>
                                            <div class="controls">
                                                <div class="input-group bootstrap-timepicker">
                                                    <asp:TextBox ID="txtBreakEndTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server" validate='vgroup' require='Please enter break end time'></asp:TextBox>
                                                    <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    </div>
                                    <tr>
                                        <td>
                                            <label>Website</label></td>
                                        <td>
                                            <asp:TextBox ID="txtWebsite" CssClass="form-control" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Address :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-close" Text="Close" OnClick="btnClose_Click" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
        <div id="companygrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Company</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdCompany" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdCompany_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="CompanyID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="CompanyShortName" HeaderText="Company Short Name" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone" HeaderStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" ToolTip="Edit" runat="server" CommandArgument='<%#Eval("CompanyID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("CompanyID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                   <%-- <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                       DataSourceID="EDS_Company"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>--%>
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
            debugger
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function binddata() {
            $('#txtBreakStartTime').timepicker({ defaultTime: '01:00 PM' });
            $('#txtBreakEndTime').timepicker({ defaultTime: '02:00 PM' });
            $("[id$=grdCompany]").prepend($("<thead></thead>").append($("[id$=grdCompany]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=grdCompany]").prepend($("<thead></thead>").append($("[id$=grdCompany]").find("tr:first"))).dataTable({
                "order": [[0, "desc"]]
            });
        }
    </script>
</asp:Content>