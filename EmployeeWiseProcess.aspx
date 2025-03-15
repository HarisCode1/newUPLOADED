<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="EmployeeWiseProcess.aspx.cs" Inherits="EmployeeWiseProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="content-header">
			  <h1>
				Employee Wise Process
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Employee Wise Process</li>
			  </ol>
			</section>
			<section class="content">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Wise Process </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdEmployeeWiseProcess" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                        <Columns>                            
                            <asp:TemplateField HeaderText="Enroll Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnrollId" runat="server" Text='<%#Eval("Enrollid")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Days">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalDays" runat="server" Text='<%#Eval("TotalDays")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                                                       
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("EWPID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("EWPID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EntityDataSource1"  runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_LoanEntry"
                        Where="it.CompanyId = @CompanyId">
                    </asp:EntityDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

                 </div>
    </div>
        </div>
                </section>
    <script type="text/javascript">
        $("[id$=grdEmployeeWiseProcess]").prepend($("<thead></thead>").append($("[id$=grdEmployeeWiseProcess]").find("tr:first"))).dataTable();
    </script>




            <%--<div class="row">
            <div class="col-md-12">
                <div class="heading">
                   Employee Wise Process
                </div>
                <div class="col-md-4 col-md-offset-4">
                    <table class="branchwise-table">
                        <tr>
                            <td style="width:100px;"><label>For Month of :</label></td>
                            <td colspan="2">
                                <select>
                                    <option>july/2014</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-offset-4"></div>
                <div class="row">
                    <div class="col-md-4 col-md-offset-8">
                        <table>
                            <tr>
                                <td><label>Search :</label></td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table class="table table-bordered table-hover">
                    <tr>             
                        <th style="width:70px;">Enroll Id</th>
                        <th>
                            Name
                        </th>
                        <th>Department</th>
                        <th>Total Days</th>
                        <th>None</th>
                        <th style="width:5%;">
                        Action
                        </th>
                    </tr>
                    <tr>   
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="action-icon">
                        <a href="#"><i class="fa fa-pencil-square-o"></i></a> &nbsp; 
                        <a href="#"><i class="fa fa-times-circle"></i></a> 
                        </td>
                    </tr>
                    <tr>   
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="action-icon">
                        <a href="#"><i class="fa fa-pencil-square-o"></i></a> &nbsp; 
                        <a href="#"><i class="fa fa-times-circle"></i></a> 
                        </td>
                    </tr>
                    <tr>   
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="action-icon">
                        <a href="#"><i class="fa fa-pencil-square-o"></i></a> &nbsp; 
                        <a href="#"><i class="fa fa-times-circle"></i></a> 
                        </td>
                    </tr>
                    <tr>   
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td class="action-icon">
                        <a href="#"><i class="fa fa-pencil-square-o"></i></a> &nbsp; 
                        <a href="#"><i class="fa fa-times-circle"></i></a> 
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="pull-right">
                            <button type="button" class="gridadd-btn btn btn-primary">Save</button>
                            <button type="button" class="gridadd-btn btn btn-primary">Close</button>
                        </td>
                    </tr>
                </table>
              </div>
        </div>--%>
</asp:Content>

