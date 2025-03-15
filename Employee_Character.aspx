<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_Character.aspx.cs" Inherits="Employee_Character" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <section class="content-header">
        <h1 style="margin-top:20px;">Employee Character</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="EmployeeAssets.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Employees EOS</li>
        </ol>
    </section>
    <div class="row">
        <div class="col-sm-12 " style="padding-bottom: 20px;">
            <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#employes" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>--%>
            <asp:UpdatePanel runat="server" ID="UpAddNew">
                <ContentTemplate>
                    <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-payroll pull-right mylinks" Text="Add New" OnClick="BtnAddNew_Click" Enabled="true"
                        CausesValidation="false" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%-- Grid bind--%>
     <section class="content">
        
            <div id="empeassetgrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Character List</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdempcharacter" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" >
                                        <Columns>
                                            
                                            <asp:BoundField DataField="Id" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="BehaviorwithColleagues" DataFormatString="{0:F2} %" HeaderText="Behavior Colleagues" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="BehaviorwithCustomers"   DataFormatString="{0:F2} %" HeaderText="Behavior Customer" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Honesty" HeaderText="Honesty" DataFormatString="{0:F2} %" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="TimeReporting" HeaderText="Time Reporting" DataFormatString="{0:F2} %" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="Date" HeaderStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" ToolTip="Edit" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("Id")%>' CausesValidation="false" OnCommand="lbtnEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" ToolTip="Delete" runat="server" Text="Delete" CommandArgument='<%#Eval("Id")%>' CausesValidation="false" OnCommand="lnkDelete_Command">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                   <%-- <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                       DataSourceID="EDS_Company"
                                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        
        </div>
     </section>
        
    



    <div class="modal fade" id="EmpcharacterForm" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">Create Behavior/Character</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">
                                    <asp:HiddenField ID="hdnID" runat="server" />
                                  
                                    <tr>
                                        <td>
                                            <label>Employee Name</label>
                                        </td>
                                        <td>
                                              <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" enabled="false"  CssClass="form-control input-sm" runat="server" validate='vgroup'  require='Please Select Employee' AutoPostBack="true" AppendDataBoundItems="True" >
                                                    </asp:DropDownList>
                                            
                             
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Behaviour With  Colleagues %</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBxColleagues" type="text" ClientIDMode="Static" onblur="CheckPercentage()"    runat="server" validate='vgroup' require='Please enter price'  CssClass="form-control"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Behaviour With  Customers %</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBxCustomers" type="text" runat="server" ClientIDMode="Static" onblur="CheckPercentage()" validate='vgroup' require='Please enter detail' CssClass="form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="Requireddetail" ControlToValidate="txtBxCustomers" ForeColor="red" runat="server" ErrorMessage="Required Behaviour Customer"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <label>Honesty/Sincerity %</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txthonesty" type="text" runat="server" ClientIDMode="Static" validate='vgroup' onblur="CheckPercentage()" require='Please enter detail' CssClass="form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txthonesty" ForeColor="red" runat="server" ErrorMessage="Required Honesty"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <label>Reporting in Time %</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txttimereporting" type="text" runat="server" ClientIDMode="Static" onblur="CheckPercentage()"  validate='vgroup' require='Please enter detail' CssClass="form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txttimereporting" ForeColor="red" runat="server" ErrorMessage="Required Time Resporting"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                  <%--  <tr>
                                            <td>Quantity
                                            </td>
                                            <td><asp:TextBox ID="txtqty" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="Requiredqty" ControlToValidate="txtqty" runat="server" ErrorMessage="Required enter Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                                      
                                        </tr>       --%>
                                 

                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" class="btn btn-payroll" OnClick="btnSave_Click" Text="Save Changes" OnClientClick="return Saveonclick();" CausesValidation="true"></asp:Button>
                                <asp:Button ID="btnClose" runat="server" class="btn" OnClick="btnClose_Click" Text="Close" CausesValidation="false"></asp:Button>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="Delete" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel2">Delete</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12" id="mybtn">

                                        <fieldset>
                                            <label>Are you sure you want to delete this record?</label>
                                            <div class="form-group">

                                                <asp:TextBox ID="MsgDelete" Visible="false" runat="server"></asp:TextBox>


                                            </div>

                                        </fieldset>
                                        <%-- End Account Setup--%>

                                        <fieldset>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                
                                <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" Text="No" Style="width: 130px;" CssClass="submit action-button"></asp:Button>
                                  <asp:LinkButton ID="lnkDelete" ClientIDMode="Static" ToolTip="Delete" runat="server" Text="Yes" CssClass="submit action-button" CommandArgument='<%#Eval("AssetID")%>' CausesValidation="false" OnClick="lnkDelete_Click">
                                      </asp:LinkButton>                                
                                <%--<asp:LinkButton ID="btndelete" ClientIDMode="Static" runat="server" Text="Yes" Style="width: 130px;" CssClass="submit action-button" OnClick="btndelete_Click"></asp:LinkButton>--%>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
    <script src="js/bootstrap-typeahead.js"></script>
    <script src="js/jquery-migrate-1.2.1.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">

        function CheckPercentage()
        {
            debugger
            var behaviorcollge = $("#txtBxColleagues").val() > 100 ? $("#txtBxColleagues").val('100') : $("#txtBxColleagues").val();
            var behaviorcustm = $("#txtBxCustomers").val() > 100 ? $("#txtBxCustomers").val('100') : $("#txtBxCustomers").val();
            var behaviorhonesty = $("#txthonesty").val() > 100 ? $("#txthonesty").val('100') : $("#txthonesty").val();
            var behaviorreporting = $("#txttimereporting").val() > 100 ? $("#txttimereporting").val('100') : $("#txttimereporting").val();            
        }
        function Saveonclick() {
           // alert('ok');
            if (validate('vgroup') == true) {
                //window.location.href = "Employee.aspx";
                msgbox(1, "Sucess", "Successfully Saved!");
                getCertificateRecords();
                return true;

            }
            else {
                return false;
            }
        }


        $(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function binddata() {
            $('#txtDate').datepicker({
                format: 'm/dd/yyyy',
                autoclose: true,
                clearBtn: false,
                minViewMode: 1
            });
            $("[id$=GridUser]").prepend($("<thead></thead>").append($("[id$=GridUser]").find("tr:first"))).dataTable();
        }
        <%--  $('#<%=txtUserName.ClientID%>').blur(function () {
            var username = $(this).val();
            if (username == null || username == 'undefined' || username == '') {
                return false;
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: "userform.aspx/Check_Username",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'Username':'" + username + "'}",
                    success: function (result) {
                        var data = JSON.parse(result.d);
                        if (data) {
                            $('#spnUsername').show();
                            //$('.next').attr('disabled', 'true');
                        }
                        else {
                            $('#spnUsername').hide();
                            //$('.next').removeAttr('disabled');
                        }

                    },
                    error: function (result) {

                    }
                });
            }
        });--%>
        <%-- $(document).ready(function () {

            $("#<%=ddlComp.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

        });
         $(document).ready(function () {

            $("#<%=ddlRole.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

      });--%>
    </script>
</asp:Content>

