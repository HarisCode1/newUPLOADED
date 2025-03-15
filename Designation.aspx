<%@ Page Title="" Language="C#" MasterPageFile="~/NewMain.master" AutoEventWireup="true" CodeFile="Designation.aspx.cs" Inherits="Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
          <h4>Company : <asp:Label ID="lblcompany" runat="server" Text=""></asp:Label></h4>  
        <h1>Designation </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li><a href="Administration.aspx"><i class="fa fa-pie-chart"></i>Administration</a></li>
            <li class="active">Designation</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-12 " style="padding-bottom: 20px;">
                <asp:UpdatePanel runat="server" ID="UpAddNew">
                    <ContentTemplate>
                        <%--<asp:LinkButton ID="lbtnAddRole" data-toggle="modal" data-target="#myModalBranch" class="btn btn-primary btn-lg btn-block admin_btn" runat="server" Text="Add New" CssClass="btn btn-primary pull-right" ></asp:LinkButton>--%>
                        <asp:Button ID="BtnAddNew" runat="server" CssClass=" pull-right btn btn-payroll" Text="Add New" OnClick="btnAddNew_OnClick" CausesValidation="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="modal fade" id="ModalDesignation" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Designation Add/Edit</h4>
                            </div>
                            <div id="pnlDetail" runat="server" class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table>
                                            <div runat="server" visible="false">
                                                <tr id="trCompany" runat="server">
                                                    <td style="width: 120px;">
                                                        <label>Company :</label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCompany" ClientIDMode="Static" runat="server" CssClass="form-control input-sm" DataSourceID="EDS_Company" DataTextField="CompanyName" DataValueField="CompanyID" validate='vgroup' require='Please select company' AppendDataBoundItems="true">
                                                            <asp:ListItem Value="" Text="Please Select Company"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:EntityDataSource ID="EDS_Company" runat="server"
                                                            ConnectionString="name=vt_EMSEntities"
                                                            DefaultContainerName="vt_EMSEntities"
                                                            EntitySetName="vt_tbl_Company">
                                                        </asp:EntityDataSource>
                                                     
                                                    </td>
                                                </tr>
                                            </div>
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Head Deparment:</label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                        runat="server" ControlToValidate="ddlheaddeparment"
                                                        ErrorMessage="Required" ForeColor="Red"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:DropDownList ID="ddlheaddeparment" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlheaddeparment_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <%--<asp:DropDownList ID="ddlDepartment" runat="server" validate='vgroup' require="Please select your department">
                                                        <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Department :</label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="ddlDepartment1"
                                                        runat="server" ControlToValidate="ddlDepartment"
                                                        ErrorMessage="Required" ForeColor="Red"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:DropDownList ID="ddlDepartment" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <%--<asp:DropDownList ID="ddlDepartment" runat="server" validate='vgroup' require="Please select your department">
                                                        <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td style="width: 120px;">
                                                    <label>Type Of Employee :</label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="DdlTypeOfEmployees1"
                                                        runat="server" ControlToValidate="DdlTypeOfEmployees"
                                                        ErrorMessage="Required" ForeColor="Red"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="DdlTypeOfEmployees" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:DropDownList ID="ddlDepartment" runat="server" validate='vgroup' require="Please select your department">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                            
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Top Designation:</label>
                                                </td>
                                                <td>
                                                    <%--<asp:RequiredFieldValidator ID="ddlDesignation1"
                                                        runat="server" ControlToValidate="ddlDesignation"
                                                        ErrorMessage="Required" ForeColor="Red"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <br />
                                                    <asp:DropDownList   ID="ddlDesignation" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                      <asp:DropDownList ID="ddltopdesignations" ClientIDMode="Static" runat="server" CssClass="form-control" visible="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Designation :</label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="txtDesignationid1"
                                                        runat="server" ControlToValidate="txtDesignationid"
                                                        ErrorMessage="Required" ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDesignationid" runat="server" CssClass="form-control" ></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 120px;">
                                                    <label>Is Line Manager:</label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ChkIsLineManager"  runat="server"></asp:CheckBox></td>
                                            </tr>
                                            <%--<div runat="server" id="DivPerc" Visible="false">
                                                <tr>
                                                <td style="width: 120px;">
                                                    <label>Percentage :</label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" validate='vgroup' require="Please select your department"></asp:TextBox></td>
                                              </tr>
                                              </div>--%>
                                            <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox> </tr>--%>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSaveCont"   runat="server" class="btn btn-payroll" Text="Save & Continue" OnClick="btnSaveCont_Click" visible="false" />
                                <%--<asp:Button ID="btnSave" runat="server" CssClass="btn btn-payroll" Text="Save" OnClick="btnSave_Click" OnClientClick="if(validate('vgroup')){return true;}else{return false;}" />--%>
                                <asp:Button ID="btnSave" ClientIDMode="Static" runat="server" CssClass="btn btn-payroll" Text="Save" OnClick="btnSave_Click"/>

                                <asp:Button ID="btnClose" runat="server" CssClass="btn cancel-btn" Text="Close" OnClick="btnClose_Click" CausesValidation="false"/>
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="row" id="divCompany" runat="server">
            <div class="col-md-6">

                <asp:UpdatePanel ID="updateCompany" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr id="trGridCompany" runat="server">
                                <td style="width: 130px;">
                                    <label>Company :</label></td>
                                <td>
                                    <asp:DropDownList ID="ddlComp" ClientIDMode="Static" runat="server" DataSourceID="EDS_Comp" DataTextField="CompanyName" DataValueField="CompanyID" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:EntityDataSource ID="EDS_Comp" runat="server"
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
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info custom_input">
                    <div class="box-header with-border">
                        <i class="fa fa-table custom_header_icon admin_icon"></i>
                        <h3 class="box-title">Designation </h3>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="UpView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="divGrid"></div>
                                    <asp:GridView ID="grdDesignation" runat="server" Visible="false" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="grdDesignation_RowCommand" OnRowDataBound="grdDesignation_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID">
                                                <HeaderStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("DesignationID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company">
                                                <HeaderStyle Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Top Designation">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTopDesignation" runat="server"  ClientIDMode="Static" validate='vgroup' require="Please Select" Text='<%#Eval("TopDesignation")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Top Designation">
                                                <HeaderStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTopDesignation" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle Width="5%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("DesignationID")%>' CommandName="EditDesignation">
                                <i class="fa fa-pencil-square-o"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;                        
                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="confirm" CommandArgument='<%#Eval("DesignationID")%>' CommandName="DeleteDesignation" OnClientClick="return confirm('Are you Sure you want to Delete?')">
                                <i class="fa fa-times-circle"></i>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="action-icon" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

     <div class="modal fade" id="Delete-Modal" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" role="document">
            <div id="Div1" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                                <h4 class="modal-title" id="myModalLabel2">Delete</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12" id="mybtn">

                                        <fieldset>
                                            <label>Are you sure you want to delete this record?</label>
                                            <div class="form-group">

                                                <asp:TextBox ID="MsgDelete" Visible="false" runat="server"></asp:TextBox>
                                                <input type="hidden" id="DesignationID" />
                                                
                                            </div>

                                        </fieldset>
                                        <%-- End Account Setup--%>

                                        <fieldset>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                               <%-- <asp:TextBox ID="TextBox4" Visible="false" runat="server"></asp:TextBox>--%>
                               <button id="btnSubmit" style="width: 130px;"  class="submit action-button">Yes</button>
                               <asp:Button ID="btncancel" ClientIDMode="Static" runat="server" data-dismiss="modal" Text="No" Style="width: 130px;" CssClass="submit action-button"></asp:Button>
                                                <%--<asp:Button ID="btndelete" ClientIDMode="Static" runat="server" Text="Yes" CommandArgument='<%#Eval("ID")%>' Style="width: 130px;" CssClass="submit action-button" OnCommand="btndelete_Command"></asp:Button>--%>

                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <%--<script src="Scripts/select2.min.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        $(function () {
            //binddata();
            divGrid_Bind();
            $('#btnSave').click(function () {
                console.log('hello 123')
                $('#btnSave').hide();
            })
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            divGrid_Bind();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            divGrid_Bind();
        });

        function binddata() {
            $("[id$=grdDesignation]").prepend($("<thead></thead>").append($("[id$=grdDesignation]").find("tr:first"))).dataTable();
        }


        <%--        $(document).ready(function () {

            $("#<%=ddlCompany.ClientID%>").select2({

          placeholder: "Select Item",

          allowClear: true

      });

  });--%>


        function divGrid_Bind() {

            $.ajax({
                type: 'POST',
                url: "Designation.aspx/Load",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {},
                success: function (result) {

                    debugger
                    JSON.parse(result.d)

                    $("#divGrid").dxTreeList({
                        dataSource: JSON.parse(result.d),
                        searchPanel: {
                            visible: true,
                            width: 240,
                            placeholder: "Search..."
                        },

                    keyExpr: "DesignationID",
                    parentIdExpr: "TopDesignationID",
                    autoExpandAll: false,
                    columns: [
                         "Department"
                        , "Designation"
                        , { 
                            dataField: "TopDesignation",
                            caption: "Top Designation",
                            cellTemplate: function (container, options) {
                                if (options.data.TopDesignationID == 0) {
                                    container.append("<span>-</span>")
                                }
                                else {
                                    container.append("<span>" + options.value + "</span>")
                                }
                            }
                        },{
                            dataField: "ReportsTo",
                            caption: "Reports To",
                        }
                        //, {
                        //    dataField: "ReportsTo",
                        //    caption: "Reports To",
                        //    cellTemplate: function (container, options) {
                        //        //debugger
                        //        if (options.value == null || options.value == "undefined" || options.value == '') {
                        //            container.append("<span>-</span>")
                        //        }
                        //        else {
                        //            container.append("<span>" + options.value + "</span>")
                        //        }
                        //    }
                        //}
                        , "CompanyName",
                    {
                        dataField: "DesignationID",
                        caption: "Action",
                        cellTemplate: function (container, options) {

                            var departmentId = options.value;
                            console.log(departmentId,"departmentId")
                            if (departmentId === 5000 || departmentId === 6000 || departmentId === 7000) {
                                container.append("<span>Cannot edit this department</span>");
                            } else {
                                container.append("<a href='/DesignationEdit.aspx?ID=" + departmentId + "'>Edit</a> | <a style='cursor:pointer' OnClick='deleteRecord(" + departmentId + ")'>Delete</a>");
                            }
                            //container.append("<a href='/DesignationEdit.aspx?ID=" + options.value + "'>Edit</a> | <a style='cursor:pointer' OnClick='deleteRecord(" + options.value + ")'>Delete</a>")
                            //container.append("<div class='dropdown'><a class='btn-sm dropdown-toggle' type='button' data-toggle='dropdown'><i class='fa fa-ellipsis-v' aria-hidden='true'></i></a><ul class='dropdown-menu'><li><a href='Employes_Edit.aspx?ID=" + options.value + "'>Edit</a></li><li><a href='Employes_Details.aspx?ID=" + options.value + "'>Details</a></li><li><a OnClick='deleteEmlpoyee(" + options.value + ")'>Delete</a></li><li><a href='Employes_Transfer.aspx?ID=" + options.value + "'>Transfer</a></li><li><a href='Employee_PromotionNew.aspx?ID=" + options.value + "'>Promotion</a></li></ul></div>")
                        }

                    },
                    ],
                    
                    expandedRowKeys: [1],
                    showRowLines: true,
                    showBorders: true,
                    columnAutoWidth: true,
                    
                });

                },
                error: function (result) {
                }
            });
        }

        function deleteRecord(id) {
            $('#Delete-Modal').modal('show');
            $('#DesignationID').val(id);
        }


    </script>

     <script>
         $('#btnSubmit').click(function () {
             debugger
            var designationID = $('#DesignationID').val();
            //$.ajax({
            //    type: 'POST',
            //    url: "Employee.aspx/Load",
            //    contentType: 'application/json; charset=utf-8',
            //    dataType: 'json',
            //    data: {},
            //    success: function (result) {})};

            $.ajax({
                type: 'POST',
                url: "Designation.aspx/Delete",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'DesignationID':'" + designationID + "'}",
                //data: {},
                success: function (result) {
                    if (result.d == "success") {
                        window.location.href = "Designation.aspx";
                    }
                    else if(result.d == "failed")
                    {
                        msgbox(4, "Sorry", "Something went wrong");
                    }
                    else if (result.d == "Exist") {

                        msgbox(4, "Sorry", "This designation can not be deleted because employee exist");
                        $('#Delete-Modal').modal('hide');
                        setInterval(function () {
                            window.location.href = "Designation.aspx";
                        }, 3000);
                    }
                    else
                    {
                        msgbox(4, "", "Sorry You can not delete main head department of designation")
                    }
                    console.log(result);
                },
                error: function (result) {
                  
                    console.log(false);
                }
            });
        });
    </script>
</asp:Content>
