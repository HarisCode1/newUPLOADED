<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="PF_Rules.aspx.cs" Inherits="PF_Rules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                    <h3 class="box-title">Type Of PF Rule</h3>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <%--OnPageIndexChanging="GridUser_PageIndexChanging" OnRowDataBound="GridUser_RowDataBound"--%>
                                <asp:GridView ID="GridEmployee" runat="server" ShowHeaderWhenEmpty="true" CssClass="GridDataTable table table-striped table-bordered dataTable " AutoGenerateColumns="False" DataKeyNames="Id">
                                    <Columns>
                                        <asp:BoundField DataField="TypeOfEmployee" HeaderText="TypeOfEmployee" SortExpression="FirstName" />
                                        <asp:BoundField DataField="SalaryType" HeaderText="SalaryType" SortExpression="FirstName" />
                                        <asp:BoundField DataField="Percent" HeaderText="Percent" SortExpression="Lastname" />
                                        <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Email" />

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
                                                <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("Id")%>' OnCommand="lbtnEdit_Command">
                                <i class="fa fa-pencil-square-o"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CssClass="confirm" CommandArgument='<%#Eval("Id")%>'  OnCommand="lbtnDelete_Modalshow">
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
                                            <h3 class="box-title">Type Of PF Rule</h3>
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
                                                        <div class="form-horizontal new_emp_form">
                                                            <h2 class="fs-title"><b>Type OF PF Rules</b></h2>
                                                            <hr />
                                                            <div class="form-group">
                                                                 <label class="col-sm-2 control-label"> Type Of PF Rule</label>
                                                                <div class="col-sm-4">
                                                                     <asp:TextBox ID="txtID" runat="server" Visible="false" ></asp:TextBox>
                                                                    <asp:DropDownList ID="ddEmployee" runat="server">
                                                                       <%-- <asp:ListItem Value="1" Text="Select Employee Type"></asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                    </div>
                                                                 <label class="col-sm-2 control-label">Type OF Salary</label>    
                                                                <div class="col-sm-4">
                                                                    <asp:DropDownList ID="ddSalaryType" runat="server"  >
                                                                         <asp:ListItem Value="1" Text="Basic"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Gross"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="Please Select"></asp:ListItem>
                                                                   </asp:DropDownList>
                                                                </div>
                                                               <%-- <label class="col-sm-2 control-label">ComapnyId </label>--%>
                                                                <div class="col-sm-4">
                                                                    <asp:TextBox ID="ComapnyId" CssClass="hidden" Text="1" class="form-control" runat="server"  ></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            
                                                                <div class="form-group">
                                                                     <label class="col-sm-2 control-label">PF Deduction (%)  </label>
                                                                     <div class="col-sm-4">
                                                                    <asp:TextBox ID="txtpercent" CssClass="" class="form-control" runat="server" validate='vgroup' require="Please Enter PF Percent" placeholder="Enter PF Percent"></asp:TextBox>
                                                                </div>
                                                                
                                                             


                                                            
                                                          
                                                                <label class="col-sm-2 control-label">Active</label>
                                                                <div class="col-sm-4">
                                                                    <asp:CheckBox ID="chkActive" runat="server" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <asp:Button ID="btnsaves" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" OnClick="btnsave_PFRules"></asp:Button>
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                           <asp:TextBox ID="TxtIDs" runat="server" Visible="false"  ></asp:TextBox>
                                                           <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" Text="Cancel" Style="width: 130px;" CssClass="submit action-button" OnCommand="lbtnDelete_ModalHide"></asp:Button>
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
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <!-- /.modal-dialog -->

    </div>
    <script>
        function ClearFields() {
            debugger
          
            $("[id $=txtID]").val("");
            $("[id $=txtpercent]").val("");
            $("[id $=btnsaves]").prop('value', 'Save');
            $("[id $=ddEmployee]").val("1");
           // $("select#elem").prop('selectedIndex', 0);
            $("[id $=ddSalaryType]").prop('selectedIndex', 2);
            $("[id $=chkActive]").prop('checked', false);
        
        }
    </script>
</asp:Content>

