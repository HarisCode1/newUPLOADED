<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Calender.aspx.cs" Inherits="Calender" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <style>
        .options {
    padding: 20px;
    background-color: rgba(191, 191, 191, 0.15);
    margin-top: 20px;
}
.select2-container {
width: 20% !important;
padding: 0;
}
.caption {
    font-size: 18px;
    font-weight: 500;
}
.option {
    margin-top: 10px;
    display: inline-block;
    width: 19%;
}
.dx-scheduler-dropdown-appointments
{
    display:none;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Attandence</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Attandence</li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Attandence</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                           
                         <div class="col-md-3">
                             <label>Employee * :</label>
                             <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" ClientIDMode="Static" style="width:250px;"  validate='vgroup' autocomplete="off" require='Please select date' onchange="toggleEmployeeCode()">
                                 <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                                 <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                 <asp:ListItem Value="1" Text="Employee"></asp:ListItem>
                             </asp:DropDownList>
                         </div>
                        
                           <div class="col-md-3">
                             <label>From Date * :</label>
                            <asp:TextBox ID="txtfromdate" runat="server" onkeypress="return isNumberKey(event);" ClientIDMode="Static" CssClass="form-control datepicker"></asp:TextBox>
                           </div> 

                           <div class="col-md-3">
                             <label>To Date * :</label>
                            <asp:TextBox ID="txttodate" runat="server" onkeypress="return isNumberKey(event);" ClientIDMode="Static" CssClass="form-control datepicker" ></asp:TextBox>
                          </div> 

                           <div class="col-md-3">
                             <label style="display:block">&nbsp;</label>
                             <asp:Button ID="btn_submit" CssClass="btn btn-primary pull-right"  runat="server" Text="View Attendance Sheet" />
                           </div>
                            
                        </div>
                        <br />
                        
                     <div class="row" id="employeeCodeDiv" style="display: none;">
                        <div class="col-md-3">
                            <label>Employee Code * :</label>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" style="width:250px;"></asp:TextBox>
                        </div>
                    </div>
                    </div>



                </div>
            </div>
        </div>
    </section>
    <link href="assets/select2-4.0.12/dist/css/select2.min.css" rel="stylesheet" />
    <script src="assets/select2-4.0.12/dist/js/select2.min.js"></script>
     <script src="js/bootstrap-datepicker.min.js"></script>
     <script src="assets/js/jquery.easing.min.js"></script>
    <script type="text/javascript">


        $('#<%= txtfromdate.ClientID %>').datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            clearbtn: false
        });
        $('#<%= txttodate.ClientID %>').datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            clearbtn: false
        });
        function toggleEmployeeCode() {
            var dropdown = document.getElementById("DropDownList1");
            var employeeCodeDiv = document.getElementById("employeeCodeDiv");

            if (dropdown.value === "1") {
                employeeCodeDiv.style.display = "flex"; // Show
            } else {
                employeeCodeDiv.style.display = "none"; // Hide
            }
        }

        document.addEventListener("DOMContentLoaded", function () {
            var dropdown = document.getElementById("DropDownList1");
            var employeeCodeDiv = document.getElementById("employeeCodeDiv");

            // Pehli baar page load hone par check karein
            if (dropdown.value === "1") {
                employeeCodeDiv.style.display = "flex"; // Agar Employee select tha, to dikhayein
            } else {
                employeeCodeDiv.style.display = "none"; // Agar default value hai to hide karein
            }

            dropdown.addEventListener("change", toggleEmployeeCode);
        });

    </script>
</asp:Content>













