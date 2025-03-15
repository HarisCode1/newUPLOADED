<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_Termination.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Resigned Employee Termination</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="employee.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Resigned Employee Termination</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Resigned Employee for Termination</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <table class="table ">
                                <tr>
                                    <td style="width: 10%;">
                                        <b>Id</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Name</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Designation</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Department</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldepartment" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                  
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <table class="table employeeTerminationStatusTable">
                                    <tr>
                                        <th colspan="2" class="text-center">Gross Salary
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>Gross Salary
                                        </th>
                                        <td>
                                            <asp:Label ID="lblSalary" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label1"  runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <th>Provident Fund
                                        </th>
                                        <td>
                                            <asp:Label ID="lblPF" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <th>Bonus
                                        </th>
                                        <td>
                                            <asp:Label ID="lblBonus" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <th>Total Salary</th>
                                        <td class="">
                                            <asp:Label ID="lbltotalsalary" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label4" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-sm-6">
                                <table class="table employeeTerminationStatusTable">
                                    <tr>
                                        <th colspan="2" class="text-center">Deductions From Salary
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>Total Salary
                                        </th>
                                        <td>
                                            <asp:Label ID="lblGrossForDeduction" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label9" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Loan Amount
                                        </th>
                                        <td>
                                            <asp:Label ID="lblLoan" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <th>Leaves Deduction
                                        </th>
                                        <td>
                                            <asp:Label ID="lblLeaves" runat="server" Text="Label"></asp:Label>
                                            <asp:Label ID="Label6" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>--%>
                                   <%-- <tr>
                                        <th>Depreciation</th>
                                        <td>
                                            <asp:Label ID="lblDepreciation" runat="server" Text="Label"></asp:Label>
                                            <asp:Label ID="Label7" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>--%>
                                      <tr>
                                        <th>Leave Deduction</th>
                                        <td>
                                            <asp:Label ID="lblleavededuction" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>
                                      
                                    <tr>

                                        <th>Total
                                        </th>
                                        <td>
                                            <asp:Label ID="LblTotal" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="Label11" runat="server" Text="PKR"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="actions">
                            <asp:Button ID="btnValidate" runat="server" Text="Terminate this Resigned Employee" CssClass="btn btn-payroll pull-right" OnClick="btnValidate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
