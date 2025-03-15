<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employes_Increment.aspx.cs" Inherits="Employes_Increment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <h1>Employee Increment</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx">Employees</a></li>
            <li class="active">Employee Increment</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Increment</h3>
                        <div class="box-tools">
                            <%--<asp:Button ID="BtnLog" runat="server" CssClass="btn btn-default pull-right" Text="Increment Log" CausesValidation="false" />--%>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h3 class="text-center"><b>Employee -</b> Increment</h3>
                                            <hr />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:HiddenField ID="hdnbtn" runat="server" />
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
                                            <label for="email">Basic Salary:</label>
                                            <asp:TextBox ID="TxtBasicSalary" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label for="email">Increment Of:</label>
                                            <asp:TextBox ID="TxtIncrement" runat="server" validate='vgroup' require="Please Enter" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label for="email">Salary after Increment:</label>
                                            <asp:TextBox ID="TxtSalaryAfterIncrement" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="email">Old Tax:</label>
                                            <asp:TextBox ID="txtoldtax" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label for="email">Current Tax:</label>
                                            <asp:TextBox ID="txtcurrenttax" runat="server" validate='vgroup' require="Please Enter" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <%--<div class="col-md-4">
                                            <label for="email">Salary after Increment:</label>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>--%>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <span class="pull-right">
                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />
                                            </span>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="content">

        <div id="companygrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Performance Evaluation</h3>
                    </div>
                    <asp:TextBox ID="txtsearchperformance" runat="server" Type="number" CssClass="form-control" placeholder="Enter Year" Width="300"></asp:TextBox>
                    <asp:Button ID="btnsearchperformance" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnsearchperformance_Click" Style="margin-top: 10px;" />
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdempperformanceevaluation" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="AnnuaLeaves" HeaderText="Annual Leaves" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Leavesavailed" HeaderText="Leaves Availed" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="ExceedDays" HeaderText="Exceed Days" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="DeductionDueToLate" HeaderText="Deduction Due To Late" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="TotalDaysOverLimit" HeaderText="Total Days Over Limit" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="AdminScore" HeaderText="Admin Score" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="BehaviorwithColleagues" HeaderText="Behavior With Colleagues Per(%)" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="BehaviorwithCustomers" HeaderText="Behavior With Customers Per(%)" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Honesty" HeaderText="Honesty Per(%)" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="TimeReporting" HeaderText="Time Reporting Per(%)" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="AttributesScore" HeaderText="Attributes Score" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="AttributesPlusAdmin" HeaderText="Attributes Plus Admin" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="AttributeResult" HeaderText="Attribute Result" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Grade" HeaderText="Grade" HeaderStyle-Width="35%" />

                                            <asp:TemplateField HeaderText="">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                       DataSourceID="EDS_Company"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>

    <section class="content">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>

                <div class="row">
                    <div class="col-md-12">

                        <div class="box box-info custom_input">

                            <div class="box-header with-border">
                                <i class="fa fa-table custom_header_icon admin_icon"></i>
                                <h3 class="box-title">Employee Leaves</h3>

                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtsearch" runat="server" Type="number" CssClass="form-control" placeholder="Enter Year" Width="300"></asp:TextBox>
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search" OnClientClick="SearchLeaves(); return false;" Style="margin-top: 10px;" />
                                        <%-- <button onclick="SearchLeaves()" CssClass="btn btn-primary"    runat="server">Search</button>--%>
                                        <h3 class="text-center"><b>Employee -</b> Leaves</h3>
                                        <hr />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="demo-container">
                                            <div id="chart"></div>
                                        </div>


                                        <span class="pull-right">

                                            <%--    <asp:Button ID="SearchRecord" OnClick="anc" CssClass="SearchLeaves btn btn-primary" Text="Save" />--%>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>


    </section>


    <script type="text/javascript">
        var basicSalary = '';
        var salaryAfterIncrement = '';
        $().ready(function () {


            basicSalary = $("#<% =TxtBasicSalary.ClientID %>").val();
            $("#<% =TxtSalaryAfterIncrement.ClientID %>").val(basicSalary);
        });

        $("#<% =TxtIncrement.ClientID %>").blur(function () {
            var increment = '';


            increment = $("#<% =TxtIncrement.ClientID %>").val();
            basicSalary = parseFloat(basicSalary);
            increment = parseFloat(increment);

            var result = basicSalary + increment;
            $("#<% =TxtSalaryAfterIncrement.ClientID %>").val(result);

        });


        function SearchLeaves() {
            debugger
            var Id = $("#<% =hdnbtn.ClientID %>").val();
            var year = $("#<% =txtsearch.ClientID %>").val();
            $.ajax({
                type: "POST",
                url: "Employes_Increment.aspx/SearchLeaves",
                data: "{'ID':'" + Id + "','Year':'" + year + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (msg) {
                    if (msg.d != "Empty") {
                        var data = JSON.parse(msg.d);

                        $(function () {
                            $("#chart").dxChart({
                                dataSource: data,
                                title: "The Over All Leaves Of Employee",
                                commonSeriesSettings: {
                                    argumentField: "state",
                                    type: "bar",
                                    color: '#0000FF',
                                    hoverMode: "allArgumentPoints",
                                    selectionMode: "allArgumentPoints",
                                    label: {
                                        visible: true,
                                        format: {
                                            type: "fixedPoint",
                                            precision: 0,

                                        }
                                    }
                                },
                                series: {
                                    argumentField: "Months",
                                    valueField: "TotalLeaves",
                                    name: "Leaves MonthWise",
                                    type: "bar",
                                    color: '#ffaa66'
                                },
                                onPointClick: function (e) {
                                    e.target.select();
                                }

                            });

                        });
                    }
                    else {
                        alert("No Leaves found in  " + year);
                    }



                    // Do something interesting with msg.d here.
                }
            });

        }



        <%--        $(#"<% =SearchRecord.ClientID %>").click(function(){
            alert("");
            $.ajax({
                type: "POST",
                url: "PageName.aspx/MethodName",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    // Do something interesting with msg.d here.
                }
            });

        })--%>

       


    </script>
</asp:Content>

