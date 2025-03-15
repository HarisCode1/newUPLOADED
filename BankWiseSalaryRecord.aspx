<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="BankWiseSalaryRecord.aspx.cs" Inherits="BankWiseSalaryRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content">

        <div id="companygrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Bank Wise Detail</h3>
                    </div>
                    <label style="margin-left: 20px; margin-top: 10px;">Select Month</label>
                    <asp:DropDownList ID="DDownMonths" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" Width="100%" AutoPostBack="true" validate='vgroup' require='Please enter detail' AppendDataBoundItems="true" Style="width: 300px; margin-left: 20px;">
                        <%--  <asp:ListItem Value="0" Text="Please Select Month ..."></asp:ListItem>--%>
                    </asp:DropDownList>
                    <label style="margin-left: 20px;">Payment Mode</label>
                    <asp:DropDownList ID="ddlsearchbankwisedetail" ClientIDMode="Static" Width="200px" runat="server" Style="margin-left: 20px;">

                        <asp:ListItem>--Select one--</asp:ListItem>
                        <asp:ListItem>Cash</asp:ListItem>
                         <asp:ListItem>Cheque</asp:ListItem>
                        <asp:ListItem>HBL</asp:ListItem>
                        <asp:ListItem>UBL</asp:ListItem>
                       
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txtsearchbankwisedetail" runat="server" validate='vgroup' require='Please enter detail' Type="text" CssClass="form-control" placeholder="Enter Bank Name" Width="300"></asp:TextBox>--%>
                    <%--<asp:Button ID="btnsearchbankwisedetail" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnsearchbankwisedetail_Click" OnClientClick="return Saveonclick();"  style="margin-top:10px;  margin-left:20px;"/>
                    <div class="box-body">--%>
                    <input type="button" id="btnsearch" value="Search" class="btn btn-primary" style="margin: 10px 20px 20px;" />
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="divGrid"></div>
                                <div id="divCashGrid"></div>
                                <div id="divChequeGrid"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        </div>
    </section>
    <script src="assets/js/jquery.easing.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //  binddata();
        })

        function binddata() {
            // debugger
            //debugger
            //$('.datepick').datepicker({
            //    format: 'm/dd/yyyy',
            //    autoclose: true,
            //    clearBtn: false
            //});

        }


        $("#btnsearch").click(function () {

            var month = $("#DDownMonths").val();
            var bankname = $("#ddlsearchbankwisedetail").val();
            var d = new Date();
            var year = d.getFullYear();
            if (bankname == "Cash") {
                $.ajax({
                    //type: 'POST',
                    //url: "BankWiseSalaryRecord.aspx/Load",
                    //contentType: 'application/json; charset=utf-8',
                    //dataType: 'json',
                    //data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month+ "'}",
                    type: 'POST',
                    url: "BankWiseSalaryRecord.aspx/Load",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month + "'}",
                    success: function (result) {
                        if (result.d == "") {
                            msgbox(4, "", "Sorry record not found");
                            $("#divGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divChequeGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divCashGrid").dxDataGrid({
                                dataSource: null,
                            });
                        }
                        else
                        {
                            $("#divGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divChequeGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divCashGrid").dxDataGrid({
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
                                     "SNo",
                                     "EmployeeName",
                                    "Designation",
                                    "SalaryAmount",


                                ],

                                showBorders: true
                            });
                        }
                        

                    },
                    error: function (result) {
                    }
                });
            }
            else if (bankname == "Cheque") {
                $.ajax({
                    //type: 'POST',
                    //url: "BankWiseSalaryRecord.aspx/Load",
                    //contentType: 'application/json; charset=utf-8',
                    //dataType: 'json',
                    //data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month+ "'}",
                    type: 'POST',
                    url: "BankWiseSalaryRecord.aspx/Load",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month + "'}",
                    success: function (result) {
                        if (result.d == "") {
                            msgbox(4, "", "Sorry record not found");
                            $("#divChequeGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divCashGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divGrid").dxDataGrid({
                                dataSource: null,
                            });
                        }
                        else
                        {
                            $("#divCashGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divChequeGrid").dxDataGrid({
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
                                     "SNo",
                                     "EmployeeName",
                                     "BankTitle",
                                    "PaymentMethod",
                                     "SalaryAmount",


                                ],

                                showBorders: true
                            });

                        }
                    

                    },
                    error: function (result) {
                    }
                });
            }
            else {
                $.ajax({
                    //type: 'POST',
                    //url: "BankWiseSalaryRecord.aspx/Load",
                    //contentType: 'application/json; charset=utf-8',
                    //dataType: 'json',
                    //data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month+ "'}",
                    type: 'POST',
                    url: "BankWiseSalaryRecord.aspx/Load",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month + "'}",
                    success: function (result) {

                        if (result.d =="") {
                            msgbox(4, "", "Sorry record not found");
                            $("#divGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divChequeGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divCashGrid").dxDataGrid({
                                dataSource: null,
                            });
                        }
                        else
                        {
                            $("#divChequeGrid").dxDataGrid({
                                dataSource: null,
                            });
                            $("#divCashGrid").dxDataGrid({
                                dataSource: null,
                            });
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
                                     "SNo",
                                     "BankTitle",
                                    "AccountNo",
                                    "BranchCode",
                                    "BranchName",
                                    "SalaryAmount",


                                ],

                                showBorders: true
                            });
                        }

                    },
                    error: function (result) {
                    }
                });

            }

        })

        //function bindGrid() {
        //    debugger
        //    var editOption = 'EditCompany';
        //    var deleteOption = '';
        //    $.ajax({
        //        type: 'POST',
        //        url: "BankWiseSalaryRecord.aspx/Load",
        //        contentType: 'application/json; charset=utf-8',
        //        dataType: 'json',
        //        data: {},
        //        success: function (result) {

        //            $("#divGrid").dxDataGrid({
        //                dataSource: JSON.parse(result.d),
        //                columnsAutoWidth: true,
        //                paging: {
        //                    pageSize: 10
        //                },
        //                filterRow: {
        //                    visible: true,
        //                    applyFilter: "auto"
        //                },
        //                searchPanel: {
        //                    visible: true,
        //                    width: 240,
        //                    placeholder: "Search..."
        //                },
        //                headerFilter: {
        //                    visible: true
        //                },
        //                "export": {
        //                                enabled: true,
        //                                fileName: "Employees",
        //                                allowExportSelectedData: false
        //                },

        //                searchPanel: { visible: true },
        //                columns: [
        //                    "EmployeeID",
        //                     "AccounTitle",
        //                    "AccountNo",
        //                    "BranchCode",
        //                    "BankName",                                                      
        //                    {
        //                        dataField: "EmployeeID",

        //                        caption: "Action",
        //                        cellTemplate: function (container, options) {                                   

        //                        }

        //                    }

        //                ],

        //                showBorders: true
        //            });

        //        },
        //        error: function (result) {
        //        }
        //    });
        //}
        function Saveonclick() {

            if (validate('vgroup') == true) {
                window.location.href = "Employee.aspx";
                //msgbox(1, "Sucess", "S!");
                getCertificateRecords();
                return true;

            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

