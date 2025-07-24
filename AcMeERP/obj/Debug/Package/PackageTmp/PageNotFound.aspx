<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeMaster.Master" AutoEventWireup="true" CodeBehind="PageNotFound.aspx.cs" Inherits="AcMeERP.PageNotFound" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
 <div style="width: 100%; text-align: center; margin-top: 150px; font-size: 25px;
        color: Red;">
        <asp:Label ID="lblErrorMessage" runat="server" 
            Text="The URL that you request is not available" 
            meta:resourcekey="lblErrorMessageResource1"></asp:Label>
    </div>
</asp:Content>
