<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="CompanySalaryReport.aspx.cs" Inherits="EmailSetting" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <h1>Salary Report</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Salary Report</li>
        </ol>
    </section>
    <section class="content">


        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">

                <div class="col-md-6">
                    <asp:Button ID="btn_submit" CssClass="btn btn-primary pull-right" OnClick="btnEmploySalaryGenClick" runat="server" Text="Employee Salary Generate" Visible="false" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Company Salary Report</h3>

                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"
                                SizeToReportContent="True" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportPath="Report\MonthlySalaryReport.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource Name="DataSet1" DataSourceId="SqlDataSource1"></rsweb:ReportDataSource>
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>

                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:vt_EMSConnectionString %>' SelectCommand="VT_SP_GetReportSalaries" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:QueryStringParameter QueryStringField="month" Name="currentMonthDate" Type="DateTime"></asp:QueryStringParameter>
                                    <asp:QueryStringParameter QueryStringField="companyID" Name="CompanyID" Type="Int32"></asp:QueryStringParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
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
    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script src="js/bootstrap-typeahead.js"></script>
    <script type="text/javascript">

        $(function () {
            binddata();
        });

        function binddata() {
            $('.date').datetimepicker({ pickTime: false, autoclose: true });

            PageMethods.getEmpList(OnRequestComplete, OnRequestError);
        }

        function OnRequestComplete(res, userContext, methodName) {
            $("#txtEmpName").typeahead({
                source: function (typeahead, query) {
                    $("#txtEmpID").val("");
                    typeahead.process($.map(res, function (item) {
                        return {
                            value: item[1],
                            EmpID: item[0]
                        }
                    }));
                },
                minLength: 1,
                property: 'value',
                appendtoSelector: "[id $= pnlDetail]",
                SubTop: 90,
                SubLeft: 182.5,
                onselect: function (obj) {
                    $("#txtEmpID").val(obj.EmpID);
                }
            });
        }

        function OnRequestError(error, userContext, methodName) {
            if (error != null) {
                $("#txtEmpID").val("");
            }
        }
    </script>


</asp:Content>

