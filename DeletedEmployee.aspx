<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NewMain.master" CodeFile="DeletedEmployee.aspx.cs" Inherits="DeletedEmployee" %>


<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />

    <style>
        .dropdown-menu {
            position: relative !important;
             overflow-x: hidden;     }
      
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="Can_View" runat="server"    Value ="false" />
    <asp:HiddenField ID="Can_Insert" runat="server" Value="false" />
    <asp:HiddenField ID="Can_Update" runat="server" Value="false" />
    <asp:HiddenField ID="Can_Delete" runat="server" Value="false" />

    <%
        string PageUrl = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        DataTable Dt = Session["PagePermissions"] as DataTable;

        if (Dt != null)
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                if (PageUrl == Dt.Rows[i]["PageUrl"].ToString())
                {
                    if (Convert.ToBoolean(Dt.Rows[i]["Can_View"].ToString()) == true)
                    {
                        Can_View.Value = "true";
                    }
                    if (Convert.ToBoolean(Dt.Rows[i]["Can_Insert"].ToString()) == false)
                    {
                        Can_Insert.Value = "true";
                    }
                    if (Convert.ToBoolean(Dt.Rows[i]["Can_Update"].ToString()) == false)
                    {
                        Can_Update.Value = "true";
                    }
                    if (Convert.ToBoolean(Dt.Rows[i]["Can_Delete"].ToString()) == false)
                    {
                        Can_Delete.Value = "true";
                    }
                }
            }
        }
    %>
   

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Deleted Employes</h3>
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
    </section>
   
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        })

        function binddata() {
            console.log("binddata")
        }
        var can_Insert = document.getElementById('<%= Can_Insert.ClientID%>').value;
        var can_View = document.getElementById('<%= Can_View.ClientID%>').value;
        var can_Update = document.getElementById('<%= Can_Update.ClientID%>').value;
        var can_Delete = document.getElementById('<%= Can_Delete.ClientID%>').value;

        $(document).ready(function () {
            bindGrid();
            console.log("bindgrid")
        });

        function bindGrid() {
            debugger
            var editOption = 'EditCompany';
            var deleteOption = '';
            $.ajax({
                type: 'POST',
                url: "DeletedEmployee.aspx/Load",
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
                         
                            "EmployeeName",                        
                            "Email",
                             "Department",
                             "Designation",
                             "EmergencyContactNo",
                             "MobileNo",
                             {
                                 dataField:"JoiningDate",
                                 dataType: "datetime",
                            },
                            {
                                dataField: "TransferDate",
                                dataType: "datetime",
                            },
                            {
                                dataField: "DeletedDate",
                                dataType: "datetime",
                               
                            },
                           
                        ],
                        showBorders: true
                    });
                },
                error: function (result) {
                }
            });
        }
        function checkright(ctrl)
        {
            var Id =$(ctrl).data("empid");
            $.ajax({
                type:'POST',
                url: "Employee.aspx/CheckRights",
                contentType: 'application/json; charset=utf-8',
                datatype:'json',
                data:"{'data':'"+Id+"'}",
                success:function(result)
                {
                    console.log("Success")
                    if (result.d) {
                        window.location = "Employes_Edit.aspx?ID=" + $(ctrl).data("empid");
    
                    }
                    else
                    {
                        alert("!Sorry You don't have  Rights");
                        console.log(result.d)
                    }
                    
                },
                error: function (s,a,d) {
                    console.log("Error")
                    console.log(s)
                    console.log(a)
                    console.log(d)
                }
            });
        }
       
    </script>
    <script type="text/javascript">
    </script>
    <!--SALMAN CODE-->
</asp:Content>