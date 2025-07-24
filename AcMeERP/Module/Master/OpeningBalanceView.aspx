<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="OpeningBalanceView.aspx.cs" Inherits="AcMeERP.Module.Master.OpeningBalanceView" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upOPBalanceView" runat="server">
        <ContentTemplate>
            <div class="criteriaribben">

                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Branch </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbBranch" runat="server" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged"
                            Width="200px" meta:resourcekey="cmbBranchResource2" AutoPostBack="true">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Project </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbProject" runat="server" Width="300px" meta:resourcekey="cmbProjectResource2">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-left: 5px; padding-bottom: 2px; padding-right: 5px">
                        <dx:ASPxButton ID="btnLoad" Text="Go" OnClick="btnLoad_Click" AutoPostBack="true"
                            runat="server" Height="20px" Width="100px" Image-Url="~/App_Themes/MainTheme/images/go.png">
                        </dx:ASPxButton>
                    </div>
                </div>
            </div>
            <div>
                <div>
                <asp:Label ID="lblBalance" runat="server" Text="test"></asp:Label>
                    <dx:ASPxGridView ID="gvOpeningBalanceView" runat="server" Theme="Office2010Blue"
                        Width="100%" OnLoad="gvOpeningBalanceView_Load">
                        <Columns>
                            <dx:GridViewDataTextColumn Name="colCode" Caption="Code" FieldName="LEDGER_CODE"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colLedger" Caption="Ledger" FieldName="LEDGER_NAME"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colGroup" Caption="Group" FieldName="LEDGER_GROUP"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colAmount" Caption="O/P Balance" FieldName="AMOUNT"
                                VisibleIndex="3">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowSort="false" />
                        <SettingsPager Position="TopAndBottom">
                            <PageSizeItemSettings Items="10,20" Visible="true">
                            </PageSizeItemSettings>
                        </SettingsPager>
                    </dx:ASPxGridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
