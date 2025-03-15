<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="MissingLogEntry.aspx.cs" Inherits="MissingLogEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
               <section class="content-header">
			  <h1>
				Missing Log Entry
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Missing Log Entry</li>
			  </ol>
			</section>
    <section class="content">


    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Missing Log Entry </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdMissingLogEntry" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
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
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Time")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                                                       
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("EntryID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("EntryID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
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
        $("[id$=grdMissingLogEntry]").prepend($("<thead></thead>").append($("[id$=grdMissingLogEntry]").find("tr:first"))).dataTable();
    </script>





    <div class="modal fade" id="missinglogentry" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">MISSING LOG ENTRY</h4>
              </div>
              <div class="modal-body"> 
                <div class="row">     
                    <div class="col-md-6">
                        <fieldset>
                            <legend>Search</legend>
                        </fieldset>
                        <table>     
                            <tr>  
                                <td style="width:130px;"><label>Dept :</label></td>
                                <td>
                                    <select>
                                        <option></option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Desig :</label>
                                </td>
                                <td>
                                    <select>
                                        <option></option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Search :</label></td>
                                <td>
                                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>  
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered table-hover">
                            <tr>        
                                <th style="width:10px;">
                                    <asp:CheckBox ID="CheckBox1" runat="server" /></th>
                                <th style="width:75px;">Enroll ID</th>
                                <th>Name</th>
                            </tr>
                            <tr>
                                <td><asp:CheckBox ID="CheckBox2" runat="server" /></td>   
                                <td>1</td>
                                <td>Minhaj</td>
                            </tr>
                            <tr>
                                <td><asp:CheckBox ID="CheckBox3" runat="server" /></td>   
                                <td>2</td>
                                <td>Amin Shah</td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-6">
                        <fieldset>
                            <legend>Missing Log Entry</legend>
                        </fieldset>
                        <table>  
                            <tr>
                                <td style="width:70px;"><label>From :</label></td>
                                <td>
                                    <select>
                                        <option>18/ Jun / 2014</option>
                                    </select>
                                </td>
                                <td>
                                    <label>To :</label>
                                </td>
                                <td>
                                    <select>
                                        <option>18/ Jun / 2014</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Status :</label>
                                </td>
                                <td>
                                    <select>
                                        <option>In</option>
                                    </select>
                                </td>
                                <td colspan="2">&nbsp;</td>

                            </tr>
                            <tr>
                                <td>
                                    <label>Time :</label>
                                </td>
                                <td>
                                    <select>
                                        <option>08:00:00 AM</option>
                                    </select>
                                </td>
                                <td colspan="2">&nbsp;</td>

                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td> 
                                    <label>Employee :</label>
                                </td>
                                <td>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <label>Name :</label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                  </div>
              </div>

              <div class="modal-footer">
                <asp:Button ID="btnClose" runat="server"  class="btn btn-primary" Text="Close" data-dismiss="modal"></asp:Button>
                <asp:Button ID="btnSave" runat="server" Text="Save"  class="btn btn-primary"></asp:Button>
                <button type="button" class="btn btn-primary">Apply</button>
              </div>

            </div><!-- /.modal-content -->
          </div><!-- /.modal-dialog -->
        </div>


   

    <%--<div class="row">
        <div class="col-md-12">
            <table class="table table-bordered table-hover">
                <tr>        
                    <th style="width:75px;">
                        Enroll Id
                    </th>
                    <th>Employee Name</th>
                    <th>Date</th>
                    <th>Time</th>
                    <th style="width:5%">Action</th>
                </tr>
                <tr>   
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
                    <td class="action-icon">
                        <a href="#"><i class="fa fa-pencil-square-o"></i></a> &nbsp; 
                        <a href="#"><i class="fa fa-times-circle"></i></a> 
                    </td>
                </tr>
            </table>
          </div>
    </div>--%>



</asp:Content>

