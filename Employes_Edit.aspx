<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Employes_Edit.aspx.cs" Inherits="Employes_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />

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
                                                            <li class="active" id="accountsetup">Account Setup</li>
                                                            <li><a href="#home" id="personalinfo">Personal Information</a></li>
                                                            <li><a href="#profile" id="academicqualification" data-toggle="tab">Academic Qualification</a></li>
                                                            <li><a href="#profile" id="jobinformation" data-toggle="tab">Job Information</a></li>
                                                             <li><a href="#profile" data-toggle="tab">Upload Document</a></li>
                                                            <%--<li><a href="#shitWOFF" data-toggle="tab">Shift / WOFF</a></li>--%>
                                                            <li id="payrolls" class="active"><a href="#payrollsettings" data-toggle="tab">Payroll Settings</a></li>
                                                        </ul>
                 

                                                        <fieldset id="one">
                                                            <div id="divemployee" class="form-horizontal new_emp_form">
                                                                <h2 class="fs-title"><b>Add New </b>Employee</h2>
                                                                <hr />
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">First Name</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" 
                                                                        placeholder="Enter First Name" Style="text-transform: capitalize;" 
                                                                        onkeypress="return onlyAlphabets(event)" 
                                                                        oninput="removeInvalidChars(this)">
                                                                    </asp:TextBox>
<%--                                                                        <asp:TextBox ID="TxtFirstName" class="form-control" runat="server" validate='vgroup' require="Please Enter your First Name" placeholder="Enter First Name" style="text-transform:capitalize;"></asp:TextBox>--%>
                                                                    </div>

                                                                    <label class="col-sm-2 control-label">Last Name</label>
                                                                    <div class="col-sm-4">
