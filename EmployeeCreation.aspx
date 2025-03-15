<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="EmployeeCreation.aspx.cs" Inherits="EmployeeCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <section class="content-header">
        

        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee</a></li>
            <li class="active">Employee - Add</li>
        </ol>
    </section>
       <style>

       
        .upload-btn {
            margin-top: 20px;
        }
        .file-actions {
            display: flex;
            gap: 10px;
        }
    </style>
    <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDetail" runat="server">
                <section id="msform" class="content cstm-csform">
                    <div class="row">
                        <div class="col-md-12">
                            <%--change--%>
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
                                                 <li><a href="#profile" data-toggle="tab">Upload Document</a></li>
                                                <%--<li><a href="#shitWOFF" data-toggle="tab">Shift / WOFF</a></li>--%>
                                                <%-- <li><a href="#payrollsettings" data-toggle="tab">Payroll Settings</a></li>--%>
                                            </ul>         

                                            <fieldset>
                                                <div id="divemployee" class="form-horizontal new_emp_form">
                                                    <h2 class="fs-title"><b>Add New Employee</b></h2>
                                                    <hr />
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">First Name</label>
                                                        <div class="col-sm-4">
                                                             <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" 
                                                                 placeholder="Enter First Name" Style="text-transform: capitalize;" 
                                                                 onkeypress="return onlyAlphabets(event)"
                                                                 validate='vgroup'
                                                                 oninput="removeInvalidChars(this)">
                                                             </asp:TextBox>
<%--                                                            <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" validate='vgroup' require="Please Enter your First Name" placeholder="Enter First Name" Style="text-transform: capitalize;"></asp:TextBox>--%>
                                                        </div>

                                                        <label class="col-sm-2 control-label">Last Name</label>
                                                        <div class="col-sm-4">
                                                        <%--    <asp:TextBox ID="TxtLastName" class="form-control" runat="server"  placeholder="Enter Last Name" Style="text-transform: capitalize;"></asp:TextBox>
                                                            <%--<asp:requireFieldValidator ID="reqTxtLastName" runat="server"
                                                                            ControlToValidate="TxtLastName" ErrorMessage="Enter your Last name">
                                                                        </asp:requireFieldValidator>--%>
 
                                                        <%--</div>--%>
                                                            <asp:TextBox ID="TxtLastName" class="form-control" runat="server" 
                                                                placeholder="Enter Last Name" Style="text-transform: capitalize;" 
                                                                onkeypress="return onlyAlphabets(event)" 
                                                                validate='vgroup'
                                                                oninput="removeInvalidChars(this)">
                                                            </asp:TextBox>
                                                             </div>                          
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Personal Email</label>
                                                        <div class="col-sm-4">
                                                            <span id="spnEmail" style="color: red; display: none">Email already exist</span>
                                                            <asp:TextBox ID="Txtemail" class="form-control" runat="server"  validate='vgroup' email="Invalid Email" require="Please Enter Your Personal Email" placeholder="Enter Email" Style="text-transform: lowercase;"></asp:TextBox>
                                                          
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
                                                            <asp:DropDownList ID="ddldepartment" class="form-control" runat="server" ClientIDMode="Static"  validate='vgroup' Custom="Select Department"  customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                     <%--      <asp:ListItem Value="0" Text="Select Type"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            <%-- <asp:DropDownList ID="ddldepartment" class="form-control" runat="server" validate='vgroup' Custom="Select Department" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                        </div>
                                                        <div id="divDesi" runat="server">
                                                            <label class="col-sm-2 control-label">Designation</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' require="Please Select Designation" OnSelectedIndexChanged="ddlLineManager_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <%-- <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                                                                            
                                                                        </asp:DropDownList>--%>
                                                                <%--  <asp:requireFieldValidator ID="reqddlDesignation"
                                                                            runat="server" ControlToValidate="ddlDesignation"
                                                                            ErrorMessage="Please Select"
                                                                            InitialValue="Please Select">
                                                                        </asp:requireFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                        <div id="div2" runat="server" visible="false">
                                                                    <label class="col-sm-2 control-label">Reports To</label>
                                                                        <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddltopdesignation" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup'  AutoPostBack="true" Enabled="false" visible="false">
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
                                                        <label class="col-sm-2 control-label">Record Managers</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="ddlLineManager" AutoPostBack="true" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" require="Please Select" customFn="var val = parseInt(this.value); return val > 0;" Style="text-transform: capitalize;" OnSelectedIndexChanged="ddlLineManager_SelectedIndexChanged1">
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

                                                           <label class="col-sm-2 control-label">Manager Name</label>
                                                              <div class="col-sm-4">
                                                                  <asp:DropDownList ID="ddlLineManagername" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" require="Please Select" customFn="var val = parseInt(this.value); return val > 0;" Style="text-transform: capitalize;">
                                                                      <%--<asp:ListItem Value="0" Text="Please Select"></asp:ListItem>--%>
                                                                  </asp:DropDownList>
                                                              </div>
                                                        <div></div>
                                                        <label class="col-sm-2 control-label pt-10">Employee Type</label>
                                                        <div class="col-sm-4 pt-10">
                                                            <asp:DropDownList ID="ddEmployeType" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" onchange="ddEmployeTypeChange(this)">
                                                                <asp:ListItem Value="0" Text="Select Type"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    
                                                  
                                                    <!-- Line Manager  -->
                                                    <div class="form-group">
                                                        <div runat="server" visible="false">
                                                            <label class="col-sm-2 control-label">Line Manager Designation</label>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList ID="ddlLineManagerDesignation" class="form-control" runat="server" require="Please" Visible="false">
                                                                    <%--<asp:ListItem Value="0" Text="Please Select"></asp:ListItem>--%>
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
                                                        <%--<label class="col-sm-2 control-label">Employee Code</label>
                                                        <div class="col-sm-4">
                                                            <span id="spnEmpCode" style="color: red; display: none">Employee Code already exist</span>
                                                            <asp:TextBox ID="TxtEmployeeCode" ClientIDMode="Static" onblur="CheckEmpCode(this)" class="form-control" runat="server" validate='vgroup' require="Please Enter your Employee Code" placeholder="Enter Employee Code"></asp:TextBox>
                                                            <%--<span id="lblEmployeeCode"><small style="color:red" >This Enrolled ID is already exist</small></span>--%>
                                                          <%--  <asp:HiddenField ID="hdEmployeeID" runat="server" />
                                                        </div>--%>
                                                        <label class="col-sm-2 control-label " style="display:none">User Name</label>
                                                        <div class="col-sm-4">
                                                            <span id="spnUsername" style="color: red; display: none">Username already exist</span>
                                                            <%--<asp:TextBox ID="Txtusername" class="form-control" validate='vgroup' runat="server" require="It should be Unique to every user." placeholder="Enter First & Last Name" ReadOnly="true" Style="text-transform: capitalize; display:none;"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group">
                                                        <label class="col-sm-2 control-label">Password</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="TxtPassword" class="form-control" validate='vgroup' runat="server" CssClass="ValidationError" require="Minimum 4 Character" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-2 control-label">Confirm Password</label>
                                                        <div class="col-sm-4">
                                                            <asp:TextBox ID="TxtConfirmPassword" class="form-control" validate='vgroup' runat="server" require="Please Enter Confirm Password" placeholder="Confirm Password" TextMode="Password" compare='password mismatch' compareTo="ContentPlaceHolder1_TxtPassword"></asp:TextBox>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtConfirmPassword" CssClass="ValidationError" ControlToCompare="TxtPassword" ForeColor="Red" ErrorMessage="Password MisMatch" ToolTip="Password must be the same"></asp:CompareValidator>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <input type="button" name="next" class="next action-button" value="Next" />
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
                                            <fieldset id="two">

                                                <div id="divgeneraldetails" class="new_emp_form">
                                                    <h2 class="fs-title"><b>General Details </b></h2>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-md-12 mt-20">
                                                            <asp:HiddenField ID="hdEmpPhotoID" runat="server" />
                                                            <asp:HiddenField ID="hdImageName" runat="server" />
                                                            <asp:HiddenField ID="hdEnrollId" runat="server" />
                                                            <img id="altimage" src="#" alt="Preview" width="100" height="100" cssclass="img-circle align" style="display: none;" />
                                                            <asp:Image ID="empImageView" ImageUrl="~/assets/img/user2-160x160.jpg" runat="server" CssClass="img-circle align" />
                                                            <div class="file-upload-wrapper" data-text="Select your file!">
                                                                <asp:FileUpload ID="uploadEmpImage" runat="server" onchange="readURL(this)" />
                                                            </div>
                                                            <asp:Button ID="btnUpload" runat="server" Height="0" Width="0" BackColor="#ECF0F5" BorderStyle="None" />
                                                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row labels">
                                                        <div class="col-md-6">
                                                          <%--  <label class="col-sm-6 control-label">Relation With Employee:</label>
                                                            <%--<asp:TextBox ID="txtFatherName" validate='vgroup' require="Please Select" class="form-control" runat="server" Placeholder="Enter Father/Husband Name"></asp:TextBox>                                                                       --%>
                                                          <%--  <asp:DropDownList ID="ddlempguardian" Width="200px" runat="server" validate='vgroup'  require="Please Enter Father/Husband Name">
                                                                <asp:ListItem>--Select One--</asp:ListItem>
                                                                <asp:ListItem>Father</asp:ListItem>
                                                                <asp:ListItem>Husband</asp:ListItem>
                                                            </asp:DropDownList>--%>

                                                            <%--
                                                               <asp:DropDownList ID="ddlempguardian" Width="200px"  ClientIDMode="Static" runat="server" asp-validation-for="Father/Husband" validate='vgroup'  require="Please Enter Father/Husband Name">
                                                                  <asp:ListItem Value="0">--Select One--</asp:ListItem>
                                                                  <asp:ListItem Value="Father">Father</asp:ListItem>
                                                                  <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                                              </asp:DropDownList>--%>

                                              <label class="col-sm-6 control-label">Relation with Employee:</label>
                                              <%--<asp:TextBox ID="txtFatherName" validate='vgroup' require="Please Select" class="form-control" runat="server" Placeholder="Enter Father/Husband Name"></asp:TextBox>                                                                       --%>
                                               <asp:DropDownList ID="ddlempguardianlist" ClientIDMode="Static" Width="200px" runat="server" >
                                                <asp:ListItem>--Select One--</asp:ListItem>
                                                    <asp:ListItem  value="Father">Father</asp:ListItem>
                                                        <asp:ListItem value="Husband">Husband</asp:ListItem>
                                                             </asp:DropDownList>

                 <%-- <label class="col-sm-6 control-label">Relation with Employee:</label>
                  <%--<asp:TextBox ID="txtFatherName" validate='vgroup' require="Please Select" class="form-control" runat="server" Placeholder="Enter Father/Husband Name"></asp:TextBox>                                                                       --%>
                  <%--  <asp:DropDownList ID="ddlempguardian" ClientIDMode="Static" Width="200px" runat="server" >
