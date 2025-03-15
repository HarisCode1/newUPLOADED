<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="IncrementDecrement.aspx.cs" Inherits="IncrementDecrement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <div class="row">
            <div class="col-md-12">
                <div class="heading">
                   Increment Decrement
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <table class="pfmonthly-table">
                            <tr>
                                <td style="width:80px;"><label>Date :</label></td>
                                <td colspan="2">
                                    <select>
                                        <option>jun/2014</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4 col-md-offset-5">
                         <table class="pfmonthly-table">  
                            <tr>
                                <td>
                                    <label>Search :</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table class="table table-bordered table-hover">
                    <tr>                    
                        <th> Member ID</th>
                        <th>Member Name</th>
                        <th>EPF wages</th>
                        <th>EPS wages</th>
                        <th>EPF Contribution (EE Share) due</th>
                        <th>EPF Contribution (EE Share ) being remitted</th>
                        <th>EPS Contribution due</th>
                        <th>EPS Contribution being remitted</th>
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
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="pull-right">
                            <button type="button" class="gridadd-btn btn btn-primary">Export To Excel</button>
                            <button type="button" class="gridadd-btn btn btn-primary">Close</button>
                        </td>
                    </tr>
                </table>
              </div>
        </div>
</asp:Content>

