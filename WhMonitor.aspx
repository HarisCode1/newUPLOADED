<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="WhMonitor.aspx.cs" Inherits="WhMonitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='css/fullcalendar.css' rel='stylesheet' />
    <link href='css/fullcalendar.print.css' rel='stylesheet' media='print' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <h1>Employee Overtime</h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Employee.aspx">Employees</a></li>
            <li class="active">Working Hour Monitor</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Working Hour Monitor</h3>
                       
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id='calendar'></div>
                                    <div id="chart" style="display:none"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script src="js/moment.min.js"></script>
    <script src='js/fullcalendar.min.js'></script>
    <script>

        var dataSource = [{
            day: '1',
            temperature: 57
        }, {
            day: '2',
            temperature: 58
        }, {
            day: '3',
            temperature: 57
        }, {
            day: '4',
            temperature: 59
        }, {
            day: '5',
            temperature: 63
        }, {
            day: '6',
            temperature: 63
        }, {
            day: '7',
            temperature: 63
        }, {
            day: '8',
            temperature: 64
        }, {
            day: '9',
            temperature: 64
        }, {
            day: '10',
            temperature: 64
        }, {
            day: '11',
            temperature: 69
        }, {
            day: '12',
            temperature: 72
        }, {
            day: '13',
            temperature: 75
        }, {
            day: '14',
            temperature: 78
        }, {
            day: '15',
            temperature: 76
        }, {
            day: '16',
            temperature: 70
        }, {
            day: '17',
            temperature: 65
        }, {
            day: '18',
            temperature: 65
        }, {
            day: '19',
            temperature: 68
        }, {
            day: '20',
            temperature: 70
        }, {
            day: '21',
            temperature: 73
        }, {
            day: '22',
            temperature: 73
        }, {
            day: '23',
            temperature: 75
        }, {
            day: '24',
            temperature: 78
        }, {
            day: '25',
            temperature: 76
        }, {
            day: '26',
            temperature: 76
        }, {
            day: '27',
            temperature: 80
        }, {
            day: '28',
            temperature: 76
        }, {
            day: '29',
            temperature: 75
        }, {
            day: '30',
            temperature: 75
        }, {
            day: '31',
            temperature: 74
        }];

        $(document).ready(function () {
            bind();
            bindChart();
        });

        function bind() {
            var id = '<%= Convert.ToInt32(Request.QueryString["ID"]) %>';
            var today = new Date();
            $.ajax({
                type: "POST",
                url: "WhMonitor.aspx/getAttendance",
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
                        timeFormat: 'H(:mm)', // uppercase H for 24-hour clock、
                        displayEventTime: false,
                        defaultDate: today,
                        //editable: true,
                        eventLimit: true, // allow "more" link when too many events{
                        events: data.d
                    });
                }
            });
        }

        function bindChart() {
            var highAverage = 77,
                lowAverage = 58;

            $("#chart").dxChart({
                dataSource: dataSource,
                customizePoint: function () {
                    if (this.value > highAverage) {
                        return { color: "#ff7c7c", hoverStyle: { color: "#ff7c7c" } };
                    } else if (this.value < lowAverage) {
                        return { color: "#8c8cff", hoverStyle: { color: "#8c8cff" } };
                    }
                },
                customizeLabel: function () {
                    if (this.value > highAverage) {
                        return {
                            visible: true,
                            backgroundColor: "#ff7c7c",
                            customizeText: function () {
                                return this.valueText + "&#176F";
                            }
                        };
                    }
                },
                "export": {
                    enabled: true
                },
                valueAxis: {
                    visualRange: {
                        startValue: 40
                    },
                    maxValueMargin: 0.01,
                    label: {
                        customizeText: function () {
                            return this.valueText + "&#176F";
                        }
                    },
                    constantLines: [{
                        label: {
                            text: "Low Average"
                        },
                        width: 2,
                        value: lowAverage,
                        color: "#8c8cff",
                        dashStyle: "dash"
                    }, {
                        label: {
                            text: "High Average"
                        },
                        width: 2,
                        value: highAverage,
                        color: "#ff7c7c",
                        dashStyle: "dash"
                    }]
                },
                series: [{
                    argumentField: "day",
                    valueField: "temperature",
                    type: "bar",
                    color: "#e7d19a"
                }],
                title: {
                    text: "Daily Temperature in May"
                },
                legend: {
                    visible: false
                }
            });
        }
    </script>
</asp:Content>