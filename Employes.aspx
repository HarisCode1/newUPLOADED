<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="~/Employes.aspx.cs" Inherits="Employes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <!--Animate CSS -->
    <link rel="stylesheet" href="assets/css/animate.css" />
    <style type="text/css">
        .table-condensed th, .table-condensed td {
            padding: 0px !important;
        }

        .PayRollTxt {
            width: 242px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Employee</li>
        </ol>
    </section>
    <section class="content">
        <asp:FileUpload type="file" class="form-control cstm-btn-file" ID="FileExportExcel" runat="server" />
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#employes" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New"  CssClass="btn btn-primary pull-right"></asp:LinkButton>--%>
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" OnClick="BtnAddNew_Click" />
                        <asp:Button ID="Btnexcelimport" ClientIDMode="Static" runat="server" Text="Bulk Import" CssClass="submit action-button btn btn-primary pull-right" OnClick="Btnexcelimport_Click" />
                        <%--  <div class="file-upload-wrapper file-upload-wrapper1" data-text="Import Excel File!">
                           <asp:FileUpload ID="FileExportExcel" runat="server" />
                         </div>--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Btnexcelimport" />
                    </Triggers>                    
                </asp:UpdatePanel>
                <asp:Label id="bulkemployee" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <!-- /.modal-content -->
        <div class="modal fade" id="employes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
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
                                                <h3 class="box-title">New Employee Registration</h3>
                                                <div class="box-tools pull-right">
                                                    <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                                    <button class="btn btn-box-tool" data-dismiss="modal"><i class="fa fa-times"></i></button>
                                                </div>
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
                                                                    <label class="col-sm-2 control-label">Personal Email</label>
                                                                    <div class="col-sm-4">
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
                                                                        <asp:TextBox ID="TxtEmployeeCode" class="form-control" runat="server" validate='vgroup' require="Please Enter your Employee Code" placeholder="Enter Employee Code"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdEmployeeID" runat="server" />
                                                                    </div>
                                                                    <label class="col-sm-2 control-label">User Name</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="Txtusername" class="form-control" runat="server" validate='vgroup' require="It should be Unique to every user." placeholder="Enter First & Last Name"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Password</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="TxtPassword" class="form-control" runat="server" validate='vgroup' CssClass="ValidationError" require="Minimum 4 Character" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                                                                    </div>
                                                                    <label class="col-sm-2 control-label">Confirm Password</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="TxtConfirmPassword" class="form-control" runat="server" validate='vgroup' require="Please Enter Confirm Password" placeholder="Confirm Password" TextMode="Password" compare='password mismatch' compareTo="ContentPlaceHolder1_TxtPassword"></asp:TextBox>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtConfirmPassword" CssClass="ValidationError" ControlToCompare="TxtPassword" ForeColor="Red" ErrorMessage="Password MisMatch" ToolTip="Password must be the same"></asp:CompareValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <input type="button" name="next" class="next action-button" value="Next" />
                                                            <%--onclick="if (validate('vgroup')) { return true; } else { return false; }"--%>
                                                            <%-- <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Save Changes" OnClientClick="if(validate('vgroup')){return true;}else{return false;}"></asp:Button>--%>
                                                        </fieldset>
                                                        <%-- End Account Setup--%>
                                                        <%-- Start Personal Info--%>
                                                        <fieldset>
                                                            <div id="divgeneraldetails" class="new_emp_form">
                                                                <h2 class="fs-title"><b>General Details </b></h2>
                                                                <hr />
                                                                <div class="row">
                                                                    <div class="col-md-8">
                                                                        <div class="form-horizontal">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-2 control-label">Father/Husband Name:</label>
                                                                                <div class="col-sm-4">
                                                                                    <asp:TextBox ID="txtFatherName" validate='vgroup' require="Please Select" class="form-control" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-2 control-label">Relation with Employee:</label>
                                                                                <div class="col-sm-4">
                                                                                    <asp:DropDownList ID="ddlrelation" validate='vgroup' require="Please Select" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                        <asp:ListItem Value="F">Father</asp:ListItem>
                                                                                        <asp:ListItem Value="H">Husband</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <div class="form-group">
                                                                            <!--label class="col-sm-2 control-label"></label-->
                                                                            <div class="col-sm-10">
                                                                                <asp:HiddenField ID="hdEmpPhotoID" runat="server" />
                                                                                <asp:HiddenField ID="hdImageName" runat="server" />
                                                                                <asp:HiddenField ID="hdEnrollId" runat="server" />
                                                                                <asp:Image ID="empImageView" ImageUrl="~/images/user-image.png" runat="server" CssClass="img-circle align" />
                                                                                <asp:FileUpload ID="uploadEmpImage" CssClass="upload_btn" runat="server" />
                                                                                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Height="0" Width="0" BackColor="#ECF0F5" BorderStyle="None" />
                                                                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <%--  Other Details--%>
                                                                <div class="form-horizontal">
                                                                    <hr />
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Blood Group</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:DropDownList ID="ddlBloodGroup" validate='vgroup' require="Please Select" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select Blood Group..</asp:ListItem>
                                                                                <asp:ListItem>A</asp:ListItem>
                                                                                <asp:ListItem>A-</asp:ListItem>
                                                                                <asp:ListItem>A+</asp:ListItem>
                                                                                <asp:ListItem>B</asp:ListItem>
                                                                                <asp:ListItem>B-</asp:ListItem>
                                                                                <asp:ListItem>B+</asp:ListItem>
                                                                                <asp:ListItem>AB</asp:ListItem>
                                                                                <asp:ListItem>AB-</asp:ListItem>
                                                                                <asp:ListItem>AB+</asp:ListItem>
                                                                                <asp:ListItem>O</asp:ListItem>
                                                                                <asp:ListItem>O-</asp:ListItem>
                                                                                <asp:ListItem>O+</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label">Gender</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:DropDownList ID="ddlSex" validate='vgroup' require="Please Select" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                <asp:ListItem>Female</asp:ListItem>
                                                                                <asp:ListItem>Male</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Date of Birth:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtDOB" runat="server" validate='vgroup' require="Please Select" CssClass="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label">Status:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:RadioButtonList ID="rdoMarriedStatus" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Class="CustomLabel" runat="server">Married</asp:ListItem>
                                                                                <asp:ListItem>Unmarried</asp:ListItem>
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" id="MarriedShow">
                                                                        <label class="col-sm-2 control-label">Wed Aniversary:</label>
                                                                        <div id="dtWeddAnnev" class="col-sm-4">
                                                                            <!--style="width: 214px;"-->
                                                                            <asp:TextBox ID="txtWeddAnnev" runat="server" CssClass="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <%--  Contact Details--%>

                                                                    <hr />
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Residance No:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtResi" validate='vgroup' require="Please Select" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>

                                                                        </div>
                                                                         <label class="col-sm-2 control-label">Mobile No:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtMobileNo" validate='vgroup' require="Please Select" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>

                                                                        </div>
                                                                        
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Phone :</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtPhone" validate='vgroup' require="Please Select" onkeypress="return isNumberKey(event);" CssClass="form-control" runat="server"></asp:TextBox>

                                                                        </div>
                                                                        <label class="col-sm-2 control-label">Current Address :</label>
                                                                        <div class="col-sm-3">
                                                                            <asp:TextBox ID="txtCurrentAddress" validate='vgroup' require="Please Select" runat="server" TextMode="MultiLine" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-sm-1">
                                                                            <asp:CheckBox ID="chkCurrentAddress" Text="Same as Above" CssClass="Add_check" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Permanent Address :</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtParmanantAddress" validate='vgroup' require="Please Select" runat="server" TextMode="MultiLine" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label"></label>
                                                                        <div class="col-sm-4">
                                                                        </div>
                                                                    </div>
                                                                     <div class="form-group">
                                                                        <label class="col-sm-2 control-label">City:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtCity" validate='vgroup' runat="server" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label">Province:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtProvince" validate='vgroup' runat="server" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Country:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label"></label>
                                                                        <div class="col-sm-4">
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Address 1:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtAddress1" style="width:275%;" runat="server" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label"></label>
                                                                        <div class="col-sm-4">
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Address 2:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtAddress2" style="width:275%;" runat="server" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label"></label>
                                                                        <div class="col-sm-4">
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-2 control-label">Address 3:</label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtAddress3" style="width:275%;" runat="server" CssClass="emp-address form-control"></asp:TextBox>
                                                                        </div>
                                                                        <label class="col-sm-2 control-label"></label>
                                                                        <div class="col-sm-4">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <h2 class="fs-title"><b>Documents Upload</b></h2>
                                                                            <div class="form-group">
                                                                                <asp:HiddenField ID="hdResumeFileUpload" runat="server" />
                                                                                <label id="txtCV" class="col-sm-4 control-label">Attach Resume</label>
                                                                                <asp:FileUpload ID="ResumeFileUpload" runat="server" />
                                                                                <asp:Label ID="LblResumeUpload" runat="server" class="labelresume control-label"></asp:Label>
                                                                                <br></br>
                                                                                <asp:HiddenField ID="hdNICName" runat="server" />
                                                                                <asp:HiddenField ID="hdNICImage" runat="server" />
                                                                                <label id="txtNIC" class="col-sm-4 control-label">Attach NIC</label>
                                                                                <asp:FileUpload ID="NICUpload" runat="server" />
                                                                                <asp:Label ID="lblNIC" runat="server" class="labelNIC control-label"></asp:Label>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <asp:HiddenField ID="hdpasportname" runat="server" />
                                                                                <asp:HiddenField ID="hdPasportImage" runat="server" />
                                                                                <label id="txtpassport" class="col-sm-4 control-label">Attach Passport</label>
                                                                                <asp:FileUpload ID="PassportUpload" runat="server" />
                                                                                <asp:Label ID="lblpasport" runat="server" class="LabelPasport control-label"></asp:Label>
                                                                                <br></br>
                                                                                <asp:HiddenField ID="hddocumenstupload" runat="server" />
                                                                                <label id="txtdocuments" class="col-sm-4 control-label">Attach Documents</label>
                                                                                <asp:FileUpload ID="documentsupload" runat="server" />
                                                                                <asp:Label ID="lbldocuments" runat="server" class="labeldocuments control-label"></asp:Label>
                                                                            </div>
                                                                            <br></br>
                                                                            <div class="form-group">
                                                                                <asp:HiddenField ID="hdotherdocumntsupload" runat="server" />
                                                                                <label id="txtothers" class="col-sm-4 control-label">Attach Others Documents</label>
                                                                                <asp:FileUpload ID="otherdocumetssupload" runat="server" />
                                                                                <asp:Label ID="lblotherdocuments" runat="server" class="labelotherdocuments control-label"></asp:Label>
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
                                                        <fieldset>

                                                            <div class="col-md-12 animated bounceInDown" id="mybtn1">
                                                                <ul id="progressbarQualification" style="margin-left: 10%">
                                                                    <li class="active">Academic Info</li>
                                                                    <li><a href="#">Certificates</a></li>
                                                                </ul>
                                                            </div>
                                                            <fieldset>

                                                                <div id="divacademicinfo" class="box-body NewEmp_boxBody">
                                                                    <div>
                                                                        <div class="form-horizontal new_emp_form">
                                                                            <h2 class="fs-title"><b>Academic Info </b></h2>
                                                                            <hr />
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:HiddenField ID="hdnIdAcademic" runat="server" />

                                                                                    <label class="col-sm-2">Institute Name</label>
                                                                                    <asp:TextBox ID="txtinsname" runat="server" require="Please Enter Institute" placeholder="Enter Institute"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label class="col-sm-2">Qualification</label>
                                                                                    <asp:TextBox ID="txtqualification" runat="server" require="Please Enter Qualification" placeholder="Enter Institute"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label class="col-sm-2">Year</label>
                                                                                    <asp:TextBox ID="txtyear" runat="server" class="is-number" require="Please Enter Year" placeholder="Enter Year"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label class="col-sm-2">Marks</label>
                                                                                    <asp:TextBox ID="txtmarks" class="col-sm-2 is-number-with-decimal" runat="server" require="Please Enter Marks" placeholder="Enter Marks"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <input type="button" id="btninssave" class="action-button" value="Add" />

                                                                                </td>
                                                                            </tr>
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

                                                                        <asp:HiddenField ID="txtHidden_Academic" runat="server" />

                                                                    </div>
                                                                </div>

                                                            </fieldset>

                                                            <fieldset>
                                                                <div id="divcertificates" class="box-body NewEmp_boxBody">
                                                                    <div>
                                                                        <div class="form-horizontal new_emp_form">
                                                                            <h2 class="fs-title"><b>Certificate Info</b></h2>
                                                                            <hr />
                                                                            <tr>
                                                                                <td>
                                                                                    <label class="col-sm-2">Institute Name</label>
                                                                                    <asp:TextBox ID="txtcrtfctname" runat="server" require="Please Enter Institute" placeholder="Enter Institute"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label class="col-sm-2">Qualification</label>
                                                                                    <asp:TextBox ID="txtcrtfctqualification" class="col-sm-2" runat="server" require="Please Enter Marks" placeholder="Enter Marks"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label class="col-sm-2">Year</label>
                                                                                    <asp:TextBox ID="txtcrtfctinsYear" runat="server" class="is-number" require="Please Enter Year" placeholder="Enter Year"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label class="col-sm-2">Grade</label>
                                                                                    <asp:TextBox ID="txtcrtfctgrade" class="col-sm-2" runat="server" require="Please Enter Grade" placeholder="Enter Grade"></asp:TextBox>

                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>
                                                                                    <input type="button" id="btncrtfctsaves" class="action-button" value="Add" />
                                                                                </td>
                                                                            </tr>

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


                                                        <%-- End  Qualification Info
                                                        
                                                        <%-- Job Info--%>
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
                                                            <%--  Job Details--%>

                                                            <h2 class="fs-title"><b>Job Detail </b></h2>
                                                            <br></br>
                                                            <hr />
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-horizontal">
                                                                        <div class="form-group">
                                                                            <label class="col-sm-4 control-label ">Joining Date:</label>
                                                                            <div id="dtJoiningDate" class="col-sm-8">
                                                                                <asp:TextBox ID="txtJoiningDate" runat="server" CssClass="form-control" autocomplete="off" ClientIDMode="Static" validate='vgroup' require="Please Enter Joining Date"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-4 control-label">Job Status:</label>
                                                                            <div id="RJobStatusr" class="col-sm-8">
                                                                                <asp:RadioButtonList ID="rdoJobStatus" runat="server" RepeatDirection="Horizontal">
                                                                                    <asp:ListItem Selected="True" Text="Active">Active</asp:ListItem>
                                                                                    <asp:ListItem Text="Deactive">Deactive</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <label class="col-sm-2 control-label"></label>
                                                                            <div class="col-sm-2">
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group Deactive" style="display: none">
                                                                            <label class="col-sm-4 control-label">Leaving Reason:</label>
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
                                                                </div>
                                                                <%--Col md end --%>
                                                                <div class="col-md-6">
                                                                    <div class="form-horizontal">
                                                                        <div class="form-group">
                                                                            <%--<label class="col-sm-4 control-label">Radio Btn</label>--%>
                                                                            <asp:RadioButton ID="rdoProb" Text="Pro: Period" runat="server" Checked="true" CssClass="col-sm-6" GroupName="rdoConf_Pro" />
                                                                            <%--<label class="col-sm-8 control-label">Radio Btn</label>--%>
                                                                            <asp:RadioButton ID="rdoConfirmation" Text="Conf: Date" runat="server" CssClass="col-sm-6" GroupName="rdoConf_Pro" />
                                                                        </div>
                                                                        <div class="form-group" id="Conf" runat="server" style="display: none;">
                                                                            <label class="col-sm-4 control-label">Confr. Date:</label>
                                                                            <div id="dtConfrDate" class="col-sm-8 ">
                                                                                <asp:TextBox ID="txtConfrDate" runat="server" CssClass="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group" id="Prov" runat="server" style="display: none">
                                                                            <label class="col-sm-4 control-label">Probation Period (No of Months):</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:TextBox ID="txtProbationPeriod" runat="server" class="is-number"></asp:TextBox>
                                                                            </div>
                                                                            <label class="col-sm-2 control-label"></label>
                                                                            <div class="col-sm-2">
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group Deactive" style="display: none">
                                                                            <label class="col-sm-4 control-label">Leaving Date:</label>
                                                                            <div id="dtLeavingDate" class="col-sm-8">
                                                                                <asp:TextBox ID="txtLeavingDate" runat="server" CssClass="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        <div style="display: none">
                                                                            <label class="col-sm-4 control-label">Shift:</label>
                                                                            <div class="col-sm-8">
                                                                                <asp:HiddenField ID="hdDdlShift" runat="server" />
                                                                                <asp:DropDownList ID="ddlShift" runat="server" AppendDataBoundItems="true" AutoPostBack="true" custom="Please Select Shift" DataSourceID="EDS_Shift" DataTextField="ShiftShortName" DataValueField="ShiftID" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged1">
                                                                                    <asp:ListItem Value="0" Text="Please Select Shift..."></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:EntityDataSource ID="EDS_Shift" runat="server" ConnectionString="name=vt_EMSEntities" DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Shift" Where='<%# "it.CompanyId ="+ Viftech.vt_Common.CompanyId %>'>
                                                                                </asp:EntityDataSource>
                                                                                <asp:Label ID="Label1" runat="server" Text="InTime"></asp:Label>
                                                                                <asp:Label ID="lblShiftInTime" runat="server"></asp:Label>

                                                                                <asp:Label ID="Label2" runat="server" Text="OutTime"></asp:Label>
                                                                                <asp:Label ID="lblShiftOutTime" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <input type="button" name="next" class="next action-button" value="Next" />
                                                            <%--onclick="if (validate('vgroup')) { return true; } else { return false; }"--%>
                                                            <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                        </fieldset>
                                                        <%--Shift/Woff--%>
                                                        <%--Payroll Setting--%>
                                                        <fieldset>
                                                            <div id="divpayrollsetting" class="form-horizontal">
                                                                <h2 class="fs-title"><b>Payroll Setting </b></h2>
                                                                <br />
                                                                <div class="form-group">
                                                                    <label class="col-sm-2">Basic Salary</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtBasicSalary" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="Basic Salary" MaxLength="9" max="9"></asp:TextBox>
                                                                    </div>

                                                                    <label class="col-sm-2">House Rent Allowance</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtHouseRentAllowance" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="House Rent Allowance" MaxLength="7" max="7"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-2 ">Medical Allowance</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtMedicalAllowance" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="Medical Allowance" MaxLength="7" max="7"></asp:TextBox>
                                                                    </div>

                                                                    <label class="col-sm-2 ">Transport Allowance</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtTransportAllowance" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="Transport Allowance" MaxLength="7" max="7"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-2 ">Fuel Allowance</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtFuelAllowance" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="Fuel Allowance" MaxLength="7" max="7"></asp:TextBox>
                                                                    </div>

                                                                    <label class="col-sm-2 ">Special Allowance</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtSpecialAllowance" validate='vgroup' require="Please Enter" class="is-number-with-decimal" runat="server" placeholder="Special Allowance" MaxLength="7" max="7"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">Type Of Provident Fund</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:DropDownList ID="ddPFtype" class="form-control" runat="server" validate='vgroup' require="Please Enter">
                                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <label class="col-sm-2 ">Provident Fund</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:CheckBox ID="ChkBxProvidentFund" runat="server" />
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <h2 class="fs-title"><b>Bank Details </b></h2>
                                                                    <label class="col-sm-2 ">From Bank</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtbankfrom" validate='vgroup' require="Please Enter" runat="server" placeholder="From Bank"></asp:TextBox>
                                                                    </div>
                                                                    <label class="col-sm-2 ">From Branch</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtbrachfrom" validate='vgroup' require="Please Enter" runat="server" placeholder="From Branch"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 ">To Bank</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtbankto" validate='vgroup' require="Please Enter" runat="server" placeholder="To Bank"></asp:TextBox>
                                                                    </div>
                                                                    <label class="col-sm-2 ">To  Branch</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtbranchto" validate='vgroup' require="Please Enter" runat="server" placeholder="To Branch"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 ">Account No</label>
                                                                    <div class="col-sm-4">
                                                                        <asp:TextBox ID="txtaccount" validate='vgroup' onkeypress="return isNumberKey(event);" require="Please Enter" runat="server" placeholder="Account NO"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Button ID="btnSaveEmployee" ClientIDMode="Static" runat="server" Text="Save" Style="width: 130px;" CssClass="submit action-button" OnClientClick="return Saveonclick();" OnClick="btnSaveEmployee_Click"></asp:Button>
                                                            <%--  <asp:Button ID="Button1" ClientIDMode="Static" runat="server" Text="Save Employes" Style="width: 130px;" CssClass="submit action-button" OnClientClick="return getCertificateRecords(); if(validate('vgroup')){return true;}else{return false;} " OnClick="btnSaveEmployes_Click"></asp:Button>--%>
                                                            <input type="button" name="previous" class="previous action-button" value="Previous" />
                                                            </label>
                                                        </fieldset>
                                                    </div>
                                                    <%--End div--%>
                                                </div>

                                                <%--</form>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="">

                                        <%--<div class="col-sm-12 " style="padding-bottom:20px;">
                                    
                                </div>--%>
                                    </div>
                                    <!-- /.box-footer .companyHeading-->
                                </div>
                                <!-- /.box -->
                                <%--	</div>
				</div>	--%>
                            </section>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="btnUpload"/>--%>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="row" id="divCompany" runat="server" visible="false">
            <div class="col-md-6">
                <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 130px;">
                                    <label>Company :</label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities"
                                        EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Employee</h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div id="divGrid"></div>
                                    <asp:GridView ID="grdEmp" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdEmp_RowCommand" Visible="false">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeID" HeaderText="Enroll Id" />
                                            <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("EmployeeID")%>' CommandName="EditCompany"> 
                                                        <i class="fa fa-pencil-square-o">
                                                        </i> 
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("EmployeeID")%>' CommandName="DeleteCompany" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                                    <i class="fa fa-times-circle">
                                                    </i>
                                                </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- <asp:EntityDataSource ID="EDS_Emp" runat="server"
                        ConnectionString="name=vt_EMSEntities"
                        DefaultContainerName="vt_EMSEntities" EntitySetName="vt_tbl_Employee"
                        Where='<%# "it.CompanyId ="+ Viftech.vt_Common.CompanyId %>'>
                    </asp:EntityDataSource>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="assets/js/jquery.easing.min.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        'use strict';

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

            //$("[id $=uploadEmpImage]").change(function () {
            //    $("[id $=btnUpload]").click();
            //});
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

            if (validate('vgroup') == true) {

                getCertificateRecords();
                return true;

            }
            else {
                return false;
            }
        }
        $("#ddEmployeType").dxSelectBox({
            searchEnabled: true
        });



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
                //validate('vgroup');
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
                //validate('vgroup');

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

    </script>

    <!--SALMAN CODE-->
    <script>

        //function edit_DevExtreme(id) {
        //    var action = 'EditCompany';
        //    $.ajax({
        //        type: 'POST',
        //        url: "Employes.aspx/DevExtreme_Actions",
        //        contentType: 'application/json; charset=utf-8',
        //        dataType: 'json',
        //        data: "{'ID':'" + id + "', 'Action':'" + action + "'}",
        //        success: function (result) {
        //        },
        //        error: function (result) {
        //        }
        //    });
        //}

        //function delete_DevExtreme(id) {
        //    var action = 'DeleteCompany';
        //    alert(id + action);
        //}

        $(document).ready(function () {
            AdminJobGrid();
        });

        function AdminJobGrid() {

            var editOption = 'EditCompany';
            var deleteOption = '';
            $.ajax({
                type: 'POST',
                url: "Employes.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {},
                success: function (result) {

                    $("#divGrid").dxDataGrid({
                        dataSource: JSON.parse(result.d),
                        columnsAutoWidth: true,
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
                        searchPanel: { visible: true },
                        columns: [
                            "EmployeeID",
                            "CompanyName",
                            "EmployeeName",
                            "FirstName",
                            "LastName",
                           "Email",
                            {
                                dataField: "EmployeeID",
                                caption: "Action",
                                cellTemplate: function (container, options) {

                                    //container.append("<a href='/Requirements/ContributorJobDetails?ID=" + options.value + "'><i data-toggle='tooltip' title='Preview' data-placement='top' class='fa fa-eye fa-2x'></i></a>")
                                    container.append("<a href='Employes_Edit.aspx?ID=" + options.value + "'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></a>  <a href='Employes_Details.aspx?ID=" + options.value + "'><i class='fa fa-trash-o' aria-hidden='true'></i></a>")
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
    </script>
    <!--SALMAN CODE-->

</asp:Content>
