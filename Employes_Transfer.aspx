<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employes_Transfer.aspx.cs" Inherits="Employes_Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee - Transfer</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee - Transfer</a></li>
            <li class="active">Employee - Transfer</li>
        </ol>
    </section>
       <asp:UpdatePanel ID="UpDetail" runat="server" >
        <ContentTemplate>
            <asp:Panel ID="pnlDetail" runat="server">
                <section id="msform" class="content cstm-csform">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-info custom_input">
                                <div class="box-header with-border">
                                    <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                                    <h3 class="box-title">Employee Transfer Form</h3>
                                    <%--<div class="box-tools pull-right">
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                    <button class="btn btn-box-tool" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                                </div>--%>
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
                                                            <asp:TextBox ID="TxtEmail" class="form-control" runat="server" require="Please Enter your First Name" placeholder="Enter First Name"></asp:TextBox>
                                                        </div>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="" />


                                                               <label class="col-sm-2 control-label">Transfer Date</label>
                                                              <div class="col-md-4">
                                                                  <asp:TextBox  id="txtEntryDate" runat="server"  autocomplete="off" ClientIDMode="Static" CssClass="form-control datepicker"></asp:TextBox>
                                                              </div>

<%--                                                        <label class="col-sm-2 control-label">Company</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlComapny1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlComapny1_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>--%>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Department</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlDepartment1" runat="server" CssClass="form-control"  AutoPostBack="true" validate='vgroup' require='Please select ' OnSelectedIndexChanged="DdlDepartment1_SelectedIndexChanged"></asp:DropDownList>      
                                                        </div>

                                                        <label class="col-sm-2 control-label">Designation</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlDesignation1" runat="server" CssClass="form-control" AutoPostBack="true"  validate='vgroup' require="Please Select" OnSelectedIndexChanged="DdlDesignation1_SelectedIndexChanged"></asp:DropDownList> 
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Line Manager</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlLineManager1" runat="server" validate='vgroup' require='Please select ' CssClass="form-control"></asp:DropDownList>      
                                                        </div>

                                                        <label class="col-sm-2 control-label">Type</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlType1" runat="server" CssClass="form-control"></asp:DropDownList> 
                                                        </div>
                                                    </div>
                                                   <div class="form-group">
                                                        <label class="col-sm-2 control-label">Employee Code</label>
                                                        <div class="col-sm-4">
                                                                                                  
                                                              <asp:TextBox ID="txtempcode" class="form-control" runat="server" validate='vgroup' require="Please Enter your Employee Code" placeholder="Enter Employee Code" ></asp:TextBox>
                                                        </div>
                                                   
                                                  
                                                    </div>
                                                    
                                                 <div class="form-group">
                                                        <label class="col-sm-2 control-label">Image</label>
                                                        <div class="col-sm-4">
                                                       <asp:HiddenField ID="hdEmpPhotoID" runat="server" />
                                                        <asp:HiddenField ID="hdImageName" runat="server" />
                                                     <asp:FileUpload ID="UploadDocImage" runat="server" />
                                                        </div>
                                                   
                                                    </div>
                                                    <%------Department-------%>

                                                    <!-- End -->
                                                </div>
                                                
                                                <%--<asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />--%>
                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" OnClientClick="if (validate('vgroup')) { return true; } else { return false; }" />
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
            <asp:PostBackTrigger ControlID="BtnSave" />
        </Triggers>
    </asp:UpdatePanel>














        <section class="content-header">
       <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Transfer Record</h3>
                        <div class="box-tools">
                            
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="devemptranfer" ></div>
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
        <//section>



        <script src="assets/js/jquery.easing.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/pikaday/pikaday.js"></script>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
        <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.9.0/dist/js/bootstrap-datepicker.min.js"></script>

        <script type="text/javascript">

      
        function getQueryParam(param) {
            const urlParams = new URLSearchParams(window.location.search);
            return urlParams.get(param);
            initializeDatePicker(); 
        }
   
            function initializeDatePicker() {
                var currentDate = new Date();
                var hiddenValue = document.getElementById('<%= HiddenField1.ClientID %>').value;
    var prevoise = new Date(hiddenValue);

                console.log("HiddenField1 Value:", $('#<%= HiddenField1.ClientID %>').val());

                $('#<%= txtEntryDate.ClientID %>').datepicker('destroy'); // Destroy previous instance
                $('#<%= txtEntryDate.ClientID %>').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    beforeShowDay: function (date) {
                        var day = date.getDay();
                        

                        if (date > currentDate) {
                            
                            return false;
                        }
                        if (date < prevoise) {
                            console.log("Date less than previous date");
                            return false;
                        }

                        if (day === 0 || day === 6) {
                            console.log("Weekend date");
                            return false;
                        }

                      
                        return true;
                    }
                });
            }





  
            $(document).ready(function () {
        
        initializeDatePicker();
        /* setMinDate()*/
       
       
    });

            $(document).ready(function () {
                $('#<%= txtEntryDate.ClientID %>').change(function () {
            console.log("Entry date changed to:", $(this).val());
            initializeDatePicker(); // Re-run the initializeDatePicker function when value changes
        });
    });

            $(document).ready(function () {
                $('#<%= DdlDepartment1.ClientID %>').change(function () {
                    console.log("Dropdown changed! Selected Value:", $(this).val());
                    initializeDatePicker();
                   
                });
            });


    $('#DdlDesignation1').change(function () {
        initializeDatePicker();
    });

    $('#DdlLineManager1').change(function () {
        initializeDatePicker();
    });

 
   
    $(document).ready(function () {
        debugger
        $.ajax({
          
            type: 'POST',
            url: "Employes_Transfer.aspx/Load",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (result) {
                if (result.d == "Empty") {
                    //alert("Please select month");
                }
                else if (result.d == "Empty1") {

                    // alert("No record found");
                }
                else {
                    $("#devemptranfer").dxDataGrid({
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
                            fileName: "TranferEmployeeRecord",
                            allowExportSelectedData: false
                        },

                        searchPanel: { visible: true },
                        columns: [{
                            dataField: "EmployeeCode",
                            width: 150
                        },
                        {
                            dataField: "EmployeeName",
                            width: 150
                        },
                    {
                        dataField: "Department",
                        minWidth: 150
                    },
                    {
                        dataField: "Designation",
                        minWidth: 150
                    },
                    {
                        dataField: "LineManager",
                        minWidth: 150
                    },
                    //{
                    //    dataField: "ReportTo",
                    //    minWidth: 150
                    //            },
                                        {
                                            dataField: "TransferDate",
                                         
                                            minWidth: 150
                                        },
             //{
             //    dataField: "Email",
             //    minWidth: 150
             //},
             // {
             //     dataField: "CompanyName",
             //     minWidth: 150
             // },
                                    
                          {
                              dataField: "Image",
                              caption: "Image",
                    minWidth: 150,
                    cellTemplate: function (container, options) {
                        if (options.value == "")
                            container.append("<button type='button' class='btn btn-icon' title='No Image Available'  disabled id='doc'><i class='fa fa-file' ></i></button>");
                        else
                            container.append("<a href='" + options.value + "' id='doc' download='" + options.value + "' target='_blank'><i class='fa fa-file'></i></a>");
                        //$("<a href='" + options.value + "'  id='doc' download='" + options.value + "'  title='Transfer Document' <'/a>").appendTo(container);
                        //container.append(" <a href='"+options.Ima+"' <a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu'><li><a href='#'  id='aid' data-empid='" + options.value + "' onclick='checkright(this)'>Edit</a></li><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><a OnClick='TerminateEmlpoyee(" + options.value + ")'>Terminate</a></li><li><a href='Employes_Transfer.aspx?ID=" + options.value + "'>Transfer</a></li><li><a href='Employes_Increment.aspx?ID=" + options.value + "'>Apply Increment</a></li><li><a href='Employee_PromotionNew.aspx?ID=" + options.value + "'>Promotion</a></li><li><a href='WhMonitor.aspx?ID=" + options.value + "'>");

                    }
                              


                     }],
                    });

                }

            },
            error: function (result) {
            }
        });
    })
    $("#btnsummarysheet").click(function () {
        var Month = $("#DDownMonths").val();


    });



        </script>

</asp:Content>