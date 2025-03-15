<%@ Page Title="" Language="C#" MasterPageFile="NewMain.master" AutoEventWireup="true" CodeFile="ReportGenerator.aspx.cs" Inherits="EmailSetting" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <h1>Salary Slip Generation</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Salary Slip Generation</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <div class="col-md-4">
                    <label>Company *:</label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCompany" Enabled="false" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-4">
                    <label>Employee *:</label>
                    <asp:DropDownList ID="ddlEmployee" Enabled="false" ClientIDMode="Static" runat="server" validate='vgroup' CssClass="form-control input-sm" require='Please select employee'>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label>Month of Salary *:</label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12">
                <%--<asp:Button ID="btn_submit" CssClass="btn btn-primary pull-right btn-color-green" OnClick="btn_submit_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" runat="server" Text="Generate PaySlips" />--%>
                <asp:Button ID="btnYearlySlip" CssClass="btn btn-primary pull-right btn-color-green" OnClick="btnYearlySlip_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" runat="server" Text="Download Yearly Slips" />   
                <%--<asp:Button ID="BtnYearly" CssClass="btn btn-primary pull-right btn-color-green" OnClick="BtnYearly_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" runat="server" Text="Download Yearly Slips" />   --%>
                <asp:Button ID="BtnMonthly" CssClass="btn btn-primary pull-right btn-color-green" OnClick="BtnMonthly_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" runat="server" Text="Download Monthly Slip" />
                
            </div>
        </div>
        <br clear="all" />
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Salary Slips</h3>
                        <div class="box-tools">
                            <%--<asp:Button ID="BtnLog" runat="server" CssClass="btn btn-default pull-right" Text="Salary Log" OnClick="BtnLog_Click" CausesValidation="false" />--%>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <rsweb:ReportViewer ID="ReportViewer1" CssClass="fonts-table" runat="server" Height="100%" Width="100%"
                                SizeToReportContent="True" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportPath="Report\SalarySlip.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="employes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg" style="width: 96% !important">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Employee - <small>Salary Log</small></h4>
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive">
                            <asp:GridView ID="GvLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="Position" HeaderText="Position" />
                                    <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary" />
                                    <asp:BoundField DataField="HouseRentAllownce" HeaderText="House Rent Allownce" />
                                    <asp:BoundField DataField="TransportAllownce" HeaderText="Transport Allownce" />
                                    <asp:BoundField DataField="FuelAllowance" HeaderText="Fuel Allowance" />
                                    <asp:BoundField DataField="MedicalAllowance" HeaderText="Medical Allowance" />
                                    <asp:BoundField DataField="Loan" HeaderText="Loan" />
                                    <asp:BoundField DataField="NetSalary" HeaderText="Net Salary" />
                                    <asp:BoundField DataField="SalaryMonth" HeaderText="Salary Month" DataFormatString="{0:MMMM d, yyyy}" />
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Generation Date" DataFormatString="{0:MMMM d, yyyy}" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
            <!-- /.modal-dialog -->
        </div>


        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <input type="button" id="btnsummarysheet" Class=" pull-left btn btn-payroll" value="SummarySheet" />
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Summary Sheet</h3>
                        <div class="box-tools">
                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-default pull-right" Text="Salary Log" OnClick="BtnLog_Click" CausesValidation="false" Visible="false"/>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="devyearlysummarysheet" ></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
        </Triggers>
    </asp:UpdatePanel>
    </section>

    <style>
        .bootstrap-datetimepicker-widget {
            z-index: 999999;
        }
    </style>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">


        $("#btnsummarysheet").click(function () {
            var Month = $("#txtDate").val();
            var EmpId = $("#ddlEmployee").val();
            $.ajax({
                //type: 'POST',
                //url: "BankWiseSalaryRecord.aspx/Load",
                //contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                //data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month+ "'}",
                type: 'POST',
                url: "ReportGenerator.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify({ Month: Month, EmpId: EmpId }),
                //data: "{'Month':'" + Month + "','EmpId':'" + EmpId + "'}",
                success: function (result) {
                    if (result.d == "Empty") {
                        alert("Please select month");
                    }
                    else {
                        $("#devyearlysummarysheet").dxDataGrid({
                            dataSource: JSON.parse(result.d),
                            columnsAutoWidth: true,
                            paging: {
                                pageSize: 10
                            },
                            filterRow: {
                                visible: true,
                                applyFilter: "auto"
                            },
                            searchPanel: {
                                visible: true,
                                width: 240,
                                placeholder: "Search..."
                            },
                            headerFilter: {
                                visible: true
                            },
                            "export": {
                                enabled: true,
                                fileName: "SummarySheet",
                                allowExportSelectedData: false
                            },

                            searchPanel: { visible: true },
                            summary: {
                                coulmn : "Total",
                                totalItems: [
                                     {
                                       

                                     },
                                {
                                    column: "Basicsalary",
                                    summaryType: "sum",

                                },
                                 {
                                     column: "ActualSalary",
                                     summaryType: "sum",

                                 },
                                    {
                                        column: "FoodAllowance",
                                        summaryType: "sum",

                                    },
                                        {
                                            column: "TransportAllowance",
                                            summaryType: "sum",

                                        },
                                         {
                                             column: "MedicalAllowance",
                                             summaryType: "sum",

                                         },
                                         {
                                             column: "OverTime",
                                             summaryType: "sum",

                                         },
                                            {
                                                column: "Bonus",
                                                summaryType: "sum",

                                            },
                                                {
                                                    column: "GrossSalary",
                                                    summaryType: "sum",

                                                },
                                                    {
                                                        column: "Tax",
                                                        summaryType: "sum",

                                                    },
                                                                {
                                                                    column: "Loan",
                                                                    summaryType: "sum",

                                                                },
                                              {
                                                  column:"Netsalary",
                                                  summaryType: "sum",

                                              },                                         

                                ],

                            },
                            columns: [                               
                                "Months",
                                 "Basicsalary",
                                 "ActualSalary",
                                "FoodAllowance",
                                "TransportAllowance",
                                "MedicalAllowance",
                                "OverTime",
                                "Bonus",
                                "GrossSalary",
                                "Tax",
                                "Loan",
                                "Netsalary"
                            ],

                            showBorders: true
                        });

                    }

                },
                error: function (result) {
                }
            });

        });


        $(function () {
            debugger;
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function binddata() {
            $("[id$=GvLog]").prepend($("<thead></thead>").append($("[id$=GvLog]").find("tr:first"))).dataTable();

            $('#txtDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1
            }).datepicker("setDate", 'now');
        }

        $(document).ready(function () {

            $("#<%=ddlCompany.ClientID%>").select2({

                placeholder: "Select Item",

                allowClear: true

            });

        });
            $(document).ready(function () {

                $("#<%=ddlEmployee.ClientID%>").select2({

                    placeholder: "Select Item",

                    allowClear: true

                });

            });
    </script>
</asp:Content>