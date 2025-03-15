<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AttendanceModule.aspx.cs" Inherits="AttendanceModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>PayRoll | Admin</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <!-- Custom CSS -->
    <link rel="stylesheet" href="assets/css/custom.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="assets/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="assets/ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="assets/css/main.css" />
    <!--Animate CSS -->
    <link rel="stylesheet" href="assets/css/animate.css" />
    <!-- Checkboxes CSS -->
    <link rel="stylesheet" href="assets/css/build.css" />
    <!-- DataTable Style CSS -->
    <link rel="stylesheet" href="assets/css/dataTables.bootstrap.min.css" />
    <!--Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="assets/css/skins/_all-skins.min.css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Content Wrapper. Contains page content -->
    <section class="content-header">
        <h1>Attendance </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Attendance</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Attendance</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                  <div id="divButtons" class="box-body">
                        <div class="row">
                            <div class="col-md-4 AttendanceModule" id="mybtn">
                                <asp:Button ID="LeaveYearbtn" runat="server" Text="Create Leave Year" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="LeaveYearbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="LeaveAllotmentbtn" Visible="false" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Leave Allotment" OnClick="LeaveAllotmentbtn_Click"></asp:Button>
                                <asp:Button ID="EmpLeaveAproval" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Leave Approval" OnClick="EmpLeaveAproval_Click" Visible="false"></asp:Button>
                                <asp:Button ID="LeavingApplicationbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Leave Application" OnClick="LeavingApplicationbtn_Click" Visible="false"></asp:Button>
                            </div>
                            <div class="col-md-4 AttendanceModule" id="mybtn1">
                                <asp:Button ID="COFFAppbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Coff Application" OnClick="COFFAppbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="ManualAttendancebtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Manual Attendance" OnClick="ManualAttendancebtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="GatePassEntrybtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="GatePass Entry" OnClick="GatePassEntrybtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="MissingLogEntrybtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Missing Log Entry" OnClick="MissingLogEntrybtn_Click" Visible="false"></asp:Button>
                            </div>
                            <div class="col-md-4 AttendanceModule" id="mybtn2">
                                 <asp:Button ID="BtnEmployeeAttendance" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Bulk Attendance" OnClick="BtnEmployeeAttendance_Click"></asp:Button>
                                <asp:Button ID="BranchWAttbtn" runat="server" type="button" class="btn btn-primary btn-lg btn-block admin_btn" Text="BranchWise Attendance" OnClick="BranchWAttbtn_Click" Visible="false"></asp:Button>
                                <%--<asp:Button ID="ProcessLogbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Process Logs" OnClick="ProcessLogbtn_Click" ></asp:Button>--%>
                                <asp:Button ID="EmployeeAttendancebtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Employee Attendance" OnClick="EmployeeAttendancebtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="DeviceUSBbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Device Usb" OnClick="DeviceUSBbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="USBbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="USB" OnClick="USBbtn_Click" Visible="false"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div id="divCompany" style="display: none;">
                        <div class="row">
                            <div class="col-md-12">
                                <h3 class="companyHeading"><i class="fa fa-building form_icon"></i>Add Company:</h3>
                                <form class="form-horizontal CompanyForm">
                                    <div class="form-group">
                                        <label for="inputEmail3" class="col-sm-4 control-label">Name of Company</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control" id="inputEmail3" placeholder="NameOfCompany" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputPassword3" class="col-sm-4 control-label">Type of Company</label>
                                        <div class="col-sm-8">
                                            <input type="text" class="form-control" id="inputPassword3" placeholder="TypeOfCompany" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-2 col-sm-10">
                                            <div class="checkbox checkbox-primary company_checkbox">
                                                <input id="checkbox2" class="styled" type="checkbox" />
                                                <label for="checkbox2">
                                                    Remember Me
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-2 col-sm-10">
                                            <button type="submit" class="btn btn-default form_btn">Submit</button>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div id="divBranch" style="display: none;">DIV BRANCH</div>
                    <div class="box-footer clearfix">
                        Footer goes here...
                    </div>
                    <!-- /.box-footer .companyHeading-->
                </div>
                <!-- /.box -->
            </div>
        </div>
    </section>
    <!-- /.content -->
</asp:Content>