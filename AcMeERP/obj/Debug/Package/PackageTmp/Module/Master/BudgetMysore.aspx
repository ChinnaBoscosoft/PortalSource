<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="BudgetMysore.aspx.cs" Inherits="AcMeERP.Module.Master.BudgetMysore"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <style type="text/css">
        .dxeBase
        {
            font: 12px Tahoma, Geneva, sans-serif;
        }
        .style1
        {
            width: 281px;
        }
        .style2
        {
            width: 94px;
        }
        .style3
        {
            width: 251px;
        }
    </style>
</asp:Content>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upFdRegistersView" runat="server">
        <ContentTemplate>
            <%--<link href="../../App_Themes/MainTheme/Printstyle.css" type="text/css" />--%>
            <div >
                <div class="reportTitle nonprintableOnScreen">
                    <div class="reportTitle">
                            <dx:ASPxLabel ID="lblSocietyName" runat="server" CssClass="reportSubTitle" 
                                Text="SocietyName" Font-Bold="True" Font-Size="X-Large"></dx:ASPxLabel>
                    </div>
               </div>
            </div>
            <div class="reportAddress nonprintableOnScreen">
                <div class="reportAddress">
                    <div class="reportAddress">
                        <dx:ASPxLabel ID="lblSocietyAddress" runat="server" Text="Address" CssClass="reportAddress" Font-Size="Small"></dx:ASPxLabel>
                    </div>
               </div>
            </div>
             <div class="reportSubTitle nonprintableOnScreen">
                <div class="reportSubTitle">
                    <div class="reportSubTitle">
                            <dx:ASPxLabel ID="lblReportTitle" runat="server" Text="Monthly Budget and Expenses" CssClass="reportSubTitle" Font-Size="Large">
                            </dx:ASPxLabel>
                    </div>
               </div>
            </div>
             <div class="reportSubTitle nonprintableOnScreen">
                <div class="reportSubTitle">
                    <div class="reportSubTitle">
                        <dx:ASPxLabel ID="lblMonth" runat="server" Text="Month : Budget for the month of Apr 2019" CssClass="reportSubTitle" Font-Size="Large">
                            </dx:ASPxLabel>
                    </div>
               </div>
            </div>
              <div class="reportSubTitle1 nonprintableOnScreen">
                <div class="reportSubTitle1">
                    <div class="reportSubTitle1">
                        <dx:ASPxLabel ID="lblBranchName" runat="server" Text="Institution Name : Christaraja Institute"  CssClass="reportSubTitle1" Font-Size="Medium">
                            </dx:ASPxLabel>
                    </div>
               </div>
            </div>
            </div>
              <div class="reportSubTitle1 nonprintableOnScreen">
                <div class="reportSubTitle1">
                    <div class="reportSubTitle1">
                        <span class="bold"><span class="bold"><dx:ASPxLabel ID="lblProject" runat="server" Text="Project"  CssClass="reportSubTitle1" Font-Size="Medium">
                            </dx:ASPxLabel>
                    </div>
               </div>
            </div>
           <p></p>

            <div class="criteriaribben nonprintable">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Branch </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbBranch" runat="server" AutoPostBack="True" IncrementalFilteringMode="Contains"
                            Width="200px" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" Theme="Office2010Blue"
                            TabIndex="1">
                        </dx:ASPxComboBox>
                        &nbsp;
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Project </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbProject" runat="server" TabIndex="2" AutoPostBack="True"
                            IncrementalFilteringMode="Contains" OnSelectedIndexChanged="cmbProject_SelectedIndexChanged"
                            Theme="Office2010Blue">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Budget </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbBudget" runat="server" TabIndex="2" Theme="Office2010Blue">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div style="float: left; padding-left: 5px; padding-bottom: 2px; padding-right: 5px">
                    <dx:ASPxButton ID="btnLoad" Text="Go" OnClick="btnLoad_Click" Theme="Office2010Blue"
                        TabIndex="5" runat="server" Height="20px" Width="80px">
                        <Image Url="~/App_Themes/MainTheme/images/go.png">
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; padding-left: 5px; padding-bottom: 2px; padding-right: 5px">
                    <dx:ASPxLabel ID="lblBudgetStatus" runat="server" Text="Budget Status" Font-Bold="True"
                        Font-Size="Large" ForeColor="#000099">
                    </dx:ASPxLabel>
                </div>
                <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                        <dx:ASPxButton ID="btnApproved" runat="server" Text="Approve" Height="20px" Theme="Office2010Blue"
                            OnClick="btnApproved_Click" TabIndex="6">
                            <%--<ClientSideEvents Click="function(s, e)
                                {
                                 e.processOnServer = confirm('Are you sure to Approve Budget?');
                                 }" />--%>
                        </dx:ASPxButton>
                    </div>
                </div>
                <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxButton ID="btnSave" runat="server" Text="Save" Height="20px" Theme="Office2010Blue"
                            OnClick="btnSave_Click" TabIndex="7">
                        </dx:ASPxButton>
                    </div>
                </div>
                <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                        <dx:ASPxButton ID="btnMove" runat="server" Text="Move" Height="20px" Theme="Office2010Blue"
                            OnClick="btnMove_Click" TabIndex="8">
                            <ClientSideEvents Click="function(s, e){ e.processOnServer = confirm('Are you sure to move Proposed Amount to Approve Amount?');}" />
                        </dx:ASPxButton>
                    </div>
                </div>
                <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                        <dx:ASPxButton ID="btnPrint" runat="server" Text="Print" Height="20px" Theme="Office2010Blue"
                            AutoPostBack="false" TabIndex="9" Visible="false">
                            <ClientSideEvents Click="function(s, e)
                                {
                                  e.processOnServer = confirm('Do you want to Print Budget?');
                                  if (e.processOnServer)
                                  {
                                    window.print(); 
                                    e.processOnServer =false;
                                  }
                                 }" />
                        </dx:ASPxButton>
                    </div>
                </div>
                <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                        <dx:ASPxButton ID="btnPrintPreview" runat="server" Text="Print Preview" Height="20px"
                            Theme="Office2010Blue" AutoPostBack="false" TabIndex="10">
                            <ClientSideEvents Click="function(s, e)
                                {
                                  e.processOnServer = confirm('Do you want to show Print Preview Budget?');
                                  if (e.processOnServer)
                                  {
                                    window.open('/Report/ReportDownload.aspx?rid=RPT-171&type=pdf');
                                    e.processOnServer =false;
                                  }
                                 }" />
                        </dx:ASPxButton>
                    </div>
                </div>
                 <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                        <dx:ASPxButton ID="btnDownload" runat="server" Text="Downlaod" Height="20px"
                            Theme="Office2010Blue" AutoPostBack="false" TabIndex="11">
                            <ClientSideEvents Click="function(s, e)
                                {
                                  e.processOnServer = confirm('Do you want to download Budget?');
                                  if (e.processOnServer)
                                  {
                                    window.open('/Report/ReportDownload.aspx?rid=RPT-171&type=xls');
                                    e.processOnServer =false;
                                  }
                                 }" />
                        </dx:ASPxButton>
                    </div>
                </div>
            </div>
            <div class="criteriaribben nonprintable">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="caption">Budget Name : </span>
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
                        <span class="caption">Date From : </span>
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
                        <span class="caption">Date To : </span>
                    </div>
                    <div style="float: left; vertical-align: middle; padding-top: 2px; padding-left: 5px;">
                        <dx:ASPxLabel ID="lblDateTo" runat="server" Text="Date To">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <div>
                <div>
                    
            <dx:ASPxGridView ID="gvBudgetView" runat="server" AutoGenerateColumns="False" KeyFieldName="LEDGER_ID"
                        Theme="Office2010Blue" Width="100%" Style="margin-top: 0px" ClientInstanceName="grd"
                        OnHtmlRowCreated="gvBudgetView_HtmlRowCreated" OnHtmlRowPrepared="gvBudgetView_HtmlRowPrepared"
                        OnHtmlDataCellPrepared="gvBudgetView_HtmlDataCellPrepared">
                        <Columns>
                        <dx:GridViewDataTextColumn Caption="SubLedgerId" FieldName="SUB_LEDGER_ID" Name="colSubLegerId"
                                Visible="false" Width="30px" VisibleIndex="0">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="BudgetGroup" FieldName="BUDGET_GROUP_ID" Name="colBudgetGroup"
                                Visible="true" GroupIndex="0" VisibleIndex="1">
                                <Settings AutoFilterCondition="Contains" AllowGroup="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="BudgetId" FieldName="BUDGET_ID" Visible="False"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="S.No" FieldName="LEDGER_CODE" Name="colCode"
                                VisibleIndex="3" Width="20px">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ledger" FieldName="LEDGER_NAME" Name="colLedger"
                                VisibleIndex="4" Width="320px">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Approved Previous" FieldName="APPROVED_PREVIOUS_YR"
                                Name="colAPPROVED_PREVIOUS_YR" VisibleIndex="5" Width="100px">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <HeaderStyle CssClass="gridheaderheight" Wrap="True"></HeaderStyle>
                                <FooterCellStyle CssClass="bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Actual" FieldName="ACTUAL" Name="colActual" VisibleIndex="6"
                                Width="100px">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <HeaderStyle CssClass="gridheaderheight" Wrap="True"></HeaderStyle>
                                <FooterCellStyle CssClass="bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Balance" FieldName="BALANCE" Name="colBalance"
                                VisibleIndex="7" Width="100px" >
                                 <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <HeaderStyle CssClass="gridheaderheight" Wrap="True"></HeaderStyle>
                                <FooterCellStyle CssClass="bold"></FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Proposed Current" FieldName="PROPOSED_CURRENT_YR"
                                Name="colPROPOSED_CURRENT_YR" VisibleIndex="8" Width="100px">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <HeaderStyle CssClass="gridheaderheight" Wrap="True"></HeaderStyle>
                                <FooterCellStyle CssClass="bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Approved Current" FieldName="APPROVED_CURRENT_YR"
                                Name="colAPPROVED_CURRENT_YR" VisibleIndex="9" Width="90px">
                                <DataItemTemplate>
                                    <dx:ASPxSpinEdit ID="txtM1ApprovedAmount" runat="server" Text='<%# Bind("APPROVED_CURRENT_YR") %>'
                                       Width="90px" DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                    </dx:ASPxSpinEdit>
                                </DataItemTemplate>
                                <HeaderStyle CssClass="nonprintable gridheaderheight" Wrap="True" />
                                <CellStyle CssClass="nonprintable gridtextboxcell">
                                </CellStyle>
                                <FooterCellStyle CssClass="nonprintable bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Proposed Next" FieldName="M2_PROPOSED_AMOUNT"
                                Name="colM2PROPOSED_AMOUNT" VisibleIndex="10" Width="100px">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains" />
                                <HeaderStyle CssClass="gridheaderheight" Wrap="True"></HeaderStyle>
                                <FooterCellStyle CssClass="bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Approved Next" FieldName="M2_APPROVED_AMOUNT"
                                Name="colM2APPROVED_AMOUNT" VisibleIndex="11" Width="100px">
                                <DataItemTemplate >
                                    <dx:ASPxSpinEdit ID="txtM2ApprovedAmount" runat="server" Text='<%# Bind("M2_APPROVED_AMOUNT") %>'
                                        Width="90px" DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                    </dx:ASPxSpinEdit>
                                </DataItemTemplate>
                                <HeaderStyle CssClass="nonprintable gridheaderheight" Wrap="True" />
                                <CellStyle CssClass="nonprintable gridtextboxcell">
                                </CellStyle>
                                <FooterCellStyle CssClass="nonprintable bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Head Office Narration" FieldName="HO_NARRATION"
                                Name="colHONarration" VisibleIndex="12" Width="210px">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtSpEditNarration" runat="server" Text='<%# Bind("HO_NARRATION") %>'
                                        MaxLength="145" Width="230px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                                <HeaderStyle CssClass="nonprintable" />
                                <CellStyle CssClass="nonprintable">
                                </CellStyle>
                                <FooterCellStyle CssClass="nonprintable bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn> 
                            <dx:GridViewDataTextColumn Caption="Head Office Narration" PropertiesTextEdit-DisplayFormatString="{0:n2}"
                                FieldName="HO_NARRATION" Name="colHONarration1" VisibleIndex="13" Width="180px">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}">
                                </PropertiesTextEdit>
                                <HeaderStyle CssClass="nonprintableOnScreen" />
                                <CellStyle CssClass="nonprintableOnScreen">
                                </CellStyle>
                                <FooterCellStyle CssClass="nonprintableOnScreen bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Narration" FieldName="NARRATION" Name="colNARRATION"
                                VisibleIndex="14" Width="260px">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            
                        </Columns>
                        <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="false" />
                        <SettingsPager Mode="ShowAllRecords" Visible="False" />
                        <Settings ShowFooter="true" ShowGroupedColumns="false" ShowGroupPanel="false" GroupFormat="{1}" />
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="ACTUAL" SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="APPROVED_PREVIOUS_YR" SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="BALANCE" SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="PROPOSED_CURRENT_YR" SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="APPROVED_CURRENT_YR" SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="M2_PROPOSED_AMOUNT" SummaryType="Sum" DisplayFormat="{0:n2}" />
                            <dx:ASPxSummaryItem FieldName="M2_APPROVED_AMOUNT" SummaryType="Sum" DisplayFormat="{0:n2}" />
                        </TotalSummary>
                        <%--<ClientSideEvents Init="function(s,e){hideHeaderCell(s,e)}" EndCallback="function(s,e){hideHeaderCell(s,e)}" />--%>
                    </dx:ASPxGridView>
                </div>
                <p></p>
                <div class="footer">
                    <div>
                        <table border="0" style="width: 365px">
                            <tr>
                                <td><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblOPCaption" runat="server" Text="Opening Balance" Font-Bold="True"
                                        Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel>
                                    </div>
                                </td>
                                <td>
                                <div align="right">
                                    <dx:ASPxLabel ID="lblOP" runat="server" Text="0.00" Font-Bold="True">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblBanKBalancePerBankCaption" runat="server" Text="Balance as per Bank" Font-Bold="False"
                                        Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td><div align="right">
                                    <dx:ASPxLabel ID="lblBanKBalancePerBank" runat="server" Text="0.00" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                <div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblBankBalanceCBCaption" runat="server" Text="Balance as per Cash Book" Font-Names="Tahoma"
                                        Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td class="style1"><div align="right">
                                    <dx:ASPxLabel ID="lblBankBalanceCB" runat="server" Text="0.00" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblDifferenceCaption" runat="server" Text="Difference" Font-Names="Tahoma"
                                        Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td class="style1"><div align="right">
                                    <dx:ASPxLabel ID="lblDifference" runat="server" Text="0.00" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblBRSCaption" runat="server" Text="Bank Reconcilation" Font-Names="Tahoma"
                                        Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td class="style1"><div align="right">
                                    <dx:ASPxLabel ID="lblBRS" runat="server" Text="" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblBRSCaption1" runat="server" Text=" ">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td class="style1"><div align="right">
                                    <dx:ASPxLabel ID="lblBRS1" runat="server" Text="0.00" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblTotalBudgetCaption" runat="server" Text="Total Budget of" Font-Names="Tahoma"
                                        Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td class="style1"><div align="right">
                                    <dx:ASPxLabel ID="lblTotalBudget" runat="server" Text="0.00" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblBalanceCaption" runat="server" Text="Balance from Previous Month Budget July"
                                        Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td class="style1"><div align="right">
                                    <dx:ASPxLabel ID="lblBalance" runat="server" Text="0.00" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3"><div align="left" style="width: 275px">
                                    <dx:ASPxLabel ID="lblMainDifferenceCaption" runat="server" Text="Difference" Font-Bold="True"
                                        Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                                <td class="style1"><div align="right">
                                    <dx:ASPxLabel ID="lblMainDifference" runat="server" Text="0.00" Font-Names="Tahoma" Font-Size="9pt">
                                    </dx:ASPxLabel></div>
                                </td>
                            </tr>
                        </table>
                    </div>
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
            </span></span>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        //        function hideHeaderCell(s, e) {

        //            var indentCell = $('td.dxgvHeader_Office2010Blue').eq(1);
        //            var firstHeader = $('td.dxgvHeader_Office2010Blue').eq(1);
        //            firstHeader.css('display', 'none');
        //            indentCell.css('display', 'none');
        //        }
    </script>
</asp:Content>
