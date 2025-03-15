<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="HolidayNew.aspx.cs" Inherits="HolidayNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        <h1>Holiday</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Holiday</li>
        </ol>
    </section>
    <section class="content">

        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Holiday</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" id="pnlDetail">
                                Designation
                                <br/>
                                <asp:DropDownList ID="DdlDesignation" runat="server" CssClass="form-control" ClientIDMode="Static" style="width:250px;">
                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            
                        </div>
                        <br />
                        <div id="scheduler"></div>
                         <div class="option">
                <div id="deleteButton"></div>
            </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <link href="assets/select2-4.0.12/dist/css/select2.min.css" rel="stylesheet" />
    <script src="assets/select2-4.0.12/dist/js/select2.min.js"></script>
    <script>
       
    
        $(document).ready(function () {


          

            $("#DdlDesignation").select2(); //select2({ dropdownParent: "#ContentPlaceHolder1_pnlDetail" });

            var data2 = null;
            var designationID = 0;
            /* designationID = $(this).val();*/
            var data = [];
            var loading = $(".loading");
            loading.show();

            debugger
            $.ajax({
                type: 'POST',
                url: "HolidayNew.aspx/getData",
                contentType: 'application/json; charset=utf-8',
                data: "{'DesignationID':'" + designationID + "'}",
                dataType: 'json',
                success: function (result) {
                    debugger
                    var response = JSON.parse(result.d);

                    loading.hide();
                    if (result.d == "[]") {
                        data.push(null);
                        console.log(data,"data")
                        $("#scheduler").dxScheduler({
                            dataSource: data,
                            views: ["month"],
                            currentView: "month",
                            currentDate: new Date(),

                            editing: { allowUpdating: true },
                            onAppointmentFormCreated: function (e) {

                                var form1 = e.form;
                                //form1.itemOption("text", {
                                //    validationRules: [{
                                //        type: "required",
                                //        message: "Subject required"
                                //    }]
                                //});
                                if (form1.getEditor("text")) {
                                    return;
                                }

                                form1.itemOption("text", {
                                    validationRules: [{
                                        type: "required",
                                        message: "Subject required"
                                    }]
                                });
                                form1.itemOption("allDay", {
                                    validationRules: []
                                });
                                var editor = e.form.getEditor('recurrenceRule');

                                //var x = e.form.getElementsByClassName("dx-recurrence-radiogroup-freq");

                                //x[0].children[1].children[2].style = "display:none;"
                                //x[0].children[1].children[3].style = "display:none;"
                                // editor._$container.find(".dx-recurrence-repeat-end-field").hide()
                                // editor._$container.find(".dx-radiobutton").children[1].hide()

                                $(".dx-visibility-change-handler").click(function () {
                                    var x = document.getElementsByClassName("dx-recurrence-radiogroup-freq")

                                    x[0].children[1].children[2].style = "display:none;"
                                    x[0].children[1].children[3].style = "display:none;"
                                });

                            },
                            //startDayHour: 9,
                            //endDayHour: 19,

                            onAppointmentUpdated: function (e) {
                                // showToast("Updated", e.appointmentData.text, "info");
                                //console.log(e.appointmentData);
                            },
                            onAppointmentDeleted: function (e) {
                                var str = e.appointmentData.startDate;
                                var rules = e.appointmentData.recurrenceRule;
                                var strdate = convert(str);
                                var day = strdate;//s getDay();
                                deleteData(e.appointmentData.ID, day, rules);
                                //  showToast("Deleted", e.appointmentData.text, "warning");
                                //console.log(e.appointmentData);
                            },
                            onAppointmentAdded: function (e) {
                                insertData(e.appointmentData);
                                // showToast("Added", e.appointmentData.text, "success");
                                //console.log(e.appointmentData);
                            },
                            height: 600
                        }).dxScheduler("instance");
                        console.log(data, "data")

                    }
                    else {
                        loading.hide();
                        $("#scheduler").dxScheduler({
                            dataSource: response,
                            views: ["month"],
                            currentView: "month",
                          
                            currentDate: new Date(),
                            onAppointmentFormCreated: function (e) {
                                console.log("Appointment Form Created", e.appointmentData);
                                var form1 = e.form;
                                //form1.itemOption("text", {
                                //    validationRules: [{
                                //        type: "required",
                                //        message: "Subject required"

                                //    }]
                                //});
                                if (form1.getEditor("text")) {
                                    return;
                                }

                                form1.itemOption("text", {
                                    validationRules: [{
                                        type: "required",
                                        message: "Subject required"
                                    }]
                                });
                                form1.itemOption("allDay", {
                                    validationRules: []
                                });

                                var editor = e.form.getEditor('recurrenceRule');

                                //var x = e.form.getElementsByClassName("dx-recurrence-radiogroup-freq");

                                //x[0].children[1].children[2].style = "display:none;"
                                //x[0].children[1].children[3].style = "display:none;"
                                // editor._$container.find(".dx-recurrence-repeat-end-field").hide()
                                // editor._$container.find(".dx-radiobutton").children[1].hide()

                                $(".dx-visibility-change-handler").click(function () {
                                    var x = document.getElementsByClassName("dx-recurrence-radiogroup-freq")

                                    x[0].children[1].children[2].style = "display:none;"
                                    x[0].children[1].children[3].style = "display:none;"
                                });

                            },
                            editing: { allowUpdating: true },

                            //startDayHour: 9,
                            //endDayHour: 19,
                            onAppointmentAdded: function (e) {
                                // showToast("Added", e.appointmentData.text, "success");
                                //console.log(e.appointmentData);
                            },
                            onAppointmentUpdated: function (e) {
                                // showToast("Updated", e.appointmentData.text, "info");
                                //console.log(e.appointmentData);
                            },
                            onAppointmentDeleted: function (e) {
                                var str = e.appointmentData.startDate;
                                var rules = e.appointmentData.recurrenceRule;
                                var strdate = convert(str);
                                var day = strdate;//s getDay();
                                deleteData(e.appointmentData.ID, day, rules);
                                //  showToast("Deleted", e.appointmentData.text, "warning");
                                //console.log(e.appointmentData);
                            },
                            onAppointmentAdded: function (e) {
                                insertData(e.appointmentData);
                                // showToast("Added", e.appointmentData.text, "success");
                                //console.log(e.appointmentData);
                            },
                            height: 600
                        }).dxScheduler("instance");
                        console.log(response, "data")
                      
                        //$(response).each(function (i, e) {
                        //    e.startDate = new Date(e.startDate);
                        //    e.endDate = new Date(e.endDate);
                        //    data.push(e);

                        //    //var check = JSON.parse(data);

                        //});
                    }

                },
                error: function (result) {

                }
            });

            function showToast(event, value, type) {
                DevExpress.ui.notify(event + " \"" + value + "\"" + " task", type, 800);
            }




            $("#deleteButtons").dxButton({
                text: "Delete",
                onClick: function () {
                    scheduler.deleteAppointment(Appoi);
                }
            });
            $("#allow-adding").dxCheckBox({
                text: "Allow adding",
                value: true,
                onValueChanged: function (data) {
                    scheduler.option("editing.allowAdding", data.value);
                }
            });

            $("#allow-deleting").dxCheckBox({
                text: "Allow deleting",
                value: true,
                onValueChanged: function (data) {
                    scheduler.option("editing.allowDeleting", data.value);
                }
            });
        });

        function insertData(obj)
        {
            var loading = $(".loading");
            loading.show();
            var startDate = new Date(obj.startDate);
            startDate = startDate.format("MM-dd-yyyy");
            var designationID = 0;
            var endDate = new Date(obj.endDate);
            endDate = endDate.format("MM-dd-yyyy");
            var a = $("#<% =DdlDesignation.ClientID %>");
    
            
            
            if (a.val() =="0") {
                a.find("option:first-child").remove();
            }
            $.ajax({
                type: "POST",
                url: "HolidayNew.aspx/Create",
                contentType: "application/json",
                //data: JSON.stringify(Obj),
                data: "{'text':'" + obj.text + "', 'startDate':'" + startDate + "', 'endDate' : '" + endDate + "', 'allDay' : '" + obj.allDay + "', 'recurrenceRule' : '" + obj.recurrenceRule + "', 'repeatOnOff' : '" + obj.repeatOnOff + "','designationID' : '" + designationID + "' }",
                dataType: "json",

                success: function (result) {
                    //console.log(result)
                    if (result.d == "Delete") {
                        window.location.href = "HolidayNew.aspx";
                    }
                    else if (result.d == "Success")
                    {
                        loading.hide();
                        msgbox(1, "", "Successfully Done");
                        setInterval(function () {
                            window.location.href = "HolidayNew.aspx";
                        }, 2000);
                    }
                    else if (result.d == "Failed")
                    {
                        loading.hide();
                        msgbox(4, "", "SomeThing Went wrong");
                        setInterval(function () {
                            window.location.href = "HolidayNew.aspx";
                        }, 2000);
                    }
                    
                    else
                    {
                        return true;
                    }
                   
                },

                error: function (result) {
                    //console.log(result)
                    return false;
                }
            });
        }
        function convert(str) {
            var date = new Date(str),
              mnth = ("0" + (date.getMonth() + 1)).slice(-2),
              day = ("0" + date.getDate()).slice(-2);
            return [mnth, day, date.getFullYear()].join("-");
        }
        function deleteData(obj, date, rules) {
            // var params = ;
            var loading = $(".loading");
            loading.show();
            $.ajax({
                type: "POST",
                url: "HolidayNew.aspx/Delete",
                contentType: "application/json",
                //data: JSON.stringify(Obj),
                data: JSON.stringify({ text: obj, date: date, designationID: designationID, rules: rules }),// "{'text':'" + obj + "'}",
                dataType: "json",

                success: function (result) {
                    //console.log(result)

                    if (result.d == true) {
                        loading.hide();
                        var data = [];
                        data.push(null);
                        $("#scheduler").dxScheduler({
                            dataSource: data,
                            views: ["month"],
                            currentView: "month",
                            currentDate: new Date(),

                            editing: { allowUpdating: true },
                            onAppointmentFormCreated: function (e) {

                                var form1 = e.form;
                                form1.itemOption("text", {
                                    validationRules: [{
                                        type: "required",
                                        message: "Subject required"

                                    }]
                                });
                                form1.itemOption("allDay", {
                                    validationRules: [{
                                        type: "required",
                                        message: "Allday required"


                                    }]
                                });
                                var editor = e.form.getEditor('recurrenceRule');
                                $(".dx-visibility-change-handler").click(function () {
                                    var x = document.getElementsByClassName("dx-recurrence-radiogroup-freq")

                                    x[0].children[1].children[2].style = "display:none;"
                                    x[0].children[1].children[3].style = "display:none;"
                                });

                            },

                        }).dxScheduler("instance");

                        msgbox(1, "", "Successfully Delete");

                        setInterval(function () {
                            window.location.href = "HolidayNew.aspx";
                        }, 2000);
                    }
                    else if (result.d == false) {
                        loading.hide();
                        msgbox(4, "", "Error occured");

                    }
                },

                error: function (result) {
                    //console.log(result)
                    return false;
                }
            });
        }

    </script>
</asp:Content>
