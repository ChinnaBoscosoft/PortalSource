<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="DownloadKey.aspx.cs" Inherits="AcMeERP.Module.Software.DownloadKey" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function chkSelectAll_CheckChanged(s, e) {
            gvDownloadKey.SelectAllRowsOnPage(s.GetChecked());
        }
        function SelectionChanged(s, e) {
            chkSelectAll.SetChecked(false);
        }
        function gvDownloadKey_SelectionChanged(s, e) {
            var count = gvDownloadKey.GetSelectedRowCount();
            if (count > 0) {
                document.getElementById('divBtnDownload').style.display = 'block';
            }
            else {
                document.getElementById('divBtnDownload').style.display = 'none';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div>
        <div id="divBtnDownload" class="criteriaribben" style="display:none">
            <div class="Note floatleft">
                <%--<span class="red"><strong>Note: </strong></span>To Download Master, Please map Project(s)
                and Ledger(s) to Branch Office--%>
            </div>
            <div class="floatright">
                <asp:Button ID="BtnDownload" runat="server" CssClass="button" Text="Download" ToolTip="Click here to download all the licence keys" 
                    OnClick="BtnDownload_Click" />
            </div>
        </div>
        <dx:ASPxGridView ID="gvDownloadKey" runat="server" Theme="Office2010Blue" Width="100%"
            OnRowCommand="gvDownloadKey_RowCommand" KeyFieldName="Branch Code" OnLoad="gvDownloadKey_Load"
            ClientInstanceName="gvDownloadKey" SettingsBehavior-AllowDragDrop="false" AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Width="40" ShowNewButtonInHeader="True"
                    Name="colMultiCheckBox">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="chkSelectAll" runat="server" ToolTip="Select/Unselect all rows on the page"
                            Theme="Office2010Blue" ClientInstanceName="chkSelectAll">
                            <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                        </dx:ASPxCheckBox>
                    </HeaderTemplate>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Name="colHeadOfficeCode" Caption="Head Office Code" FieldName="HEAD_OFFICE_CODE"
                    VisibleIndex="0">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchCode" Caption="Branch Office Code" FieldName="Branch Code"
                    VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colbranchOfficeName" Caption="Branch Office Name"
                    FieldName="Branch Name" VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colSystemType" Caption="System Type" FieldName="System Type"
                    VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colAddress" Caption=" Address" FieldName="Address"
                    VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Name="colDownloadKey" VisibleIndex="5" Width="45px" Caption=""
                    Visible="true">
                    <DataItemTemplate>
                        <dx:ASPxButton ID="btnDownloadKey" runat="server" AutoPostBack="true" AllowFocus="False"
                            ToolTip="Click here to download license key" RenderMode="Link" EnableTheming="False">
                            <Image>
                                <SpriteProperties CssClass="DownloadButton" />
                            </Image>
                        </dx:ASPxButton>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
            <Settings ShowFilterRow="True" ShowHeaderFilterButton="true" />
            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" FilterRowMode="Auto"
                AllowFocusedRow="true" />
            <SettingsPager Position="TopAndBottom" PageSize="20" SEOFriendly="Enabled">
                <PageSizeItemSettings Visible="true">
                </PageSizeItemSettings>
            </SettingsPager>
            <ClientSideEvents SelectionChanged="gvDownloadKey_SelectionChanged" />
        </dx:ASPxGridView>
    </div>
</asp:Content>
