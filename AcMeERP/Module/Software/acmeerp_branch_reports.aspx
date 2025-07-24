<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="acmeerp_branch_reports.aspx.cs" Inherits="AcMeERP.Module.Software.acmeerp_branch_reports"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="height: 20px; vertical-align= middle">
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">
            Export : 
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align= middle">
            <dx:ASPxButton ID="aspxBtnExcel" runat="server" Text=""  
                Image-Url="~/App_Themes/MainTheme/images/excel.png" ToolTip="Export to Excel of list of Branch Reports" 
                RenderMode="Link" onclick="aspxBtnExcel_Click"></dx:ASPxButton>
        </div>
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">&nbsp;&nbsp;&nbsp&nbsp
        </div>
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">
            File Type : 
        </div>
         <div style="float: left; padding-left: 5px; padding-bottom:8px; vertical-align: middle">
             <asp:RadioButtonList ID="rbFileType" RepeatDirection="Horizontal"  
                 runat="server" onselectedindexchanged="rbFileType_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Value="0">.Xlsx</asp:ListItem>
                 <asp:ListItem Value="1">.Pdf</asp:ListItem>
             </asp:RadioButtonList>
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px;">&nbsp;&nbsp;</div>
         <div style="float: left; padding-left: 5px; padding-bottom:8px; vertical-align: middle">
            <dx:ASPxButton ID="btnShowBranchReports" runat="server" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png" TabIndex="3"
                Text="Show Branch Reports" Height="24px" ToolTip="Show Branch Reports">
            </dx:ASPxButton>
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
        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvDownloadBranchReports" ></dx:ASPxGridViewExporter>
         <dx:ASPxGridView ID="gvDownloadBranchReports" runat="server" Theme="Office2010Blue" Width="100%" 
            ClientInstanceName="gvDownloadBranchReports" OnRowCommand="gvDownloadBranchReports_RowCommand"
            KeyFieldName="Attachments" 
            SettingsBehavior-AllowDragDrop="false" AutoGenerateColumns="False">
            <Columns>
                 <dx:GridViewDataTextColumn Name="colbranchOfficeName" Caption="Branch Office Name"
                    FieldName="Branch" VisibleIndex="1">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn Name="colReportFile" Caption="Report File"
                    FieldName="Attachments" VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colUploadedon" Caption="Upload On" FieldName="UploadedOn"
                    VisibleIndex="3">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss tt"></PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colFileSize" Caption="File Size" FieldName="Filesize"
                    VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Name="colDownloadMaster" VisibleIndex="5" Width="50px" Caption=""
                    Visible="true">
                    <DataItemTemplate>
                        <dx:ASPxButton ID="BtnDownloadReportFile" runat="server" AutoPostBack="true" AllowFocus="False"
                            ToolTip="Click here to download Branch Report" RenderMode="Link" EnableTheming="False" CommandName="downloadfile">
                            <Image>
                                <SpriteProperties CssClass="DownloadButton" />
                            </Image>
                        </dx:ASPxButton>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataColumn>
            </Columns>
            <SettingsBehavior AllowSort="false" ColumnResizeMode="Control" AllowFocusedRow="true"  />
            <SettingsPager Position="TopAndBottom" PageSize="20" SEOFriendly="Enabled">
                <PageSizeItemSettings Visible="true">
                </PageSizeItemSettings>
            </SettingsPager>
            <Settings ShowFilterRow="True" ShowHeaderFilterButton="true" />
            <ClientSideEvents SelectionChanged="gvDownloadMaster_SelectionChanged" />
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
