<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AmendmentsView.aspx.cs" Inherits="AcMeERP.Module.Master.AmendmentsView"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben">
        <div>
            <div style="float: right; padding-left: 5px; padding-right: 5px">
                <dx:ASPxComboBox ID="cmbBranch" runat="server" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged"
                    TabIndex="1" Theme="Office2010Blue" IncrementalFilteringMode="Contains" DropDownStyle="DropDownList"
                    meta:resourcekey="cmbBranchResource2" AutoPostBack="True" Width="250px">
                </dx:ASPxComboBox>
            </div>
            <div class="Note floatleft">
                <span class="red"><strong>Note: </strong></span>Amendment voucher(s) are available,
                please update the voucher(s).
            </div>
            <div style="float: right; padding-top: 2px; padding-left: 5px;">
                <span class="bold">Branch </span>
            </div>
        </div>
    </div>
    <div>
        <dx:ASPxGridView ID="gvAmendmentHistory" runat="server" Width="100%" SettingsBehavior-AllowDragDrop="false"
            KeyFieldName="VOUCHER_ID" Theme="Office2010Blue" OnLoad="gvAmendmentHistory_Load">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Date" VisibleIndex="0" Width="50px" FieldName="AMENDMENT_DATE">
                    <PropertiesTextEdit DisplayFormatString="{0:d}">
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Project" VisibleIndex="1" Width="100px" FieldName="PROJECT">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="V.No" VisibleIndex="2" Width="25px" FieldName="VOUCHER_NO">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Type" VisibleIndex="3" Width="50px" FieldName="VOUCHER_TYPE">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Particulars" VisibleIndex="4" Width="200px" FieldName="LEDGER_NAME">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Amount" VisibleIndex="5" Width="100px" FieldName="AMOUNT">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Description" Width="270px" FieldName="REMARKS">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="true" />
            <Settings ShowFilterRow="true" />
            <SettingsPager Position="TopAndBottom" PageSize="20">
                <PageSizeItemSettings Items="30, 40, 50" Visible="True">
                </PageSizeItemSettings>
            </SettingsPager>
        </dx:ASPxGridView>
    </div>
</asp:Content>
