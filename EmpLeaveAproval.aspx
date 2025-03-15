<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="EmpLeaveAproval.aspx.cs" Inherits="EmpLeaveAproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee Leave Approval </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="AttendanceModule.aspx"><i class="fa fa-pie-chart"></i>Leaves</a></li>
            <li class="active">Employee Leave Approval</li>
        </ol>
    </section>
    <section class="content">
        <%--Date Picker--%>
        <div class="row">
            <div class="col-md-8">
            </div>
        </div>
        <br clear="all" />
        <br />
        <%--Button Save--%>
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpSaveAttendance">
                    <ContentTemplate>
                        <asp:Button ID="btnSaveAttendance" runat="server" CssClass="hidden pull-right btn btn-primary" Text="Save" OnClick="btnSaveAttendance_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Leave Approval</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdAttendance" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" ClientIDMode="Static" OnRowCommand="grdAttendance_RowCommand" OnRowDataBound="grdAttendance_RowDataBound">
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="Attendance">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" ClientIDMode="Static" Checked="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAttendance" data-size="mini" runat="server" ClientIDMode="Static" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Employee">
                                                <HeaderStyle Width="40%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeName" runat="server" OnDataBin></asp:Label>

                                                    <%--<asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>--%>
                                                <%--</ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" SortExpression="Title" />

                                            <asp:TemplateField HeaderText="Employee" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" ></asp:Label>

                                                    <%--<asp:Label ID="lblEmployeeID" runat="server" Text='<%#Eval("EmployeeID")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date">
                                                <HeaderStyle Width="20%" />
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="grdtxtInTime" ClientIDMode="Static" CssClass="form-control input-small" ReadOnly="true" runat="server" Text='<%#Eval("FromDate","{0:dd-M-yyyy}")%>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date">
                                                <HeaderStyle Width="20%" />
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="grdtxtOutTime" ClientIDMode="Static" CssClass="form-control input-small" ReadOnly="true" runat="server" Text='<%#Eval("ToDate","{0:dd-M-yyyy}")%>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leaves Approve/Reject">
                                                <HeaderTemplate>
        <img src="images/action-1.png" width="50px" height="50px" />
    </HeaderTemplate>
                                                <HeaderStyle Width="12%"  />
                                                <ItemTemplate>
                                                     <asp:HyperLink ID="hyplnkEdit" runat="server" DataNavigateUrlFields="ID" CssClass=" pull-left btn btn-primary" NavigateUrl='<%# "/EmployeeLeaves.aspx?v2s=y&LOA="+Eval("LeaveApplicationID") %>' Text='Action'></asp:HyperLink>
                                                    <asp:HyperLink ID="hyplnkView" runat="server" DataNavigateUrlFields="ID" CssClass=" pull-right btn btn-primary" NavigateUrl='<%# "/EmployeeLeavesView.aspx?v2s=z&LOA="+Eval("LeaveApplicationID") %>' Text='View'></asp:HyperLink>
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
        <%--Modal--%>
        <div class="modal fade" id="EmployeeAttendance" tabindex="-1" role="dialog" aria-labelledby="EmployeeAttendance">
            <div class="modal-dialog" role="document">
                <div id="pnlDetail" runat="server">
                    <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalLabel">Attendance</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:GridView ID="grdMultiAttendance" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" ClientIDMode="Static" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="EmployeeAttendanceID" Visible="false">
                                                <HeaderStyle Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMultiGridEmpAttendanceID" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMultiGridEmpID" runat="server" Text='<%#Eval("EmployeeID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time In">
                                                <HeaderStyle Width="25%" />
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="MultitxtInTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time Out">
                                                <HeaderStyle Width="25%" />
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="MultitxtOutTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <HeaderStyle Width="25%" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMultiGridReason" ClientIDMode="Static" CssClass="form-control input-small" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkBtnAdd" runat="server" Text="Add Row" CssClass=" pull-right btn btn-primary" OnClick="lnkBtnAdd_Click"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnClose" runat="server" class="btn btn-primary" OnClick="btnClose_Click" Text="Closee"></asp:Button>
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-primary" OnClick="btnSave_Click" Text="Savee" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
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
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script src="assets/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
            $('#txtDate').datepicker('setDate', new Date());
            $("[id$=grdAttendance]").prepend($("<thead></thead>").append($("[id$=grdAttendance]").find("tr:first"))).dataTable();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function binddata() {
            $("#MultitxtOutTime ,#MultitxtInTime").timepicker();
            $('[id$=chkAttendance]').bootstrapSwitch({ 'size': 'small', state: $(this).prop("checked"), onText: 'Approve', offText: 'Reject', handleWidth: 27, onColor: 'success', offColor: 'danger' });
            $('[id$=chkSelectAll]').bootstrapSwitch({ 'size': 'small', state: $('[id$=chkSelectAll]').prop("checked"), onText: 'Approve', offText: 'Reject', handleWidth: 27, onColor: 'success', offColor: 'danger', labelText: 'ALL' });
            $("[id$=chkSelectAll").on('switchChange.bootstrapSwitch', function () {
                if ($('[id $=chkSelectAll]').is(':checked')) {
                    $('[id $=chkAttendance]').bootstrapSwitch('state', true);
                }
                else {
                    $('[id $=chkAttendance]').bootstrapSwitch('state', false);
                }
            });
        }
        $(window).load(function () {
            $('#ContentPlaceHolder1_btnSearch').click();
        });
    </script>
</asp:Content>
