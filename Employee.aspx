<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Employee" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <%-- <link href="css/jquery.timeentry.css" rel="stylesheet" />--%>
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />


    

    <style>
        .dropdown-menu {
            position: relative !important;
            width: 100%; /* Ensures the grid takes full width */
            overflow-x: auto; /* Horizontal scroll enabled */
            overflow-y: hidden;
        }


            .table-responsive {
            width: 100%;
            overflow-y: auto;
  overflow-x: hidden;
            }

        

      
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
                    if (Convert.ToBoolean(Dt.Rows[i]["Can_Insert"].ToString()) == true)
                    {
                        Can_Insert.Value = "true";
                    }
                    if (Convert.ToBoolean(Dt.Rows[i]["Can_Update"].ToString()) == true)
                    {
                        Can_Update.Value = "true";
                    }
                    if (Convert.ToBoolean(Dt.Rows[i]["Can_Delete"].ToString()) == true)
                    {
                        Can_Delete.Value = "true";
                    }
                }
            }
        }
    %>
    <%--  <% if (Session["Username"].ToString() == "SuperAdmin")
                                 {
                                     if (Can_Insert.Value.ToString() == "true")
                                     { %>
                            <a href="Employes_Add.aspx" class="btn btn-primary pull-right">Add New</a>
                        <%}
                                 else
                                 {%>
                            <a href="Employes_Add.aspx" class="btn btn-primary pull-right disabled">Add New</a>
                        <%}
                                 }%>--%>
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Employee</li>
        </ol>
    </section>
    <section class="content">
        <% if (Session["Username"].ToString() == "SuperAdmin")
                            { %>
                       
                        <% }
                        else
                        { %>
                                <asp:FileUpload type="file" class="form-control cstm-btn-file" ID="FileExportExcel" runat="server" />

                        <%} %>
       


        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#employes" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>--%>
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <% if (Session["Username"].ToString() == "SuperAdmin")
                            { %>
                        <%--<a href="Employes_Add.aspx" class="btn btn-payroll pull-right">Add New</a>--%>
                        <% }
                        else if (Can_Insert.Value.ToString() == "true")
                        { %>
                        <a href="DeactiveEmployees.aspx" id="A1" runat="server" class="btn btn-payroll pull-right">Deactive Employees</a>
                        <a href="EmployeeCreation.aspx" id="chechempright" runat="server" class="btn btn-payroll pull-right">Add New</a>
                        <asp:Button ID="Btnexcelimport" ClientIDMode="Static" runat="server" Text="Bulk Import" CssClass="submit action-button btn btn-payroll pull-right" OnClick="Btnexcelimport_Click" />

                        <% }
                        else
                        { %>
                        <%--<a href="Employes_Add.aspx"class="btn btn-payroll pull-right disabled">Add New</a>
                        <asp:Button ID="Btnexcelimport" ClientIDMode="Static" runat="server" Text="Bulk Import" CssClass="submit action-button btn btn-payroll pull-right" OnClick="Btnexcelimport_Click" />--%>

                        <%} %>

                        <%--  <div class="file-upload-wrapper file-upload-wrapper1" data-text="Import Excel File!">
                           <asp:FileUpload ID="FileExportExcel" runat="server" />
                         </div>--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Btnexcelimport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee</h3>
                    </div>
                    <div class="box-body" >
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
    <!--SALMAN CODE-->
    
    
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        })

        function binddata() {
            // debugger
            //debugger
            //$('.datepick').datepicker({
            //    format: 'm/dd/yyyy',
            //    autoclose: true,
            //    clearBtn: false
            //});
         
        }
        var can_Insert = document.getElementById('<%= Can_Insert.ClientID%>').value;
        var can_View = document.getElementById('<%= Can_View.ClientID%>').value;
        var can_Update = document.getElementById('<%= Can_Update.ClientID%>').value;
        var can_Delete = document.getElementById('<%= Can_Delete.ClientID%>').value;

        $(document).ready(function () {
            bindGrid();
        });

        function bindGrid() {
            debugger
            var editOption = 'EditCompany';
            var deleteOption = '';
            $.ajax({
                type: 'POST',
                url: "Employee.aspx/Load",
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
                            pageSize: 20
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
                       
                        
                        scrolling: { mode: "standard" },
                     
                      
                        columns: [
                         
                            "EmployeeName",                        
                            "Email",
                             {
                                 dataField: "ProbationPeriod",
                                 caption: "Employee Status" ,
                                 cellTemplate: function (container, options) {
                               
                                     var clsBadge = ''
                                     if(options.value == 'Temporary'){
                                         clsBadge = 'badge badge-danger'
                                     }
                                     else if(options.value == 'Completed'){
                                         clsBadge = 'badge badge-dangerone'
                                     }
                                     else if(options.value == 'Permanent'){
                                       
                                         clsBadge = 'badge badge-success'
                                     }
                                     container.append("<span class='"+clsBadge+"'>"+options.value+"</span>")
                                 }
                             },
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
                                dataField: "EmployeeID",
                                width:150,
                                caption: "Action",
                                cellTemplate: function (container, options) {
                                    //debugger                                    
                                    //container.append("<a href='/Requirements/ContributorJobDetails?ID=" + options.value + "'><i data-toggle='tooltip' title='Preview' data-placement='top' class='fa fa-eye fa-2x'></i></a>")
                                    if (options.values[7] == true) {
                                        //debugger
                                        //debugger
                                        var EndOfServiceBtn = "";
                                        var setsalary="";
                                        var RoleId = <%= int.Parse((Session["RoleId"] == null ?0 : Session["RoleId"]).ToString()) %>;
                                        //if (RoleId==2) {
                                        //    EndOfServiceBtn = "<li><a href='EndOfServices.aspx?ID=" + options.value + "'>End of Services</a></li>";
                                        //}else
                                    
                                        if (RoleId == 1 || RoleId == 2){
                                         EndOfServiceBtn = "<li><a href='Employee_Termination.aspx?ID=" + options.value + "'>End of Services</a></li>";
                                         SetSalaryBtn = "<li><a href='SetSalary.aspx?ID=" + options.value + "'>Set Salary</a></li>";
                                     }
                                     EndOfServiceBtn += "</ul></div>"
                                 } else {
                                     EndOfServiceBtn = "";
                                 }

                                    //container.append("<div class='dropdown'><a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu'><li><a href='Employes_Edit.aspx?ID=" + options.value + "'>Edit</a></li><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><a OnClick='deleteEmlpoyee(" + options.value + ")'>Delete</a></li><li><a href='Employes_Transfer.aspx?ID=" + options.value + "'>Transfer</a></li><li><a href='Employes_Increment.aspx?ID=" + options.value + "'>Apply Increment</a></li><li><a href='Employee_PromotionNew.aspx?ID=" + options.value + "'>Promotion</a></li><li><a href='WhMonitor.aspx?ID=" + options.value + "'>Working Hours</a></li><li><a href='employee_assets.aspx?ID=" + options.value + "'>Employee Assets</a></li>" + EndOfServiceBtn)
                                    container.append("<div class='dropdown'><a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'  ><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu '><li><a href='#'  id='aid' data-empid='" + options.value + "' onclick='checkright(this)'>Edit</a></li><li><a OnClick='PersonalEmail(" + options.value + ")'>Email</a></li><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><a OnClick='DeleteEmlpoyee(" + options.value + ")'>Delete</a></li><li><li><a href='Employee_Terminate.aspx?ID=" + options.value + "'>Terminate</a></li><li><a href='Employes_Transfer.aspx?ID=" + options.value + "'>Transfer</a></li><li><a href='Employes_Increment.aspx?ID=" + options.value + "'>Apply Increment</a></li><li><a href='Employee_PromotionNew.aspx?ID=" + options.value + "'>Promotion</a></li><li><a href='WhMonitor.aspx?ID=" + options.value + "'>Working Hours</a></li><li><li><a href='Employee_Character.aspx?ID=" + options.value + "'>Behavior</a></li><li><a OnClick='resetPassword(" + options.value + ")'>Defualt Password</a></li><li><a href='EmployeeFineCharges.aspx?ID=" + options.value + "'>Fine/Charges</a></li>"+ EndOfServiceBtn)
                                    console.log("options", options.value);
                                    //container.append("<div class='dropdown'><a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu'><li><a href='#'  id='aid' data-empid='"+options.value +"' onclick='checkright(this)'>Edit</a></li><li><a href='SetSalary.aspx?ID=" + options.value + "'>Set Salary</a></li><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><a OnClick='deleteEmlpoyee(" + options.value + ")'>Delete</a></li><li><a href='Employes_Transfer.aspx?ID=" + options.value + "'>Transfer</a></li><li><a href='Employes_Increment.aspx?ID=" + options.value + "'>Apply Increment</a></li><li><a href='Employee_PromotionNew.aspx?ID=" + options.value + "'>Promotion</a></li><li><a href='WhMonitor.aspx?ID=" + options.value + "'>Working Hours</a></li><li>"+ EndOfServiceBtn)
                                }

                            },
                            
                        ],
                        showBorders: true
                    });
                },
                error: function (result) {
                }
            });
        }

      
        
        //function InactiveEmlpoyee(Employeeid) {
        //    var empid = Employeeid;
           
        //    console.log("InactiveEmlpoyee", empid);
        //        $.ajax({
        //            type: 'POST',
        //            url: "Employee.aspx/EmployeeInctive",
        //            contentType: 'application/json; charset=utf-8',
        //            dataType: 'json',
        //            data: "{'EmployeeID':'" + empid + "'}",
        //            success: function (message) {
        //                if (message.d === 'User Inactivated succesfully') {

        //                    alert("User Inactivated succesfully");
        //                    $("a[href='#'][onclick='InactiveEmlpoyee(" + empid + ")']").text("Activate");
        //                    location.reload();
        //                }

        //                else if (message.d === 'User Activated succesfully') {
        //                    alert("User Activated succesfully");
        //                    $("a[href='#'][onclick='InactiveEmlpoyee(" + empid + ")']").text("Inactivate");
        //                    location.reload();
        //                }
        //                else
        //                {
        //                    alert("EmployeeID is is not found");
        //                    location.reload();
        //                }
        //            },
        //      error: function (result) {

        //      }
        //        });
            
        //}


        function resetPassword(Employeeid) {

            var empid = Employeeid;

            console.log("resetpassword console", empid);
            $.ajax({
                type: "POST",
                url: 'Employee.aspx/SetDefaultPassword',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'EmployeeID':'" + empid + "'}",
                //data: JSON.stringify({ EmployeeID: Employeeid }), 
                success: function (message) {
                    if (message.d === 'Employee not found') {
                       
                        alert("Employee not found!");
                    }
                    else if (message.d === 'User not found') {
                        alert("User not found!");
                    }
                    else
                    {
                        alert("Password reset successfully!");
                        location.reload();
                    }
                },
                error: function (error) {
                        alert("Error resetting password.");
                }
            });
        }

        function DeleteEmlpoyee(id)
        {
            $("#Delete").modal('show');
            $("#hdnempid2").val(id);
            //$.ajax({
            //    type:'Post',
            //    urll: "Employee.aspx/DeleteEmployee",
            //    contentType:'',
            //    datatype:'json',
            //    data:"{'data':'"+id+"'}",
            //    success:function(result)
            //    {

            //    }


            //})

        }
        function PersonalEmail(id) {
           
            $('#Email-Modal').modal('show');
            $('#hdnempid').val(id);
            var  obj = {data:id};
            $.ajax({
                type:'POST',
                url: "Employee.aspx/GetPersonalEmail",
                contentType: 'application/json; charset=utf-8',
                datatype:'json',
                data:JSON.stringify(obj),
                success:function(result)
                {
                    console.log("Success")
                    if (result.d) {
                        
                        $("#txtPersonaEmail").val(result.d);
                    }
                    else
                    {
                       
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
        function TerminateEmlpoyee(id) {
           
            $('#Delete-Modal').modal('show');
            $('#hdnempid').val(id);
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
        function deleteEmlpoyee(id) {
         

            $.ajax({
                type:'Post',
                url:"Employee.aspx/CheckRightsofDelete",
                contentType: 'application/json; charset=utf-8',
                dataType:'json',
                data:"{'data':'"+id+"'}",
                success:function(result)
                {
                    if (result.d)
                    {

                        $('#Delete-Modal').modal('show');
                        $('#EmployeeID').val(id);
                    }
                    else
                    {
                        alert("You don't have permission by super admin to delete record ")
                    }
                }
            })
         
           
        }
    </script>

    <div class="modal fade" id="Delete-Modal" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel2">Termination</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12" id="mybtn">

                                        <fieldset>
                                            <label>Are you sure you want to Terminate this Employee?</label>

                                            <div class="form-group col-md-12">

                                                <asp:TextBox ID="MsgDelete" Visible="false" runat="server"></asp:TextBox>
                                                <asp:HiddenField ID="hdnempid" runat="server" ClientIDMode="Static"></asp:HiddenField>
                                                <asp:TextBox ID="txtreason" runat="server" ClientIDMode="Static" placeholder="Reason of termination"></asp:TextBox>
                                               
                                            </div>
                                            <div class="form-group col-md-12">

                                                 <span id="spncheck" style="display:none;color:red;">Please select date</span>
                                                  <asp:TextBox type="date" id="txtEntryDate" runat="server"  class="form-control" validate='vgroup' require="Please Enter date" />
                                                <%--<input type="date" id="txtFromDate" Class="form-control" validate='vgroup' require="Please Enter your First Name" />--%>
                                                <br />
                                                <asp:FileUpload ID="UploadDocImage" runat="server" />                                              
                                            </div>
                                            

                                        </fieldset>
                                        <%-- End Account Setup--%>

                                        <fieldset>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                               <%-- <asp:TextBox ID="TextBox4" Visible="false" runat="server"></asp:TextBox>--%>
                             <asp:Button id="btnsave" runat="server" Text="Terminate" class="btn btn-primary" onclick="btnsave_Click" OnClientClick="if (validate('vgroup')) { return true; } else { return false; }"></asp:Button>
                                <%--<button id="btnSubmit" style="width: 130px;"  class="submit action-button">Yes</button>--%>
                               <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" class="btn btn-primary" Text="No" Style="width: 130px;" ></asp:Button>
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
     <%--For Personal EMail--%>
      <div class="modal fade" id="Email-Modal" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div3" runat="server">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel3">Personal Email</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12" id="mybtnnn">

                                        <fieldset>
                                            <%--<label>Are you sure you want to Terminate this Employee?</label>--%>

                                            <div class="form-group col-md-12">

                                                
                                                <asp:TextBox ID="txtPersonaEmail" AutoComplete="Off"  runat="server" ClientIDMode="Static" placeholder="Enter Email"></asp:TextBox>
                                                <asp:Label ID="lblpwd" runat="server" Text="Label" ClientIDMode="Static" Width="450" style="text-align:center; height:40px; display:none; font-size:18px; color:red;"></asp:Label>
                                               
                                            </div>
                                            
                                            

                                        </fieldset>
                                        <%-- End Account Setup--%>

                                        <fieldset>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                               <%-- <asp:TextBox ID="TextBox4" Visible="false" runat="server"></asp:TextBox>--%>
                             <asp:Button id="btnPEmail" ClientIDMode="Static" runat="server" Text="Save"   onclick="btnPEmail_Click"  class="btn btn-primary"></asp:Button>
                               <%--<button id="btnSubmit" style="width: 130px;"  class="submit action-button">Yes</button>--%>
                               <%--<asp:Button ID="btnClose2" ClientIDMode="Static" runat="server"  class="btn btn-primary"  Text="No" Style="width: 130px;" ></asp:Button>--%>
                                <input type="button" id="btnClose2"  class="btn btn-primary" value="Close" />
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
     <div class="modal fade" id="Delete" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div2" runat="server">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">Permanent Delete</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12" id="mybtnn">

                                        <fieldset>
                                            <label>Are you sure you want to Permanent Delete this Employee?</label>      
                                               <asp:HiddenField ID="hdnempid2" runat="server" ClientIDMode="Static"></asp:HiddenField>                                                                             
                                        </fieldset>
                                        <%-- End Account Setup--%>

                                        <fieldset>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                               <%-- <asp:TextBox ID="TextBox4" Visible="false" runat="server"></asp:TextBox>--%>
                             <asp:Button id="btndelete" runat="server" Text="Delete" class="btn btn-primary" onclick="btndelete_Click" ></asp:Button>
                                <%--<button id="btnSubmit" style="width: 130px;"  class="submit action-button">Yes</button>--%>
                               <asp:Button ID="btnclose" ClientIDMode="Static" runat="server" data-dismiss="modal" class="btn btn-primary" Text="No" Style="width: 130px;" ></asp:Button>
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

    <script type="text/javascript">
        $("#btnClose2").click(function()
        {
            $("#Email-Modal").modal('hide');
            $("#txtPersonaEmail").val('');

        })
        $("#txtPersonaEmail").blur(function()
        {

            var email = $("#txtPersonaEmail").val();
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                $("#lblpwd").show(1000).html("Invalid Email Address");
               

                
                setTimeout(function () {
                    $('#lblpwd').fadeOut('fast');
                }, 4000);
                return false;
            } else {
                $("#lblpwd").hide();
    
                $('[id$=btnPEmail]').removeAttr('disabled'); 
                $('[id$=btnPEmail]').addClass("btn btn-primary");
                return true;
            }
        })
            $('#btnSubmit').click(function (e) {
                debugger
                e.preventDefault();
                var employeeID = $('#EmployeeID').val();
                var Reason =$('#txtreason').val(); 
                var date =$('#txtFromDate').val();
                var formdata = new FormData();
                var files = $('input[type="file"][id $=UploadDocImage]').get(0).files;
                var image =files[0];
                if (files.length > 0) {
                    formdata.append("ProfileImg", files[0]);
                    //formdata.append("Reason",Reason);
                    $('.labelNIC').text('');
                    $.ajax({
                                type: 'POST',
                                url: 'Service/PostImage.asmx/PostFiles',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                data:"{'"+formdata+"','Reason':'"+Reason+"'}",                        
                        //type: 'Post',
                        //url: 'Service/PostImage.asmx/PostFiles',
                        //        contentType: 'application/json; charset=utf-8',
                        //        dataType: 'json',                        
                        //        data:"{'img':'"+ files +"','Reason':'"+Reason+"'}",
                        success: function (data) {
                            if ($(data).text() != "") {
                                $('[id $=hdNICName]').val($(data).text());
                            }
                            else {
                                $('[id $=NICUpload]').val('');
                                msgbox(4, "Error", "File is not Supported!");
                            }
                        }
                    });
                }
                //var image= $('#UploadDocImage').val();
                //var formData = new FormData();
                ////formData.append('file', $('#f_UploadImage')[0].files[0]);
                //if (date == "") {
                //    $("#spncheck").show();
                //    //alert("Please select date");
                //    //window.location.href = "Employee.aspx";
                //    //bindGrid();
                //}
                //else
                //{
                //    $("#spncheck").hide();
                //    $.ajax({
                //        type: 'POST',
                //        url: "Employee.aspx/Terminate",
                //        contentType: 'application/json; charset=utf-8',
                //        dataType: 'json',
                //        data:"{'EmployeeID':'"+ employeeID +"','Reason':'"+Reason+"','date':'"+date+"'}",
                //        //data: {},
                //        success: function (result) {
                //            if (result) {
                //                window.location.href = "Employee.aspx";
                //            }
                //            console.log(true);
                //        },
                //        error: function(result) {
                //            console.log(false);
                //        }
                //    });
                //}
          
            });
    
       

    
     
    </script>
    <!--SALMAN CODE-->
</asp:Content>