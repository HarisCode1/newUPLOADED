<%@ Page Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Employee_Terminate.aspx.cs" Inherits="Employee_Terminate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Employee</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx"><i class="fa fa-dashboard"></i>Employee</a></li>
            <li class="active">Employee - Employee Terminated</li>
        </ol>
    </section>

        <ContentTemplate>
            <asp:Panel ID="pnlDetail" runat="server">
                <section id="msform" class="content cstm-csform">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-info custom_input">
                                <div class="box-header with-border">
                                    <i class="fa fa-user-plus custom_header_icon admin_icon"></i>
                                    <h3 class="box-title">Terminate Employee</h3>
                                   
                                </div>
                               <asp:Panel ID="Panel1" runat="server">
                                 <section id="msform" class="content cstm-csform">
                                <div class="row">
                                             <div class="col-md-12" id="mybtn">

                                             <fieldset>

                                                 <div class="form-group col-md-12 ">

                                                     <asp:TextBox ID="MsgDelete" Visible="false" runat="server"></asp:TextBox>
                                                     <asp:HiddenField ID="hdnempid" runat="server" ClientIDMode="Static"></asp:HiddenField>
                                                     <asp:TextBox ID="txtreason" runat="server" ClientIDMode="Static" placeholder="Reason of termination"></asp:TextBox>
                    
                                                 </div>
                                                 <div class="form-group col-md-12">
                                                     <asp:HiddenField ID="HiddenField1" runat="server" Value="SomeValue" />

                                                      <span id="spncheck" style="display:none;color:red;">Please select date</span>
                                                       <asp:TextBox  ID="txtEntryDate" runat="server"  Cssclass="datepicker" validate='vgroup' require="Please Enter date"  ></asp:TextBox>
                                                     <br />
                                                     <asp:FileUpload ID="UploadDocImage" runat="server" />                                              
                                                 </div>
                 

                                             </fieldset>
                                         </div>
                                     </div>



                              </section>
                            </asp:Panel>
                                 <div class="modal-footer">
                                <asp:Button id="btnsave1" runat="server" Text="Terminate" class="btn btn-primary" onclick="btnsave_Click" OnClientClick="$('#confirmationModal').modal('show'); return false;"></asp:Button>
                            
                             </div>

                            </div>
                        </div>
                    </div>
                </section>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave1" />
        </Triggers>
  

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
                    <p>Are you sure you want to terminate this employee?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" id="btnConfirmYes">Yes</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>




    <div class="modal fade" id="successModal" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Success</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>The employee has been successfully terminated.</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal">Ok</button>
            </div>
        </div>
    </div>
</div>



    
    <script src="assets/js/jquery.easing.min.js"></script>
   <%-- <script src="js/bootstrap-datepicker.min.js"></script>--%>
      <script src="https://cdn.jsdelivr.net/npm/pikaday/pikaday.js"></script>
 <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
<script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.9.0/dist/js/bootstrap-datepicker.min.js"></script>


  <script>

      var currentDate = new Date();
      var hiddenValue = document.getElementById('<%= HiddenField1.ClientID %>').value;
      var prevoise = new Date(hiddenValue);
      console.log(hiddenValue, "hiddenValue");

      // Bootstrap Datepicker
      function initializeDatePicker() {


          $('#<%= txtEntryDate.ClientID %>').datepicker({
              format: 'dd/mm/yyyy',
              autoclose: true,
              clearbtn: false,
              beforeShowDay: function (date) {
                  var day = date.getDay();

                  if (date > currentDate) {
                      return false;
                  }
                  if (date < prevoise) {
                      return false;
                  }

                  if (day === 0 || day === 6) {
                      return false;
                  }
                  return true;
              }
          });
      }
      $(document).ready(function () {
          initializeDatePicker();
      });
      document.getElementById('btnConfirmYes').addEventListener('click', function () {
          __doPostBack('<%= btnsave1.UniqueID %>', '');
       });
  </script>
    </asp:Content>

