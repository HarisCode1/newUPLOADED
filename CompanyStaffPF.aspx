<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="CompanyStaffPF.aspx.cs" Inherits="CompanyStaffPF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>STAFF PROVIDENT FUND</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Staff Provident Fund</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <% if (Session["Username"].ToString() == "SuperAdmin")
                            { %>

                        <% }
                            else
                            { %>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass="pull-right btn btn-payroll" Text="Add New" OnClick="BtnAddNew_Click" />
                        <%} %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="CompanyModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:HiddenField ID="hdnId" runat="server" />
                <asp:UpdatePanel ID="upCompanyStaffPF" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Staff Provident Fund Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <table class="all-table all-tables">
                                    <div runat="server" visible="false">
                                        <tr id="trCompany" runat="server">
                                            <td style="width: 130px;">
                                                <%-- <asp:TextBox ID="txtid" runat="server" Visible="true" ></asp:TextBox>--%>
                                                <label>Company :</label></td>
                                            <td>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlcomp">
                                                </asp:DropDownList>
                                                <%-- <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' require='Please select company' customFn='var r = parseInt(this.value); return r > 0;' custom="Select Company" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                                                <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:EntityDataSource ID="EDS_Comp" runat="server"
                                                ConnectionString="name=vt_EMSEntities"
                                                DefaultContainerName="vt_EMSEntities"
                                                EntitySetName="vt_tbl_Company">
                                            </asp:EntityDataSource>--%>
                                            </td>
                                        </tr>
                                    </div>

                                    <%--<tr>
                                        <td style="width: 120px;">
                                            <label > </label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEmployee" Visible="false" ClientIDMode="Static" CssClass="form-control" runat="server" validate='vgroup' AutoPostBack="True" require='Please Select Employee' OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                <asp:ListItem Value=""  Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:EntityDataSource ID="EntityDataSource1" runat="server"
                                                ConnectionString="name=vt_EMSEntities"
                                                DefaultContainerName="vt_EMSEntities"
                                                EntitySetName="vt_tbl_Employee">
                                            </asp:EntityDataSource>
                                        </td>
                                    </tr>--%>
                                    <%--  <tr>
                                        <td>
                                            <label>Nature Of Appointment :</label></td>
                                        <td>
                                            <asp:TextBox ID="txtNatureOfApp" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a Nature of appointment'></asp:TextBox>

                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>

                                            <label class="col-sm-2 control-label">Salary Type:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddSalaryType" ClientIDMode="Static" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="col-sm-2 control-label">Employee Type:</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEmployeeType" ClientIDMode="Static" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <label style="color: red" id="lblddCompany"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="col-sm-2 control-label">Percentage</label>
                                        </td>
                                        <td>

                                            <asp:TextBox ID="txtpercent" CssClass="col-sm-2 is-number-with-decimal" runat="server" validate='vgroup' placeholder="Enter PF Percent" require='Please enter pr percent'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="col-sm-2 control-label">Active</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkActive" runat="server" />
                                        </td>
                                    </tr>
                            </div>
                            <tr>
                                <%-- <td>
                                            <label>Date of joining service :</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDateOfJoiningService" runat="server" CssClass="form-control" ReadOnly="true" validate='vgroup' autocomplete="off" require='Please select Date of joining service' ClientIDMode="Static"></asp:TextBox>
                                        </td>--%>
                            </tr>
                            <tr>
                                <%-- <td>
                                            <label>Date of joining Fund :</label>
                                        </td>
                                        <td>s
                                            <asp:TextBox ID="TxtBxDateofjoiningFund" runat="server" CssClass="form-control" ReadOnly="true" validate='vgroup' autocomplete="off" require='Please select Date of joining Fund' ClientIDMode="Static"></asp:TextBox>
                                        </td>--%>
                            </tr>
                            <tr>
                                <%-- <td>
                                            <label>Salary per Month</label></td>
                                        <td>
                                            <asp:TextBox ID="txtSalaryPerMonth" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>--%>
                            </tr>
                            <br />
                            <tr>
                                <%--<td>
                                            <label>DECLARATION</label></td>
                                        <td>
                                            <asp:TextBox ID="TxtBxDeclareation" TextMode="MultiLine" Rows="9" CssClass="form-control" ReadOnly="true" runat="server" Text='I hereby declare that I have read the Rules of the “Burshan - STAFF PROVIDENT FUND” and that I hereby authorize the Company to make the deductions from my basic monthly salary as prescribed in Rule 7. I further agree to be bound by the said Rules and by any additions to or alterations in them and by any new Rules in connection therewith which may from time to time hereafter be made.'></asp:TextBox>
                                        </td>--%>
                            </tr>
                            <%-- <tr>
                                <td>
                                    <label>Witnesses :</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label>1. Name :</label></td>
                                <td>
                                    <asp:TextBox ID="TxtBxWitnessNameFirst" CssClass="form-control" validate='vgroup' autocomplete="off" require='Please Enter Witness Name' runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>CNIC :</label></td>
                                <td>
                                    <asp:TextBox ID="TxtBxWitnessCNICFirst" CssClass="form-control is-number" validate='vgroup' autocomplete="off" require='Please Enter Witness CNIC' runat="server" MaxLength="13"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="Mask1" runat="server" TargetControlID="TxtBxWitnessCNICFirst" Mask="99999-9999999-9"
                                            MessageValidatorTip="true" 
                                            OnFocusCssClass="MaskedEditFocus" 
                                            OnInvalidCssClass="MaskedEditError"
                                            MaskType="Number" 
                                            InputDirection="RightToLeft"
                                            ErrorTooltipEnabled="True"
                                            ClearMaskOnLostFocus="false"/>
                                </td>
                            </tr>
                            <br />
                            <tr>
                                <td>
                                    <label>2. Name :</label></td>
                                <td>
                                    <asp:TextBox ID="TxtBxWitnessNameSecond" CssClass="form-control " validate='vgroup' autocomplete="off" require='Please Enter Witness Name' runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>CNIC :</label></td>
                                <td>
                                    <asp:TextBox ID="TxtBxWitnessCNICSecond" CssClass="form-control is-number" validate='vgroup' autocomplete="off" require='Please Enter Witness CNIC' runat="server" MaxLength="13"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="Mask2" runat="server" TargetControlID="TxtBxWitnessCNICSecond" Mask="99999-9999999-9" 
                                            ClearMaskOnLostFocus="false" />
                                </td>
                            </tr>--%>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" OnClick="btnClose_Click" />

                        </div>
                        </div>
                        <!-- /.modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal New -->
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Company Staff PF</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <%--OnPageIndexChanging="GridUser_PageIndexChanging" OnRowDataBound="GridUser_RowDataBound"--%>
                                        <asp:GridView ID="GridEmployee" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridDataTable table table-striped table-bordered dataTable " AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="StaffPFID" HeaderText="StaffPFID" SortExpression="StaffPFID" />
                                                <%--  <asp:BoundField DataField="Employee" HeaderText="Employee" SortExpression="Employee" />
                                         <asp:BoundField DataField="SalaryType" HeaderText="SalaryType" SortExpression="SalaryType" />
                                        <asp:BoundField DataField="EmployeeType" HeaderText="EmployeeType" SortExpression="EmployeeType" />--%>
                                                <asp:BoundField DataField="Percentage" HeaderText="Percentage" SortExpression="Percentage" />
                                                <asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <HeaderStyle Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("StaffPFID")%>' OnCommand="lbtnEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="confirm" CommandArgument='<%#Eval("StaffPFID")%>' OnCommand="lbtnDelete_Modalshow">
                             <i class="fa fa-times-circle"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="action-icon" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="deleteform" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpView1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <section id="msform" class="content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="box box-info custom_input">
                                            <div class="box-header with-border">
                                                <%-- <i class="fa fa-user-plus custom_header_icon admin_icon"></i>--%>
                                                <h3 class="box-title" id="h1" runat="server">Delete </h3>
                                                <div class="box-tools pull-right">
                                                    <%-- <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
                                                    <button class="btn btn-box-tool" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                                </div>
                                            </div>
                                            <!-- /.box-header -->
                                            <div id="divButtons" class="box-body NewEmp_boxBody">
                                                <div class="row">
                                                    <div class="col-md-12" id="mybtn">

                                                        <fieldset>
                                                            <label>Are you sure you want to delete this record?</label>
                                                            <div class="form-group">
                                                                <asp:TextBox ID="TxtIDs" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" Text="Cancel" Style="width: 130px;" CssClass="submit action-button"></asp:Button>
                                                                <asp:Button ID="btndelete" ClientIDMode="Static" runat="server" Text="Delete" CommandArgument='<%#Eval("UserID")%>' Style="width: 130px;" CssClass="submit action-button" OnCommand="lbtnDelete_Command"></asp:Button>
                                                            </div>

                                                        </fieldset>
                                                        <%-- End Account Setup--%>

                                                        <fieldset>
                                                    </div>

                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="">
                                    </div>

                                </div>

                            </section>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->

        </div>
        <!-- /.modal New End -->
        <%-- <div id="companygrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Staff Provident Fund</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdCompanys" runat="server" DataSourceID="EDS_Company" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdCompany_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="CompanyID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="CompanyShortName" HeaderText="Company Short Name" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Phone" HeaderText="Phone" HeaderStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("CompanyID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("CompanyID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="grdCompany" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" ClientIDMode="Static" ShowFooter="true" OnRowCommand="grdCompany_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMultiGridCompanyName" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Salary">
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblMonthlySalary" runat="server" Text='<%#Eval("MonthlySalary")%>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Joining Date">
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group bootstrap-timepicker">
                                                            <asp:Label ID="lblJoiningDate" runat="server" Text='<%#Eval("JoiningDate")%>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fund joining Date">
                                                <ItemTemplate>
                                                    <div class="controls">
                                                        <div class="input-group">
                                                            <asp:Label ID="lblFundjoiningDate" runat="server" Text='<%#Eval("FundjoiningDate")%>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("StaffPFID")%>' CommandName="EditCompanyStaffPF">
                                                         <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("StaffPFID")%>' CommandName="DeleteCompanyStaffPF" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                                         <i class="fa fa-times-circle"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </section>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/jquery.plugin.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">

        <%--$(document).ready(function () {

            $("#<%=ddlcomp.ClientID%>").select2({

                placeholder: "Select Item",

                allowClear: true

            });

        });
            $(document).ready(function () {
                
                $("#<%=ddSalaryType.ClientID%>").select2({

                    placeholder: "Select Item",

                    allowClear: true

                });

            });
            $(document).ready(function () {

                $("#<%=ddlEmployeeType.ClientID%>").select2({

                placeholder: "Select Item",

                allowClear: true

            });

        });--%>
        //$(function () {
        //    binddata();
        //});
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        //    binddata();
        //});
        //Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
        //    binddata();
        //});
        //function binddata() {
        //    $("[id$=grdCompany]").prepend($("<thead></thead>").append($("[id$=grdCompany]").find("tr:first"))).dataTable();

        //    //$("#TxtBxDateofjoiningFund").datepicker({
        //    //    format: 'm/dd/yyyy',
        //    //    autoclose: true,
        //    //    clearBtn: false
        //    //});
        //}

        function ClearFields() {
            $("[id $=hdnId]").val("");
            //$("[id $=ddlcomp]").val("0");
            $("[id $=ddlEmployee]").val("0");
            $("[id $=ddSalaryType]").val("0");
            $("[id $=ddlEmployeeType]").val("0");
            $("[id $=txtpercent]").val("");
            $("[id $=TxtBxWitnessNameFirst]").val("");
            $("[id $=TxtBxWitnessCNICFirst]").val("");
            $("[id $=TxtBxWitnessNameSecond]").val("");
            $("[id $=TxtBxWitnessCNICSecond]").val("");
            $("[id $=chkActive]").prop('checked', false);
            $("[id $=btnSave]").prop('value', 'Save');

            //$('h3').text('Add New User');
            //$('#title').text('Add New User');
        }

    </script>
</asp:Content>
