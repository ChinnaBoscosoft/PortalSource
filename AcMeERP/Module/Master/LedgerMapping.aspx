<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="LedgerMapping.aspx.cs" Inherits="AcMeERP.Module.Master.LedgerMapping" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function chkSelectAll_CheckChanged(s, e) {
            gvLedger.SelectAllRowsOnPage(s.GetChecked());
        }
        function SelectionChanged(s, e) {
            colSelectAll.SetChecked(false);
        }
        function DisableSave(s, e) {
            var count = gvLedger.GetSelectedRowCount();
            if (count > 0) {
                document.getElementById('divSave').style.display = 'block';
            }
            else {
                document.getElementById('divSave').style.display = 'none';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div>
        <div class="criteriaribben">
            <div class="floatright" style="padding-right: 5px; padding-left: 5px">
                <dx:ASPxButton ID="btnSaveOnTop" Text="Save" OnClick="btnSaveOnTop_Click" Height="20px"
                    Theme="Office2010Blue" runat="server" />
            </div>
            <div class="floatright" style="padding-right: 5px">
                <dx:ASPxComboBox ID="cmbProjectCategory" runat="server" OnSelectedIndexChanged="cmbProjectCategory_cmbBranch"
                    IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" AutoPostBack="true"
                    Width="250px">
                </dx:ASPxComboBox>
            </div>
            <div class="floatright">
                <span class="bold" style="padding: 5px;">Project Category</span>
            </div>
            <div class="Note floatleft">
                <span class="red"><strong>
                    <asp:Literal runat="server" ID="ltrlSelected" Text=""></asp:Literal></strong></span>
            </div>
        </div>
        <div>
            <dx:ASPxGridView ID="gvLedger" runat="server" SettingsBehavior-AllowDragDrop="false"
                IncrementalFilteringMode="Contains" Settings-ShowFilterRow="true" Theme="Office2010Blue"
                Width="100%" OnLoad="gvLedger_Load" ClientInstanceName="gvLedger" KeyFieldName="LEDGER_ID"
                AutoGenerateColumns="False">
                <Columns>
                    <dx:GridViewCommandColumn Name="ColSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                        Width="25px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="colSelectAll" runat="server" ClientInstanceName="colSelectAll"
                                Theme="Office2010Blue" ToolTip="Select/Unselect all rows on the page">
                                <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn Name="colLedger" Caption="Ledger" VisibleIndex="1" FieldName="LEDGER_NAME">
                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colLedgerGroup" Caption="Ledger Group" VisibleIndex="2"
                        FieldName="LEDGER_GROUP">
                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colLedgeId" FieldName="LEDGER_ID" Visible="false">
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsPager Position="TopAndBottom" PageSize="300">
                    <PageSizeItemSettings Items="300,400,500" Visible="True">
                    </PageSizeItemSettings>
                </SettingsPager>
                <SettingsBehavior AllowSort="true" />
                <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="true">
                </Settings>
            </dx:ASPxGridView>
        </div>
        <br />
        <div class="Note floatleft">
            <span class="red" style="padding: 3px;">
                <asp:Label ID="lblDefaultCaptions" runat="server" Text="lblDefaultCaption"></asp:Label></span>
        </div>
        <br />
        <div>
            <span style="padding: 5px">
                <asp:Label ID="lblDefaultLedger" runat="server" Text="lblDefaultLedger"></asp:Label></span>
        </div>
        <br />
        <div id="divSave" class="textcenter">
            <dx:ASPxButton ID="btnSave" Text="Save" OnClick="btnSave_Click" Theme="Office2010Blue"
                runat="server" /> 
                <dx:ASPxButton ID="btnDefaultLedgerMapping" 
                Text="Map Default Ledger for Project Category" 
                OnClick="btnDefaultLedgerMapping_Click" Theme="Office2010Blue"
                runat="server" />
        </div>
    </div>
</asp:Content>
