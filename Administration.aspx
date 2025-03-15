<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Administration.aspx.cs" Inherits="Administration" EnableEventValidation="false" %>

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
        <h1>Administration</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Administration</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Administration</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div id="divButtons" class="box-body">
                        <div class="row">
                            <div class="col-md-12 cstm-btns" id="mybtn">
                                <asp:Button ID="Companybtn" runat="server" Text="Company" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="Companybtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Branchbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Branch" OnClick="Branchbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Departmentbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Department" OnClick="Departmentbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Categorybtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Category" OnClick="Categorybtn_Click" Visible="false"></asp:Button>
                                <%--<asp:Button ID="PFbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="PF" OnClick="PFbtn_Click" Visible="false"></asp:Button>--%>
                                <asp:Button ID="Otherbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Other" OnClick="Otherbtn_Click" Visible="false"></asp:Button>
                                
                       
                                <asp:Button ID="Bankbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Bank Information" OnClick="Bankbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Employeebtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Employees" OnClick="Employeebtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Designation" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Designation" OnClick="Designation_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Salarybtn" runat="server" Visible="false" class="btn btn-primary btn-lg btn-block admin_btn" Text="Salary Head" OnClick="Salarybtn_Click"></asp:Button>
                                <asp:Button ID="TaxMasterBtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Tax" OnClick="TaxMasterbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Holidaybtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Holiday" OnClick="Holidaybtn_Click" Visible="false"></asp:Button>

<%--                                <asp:Button ID="BtnEmployeeTaxAdjustment" runat="server" Visible="false" class="btn btn-primary btn-lg btn-block admin_btn" Text="Employee Tax Adjustments" OnClick="BtnEmployeeTaxAdjustment_Click" Visible="false"></asp:Button>--%>
                                <asp:Button ID="PTbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="PT" OnClick="PTbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Devicebtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Device" OnClick="Devicebtn_Click" Visible="false"></asp:Button>
                        
                         
                                <asp:Button ID="Loanbtn" runat="server" type="button" class="btn btn-primary btn-lg btn-block admin_btn" Text="Loan Category" OnClick="Loanbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Shiftbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Shift" OnClick="Shiftbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Leavebtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Leave" OnClick="Leavebtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="EmployeeTypebtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Type Of Employee" OnClick="EmployeeTypebtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="Btn_CoreManagement" runat="server"  class="btn btn-primary btn-lg btn-block admin_btn" Text="Core Management" OnClick="Btn_CoreManagement_Click" Visible="TRue" style="display:none" ></asp:Button>
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Calender" OnClick="Canlenderbtn_Click" Visible="true"></asp:Button>

                                <asp:Button ID="EOBIbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="EOBI" OnClick="EOBIbtn_Click" Visible="false"></asp:Button>
                                <asp:Button ID="DatabaseDevbtn" runat="server" class="btn btn-primary btn-lg btn-block admin_btn" Text="Database Device" OnClick="DatabaseDevbtn_Click" Visible="false"></asp:Button>
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
                    <div style="padding-left:43px; border:solid black 2px; background-color:darkblue; color:white;">A Div Branch</div>
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