<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="MapSubsidyLedgers.aspx.cs" Inherits="AcMeERP.Module.Master.MapSubsidyLedgers" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function chkSelectAll_CheckChanged(s, e) {
            gvLedgerList.SelectAllRowsOnPage(s.GetChecked());
        }
        function SelectionChanged(s, e) {
            chkSelectAll.SetChecked(false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div>
        <div class="criteriaribben">
            <div class="floatright" style="padding-right: 5px; padding-left: 5px">
                <dx:ASPxButton ID="btnSave" Text="Save" Height="20px" Theme="Office2010Blue" runat="server" TabIndex="2"
                    OnClick="btnSave_Click" />
            </div>
            <div class="floatright" style="padding-right: 5px">
                <dx:ASPxComboBox ID="cmbLedgerType" runat="server" DropDownStyle="DropDownList" AutoPostBack="true" TabIndex="1"
                    IncrementalFilteringMode="Contains" Width="250px" OnSelectedIndexChanged="cmbLedgerType_SelectedIndexChanged">
                </dx:ASPxComboBox>
            </div>
            <div class="floatright">
                <span class="bold" style="padding: 5px;">Ledger Type</span>
            </div>
        </div>
        <div>
            <dx:ASPxGridView ID="gvLedgerList" runat="server" SettingsBehavior-AllowDragDrop="false"
                IncrementalFilteringMode="Contains" Settings-ShowFilterRow="true" Theme="Office2010Blue"
                Width="100%" ClientInstanceName="gvLedgerList" KeyFieldName="LEDGER_ID" AutoGenerateColumns="False">
                <Columns>
                    <dx:GridViewCommandColumn Name="colchkSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                        Width="20px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="chkSelectAll" runat="server" ToolTip="Select/Unselect all rows on the page"
                                Theme="Office2010Blue">
                                <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="Ledger Id" FieldName="LEDGER_ID" Name="LEDGER_ID"
                        Visible="False" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Ledger Name" FieldName="Name" Name="NAME" VisibleIndex="2">
                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                        <CellStyle Wrap="True">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn Name="colSelect" FieldName="SELECT" VisibleIndex="3" Visible="false">
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsPager Position="TopAndBottom" PageSize="300">
                    <PageSizeItemSettings Items="300,400,500" Visible="True">
                    </PageSizeItemSettings>
                </SettingsPager>
                <SettingsBehavior AllowSort="true" />
                <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="true"></Settings>
            </dx:ASPxGridView>
        </div>
        <br />
        <div id="divSave" class="textcenter">
            <dx:ASPxButton ID="btnSaveLedger" Text="Save" Theme="Office2010Blue" runat="server"
                OnClick="btnSaveLedger_Click" />
        </div>
    </div>
</asp:Content>
