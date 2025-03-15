<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="InputModules.aspx.cs" Inherits="InputModules" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Input Modules </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Input Modules</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Input Modules</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div id="divButtons" class="box-body">
                        <div class="row">
                            <div class="col-md-4" id="mybtn">
                                <asp:Button ID="LoanInputbtn" runat="server" Visible="false" Text="Loan Input" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="LoanInputbtn_Click"></asp:Button>
                                <asp:Button ID="LoanApproval" runat="server" Visible="false" Text="Loan Approval" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="LoanApproval_Click"></asp:Button>
                                <%--<button type="button" class="btn-Option btn btn-primary btn-lg btn-block admin_btn" style="display: none"><i class="fa fa-pie-chart admin_btn_icon"></i>Advance Salary</button>--%>

                                <button type="button" data-toggle="modal" data-target="#myModal" class="btn btn-primary btn-lg btn-block admin_btn" style="display: none"><i class="fa fa-user-md admin_btn_icon"></i>Medical</button>
                                <button type="button" class="btn btn-primary btn-lg btn-block admin_btn" style="display: none"><i class="fa fa-th-large admin_btn_icon" style="display: none"></i>Bonus</button>
                            </div>
                            <div class="col-md-4" id="mybtn1">
                                <button type="button" class="btn btn-primary btn-lg btn-block admin_btn" style="display: none"><i class="fa fa-info admin_btn_icon"></i>Appraisal</button>
                                <asp:Button ID="AdvanceSalarybtn" Visible="false" runat="server" Text="Advance Salary" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="AdvanceSalarybtn_Click"  style="display: none"></asp:Button>
                            </div>
                            <div class="col-md-4" id="mybtn2">
                                <asp:Button ID="BtnLoanAdjustment" Visible="false" runat="server" Text="Loan Adjustnment" class="btn-Option btn btn-primary btn-lg btn-block admin_btn face front" OnClick="BtnLoanAdjustment_Click"></asp:Button>
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
                     
                    </div>
                    <!-- /.box-footer .companyHeading-->
                </div>
                <!-- /.box -->
            </div>
        </div>
    </section>
</asp:Content>
