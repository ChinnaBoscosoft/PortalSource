<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="LedgerGroup.aspx.cs" Inherits="AcMeERP.Module.Master.LedgerGroup"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upLedgerGroup" runat="server">
        <ContentTemplate>
            <div class="div100" style="border: 1px solid #EBEBEB; box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);">
                <div class="criteriaribben" runat="server" id="divLedgerGroup">
                    <div class="btnribben">
                        <dx:ASPxButton ID="btnAddGroup" runat="server" CausesValidation="False" Theme="iOS"
                            Text="Add" OnClick="btnAddGroup_Click" meta:resourcekey="btnAddGroupResource1">
                            <Image Url="~/App_Themes/MainTheme/images/add16.png">
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnEditGroup" runat="server" CausesValidation="False" Theme="iOS"
                            Text="Edit" OnClick="btnEditGroup_Click" meta:resourcekey="btnEditGroupResource1">
                            <Image Url="~/App_Themes/MainTheme/images/Edit.png">
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnDeleteGroup" runat="server" Theme="iOS" Text="Delete" CausesValidation="False"
                            OnClick="btnDeleteGroup_Click" meta:resourcekey="btnDeleteGroupResource1">
                            <Image Url="~/App_Themes/MainTheme/images/Delete1.png">
                            </Image>
                            <ClientSideEvents Click="function(s, e){ e.processOnServer = confirm('Are you sure to delete?');}" />
                        </dx:ASPxButton>
                    </div>
                </div>
                <div style="float: left; width: 100%">
                    <div style="float: left; width: 24%; margin-right: 5px">
                        <asp:TreeView ID="trlLedgerGroup" runat="server" OnSelectedNodeChanged="trlLedgerGroup_SelectedNodeChanged"
                            BorderColor="#CD8F3E" ForeColor="#3A4F63" BorderStyle="Solid" NodeWrap="True"
                            BorderWidth="1px" meta:resourcekey="trlLedgerGroupResource1">
                            <LevelStyles>
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                            </LevelStyles>
                            <SelectedNodeStyle ForeColor="#0B57BC" />
                        </asp:TreeView>
                    </div>
                    <div style="float: left; width: 24%; margin-right: 5px">
                        <asp:TreeView ID="trlChild" runat="server" OnSelectedNodeChanged="trlChild_SelectedNodeChanged"
                            BorderColor="#CD8F3E" ForeColor="#3A4F63" BorderStyle="Solid" NodeWrap="True"
                            BorderWidth="1px" meta:resourcekey="trlChildResource1">
                            <LevelStyles>
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                            </LevelStyles>
                            <SelectedNodeStyle ForeColor="#0B57BC" />
                        </asp:TreeView>
                    </div>
                    <div style="float: left; width: 50%; margin-top: 18px">
                        <dx:ASPxGridView ID="gvView" Width="100%" Theme="Office2010Blue" runat="server" OnPageIndexChanged="gvView_PageIndexChanged"
                            AutoGenerateColumns="False" meta:resourcekey="gvViewResource1">
                            <Columns>
                                <dx:GridViewDataTextColumn Name="colLedderCode" Width="50px" FieldName="Ledger Code"
                                    VisibleIndex="0" meta:resourcekey="GridViewDataTextColumnResource1" ShowInCustomizationForm="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Name="colLedderCode" Width="50px" FieldName="Ledger Name"
                                    VisibleIndex="1" meta:resourcekey="GridViewDataTextColumnResource2" ShowInCustomizationForm="True">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" />
                            <SettingsBehavior AllowSort="False" AllowDragDrop="False" />
                            <SettingsPager>
                                <PageSizeItemSettings Visible="True">
                                </PageSizeItemSettings>
                            </SettingsPager>
                        </dx:ASPxGridView>
                    </div>
                </div>
                <div id="ModalOverlay" class="modal_popup_overlay">
                </div>
                <asp:Panel ID="pnlLedgerGAdd" DefaultButton="btnSaveUser" runat="server" meta:resourcekey="pnlLedgerGAddResource1">
                    <div id="Display" class="modal_popup_logo" style="width:515px;">
                        <div class="div100 modal_popup_title">
                            <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                                Ledger Group
                            </div>
                            <div class="floatright">
                                <img alt="" onclick="javascript:HideDisplayPopUp();" class="handcursor" src="../../App_Themes/MainTheme/images/PopupClose.png"
                                    id="img2" title="Close" />
                            </div>
                        </div>
                        <div style="margin-left: 80px; width: 80%">
                            <div class="divrow red" style="text-align: center">
                                <asp:Label ID="lblMessage" runat="server" meta:resourcekey="lblMessageResource1"></asp:Label>
                            </div>
                            <div class="divrow">
                                <div class="divcolumn fieldcaption" style="width: 37%;">
                                    <asp:Literal ID="Literal4" runat="server" Text="Under the Group *" meta:resourcekey="Literal4Resource1"></asp:Literal>
                                </div>
                                <div class="divcolpad">
                                    <asp:DropDownList ID="ddlgroup" runat="server" CssClass="combobox" meta:resourcekey="ddlgroupResource1">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="cmpRl" runat="server" ErrorMessage="Select ledger group"
                                        ControlToValidate="ddlgroup" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                        CssClass="requiredcolor" meta:resourcekey="cmpRlResource1" Text="*"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="divrow">
                                <div class="divcolumn fieldcaption" style="width: 37%;">
                                    <asp:Literal ID="Literal1" runat="server" Text="Code" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                </div>
                                <div class="divcolpad">
                                    <asp:TextBox ID="txtCode" runat="server" Width="200px" CssClass="textbox"
                                        MaxLength="5" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="divrow">
                                <div class="divcolumn fieldcaption" style="width: 37%;">
                                    <asp:Literal ID="Literal2" runat="server" Text="Name *" meta:resourcekey="Literal2Resource1"></asp:Literal>
                                </div>
                                <div class="divcolpad">
                                    <asp:TextBox ID="txtLedgerName" runat="server" Width="200px" CssClass="textbox manfield"
                                        MaxLength="75" meta:resourcekey="txtLedgerNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtLedgerName" runat="server" Text="*" ErrorMessage="Ledger Name is required"
                                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtLedgerName"
                                        Display="Dynamic" meta:resourcekey="rfvtxtLedgerNameResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div style="text-align: center" class="divrow">
                                <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" runat="server" CssClass="button"
                                    Text="Save" ToolTip="Click here to save ledger group Details" meta:resourcekey="btnSaveUserResource1">
                                </asp:Button>
                                <asp:Button ID="btnNew" OnClick="btnNew_Click" CausesValidation="False" runat="server"
                                    CssClass="button" Text="New" ToolTip="Click here to clear group Details" meta:resourcekey="btnNewResource1">
                                </asp:Button>
                                <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close"
                                    CausesValidation="False" OnClientClick="javascript:HideDisplayPopUp()" meta:resourcekey="btnCloseResource1">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="LedgerGp" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="LedgerGpResource1"></asp:ValidationSummary>
    <script language="javascript" type="text/javascript">
        function ShowDisplayPopUp(modal) {
            $("#ModalOverlay").show();
            $("#Display").fadeIn(300);
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
