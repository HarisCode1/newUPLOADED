<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_PromotionNew.aspx.cs" Inherits="Employee_PromotionNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <style>
        .btn-dark{
            color:dimgray!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee</a></li>
            <li class="active">Employee - Promotion</li>
        </ol>
    </section>
    <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDetail" runat="server">
                <div class="container-fluid">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                            <h3 class="box-title">Employee Promotion</h3>
                            <div class="box-tools">
                                <asp:Button ID="BtnLog" runat="server" CssClass="btn btn-default pull-right" Text="Promotoion Log" OnClick="BtnLog_Click" CausesValidation="false" />
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h3 class="text-center"><b>Employee -</b> Promotion</h3>
                                    <hr />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">First Name:</label>
                                    <asp:TextBox ID="TxtFirstName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Last Name:</label>
                                    <asp:TextBox ID="TxtLastName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Email:</label>
                                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">Current Department:</label>
                                    <asp:TextBox ID="TxtCurrentDepartment" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Current Designation:</label>
                                    <asp:TextBox ID="TxtCurrentDesignation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Current Line Manager:</label>
                                    <asp:TextBox ID="TxtCurrentLineManager" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-4">
                                    <label for="email">Current Employment Type:</label>
                                    <asp:TextBox ID="TxtCurrentEmploymentType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Date:</label>
                                    <asp:TextBox ID="TxtDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <hr />

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">Current Basic Salary:</label>
                                    <asp:TextBox ID="TxtCurrentBasicSalary" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Current Food Allowance:</label>
                                    <asp:TextBox ID="TxtCurrentHouseRentAllowance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Current Medical Allowance:</label>
                                    <asp:TextBox ID="TxtCurrentMedicalRentAllowance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">Current Transport Allowance:</label>
                                    <asp:TextBox ID="TxtCurrentTransportAllowance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Current Fuel Allowance:</label>
                                    <asp:TextBox ID="TxtCurrentFuelAllowance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Current Special Allowance:</label>
                                    <asp:TextBox ID="TxtCurrentSpecialAllowance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">Current PF Status:</label>
                                    <asp:TextBox ID="TxtCurrentPFStatus" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Tax :</label>
                                    <asp:TextBox ID="txtoldtax" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email"><%--Current PF Type:--%></label>
                                    <asp:TextBox ID="TxtCurrentPFType" runat="server" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>
                                </div>
                            </div>

                            <h2>Promoted To</h2>
                            <hr />

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">New Department:</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="DdlNewDepartment"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="DdlNewDepartment" runat="server" ClientIDMode="Static" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlNewDepartment_SelectedIndexChanged" AppendDataBoundItems="true">
                                        <asp:ListItem Text="--Select One--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>


                                </div>
                                <div class="col-md-4">
                                    <label for="email">New Designation:</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server" ControlToValidate="DdlNewDesignation"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="0">
                                    </asp:RequiredFieldValidator>
<%--                                    <asp:DropDownList ID="DdlNewDesignation" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlNewDesignation_SelectedIndexChanged"></asp:DropDownList>--%>
                                     <asp:DropDownList ID="DdlNewDesignation" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlNewDesignation_SelectedIndexChanged" AppendDataBoundItems="true">
                                        <asp:ListItem Text="--Select One--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">New Line Manager:</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="DdlNewLineManager"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="DdlNewLineManager" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">New Employement Type:</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        runat="server" ControlToValidate="DdlNewEmploymentType"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="DdlNewEmploymentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <hr />

                            <div class="row">
                                    <div class="col-md-4">
                                        <label for="email">New Basic Salary:</label>
                                        <asp:RequiredFieldValidator ID="rfvcandidate"
                                            runat="server" ControlToValidate="TxtNewBasicSalary"
                                            ErrorMessage="Required" ForeColor="Red"
                                            InitialValue="">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="TxtNewBasicSalary" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                 <div class="col-md-4">

                                      <label for="email">New Tax:</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server" ControlToValidate="txtcurrenttax"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtcurrenttax" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                 
                                <div class="col-md-4">
                                   
                                  

                             
                                    <label for="email">New Food Allowance:</label>
                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server" ControlToValidate="TxtNewHouseRentAllowance"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="TxtNewHouseRentAllowance" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                  <div class="col-md-4">
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="SomeValue" />
                                    <label for="TxtEffectiveDate">New PayScale Effective Date:</label>
                                    <asp:TextBox ID="TxtEffectiveDate" runat="server" CssClass="datepicker" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">New Medical Allowance:</label>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server" ControlToValidate="TxtNewMedicalAllowance"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="TxtNewMedicalAllowance" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                 <div class="col-md-4">
                                    <label for="email">New Transport Allowance:</label>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server" ControlToValidate="TxtNewTransportAllowance"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="TxtNewTransportAllowance" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row">
                               
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <label for="email">New Fuel Allowance:</label>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                        runat="server" ControlToValidate="TxtNewFuelAllowance"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="TxtNewFuelAllowance" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4" style="display:none;">
                                    <label for="email">New Special Allowance:</label>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                        runat="server" ControlToValidate="TxtNewSpecialAllowance"
                                        ErrorMessage="Required" ForeColor="Red"
                                        InitialValue="">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:TextBox ID="TxtNewSpecialAllowance" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                         <%--   <div class="row">
                                <div class="col-md-4">
                                    <label for="email">New PF Status:</label>
                                    <br />
                                    <asp:CheckBox ID="ChkPFStatus" runat="server" Text="PF Applicable" OnCheckedChanged="ChkPFStatus_CheckedChanged" AutoPostBack="true" />
                                </div>--%>
                                <%--<div class="col-md-4">
                                    <label for="email">New PF Type:</label>
                                    <asp:DropDownList ID="DdlPFType" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                </div>--%>
                            <div class="row">
                              
                                <div class="col-md-4">
                                     <label for="fluploadPromotionDocx">Upload File: </label>
                                     <asp:FileUpload runat="server" ID="fluploadPromotionDocx" AllowMultiple="false" />
                                </div>
                            </div>
                                
                            </div>

                            <div class="row" style="margin-top: 15px !important;">
                                <div class="col-md-12">
                                    <span class="pull-right">
                                        <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />
                                        <asp:Button ID="BtnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="false" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="modal fade" id="employes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 96% !important">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Employee - <small>Promotoion Log</small></h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GvLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                           <asp:BoundField DataField="SNO" HeaderText="S.NO"/>
                                        <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" DataFormatString="{0:MMMM d, yyyy}" />
                                        <%--<asp:BoundField DataField="EntryDate" HeaderText="Entry Time" DataFormatString="{0:hh:mm}" />--%>
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Department" HeaderText="Department" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />                                                                              
                                        <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary" />
                                        <asp:BoundField DataField="HouseRentAllownce" HeaderText="Food Allownce" />
                                        <asp:BoundField DataField="TransportAllownce" HeaderText="Transport Allownce" />
                                        <asp:BoundField DataField="MedicalAllowance" HeaderText="Medical Allowance" />
                                        <asp:BoundField DataField="FuelAllowance" HeaderText="Fuel Allowance" />
                                        <asp:BoundField DataField="PfApplicable" HeaderText="Provident Fund" />
                                        <asp:BoundField DataField="SpecialAllowance" HeaderText="Special Allowance" />
                                        <asp:BoundField DataField="effectivedate" HeaderText="Effective Date" DataFormatString="{0:dd/MM/yyyy}" />
                                           <%--<asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:MMMM d, yyyy}" />--%>
                                        <asp:BoundField DataField="Username" HeaderText="Created By" />
                                          <asp:BoundField DataField="Tax" HeaderText="Tax" />
                                        <asp:TemplateField HeaderText="Promotion Docx">
                                            <ItemTemplate>
                                                <a href="<%# Eval("PromotionDocxPath") %>" class='<%# Eval("FileClass") %>' title="Download File" download target="_blank"><i class="fa fa-file"></i></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
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

    <script src="assets/js/jquery.easing.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">

        $(function () {
            binddata();


        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });

        function binddata() {
            $("[id$=GvLog]").prepend($("<thead></thead>").append($("[id$=GvLog]").find("tr:first"))).dataTable();

            var currentDate = new Date();
            var hiddenValue = document.getElementById('<%= HiddenField1.ClientID %>').value;
            var prevoise = new Date(hiddenValue);
            $('#<%= TxtEffectiveDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                clearbtn: false,

                beforeShowDay: function (date) {
                    var day = date.getDay();

                    if (date > currentDate) {
                        return false;
                    }
                    if (date < prevoise) {
                        return false;
                    }

                    if (day === 0 || day === 6) {
                        return false;
                    }
                    return true;
                }
            });

            console.log('success')
        }
    </script>
</asp:Content>
