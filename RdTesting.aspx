<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="RdTesting.aspx.cs" Inherits="RdTesting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Employee</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <div id="divGrid"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <script>
     

        $(document).ready(function () {
                AdminJobGrid();
            });

        function AdminJobGrid() {
            $.ajax({
                    type: 'POST',
                    url: "RdTesting.aspx/Test",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: { },
                    success: function (result) {

                        debugger
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
                            columns: ["CompanyID", "CompanyName", "EmployeeID", "EmployeeName"],
                            showBorders: true
                        });

                    },
                    error: function (result) {
                    }
                });
            }
</script>

</asp:Content>


