<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="AttendanceExcel.aspx.cs" Inherits="ExportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <section class="content-header">
        <h1>Attendance</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active"></li>
        </ol>
    </section>
     <section class="content">
    <div class="form-group">
        <asp:HiddenField ID="hdExcelupload" runat="server" />
       <%-- <label id="txtExcel" class="col-sm-4 control-label">Attach Excel File</label>--%>
        <asp:FileUpload ID="FileUploadExcel" runat="server" />
        <asp:Label ID="LblExcelUpload" runat="server" class="labelExcel control-label"></asp:Label>
         <asp:Button ID="btnExportExcel" ClientIDMode="Static" runat="server" Text="Bulk Import" CssClass="submit action-button btn btn-primary pull-right" OnClick="btnExcportExcel_Click"></asp:Button>
    </div>
         <br />
         <br />
         <br />
         <div class="form-group">
         <div id="dataGrid"></div>
             </div>
</section>
    <script>
            $.ajax({
                type: 'POST',
                url: "Employes.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: { },
                success: function (result) {

                    $("#divGrid").dxDataGrid({
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
                        searchPanel: { visible: true },
                        columns: [
                            "AttendanceID",
                            "CompanyName",
                            "EmployeeName",
                            "Date",
                            "InTime",
                            "OutTime"
                        ],
                        showBorders: true
                    });

                },
                error: function (result) {
                }
            });
           
   

        </script>
</asp:Content>

