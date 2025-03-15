<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Learning.aspx.cs" Inherits="Learning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
    <asp:TextBox type="text" runat="server" id="asa" ClientIDMode="Static"/>
    <button runat="server" class="aaa">asd</button>
    <asp:Button ID="BtnEmailSender" runat="server" CssClass="btn btn-primary" Text="Send Email" OnClick="BtnEmailSender_Click" />
    <asp:Button ID="aspbutton" runat="server" text="test" ClientIDMode="Static"/>
    <script>
        var app = this.app();
        if (app) {
            console.log('Hello World');
        }
        $("#aspbutton").click(function () {
            debugger;
            var name = $('[id*=asa]').val('');
            if(name == 'as')
            {
                alert("PLEASE FILL");
            }
                
            
        });
        //$(".aaa").click(function () {
        //    alert("Kia Bat ha");
        //    aba();
        //});


        //function aba() {
        //    $('[id*=asa]').val('FSDF');
        //}
    </script>

</asp:Content>

