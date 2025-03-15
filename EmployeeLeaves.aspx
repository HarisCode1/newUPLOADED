<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="EmployeeLeaves.aspx.cs" Inherits="EmployeeLeaves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee Leave </h1>
         <asp:Label ID="lblapprove" runat="server" Text=""></asp:Label>
    </section>
    <section class="content">
        <div class="all-tables">
            <div class="row">
                <div class="col-md-2">
                      <asp:HiddenField ID="lblid" runat="server" />
                    <asp:Label ID="LblCompName" runat="server" Text="Company Name"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="LblCompanyName" runat="server" Text="" class="form-control"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="LblEmpName" runat="server" Text="Employee name"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="LblEmployeeName" runat="server" Text="" class="form-control"></asp:Label>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="LblApplyLeaves" runat="server" Text="Leave Apply On"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="LblOnApplyLeaves" runat="server" Text="" class="form-control"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="LblTotalLeaves" runat="server" Text="Days of Leaves Apply"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="LblTotalNoLeaves" runat="server" Text="" class="form-control"></asp:Label>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="LblFrmDate" runat="server" Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="LblLeavesFromDate" runat="server" Text="" class="form-control"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="LblTodate" runat="server" Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="LblLeavesTodate" runat="server" Text="" class="form-control"></asp:Label>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="LblROLeaves" runat="server" Text="Reason Of Leaves"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="TxtBxReasonOfLeaves" runat="server" ReadOnly="true" class="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="TxtBxComments" runat="server" class="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
            </div>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="LblAction" runat="server" Text="Decision For Leaves Approval By line Manager"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddldecision" class="form-control" runat="server" validate='vgroup' require='Please select Option' Custom="Please Select Option" customFn=" var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Reject"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Approve"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div runat="server" id="divHR" class="row" visible="false">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Decision For Leaves Approval By HR"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="DdlHrApproval" class="form-control" runat="server" validate='vgroup' require='Please select Option' Custom="Please Select Option" customFn=" var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text="Select Option"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Reject"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Approve"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Button ID="btnSaveLeaveApproval" runat="server" CssClass="pull-right btn btn-primary" Text="Save" OnClick="btnSaveLeaveApproval_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"/>
                <asp:Button ID="btnClose" runat="server" CssClass="pull-right btn btn-primary" Text="Close" OnClick="btnClose_Click" />
            </div>
        </div>
    </section>
    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <%--<script type="text/javascript">
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
            $("[id$=grdDesignation]").prepend($("<thead></thead>").append($("[id$=grdDesignation]").find("tr:first"))).dataTable();
        }
    </script>--%>
</asp:Content>
