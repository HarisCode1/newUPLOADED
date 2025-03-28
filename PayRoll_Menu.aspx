﻿
<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="PayRoll_Menu.aspx.cs" Inherits="PayRoll_Menu" EnableEventValidation="false" %>

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
            <li class="active"></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                        <h3 class="box-title">PayRollMenu</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div id="divButtons" class="box-body">
                        <div class="row">
                            <div class="col-md-12 payrollmenu-btn" id="mybtn">

                                <asp:Button ID="btnbonus" runat="server" Text="Bonus" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="btnbonus_Click" Visible="false"></asp:Button>
                                <asp:Button ID="btnStaffBonus" runat="server" Text="Staff Bonus" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="btnStaffBonus_Click" Visible="false"></asp:Button>
                                
                                <asp:Button ID="btnpf" runat="server" Text="Providient Fund" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="btnpf_Click" Visible="false" Style="display:none;"></asp:Button>
                                <asp:Button ID="btnincrement" runat="server" Text="Increment" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" Visible="false" ></asp:Button>
                                <asp:Button ID="btngraduity" runat="server" Text="Graduity" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="btngraduity_Click" Visible="false" Style="display:none;"></asp:Button>
                                
                            </div>
                            
                        </div>
                    </div>
                
                    
                    <div class="box-footer clearfix">
                       
                    </div>
                    <!-- /.box-footer .companyHeading-->
                </div>
                <!-- /.box -->
            </div>
        </div>
    </section>
    <!-- /.content -->
</asp:Content>