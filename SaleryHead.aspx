<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="SaleryHead.aspx.cs" Inherits="SaleryHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Salary Head </h1>
        <ol class="breadcrumb">
           <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Salary Head</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-primary" Text="Add New" OnClick="btnAddnew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="saleryhead" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Salary Head Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table>
                                            <tr id="trCompany" runat="server">
                                                <td style="width: 130px;">
                                                    <label>Company :</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' AutoPostBack="true" AppendDataBoundItems="true">
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
                                                <td style="width: 130px;">
                                                    <label>Name :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a name'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Printing Name :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtPrintingName" runat="server" CssClass="salery-printingtxt form-control" validate='vgroup' require='Please enter a Printing name'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Type :</label></td>
                                                <td>
                                                    <div class="pull-left salery-type">
                                                        <asp:DropDownList ID="ddlType" ClientIDMode="Static" runat="server">
                                                            <asp:ListItem>Earning</asp:ListItem>
                                                            <asp:ListItem>Deduction</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="pull-left salery-variable">
                                                        <asp:CheckBox ID="chkVariable" ClientIDMode="Static" runat="server" CssClass="checkboxbtn" Text="Variable" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Applicable From :</label></td>
                                                <td>
                                                    <div id="dtpAppFromDate" class="input-group date" style="width: 214px;">
                                                        <asp:TextBox ID="txtAppFromDate" runat="server"></asp:TextBox>
                                                        <span class="add-on input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:CheckBox ID="chkPF" ClientIDMode="Static" runat="server" CssClass="checkboxbtn" Text="PF" />
                                                    <asp:CheckBox ID="chkPT" ClientIDMode="Static" runat="server" CssClass="checkboxbtn" Text="PT" />
                                                    <asp:CheckBox ID="chkEOBI" ClientIDMode="Static" runat="server" CssClass="checkboxbtn" Text="EOBI" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Remark :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
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
                        <h3 class="box-title">Salary Head </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdSalaryHead" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdSalaryHead_RowCommand" OnRowDataBound="grdSalaryHead_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID">
                                                <HeaderStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("SalaryHeadID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Company">
                                                <HeaderStyle Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SalaryHeadName" HeaderText="Salary Head Name" />
                                            <asp:BoundField DataField="PrintingName" HeaderText="Printing Name" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="5%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("SalaryHeadID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("SalaryHeadID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:EntityDataSource ID="EDS_SalaryHead" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_SalaryHead"
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
    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        });
        function binddata() {
            $('#dtpAppFromDate').datetimepicker({ pickTime: false });
            if ($("#ddlType").val() == "Deduction") {
                $("#chkPF,#chkPT,#chkEOBI").prop("checked", false);
                $("#chkPF,#chkPT,#chkEOBI").prop("disabled", true);
            }
            else if ($("#chkVariable").prop("checked")) {
                $("#chkPF,#chkPT").prop("checked", false);
                $("#chkPF,#chkPT").prop("disabled", true);
            }
            $("#ddlType").change(function () {
                if (this.value == "Deduction") {
                    $("#chkPF,#chkPT,#chkEOBI").prop("checked", false);
                    $("#chkPF,#chkPT,#chkEOBI").prop("disabled", true);
                }
                else if ($("#chkVariable").prop("checked")) {
                    $("#chkPF,#chkPT").prop("checked", false);
                    $("#chkPF,#chkPT").prop("disabled", true);
                    $("#chkEOBI").prop("disabled", false);
                }
                else {
                    $("#chkPF,#chkPT,#chkEOBI").prop("disabled", false);
                }
            });
            $("[id$=grdSalaryHead]").prepend($("<thead></thead>").append($("[id$=grdSalaryHead]").find("tr:first"))).dataTable();
            $("#chkVariable").change(function () {
                if (this.checked) {
                    $("#chkPF,#chkPT").prop("checked", false);
                }
                $("#chkPF,#chkPT").prop("disabled", this.checked);
            });
        }
    </script>
</asp:Content>