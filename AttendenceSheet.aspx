<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="~/AttendenceSheet.aspx.cs" Inherits="AttendenceSheet" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Attendence Sheet</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Attendence Sheet</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <div class="col-md-3">
                    <label>Company *:</label>
                    <asp:UpdatePanel ID="updateCompany" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server"></asp:DropDownList>
                       <%--     <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" require='Please select company' CssClass="form-control input-sm" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' AutoPostBack="true" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <asp:EntityDataSource ID="EDS_Company" runat="server"
                                ConnectionString="name=vt_EMSEntities"
                                DefaultContainerName="vt_EMSEntities"
                                EntitySetName="vt_tbl_Company">
                            </asp:EntityDataSource>--%>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-3">
                    <label>Month of Attendence * :</label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" require='Please select date' ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Select Department:</label>
                    <asp:DropDownList ID="DDldepartment" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label style="display:block">&nbsp;</label>
                    <asp:Button ID="btn_submit" CssClass="btn btn-primary pull-right" OnClick="btn_submit_Click" runat="server" Text="Generate Attendance Sheet" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
                </div>
                
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Attendence Sheet</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%"
                                SizeToReportContent="True" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportPath="Report\rpt_attendanceSheet.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>

                            <asp:ObjectDataSource runat="server" SelectMethod="GetData" TypeName="vt_EMSDataSetTableAdapters.VT_SP_GetEmpAttendanceReportTableAdapter" ID="ObjectDataSource1"></asp:ObjectDataSource>
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
                format: 'dd/m/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1
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
