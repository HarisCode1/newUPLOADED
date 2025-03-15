<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="LeaveApplication.aspx.cs" Inherits="LeaveApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Leave Application </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="AttendanceModule.aspx"><i class="fa fa-pie-chart"></i>Attendance</a></li>
            <li class="active">Leave Application</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-payroll" Text="Add New" OnClick="btnAddNew_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="leaveapplication" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Leave Application Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-6">
                                        <%--<div class="col-md-6">
                        <fieldset>
                            <legend>Search</legend>
                        </fieldset>
                        <table>     
                            <tr>  
                                <td style="width:130px;">
                                    <label>Dept :</label></td>
                                <td>
                                    <asp:DropDownList  ID="ddlDepartment" AppendDataBoundItems="true" runat="server"  DataSourceID="EDS_Department" DataTextField="Department" DataValueField="DepartmentID" validate='vgroup' custom='Please Select Department' customFn='var r = parseInt(this.value); return r > 0;' >
                                       <asp:ListItem Value="0" Text="Please Select Department..."></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Department" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities"
                                        EntitySetName="vt_tbl_Department">
                                    </asp:EntityDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Desig :</label>
                                </td>
                                <td>
                                    <asp:DropDownList  ID="ddlDesignation" AppendDataBoundItems="true" runat="server"  DataSourceID="EDS_Designation" DataTextField="Designation" DataValueField="DesignationID" validate='vgroup' custom='Please Select Department' customFn='var r = parseInt(this.value); return r > 0;' >
                                       <asp:ListItem Value="0" Text="Please Select Designation..."></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Designation" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities"
                                        EntitySetName="vt_tbl_Designation">
                                    </asp:EntityDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Search :</label></td>
                                <td>
                                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>  
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered table-hover">
                            <tr>        
                                <th style="width:75px;">Enroll ID</th>
                                <th>Name</th>
                            </tr>
                            <tr>   
                                <td>1</td>
                                <td>Minhaj</td>
                            </tr>
                            <tr>   
                                <td>2</td>
                                <td>Amin Shah</td>
                            </tr>
                        </table>
                    </div>--%>
                                        <fieldset>
                                            <legend>Leave Entry</legend>
                                        </fieldset>
                                        <table>
                                           <%--  <div runat="server" visible="true">--%>
                                                <tr>
                                                <td>
                                                    <label>Employee :</label>
                                                </td>
                                                 <td>
                                                    <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" validate='vgroup'  require='Please Select Employee' AutoPostBack="true" AppendDataBoundItems="True" >
                                                    </asp:DropDownList>
                                                </td>
                                               <%-- <td>
                                                    <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" validate='vgroup' AutoPostBack="True" require='Please Select Employee' OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>--%>
                                            </tr>
                                         <%--  </div>--%>
                                            <tr>
                                                <td>
                                                    <label>
                                                    From :</label> </td>
                                                <td>
                                                    <asp:TextBox ID="txtLeaveFromDate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" require="Please select from date" validate="vgroup"></asp:TextBox>
                                                </td>
                                            <tr>
                                                <td>
                                                    <label>
                                                    To :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtLeaveToDate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" require="Please select from date" validate="vgroup"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Total Leaves :</label></td>
                                                <td>
                                                    <asp:HiddenField ID="hdnTotalLeaves" runat="server" />
                                                    <asp:TextBox ID="txtTotalLeaves" runat="server" ClientIDMode="Static" CssClass="form-control" ReadOnly="true" require="Please Enter Total leaves." validate="vgroup"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="trCompany" runat="server">
                                                <td style="width: 130px;">
                                                    <%--<label>
                                                    Company :</label>--%></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcomp" runat="server" AppendDataBoundItems="true" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control input-sm" custom="Select Company" customFn="var r = parseInt(this.value); return r &gt; 0;" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged" require="Please select company" validate="vgroup">
                                                        <asp:ListItem Text="Please Select Company" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:EntityDataSource ID="EDS_Comp" runat="server" ConnectionString="name=vt_EMSEntities" DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Company">
                                                    </asp:EntityDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Leave Type :</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLeaveType" runat="server" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" require="Please Select LeaveType" validate="vgroup">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Total Leaves :
                                                    </label>
                                                    <asp:Label ID="lblTotalLeave" runat="server" ClientIDMode="Static" Text="0"></asp:Label>
                                                </td>
                                                <td>
                                                    <label>
                                                    Remaining Leaves :
                                                    </label>
                                                    <asp:Label ID="lblRemainingLeaves" runat="server" ClientIDMode="Static" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                    Reason :
                                                    </label>
                                                </td>

                                                <td>
    <asp:TextBox 
        ID="txtReason" 
        runat="server" 
        ClientIDMode="Static" 
        AutoPostBack="true"  
        CssClass="form-control" 
        Height="80px" 
        TextMode="MultiLine" 
        require="Please Enter Total leaves." 
        validate="vgroup">
    </asp:TextBox>
