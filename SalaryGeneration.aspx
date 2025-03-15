<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="SalaryGeneration.aspx.cs" Inherits="SalaryGeneration" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="CntSalaryGenerator" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table-responsive {
            overflow-x: auto !important;
        }
    </style>
    <section class="content-header">
        <h1>Salary Generation
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Salary Generation</li>
        </ol>
    </section>

    <section class="content">
        <div class="row" id="divCompany" runat="server">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 130px;">
                                    <label>Company :</label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" Enabled="false" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" Width="100%" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities"
                                        EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px;">
                                    <label>Salary Generate Month :</label></td>
                                <td>
                                    <asp:DropDownList ID="DDownMonths" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" Width="100%" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Please Select Month ..."></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <asp:Button ID="btnbankwisesalary" runat="server" CssClass=" pull-right btn btn-payroll" Text="Bank Wise Salary Record" OnClick="btnbankwisesalary_Click" />
                        <asp:Button ID="btnSalaryGen" runat="server" CssClass=" pull-right btn btn-payroll" Text="Salary Generate" OnClick="btnSalaryGen_Click" />
<%--                        <asp:Button ID="btnSalary" runat="server" CssClass=" pull-right btn btn-payroll" Text="Salary Generate Demo" OnClick="btnSalary_Click" />--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

         <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Salary Generate</h3>
                        <div class="box-tools">
                            <asp:Button ID="BtnLog" runat="server" CssClass="btn btn-default pull-right" Text="Salary Log" OnClick="BtnLog_Click" CausesValidation="false" Visible="false"/>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdSalaryGen" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdSalaryGen_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompnayName" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Number of Employees">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoEmployees" runat="server" Text='<%#Eval("EmployeeCount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalaryMonth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SalaryMonth","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="5%" />
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/CompanySalaryReport.aspx?CompanyID={0}&month={1}",
                                    HttpUtility.UrlEncode(Eval("CompanyID").ToString()), HttpUtility.UrlEncode(DataBinder.Eval(Container.DataItem,"SalaryMonth","{0:MM/dd/yyyy}"))) %>'
                                                        Text="View Details" />
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
            </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
        </Triggers>
    </asp:UpdatePanel>
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <input type="button" id="btnsummarysheet" Class=" pull-left btn btn-payroll" value="SummarySheet" />
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="employes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 96% !important">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Employee - <small>Salary Log</small></h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GvLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="MonthOfSalaryGen" HeaderText="Salary for Month" />
                                        <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary" />
                                        <asp:BoundField DataField="PF" HeaderText="PF" />
                                        <asp:BoundField DataField="Bonus" HeaderText="Bonus" />
                                        <asp:BoundField DataField="Advance" HeaderText="Advance" />
                                        <asp:BoundField DataField="Loan" HeaderText="Loan" />
                                        <asp:BoundField DataField="EOBI" HeaderText="EOBI" />
                                        <asp:BoundField DataField="Tax" HeaderText="Tax" />
                                        <asp:BoundField DataField="TotalSalary" HeaderText="Total Salary" />
                                        <asp:BoundField DataField="NumberOfEmployee" HeaderText="No of Employee" />
                                        <asp:BoundField DataField="CreateDate" HeaderText="Created Date" DataFormatString="{0:MMMM d, yyyy}" />
                                        <%--<asp:BoundField DataField="NetSalary" HeaderText="Net Salary" />
                                        <asp:BoundField DataField="SalaryMonth" HeaderText="Salary Month" DataFormatString="{0:MMMM d, yyyy}" />
                                        <asp:BoundField DataField="CreatedDate" HeaderText="Generation Date" DataFormatString="{0:MMMM d, yyyy}" />--%>
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Summary Sheet</h3>
                        <div class="box-tools">
                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-default pull-right" Text="Salary Log" OnClick="BtnLog_Click" CausesValidation="false" Visible="false"/>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="devsummarysheet" ></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
        </Triggers>
    </asp:UpdatePanel>

        <div id="OnMonthlyReport">

            <%--    <rsweb:ReportViewer ID="RptViewDetail" runat="server"></rsweb:ReportViewer>--%>
        </div>
    </section>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/jquery.plugin.js"></script>
    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script src="js/bootstrap-typeahead.js"></script>
     <script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=ddlCompany.ClientID%>").select2({

                placeholder: "Select Item",

                allowClear: true
            })

        });
        $("#btnsummarysheet").click(function () {
            var Month = $("#DDownMonths").val();
            $.ajax({
                //type: 'POST',
                //url: "BankWiseSalaryRecord.aspx/Load",
                //contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                //data: "{'year':'" + year + "','bankname':'" + bankname + "','month':'" + month+ "'}",
                type: 'POST',
                url: "SalaryGeneration.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'Month':'" + Month + "'}",
                success: function (result) {
                    if (result.d == "Empty") {
                        alert("Please select month");
                    }
                    else if (result.d == "Empty1")
                    {
                        
                        alert("No record found");
                    }
                    else
                    {
                        $("#devsummarysheet").dxDataGrid({
                            dataSource: JSON.parse(result.d),
                            columnsAutoWidth: true,
                            paging: {
                                pageSize: 10
                            },
                            filterRow: {
                                visible: true,
                                applyFilter: "auto"
                            },
                            searchPanel: {
                                visible: true,
                                width: 240,
                                placeholder: "Search..."
                            },
                            headerFilter: {
                                visible: true
                            },
                            "export": {
                                enabled: true,
                                fileName: "SummarySheet",
                                allowExportSelectedData: false
                            },

                            searchPanel: { visible: true },
                            summary: {
                                totalItems: [
                                    
                                {
                                    column: "Basicsalary",
                                    summaryType: "sum",

                                },
                                {
                                    column: "ActualSalary",
                        summaryType: "sum",

                        },
                                    {
                                        column: "FoodAllowance",
                                        summaryType: "sum",

                                    },
                                        {
                                            column: "TransportAllowance",
                                            summaryType: "sum",

                                        },
                                        {
                                            column: "MedicalAllowance",
                                            summaryType:"sum",
                                        },
                                         {
                                             column: "OverTime",
                                             summaryType: "sum",

                                         },
                                            {
                                                column: "Bonus",
                                                summaryType: "sum",

                                            },
                                                {
                                                    column: "GrossSalary",
                                                    summaryType: "sum",

                                                },
                                                    {
                                                        column: "Tax",
                                                        summaryType: "sum",

                                                    },
                                                                {
                                                                    column: "Loan",
                                                                    summaryType: "sum",

                                                                },
                                              {
                                                  column: "Netsalary",
                                                  summaryType: "sum",

                                              },

                                ],

                            },
                            columns: [{
                                dataField:  "SNo",
                                width: 60
                            },
                            {
                                dataField: "Department",
                                minWidth: 200
                            },
                            {
                                dataField: "Basicsalary",
                                minWidth: 50
                    },
                     {
                         dataField: "ActualSalary",
                         minWidth: 50
                     },
                             {
                                 dataField: "FoodAllowance",
                                 minWidth: 50
                             },
                              {
                                  dataField: "TransportAllowance",
                                  minWidth: 50
                              },
                               {
                                   dataField: "MedicalAllowance",
                                   minWidth: 50
                               },
                                {
                                    dataField: "OverTime",
                                    minWidth: 50
                                },
                                  {
                                      dataField: "Bonus",
                                      minWidth: 50
                                  },
                                    {
                                        dataField: "GrossSalary",
                                        minWidth: 50
                                    },
                                     {
                                         dataField: "Tax",
                                         minWidth: 50
                                     },
                                      {
                                          dataField: "Loan",
                                          minWidth: 50
                                      },
                                        {
                                            dataField: "Netsalary",
                                            minWidth: 100
                                        
                            }],
                        //    columns: [
                               
                        //         "Department",
                        //         "Basicsalary",
                        //        "FoodAllowance",
                        //        "TransportAllowance",
                        //        "OverTime",
                        //        "Bonus",
                        //        "GrossSalary",
                        //        "Tax",
                        //        "Loan",
                        //        "Netsalary"
                        //    ],

                        //    showBorders: true
                        });

                    }
                 
                },
                error: function (result) {
                }
            });

        });
         $(document).ready(function () {

            $("#<%=DDownMonths.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

        });

   </script>
  

</asp:Content>
