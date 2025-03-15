 <%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="AssetsAssign.aspx.cs" Inherits="AssetsAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="Content/css/select2.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <br />
        <section class="content-header">
        <h1>Assets</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="AssetsAssign.aspx"><i class="fa fa-dashboard"></i>Employees</a></li>
            <li class="active">Assign Assets</li>
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
        
            <div id="companygrid" runat="server" class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Assign Assets</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdasset" runat="server"  CssClass="table table-bordered table-hover" AutoGenerateColumns="False" >
                                        <Columns>
                                            <asp:BoundField DataField="AssetParentID" HeaderText="Employee ID" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="35%" />
                                             <asp:BoundField DataField="List_Output" HeaderText="Assets List" HeaderStyle-Width="35%" />
                                          
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" ToolTip="Edit" CssClass="myLink" runat="server" Text="Edit" CommandArgument='<%#Eval("AssetParentID")%>' OnCommand="lbtnEdit_Command" CausesValidation="false" >
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" ToolTip="Delete" runat="server" Text="Delete" CommandArgument='<%#Eval("AssetParentID")%>' OnCommand="lnkDelete_Command" CausesValidation="false" >
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
        
    



    <div class="modal fade" id="AssignAssetForm" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="pnlDetail" runat="server">
                <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel">Assign Assest Add/Edit</h4>
                            </div>
                            <div class="modal-body">
                                <table class="all-table all-tables">
                                    <asp:HiddenField ID="hdnID" runat="server" />
                                    <tr runat="server">                                       
                                        <td>
                                            <label>Over All Assets</label>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstassets" runat="server" CssClass="form-control cstm-formcontrol" SelectionMode="Multiple" Width="100%"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="Requiredlistasst" ControlToValidate="lstassets"  ForeColor="red" runat="server" ErrorMessage="Assign Assest Name"></asp:RequiredFieldValidator>
                                               
                                            <%--<asp:DropDownList ID="ddlasset" runat="server"  ClientIDMode="Static" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="Requiredasset" ControlToValidate="ddlasset" InitialValue="0" ForeColor="red" runat="server" ErrorMessage="Required Assest Name"></asp:RequiredFieldValidator>--%>
                                            <%--<asp:DropDownList ID="ddlComp" runat="server" require='Please enter a Mature Days'>
                                            </asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>All Employee</label>
                                        </td>
                                        <td>
                                              <asp:DropDownList ID="ddlallemployee" runat="server"  ClientIDMode="Static" CssClass="form-control"  Width="100%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredAllEmployee" ControlToValidate="ddlallemployee" InitialValue="0" ForeColor="red" runat="server" ErrorMessage="Required Assest Name"></asp:RequiredFieldValidator>
                                                      <asp:DropDownList ID="ddlassetassignedemployee" runat="server"  Visible="false" ClientIDMode="Static" CssClass="form-control"  Width="100%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlallemployee" InitialValue="0" ForeColor="red" runat="server" ErrorMessage="Required Assest Name"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                            <td>Description
                                            </td>
                                            <td><asp:TextBox ID="txtdescription" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID= "Requireddescription" ControlToValidate="txtdescription" runat="server" ErrorMessage="Required enter description" ForeColor="Red"></asp:RequiredFieldValidator>
                                      
                                        </tr>    
                                                             
                                 
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
                                  <asp:LinkButton ID="lnkDelete" ClientIDMode="Static"  ToolTip="Delete" runat="server" Text="Yes" CssClass="submit action-button" CommandArgument='<%#Eval("AssetID")%>' CausesValidation="false" OnClick="lnkDelete_Click">
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
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ContentPlaceHolder1_lstassets").select2();
            
        });
    </script>

</asp:Content>

