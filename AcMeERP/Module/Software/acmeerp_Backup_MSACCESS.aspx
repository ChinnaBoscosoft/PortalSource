<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="acmeerp_Backup_MSACCESS.aspx.cs" Inherits="AcMeERP.Module.Software.acmeerp_Backup_MSACCESS"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="display: flex; align-items: center; gap: 10px;
        padding: 5px; background: #f5f5f5; justify-content: flex-end;">
        <div class="bold">
            Branch :</div>
        <dx:ASPxComboBox ID="cmbBranch" runat="server" IncrementalFilteringMode="Contains"
            Height="30px" Width="350px" ValueType="System.String" Placeholder="Select Branch">
            <Columns>
                <dx:ListBoxColumn FieldName="BNAME_CODE" Caption="BRANCH OFFICE" />
                <dx:ListBoxColumn FieldName="BRANCH_OFFICE_ID" Caption="Branch ID" Visible="false" />
            </Columns>
        </dx:ASPxComboBox>
        <dx:ASPxUploadControl ID="aspxUploadBrowsefile" runat="server" ShowProgressPanel="True"
            ClientInstanceName="uploadControl" NullText="Click to Browse Files" ButtonStyle-CssClass="upload-btn"
            Width="200px" OnFileUploadComplete="aspxUploadBrowsefile_FileUploadComplete">
            <ValidationSettings AllowedFileExtensions=".mdb,.accdb" />
        </dx:ASPxUploadControl>
        <dx:ASPxButton ID="btnUpload" runat="server" Text="Upload" AutoPostBack="true" Width="100px"
            Height="30px" CssClass="upload-btn" OnClick="btnUpload_Click">
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnRefresh" runat="server" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png"
            TabIndex="3" Height="24px" ToolTip="Refresh" OnClick="btnRefresh_Click" meta:resourcekey="btnRefreshResource2">
        </dx:ASPxButton>
    </div>
    <div class="div100">
        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvDownloadBackup">
        </dx:ASPxGridViewExporter>
        <dx:ASPxGridView ID="gvDownloadBackup" runat="server" Theme="Office2010Blue" Width="100%"
            ClientInstanceName="gvDownloadBackup" OnRowCommand="gvDownloadBackup_RowCommand"
            KeyFieldName="Attachments" EnableCallBacks="true" SettingsBehavior-AllowDragDrop="false"
            AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewDataTextColumn Name="colbranchOfficeName" Caption="Branch Office Name"
                    FieldName="Attachments" VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colUploadedon" Caption="Upload On" FieldName="UploadedOn"
                    VisibleIndex="2">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colFileSize" Caption="File Size" FieldName="Filesize"
                    VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Name="colDownloadMaster" VisibleIndex="6" Width="50px" Caption=""
                    Visible="true">
                    <DataItemTemplate>
                        <dx:ASPxButton ID="BtnDownloadReportFile" runat="server" AutoPostBack="true" AllowFocus="False"
                            ToolTip="Click here to download Branch Report" RenderMode="Link" EnableTheming="False"
                            CommandName="downloadfile">
                            <Image>
                                <SpriteProperties CssClass="DownloadButton" />
                            </Image>
                        </dx:ASPxButton>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataColumn>
            </Columns>
            <Settings ShowFilterRow="True" />
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
