<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="AcMeERP.ErrorPage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div style="width: 100%; text-align: center; margin-top: 150px; font-size: 25px;
        color: Red;">
        <asp:Label ID="lblErrorMessage" runat="server" 
            Text="The page that you have tried is inaccessible due to user rights policy defined" 
            meta:resourcekey="lblErrorMessageResource1"></asp:Label>
    </div>
</asp:Content>
