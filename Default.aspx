<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" Async="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset='utf-8' />
    <link href='css/fullcalendar.css' rel='stylesheet' />
    <link href='css/fullcalendar.print.css' rel='stylesheet' media='print' />
     <link href="Content/css/select2.min.css" rel="stylesheet" />
    <style>
/*        body {*/
		/*margin: 40px 10px;
		padding: 0;
		font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
		font-size: 14px;
	}*/

        #calendar {
            max-width: 900px;
            margin: 0 auto;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="Scripts/select2.min.js"></script>

    <link href="Content/css/select2.min.css" rel="stylesheet" />--%>

<%--    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=ddlCompany.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

      });

    </script>--%>


    <section class="content-header">
        <h1 class="float-left mt-5">Dashboard</h1>
        <img src="https://www.ainfinance.com/static/media/AinLogo.5ee634958fe0c54c09342d12c95815bd.svg" class="float-right" alt="payroll" style="width: 186px !important;margin-right: 13px;max-height: 69px;height: 51px;"/>
    </section>
    <section class="content pt-0">
        <asp:UpdatePanel ID="updateCompany" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row" id="divCompany" runat="server">
                    <div class="col-md-12">
                    
                               
                                    <label>Company :</label><h3><asp:Label ID="lblcompany" runat="server" Text=""></asp:Label></h3> 
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCompany" runat="server" ClientIDMode="Static" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                                </td>
                                <%--<td style="width: 130px;">
                                    <label>Company :</label></td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" CssClass="form-control input-sm" ClientIDMode="Static" runat="server" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--Please Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Company" runat="server"
                                        ConnectionString="name=vt_EMSEntities"
                                        DefaultContainerName="vt_EMSEntities"
                                        EntitySetName="vt_tbl_Company">
                                    </asp:EntityDataSource>
                                </td>--%>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div class="container-fluid mb-30">
                    <div class="row">
                        <div class="col-md-3 pl-5 pr-5">
                            <div class="cstm-badge">
                                <div class="float-left">
                                    <img src="images/database.png" style="height:33px;width:27px;" />
                                    <i class="" style="background-color: #2a868f;"></i></div>
                                <div class="float-left">
                                    <h1 class="fs-16 mt-5 ml-15 mb-5 color-light-black">Data 1</h1>
                                    <h1 class="fs-25 mt-0 ml-10"> <asp:Label ID="lbltotalemployee" runat="server" Text=""></asp:Label></h1>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3 pl-5 pr-5">
                            <div class="cstm-badge">
                                <div class="float-left">
                                    <img src="images/data-exchange.png" style="height:37px;width:35px;" />
                                    <i class="" style="background-color:#27A9E3;"></i></div>
                                <div class="float-left">
                                    <h1 class="fs-16 mt-5 ml-15 mb-5 color-light-black">Data 2</h1>
                                    <h1 class="fs-25 mt-0 ml-10">   <asp:Label ID="lbldep" runat="server" Text=""></asp:Label></h1>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3 pl-5 pr-5">
                            <div class="cstm-badge">
                                <div class="float-left">
                                    <img src="images/server.png" style="height:37px; width:35px;" />
                                    <i class="" style="background-color:#f36d32 ;"></i></div>
                                <div class="float-left">
                                    <h1 class="fs-16 mt-5 ml-15 mb-5 color-light-black">Data 3</h1>
                                    <h1 class="fs-25 mt-0 ml-10"> <asp:Label ID="lbldesg" runat="server" Text=""></asp:Label></h1>
                                </div>
                            </div>
                        </div>
                           <div class="col-md-3 pl-5 pr-5">
                            <div class="cstm-badge">
                                <div class="float-left">
                                    <img src="images/data-collection.png" style="height:37px;width:35px;" />
                                    <i class="" style="background-color: #28B779;"></i></div>
                                <div class="float-left">
                                    <h1 class="fs-16 mt-5 ml-15 mb-5 color-light-black">Data 4</h1>
                                    <h1 class="fs-25 mt-0 ml-10"> <asp:Label ID="lblattendance" runat="server" Text=""></asp:Label></h1>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row dashboard_row" style="">
                    <div class="col-md-3 col-sm-6 col-xs-12 custom_col">
                        <div class="box_1">
                            <div class="visual">
                                <img src="images/database(black).png" class="custom_dash_icon" />
                                
                            </div>
                            <div class="dash_content">
                                <span class=""><span>Employees : </span>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 custom_col">
                        <div class="box_2">
                            <div class="visual">
                                <img src="images/calendar-(6)[1].png" class="custom_dash_icon1" />
                                

                            </div>
                            <div class="dash_content">
                                <span class=""><span>Departments : </span>
                                 </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 custom_col">
                        <div class="box_3">
                            <div class="visual">
                                <img src="images/hierarchical.png" class="custom_dash_icon2"/>
                                

                            </div>
                            <div class="dash_content">
                                <span>Designation : </span>
                              
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 custom_col">
                        <div class="box_4">
                            <div class="visual">
                                <img src="images/user-check.png" class="custom_dash_icon3" />

                            </div>
                            <div class="dash_content">
                                <span>Attendance : </span>
                                
                            </div>
                        </div>
                    </div>
                </div>

                <div class="container" id="SpnNotification" runat="server" visible="false">
                    <div class="row">
                        <div class="alert alert-warning alert-dismissible">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close" onclick="Update_Notification()">&times;</a>
                            <strong>Dear <asp:Label ID="LblEmpName" runat="server"></asp:Label><asp:HiddenField ID="HdnEmployeeID" runat="server" /> !</strong> your salary has been generated for the month of <asp:Label ID="LblDate" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="row ">
                    <div class="col-md-12">
                        <div id='calendar'></div>
                    </div>
                </div>
                <div class="row" >
                    <div class="col-md-1">
                        <asp:TreeView ID="treeDesignation" runat="server" ShowLines="true" AutoGenerateDataBindings="false"></asp:TreeView>
                    </div>
                </div>
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>


    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script src="assets/js/bootstrap-switch.min.js"></script>
    <script src="Scripts/select2.min.js"></script>
    <script src='js/fullcalendar.min.js'></script>


    

   
    <script>

        function Update_Notification() {
            var employeeID = $('#<%=HdnEmployeeID.ClientID%>').val();
            $.ajax({
                type: "POST",
                url: "Default.aspx/Update_SalaryNotification",
                data: "{'EmployeeID':" + employeeID + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                }
            });
        }

        function BindCalender() {
            var id = 0;
            if ($("#ddlCompany").length > 0) {
                id = $("#ddlCompany").val();
            }
            var today = new Date();
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetHoliday",
                data: "{'id':" + id + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#calendar').fullCalendar({
                        header: {
                            left: 'prev,next today',
                            center: 'title',
                            right: 'month,basicWeek,basicDay'
                        },

                        defaultDate: today,
                        //editable: true,
                        eventLimit: true, // allow "more" link when too many events{
                        events: data.d
                    });
                }
            });
        }

        
        $(document).ready(function () {

            $("#<%=ddlCompany.ClientID%>").select2({

              placeholder: "Select Item",

              allowClear: true

          });

      });

        $(document).ready(function () {
            //BindCalender();
            //    $('#calendar').fullCalendar({
            //        header: {
            //            left: 'prev,next today',
            //            center: 'title',
            //            right: 'month,basicWeek,basicDay'
            //        },

            //        defaultDate: today,
            //        editable: true,
            //        eventLimit: true, // allow "more" link when too many events
            //        events: [
            //			{
            //			    title: 'All Day Event',
            //			    start: '2015-11-01'
            //			},
            //			{
            //			    title: 'Long Event',
            //			    start: '2015-02-07',
            //			    end: '2015-11-10'
            //			},
            //			{
            //			    id: 999,
            //			    title: 'Repeating Event',
            //			    start: '2015-10-09T16:00:00',
            //			    end: '2015-10-10'
            //			},
            //			{
            //			    id: 999,
            //			    title: 'Repeating Event',
            //			    start: '2015-11-16T16:00:00'
            //			},
            //			{
            //			    title: 'Conference',
            //			    start: '2015-02-11',
            //			    end: '2015-11-13'
            //			},
            //			{
            //			    title: 'Meeting',
            //			    start: '2015-02-12T10:30:00',
            //			    end: '2015-11-12T12:30:00'
            //			},
            //			{
            //			    title: 'Lunch',
            //			    start: '2015-11-12T12:00:00'
            //			},
            //			{
            //			    title: 'Meeting',
            //			    start: '2015-02-12T14:30:00'
            //			},
            //			{
            //			    title: 'Happy Hour',
            //			    start: '2015-02-12T17:30:00'
            //			},
            //			{
            //			    title: 'Dinner',
            //			    start: '2015-02-12T20:00:00'
            //			},
            //			{
            //			    title: 'Birthday Party',
            //			    start: '2015-02-13T07:00:00'
            //			},
            //			{
            //			    title: 'Click for Google',
            //			    url: 'http://google.com/',
            //			    start: '2015-02-28'
            //			}
            //        ]
            //    });
        });
    </script>
</asp:Content>