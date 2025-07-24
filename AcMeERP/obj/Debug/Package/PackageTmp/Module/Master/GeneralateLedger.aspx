<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="GeneralateLedger.aspx.cs" Inherits="AcMeERP.Module.Master.GeneralateLedger" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upGeneralateLedger" runat="server">
        <ContentTemplate>
            <div class="div100" style="border: 1px solid #EBEBEB; box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);">
                <div class="criteriaribben" runat="server" id="divLedger">
                    <div class="btnribben">
                        <dx:ASPxButton ID="btnAddLedger" runat="server" CausesValidation="False" Theme="iOS"
                            Text="Add" OnClick="btnAddLedger_Click" meta:resourcekey="btnAddLedgerResource1">
                            <Image Url="~/App_Themes/MainTheme/images/add16.png">
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnEditLedger" runat="server" CausesValidation="False" Theme="iOS"
                            Text="Edit" OnClick="btnEditLedger_Click" meta:resourcekey="btnAddLedgerResource1">
                            <Image Url="~/App_Themes/MainTheme/images/Edit.png">
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnDeleteLedger" runat="server" Theme="iOS" Text="Delete" CausesValidation="False"
                            OnClick="btnDeleteLedger_Click" meta:resourcekey="btnAddLedgerResource1">
                            <Image Url="~/App_Themes/MainTheme/images/Delete1.png">
                            </Image>
                            <ClientSideEvents Click="function(s, e){ e.processOnServer = confirm('Are you sure to delete?');}" />
                        </dx:ASPxButton>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="float: left; width: 30%; margin-right: 5px">
                        <div style="background-color: transparent; color: #003C72; text-align: center; padding-top: 8px;">
                            <span class="bold" style="padding: 5px; font-size: 14px;">Generalate Ledger Group</span>
                        </div>
                        <asp:TreeView ID="trlGeneralateLedger" runat="server" BorderColor="#CD8F3E" ForeColor="#3A4F63"
                            BorderStyle="Solid" NodeWrap="True" BorderWidth="1px" meta:resourcekey="trlGeneralateLedgerResource1"
                            OnSelectedNodeChanged="trlGeneralateLedger_SelectedNodeChanged">
                            <LevelStyles>
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                            </LevelStyles>
                            <SelectedNodeStyle ForeColor="#0B57BC" />
                        </asp:TreeView>
                    </div>
                    <%--<div style="float: left; width: 24%; margin-right: 5px">
                        <asp:TreeView ID="trlChild" runat="server" BorderColor="#CD8F3E" ForeColor="#3A4F63"
                            BorderStyle="Solid" NodeWrap="True" BorderWidth="1px" meta:resourcekey="trlChildResource1"
                            OnSelectedNodeChanged="trlChild_SelectedNodeChanged">
                            <LevelStyles>
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                            </LevelStyles>
                            <SelectedNodeStyle ForeColor="#0B57BC" />
                        </asp:TreeView>
                    </div>--%>
                    <div style="float: left; width: 69%;">
                        <div style="background-color: transparent; color: #003C72; text-align: center; padding-top: 8px;">
                            <span class="bold" style="padding: 5px; font-size: 14px;">
                            <asp:Label ID="lblGeneralateLedgerGroup" runat="server"></asp:Label></span>
                        </div>
                        <dx:ASPxGridView ID="gvView" Width="100%" Theme="Office2010Blue" runat="server" AutoGenerateColumns="False"
                            meta:resourcekey="gvViewResource1" Style="margin-top: 17px" OnPageIndexChanged="gvView_PageIndexChanged">
                            <Columns>
                                <dx:GridViewDataTextColumn Name="colLedderCode" Width="50px" FieldName="LEDGER_CODE"
                                    VisibleIndex="0" meta:resourcekey="GridViewDataTextColumnResource1" ShowInCustomizationForm="True"
                                    Caption="Ledger Code">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Name="colLedderCode" Width="50px" FieldName="LEDGER_NAME"
                                    VisibleIndex="1" meta:resourcekey="GridViewDataTextColumnResource2" ShowInCustomizationForm="True"
                                    Caption="Ledger Name">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" />
                            <SettingsBehavior AllowSort="False" AllowDragDrop="False" />
                            <SettingsPager>
                                <PageSizeItemSettings Visible="True">
                                </PageSizeItemSettings>
                            </SettingsPager>
                        </dx:ASPxGridView>
                        <asp:HiddenField ID="hdnValue" runat="server" />
                    </div>
                    <div id="ModalOverlay" class="modal_popup_overlay">
                    </div>
                    <asp:Panel ID="pnlGeneralateLedgerAdd" DefaultButton="btnSaveUser" runat="server"
                        meta:resourcekey="pnlGeneralateLedgerAddResource1">
                        <div id="Display" class="modal_popup_logo" style="width: 515px;">
                            <div class="div100 modal_popup_title">
                                <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                                    Generalate Ledger Group
                                </div>
                                <div class="floatright">
                                    <img alt="" onclick="javascript:HideDisplayPopUp();" class="handcursor" src="../../App_Themes/MainTheme/images/PopupClose.png"
                                        id="img2" title="Close" />
                                </div>
                            </div>
                            <div style="margin-left: 10px; width: 80%">
                                <div class="divrow red" style="text-align: center">
                                    <asp:Label ID="lblMessage" runat="server" meta:resourcekey="lblMessageResource1"></asp:Label>
                                </div>
                                <div class="divrow">
                                    <div class="divcolumn fieldcaption" style="width: 40%;">
                                        <asp:Literal ID="Literal4" runat="server" Text="Parent ledger *" meta:resourcekey="Literal4Resource1"></asp:Literal>
                                    </div>
                                    <div class="divcolpad">
                                        <asp:DropDownList ID="ddlParent" runat="server" CssClass="combobox" meta:resourcekey="ddlParentResource1">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="cmpRl" runat="server" ErrorMessage="Select parent ledger"
                                            ControlToValidate="ddlParent" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                            CssClass="requiredcolor" meta:resourcekey="cmpRlResource1" Text="*"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="divrow">
                                    <div class="divcolumn fieldcaption" style="width: 40%;">
                                        <asp:Literal ID="Literal1" runat="server" Text="Code *" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                    </div>
                                    <div class="divcolpad">
                                        <asp:TextBox ID="txtCode" runat="server" Width="200px" CssClass="textbox manfield"
                                            MaxLength="10" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCode" runat="server" Text="*" ErrorMessage="Ledger Code is required"
                                            CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtCode" Display="Dynamic"
                                            meta:resourcekey="rfvCodeResource1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="divrow">
                                    <div class="divcolumn fieldcaption" style="width: 40%;">
                                        <asp:Literal ID="Literal2" runat="server" Text="Name *" meta:resourcekey="Literal2Resource1"></asp:Literal>
                                    </div>
                                    <div class="divcolpad">
                                        <asp:TextBox ID="txtLedgerName" runat="server" Width="200px" CssClass="textbox manfield"
                                            MaxLength="60" meta:resourcekey="txtLedgerNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtLedgerName" runat="server" Text="*" ErrorMessage="Ledger Name is required"
                                            CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtLedgerName"
                                            Display="Dynamic" meta:resourcekey="rfvtxtLedgerNameResource1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div style="text-align: center" class="divrow"></div>
                                <div style="text-align: center" class="divrow">
                                    <div class="divcolumn fieldcaption" style="width: 40%;">
                                    </div>
                                    <div class="divcolpad">
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
                                <div style="text-align: center" class="divrow"></div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="GeneralateLedgerGp" runat="server" ShowSummary="False"
        ShowMessageBox="True" meta:resourcekey="GeneralateLedgerGpResource1"></asp:ValidationSummary>
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

        function ShowConfirmationMessage(model ) {
            if (confirm("test")) {
                document.getElementById('<%=hdnValue.ClientID %>').value = "true";
            }
            else {
                document.getElementById('<%= hdnValue.ClientID %>').value = "false";
            }
        }
      
    </script>
</asp:Content>
