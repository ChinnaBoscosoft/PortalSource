<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AcmeWinServiceStatus.aspx.cs" Inherits="AcMeERP.Module.Software.AcmeWinServiceStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel runat="server" ID="upAcMEWinServices">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span5 textright bold">
                    <asp:Label runat="server" ID="lblSerivce" Text="Service Status:"></asp:Label>
                </div>
                <div class="span7">
                    <asp:Label runat="server" ID="lblSerivceType" CssClass="textSize"></asp:Label>
                    <asp:Button runat="server" ID="btnServiceStatus" Text="Stop" CssClass="button" ToolTip="Click here to Start / Stop the service"
                        OnClick="btnServiceStatus_Click" />
                </div>
            </div>
            <div class="row-fluid">
                <div class="span7 textright">
                    <asp:TextBox runat="server" ID="txtServiceDescription" TextMode="MultiLine" CssClass="textbox textMulitiline" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button runat="server" CssClass="button" Text="Clear Log" ID="btnClearLog" ToolTip="Click here to clear the log"
                    OnClick="btnClearLog_Click" CausesValidation="False" />
                <asp:Button runat="server" CssClass="button" Text="Refresh" ID="btnRefresh" ToolTip="Click here to refresh"
                    OnClick="btnRefresh_Click" CausesValidation="False" />
                <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    OnClick="btnClose_Click" CausesValidation="False"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
