<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="HeadOfficeListBranchCount.aspx.cs" Inherits="AcMeERP.Module.Office.HeadOfficeListBranchCount"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
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
            <dx:ASPxButton ID="aspxBtnExcel" runat="server" Text="" Image-Url="~/App_Themes/MainTheme/images/excel.png"
                ToolTip="Export to Excel" RenderMode="Link" OnClick="aspxBtnExcel_Click">
            </dx:ASPxButton>
        </div>
    </div>
    <div class="div100">
        <dx:ASPxGridViewExporter ID="gridHOwiseBranchList" runat="server" GridViewID="gvHOwiseBranchlistdetails">
        </dx:ASPxGridViewExporter>
        <dx:ASPxGridView ID="gvHOwiseBranchlistdetails" runat="server" Theme="Office2010Blue"
            Width="100%" KeyFieldName="HEAD_OFFICE_CODE" SettingsBehavior-AllowDragDrop="false"
            AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewDataTextColumn Name="colHeadOfficeCode" Caption="Head Office Code" FieldName="HEAD_OFFICE_CODE"
                    VisibleIndex="1" Width="115px">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colHeadOfficeName" Caption="Head Office Name" FieldName="HEAD_OFFICE_NAME"
                    VisibleIndex="2">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeCount" Caption="Branch Count"
                    FieldName="COUNT" VisibleIndex="3" Width="100">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="True" />
            <SettingsBehavior ColumnResizeMode="Control" FilterRowMode="Auto" AllowFocusedRow="true"
                AllowSort="true" />
            <SettingsPager Position="TopAndBottom" PageSize="20" SEOFriendly="Enabled">
                <PageSizeItemSettings Visible="true">
                </PageSizeItemSettings>
            </SettingsPager>
        </dx:ASPxGridView>
    </div>
</asp:Content>
