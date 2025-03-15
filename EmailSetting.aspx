<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="EmailSetting.aspx.cs" Inherits="EmailSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <section class="content-header">
			  <h1>

				Email Setting 
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Email Setting</li>
			  </ol>
			</section>
			<section class="content">


    <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
          
        </div>
    </div>



       <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
            <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
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





                
        <div class="modal fade" id="EmailSetting" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Email Setting</h4>
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


                           <tr id="trFromEmailId" runat="server">
                               <td style="width:120px;">
                               <label>From Email Id :</label></td>
                               <td>
                                   <asp:HiddenField ID="hdEmailSettingID" runat="server"/>
                                   <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" validate='vgroup' require='Please Enter Email'></asp:TextBox>
                               </td>
                               
                           </tr>
                           <tr>

                               <td>
                                   <label>Password :</label>
                               </td>
                               <td>
                                   <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" validate='vgroup' require='Please Enter Password' TextMode="Password" ></asp:TextBox>
                               </td>
                              
                           </tr>
                           <tr>
                               <td><label>Smtp Server :</label></td>
                               <td>
                                   <asp:TextBox ID="txtSmtp" runat="server" CssClass="form-control" validate='vgroup' require='Please Enter SMTP'></asp:TextBox>
                               </td>
                               
                           </tr>
                           <tr>
                               <td><label>Port :</label></td>
                               <td>
                                   <asp:TextBox ID="txtPort" runat="server" CssClass="form-control" validate='vgroup' require='Please Enter SMTP'></asp:TextBox>
                               </td>
                               
                           </tr>
                       
                   </table>
                        </div>
                         <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_OnClick" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
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
                        <h3 class="box-title">Email Setting </h3>
                        
                    </div>
                <div class="box-body">
                    <div class="table-responsive">

                    
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdEmailSetting"  runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdEmailSetting_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="From Email Id :">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromEmailId" runat="server" Text='<%#Eval("FromEmailId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Password">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Smtp Server">
                                <ItemTemplate>
                                    <asp:Label ID="lblSmtpServer" runat="server" Text='<%#Eval("SmtpServer")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Port">
                                <ItemTemplate>
                                    <asp:Label ID="lblPort" runat="server" Text='<%#Eval("Port")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("SettingID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("SettingID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_EmailSettings" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_EmailSettings">
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
            $("[id$=grdEmailSetting]").prepend($("<thead></thead>").append($("[id$=grdEmailSetting]").find("tr:first"))).dataTable();
        }
    </script>


</asp:Content>

