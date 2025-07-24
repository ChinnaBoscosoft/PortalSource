<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master" AutoEventWireup="true" CodeBehind="RoleView.aspx.cs" Inherits="AcMeERP.Module.User.RoleView" %>
 <%@ Register src="../../WebControl/GridViewControl.ascx" tagname="GridViewControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
 <asp:UpdatePanel ID="upRole" runat="server">
        <ContentTemplate>
            <uc1:GridViewControl ID="gvRole" runat="server" EnableSort="true" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
