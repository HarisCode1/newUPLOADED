<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true"  EnableViewState="True" CodeFile="TerminatedEmployee.aspx.cs" Inherits="TerminatedEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="content-header">
        <h1>List Of Terminated Employee </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="TerminatedEmployee.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Terminated Employee</li>
        </ol>
    </section>
     <section class="content">        
            <div id="companygrid" runat="server" class="row">  </div>


                <div class="row">
                     <div class="col-md-12">
                         <div class="box box-info custom_input">
                             <div class="box-header with-border">
                                 <i class="fa fa-table custom_header_icon admin_icon"></i>
                                 <h3 class="box-title">Terminated Employees</h3>
                             </div>
                             <div class="box-body">
                                 <div class="table-responsive">
                                     <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                         <ContentTemplate>
                                             <div id="divGrid"></div>
                                         </ContentTemplate>
                                     </asp:UpdatePanel>
                                 </div>
                             </div>
                         </div>
                     </div>
                  </div>


                  <%--  <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="Updateterminatedemp" runat="server" UpdateMode="Conditional" EnableViewState="True">
                                <ContentTemplate>
                                    <asp:GridView 
                                        ID="grdtermemp" 
                                        runat="server" 
                                        CssClass="table table-bordered table-hover" 
                                        AutoGenerateColumns="False" 
                                        AllowPaging="True" 
                                        PageSize="10" 
                                        OnPageIndexChanging="grdtermemp_PageIndexChanging1">

                                        <Columns>
                                            <asp:BoundField DataField="EnrollId" HeaderText="EnrollId" Visible="false" HeaderStyle-Width="10%" />
                                          <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeCode" HeaderStyle-Width="12%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" HeaderStyle-Width="10%" />                                            
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Documents" visible="false" HeaderText="Documents" HeaderStyle-Width="20%" />                                            
                                            <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="joiningDate" HeaderText="joiningDate" HeaderStyle-Width="15%" />                                            
                                            <asp:BoundField DataField="TerminatedDate" HeaderText="TerminatedDate" HeaderStyle-Width="40%" />
                                            <asp:TemplateField HeaderText="Action"> <HeaderStyle Width="10%"/>
                                            <ItemTemplate>    
                                              <asp:LinkButton ID="lbtndetail" ToolTip="Approve" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("EnrollId")%>' OnCommand="lbtndetail_Command" CausesValidation="false" >
                                                <i class="fa fa-check-circle"></i>
                                                     <a href='<%# Eval("Documents") %>' id="doc" download='<%# Eval("Documents") %>' target="_blank" data-toggle="tooltip" title="Termination Document" runat="server"><i class="fa fa-file"></i></a>
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


                   <%-- testing--%>
     </section>



     <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {
            bindGrid();
        });

        function bindGrid() {
            debugger
            var editOption = 'EditCompany';
            var deleteOption = '';
            $.ajax({
                type: 'POST',
                url: "TerminatedEmployee.aspx/LoadData",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {},
                success: function (result) {

                    $("#divGrid").dxDataGrid({
                        dataSource: JSON.parse(result.d),

                        allowColumnResizing: true,
                        columnAutoWidth: true,
                        showBorders: true,
                        columnChooser: {
                            enabled: true
                        },
                        columnFixing: {
                            enabled: true
                        },
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
                            fileName: "Employees",
                            allowExportSelectedData: false
                        },


                        columns: [

                            
                            "EmployeeID",
                            "EmployeeName",
                            "Department",
                            "Reason",
                            "Status",

                            {
                                dataField: "JoiningDate",
                                dataType: "datetime",

                            },
                            {
                                dataField: "TerminatedDate",
                                dataType: "datetime",
                            },

                            {
                                dataField: "EnrollId", 
                                width: 150,
                                caption: "Action",
                                cellTemplate: function (container, options) {
                                    
                                    var empID = options.value;
                                    var documentUrl = options.data.Documents; 
                                    console.log(empID,"empID")
                           
                                    var html = `
                                    <div style="text-align: left;">
                                        <button class="myLink" onclick="editEmployee(${empID})" title="Edit">
                                            <i class="fa fa-check-circle"></i>
                                        </button>
                                        <a href="${documentUrl}" id="doc" download="${documentUrl}" target="_blank" data-toggle="tooltip" title="Termination Document">
                                            <i class="fa fa-file"></i>
                                        </a>
                                        </div>`;
                                        
                                        
                                          container.append(html);
                                }
                            }

                        ],
                         showBorders: true
                    });
                },
                error: function (result) {
                }
            });
        }
        function editEmployee(empID) {  
            window.location.href = "Employes_Details.aspx?ID=" + empID;
        }


    </script>

</asp:Content>

