<%@ Page Title="" Language="C#" MasterPageFile="NewMain.master" AutoEventWireup="true" CodeFile="LoanEntry.aspx.cs" Inherits="LoanEntry" %>

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
        <h1>Loan Entry </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="InputModules.aspx"><i class="fa fa-laptop"></i>Input Modules</a></li>
            <li class="active">Loan Entry</li>

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
                        <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-payroll" Text="Add New" OnClick="btnAddnew_Click" />



                        <%} %>


                        <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="loanentry" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Loan Entry Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-6">
                                        <table>
                                            <tr id="trCompany" runat="server" visible="false">
                                                <td style="width: 180px;">
                                                    <label>Company :</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' require='Please select company' customFn='var r = parseInt(this.value); return r > 0;' AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:EntityDataSource ID="EDS_Comp" runat="server"
                                                        ConnectionString="name=vt_EMSEntities"
                                                        DefaultContainerName="vt_EMSEntities"
                                                        EntitySetName="vt_tbl_Company">
                                                    </asp:EntityDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Employee :</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="form-control" validate='vgroup' require="Please Select" runat="server">
                                                        <asp:ListItem Value="0" Text="Please Select Employee"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Loan Type :</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLoanType" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" Width="100%" validate='vgroup' require="Please select loan type">
                                                        <asp:ListItem Value="" Text="Please Select Loan Type..."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <label>Description :</label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 130px;">
                                                    <label>Start Deduction From :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Loan Amount :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control auto-numeric" validate="vgroup" require="please enter loan amount"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Number of Months :</label></td>
                                                <td>
                                                    <div class="pull-left leavapp-from">
                                                        <asp:TextBox ID="txtMonths" runat="server" CssClass="form-control only-number" validate="vgroup" require="please enter Months"></asp:TextBox>
                                                    </div>
                                                    <div class="pull-left leavapp-fromcheckbox" style="display: none">
                                                        <label>No EMI's </label>
                                                    </div>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divloan" class="pull-left leavapp-from" visible="false" runat="server">

                                                        <label>Loan Not Deduction?</label>
                                                        <asp:CheckBox ID="chknloanotdeduction" runat="server" OnCheckedChanged="chknloanotdeduction_CheckedChanged" validate='vgroup' require="Please Select" AutoPostBack="true" />

                                                    </div>


                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnotdedctiondate" runat="server" CssClass="form-control" validate='vgroup' autocomplete="off" ClientIDMode="Static" require='Please select from date' Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr style="display: none">
                                                <td>
                                                    <label>EMI Type :</label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEMIType" AppendDataBoundItems="true" runat="server" CssClass="form-control" custom="Please Select EMIType" customFn='var r = parseInt(this.value); return r > 0;'>
                                                        <%--validate='vgroup'--%>
                                                        <asp:ListItem Value="0">Please Select EMI Type</asp:ListItem>
                                                        <asp:ListItem Value="1">Monthly</asp:ListItem>
                                                        <asp:ListItem Value="2">Amount</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="display: none">
                                                <td>
                                                    <label>EMI's :</label></td>
                                                <td>
                                                    <asp:TextBox ID="txtEMI" runat="server" CssClass="form-control" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grdnotdeductionmonth" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdLoanEntry_RowCommand">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" />
                                                                    <asp:BoundField DataField="IsnotDeductionMonth" HeaderText="No deduction months" HeaderStyle-Width="50%" />
                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClick="btnSave_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
                                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" OnClick="btnClose_Click" />

                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>

              <div class="modal fade" id="Delete-Modal" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel2">Loan</h4>
                            </div>
                            <div class="modal-body">                                
                                Are you sure you want to delete this record?
                            </div>
                            <div class="modal-footer">                                
                                <asp:Button ID="btndelete" runat="server" Text="Yes" class="btn btn-payroll" OnClick="btndelete_Click"></asp:Button>                                
                                <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" class="btn btn-payroll" Text="No"></asp:Button>
                                <%--<asp:Button ID="btndelete" ClientIDMode="Static" runat="server" Text="Yes" CommandArgument='<%#Eval("ID")%>' Style="width: 130px;" CssClass="submit action-button" OnCommand="btndelete_Command"></asp:Button>--%>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsave" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
        <!-- /.modal -->
        <div class="row" id="divCompany" runat="server" visible="false">
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
                        <h3 class="box-title">Loan Entry</h3>
                        <asp:UpdatePanel ID="UpPanelLog" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--<asp:Button ID="BtnLog" runat="server" CssClass="btn btn-default pull-right" Text="Loan Entry Log" OnClick="BtnLog_Click" CausesValidation="false" />--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdLoanEntry" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdLoanEntry_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="EntryID" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Name" HeaderText="Loan Type" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="AppliedDate" HeaderText="Date" HeaderStyle-Width="15%" DataFormatString="{0:MM/dd/yyy}" />
                                            <asp:BoundField DataField="AppliedAmount" HeaderText="Loan Amount" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="MonthDuration" HeaderText="Number of Months" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="LineManager" HeaderText="Manager Status" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="HRAdmin" HeaderText="HR Status" HeaderStyle-Width="10%" />

                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Edit" CommandArgument='<%#Eval("EntryID")%>' CommandName="EditCompany"> 
                                                        <i class="fa fa-pencil-square-o"></i> 
                                                    </asp:LinkButton>
                                                    &nbsp;
                                                    <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CssClass="confirm" CommandArgument='<%#Eval("EntryID")%>' CommandName="DeleteCompany">
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
        </div>
    </section>
    <div class="modal fade" id="LoanEntryModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 96% !important">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Loan - <small>Entry Log</small></h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GvLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:MMMM d, yyyy}" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Time" DataFormatString="{0:hh:mm}" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                        <asp:BoundField DataField="Name" HeaderText="Loan Type" />
                                        <asp:BoundField DataField="AppliedDate" HeaderText="Deduction Date" />
                                        <asp:BoundField DataField="AppliedAmount" HeaderText="Amount" />
                                        <asp:BoundField DataField="MonthDuration" HeaderText="Duration" />
                                        <asp:BoundField DataField="LineManagerStatus" HeaderText="Manager Status" />
                                        <asp:BoundField DataField="HRAdminStatus" HeaderText="HR Status" />
                                        <asp:BoundField DataField="Action" HeaderText="Action" />
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
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/moment.min.js"></script>
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
        $("#btnSave_Click").click(function () {
            if ($("#txtnotdedctiondate").val()) {

            }

        })
        function binddata() {

            $('#txtDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1
            });
            $("[id$=GvLog]").prepend($("<thead></thead>").append($("[id$=GvLog]").find("tr:first"))).dataTable();

            $("[id$=grdLoanEntry]").prepend($("<thead></thead>").append($("[id$=grdLoanEntry]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=grdLoanEntry]").prepend($("<thead></thead>").append($("[id$=grdLoanEntry]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });


            $("[id$=GvLog]").prepend($("<thead></thead>").append($("[id$=GvLog]").find("tr:first"))).dataTable();

            $("[id$=grdLoanEntry]").prepend($("<thead></thead>").append($("[id$=grdLoanEntry]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=grdLoanEntry]").prepend($("<thead></thead>").append($("[id$=grdLoanEntry]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });

            $('#txtnotdedctiondate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1,

            });
            //var date = new Date();
            //var year = date.getFullYear(); //get year
            //var month = txtDate; //get month
            //$('#txtnotdedctiondate').datepicker({
            //    format: 'm/dd/yyyy',
            //    autoclose: true,
            //    clearBtn: false,
            //    minViewMode: 1,
            //    startDate: new Date(year, month, '01'), //set it here
            //    endDate: new Date(year + 1, month, '31')

            //});
        }

    </script>
</asp:Content>
