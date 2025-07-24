<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="RefreshBalance.aspx.cs" Inherits="AcMeERP.Module.Office.RefreshBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="row-fluid">
        <div class="span8 offset2">
            <asp:Panel ID="pnlLedgerBalance" GroupingText="Balance Refresh" runat="server">
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="ltrlDateFrom" runat="server" Text="Date From *"></asp:Literal>
                    </div>
                    <div class="span7">
                        <dx:ASPxDateEdit ID="dteFrom" runat="server" CssClass="combobox manfield" UseMaskBehavior="True"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="100px"
                            EditFormat="Custom">
                        </dx:ASPxDateEdit>
                        <asp:RequiredFieldValidator ID="rvfDateFrom" ControlToValidate="dteFrom" Display="Dynamic"
                            CssClass="requiredcolor" runat="server" Text="*" SetFocusOnError="True" ErrorMessage="Started On is required"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="ltrlBranch" runat="server" Text="Branch *"></asp:Literal></div>
                    <div class="span7">
                        <dx:ASPxComboBox ID="cmbBranches" runat="server" Theme="Office2010Blue" OnSelectedIndexChanged="cmbBranches_SelectedIndexChanged"
                            AutoPostBack="true" IncrementalFilteringMode="Contains" Width="350px" CssClass="combobox manfield ">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="ltrlProject" runat="server" Text="Project *"></asp:Literal></div>
                    <div class="span7">
                        <dx:ASPxComboBox ID="cmbProject" runat="server" IncrementalFilteringMode="Contains"
                            CssClass="combobox manfield" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="cmbProject_SelectedIndexChanged"
                            Theme="Office2010Blue">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div class="textcenter" align="center">
                    <asp:Button ID="btnRefresh" OnClick="btnRefreshBalance_OnClick" runat="server" CssClass="button"
                        Text="Refresh" ToolTip="Click here to refresh balance"></asp:Button>
                    <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                        CausesValidation="False"></asp:Button>
                </div>
        </div>
    </div>
    </asp:Panel>
</asp:Content>
