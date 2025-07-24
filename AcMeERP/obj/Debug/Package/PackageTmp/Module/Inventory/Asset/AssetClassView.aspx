<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AssetClassView.aspx.cs" Inherits="AcMeERP.Module.Inventory.Asset.AssetClassView" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upAssetClass" runat="server">
        <ContentTemplate>
            <div class="div100" style="border: 1px solid #EBEBEB; box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);">
                <div class="criteriaribben" runat="server" id="divLedgerGroup">
                    <div class="btnribben">
                        <dx:ASPxButton ID="btnAddClass" runat="server" CausesValidation="False" Theme="iOS"
                            Text="Add" OnClick="btnAddClass_Click">
                            <Image Url="~/App_Themes/MainTheme/images/add16.png">
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnEditClass" runat="server" CausesValidation="False" Theme="iOS"
                            Text="Edit" OnClick="btnEditClass_Click">
                            <Image Url="~/App_Themes/MainTheme/images/Edit.png">
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnDeleteClass" runat="server" Theme="iOS" Text="Delete" CausesValidation="False" OnClick="btnDeleteClass_Click">
                            <Image Url="~/App_Themes/MainTheme/images/Delete1.png">
                            </Image>
                            <ClientSideEvents Click="function(s, e){ e.processOnServer = confirm('Are you sure to delete?');}" />
                        </dx:ASPxButton>
                    </div>
                </div>
                <div style="float: left; width: 100%">
                    <div style="float: left; width: 24%; margin-right: 5px">
                        <asp:TreeView ID="trlAssetClass" runat="server" BorderColor="#CD8F3E" ForeColor="#3A4F63"
                            BorderStyle="Solid" NodeWrap="True" BorderWidth="1px" OnSelectedNodeChanged="trlAssetClass_SelectedNodeChanged">
                            <LevelStyles>
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                            </LevelStyles>
                            <SelectedNodeStyle ForeColor="#0B57BC" />
                        </asp:TreeView>
                    </div>
                    <div style="float: left; width: 24%; margin-right: 5px">
                        <asp:TreeView ID="trlAssetClassChild" runat="server" BorderColor="#CD8F3E" ForeColor="#3A4F63"
                            BorderStyle="Solid" NodeWrap="True" BorderWidth="1px" OnSelectedNodeChanged="trlAssetClassChild_SelectedNodeChanged">
                            <LevelStyles>
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                                <asp:TreeNodeStyle ImageUrl="~/App_Themes/MainTheme/images/folder.png" Font-Underline="False" />
                            </LevelStyles>
                            <SelectedNodeStyle ForeColor="#0B57BC" />
                        </asp:TreeView>
                    </div>
                    <div style="float: left; width: 50%; margin-top: 18px">
                        <dx:ASPxGridView ID="gvAssetClassView" Width="100%" Theme="Office2010Blue" runat="server" AutoGenerateColumns="False" OnPageIndexChanged="gvAssetClassView_PageIndexChanged">
                            
                            <Columns>
                                <dx:GridViewDataTextColumn Name="colParentClass" Width="50px" FieldName="Parent Class"
                                    VisibleIndex="0" ShowInCustomizationForm="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Name="colAssetClass" Width="50px" FieldName="Asset Class"
                                    VisibleIndex="1" ShowInCustomizationForm="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Name="colAssetItem" Width="50px" FieldName="Asset Item"
                                    VisibleIndex="1" ShowInCustomizationForm="True">
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
                <asp:Panel ID="pnlAssetClassId" DefaultButton="btnSaveAssetClass" runat="server">
                    <div id="Display" class="modal_popup_logo" style="width: 515px;">
                        <div class="div100 modal_popup_title">
                            <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                                Asset Class
                            </div>
                            <div class="floatright">
                                <img alt="" onclick="javascript:HideDisplayPopUp();" class="handcursor" src="../../../App_Themes/MainTheme/images/PopupClose.png"
                                    id="img2" title="Close" />
                            </div>
                        </div>
                        <div style="margin-left: 80px; width: 80%">
                            <div class="divrow red" style="text-align: center">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>
                            <div class="divrow">
                                <div class="divcolumn fieldcaption" style="width: 37%;">
                                    <asp:Literal ID="Literal4" runat="server" Text="Parent Class *"></asp:Literal>
                                </div>
                                <div class="divcolpad">
                                    <asp:DropDownList ID="ddlParentClass" runat="server" CssClass="combobox">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="cmpRl" runat="server" ErrorMessage="Select parent class"
                                        ControlToValidate="ddlParentClass" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                        CssClass="requiredcolor" Text="*"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="divrow">
                                <div class="divcolumn fieldcaption" style="width: 37%;">
                                    <asp:Literal ID="Literal2" runat="server" Text="Class Name *"></asp:Literal>
                                </div>
                                <div class="divcolpad">
                                    <asp:TextBox ID="txtClassName" runat="server" Width="200px" CssClass="textbox manfield"
                                        MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtClassName" runat="server" Text="*" ErrorMessage="Class Name is required"
                                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtClassName"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="divrow">
                                <div class="divcolumn fieldcaption" style="width: 37%;">
                                    <asp:Literal ID="ltrlDepr" runat="server" Text="Depreciation Method"></asp:Literal>
                                </div>
                                <div class="divcolpad">
                                    <asp:DropDownList ID="ddlDepreciation" runat="server" CssClass="combobox">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="divrow">
                                <div class="divcolumn fieldcaption" style="width: 37%;">
                                    <asp:Literal ID="Literal1" runat="server" Text="Depreciation %"></asp:Literal>
                                </div>
                                <div class="divcolpad">
                                    <asp:TextBox ID="txtDepPercent" runat="server" Width="200px" CssClass="textbox" MaxLength="5"></asp:TextBox>
                                </div>
                            </div>
                            <div style="text-align: center" class="divrow">
                                <asp:Button ID="btnSaveAssetClass" OnClick="btnSaveAssetClass_Click" runat="server" CssClass="button"
                                    Text="Save" ToolTip="Click here to save asset class Details"></asp:Button>
                                <asp:Button ID="btnNew" OnClick="btnNew_Click" CausesValidation="False" runat="server"
                                    CssClass="button" Text="New" ToolTip="Click here to clear asset class"></asp:Button>
                                <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close"
                                    CausesValidation="False" OnClientClick="javascript:HideDisplayPopUp()"></asp:Button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsAssetclass" runat="server" ShowSummary="False" ShowMessageBox="True">
    </asp:ValidationSummary>
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
