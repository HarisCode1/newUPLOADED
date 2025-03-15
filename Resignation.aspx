<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Resignation.aspx.cs" Inherits="Resignation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Resignation</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Resignation.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Employees Resignation</li>
        </ol>
    </section>
    <section class="content">
        <div id="companygrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">
                        Employees Resignation
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpdateResign" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdresignation" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="ResignationId" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="Image" Visible="false" HeaderText="image" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="AppliedDate" HeaderText="Applied Date" HeaderStyle-Width="15%" />
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <%--     <asp:LinkButton ID="lnkView" ToolTip="View" runat="server" Text="View" CommandArgument='<%#Eval("ResignationId")%>' CausesValidation="false"  OnCommand="lnkView_Command">
                                <i class="fa fa-eye"></i></asp:LinkButton>--%>
                                           
		 <a href='<%# Eval("Image") %>' id="doc" download='<%# Eval("Image") %>' target="_blank" data-toggle="tooltip" title="Resignation Document" runat="server"><i class="fa fa-file"></i></a>
                                                    

                                                    <%--<asp:LinkButton ID="lnkDownload" CssClass="myLink" runat="server" CommandArgument='<%# Eval("ResignationId") %>' OnCommand="lnkDownload_Command" CausesValidation="false">Download</asp:LinkButton>--%>
                                                    <asp:LinkButton ID="lbtnEdit" ToolTip="Approve" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("ResignationId")%>' OnCommand="lbtnEdit_Command" CausesValidation="false">
                                <i class="fa fa-check-circle"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                         
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
    <section class="content">
        <div id="Div2" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">
                        Revert List Of Employees Resignation
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdrevertresignation" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="ResignationId" HeaderText="ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="AppliedDate" HeaderText="Applied Date" HeaderStyle-Width="15%" />
                                            <asp:TemplateField>
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <%-- <asp:LinkButton ID="lbtnEdit" ToolTip="Approve" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("ResignationId")%>' OnCommand="lbtnEdit_Command" CausesValidation="false" >
                                <i class="fa fa-check-circle"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                 --%>
                                                    <%--<asp:LinkButton ID="lnkDelete" ToolTip="Delete" runat="server" Text="Delete" CommandArgument='<%#Eval("ResignationId")%>' CausesValidation="false"  OnCommand="lnkDelete_Command">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>--%>
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
    <section class="content">
        <div id="resignapprovalgrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">
                        Approval List Of Employees Resignation
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpdateApproval" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdapvdresg" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="ResignationId" HeaderText="ID" HeaderStyle-Width="5%" />

                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="15%" />
                                            <asp:BoundField DataField="AppliedDate" HeaderText="Applied Date" HeaderStyle-Width="15%" />
                                            <asp:TemplateField>
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <%-- <asp:LinkButton ID="lbtnEdit" ToolTip="Approve" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("ResignationId")%>' OnCommand="lbtnEdit_Command" CausesValidation="false" >
                                <i class="fa fa-check-circle"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                 --%>
                                                    <%--<asp:LinkButton ID="lnkDelete" ToolTip="Delete" runat="server" Text="Delete" CommandArgument='<%#Eval("ResignationId")%>' CausesValidation="false"  OnCommand="lnkDelete_Command">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>--%>
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
    <div class="modal fade" id="ResignationForm" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">End Of Services</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">

                                    <tr runat="server">
                                        <td>
                                            <label>Applied Date </label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbldate" runat="server" Text=""></asp:Label>

                                            <%--<asp:TextBox ID="TextBox1" type="text" runat="server" validate='vgroup' require='Please enter name' CssClass="form-control"></asp:TextBox>--%>

                                            <%--<asp:DropDownList ID="ddlComp" runat="server" require='Please enter a Mature Days'>
                                            </asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>EOS Date</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control date-picker" ClientIDMode="Static" require='Please select from date' type="date"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredDate" ControlToValidate="txtFromDate" ForeColor="red" runat="server" ErrorMessage="Required EOS Date"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <%-- <tr>
                                        <td>
                                            <label>Assest Detail</label>
                                        </td>
                                        <td>
                                            <span id="spnUsername" style="color:red; display:none">Username already exist</span>
                                            <asp:TextBox ID="txtUserName" type="text" runat="server" validate='vgroup' require="It should be Unique to every user." CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>  --%>
                                    <%--  <div runat="server">
                                        <tr>
                                            <td>
                                                <label>Active</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" />
                                                <asp:Label ID="lblcheck" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </div>--%>
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

    <div class="modal fade" id="ViewForm" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">End Of Services Details</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">

                                    <tr runat="server">
                                        <td>
                                            <label>Employee Name</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblempname" runat="server" Text=""></asp:Label>

                                            <%--<asp:TextBox ID="TextBox1" type="text" runat="server" validate='vgroup' require='Please enter name' CssClass="form-control"></asp:TextBox>--%>

                                            <%--<asp:DropDownList ID="ddlComp" runat="server" require='Please enter a Mature Days'>
                                            </asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Reason</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblreason" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <label>Remarks</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblremarks" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Status</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Applied Date</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblapplieddate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Image</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Image</label>
                                        </td>
                                        <td>
                                            <asp:Image ID="recvltr" runat="server" />
                                        </td>
                                    </tr>
                                    <%--  <div runat="server">
                                        <tr>
                                            <td>
                                                <label>Active</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" />
                                                <asp:Label ID="lblcheck" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </div>--%>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <%--<asp:Button ID="btndownload" runat="server" class="btn btn-payroll" OnClick="btndownload_Click" Text="Save Changes" CausesValidation="true"></asp:Button>--%>
                                <asp:Button ID="btncloseviewmodal" runat="server" class="btn" OnClick="btncloseviewmodal_Click" Text="Close" CausesValidation="false"></asp:Button>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script src="assets/js/bootstrap-switch.min.js"></script>
    <script type="text/javascript">
        //$(function () {
        //    binddata();
        //    $('#txtFromDate').datepicker('setDate', new Date());
        //});

        //function binddata() {
        //    $('#txtFromDate').datepicker({
        //        format: 'm/dd/yyyy',
        //        autoclose: true,
        //        clearBtn: false
        //    }).on("change", function () {
        //        $('#ContentPlaceHolder1_hdDate').attr('value', $("#txtDate").val());
        //    });
        //}

        $(function () {
            debugger
            $(".date-picker").datepicker({
                dateFormat: 'dd-M-yy'
            });
        });


    </script>
</asp:Content>