<asp:ListItem Value="0">--Select One--</asp:ListItem>
    <asp:ListItem  value="Father">Father</asp:ListItem>
        <asp:ListItem value="Husband">Husband</asp:ListItem>
             </asp:DropDownList>--%>
                                                              

             
                                                            <label class="col-sm-6 control-label">Date of Birth:</label>
                                                            <asp:TextBox ID="txtDOB" runat="server"  autocomplete="off" ClientIDMode="Static" CssClass="datepicker"></asp:TextBox>
<%--                                                            <asp:TextBox ID="txtDOB" runat="server" ClientIDMode="Static" CssClass="datepicker"></asp:TextBox>--%>
                                                            <label class="col-sm-6 control-label">Emergency Contact Number:</label>
                                                            <asp:TextBox ID="txtemgnumber" validate='vgroup'  onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>


                                                           <%-- <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtemgnumber" Mask="9999-9999999"
                                                                MessageValidatorTip="true"
                                                                OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError"
                                                                MaskType="Number"
                                                                InputDirection="RightToLeft"
                                                                ErrorTooltipEnabled="True"
                                                                ClearMaskOnLostFocus="false" />--%>
                                                               <label class="col-sm-6 control-label">Gender</label>
                                                                        <asp:DropDownList ID="ddlSex" 
                                                                            validate='vgroup' 
                                                                            Custom="Select Designation" 
                                                                            ClientIDMode="Static"
                                                                            runat="server"
                                                                            customFn="var val = this.value; return val  > 0;"  
                                                                            CssClass="form-control"
                                                                            onchange="setNodeTypeText(this)">

                                                                             <asp:ListItem Value="0" Text="Select One"></asp:ListItem>
                                                                            <asp:ListItem Value="1" >Female</asp:ListItem>
                                                                            <asp:ListItem Value="2">Male</asp:ListItem>
                                                                        </asp:DropDownList>
                                                          



                                                            <label class="col-sm-6 control-label">Marital Status:</label>


                                                            <asp:RadioButtonList BorderStyle="none" ID="rdoMarriedStatus" CssClass="radiobuton" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Class="CustomLabel" runat="server">Married</asp:ListItem>
                                                                <asp:ListItem>Unmarried</asp:ListItem>
                                                            </asp:RadioButtonList>





                                                        </div>

                                                        <div class="col-md-6">

