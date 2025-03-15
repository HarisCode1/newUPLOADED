<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="TypeOfEmployee.aspx.cs" Inherits="TypeOfEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="row">
        <div class="col-sm-12 " style="padding-bottom: 20px;">
            <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#employes" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>--%>
            <asp:UpdatePanel runat="server" ID="UpAddNew">
                <ContentTemplate>
                    <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
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

                                <%--OnPageIndexChanging="GridUser_PageIndexChanging" OnRowDataBound="GridUser_RowDataBound"--%>
                                <asp:GridView ID="GridEmployee" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridDataTable table table-striped table-bordered dataTable " AutoGenerateColumns="False" DataKeyNames="Id">
                                    <Columns>

                                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" SortExpression="FirstName" Visible="false" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Lastname" />
                                        <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Email" />

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
    <%-- Grid bind END--%>


    <%-- Modal Dialog--%>
    <div class="modal fade" id="Modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlDetail" runat="server">
                        <section id="msform" class="content">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box box-info custom_input">
                                        <div class="box-header with-border">
                                            <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                                            <h3 class="box-title">Type Of Employee</h3>
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
                                                        <div class="form-horizontal new_emp_form">
                                                            <h2 class="fs-title"><b>Graduity</b></h2>
                                                            <hr />
                                                            <div class="form-group">
                                                                
                                                                <label class="col-sm-2 control-label">Employee Type</label>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="TxtEmpID" runat="server" Visible="false" ></asp:TextBox>
                                                                    <asp:TextBox ID="TxtEmployee" class="form-control" runat="server" validate='vgroup' require="Please Enter your Last Name" placeholder="Enter Employee Type"></asp:TextBox>
                                                                </div>
                                                                
                                                               <%-- <label class="col-sm-2 control-label">ComapnyId </label>--%>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="ComapnyId" CssClass="hidden" Text="1" class="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            
                                                          


                                                            
                                                            <div class="form-group">
                                                                <label class="col-sm-2 control-label">Active</label>
                                                                <div class="col-sm-4">
                                                                    <asp:CheckBox ID="chkActive" runat="server" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                       
                                                        <asp:Button ID="btnsaves" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnsave_TypeEmployee"></asp:Button>
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
    
  
  <script>

      function ClearFields() {

          $("[id $=TxtEmpID]").val("");
          $("[id $=TxtEmployee]").val("");
          $("[id $=chkActive]").prop('checked', false);
          $("[id $=btnsaves]").prop('value', 'Save');

      }
    
    </script>
</asp:Content>


