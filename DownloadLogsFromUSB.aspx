<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="DownloadLogsFromUSB.aspx.cs" Inherits="DownloadLogsFromUSB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <fieldset>
                <legend></legend>
            </fieldset>
            <div class="col-md-6 col-md-offset-3">
                <table>
                    <tr>
                        <td style="width:100px"><label>Device Company :</label></td>
                        <td>
                            <select>
                                <option>ZK</option>
                            </select>
                        </td>
                        <td><label>Count :</label></td>
                    </tr>
                </table>
            </div>
            <div class="col-md-offset-3"></div>
        </div>
    </div>
</asp:Content>