<%--                                                            <label  id="dynamicLabel" class="col-sm-6 control-label">Name of Father/Husband :</label>
                                                            <asp:TextBox ID="txtguardianname" class="form-control"  ClientIDMode="Static" runat="server" Placeholder="Enter Father/Husband Name" Style="text-transform: capitalize;"></asp:TextBox>--%>


                                                              <label id="dynamicLabel" class="col-sm-6 control-label">Name of Father/Husband :</label>
                                                               <asp:TextBox ID="txtguardianname" ClientIDMode="Static" class="form-control" runat="server" Placeholder="Enter Name" style="text-transform:capitalize;"></asp:TextBox>
                                                                     
                                                            <label class="col-sm-6 control-label">Mobile Number:</label>
                                                            <asp:TextBox ID="TxtHomePhone"  onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                           <%-- <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="TxtHomePhone" 
                                                                MessageValidatorTip="true"
                                                                OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError"
                                                                MaskType="Number"
                                                                InputDirection="RightToLeft"
                                                                ErrorTooltipEnabled="True"
                                                                ClearMaskOnLostFocus="false" />--%>
                                                            <label class="col-sm-6 control-label">CNIC Number:</label>
                                                            <asp:TextBox ID="txtcnic"  onkeypress="return isNumberKey(event);" CssClass="form-control"  runat="server"></asp:TextBox>

                                                            <label class="col-sm-6 control-label" style="margin-top:14px;">Religion:</label>
                                                            <asp:TextBox ID="txtreligion"   CssClass="form-control" runat="server"></asp:TextBox>

                                                            <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtcnic" 
                                                                MessageValidatorTip="true"
                                                                OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError"
                                                                MaskType="Number"
                                                                InputDirection="RightToLeft"
                                                                ErrorTooltipEnabled="True"
                                                                ClearMaskOnLostFocus="false" />--%>




                                                        </div>


                                                    </div>
                                                    <div class="form-group">
                                                        <h3 class="mt-40 text-left mb-15">Current Address</h3>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label class="control-label pull-left">Address Line 1:</label>
                                                                <asp:TextBox ID="TxtCurrent_Address1" runat="server" CssClass="emp-address form-control"  Placeholder="Enter Address 1" Style="text-transform: capitalize;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <label class="control-label pull-left">City:</label>
                                                                <asp:TextBox ID="TxtCurrent_City" runat="server" CssClass="emp-address form-control form-group" Placeholder="Enter City" Style="text-transform: capitalize;"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label class="control-label pull-left">State/Region:</label>
                                                                <asp:TextBox ID="TxtCurrent_State" runat="server" CssClass="emp-address form-control" Placeholder="Enter State/Region" Style="text-transform: capitalize;"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label class="control-label pull-left">Zip/Postal Code:</label>
                                                                <asp:TextBox ID="TxtCurrent_Zip" runat="server"   CssClass="emp-address form-control" Placeholder="Enter Zip/Postal Code" Style="text-transform: capitalize;"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label class="control-label pull-left">Country:</label>
                                                                <asp:TextBox ID="TxtCurrent_Country" runat="server"  CssClass="emp-address form-control" Placeholder="Enter Country" Style="text-transform: capitalize;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" visible="false">
                                                            <div class="col-sm-10" runat="server">
                                                                <label class="control-label pull-left">Current Address :</label>
                                                                <asp:TextBox ID="txtCurrentAddress"  runat="server" TextMode="MultiLine" Rows="7"
                                                                    CssClass="emp-address form-control" Style="text-transform: capitalize;"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>



                                                        <div class="form-group">
        <h3 class="mt-40 text-left mb-15">Permanent Address</h3>
        <div class="row">
            <div class="col-sm-12">
                <label class="control-label pull-left">Address Line 1:</label>
                <asp:TextBox ID="txtPermanentAddress1" runat="server" CssClass="emp-address form-control"  Placeholder="Enter Address 1" Style="text-transform: capitalize;"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <label class="control-label pull-left">City:</label>
                <asp:TextBox ID="txtPermanentCity" runat="server" CssClass="emp-address form-control form-group" Placeholder="Enter City" Style="text-transform: capitalize;"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <label class="control-label pull-left">State/Region:</label>
                <asp:TextBox ID="txtPermanentState" runat="server" CssClass="emp-address form-control" Placeholder="Enter State/Region" Style="text-transform: capitalize;"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <label class="control-label pull-left">Zip/Postal Code:</label>
                <asp:TextBox ID="txtPermanentPostalCode" runat="server"  CssClass="emp-address form-control" Placeholder="Enter Zip/Postal Code" Style="text-transform: capitalize;"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <label class="control-label pull-left">Country:</label>
                <asp:TextBox ID="txtPermanentCounry" runat="server"  CssClass="emp-address form-control" Placeholder="Enter Country" Style="text-transform: capitalize;"></asp:TextBox>
            </div>
        </div>
        <div class="row" runat="server" visible="false">
            <div class="col-sm-10" runat="server">
                <label class="control-label pull-left">Permanent Address :</label>
                <asp:TextBox ID="txtPermanentAddress"  runat="server" TextMode="MultiLine" Rows="7"
                    CssClass="emp-address form-control" Style="text-transform: capitalize;"></asp:TextBox>
            </div>

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
                                                              <%--  <h2 class="fs-title"><b>Documents Upload</b></h2>--%>
                                                              <%--  <div class="form-group">

                                                                    <div class="choose-file mb-10">
                                                                        <asp:HiddenField ID="hdNICName" runat="server" />
                                                                        <asp:HiddenField ID="hdNICImage" runat="server" />
                                                                        <asp:Label ID="lblNIC" runat="server" class="control-label"></asp:Label>
                                                                        <asp:Label ID="txtNIC" runat="server" Text="Attach NIC"></asp:Label>
                                                                        <asp:FileUpload ID="NICUpload" runat="server" />
                                                                        <b>(jpg, png, gif, jpeg, bmp)</b>
                                                                    </div>

                                                                </div>--%>

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
                                            <fieldset class="divhide">
                                                <div class="col-md-12 animated bounceInDown" id="mybtn1">
                                                    <ul id="progressbarQualification" style="margin-left: 10%">
                                                        <li class="active">Academic</li>
                                                        <li><a href="#">Certificates</a></li>
                                                    </ul>
                                                </div>
                                                <fieldset class="divhide">
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
                                                                            <asp:TextBox ID="txtinsname" runat="server" require="Please Enter Institute" placeholder="Enter Institute" Style="text-transform: capitalize;"></asp:TextBox>
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
                                                                            <asp:TextBox ID="txtqualification" runat="server" require="Please Enter Qualification" placeholder="Enter Degree / Majors" Style="text-transform: capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <label class="col-sm-12">C GPA/Grade/Status</label>
                                                                            <asp:TextBox ID="txtmarks" runat="server"   require="Please Enter " placeholder="Enter CGPA/Grade/Status" Style="text-transform: capitalize;"></asp:TextBox>
                                                                            <%--<asp:TextBox ID="txtmarks" class="col-sm-2 is-number-with-decimal" runat="server" require="Please Enter Marks" placeholder="Enter CGPA/Grade/Status" style="text-transform:capitalize;"></asp:TextBox>--%>
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
                                                                <table class="table table-bordered table-hover divhide">
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
                                                                                <asp:TextBox ID="txtcrtfctname" runat="server" require="Please Enter Institute" placeholder="Enter Institute" Style="text-transform: capitalize;"></asp:TextBox>
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
                                                                                <asp:TextBox ID="txtcrtfctqualification" class="col-sm-2" runat="server" require="Please Enter Marks" placeholder="Enter Degree / Majors" Style="text-transform: capitalize;"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label class="col-sm-12">Grade</label>
                                                                                <asp:TextBox ID="txtcrtfctgrade" class="col-sm-2" runat="server" require="Please Enter Grade" placeholder="Enter Grade" Style="text-transform: capitalize;"></asp:TextBox>
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
                                                <div id="divpreviousjobinformation" class="box-body NewEmp_boxBody">
                                                    <div>
                                                        <div class="form-horizontal new_emp_form labels">
                                                            <h2 class="fs-title"><b>Previous Job Info</b></h2>
                                                            <hr />
                                                            <div class="row mb-5">
                                                                <div class="col-md-6">
                                                                    <tr>
                                                                        <td>
                                                                            <label class="col-sm-12">Company Name</label>
                                                                            <asp:TextBox ID="txtpcompanyname" ClientIDMode="Static" runat="server" placeholder="Enter Company" Style="text-transform: capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>   
                                                                      <%--      onchange="validateDates()"--%>
