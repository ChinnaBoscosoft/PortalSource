<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="FCPurposeAdd.aspx.cs" Inherits="AcMeERP.Module.Master.FCPurposeAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upPurpose" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal4" runat="server" Text="code*" meta:resourcekey="Literal4Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <%--<asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textbox manfield" MaxLength="5"
                        onkeyup="ChangeCase(this.id)" ToolTip="Enter Purpose Code" meta:resourcekey="txtPurposeCodeResource1"></asp:TextBox>--%>
                        <asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textbox" MaxLength="10"
                            Width="90px" ToolTip="Enter Purpose Code" meta:resourcekey="txtPurposeCodeResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfPC" runat="server" Text="*" ErrorMessage="FC Purpose Code is required"
                            CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtPurposeCode"
                            Display="Dynamic" meta:resourcekey="rfPCResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal1" runat="server" Text="FC Purpose*" meta:resourcekey="Literal4Resource1"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtPurpose" runat="server" CssClass="textbox manfield" MaxLength="150"
                            onkeyup="ChangeCase(this.id)" ToolTip="Enter FC Purpose Category" meta:resourcekey="txtPurposeResource1"
                            Width="550px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                            ErrorMessage="FC Purpose is required" CssClass="requiredcolor" SetFocusOnError="True"
                            ControlToValidate="txtPurpose" Display="Dynamic" meta:resourcekey="rfPCResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnSaveFCPurpose" OnClick="btnSaveFCPurpose_Click" runat="server"
                    CssClass="button" Text="Save" ToolTip="Click here to save" meta:resourcekey="btnSaveFcPurposeResource1">
                </asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new FC Purpose" CausesValidation="False" meta:resourcekey="btnNewResource1">
                </asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="FcPurposeVs" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="FcPurposeVsResource1"></asp:ValidationSummary>
</asp:Content>
