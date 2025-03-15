<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="PF.aspx.cs" Inherits="PF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="content-header">
			  <h1>
				PF
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">PF</li>
			  </ol>
			</section>
			<section class="content">


    <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
            <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-primary" Text="Add New" OnClick="btnAddnew_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>



    <div class="row" id="divCompany" runat="server">
        <div class="col-md-6" >
            <asp:UpdatePanel ID="updateCompany" runat="server">
                <ContentTemplate>
                    <table>
                                <tr>
                                    <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:EntityDataSource ID="EDS_Company" runat="server"
                                            ConnectionString="name=vt_EMSEntities"
                                            DefaultContainerName="vt_EMSEntities"
                                            EntitySetName="vt_tbl_Company">
                                        </asp:EntityDataSource>
                                    </td>
                                </tr>
                    </table>
               </ContentTemplate>
           </asp:UpdatePanel>
        </div>
    </div>
















    <div class="modal fade" id="PF" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">PF Settings</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <table>
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
                                    <td style="width: 130px">
                                        <label>Applicable From :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtAppFrom" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a applicable from'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="pull-left">
                                <table>
                                    <tr>
                                        <td style="width: 130px">
                                            <label>Employees Share :</label>
                                        </td>
                                        <td>
                                            <div class="pull-left">% &nbsp;&nbsp;</div>
                                            <div class="pull-left pfsetting-percent">
                                                <asp:TextBox ID="txtEmployeesShare" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a employees share'></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="height:40px;"></tr>
                                    <tr>
                                        <td style="width: 130px">
                                            <label>Employers Share :</label>
                                        </td>
                                        <td>
                                            <div class="pull-left">% &nbsp;&nbsp;</div>
                                            <div class="pull-left pfsetting-percent">
                                                <asp:TextBox ID="txtEmployersShare" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a employers share'></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>     
                                    <tr style="height:40px;"></tr>                               
                                    <tr>
                                        <td style="width: 130px">
                                            <label>Pension Fund:</label>
                                        </td>
                                        <td>
                                            <div class="pull-left">% &nbsp;&nbsp;</div>
                                            <div class="pull-left pfsetting-percent">
                                                <asp:TextBox ID="txtPensionFund" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a pension fund'></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="pull-left">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkFixEmployerShare" runat="server" Text="Fix Employer Share" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 165px;">
                                            <label>PF Limit: </label></td>
                                        <td>
                                            <div class="pull-left">Rs &nbsp;</div>
                                            <div class="pull-left pfsetting-percent">
                                                <asp:TextBox ID="txtPFLimit" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a PF Limit'></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>PF Admin Ch: </label>
                                        </td>
                                        <td>
                                            <div class="pull-left">% &nbsp;&nbsp;</div>
                                            <div class="pull-left pfsetting-percent">
                                                <asp:TextBox ID="txtPFAdminCh" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a PF Admin Ch'></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>EDLI Charges: </label>
                                        </td>
                                        <td>
                                            <div class="pull-left">% &nbsp;&nbsp;</div>
                                            <div class="pull-left pfsetting-percent">
                                                <asp:TextBox ID="txtEDLICharges" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a EDLI Charges'></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>EDLI Admin Charges: </label>
                                        </td>
                                        <td>
                                            <div class="pull-left">% &nbsp;&nbsp;</div>
                                            <div class="pull-left pfsetting-percent">
                                                <asp:TextBox ID="txtEDLIAdminCharges" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a EDLI Admin Charges'></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clearfix"></div>
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

    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">PF </h3>
                        
                    </div>
                <div class="box-body">
                    <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdPFSetting" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdPFSetting_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <HeaderStyle Width="3%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("PFSettingID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month Year">
                                <HeaderStyle Width="22%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMonthYear" runat="server" Text='<%#Eval("MonthYear")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EmployeesShare">
                                <HeaderStyle Width="30%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeesShare" runat="server" Text='<%#Eval("EmployeesShare")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EmployersShare">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployersShare" runat="server" Text='<%#Eval("EmployersShare")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("PFSettingID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("PFSettingID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_PFSetting" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_PFSettings"
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
            $("[id$=grdPFSetting]").prepend($("<thead></thead>").append($("[id$=grdPFSetting]").find("tr:first"))).dataTable();
        }
    </script>
</asp:Content>

