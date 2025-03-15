<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Employes_Details.aspx.cs" Inherits="Employes_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee</a></li>
            <li class="active">Employee - Details</li>
        </ol>
    </section>

    <!---nEW sHOW--->
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 ">

                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="panel-heading pt-5">
                            <h4 class="mt-0">Employee Details

                                <asp:Button ID="BtnLog" runat="server" CssClass="btn btn-primary pull-right" Text="Transfer Log" OnClick="BtnLog_Click" Visible="false" />
                                <asp:Button ID="BtnPrintPdf" runat="server" CssClass="btn btn-primary pull-right" Text="Generate Pdf" OnClick="BtnPrintPdf_Click" />
                            </h4>
                        </div>
                        <div class="box box-info">
                            <div class="box-body">
                                <div class="col-sm-6">
                                    <div class="text-left float-left">

                                        <a data-fancybox="gallery" runat="server" id="linkImage">
                                            <asp:Image ID="empImageView" runat="server" CssClass="img-circle img-responsive" />
                                            <%--<img alt="User Pic" src="https://x1.xingassets.com/assets/frontend_minified/img/users/nobody_m.original.jpg" id="profile-image1" class="img-circle img-responsive">--%>
                                        </a>
                                        <!--Upload Image Js And Css-->
                                    </div>

                                    <div class="employeename float-left display-block ml-20 mt-20">
                                        <h3 style="color: #00b1b1;"><asp:Label ID="LblName1" runat="server"></asp:Label> <asp:Label ID="LblName2" runat="server"></asp:Label></h3>
                                        <span>
                                            <p><asp:Label ID="LblDesignation1" runat="server"></asp:Label></p>
                                        </span>
                                    </div>
                                    <!-- /input-group -->
                                </div>
                                <div class="col-sm-6 text-right">
                                </div>
                                <div class="clearfix"></div>
                                <hr style="margin: 5px 0 5px 0;">

                                <div class="col-sm-3 col-xs-3 tital ">First Name:</div>
                                <div class="col-sm-3 col-xs-3"><span id="LblFirstName" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Last Name:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblLastName" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                               <%-- <div class="col-sm-3 col-xs-3 tital ">Email:</div>
                                <div class="col-sm-3"><span id="LblEmail" runat="server"></span></div>--%>
                                <div class="col-sm-3 col-xs-3 tital ">Department:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblDepartment" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Type:</div>
                                <div class="col-sm-3"><span id="LblType" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Line Manager:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblDesignation" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>
                              

                                <div class="col-sm-3 col-xs-3 tital ">Job Status:</div>
