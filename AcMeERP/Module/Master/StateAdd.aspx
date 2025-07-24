<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="StateAdd.aspx.cs" Inherits="AcMeERP.Module.Master.StateAdd" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="~/WebControl/DateControl.ascx" TagName="Datecontrol" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upCountryAdd" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal4" runat="server" Text="Country *" 
                            meta:resourcekey="Literal4Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox manfield" 
                            MaxLength="50" ToolTip="Enter Country" meta:resourcekey="txtCountryResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfNm" runat="server" Text="*" 
                            ErrorMessage="Country is required" CssClass="requiredcolor"
                            SetFocusOnError="True" ControlToValidate="txtCountry" Display="Dynamic" 
                            meta:resourcekey="rfNmResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal1" runat="server" Text="Country Code *" 
                            meta:resourcekey="Literal1Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox manfield" ToolTip="Enter Country Code"
                            MaxLength="5" meta:resourcekey="txtCountryCodeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*"
                            ErrorMessage="Country Code is required" SetFocusOnError="True" 
                            ControlToValidate="txtCountryCode" CssClass="requiredcolor"
                            Display="Dynamic" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal2" runat="server" Text="Symbol *" 
                            meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                    <asp:TextBox ID="txtSymbolIcon" runat="server" CssClass="textbox manfield" 
                            MaxLength="8" ToolTip="Select Symbol" 
                            meta:resourcekey="txtSymbolResource1"></asp:TextBox>

                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*"
                            ErrorMessage="Symbol Code is required" SetFocusOnError="True" 
                            ControlToValidate="txtSymbolIcon" CssClass="requiredcolor"
                            Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                       <%-- <asp:DropDownList ID="ddlSymbol" runat="server" CssClass="combobox manfield" Width="255px"
                            ToolTip="Select Symbol" meta:resourcekey="ddlSymbolResource1">
                        </asp:DropDownList>--%>

                        <%--<asp:CompareValidator ID="cmpRl" runat="server" 
                            ErrorMessage="Symbol is required" ControlToValidate="ddlSymbol" CssClass="requiredcolor"
                            Operator="NotEqual" Type="Integer" ValueToCompare="0" 
                            meta:resourcekey="cmpRlResource1">*</asp:CompareValidator>--%>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal3" runat="server" Text="Code *" 
                            meta:resourcekey="Literal3Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtSymbolCode" runat="server" CssClass="textbox manfield" 
                            MaxLength="8" ToolTip="Select Symbol Code" 
                            meta:resourcekey="txtSymbolCodeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                            ErrorMessage="Symbol Code is required" SetFocusOnError="True" 
                            ControlToValidate="txtSymbolCode" CssClass="requiredcolor"
                            Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal5" runat="server" Text="Name *" 
                            meta:resourcekey="Literal5Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtSymbalName" runat="server" CssClass="textbox manfield" MaxLength="40"
                            ToolTip="Enter Symbal Name" meta:resourcekey="txtSymbalNameResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            Text="*" CssClass="requiredcolor"
                            ErrorMessage="Name is required" SetFocusOnError="True" ControlToValidate="txtSymbalName"
                            Display="Dynamic" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="textcenter" align="center">
                    <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" runat="server" CssClass="button"
                        Text="Save" ToolTip="Click here to save " 
                        meta:resourcekey="btnSaveUserResource1"></asp:Button>
                    <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                        ToolTip="Click here for new Country" CausesValidation="False" 
                        meta:resourcekey="btnNewResource1"></asp:Button>
                    <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                        CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
                </div>
                <div class="clearfix"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="CountryVs" runat="server" ShowSummary="False"
        ShowMessageBox="True" meta:resourcekey="CountryVsResource1"></asp:ValidationSummary>
</asp:Content>