<%--                                                                        <asp:TextBox ID="TxtLastName" class="form-control" runat="server"  placeholder="Enter Last Name" style="text-transform:capitalize; "></asp:TextBox>--%>
                                                                         <asp:TextBox ID="TxtLastName" class="form-control" runat="server"  
                                                                             placeholder="Enter Last Name" Style="text-transform: capitalize;" 
                                                                             onkeypress="return onlyAlphabets(event)" 
                                                                             oninput="removeInvalidChars(this)">
                                                                         </asp:TextBox>
                                                                        <%--<asp:RequiredFieldValidator ID="reqTxtLastName" runat="server"
                                                                            ControlToValidate="TxtLastName" ErrorMessage="Enter your Last name">
                                                                        </asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Personal Email</label>
                                                                    <div class="col-sm-4">
                                                                        <span id="spnEmail" style="color:red; display:none">Email already exist</span>
                                                                        <asp:TextBox ID="Txtemail" class="form-control" runat="server" email="Invalid Email" validate='vgroup' require="Please Enter Personal Email" placeholder="Enter Email" style="text-transform:lowercase;"></asp:TextBox>
                                                                    </div>
                                                                   <%-- <div id="divComp" runat="server">
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
                                                                    </div>--%>
                                                                     <label class="col-sm-2 control-label">Employee Type</label>
                                                                     <div class="col-sm-4">
                                                                         <asp:DropDownList ID="ddEmployeType" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                                                                             <asp:ListItem Value="0" Text="Select Type"></asp:ListItem>
                                                                         </asp:DropDownList>
                                                                     </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Department</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddldepartment" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Department" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
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
                                                                    <div id="div2" runat="server" visible="false">
                                                                    <label class="col-sm-2 control-label">Report To</label>
                                                                        <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddltopdesignation" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                   <label class="col-sm-2 control-label">Record Managers</label>
                                                                    <div class="col-sm-4">
                                                                        <%--<asp:DropDownList ID="ddlLineManager" class="form-control" runat="server" OnSelectedIndexChanged="ddlLineManager_SelectedIndexChanged" AutoPostBack="true" >
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                        <asp:DropDownList ID="ddlLineManager" validate='vgroup' require="Please Select" class="form-control" runat="server" style="text-transform:capitalize;">
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
                                                                   <%-- <label class="col-sm-2 control-label">Employee Type</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddEmployeType" class="form-control" runat="server" ClientIDMode="Static" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0" Text="Select Type"></asp:ListItem>
                                                                        </asp:DropDownList>  onblur="EmployeeCode(this)"
                                                                    </div>--%>


   <div class="form-group">
  <label class="col-sm-2 control-label">Employee Code</label>
  <div class="col-sm-4">
      <span id="spnEmpCode" style="color:red; display:none">Employee Code already exist</span>
      <asp:TextBox ID="TxtEmployeeCode" ClientIDMode="Static" class="form-control"  
           runat="server" validate='vgroup' require="Please Enter your Employee Code" placeholder="Enter Employee Code" ></asp:TextBox>
      <%--<span id="lblEmployeeCode"><small style="color:red" >This Enrolled ID is already exist</small></span>--%>
      <asp:HiddenField ID="HiddenField1" runat="server" />
  </div>

                                                                </div>
                                                                <!-- Line Manager  -->
                                                                <div class="form-group">
                                                                    <div runat="server" visible="false">
                                                                        <label class="col-sm-2 control-label">Line Manager Designation</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:DropDownList ID="ddlLineManagerDesignation" class="form-control" validate='vgroup' runat="server" Visible="false">
                                                                                <%--<asp:ListItem Value="0" Text="Please Select"></asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                            <%--<asp:DropDownList ID="ddlLineManagerDesignation" class="form-control" runat="server" validate='vgroup' Custom="Select Designation" customFn="var val = parseInt(this.value); return val > 0;" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="ddlLineManagerDesignation_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                        </div>
                                                                    </div>
                                                                    
                                                                </div>
                                                                <%------Department-------%>
                                                                
                                                              

                                                                   <%-- <label class="col-sm-2 control-label">User Name</label>
                                                                    <div class="col-sm-4">
                                                                        <span id="spnUsername" style="color:red; display:none">Username already exist</span>
                                                                        <asp:TextBox ID="Txtusername" class="form-control" runat="server" require="It should be Unique to every user." placeholder="Enter First & Last Name" style="text-transform:capitalize;"></asp:TextBox>
                                                                        
                                                                    </div>--%>
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
                                                                          <asp:HiddenField ID="hdprofileimage" runat="server" />
                                                                         <img id="altimage"  src="#" alt="Preview" width="100" height="100"  CssClass="img-circle align" style="display:none;"/>
                                                                            <asp:Image ID="empImageVieW" ClientIDMode="Static"    runat="server" CssClass="img-circle align" />
                                                                        <div class="file-upload-wrapper" data-text="Select your file!">
                                                                            <asp:FileUpload ID="uploadEmpImage" runat="server" onchange="readURL(this)"/>
                                                                        </div>
                                                                            <asp:Button ID="btnUpload" runat="server"  Height="0" Width="0" BackColor="#ECF0F5" BorderStyle="None" />
                                                                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row labels">
                                                                    <div class="col-md-6">
                                                                        <label id ="dynamicLabel" class="col-sm-6 control-label">Relation with Employee:</label>
                                                                        <%--<asp:TextBox ID="txtFatherName" validate='vgroup' require="Please Select" class="form-control" runat="server" Placeholder="Enter Father/Husband Name"></asp:TextBox>                                                                       --%>
                                                                         <asp:DropDownList ID="ddlempguardian" ClientIDMode="Static" Width="200px" runat="server" >
                                                      <asp:ListItem value="--Select One--">--Select One--</asp:ListItem>
                                                          <asp:ListItem  value="Father">Father</asp:ListItem>
                                                              <asp:ListItem value="Husband">Husband</asp:ListItem>
                                                                   </asp:DropDownList>
                                                                       
                                                                        <label class="col-sm-6 control-label">Date of Birth:</label>

                                                                        <asp:TextBox ID="txtDOB" runat="server" validate='vgroup' require="Please Select" CssClass="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                                      <label class="col-sm-6 control-label">Emergency Contact Number:</label>
                                                                        <asp:TextBox ID="txtemgnumber" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtemgnumber" Mask="9999-9999999"
                                                                            MessageValidatorTip="true" 
                                                                            OnFocusCssClass="MaskedEditFocus" 
                                                                            OnInvalidCssClass="MaskedEditError"
                                                                            MaskType="Number" 
                                                                            InputDirection="RightToLeft"
                                                                            ErrorTooltipEnabled="True"
                                                                            ClearMaskOnLostFocus="false"/>--%>
                                                                           <label class="col-sm-6 control-label">Gender</label>
                                                                        <asp:DropDownList ID="ddlSex" validate='vgroup' require="Please Select" runat="server" CssClass="form-control">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem>Female</asp:ListItem>
                                                                            <asp:ListItem>Male</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <label class="col-sm-6 control-label">Marital Status:</label>


                                                                        <asp:RadioButtonList ID="rdoMarriedStatus" CssClass="radiobuton" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Class="CustomLabel" runat="server">Married</asp:ListItem>
                                                                            <asp:ListItem>Unmarried</asp:ListItem>
                                                                        </asp:RadioButtonList>

                                                                        



                                                                    </div>

                                                                     <div class="col-md-6">
                                                                          <label id ="dynamicLabel1" class="col-sm-6 control-label">Name of Father/Husband:</label>
                                                                       
                                                                        <%--<asp:Label  ID ="dynamicLabel1" class="col-sm-6 control-label" runat="server"></asp:Label>--%>
                                                                        <asp:TextBox ID="txtguardianname" ClientIDMode="Static" class="form-control" runat="server" Placeholder="Enter Father/Husband Name" style="text-transform:capitalize;"></asp:TextBox>
                                                                     
                                                                        <label class="col-sm-6 control-label">Mobile Number:</label>
                                                                        <asp:TextBox ID="TxtHomePhone"  onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                                                       <%-- <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="TxtHomePhone" 
                                                                            MessageValidatorTip="true" 
                                                                            OnFocusCssClass="MaskedEditFocus" 
                                                                            OnInvalidCssClass="MaskedEditError"
                                                                            MaskType="Number" 
                                                                            InputDirection="RightToLeft"
                                                                            ErrorTooltipEnabled="True"
                                                                            ClearMaskOnLostFocus="false"/>--%>
                                                                          <label class="col-sm-6 control-label">CNIC Number:</label>
                                                                         <asp:TextBox ID="txtcnic"  onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>

                                                                           <label class="col-sm-6 control-label">Religion:</label>
                                                                           <asp:TextBox ID="txtreligion"   CssClass="form-control" runat="server"></asp:TextBox>
                                                                      <%--    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtcnic" 
                                                                            MessageValidatorTip="true" 
                                                                            OnFocusCssClass="MaskedEditFocus" 
                                                                            OnInvalidCssClass="MaskedEditError"
                                                                            MaskType="Number" 
                                                                            InputDirection="RightToLeft"
                                                                            ErrorTooltipEnabled="True"
                                                                            ClearMaskOnLostFocus="false"/>--%>
                                                                       <%-- <label class="col-sm-6 control-label">Marital Status:</label>


                                                                        <asp:RadioButtonList ID="RadioButtonList1" CssClass="radiobuton" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Class="CustomLabel" runat="server">Married</asp:ListItem>
                                                                            <asp:ListItem>Unmarried</asp:ListItem>
                                                                        </asp:RadioButtonList>--%>

                                                                        



                                                                    </div>
                                                                </div>
                                                                <h3 class="mt-40 text-left mb-15">Current Address</h3>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <label class="control-label pull-left">Address Line 1:</label>
                                                                        <asp:TextBox ID="TxtCurrent_Address1" runat="server"  CssClass="emp-address form-control" Placeholder="Enter Address 1" style="text-transform:capitalize;"></asp:TextBox>
                                                                    </div>                                                                
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">City:</label>
                                                                        <asp:TextBox ID="TxtCurrent_City" runat="server"  CssClass="emp-address form-control" Placeholder="Enter City" style="text-transform:capitalize;"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">State/Region:</label>
                                                                        <asp:TextBox ID="TxtCurrent_State" runat="server"  CssClass="emp-address form-control" Placeholder="Enter State/Region" style="text-transform:capitalize;"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">Zip/Postal Code:</label>
                                                                        <asp:TextBox ID="TxtCurrent_Zip" runat="server" CssClass="emp-address form-control" Placeholder="Enter Zip/Postal Code" style="text-transform:capitalize;"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">Country:</label>
                                                                        <asp:TextBox ID="TxtCurrent_Country" runat="server"  CssClass="emp-address form-control" Placeholder="Enter Country" style="text-transform:capitalize;"></asp:TextBox>
                                                                    </div>
                                                                </div>                                                                                                                        
                                                                <div class="row" runat="server" visible="false">
                                                                    <div class="col-sm-10" runat="server">
                                                                        <label class="control-label pull-left">Current Address :</label>
                                                                        <asp:TextBox ID="txtCurrentAddress"    runat="server" TextMode="MultiLine" Rows="7" 
                                                                            CssClass="emp-address form-control" style="text-transform:capitalize;"></asp:TextBox>
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
                                                                        <asp:TextBox ID="txtPermanentPostalCode" runat="server"   CssClass="emp-address form-control" Placeholder="Enter Zip/Postal Code" Style="text-transform: capitalize;"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <label class="control-label pull-left">Country:</label>
                                                                        <asp:TextBox ID="txtPermanentCounry" runat="server"  CssClass="emp-address form-control" Placeholder="Enter Country" Style="text-transform: capitalize;"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row" runat="server" visible="false">
                                                                    <div class="col-sm-10" runat="server">
                                                                        <label class="control-label pull-left">Perminant Address :</label>
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
                                                                            <%--<h2 class="fs-title"><b>Documents Upload</b></h2>  
                                                                            <div class="form-group">                                                                                

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
                                                        <fieldset id="three">
                                                            <div class="col-md-12 animated bounceInDown divhide"  id="mybtn1" runat="server">
                                                                <ul id="progressbarQualification" style="margin-left: 10%">
                                                                    <li class="active">Academic</li>
                                                                    <li><a href="#">Certificates</a></li>                                                                    
                                                                </ul>
                                                            </div>
                                                            <fieldset>
                                                                <div id="divacademicinfo" class="box-body NewEmp_boxBody">
                                                                    <div class="form-horizontal new_emp_form labels">
                                                                        <h2 class="fs-title"><b>Academic Info </b></h2>
                                                                        <hr />
                                                                        <div class="row mb-5">
                                                                            <div class="col-md-6">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="hdnIdAcademic" runat="server" />

                                                                                        <label class="col-sm-12">Institute Name</label>
                                                                                        <asp:TextBox ID="txtinsname" runat="server" require="Please Enter Institute" placeholder="Enter Institute" style="text-transform:capitalize;"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <label class="col-sm-12">Year</label>
                                                                                        <asp:TextBox ID="txtyear" runat="server" class="is-number" require="Please Enter Year" placeholder="Enter Year" style="text-transform:capitalize;"></asp:TextBox>
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
                                                                                        <label class="col-sm-12">C GPA/Grade/Status</label>
                                                                                        <asp:TextBox ID="txtmarks" class="col-sm-2 is-number-with-decimal" runat="server" require="Please Enter Marks" placeholder="Enter CGPA/Grade/Status" style="text-transform:capitalize;"></asp:TextBox>
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
                                                                                            <label class="col-sm-12">Qualification</label>
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
                                                        <fieldset class="divhide" id="four">
                                                            <div id="divcategories" class="form-horizontal hidden divhide">
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
                                                                            Where='<%# "it.CompanyId ="+ Viftech.vt_Common.CompanyId %>'>
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
                                                            <div id="divbank" class="form-horizontal hidden divhide">
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
                                                                                        <td>
                                                                                            
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label class="col-sm-12">Joining</label>
                                                                                            <asp:TextBox ID="txtpjoiningdate" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="validateDates()" ></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label class="col-sm-12">Designation</label>
                                                                                            <asp:TextBox ID="txtpdesignation" runat="server" ClientIDMode="Static" placeholder="Enter Designation" Style="text-transform: capitalize;"></asp:TextBox>

                                                                                            <label class="col-sm-12">End Date</label>
                                                                                            <asp:TextBox ID="txtpenddate" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onchange="validateDates()"></asp:TextBox>
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
                                                           
                                                            <h2 class="fs-title"><b>Job Detail </b></h2>                                            
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
                                                                <div style="margin-top:64px" class="col-md-6">  Probation Period:
                                                                    <tr>
                                                                        <td>
                                                                    <div id="dtprobationperiod">
                                                                        
                                                                          <div id="RJobStatus" class="radiobuton">
                                                                        <asp:RadioButtonList ID="rdoisconfirm" runat="server" onchange="return selectRadioValue();" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Selected="True" Text="Active">Probation</asp:ListItem>
                                                                            <asp:ListItem Text="Deactive">Confirm</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        </div>

                                                                        <asp:TextBox ID="txtprobationperiod" runat="server"  onkeypress="return isNumberKey(event);"   ClientIDMode="Static" CssClass="form-control" style="display:none;"></asp:TextBox>
                                                                        <asp:TextBox ID="txtconfirmdate" runat="server" onkeypress="return isNumberKey(event);" ClientIDMode="Static" CssClass="form-control datepicker" style="display:none;"></asp:TextBox>

                                                                    </div>
                                                                       </td>
                                                                     </tr>
                                                                    
                                                                </div>
                                                                <div style="margin-top:-54px" class="col-md-6"> Joining Date:
                                                                      <tr>
                                                                          <td>
                                                                   
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
                                                                        </td>
                                                                     </tr>
                                                                    
                                                                </div>
                                                            </div>
                                                            <input type="button" name="next"  style="margin-top:40px" class="next1 action-button" value="Next" />
                                                            <input type="button" name="previous"  style="margin-top:40px" class="previous1 action-button" value="Previous" />

                                                        <%--    <input type="button" name="next" class="next action-button" value="Next" />--%>
                                                           <%-- <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow" OnClientClick="$('#confirmationModal').modal('show'); return false;" OnClick="btnSaveEmployee_Click"></asp:Button>           
                                                            <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                            <br>
                                                            </br>--%>
                                                        </fieldset>




                                                           
                                    <%--<fieldset>--%>    
                                           <fieldset>
                                            <div id="divpayrollsetting1" class="form-horizontal">
                                               <h3 class="mb-4">Upload Document</h3>
                                                    <!-- File Input and Dropdown -->
                                                    <div class="form-row mb-3">
                                                        <div class="col-md-4">
                                                            <input type="file" id="fileInput" class="form-control" >
                                                        </div>
                                                        <div class="col-md-4">
                                                            <input type="text" id="documentType" class="form-control" >
                                                        </div>
                                                        <div class="col-md-4">
                                                            <button class="btn btn-success" onclick="addFiles(event)">Add Files</button>
                                                        </div>
                                                    </div>
                                                    <!-- File Table -->
                                                    <table class="table table-bordered" id="fileTable" style="display:none;" >
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
                                            <%--   <asp:Button ID="Button1" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>
                                                <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                <input type="button" name="next" class="next action-button" value="Next" />--%> 



                                      


                                      <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                               <div class="col-md-12">
                                                   <div class="box box-info custom_input">
                                                     <%--  <div class="box-header with-border">
                                                           <i class="fa fa-table custom_header_icon admin_icon"></i>
                     
                                                       </div>--%>
                                                       <div class="box-body">
                                                           <div class="table-responsive">
                                                               <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                                                   <ContentTemplate>
                                                                       <div id="divGrid"></div>
                                                                   </ContentTemplate>
                                                               </asp:UpdatePanel>
                                                           </div>
                                                       </div>
                                                   </div>
                                               </div>
                                            </div> 
                                            </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>



                                                <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow"    OnClientClick="return Saveonclick();"   OnClick="btnSaveEmployee_Click"></asp:Button>           
                                                <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                 <br>
                                                 </br>
                                          
                                          </fieldset>











                                                        <fieldset>
                                                            <div id="divpayrollsetting" class="form-horizontal">
                                                    <h2 class="fs-title"><b>Payroll Setting </b></h2>

                                                    <br />
                                                    <div class="row cstm-align-labels">
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12">Basic Salary</label>
                                                            <span id="spnBasicSalary" style="color:red; display:none">Basic Salary Should be greater than zero</span>
                                                            <asp:TextBox ID="txtBasicSalary" ClientIDMode="Static" onkeypress="return isNumberKey(event);" onblur="calculategrosssalary()" runat="server" placeholder="Basic Salary" MaxLength="9" max="9"></asp:TextBox>


                                                        </div>
                                                         <div class="col-md-6">
                                                            <label class="col-sm-12">Food Allowance</label>
                                                            <asp:TextBox ID="txthouserentallowance" onkeypress="return isNumberKey(event);" ClientIDMode="Static" onblur="calculategrosssalary()" class="is-number-with-decimal" runat="server" placeholder="House rent" MaxLength="9" max="9"></asp:TextBox>
                                                             

                                                        </div>
                                                         <div class="col-md-6">
                                                            <label class="col-sm-12">Transport Allowance</label>
                                                            <asp:TextBox ID="txttransportallowance" onkeypress="return isNumberKey(event);" ClientIDMode="Static" onblur="calculategrosssalary()" class="is-number-with-decimal" runat="server" placeholder="Transport Allowance" MaxLength="9" max="9"></asp:TextBox>
                                                             <label class="col-sm-12">Over Time Amount</label>
                                                            <asp:TextBox ID="txtovertimeamount" onkeypress="return isNumberKey(event);" ClientIDMode="Static" class="is-number-with-decimal" runat="server" placeholder="Over Time Amount" MaxLength="9" max="9"></asp:TextBox>

                                                        </div>
                                                         <div class="col-md-6">
                                                            <label class="col-sm-12">Medical Allowance</label>
                                                            <asp:TextBox ID="txtmedicalalowwance" onkeypress="return isNumberKey(event);" ClientIDMode="Static" onblur="calculategrosssalary()" class="is-number-with-decimal" runat="server" placeholder="Medical Allowance" MaxLength="9" max="9"></asp:TextBox>
                                                             <div class="col-md-6">
                                                            <label class="col-sm-12">Tax</label>
                                                            <span id="spntax" style="color:red; display:none">Basic Salary Should be greater than zero</span>
                                                            <asp:TextBox ID="txttax" ClientIDMode="Static" onkeypress="return isNumberKey(event);"   runat="server" placeholder="Tax Amount" MaxLength="9" max="9"></asp:TextBox>


                                                        </div>
                                                                   <label class="col-sm-12">Gross Salary</label>
                                                            <label class="col-sm-12" id="lblgrosssalary" value=""></label>
                                                        </div>
                                                    </div>

                                                    <h2 class="fs-title"><b>Bank Details </b></h2>
                                                                 <label class="col-sm-12 ">Payment Mode</label>
                                                            <asp:DropDownList ID="ddlpaymentmethod" ClientIDMode="Static" Width="200px" runat="server" >
                                                      <asp:ListItem>--Select one--</asp:ListItem>
                                                          <asp:ListItem>Cash</asp:ListItem>
                                                              <asp:ListItem>Cheque</asp:ListItem>
                                                                <asp:ListItem>Bank</asp:ListItem>
                                                                   </asp:DropDownList> 
                                                    <div class="row cstm-align-labels" id="bankdetails" style="display:none;">
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12 ">Bank Name</label>
                                                            <%--<asp:TextBox ID="txtbankname" runat="server" placeholder="Bank Name" style="text-transform:capitalize"></asp:TextBox>--%>
                                                             <asp:DropDownList ID="ddlbankname" Width="200px" runat="server" >
                                                      <asp:ListItem>--Select Bank--</asp:ListItem>
                                                          <asp:ListItem>HBL </asp:ListItem>
                                                              <asp:ListItem>UBL </asp:ListItem>
                                                                   </asp:DropDownList>
                                                              <label class="col-sm-12 ">Branch Name</label>
                                                            <asp:TextBox ID="txtfrombranch"  runat="server" placeholder="Bank Name" style="text-transform:capitalize;"></asp:TextBox>
                                                            

                                                            

                                                        </div>

                                                        <div class="col-md-6">
                                                             <label class="col-sm-12 ">Branch Code</label>
                                                            <asp:TextBox ID="txtbranchcode" Type="number" runat="server" placeholder="Bank Code"></asp:TextBox>
                                                            <label class="col-sm-12 ">Account No</label>
                                                            <asp:Label ID="lblaccountno" runat="server" value=""></asp:Label>
                                                            <asp:TextBox ID="txtaccount"  onkeypress="return isNumberKey(event);"  require='Please select ' MaxLength="11"  runat="server" placeholder="Account NO"></asp:TextBox>
                                                            
                                   
                                                           
                                                            <%--<label class="col-sm-12 ">From Branch</label>
                                                            <asp:TextBox ID="txtbrachfrom" validate='vgroup' require="Please Enter" runat="server" placeholder="From Branch"></asp:TextBox>

                                                            <label class="col-sm-12 ">To  Branch</label>
                                                            <asp:TextBox ID="txtbranchto" validate='vgroup' require="Please Enter" runat="server" placeholder="To Branch"></asp:TextBox>--%>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <label class="col-sm-12 ">Account Title</label>
                                                            
                                                            <asp:TextBox ID="txtaccounttitle"   runat="server" placeholder="Account Titile" style="text-transform:capitalize;"></asp:TextBox>

                                                        </div>
                                                          
                                                    </div>
                                                </div>
                                                            <div id="divpayrolltitles" class="form-horizontal"  style="display:none;">
                                                            <label class="col-sm-12 ">Account Title</label>
                                                            <asp:TextBox ID="txtaccounttitile2"    require="Please Enter" runat="server" placeholder="Account Titile"></asp:TextBox>
                                                                </div>

                                                            
                                                            <%--<asp:Button ID="Button1" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button btnshow" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>           --%>
                                                            <asp:Button ID="btnsavepayroll" ClientIDMode="Static"  runat="server" Text="Save" CssClass="submit action-button btnshow" OnClientClick="return ContractSaveonclick();" Visible="true" OnClick="btnsavepayroll_Click" style="display:none;width: 130px;"></asp:Button>                              

                                                         

                                                            <input type="button" name="previous" Class="action-button" id="prvsbtn" class="previous action-button" value="Previous" />
                                                            </label>
                                                        </fieldset>
                                                        <asp:Button ID="btnsavecontractemployee" ClientIDMode="Static"  runat="server" Text="Save" CssClass="submit action-button btnshow" OnClientClick="return ContractSaveonclick();" Visible="true" OnClick="btnsavecontractemployee_Click" style="display:none;width: 130px;"></asp:Button>
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
                          <asp:PostBackTrigger ControlID="btnsavecontractemployee"/>
                    </Triggers>
                </asp:UpdatePanel>
            <!-- Confirmation Modal -->
    <div class="modal fade" id="confirmationModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>DO You Want to Update Employeee?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" id="btnConfirmYes">Yes</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>
    
    <script src="assets/js/jquery.easing.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    
