<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="HeadOfficeView.aspx.cs" Inherits="AcMeERP.Module.Office.HeadOfficeView"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../../WebControl/GridViewControl.ascx" TagName="GridViewControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="div100">
        <uc1:GridViewControl ID="gvHeadOffice" runat="server" />
    </div>
    <asp:UpdatePanel ID="upHeadOfficeView" runat="server">
        <ContentTemplate>
            <div id="ModalOverlay" class="modal_popup_overlay">
            </div>
            <asp:Panel ID="pnlUpdateDB" runat="server" DefaultButton="btnSaveDB" meta:resourcekey="pnlUpdateDBResource1">
                <div id="Display" class="modal_popup_logo">
                    <div class="div100 modal_popup_title">
                        <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                            Database Connection
                        </div>
                        <div class="floatright">
                            <img alt="" onclick="javascript:HideDisplayPopUp();" class="handcursor" src="../../App_Themes/MainTheme/images/PopupClose.png"
                                id="img2" title="Close" />
                        </div>
                    </div>
                    <div style="width: 93%; margin: 5px 25px; float: left">
                        <div style="text-align: center" class="divrow">
                            <div class="divcolumn fieldcaption width35per">
                                <asp:Literal ID="Literal13" runat="server" Text="Host Name *" meta:resourcekey="Literal13Resource1"></asp:Literal>
                            </div>
                            <div class="divcolpad">
                                <asp:TextBox ID="txtHostName" runat="server" CssClass="textbox manfield" MaxLength="50"
                                    ToolTip="Enter Host Name" meta:resourcekey="txtHostNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfHostName" CssClass="requiredcolor" runat="server"
                                    Text="*" ErrorMessage="Host Name is required" SetFocusOnError="True" ControlToValidate="txtHostName"
                                    Display="Dynamic" meta:resourcekey="rvfHostNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="text-align: center" class="divrow">
                            <div class="divcolumn fieldcaption width35per">
                                <asp:Literal ID="Literal14" runat="server" Text="Database Name *" meta:resourcekey="Literal14Resource1"></asp:Literal>
                            </div>
                            <div class="divcolpad">
                                <asp:TextBox ID="txtDBName" runat="server" CssClass="textbox manfield" MaxLength="30"
                                    ToolTip="Enter Database Name" meta:resourcekey="txtDBNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfDBName" runat="server" Text="*" ErrorMessage="Database Name is required"
                                    CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtDBName"
                                    Display="Dynamic" meta:resourcekey="rvfDBNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="text-align: center" class="divrow">
                            <div class="divcolumn fieldcaption width35per">
                                <asp:Literal ID="Literal18" runat="server" Text="Database Username *" meta:resourcekey="Literal18Resource1"></asp:Literal>
                            </div>
                            <div class="divcolpad">
                                <asp:TextBox ID="txtDBUserName" runat="server" CssClass="textbox manfield" MaxLength="30"
                                    ToolTip="Enter Username" meta:resourcekey="txtDBUserNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfDBUserName" runat="server" Text="*" ErrorMessage="Username is required"
                                    CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtDBUserName"
                                    Display="Dynamic" meta:resourcekey="rvfDBUserNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="text-align: center" class="divrow">
                            <div class="divcolumn fieldcaption width35per">
                                <asp:Literal ID="Literal19" runat="server" Text="Database Password *" meta:resourcekey="Literal19Resource1"></asp:Literal>
                            </div>
                            <div class="divcolpad">
                                <asp:TextBox ID="txtDBPassword" runat="server" CssClass="textbox manfield" TextMode="Password"
                                    MaxLength="30" ToolTip="Enter Password" meta:resourcekey="txtDBPasswordResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfDBPassword" runat="server" Text="*" ErrorMessage="Password is required"
                                    CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtDBPassword"
                                    Display="Dynamic" meta:resourcekey="rvfDBPasswordResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="text-align: center" class="divrow">
                            <asp:Button ID="btnSaveDB" OnClick="btnSaveDB_Click" runat="server" CssClass="button"
                                Text="Save" ToolTip="Click here to save head office Database Details" meta:resourcekey="btnSaveDBResource1">
                            </asp:Button>
                            <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" OnClientClick="javascript:HideDisplayPopUp()"
                                ToolTip="Click here to close" CausesValidation="False" meta:resourcekey="hlkCloseResource1">
                            </asp:Button>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function ShowDisplayPopUp(modal) {
            $("#ModalOverlay").show();
            $("#Display").fadeIn(300);
            document.getElementById('<%=txtHostName.ClientID %>').focus();
            if (modal) {
                $("#ModalOverlay").unbind("click");
            }
            else {
                $("#ModalOverlay").click(function (e) {

                });
            }
        }

        function HideDisplayPopUp() {
            $("#ModalOverlay").hide();
            $("#Display").fadeOut(300);
        }
    </script>
</asp:Content>
