<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="DatabaseSetting.aspx.cs" Inherits="DatabaseSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    

            <div class="row">
            <div class="col-md-12">
                <div class="heading">
                   Database Setting
                </div>
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                       <table>
                           <tr>  
                               <td style="width:160px;"><label>Backup Path :</label></td>
                               <td>
                                   <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   <label>Company Backup Code :</label>
                               </td>
                               <td>
                                     <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                   <label>Prompt For Backup :</label>
                               </td>
                               <td>
                                   <select>
                                       <option></option>
                                   </select>
                               </td>
                           </tr>                         
                       </table>
                        <div class="modal-footer">
                           
                               
                              <asp:Button ID="btnTest" runat="server" Text="Test"  class="btn btn-primary pull-left"></asp:Button>                                                          
                              <asp:Button ID="btnSave" runat="server" Text="Save"  class="btn btn-primary"></asp:Button>
                              <asp:Button ID="btnClose" runat="server" Text="Close"  class="btn btn-primary"></asp:Button>
                             
                                
                               </div>
                    </div>
                    <div class="col-md-4">
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

