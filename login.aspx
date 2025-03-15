<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>PayRoll | LogIn</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/until.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/font-awesome/css/font-awesome.min.css" />
    <link href="css/customstyles.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <%-- <link rel="stylesheet" href="assets/css/custom.css" />--%>
    <!-- Custom CSS -->
    <%--  <link rel="stylesheet" href="css/login.css" />--%>
    <!-- Font Awesome -->
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
			<script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
			<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
		<![endif]-->
</head>
<body>
    <form runat="server">
        <!---New Login--->
        <div class="limiter1">
            <div class="container-login100">
                <div class="wrap-login100">
                 
                    <div class="login100-pic js-tilt for_rotateimg" data-tilt="">
                           <span class="forlogoemsClient"><img src="images/Ain-Al-aseel.png"/></span>
                        <%--<img src="/Content/App_Images/E-com Logo.png" class="forlogoemsClient" alt="img" />--%>
                    </div>
                    <%--<form runat="server" class="login100-form validate-form">--%>
                    <div class="login100-form validate-form">
                        <div class="login100-pic1 js-tilt" data-tilt="">

                            <img src="images/Ain-oneApp1.png" class="forlogoemsClient" alt="img" />
                        </div>
                        <span class="login100-form-title">Welcome to <span style="color:#f36d32">Payroll</span>
                        </span>
                        <div class="wrap-input100 validate-input" data-validate="Valid email is required: ex@abc.xyz">
                            <asp:TextBox Type="text" autocomplete="off" class="form-control input100" runat="server" ID="txtUserName" minlength="4" name="UserName" placeholder="Name" required="" value="" ></asp:TextBox>

                            <span class="focus-input100" style="color: #4c3626"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-user" aria-hidden="true" style="color: #2175b5"></i>
                            </span>
                        </div>
                        <div class="wrap-input100 validate-input" data-validate="Password is required">
                            <asp:TextBox class="form-control input100" ID="txtPassword" autocomplete="off" runat="server" name="UserPassword" placeholder="Password" required="" type="password" ></asp:TextBox>

                            <span class="focus-input100" style="color: #4c3626"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-lock" id="lock11" aria-hidden="true" style="color: #2175b5"></i>
                            </span>
                        </div>
                        <div class="container-login100-form-btn">
                            <asp:Button ID="Login" runat="server" class="login100-form-btn" Text="Sign In" OnClick="Login_Click" Style="background-color: #2175b5;"></asp:Button>
                            <button type="button" class="login100-form-btn mt-20" data-toggle="modal" data-target="#myModal" style="background-color: #6a5958; display:none !important">Sign Up</button>
                        </div>

                        <div class="group">
                            <asp:Label ID="lblErrorMsg" runat="server" Text="Label" Visible="false" CssClass="alert-danger"></asp:Label>
                        </div>

                        <div class="container-login100-form-btn red">
                        </div>
                        <div class="text-center p-t-12">
                            <span class="txt1">Forgot
                            </span>
                            <a class="txt2" href="#">Username / Password?
                            </a>
                        </div>
                        <div class="text-center">
                            <a class="txt2" href="#">Create your Account
                                <i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="text-center mobile1" style="margin-top: -40px;position: relative;"><strong>Copyright © 2017-2019 <a href="https://www.ainfinance.com/" target="_blank">Ain Al-aseel consultancy llc.</a></strong> All rights reserved.</div>
        </div>


               <%--resetpassword--%>
 <%--<div class="login100-form validate-form " id="resetpassword" style="display: none;">
     <div class="login100-pic1 js-tilt" data-tilt="">

       <div id="divPassword1">
         
           <div class="form-group">
               <h3 class="fs-18 fw-500 mb-10">New Password</h3>
               <input type="password" class="form-control" runat="server" id="Password1" placeholder="Password" />
           </div>
           <div class="form-group">
               <h3 class="fs-18 fw-500 mb-10">Confirm Password</h3>
               <input type="password" class="form-control" runat="server" id="Password2" placeholder="Confirm Password" />
               <span id="lblPasswordMatch1" style="display: none;"><small style="color: red" >Password didn't match</small></span>
           </div>
       </div>
   
   <div class="modal-footer">
      <asp:Button ID="btnSubmit1" runat="server" Text="Confirm" OnClick="saveButton_Click" Style="background-color: #806e44;"></asp:Button>
    <%--   <a id="btnSubmit1-" class="btn btn-primary">Save</a>--%>
   
       

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Verify Your Email Address</h4>
                    </div>
                    <div class="modal-body">

                        <div class="alert alert-success mt-20" role="alert" id="lblSuccess">
                            <strong>Success !</strong> Your email was found in our database
                        </div>
                        <div class="alert alert-info mt-20" role="alert" id="lblPasswordSet">
                            <strong>Success !</strong> Your email was found in our database and your password is already been set, please contact administrator
                        </div>
                        <div class="alert alert-danger mt-20" role="alert" id="lblError">
                            <strong>Error !</strong> Something went wrong, please contact administrator
                        </div>

                        <div id="divEmail">
                            <div class="form-group">
                                <h3 class="fs-18 fw-500 mb-10">Email Address</h3>
                                <input type="email" id="txtEmailNew" class="form-control" placeholder="Enter Your Email Address" />
                            </div>
                            <a id="btnVerify" class="btn btn-success mt-10">Verify</a>
                        </div>
                        <div id="divPassword">
                            <div class="form-group">
                                <h3 class="fs-18 fw-500 mb-10">Your Username</h3>
                                <input type="text" class="form-control" runat="server" id="txtUsernameNew" placeholder="Enter Your Username" onblur="checkUsername()" />
                                <span id="lblUsername"><small style="color: red">This username is already exist</small></span>
                            </div>
                            <div class="form-group">
                                <h3 class="fs-18 fw-500 mb-10">Enter Your Password</h3>
                                <input type="password" class="form-control" runat="server" id="txtPasswordNew" placeholder="Password" />
                            </div>
                            <div class="form-group">
                                <h3 class="fs-18 fw-500 mb-10">Confirm Password</h3>
                                <input type="password" class="form-control" runat="server" id="txtPasswordConfirm" placeholder="Confirm Password" />
                                <span id="lblPasswordMatch"><small style="color: red" >Password didn't match</small></span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a id="btnSubmit" class="btn btn-primary">Submit</a>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!----Close Bootstrap Modal ----->

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/tilt.jquery.min.js"></script>
    <script src="js/main.js"></script>
    <script>
        //Toggle Password
        const togglePassword = document.querySelector('#lock11');
        const password111 = document.querySelector('#txtPassword');
        togglePassword.addEventListener('click', () => {
            // Toggle the type attribute using
            // getAttribure() method
            const type1 = password111.getAttribute('type') === 'password' ? 'text' : 'password';
            password111.setAttribute('type', type1);
            // Toggle the eye and bi-eye icon
            //togglePassword.classList.toggle('bi-eye');
        });

        $(window).load()
        {
            //debugger
            var URL = "Promotion_SpCall.aspx";
            $.ajax({
                type: 'POST',
                url: URL,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                //data: "{'Username':'" + username + "'}",
                success: function () {
                    // alert("Check");

                },
                error: function () {
                    //   alert("Check");
                }
            });
        }
        $(document).ready(function () {

            $('#lblSuccess').hide();
            $('#lblError').hide();
            $('#divPassword').hide();
            $('#lblPasswordMatch').hide();
            $('#btnSubmit').hide();
            $('#lblPasswordSet').hide();
            $('#lblUsername').hide();
            $('#btnSubmit').hide();
        });

        $('.js-tilt').tilt({
            scale: 1.1
        });

        $('#btnVerify').click(function () {
            var email = $('#txtEmailNew').val();
            if (email == null || email == 'undefined' || email == '') {
                return false;
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: "login.aspx/CheckEmail",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'Email':'" + email + "'}",
                    success: function (result) {
                        var data = JSON.parse(result.d);

                        if (data.PasswordSet == true && data.Status == false) {
                            passwordSet();
                        }
                        else if (data.Status == true && data.PasswordSet == false) {
                            emailFound();
                            $('#txtUsernameNew').val(data.Username);
                        }
                        else {
                            notFound();
                        }
                    },
                    error: function (result) {

                    }
                });
            }
        });

        $('#btnSubmit').click(function () {
            var email = $('#txtEmailNew').val();
            var username = $('#txtUsernameNew').val();
            var password1 = $('#txtPasswordNew').val();
            var password2 = $('#txtPasswordConfirm').val();
            if (password1 != password2) {
                $('#lblPasswordMatch').show();
                return false;
            }
            else {
                $('#lblPasswordMatch').hide();
                $.ajax({
                    type: 'POST',
                    url: "login.aspx/SetPassword",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'Email':'" + email + "', 'Password':'" + password1 + "', 'Username' : '" + username + "'}",
                    // data: "{'Email':'" + email + "'}",
                    //data: "{'Email':'" + email + ", Password:''"+password1+"'}",
                    success: function (result) {

                        window.location = "/login.aspx";

                    },
                    error: function (result) {
                    }
                });
            }

        });

        function checkUsername() {
            var userName = $('#txtUsernameNew').val();
            $.ajax({
                type: 'POST',
                url: "login.aspx/CheckUsername",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'Username':'" + userName + "'}",
                success: function (result) {
                    if (result.d) {
                        $('#btnSubmit').show();
                        $('#lblUsername').hide();

                    }
                    else {
                        $('#btnSubmit').hide();
                        $('#lblUsername').show();

                    }
                },
                error: function (result) {

                }
            });

        }

        function emailFound() {
            $('#lblSuccess').show();
            $('#divPassword').show();

            $('#lblError').hide();
            $('#divEmail').hide();
            $('#lblPasswordSet').hide();
        }
        function notFound() {
            $('#lblError').show();
            $('#divEmail').show();

            $('#lblSuccess').hide();
            $('#divPassword').hide();

            $('#lblPasswordSet').hide();
        }
        function passwordSet() {
            $('#lblPasswordSet').show();

            $('#divEmail').hide();
            $('#lblError').hide();

            $('#lblSuccess').hide();
            $('#divPassword').hide();
            $('#btnSubmit').hide();
        }
    </script>
    <!---Close Script---->
    <!---Close Login Css---->

    <%--onload="myFunction()"--%>
    <%--<hgroup>
        <h1>Pay Roll</h1>
        <h3>Login here</h3>
    </hgroup>
    <form runat="server">
        <div class="group">
            <asp:TextBox ID="txtUserName" autocomplete="off" CssClass="used" runat="server" /><span class="highlight"></span><span class="bar"></span>
            <label>UserName</label>
        </div>
        <div class="group">
            <asp:TextBox ID="txtPassword" autocomplete="off" CssClass="used" runat="server" type="Password" /><span class="highlight"></span><span class="bar"></span>
            <label>Password</label>
        </div>
        <div class="group">
            <asp:Label ID="lblErrorMsg" runat="server" Text="Label" Visible="false" CssClass="alert-danger"></asp:Label>
        </div>
        <asp:Button ID="Login" runat="server" class="button buttonBlue" Text="Sign In" OnClick="Login_Click">
            <%--<div class="ripples buttonRipples"><span class="ripplesCircle"></span></div>--%>
    <%--   </asp:Button>
    </form>
    <footer>
        <a href="#" target="_blank">
            <img src="images/logo.png" /></a>
        <p>&#0169; Copyrights Reserved 2015 - 2016</p>
    </footer>
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="js/loginjquery.min.js"></script>
    <script src="js/Common.js"></script>--%>
</body>
</html>