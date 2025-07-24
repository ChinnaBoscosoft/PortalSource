<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="SDBGeneralateLedgerMapping.aspx.cs" Inherits="AcMeERP.Module.Master.SDBGeneralateLedgerMapping" %>

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
                <dx:ASPxButton ID="btnSaveOnTop" Text="Save" Height="20px" Theme="Office2010Blue"
                    runat="server" OnClick="btnSaveOnTop_Click" />
            </div>
            <div class="floatright" style="padding-right: 5px">
                <dx:ASPxComboBox ID="cmbProjectCategoryLedger" runat="server" IncrementalFilteringMode="Contains"
                    AutoPostBack="True" Width="250px" 
                    OnSelectedIndexChanged="cmbProjectCategoryLedger_SelectedIndexChanged">
                </dx:ASPxComboBox>
            </div>
            <div class="floatright">
                <span class="bold" style="padding: 5px;">Project Catogory Group</span>
            </div>
            <div class="Note floatleft">
                <span class="red"><strong>
                    <asp:Literal runat="server" ID="ltrlSelected" Text=""></asp:Literal></strong></span>
            </div>
        </div>
        <div>
            <dx:ASPxGridView ID="gvGeneralateLedger" runat="server" SettingsBehavior-AllowDragDrop="false"
                IncrementalFilteringMode="Contains" Settings-ShowFilterRow="true" Theme="Office2010Blue"
                KeyFieldName="LEDGER_ID" Width="100%" ClientInstanceName="gvLedger" AutoGenerateColumns="False"
                OnLoad="gvLedger_Load">
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" ShowClearFilterButton="True" 
                        Visible="False">
                    </dx:GridViewCommandColumn>
                     <dx:GridViewDataColumn Name="colLedgercode" Caption="Code" FieldName="LEDGER_CODE"
                        VisibleIndex="1">
                        <Settings HeaderFilterMode="CheckedList" AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colLedger" Caption="Ledger" FieldName="LEDGER_NAME"
                        VisibleIndex="2">
                        <Settings HeaderFilterMode="CheckedList" AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colLedgerGroup" Caption="Ledger Group" FieldName="LEDGER_GROUP"
                        VisibleIndex="3">
                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" 
                            AllowAutoFilter="False"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colLedgeId" FieldName="LEDGER_ID" Visible="false">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn Name="colGeneralateLedger" VisibleIndex="5" 
                        Width="170px" Caption="Generalate Ledger"
                    FieldName="CON_LEDGER_ID" Visible="true">
                    <Settings AllowAutoFilter="False"></Settings>
                    <DataItemTemplate>
                        <asp:DropDownList ID="ddlGeneralateLedger" runat="server" DataTextField="CON_LEDGER_NAME" DataValueField="CON_LEDGER_ID" OnInit="ddl_Init"></asp:DropDownList>  
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                </Columns>
               <%-- <SettingsPager Position="TopAndBottom" PageSize="300">
                    <PageSizeItemSettings Items="300,400,500" Visible="True">
                    </PageSizeItemSettings>
                </SettingsPager>--%>
                <%--<SettingsBehavior AllowSort="true" />--%>

<SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False"></SettingsBehavior>

                <SettingsPager Visible="False" Mode="ShowAllRecords">
                </SettingsPager>

                <Settings ShowFilterRow="false" />

                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
                    AllowInsert="False" />
            </dx:ASPxGridView>
        </div>
        <br />
        <div id="divSave" class="textcenter">
            <dx:ASPxButton ID="btnSave" Text="Save" Theme="Office2010Blue" runat="server" OnClick="btnSave_Click" />
        </div>
    </div>
</asp:Content>
