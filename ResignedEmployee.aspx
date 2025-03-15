<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="ResignedEmployee.aspx.cs" Inherits="ResignedEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <section class="content-header">
        <h1>List Of Resigned Employee </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="TerminatedEmployee.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Resigned Employee</li>
        </ol>
    </section>
     <section class="content">        
            <div id="companygrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Resigned Employees 
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="Updateterminatedemp" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdresgemp" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" >
                                        <Columns>
                                            <asp:BoundField DataField="EnrollId" HeaderText="EmployeeID" HeaderStyle-Width="5%" />
                                           
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="25%" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="AppliedDate" HeaderText="Applied Date" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="TerminationDate" HeaderText="Approved Date" HeaderStyle-Width="15%" />
                                              <asp:BoundField DataField="Image" Visible="false" HeaderText="image" HeaderStyle-Width="5%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                 <asp:LinkButton ID="lbtndetail" ToolTip="Approve" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("EmployeeID")%>' OnCommand="lbtndetail_Command" CausesValidation="false" >
                                <i class="fa fa-check-circle"></i>
                                                    </asp:LinkButton>
                                                    <a href='<%# Eval("Image") %>' id="doc" download='<%# Eval("Image") %>' target="_blank" data-toggle="tooltip" title="Resignation Document" runat="server"><i class="fa fa-file"></i></a>
                                                </ItemTemplate>

                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        
        </div>
   
     </section>
</asp:Content>

