<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="ManualAttendance.aspx.cs" Inherits="ManualAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <%-- <link href="css/jquery.timeentry.css" rel="stylesheet" />--%>
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Manual Attendance</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="AttendanceModule.aspx"><i class="fa fa-pie-chart"></i>Attendance</a></li>
            <li class="active">Manual Attendance</li>
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
                        <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-payroll" Text="Add New" OnClick="btnAddNew_Click" />
                        <%} %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="manualattendance" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="upMAttendance" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Manual Attendance Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-6">
                                        <fieldset>
                                            <legend>Manual Attendance Entry</legend>
                                        </fieldset>
                                        <table>
                                            <tr id="trCompany" runat="server">
                                                <td style="width: 120px;">
                                                    <label>Company :</label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlModalCompany" ClientIDMode="Static" runat="server" DataSourceID="EDS_ModalCompany" validate='vgroup' require='Please select company' DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlModalCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:EntityDataSource ID="EDS_ModalCompany" runat="server"
                                                        ConnectionString="name=vt_EMSEntities"
                                                        DefaultContainerName="vt_EMSEntities"
                                                        EntitySetName="vt_tbl_Company">
                                                    </asp:EntityDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Employee :</label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" runat="server" validate='vgroup' require='Please select employee'>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px;">
                                                    <label>Date :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Status :</label>
                                                </td>
                                                <td>
                                                    <div class="pull-left">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" validate='vgroup' ClientIDMode="Static" require='Please select status'>
                                                            <asp:ListItem>P</asp:ListItem>
                                                            <asp:ListItem>A</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>In :</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="txtInTime" ClientIDMode="Static" runat="server" CssClass="form-control input-small" validate='vgroup' require='Please enter time in'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>Out :</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="txtOutTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server" validate='vgroup' require='Please enter time out'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" OnClick="btnClose_Click" />

                            </div>
                        </div>
                        <!-- /. modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="row" id="divCompany" runat="server">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 130px;">
                                    <label>Company :</label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
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
                        <h3 class="box-title">Manual Attendance</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdManualAttendance" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdManualAttendance_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="ManualAttendanceID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-Width="15%" DataFormatString="{0:MM/dd/yyy}" />
                                            <asp:BoundField DataField="InTime" HeaderText="In Time" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="OutTime" HeaderText="Out Time" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="10%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" ToolTip="Edit" runat="server" CommandArgument='<%#Eval("ManualAttendanceID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("ManualAttendanceID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
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
    </section>
    <%-- <script src="js/jquery.plugin.js"></script>--%>
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
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
            $('#txtFromDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false
            }).datepicker("setDate", 'now');
            $('#txtInTime').timepicker({ defaultTime: '09:00 AM' });
            $('#txtOutTime').timepicker({ defaultTime: '06:00 PM' });
            $("[id$=grdManualAttendance]").prepend($("<thead></thead>").append($("[id$=grdManualAttendance]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=grdManualAttendance]").prepend($("<thead></thead>").append($("[id$=grdManualAttendance]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });
        }
        $(document).on("change", "#ddlStatus", function () {
            if ($(this).val() == "P") {
                $("#txtInTime").attr("validate", "vgroup");
                $("#txtOutTime").attr("validate", "vgroup");
                $(".present-area").show();
            } else {
                $(".present-area").hide();
                $("#txtInTime").val("");
                $("#txtOutTime").val("");
                $("#txtInTime").removeAttr("validate");
                $("#txtOutTime").removeAttr("validate");
            }
        });
    </script>
</asp:Content>
