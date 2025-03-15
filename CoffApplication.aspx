<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="CoffApplication.aspx.cs" Inherits="CoffApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
      <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <section class="content-header">
			  <h1>
				Coff Application
			  
			  </h1>
			  <ol class="breadcrumb">
				<li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
				<li class="active">Coff Application</li>
			  </ol>
			</section>
			<section class="content">

    <div class="row">
        <div class="col-sm-12 " style="padding-bottom:20px;">
            <asp:UpdatePanel runat="server" ID="UpAddNew">
            <ContentTemplate>
            <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" OnClick="btnAddnew_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
          
        </div>
    </div>


    <div class="modal fade" id="Coffapplication" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-lg">
              <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">COFF APPLICATION</h4>
              </div>
              <div id="pnlDetail" runat="server" class="modal-body"> 
                <div class="row">     
                    <div class="col-md-offset-3 col-md-6">
                        <%--<fieldset>
                            <legend>Search</legend>
                        </fieldset>
                        <table>     
                            <tr>
                                <td><label>Search :</label></td>
                                <td>
                                    <asp:TextBox ID="txtSearch" autocomplete="off" ClientIDMode="Static" runat="server" placeholder="Search employee"></asp:TextBox>
                                    <asp:HiddenField ID="hdnEmpID" ClientIDMode="Static" runat="server" />
                                </td>
                            </tr>
                        </table> --%>                                        
                        <fieldset>
                            <legend>Coff Entry</legend>
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
                                <td style="width:130px;"><label>Date :</label></td>
                                <td>
                                    <div id="dtpDate" class="input-group date" style="width: 140px;">
                                                    <asp:TextBox ID="txtDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
                                    </div>
                                </td>
                            </tr>
                            
                            <tr>
                                <td><label>Balance :</label></td>
                                <td>
                                    <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>From :</label></td>
                                <td>
                                   <div class="pull-left leavapp-from">
                                        <div id="dtpFromDate" class="input-group date" style="width: 140px;">
                                                    <asp:TextBox ID="txtFromDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
                                                </div>
                                    </div> 
                                    <div class="pull-left leavapp-fromcheckbox">
                                        <asp:CheckBox ID="chkFromHalfDay" runat="server" CssClass="checkboxbtn" Text="Half Day" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label>To :</label></td>
                                <td>
                                   <div class="pull-left leavapp-from">
                                        <div id="dtpToDate" class="input-group date" style="width: 140px;">
                                                    <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="add-on input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar" data-time-icon="icon-time" data-date-icon="icon-calendar"></span>
                                                    </span>
                                                </div>
                                    </div> 
                                    <div class="pull-left leavapp-fromcheckbox">
                                        <asp:CheckBox ID="chkToHalfDay" runat="server" CssClass="checkboxbtn" Text="Half Day" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Total  Leaves :</label></td>
                                <td>
                                    <asp:TextBox ID="txtTotalLeaves" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Reason :</label></td>
                                <td>
                                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Style="resize:none;" Height="80px"></asp:TextBox>
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
                        <h3 class="box-title">Coff Application </h3>
                        
                    </div>
                 <div class="box-body">
                 <div class="table-responsive">
            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdCoffApplication" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdCoffApplication_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Enroll Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblEnrollId" runat="server" Text='<%#Eval("Enrollid")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <HeaderStyle Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("COFFApplicationId")%>' CommandName="EditCompany">
                                <i class="fa fa-pencil-square-o"></i>
                                    </asp:LinkButton>
                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("COFFApplicationId")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="action-icon" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--<asp:EntityDataSource EnableFlattening="false" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_COFFApplication"
                        Where="it.CompanyId = @CompanyId">
                    </asp:EntityDataSource>--%>
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

     <script src="js/jquery-migrate-1.2.1.js"></script> 
    <%--<script src="js/jquery.plugin.js"></script>--%>
    <script src="js/bootstrap-datetimepicker.min.js"></script>       
    <script src="js/bootstrap-typeahead.js"></script>
    <script type="text/javascript">
        $(function () {
            binddata();
        });

        function binddata() {
            $('#dtpDate').datetimepicker({ pickTime: false, autoclose: true });
            $('#dtpFromDate').datetimepicker({ pickTime: false, autoclose: true });
            $('#dtpToDate').datetimepicker({ pickTime: false, autoclose: true });
            PageMethods.getEmpList(OnRequestComplete, OnRequestError);



            $("[id$=grdCoffApplication]").prepend($("<thead></thead>").append($("[id$=grdCoffApplication]").find("tr:first"))).dataTable();


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
</asp:Content>

