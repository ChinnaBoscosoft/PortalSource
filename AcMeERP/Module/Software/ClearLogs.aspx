<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ClearLogs.aspx.cs" Inherits="AcMeERP.Module.Software.ClearLogs"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upClearLog" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlClearVoucher" GroupingText="Clear Log" runat="server" meta:resourcekey="pnlHeadOfficeBasicInfoResource1">
                <div class="row-fluid">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <div class="bold">
                                Data Synchronization Status
                            </div>
                        </div>
                        <div class="span7">
                            <asp:CheckBoxList ID="chblLogStatus" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="false" >
                                <asp:ListItem>Received</asp:ListItem>
                                <asp:ListItem>Closed</asp:ListItem>
                                <asp:ListItem>InProgress</asp:ListItem>
                                <asp:ListItem>Failed</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <div class="bold">
                                Clear Log Files
                            </div>
                        </div>
                        <div class="span7">
                            <asp:CheckBoxList ID="chkblClearLogFile" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="false"> 
                                <asp:ListItem>Portal Log</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="textcenter" align="center">
                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" ToolTip="Click here to Clear "
                            OnClick="btnClear_Click" meta:resourcekey="btnSaveUserResource1"></asp:Button>
                        <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                            OnClick="btnClose_Click" CausesValidation="False" meta:resourcekey="hlkCloseResource1">
                        </asp:Button>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
