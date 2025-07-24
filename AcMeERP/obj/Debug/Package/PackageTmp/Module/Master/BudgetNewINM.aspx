<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="BudgetNewINM.aspx.cs" Inherits="AcMeERP.Module.Master.BudgetNewINM"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <style type="text/css">
        .dxeBase
        {
            font: 12px Tahoma, Geneva, sans-serif;
        }
    </style>
</asp:Content>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upFdRegistersView" runat="server">
        <ContentTemplate>
            <%--<link href="../../App_Themes/MainTheme/Printstyle.css" type="text/css" />--%>
            <div class="criteriaribben nonprintable">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Branch </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbBranch" runat="server" AutoPostBack="True" IncrementalFilteringMode="Contains"
                            Width="300px" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" Theme="Office2010Blue"
                            TabIndex="1">
                        </dx:ASPxComboBox>
                        &nbsp;
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Budget </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbBudget" runat="server" TabIndex="2" Theme="Office2010Blue"
                            Width="300px">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    <dx:ASPxButton ID="btnLoad" Text="Go" OnClick="btnLoad_Click" Theme="Office2010Blue"
                        TabIndex="5" runat="server" Height="20px" Width="80px">
                        <Image Url="~/App_Themes/MainTheme/images/go.png">
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    <dx:ASPxButton ID="btnMove" runat="server" Text="Move" Height="20px" Theme="Office2010Blue"
                        OnClick="btnMove_Click" TabIndex="6">
                        <%--<ClientSideEvents Click="function(s, e)
                                {
                                 e.processOnServer = confirm('Are you sure to Approve Budget?');
                                 }" />--%>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Height="20px" Theme="Office2010Blue"
                        OnClick="btnSave_Click" TabIndex="6">
                        <%--<ClientSideEvents Click="function(s, e)
                                {
                                 e.processOnServer = confirm('Are you sure to Approve Budget?');
                                 }" />--%>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    <dx:ASPxButton ID="btnApproved" runat="server" Text="Approve" Height="20px" Theme="Office2010Blue"
                        OnClick="btnApproved_Click" TabIndex="6">
                        <%--<ClientSideEvents Click="function(s, e)
                                {
                                 e.processOnServer = confirm('Are you sure to Approve Budget?');
                                 }" />--%>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    <dx:ASPxButton ID="btnPrintPreview" runat="server" Text="Print Preview" Height="20px"
                        Theme="Office2010Blue" AutoPostBack="false" TabIndex="10">
                        <ClientSideEvents Click="function(s, e)
                                {
                                  e.processOnServer = confirm('Do you want to show Print Preview Budget?');
                                  if (e.processOnServer)
                                  {
                                    window.open('/Report/ReportDownload.aspx?rid=RPT-191&type=pdf');
                                    e.processOnServer =false;
                                  }
                                 }" />
                    </dx:ASPxButton>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    <dx:ASPxButton ID="btnDownload" runat="server" Text="Downlaod" Height="20px" Theme="Office2010Blue"
                        AutoPostBack="false" TabIndex="11">
                        <ClientSideEvents Click="function(s, e)
                                {
                                  e.processOnServer = confirm('Do you want to download Budget?');
                                  if (e.processOnServer)
                                  {
                                    window.open('/Report/ReportDownload.aspx?rid=RPT-191&type=xls');
                                    e.processOnServer =false;
                                  }
                                 }" />
                    </dx:ASPxButton>
                </div>
            </div>
            <div>
                &nbsp;&nbsp;</div>
            <div class="criteriaribben">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="caption">
                            <dx:ASPxLabel ID="lblBudgetNameCaption" CssClass="caption" runat="server" Text="Budget Name : "
                                Font-Bold="true">
                            </dx:ASPxLabel>
                        </span>
                    </div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <dx:ASPxLabel ID="lblBudgetName" runat="server" Text="BudgetName">
                        </dx:ASPxLabel>
                    </div>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    &nbsp;&nbsp;</div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="caption">
                            <dx:ASPxLabel ID="lblDateFromCaption" CssClass="caption" runat="server" Text="Date From : "
                                Font-Bold="true">
                            </dx:ASPxLabel>
                        </span>
                    </div>
                    <div style="float: left; vertical-align: middle; padding-top: 2px; padding-left: 5px;">
                        <dx:ASPxLabel ID="lblDateFrom" runat="server" Text="Date From">
                        </dx:ASPxLabel>
                    </div>
                </div>
                <div style="float: left; padding-top: 2px; padding-left: 5px;">
                    &nbsp;&nbsp;</div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="caption">
                            <dx:ASPxLabel ID="lblDateToCaption" CssClass="caption" runat="server" Text="Date To : "
                                Font-Bold="true">
                            </dx:ASPxLabel>
                        </span>
                    </div>
                    <div style="float: left; vertical-align: middle; padding-top: 2px; padding-left: 5px;">
                        <dx:ASPxLabel ID="lblDateTo" runat="server" Text="Date To">
                        </dx:ASPxLabel>
                    </div>
                </div>
                <div style="float: right; padding-left: 5px; padding-bottom: 2px; padding-right: 5px">
                    <dx:ASPxLabel ID="lblBudgetStatus" runat="server" Text="Budget Status" Font-Bold="True"
                        Font-Size="Large" ForeColor="#000099">
                    </dx:ASPxLabel>
                </div>
            </div>
            <div class="criteriaribben">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="caption">
                            <dx:ASPxLabel ID="lblProjectNameCaption" runat="server" Text="Projects : " Font-Bold="True">
                            </dx:ASPxLabel>
                        </span>
                    </div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <dx:ASPxLabel ID="lblProjectName" runat="server" Text="Project Name">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 1px;">
                        <dx:ASPxLabel ID="lblDetailCashBankBalance" runat="server" Text="Cash/Bank Balance"
                            CssClass="caption" EncodeHtml="False">
                        </dx:ASPxLabel>
                        <dx:ASPxGridView ID="gvCashBankDetails" runat="server" AutoGenerateColumns="False"
                            Width="501px" Font-Bold="False" ClientInstanceName="grd" EnableTheming="True"
                            Theme="Office2010Blue">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Cash/Bank" FieldName="LEDGER_NAME" Name="ColCashBankName"
                                    VisibleIndex="0" Width="250px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Amount" FieldName="AMOUNT" Name="colCashBankAmount"
                                    VisibleIndex="1" Width="92">
                                    <PropertiesTextEdit DisplayFormatString="{0:C}" NullDisplayText="0.00">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mode" FieldName="TRANS_MODE" Name="ColCashBankTransMode"
                                    VisibleIndex="2" Width="20">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowSort="False" />
                            <SettingsPager Visible="False" Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowColumnHeaders="true" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
            <div>
                <div>
                    <dx:ASPxGridView ID="gvBudgetView" runat="server" AutoGenerateColumns="False" KeyFieldName="LEDGER_ID"
                        Theme="Office2010Blue" Width="100%" Style="margin-top: 0px" ClientInstanceName="grd"
                        ViewStateMode="Enabled" OnHtmlRowCreated="gvBudgetView_HtmlRowCreated" OnHtmlRowPrepared="gvBudgetView_HtmlRowPrepared">
                        <ClientSideEvents Init="init" />
                        <Settings ShowGroupFooter="VisibleAlways" />
                        <GroupSummary>
                            <dx:ASPxSummaryItem FieldName="ACTUAL" ShowInGroupFooterColumn="colActual" SummaryType="Sum"
                                DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="APPROVED_PREVIOUS_YR" ShowInGroupFooterColumn="colAPPROVED_PREVIOUS_YR"
                                SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="PROPOSED_CURRENT_YR" ShowInGroupFooterColumn="colPROPOSED_CURRENT_YR"
                                SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="APPROVED_CURRENT_YR" ShowInGroupFooterColumn="colAPPROVED_CURRENT_YR"
                                SummaryType="Sum" DisplayFormat="{0:n2}" />
                        </GroupSummary>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="BudgetId" FieldName="BUDGET_ID" Visible="False"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="BudgetTransMode" FieldName="BUDGET_TRANS_MODE"
                                GroupIndex="0" Visible="false" Name="colBudgetTransMode">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="BudgetNature" FieldName="BUDGET_NATURE" VisibleIndex="2"
                                Visible="false" Name="colBudgetNature">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nature" FieldName="NATURE" Name="colNature" Visible="false"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Code" FieldName="LEDGER_CODE" Name="colCode"
                                VisibleIndex="3" Width="5%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ledger" FieldName="LEDGER_NAME" Name="colLedger"
                                VisibleIndex="4" Width="30%">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nature" FieldName="NATURE" Name="colNature" VisibleIndex="5"
                                Width="10%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Group" FieldName="LEDGER_GROUP" Name="colGroup"
                                Visible="false" Width="25%">
                                <%--VisibleIndex="6"--%>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Budget Group" FieldName="BUDGET_GROUP" Name="colBudgetGroup"
                                Visible="false" Width="5%">
                                <%--VisibleIndex="7" --%>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BUDGET_GROUP_SORT_ID" Visible="False" ShowInCustomizationForm="False"
                                SortOrder="Ascending" GroupIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Budget Sub Group" FieldName="BUDGET_SUB_GROUP"
                                Name="colBudgetSubGroup" VisibleIndex="8" Width="5%" GroupIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Actual" FieldName="ACTUAL" Name="colActual" VisibleIndex="10"
                                Width="8%">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <GroupFooterCellStyle CssClass="nonprintable bold">
                                </GroupFooterCellStyle>
                                <FooterCellStyle CssClass="bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Approved Previous" FieldName="APPROVED_PREVIOUS_YR"
                                Name="colAPPROVED_PREVIOUS_YR" VisibleIndex="9" Width="8%">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <GroupFooterCellStyle CssClass="nonprintable bold">
                                </GroupFooterCellStyle>
                                <FooterCellStyle CssClass="bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Proposed Current" FieldName="PROPOSED_CURRENT_YR"
                                Name="colPROPOSED_CURRENT_YR" VisibleIndex="11" Width="8%">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <GroupFooterCellStyle CssClass="nonprintable bold">
                                </GroupFooterCellStyle>
                                <FooterCellStyle CssClass="bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Approved Current" FieldName="APPROVED_CURRENT_YR"
                                Name="colAPPROVED_CURRENT_YR" VisibleIndex="13" Width="8%">
                                <DataItemTemplate>
                                    <dx:ASPxSpinEdit ID="txtSpEdit" runat="server" Width="120px" DisplayFormatString="{0:n1}"
                                        NullDisplayText="0.00" MaxLength="13" Text='<%# Bind("APPROVED_CURRENT_YR") %>'>
                                    </dx:ASPxSpinEdit>
                                </DataItemTemplate>
                                <HeaderStyle CssClass="nonprintable" />
                                <CellStyle CssClass="nonprintable">
                                </CellStyle>
                                <GroupFooterCellStyle CssClass="nonprintable bold">
                                </GroupFooterCellStyle>
                                <FooterCellStyle CssClass="nonprintable bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Approved Current" FieldName="APPROVED_CURRENT_YR"
                                Name="colAPPROVED_CURRENT_YR1" VisibleIndex="15" Width="8%">
                                <PropertiesTextEdit DisplayFormatString="{0:n1}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <HeaderStyle CssClass="nonprintableOnScreen" />
                                <CellStyle CssClass="nonprintableOnScreen">
                                </CellStyle>
                                <GroupFooterCellStyle CssClass="nonprintable bold">
                                </GroupFooterCellStyle>
                                <FooterCellStyle CssClass="nonprintableOnScreen bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="HO Narration" FieldName="HO_NARRATION" Name="colHONarration"
                                VisibleIndex="14" Width="10%">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtSpEditNarration" runat="server" Text='<%# Bind("HO_NARRATION") %>'
                                        MaxLength="150" Width="100%">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                                <HeaderStyle CssClass="nonprintable" />
                                <CellStyle CssClass="nonprintable">
                                </CellStyle>
                                <FooterCellStyle CssClass="nonprintable bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="HO Narration" PropertiesTextEdit-DisplayFormatString="{0:n2}"
                                FieldName="HO_NARRATION" Name="colHONarration1" VisibleIndex="16" Width="10%">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                </PropertiesTextEdit>
                                <HeaderStyle CssClass="nonprintableOnScreen" />
                                <CellStyle CssClass="nonprintableOnScreen">
                                </CellStyle>
                                <FooterCellStyle CssClass="nonprintableOnScreen bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Narration" FieldName="NARRATION" Name="colNARRATION"
                                VisibleIndex="17" Width="20%">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="false" />
                        <SettingsPager Mode="ShowAllRecords" Visible="False" />
                        <Settings ShowGroupButtons="false" />
                        <Styles>
                            <GroupRow Font-Bold="True" Font-Size="12pt">
                            </GroupRow>
                            <GroupFooter Font-Bold="true" Font-Size="10pt">
                            </GroupFooter>
                            <Header CssClass="customHeader">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxLabel ID="lblBudgetNote" runat="server" Text="Budget Note" Font-Bold="true">
                    </dx:ASPxLabel>
                </div>
            </div>
            <div>
                <div style="float: left; vertical-align: middle; padding-top: 2px; padding-left: 5px;">
                </div>
            </div>
            <p>
                &nbsp; &nbsp;</p>
            <div style="float: right">
                <table border="0">
                    <tr>
                        <td align="center">
                            <dx:ASPxLabel ID="lblPersonName" runat="server" Text="Person Name" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="lblRole" runat="server" Text="Role" Font-Bold="True">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function init(s, e) {
            var indentCell = $('td.customHeader').eq(0);
            var firstHeader = $('td.customHeader').eq(1);
            var firstHeader1 = $('td.customHeader').eq(2);
            indentCell.css('display', 'none');
            firstHeader.css('display', 'none');
            firstHeader1.css('display', 'none');
            //firstHeader.attr('colspan', 2);
        }

        function hideHeaderCell(s, e) {
            var indentCell = $('td.dxgvHeader').eq(0);
            indentCell.css('display', 'none');
        }  
    </script>
</asp:Content>
