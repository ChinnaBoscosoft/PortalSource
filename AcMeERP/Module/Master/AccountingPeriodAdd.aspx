<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AccountingPeriodAdd.aspx.cs" Inherits="AcMeERP.Module.Master.AccountingPeriodAdd"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upAPAdd" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="row-fluid">
                    <div class="span6 textright">
                        <asp:Literal ID="Literal13" runat="server" Text="Year From *" meta:resourcekey="Literal13Resource1"></asp:Literal>
                    </div>
                    <div class="span5">
                        <dx:ASPxDateEdit ID="dteYearFrom" runat="server" Width="94px" UseMaskBehavior="True"
                            ClientInstanceName="dteYearFrom" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                            EditFormat="Custom" meta:resourcekey="dteYearFromResource1">
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span6 textright">
                        <asp:Literal ID="Literal14" runat="server" Text="Year To *" meta:resourcekey="Literal14Resource1"></asp:Literal>
                    </div>
                    <div class="span5">
                        <dx:ASPxDateEdit ID="dteYearTo" runat="server" Width="94px" UseMaskBehavior="True"
                            ClientInstanceName="dteYearTo" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                            EditFormat="Custom" meta:resourcekey="dteYearToResource1">
                        </dx:ASPxDateEdit>
                    </div>
                </div>
            </div>
            <div class="clearfix marginbottom">
            </div>
            <div class="textcenter">
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ToolTip="Click here to save accounting period"
                    OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1"></asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close"
                    CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
