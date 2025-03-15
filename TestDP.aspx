<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestDP.aspx.cs" Inherits="TestDP" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bootstrap Datepicker in WebForms</title>
    
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Bootstrap Datepicker CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />

    <!-- jQuery (required for Bootstrap JS and Datepicker) -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>

    <!-- Bootstrap JS -->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>

    <!-- Bootstrap Datepicker JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Bootstrap Datepicker in WebForms</h2>
            
            <!-- Datepicker TextBox -->
            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control datepicker" ClientIDMode="Static" placeholder="Select Date"></asp:TextBox>
        </div>
        
        <script>
            // Initialize Bootstrap Datepicker on the TextBox
            $(document).ready(function () {
                $('.datepicker').datepicker({
                    format: 'dd-mm-yyyy',  // Format: Day-Month-Year
                    autoclose: true        // Automatically close the datepicker when a date is selected
                });
            });
        </script>
    </form>
</body>
</html>
