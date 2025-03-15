<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="Reminder.aspx.cs" Inherits="Reminder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <section class="content-header">
			  <h1>
			
			  </h1>
			  
			</section>
			<section class="content">



    
    <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel ID="upReminder" runat="server">
                    <ContentTemplate>
                <div class="heading">
                   Reminder
                </div>
                <div class="row">
                    <div class="col-md-7 col-md-offset-3">
                       <div class="row">
                            
                            <div class="col-md-6">
                                <table>  
                                   <tr>
                                       <td><label>Date:</label></td>
                                       <td>
                                           <div id="dtpFromDate" class="input-group date">
                                                <asp:TextBox ID="txtDate" runat="server"  AutoPostBack="True"  ></asp:TextBox>
                                                <span class="add-on input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                </span>
                                            </div>                 
                                       </td>
                                       
                                   </tr>
                               </table>
                                     
                                <br />
                            </div>
                       </div>
                        <div class="row">
                           <div class="col-md-6">
                               <fieldset>
                                   <legend>Today birthdays</legend>
                               </fieldset>
                                <asp:ListBox ID="lsbEmployeeBirthDay"  runat="server"/>
                           </div>
                           <div class="col-md-6">
                               <fieldset>
                                   <legend>Today Wedding Annv.</legend>
                               </fieldset>
                                <asp:ListBox ID="lsbEmployeeWedding"  runat="server"/>
                           </div>
                       </div>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                        <asp:Button ID="btnShow" runat="server"  OnClick="btnShow_OnClick" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" Width="0" Height="0" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
              </div>
        </div>


    </section>

    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script>

        $(function () {
            BindData();
        });

        function BindData() {
            $('#dtpFromDate').datetimepicker(
            {
                pickDate: true,
                pickTime: false,
            }).on('changeDate', function (ev) {
                $('[id $=btnShow]').click();
                $('#dtpFromDate').datetimepicker('hide');
            });
        }
    </script>
      
</asp:Content>

