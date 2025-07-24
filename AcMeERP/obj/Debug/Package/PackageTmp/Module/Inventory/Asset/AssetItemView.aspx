<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master" AutoEventWireup="true" CodeBehind="AssetItemView.aspx.cs" Inherits="AcMeERP.Module.Inventory.Asset.AssetItemView" %>
 <%@ Register src="~/WebControl/GridViewControl.ascx" tagname="GridViewControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
<uc1:GridViewControl ID="gvAssetItem" runat="server" />
</asp:Content>
