<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="EmpSalaryGenerateNew.aspx.cs" Inherits="EmpSalaryGenerateNew" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>Salary Slip Generation</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Salary Slip Generation</li>
        </ol>
    </section>
    <section class="content">


        <div class="row">
            <div class="col-sm-12 " style="padding-bottom:20px;">

                <div class="col-md-6">
                    <asp:Button ID="btn_submit" CssClass="btn btn-payroll pull-right" runat="server" OnClick="btnEmploySalaryGenClick" Text="Employee Salary Generate" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Salary Generate</h3>

                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"
                                SizeToReportContent="True" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportPath="Report\EmpSalaryGenReport.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource Name="DataSet1" DataSourceId="SqlDataSource1"></rsweb:ReportDataSource>
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>

                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:vt_EMSConnectionString %>' SelectCommand="VT_SP_GetEmpSalaries6Feb2019" SelectCommandType="StoredProcedure">
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
</asp:Content>

