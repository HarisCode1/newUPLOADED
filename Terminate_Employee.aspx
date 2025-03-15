<%@ Page Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Terminate_Employee.aspx.cs"Inherits="Terminate_Employee"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee</a></li>
            <li class="active">Employee - Transfer</li>
        </ol>
    </section>
<%--    <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">--%>
        <ContentTemplate>
            <asp:Panel ID="pnlDetail" runat="server">
                <section id="msform" class="content cstm-csform">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-info custom_input">
                                <div class="box-header with-border">
                                    <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                                    <h3 class="box-title">Employee Form</h3>
                                    <%--<div class="box-tools pull-right">
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                    <button class="btn btn-box-tool" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                                </div>--%>
                                </div>
                                <!-- /.box-header -->
                                <div id="divButtons" class="box-body NewEmp_boxBody">
                                    <div class="row">
                                        <div class="col-md-12" id="mybtn">
                                            <!-- multistep form -->
                                            <%--<form id="msform">--%>
                                            <!-- progressbar -->
                                            <ul id="progressbar" style="margin-left: 10%">
                                                <li class="active">Current Information</li>
                                                <%--<li><a href="#home">New Information</a></li>--%>
                                            </ul>
                                            <!-- fieldsets -->
                                            <%-- Start Account Setup--%>
                                            <fieldset>
                                                <div id="divemployee" class="form-horizontal new_emp_form">
                                                    <h2 class="fs-title"><b>Current Information</b></h2>
                                                    <hr />
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">First Name</label>
                                                        <div class="col-sm-4">
<%--                                                            <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" ReadOnly="true" require="Please Enter your First Name" placeholder="Enter First Name"></asp:TextBox>--%>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Last Name</label>
                                                        <div class="col-sm-4">
<%--                                                            <asp:TextBox ID="TxtLastName" class="form-control" runat="server" ReadOnly="true" require="Please Enter your Last Name" placeholder="Enter Last Name"></asp:TextBox>--%>
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Email</label>
                                                        <div class="col-sm-4">
<%--                                                            <asp:TextBox ID="TxtEmail" class="form-control" runat="server" require="Please Enter your First Name" placeholder="Enter First Name"></asp:TextBox>--%>
                                                        </div>
                                                               <label class="col-sm-2 control-label">Transfer Date</label>
      <div class="col-md-4">
<%--          <asp:TextBox type="date" id="txtEntryDate" runat="server"  class="form-control" validate='vgroup' require="Please Enter date"  --%>
   />
      </div>

<%--                                                        <label class="col-sm-2 control-label">Company</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="DdlComapny1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlComapny1_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>--%>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Department</label>
                                                        <div class="col-sm-4">
<%--                                                            <asp:DropDownList ID="DdlDepartment1" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="DdlDepartment1_SelectedIndexChanged"></asp:DropDownList>      --%>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Designation</label>
                                                        <div class="col-sm-4">
<%--                                                            <asp:DropDownList ID="DdlDesignation1" runat="server" CssClass="form-control" AutoPostBack="true"  validate='vgroup' require="Please Select" OnSelectedIndexChanged="DdlDesignation1_SelectedIndexChanged"></asp:DropDownList> --%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Line Manager</label>
                                                        <div class="col-sm-4">
<%--                                                            <asp:DropDownList ID="DdlLineManager1" runat="server" CssClass="form-control"></asp:DropDownList>      --%>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Type</label>
                                                        <div class="col-sm-4">
<%--                                                            <asp:DropDownList ID="DdlType1" runat="server" CssClass="form-control"></asp:DropDownList> --%>
                                                        </div>
                                                    </div>
                                                   <div class="form-group">
                                                        <label class="col-sm-2 control-label">Employee Code</label>
                                                        <div class="col-sm-4">
                                                            
                                                                                                                      
                                                        </div>
                                                   
                                                  
                                                    </div>
                                                    
 <div class="form-group">
                                                        <label class="col-sm-2 control-label">Image</label>
                                                        <div class="col-sm-4">
                                                 
                                <%--    <asp:HiddenField ID="hdImageName" runat="server" --%>
                              <%--   <asp:FileUpload ID="UploadDocImage" runat="server" />--%>
                                                        </div>
                                                   
                                                    </div>
                                                    <%------Department-------%>

                                                    <!-- End -->
                                                </div>
                                                
                                                <%--<asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />--%>
<%--                                                <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" OnClientClick="if (validate('vgroup')) { return true; } else { return false; }" />--%>
                                                <%--onclick="if (validate('vgroup')) { return true; } else { return false; }"--%>
                                                <%-- <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>--%>
                                            </fieldset>

                                            <fieldset>

                                                <div id="divgeneraldetails" class="new_emp_form">
                                                    <h2 class="fs-title"><b>New Information</b></h2>
                                                    <hr />
                                                    
                                                   
                                                </div>

                                                <%--onclick="if (validate('vgroup')) { return true; } else { return false; }" --%>
<%--                                                <input type="button" name="previous" class="previous action-button" value="Previous" />--%>
                                            </fieldset>
                                            <%-- End Personal Info--%>

                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </asp:Panel>

        </ContentTemplate>
      <%--  <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave" />
        </Triggers>--%>
    </asp:UpdatePanel>

    </asp:Content>