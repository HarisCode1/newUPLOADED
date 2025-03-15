<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="EmployeeType.aspx.cs" Inherits="EmployeeType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee Type</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-dashboard"></i>Administration</a></li>
            <li class="active">Employee Type</li>
        </ol>
    </section>
    <br />
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12 " style="padding-bottom: 20px;">
                    <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#employes" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>--%>
                    <asp:UpdatePanel runat="server" ID="UpAddNew">
                        <ContentTemplate>
                            <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" OnClick="btnAddNew_OnClick" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info custom_input">
                        <div class="box-header with-border">
                            <i class="fa fa-table custom_header_icon admin_icon"></i>
                            <h3 class="box-title">Employee Type</h3>
                        </div>
                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GvEmployeeType" runat="server" CssClass="table table-bordered table-hover" EmptyDataText="No Record Found" AutoGenerateColumns="false"
                                            OnRowCommand="GvEmployeeType_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                                <asp:BoundField DataField="TOS" HeaderText="Salary Type" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                <asp:BoundField DataField="IsOvertime" HeaderText="Overtime Applicable" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <HeaderStyle Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="EditEmployeeType">
                                                            <i class="fa fa-pencil-square-o"></i>
                                                        </asp:LinkButton>
                                                        &nbsp;                        
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("Id")%>' CommandName="DeleteEmployeeType" OnClientClick="return confirm('Are you Sure you want to Delete?')">
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
        </div>
    </section>
    <!--Modal-->
    <div class="modal fade" id="ModalDesignation" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Employee Type Add/Edit</h4>
                        </div>
                        <div id="pnlDetail" runat="server" class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table>
                                        <tr id="trCompany" runat="server">
                                            <td style="width: 120px;">
                                                <label>Employement Type :</label>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="HdnID" runat="server"></asp:HiddenField>
                                                <asp:TextBox ID="TxtEmployementType" runat="server"  validate='vgroup' require="Please Select" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px;">
                                                <label>Salary Type :</label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlSalaryType" runat="server" CssClass="form-control" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px;">
                                                <label>Description :</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtDescription" runat="server"  validate='vgroup' require="Please Select"  CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px;">
                                                <label></label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkOvertime" runat="server" Text=" Is Overtime Applicable"  OnCheckedChanged="ChkOvertime_CheckedChanged" AutoPostBack="true" visible="false"/>
                                            </td>
                                        </tr>
                                        <div runat="server" visible="false" id="DivSalartType">
                                        <tr>
                                            <td style="width: 120px;">
                                                <label>Amount (in %):</label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtAmount" runat="server" CssClass="form-control" ></asp:TextBox>

                                            </td>
                                        </tr>
                                        </div>

                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button ID="btnSaveCont" runat="server" class="btn btn-payroll" Text="Save & Continue" />--%>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClick="btnSave_OnClick" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />
                            <button type="button" class="btn cancel-btn" data-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--Modal-->
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

            //$("[id$=GvEmployeeType]").prepend($("<thead></thead>").append($("[id$=GvEmployeeType]").find("tr:first"))).dataTable().fnDestroy();
            //$("[id$=GvEmployeeType]").prepend($("<thead></thead>").append($("[id$=GvEmployeeType]").find("tr:first"))).dataTable({
            //    "order": [[0, "desc"]]
            //});
        }
        $('#btnTest').click(function () {
            alert();
            $(this).attr("disabled", true)
        });
       
    </script>
</asp:Content>

