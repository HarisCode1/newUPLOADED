<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="EmpContractNotif.aspx.cs" Inherits="EmployeeContractNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="content-header">
			  <h1>
			
			  </h1>
			  
			</section>
			<section class="content">




    <%--Simple Form--%>
    <div class="row" id="EmployeeContractForm" runat="server">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                        <div class="modal-header">                            
                            <h4 class="modal-title">Contract Notification</h4>
                        </div>
                        <div class="modal-body">
                            <table class="all-table company-table all-tables">
                               <tr>
                                    <td>
                                       <div class="form-group" id="Div1" runat="server">
                                         <label class="col-sm-4 control-label">Employee Name:</label>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                            </div>
                                            </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                       <div class="form-group" id="Div2" runat="server">
                                         <label class="col-sm-4 control-label">Joining Date:</label>
                                        <div class="col-sm-8">
                                             <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                            </div>
                                            </div>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td>
                                        <div class="form-group">
                                                <asp:RadioButton ID="rdoProb" Text="Extend: Probation" runat="server" CssClass="col-sm-6"  GroupName="rdoConf_Pro" Checked="true" />
                                                <asp:RadioButton ID="rdoConfirmation" Text="Conf: Employee" runat="server" CssClass="col-sm-6" GroupName="rdoConf_Pro" />
                                            </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                         <div class="form-group" id="Conf" runat="server" style="display:none;">
                                  <label class="col-sm-4 control-label">Confirm Date:</label>
                              <div id="dtConfrDate" class="input-group datetimepick col-sm-8 ">
                              <asp:TextBox ID="txtConfrDate" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="add-on input-group-addon">
                                  <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                  </span>

                                 </div>
                                        </div>
                                    </td>
                                    </tr>

                                <tr>
                                    <td>
                                        <div class="form-group" id="RemDays">
                                            <label class="col-sm-4 control-label">Remaining Days:</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="lblRemDays" runat="server"></asp:Label>
                                            </div> 

                                        </div>

                                    </td>

                                </tr>


                                <tr>
                                    <td>
                                        <div class="form-group" id="Prov" runat="server">
                                         <label class="col-sm-4 control-label">Probation Period:</label>
                                        <div class="col-sm-8">
                                         <asp:Label ID="lblProbationPeriod" Width="50%" runat="server"></asp:Label>
                           
                                            </div>

                                            </div>
                                    </td>
                                   
                                </tr>

                                <tr>

                                    <td>
                                        <div class="form-group" id="ExtendProb">
                                            <label class="col-sm-4 control-label">Extend Probation:</label>
                                            <div class="col-sm-8">
                                            <asp:TextBox ID="txtExtendProbation" placeholder="Probation in Month E.g. 1 or 2"   Width="70%"  runat="server"></asp:TextBox>
                                                
                                           </div>

                                        </div>
                                    </td>



                                </tr>
                                
                                                                                              
                            </table>
                        </div>
                        <div class="modal-footer" id="footer" runat="server">                            
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click"/>                            
                        </div>
                    
                </ContentTemplate>
                <Triggers>                    
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->






</section>

    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script src="js/fullCalender.js"></script>
    <script type="text/javascript">
        function BindData()
        {
            Confr();
            Prov();
            $('#dtConfrDate').datetimepicker({ pickTime: false, autoclose: true });
        }

        function Confr() {
            $('[id $=rdoConfirmation]').change(function () {
                if ($(this).is(":checked")) {
                    $('[id$=Conf]').show();
                    $('[id$=Prov]').hide();
                    $('[id$=ExtendProb]').hide();
                    $('[id$=RemDays]').hide();

                }
            });
        };

        function Prov() {
            $('[id $=rdoProb]').change(function () {
                if ($(this).is(":checked")) {
                    $('[id$=Prov]').show();
                    $('[id$=ExtendProb]').show();
                    $('[id$=Conf]').hide();
                    $('[id$=RemDays]').show();
                }
            });
        };

      
     </script>   

</asp:Content>

