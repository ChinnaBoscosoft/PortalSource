<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="TDSDutyTax.aspx.cs" Inherits="AcMeERP.Module.Master.TDSDutyTax" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/WebControl/GridViewControl.ascx" TagName="GridViewControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <uc1:GridViewControl ID="gvDutyTax" runat="server" />
</asp:Content>
