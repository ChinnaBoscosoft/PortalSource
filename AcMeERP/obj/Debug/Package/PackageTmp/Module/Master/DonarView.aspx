<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="DonarView.aspx.cs" Inherits="AcMeERP.Module.Master.DonarView" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../../WebControl/GridViewControl.ascx" TagName="GridViewControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upDonorView" runat="server">
        <ContentTemplate>
            <uc1:GridViewControl ID="gvDonorView" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
