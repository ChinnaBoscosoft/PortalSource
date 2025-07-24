<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="LockVoucherAdd.aspx.cs" Inherits="AcMeERP.Module.Master.LockVoucherAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upAuditLock" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal1" runat="server" Text="Branch*" meta:resourcekey="Literal1Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="combobox manfield" Width="300px"
                            ToolTip="Select Branch Name" meta:resourcekey="ddlBranchResource1" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpBranch" runat="server" ErrorMessage="Branch Name is required"
                            CssClass="requiredcolor" ControlToValidate="ddlBranch" Operator="NotEqual" Type="Integer"
                            ValueToCompare="0" meta:resourcekey="cmpBranchResource1" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal2" runat="server" Text="Project*" meta:resourcekey="Literal2Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:DropDownList ID="ddlProjects" runat="server" CssClass="combobox manfield" Width="300px"
                            ToolTip="Select Project Name" meta:resourcekey="ddlProjectsResource1">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpProjects" runat="server" ErrorMessage="Project Name is required"
                            CssClass="requiredcolor" ControlToValidate="ddlProjects" Operator="NotEqual"
                            Type="Integer" ValueToCompare="0" meta:resourcekey="cmpProjectsResource1" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal3" runat="server" Text="Date From *" meta:resourcekey="Literal3Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <dx:ASPxDateEdit ID="dteDateFrom" runat="server" CssClass="combobox manfield" UseMaskBehavior="True"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="100px"
                            meta:resourcekey="dteDateFromResource1" EditFormat="Custom">
                        </dx:ASPxDateEdit>
                        <asp:RequiredFieldValidator ID="rvfDateFrom" ControlToValidate="dteDateFrom" Display="Dynamic"
                            CssClass="requiredcolor" runat="server" Text="*" SetFocusOnError="True" ErrorMessage="Date From is required"
                            meta:resourcekey="rvfDateFromResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal4" runat="server" Text="Date To*" meta:resourcekey="Literal4Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <dx:ASPxDateEdit ID="dteDateTo" runat="server" CssClass="combobox manfield" UseMaskBehavior="True"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="100px"
                            meta:resourcekey="dteDateToResource1" EditFormat="Custom">
                        </dx:ASPxDateEdit>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="dteDateTo"
                            Display="Dynamic" CssClass="requiredcolor" runat="server" Text="*" SetFocusOnError="True"
                            ErrorMessage="Date To is required" meta:resourcekey="rvfDateToResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal5" runat="server" Text="Password*" meta:resourcekey="Literal5Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" Width="294px" ToolTip="Enter Password"
                           meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="requiredcolor"
                            ErrorMessage="Password is required" ToolTip="Password is required" ValidationGroup="LockGroup"
                            ControlToValidate="txtPassword" meta:resourcekey="RequiredFieldValidator1Resource1"
                            Text="*"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal6" runat="server" Text="Hint*" meta:resourcekey="Literal6Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtPasswordHint" runat="server" CssClass="textbox" Width="294px"
                            ToolTip="Enter Password hint" MaxLength="25" AutoCompleteType="Disabled" 
                            meta:resourcekey="txtPasswordHintResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*"
                            CssClass="requiredcolor" ErrorMessage="Password is required" SetFocusOnError="True"
                            ControlToValidate="txtPasswordHint" Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal7" runat="server" Text="Reasons" meta:resourcekey="Literal7Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtReasons" runat="server" CssClass="textbox multiline" Width="294px"
                            onkeypress="return textboxMultilineMaxNumber(event,this,250);" ToolTip="Enter Reasons"
                            TextMode="MultiLine" meta:resourcekey="txtReasonsResource1"></asp:TextBox>
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
    <asp:ValidationSummary ID="AuditLockVs" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="AuditLockResource1"></asp:ValidationSummary>
</asp:Content>
