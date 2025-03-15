<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test1.aspx.cs" Inherits="test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <script src="js/jquery.min.js"></script>   
    <script src="js/jquery-migrate-1.2.1.js"></script> 
    <script src="js/bootstrap.min.js"></script>        
    <script src="js/bootstrap-typeahead.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            PageMethods.getEmpList(OnRequestComplete, OnRequestError);
        });

        function OnRequestComplete(res, userContext, methodName) {
            $("#txtSearch").typeahead({
                source: function (typeahead, query) {
                    typeahead.process($.map(res, function (item) {
                        return {
                            value: item[1],
                            EmpID: item[0]
                        }
                    }));
                },
                minLength: 1,
                property: 'value',
                appendtoSelector: "[id $= pnlDetail]",
                onselect: function (obj) {
                    alert(obj);
                }
            });
        }

        function OnRequestError(error, userContext, methodName) {
            //if (error != null) {
            //    alert(error.get_message());
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:TextBox ID="txtSearch" ClientIDMode="Static" runat="server"></asp:TextBox>
    </form>
</body>
</html>
