<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="Voucher_Lock.aspx.cs" Inherits="AcMeERP.Module.Software.Voucher_Lock"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="display: flex; align-items: center; gap: 10px;
        padding: 5px; background: #f5f5f5; justify-content: flex-end;">
        <dx:ASPxButton ID="btnUpdate" runat="server" Text="Update" AutoPostBack="true" Width="100px"
            Height="30px" CssClass="upload-btn" OnClick="btnUpdate_Click">
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnRefresh" runat="server" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png"
            TabIndex="3" Height="24px" ToolTip="Refresh" OnClick="btnRefresh_Click" meta:resourcekey="btnRefreshResource2">
        </dx:ASPxButton>
    </div>
    <div class="div100">
        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvDownloadBackup">
        </dx:ASPxGridViewExporter>
        <dx:ASPxGridView ID="gvDownloadBackup" runat="server" Theme="Office2010Blue" Width="100%"
            ClientInstanceName="gvDownloadBackup" KeyFieldName="BRANCH_OFFICE_NAME" EnableCallBacks="true"
            SettingsBehavior-AllowDragDrop="false" AutoGenerateColumns="False">
            <SettingsEditing Mode="Inline" />
            <Columns>
                <dx:GridViewDataTextColumn Name="colbranchOfficeName" Caption="Branch Office Name"
                    FieldName="BRANCH_OFFICE_NAME" Width="350" VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="collocationName" Caption="Location Name" FieldName="location_name"
                    Width="150px" VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colEnforeGraceDays" Caption="Enforce" FieldName="ENFORCE_GRACE_DAYS"
                    Width="50px" VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colGraceDays" Caption="Grace Days" FieldName="GRACE_DAYS"
                    Width="50px" VisibleIndex="4">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="txtGraceDays" runat="server" Text='<%# Bind("GRACE_DAYS") %>'
                            MaxLength="5" Width="65px">
                            <MaskSettings Mask="" AllowMouseWheel="false" />
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Grace Date From" FieldName="GRACE_TMP_DATE_FROM"
                    Name="colGraceTmpDateFrom" VisibleIndex="5" Width="10%">
                    <DataItemTemplate>
                        <dx:ASPxDateEdit ID="txtGraceTmpDateFrom" runat="server" DisplayFormatString="yyyy-MM-dd"
                            EditFormat="Date" EditFormatString="yyyy-MM-dd" Text='<%# Eval("GRACE_TMP_DATE_FROM", "{0:yyyy-MM-dd}") %>'
                            Width="100%">
                        </dx:ASPxDateEdit>
                    </DataItemTemplate>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Grace Date To" FieldName="GRACE_TMP_DATE_TO"
                    Name="colGraceTmpDateTo" VisibleIndex="6" Width="10%">
                    <DataItemTemplate>
                        <dx:ASPxDateEdit ID="txtGraceTmpDateTo" runat="server" EditFormat="Date" EditFormatString="yyyy-MM-dd"
                            Text='<%# Eval("GRACE_TMP_DATE_TO", "{0:yyyy-MM-dd}") %>' Width="100%">
                        </dx:ASPxDateEdit>
                    </DataItemTemplate>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Grace Valid Upto" FieldName="GRACE_TMP_VALID_UPTO"
                    Name="colGraceTmpValidUpto" VisibleIndex="7" Width="10%">
                    <DataItemTemplate>
                        <dx:ASPxDateEdit ID="txtGraceTmpValidUpto" runat="server" EditFormat="Date" EditFormatString="yyyy-MM-dd"
                            Text='<%# Eval("GRACE_TMP_VALID_UPTO", "{0:yyyy-MM-dd}") %>' Width="100%">
                        </dx:ASPxDateEdit>
                    </DataItemTemplate>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <%-- <Settings ShowFilterRow="True" />--%>
            <SettingsBehavior ColumnResizeMode="Control" FilterRowMode="Auto" AllowFocusedRow="true" />
            <SettingsPager Position="TopAndBottom" PageSize="20" SEOFriendly="Enabled">
                <PageSizeItemSettings Visible="true">
                </PageSizeItemSettings>
            </SettingsPager>
        </dx:ASPxGridView>
    </div>
    <script type="text/javascript">
    </script>
</asp:Content>
