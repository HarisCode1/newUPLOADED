<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="LeaveYear.aspx.cs" Inherits="LeaveYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
   
    <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
			  <h1>
				Leave Year
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Leave Year</li>
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




    <div class="modal fade" id="leaveyear" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">LEAVE YEAR</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
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
                                            <td style="width: 130px;">
                                                <label>Leave Year :</label></td>
                                            <td>
                                                <div id="dtpAppFromDate" class="input-group date" style="width: 214px;">
                                                    <asp:TextBox ID="txtLeaveYear" validate='vgroup' CssClass="form-control" require='Please Enter Leave Year'  runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
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





    <div class="row">
        <div class="col-md-12">
             <div class="box box-info custom_input">
                <div class="box-header with-border">
                    
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Leave Year </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdLeaveYear" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdLeaveYear_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Year" HeaderText="Year" />
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("LeaveYearID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("LeaveYearID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_LeaveYear" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_LeaveYear"
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
    </script>

     <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        });

        function binddata() {
            $("#dtpAppFromDate").datetimepicker({
                format: " yyyy",
                viewMode: "years",
                minViewMode: "years"
            });


            $("[id$=grdLeaveYear]").prepend($("<thead></thead>").append($("[id$=grdLeaveYear]").find("tr:first"))).dataTable();


        }
    </script>

</asp:Content>

