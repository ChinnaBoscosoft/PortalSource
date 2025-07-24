<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="DownloadTemplates.aspx.cs" Inherits="AcMeERP.Module.Software.DownloadTemplates" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div id="divUpload" class="criteriaribben" runat="server" visible="false" style="height: 35px">
        <div style="float: right; padding-left: 2px; padding-right: 3px; padding-bottom: 2px;">
            <dx:ASPxButton ID="btnFileUpload" runat="server" Text="Upload File" Height="25px"
                OnClick="btnFileUpload_Click" TabIndex="2" ClientInstanceName="btnFileUpload"
                ToolTip="Upload File" Image-Url="~/App_Themes/MainTheme/images/uploadfile.jpg">
            </dx:ASPxButton>
        </div>
        <div style="float: right; padding-left: 2px; padding-right: 3px;">
            <dx:ASPxUploadControl ID="UlcFileUpload" runat="server" Size="30" ShowProgressPanel="True"
                TabIndex="1" ClientInstanceName="UlcFileUpload" NullText="Click here browse files">
                <ValidationSettings AllowedFileExtensions=".xlsx,.xls" MaxFileSize="26000000">
                </ValidationSettings>
                <ClientSideEvents TextChanged="function(s,e){ ValidateUpload(); }" />
            </dx:ASPxUploadControl>
        </div>
        <div style="float: right; padding-left: 2px; padding-top: 2px; padding-right: 3px;">
            <dx:ASPxComboBox ID="cmbModule" runat="server" IncrementalFilteringMode="Contains"
                AutoPostBack="true">
            </dx:ASPxComboBox>
        </div>
        <div style="float: right; padding-top: 3px; padding-left: 2px; padding-right: 3px;">
            <span class="bold"><strong>Select Module</strong> </span>
        </div>
    </div>
    <asp:UpdatePanel ID="upTemplates" runat="server">
        <ContentTemplate>
            <div class="row-fluid filesdiv" style="width: 50% !important;">
                <div class="divscroll">
                    <div class="divscroll">
                        <asp:Repeater ID="rptTemplates" runat="server">
                            <ItemTemplate>
                                <div style="background-color: #CECF9C;" class="files">
                                    <strong>
                                        <%-- <%#Eval("UploadedOn")%></strong> <strong class="fileSize">
                                            <%#Eval("Filesize")%></strong>--%>
                                        <strong class="fileSize">
                                            <%#Eval("Module")%></strong> <a class="aimg_cursor" href='<%#Eval("PhysicalFile").ToString() %>'
                                                title="Download File" target="_self"><strong class="filelist">
                                                    <%#Eval("Attachments")%></strong>
                                                <div class="floatright" style="margin-right: 5px;">
                                                    <asp:Image ID="imgDownldFile" runat="server" ImageUrl="~/App_Themes/MainTheme/images/download-icon.png"
                                                        AlternateText="No Image..."></asp:Image>
                                                </div>
                                            </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function ValidateUpload() {
            if (UlcFileUpload.GetText(0) == "") {
                document.getElementById('divUploadButton').style.display = 'none';
            } else {
                document.getElementById('divUploadButton').style.display = 'block';
            }
        }
    </script>
</asp:Content>
