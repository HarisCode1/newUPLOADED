<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="DesignationEdit.aspx.cs" Inherits="DesignationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>Designation </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Designation - Edit</li>
        </ol>
    </section>
    <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDetail" runat="server">
                <div class="container-fluid">
                    <div class="box box-info">

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h3 class="text-center"><b>Designation -</b> Edit</h3>
                                    <hr />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">
                                        Department :</label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label for="email">Type Of Employee :</label>
                                    <asp:DropDownList ID="DdlTypeOfEmployees" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%">
                                        <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4" id="topdesignaton" runat="server">
                                    <label for="email" >Top Designation:</label>
                                    <asp:DropDownList ID="ddlDesignation" ClientIDMode="Static" runat="server" CssClass="form-control" visible="true">
                                    </asp:DropDownList>
                                </div>
                              
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="email">Designation :</label>
                                    <asp:TextBox ID="txtDesignationid" runat="server" CssClass="form-control" validate='vgroup' require="Please select your department"></asp:TextBox></td>

                                </div>
                                  <div class="col-md-4" id="reportto" runat="server">
                                    <label for="email">Report To:</label>
                                    <asp:DropDownList ID="ddltopdesignation" ClientIDMode="Static" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                  </div>
                                  <div class="col-md-4" id="Div1" runat="server">
                                    <label for="email">Is Line Manager:</label>
                                     <asp:CheckBox ID="ChkIsLineManager"  runat="server"></asp:CheckBox></td>
                                </div>

                            </div>
                            

                            <hr />



                        </div>

                        <div class="row" style="margin-top: 15px !important;">
                            <div class="col-md-12">
                                <span class="pull-right">
                                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click" />
                                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn" Text="Cancel" OnClick="BtnCancel_Click" CausesValidation="false" />
                                </span>
                            </div>
                        </div>
                        <hr />
                    </div>
                </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

