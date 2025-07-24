<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="FCPurposeView.aspx.cs" Inherits="AcMeERP.Module.Master.FCPurposeView"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../../WebControl/GridViewControl.ascx" TagName="GridViewControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <%-- <asp:UpdatePanel ID="upProject" runat="server">
        <ContentTemplate>--%>
    <uc1:GridViewControl ID="gvPurpose" runat="server" />
    <%--</ContentTemplate> 
    </asp:UpdatePanel>--%>
</asp:Content>
