<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master" AutoEventWireup="true" CodeBehind="LockVoucherView.aspx.cs"
 Inherits="AcMeERP.Module.Master.LockVoucherView" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register src="../../WebControl/GridViewControl.ascx" tagname="GridViewControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <uc1:GridViewControl ID="gvLockView" runat="server" />
</asp:Content>