</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" class="btn btn-payroll" OnClick="btnSave_Click" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
                                <asp:Button ID="btnClose" runat="server" class="btn" OnClick="btnClose_Click" data-dismiss="modal" Text="Close"></asp:Button>

                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="row" id="divCompany" runat="server">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 130px;">
                                    <label>Company :</label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities"
                                        EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Leave Application </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdLeaveApplication" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdLeaveApplication_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="LeaveApplicationID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="LeaveName" HeaderText="Leave Type" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyy}" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date"     DataFormatString="{0:dd/MM/yyy}" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="IsApprovedToLM" HeaderText="LM Status" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="IsApprovedToHR" HeaderText="HR Status" HeaderStyle-Width="15%" />

                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("LeaveApplicationId")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("LeaveApplicationId")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%--<asp:EntityDataSource ID="EDS_LeaveApplication" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_LeaveApplication"
                         Where="it.CompanyId = @CompanyId">        
                    </asp:EntityDataSource>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/jquery.plugin.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="js/bootstrap-typeahead.js"></script>
     <script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
            CalculateTotalLeave();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        var CalculateTotalLeave = function () {
            if ($("[id$=txtLeaveFromDate]").val() != "" && $("[id$=txtLeaveToDate]").val() != "") {
                var start = $("[id$=txtLeaveFromDate]").val().split("/");
                var end = $("[id$=txtLeaveToDate]").val().split("/");
                var startDate = new Date(start[2], start[0] - 1, start[1]);
                var endDate = new Date(end[2], end[0] - 1, end[1]);
                var oneDay = 24 * 60 * 60 * 1000;
                var diff = Math.round(Math.abs((endDate.getTime() - startDate.getTime()) / (oneDay))) + 1;
                var start_date_array = $("#txtLeaveFromDate").val()
                var end_date_array = $("#txtLeaveToDate").val()
                console.log(start_date_array, end_date_array,"start end ")
                if (diff > 0) {  
                    $("[id$=txtTotalLeaves]").val(diff);
                    $("[id$=hdnTotalLeaves]").val(diff);
                    console.log(diff,"diff");
                } else {
                    //$("[id$=txtTotalLeaves]").val("");
                    //$("[id$=hdnTotalLeaves]").val("");
                }
            }
        }
        function binddata() {
            $("#txtLeaveFromDate").datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                //startDate: new Date()
            }).on("change", function () {
                var start_date_array = $("#txtLeaveFromDate").val().split("/");
                $('#txtLeaveToDate').val("");
                $('#txtTotalLeaves').val("");
                $("#ddlLeaveType").val("");
               // $("#ddlEmployee").val("");
                $("#lblTotalLeave").html("");
                $("#lblRemainingLeaves").html("");
                $('#txtLeaveToDate').datepicker("destroy");
                if (start_date_array.length > 1) {
                    var dt = new Date(start_date_array[2], start_date_array[0] - 1, start_date_array[1]);
                    $('#txtLeaveToDate').datepicker({
                        format: 'm/dd/yyyy',
                        //startDate: dt,
                        autoclose: true,
                        clearBtn: false
                    }).on("change", function () {
                        CalculateTotalLeave();
                        $("#ddlLeaveType").val("");
                       // $("#ddlEmployee").val("");
                        $("#lblTotalLeave").html("");
                        $("#lblRemainingLeaves").html("");
                    });
                } else {
                    $('#txtLeaveToDate').datepicker({
                        format: 'm/dd/yyyy',
                        autoclose: true,
                        clearBtn: false
                    }).on("change", function () {
                        CalculateTotalLeave();
                        $("#ddlLeaveType").val("");
                       // $("#ddlEmployee").val("");
                        $("#lblTotalLeave").html("");
                        $("#lblRemainingLeaves").html("");
                    });
                }
            });
            if ($("#txtLeaveFromDate").val() != "") {
                var start_date_array = $("#txtLeaveFromDate").val().split("/");
                var dt = new Date(start_date_array[2], start_date_array[0] - 1, start_date_array[1]);
                $('#txtLeaveToDate').datepicker({
                    format: 'm/dd/yyyy',
                    startDate: dt,
                    autoclose: true,
                    clearBtn: false
                }).on("change", function () {
                    CalculateTotalLeave();
                    $("#ddlLeaveType").val("");
                //    $("#ddlEmployee").val("");
                    $("#lblTotalLeave").html("");
                    $("#lblRemainingLeaves").html("");
                });
            }
            else {
                $('#txtLeaveToDate').datepicker({
                    format: 'm/dd/yyyy',
                    autoclose: true,
                    clearBtn: false
                }).on("change", function () {
                    CalculateTotalLeave();
                    $("#ddlLeaveType").val("");
                 //   $("#ddlEmployee").val("");
                    $("#lblTotalLeave").html("");
                    $("#lblRemainingLeaves").html("");
                });
            }
            $("[id$=grdLeaveApplication]").prepend($("<thead></thead>").append($("[id$=grdLeaveApplication]").find("tr:first"))).dataTable();
        }
       
  $(document).ready(function () {

            $("#<%=ddlEmployee.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

  });
         $(document).ready(function () {

            $("#<%=ddlLeaveType.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

      });
    </script>
</asp:Content>