<%--                                                                            <label class="col-sm-12">Joining</label>
                                                                            <asp:TextBox ID="txtpjoiningdate" runat="server" ClientIDMode="Static" CssClass="form-control"  onchange="validateDates()"  ></asp:TextBox>--%>
<%--                                                                          <asp:TextBox ID="TextBox1" runat="server"  autocomplete="off" ClientIDMode="Static" CssClass="datepicker"></asp:TextBox>--%>

<%--                                                                            <asp:TextBox ID="txtpjoiningdate" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>--%>
                                                                     <%--   </td>--%>
                                                                            <tr>
    <td>
        <label class="col-sm-12">Joining</label>
        <asp:TextBox ID="txtpjoiningdate" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="validateDates()" ></asp:TextBox>
    </td>
</tr>
                                                                    </tr>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <tr>
                                                                        <td>
                                                                            <label class="col-sm-12">Designation</label>
                                                                            <asp:TextBox ID="txtpdesignation" runat="server" ClientIDMode="Static" placeholder="Enter Designation" Style="text-transform: capitalize;"></asp:TextBox>

                                                                            <label class="col-sm-12">End Date</label>
                                                                            <asp:TextBox ID="txtpenddate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onchange="validateDates()"  ></asp:TextBox>

<%--                                                                            <asp:TextBox ID="txtpenddate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>--%>
                                                                        </td>
                                                                    </tr>
                                                                </div>
                                                                <tr>
                                                                    <td>
                                                                        <input type="button" id="btnpjobsave" class="action-button" value="Add" />
                                                                    </td>
                                                                </tr>
                                                            </div>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>CompanyName</th>
                                                                        <th>Designation</th>
                                                                        <th>Joining Date</th>
                                                                        <th>End Date</th>
                                                                        <th>Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tblpjobinfo" class="tpbody">
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <asp:HiddenField ID="hdnpjob" ClientIDMode="Static" runat="server" />
                                                    </div>
                                                </div>


                                                <%--upload document start--%>

                                                


                                              <%--  upload document  end --%>


                                                <h2 class="fs-title" style="margin-left:-35px;"><b>Job Detail </b></h2>
                                                <div class="row cstm-align-labels" id="jobdetail">
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
                                                 
                                                    <div style="margin-top:25px" class="col-md-6"> Probation Period:
                                                           <tr>
                                                               <td>
                                                       
                                                                    <div id="dtprobationperiod">
                                                                         <div id="RJobStatus" class="radiobuton">
                                                                             <asp:RadioButtonList ID="rdoisconfirm" runat="server" onchange="return selectRadioValue();" RepeatDirection="Horizontal">
                                                                                 <asp:ListItem Selected="True" Text="Active">Probation</asp:ListItem>
                                                                                 <asp:ListItem Text="Deactive">Confirm</asp:ListItem>
                                                                             </asp:RadioButtonList>
                                                                         </div>
                                                                         <div id="dtprobationorconfirm">
                                                                        <asp:TextBox ID="txtprobationperiod" runat="server" ClientIDMode="Static" CssClass="form-control" style="display:none;" onchange="joiningProbation();"></asp:TextBox>
                                                                        <asp:TextBox ID="txtconfirmdate"     runat="server" onkeypress="return isNumberKey(event);" validate="vgroup" ClientIDMode="Static" CssClass="form-control datepicker" style="display:none;" ></asp:TextBox>
                                                                         </div>
                                                                       
                                                                    </div>
                                                               </td>
                                                         </tr>

                                                    </div>
                                                    <div style="margin-top:-54px" class="col-md-6">   Joining Date:
                                                          <tr>
                                                             <td>      
                                                                    <div id="dtJoiningDate">
                                                                        <asp:TextBox ID="txtJoiningDate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" require="Please Enter Joining Date" validate="vgroup" onchange="joiningProbation();"></asp:TextBox>
                                                                    </div>
                                                        <div class="Deactive" style="display: none">
                                                            <label class="col-sm-4 control-label">
                                                                Leaving Date:</label>
                                                            <div id="dtLeavingDate" class="col-sm-8">
                                                                <asp:TextBox ID="txtLeavingDate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                      <%--  <br />--%>
                                                              </td>
                                                         </tr>
                                                    </div>
                                                </div>
