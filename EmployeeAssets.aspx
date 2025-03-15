<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="EmployeeAssets.aspx.cs" Inherits="EmployeeAssets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <section class="content-header">
        <h1> Assets Creation</h1>
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
                        <h3 class="box-title">Employee Asset</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdempasset" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" >
                                        <Columns>
                                            <asp:BoundField DataField="AssetId" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="Name" HeaderText="Assest Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Price"  DataFormatString="{0:N}" HeaderText="Price" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Detail" HeaderText="Detail" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="Date" HeaderStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" ToolTip="Edit" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("AssetID")%>' CausesValidation="false" OnCommand="lbtnEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" ToolTip="Delete" runat="server" Text="Delete" CommandArgument='<%#Eval("AssetID")%>' CausesValidation="false" OnCommand="lnkDelete_Command">
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
        
    



    <div class="modal fade" id="EmpAssetForm" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">Create Assets</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">
                                    <asp:HiddenField ID="hdnID" runat="server" />
                                    <tr runat="server" visible="false">
                                        <td>
                                            <label>Company</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlComp"  runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>

                                            <%--<asp:DropDownList ID="ddlComp" runat="server" require='Please enter a Mature Days'>
                                            </asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Assest Name</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAssestName" type="text" runat="server" validate='vgroup' require='Please enter name' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredAssest" ControlToValidate="txtAssestName" ForeColor="red" runat="server" ErrorMessage="Required Assest Name"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Price</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPrice" type="number" runat="server" validate='vgroup' require='Please enter price' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredPrice" ControlToValidate="txtPrice" ForeColor="red" runat="server" ErrorMessage="Required Price"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Assest Details</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDetail" type="text" runat="server" email="Invalid Email" validate='vgroup' require='Please enter detail' CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="Requireddetail" ControlToValidate="txtDetail" ForeColor="red" runat="server" ErrorMessage="Required Detail"></asp:RequiredFieldValidator>
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
                                <asp:Button ID="btnSave" runat="server" class="btn btn-payroll" OnClick="btnSave_Click" Text="Save Changes" CausesValidation="true"></asp:Button>
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
                                  <asp:LinkButton ID="lnkDelete" ClientIDMode="Static" ToolTip="Delete" runat="server" Text="Yes" CssClass="submit action-button" CommandArgument='<%#Eval("AssetID")%>' CausesValidation="false" OnClick="btndelete_Click">
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

