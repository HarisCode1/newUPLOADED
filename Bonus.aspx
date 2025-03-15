<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Bonus.aspx.cs" Inherits="Bonus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Bonus</h1>
    </section>
    <section class="">
        <div class="col-sm-12" style="padding-bottom: 20px;">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <% if (Session["Username"].ToString() == "SuperAdmin")
                        { %>

                    <% }
                        else
                        { %>
                    <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="btn btn-payroll pull-right" OnClick="btnAdd_Click" />
                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>
    <div class="modal fade" id="ModalBonus" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">Bonus Add/Edit</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">
                                    <asp:HiddenField ID="hdnID" runat="server" />


                                    <tr>
                                        <div runat="server" visible="false">
                                            <td>
                                                <label>Company</label>
                                            </td>
                                            <td>

                                                <asp:DropDownList ID="ddlComp" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </div>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Salary Type</label>
                                        </td>
                                        <td>

                                            <asp:DropDownList ID="ddlSalaryType" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlSalaryType_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%--<asp:TextBox ID="txtID" runat="server" Visible="false" validate='vgroup' require='Please enter a branch name' CssClass="form-control"></asp:TextBox>--%>
                                            <label>Employee Type</label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTypeOfEmp" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%">
                                                <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Bonus Title</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBonusTitle" type="text" runat="server" validate='vgroup' require='Please enter a Mature Days' CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Mature Days</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMatureDays" type="Number" runat="server" validate='vgroup' require='Please enter a Mature Days' CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--<div runat="server" visible="true">
                                        <tr>
                                            <td>
                                                <label>Bonus</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkBonus" runat="server" OnCheckedChanged="chkBonus_CheckedChanged" AutoPostBack="true"/>
                                            </td>
                                        </tr>
                                    </div>--%>
                                    <%--<div runat="server" visible="true">
                                        <tr>
                                            <td>
                                                <label>Other</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkOthers" runat="server" OnCheckedChanged="chkOthers_CheckedChanged" AutoPostBack="true"/>
                                            </td>
                                        </tr>
                                    </div>--%>
                                    <div runat="server" id="divBonus" visible="false">
                                        <tr>
                                            <td>
                                                <label>Bonus Days</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBonusDays" type="Number" runat="server" validate="vgroup" require='Please enter a Bonus Days' CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </div>
                                    <div runat="server" id="divother" visible="false">
                                        <tr>
                                            <td>
                                                <label>Amount</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" type="Number" runat="server" validate="vgroup" require='Please enter a Bonus Days' CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </div>

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
                                <asp:Button ID="btnSave" runat="server" class="btn btn-payroll" OnClick="btnSave_Click" Text="Save" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>
                                <asp:Button ID="btnClose" runat="server" class="btn" OnClick="btnClose_Click" Text="Close"></asp:Button>

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
                                <asp:Button ID="btndelete" ClientIDMode="Static" runat="server" Text="Yes" CommandArgument='<%#Eval("ID")%>' Style="width: 130px;" CssClass="submit action-button" OnCommand="btndelete_Command"></asp:Button>

                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <section class="">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Bonus </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridBonus" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="ID">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="Type" Visible="true" />

                                            <asp:BoundField DataField="BonusTitle" HeaderText="Bonus Title" SortExpression="Type" Visible="true" />
                                            <asp:BoundField DataField="salaryType" HeaderText="Salary Type" SortExpression="Type" Visible="true" />

                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="Type" Visible="true" />

                                            <asp:BoundField DataField="Type" HeaderText="Type Of Employee" SortExpression="Type" Visible="true" />

                                            <%--<asp:BoundField DataField="MatureDays" HeaderText="Mature Days" SortExpression="MatureDays" Visible="true" />
                                            <asp:BoundField DataField="BonusDays" HeaderText="Bonus Days" SortExpression="BonusDays" />--%>


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("ID") %>' OnCommand="lbtnEdit_Command"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server"
                                                        CommandArgument='<%# Eval("ID") %>' OnCommand="lbtnDelete_Command"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                                </ItemTemplate>
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
    </section>
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
            $("[id$=GridBonus]").prepend($("<thead></thead>").append($("[id$=GridBonus]").find("tr:first"))).dataTable().fnDestroy();
            $("[id$=GridBonus]").prepend($("<thead></thead>").append($("[id$=GridBonus]").find("tr:first"))).dataTable({
                "order": [[0, "decs"]]
            });

        }
    </script>
</asp:Content>

