<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Shift.aspx.cs" Inherits="Shift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="css/jquery.timeentry.css" rel="stylesheet" />--%>
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <section class="content-header">
			  <h1>
				Shift
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Shift</li>
			  </ol>
			</section>
			<section class="content">


     <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
            
          
        </div>
    </div>

                <div class="modal fade" id="shift" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">SHIFT</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table style="width: 100%">
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
                                                <label>Name :</label></td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" validate='vgroup' require='Please enter a shift name' CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <table>
                                        <tr>
                                            <td style="width: 130px">
                                                <label>Short Name :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtShortName" runat="server" validate='vgroup' require='Please enter a short name' CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>In Time :</label>
                                            </td>
                                            <td>
                                                <%--<div class="form-group has-feedback bootstrap-timepicker timepicker input-small input-group-addon">
                                                    <asp:TextBox ID="txtInTime" ClientIDMode="Static" runat="server" CssClass="form-control   glyphicon glyphicon-time"></asp:TextBox>
                                                </div>--%>
                                                <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtInTime" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>
                                               
 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Out Time :</label>
                                            </td>
                                            <td>
                                                <%--<div class="form-group has-feedback">
                                                    <asp:TextBox ID="txtOutTime" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>--%>
                                                 <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtOutTime" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Full Day Mins :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFullDayMinutes" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-6">
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="chkFixShift" ClientIDMode="Static" runat="server" CssClass="checkboxbtn" Text="Fix Shift" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px">
                                                <label>Late Allowed Min :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLateAllowed" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Early Allowed Min:</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEarlyAllowed" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Half Day Mins :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtHalfDayMinutes" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="row">
                                <div id="AutoShift" class="col-md-12">
                                    <fieldset>
                                        <legend>Auto Shift (For In Time)
                                        </legend>
                                    </fieldset>
                                    <div class="col-md-6">
                                    <table>
                                        <tr>
                                            <td style="width:100px;">
                                                <label>Before Time :</label>
                                            </td>
                                            <td>
                                               <%-- <div class="form-group has-feedback timeBox">
                                                    <asp:TextBox ID="txtBeforeTime" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>--%>
                                                 <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtBeforeTime" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>
                                            </td>
                                            </tr>
                                        </table>
                                        </div>
                                    <div class="col-md-6">
                                        <table>
                                            <tr>
                                            <td style="width:100px;">
                                                <label>After Time :</label>
                                            </td>
                                            <td>
                                                <%--<div class="form-group has-feedback timeBox">
                                                    <asp:TextBox ID="txtAfterTime" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>--%>
                                                 <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtAfterTime" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                         </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>OT Setting</legend>
                                    </fieldset>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkOT" runat="server" Text="OT" CssClass="checkboxbtn" ClientIDMode="Static" />
                                            </td>
                                            <td class="OT" style="display:none;">
                                                <label>Grace Period :</label>
                                            </td>
                                            <td class="OT" style="display:none;">                                                
                                                <div class="pull-left pfsetting-percent">
                                                    <asp:TextBox ID="txtGracePeriod" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="OT" style="display:none;">
                                                <label>1 Day Hrs :</label>
                                            </td>
                                            <td class="OT" style="display: none;">                                                
                                                <div class="pull-left pfsetting-percent">
                                                    <asp:TextBox ID="txtOneDayHrs" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="OT" style="display:none;">
                                                <label>Max Hrs :</label>
                                            </td>
                                            <td class="OT" style="display: none;">                                                
                                                <div class="pull-left pfsetting-percent">
                                                    <asp:TextBox ID="txtMaxOTHrs" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>                                
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Lunch / Break Setting  
                                        </legend>
                                    </fieldset>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkLunchApplicable" ClientIDMode="Static" runat="server" Text="Lunch" CssClass="checkboxbtn" />
                                            </td>
                                            <td class="Lunch" style="display:none;width:100px;">
                                                <label>Out Time :</label>
                                            </td>
                                            <td class="Lunch" style="display:none;width:150px;">                                                
                                                    <%--div class="form-group has-feedback timeBox">
                                                        <asp:TextBox ID="txtLunchOut" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>    --%>        
                                                 <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtLunchOut" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>                                    
                                            </td>
                                            <td class="Lunch" style="display:none;width:100px;">
                                                <label>In Time :</label>
                                            </td>
                                            <td class="Lunch" style="display:none;width:150px;">                                                
                                                    <%--<div class="form-group has-feedback timeBox">
                                                        <asp:TextBox ID="txtLunchIn" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>                                       --%>
                                                 <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtLunchIn" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>         
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkBreakApplicable" ClientIDMode="Static" runat="server" Text="Break" CssClass="checkboxbtn" />
                                            </td>
                                            <td class="Break" style="display:none;width:100px;">
                                                <label>Out Time :</label>
                                            </td>
                                            <td class="Break" style="display:none;width:150px;">                                                
                                               <%-- <div class="form-group has-feedback timeBox">
                                                    <asp:TextBox ID="txtBreakOut" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>  --%>     
                                                 <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtBreakOut" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>                                         
                                            </td>
                                            <td class="Break" style="display:none;width:100px;">
                                                <label>In Time :</label>
                                            </td>
                                            <td class="Break" style="display:none;width:150px;">                                                
                                                <%--<div class="form-group has-feedback timeBox">
                                                    <asp:TextBox ID="txtBreakIn" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>  --%>                                              
                                                 <div class="controls">
                                                    <div class="input-group bootstrap-timepicker">
                                                         <asp:TextBox ID="txtBreakIn" ClientIDMode="Static" runat="server" CssClass="form-control input-small"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
								                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Other Details  
                                        </legend>
                                    </fieldset>
                                    <table>
                                        <tr>
                                            <td class="FixLunch" style="width: 100px;">
                                                <label>Fix Lunch :</label>
                                            </td>
                                            <td class="FixLunch">
                                                <div class="pull-left pfsetting-percent">
                                                    <asp:TextBox ID="txtFixLunchMins" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkEndsOnNextDay" runat="server" Text="Ends On Next Day" CssClass="checkboxbtn checkbox-nexday" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td></td>
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
            <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
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
                        <h3 class="box-title">Shift </h3>
                        
                    </div>
                <div class="box-body"><div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdShift" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdShift_RowCommand">
                        <Columns>                                                      
                            <asp:TemplateField HeaderText="Name">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblShiftName" runat="server" Text='<%#Eval("ShiftName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short Name">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblShiftShortName" runat="server" Text='<%#Eval("ShiftShortName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Time">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblInTime" runat="server" Text='<%#Eval("InTime")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Time">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblOutTime" runat="server" Text='<%#Eval("OutTime")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ShiftID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("ShiftID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:EntityDataSource ID="EDS_Shift" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Shift"
                        Where="it.CompanyId = @CompanyId">
                    </asp:EntityDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>   
                    </div></div>         
          </div>
    </div>
         </div>
                </section>
    <script type="text/javascript">
    </script>
    <%--<script src="js/jquery.plugin.js" type="text/javascript"></script>
    <script src="js/jquery.timeentry.js" type="text/javascript"></script>--%>
    
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            bindLoadData();
        });

        function bindLoadData() {

            $("[ id $=BtnAddNew]").click(function () {
                $("#txtInTime,#txtOutTime,#txtBeforeTime,#txtAfterTime,#txtBreakIn,#txtBreakOut,#txtLunchIn,#txtLunchOut").timepicker();
            });

            $("#txtInTime,#txtOutTime,#txtBeforeTime,#txtAfterTime,#txtBreakIn,#txtBreakOut,#txtLunchIn,#txtLunchOut").timepicker();
            if ($("#chkOT").prop("checked"))
                $(".OT").show();

            $("#chkOT").change(function () {
                $(".OT").toggle();
            });

            if ($("#chkBreakApplicable").prop("checked"))
                $(".Break").show();

            $("#chkBreakApplicable").change(function () {
                $(".Break").toggle();
            });

            if ($("#chkLunchApplicable").prop("checked")) {
                $(".Lunch").show();
                $(".FixLunch").hide();
            }

            $("[id$=grdShift]").prepend($("<thead></thead>").append($("[id$=grdShift]").find("tr:first"))).dataTable();


            $("#chkLunchApplicable").change(function () {
                $(".Lunch").toggle();
                if (this.checked)
                    $(".FixLunch").hide();
                else
                    $(".FixLunch").show();
            });

            if ($("#chkFixShift").prop("checked"))
                $("#AutoShift").hide();

            $("#chkFixShift").change(function () {
                $("#AutoShift").toggle();
            });
        }

    </script>
</asp:Content>

