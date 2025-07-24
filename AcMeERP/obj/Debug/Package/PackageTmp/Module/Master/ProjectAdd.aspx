<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ProjectAdd.aspx.cs" Inherits="AcMeERP.Module.Master.ProjectAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="~/WebControl/DateControl.ascx" TagName="Datecontrol" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upProjectAdd" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal4" runat="server" Text="Code" meta:resourcekey="Literal4Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtProjectCode" runat="server" CssClass="textbox" Width="294px"
                            ToolTip="Enter Project Code" MaxLength="5" AutoCompleteType="Disabled" meta:resourcekey="txtProjectCodeResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal10" runat="server" Text="Society Name *" meta:resourcekey="Literal10Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:DropDownList ID="ddlSociety" runat="server" CssClass="combobox manfield" Width="300px"
                            ToolTip="Select Society Name" meta:resourcekey="ddlSocietyResource1">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpSociety" runat="server" ErrorMessage="Society Name is required"
                            CssClass="requiredcolor" ControlToValidate="ddlSociety" Operator="NotEqual" Type="Integer"
                            ValueToCompare="0" meta:resourcekey="cmpSocietyResource1" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal2" runat="server" Text="Project Category *" meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:DropDownList ID="ddlProjectCategory" runat="server" CssClass="combobox manfield"
                            Width="300px" ToolTip="Select Project Category" meta:resourcekey="ddlProjectCategoryResource1">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpRl" runat="server" ErrorMessage="Project Category is required"
                            CssClass="requiredcolor" ControlToValidate="ddlProjectCategory" Operator="NotEqual"
                            Type="Integer" ValueToCompare="0" meta:resourcekey="cmpRlResource1" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal3" runat="server" Text="Project *" meta:resourcekey="Literal3Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtProject" runat="server" CssClass="textbox manfield" Width="294px"
                            MaxLength="100" onkeyup="ChangeCase(this.id)" ToolTip="Enter Project" meta:resourcekey="txtProjectResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                            CssClass="requiredcolor" ErrorMessage="Project is required" SetFocusOnError="True"
                            ControlToValidate="txtProject" Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal5" runat="server" Text="Description" meta:resourcekey="Literal5Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox multiline" Width="294px"
                            onkeypress="return textboxMultilineMaxNumber(event,this,100);" TextMode="MultiLine"
                            ToolTip="Enter Description" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal6" runat="server" Text="Started On *" meta:resourcekey="Literal6Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <dx:ASPxDateEdit ID="dteStartedOn" runat="server" CssClass="combobox manfield" UseMaskBehavior="True"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="100px"
                            meta:resourcekey="dteStartedOnResource1" EditFormat="Custom">
                        </dx:ASPxDateEdit>
                        <asp:RequiredFieldValidator ID="rvfStartedOn" ControlToValidate="dteStartedOn" Display="Dynamic"
                            CssClass="requiredcolor" runat="server" Text="*" SetFocusOnError="True" ErrorMessage="Started On is required"
                            meta:resourcekey="rvfStartedOnResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal7" runat="server" Text="Closed On" meta:resourcekey="Literal7Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <dx:ASPxDateEdit ID="dteClosedOn" runat="server" CssClass="combobox manfield" UseMaskBehavior="True"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="100px"
                            meta:resourcekey="dteClosedOnResource1" EditFormat="Custom">
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal8" runat="server" Text="Division *" meta:resourcekey="Literal8Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="combobox manfield" Width="294px"
                            ToolTip="Select division" meta:resourcekey="ddlDivisionResource1">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Division is required"
                            CssClass="requiredcolor" ControlToValidate="ddlDivision" Operator="NotEqual"
                            Type="Integer" ValueToCompare="0" meta:resourcekey="CompareValidator1Resource1"
                            Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal9" runat="server" Text="Address" meta:resourcekey="Literal9Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox multiline" Width="294px"
                            onkeypress="return textboxMultilineMaxNumber(event,this,250);" ToolTip="Enter Notes"
                            TextMode="MultiLine" meta:resourcekey="txtNotesResource1"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" runat="server" CssClass="button"
                    Text="Save" ToolTip="Click here to save " meta:resourcekey="btnSaveUserResource1">
                </asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for New Project" CausesValidation="False" meta:resourcekey="btnNewResource1">
                </asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ProjectVs" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="ProjectVsResource1"></asp:ValidationSummary>
</asp:Content>
