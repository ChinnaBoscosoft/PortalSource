<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserView.aspx.cs" Inherits="AcMeERP.Module.User.UserView"
    MasterPageFile="~/MasterPage/HomeLoginMaster.master" %>

<%@ Register Src="../../WebControl/GridViewControl.ascx" TagName="GridViewControl"
    TagPrefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
    <asp:UpdatePanel ID="upU" runat="server">
        <ContentTemplate>
            <div class="div100">
                <uc1:GridViewControl ID="gvUser" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="changePasswordVs" runat="server" ShowSummary="false" EnableClientScript="true"
        ShowMessageBox="true" DisplayMode="BulletList"></asp:ValidationSummary>
</asp:Content>
