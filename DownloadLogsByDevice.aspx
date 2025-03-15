<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="DownloadLogsByDevice.aspx.cs" Inherits="DownloadLogsByDevice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="row">
            <div class="col-md-12">
                <div class="heading">
                    Download Logs
                </div>
                <fieldset>
                    <legend>Communication with Device</legend>
                </fieldset>
                <div class="col-md-6 col-md-offset-3">
                    <table>
                        <tr>
                            <td style="width:100px;"><label>Machine :</label></td>
                            <td colspan="2">
                                <select>
                                    <option>1</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2">
                                 <asp:CheckBox ID="CheckBox1" runat="server" Text="Database Device" CssClass="checkboxbtn" />
                            </td>
                            <td>
                                 <button type="button" class="gridadd-btn pull-right btn btn-primary">Connect</button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-offset-3"></div>
                <table class="table table-bordered table-hover">
                    <tr>         
                        <th>Sr No</th>
                        <th>
                            EnrollNumber
                        </th>
                        <th>Date</th>
                        <th>Machine No</th>
                        <th>Verify No</th>
                    </tr>
                    <tr>   
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
                    </tr>
                    <tr>   
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
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="pull-right">
                            <button type="button" class="gridadd-btn btn btn-primary">Download Logs</button>
                            <button type="button" class="gridadd-btn btn btn-primary">Save Logs</button>
                            <button type="button" class="gridadd-btn btn btn-primary">Close</button>
                        </td>
                    </tr>
                </table>
              </div>
        </div>
</asp:Content>

