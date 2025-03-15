<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="BankInformation.aspx.cs" Inherits="BankInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/jquery.validator.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <section class="content-header">
			  <h1>
				Bank Information
			  <small>Main View</small>
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Bank Info</li>
			  </ol>
			</section>
			<section class="content">
   


     <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
            <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-primary" Text="Add New" OnClick="btnAddnew_Click1" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>



    <div class="modal fade" id="bankinformation" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
			<div class="modal-dialog" role="document">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
						<h4 class="modal-title" id="myModalLabel">Bank Information</h4>
					</div>
                    
					<div id="pnlDetail" runat="server" class="modal-body">
						<table class="all-table all-tables">
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
                                    <td style="width: 120px;">
                                        <label>Bank Name :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a branch name'></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Branch Code :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a Short name'></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Address :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a branch phone'></asp:TextBox>

                                    </td>
                                </tr>
                               
                                
                            </table>
					</div>
						<div class="modal-footer">
							<asp:Button ID="btnSave" runat="server" class="btn btn-primary" data-dismiss="modal" OnClick="btnClose_Click" Text="Close"></asp:Button>
							<asp:Button ID="btnClose"  runat="server" class="btn btn-primary" OnClick="btnSave_Click" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
						</div>
				</div>
                    <!-- /.modal-content -->
                    </ContentTemplate>
                    </asp:UpdatePanel>
			</div>
        <!-- /.modal-dialog -->
		</div>
<!-- /.modal -->



    
    
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
                        <h3 class="box-title">Bank Information</h3>
                        
                    </div>
                <div class="box-body">
                    <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdBankInfo"  runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdBankInfo_RowCommand" OnPageIndexChanging="grdBankInfo_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <HeaderStyle Width="3%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("BankID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Bank Name">
                                <HeaderStyle Width="22%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BankName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IFSC Code">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" runat="server" Text='<%#Eval("IFSCCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <HeaderStyle Width="30%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Address")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("BankID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="confirm" CommandArgument='<%#Eval("BankID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                    
                    <asp:EntityDataSource ID="EDS_BankInfo" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Bank"
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
            $("[id$=grdBankInfo]").prepend($("<thead></thead>").append($("[id$=grdBankInfo]").find("tr:first"))).dataTable();
        }
    </script>
</asp:Content>

