<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="EmployeeAttendance.aspx.cs" Inherits="EmployeeAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee Attendance </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="AttendanceModule.aspx"><i class="fa fa-pie-chart"></i>Attendance</a></li>
            <li class="active">Employee Attendance</li>
        </ol>
    </section>
    <section class="content">
        <%--Date Picker--%>
        <div class="row">
            <div class="col-md-8">
                <asp:UpdatePanel ID="updateDate" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr id="trCompany" runat="server">
                                <td>
                                    <label>Company :</label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' require='Please select company' customFn='var r = parseInt(this.value); return r > 0;' AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities"
                                        EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px;">
                                    <label>Date :</label></td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" autocomplete="off" validate='vgroup' require='Please select date' ClientIDMode="Static"></asp:TextBox>
                                    <asp:HiddenField ID="hdDate" runat="server" />
                                    <asp:HiddenField ID="hdCompany" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-payroll" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" Text="Search" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <br clear="all" />
        <br />
        <%--Button Save--%>
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpSaveAttendance">
                    <ContentTemplate>
                        <% if (Session["Username"].ToString() == "SuperAdmin")
                            { %>

                        <% }
                            else
                            { %>
                        <asp:Button ID="btnSaveAttendance" runat="server" CssClass=" pull-right btn btn-payroll" Visible="false" Text="Save" OnClick="btnSaveAttendance_Click" />

                        <%} %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Attendance</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdAttendance" EmptyDataText="No Data" ShowHeaderWhenEmpty="true" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" ClientIDMode="Static" OnRowCommand="grdAttendance_RowCommand" OnRowDataBound="grdAttendance_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="EmployeeAttendanceID" Visible="false">
                                                <HeaderStyle Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeAttendanceID" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attendance">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" ClientIDMode="Static" Checked="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAttendance" data-size="mini" runat="server" ClientIDMode="Static" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee">
                                                <HeaderStyle Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeID" runat="server" Text='<%#Eval("EmployeeID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time In">
                                                <HeaderStyle Width="25%" />
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="grdtxtInTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server"></asp:TextBox>
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
                                                            <asp:TextBox ID="grdtxtOutTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Multiple Entries" Visible="false">
                                                <HeaderStyle Width="25%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBtnAddMultiple" runat="server" Text="Add More" CssClass=" pull-right btn btn-primary" CommandName="AddMultipleAttendance" CommandArgument='<%#Eval("EmployeeID")%>'></asp:LinkButton>
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
                                    <asp:Button ID="btnClose" runat="server" class="btn btn-primary" OnClick="btnClose_Click" Text="Close"></asp:Button>
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-primary" OnClick="btnSave_Click" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
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
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function binddata() {
            $("#MultitxtOutTime ,#MultitxtInTime").timepicker();
            $('#grdtxtInTime').timepicker({ defaultTime: '09:00 AM' });
            $('#grdtxtOutTime').timepicker({ defaultTime: '06:00 PM' });
            $('#txtDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false
            }).on("change", function () {
                $('#ContentPlaceHolder1_hdDate').attr('value', $("#txtDate").val());
            });
            //$('[type="checkbox"]').bootstrapSwitch({ 'size': 'small', state: true, onText: 'P', offText: 'A', handleWidth: 27, onColor: 'success', offColor: 'danger', labelText: 'Select ALL' });
            $('[id$=chkAttendance]').bootstrapSwitch({ 'size': 'small', state: $(this).prop("checked"), onText: 'P', offText: 'A', handleWidth: 27, onColor: 'success', offColor: 'danger' });
            $('[id$=chkSelectAll]').bootstrapSwitch({ 'size': 'small', state: $('[id$=chkSelectAll]').prop("checked"), onText: 'P', offText: 'A', handleWidth: 27, onColor: 'success', offColor: 'danger', labelText: 'ALL' });
            //$("#dtpDate").on('changeDate', function (e) {
            //    $('#hdDate').attr('value', $("#dtpDate").val());
            //});
            // Script
            //$("[id$=chkSelectAll").change(function () {
            //    //$("[id$=chkAttendance").prop("checked", this.checked);
            //});
            $("#chkSelectAll").change(function () {
                alert('hi');
            });
            $("[id$=chkSelectAll").on('switchChange.bootstrapSwitch', function () {
                if ($('[id $=chkSelectAll]').is(':checked')) {
                    $('[id $=chkAttendance]').bootstrapSwitch('state', true);
                }
                else {
                    $('[id $=chkAttendance]').bootstrapSwitch('state', false);
                }
            });
        }
        //$("[id$=chkAttendance").on('switchChange.bootstrapSwitch', function () {
        //    //alert("attendance");
        //    var totalItems = $("[id$=chkAttendance").length;
        //    var totalCheckedItems = $('input[id$=chkAttendance]:checked').length
        //    if (totalItems == totalCheckedItems) {
        //        $("[id$=chkSelectAll").bootstrapSwitch('state', true);
        //    }
        //    else {
        //        $("[id$=chkSelectAll").bootstrapSwitch('state', false);
        //    }
        //});
        //          $("[id$=chkAttendance").change(function () {
        //               var totalItems = $("[id$=chkAttendance").length;
        //               var totalCheckedItems = $('input[id$=chkAttendance]:checked').length
        //               if (totalItems == totalCheckedItems) {
        //                   $("[id$=chkSelectAll").prop("checked", true)
        //                   }
        //               else{
        //                   $("[id$=chkSelectAll").prop("checked", false)
        //                  }
        //       });
        //}
        $(window).load(function () {
            $('#ContentPlaceHolder1_btnSearch').click();
        });
    </script>
</asp:Content>
