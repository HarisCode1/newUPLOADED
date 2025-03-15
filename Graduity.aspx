<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Graduity.aspx.cs" Inherits="Graduity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="row">
        <div class="col-sm-12 " style="padding-bottom: 20px;">
            <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#employes" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>--%>
            <asp:UpdatePanel runat="server" ID="UpAddNew">
                <ContentTemplate>
                    <% if (Session["Username"].ToString() == "SuperAdmin")
                        { %>

                    <% }
                        else
                        { %>
                    <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-payroll pull-right" Text="Add New" OnClick="BtnAddNew_Click" />

                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%-- Grid bind--%>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-info custom_input">
                <div class="box-header with-border">
                    <i class="fa fa-table custom_header_icon admin_icon"></i>
                    <h3 class="box-title">Graduity</h3>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <%--OnPageIndexChanging="GridUser_PsageIndexChanging" OnRowDataBound="GridUser_RowDataBound"--%>
                                <asp:GridView ID="GridEmployee" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridDataTable table table-striped table-bordered dataTable " AutoGenerateColumns="False" DataKeyNames="Id">
                                    <Columns>

                                        <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                        <asp:BoundField DataField="MaturityOfDays" HeaderText="MaturityOfDays" SortExpression="MaturityOfDays" />
                                        <asp:BoundField DataField="NoOfDays" HeaderText="NoOfDays" SortExpression="NoOfDays" />
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("Id")%>' OnCommand="lbtnEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="confirm" CommandArgument='<%#Eval("Id")%>' OnCommand="lbtnDelete_Modalshow">
                             <i class="fa fa-times-circle"></i>
                                                </asp:LinkButton>
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
    </div>
    <%-- Modal Dialog--%>
    <div class="modal fade" id="Modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <asp:HiddenField ID="hdnId" runat="server" />
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlDetail" runat="server">
                        <section id="msform" class="content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box box-info custom_input">
                                        <div class="box-header with-border">
                                            <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                                            <h3 class="box-title">Graduity</h3>
                                            <div class="box-tools pull-right">
                                                <%--  <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
                                                <button class="btn btn-box-tool" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                            </div>
                                        </div>
                                        <!-- /.box-header -->
                                        <div id="divButtons" class="box-body NewEmp_boxBody">
                                            <div class="row">
                                                <div class="col-md-12" id="mybtn">

                                                    <fieldset>

                                                        <hr />

                                                        <tr>
                                                            <td>
                                                                <label class="col-sm-2 control-label">Employee Type:</label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                                <label style="color: red" id="lblddCompany"></label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label class="col-sm-2 control-label">Mature Of Days</label>
                                                            </td>
                                                            <td>

                                                                <asp:TextBox ID="txtmaturedays" CssClass="only-number" runat="server" validate='vgroup' placeholder="Enter Mature Of Days"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label class="col-sm-2 control-label">No Of Days</label>
                                                            </td>
                                                            <td>

                                                                <asp:TextBox ID="txtnodays" CssClass="only-number" runat="server" validate='vgroup' placeholder="Enter No Of Days"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label class="col-sm-2 control-label">Active</label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkactive" runat="server" />
                                                            </td>
                                                        </tr>


                                                        <asp:Button ID="btnsaves" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="btn btn-payroll" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnsave_TypeEmployee"></asp:Button>
                                                    </fieldset>


                                                    <fieldset>
                                                </div>

                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <div class="">
                                </div>

                            </div>

                        </section>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </div>
    <div class="modal fade" id="deleteform" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpView1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <section id="msform" class="content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box box-info custom_input">
                                        <div class="box-header with-border">
                                            <%-- <i class="fa fa-user-plus custom_header_icon admin_icon"></i>--%>
                                            <h3 class="box-title" id="h1" runat="server">Delete </h3>
                                            <div class="box-tools pull-right">
                                                <%-- <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
                                                <button class="btn btn-box-tool" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                            </div>
                                        </div>
                                        <!-- /.box-header -->
                                        <div id="divButtons" class="box-body NewEmp_boxBody">
                                            <div class="row">
                                                <div class="col-md-12" id="mybtn">

                                                    <fieldset>
                                                        <label>Are you sure you want to delete this record?</label>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="TxtIDs" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" Text="Cancel" Style="width: 130px;" CssClass="submit action-button"></asp:Button>
                                                            <asp:Button ID="btndelete" ClientIDMode="Static" runat="server" Text="Delete" CommandArgument='<%#Eval("UserID")%>' Style="width: 130px;" CssClass="submit action-button" OnCommand="lbtnDelete_Command"></asp:Button>
                                                        </div>

                                                    </fieldset>
                                                    <%-- End Account Setup--%>

                                                    <fieldset>
                                                </div>

                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <div class="">
                                </div>

                            </div>

                        </section>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <script>
            function ClearFields() {
                $("[id $=hdnId]").val("");
                $("[id $=txtmaturedays]").val("");
                $("[id $=txtnodays]").val("");
                $("[id $=ddlEmployeeType]").val("0");
                $("[id $=chkactive]").prop('checked', false);
            }
        </script>
</asp:Content>