<%--                                                <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow"  OnClick="btnSaveEmployee_Click" OnClientClick="return Saveonclick();"></asp:Button>--%>
<%--                                                  <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>--%>

                                              <%--  <input type="button" name="previous" class="previous action-button" value="Previous" />--%>
                                              <%--  <div style="margin-top:10px">--%>
                                                   <input type="button" style="margin-top:40px" name="next" class="next action-button" value="Next" />
                                                   <input type="button" style="margin-top:40px" name="previous" class="previous action-button" value="Previous" />
                                                   <%-- </div>--%>
                                                <br>
                                                <br>
                                                <br></br>
                                                <br></br>
                                                </br>
                                                </br>
                                           </fieldset>


                                          <%--      payroll entery starts --%>
                                         
                                           <%-- <fieldset>
                                               <div id="divpayrollsetting" class="form-horizontal">
                                                    <h2 class="fs-title"><b>Payroll Setting </b></h2>
                                                    <br />
                                                    <div class="row cstm-align-labels">
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12">Basic Salary</label>
                                                            <asp:TextBox ID="txtBasicSalary" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="Basic Salary" MaxLength="9" max="9"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12">Lunch Allowance</label>
                                                            <asp:TextBox ID="txthouserentallowance" class="is-number-with-decimal" runat="server" placeholder="House rent" MaxLength="9" max="9"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12">Transport Allowance</label>
                                                            <asp:TextBox ID="txttransportallowance" class="is-number-with-decimal" runat="server" placeholder="Transport Allowance" MaxLength="9" max="9"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12">Medical Allowance</label>
                                                            <asp:TextBox ID="txtmedicalalowwance" class="is-number-with-decimal" runat="server" placeholder="Medical Allowance" MaxLength="9" max="9"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <h2 class="fs-title"><b>Bank Details </b></h2>
                                                    <div class="row cstm-align-labels">
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12 ">Bank Name</label>
                                                            <asp:TextBox ID="txtbankname"  runat="server" placeholder="Bank Name" Style="text-transform: capitalize"></asp:TextBox>
                                                              <label class="col-sm-12 ">Branch Name</label>
                                                            <asp:TextBox ID="txtfrombranch" runat="server" placeholder="Bank Name" style="text-transform:capitalize;"></asp:TextBox>
                                                         </div>

                                                          <div class="col-md-6">         
                                                              <label class="col-sm-12 ">Account No</label>
                                                              <asp:TextBox ID="txtaccount"  onkeypress="return isNumberKey(event);" require="Please Enter" runat="server" MaxLength="11" placeholder="Account NO"></asp:TextBox>
                                                              <label class="col-sm-12 ">Branch Code</label>
                                                              <asp:TextBox ID="txtbranchcode"  Type="number" runat="server" placeholder="Bank Code"></asp:TextBox>
                                                           </div>
                                                       </div>
                                                    </div>

                                              <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>--%>
                                              <%--  <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                <input type="button" name="next" class="next action-button" value="Next" />--%>
                                          
                                             <%--   </fieldset>--%>
                                            

                                         
                                           <fieldset>
                                               <div id="divpayrollsetting1" class="form-horizontal">
                                               <h3 class="mb-4">Upload Files with Document Type</h3>
                                                    <!-- File Input and Dropdown -->
                                                    <div class="form-row mb-3">
                                                        <div class="col-md-4">
                                                            <input type="file" id="fileInput" class="form-control" >
                                                        </div>
                                                        <div class="col-md-4">
                                                         <%--   <select id="documentType" class="form-control">
                                                                <option value="" disabled selected>Select Document Type</option>
                                                                <option value="CNIC Front">CNIC Front</option>
                                                                <option value="CNIC Back">CNIC Back</option>
                                                                <option value="Certificate">Certificate</option>
                                                                <option value="Transcript">Transcript</option>
                                                                <option value="Degree">Degree</option>
                                                                <option value="Other">Other</option>
                                                            </select>--%>

                                                            <input type="text" id="documentType" class="form-control" >
                                                        </div>
                                                        <div class="col-md-4">
                                                            <button class="btn btn-success" onclick="addFiles(event)">Add Files</button>
                                                        </div>
                                                    </div>

                                                    <!-- File Table -->
                                                    <table class="table table-bordered" id="fileTable">
                                                        <thead>
                                                            <tr>
                                                                <th class="text-left">Document</th>
                                                                <th>Size</th>
                                                                <th>Name</th>
                                                                <th>Action</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>

                                                    <!-- Upload Button -->
                                                   <%-- <button class="btn btn-primary upload-btn" onclick="uploadFiles()">Upload Files</button>--%>
                                                </div>

                                                 <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>
                                                <input type="button" name="previous" class="previous action-button" value="Previous" />
                                            <%--    <input type="button" name="next" class="next action-button" value="Next" />--%>
                                          
                                                </fieldset>



           <%--  updoad document   starts --%>
        
<%--           <fieldset>
              <div id="divpayrollsetting" class="form-horizontal">
              <h2 class="fs-title"><b>Upload Employee Documents</b></h2>
           <br/>
   
          <div class="row cstm-align-labels">
            <!-- CNIC Upload -->
            <div class="col-md-6">
                <label class="col-sm-12">Document Name (e.g., CNIC front)</label>
                <input type="text" id="cnicNamefront" class="form-control" placeholder="Enter Document Name" />
                <label class="col-sm-12">CNIC Copy</label>
                <input type="file" id="cnicUploadfront" class="form-control" accept=".pdf, .jpg, .png" />
            </div>
             <!-- CNIC Upload -->
             <div class="col-md-6">
                 <label class="col-sm-12">Document Name (e.g., CNIC back)</label>
                 <input type="text" id="cnicNameback" class="form-control" placeholder="Enter Document Name" />
                 <label class="col-sm-12">CNIC Copy</label>
                 <input type="file" id="cnicUploadback" class="form-control" accept=".pdf, .jpg, .png" />
             </div>

            <!-- Passport Upload -->
            <div class="col-md-6">
                <label class="col-sm-12">Document Name (e.g., Passport)</label>
                <input type="text" id="passportName" class="form-control" placeholder="Enter Document Name" />
                <label class="col-sm-12">Passport Copy</label>
                <input type="file" id="passportUpload" class="form-control" accept=".pdf, .jpg, .png" />
            </div>

            <!-- Employment Contract Upload -->
            <div class="col-md-6">
                <label class="col-sm-12">Document Name (e.g., Contract)</label>
                <input type="text" id="contractName" class="form-control" placeholder="Enter Document Name" />
                <label class="col-sm-12">Employment Contract</label>
                <input type="file" id="contractUpload" class="form-control" accept=".pdf, .jpg, .png" />
            </div>

            <!-- Other Documents Upload -->
            <div class="col-md-6">
                <label class="col-sm-12">Document Name (e.g., Other Documents)</label>
                <input type="text" id="otherDocsName" class="form-control" placeholder="Enter Document Name" />
                <label class="col-sm-12">Other Supporting Documents</label>
                <input type="file" id="otherDocsUpload" class="form-control" multiple accept=".pdf, .jpg, .png" />
            </div>
        </div>

        <!-- Upload Button -->
        <br />
        <div class="row">
            <div class="col-md-12 text-center">
                <button type="button" class="btn btn-primary" onclick="uploadDocuments()">Upload Documents</button>
            </div>
        </div>

          
        <!-- Upload Button -->
        <br />

                <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>
               <input type="button" name="previous" class="previous action-button" value="Previous" />
         
           </fieldset>--%>
             <%--  updoad document   end --%>
            <asp:Button ID="btnsavecontractemployee" ClientIDMode="Static" runat="server" Text="Save" CssClass="submit action-button btnshow" OnClientClick="return ContractSaveonclick();" Visible="true" OnClick="btnsavecontractemployee_Click" Style="display: none; width: 130px;"></asp:Button>
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
            <asp:PostBackTrigger ControlID="btnSaveEmployee" />
            <asp:PostBackTrigger ControlID="btnsavecontractemployee" />
        </Triggers>
    </asp:UpdatePanel>


    <script src="assets/js/jquery.easing.min.js"></script>
   <%-- <script src="js/bootstrap-datepicker.min.js"></script>--%>
      <script src="https://cdn.jsdelivr.net/npm/pikaday/pikaday.js"></script>
 <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
