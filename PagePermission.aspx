<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="PagePermission.aspx.cs" Inherits="PagePermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="content-header">
			  <h1>
				Page Permission
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Page Permission</li>
			  </ol>
			</section>
			<section class="content">


    <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
           <asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#PagePermission" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>
          
        </div>
    </div>




    <div class="modal fade" id="PagePermission" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg">
              <asp:UpdatePanel ID="upPagePermission" runat="server" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <script type="text/javascript">
                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                prm.add_endRequest(function () {
                                    binddata();
                                });
                            </script>
            <div class="modal-content">
                
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Roles Permission</h4>
              </div>
              <div id="pnlDetail" runat="server" class="modal-body">
                  <div class="row">     
                    <div class="col-md-6 col-md-offset-3">
                        <table>
                            <tr>
                                <td>
                                    <label> Role Name :</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoleName" CssClass="form-control" runat="server" validate='vgroup' require='Please Enter a Role'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                  </div>

                  <div class="row">     
                    <div class="col-md-12">
                    <asp:GridView ID="grdPagePermission" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PageName">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnPageID" runat="server" Value='<%# Eval("PageID") %>' />
                                        <asp:label id="lblpagename" runat="server" text='<%# Eval("PageName") %>'></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:CheckBox CssClass="chkPermission" ID="chkView" runat="server" Checked='<%# Eval("Can_View") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add New/Save">
                                    <ItemTemplate>
                                        <asp:CheckBox CssClass="chkPermission" ID="chkInsert" runat="server" Checked='<%# Eval("Can_Insert") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update">
                                    <ItemTemplate>
                                        <asp:CheckBox CssClass="chkPermission" ID="chkUpdate" runat="server" Checked='<%# Eval("Can_Update") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:CheckBox CssClass="chkPermission" ID="chkDelete" runat="server" Checked='<%# Eval("Can_Delete") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>                        
                  </div>
                 </div>
                </div>
              <div class="modal-footer">
                  <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click"  />
                  <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"  OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnSave_Click"  />
              </div>
            
            </div><!-- /. modal-content -->
               </ContentTemplate>
         </asp:UpdatePanel>  
          </div><!-- /.modal-dialog -->
        </div>


    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Page Permission </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdRolesPermission" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="grdRolesPermission_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Role ID">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleID" runat="server" Text='<%#Eval("RoleID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRole" runat="server" Text='<%#Eval("Role")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("RoleID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("RoleID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
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

        <script type="text/javascript">
        function binddata() {
            $("#lnkAdmin").parent("li").addClass("active");

            $("[id*=chkSelect]").change(function () {
                $(this).parents("tr").find("input[type=checkbox]").prop("checked", this.checked);
            });

            $("[id *= chkSelect]").parents("tr").each(function () {
                var chkTotalCounter = $(this).find("input[type=checkbox]").length - 1;

                $(this).find("[id *= chkSelect]").prop("checked", ($(this).find(".chkPermission>input:checked").length == chkTotalCounter));
            });

            $(".chkPermission").change(function () {
                var tr = $(this).parents("tr");
                var chkTotalCounter = tr.find("input[type=checkbox]").length - 1;

                tr.find("[id *= chkSelect]").prop("checked", (tr.find(".chkPermission>input:checked").length == chkTotalCounter));
            });
        }

        $(function () {
            binddata();
        });


        function binddata() {
            $("[id$=grdRolesPermission]").prepend($("<thead></thead>").append($("[id$=grdRolesPermission]").find("tr:first"))).dataTable();
        }


        </script>


        
  
</asp:Content>

