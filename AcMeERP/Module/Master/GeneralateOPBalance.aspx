<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="GeneralateOPBalance.aspx.cs" Inherits="AcMeERP.Module.Master.GeneralateOPBalance"
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
            <div class="reportAddress nonprintableOnScreen">
                <div class="reportAddress">
                    <div class="reportAddress">
                        <dx:ASPxLabel ID="lblSocietyAddress" runat="server" Text="Address" CssClass="reportAddress"
                            Font-Size="Small">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <div class="reportSubTitle nonprintableOnScreen">
                <div class="reportSubTitle">
                    <div class="reportSubTitle">
                        <dx:ASPxLabel ID="lblReportTitle" runat="server" Text="Monthly Budget and Expenses"
                            CssClass="reportSubTitle" Font-Size="Large">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <div class="reportSubTitle nonprintableOnScreen">
                <div class="reportSubTitle">
                    <div class="reportSubTitle">
                        <dx:ASPxLabel ID="lblMonth" runat="server" Text="Month : Budget for the month of Apr 2019"
                            CssClass="reportSubTitle" Font-Size="Large">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <div class="reportSubTitle1 nonprintableOnScreen">
                <div class="reportSubTitle1">
                    <div class="reportSubTitle1">
                        <dx:ASPxLabel ID="lblBranchName" runat="server" Text="Institution Name : Christaraja Institute"
                            CssClass="reportSubTitle1" Font-Size="Medium">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            </div>
            <div class="reportSubTitle1 nonprintableOnScreen">
                <div class="reportSubTitle1">
                    <div class="reportSubTitle1">
                        <span class="bold"><span class="bold">
                            <dx:ASPxLabel ID="lblProject" runat="server" Text="Project" CssClass="reportSubTitle1"
                                Font-Size="Medium">
                            </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <div class="criteriaribben nonprintable">
                <div style="float: left; padding-left: 5px; padding-bottom: 2px; padding-right: 5px">
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
                    </div>
                </div>
                <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                    </div>
                </div>
                <div class="floatright" style="padding-right: 5px">
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                    </div>
                </div>
            </div>
            <div class="criteriaribben nonprintable">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="caption">As on : </span>
                    </div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <dx:ASPxLabel ID="lblYear" runat="server" Text="Year" Font-Bold="True">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <div>
                <div>
                    <dx:ASPxGridView ID="gvGeneralateOpeningBalance" runat="server" AutoGenerateColumns="False"
                        KeyFieldName="LEDGER_ID" Theme="Office2010Blue" Width="100%" Style="margin-top: 0px"
                        ClientInstanceName="grd">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="ConLedgerId" FieldName="CON_LEDGER_ID" Visible="False"
                                VisibleIndex="0">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="S.No" FieldName="CON_LEDGER_CODE" Name="colCode"
                                Visible="true" Width="20px" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ledger" FieldName="CON_LEDGER_NAME" VisibleIndex="2"
                                Name="colLedger" Width="960px">
                                <Settings AutoFilterCondition="Contains" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Amount" FieldName="AMOUNT" Name="colAmount" VisibleIndex="3"
                                Width="60px">
                                <DataItemTemplate>
                                    <dx:ASPxSpinEdit ID="txtOPAmount" runat="server" DisplayFormatString="{0:n2}" NullDisplayText="0.00"
                                        Text='<%# Bind("AMOUNT") %>'>
                                    </dx:ASPxSpinEdit>
                                </DataItemTemplate>
                                <HeaderStyle CssClass="nonprintable gridheaderheight" Wrap="True" />
                                <CellStyle CssClass="nonprintable gridtextboxcell">
                                </CellStyle>
                                <FooterCellStyle CssClass="nonprintable bold">
                                </FooterCellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="false" />
                        <SettingsPager Mode="ShowAllRecords" Visible="False" />
                        <Settings ShowFooter="true" ShowGroupedColumns="false" ShowGroupPanel="false" GroupFormat="{1}" />
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="AMOUNT" SummaryType="Sum" DisplayFormat="{0:n2}" />
                        </TotalSummary>
                        <%--<ClientSideEvents Init="function(s,e){hideHeaderCell(s,e)}" EndCallback="function(s,e){hideHeaderCell(s,e)}" />--%>
                    </dx:ASPxGridView>
                </div>
                <p>
                </p>
                <div class="footer">
                    <div>
                    </div>
                </div>
            </div>
            <p>
                &nbsp; &nbsp;</p>
            <div style="float: right">
                <table border="0">
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
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
