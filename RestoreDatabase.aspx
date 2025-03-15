<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="RestoreDatabase.aspx.cs" Inherits="RestoreDatabase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="row">
            <div class="col-md-12">
                <div class="heading">
                   Restore Database 
                </div>
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">
                       <table>
                           <tr>
                               <td><label>Back up file :</label></td>
                               <td>
                                   <asp:FileUpload ID="FileUpload1" runat="server" />
                               </td>
                           </tr>
                        </table>
                        <div class="modal-footer">
                        <table>
                           <tr>

                               <td>
                                   <label>Note : Bak extension compulsory.</label>
                               </td>
                               
                                     <asp:Button ID="btnSave" runat="server" Text="Restore" class="btn btn-primary"></asp:Button>
                                     <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-primary"></asp:Button>
                               
                           </tr>
                       </table>
                            </div>
                    </div>
                    <div class="col-md-6">
                    </div>
                </div>
                <table>
                    <tr>
                        <td class="pull-right">
                           
                        </td>
                    </tr>
                </table>
              </div>
        </div>
</asp:Content>

