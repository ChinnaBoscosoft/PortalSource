<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="Branch_Logged_History.aspx.cs" Inherits="AcMeERP.Module.Office.Branch_Logged_History"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
<%--to refresh page every after 1 mintue--%>
<meta http-equiv="refresh" content="60" />
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
        <div class="bold" style="float: left; padding-top: 2px; padding-left: 5px; vertical-align = middle">
            Date : 
        </div>
         <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align: middle">
             <dx:ASPxDateEdit ID="apxDate" Width="120px" runat="server" 
                 ondatechanged="apxDate_DateChanged"></dx:ASPxDateEdit>
        </div>
        <div style="float: left; padding-top: 2px; padding-left: 5px;">&nbsp;&nbsp;</div>
         <div style="float: left; padding-left: 5px; padding-bottom:8px; vertical-align: middle">
            <dx:ASPxButton ID="btnRefresh" runat="server" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png" TabIndex="3"
                Text="Refresh" Height="24px" ToolTip="Refresh" meta:resourcekey="btnRefreshResource2">
            </dx:ASPxButton>
        </div>
    </div>
    <div class="div100">
        <dx:ASPxGridViewExporter ID="gridBranchLoggedHistory" runat="server" GridViewID="gvBranchLoggedHistory" ></dx:ASPxGridViewExporter>
        <dx:ASPxGridView ID="gvBranchLoggedHistory" runat="server" Theme="Office2010Blue" Width="100%"
            KeyFieldName="BRANCH_OFFICE_CODE" SettingsBehavior-AllowDragDrop="false" 
            AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewDataTextColumn Name="colHeadOfficeCode" Caption="Head Office Code"
                    FieldName="HEAD_OFFICE_CODE" VisibleIndex="1" Width="115px">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colHeadOfficeName" Caption="Head Office Name"
                    FieldName="HEAD_OFFICE_NAME" VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeName" Caption="Branch Office Name"
                    FieldName="BRANCH_OFFICE_NAME" VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colLocation" Caption="Location" FieldName="LOCATION"
                    VisibleIndex="4" Width="160px">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colLoggedOn" Caption="Logged On" FieldName="LOGGED_ON"
                    VisibleIndex="5" Width="165px">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss tt"></PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colLicenseKey" Caption="License Key" FieldName="LICENSE_KEY_NUMBER"
                    VisibleIndex="6" Width="175px">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="col" Caption="Remarks" FieldName="REMARKS"
                    VisibleIndex="7">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="True" />
            <SettingsBehavior ColumnResizeMode="Control" FilterRowMode="Auto"
                AllowFocusedRow="true"  AllowSort="true"/>
            <SettingsPager Position="TopAndBottom" PageSize="20" SEOFriendly="Enabled">
                <PageSizeItemSettings Visible="true">
                </PageSizeItemSettings>
            </SettingsPager>
        </dx:ASPxGridView>

    </div>
</asp:Content>
