<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DBH.ModuleGenerator.Settings" %>


<!-- uncomment the code below to start using the DNN Form pattern to create and update settings -->
  

<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="plDepartment" controlname="ddlDepartment" runat="server" /> 
            <asp:DropDownList ID="ddlDepartment" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plLanguage" controlname="optLanguage" runat="server" />
            <asp:RadioButtonList ID="optLanguage" CssClass="dnnFormRadioButtons" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="plTemplate" controlname="cboTemplate" runat="server" />
            <asp:DropDownList ID="cboTemplate" runat="server" AutoPostBack="True" />
        </div>
        <div class="dnnFormItem">
            <asp:Label ID="lblDescription" runat="server" />
        </div>
    </fieldset>



