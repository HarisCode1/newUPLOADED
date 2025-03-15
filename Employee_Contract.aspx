<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_Contract.aspx.cs" Inherits="Employee_Contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee</a></li>
            <li class="active">Employee - Add</li>
        </ol>
    </section>
     <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
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
                                                            <li class="active">Account Setup</li>
                                                            <li><a href="#home">Personal Information</a></li>
                                                            <li><a href="#profile" data-toggle="tab">Academic Qualification</a></li>
                                                            <li><a href="#profile" data-toggle="tab">Job Information</a></li>
                                                            <%--<li><a href="#shitWOFF" data-toggle="tab">Shift / WOFF</a></li>--%>
                                                            <li><a href="#payrollsettings" data-toggle="tab">Payroll Settings</a></li>
                                                        </ul>
                                                        <!-- fieldsets -->
                                                        <%-- Start Account Setup--%>
                                                        <%--<fieldset>
                                                            <div id="divemployee" class="form-horizontal new_emp_form">
                                                                <h2 class="fs-title"><b>Add New </b>Employee</h2>
                                                                <hr />
                                                                <div class="row labels">
                                                                    <div class="col-md-6">
                                                                        <label class="col-sm-6 control-label">First Name</label>
                                                                        <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" validate='vgroup' require="Please Enter your First Name" placeholder="Enter First Name"></asp:TextBox>

                                                                        <label class="col-sm-6 control-label">Email</label>
                                                                        <asp:TextBox ID="Txtemail" class="form-control" runat="server" validate='vgroup' require="It should be Unique to every user." email="Invalid Email" placeholder="Enter Email"></asp:TextBox>

                                                                        <label class="col-sm-6 control-label">Designation</label>
                                                                        <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="" Text=" Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <label class="col-sm-6 control-label">Line Manager Designation</label>
                                                                        <asp:DropDownList ID="ddlLineManagerDesignation" class="form-control" runat="server" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="true" >
                                                                            <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <label class="col-sm-6 control-label">Department</label>
                                                                        <asp:DropDownList ID="ddldepartment" class="form-control" runat="server" validate='vgroup' Custom="Select Department" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <label class="col-sm-6 control-label">Employee Code</label>

                                                                        <asp:TextBox ID="TxtEmployeeCode" class="form-control" runat="server" validate='vgroup' require="Please Enter your Employee Code" placeholder="Enter Employee Code"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdEmployeeID" runat="server" />

                                                                        <label class="col-sm-6 control-label">Password</label>
                                                                        <asp:TextBox ID="TxtPassword" class="form-control" runat="server" validate='vgroup' require="Minimum 4 Character" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <label class="col-sm-6 control-label">Last Name</label>
                                                                        <asp:TextBox ID="TxtLastName" class="form-control" runat="server" validate='vgroup' require="Please Enter your Last Name" placeholder="Enter Last Name"></asp:TextBox>

                                                                        <label class="col-sm-6 control-label">Company</label>
                                                                        <asp:DropDownList ID="ddlcomp" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' require="Select Company" Custom="Select Company" customFn=" var val = parseInt(this.value); return val > 0;" DataSourceID="EDS_Emp_Company" DataTextField="CompanyName" DataValueField="CompanyID" AppendDataBoundItems="true" AutoPostBack="true" >
                                                                            <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:EntityDataSource ID="EDS_Emp_Company" runat="server"
                                                                            ConnectionString="name=vt_EMSEntities"
                                                                            DefaultContainerName="vt_EMSEntities"
                                                                            EntitySetName="vt_tbl_Company">
                                                                        </asp:EntityDataSource>

                                                                        <label class="col-sm-6 control-label">Type</label>
                                                                        <asp:DropDownList ID="ddEmployeType" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="" Text="Select Type"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <label class="col-sm-6 control-label">Line Managers</label>

                                                                        <asp:DropDownList ID="ddlLineManager" class="form-control" runat="server" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <label class="col-sm-6 control-label">User Name</label>

                                                                        <asp:TextBox ID="Txtusername" class="form-control" runat="server" validate='vgroup' require="It should be Unique to every user." placeholder="Enter First & Last Name"></asp:TextBox>

                                                                        <label class="col-sm-6 control-label">Confirm Password</label>
                                                                        <asp:TextBox ID="TxtConfirmPassword" class="form-control" runat="server" validate='vgroup' require="Please Enter Confirm Password" placeholder="Confirm Password" TextMode="Password" compare='password mismatch' compareTo="ContentPlaceHolder1_TxtPassword"></asp:TextBox>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtConfirmPassword" CssClass="ValidationError" ControlToCompare="TxtPassword" ForeColor="Red" ErrorMessage="Password MisMatch" ToolTip="Password must be the same"></asp:CompareValidator>
                                                                    </div>
                                                                </div>


                                                                <div class="form-group">

                                                                    <div id="divComp" runat="server">
                                                                    </div>
                                                                </div>


                                                                <%-- <label class="col-sm-2 control-label">Role</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddlRole" class="form-control" runat="server" validate='vgroup' require="Select Role" Custom="Please Select the Role Id" customFn=" var val = parseInt(this.value); return val > 0;" DataSourceID="EDS_Role" DataTextField="Role" DataValueField="RoleID" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:EntityDataSource ID="EDS_Role" runat="server"
                                                                            ConnectionString="name=vt_EMSEntities"
                                                                            DefaultContainerName="vt_EMSEntities"
                                                                            EntitySetName="vt_tbl_Role">
                                                                        </asp:EntityDataSource>
                                                                    </div>--%>

                                                        <fieldset>
                                                            <div id="divemployee" class="form-horizontal new_emp_form">
                                                                <h2 class="fs-title"><b>Add New </b>Employee</h2>
                                                                <hr />
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">First Name</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" validate='vgroup' require="Please Enter your First Name" placeholder="Enter First Name"></asp:TextBox>
                                                                    </div>

                                                                    <label class="col-sm-2 control-label">Last Name</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="TxtLastName" class="form-control" runat="server" validate='vgroup' require="Please Enter your Last Name" placeholder="Enter Last Name"></asp:TextBox>
                                                                        <%--<asp:RequiredFieldValidator ID="reqTxtLastName" runat="server"
                                                                            ControlToValidate="TxtLastName" ErrorMessage="Enter your Last name">
                                                                        </asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Email</label>
                                                                    <div class="col-sm-4">
                                                                        <span id="spnEmail" style="color:red; display:none">Email already exist</span>
                                                                        <asp:TextBox ID="Txtemail" class="form-control" runat="server" validate='vgroup' require="It should be Unique to every user." email="Invalid Email" placeholder="Enter Email"></asp:TextBox>
                                                                    </div>
                                                                    <div id="divComp" runat="server">
                                                                        <label class="col-sm-2 control-label">Company</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:DropDownList ID="ddlcomp" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' require="Select Company" Custom="Select Company" customFn=" var val = parseInt(this.value); return val > 0;" DataSourceID="EDS_Emp_Company" DataTextField="CompanyName" DataValueField="CompanyID" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:EntityDataSource ID="EDS_Emp_Company" runat="server"
                                                                                ConnectionString="name=vt_EMSEntities"
                                                                                DefaultContainerName="vt_EMSEntities"
                                                                                EntitySetName="vt_tbl_Company">
                                                                            </asp:EntityDataSource>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Department</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddldepartment" class="form-control" runat="server" validate='vgroup' Custom="Select Department" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:DropDownList ID="ddldepartment" class="form-control" runat="server" validate='vgroup' Custom="Select Department" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                    </div>
                                                                    <div id="divDesi" runat="server">
                                                                    <label class="col-sm-2 control-label">Designation</label>
                                                                        <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' OnSelectedIndexChanged="ddlLineManager_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                                                                            
                                                                        </asp:DropDownList>--%>
                                                                        <%--  <asp:RequiredFieldValidator ID="reqddlDesignation"
                                                                            runat="server" ControlToValidate="ddlDesignation"
                                                                            ErrorMessage="Please Select"
                                                                            InitialValue="Please Select">
                                                                        </asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                   <label class="col-sm-2 control-label">Line Managers</label>
                                                                    <div class="col-sm-4">
                                                                        <%--<asp:DropDownList ID="ddlLineManager" class="form-control" runat="server" OnSelectedIndexChanged="ddlLineManager_SelectedIndexChanged" AutoPostBack="true" >
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                        <asp:DropDownList ID="ddlLineManager" class="form-control" runat="server">
                                                                            <%--<asp:ListItem Value="0" Text="Please Select"></asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <%-- <label class="col-sm-2 control-label">Role</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddlRole" class="form-control" runat="server" validate='vgroup' require="Select Role" Custom="Please Select the Role Id" customFn=" var val = parseInt(this.value); return val > 0;" DataSourceID="EDS_Role" DataTextField="Role" DataValueField="RoleID" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:EntityDataSource ID="EDS_Role" runat="server"
                                                                            ConnectionString="name=vt_EMSEntities"
                                                                            DefaultContainerName="vt_EMSEntities"
                                                                            EntitySetName="vt_tbl_Role">
                                                                        </asp:EntityDataSource>
                                                                    </div>--%>
                                                                    <label class="col-sm-2 control-label">Type</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddEmployeType" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0" Text="Select Type"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <!-- Line Manager  -->
                                                                <div class="form-group">
                                                                    <div runat="server" visible="false">
                                                                        <label class="col-sm-2 control-label">Line Manager Designation</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:DropDownList ID="ddlLineManagerDesignation" class="form-control" runat="server" Visible="false">
                                                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%--<asp:DropDownList ID="ddlLineManagerDesignation" class="form-control" runat="server" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="ddlLineManagerDesignation_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                        </div>
                                                                    </div>
                                                                    
                                                                </div>
                                                                <%------Department-------%>
                                                                
                                                                <!-- End -->
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Employee Code</label>
                                                                    <div class="col-sm-4">
                                                                        <span id="spnEmpCode" style="color:red; display:none">Employee Code already exist</span>
                                                                        <asp:TextBox ID="TxtEmployeeCode" class="form-control" runat="server" validate='vgroup' ReadOnly="true" require="Please Enter your Employee Code" placeholder="Enter Employee Code"></asp:TextBox>
                                                                        <%--<span id="lblEmployeeCode"><small style="color:red" >This Enrolled ID is already exist</small></span>--%>
                                                                        <asp:HiddenField ID="hdEmployeeID" runat="server" />
                                                                    </div>
                                                                    <label class="col-sm-2 control-label">User Name</label>
                                                                    <div class="col-sm-4">
                                                                        <span id="spnUsername" style="color:red; display:none">Username already exist</span>
                                                                        <asp:TextBox ID="Txtusername" class="form-control" runat="server" require="It should be Unique to every user." placeholder="Enter First & Last Name"></asp:TextBox>
                                                                        
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Password</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="TxtPassword" class="form-control" runat="server" CssClass="ValidationError" require="Minimum 4 Character" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                                                    </div>
                                                                    <label class="col-sm-2 control-label">Confirm Password</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="TxtConfirmPassword" class="form-control" runat="server" require="Please Enter Confirm Password" placeholder="Confirm Password" TextMode="Password" compare='password mismatch' compareTo="ContentPlaceHolder1_TxtPassword"></asp:TextBox>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtConfirmPassword" CssClass="ValidationError" ControlToCompare="TxtPassword" ForeColor="Red" ErrorMessage="Password MisMatch" ToolTip="Password must be the same"></asp:CompareValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--<input type="button" name="next" class="next action-button" value="Next" />--%>
                                                            <%--onclick="if (validate('vgroup')) { return true; } else { return false; }"--%>
                                                            <%-- <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>--%>
                                                        </fieldset>


                                                                <!-- Line Manager  -->

                                                                <!-- End -->


                                                            <%--</div>
                                                            <input type="button" name="next" class="next action-button mr-10" value="Next" />--%>
                                                            <%--onclick="if (validate('vgroup')) { return true; } else { return false; }"--%>
                                                            <%-- <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>--%>
                                                        <%--</fieldset>--%>
                                                        <%-- End Account Setup--%>
                                                        <%-- Start Personal Info--%>
                                                        <fieldset>

                                                            <div id="divgeneraldetails" class="new_emp_form">
                                                                <h2 class="fs-title"><b>General Details </b></h2>
                                                                <hr />
                                                                <div class="row">
                                                                    <div class="col-md-12 mt-20">
                                                                            <asp:HiddenField ID="hdEmpPhotoID" runat="server" />
                                                                            <asp:HiddenField ID="hdImageName" runat="server" />
                                                                            <asp:HiddenField ID="hdEnrollId" runat="server" />
                                                                            <asp:Image ID="empImageView" ImageUrl="~/assets/img/user2-160x160.jpg" runat="server" CssClass="img-circle align" />
                                                                        <div class="file-upload-wrapper" data-text="Select your file!">
                                                                            <asp:FileUpload ID="uploadEmpImage" runat="server" />
                                                                        </div>
                                                                            <asp:Button ID="btnUpload" runat="server"  Height="0" Width="0" BackColor="#ECF0F5" BorderStyle="None" />
                                                                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row labels">
                                                                    <div class="col-md-6">
                                                                        <label class="col-sm-6 control-label">Father/Husband Name:</label>
                                                                        <asp:TextBox ID="txtFatherName" validate='vgroup' require="Please Select" class="form-control" runat="server" Placeholder="Enter Father/Husband Name"></asp:TextBox>                                                                       

                                                                        <label class="col-sm-6 control-label">Date of Birth:</label>

                                                                        <asp:TextBox ID="txtDOB" runat="server" validate='vgroup' require="Please Select" CssClass="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                                        <label class="col-sm-6 control-label">Home Phone :</label>
                                                                        <asp:TextBox ID="TxtHomePhone" validate='vgroup' require="Please Select" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="TxtHomePhone" Mask="999-99999999"
                                                                            MessageValidatorTip="true" 
                                                                            OnFocusCssClass="MaskedEditFocus" 
                                                                            OnInvalidCssClass="MaskedEditError"
                                                                            MaskType="Number" 
                                                                            InputDirection="RightToLeft"
                                                                            ErrorTooltipEnabled="True"
                                                                            ClearMaskOnLostFocus="false"/>
                                                                        <label class="col-sm-6 control-label">Marital Status:</label>


                                                                        <asp:RadioButtonList ID="rdoMarriedStatus" CssClass="radiobuton" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Class="CustomLabel" runat="server">Married</asp:ListItem>
                                                                            <asp:ListItem>Unmarried</asp:ListItem>
                                                                        </asp:RadioButtonList>          
                                                                    </div>

                                                                    
                                                                </div>
                                                                <h3 class="mt-40 text-left mb-15">Current Address</h3>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <label class="control-label pull-left">Address Line 1:</label>
                                                                        <asp:TextBox ID="TxtCurrent_Address1" runat="server" CssClass="emp-address form-control" Placeholder="Enter Address 1"></asp:TextBox>
                                                                    </div>                                                                
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">City:</label>
                                                                        <asp:TextBox ID="TxtCurrent_City" runat="server" CssClass="emp-address form-control" Placeholder="Enter City"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">State/Region:</label>
                                                                        <asp:TextBox ID="TxtCurrent_State" runat="server" CssClass="emp-address form-control" Placeholder="Enter State/Region"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">Zip/Postal Code:</label>
                                                                        <asp:TextBox ID="TxtCurrent_Zip" runat="server" CssClass="emp-address form-control" Placeholder="Enter Zip/Postal Code"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">Country:</label>
                                                                        <asp:TextBox ID="TxtCurrent_Country" runat="server" CssClass="emp-address form-control" Placeholder="Enter Country"></asp:TextBox>
                                                                    </div>
                                                                </div>                                                                                                                        
                                                                <div class="row" runat="server" visible="false">
                                                                    <div class="col-sm-10" runat="server">
                                                                        <label class="control-label pull-left">Current Address :</label>
                                                                        <asp:TextBox ID="txtCurrentAddress" require="Please Select" runat="server" TextMode="MultiLine" Rows="7" 
                                                                            CssClass="emp-address form-control"></asp:TextBox>
                                                                    </div>                                        
                                                                    
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-8">
                                                                        <div class="form-horizontal">
                                                                            <div class="form-group">
                                                                            </div>
                                                                            <div class="form-group">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <div class="form-group">

                                                                            <div class="col-sm-10 display-none">
                                                                                <%--<asp:HiddenField ID="hdEmpPhotoID" runat="server" />
                                                                                <asp:HiddenField ID="hdImageName" runat="server" />
                                                                                <asp:HiddenField ID="hdEnrollId" runat="server" />
                                                                                <asp:Image ID="empImageView" ImageUrl="~/assets/img/user2-160x160.jpg" runat="server" CssClass="img-circle align" />
                                                                                <asp:FileUpload ID="uploadEmpImage" CssClass="upload_btn" runat="server" />
                                                                                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Height="0" Width="0" BackColor="#ECF0F5" BorderStyle="None" />
                                                                                <asp:Label ID="lblmsg" runat="server"></asp:Label>--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <%--  Other Details--%>
                                                                <div class="form-horizontal">
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group text-left cstm-form-group">
                                                                            <h2 class="fs-title"><b>Documents Upload</b></h2>
                                                                            <div class="form-group">                                                                                

                                                                                <div class="choose-file mb-10">
                                                                                    <asp:HiddenField ID="hdNICName" runat="server" />
                                                                                    <asp:HiddenField ID="hdNICImage" runat="server" />
                                                                                    <asp:Label ID="lblNIC" runat="server" class="control-label"></asp:Label>
                                                                                    <asp:Label ID="txtNIC" runat="server" Text="Attach NIC"></asp:Label>
                                                                                    <asp:FileUpload ID="NICUpload" runat="server" />
                                                                                </div>                                             
                                                                             
                                                                            </div>

                                                                            <div class="form-group">
                                                                            </div>
                                                                            <div class="form-group">
                                                                            </div>
                                                                            <div class="form-group">
                                                                            </div>
                                                                            <div class="form-group">
                                                                            </div>
                                                                            <%--<asp:FileUpload ID="FileUpload3" runat="server" />
                                                                        <asp:TextBox ID="txtCV" runat="server" placeholder="A
                                                                             <asp:FileUpload ID="FileUpload4" runat="server" />
                                                                        <asp:TextBox ID="txtCV" runat="server" placeholder="A--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <input type="button" name="next" class="next action-button" value="Next" />
                                                            <%--onclick="if (validate('vgroup')) { return true; } else { return false; }" --%>
                                                            <input type="button" name="previous" class="previous action-button" value="Previous" />

                                                        </fieldset>
                                                        <%-- End Personal Info--%>

                                                        <%--  Academic Qualification Info--%>
                                              <%--          <fieldset>
                                                            <div class="col-md-12 animated bounceInDown" id="mybtn1">
                                                                <ul id="progressbarQualification" style="margin-left: 10%">
                                                                    <li class="active">Academic</li>
                                                                    <li><a href="#">Certificates</a></li>
                                                                </ul>
                                                            </div>
                                                            <fieldset>
                                                                <div id="divacademicinfo" class="box-body NewEmp_boxBody divhide">
                                                                    <div class="form-horizontal new_emp_form labels">
                                                                        <h2 class="fs-title"><b>Academic Info </b></h2>
                                                                        <hr />
                                                                        <div class="row mb-5">
                                                                            <div class="col-md-6">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="hdnIdAcademic" runat="server" />

                                                                                        <label class="col-sm-12">Institute Name</label>
                                                                                        <asp:TextBox ID="txtinsname" runat="server" require="Please Enter Institute" placeholder="Enter Institute"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <label class="col-sm-12">Year</label>
                                                                                        <asp:TextBox ID="txtyear" runat="server" class="is-number" require="Please Enter Year" placeholder="Enter Year"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <tr>
                                                                                    <td>
                                                                                        <label class="col-sm-12">Degree / Majors</label>
                                                                                        <asp:TextBox ID="txtqualification" runat="server" require="Please Enter Qualification" placeholder="Enter Degree / Majors"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <label class="col-sm-12">C GPA</label>
                                                                                        <asp:TextBox ID="txtmarks" class="col-sm-2 is-number-with-decimal" runat="server" require="Please Enter Marks" placeholder="Enter CGPA"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </div>
                                                                            <tr>
                                                                                <td>
                                                                                    <input type="button" id="btninssave" class="action-button" value="Add" />
                                                                                </td>
                                                                            </tr>
                                                                        </div>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>Institute Name</th>
                                                                                        <th>Qualification</th>
                                                                                        <th>Year</th>
                                                                                        <th>Marks</th>
                                                                                        <th>Action</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody id="tblAcademicInfo" class="tbody">
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                    <asp:HiddenField ID="txtHidden_Academic" runat="server" />
                                                                </div>
                                                            </fieldset>
                                                            <fieldset>
                                                                <div id="divcertificates" class="box-body NewEmp_boxBody">
                                                                    <div>
                                                                        <div class="form-horizontal new_emp_form labels">
                                                                            <h2 class="fs-title"><b>Certificate Info</b></h2>
                                                                            <hr />
                                                                            <div class="row mb-5">
                                                                                <div class="col-md-6">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label class="col-sm-12">Institute Name</label>
                                                                                            <asp:TextBox ID="txtcrtfctname" runat="server" require="Please Enter Institute" placeholder="Enter Institute"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label class="col-sm-12">Year</label>
                                                                                            <asp:TextBox ID="txtcrtfctinsYear" runat="server" class="is-number" require="Please Enter Year" placeholder="Enter Year"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label class="col-sm-12">Degree / Majors</label>
                                                                                            <asp:TextBox ID="txtcrtfctqualification" class="col-sm-2" runat="server" require="Please Enter Marks" placeholder="Enter Degree / Majors"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label class="col-sm-12">Grade</label>
                                                                                            <asp:TextBox ID="txtcrtfctgrade" class="col-sm-2" runat="server" require="Please Enter Grade" placeholder="Enter Grade"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </div>
                                                                                <tr>
                                                                                    <td>
                                                                                        <input type="button" id="btncrtfctsaves" class="action-button" value="Add" />
                                                                                    </td>
                                                                                </tr>
                                                                            </div>
                                                                            <table class="table table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>InstituteName</th>
                                                                                        <th>Qualification</th>
                                                                                        <th>Year</th>
                                                                                        <th>Grade</th>
                                                                                        <th>Action</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody id="tblcertificateInfo" class="tbodys">
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                        <asp:HiddenField ID="txt_HiidenCertificate" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                            <input type="button" name="next" class="next1 action-button" value="Next" />
                                                            <input type="button" name="previous" class="previous1 action-button" value="Previous" />

                                                        </fieldset>
                                                        <fieldset>
                                                            <div id="divcategories" class="form-horizontal hidden">
                                                                <h2 class="fs-title hidden"><b>Categories </b></h2>
                                                                <hr />
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label hidden">Branch :</label>
                                                                    <div class="col-sm-4 hidden">
                                                                        <asp:DropDownList ID="ddlBranch" CssClass="form-control" AppendDataBoundItems="true" custom="Please Select Branch" runat="server" DataSourceID="EDS_Branch" DataTextField="BranchShortName" DataValueField="BranchID">
                                                                            <asp:ListItem Value="0" Text="Please Select Branch..."></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:EntityDataSource ID="EDS_Branch" runat="server"
                                                                            ConnectionString="name=vt_EMSEntities"
                                                                            DefaultContainerName="vt_EMSEntities"
                                                                            EntitySetName="vt_tbl_Branch"
                                                                            Where='<%# "it.CompanyId ="+ ddlcomp.SelectedValue %>'>
                                                                        </asp:EntityDataSource>
                                                                    </div>
                                                                    <%--<label class="col-sm-2 control-label hidden">Type :</label>
                                                                    <div class="col-sm-4 hidden">
                                                                        <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="0" Text="Please Select Type..."></asp:ListItem>
                                                                            <asp:ListItem Text="None">None</asp:ListItem>
                                                                            <asp:ListItem Text="Permanent">Permanent</asp:ListItem>
                                                                            <asp:ListItem Text="Probation">Probation</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>--%>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div id="divbank" class="form-horizontal hidden">
                                                                <div class="form-group hidden">
                                                                    <label class="col-sm-2 control-label">Bank :</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList AppendDataBoundItems="true" CssClass="form-control" ID="ddlBank" runat="server" custom="Please Select Bank" DataSourceID="EDS_Bank" DataTextField="BankName" DataValueField="BankID">
                                                                            <asp:ListItem Value="0" Text="Please Select Bank..."></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:EntityDataSource ID="EDS_Bank" runat="server"
                                                                            ConnectionString="name=vt_EMSEntities"
                                                                            DefaultContainerName="vt_EMSEntities"
                                                                            EntitySetName="vt_tbl_Bank"
                                                                            Where='<%# "it.CompanyID ="+ Viftech.vt_Common.CompanyId %>'>
                                                                        </asp:EntityDataSource>
                                                                    </div>
                                                                    <label class="col-sm-2 control-label">A/c No:</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <h2 class="fs-title"><b>Job Detail </b></h2>                                            
                                                            <div class="row cstm-align-labels">
                                                                <div class="col-md-6">                                                                                                                                       
                                                                    <label class="col-sm-12 control-label">
                                                                    Job Status:</label>
                                                                    <div id="RJobStatusr" class="radiobuton">
                                                                        <asp:RadioButtonList ID="rdoJobStatus" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Selected="True" Text="Active">Active</asp:ListItem>
                                                                            <asp:ListItem Text="Deactive">Deactive</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <div class="Deactive" style="display: none">
                                                                        <label class="col-sm-4 control-label">
                                                                        Leaving Reason:</label>
                                                                        <div id="Div1" class="input-group col-sm-8">
                                                                            <asp:DropDownList ID="ddlLeavingReason" runat="server" CssClass="form-control">
                                                                                <asp:ListItem>C</asp:ListItem>
                                                                                <asp:ListItem>S</asp:ListItem>
                                                                                <asp:ListItem>R</asp:ListItem>
                                                                                <asp:ListItem>D</asp:ListItem>
                                                                                <asp:ListItem>P</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    Joining Date:
                                                                    <div id="dtJoiningDate">
                                                                        <asp:TextBox ID="txtJoiningDate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" require="Please Enter Joining Date" validate="vgroup"></asp:TextBox>
                                                                    </div>
                                                                    <div class="Deactive" style="display: none">
                                                                        <label class="col-sm-4 control-label">
                                                                        Leaving Date:</label>
                                                                        <div id="dtLeavingDate" class="col-sm-8">
                                                                            <asp:TextBox ID="txtLeavingDate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    
                                                                </div>
                                                            </div>
                                                            <input type="button" name="next" class="next action-button" value="Next" />
                                                            <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                            <br>
                                                            </br>
                                                        </fieldset>--%>
                                                        <fieldset>
                                                            <div id="divpayrollsetting" class="form-horizontal">
                                                                <h2 class="fs-title"><b>Payroll Setting </b></h2>
                                                                <br />
                                                                <div class="row cstm-align-labels">
                                                                    <div class="col-md-6">
                                                                        <label class="col-sm-12">Basic Salary</label>
                                                                        <asp:TextBox ID="txtBasicSalary" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="Basic Salary" MaxLength="9" max="9" ></asp:TextBox>
                                                                                                                                                                                                                      
                                                                    </div>                                                                                                                                    
                                                                </div>

                                                                <h2 class="fs-title"><b>Bank Details </b></h2>

                                                                <div class="row cstm-align-labels">
                                                                    <div class="col-md-6">
                                                                        <label class="col-sm-12 ">From Bank</label>
                                                                        <asp:TextBox ID="txtbankfrom" validate='vgroup' require="Please Enter" runat="server" placeholder="From Bank"></asp:TextBox>

                                                                        <label class="col-sm-12 ">To Bank</label>
                                                                        <asp:TextBox ID="txtbankto" validate='vgroup' require="Please Enter" runat="server" placeholder="To Bank"></asp:TextBox>

                                                                        <label class="col-sm-12 ">Account No</label>
                                                                        <asp:TextBox ID="txtaccount" validate='vgroup' onkeypress="return isNumberKey(event);" require="Please Enter" runat="server" placeholder="Account NO"></asp:TextBox>

                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <label class="col-sm-12 ">From Branch</label>
                                                                        <asp:TextBox ID="txtbrachfrom" validate='vgroup' require="Please Enter" runat="server" placeholder="From Branch"></asp:TextBox>

                                                                        <label class="col-sm-12 ">To  Branch</label>
                                                                        <asp:TextBox ID="txtbranchto" validate='vgroup' require="Please Enter" runat="server" placeholder="To Branch"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>           
                                                                                                         
                                                            <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                            </label>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </section>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                         <asp:PostBackTrigger ControlID="btnSaveEmployee"/>
                    </Triggers>
                </asp:UpdatePanel>
    
    
    <script src="assets/js/jquery.easing.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>

     <script type="text/javascript">
        'use strict';
          

        



        $(function () {

            $("#<%= ddEmployeType.ClientID %>").change(function(){
                if ($(this).find("option:selected").text() == "Contract") {
                    $(".divhide").hide();
                }
            })
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function binddata() {

            $('#mybtn,#mybtn1').addClass('animated bounceInDown');

            $('#txtDOB').datepicker({
                dateFormat: 'dd-mm-yy',
                autoclose: true,
                clearBtn: false
            });
            $('#txtWeddAnnev').datepicker({
                dateFormat: 'dd-mm-yy',
                autoclose: true,
                clearBtn: false
            });
            $('#txtJoiningDate').datepicker({
                dateFormat: 'dd-mm-yy',
                autoclose: true,
                clearBtn: false
            });
            $('#txtLeavingDate').datepicker({
                dateFormat: 'dd-mm-yy',
                autoclose: true,
                clearBtn: false
            });
            $('#txtConfrDate').datepicker({
                dateFormat: 'dd-mm-yy',
                autoclose: true,
                clearBtn: false
            });

            //$("[id $=uploadEmpImage]").change(function () {
            //    $("[id $=btnUpload]").click();
            //});
       
            // $('.datetimepick').datetimepicker({ pickTime: false }).on('changeDate', function (e) { $(this).datetimepicker('hide'); });
            $("[id$=grdEmp]").prepend($("<thead></thead>").append($("[id$=grdEmp]").find("tr:first"))).dataTable();
            $("#progressbar li a").each(function () {
                $(this).click(function () {
                    return false;
                })
            });

            NextButton();
            PreviousButton();
            NextButtonQualification();
            PreviousButtonQualification();
            fileUpload();
            lunchallw();
            MaritalStatus();
            LateAllowedMin();
            EarlyAllowedMin();
            HalfDayMin();
            FullDayMin();
            graceperd();
            Woff();
            Confr();
            Prov();
            OnclienClick();
            fileUpload();
            AddRecord();
            AcademicDataBind();

        }

        function Saveonclick() {
            alert('ok');
            
            if (validate('vgroup') == true) {
                window.location.href = "Employee.aspx";
                msgbox(1, "Sucess", "Successfully Saved!");
              //  getCertificateRecords();
                return true;

            }
            else {
                return false;
            }
        }
       
           
      

        function AcademicDataBind() {
            var arrayacademic;
            if ($('[id*=txtHidden_Academic]').val() != "") {
                arrayacademic = JSON.parse($('[id*=txtHidden_Academic]').val());

                if (arrayacademic.length > 0) {
                    $(arrayacademic).each(function (key, value) {

                        $("#tblAcademicInfo").append('<tr><td>' + value.InstituteName + '</td><td>' + value.Qualification + '</td><td>' + value.Year + '</td><td>' + value.Marks + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    });
                }
            }

            var ArrayCertificate;
            if ($('[id*=txt_HiidenCertificate]').val() != "") {
                ArrayCertificate = JSON.parse($('[id*=txt_HiidenCertificate]').val());

                if (ArrayCertificate.length > 0) {
                    $(ArrayCertificate).each(function (key, value) {

                        $("#tblcertificateInfo").append('<tr><td>' + value.InstituteName + '</td><td>' + value.Qualification + '</td><td>' + value.Year + '</td><td>' + value.Grade + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    });
                }
            }
        }

        function AddRecord() {

            $("#btninssave").click(function () {
                var name = $('[id*=txtinsname]').val();
                var qualification = $('[id*=txtqualification]').val();
                var year = $('[id*=txtyear]').val();
                var grade = $('[id*=txtmarks]').val();
                if (name != "" && qualification != "" && year != "" && grade != "") {
                    $(".tbody").append('<tr><td>' + name + '</td><td>' + qualification + '</td><td>' + year + '</td><td>' + grade + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    clearfields();
                }
            });


            $("#btncrtfctsaves").click(function () {
                var name = $('[id*=txtcrtfctname]').val();
                var qualification = $('[id*=txtcrtfctqualification]').val();
                var year = $('[id*=txtcrtfctinsYear]').val();
                var marks = $('[id*=txtcrtfctgrade]').val();
                if (name != "" && qualification != "" && year != "" && marks != "") {

                    $(".tbodys").append('<tr><td>' + name + '</td><td>' + qualification + '</td><td>' + year + '</td><td>' + marks + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    clearfields();
                }
            });
        }

        function RemoveRecord(row) {
            row.parent().parent().remove();

        }

        function clearfields() {
            $('[id*=txtinsname]').val('');
            $('[id*=txtqualification]').val('');
            $('[id*=txtyear]').val('');
            $('[id*=txtmarks]').val('');
            $('[id*=txtcrtfctname]').val('');
            $('[id*=txtcrtfctqualification]').val('');
            $('[id*=txtcrtfctinsYear]').val('');
            $('[id*=txtcrtfctgrade]').val('');
        }

        function fileUpload() {

            $('[id $=uploadEmpImage]').off('change').on('change', e => {
                var formdata = new FormData();
                var files = $('input[type="file"][id $=uploadEmpImage]').get(0).files;
                if (files.length > 0) {
                    formdata.append("ProfileImg", files[0]);
                    formdata.append("ImageName", $("[id$=chkFinancials]"));
                    formdata.append("chkStatistics", $("[id$=chkStatistics]").prop("checked"));

                    $.ajax({
                        type: 'Post',
                        url: 'Service/PostImage.asmx/PostFile',
                        async: false,
                        contentType: false,
                        processData: false,
                        data: formdata,
                        success: function (data) {
                            if ($(data).text() != "") {
                                $('[id $=empImageView]').attr('src', '/images/Employees/' + $(data).text());
                                $('[id $=hdImageName]').val($(data).text());
                            }
                            else {
                                msgbox(4, "Error", "File is not Supported!");
                                $('[id $=empImageView]').attr('src', '/assets/img/user2-160x160.jpg');
                                $('[id $=hdImageName]').val('');
                            }
                        }
                    });
                }
                return false;
            });

            $('[id $=NICUpload]').off('change').on('change', e => {
                var formdata = new FormData();
                var files = $('input[type="file"][id $=NICUpload]').get(0).files;
                if (files.length > 0) {
                    formdata.append("ProfileImg", files[0]);
                    $('.labelNIC').text('');
                    $.ajax({
                        type: 'Post',
                        url: 'Service/PostImage.asmx/PostFile',
                        async: false,
                        contentType: false,
                        processData: false,
                        data: formdata,
                        success: function (data) {
                            if ($(data).text() != "") {
                                $('[id $=hdNICName]').val($(data).text());
                            }
                            else {
                                $('[id $=NICUpload]').val('');
                                msgbox(4, "Error", "File is not Supported!");
                            }
                        }
                    });
                }
                return false;
            });
            $('[id $=RearImageUpload]').off('change').on('change', e => {
                var formdata = new FormData();
                var files = $('input[type="file"][id $=NICUpload]').get(0).files;
                if (files.length > 0) {
                    formdata.append("ProfileImg", files[0]);
                    $('.labelNIC').text('');
                    $.ajax({
                        type: 'Post',
                        url: 'Service/PostImage.asmx/PostFile',
                        async: false,
                        contentType: false,
                        processData: false,
                        data: formdata,
                        success: function (data) {
                            if ($(data).text() != "") {
                                $('[id $=hdnRearName]').val($(data).text());
                            }
                            else {
                                $('[id $=RearImageUpload]').val('');
                                msgbox(4, "Error", "File is not Supported!");
                            }
                        }
                    });
                }
                return false;
            });

            $('[id $=PassportUpload]').off('change').on('change', e => {
                var formdata = new FormData();
                var files = $('input[type="file"][id $=PassportUpload]').get(0).files;
                if (files.length > 0) {
                    formdata.append("ProfileImg", files[0]);
                    $('.LabelPasport').text('');
                    $.ajax({
                        type: 'Post',
                        url: 'Service/PostImage.asmx/PostFile',
                        async: false,
                        contentType: false,
                        processData: false,
                        data: formdata,
                        success: function (data) {
                            if ($(data).text() != "") {
                                $('[id $=hdpasportname]').val($(data).text());
                            }
                            else {
                                $('[id $=txtpassport]').val('');
                                msgbox(4, "Error", "File is not Supported!");
                            }
                        }
                    });
                }
                return false;
            });

            $('[id $=ResumeFileUpload]').off('change').on('change', e => {
                var formdata = new FormData();
                var files = $('input[type="file"][id $=ResumeFileUpload]').get(0).files;
                if (files.length > 0) {
                    formdata.append("ResumeUploadFile", files[0]);
                    $('.labelresume').text('');
                    $.ajax({
                        type: 'Post',
                        url: 'Service/PostImage.asmx/PostResumeFile',
                        async: false,
                        contentType: false,
                        processData: false,
                        data: formdata,
                        success: function (data) {
                            if ($(data).text() !== "") {
                                $('[id $=hdResumeFileUpload]').val($(data).text());
                            }
                            else {
                                $('[id $=ResumeFileUpload]').val('');
                                msgbox(4, "Error", "File is not Supported!");
                            }
                        }
                    });
                }
                return false;
            });


            $('[id $=documentsupload]').off('change').on('change', e => {
                var formdata = new FormData();
                var files = $('input[type="file"][id $=documentsupload]').get(0).files;
                if (files.length > 0) {
                    formdata.append("DocumentsUpload", files[0]);
                    $('.labeldocuments').text('');
                    $.ajax({
                        type: 'Post',
                        url: 'Service/PostImage.asmx/DocumentsUpload',
                        async: false,
                        contentType: false,
                        processData: false,
                        data: formdata,
                        success: function (data) {
                            if ($(data).text() != "") {
                                $('[id $=hddocumenstupload]').val($(data).text());
                            }
                            else {
                                $('[id $=documentsupload]').val('');
                                msgbox(4, "Error", "File is not Supported!");
                            }
                        }
                    });
                }
                return false;
            });

            $('[id $=otherdocumetssupload]').off('change').on('change', e => {
                var formdata = new FormData();
                var files = $('input[type="file"][id $=otherdocumetssupload]').get(0).files;
                if (files.length > 0) {
                    formdata.append("OtherDocumetsUpload", files[0]);
                    $('.labelotherdocuments').text('');
                    $.ajax({
                        type: 'Post',
                        url: 'Service/PostImage.asmx/OtherDocumetsUpload',
                        async: false,
                        contentType: false,
                        processData: false,
                        data: formdata,
                        success: function (data) {
                            if ($(data).text() != "") {
                                $('[id $=hdotherdocumntsupload]').val($(data).text());
                            }
                            else {
                                $('[id $=otherdocumetssupload]').val('');
                                msgbox(4, "Error", "File is not Supported!");
                            }
                        }
                    });
                }
                return false;
            });
        };


        function lunchallw() {
            $('[id $=chkLunchAsPerShiftRule]').change(function () {
                $('[id $=txtLunchAllowdMins]').prop("disabled", $(this).prop("checked")).val("", $(this).prop("checked"));
            });
            $('[id $=chkLunchAsPerShiftRule]').trigger("change");
        };
        function MaritalStatus() {
            $('[id $=rdoMarriedStatus]').change(function () {
                $('[id $=dtWeddAnnev]').prop("disabled", $(this).prop("checked")).val("", $(this).prop("checked"));
                var isMarried = $("input[name$='rdoMarriedStatus']:radio:checked").val();

                if (isMarried != "Married")
                    $('[id$=MarriedShow]').hide();
                else
                    $('[id$=MarriedShow]').show();
            });
            $("[id$=rdoMarriedStatus]").trigger("change");
            $("[id$=chkCurrentAddress]").change(function () {
                if ($('[id $=chkCurrentAddress]').is(':checked')) {
                    $("[id$=txtParmanantAddress]").val($("[id$=txtCurrentAddress]").val());
                    $("[id$=txtParmanantAddress]").attr("readonly", "readonly");
                }
                else {
                    $("[id$=txtParmanantAddress]").val('');
                    $("[id$=txtParmanantAddress]").removeAttr("readonly");
                }
            });
            $("[id$=txtCurrentAddress]").change(function () {
                if ($('[id $=chkCurrentAddress]').is(':checked')) {
                    $("[id$=txtParmanantAddress]").val($("[id$=txtCurrentAddress]").val());
                }
            });
            $("[id$=chkCurrentAddress]").trigger("change");
        };
        function LateAllowedMin() {
            $('[id $=chkLateAllowdAsShift]').change(function () {
                $('[id $=txtLateAllowedMin]').prop("disabled", $(this).prop("checked")).val("", $(this).prop("checked"));
            });
            $("[id$=chkLateAllowdAsShift]").trigger("change");
        };
        function EarlyAllowedMin() {
            $('[id $=chkEarlyAllowedAsShift]').change(function () {
                $('[id $=txtEarlyAllowedMin]').prop("disabled", $(this).prop("checked")).val("", $(this).prop("checked"));
            });
            $("[id$=chkEarlyAllowedAsShift]").trigger("change");
        };
        function HalfDayMin() {
            $('[id $=chkHalfDayMinAsShift]').change(function () {
                $('[id $=txtHalfDayMin]').prop("disabled", $(this).prop("checked")).val("", $(this).prop("checked"));
            });
            $("[id$=chkHalfDayMinAsShift]").trigger("change");
        };
        function FullDayMin() {
            $('[id $=chkFullDayMinAsShift]').change(function () {
                $('[id $=txtFullDayMin]').prop("disabled", $(this).prop("checked")).val("", $(this).prop("checked"));
            });
            $("[id$=chkFullDayMinAsShift]").trigger("change");
        };
        function graceperd() {
            $('[id $=chkOTAsPerShiftRule]').change(function () {
                $('[id $=txtGracePeriod]').prop("disabled", $(this).prop("checked")).val("", $(this).prop("checked"));
            });
            $("[id$=chkOTAsPerShiftRule]").trigger("change");
        };
        function Woff() {
            $('[id $=chkSecondWeeklyOff]').click(function () {
                if ($(this).is(":checked")) {
                    $('.SecondWeeklyOff').show();

                } else {
                    $('.SecondWeeklyOff').hide();
                }
            });
            $("[id$=chkSecondWeeklyOff]").trigger("click");
        };
        function Confr() {
            $('[id $=rdoConfirmation]').change(function () {
                if ($(this).is(":checked")) {
                    $('[id$=Conf]').show();
                    $('[id$=Prov]').hide();

                }
            });
            $("[id$=rdoConfirmation]").trigger("change");
        };
        function Prov() {
            $('[id $=rdoProb]').change(function () {
                if ($(this).is(":checked")) {
                    $('[id$=Prov]').show();
                    $('[id$=Conf]').hide();
                }
            });
            $("[id$=rdoProb]").trigger("change");
        };
        function OnclienClick() {
            var ddlShiftVal = $("[id$=ddlShift] option:selected").val();
            $("[id$=hdDdlShift]").val(ddlShiftVal);
        }
        //jQuery time
        var current_fs, next_fs, previous_fs; //fieldsets
        var left, opacity, scale; //fieldset properties which we will animate
        var animating; //flag to prevent quick multi-click glitches        

        function getCertificateRecords() {

            var arrayacademic = [];

            $("#tblAcademicInfo tr").each(function (key, value) {
                var arr = {};
                arr.InstituteName = $(this).closest('tr').find('td:eq(0)').html();
                arr.Qualification = $(this).closest('tr').find('td:eq(1)').html();
                arr.Year = parseInt($(this).closest('tr').find('td:eq(2)').html());
                arr.Marks = parseInt($(this).closest('tr').find('td:eq(3)').html());

                arrayacademic.push(arr);
            });

            $('#ContentPlaceHolder1_txtHidden_Academic').val(JSON.stringify(arrayacademic));


            var arraycertificate = [];
            $("#tblcertificateInfo tr").each(function (key, value) {
                var arrcertificate = {};
                arrcertificate.InstituteName = $(this).closest('tr').find('td:eq(0)').html();
                arrcertificate.Qualification = $(this).closest('tr').find('td:eq(1)').html();
                arrcertificate.Year = parseInt($(this).closest('tr').find('td:eq(2)').html());
                arrcertificate.Grade = $(this).closest('tr').find('td:eq(3)').html();
                arraycertificate.push(arrcertificate);

            });

            $('#ContentPlaceHolder1_txt_HiidenCertificate').val(JSON.stringify(arraycertificate));

        }

        function NextButton() {
            $(".next").click(function () {

                //Academic Popup
                if ($('#divacademicinfo').is(':visible')) {
                    var count = $('#tblAcademicInfo tr').length;
                    if (count <= 0) {
                        msgbox(4, "Error", "Please enter the qualitifcation record !");
                        return false;
                    }
                }
                    //Certificate Popup
                else if ($('#divcertificates').is(':visible')) {
                    var count = $('#tblcertificateInfo tr').length;
                    if (count <= 0) {
                        msgbox(4, "Error", "Please enter the qualitifcation record !");
                        return false;
                    }
                }

                if (animating) return false;

                animating = true;
                validate('vgroup');
                current_fs = $(this).parent();
                next_fs = $(this).parent().next();
                var isValid = true;
                current_fs.find(".form-group").each(function () {
                    if ($(this).hasClass("has-error") || $(this).hasClass("has-warning")) {
                        isValid = false;
                        return;
                    }
                });
                next_fs.find(".form-group").each(function () {
                    $(this).removeClass("has-error");
                    $(this).removeClass("has-warning");
                    $(this).removeClass("has-success");
                    $(this).children("span").removeClass('glyphicon-warning-sign')
                    $(this).children("span").removeClass('glyphicon-ok')
                    return;
                });
                if (isValid) {

                    if ($("#progressbar li").eq($("fieldset").index(0) - 3).hasClass('active') && $("#progressbar li").eq($("fieldset").index(0) - 4).hasClass('active')) {
                        $("#progressbar li").eq($("fieldset").index(next_fs) - 2).addClass("active");
                    } else {
                        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
                    }

                    //activate next step on progressbar using the index of next_fs

                    //show the next fieldset
                    next_fs.show();
                    //hide the current fieldset with style
                    current_fs.animate({ opacity: 0 }, {
                        step: function (now, mx) {
                            //as the opacity of current_fs reduces to 0 - stored in "now"
                            //1. scale current_fs down to 80%
                            scale = 1 - (1 - now) * 0.2;
                            //2. bring next_fs from the right(50%)
                            left = (now * 50) + "%";
                            //3. increase opacity of next_fs to 1 as it moves in
                            opacity = 1 - now;
                            current_fs.css({ 'transform': 'scale(' + scale + ')' });
                            next_fs.css({ 'left': left, 'opacity': opacity });
                        },
                        duration: 800,
                        complete: function () {
                            current_fs.hide();
                            animating = false;
                        },
                        //this comes from the custom easing plugin
                        easing: 'easeInOutBack'
                    });
                }
                else {
                    animating = false;
                }
            });
        }


        function PreviousButton() {
            $(".previous").click(function () {
                if (animating) return false;
                animating = true;

                current_fs = $(this).parent();
                previous_fs = $(this).parent().prev();
                //de-activate current step on progressbar

                if ($("#progressbar li").eq($("fieldset").index(current_fs) - 2).hasClass('active')) {
                    $("#progressbar li").eq($("fieldset").index(current_fs) - 2).removeClass("active");
                } else {
                    $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
                }

                //show the previous fieldset
                previous_fs.show();
                //hide the current fieldset with style
                current_fs.animate({ opacity: 0 }, {
                    step: function (now, mx) {
                        //as the opacity of current_fs reduces to 0 - stored in "now"
                        //1. scale previous_fs from 80% to 100%
                        scale = 0.8 + (1 - now) * 0.2;
                        //2. take current_fs to the right(50%) - from 0%
                        left = ((1 - now) * 50) + "%";
                        //3. increase opacity of previous_fs to 1 as it moves in
                        opacity = 1 - now;
                        current_fs.css({ 'left': left });
                        previous_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
                    },
                    duration: 800,
                    complete: function () {
                        current_fs.hide();
                        animating = false;
                    },
                    //this comes from the custom easing plugin
                    easing: 'easeInOutBack'
                });
            });
        }

        function NextButtonQualification() {
            $(".next1").click(function () {
                if (animating) return false;

                animating = true;
                validate('vgroup');

                var isValid = true;
                current_fs.find(".form-group").each(function () {
                    if ($(this).hasClass("has-error") || $(this).hasClass("has-warning")) {
                        isValid = false;
                        return;
                    }
                });
                if (isValid) {

                    if ($("#progressbarQualification li").eq($("fieldset").index(0) - 1).hasClass('active') == true && $("#progressbarQualification li").eq($("fieldset").index(0)).hasClass('active') == false) {
                        current_fs = $(this).prev().prev();
                        next_fs = $(this).prev();

                        $("#progressbarQualification li").eq($("fieldset").index(0)).addClass("active");

                    } else if ($("#progressbarQualification li").eq($("fieldset").index(0) - 1).hasClass('active') == true && $("#progressbarQualification li").eq($("fieldset").index(0)).hasClass('active') == true) {
                        current_fs = $(this).parent();
                        next_fs = $(this).parent().next();

                        $("#progressbar li").eq($("fieldset").index(next_fs) - 2).addClass("active");
                    }

                    //show the next fieldset
                    next_fs.show();
                    //hide the current fieldset with style
                    current_fs.animate({ opacity: 0 }, {
                        step: function (now, mx) {
                            //as the opacity of current_fs reduces to 0 - stored in "now"
                            //1. scale current_fs down to 80%
                            scale = 1 - (1 - now) * 0.2;
                            //2. bring next_fs from the right(50%)
                            left = (now * 50) + "%";
                            //3. increase opacity of next_fs to 1 as it moves in
                            opacity = 1 - now;
                            current_fs.css({ 'transform': 'scale(' + scale + ')' });
                            next_fs.css({ 'left': left, 'opacity': opacity });
                        },
                        duration: 800,
                        complete: function () {
                            current_fs.hide();
                            animating = false;
                        },
                        //this comes from the custom easing plugin
                        easing: 'easeInOutBack'
                    });
                }
                else {
                    animating = false;
                }
            });
        }

        function PreviousButtonQualification() {
            $(".previous1").click(function () {
                if (animating) return false;
                animating = true;

                if ($("#progressbarQualification li").eq($("fieldset").index(0) - 1).hasClass('active') && $("#progressbarQualification li").eq($("fieldset").index(0)).hasClass('active')) {
                    current_fs = $(this).prev().prev();
                    previous_fs = $(this).prev().prev().prev();

                    $("#progressbarQualification li").eq($("fieldset").index(0)).removeClass("active");
                } else if ($("#progressbarQualification li").eq($("fieldset").index(0) - 1).hasClass('active') == true && $("#progressbarQualification li").eq($("fieldset").index(0)).hasClass('active') == false) {
                    current_fs = $(this).parent();
                    previous_fs = $(this).parent().prev();

                    $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
                }

                //show the previous fieldset
                previous_fs.show();
                //hide the current fieldset with style
                current_fs.animate({ opacity: 0 }, {
                    step: function (now, mx) {
                        //as the opacity of current_fs reduces to 0 - stored in "now"
                        //1. scale previous_fs from 80% to 100%
                        scale = 0.8 + (1 - now) * 0.2;
                        //2. take current_fs to the right(50%) - from 0%
                        left = ((1 - now) * 50) + "%";
                        //3. increase opacity of previous_fs to 1 as it moves in
                        opacity = 1 - now;
                        current_fs.css({ 'left': left });
                        previous_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
                    },
                    duration: 800,
                    complete: function () {
                        current_fs.hide();
                        animating = false;
                    },
                    //this comes from the custom easing plugin
                    easing: 'easeInOutBack'
                });
            });
        }


        function SameAddress(isChecked) {
            if (isChecked.checked) {

               
            }
            else {

            

            }
        }

         //function CheckEnrollID() {
         //    debugger;
         //    var EnrollID = $("input[id*='TxtEmployeeCode']").val()
             
         //    $.ajax({
         //        type: 'POST',
         //        url: "Employes_Add.aspx/CheckEnrollID",
         //        contentType: 'application/json; charset=utf-8',
         //        dataType: 'json',
         //        data: "{'EnrollId':'" + EnrollID + "'}",
         //        success: function (result) {
         //            if (result.d) {
         //                $('#lblEmployeeCode').hide();
         //            }
         //            else {
         //                $('#lblEmployeeCode').show();
         //            }
         //        },
         //        error: function (result) {
         //        }
         //    });
         //}

         $('#<%=Txtemail.ClientID%>').blur(function () {
             
             var email = $(this).val();
             if (email == null || email == 'undefined' || email == '') {
                 return false;
             }
             else {
                 $.ajax({
                     type: 'POST',
                     url: "Employes_Add.aspx/CheckEmail",
                     contentType: 'application/json; charset=utf-8',
                     dataType: 'json',
                     data: "{'Email':'" + email + "'}",
                     success: function (result) {
                         var data = JSON.parse(result.d);
                         if (data) {
                             $('#spnEmail').show();
                             $('.next').attr('disabled', 'true');
                         }
                         else {
                             $('#spnEmail').hide();
                             $('.next').removeAttr('disabled');
                         }

                     },
                     error: function (result) {

                     }
                 });
             }
         });


         $('#<%=TxtEmployeeCode.ClientID%>').blur(function () {
             var employeeCode = $(this).val();
             if (employeeCode == null || employeeCode == 'undefined' || employeeCode == '') {
                 return false;
             }
             else {
                 $.ajax({
                     type: 'POST',
                     url: "Employes_Add.aspx/Check_EmployeeCode",
                     contentType: 'application/json; charset=utf-8',
                     dataType: 'json',
                     data: "{'EmployeeCode':'" + employeeCode + "'}",
                     success: function (result) {
                         var data = JSON.parse(result.d);
                         if (data) {
                             $('#spnEmpCode').show();
                             $('.next').attr('disabled', 'true');
                         }
                         else {
                             $('#spnEmpCode').hide();
                             $('.next').removeAttr('disabled');
                         }

                     },
                     error: function (result) {

                     }
                 });
             }
         });


         $('#<%=Txtusername.ClientID%>').blur(function () {
             var username = $(this).val();
             if (username == null || username == 'undefined' || username == '') {
                 return false;
             }
             else {
                 $.ajax({
                     type: 'POST',
                     url: "Employes_Add.aspx/Check_Username",
                     contentType: 'application/json; charset=utf-8',
                     dataType: 'json',
                     data: "{'Username':'" + username + "'}",
                     success: function (result) {
                         var data = JSON.parse(result.d);
                         if (data) {
                             $('#spnUsername').show();
                             $('.next').attr('disabled', 'true');
                         }
                         else {
                             $('#spnUsername').hide();
                             $('.next').removeAttr('disabled');
                         }

                     },
                     error: function (result) {

                     }
                 });
             }
         });

    </script>
</asp:Content>

