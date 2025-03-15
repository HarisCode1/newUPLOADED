<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="DeactiveEmployees.aspx.cs" Inherits="DeactiveEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <%-- <link href="css/jquery.timeentry.css" rel="stylesheet" />--%>
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />


    

    <style>
        .dropdown-menu {
            position: relative !important;
        }
    </style>
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
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#employes" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>--%>
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                   
                        <%--<a href="EmployeeCreation.aspx" id="chechempright" runat="server" class="btn btn-payroll pull-right">Add New</a>
                        <asp:Button ID="Btnexcelimport" ClientIDMode="Static" runat="server" Text="Bulk Import" CssClass="submit action-button btn btn-payroll pull-right" OnClick="Btnexcelimport_Click" />--%>

                      
                    </ContentTemplate>                    
                </asp:UpdatePanel>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Deactive Employees</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="divGrid"></div>
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
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
  $(document).ready(function () {
            bindGrid();
        });

        function bindGrid() {
            
            var editOption = 'EditCompany';
            var deleteOption = '';
            $.ajax({
                type: 'POST',
                url: "DeactiveEmployees.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {},
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
                        "export": {
                           enabled: true,
                           fileName: "Employees",
                           allowExportSelectedData: false
                        },
                        searchPanel: { visible: true },
                        columns: [
                            {
                                dataField: "EnrollId",
                                caption: "Employee Code",
                                width: 240,
                            },
                            "EmployeeName",
                            {
                                dataField: "Email",
                                width: 240,
                            },
                            {
                                dataField: "Designation",
                                width: 220,
                            },
                            {
                                dataField: "EmployeeID",
                                width: 240,
                                caption: "Action",
                                cellTemplate: function (container, options) {
                                    //container.append("<a href='/Requirements/ContributorJobDetails?ID=" + options.value + "'><i data-toggle='tooltip' title='Preview' data-placement='top' class='fa fa-eye fa-2x'></i></a>")       
                                    //container.append("<div class='dropdown'><a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu'><li><a href='Employes_Edit.aspx?ID=" + options.value + "'>Edit</a></li><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><a OnClick='deleteEmlpoyee(" + options.value + ")'>Delete</a></li><li><a href='Employes_Transfer.aspx?ID=" + options.value + "'>Transfer</a></li><li><a href='Employes_Increment.aspx?ID=" + options.value + "'>Apply Increment</a></li><li><a href='Employee_PromotionNew.aspx?ID=" + options.value + "'>Promotion</a></li><li><a href='WhMonitor.aspx?ID=" + options.value + "'>Working Hours</a></li><li><a href='employee_assets.aspx?ID=" + options.value + "'>Employee Assets</a></li>" + EndOfServiceBtn)
                                    //container.append("<div class='dropdown'><a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu'><li><a href='#'  id='aid' data-empid='"+options.value +"' onclick='checkright(this)'>Edit</a></li><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><a OnClick='DeleteEmlpoyee(" + options.value + ")'>Delete</a></li><li><li><a OnClick='TerminateEmlpoyee(" + options.value + ")'>Terminate</a></li><li><a href='Employes_Transfer.aspx?ID=" + options.value + "'>Transfer</a></li><li><a href='Employes_Increment.aspx?ID=" + options.value + "'>Apply Increment</a></li><li><a href='Employee_PromotionNew.aspx?ID=" + options.value + "'>Promotion</a></li><li><a href='WhMonitor.aspx?ID=" + options.value + "'>Working Hours</a></li><li><li><a href='Employee_Character.aspx?ID=" + options.value + "'>Behavior</a></li><li><a href='EmployeeFineCharges.aspx?ID=" + options.value + "'>")
                                    container.append("<div class='dropdown'><a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu'><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><li><a OnClick='activeEmlpoyee(" + options.value + ")'>Active</a></li>")
                                }

                            }
                            
                        ],
                       
                        showBorders: true
                    });

                },
                error: function (result) {
                }
            });            
        }

        function activeEmlpoyee(Employeeid) {
            var empid = Employeeid;

            console.log("InactiveEmlpoyee", empid);
            $.ajax({
                type: 'POST',
                url: "DeactiveEmployees.aspx/EmployeeInctive",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'EmployeeID':'" + empid + "'}",
                success: function (message) {
                    if (message.d === 'User activated succesfully') {

                        alert("User activated succesfully");
                        $("a[href='#'][onclick='activeEmlpoyee(" + empid + ")']").text("Activate");
                        location.reload();
                    }


                    else {
                        alert("EmployeeID is is not found");
                        location.reload();
                    }
                },
                error: function (result) {

                }
            });

        }
    </script>
</asp:Content>


