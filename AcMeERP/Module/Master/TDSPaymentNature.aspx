<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="TDSPaymentNature.aspx.cs" Inherits="AcMeERP.Module.Master.TDSPaymentNature" %>

<%@ Register Src="~/WebControl/GridViewControl.ascx" TagName="GridViewControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
  <uc1:GridViewControl ID="gvPaymentNature" runat="server" />
</asp:Content>
