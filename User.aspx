<%@ Page Title="" Language="C#" MasterPageFile="main.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tree {
            min-height: 20px;
            padding: 19px;
            margin-bottom: 20px;
            background-color: #fbfbfb;
            border: 1px solid #999;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
            -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
        }

            .tree li {
                list-style-type: none;
                margin: 0;
                padding: 10px 5px 0 5px;
                position: relative;
            }

                .tree li::before, .tree li::after {
                    content: '';
                    left: -20px;
                    position: absolute;
                    right: auto;
                }

                .tree li::before {
                    border-left: 1px solid #999;
                    bottom: 50px;
                    height: 100%;
                    top: 0;
                    width: 1px;
                }

                .tree li::after {
                    border-top: 1px solid #999;
                    height: 20px;
                    top: 25px;
                    width: 25px;
                }

                .tree li span {
                    -moz-border-radius: 5px;
                    -webkit-border-radius: 5px;
                    border: 1px solid #999;
                    border-radius: 5px;
                    display: inline-block;
                    padding: 3px 8px;
                    text-decoration: none;
                }

                .tree li.parent_li > span {
                    cursor: pointer;
                }

            .tree > ul > li::before, .tree > ul > li::after {
                border: 0;
            }

            .tree li:last-child::before {
                height: 30px;
            }
            /*.tree li.parent_li>span:hover, .tree li.parent_li>span:hover+ul li span {
                changes
    background:#eee;
    border:1px solid #94a0b4;
    color:#000
}*/

            .tree ul {
                margin: 0 0 10px 25px;
            }

        .divexpand {
            float: left;
            margin-left: -8px;
            padding-top: 4px;
        }

            .divexpand > span.glyphicon {
                font-size: 15px;
            }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <section class="content-header">
			  <h1>
				User
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">User</li>
			  </ol>
			</section>
			<section class="content">

                <%-- <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
           <asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#createuser" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>
          
        </div>
    </div>--%>


    <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
            <asp:Button ID="BtnAddNew" runat="server" data-toggle="modal" data-target="#createuser" CssClass="btn btn-primary pull-right" Text="Add New" />
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
                                        <asp:DropDownList ID="ddlComp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' OnSelectedIndexChanged="ddlComp_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:EntityDataSource ID="EDS_Comp" runat="server"
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






    <div class="modal fade" id="createuser" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">USER DETAIL</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-10">                                    
                                    <table>
                                        <tr>
                                            <td>
                                                <label>User Name :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" validate='vgroup' require='Please enter a User name'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Password :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtPwd" TextMode="Password" CssClass="form-control" ClientIDMode="Static" runat="server" validate='vgroup' require='Please enter a Password name'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Re Type :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtRePwd" TextMode="Password" CssClass="form-control" runat="server" validate='vgroup' require='Please enter a Re type Password' compare='password mismatch' compareTo='txtPwd'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trddlcompany" runat="server">
                                            <td>
                                                <label>Company</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' custom="Please Select Company" customFn='var r = parseInt(this.value); return r > 0;'>
                                                        <asp:ListItem Value="0" Text="Please Select Company"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:EntityDataSource ID="EDS_Company" runat="server"
                                                    ConnectionString="name=vt_EMSEntities"
                                                    DefaultContainerName="vt_EMSEntities"
                                                    EntitySetName="vt_tbl_Company">
                                                </asp:EntityDataSource>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Role</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlRole" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Role" DataTextField="Role" DataValueField="RoleID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;'>
                                                </asp:DropDownList>
                                                <asp:EntityDataSource ID="EDS_Role" runat="server"
                                                    ConnectionString="name=vt_EMSEntities"
                                                    DefaultContainerName="vt_EMSEntities"
                                                    EntitySetName="vt_tbl_Role">
                                                </asp:EntityDataSource>
                                                <div class="btn-group">                                                    
                                                    <asp:LinkButton ID="btnDeleteRole" ClientIDMode="Static" runat="server" CssClass="gridadd-btn pull-right btn btn-primary confirm" Text="Delete Role" OnClick="btnDeleteRole_Click" Visible="false"></asp:LinkButton>
                                                    <asp:Button ID="btnNewRole" ClientIDMode="Static" runat="server" CssClass="gridadd-btn pull-right btn btn-primary" Text="New Role" OnClick="btnEditRole_Click" Visible="false"/>
                                                    <asp:Button ID="btnEditRole" ClientIDMode="Static" runat="server" CssClass="gridadd-btn pull-right btn btn-primary" Text="Edit Role" OnClick="btnEditRole_Click" Visible="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
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
 
    <div class="modal fade" id="createrole" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UproleDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">CREATE ROLE</h4>
                        </div>
                        <div id="pnlrDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-10">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ClientIDMode="Static" ID="hdnSelectedVal" runat="server" />
                                                <asp:HiddenField ClientIDMode="Static" ID="hdnRoleID" runat="server" Value="-1" />
                                                <label>Role Name :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtRoleName" runat="server" validate='rgroup' require='Please enter a Role name'></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="tree well" runat="server" id="Mytree">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnrClose" runat="server" CssClass="btn btn-default" Text="Close" OnClick="btnrClose_Click" />
                            <asp:Button ID="btnrSave" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Save" OnClick="btnrSave_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
             <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">User </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdUser" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdUser_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("EmployeeID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("EmployeeID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_User" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Employee"
                        Where="it.CompanyId = @CompanyId">
                    </asp:EntityDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-offset-3"></div>
    </div>
                   </div>
    </div>
        </div>
                </section>
    
        

     <script src="js/JSonHelper.js"></script>
    <script type="text/javascript">
        var CData = [];

        $(function () {
            loadRoleData();
        });

        function loadRoleData() {
            $("#btnEditRole").click(function () {
                if (parseInt($("#ddlRole").val()) > 0) {
                    $("#hdnRoleID").val($("#ddlRole").val());
                    return true;
                }
                else {
                    $("#hdnRoleID").val("-1");
                    return false;
                }
            });

            $("#btnNewRole").click(function () {
                $("#hdnRoleID").val("-1");
            });

            $(".parent_li input[type=checkbox][id^=chkAll]").change(function () {
                $(this).closest("span").find("input[type=checkbox]").not(this).prop("checked", this.checked);
                if ($(this).closest("span").siblings("ul").length) {
                    $(this).closest("span").siblings("ul").find("input[type=checkbox][id^=chkAll]").prop("checked", this.checked).change();
                }                
            });

            $(".parent_li > span input[type=checkbox][id^=chkView]").change(function () {
                $(this).closest("span").siblings("ul").find("input[type=checkbox][id^=chkView]").prop("checked", this.checked).change();
            });

            $(".parent_li > span input[type=checkbox][id^=chkInsert]").change(function () {
                $(this).closest("span").siblings("ul").find("input[type=checkbox][id^=chkInsert]").prop("checked", this.checked).change();
            });

            $(".parent_li > span input[type=checkbox][id^=chkUpdate]").change(function () {
                $(this).closest("span").siblings("ul").find("input[type=checkbox][id^=chkUpdate]").prop("checked", this.checked).change();
            });

            $(".parent_li > span input[type=checkbox][id^=chkDelete]").change(function () {
                $(this).closest("span").siblings("ul").find("input[type=checkbox][id^=chkDelete]").prop("checked", this.checked).change();
            });

            $('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');

            $('.tree li.parent_li .divexpand').each(function (e) {
                var pli = $(this).parent("span");
                var children = $(pli).parent('li.parent_li').find(' > ul > li');
                children.hide('fast');
                $(pli).attr('title', 'Expand this branch').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');
            });

            $('.tree li.parent_li .divexpand').on('click', function (e) {
                var pli = $(this).parent("span");
                var children = $(pli).parent('li.parent_li').find(' > ul > li');
                if (children.is(":visible")) {
                    children.hide('fast');
                    $(pli).attr('title', 'Expand this branch').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');
                } else {
                    children.show('fast');
                    $(pli).attr('title', 'Collapse this branch').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');
                }
                e.stopPropagation();
            });

            $("#btnrSave").click(function () {
                if (validate('rgroup')) {
                    $("[id$=Mytree] input[type=checkbox]").each(function () {
                        var id = this.id.split('_');
                        var currentrow;
                        if (CData.length) {
                            currentrow = CData.filter(filter, ["ID", id[1]])[0];
                        }

                        if (!currentrow) {
                            currentrow = { ID: id[1], View: false, Insert: false, Update: false, Delete: false };
                            CData.push(currentrow);
                        }

                        switch (id[0]) {
                            case "chkView":
                                currentrow.View = this.checked;
                                break;
                            case "chkInsert":
                                currentrow.Insert = this.checked;
                                break;
                            case "chkUpdate":
                                currentrow.Update = this.checked;
                                break;
                            case "chkDelete":
                                currentrow.Delete = this.checked;
                                break;
                            default:
                                break;
                        }
                    });

                    $("#hdnSelectedVal").val(JSON.stringify(CData));
                    return true;
                } else {
                    return false;
                }
            });
        }


        $(function () {
            binddata();
        });


        function binddata() {
            $("[id$=grdUser]").prepend($("<thead></thead>").append($("[id$=grdUser]").find("tr:first"))).dataTable();
        }
    </script>
    



</asp:Content>

