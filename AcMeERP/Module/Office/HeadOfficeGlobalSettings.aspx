<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="HeadOfficeGlobalSettings.aspx.cs" Inherits="AcMeERP.Module.Office.HeadOfficeGlobalSettings"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">

    <asp:UpdatePanel ID="upHeadOfficeCreate" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span11 offset1">
                    <asp:Panel ID="pnlHeadOfficeSettings" GroupingText="Generlate Report - Ledger Configuration (Inter Account/Province Contribution)" runat="server"
                        meta:resourcekey="pnlHeadOfficeBasicInfoResource1">
                        <div class="row-fluid">
                            <div class="span2_5">
                                 <div class="bold"><asp:Literal ID="Literal4" runat="server" Text="Inter Account From" meta:resourcekey="Literal16Resource1"></asp:Literal></div>
                                 <div class="span3_25" style="overflow: auto;height:100px;width:400px;">
                                    <dx:ASPxCheckBoxList ID="cbchkInterAcFromList" runat="server" ValueType="System.Int32" Width="100%" >
                                    </dx:ASPxCheckBoxList>
                                </div>
                            </div>
                            <div class="span2"></div>
                            <div class="span2_5">
                                <div class="bold"><asp:Literal ID="Literal2" runat="server" Text="Province Contribution From" meta:resourcekey="Literal16Resource1"></asp:Literal></div>
                                <div class="span3_25" style="overflow: auto;height:100px;width:400px;">
                                    <dx:ASPxCheckBoxList ID="cbchkContributionFromList" runat="server" ValueType="System.Int32" Width="100%" >
                                    </dx:ASPxCheckBoxList>
                                </div>
                            </div>
                         </div>
                         <div class="row-fluid"><div class="span2"></div></div>
                        <div class="row-fluid">
                            <div class="span2_5">
                                <div class="bold"><asp:Literal ID="Literal5" runat="server" Text="Inter Account To" meta:resourcekey="Literal16Resource1"></asp:Literal></div>
                                <div class="span3_25" style="overflow: auto;height:100px;width:400px;">
                                    <dx:ASPxCheckBoxList ID="cbchkInterAcToList" runat="server" ValueType="System.Int32" Width="100%" >
                                    </dx:ASPxCheckBoxList>
                                </div>
                            </div>
                            <div class="span2"></div>
                            <div class="span2_5">
                                <div class="bold"><asp:Literal ID="Literal3" runat="server" Text="Province Contribution To" meta:resourcekey="Literal16Resource1"></asp:Literal></div>
                                <div class="span3_25" style="overflow: auto;height:100px;width:400px;">
                                    <dx:ASPxCheckBoxList ID="cbchkContributionToList" runat="server" ValueType="System.Int32" Width="100%" >
                                    </dx:ASPxCheckBoxList>
                                </div>
                            </div>
                         </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="clearfix marginbottom">
            </div>
            <div class="textcenter">
                <asp:Button ID="btnSaveHeadOffice" OnClick="btnSaveHeadOffice_Click" runat="server"
                    CssClass="button" Text="Save" ToolTip="Click here to save head office Details"
                    meta:resourcekey="btnSaveHeadOfficeResource1"></asp:Button>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsHeadOffice" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="vsHeadOfficeResource1"></asp:ValidationSummary>
</asp:Content>