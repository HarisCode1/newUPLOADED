<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="RolePermission.aspx.cs" Inherits="RolePermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script> 
    <script type="text/javascript">

         $(document).ready(function () {
             debugger
            //SetActiveFileApplicationSetup();
            BindCheckBoxGrid();
            $('.tab-a').on("click", function () {
                $("[Id$=hddTabID]").val($(this).find('a').attr('id'));
            });
            TabActive();
        });

        function TabActive() {
            var ActiveID = $("[Id$=hddTabID]").val();
            if (ActiveID != "") {
                $('#' + ActiveID + '').trigger('click');
            }
        }

        function BindCheckBoxGrid() {
            debugger;

            $(".GridAllModule").prepend($("<thead></thead>").append($(".GridAllModule").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridAdministrations").prepend($("<thead></thead>").append($(".GridAdministrations").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridEmployeeCreation").prepend($("<thead></thead>").append($(".GridEmployeeCreation").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridAttendances").prepend($("<thead></thead>").append($(".GridAttendances").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridAttendenceSheet").prepend($("<thead></thead>").append($(".GridAttendenceSheet").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".Gridinputmodules").prepend($("<thead></thead>").append($(".Gridinputmodules").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridSalariesGeneration").prepend($("<thead></thead>").append($(".GridSalariesGeneration").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridSalarySlip").prepend($("<thead></thead>").append($(".GridSalarySlip").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridRoles").prepend($("<thead></thead>").append($(".GridRoles").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".Gridpayroll").prepend($("<thead></thead>").append($(".Gridpayroll").find("tr:first"))).dataTable({ "pageLength": 50 });
            $(".GridUserCreation").prepend($("<thead></thead>").append($(".GridUserCreation").find("tr:first"))).dataTable({ "pageLength": 50 });


            
            //$('input[type="checkbox"]').checkbox();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript">
                $(document).ready(function () {
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    function EndRequestHandler(sender, args) {
                        $(function () {
                            debugger;
                            BindCheckBoxGrid();
                            $('.tab-a').on("click", function () {
                                $("[Id$=hddTabID]").val($(this).find('a').attr('id'));
                                //post code
                            });
                            TabActive();
                        });

                        function TabActive() {
                            debugger;
                            var ActiveID = $("[Id$=hddTabID]").val();
                            if (ActiveID != "") {
                                $('#' + ActiveID + '').trigger('click');
                            }
                        }

                        function BindCheckBoxGrid() {
                            debugger;
                            //$(".GridAllModule").prepend($("<thead></thead>").append($(".GridAllModule").find("tr:first"))).dataTable({ "pageLength": 50 });
                            $(".GridAdministration").prepend($("<thead></thead>").append($(".GridAdministration").find("tr:first"))).dataTable({ "pageLength": 50 });
                            $(".GridAttendances").prepend($("<thead></thead>").append($(".GridAttendances").find("tr:first"))).dataTable({ "pageLength": 50 });
                            $(".GridAttendenceSheet").prepend($("<thead></thead>").append($(".GridAttendenceSheet").find("tr:first"))).dataTable({ "pageLength": 50 });
                            $(".Gridinputmodules").prepend($("<thead></thead>").append($(".Gridinputmodules").find("tr:first"))).dataTable({ "pageLength": 50 });
                            $(".GridSalariesGeneration").prepend($("<thead></thead>").append($(".GridSalariesGeneration").find("tr:first"))).dataTable({ "pageLength": 50 });
                            $(".GridSalarySlip").prepend($("<thead></thead>").append($(".GridSalarySlip").find("tr:first"))).dataTable({ "pageLength": 50 });
                            $(".GridUserCreation").prepend($("<thead></thead>").append($(".GridUserCreation").find("tr:first"))).dataTable({ "pageLength": 50 });
                            //$('input[type="checkbox"]').checkbox(); 
                        }
                    }
                });
            </script>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info custom_input">
                            <div class="box-header with-border">
                                <i class="fa fa-table custom_header_icon admin_icon"></i>
                                <h3 class="box-title">Role Permission</h3>
                                <div class="box-tools pull-right">
                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="bs-example pull-right">
                                            <div class="btn-group">
                                                <asp:HiddenField runat="server" ID="hddRoleID" />
                                                <asp:Button ID="btnSave" class="btn btn-success" runat="server" OnClick="btnSave_Click" Text="Save Role Permission" />
                                            </div>
                                            <div class="btn-group">
                                                <a href="Role.aspx" class="btn btn-default">Cancel</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                 
                                


                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Role :</label>
                                         <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" >
                                                
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div><br />

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">User / Employee :</label>
                                         <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlUserAndEmp" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlUserAndEmp_SelectedIndexChanged">
                                                <asp:listitem value="0" text="--Please Select--" runat="server"></asp:listitem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />

                                </div>

                                <div class="form-horizontal" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Role Name :</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="txtRoleName" CssClass="form-control" Width="100%" placeholder="Role Name" Enabled="false" />
                                        </div>
                                    </div>

                                </div>

                                <section class="panel panel-default">

                                    <header class="panel-heading bg-light">
                                        <ul class="nav nav-tabs nav-divider">
                                            <li class="tab-a active"><a href="#ALLMODULE" data-toggle="tab" id="Module">All Module</a></li>
                                            <li class="tab-a"><a href="#Administrations" data-toggle="tab" id="Administrator">Administration</a></li>
                                            <li class="tab-a"><a href="#EmployeeCreation" data-toggle="tab" id="EmployeesCreation">Employee Creation</a></li>
                                            <li class="tab-a"><a href="#Attendances" data-toggle="tab" id="Attendance">Attendance</a></li>
                                            <li class="tab-a"><a href="#AttendenceSheets" data-toggle="tab" id="AttendanceSheets">Attendance Sheet</a></li>
                                            <li class="tab-a"><a href="#inputmodules" data-toggle="tab" id="InputModule">Input Modules</a></li>
                                            <li class="tab-a"><a href="#SalariesGeneration" data-toggle="tab" id="SalaryGenerations">Salary Generation</a></li>
                                            <li class="tab-a"><a href="#SalarySlip" data-toggle="tab" id="SalarySlipGeneration">Salary Slip Generation</a></li>

                                            <li class="tab-a"><a href="#Role" data-toggle="tab" id="Tools">Role</a></li>
                                            <li class="tab-a"><a href="#payroll" data-toggle="tab" id="payrolls">PayRoll</a></li>
                                            <li class="tab-a"><a href="#UserCreation" data-toggle="tab" id="UserCrea">User Creation</a></li>


                                            <%--<li class="tab-a"><a href="#Reports" data-toggle="tab" id="report">Reports</a></li>--%>
                                            
                                            <%--<li class="tab-a"><a href="#Admin" data-toggle="tab" id="Calendar">Calendar</a></li>--%>
                                            <%--<li class="tab-a"><a href="#Reports" data-toggle="tab" id="Screens">Screens</a></li>--%>
                                        </ul>
                                    </header>
                                    <div class="panel-body table-responsive">
                                        <div class="tab-content">
                                            <asp:HiddenField runat="server" ID="hddTabID" />
                                            <div class="tab-pane active" id="ALLMODULE">
                                                <asp:UpdatePanel runat="server" ID="Modal">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GridAllModule" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridAllModule table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False" DataKeyNames="ModuleID" OnRowDataBound="GridAllModule_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged" />
                                                                        <asp:Label runat="server" Visible="false" ID="lblModuleID" Text='<%#Eval("ModuleID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName" HeaderStyle-Width="20%" />
                                                                <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" AutoPostBack="true" OnCheckedChanged="ChkInsert_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" AutoPostBack="true" OnCheckedChanged="ChkUpdate_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" AutoPostBack="true" OnCheckedChanged="ChkDelete_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>


                                            <div class="tab-pane" id="Administrations">
                                                
                                                <asp:GridView ID="GridAdministrations" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridAdministrations table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="EmployeeCreation">
                                                <asp:GridView ID="GridEmployeeCreation" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridEmployeeCreation table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="inputmodules">
                                                <asp:GridView ID="Gridinputmodules" runat="server" ShowHeaderWhenEmpty="true" CssClass="Gridinputmodules table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="AttendenceSheets">
                                                <asp:GridView ID="GridAttendenceSheet" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridAttendenceSheet table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="SalarySlip">
                                                <asp:GridView ID="GridSalarySlip" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridSalarySlip table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="Role">
                                                <asp:GridView ID="GridRoles" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridRoles table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="Attendances">
                                                <asp:GridView ID="GridAttendances" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridAttendances table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="SalariesGeneration">
                                                <asp:GridView ID="GridSalariesGeneration" runat="server" PageSize="100" ShowHeaderWhenEmpty="true" CssClass="GridSalariesGeneration table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="payroll">
                                                <asp:GridView ID="Gridpayroll" runat="server" PageSize="100" ShowHeaderWhenEmpty="true" CssClass="Gridpayroll table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane" id="UserCreation">
                                                <asp:GridView ID="GridUserCreation" runat="server" PageSize="100" ShowHeaderWhenEmpty="true" CssClass="GridUserCreation table table-striped table-bordered dataTable table-responsive" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkAllSelect" CssClass="checkbox" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAllSelect_CheckedChanged1" />
                                                                <asp:Label runat="server" Visible="false" ID="lblPageID" Text='<%#Eval("PageID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" HeaderStyle-Width="50%" />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkView" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insert" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkInsert" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Update" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkUpdate" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkDelete" CssClass="checkbox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </section>

                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

