<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master" AutoEventWireup="true" CodeBehind="LedgerView.aspx.cs" Inherits="AcMeERP.Module.Master.LedgerView" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../../WebControl/GridViewControl.ascx" TagName="GridViewControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
<%--<asp:UpdatePanel ID="upLedgerView" runat="server">
        <ContentTemplate>--%>
            <uc1:GridViewControl ID="gvLedgerView" runat="server" />
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
