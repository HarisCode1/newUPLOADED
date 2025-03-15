<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="PT.aspx.cs" Inherits="PT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <section class="content-header">
			  <h1>
				PT
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">PT</li>
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


    <div class="modal fade" id="PT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">PT</h4>
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
                                    <td style="width: 150px">
                                        <label>State :</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server" >
                                            <asp:ListItem>Sindh</asp:ListItem>
                                            <asp:ListItem>Punjab</asp:ListItem>
                                            <asp:ListItem>KPK</asp:ListItem>
                                            <asp:ListItem>Balochistan</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">
                                        <label>Min Salary :</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMinSalary" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a min salary'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">
                                        <label>Max Salary :</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaxSalary" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a max salary'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">
                                        <label>P Tax :</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPTax" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a PTax'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkSpecialCalcApplicable" runat="server" CssClass="checkboxbtn" Text="Special Calc Applicable" />
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


    <div class="row" id="divCompany" runat="server">
        <div class="col-md-6" >

            <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                                <tr>
                                    <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" CssClass="form-control" runat="server" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
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



    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">PT </h3>
                        
                    </div>
                <div class="box-body">
                    <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdPTSettings" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdPTSettings_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("PTSettingsID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("State")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="MinSalary">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblMinSalary" runat="server" Text='<%#Eval("MinSalary")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MaxSalary">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblMaxSalary" runat="server" Text='<%#Eval("MaxSalary")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>     
                            <asp:TemplateField HeaderText="PTax">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblPTax" runat="server" Text='<%#Eval("PTax")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                       
                            <asp:TemplateField HeaderText="Action">                                
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("PTSettingsID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("PTSettingsID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_PTSettings" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_PTSettings"
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
            $("[id$=grdPTSettings]").prepend($("<thead></thead>").append($("[id$=grdPTSettings]").find("tr:first"))).dataTable();
        }
    </script>
</asp:Content>

