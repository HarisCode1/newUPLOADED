<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_Resignation.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>Employee Resignation</h1>
               <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="employee.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Employee Resignation</li>
        </ol>
    </section>

    <section class="content">
          <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Resignation</h3>
                    </div>
                    <div class="box-body">
                            
                        <asp:Panel runat="server" ID="ApplySection" Visible="false">
                                             <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static"  CssClass="form-control input-sm" runat="server" validate='vgroup'  require='Please Select Employee' AppendDataBoundItems="True" style="width:400px;" >
                                                    </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredddlEmployee" ControlToValidate="ddlEmployee" InitialValue="0" runat="server" ErrorMessage="Required enter Remarks" ForeColor="Red"></asp:RequiredFieldValidator>
                                  <asp:Label ID="lblrecieved" text="Receiving Document" runat="server"></asp:Label>
                             <asp:HiddenField ID="hdEmpPhotoID" runat="server" />
                             <asp:HiddenField ID="hdImageName" runat="server" />

                                       
                                 <asp:FileUpload ID="UploadDocImage" runat="server" />
                            <%-- <asp:RequiredFieldValidator ID="RequiredUploadDocImage" runat="server"
            ControlToValidate="UploadDocImage" ErrorMessage="Receiving Letter File Required!" ForeColor="Red">
        </asp:RequiredFieldValidator>--%>
                             <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            <asp:TextBox ID="txtReason" CssClass="form-control mb-15" runat="server" TextMode="MultiLine" placeholder="Reason" Rows="8" style="margin-top:6px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Requiredreason" ControlToValidate="txtReason" runat="server" ErrorMessage="Required enter Reason" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtRemarks" CssClass="form-control mb-15" runat="server" TextMode="MultiLine" placeholder="Remarks" Rows="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="Requiredremarks" ControlToValidate="txtRemarks" runat="server" ErrorMessage="Required enter Remarks" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnApply" runat="server" Text="Apply For Resignation" CssClass="btn btn-payroll pull-right" OnClick="btnApply_Click"/>
                        </asp:Panel>
                        
                        
                        <%--<asp:Panel runat="server" ID="RevertSection" Visible="false">
                            <div class="table-responsive">
                                <table class="table table-striped">
                                    <tbody>
                                      
                                        <tr>
                                            <th>
                                                Reason
                                            </th>
                                            <td>
                                                <asp:Label ID="lblReason" runat="server" Text="Label"></asp:Label>

                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                Remarks
                                            </th>
                                            <td>
                                                <asp:Label ID="lblRemarks" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                Applied Date
                                            </th>
                                            <td>
                                                <asp:Label ID="lblAppliedDate" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                          <tr>
                                            <th>
                                                Status
                                            </th>
                                            <td>
                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                            <asp:Button ID="btnRevert" runat="server" Text="Revert Resignation" CssClass="btn btn-payroll pull-right" OnClick="btnRevert_Click"/>

                        </asp:Panel>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Resigned Employees</h3>
                    </div>
                    <asp:UpdatePanel ID="UpdateResign" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdresignation" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                                        <Columns>
                                     <asp:BoundField DataField="EnrollId" HeaderText="EmployeeID" HeaderStyle-Width="5%" /><%--<asp:BoundField DataField="ResignationId" HeaderText="ID" HeaderStyle-Width="5%" />--%>
                                            <asp:BoundField DataField="Image" Visible="false" HeaderText="image" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="AppliedDate" HeaderText="Applied Date" HeaderStyle-Width="15%" />
                                           
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <%--     <asp:LinkButton ID="lnkView" ToolTip="View" runat="server" Text="View" CommandArgument='<%#Eval("ResignationId")%>' CausesValidation="false"  OnCommand="lnkView_Command">
                                <i class="fa fa-eye"></i></asp:LinkButton>--%>
                                           
		 <a href='<%# Eval("Image") %>' id="doc" download='<%# Eval("Image") %>' target="_blank" data-toggle="tooltip" title="Resignation Document" runat="server"><i class="fa fa-file"></i></a>
                                                    

                                                    <%--<asp:LinkButton ID="lnkDownload" CssClass="myLink" runat="server" CommandArgument='<%# Eval("ResignationId") %>' OnCommand="lnkDownload_Command" CausesValidation="false">Download</asp:LinkButton>--%>
                                      <%--              <asp:LinkButton ID="lbtnEdit" ToolTip="Approve" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("ResignationId")%>' OnCommand="lbtnEdit_Command" CausesValidation="false">
                                <i class="fa fa-check-circle"></i>
                                                    </asp:LinkButton>--%>
                                                    &nbsp;                        
                         
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                       DataSourceID="EDS_Company"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </div>
            </div>
        </div>
        

    </section>
        <section class="content-header">
            </section>
   <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script src="assets/js/bootstrap-switch.min.js"></script>
    <script src="Scripts/select2.min.js"></script>
    <script src='js/fullcalendar.min.js'></script>
    <script>
       $(document).ready(function () {

            $("#<%=ddlEmployee.ClientID%>").select2({

              placeholder: "Select Item",        
              allowClear: true

          });

      });
</script>
</asp:Content>


