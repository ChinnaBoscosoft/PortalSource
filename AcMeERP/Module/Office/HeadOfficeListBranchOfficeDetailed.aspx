<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="HeadOfficeListBranchOfficeDetailed.aspx.cs"
    Inherits="AcMeERP.Module.Office.HeadOfficeListBranchOfficeDetailed" meta:resourcekey="PageResource1" %>

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
        <dx:ASPxGridViewExporter ID="gridHOwiseBranchList" runat="server" GridViewID="gvHOwiseBranchlistdetailed">
        </dx:ASPxGridViewExporter>
        <dx:ASPxGridView ID="gvHOwiseBranchlistdetailed" runat="server" Theme="Office2010Blue"
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
                <dx:GridViewDataTextColumn Name="colBranchOfficeCode" Caption="Branch Office Code"
                    FieldName="BRANCH_OFFICE_CODE" VisibleIndex="3">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeName" Caption="Branch Office Name"
                    FieldName="BRANCH_OFFICE_NAME" VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeCreated" Caption="Branch Created"
                    FieldName="BRANCH_CREATED" VisibleIndex="5">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeAddress" Caption="ADDRESS" FieldName="ADDRESS"
                    VisibleIndex="6">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeIncharge" Caption="INCHARGE_NAME"
                    FieldName="INCHARGE_NAME" VisibleIndex="7">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeMobile" Caption="MOBILE_NO" FieldName="MOBILE_NO"
                    VisibleIndex="8">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Name="colBranchOfficeMobile" Caption="BRANCH_EMAIL_ID"
                    FieldName="BRANCH_EMAIL_ID" VisibleIndex="9">
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
