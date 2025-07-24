<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ImportAssetMasters.aspx.cs" Inherits="AcMeERP.Module.Inventory.Asset.ImportAssetMasters" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1">
            <div class="row-fluid">
                <div class="floatright red">
                    <span style="">Download the Asset Masters Template Here</span>
                    <asp:ImageButton ID="imgDownloadMastersTemplate" OnClick="imgDownloadMastersTemplate_Click"
                        runat="server" AlternateText="DownloadAssetMastersTemplate" ToopTip="Click to Download Asset Masters Template"
                        CausesValidation="False" SkinID="download_bt" />
                </div>
            </div>
            <div class="row-fluid">
                <div class="span4 textright">
                    <dx:ASPxLabel ID="lblFile" runat="server" Text="Select a file " Theme="Office2010Blue">
                    </dx:ASPxLabel>
                </div>
                <div class="span3">
                    <dx:ASPxUploadControl ID="UlcFileUpload" runat="server" Size="30" ShowProgressPanel="True"
                        Theme="Office2010Blue" ClientInstanceName="UlcFileUpload" NullText="Click here browse files"
                        Width="260px">
                        <ValidationSettings AllowedFileExtensions=".xlsx,.xls" MaxFileSize="26000000">
                        </ValidationSettings>
                        <ClientSideEvents TextChanged="function(s,e){ ValidateUpload(); }" />
                    </dx:ASPxUploadControl>
                </div>
                <div id="divUploadButton" class="span2" style="display: none">
                    <dx:ASPxButton ID="btnUpload" Text="Upload" runat="server" Height="19px" Theme="Office2010Blue"
                        OnClick="btnUpload_Click" Image-Url="~/App_Themes/MainTheme/images/uploadfile.jpg">
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="row-fluid">
                <div class="div100">
                    <dx:ASPxLabel ID="lblSummary" runat="server" Text="Summary" ForeColor="Teal" Font-Bold="true"
                        Theme="Office2010Blue">
                    </dx:ASPxLabel>
                </div>
            </div>
            <div class="row-fulid">
                <div class="div100">
                    <dx:ASPxMemo ID="meoSummary" Width="100%" runat="server" ClientInstanceName="meoSummary"
                        ForeColor="Red" Height="300px" ReadOnly="True" Theme="Office2010Blue" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/jscript">
        function ValidateUpload() {
            if (UlcFileUpload.GetText(0) == "") {
                document.getElementById('divUploadButton').style.display = 'none';
            } else {
                document.getElementById('divUploadButton').style.display = 'block';
            }
        }
    </script>
</asp:Content>
