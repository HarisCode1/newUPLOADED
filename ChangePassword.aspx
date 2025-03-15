<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <section class="content-header">
			  <h1>
			
			  </h1>
			  
			</section>
			<section class="content">

    
    <div class="row">
        
            <div class="col-md-12">
                <asp:UpdatePanel ID="upChangePassword" runat="server">
                    <ContentTemplate>

                    
                <div class="heading">
                   Change Password
                </div>
                <div class="row">
                    <div class="col-md-5 col-md-offset-3">
                        
                       <table>
                           
                           <tr>
                               <td style="width:150px;"><label>Current Password :</label></td>
                               <td>
                                   <asp:TextBox ID="txtOldPassword" validate='vgroup' require='Please Enter Old Password'  Type="password"  runat="server"></asp:TextBox>
                               </td>
                           </tr>
                           <tr>

                               <td>
                                   <label>New Password :</label>
                               </td>
                               <td>
                                   <asp:TextBox ID="txtNewPassword" validate='vgroup' require='Please Enter New Password' Type="password" runat="server"></asp:TextBox>
                               </td>
                           </tr>
                           <tr>
                               <td><label>Re Type :</label></td>
                               <td>
                                   <asp:TextBox ID="txtReType" validate='vgroup' require='Please Re-Enter Password' Type="password" runat="server"></asp:TextBox>
                               </td>
                           </tr>
                           
                       </table>
                        <div class="modal-footer">
                        <tr>
                               <td>&nbsp;</td>
                               <td class="pull-right">

                                     <asp:Button ID="btnChange" runat="server"  class="btn btn-primary" Text="Change"  OnClientClick="if(validate('vgroup')){return true;}else{return false;} PasswordLength();" OnClick="btnChange_Click" ></asp:Button>
                                     <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click"  />
                            
                               </td>
                           </tr>
                                                    
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                        </ContentTemplate>
                </asp:UpdatePanel>
              </div>
       
        </div>

    </section>

    <script type="text/javascript">

        function PasswordLength() {
            var plength = $('[id $ = txtNewPassword]').val();
        }

        </script>
        
</asp:Content>

