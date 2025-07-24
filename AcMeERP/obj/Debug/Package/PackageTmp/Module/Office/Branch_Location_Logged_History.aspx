<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="Branch_Location_Logged_History.aspx.cs" Inherits="AcMeERP.Module.Office.Branch_Location_Logged_History"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="height: 20px; vertical-align: middle">
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">
            Export : 
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align: middle">
            <dx:ASPxButton ID="aspxBtnExcel" runat="server" Text=""  
                Image-Url="~/App_Themes/MainTheme/images/excel.png" ToolTip="Export to Excel" 
                RenderMode="Link" onclick="aspxBtnExcel_Click"></dx:ASPxButton>
        </div>
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">&nbsp;&nbsp;&nbsp&nbsp
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px;">&nbsp;&nbsp;</div>
         <div style="float: left; padding-left: 5px; padding-bottom:8px; vertical-align: middle">
            <dx:ASPxButton ID="btnRefresh" runat="server" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png" TabIndex="3"
                Text="Refresh" Height="24px" ToolTip="Refresh" meta:resourcekey="btnRefreshResource2">
            </dx:ASPxButton>
        </div>
        <div style="float: left; padding-left: 5px; padding-bottom:8px; vertical-align: middle">
            <dx:ASPxButton ID="btnExpandAll" runat="server" 
                Image-Url="~/App_Themes/MainTheme/images/file-refresh.png" TabIndex="3"
                Text="Expand All" Height="24px" ToolTip="Refresh" 
                meta:resourcekey="btnRefreshResource2" onclick="btnExpandAll_Click">
            </dx:ASPxButton>
        </div>
    </div>
    <div style="width:80%; margin: auto; text-align:center">
    <div>
        <dx:ASPxGridViewExporter ID="gridExportBranchLocationLoggedHistory" runat="server" GridViewID="gvMorethanoneBranchSystem" ExportEmptyDetailGrid="true">
        </dx:ASPxGridViewExporter>
        <dx:ASPxGridView ID="gvMorethanoneBranchSystem" runat="server" 
                                AutoGenerateColumns="False" KeyFieldName="BRANCH_OFFICE_CODE"
                                Theme="Office2010Blue" Width="100%" Style="margin-top: 0px" 
                                ClientInstanceName="grdBranchSystem" 
                                onload="gvMorethanoneBranchSystem_Load" >
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="HO Code" FieldName="HEAD_OFFICE_CODE" Name="colHOCode" VisibleIndex="1" Width="5%" Visible ="false" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"/>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="HO Name" FieldName="HEAD_OFFICE_NAME" Name="colHOName" VisibleIndex="2" Width="30%" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="BO Code" FieldName="BRANCH_OFFICE_CODE" Name="colBOCode" VisibleIndex="3" Width="5%" Visible ="false" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Branch" FieldName="BRANCH_OFFICE_NAME" Name="colBranchName" VisibleIndex="4" Width="30%" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="No.System" FieldName="IPs" Name="colSystem" VisibleIndex="5" Width="3%" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowSort="true" AllowDragDrop="False" AllowFocusedRow="True"/>
                        <SettingsPager Mode="ShowAllRecords" Visible="False" />
                        <Settings ShowFilterRow="true" />
                        <SettingsDetail ShowDetailRow="True"  ExportMode="All"/>
                        <ClientSideEvents SelectionChanged="function(s, e) { CheckSelect(s, e); }" />
                        <Templates>
                    <DetailRow>
                        <dx:ASPxGridView ID="gvMorethanoneBranchSystemDetails" runat="server" KeyFieldName="BRANCH_OFFICE_CODE"
                            Theme="Office2010Blue" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="gvMorethanoneBranchSystemDetails_BeforePerformDataSelect">
                            <Columns>
                            <dx:GridViewDataTextColumn Caption="BO Code" FieldName="BRANCH_OFFICE_CODE1" Name="colBOCode" VisibleIndex="1" Width="5%" Visible ="true" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Location" FieldName="LOCATION_NAME" Name="colLocation" VisibleIndex="2" Width="25%" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="License Key Number" FieldName="LICENSE_KEY_NUMBER" Name="colLocation1" VisibleIndex="3" Width="15%" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Logged On" FieldName="LOGGED_ON" Name="colLoggedOn1" VisibleIndex="4" Width="5%" PropertiesTextEdit-DisplayFormatString="{0:dd/MM/yyyy}" CellStyle-HorizontalAlign="Left">
                            <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                            </PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Remarks" FieldName="REMARKS" Name="colSystemDetails" VisibleIndex="5" CellStyle-HorizontalAlign="Left">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                            </dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFilterRow="true" ShowHeaderFilterButton="true"/>
                            <SettingsBehavior AllowDragDrop="False" AllowSort="true" />
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
                    </dx:ASPxGridView>
    </div>
    </div>
</asp:Content>
