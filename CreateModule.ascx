<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateModule.ascx.cs" Inherits="DBH.ModuleGenerator.CreateModule" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web.Deprecated" %>

<ul id="NavigationBar" runat="server"></ul>

<div class="container-fluid">

    <div class="dnnFormMessage dnnFormInfo">
        <asp:Label ID="lblIntro" runat="server" resourceKey="lblIntro" />
    </div>

    <div class="dnnForm dnnEditBasicSettings" id="dnnEditBasicSettings">
        <div class="dnnFormExpandContent dnnRight "><a href=""><%=LocalizeString("ExpandAll")%></a></div>

        <div class="well">
            <h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead dnnClear">
                <a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a>
            </h2>
            <fieldset>
                <div class="row">
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="ddlDepartment" ResourceKey="ddlDepartment" CssClass="dnnFormRequired" />
                        <asp:DropDownList runat="server" ID="ddlDepartment" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            <asp:ListItem Value="" Text="Please select department" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDepartment" CssClass="dnnFormMessage dnnFormError" ResourceKey="ddlDepartment.Required" Display="Dynamic" />
        <%--                <asp:HyperLink ID="editDepartment" NavigateUrl='<%# EditURL("ItemID",Eval("ItemID")) %>'
                            Visible="<%# IsEditable %>" runat="server">
                            <asp:Image ID="editLinkImage" ImageUrl="~/images/edit.gif" AlternateText="Edit" Visible="<%# IsEditable %>"
                                runat="server" />
                        </asp:HyperLink>--%>
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label ID="lblProjectFolder" runat="server" CssClass="dnnFormRequired" />
                        <asp:TextBox runat="server" ID="txtProjectFolder" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProjectFolder" CssClass="dnnFormMessage dnnFormError" ResourceKey="txtProjectFolder.Required" Display="Dynamic" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label ID="lblModuleName" runat="server" CssClass="dnnFormRequired" />
                        <asp:TextBox runat="server" ID="txtModuleName" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModuleName" CssClass="dnnFormMessage dnnFormError" ResourceKey="txtModuleName.Required" Display="Dynamic" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label ID="lblModuleAbbreviation" runat="server" CssClass="dnnFormRequired" />
                        <asp:TextBox ID="txtModuleAbbreviation" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModuleAbbreviation" CssClass="dnnFormMessage dnnFormError" ResourceKey="txtModuleAbbreviation.Required" Display="Dynamic" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label ID="lblModuleFriendlyName" runat="server" CssClass="dnnFormRequired" />
                        <asp:TextBox ID="txtModuleFriendlyName" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModuleFriendlyName" CssClass="dnnFormMessage dnnFormError" ResourceKey="txtModuleFriendlyName.Required" Display="Dynamic" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label ID="lblModuleDescription" runat="server" CssClass="dnnFormRequired" />
                        <asp:TextBox ID="txtModuleDescription" runat="server" TextMode="MultiLine" Rows="4" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtModuleDescription" CssClass="dnnFormMessage dnnFormError" ResourceKey="txtModuleDescription.Required" Display="Dynamic" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label id="plLanguage" controlname="optLanguage" runat="server" />
                        <asp:RadioButtonList ID="optLanguage" CssClass="dnnFormRadioButtons" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label id="plTemplate" controlname="cboTemplate" runat="server" />
                        <asp:DropDownList ID="cboTemplate" runat="server" AutoPostBack="True" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="well">
            <h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead dnnClear">
                <a href="" class="dnnSectionExpanded"><%=LocalizeString("DepartmentInfo")%></a>
            </h2>
            <fieldset>
                <div class="row">
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="lblOwnerFolder" ResourceKey="lblOwnerFolder" />
                        <asp:Label ID="lblOwnerFolder" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="lblRootNamespace" ResourceKey="lblRootNamespace" />
                        <asp:Label ID="lblRootNamespace" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="lblOwnerName" ResourceKey="lblOwnerName" />
                        <asp:Label ID="lblOwnerName" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="lblOwnerOrganization" ResourceKey="lblOwnerOrganization" />
                        <asp:Label ID="lblOwnerOrganization" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="lblOwnerWebsite" ResourceKey="lblOwnerWebsite" />
                        <asp:Label ID="lblOwnerWebsite" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="lblOwnerEmail" ResourceKey="lblOwnerEmail" />
                        <asp:Label ID="lblOwnerEmail" runat="server" />
                    </div>
                    <div class="dnnFormItem">
                        <dnn:Label runat="server" ControlName="imgOwnerIcon" ResourceKey="imgOwnerIcon" />
                        <asp:Image ID="imgIconFile" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Icon File" Visible="<%# IsEditable %>" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <div class="dnnForm">
        <div class="dnnFormItem">
            <ul class="dnnActions dnnClear">
                <li><asp:LinkButton runat="server" CssClass="dnnPrimaryAction" ID="cmdCreate" Resourcekey="cmdCreate" OnClick="cmdCreate_Click" /></li>
            </ul>
        </div>
    </div>
    <div class="dnnFormItem">
        <asp:Label ID="lblDescription" runat="server" />
    </div>
</div>

<script type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function dnnEditBasicSettings() {
            $('#dnnEditBasicSettings').dnnPanels();
            $('#dnnEditBasicSettings .dnnFormExpandContent a').dnnExpandAll({ expandText: '<%=Localization.GetString("ExpandAll", LocalResourceFile)%>', collapseText: '<%=Localization.GetString("CollapseAll", LocalResourceFile)%>', targetArea: '#dnnEditBasicSettings' });
        }

        $(document).ready(function () {
            dnnEditBasicSettings();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                dnnEditBasicSettings();
            });
        });

    }(jQuery, window.Sys));
</script>

