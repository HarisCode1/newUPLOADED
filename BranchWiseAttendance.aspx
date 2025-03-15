<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="BranchWiseAttendance.aspx.cs" Inherits="BranchWiseAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
             <section class="content-header">
			  <h1>
				Branch Wise Attendance
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Branch Wise Attendance</li>
			  </ol>
			</section>
			<section class="content">
     

                <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
           <asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#branchwiseattendance" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>
          
        </div>
    </div>

                <div class="modal fade" id="branchwiseattendance" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg">
              <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Branch Wise Attendance</h4>
              </div>
              <div id="pnlDetail" runat="server" class="modal-body"> 
                <div class="row">     
                    <div class="col-md-offset-3 col-md-6">
                        <%--<fieldset>
                            <legend>Search</legend>
                        </fieldset>
                        <table>     
                            <tr>
                                <td><label>Search :</label></td>
                                <td>
                                    <asp:TextBox ID="txtSearch" autocomplete="off" ClientIDMode="Static" runat="server" placeholder="Search employee"></asp:TextBox>
                                    <asp:HiddenField ID="hdnEmpID" ClientIDMode="Static" runat="server" />
                                </td>
                            </tr>
                        </table> --%>                                        
                        <fieldset>
                            <legend>Coff Entry</legend>
                        </fieldset>
                        <table>
                            <tr id="trCompany" runat="server">
                                    <td style="width:120px;">
                                        <label>Company :</label> </td>
                                    <td>
                                    <asp:TextBox ID="txtCompany" runat="server"  ReadOnly="true" ></asp:TextBox></td>
                                </tr>
                            <tr>
                                <td style="width:130px;"><label>Date :</label></td>
                                <td>
                                    <div id="dtpDate" class="input-group date" style="width: 140px;">
                                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Employee :</label></td>
                                <td>
                                    <div class="loanentry-txtsmall">
                                                    <asp:TextBox ID="txtEmpID" ClientIDMode="Static" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="loanentry-txtlarge">
                                                    <asp:TextBox ID="txtEmpName" autocomplete="off" ClientIDMode="Static" runat="server" validate="vgroup" placeholder="Search employee" require="Please select employee"></asp:TextBox>
                                                </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Balance :</label></td>
                                <td>
                                    <asp:TextBox ID="txtBalance" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>From :</label></td>
                                <td>
                                   <div class="pull-left leavapp-from">
                                        <div id="dtpFromDate" class="input-group date" style="width: 140px;">
                                                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
                                                </div>
                                    </div> 
                                    <div class="pull-left leavapp-fromcheckbox">
                                        <asp:CheckBox ID="chkFromHalfDay" runat="server" CssClass="checkboxbtn" Text="Half Day" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label>To :</label></td>
                                <td>
                                   <div class="pull-left leavapp-from">
                                        <div id="dtpToDate" class="input-group date" style="width: 140px;">
                                                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
                                                </div>
                                    </div> 
                                    <div class="pull-left leavapp-fromcheckbox">
                                        <asp:CheckBox ID="chkToHalfDay" runat="server" CssClass="checkboxbtn" Text="Half Day" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Total  Leaves :</label></td>
                                <td>
                                    <asp:TextBox ID="txtTotalLeaves" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Reason :</label></td>
                                <td>
                                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Style="resize:none;" Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                  </div>
              </div>
              <div class="modal-footer">
                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-default" Text="Close" OnClick="btnClose_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
              </div>
            </div><!-- /.modal-content -->
                    </ContentTemplate>
                  </asp:UpdatePanel>
          </div><!-- /.modal-dialog -->
        </div>


    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Branch Wise Attendance </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdBranchWiseAttendance" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
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
                            <asp:TemplateField HeaderText="Loan Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblPresent" runat="server" Text='<%#Eval("Present")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMI Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount Month">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("ID")%>' CommandName="DeleteCompany">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--<asp:EntityDataSource ID="EntityDataSource1"  runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_LoanEntry"
                        Where="it.CompanyId = @CompanyId">
                    </asp:EntityDataSource>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

     </div>
    </div>
        </div>
                </section>
    <script type="text/javascript">
        $("[id$=grdBranchWiseAttendance]").prepend($("<thead></thead>").append($("[id$=grdBranchWiseAttendance]").find("tr:first"))).dataTable();
    </script>
    
   <%-- <div class="row">
            <div class="col-md-12">
                <div class="heading">
                   Branch Wise Attendance 
                </div>
                <div class="col-md-6 col-md-offset-3">
                    <table class="branchwise-table">
                        <tr>
                            <td style="width:100px;"><label>Month/Year :</label></td>
                            <td colspan="2">
                                <select>
                                    <option>1</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-offset-3"></div>
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
                            <asp:Button ID="btnSave" runat="server" class="gridadd-btn btn btn-primary" Text="Save"></asp:Button>
                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click"  class="gridadd-btn btn btn-primary" Text="Close"></asp:Button>
                        </td>
                    </tr>
                </table>
              </div>
        </div>--%>
</asp:Content>

