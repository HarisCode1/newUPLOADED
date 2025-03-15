<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_Assets.aspx.cs" Inherits="Employee_Assets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>Employee Assets</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="employee.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Employee Assets</li>
        </ol>
    </section>

    <section class="content">
          <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Assets</h3>
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
    <script type="text/javascript">

        $(document).ready(function () {
            bindGrid();
        });

        //function bindGrid() {

        //    var editOption = 'EditCompany';
        //    var deleteOption = '';
        //    $.ajax({
        //        type: 'POST',
        //        url: "Employee_Assets.aspx/Load",
        //        contentType: 'application/json; charset=utf-8',
        //        dataType: 'json',
        //        data: {},
        //        success: function (result) {

        //            $("#divGrid").dxDataGrid({
        //                dataSource: JSON.parse(result.d),
        //                columnsAutoWidth: true,
        //                searchPanel: { visible: true },
        //                columns: [
        //                    "Title",
        //                    "Description",
        //                ],
        //                showBorders: true
        //            });

        //        },
        //        error: function (result) {
        //        }
        //    });
        //}

        function bindGrid() {
            $.ajax({
                url: "Employee_Assets.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                type: 'POST',
                dataType: 'json',
                data: {},
                success: function (result) {
                    debugger

                    var lookUpCondition = [
                                        { condition: "Excellent" },
                                        { condition: "Good" },
                                        { condition: "Fair" },
                    ];
                    $("#divGrid").dxDataGrid({
                        dataSource: JSON.parse(result.d),
                        keyExpr: "ID",
                        showBorders: true,
                        allowColumnResizing: true,
                        columnAutoWidth: true,
                        paging: {
                            enabled: false
                        },
                        editing: {
                            mode: "popup",
                            allowUpdating: true,
                            allowDeleting: true,
                            allowAdding: true,
                            popup: {
                                title: "Employee Assets",
                                showTitle: true,
                                width: 700,
                                height: 345,
                                position: {
                                    my: "top",
                                    at: "top",
                                    of: window
                                }
                            }
                        },
                        columns: [
                            {
                                dataField: "Title",
                                caption: "Title",
                                columnAutoWidth: true,
                                toolTip: "Some toolTip",
                                validationRules: [{
                                    type: "required"
                                }],
                            }, {
                                dataField: "Description",
                                caption: "Description",
                                columnAutoWidth: true,
                                validationRules: [{
                                    type: "required"
                                }]
                            },
                            {
                                dataField: "Given_Value",
                                columnAutoWidth: true,
                                validationRules: [{
                                    type: "required"
                                }]
                            },
                            {
                                dataField: "Given_Condition",
                                caption: "Condition",
                                validationRules: [{
                                    type: "required"
                                }],
                                lookup: {
                                    dataSource: lookUpCondition,
                                    displayExpr: "condition",
                                    valueExpr: "condition"
                                }
                            },
                            {
                                dataField: "CreatedDate",
                                caption: "Given Date",
                                dataType: "date",
                                formItem: { visible : false },
                                columnAutoWidth: true,
                                validationRules: [{
                                    type: "required"
                                }]
                            },
                             {
                                 dataField: "CreatedBy",
                                 caption: "Given By",
                                 formItem: { visible: false },
                             },
                        ],

                        onEditingStart: function (e) {
                            //logEvent("EditingStart");
                            //console.log(e.data);
                        },
                        onInitNewRow: function (e) {
                            //e.data.QuotedQuantity = 0;
                            //e.data.IsSubTotalApplicable = false;
                            //logEvent("InitNewRow");
                            //console.log(e);
                        },
                        onRowInserting: function (e) {
                            //logEvent("RowInserting");
                            console.log(e.data);
                            insertData(e.data);
                        },
                        onRowInserted: function (e) {
                            //logEvent("RowInserted");
                            //console.log(e.data);
                            bindGrid();
                        },
                        onRowUpdating: function (e) {
                            //logEvent("RowUpdating");
                            updateData(e.key, e.oldData, e.newData);
                            //console.log(e.key, e.oldData, e.newData);
                        },
                        onRowUpdated: function (e) {
                            //logEvent("RowUpdated");
                            //console.log(e.data);
                        },
                        onRowRemoving: function (e) {
                            //logEvent("RowRemoving");
                            deleteData(e.key);
                        },
                        onRowRemoved: function (e) {
                            //logEvent("RowRemoved");
                            //console.log(e.data);
                        }
                    });

                },
                error: function (result) {
                }
            });
        }


        function insertData(obj) {
            $.ajax({
                type: "POST",
                url: "Employee_Assets.aspx/Insert",
                contentType: "application/json",
                data: "{'Title':'" + obj.Title + "', 'Description':'" + obj.Description + "', 'Given_Value' : '" + obj.Given_Value + "', 'Given_Condition' : '" + obj.Given_Condition + "'}",
                dataType: "json",

                success: function (result) {
                    return true;
                },

                error: function (result) {
                    return false;
                }
            });
        }
        function updateData(key, oldData, obj) {

            obj.Title !== undefined ? oldData.Title = obj.Title : oldData.Title = oldData.Title;
            obj.Description !== undefined ? oldData.Description = obj.Description : oldData.Description = oldData.Description;
            obj.Given_Value !== undefined ? oldData.Given_Value = obj.Given_Value : oldData.Given_Value = oldData.Given_Value;
            obj.Given_Condition !== undefined ? oldData.Given_Condition = obj.Given_Condition : oldData.Given_Condition = oldData.Given_Condition;

            $.ajax({
                type: "POST",
                url: "Employee_Assets.aspx/Update",
                contentType: "application/json",
                data: "{'ID':'" + key + "','Title':'" + obj.Title + "', 'Description':'" + obj.Description + "', 'Given_Value' : '" + obj.Given_Value + "', 'Given_Condition' : '" + obj.Given_Condition + "'}",
                dataType: "json",

                success: function (result) {
                    return true;
                },

                error: function (result) {
                    return false;
                }
            });
        }

    </script>
</asp:Content>

