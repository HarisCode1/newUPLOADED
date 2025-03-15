<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="ShiftAllotment.aspx.cs" Inherits="ShiftAllotment" %>

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
				Shift Allotment
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Shift Allotment</li>
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


    <div class="modal fade" id="shiftallotment" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">SHIFT ALLOTMENT</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-8">
                                    <table>
                                  <tr id="trCompany" runat="server">
                                    <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;'  AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
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
                                            <td style="width:130px;">
                                                <label>Enrollid :</label>
                                                </td>
                                             
                                                <td>

                                                <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" runat="server" validate='vgroup' require='Please Select Employee'>
                                                </asp:DropDownList>

                                                </td>
                                            
                                        </tr>
                                        <tr>
                                            <td style="width: 130px;">
                                                <label>Month :</label></td>
                                            <td>                                              
                                                <div id="dtpAppFromDate" class="input-group date" style="width: 214px;">
                                                    <asp:TextBox ID="txtAppFromDate" validate='vgroup' CssClass="form-control" require='Please Enter Date' runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
                                                </div>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <label>Department :</label>
                                            </td>
                                            <td>
                                                <asp:DropDownList  ID="ddlDepartment" AppendDataBoundItems="true" runat="server"  DataSourceID="EDS_Department" DataTextField="Department" DataValueField="DepartmentID" validate='vgroup' custom='Please Select Department' customFn='var r = parseInt(this.value); return r > 0;' >
                                                <asp:ListItem Value="0" Text="Please Select Department..."></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:EntityDataSource ID="EDS_Department" runat="server"
                                                    ConnectionString="name=vt_EMSEntities"
                                                    DefaultContainerName="vt_EMSEntities"
                                                    EntitySetName="vt_tbl_Department">
                                                </asp:EntityDataSource>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <label>Shift :</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlShift" AppendDataBoundItems="true" runat="server" CssClass="form-control"  DataSourceID="EDS_Shift" DataTextField="ShiftShortName" DataValueField="ShiftID" custom='Please Select Shift' validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' >
                                               <asp:ListItem Value="0" Text="Please Select Shift..."></asp:ListItem>
                                                     </asp:DropDownList>
                                                <asp:EntityDataSource ID="EDS_Shift" runat="server"
                                                    ConnectionString="name=vt_EMSEntities"
                                                    DefaultContainerName="vt_EMSEntities"
                                                    EntitySetName="vt_tbl_Shift">
                                                </asp:EntityDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-offset-4"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                            <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" Text="Apply" OnClick="btnApply_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
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
                                        <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
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
                        <h3 class="box-title">Shift Allotment </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdshiftAllotment" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdshiftAllotment_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="EnrollId" HeaderText="Enroll ID" />
                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ShiftAllotmentID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("ShiftAllotmentID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource  runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_ShiftAllotment"
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
                format: "MM-yyyy",
                viewMode: "months",
                minViewMode: "months"
            });

            $("[id$=grdshiftAllotment]").prepend($("<thead></thead>").append($("[id$=grdshiftAllotment]").find("tr:first"))).dataTable();

        }
    </script>
</asp:Content>

