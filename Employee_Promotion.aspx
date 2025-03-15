<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_Promotion.aspx.cs" Inherits="Employee_Promotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                <section id="msform" class="content cstm-csform">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-info custom_input">
                                <div class="box-header with-border">
                                    <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                                    <h3 class="box-title">Employee Promotion</h3>
                                    <div class="box-tools pull-right">
                                        <asp:Button ID="BtnLog" runat="server" CssClass="btn btn-default pull-right" Text="Promotoion Log" OnClick="BtnLog_Click" />
                                    </div>
                                </div>
                                <!-- /.box-header -->
                                <div id="divButtons" class="box-body NewEmp_boxBody">
                                    <div class="row">
                                        <div class="col-md-12" id="mybtn">
                                            <fieldset>
                                                <div id="divemployee" class="form-horizontal new_emp_form">
                                                    <h2 class="fs-title"><b>Employee</b> - Promotion and Demotion</h2>
                                                    <hr />
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Type</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlType" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="Promotion" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="Demotion"></asp:ListItem>
                                                            </asp:DropDownList>      
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Basic Salary</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TxtBasicSalary" runat="server" CssClass="form-control"></asp:TextBox>     
                                                        </div>

                                                        <label class="col-sm-2 control-label">House Rent Allowance</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TxtHouseRentAllowance" runat="server" CssClass="form-control"></asp:TextBox>       
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Medical Allowance</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TxtMedicalAllowance" runat="server" CssClass="form-control"></asp:TextBox>     
                                                        </div>

                                                        <label class="col-sm-2 control-label">Transport Allowance</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TxtTransportAllowance" runat="server" CssClass="form-control"></asp:TextBox>       
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Fuel Allowance</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TxtFuelAllowance" runat="server" CssClass="form-control"></asp:TextBox>     
                                                        </div>

                                                        <label class="col-sm-2 control-label">Special Allowance</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TxtSpecialAllowance" runat="server" CssClass="form-control"></asp:TextBox>       
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Type of Pf</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>     
                                                        </div>

                                                        <label class="col-sm-2 control-label">Providient Fund</label>
                                                        <div class="col-sm-4">
                                                               
                                                        </div>
                                                    </div>

                                                    <hr />

                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Effective Date</label>
                                                        <div class="col-sm-4">
                                                           <asp:TextBox ID="TxtEffectiveDate" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>     
                                                        </div>
                                                    </div>

                                                </div>
                                                
                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />
                                               
                                            </fieldset>

                                            
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
                          <h4 class="modal-title">Employee - <small>Promotoion Log</small></h4>
                        </div>
                        <div class="modal-body">
                          <asp:GridView ID="GvLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                              <Columns>
                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:MMMM d, yyyy}" />
                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Time" DataFormatString="{0:hh:mm}" />
                                  <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                  <asp:BoundField DataField="PromotionType" HeaderText="Promotion Type" />
                                  <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary" />
                                  <asp:BoundField DataField="HouseRentAllownce" HeaderText="HouseRent Allownce" />
                                  <asp:BoundField DataField="TransportAllownce" HeaderText="Transport Allownce" />
                                  <asp:BoundField DataField="MedicalAllowance" HeaderText="Medical Allowance" />
                                  <asp:BoundField DataField="FuelAllowance" HeaderText="Fuel Allowance" />
                                  <asp:BoundField DataField="ProvidentFund" HeaderText="Provident Fund" />
                                  <asp:BoundField DataField="SpecialAllowance" HeaderText="Special Allowance" />
                                  <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:MMMM d, yyyy}"/>
                                  <asp:BoundField DataField="Username" HeaderText="Created By" />
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

            $('#TxtEffectiveDate').datepicker({
                dateFormat: 'MM/dd/yyyy',
                autoclose: true,
                clearBtn: false,


            });
        }
    </script>
</asp:Content>