<script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.9.0/dist/js/bootstrap-datepicker.min.js"></script>


    <script type="text/javascript">


        function setNodeTypeText(dropdown) {
            const selectedText = dropdown.options[dropdown.selectedIndex].text;
            console.log(selectedText,'selectedText')
          <%--  document.getElementById('<%= hdnSelectedText.ClientID %>').value = selectedText;--%>
        }
  


        'use strict';

        const filesArray = []; 

        function addFiles(event) {
            event.preventDefault();

            const input = document.getElementById('fileInput');
            const documentType = document.getElementById('documentType').value;

            // Validate file input
            if (!input.files || input.files.length === 0) {
                alert("Please select files to upload.");
                return;
            }

            // Validate document type
            if (!documentType) {
                alert("Please select a document type.");
                return;
            }

            const allowedExtensions = ['png', 'jpg', 'jpeg', 'word', 'pdf', 'xls','xlsx','docx','dec','doc'];
            const tableBody = document.querySelector('#fileTable tbody');
            const files = Array.from(input.files);

            let processedCount = 0; 

            files.forEach(file => {
                const fileExtension = file.name.split('.').pop().toLowerCase();

                if (!allowedExtensions.includes(fileExtension)) {
                    alert(`${fileExtension} file extension is not supported.`);
                    return;
                }

                const reader = new FileReader();

                reader.onload = function (event) {
                    // Get base64 content
                    const base64Content = event.target.result.split(',')[1];
                    // Get Unique file name in the case of delete
                    const fileId = 'file-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9);

                    // Add file to filesArray
                    filesArray.push({
                        id: fileId,
                        fileName: file.name,
                        fileSize: (file.size / 1024).toFixed(2), // In KB
                        fileType: documentType,
                        fileBase64: base64Content
                    });

                    // Add row to table
                    const row = `
                                <tr id="${fileId}">
                                    <td>${file.name}</td>
                                    <td>${(file.size / 1024).toFixed(2)} KB</td>
                                    <td>${documentType}</td>
                                    <td class="file-actions">
                                        <button class="btn btn-danger btn-sm" onclick="removeFile('${fileId}')">Delete</button>
                                    </td>
                                </tr>
                            `;
                    tableBody.insertAdjacentHTML('beforeend', row);
                    processedCount++;

                    // If all files are processed, send API call
                    if (processedCount === files.length) {
                        sendFilesToServer();
                    }
                };

                reader.readAsDataURL(file); // Trigger FileReader
            });

            // Clear input after processing
            input.value = "";
            document.getElementById('documentType').value = "";
        }

        function sendFilesToServer() {
            if (filesArray.length === 0) {
               /* alert("No files to send!");*/
                console.error("No files to send!");
                return;
            }

            const jsonData = JSON.stringify(filesArray);

            $.ajax({
                type: "POST",
                url: "EmployeeCreation.aspx/SaveWithApi",
                data: JSON.stringify({ data: jsonData }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    try {
                        const response = JSON.parse(data.d);
                        if (response.success) {
                            /* alert(response.message);*/
                            console.error(response.message);
                           
                        } else {
                            console.error(response.message);
                        }
                    } catch (e) {
                        console.error("Invalid JSON response:", data);
                       /* alert("Unexpected server response.");*/
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error:", xhr.responseText);
                    /*alert("An error occurred: " + error);*/
                }
            });

            console.log("Files Array Sent:", filesArray);
        }



        function removeFile(fileId) {
           
            const index = filesArray.findIndex(file => file.id === fileId);
            if (index !== -1) {
             
                filesArray.splice(index, 1);
                console.log(`Removed file with ID: ${fileId}`, filesArray);

             
                $.ajax({
                    type: "POST",
                    url: 'EmployeeCreation.aspx/RemoveFileFromSession', 
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'fileId':'" + fileId + "'}",
                   /* data: { fileId: fileId },  */
                    success: function (response) {
                        console.log('Session updated successfully');
                       /* alert(response.message);*/
                    },
                    error: function (xhr, status, error) {
                        console.log('Error updating session:', error);
                    /*    alert(response.message);*/
                    }
                });
            }

          
            const row = document.getElementById(fileId);
            if (row) {
                row.remove();
            }
        }











        // Refresh Table
        function refreshTable() {
            const tableBody = document.querySelector('#fileTable tbody');
            tableBody.innerHTML = "";
            filesArray.forEach(({ file, type }) => {
                const row = `
                    <tr>
                        <td>${file.name}</td>
                        <td>${(file.size / 1024).toFixed(2)} KB</td>
                        <td>${type}</td>
                        <td class="file-actions">
                            <button class="btn btn-danger btn-sm" onclick="removeFile('${file.name}')">Delete</button>
                        </td>
                    </tr>
                `;
                tableBody.insertAdjacentHTML('beforeend', row);
            });
        }

        // Upload Files
        function uploadFiles() {
            if (filesArray.length === 0) {
                alert('Please select files first!');
                return;
            }
            const formData = new FormData();
            filesArray.forEach(({ file, type }) => {
                formData.append('files[]', file);
                formData.append('types[]', type);
            });

            // Dummy Upload Simulation
            alert('Uploading ' + filesArray.length + ' file(s) with document types...');
            console.log("Files uploaded: ", filesArray);
        }


        function ddEmployeTypeChange(ctrl) {
            var $this = $(ctrl);
            if ($this.find("option:selected").text() == "Contractss") {
                //$(".divhide").hide();
                $("#divgeneraldetails").after("<span class='divgeneraldetailsLocator'></span>")
                $("#divgeneraldetails").appendTo($this.parents("fieldset").find("#divemployee"))
                //Academic Qualification
                $("#divacademicinfo").after("<span class='divacademicinfoinformationLocator'></span>")
                $("#divacademicinfo").appendTo($this.parents("fieldset").find("#divgeneraldetails"))
                //Certificate
                $("#divcertificates").after("<span class='divcertificatesLocator'></span>")
                $("#divcertificates").appendTo($this.parents("fieldset").find("#divacademicinfo"))
                //Previous Job Info
                $("#divpreviousjobinformation").after("<span class='divpreviousjobinformationLocator'></span>")
                $("#divpreviousjobinformation").appendTo($this.parents("fieldset").find("#divcertificates"))

                $("#jobdetail").after("<span class='jobdetailLocator'></span>")
                $("#jobdetail").appendTo($this.parents("fieldset").find("#divpreviousjobinformation"))
                $("#divpayrollsetting").after("<span class='divpayrollsettingLocator'></span>")
                $("#divpayrollsetting").appendTo($this.parents("fieldset").find("#jobdetail"))


                $(".next").hide();
                debugger
                $("#btnsavecontractemployee").show();
                //$("#btncontract").css('visibility', 'visible');
                //$("#btnSaveEmployee").appendTo($("#divemployee").parents("fieldset"));
            }
            else if ($this.find("option:selected").text() == "Permanent") {

                //$(".divhide").show();
                $("#divgeneraldetails").insertBefore($(".divgeneraldetailsLocator"))
                $(".divgeneraldetailsLocator").remove();
                $("#jobdetail").insertBefore($(".jobdetailLocator"))
                $(".jobdetailLocator").remove();
                $("#divpayrollsetting").insertBefore($(".divpayrollsettingLocator"))
                $(".divpayrollsettingLocator").remove();
                $(".next").show();
                $("#btnsavecontractemployee").hide();





            }

        }

        $(function () {

           



            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            binddata();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            binddata();
        });
        function Employeedata() {
            document.getElementById('ddlempguardianlist').addEventListener('change', function () {

                console.log("change calls");

                const selectedValue = this.value;
                const label = document.getElementById('dynamicLabel');
                console.log("label")
                console.log(selectedValue)
                console.log(label)
                console.log(label.textContent)

                if (selectedValue === 'Father') {
                    label.textContent = 'Name of Father';
                } else if (selectedValue === 'Husband') {
                    label.textContent = 'Name of Husband';
                }
                console.log("success")
            });
        }
      
        function binddata() {
            Employeedata();


            function parseDate(dateString) {
                var parts = dateString.split('/');
                return new Date(parts[2], parts[1] - 1, parts[0]);
            }

            var currentDate = new Date();
            var joinningd = document.getElementById("txtJoiningDate").value;
            console.log(joinningd, "joinningd", "probationperiod");

            var prevoise = parseDate(joinningd);

            $('#<%= txtprobationperiod.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                beforeShowDay: function (date) {
                    var day = date.getDay();
                    if (date < prevoise) {
                        return false;
                    }

                    if (day === 0 || day === 6) {
                        return false;
                    }

                    return true;
                }
            });



            var currentDate = new Date();
            var joinningd = document.getElementById("txtJoiningDate").value;
            console.log(joinningd, "joinningd", "probationperiod");

            var prevoise = parseDate(joinningd);

            $('#<%= txtconfirmdate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                beforeShowDay: function (date) {
                    var day = date.getDay();
                    if (date < prevoise) {
                        return false;
                    }

                    if (day === 0 || day === 6) {
                        return false;
                    }

                    return true;
                }
            });




            $('#mybtn,#mybtn1').addClass('animated bounceInDown');

            // Bootstrap Datepicker
            $('#<%= txtDOB.ClientID %>').datepicker({
               format: 'dd-mm-yyyy', 
                autoclose: true,
                 clearbtn: false

            });

