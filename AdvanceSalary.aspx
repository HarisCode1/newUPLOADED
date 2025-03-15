<%@ Page Title="" Language="C#" MasterPageFile="NewMain.master" AutoEventWireup="true" CodeFile="AdvanceSalary.aspx.cs" Inherits="EmailSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Advance Salary</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="InputModules.aspx"><i class="fa fa-laptop"></i>Input Modules</a></li>
            <li class="active">Advance Salary</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-payroll pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row" id="divCompany" runat="server">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updateCompany" runat="server">
                    <ContentTemplate>
                        <table style="display:none">
                            <tr>
                                <td style="width: 130px;">
                                    <label>Company :</label></td>
                                <td>

                                    <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
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
        <div class="modal fade" id="EmailSetting" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Advance Salary</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <table class="all-tables">
                                    <tr id="trCompany" runat="server">
                                        <td style="width: 130px;">
                                            <label>Company :</label></td>
                                        <td>
                                            <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' require='Please select company' customFn='var r = parseInt(this.value); return r > 0;' AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                                                <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:EntityDataSource ID="EDS_Comp" runat="server"
                                                ConnectionString="name=vt_EMSEntities"
                                                DefaultContainerName="vt_EMSEntities"
                                                EntitySetName="vt_tbl_Company">
                                            </asp:EntityDataSource>
                                        </td>
                                    </tr>
                                    <asp:HiddenField ID="hdnAdvSalaryID" ClientIDMode="Static" runat="server" />
                                    <tr>
                                        <td style="width: 120px;">
                                            <label>Employee :</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" validate='vgroup' require='Please select employee'>
                                                <asp:ListItem Value="" Text="Please Select Employee"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Advance of Month :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Advance Amount :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtGrossSalary" Style="display:none;" runat="server" CssClass="form-control auto-numeric" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                            <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control auto-numeric" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Advance Release Date :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtDateRelease" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClick="btnSave_OnClick" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
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
                        <h3 class="box-title">Advance Salary </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdEmailSetting" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdEmailSetting_RowCommand" OnRowDataBound="grdEmailSetting_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="AdvSalaryID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:TemplateField HeaderText="Company">
                                                <HeaderStyle Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="SalaryOfMonth" HeaderText="Advance of month" HeaderStyle-Width="15%" DataFormatString="{0:MM/dd/yyy}" />
                                            <asp:BoundField DataField="SalaryAmount" HeaderText="Advance Amount" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="AdvSalaryReleaseDate" HeaderText="Advance Release Date" HeaderStyle-Width="15%" DataFormatString="{0:MM/dd/yyy}" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" ToolTip="Edit"  runat="server" CommandArgument='<%#Eval("AdvSalaryID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("AdvSalaryID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
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
    <style>
        .bootstrap-datetimepicker-widget {
            z-index: 999999;
        }
    </style>
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
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
            $('#txtDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1,
                startDate: new Date()
            });
            $('#txtDateRelease').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false
            });
            $(document).on("change", "#txtAmt", function () {
                if ($(this).val() != "" && $("#txtGrossSalary").val() != "") {
                    var gross = parseFloat($("#txtGrossSalary").val().replace(",", ""));
                    var current = parseFloat($(this).val().replace(",", ""));
                    if (current > gross) {
                        $(this).val("");
                    }
                } else {
                    $(this).val("");
                }
            });
            $("[id$=grdEmailSetting]").prepend($("<thead></thead>").append($("[id$=grdEmailSetting]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=grdEmailSetting]").prepend($("<thead></thead>").append($("[id$=grdEmailSetting]").find("tr:first"))).dataTable({
                "order":[[0,"decs"]]
            });

        }
        
  $(document).ready(function () {

            $("#<%=ddlCompany.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

      });
    </script>
</asp:Content>