<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ViewMessage.aspx.cs" Inherits="AcMeERP.Module.Office.ViewMessage" %>

<%@ Register Src="../../WebControl/GridViewControl.ascx" TagName="GridViewControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upRole" runat="server">
        <ContentTemplate>
            <uc1:GridViewControl ID="gvMessageView" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