<script type="text/javascript">
    <%-- function CheckProbationPeriod()
    { 
        $('[id $=rdoisconfirm]').change(function()
        {
            var rdovalue=$('#<%= rdoisconfirm.ClientID %> input:checked').val()
            if (rdovalue == 'Probation') {
                $('#txtprobationperiod').show();
            }
            else
            {
                $('#txtprobationperiod').hide();
            }            
        })            
    }--%>

 

    function getgardiannaame() {
       
        const ddlempguardian = document.getElementById('ddlempguardian');
        const label = document.getElementById('dynamicLabel1');

       
        const updateLabel = () => {
            const selectedValue = ddlempguardian.value;

            if (selectedValue === "--Select One--") {
                label.textContent = "Husband's /Father's Name";
            } else {
                label.textContent = selectedValue + "'s Name";
            }
        };
        updateLabel();
        ddlempguardian.addEventListener('change', () => {
            updateLabel();
            console.log("Dropdown changed to:", ddlempguardian.value);
        });
    }

   
   

    function selectRadioValue() {

         var rdovalue=$('#<%= rdoisconfirm.ClientID %> input:checked').val()
        if (rdovalue == 'Probation') {
            $('#txtconfirmdate').hide();
                $('#txtprobationperiod').show();
            }
            else
            {
                $('#txtprobationperiod').hide();
              
                $('#txtconfirmdate').show();
            }            
    } 
  
    $(document).ready(function()
    {
       

        $(window).focusout(function(){
              var rdovalue=$('#<%= rdoisconfirm.ClientID %> input:checked').val()
            if (rdovalue == 'Probation') {
                $('#txtprobationperiod').show();
            }
            else {
                $('#txtprobationperiod').hide();

                $('#txtconfirmdate').show();
            } 
            selectRadioValue();
        })
       
      //  CheckProbationPeriod();
        var basicsalary = $("#txtBasicSalary").val();
        var houseamount = $("#txthouserentallowance").val();
        var transportamount = $("#txttransportallowance").val();
        var medicalamount = $("#txtmedicalalowwance").val();
        var totalgross = parseInt((basicsalary == "" ? 0 : basicsalary))
            + parseInt((houseamount == "" ? 0 : houseamount))
            + parseInt((transportamount == "" ? 0 : transportamount))
            + parseInt((medicalamount == "" ? 0 : medicalamount));
        $("#lblgrosssalary").html(totalgross);
        if ($("#ddlpaymentmethod").find("option:selected").text() == "Bank") {
            $("#bankdetail").show();
            $("#bankdetails").show();
            $("#divpayrolltitles").hide();
        }
        else if ($("#ddlpaymentmethod").find("option:selected").text() == "Cheque") {
              
            $("#bankdetail").hide();
            $("#bankdetails").hide();
            $("#divpayrolltitles").show();
     
                


        }
    })
    function calculategrosssalary()
    {
      //  $("#lblgrosssalary").html('');
        var basicsalary = $("#txtBasicSalary").val();
        var houseamount = $("#txthouserentallowance").val();
        var transportamount = $("#txttransportallowance").val();
        var medicalamount = $("#txtmedicalalowwance").val();
        var totalgross = parseInt((basicsalary == "" ? 0 : basicsalary))
            + parseInt((houseamount == "" ? 0 : houseamount))
            + parseInt((transportamount == "" ? 0 : transportamount))
            + parseInt((medicalamount == "" ? 0 : medicalamount));
        $("#lblgrosssalary").html(totalgross);
    }

      $("#<%=ddlpaymentmethod.ClientID%>").change(function () {
        
            if ($("#ddlpaymentmethod").find("option:selected").text() == "Bank") {
                $("#bankdetail").show();
                $("#bankdetails").show();
                $("#divpayrolltitles").hide();
            }
            else if ($("#ddlpaymentmethod").find("option:selected").text() == "Cheque") {
              
                $("#bankdetail").hide();
                $("#bankdetails").hide();
                $("#divpayrolltitles").show();
   
                


            }
            else if ($("#ddlpaymentmethod").find("option:selected").text() == "Cash") {
                $("#bankdetail").hide();
                $("#bankdetails").hide();
                $("#divpayrolltitles").hide();
            }
          
        })
    $(document).ready(function () {
      
         var RoleId = <%= int.Parse((Session["RoleId"] == null ?0 : Session["RoleId"]).ToString()) %>;
        if (RoleId == 2) {
            $("#divpayrollsetting").hide();
            $("#btnSaveEmployee").hide();
            $("#payrolls").hide();
            $("#btnSaveEmployee").show();
        }
      else  if (RoleId == 1) {
          $("#accountsetup").remove();
          $("#personalinfo").remove();
          $("#academicqualification").remove();
          $("#jobinformation").remove();
          $("#one").remove();
          $("#two").remove();
          $("#three").remove();
          $("#four").remove();
          $("#btnsavepayroll").show();
          $("#prvsbtn").hide();

        }
       
    });
  <%--  $(document).ready(function () {
        var basicsalarys = $("#<%= txtBasicSalary.ClientID%>").val();
        if (basicsalarys == 0) {
           $("#<%= txtBasicSalary.ClientID%>").val('');
        }
       
    });
    $("#<%= txtBasicSalary.ClientID%>").blur(function () {

        var basicsalary = $("#<%= txtBasicSalary.ClientID%>").val();
        if (basicsalary ==0) {
            $("#spnBasicSalary").show();
        }
        else
        {
            $("#spnBasicSalary").hide();
        }
    })--%>

    //function ShowHideDiv() {
    //    debugger
    //    var ddEmployeType = document.getElementById("ddEmployeType");
    //    var mybtn1 = document.getElementById("divacademicinfo");
    //    mybtn1.style.display = ddEmployeType.value == "Contract" ? "block" : "none";
    //}
    //function showDiv(ddl) {
    //    var dv1 = document.getElementById("select_class");
    //    var dv2 = document.getElementById("new_class");
    //    if (ddl.value == "0") {
    //        dv1.style.display = "block";
    //        dv2.style.display = "none";
    //    }
    //    if (ddl.value == "1") {
    //        dv1.style.display = "none";
    //        dv2.style.display = "block";
    //    }
    //}
    //$(function () {
    //    $("#ddEmployeType").change(function () {
    //        if ($(this).val() == "Contract") {
    //            $("#mybtn1").hide();
    //        } else {
    //            $("#bounceInDown").show();
    //        }
    //    });
    //});
