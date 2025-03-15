<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_ChangeDesignation.aspx.cs" Inherits="Employee_ChangeDesignation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee</a></li>
            <li class="active">Employee - Transfer</li>
        </ol>
    </section>
    <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDetail" runat="server">
                <section id="msform" class="content cstm-csform">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-info custom_input">
                                <div class="box-header with-border">
                                    <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                                    <h3 class="box-title">Employee Change Designation</h3>
                                    <div class="box-tools pull-right">
                                        <asp:Button ID="BtnLog" runat="server" CssClass="btn btn-default pull-right" Text="Change Designation Log" OnClick="BtnLog_Click" />
                                    </div>
                                </div>
                                <!-- /.box-header -->
                                <div id="divButtons" class="box-body NewEmp_boxBody">
                                    <div class="row">
                                        <div class="col-md-12" id="mybtn">
                                            <!-- multistep form -->
                                            <%--<form id="msform">--%>
                                            <!-- progressbar -->
                                            <ul id="progressbar" style="margin-left: 10%">
                                                <li class="active">Current Information</li>
                                                <%--<li><a href="#home">New Information</a></li>--%>
                                            </ul>
                                            <!-- fieldsets -->
                                            <%-- Start Account Setup--%>
                                            <fieldset>
                                                <div id="divemployee" class="form-horizontal new_emp_form">
                                                    <h2 class="fs-title"><b>Current Information</b></h2>
                                                    <hr />
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">First Name</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" ReadOnly="true" require="Please Enter your First Name" placeholder="Enter First Name"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Last Name</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="TxtLastName" class="form-control" runat="server" ReadOnly="true" require="Please Enter your Last Name" placeholder="Enter Last Name"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Email</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="TxtEmail" class="form-control" runat="server" ReadOnly="true" require="Please Enter your First Name" placeholder="Enter First Name"></asp:TextBox>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Company</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlComapny1" Enabled="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Date</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>

                                                        </div>

                                                        <label class="col-sm-2 control-label">Designation From</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlDesignationFrom" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlDesignation1_SelectedIndexChanged"></asp:DropDownList>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Department</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlDepartment1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlDepartment1_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Designation To</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlDesignation1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlDesignation1_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Line Manager</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlLineManager1" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Type</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlType1" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <%------Department-------%>

                                                    <!-- End -->
                                                </div>

                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />
                                                <%--onclick="if (validate('vgroup')) { return true; } else { return false; }"--%>
                                                <%-- <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>--%>
                                            </fieldset>

                                            <fieldset>

                                                <div id="divgeneraldetails" class="new_emp_form">
                                                    <h2 class="fs-title"><b>New Information</b></h2>
                                                    <hr />


                                                </div>

                                                <%--onclick="if (validate('vgroup')) { return true; } else { return false; }" --%>
                                                <input type="button" name="previous" class="previous action-button" value="Previous" />
                                            </fieldset>
                                            <%-- End Personal Info--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <div class="modal fade" id="employes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg" style="width:96% !important">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                          <button type="button" class="close" data-dismiss="modal">&times;</button>
                          <h4 class="modal-title">Employee - <small>Change Designation Log</small></h4>
                        </div>
                        <div class="modal-body">
                          <asp:GridView ID="GvLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                              <Columns>
                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:MMMM d, yyyy}" />
                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Time" DataFormatString="{0:hh:mm}" />
                                  <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                  <asp:BoundField DataField="Email" HeaderText="Email" />
                                  <asp:BoundField DataField="Date" HeaderText="Date" />
                                  <asp:BoundField DataField="DesignationFrom" HeaderText="Designation From" />
                                  <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                  <asp:BoundField DataField="Department" HeaderText="Department" />
                                  <asp:BoundField DataField="DesignationTo" HeaderText="Designation To" />
                                  <asp:BoundField DataField="EmployeeName" HeaderText="Line Manager" />
                                  <asp:BoundField DataField="Type" HeaderText="Employee Type" />
                                  <%--<asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:MMMM d, yyyy}"/>--%>
                                  <asp:BoundField DataField="UserName" HeaderText="Created By" />
                              </Columns>
                          </asp:GridView>
                        </div>
                        <div class="modal-footer">
                          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $('#txtFromDate').datepicker({
            format: 'm/dd/yyyy',
            autoclose: true,
            clearBtn: false
        }).datepicker("setDate", 'now');
    </script>
</asp:Content>

