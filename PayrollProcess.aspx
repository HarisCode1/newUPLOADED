<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="PayrollProcess.aspx.cs" Inherits="PayrollProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
            <div class="col-md-12">
                <div class="heading">
                   Payroll Process
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <table class="parolproc-table">
                            <tr>
                                <td style="width:100px;"><label>Month/Year :</label></td>
                                <td>
                                    <select>
                                        <option>july/2014</option>
                                    </select>
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Recalculate" CssClass="btn btn-primary" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4 col-md-offset-3">
                         <table class="parolproc-table">  
                            <tr>
                                <td style="width:80px;"><label>Search :</label></td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table class="table table-bordered table-hover">
                    <tr>             
                        <th>        
                            <asp:CheckBox ID="CheckBox2" runat="server" /></th>
                        <th style="width:70px;">Enroll Id</th>
                        <th>Name</th>
                        <th>Department</th>
                        <th>Total Days</th>
                        <th>P Days</th>
                        <th>A Days</th>
                        <th>Wages</th>
                        <th>OT Hrs</th>
                        <th>OT Days</th>
                        <th>Salary</th>
                        <th>Daily Wages</th>
                    </tr>
                    <tr>   
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>   
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>   
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="pull-right">
                            <button type="button" class="gridadd-btn btn btn-primary">Export To Excel</button>
                            <button type="button" class="gridadd-btn btn btn-primary">Save</button>
                            <button type="button" class="gridadd-btn btn btn-primary">Close</button>
                            
                        </td>
                    </tr>
                </table>
              </div>
        </div>
</asp:Content>

