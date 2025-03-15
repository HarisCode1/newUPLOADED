<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="SalaryRateAllotment.aspx.cs" Inherits="SalaryRateAllotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="content-header">
			  <h1>
				Salary Rate Allotment
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Salary Rate Allotment</li>
			  </ol>
			</section>
			<section class="content">

                
     <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
            <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-primary" Text="Add New" OnClick="BtnAddNew_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>



      <div class="modal fade" id="SalaryRateAllotment" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Salary Rate Allotment</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <table class="all-tables">
                                <tr id="trCompany" runat="server">
                                    <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;'  AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Please Select Company"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:EntityDataSource ID="EDS_Comp" runat="server"
                                            ConnectionString="name=vt_EMSEntities"
                                            DefaultContainerName="vt_EMSEntities"
                                            EntitySetName="vt_tbl_Company">
                                        </asp:EntityDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Category :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a leave name'></asp:TextBox>
                                    </td>
                                </tr>
                                
                                
                            </table>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.modal-dialog -->
    </div>










                  <%--<div class="row">
                    <div class="col-md-12">
                <div class="heading">
                   Salary Rate Allotment
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <table>
                            <tr>
                                <td style="width:100px;"><label>Rate Type :</label></td>
                                <td colspan="2">
                                    <select>
                                        <option>Monthly</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Increment/Decrement" CssClass="checkboxbtn" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-5 col-md-offset-3">
                         <table>  
                            <tr>
                                <td style="width:100px;"><label>Search :</label></td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:100px;"><label>Salary Head :</label></td>
                                <td style="width:270px;">
                                    <div class="pull-left salary-salryhead">
                                        <select>
                                            <option></option>
                                        </select>
                                    </div>
                                    <div class="pull-left salary-salryheartxt">
                                         <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Apply" class="pull-right btn btn-primary" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
               
              </div>
        </div>--%>




    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Salary Rate Allotment </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdSalaryRateAllotment" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
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
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("JoiningDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Salary")%>'></asp:Label>
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

        $(function () {
            binddata();
        });


        function binddata() {
            $("[id$=grdSalaryRateAllotment]").prepend($("<thead></thead>").append($("[id$=grdSalaryRateAllotment]").find("tr:first"))).dataTable();
        }
    </script>
    
</asp:Content>

