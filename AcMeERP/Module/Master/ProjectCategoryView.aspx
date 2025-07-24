<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ProjectCategoryView.aspx.cs" Inherits="AcMeERP.Module.Master.ProjectCategoryView" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
    <%@ Register src="../../WebControl/GridViewControl.ascx" tagname="GridViewControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
   <%--<asp:UpdatePanel ID="upProjectCategory" runat="server">
        <ContentTemplate>--%>
            <uc1:GridViewControl ID="gvProjectCategory" runat="server" />
     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
