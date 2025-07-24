<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAccountBalance.ascx.cs"
    Inherits="AcMeERP.WebControl.ucAccountBalance" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<div class="opribben">
    <div style="width: 15%; float: left; color: Red">
        <div class="optextitem">
            <dx:ASPxLabel ID="lblBalance" runat="server" Text="" Style="font-weight: bold">
            </dx:ASPxLabel>
        </div>
    </div>
    <div class="opcol">
        <div class="optextitem">
            <dx:ASPxLabel ID="lblCash" runat="server" Text="Cash" Style="font-weight: bold;">
            </dx:ASPxLabel>
        </div>
        <div style="float: left; padding: 3px 5px 5px">
            <%--<asp:ImageButton ID="btnCash" runat="server" ImageUrl="~/App_Themes/MainTheme/images/Money-icon.png" />--%>
            <dx:ASPxButton ID="btnCashBalance" runat="server" AutoPostBack="False" AllowFocus="False"
                RenderMode="Link" EnableTheming="False">
                <Image>
                    <SpriteProperties CssClass="btnCash" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxPopupControl ID="pucCashBalance" runat="server" CloseAction="OuterMouseClick"
                Theme="Office2010Blue"  PopupElementID="btnCashBalance"
                PopupVerticalAlign="Below" PopupHorizontalAlign="LeftSides" AllowDragging="false"
                ShowFooter="false" Width="310px" Height="160" HeaderText="Cash Balance" ClientInstanceName="ClientPopupControl">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server">
                        <dx:ASPxGridView ID="gvCashBalance" runat="server" Width="100%" Theme="Office2010Blue">
                            <Columns>
                                <dx:GridViewDataColumn Name="colCashLedger" FieldName="LEDGER_NAME" Caption="Cash Ledger"
                                    VisibleIndex="0">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn Name="colCashAmount" FieldName="AMOUNT" Caption="Amount"
                                    VisibleIndex="1">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataColumn Name="colGroupId" FieldName="GROUP_ID" Caption="Group Id"
                                    Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Name="colTrasnMode" FieldName="TRANSMODE" Visible="true" VisibleIndex="2" Caption=" " Width="25px">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Settings VerticalScrollBarMode="Visible" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsBehavior AllowSort="false" />
                        </dx:ASPxGridView>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
        <div class="optextitem">
            <dx:ASPxLabel ID="lblCashBalance" runat="server" Text="0.00 DR" Style="font-weight: bold;
                color: Green">
            </dx:ASPxLabel>
        </div>
    </div>
    <div class="opcol">
        <div class="optextitem">
            <dx:ASPxLabel ID="lblBank" runat="server" Text="Bank" Style="font-weight: bold">
            </dx:ASPxLabel>
        </div>
        <div style="float: left; padding: 3px 5px 5px">
            <dx:ASPxButton ID="btnBankBalance" runat="server" AutoPostBack="False" AllowFocus="False"
                RenderMode="Link" EnableTheming="False">
                <Image>
                    <SpriteProperties CssClass="btnBank" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxPopupControl ID="pucBankBalance" runat="server" CloseAction="OuterMouseClick"
                Theme="Office2010Blue"  PopupElementID="btnBankBalance"
                PopupVerticalAlign="Below" PopupHorizontalAlign="LeftSides" AllowDragging="false"
                ShowFooter="false" Width="310px" Height="160px" HeaderText="Bank Balance" ClientInstanceName="ClientPopupControl">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <dx:ASPxGridView ID="gvBankBalance" runat="server" Width="100%" Theme="Office2010Blue">
                            <Columns>
                                <dx:GridViewDataColumn Name="colBankLedger" Caption="Bank Ledger" FieldName="LEDGER_NAME"
                                    VisibleIndex="0">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn Name="colBankAmount" Caption="Amount" FieldName="AMOUNT"
                                    VisibleIndex="1">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataColumn Name="colGroupId" FieldName="GROUP_ID" Caption="Group Id"
                                    Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Name="colTrasnMode" FieldName="TRANSMODE" Visible="true" VisibleIndex="2" Caption=" " Width="25px">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Settings VerticalScrollBarMode="Visible" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsBehavior AllowSort="false" />
                        </dx:ASPxGridView>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
        <div class="optextitem">
            <dx:ASPxLabel ID="lblBankBalance" runat="server" Text="0.00 DR" Style="font-weight: bold;
                color: Green">
            </dx:ASPxLabel>
        </div>
    </div>
    <div>
        <div class="optextitem">
            <dx:ASPxLabel ID="lblFixedDeposit" runat="server" Text="Fixed Deposit" Style="font-weight: bold">
            </dx:ASPxLabel>
        </div>
        <div class="opimgitem">
            <dx:ASPxButton ID="btnFd" runat="server" AutoPostBack="False" AllowFocus="False"
                RenderMode="Link" EnableTheming="False">
                <Image>
                    <SpriteProperties CssClass="btnFd" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxPopupControl ID="pucFdBalance" runat="server" CloseAction="OuterMouseClick"
                Theme="Office2010Blue"  PopupElementID="btnFd"
                PopupVerticalAlign="Below" PopupHorizontalAlign="RightSides" AllowDragging="false"
                ShowFooter="false" Width="310px" Height="160px" HeaderText="Fixed Deposit" ClientInstanceName="ClientPopupControl">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <dx:ASPxGridView ID="gvFdBalance" runat="server" Width="100%" Theme="Office2010Blue">
                            <Columns>
                                <dx:GridViewDataColumn Name="colFdLedger" Caption="FD Ledger" FieldName="LEDGER_NAME"
                                    VisibleIndex="0">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn Name="colFdAmount" Caption="Amount" FieldName="AMOUNT"
                                    VisibleIndex="1">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataColumn Name="colGroupId" Caption="Group Id" FieldName="GROUP_ID"
                                    Visible="false">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn Name="colTrasnMode" FieldName="TRANSMODE" Visible="true" VisibleIndex="2" Caption=" " Width="25px">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Settings VerticalScrollBarMode="Visible" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsBehavior AllowSort="false" />
                        </dx:ASPxGridView>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
        <div class="optextitem">
            <dx:ASPxLabel ID="lblFdBalance" runat="server" Text="0.00 DR" Style="font-weight: bold;
                color: Green;">
            </dx:ASPxLabel>
        </div>
    </div>
</div>
