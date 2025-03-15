<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="LeaveAppNotification.aspx.cs" Inherits="LeaveAppNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="content-header">
			  <h1>
			
			  </h1>
			  
			</section>
			<section class="content">






    <div class="row" >
        <div class="col-sm-12 ">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
            <asp:Button ID="btnOtherLeave" runat="server" data-toggle="modal" data-target="#OtherLeave" CssClass=" pull-right btn btn-primary" Text="Other Leaves" OnClick="btnOtherLeave_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>


    




    <%--Model Grid--%>
    <div class="modal fade" id="OtherLeave" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Other Leaves</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            
                        <div class="box box-info custom_input">
                            <div class="box-header with-border">
                        <%--<i class="fa fa-table custom_header_icon admin_icon"></i>--%>
                        <h3 class="box-title"></h3>
                        
                    </div>
                <div class="box-body">
                    <div class="table-responsive">
           <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdOtherLeave"  runat="server" AllowPaging="true" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" >
                        <Columns>
                            <asp:TemplateField HeaderText="FromDate">
                                <HeaderStyle Width="3%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="ToDate">
                                <HeaderStyle Width="30%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTodate" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Reason">
                                <HeaderStyle Width="3%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                             <asp:TemplateField HeaderText="Status">
                                <HeaderStyle Width="3%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLeaveStatus" runat="server" Text='<%#Eval("LeaveStatus")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_LeaveApplctn" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_LeaveApplication"
                        Where="it.CompanyId = @CompanyId">
                    </asp:EntityDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

         </div>

        </div>

             </div>

                  <div class="modal-footer">
                            
							
                           
                  </div>

                    </div>
                    <!-- /.modal-content -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.modal-dialog -->
    </div>




    



    <%--Simple Form--%>
    <div class="row" id="LeaveForm" runat="server">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                        <div class="modal-header">                            
                            <h4 class="modal-title">Leave Notification</h4>
                        </div>
                        <div class="modal-body">
                            <table class="all-table company-table all-tables">

                                <tr>
                                    <td>
                                        Leave Request # :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLeaveRequest" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>



                                <tr>
                                    <td>
                                        Employee Name :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        Designation :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDesignation" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        Leave Date :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFromLeaveDate" Font-Bold="true" runat="server"></asp:Label>
                                        <asp:Label Font-Bold="true" runat="server" Text="To"></asp:Label>
                                        <asp:Label ID="lblToLeaveDate" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                
                                <tr>
                                    <td>
                                        Leave Type :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLeaveType" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        Reason :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReason" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>


                            
                                <tr>
                                    <td>
                                        Comments :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lblComments"  CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        Leave Approval/Reject :
                                    </td>
                                    
                                    <td>
                                        <asp:RadioButton ID="rdbApprove" GroupName="LeaveProcess" runat="server" Text="Approve"/>
                                        <asp:RadioButton ID="rdbReject" runat="server" GroupName="LeaveProcess" Text="Reject" />
                                        <asp:RadioButton ID="rdbPending" runat="server" GroupName="LeaveProcess" Checked="true" Text="Pending" />
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


    <script type="text/javascript">

        $(function () {
            binddata();
        });

        function binddata() {
            $("[id$=grdOtherLeave]").prepend($("<thead></thead>").append($("[id$=grdOtherLeave]").find("tr:first"))).dataTable();
        }
    </script>



</asp:Content>

