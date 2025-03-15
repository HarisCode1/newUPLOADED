<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="EOBIMonthlyReturns.aspx.cs" Inherits="EOBI_MonthlyReturns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <div class="row">
            <div class="col-md-12">
                <div class="heading">
                   EOBI Monthly Returns
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <table class="pfmonthly-table">
                            <tr>
                                <td style="width:100px;"><label>Month/Year :</label></td>
                                <td colspan="2">
                                    <select>
                                        <option>jun/2014</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-5 col-md-offset-4">
                         <table class="pfmonthly-table">  
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Export TO CSV" class="pull-right btn btn-primary" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table class="table table-bordered table-hover">
                    <tr>               
                        <th>IP Number</th>
                        <th>IP Name</th>
                        <th>NO of Days for which <br /> wages paid/payable <br /> during the month</th>
                        <th>Total Monthly Wages</th>
                        <th>Reason for Zero working <br /> days (Numeric only)</th>
                        <th>Last Working</th>
                        <th>Day Reason Details</th>
                    </tr>
                    <tr>   
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
                    </tr>
                    <tr>   
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
                            <button type="button" class="gridadd-btn btn btn-primary">Report</button>
                            <button type="button" class="gridadd-btn btn btn-primary">Close</button>
                        </td>
                    </tr>
                </table>
              </div>
        </div>
</asp:Content>

