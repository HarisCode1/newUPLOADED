<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="LeaveAllotment.aspx.cs" Inherits="LeaveAllotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
			  <h1>
				LEAVE ALLOTMENT
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">LEAVE ALLOTMENT</li>
			  </ol>
			</section>
			<section class="content">

    <div class="row">
        <div class="col-sm-6 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
            <table>
                     <%--<tr>
                            <td style="width: 130px;">
                                <label>Leave Year :</label></td>
                            <td>
                                <div id="dtpAppFromDate" class="input-group date" style="width: 300px;">
                                    <asp:TextBox ID="txtLeaveYear" validate='vgroup' CssClass="form-control" require='Please Enter Leave Year'  runat="server"></asp:TextBox>
                                    <span class="add-on input-group-addon">
                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                    </span>
                                </div>
                            </td>
                        </tr>--%>
                         <tr>
                                    <td style="width: 130px;">
                                        <label>Company :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' custom="Select Company" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
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
                                <label>Department :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"  require="Select Department" validate='vgroup'     AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Leave :</label></td>
                            <td>
                                    <asp:DropDownList ID="ddlLeaveShortName" runat="server" CssClass="form-control"  require='Select Leave Type' validate='vgroup'  AppendDataBoundItems="true">
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Allotment :</label>
                            </td>
                            <td>
                            <div class="pull-left salery-variable">
                                            <asp:TextBox ID="txtLeave" runat="server" CssClass="lev-leavetxt" validate="vgroup" require="Please enter a leave"></asp:TextBox>
                            </div>
                                </td>        
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnApply" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnApply_Click" />
                            </td>
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
          
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

