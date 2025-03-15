<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Userform.aspx.cs" Inherits="Userform" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <br />
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

   
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    <i class="fa fa-table custom_header_icon admin_icon"></i>
                    <h3 class="box-title">Users</h3>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <%--OnPageIndexChanging="GridUser_PageIndexChanging" OnRowDataBound="GridUser_RowDataBound"--%>
                                <asp:GridView ID="GridUser" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridDataTable table table-striped table-bordered dataTable " AutoGenerateColumns="False" DataKeyNames="UserID" OnRowDataBound="GridUser_RowDataBound">
                                    <Columns>

                                        <asp:BoundField DataField="UserId" HeaderText="ID" SortExpression="UserId" />
                                        <asp:BoundField DataField="EmployeeEnrollId" HeaderText="EnrollID" SortExpression="EmployeeEnrollId" />
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="FirstName" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="Lastname" />
                                        <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Lastname" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />

                                        <asp:TemplateField HeaderText="Action">
                                            <%--<ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" CssClass="btn btn-success"
                                                    CommandArgument='<%# Eval("UserID") %>' OnCommand="lbtnEdit_Command"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" Text="Delete" CssClass="btn btn-danger"
                                                    CommandArgument='<%# Eval("UserID") %>' OnCommand="lbtnDelete_Command"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <HeaderStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" ToolTip="Edit" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("UserID")%>' CausesValidation="false" OnCommand="lbtnEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkDelete" ToolTip="Delete" runat="server" Text="Delete" CommandArgument='<%#Eval("UserID")%>' CausesValidation="false" OnCommand="lbtnDelete_Modalshow">
                             <i class="fa fa-times-circle"></i>
                                                </asp:LinkButton>
                                                <h2 id="titles"></h2>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="action-icon" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
    </div>



    <div class="modal fade" id="UserForm" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">Users Add/Edit</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">
                                    <asp:HiddenField ID="hdnID" runat="server" />
                                    <tr runat="server" visible="false">
                                        <td>
                                            <label>Company</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlComp" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>

                                            <%--<asp:DropDownList ID="ddlComp" runat="server" require='Please enter a Mature Days'>
                                            </asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Role</label>
                                        </td>

                                        <td>
                                            <asp:RequiredFieldValidator ID="ddlRole1"
                                                runat="server" ControlToValidate="ddlRole"
                                                ErrorMessage="Required" ForeColor="Red"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>

                                            <asp:Label ID="lblErrorMessage" runat="server" Text="Company already has an HR or AM." 
                                             CssClass ="text-danger" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlRole" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <label>First Name</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFirstName" type="text" runat="server" validate='vgroup' require='Please enter a Mature Days' CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Last Name</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLastName" type="text" runat="server" validate='vgroup' require='Please enter a Mature Days' CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Email</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtEmailerror" runat="server" Text="This Email Is Already Exist" 
                                             CssClass                ="text-danger" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtEmail" type="text" runat="server" email="Invalid Email" validate='vgroup' require='Please enter a Mature Days' CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trUserName" Visible="false">
                                        <td>
                                            <label>User Name</label>
                                        </td>
                                        <td>
                                            <span id="spnUsername" style="color:red; display:none">Username already exist</span>
                                              <span id="spnUsernamecheck" style="color:red; display:none">Please Enter atleast four character in User Name</span>
                                            <asp:TextBox ID="txtUserName" type="text" runat="server" validate='vgroup' require="It should be Unique to every user." CssClass="form-control" AutoComplete="off"></asp:TextBox>
                  1
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trUserpass1" Visible="false">
                                        <td   >
                                            <label>Password</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" type="password" runat="server" validate='vgroup' require='Please enter a Mature Days' CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <div runat="server" visible="false">
                                        <tr>
                                            <td>
                                                <label>Active</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" />
                                            </td>
                                        </tr>
                                    </div>

                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" class="btn btn-payroll" OnClick="btnSave_Click" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" CausesValidation="true"></asp:Button>
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
                                <asp:TextBox ID="TextBox4" Visible="false" runat="server"></asp:TextBox>
                                <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" Text="No" Style="width: 130px;" CssClass="submit action-button"></asp:Button>
                                <asp:Button ID="btndelete" ClientIDMode="Static" runat="server" Text="Yes" CommandArgument='<%#Eval("ID")%>' CausesValidation="false" Style="width: 130px;" CssClass="submit action-button" OnCommand="btndelete_Command"></asp:Button>

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
          //  usernamecheck();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
            $(window).focusout(function () {
                // Run code
                $("[id$=txtUserName]").blur(function () {
                    var username = $("[id$=txtUserName]").val();
                    if (username.length < 4) {
                        $('#spnUsernamecheck').show();
                        //alert("please enter atleast 4 character");
                        $("[id$=txtUserName]").val('');
                    }
                    else
                    {
                        $('#spnUsernamecheck').hide();

                    }
                });
            });
        //$(document).ready(function () {
        
        //    usernamecheck();
        //});
           
             
            
        function usernamecheck() {
          
            debugger
         
            //$("[id$=txtUserName]").blur(function () {
            //    var username = $("[id$=txtUserName]").val();
            //        if (username.length < 4) {
            //            alert("please enter atleast 4 character");
            //            $("[id$=txtUserName]").val('');
            //        }
            //    });
          
        }
            function binddata() {
                $('#txtDate').datepicker({
                    format: 'm/dd/yyyy',
                    autoclose: true,
                    clearBtn: false,
                    minViewMode: 1
                });
                $("[id$=GridUser]").prepend($("<thead></thead>").append($("[id$=GridUser]").find("tr:first"))).dataTable();
            }
            $('#<%=txtUserName.ClientID%>').blur(function () {
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
                            //if (data) {
                            //    debugger
                            //    $('#spnUsername').show();
                            //    // username.val("");
                            //    // var txtusername = $('#txtUserName').val();
                            //    // $('#txtUserName').val() =='';
                            //    //$('.next').attr('disabled', 'true');
                        //}
                            if (result.d === true) { // If Check_Username returned true
                                $('#spnUsername').show(); // Show the error span
                                $('#txtUserName').val(''); // Optionally clear the input field
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
            });
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

