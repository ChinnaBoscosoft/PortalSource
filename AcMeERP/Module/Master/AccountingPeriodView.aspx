<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AccountingPeriodView.aspx.cs" Inherits="AcMeERP.Module.Master.AccountingPeriodView"
    meta:resourcekey="PageResource1" %>

<%@ Register Src="~/WebControl/GridViewControl.ascx" TagName="GridViewControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upaccountYearView" runat="server">
        <ContentTemplate>
            <div class="div100">
                <uc1:GridViewControl ID="gvAccountingYear" runat="server" EnableSort="true" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
