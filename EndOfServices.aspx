<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="EndOfServices.aspx.cs" Inherits="EndOfServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style>
        .bxwidth{
                width: 100%;
    height: 41px;
        }
    </style>

    <link href="assets/css/multi-select.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>End Of Services</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="EndOfServices.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">EOS Of Employe</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee Details</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table border="1" width="39%">
                                        <tr>
                                            <td width="33%">Id
                                            </td>
                                            <td width="33%">
                                                <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                                            </td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="33%">Name
                                            </td>
                                            <td width="33%"><asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                            </td>
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="33%">Designation
                                            </td>
                                            <td width="33%"><asp:Label ID="lbldesignation" runat="server" Text=""></asp:Label>
                                            </td>
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="33%">Department
                                            </td>
                                            <td width="33%"><asp:Label ID="lbldepartment" runat="server" Text=""></asp:Label>
                                            </td>
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="33%">Termination Date
                                            </td>
                                            <td width="33%"><asp:Label ID="lblterminationdate" runat="server" Text=""></asp:Label>
                                            </td>
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnsave" class="btn btn-payroll" runat="server" OnClick="btnsave_Click" Text=" Process End Of Services" />  
                                            </td>
                                        </tr>
                                      <%--  <tr>
                                            <td width="33%">Termination Date
                                            </td>
                                            <td width="33%">Row2,Col2
                                            </td>
                                            
                                            </td>
                                        </tr>--%>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>
    
    <script src="assets/js/jquery.multi-select.js"></script>
    <script type="text/javascript">
        $(document).ready(function (e) {
            
            $('#ContentPlaceHolder1_ddlassigneditems').multiSelect({
                selectableHeader: "<div class='custom-header'>Assigned Assets</div>",
                selectionHeader: "<div class='custom-header'>Recieved Assets </div>",
            });
        })
    </script>
</asp:Content>

