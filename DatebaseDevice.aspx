<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="DatebaseDevice.aspx.cs" Inherits="DatebaseDevice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <section class="content-header">
			  <h1>
				Database Device
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Database Device</li>
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



    <div class="modal fade" id="databasedevice" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">DATABASE DEVICE</h4>
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
                                    <td style="width: 130px;">
                                        <label>Device Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlDevCompany" CssClass="form-control" runat="server">
                                            <asp:ListItem>Anviz</asp:ListItem>
                                            <asp:ListItem>NAC</asp:ListItem>
                                            <asp:ListItem>Virdi</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Device Name :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtDeviceName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a device name'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>DB Type :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server">
                                            <asp:ListItem>MSSQL</asp:ListItem>
                                            <asp:ListItem>MSACCESS</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>DB String\Path :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtStringPath" CssClass="form-control" runat="server"></asp:TextBox><asp:FileUpload ID="FileUpload1" runat="server" />
                                        <button type="button" data-toggle="modal" data-target="#mysqldialog" class="gridadd-btn pull-right btn btn-primary">...</button>
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


    



    <div class="modal fade" id="mysqldialog" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpMySQL" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Details</h4>
                        </div>
                        <div id="pnlMySQL" runat="server" class="modal-body">
                            <table class="all-tables">
                                <tr>
                                    <td style="width: 130px;">
                                        <label>Authentication :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlAuthentication" CssClass="form-control" runat="server">
                                            <asp:ListItem>SQL Server</asp:ListItem>
                                            <asp:ListItem>Windows</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Server Name :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtServerName" runat="server" validate='vgroupMySQL' require='Please enter a server name'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Database :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtDatabase" runat="server" validate='vgroupMySQL' require='Please enter a database name'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>User Name :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" validate='vgroupMySQL' require='Please enter a user name'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Password :</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" Width="288px" TextMode="Password" runat="server" validate='vgroupMySQL' require='Please enter a password'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDispose" runat="server" CssClass="btn btn-default" Text="Close" OnClick="btnDispose_Click" />
                            <asp:Button ID="btnSaveMySQL" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSaveMySQL_Click" OnClientClick="if(validate('vgroupMySQL')){return true;}else{return false;}" />
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
                                        <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' OnSelectedIndexChanged="ddlComp_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
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
                        <h3 class="box-title">Database Device </h3>
                        
                    </div>
                <div class="box-body">
                    <div class="table-responsive">

                    
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdDatabaseDevice" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdDatabaseDevice_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Device Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceName" runat="server" Text='<%#Eval("DeviceName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DB Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblDBType" runat="server" Text='<%#Eval("DBType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ConStr">
                                <ItemTemplate>
                                    <asp:Label ID="lblConStr" runat="server" Text='<%#Eval("ConStr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Device Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeviceCompany" runat="server" Text='<%#Eval("DeviceCompany")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("NacDevID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("NacDevID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_DatabaseDevice" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_NACDeviceConfig"
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
            bindLoadData();
        });

        function bindLoadData() {
            if ($("#ddlCompany").val() == "NAC" || $("#ddlCompany").val() == "Virdi") {
                $("#ddlType").prop("disabled", true);
            }


            $("[id$=grdDatabaseDevice]").prepend($("<thead></thead>").append($("[id$=grdDatabaseDevice]").find("tr:first"))).dataTable();



            if ($("[id$=ddlAuthentication]").val() == "Windows")
                $("#txtUserName,#txtPassword").hide();
            else
                $("#txtUserName,#txtPassword").show();

        }
        





        
        </script>


    


</asp:Content>

