<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditDepartment.ascx.cs" Inherits="DBH.ModuleGenerator.EditDepartment" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke.WebControls" %>

<%-- <link href='<%=ResolveUrl("~/bootstrap/css/bootstrap.min.css") %>' rel="stylesheet" type="text/css" /> --%>


<link href='<%=ResolveUrl("~/bootstrap/datatables/css/jquery.dataTables.min.css") %>' rel="stylesheet" type="text/css" />
<script src='<%=ResolveUrl("~/bootstrap/datatables/js/jquery.dataTables.min.js") %>' type="text/javascript"></script>

<ul id="NavigationBar" runat="server"></ul>

<div class="container-fluid">
    <div class="dnnFormMessage dnnFormInfo">
        <asp:Label ID="lblIntro" runat="server" resourceKey="lblIntro" />
    </div>

    <div class="dnnForm dnnEditExtension dnnClear" id="dnnEditExtension">
        <fieldset>
            <div class="row">
                <div class="col-md-6">
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtDepartment" ResourceKey="txtDepartment" CssClass="dnnFormRequired" />
                        <asp:TextBox runat="server" ID="txtDepartment" CssClass="dnnFormRequired" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtOwnerFolder" ResourceKey="txtOwnerFolder" />
                        <asp:TextBox ID="txtOwnerFolder" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtRootNamespace" ResourceKey="txtRootNamespace" />
                        <asp:TextBox ID="txtRootNamespace" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtIconFile" ResourceKey="txtIconFile" />
                        <asp:TextBox ID="txtIconFile" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtOwnerName" ResourceKey="txtOwnerName" />
                        <asp:TextBox ID="txtOwnerName" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtOwnerOrganization" ResourceKey="txtOwnerOrganization" />
                        <asp:TextBox ID="txtOwnerOrganization" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtOwnerWebsite" ResourceKey="txtOwnerWebsite" />
                        <asp:TextBox ID="txtOwnerWebsite" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="txtOwnerEmail" ResourceKey="txtOwnerEmail" />
                        <asp:TextBox ID="txtOwnerEmail" runat="server" />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="dnnFormItem">
                        <ul class="dnnActions dnnClear">
                            <li><asp:LinkButton runat="server" CssClass="dnnPrimaryAction" ID="cmdAddNewRecord" Resourcekey="cmdAddNewRecord" OnClick="cmdAddNewRecord_Click" /></li>
                        </ul>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <%--    <div class="row header" style="text-align: center; color: green">
        <h3>Human Resource is the interface to access the database that has all the information of all the L.A. County Department of Beaches and Harbors employees and positions.<br />
            You can add, modify or delete employees and positions.<br />
            And you can also create and access reports with the information up to date.</h3>
    </div>--%>
            <asp:GridView ID="gvDepartment" runat="server" AutoGenerateColumns="false"
                OnRowUpdating="gvDepartment_RowUpdating"
                OnRowCancelingEdit="gvDepartment_RowCancelingEdit"
                OnRowDeleting="gvDepartment_RowDeleting"
                OnRowEditing="gvDepartment_RowEditing"
                OnPreRender="gvDepartment_PreRender"
                CssClass="table table-striped">
                <EmptyDataTemplate>
                    <asp:Image ID="Image0" ImageUrl="~/Images/yellow-warning.gif" AlternateText="No Image" runat="server" />No Data Found.
                </EmptyDataTemplate>
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="deleteButton" runat="server" CommandName="Delete" Text="Delete"
                                OnClientClick="return confirm('Are you sure you want to delete this department?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Department" SortExpression="Department" HeaderText="Department" />
                    <asp:BoundField DataField="OwnerFolder" SortExpression="OwnerFolder" HeaderText="Owner Folder" />
                    <asp:BoundField DataField="RootNamespace" SortExpression="RootNamespace" HeaderText="Module Namespace" />
                    <asp:BoundField DataField="OwnerName" SortExpression="OwnerName" HeaderText="Owner Name" />
                    <asp:BoundField DataField="OwnerOrganization" SortExpression="OwnerOrganization" HeaderText="Owner Organization" />
                    <asp:BoundField DataField="OwnerWebsite" SortExpression="OwnerWebsite" HeaderText="Owner Url" />
                    <asp:BoundField DataField="OwnerEmail" SortExpression="OwnerEmail" HeaderText="Owner Email" />
                    <asp:BoundField DataField="IconFile" SortExpression="IconFile" HeaderText="Icon File" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>

<%--<script>
    $(document).ready(function () {
        $('#<%= gvDepartment.ClientID %>').dataTable({
            "aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],
            "iDisplayLength": -1,
            "order": [[2, "asc"]],
            stateSave: true,
            stateSaveCallback: function (settings, data) {
                localStorage.setItem('DataTables_' + settings.sInstance, JSON.stringify(data));
            },
            stateLoadCallback: function (settings) {
                return JSON.parse(localStorage.getItem('DataTables_' + settings.sInstance));
            }
        });
    });
</script>--%>

