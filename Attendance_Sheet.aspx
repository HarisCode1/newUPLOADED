<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Attendance_Sheet.aspx.cs" Inherits="Attendance_Sheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <%-- <link href="css/jquery.timeentry.css" rel="stylesheet" />--%>
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />
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
            <%--     <asp:FileUpload ID="FileUploadExcel" runat="server" />
        <asp:Label ID="LblExcelUpload" runat="server" class="labelExcel control-label"></asp:Label>
         <asp:Button ID="btnExportExcel" ClientIDMode="Static" runat="server" Text="Bulk Import" CssClass="submit action-button btn btn-primary pull-right" OnClick="btnExcportExcel_Click"></asp:Button>--%>
            <% if (Session["Username"].ToString() == "SuperAdmin")
                { %>

            <% }
                else
                { %>
            <asp:Button ID="Btnexcelimport" ClientIDMode="Static" runat="server" Text="Bulk Import" CssClass="submit action-button btn btn-payroll pull-right" OnClick="btnExcportExcel_Click" />
            <asp:Button ID="btnAdd" Visible="true" runat="server" Text="Add Manual Attendance" CssClass="submit action-button btn btn-payroll pull-right" OnClick="btnAdd_Click" />
            <asp:Button ID="btntimeout" Visible="true" runat="server" Text="Set Time out" CssClass="submit action-button btn btn-payroll pull-right" OnClick="btntimeout_Click" />


            <%} %>



            <%--  <div class="file-upload-wrapper file-upload-wrapper1" data-text="Import Excel File!">
                <asp:FileUpload ID="FileExportExcel" runat="server" />
            </div>--%>
            <% if (Session["Username"].ToString() == "SuperAdmin")
                { %>

            <% }
                else
                { %>
            <asp:FileUpload type="file" class="form-control cstm-btn-file" ID="FileExportExcel" runat="server" />


            <%} %>
            
            <label style="margin-top:20px;"> Search Attendence By Month* :</label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off"  ClientIDMode="Static" onchange="SearchByDate(this)" Width="300px" style="display:inline;"></asp:TextBox>
                    <input type="button" id="Btncancel" value="Load" style="width:100px; height:35px;"/>
        </div>
        <br />
        <br />
        <br />

        <div class="form-group">
            <div id="divGrid"></div>
        </div>
        <div class="modal fade" id="manualattendance" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="upMAttendance" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Manual Attendance Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-6">
                                        <fieldset>
                                            <legend>Manual Attendance Entry</legend>
                                        </fieldset>
                                        <table>
                                            <%--<tr id="trCompany" runat="server">
                                                <td style="width: 120px;">
                                                    <label>Company :</label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlModalCompany" ClientIDMode="Static" runat="server" DataSourceID="EDS_ModalCompany" validate='vgroup' require='Please select company' DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlModalCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:EntityDataSource ID="EDS_ModalCompany" runat="server"
                                                        ConnectionString="name=vt_EMSEntities"
                                                        DefaultContainerName="vt_EMSEntities"
                                                        EntitySetName="vt_tbl_Company">
                                                    </asp:EntityDataSource>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Employee :</label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" runat="server" validate='vgroup' require='Please select employee'>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px;">
                                                    <label>Date :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Status :</label>
                                                </td>
                                                <td>
                                                    <div class="pull-left">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" validate='vgroup' ClientIDMode="Static" require='Please select status'>
                                                            <asp:ListItem>P</asp:ListItem>
                                                            <asp:ListItem>A</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>In :</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="txtInTime" ClientIDMode="Static" runat="server" CssClass="form-control input-small" validate='vgroup' require='Please enter time in'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>Out :</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="txtOutTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server" validate='vgroup' require='Please enter time out'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>Overtime:</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group">
                                                            <asp:CheckBox ID="chkOvertime" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-payroll" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnSave_Click" />--%>
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save"  OnClick="btnSave_Click" />
                                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" OnClick="btnClose_Click" />

                            </div>
                        </div>
                        <!-- /. modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="modal fade" id="manualattendanceUpdate" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Manual Attendance Edit</h4>
                            </div>
                            <div id="pnlDetail2" runat="server" class="modal-body">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-6">
                                        <fieldset>
                                            <legend>Manual Attendance Entry</legend>
                                        </fieldset>
                                        <table>
                                            <%--<tr id="trCompany" runat="server">
                                                <td style="width: 120px;">
                                                    <label>Company :</label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlModalCompany" ClientIDMode="Static" runat="server" DataSourceID="EDS_ModalCompany" validate='vgroup' require='Please select company' DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlModalCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:EntityDataSource ID="EDS_ModalCompany" runat="server"
                                                        ConnectionString="name=vt_EMSEntities"
                                                        DefaultContainerName="vt_EMSEntities"
                                                        EntitySetName="vt_tbl_Company">
                                                    </asp:EntityDataSource>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Employee :</label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUEmp" ClientIDMode="Static" runat="server" validate='vgroup' require='Please select employee' Enabled="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px;">
                                                    <label>Date :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtUdate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date' Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Status :</label>
                                                </td>
                                                <td>
                                                    <div class="pull-left">
                                                        <asp:DropDownList ID="ddlUstatus" runat="server" validate='vgroup' ClientIDMode="Static" require='Please select status'>
                                                            <asp:ListItem Selected="True">P</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>In :</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="txtUIN" ClientIDMode="Static" runat="server" CssClass="form-control input-small" validate='vgroup' require='Please enter time in'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>Out :</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:TextBox ID="txtUOUT" ClientIDMode="Static" CssClass="form-control input-small" runat="server" validate='vgroup' require='Please enter time out'></asp:TextBox>
                                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="present-area">
                                                <td>
                                                    <label>Overtime:</label>
                                                </td>
                                                <td>
                                                    <div class="controls">
                                                        <div class="input-group">
                                                            <asp:CheckBox ID="chkUOvertime" runat="server" ClientIDMode="Static" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:HiddenField ID="hdnAttID" runat="server" ClientIDMode="Static" />
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-payroll" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnUClose" runat="server" CssClass="btn btn-payroll" Text="Close" OnClick="btnClose_Click" />

                            </div>
                        </div>
                        <!-- /. modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
    </section>
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
       function reload()
       {
           BindAttendance();

       }
        $(function () {
           
            binddata();
            BindAttendance();
            $('#txtDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1
            });
        
        });
        $("#Btncancel").click(function () {

          
            BindAttendance();
            $("#txtDate").val('');
        })
        function SearchByDate(e) {

            var date = $(e).val();
            $.ajax({
                type: 'POST',
                url: "Attendance_Sheet.aspx/LoadBySearch",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify({ date: date }),
                success: function (result) {


                    //  debugger
                    if (result.d == "") {
                        $("#divGrid").dxDataGrid({
                            dataSource: null,
                        });
                    }
                    else {
                        $.each(JSON.parse(result.d), function (key, value) {
                            if (value.Date.startsWith("/Date")) {
                                result.List[key].StartDate = new Date(parseInt(value.StartDate.substr(6)))
                            }
                        });
                        $("#divGrid").dxDataGrid({
                            allowColumnResizing: true,
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
                                 {
                                     dataField: "AttendanceID",
                                     caption: "AttendanceID",
                                     width: 80,
                                     visible: false
                                 },
                                  {
                                      dataField: "CompanyName",
                                      caption: 'CompanyName',
                                      width: 160,
                                  },
                                 {
                                     dataField: "EmployeeCode",
                                     caption: 'EmployeeID',
                                     width: 110,
                                 },

                                {
                                    dataField: "EmployeeName",
                                    caption: 'EmployeeName',


                                },

                                {
                                    dataField: "Date",
                                    caption: 'Date',
                                    format: 'yyyy-MM-dd',
                                    dataType: "date",
                                    width: 100,
                                },

                                 {
                                     dataField: "InTime",
                                     caption: "InTime",
                                     width: 200,
                                 },
                                 {
                                     dataField: "OutTime",
                                     caption: "OutTime",
                                     width: 200,
                                 },
                                   {
                                       dataField: "TotalHours",
                                       caption: "Hours",
                                       width: 70,
                                   },

                                 {
                                     dataField: "OT",
                                     caption: "OverTime",
                                     width: 100,
                                     cellTemplate: function (container, options) {

                                         if (options.values[7] == 1) {

                                             container.append("<span class='fa fa-check' style='color:Blue;'></span>");
                                         }

                                     }
                                 },
                                 {
                                     dataField: "AttendanceID",
                                     caption: "Action",
                                     width: 80,
                                     cellTemplate: function (container, options) {

                                         container.append("<a href='#' onclick='return editRecord(" + options.value + ")'><i class='fa fa-edit'></i> Edit</a>")
                                     }
                                 }



                            ],
                            showBorders: true
                        });
                    }


                },
                error: function (result) {
                }
            });

        }

        function BindAttendance() {
            $.ajax({

                type: 'POST',
                url: "Attendance_Sheet.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {},
                success: function (result) {

                    //$.each(JSON.parse(result.d), function (key, value) {

                    //    if (value.Date.startsWith("/Date")) {
                    //        result.List[key].StartDate = new Date(parseInt(value.StartDate.substr(6)))
                    //    }
                    //});
                    $("#divGrid").dxDataGrid({

                        dataSource: JSON.parse(result.d),
                        columnsAutoWidth: true,
                        allowColumnResizing: true,
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
                             {
                                 dataField: "AttendanceID",
                                 caption: "AttendanceID",
                                 width: 20,
                                 visible: false
                             },
                            {
                                dataField: "CompanyName",
                                caption: 'CompanyName',
                                width: 160,
                            },
                             {
                                 dataField: "EmployeeCode",
                                 caption: 'EmployeeID',
                                 width: 110,
                             },

                            {
                                dataField: "EmployeeName",
                                caption: 'EmployeeName',


                            },

                            {
                                dataField: "Date",
                                caption: 'Date',
                                format: 'yyyy-MM-dd',
                                dataType: "date",
                                width: 100,
                            },

                             {
                                 dataField: "InTime",
                                 caption: "InTime",
                                 width: 200,
                             },
                             {
                                 dataField: "OutTime",
                                 caption: "OutTime",
                                 width: 200,
                             },
                               {
                                   dataField: "TotalHours",
                                   caption: "Hours",
                                   width: 70,
                               },

                             {
                                 dataField: "OT",
                                 caption: "OverTime",
                                 width: 100,
                                 cellTemplate: function (container, options) {

                                     if (options.values[7] == 1) {

                                         container.append("<span class='fa fa-check' style='color:Blue;'></span>");
                                     }

                                 }
                             },
                             {
                                 dataField: "AttendanceID",
                                 caption: "Action",
                                 width: 80,
                                 cellTemplate: function (container, options) {

                                     container.append("<a href='#' onclick='return editRecord(" + options.value + ")'><i class='fa fa-edit'></i> Edit</a>")
                                 }
                             }



                        ],
                        showBorders: true
                    });

                },
                error: function (result) {

                    console.log(result);
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });

        function binddata() {

            $('#txtFromDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false
            }).datepicker("setDate", 'now');
            $('#txtInTime').timepicker({ defaultTime: '09:00 AM' });
            $('#txtOutTime').timepicker({ defaultTime: '06:00 PM' });
            $('#txtUIN').timepicker();
            $('#txtUOUT').timepicker();
            $("[id$=grdManualAttendance]").prepend($("<thead></thead>").append($("[id$=grdManualAttendance]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=grdManualAttendance]").prepend($("<thead></thead>").append($("[id$=grdManualAttendance]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });

        }
        $(document).on("change", "#ddlStatus", function () {
            if ($(this).val() == "P") {
                $("#txtInTime").attr("validate", "vgroup");
                $("#txtOutTime").attr("validate", "vgroup");
                $(".present-area").show();
            } else {
                $(".present-area").hide();
                $("#txtInTime").val("");
                $("#txtOutTime").val("");
                $("#txtInTime").removeAttr("validate");
                $("#txtOutTime").removeAttr("validate");
            }
        });


        $(document).on("change", "#ddlStatus", function () {
            if ($(this).val() == "P") {
                $("#txtInTime").attr("validate", "vgroup");
                $("#txtOutTime").attr("validate", "vgroup");
                $(".present-area").show();
            } else {
                $(".present-area").hide();
                $("#txtInTime").val("");
                $("#txtOutTime").val("");
                $("#txtInTime").removeAttr("validate");
                $("#txtOutTime").removeAttr("validate");
            }
        });

      

        

        function editRecord(Rid) {
            $("#hdnAttID").val(Rid);
            $.ajax({
                type: 'POST',
                url: "Attendance_Sheet.aspx/OpenUpdateModel",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'id':'"+Rid+"'}",
                success: function (result) {

                    $('#manualattendanceUpdate').modal('show');

                    var a = JSON.parse(result.d);
                    $("#ddlUEmp").find('option[value="' + a.EmployeeID + '"]').prop('selected', true);
                    $("#txtUdate").val(a.InTime.split(' ')[0]);
                    var InTime = a.InTime.split(' ')[1];
                    var InTime_am_or_pm = a.InTime.split(' ')[2];
                    $("#txtUIN").val(InTime + ' ' + InTime_am_or_pm);

                    var OutTime = a.OutTime.split(' ')[1];
                    var OutTime_am_or_pm = a.OutTime.split(' ')[2];
                    $("#txtUOUT").val(OutTime + ' ' + OutTime_am_or_pm);
                    //$("#txtUIN").val(a.InTime.split(' ')[1]);
                    //$('#manualattendanceUpdate').modal('show');
                    //var a = JSON.parse(result.d);
                    //$("#ddlUEmp").find('option[value="' + a.EmployeeID + '"]').prop('selected', true);
                    //$("#txtUdate").val(a.InTime.split(' ')[0]);
                    //$("#txtUIN").val(a.InTime.split(' ')[1]);
                    //$("#txtUOUT").val(a.OutTime.split(' ')[1]);
                    if (a.OT == "1") {
                        $("#chkUOvertime").get(0).checked = true;
                    } else {
                        $("#chkUOvertime").get(0).checked = false;
                    }
                    $("#hdnAttID").val(a.AttendanceID);
                },
                error: function (result,a,b) {
                    console.log(result)
                    console.log(a)
                    console.log(b)
                }
            });

            return true;

        }



    </script>
</asp:Content>

