<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="acmeerp_Backup.aspx.cs" Inherits="AcMeERP.Module.Software.acmeerp_Backup"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="height: 20px; vertical-align= middle">
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">
            Export : 
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align= middle">
            <dx:ASPxButton ID="aspxBtnExcel" runat="server" Text=""  
                Image-Url="~/App_Themes/MainTheme/images/excel.png" ToolTip="Export to Excel" 
                RenderMode="Link" onclick="aspxBtnExcel_Click"></dx:ASPxButton>
        </div>
    </div>
    <div class="criteriaribben" style="height: 35px;display: none">
        <div id="divUploadButton" style="float: right; padding-left: 2px; padding-right: 3px;
            padding-bottom: 2px; display: none;">
            <dx:ASPxButton ID="btnFileUpload" runat="server" Text="Upload File" Height="19" TabIndex="3"
                OnClick="btnFileUpload_Click" ClientInstanceName="btnFileUpload" ToolTip="Upload File"
                Image-Url="~/App_Themes/MainTheme/images/uploadfile.jpg" meta:resourcekey="btnFileUploadResource1">
            </dx:ASPxButton>
        </div>
        <div style="float: right; padding-left: 2px; padding-right: 3px;display: none;">
            <dx:ASPxUploadControl ID="UlcFileUpload" runat="server" ShowProgressPanel="True"
                Size="30" TabIndex="2" ClientInstanceName="UlcFileUpload" NullText="Click here browse files"
                meta:resourcekey="UlcFileUploadResource1">
                <ValidationSettings AllowedFileExtensions=".sql,.zip,.rar,.gzip,.gz">
                </ValidationSettings>
                <ClientSideEvents TextChanged="function(s,e){ ValidateUpload(); }" />
            </dx:ASPxUploadControl>
        </div>
        <div style="float: right; padding-left: 2px; padding-right: 3px;">
            <dx:ASPxComboBox ID="cmbBranch" runat="server" IncrementalFilteringMode="Contains" TabIndex="1"
                Height="25" Width="350px" meta:resourcekey="cmbBranchResource1">
            </dx:ASPxComboBox>
        </div>
        <div style="float: right; padding-top: 2px; padding-left: 5px;">
            <span class="bold">Branch</span>
        </div>
    </div>
    <div class="div100">
        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvDownloadBackup" ></dx:ASPxGridViewExporter>
        <dx:ASPxGridView ID="gvDownloadBackup" runat="server" Theme="Office2010Blue" Width="100%"
            KeyFieldName="Attachments" SettingsBehavior-AllowDragDrop="false" 
            AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewDataTextColumn Name="colbranchOfficeName" Caption="Branch Office Name"
                    FieldName="Attachments" VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colUploadedon" Caption="Upload On" FieldName="UploadedOn"
                    VisibleIndex="2">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss tt"></PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colFileSize" Caption="File Size" FieldName="Filesize"
                    VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Name="colDownloadMaster" VisibleIndex="4" Width="50px" Caption=""
                    Visible="true">
                    <DataItemTemplate>
                        <a href='<%#Eval("PhysicalFile") %>' title="Download File" target="_blank">
                            <dx:ASPxImage ID="BtnDownloadMaster" runat="server" ToolTip="Click here to download backup file"
                                ImageUrl="~/App_Themes/MainTheme/images/download-icon.png" CssClass="DownloadButton"
                                RenderMode="Link" EnableTheming="False">
                            </dx:ASPxImage>
                        </a>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataColumn>
            </Columns>
            <Settings ShowFilterRow="True" />
            <SettingsBehavior ColumnResizeMode="Control" FilterRowMode="Auto"
                AllowFocusedRow="true" />
            <SettingsPager Position="TopAndBottom" PageSize="20" SEOFriendly="Enabled">
                <PageSizeItemSettings Visible="true">
                </PageSizeItemSettings>
            </SettingsPager>
        </dx:ASPxGridView>

    </div>
    <script type="text/javascript">

        function ValidateUpload() {
            if (UlcFileUpload.GetText(0) == "") {
                document.getElementById('divUploadButton').style.display = 'none';
            } else {
                document.getElementById('divUploadButton').style.display = 'block';
            }
        }
    </script>
</asp:Content>