</script>
     <script type="text/javascript">
        'use strict';
          

        $(function () {
           
            $(window).load(function () {
                getgardiannaame();
                //debugger
                if ($("#<%= ddEmployeType.ClientID%>").find("option:selected").text() == "Contractss") {
                    $(".divhide").hide();
                    var $this = $("#<%= ddEmployeType.ClientID%>");
                    //$("#divgeneraldetails").appendTo($(this).parents("fieldset").find("#divemployee"))
                    //$("#jobdetail").appendTo($(this).parents("fieldset").find("#divgeneraldetails"))
                        
                    //$(".next").hide();
                    //debugger
                    //$("#btnSaveEmployee").appendTo($("#divemployee").parents("fieldset"));
                    $("#divgeneraldetails").appendTo($this.parents("fieldset").find("#divemployee"))
                    $("#divacademicinfo").appendTo($this.parents("fieldset").find("#divgeneraldetails"))
                    $("#divcertificates").appendTo($this.parents("fieldset").find("#divacademicinfo"))
                    $("#divpreviousjobinformation").appendTo($this.parents("fieldset").find("#divcertificates"))
                    $("#jobdetail").appendTo($this.parents("fieldset").find("#divpreviousjobinformation"))
                    $("#divpayrollsetting").appendTo($this.parents("fieldset").find("#jobdetail"))
                    $(".next").hide();
                 ///   debugger
                    $("#btnsavecontractemployee").show();
                    //$("#btnSaveEmployee").appendTo($("#btnSaveEmployee").parents("#mybtn").find("fieldset").eq(1));
                      //  $(".btnshow").show();
                                       

                }

                //.css("display","none !important")
                //else {
                //    $(".divhide").css("display", "block !important");
                //}
               // alert();
            })
        })
    
        
        $(function () {
           
            $("#<%= ddEmployeType.ClientID%>").change(function () {
               // debugger
                if ($("#<%= ddEmployeType.ClientID%>").find("option:selected").text() == "Contract") {
                    $(".divhide").hide();
                    var $this = $("#<%= ddEmployeType.ClientID%>");
                    //$("#divgeneraldetails").appendTo($(this).parents("fieldset").find("#divemployee"))
                    //$("#jobdetail").appendTo($(this).parents("fieldset").find("#divgeneraldetails"))
                        
                    //$(".next").hide();
                    //debugger
                    //$("#btnSaveEmployee").appendTo($("#divemployee").parents("fieldset"));
                    $("#divgeneraldetails").appendTo($this.parents("fieldset").find("#divemployee"))
                    $("#jobdetail").appendTo($this.parents("fieldset").find("#divgeneraldetails"))
                    $("#divpayrollsetting").appendTo($this.parents("fieldset").find("#jobdetail"))
                    $(".next").hide();
                  //  debugger
                    $("#btnsavecontractemployee").show();
                    //$("#btnSaveEmployee").appendTo($("#btnSaveEmployee").parents("#mybtn").find("fieldset").eq(1));
                      //  $(".btnshow").show();
                                       

                }

                //.css("display","none !important")
                //else {
                //    $(".divhide").css("display", "block !important");
                //}
               // alert();
            })
        })
    


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

            $('#mybtn,#mybtn1').addClass('animated bounceInDown');

            $('#<%= txtDOB.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                clearbtn: false
            });
            $('#txtWeddAnnev').datepicker({
                dateFormat: 'dd/mm/yy',
                autoclose: true,
                clearBtn: false
            });
            $('#<%= txtJoiningDate.ClientID %>').datepicker({
               format: 'dd/mm/yyyy',
               autoclose: true,
               clearBtn: false
            });

            $('#<%=txtpjoiningdate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                clearBtn: false
            });


            $('#<%=txtpenddate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                clearBtn: false
            });

            //$('#txtpenddate').datepicker({
            //    dateFormat: 'dd-mm-yy',
            //    autoclose: true,
            //    clearBtn: false
            //});
            $('#txtLeavingDate').datepicker({
                dateFormat: 'dd/mm/yy',
                autoclose: true,
                clearBtn: false
            });
            $('#txtConfrDate').datepicker({
                dateFormat: 'dd/mm/yy',
                autoclose: true,
                clearBtn: false
            });

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
          //  debugger
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
                    $('#empImageVieW').hide();
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#uploadEmpImage").change(function () {

            readURL(this);
            $('#empImageVieW').hide();
        });
        function Saveonclick() {
            /*alert('DO You Want to Update Employeee?');*/
            /*OnClientClick = "$('#confirmationModal').modal('show'); return false;"*/

           /* $('#confirmationModal').modal('show'); return false;*/
            debugger
            if (validate('vgroup') == true) {
                window.location.href = "Employee.aspx";
                msgbox(1, "Sucess", "Successfully Saved!");
                GetpJobrecord();
                getCertificateRecords();
                return true;
            }
            else
            {
                return false;
            }
        }
      
        function ContractSaveonclick() {
          //  alert('DO You Want to Add Employeee');
           // debugger
            if (validate('vgroup') == true) {
                window.location.href = "Employee.aspx";
                msgbox(1, "Sucess", "Successfully Saved!");
                msgbox(1, "Sucess", "Successfully Saved!");GetpJobrecord
                GetpJobrecord();
                getCertificateRecords();
                return true;
            }
            else {
                return false;
            }

        }
           
      
        function PreviousJobBind()
        {
            var arraypjob;
            if ($('[id*=hdnpjob]').val() != "") {
                arraypjob = JSON.parse($('[id*=hdnpjob]').val());

                if (arraypjob.length > 0) {
                    $(arraypjob).each(function (key, value) {

                        $("#tblpjobinfo").append('<tr><td>' + value.PreviousCompanyName + '</td><td>' + value.PreviousDesignation+ '</td><td>' + value.JoiningDate + '</td><td>' + value.EndDate + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    });
                }
            }


        }
        function AddPJobRecord()
        {
            $("#btnpjobsave").click(function () {
               
                debugger
                var PreviousCompanyName = $('[id*=txtpcompanyname]').val();
                var PreviousDesignation= $('[id*=txtpdesignation]').val();
                var JoiningDate = $('[id*=txtpjoiningdate]').val();
                var EndDate= $('[id*=txtpenddate]').val();
                if (PreviousCompanyName != "" && PreviousCompanyName  != "" && JoiningDate != "" && EndDate != "") {
                    $(".tpbody").append('<tr><td>' + PreviousCompanyName + '</td><td>' + PreviousDesignation + '</td><td>' + JoiningDate + '</td><td>' + EndDate + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    Pjobclearfields();
                }
            });

        }
        function AcademicDataBind() {
            
            var arrayacademic = "";
           
            if ($('[id*=txtHidden_Academic]').val() != "") {
                arrayacademic = JSON.parse($('[id*=txtHidden_Academic]').val());
                $("#tblAcademicInfo").empty();
                if (arrayacademic.length > 0) {
                    console.log("tblAcademicInfo", arrayacademic.length)
                    $(arrayacademic).each(function (key, value) {

                        $("#tblAcademicInfo").append('<tr><td>' + value.InstituteName + '</td><td>' + value.Qualification + '</td><td>' + value.Year + '</td><td>' + value.Marks + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    });
                }
            }
            console.log("arrayacademic1", arrayacademic)
            var ArrayCertificate;
            if ($('[id*=txt_HiidenCertificate]').val() != "") {
                ArrayCertificate = JSON.parse($('[id*=txt_HiidenCertificate]').val());
                $("#tblcertificateInfo").empty();
                if (ArrayCertificate.length > 0) {
                    $(ArrayCertificate).each(function (key, value) {

                        $("#tblcertificateInfo").append('<tr><td>' + value.InstituteName + '</td><td>' + value.Qualification + '</td><td>' + value.Year + '</td><td>' + value.Grade + '</td><td><button type="button" onclick="RemoveRecord($(this))">Remove</button></td> </tr>');
                    });
                }
            }
        }
      
        function AddRecord() {

            $("#btninssave").click(function () {
                debugger
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
                console.log("enter success")
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

        
        function Pjobclearfields() {
            $('[id*=txtpcompanyname]').val('');
            $('[id*=txtpdesignation]').val('');
            $('[id*=txtpjoiningdate]').val('');
            $('[id*=txtpenddate]').val('');
        }
        function GetpJobrecord()
        {
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
                                $('[id $=empImageView]').attr('src' + $(data).text());
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

            var academicValue = $('#ContentPlaceHolder1_txtHidden_Academic').val(); // Get value

            if (academicValue && academicValue.trim() !== "") {
                // Value exists, perform your desired actions here
                console.log("Value exists in the hidden field: ", academicValue);
            } else {
                // No value in the hidden field
                console.log("No value found in the hidden field.");
            }
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
         //    });]
         //}

         function getQueryParam(param) {
             const urlParams = new URLSearchParams(window.location.search);
             return urlParams.get(param);
         }
         function checkemail() {


             $('#<%=Txtemail.ClientID%>').blur(function () {
                 var email = $(this).val();
                 var empID = getQueryParam('ID');

                 if (email == null || email == 'undefined' || email == '') {
                     console.log(email, 'success');
                     return false;
                 }

                 else {
                     $.ajax({
                         type: 'POST',
                         url: "Employes_Edit.aspx/CheckEmailEdit",
                         contentType: 'application/json; charset=utf-8',
                         dataType: 'json',
                         /*   data: "{'Email':'" + email + "'}",*/
                         data: "{'Email':'" + email + "','EmployeeID':'" + empID + "'}",
                         success: function (result) {
                             var data = JSON.parse(result.d);
                             if (data) {
                                 console.log('success');
                                 $('#spnEmail').show();
                                 $('.next').attr('disabled', 'true');
                                 $('#ddlDesignation').prop('disabled', true)
                                 $('#ddEmployeType').prop('disabled', true)
                                 $('#ddldepartment').prop('disabled', true)
                                 console.log('success');

                             }
                             else {
                                 $('#spnEmail').hide();
                                 $('.next').removeAttr('disabled');
                                 $('#ddlDesignation').prop('disabled', false)
                                 $('#ddEmployeType').prop('disabled', false)
                                 $('#ddldepartment').prop('disabled', false)
                                 console.log('error');
                             }

                         },
                         error: function (result) {

                         }
                     });
                 }
             });
         }
      <%--   document.getElementById('btnconfirmyes').addEventListener('click', function () {
             __DoPostBack('<%= btnSaveEmployee.UniqueID %>', '');
            });--%>

         document.addEventListener('DOMContentLoaded', function () {
             document.getElementById('btnconfirmyes').addEventListener('click', function () {
                 __DoPostBack('<%= btnSaveEmployee.UniqueID %>', '');
    });
        });
         $(window).load(function ()
         {
    
         })
           

         $(document).ready(function () {
             checkemail();

         });

         const filesArray = [];

         function addFiles(event) {
             event.preventDefault();

             const input = document.getElementById('fileInput');
             const documentType = document.getElementById('documentType').value;

             if (!input.files || input.files.length === 0) {
                 alert("Please select files to upload.");
                 return;
             }
             if (!documentType) {
                 alert("Please select a document name.");
                 return;
             }

             const allowedExtensions = ['png', 'jpg', 'jpeg', 'word', 'pdf', 'xls', 'xlsx', 'docx', 'dec', 'doc'];
             const tableBody = document.querySelector('#fileTable tbody');
             const files = Array.from(input.files);

             // Clear filesArray to ensure only new files are added
             filesArray.length = 0;

             let processedCount = 0;

             files.forEach(file => {
                 const fileExtension = file.name.split('.').pop().toLowerCase();

                 


                 if (!allowedExtensions.includes(fileExtension)) {
                     alert(`${fileExtension} file extension is not supported.`);
                     return;
                 }

                 const reader = new FileReader();

                 reader.onload = function (event) {
                     const base64Content = event.target.result.split(',')[1];
                     const fileId = 'file-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9);

                     filesArray.push({
                         id: fileId,
                         fileName: file.name,
                         fileSize: (file.size / 1024).toFixed(2),
                         fileType: documentType,
                         fileBase64: base64Content
                     });

                     processedCount++;

                     if (processedCount === files.length) {
                         bindGrid();
                         sendFilesToServer();
                     }
                 };

                 reader.readAsDataURL(file);
             });

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
                 url: "Employes_Edit.aspx/SaveWithApi",
                 data: JSON.stringify({ data: jsonData }),
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     try {
                         const response = JSON.parse(data.d);
                         if (response.success) {
                              /*alert(response.message);*/
                             console.error(response.message, "response.message");
                             bindGrid();

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

         function refreshGrid(newData) {
             const grid = $("#divGrid").dxDataGrid("instance");
             console.log(filesArray, "DataSource Before Refresh"); // Verify data
             grid.option("dataSource", filesArray); // Correctly update the data source
             grid.refresh(); // Refresh the grid
         }
         var empID = getQueryParam('ID');   
         var ID = "";
         $(document).ready(function ()
         {
                      bindGrid();
         });
      
         function cleanDocumentUrl(documentUrl) {
            
             console.log("Original documentUrl: ", documentUrl);
             let cleanedUrl = documentUrl.replace(/[\r\n]/g, "");
             cleanedUrl = cleanedUrl.replace(/\\/g, "/");
             cleanedUrl = encodeURI(cleanedUrl);
             console.log("cleanedUrl documentUrl: ", cleanedUrl);
             return cleanedUrl;
         }

         function downloadDocument(documentUrl, documentName) {
             var cleanedUrl = cleanDocumentUrl(documentUrl);  
             var link = document.createElement('a');
             link.href = cleanedUrl; 
             link.download = documentName;  
             link.click(); 
         }

      
                  function bindGrid() {
                  debugger
                  var editOption = 'EditCompany';
                  var deleteOption = '';
                  $.ajax({
                      type: 'POST',
                      url: "Employes_Edit.aspx/LoadData",
                  contentType: 'application/json; charset=utf-8',
                  dataType: 'json',
                      data: { },
                  success: function (result) {

                      $("#divGrid").dxDataGrid({
                          dataSource: JSON.parse(result.d),

                          allowColumnResizing: true,
                          columnAutoWidth: true,
                          showBorders: true,
                          columnChooser: {
                                enabled: true
                          },
                          columnFixing: {
                              enabled: true
                          },
                          paging: {
                              pageSize: 10
                          },
                          filterRow: {
                              visible: true,
                              applyFilter: "auto"
                          },
                          searchPanel: {
                              visible: true,
                              width: 240,
                              placeholder: "Search..."
                          },
                          headerFilter: {
                              visible: true
                          },
                          "export": {
                              enabled: true,
                              fileName: "Employees",
                              allowExportSelectedData: false
                          },
                          columns: [
                              "FileName",
                              "DocumentType",
                              {
                                  dataField: "Id",
                                  width: 150,
                                  caption: "Action",
                                  cellTemplate: function (container, options) {
                                      var ID = options.value;
                                      var documentName = options.data.FileName;
                                      var documentPa = options.data.DocumentPath;
                                      var serverBaseUrl = 'https://hr.ainfinance.com/Uploads';
                                      var serverlocalpath = 'C:/rhussain/HRMS/Publish/Publish/Uploads';

                                      documentPa = documentPa.replace(/\\/g, '/');
                                      var documentUrl = documentPa.replace(serverlocalpath.replace(/\\/g, '/'), serverBaseUrl);
                                      console.log(ID, "docID")
                                      console.log(documentPa, "documentpa")
                                      console.log(documentUrl, "documentUrl")
                                      console.log(serverBaseUrl, "serverBaseUrl")
                                      var html = `
                                                <div style="text-align: left;">
                                                    <button class="myLink" onclick="downloadFile('${documentUrl}', '${documentName}', event)" data-toggle="tooltip" title="Download Document">
                                                        <i class="fa fa-download"></i> Download
                                                    </button>
                                                    <button class="myLink" onclick="deleteDocument(${ID})" data-toggle="tooltip" title="Delete Document">
                                                        <i class="fa fa-trash"></i>
                                                    </button>
                                                </div>`;
                                      container.append(html);
                                  }
                              }
                          ],
                          showBorders: true
                      });
                  },
                  error: function (result) {
                  }
                  });
                  }
         


         function downloadFile(documentUrl, documentName, event) {
             event.preventDefault(); 
             event.stopPropagation();

             if (!documentUrl || !documentName) {
                 console.error("Invalid URL or Name:", documentUrl, documentName);
                 return;
             }

             fetch(documentUrl)
                 .then(response => {
                     if (!response.ok) {
                         throw new Error("Network response was not ok.");
                     }
                     return response.blob();  // Convert the response to a Blob (binary data)
                 })
                 .then(blob => {
                     const link = document.createElement('a');  // Create an anchor element
                    
                     const url = window.URL.createObjectURL(blob);  // Create a URL for the Blob
                     link.href = url;  // Set the URL as the href of the anchor element
                     link.download = documentName;  // Set the file name for the download
                     link.click();  // Programmatically trigger a click to download the file

                     window.URL.revokeObjectURL(url);  // Clean up and revoke the object URL
                 })
                 .catch(error => {
                     console.error("Error downloading file:", error);
                 });
          
         }










         function deleteDocument(ID) {
   
             if (!confirm("Are you sure you want to delete this document?")) {
                 return;
             }
             console.log("ID to be deleted:", ID);
             $.ajax({
                 type: 'POST',
                 url: 'Employes_Edit.aspx/DeleteDocument',
                 data: JSON.stringify({ ID: ID }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (result) {
                    
                     if (result.d === "Success") {
                       /*  alert("Document deleted successfully!");*/

                    
                         bindGrid();  
                     } else {
                        /* alert("Failed to delete the document.");*/
                     }
                 },
                 error: function (xhr, status, error) {
                     console.error("Error in AJAX request: ", status, error);
                    /* alert("An error occurred while deleting the document.");*/
                 }
             });
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





     </script>
</asp:Content>