<div class="col-sm-3 col-xs-3 "><asp:Label ID="LblProbationPeriod" runat="server" Text="" /></div>
                                <div class="col-sm-3 col-xs-3 tital " id="repotto" visible="false" runat="server">Reports Too:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="lblreportto" runat="server"></span></div>

                                
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>


                                <!-- /.box-body -->
                            </div>
                            <!-- /.box -->
                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="panel-heading">
                                <h4>Personal Information</h4>
                            </div>
                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="box-body">
                                <div class="clearfix"></div>
                                <hr style="margin: 5px 0 5px 0;">

                                <div class="col-sm-3 col-xs-3 tital relation "> <asp:Label ID="Label1" runat="server">Name of Father/Husband:</asp:Label></div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblRelation" runat="server"></span></div>
                               <%-- <div class="col-sm-3 col-xs-3 tital " >Relation With Employee:</div>--%>
                                <div class="col-sm-3 col-xs-3 "><span id="LblFatherName" runat="server" visible="false" ></span></div>
                                
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Date of Birth:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblDOB" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Marital Status:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblMaritalStatus" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Gender:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblGender" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Emergency Contact Number:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblPhone" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Mobile:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblMobile" runat="server"></span></div>
                               
                                <div class="bot-border"></div>
                                
                                <div class="col-sm-3 col-xs-3 tital ">CNIC Number:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="lblcnic" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                      
                                <div class="col-sm-3 col-xs-3 tital ">Religion:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="sreligion" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>
                               <div class="col-sm-3 col-xs-3 tital " id="Div1" runat="server">Personal Email:</div>
                               <div class="col-sm-3 col-xs-3 "><span id="lblPEmail" runat="server"></span></div>
                               <div class="clearfix"></div>
                               <div class="bot-border"></div>



                                <div class="col-sm-3 col-xs-3 tital ">Current Address:</div>
                                <div class="col-sm-3 col-xs-9 "><span id="LblCurrentAddress" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>
                                
                               


                            
                                <div class="col-sm-3 col-xs-3 tital" >Permanent Address:</div>
                                <div class="col-sm-3 col-xs-9 "><span id="LblPermenantAddress" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                              <%--  <div class="table-responsive mt-20 cstm-table">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>Document Type</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            
                                            <tr>
                                                <td>CNIC Front</td>
                                                <td>
                                         <a id="doccnicfront"  target="_blank"   data-toggle="tooltip" title="Nic Front Document" runat="server">Download</a></td>
                                            </tr>   
                                            <tr style="display:none;">
                                                <td>Employee Tansfer</td>
                                                <td>
                                         <a id="doctransferEmp"  target="_blank"   data-toggle="tooltip" title="Employee Transfer Document" runat="server">Download</a></td>
                                            </tr>     
                                               <tr>
                                                <td>Employee Resigned</td>
                                                <td>
                                         <a id="docresignedEmp"  target="_blank"   data-toggle="tooltip" title="Employee Resigned Document" runat="server">Download</a></td>
                                            </tr>     
                                               <tr>
                                                <td>Employee Terminated</td>
                                                <td>
                                         <a id="docterminatedEmp"  target="_blank"   data-toggle="tooltip" title="Employee Terminated Document" runat="server">Download</a></td>
                                            </tr>       
                                              
                                               <tr style="display:none;">
                                                <td>Promotion Letter</td>
                                                <td>
                                         <a id="emppl"  target="_blank"   data-toggle="tooltip" title="Employee Promotion letter" runat="server">Download</a></td>
                                            </tr>                                                      
                                          
                                        </tbody>
                                    </table>
                                </div>--%>
                            </div>
                            <!-- /.box -->



                      <%--      documents section--%>
                                                  <hr class="mt-0 mb-0 border-cstm" />
                      <div class="panel-heading">
                          <h4>Uploaded Documents</h4>
                      </div>
                      <hr class="mt-0 mb-0 border-cstm" />
                      <div class="box-body">
                          <div class="table-responsive mt-20 cstm-table">
                              <table class="table">
                                  <%--<thead>
                                      <tr>
                                          <th>Document Type</th>
                                          <th>Action</th>
                                      </tr>
                                  </thead>--%>
                                  <tbody>
                                      
                      <%--                <tr>
                                          <td>CNIC Front</td>
                                          <td>
                                   <a id="doccnicfront"  target="_blank"   data-toggle="tooltip" title="Nic Front Document" runat="server">Download</a></td>
                                      </tr>   
                                      <tr style="display:none;">
                                          <td>Employee Tansfer</td>
                                          <td>
                                   <a id="doctransferEmp"  target="_blank"   data-toggle="tooltip" title="Employee Transfer Document" runat="server">Download</a></td>
                                      </tr>     
                                         <tr>
                                          <td>Employee Resigned</td>
                                          <td>
                                   <a id="docresignedEmp"  target="_blank"   data-toggle="tooltip" title="Employee Resigned Document" runat="server">Download</a></td>
                                      </tr>     
                                         <tr>
                                          <td>Employee Terminated</td>
                                          <td>
                                   <a id="docterminatedEmp"  target="_blank"   data-toggle="tooltip" title="Employee Terminated Document" runat="server">Download</a></td>
                                      </tr>       
                                        
                                         <tr style="display:none;">
                                          <td>Promotion Letter</td>
                                          <td>
                                   <a id="emppl"  target="_blank"   data-toggle="tooltip" title="Employee Promotion letter" runat="server">Download</a></td>
                                      </tr> --%>   
                                        
                                           <asp:GridView ID="UPGridView" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderText="Document Info">
            <ItemTemplate>
                <%# Eval("FileName") + " " + Eval("DocumentType") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <HeaderStyle Width="15%" />
               <ItemTemplate>
                <!-- Actual file download -->
                <a href='<%# Eval("DocumentPath") %>' 
                   download='<%# Eval("FileName") %>' 
                   target="_blank" 
                   class="btn btn-primary" 
                   data-toggle="tooltip">
                    <i class="fa fa-download"></i> Download
                </a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

                                        
                                    
                                  </tbody>
                              </table>
                          </div>
                     














                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="panel-heading">
                                <h4>Academic Qualification</h4>
                            </div>
                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="box-body">

                                <div class="clearfix"></div>
                                <h4>Academic Info</h4>
                                <asp:GridView ID="GvAcademics" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="Institute Name" DataField="InstituteName" />
                                        <asp:BoundField HeaderText="Degree & Majors" DataField="Degree" />
                                        <asp:BoundField HeaderText="Passing Year" DataField="PassingYear" />
                                        <asp:BoundField HeaderText="CGPA" DataField="CGPA" />
                                    </Columns>
                                </asp:GridView>
                                <div class="clearfix"></div>
                                <h4>Certificate Info</h4>
                                <asp:GridView ID="GvCertificates" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="Institute Name" DataField="InstituteName" />
                                        <asp:BoundField HeaderText="Degree & Majors" DataField="Degree" />
                                        <asp:BoundField HeaderText="Passing Year" DataField="PassingYear" />
                                        <asp:BoundField HeaderText="CGPA" DataField="CGPA" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <!-- /.box -->
                                   <!-- /.box -->

                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="panel-heading">
                                <h4>Previous Job Information</h4>
                            </div>
                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="box-body">

                                <div class="clearfix"></div>
                          
                                <asp:GridView ID="grdpjobinfo" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="CompanyName" DataField="PreviousCompanyName" />
                                        <asp:BoundField HeaderText="Designation" DataField="PreviousDesignation" />
                                        <asp:BoundField HeaderText="JoiningDate" DataField="JoiningDate"/>
                                        <asp:BoundField HeaderText="EndDate" DataField="EndDate" />
                                        
                                    </Columns>
                                </asp:GridView>
                              
                            </div>
                             <hr class="mt-0 mb-0 border-cstm" />
                              <div class="panel-heading">
                                <h4>Employee Transfer Information</h4>
                            </div>
                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="box-body">

                                <div class="clearfix"></div>
                            <div class="table-responsive">
                                <asp:GridView ID="grdtranferemp" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                       
                                        <asp:BoundField HeaderText="EmployeeCode" DataField="EmployeeCode" />
                                        <asp:BoundField HeaderText="EmployeeName" DataField="EmployeeName" />
                                        <asp:BoundField HeaderText="Department" DataField="Department" />
                                          <asp:BoundField HeaderText="Designation" DataField="Designation" />
                                        <asp:BoundField HeaderText="LineManager" DataField="LineManager" />
                                      <%--  <asp:BoundField HeaderText="ReportTo" DataField="ReportTo" />--%>
                                    <%--    <asp:BoundField HeaderText="Email" DataField="Email" />--%>
                                   <%--     <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />--%>
                                        <%--<asp:BoundField HeaderText="TransferDate" DataField="TransferDate"  />--%>
                                        <asp:BoundField HeaderText="TransferDate" DataField="TransferDate" 
                                          DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                                          <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <a href='<%# Eval("Image") %>' id="doc" download='<%# Eval("Image") %>' target="_blank" data-toggle="tooltip" title="Transfer Document" runat="server"><i class="fa fa-file"></i></a>
                                                </ItemTemplate>

                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                              </div>
                            </div>
                            <!-- /.box -->
                               <hr class="mt-0 mb-0 border-cstm" />
                              <div class="panel-heading">
                                <h4>Employee Promotion Information</h4>
                            </div>
                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="box-body">

                                <div class="clearfix"></div>
                             <div class="table-responsive">
                                <asp:GridView ID="GvLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                    <Columns>
                                           <asp:BoundField HeaderText="S.NO" DataField="SNO" />
                                        <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" DataFormatString="{0:MMMM d, yyyy}" />
                                        <%--<asp:BoundField DataField="EntryDate" HeaderText="Entry Time" DataFormatString="{0:hh:mm}" />--%>
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Department" HeaderText="Department" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />                                                                              
                                        <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary" />
                                        <asp:BoundField DataField="HouseRentAllownce" HeaderText="Food Allownce" />
                                        <asp:BoundField DataField="TransportAllownce" HeaderText="Transport Allownce" />
                                        <asp:BoundField DataField="MedicalAllowance" HeaderText="Medical Allowance" />
                                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:d MM, yyyy}" />
                                           <%--<asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:MMMM d, yyyy}" />--%>
                                        <%--<asp:BoundField DataField="Username" HeaderText="Created By" />--%>
                                          <asp:BoundField DataField="Tax" HeaderText="Tax" />
                                     <%--   <asp:TemplateField HeaderText="Promotion Docx">
                                            <ItemTemplate>
                                                <a href="<%# Eval("PromotionDocxPath") %>" class='<%# Eval("FileClass") %>' title="Download File" download target="_blank"><i class="fa fa-file"></i></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            </div>
                            <!-- /.box -->

                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="panel-heading">
                                <h4>Job Information</h4>
                            </div>
                            <hr class="mt-0 mb-0 border-cstm" />
                         <%--   <div class="box-body">
                                <div class="clearfix"></div>
                                <hr style="margin: 5px 0 5px 0;">

                                <div class="col-sm-3 col-xs-3 tital ">Probation Period:</div>
                                <div class="col-sm-3 col-xs-3 tital">
                                    
                                      <asp:Label ID="LblProbationPeriod" runat="server" Text="" />

                                </div>
                              
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Joining Date:</div>
                                <div class="col-sm-3"><span id="LblJoiningDate" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Status:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblStatus" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>
                            </div>--%>


                            <div class="table-responsive">
                             <asp:GridView ID="JLGrid" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                 <Columns>
                                     <asp:BoundField DataField="joiningdate" HeaderText="Joining Date" />
                                     <asp:BoundField DataField="jobstatus" HeaderText="Employee Status" />
                                     <asp:BoundField DataField="type" HeaderText="Job Type" />   
                                     <asp:BoundField DataField="Designation" HeaderText="Designation" />  
                                     <asp:BoundField DataField="department" HeaderText="Department" /> 
                                 </Columns>
                             </asp:GridView>
                         </div>
                            <!-- /.box -->


                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="panel-heading" id="divpayroll" runat="server">
                                <h4>Payroll Information</h4>
                            </div>
                            <hr class="mt-0 mb-0 border-cstm" />
                            <div class="box-body" id="divbankmode" runat="server">
                                <div class="clearfix"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Basic Salary:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblBasicSalary" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Food Allowance:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblHouseRent" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Medical Allowance:</div>
                                <div class="col-sm-3"><span id="LblMedical" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Transport Allowance:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblTransport" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <div class="col-sm-3 col-xs-3 tital ">Fuel Allowance:</div>
                                <div class="col-sm-3"><span id="LblFuel" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Special Allowance:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblSpecial" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                              <%--  <div class="col-sm-3 col-xs-3 tital ">Type of Providient Fund:</div>
                                <div class="col-sm-3"><span id="LblPfType" runat="server"></span></div>
                                <div class="col-sm-3 col-xs-3 tital ">Providient Fund:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblPf" runat="server"></span></div>--%>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                                <hr />
                                 <div class="panel-heading">
                                <h4>Payment Mode  :- <span id="lblpaymentmethod" runat="server"></span></h4>:
                            </div>
                                <div class="col-sm-3 col-xs-3 tital ">Bank Name:</div>
                                <div class="col-sm-3"><span id="LblFromBank" runat="server"></span></div>
                               <div class="col-sm-3 col-xs-3 tital ">Branch Name:</div>
                                <div class="col-sm-3"><span id="lblbranchname" runat="server"></span></div>
                                <div class="clearfix"></div>
                                <div class="bot-border"></div>

                              <%--  <div class="col-sm-3 col-xs-3 tital ">To Bank:</div>
                                <div class="col-sm-3"><span id="LblToBank" runat="server"></span></div>--%>
                               <%-- <div class="col-sm-3 col-xs-3 tital ">To Branch:</div>
                                <div class="col-sm-3 col-xs-3 "><span id="LblToBranch" runat="server"></span></div>
                                <div class="clearfix"></div>--%>
                                <div class="bot-border"></div>
                                  <div class="col-sm-2 col-xs-3 tital ">Account No:</div>
                                <div class="col-sm-2"><span id="lblaccountno" runat="server"></span></div>
                                  <div class="col-sm-2 col-xs-3 tital ">Account Titles:</div>
                                <div class="col-sm-2"><span id="lblaccountitile" runat="server"></span></div>
                               <div class="col-sm-2 col-xs-3 tital ">Branch Code:</div>
                                <div class="col-sm-2"><span id="lblbranchcode" runat="server"></span></div>                                
         

                                <div class="clearfix"></div>
                            </div>
                            <!-- /.box -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
    <div class="modal fade" id="employes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 96% !important">
            <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Employee - <small>Transfer Log</small></h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="GvTransferLog" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:MMMM d, yyyy}" />
                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Time" DataFormatString="{0:hh:mm}" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />

                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                    <asp:BoundField DataField="ManagerID" HeaderText="ManagerID" />
                                    <asp:BoundField DataField="Action" HeaderText="Action" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!----cLOSE--->

    <script type="text/javascript">
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
            $("[id$=GvTransferLog]").prepend($("<thead></thead>").append($("[id$=GvTransferLog]").find("tr:first"))).dataTable();
            
        }
     
           

        function UpdateEmployeeType() {
       /*     LblType*/

            console.log("change calls");
            $.ajax({
                type: 'POST',
                url: "Employes_Details.aspx/CheckEmail",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'Email':'" + email + "'}",
                success: function (result) {
                    var data = JSON.parse(result.d);
                    if (data) {
                        //$('#spnEmail').show();
                        //$('.next').attr('disabled', 'true');
                        $('#spnEmail').hide();
                        $('.next').removeAttr('disabled');
                        $('#ddlDesignation').prop('disabled', false)
                        $('#ddEmployeType').prop('disabled', false);


                    }
                    else {
                        //$('#spnEmail').hide();
                        //$('.next').removeAttr('disabled');

                        $('#spnEmail').show();
                        $('.next').attr('disabled', 'true');
                        $('#ddlDesignation').prop('disabled', true);
                        $('#ddEmployeType').prop('disabled', true);

                    }

                },
                error: function (result) {

                }
            });

        }
        

        

            
    </script>
</asp:Content>