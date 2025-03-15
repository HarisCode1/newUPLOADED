<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="GetPassEntry.aspx.cs" Inherits="GetPassEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
   <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <%--<link href="css/jquery.timeentry.css" rel="stylesheet" />--%>
    <link href="assets/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
              <section class="content-header">
			  <h1>
				Gate Pass Entry
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Gate Pass Entry</li>
			  </ol>
			</section>
			<section class="content">


   <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
           <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
            <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-primary" Text="Add New" OnClick="btnAddNew_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>



    <div class="modal fade" id="getpassentry" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg">
             <asp:UpdatePanel ID="upMAttendance" runat="server" UpdateMode="Conditional">
                        <ContentTemplate> 
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">GATE PASS ENTRY</h4>
              </div>
              <div id="pnlDetail" runat="server" class="modal-body"> <div class="row">     
                    <div class="col-md-offset-3 col-md-6">
                        <fieldset>
                            <legend>Gate Pass Entry</legend>
                        </fieldset>
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
                                <td style="width:120px;">
                                        <label>Employee :</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" runat="server" validate='vgroup' require='Please Select Employee'>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label>Date :</label>
                                </td>
                                 <td>                                        
                                    <div id="dtpDate" class="input-group date">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" validate='vgroup' require='Please select date'></asp:TextBox>
                                          <span class="add-on input-group-addon">
                                           <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                           </span>
                                   </div>       
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label>From:</label>
                                </td>
                                <td>
                                    <%--<div class="form-group has-feedback">
                                        <asp:TextBox ID="txtFromTime" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>--%>
                                    <div class="controls">
                                        <div class="input-group bootstrap-timepicker">
                                          <asp:TextBox ID="txtFromTime" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                             <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                     </div>
								      </div>
                                </td>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <tr>
                                <td>
                                    <label>To:</label>
                                </td>
                                <td>
                                    
                                    <div class="controls">
                                        <div class="input-group bootstrap-timepicker">
                                          <asp:TextBox ID="txtToTime" ClientIDMode="Static" CssClass="form-control input-small" runat="server" ></asp:TextBox>
                                             <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                     </div>
								      </div>
                                </td>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>   
                                <td>
                                    <label style="width:100px">Duration :</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDuration" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Reason :</label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control" AppendDataBoundItems="true" validate='vgroup' require='Please select Employee' >
                                        <asp:ListItem Text="Personel"></asp:ListItem>
                                        <asp:ListItem Text="Office Work"></asp:ListItem>
                                        </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <label>Remark :</label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtRemark" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:100px">
                                    <label>Approved By :</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprovedBy" ClientIDMode="Static" runat="server" CssClass="form-control" validate='vgroup' require='Approval Name'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                  </div>
                 </div>
              <div class="modal-footer">
                  <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" OnClick="btnClose_Click" />
                  <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"  OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnSave_Click" />
              </div>
            </div><!-- /.modal-content -->
                        </ContentTemplate>
             </asp:UpdatePanel>  
          </div><!-- /.modal-dialog -->
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
                        <h3 class="box-title">Get Pass Entry </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdGatePass" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdGatePass_RowCommand" >
                        <Columns>
                            <asp:BoundField DataField="ManualAttendanceID"  Visible="false" />
                            <asp:TemplateField HeaderText="Employee">
                                <HeaderStyle Width="3%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Date">
                                <HeaderStyle Width="22%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From">
                                <HeaderStyle Width="22%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFromTime" runat="server" Text='<%#Eval("FromTime")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <HeaderStyle Width="22%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblToTime" runat="server" Text='<%#Eval("ToTime")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Total HRs">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalHRs" runat="server" Text='<%#Eval("TotalHrs")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Reason">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved By">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedBy" runat="server" Text='<%#Eval("ApprovedByName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("GatePassID")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("GatePassID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
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

   <script src="js/bootstrap-datetimepicker.min.js"></script>
    <%--<script src="js/jquery.plugin.js"></script>--%>
    
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script> 
    <script src="assets/js/bootstrap-timepicker.min.js"></script>

    <script type="text/javascript">

        $(function () {
            binddata();
        });

        function binddata() {

            //$('#dtpDate').datetimepicker({ pickTime: false });

            $('#dtpDate').datetimepicker({ pickTime: false, autoclose: true });
            PageMethods.getEmpList(OnRequestComplete, OnRequestError);

            $("[ id $=BtnAddNew]").click(function () {
                $("#txtFromTime,#txtToTime").timepicker();
            });

            $("#txtFromTime,#txtToTime").timepicker();

            $("[id$=grdGatePass]").prepend($("<thead></thead>").append($("[id$=grdGatePass]").find("tr:first"))).dataTable();

        }

            function OnRequestComplete(res, userContext, methodName) {
                $("#txtEmpName").typeahead({
                    source: function (typeahead, query) {
                        $("#txtEmpID").val("");
                        typeahead.process($.map(res, function (item) {
                            return {
                                value: item[1],
                                EmpID: item[0]
                            }
                        }));
                    },
                    minLength: 1,
                    property: 'value',
                    appendtoSelector: "[id $= pnlDetail]",
                    SubTop: 90,
                    SubLeft: 182.5,
                    onselect: function (obj) {
                        $("#txtEmpID").val(obj.EmpID);
                    }
                });
            }

            function OnRequestError(error, userContext, methodName) {
                if (error != null) {
                    $("#txtEmpID").val("");
                }
            }
        
        </script>




    <script type="text/javascript">
        
    </script>
</asp:Content>

