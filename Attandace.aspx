<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Attandace.aspx.cs" Inherits="Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(function () {
            bindLoadData();
        });

        function bindLoadData() {
            if ($("#chkSecondWeeklyOff").prop("checked"))
                $(".SecondWeeklyOff").show();

            $("#chkSecondWeeklyOff").change(function () {
                $(".SecondWeeklyOff").toggle('slow');
            });

            if ($("#chkLateCut").prop("checked"))
                $(".LateCut").show();

            $("#chkLateCut").change(function () {
                $(".LateCut").toggle();
            });

            if ($("#chkOT").prop("checked"))
                $(".OverTime").show();

            $("#chkOT").change(function () {
                $(".OverTime").toggle();
            });

            if ($("[id$=rdopunch] input[type=radio]:checked").val() != "Single")
                $("#NoOutPunchFound,#TolleranceBetweenTwoPunches").show();

            $("[id$=rdopunch] input[type=radio]").change(function () {
                if (this.value == "Single") {
                    $("#NoOutPunchFound,#TolleranceBetweenTwoPunches").hide();
                }
                else
                    $("#NoOutPunchFound,#TolleranceBetweenTwoPunches").show();
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading">
        ATTENDANCE
    </div>
    <div class="row" id="divCompany" runat="server">
        <div class="col-md-6">
            <asp:UpdatePanel ID="updateCompany" runat="server">
                <ContentTemplate>
                    <table>
                        <tr id="trCompany" runat="server">
                            <td style="width: 130px;">
                                <label>Company :</label></td>
                            <td>
                                <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" CssClass="form-control" runat="server" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' customFn='var r = parseInt(this.value); return r > 0;' OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                </asp:DropDownList>
                                <asp:EntityDataSource ID="EDS_Company" runat="server"
                                    ConnectionString="name=vt_EMSEntities"
                                    DefaultContainerName="vt_EMSEntities"
                                    EntitySetName="vt_tbl_Company">
                                </asp:EntityDataSource>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="UpdateModel">
        <asp:UpdatePanel ID="Upview" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="pnlDetail" runat="server">
                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">

                            <fieldset>
                                <legend>Shift Details
                                </legend>
                            </fieldset>
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 160px;">
                                        <label>Full Day Mins :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtFullDayMins" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Half Day Mins :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtHalfDayMins" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Late Allowed Min :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtLateAllowedMin" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Early Allowed Min :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtEarlyAllowdMin" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-5">
                            <fieldset>
                                <legend>Weekly Off
                                </legend>
                            </fieldset>
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 100px;">
                                        <label>Weekly Off :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlWeeklyOff" runat="server">
                                            <asp:ListItem>Sunday</asp:ListItem>
                                            <asp:ListItem>Monday</asp:ListItem>
                                            <asp:ListItem>Tuesday</asp:ListItem>
                                            <asp:ListItem>Wednesday</asp:ListItem>
                                            <asp:ListItem>Thursday</asp:ListItem>
                                            <asp:ListItem>Friday</asp:ListItem>
                                            <asp:ListItem>Saturday</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">&nbsp;</td>
                                    <td>
                                        <asp:CheckBox ClientIDMode="Static" ID="chkSecondWeeklyOff" runat="server" Text="SecondWeeklyoff" CssClass="Shift-det-radiobtn" />
                                    </td>
                                </tr>
                                <tr class="SecondWeeklyOff" style="display: none;">
                                    <td style="width: 100px;">
                                        <label>Weekly Off :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlSecondWeeklyOff" runat="server">
                                            <asp:ListItem>Sunday</asp:ListItem>
                                            <asp:ListItem>Monday</asp:ListItem>
                                            <asp:ListItem>Tuesday</asp:ListItem>
                                            <asp:ListItem>Wednesday</asp:ListItem>
                                            <asp:ListItem>Thursday</asp:ListItem>
                                            <asp:ListItem>Friday</asp:ListItem>
                                            <asp:ListItem>Saturday</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="SecondWeeklyOff" style="display: none;">
                                    <td style="width: 100px;">
                                        <label>Satus :</label></td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoSecondWeeklyOffStatus" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>Full Day</asp:ListItem>
                                            <asp:ListItem>Half Day</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="SecondWeeklyOff" style="display: none;">
                                    <td style="width: 100px;">
                                        <label>Weeks :</label></td>
                                    <td>
                                        <asp:CheckBoxList ID="chkSecondWeeklyOffPattern" runat="server" RepeatDirection="Horizontal" CellPadding="5">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-offset-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <fieldset>
                                <legend>Punch
                                </legend>
                                <asp:RadioButtonList ID="rdopunch" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">Single</asp:ListItem>
                                    <asp:ListItem>Double</asp:ListItem>
                                    <asp:ListItem>Multiple</asp:ListItem>
                                </asp:RadioButtonList>
                            </fieldset>
                        </div>
                        <div class="col-md-5" id="TolleranceBetweenTwoPunches" style="display: none;">
                            <fieldset>
                                <legend>Tollerance Between two Punches
                                </legend>
                            </fieldset>
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 160px;">
                                        <label>Tollerance Minutes :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtTolleranceMin" validate='vgroup' require='Please Enter Minutes' runat="server" Width="50"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-offset-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-10 col-md-offset-1">
                            <fieldset>
                                <legend>Late Cut
                                </legend>
                                <table>
                                    <tr>
                                        <td style="width: 100px;">
                                            <asp:CheckBox ID="chkLateCut" ClientIDMode="Static" runat="server" CssClass="puch-radiobtn" Text="Allowed" />
                                        </td>
                                        <td class="LateCut" style="display: none; width: 100px;">
                                            <label>Allowed Days :</label>
                                        </td>
                                        <td class="LateCut" style="width: 100px; display: none; width: 85px;">
                                            <asp:TextBox ID="txtLateCutAllowedDays" runat="server" Width="50"></asp:TextBox>
                                        </td>
                                        <td class="LateCut" style="display: none; width: 110px;">
                                            <label>Actions :</label>
                                        </td>
                                        <td class="LateCut" style="display: none;">
                                            <asp:DropDownList ID="ddlLateCutActions" runat="server">
                                                <asp:ListItem>None</asp:ListItem>
                                                <asp:ListItem>HalfDay</asp:ListItem>
                                                <asp:ListItem>Absent</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <div class="col-md-offset-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <fieldset>
                                <legend>OT
                                </legend>
                            </fieldset>
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:CheckBox ID="chkOT" ClientIDMode="Static" runat="server" CssClass="puch-radiobtn" Text="Allowed" />
                                    </td>
                                    <td class="OverTime" style="display: none; width: 100px;">
                                        <label>Grace Period :</label>
                                    </td>
                                    <td class="OverTime" style="display: none; width: 110px;">
                                        <asp:TextBox ID="txtOtGracePeriod" runat="server" CssClass="ot-G-Period" Width="50"></asp:TextBox>
                                    </td>
                                    <td class="OverTime" style="display: none; width: 80px;">
                                        <label>1 Day Hrs :</label>
                                    </td>
                                    <td class="OverTime" style="display: none;">
                                        <asp:TextBox ID="txtOt1DayHrs" runat="server" Width="50"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-5" id="NoOutPunchFound" style="display: none;">
                            <fieldset>
                                <legend>No Out Punch Found
                                </legend>
                            </fieldset>
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 100px;">
                                        <label>Action :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlNoOutPunchFound" runat="server">
                                            <asp:ListItem>None</asp:ListItem>
                                            <asp:ListItem>ShiftTimeOut</asp:ListItem>
                                            <asp:ListItem>HalfDay</asp:ListItem>
                                            <asp:ListItem>Absent</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-offset-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <fieldset>
                                <legend>Lunch
                                </legend>
                            </fieldset>
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:CheckBox ID="chkIsLunch" runat="server" CssClass="puch-radiobtn" Text="Allowed" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-5">
                            <fieldset>
                                <legend>If Presrent on WOFF or HOFF 
                                </legend>
                            </fieldset>
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 100px;">
                                        <label>Action :</label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlWOFFAction" runat="server">
                                            <asp:ListItem>None</asp:ListItem>
                                            <asp:ListItem>COFF</asp:ListItem>
                                            <asp:ListItem>OT</asp:ListItem>
                                            <asp:ListItem>AddInPayDays</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-offset-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-md-offset-1">
                            <table class="Shift-det">
                                <tr>
                                    <td style="width: 130px;">
                                        <label>Maximum OT Hrs :</label></td>
                                    <td>
                                        <asp:TextBox ID="txtMaxOtHrs" runat="server" Width="50"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10 col-md-offset-1 ">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary pull-right" OnClick="btnSave_Click" />
                    </div>
                    <div class="col-md-offset-1"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>