<%--            $('#<%= txtconfirmdate.ClientID %>').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                clearbtn: false
            });--%>
            $('.datepicker').datepicker({

                 format: 'dd-mm-yyyy', 
                autoclose: true,      
                clearBtn: true,       // Show Clear button
                startView: 1,         // Start view is month (viewing months)
                minViewMode: 0,       // Allow viewing days
                endDate: new Date(),  // Restrict to today's date
                startDate: new Date(new Date().setFullYear(new Date().getFullYear() - 1)), // Start from 1 year ago
                todayHighlight: true  // Highlight today's date
            });

            $('#txtWeddAnnev').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                clearBtn: false
            });







<%--            var currentDate = new Date();
            var joinningd = document.getElementById("txtpjoiningdate").value;
            console.log("joinningdjjj", joinningd);

            var prevoise = parseDate(joinningd);

            $('#<%= txtpenddate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                beforeShowDay: function (date) {
                    var day = date.getDay();

                    if (date < prevoise) {
                        return false; 
                    }

                    if (day === 0 || day === 6) {
                        return false;
                    }

                    return true;
                }
            });--%>






            
           
            $('#<%= txtJoiningDate.ClientID %>').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                clearBtn: false
            });

            $('#<%=txtpjoiningdate.ClientID %>').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                clearBtn: false
            });
     
            $('#txtpenddate').datepicker({
                format: 'dd-mm-yyyy',
                autoclose: true,
                clearBtn: false
            });
            $('#txtLeavingDate').datepicker({
               format: 'dd-mm-yyyy',
                autoclose: true,
                clearBtn: false
            });
            $('#txtConfrDate').datepicker({
                format: 'dd-mm-yyyy',
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
            console.log("arrayacademic", AcademicDataBind())
            PreviousJobBind();
            AddPJobRecord();
            checkemail();

        }
        ///sdd
        function readURL(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#altimage').show();
                    $('#altimage').attr('src', e.target.result);
                    $('#ContentPlaceHolder1_empImageView').hide();
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#uploadEmpImage").change(function () {

            readURL(this);
            $('#ContentPlaceHolder1_empImageView').hide();
        });
        function Saveonclick() {
            /* alert('ok');*/

         

            if (validate('vgroup') == true) {
               
                window.location.href = "Employee.aspx";
                msgbox(1, "Sucess", "Successfully Saved!");
                GetpJobrecord();
                getCertificateRecords();
                return true;

            }
            else {
                return false;
            }
        }
        function ContractSaveonclick() {
            alert('ok');
            debugger
            if (validate('vgroup') == true) {
                window.location.href = "Employee.aspx";
                msgbox(1, "Sucess", "Successfully Saved!");
                GetpJobrecord();
                getCertificateRecords();
                return false;
            }
            else {
                return false;
            }

        }


        //Previous job Detail
        function PreviousJobBind() {
            var arraypjob;
            if ($('[id*=hdnpjob]').val() != "") {
                arraypjob = JSON.parse($('[id*=hdnpjob]').val());

                if (arraypjob.length > 0) {
                    $(arraypjob).each(function (key, value) {

                        $("#tblpjobinfo").append('<tr><td>' + value.PreviousCompanyName + '</td><td>' + value.PreviousDesignation + '</td><td>' + value.JoiningDate + '</td><td>' + value.EndDate + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    });
                }
            }


        }
        function AddPJobRecord() {
            $("#btnpjobsave").click(function () {

                debugger
                var PreviousCompanyName = $('[id*=txtpcompanyname]').val();
                var PreviousDesignation = $('[id*=txtpdesignation]').val();
                var JoiningDate = $('[id*=txtpjoiningdate]').val();
                var EndDate = $('[id*=txtpenddate]').val();
                if (PreviousCompanyName != "" && PreviousCompanyName != "" && JoiningDate != "" && EndDate != "") {
                    $(".tpbody").append('<tr><td>' + PreviousCompanyName + '</td><td>' + PreviousDesignation + '</td><td>' + JoiningDate + '</td><td>' + EndDate + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    Pjobclearfields();
                }
            });

        }
        function Pjobclearfields() {
            $('[id*=txtpcompanyname]').val('');
            $('[id*=txtpdesignation]').val('');
            $('[id*=txtpjoiningdate]').val('');
            $('[id*=txtpenddate]').val('');
        }
        function GetpJobrecord() {
            var arraypjobinfo = [];




            $("#tblpjobinfo tr").each(function (key, value) {
                var arr = {};

                arr.PreviousCompanyName = $(this).closest('tr').find('td:eq(0)').html();
                arr.PreviousDesignation = $(this).closest('tr').find('td:eq(1)').html();
                arr.JoiningDate = $(this).closest('tr').find('td:eq(2)').html();
                arr.EndDate = $(this).closest('tr').find('td:eq(3)').html();

                //arr.CompanyName = $(this).closest('tr').find('td:eq(0)').html();
                //arr.Designation = $(this).closest('tr').find('td:eq(1)').html();
                //arr.Joiningdate = parseInt($(this).closest('tr').find('td:eq(2)').html());
                //arr.PreviousDate = parseInt($(this).closest('tr').find('td:eq(3)').html());

                arraypjobinfo.push(arr);
            });

            $('#hdnpjob').val(JSON.stringify(arraypjobinfo));
        }
        //End Previous job Detail

        function AcademicDataBind() {
            var arrayacademic;
            if ($('[id*=txtHidden_Academic]').val() != "") {
                arrayacademic = JSON.parse($('[id*=txtHidden_Academic]').val());
                $("#tblAcademicInfo").empty();
                if (arrayacademic.length > 0) {
                    $(arrayacademic).each(function (key, value) {

                        $("#tblAcademicInfo").append('<tr><td>' + value.InstituteName + '</td><td>' + value.Qualification + '</td><td>' + value.Year + '</td><td>' + value.Marks + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    });
                }
            }
            console.log("arrayacademic", arrayacademic)
            var ArrayCertificate;
            if ($('[id*=txt_HiidenCertificate]').val() != "") {
                ArrayCertificate = JSON.parse($('[id*=txt_HiidenCertificate]').val());
                $("#tblAcademicInfo").empty();
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
                arr.Marks = $(this).closest('tr').find('td:eq(3)').html();

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
                //probation or confirm date should not be null
                var probationperiod = $('#<%= txtprobationperiod.ClientID %>').val();
                var confirmdate = $('#<%= txtconfirmdate.ClientID %>').val();
                var rdovalue = $('#<%= rdoisconfirm.ClientID %> input:checked').val()
                console.log(rdovalue, "rdovalue");
                console.log(probationperiod, "probationperiod");
                console.log(confirmdate, "confirmdatewww");

                if (rdovalue == 'Probation')
                {
                    if (probationperiod == "" || probationperiod == null)
                    {
                        alert("Please select probation date!");
                        return ;
                    }
                    console.log(probationperiod, "probationperiod");
                }
                else
                {
                    if (confirmdate == "" || confirmdate == null) {
                        alert("Please select confirm date!");
                        return;
                    }
                    console.log(confirmdate, "confirmdate");
                }



                //Academic Popup
                if ($('#divacademicinfo').is(':visible')) {
                    var count = $('#tblAcademicInfo tr').length;
                    if (count <= 0) {
                        alert("Error", "Please enter the qualitifcation record !");
                        return false;
                    }
                }
                    //Certificate Popup
                else if ($('#divcertificates').is(':visible')) {
                    var count = $('#tblcertificateInfo tr').length;
                    if (count <= 0) {
                        alert("Error","Please enter the qualitifcation record !");
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

        $('#<%=ddldepartment.ClientID%>').change(function () {
            console.log("Dropdown value changed:", $(this).val());
            console.log("Dropdown changed. Reattaching blur event...");
            $('#<%=Txtemail.ClientID%>').off('blur').on('blur', function () {
                console.log("Blur event triggered after reattaching");
            });
        });
       
        var lastEmail = "";
        function checkemail() {


            $('#<%=Txtemail.ClientID%>').blur(function () {

                var email = $(this).val();
                if (email === lastEmail) {
                    console.log("Same email entered, skipping validation");
                    return; // Skip the validation if the email is same as last time
                }

                lastEmail = email; // Update last email value

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

                                $('#spnEmail').hide();
                                $('.next').removeAttr('disabled');
                                $('#ddlDesignation').prop('disabled', false)
                                $('#ddEmployeType').prop('disabled', false);
                                $('#ddldepartment').prop('disabled', false);
                                $('#ddlLineManager').prop('disabled', false);
                                $('#ddlLineManagername').prop('disabled', false);

                                console.log('success')

                            }
                            else {


                                $('#spnEmail').show();
                                $('.next').attr('disabled', 'true');
                                $('#ddlDesignation').prop('disabled', true);
                                $('#ddEmployeType').prop('disabled', true);
                                $('#ddldepartment').prop('disabled', true);
                                $('#ddlLineManager').prop('disabled', true);
                                $('#ddlLineManagername').prop('disabled', true);
                                console.log('error')

                            }

                        },
                        error: function (result) {

                        }
                    });
                }
            });

        }






        $(document).ready(function () {
            checkemail()
            selectRadioValue();
            
        });



          function selectRadioValue() {

               var rdovalue = $('#<%= rdoisconfirm.ClientID %> input:checked').val()
               console.log(rdovalue, "rdovalue");
               if (rdovalue == 'Probation')
               {
                    $('#txtprobationperiod').show().prop('required', true);
                   $('#txtconfirmdate').hide().prop('required', false);
                  
                }
                else
                {
                    $('#txtprobationperiod').hide().prop('required', false);
                   $('#txtconfirmdate').show().prop('required', true);
               
              } 
        /*      joiningProbation();*/
             
           } 

        // Stops insertion of invalid character
        function onlyAlphabets(event) {
            var char = String.fromCharCode(event.which);
            if (!/^[a-zA-Z\s]+$/.test(char)) {
                event.preventDefault(); 
                return false;
            }
            return true;
        }

        function removeInvalidChars(input) {
            input.value = input.value.replace(/[^a-zA-Z\s]/g, ''); 
        }




        function validateDates() {
            var joinDateStr = document.getElementById("txtpjoiningdate").value;
            var endDateStr = document.getElementById("txtpenddate").value;
            var currentDate = new Date();

            console.log("Joining date:", joinDateStr, "End date:", endDateStr);
            function parseDate(dateStr) {
                var parts = dateStr.split("/");
                return new Date(parts[2], parts[1] - 1, parts[0]);
            }

            if (joinDateStr && endDateStr) {
                var joinDate = parseDate(joinDateStr);
                var endDate = parseDate(endDateStr);


                if (joinDate < currentDate) {
                    alert("Joining date cannot be in the past!");
                    document.getElementById("txtpjoiningdate").value = "";
                    return;
                }

                if (endDate < joinDate) {
                    alert("Joining date cannot be greater than End date!");
                    document.getElementById("txtpenddate").value = "";
                    console.log("Joining date is greater than End date");
                    return;
                }
            }
        }

        function joiningProbation() {
            var probationdate = document.getElementById("txtprobationperiod").value;
            var joinningd = document.getElementById("txtJoiningDate").value;
            console.log("probation date:", probationdate, "joining date:", joinningd);
            var probationDateObj = new Date(probationdate);
            var joiningDateObj = new Date(joinningd);

            if (probationdate !=null && joinningd != null) {
                if (probationDateObj < joiningDateObj ) {
                    alert("probation date cannot be less than joining date!");
                    document.getElementById("txtprobationperiod").value = "";
                    console.log("Joining date is greater than probation date");
                }
            }           
        }
    </script>
</asp:Content>

