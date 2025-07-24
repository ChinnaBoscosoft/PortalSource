<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="acmeerp_Backup2015old.aspx.cs" Inherits="AcMeERP.Module.Software.acmeerp_Backup2015old"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript" src="/DXR.axd?r=1_0&v=<%= DateTime.Now.Ticks %>"></script>
    <script type="text/javascript" src="/Scripts/DevExpressWeb.js?v=<%= DateTime.Now.Ticks %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="height: 20px; vertical-align= middle">
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">
            Export :
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align= middle">
            <dx:ASPxButton ID="aspxBtnExcel" runat="server" Text="" Image-Url="~/App_Themes/MainTheme/images/excel.png"
                ToolTip="Export to Excel" RenderMode="Link" OnClick="aspxBtnExcel_Click">
            </dx:ASPxButton>
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align= middle">
            <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" UploadMode="Auto" Width="280px">
            </dx:ASPxUploadControl>
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align= middle">
            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton">
            </dx:ASPxButton>
        </div>
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
                <dx:GridViewDataColumn Caption="File Upload" VisibleIndex="4" Width="100px">
                    <DataItemTemplate>
                        <asp:HiddenField ID="hfAttachmentName" runat="server" Value='<%# Eval("Attachments") %>' />
                        <dx:ASPxUploadControl ID="uploadControl" runat="server" Width="20px" NullText="Select File"
                            AutoPostBack="false" Enabled="true" ShowUploadButton="true" OnFileUploadComplete="uploadControl_FileUploadComplete">
                            <ValidationSettings AllowedFileExtensions=".zip,.sql" />
                            <ClientSideEvents FileUploadStart="function(s, e) {
                                    var grid = gvDownloadBackup;
                                    var rowIndex = grid.GetFocusedRowIndex();
        
                                    if (rowIndex &lt; 0) {
                                        alert(&quot;⚠ Error: No row selected. Please select a row before uploading.&quot;);
                                        e.cancel = true;
                                        return;
                                    }

                                var attachmentName = grid.GetRowKey(rowIndex);
                                if (!attachmentName) {
                                    alert(&quot;⚠ Error: Unable to retrieve attachment name.&quot;);
                                    e.cancel = true;
                                    return;
                                }

                                var rowElement = grid.GetRow(rowIndex);
                                if (rowElement) {
                                    var hiddenField = rowElement.querySelector(&quot;[id*=hfAttachmentName]&quot;);
                                    if (hiddenField) {
                                        s.SetText(hiddenField.value);
                                        console.log(&quot;✅ Attachment Name Set:&quot;, hiddenField.value);
                                    } else {
                                        console.error(&quot;❌ HiddenField not found in the row.&quot;);
                                    }
                                } else {
                                    console.error(&quot;❌ Row element not found for index:&quot;, rowIndex);
                                }
                            }"
                                FileUploadComplete="function(s, e) {
                                console.log(&quot;✅ File upload completed.&quot;);

                                if (window.gvDownloadBackup) {
                                    setTimeout(function() {
                                        gvDownloadBackup.PerformCallback();
                                    }, 500);
                                } else {
                                    console.error(&quot;❌ Grid not found for callback.&quot;);
                                }
                            }" />
                        </dx:ASPxUploadControl>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn VisibleIndex="5" Width="40px">
                    <DataItemTemplate>
                        <dx:ASPxButton ID="btnUpload" runat="server" Text="⬆" CommandName="UploadFile" Width="15px" />
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colDownloadMaster" VisibleIndex="6" Width="50px" Caption=""
                    Visible="true">
                    <%-- <DataItemTemplate>
                        <a href='<%#Eval("PhysicalFile") %>' title="Download File" target="_blank">
                            <dx:ASPxImage ID="BtnDownloadMaster" runat="server" ToolTip="Click here to download backup file"
                                ImageUrl="~/App_Themes/MainTheme/images/download-icon.png" CssClass="DownloadButton"
                                RenderMode="Link" EnableTheming="False">
                            </dx:ASPxImage>
                        </a>
                    </DataItemTemplate>--%>
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
        function refreshUploadControl() {
            if (typeof gvDownloadBackup !== "undefined" && gvDownloadBackup) {
                console.log("✅ Grid Found! Performing Callback...");
                gvDownloadBackup.PerformCallback();
            } else {
                console.error("❌ Grid Not Found! Skipping refresh.");
            }
        }

        document.addEventListener("DOMContentLoaded", function () {
            var requestManager = Sys.WebForms.PageRequestManager.getInstance();

            if (requestManager) {
                requestManager.add_endRequest(function () {
                    console.log("🔄 AJAX Request Completed. Refreshing Grid...");
                    setTimeout(refreshUploadControl, 500);
                });
            } else {
                console.warn("⚠ PageRequestManager instance not found. Skipping AJAX handler.");
            }
        });
    </script>
</asp:Content>
