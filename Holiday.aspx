<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Holiday.aspx.cs" Inherits="Holiday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Holiday</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Holiday</li>
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
                        <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="Holiday" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Holiday Add/Edit</h4>
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
                                            <label>Holiday :</label></td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a holiday name'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>From Date :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" require='Please select from date' ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>To Date :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" require='Please select to date' ClientIDMode="Static"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>
                                            <label>Is Payable ?</label></td>
                                        <td>
                                            <asp:CheckBox ID="chkPayble" runat="server" CssClass="checkboxbtn" Text="" />
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
        <div class="row" id="divCompany" runat="server">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
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
                        <h3 class="box-title">Holiday </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdHoliday" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdHoliday_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="HolidayID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="HolidayName" HeaderText="Name" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:MM/dd/yyy}" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:MM/dd/yyy}" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Payble" HeaderText="Payable" HeaderStyle-Width="10%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" ToolTip="Edit" runat="server" CommandArgument='<%#Eval("HolidayID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("HolidayID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:EntityDataSource ID="EDS_Holiday" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Holiday"
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
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
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
            $("#txtFromDate").datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false
            }).on("change", function () {
                var start_date_array = $("#txtFromDate").val().split("/");
                $('#txtToDate').val("");
                $('#txtToDate').datepicker("destroy");
                if (start_date_array.length > 1) {
                    var dt = new Date(start_date_array[2], start_date_array[0] - 1, start_date_array[1]);
                    $('#txtToDate').datepicker({
                        format: 'm/dd/yyyy',
                        startDate: dt,
                        autoclose: true,
                        clearBtn: false
                    });
                } else {
                    $('#txtToDate').datepicker({
                        format: 'm/dd/yyyy',
                        autoclose: true,
                        clearBtn: false
                    });
                }
            });
            if ($("#txtFromDate").val() != "") {
                var start_date_array = $("#txtFromDate").val().split("/");
                var dt = new Date(start_date_array[2], start_date_array[0] - 1, start_date_array[1]);
                $('#txtToDate').datepicker({
                    format: 'm/dd/yyyy',
                    startDate: dt,
                    autoclose: true,
                    clearBtn: false
                });
            }
            else {
                $('#txtToDate').datepicker({
                    format: 'm/dd/yyyy',
                    autoclose: true,
                    clearBtn: false
                });
            }
            $("[id$=grdHoliday]").prepend($("<thead></thead>").append($("[id$=grdHoliday]").find("tr:first"))).dataTable().fnDestroy();

            $("[id$=grdHoliday]").prepend($("<thead></thead>").append($("[id$=grdHoliday]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });
        }
    </script>
</asp:Content>
