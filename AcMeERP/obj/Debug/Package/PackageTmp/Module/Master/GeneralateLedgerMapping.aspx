<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="GeneralateLedgerMapping.aspx.cs" Inherits="AcMeERP.Module.Master.GeneralateLedgerMapping" %>

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
                <dx:ASPxButton ID="btnSaveOnTop" Text="Save" Height="20px"
                    Theme="Office2010Blue" runat="server" onclick="btnSaveOnTop_Click" />
            </div>
            <div class="floatright" style="padding-right: 5px">
                <dx:ASPxComboBox ID="cmbGeneralateLedger" runat="server" 
                    IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" AutoPostBack="true"
                    Width="250px" 
                    onselectedindexchanged="cmbGeneralateLedger_SelectedIndexChanged">
                </dx:ASPxComboBox>
            </div>
            <div class="floatright">
                <span class="bold" style="padding: 5px;">Generalate Ledger Group</span>
            </div>
            <div class="Note floatleft">
                <span class="red"><strong>
                    <asp:Literal runat="server" ID="ltrlSelected" Text=""></asp:Literal></strong></span>
            </div>
        </div>
        <div>
            <dx:ASPxGridView ID="gvLedger" runat="server" SettingsBehavior-AllowDragDrop="false"
                IncrementalFilteringMode="Contains" Settings-ShowFilterRow="true" 
                Theme="Office2010Blue" KeyFieldName="LEDGER_ID"
                Width="100%" ClientInstanceName="gvLedger"
                AutoGenerateColumns="False" onload="gvLedger_Load">
                <columns>
                    <dx:GridViewCommandColumn Name="ColSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                        Width="25px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="colSelectAll" runat="server" ClientInstanceName="colSelectAll"
                                Theme="Office2010Blue" ToolTip="Select/Unselect all rows on the page">
                                <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn Name="colLedger" Caption="Ledger" FieldName="LEDGER_NAME" VisibleIndex="1">
                     <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colLedgerGroup" Caption="Ledger Group" FieldName="LEDGER_GROUP" VisibleIndex="2">
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colLedgeId" FieldName="LEDGER_ID" Visible="false">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn Caption="Nature" FieldName="NATURE" Name="colNature" 
                        VisibleIndex="3">
                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>
                </columns>
                <settingspager position="TopAndBottom" pagesize="300">
                    <PageSizeItemSettings Items="300,400,500" Visible="True">
                    </PageSizeItemSettings>
                </settingspager>
                <settingsbehavior allowsort="true" />
                <settings showfilterrow="True" showfilterrowmenu="true" showheaderfilterbutton="true"></settings>
            </dx:ASPxGridView>
        </div>
        <br />
        <div id="divSave" class="textcenter">
            <dx:ASPxButton ID="btnSave" Text="Save" Theme="Office2010Blue" runat="server" 
                onclick="btnSave_Click" />
        </div>
    </div>
</asp:Content>
