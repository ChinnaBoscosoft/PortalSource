<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoucherDetailView.aspx.cs"
    Inherits="AcMeERP.Report.VoucherDetailView" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/MainTheme/mainstyle.css" type="text/css" />
</head>
<body>
    <form id="frmVoucherDetailView" runat="server">
    <div class="WrapperVoucherDetails">
        <div class="VoucherDetalilHeader">
            <asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <div style="width: 100%; float: left;">
            <div class="VoucherSegment bold">
                <div class="VoucherInnerSegment">
                    Branch
                </div>
                <div class="VoucherInnerSegment bold wordwrap" style="color: #3399FF;">
                    <asp:Label ID="lblBranchName" runat="server" Text=""></asp:Label>
                    
                </div>
            </div>
        </div>
        <br />
        <br />
        <div style="width: 100%; float: left;">
            <div class="VoucherSegment bold">
                <div class="VoucherInnerSegment">
                    Voucher Type
                </div>
                <div class="VoucherInnerSegment" style="font-weight: bold; color: #3399FF;">
                    <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="VoucherSegment bold" style="width: 40%;">
                <div class="VoucherInnerSegment">
                    Voucher Date
                </div>
                <div class="VoucherInnerSegment" style="font-weight: bold; color: #3399FF;">
                    <asp:Label ID="lblVoucherDate" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="VoucherSegment ">
                <div class="VoucherInnerSegment bold">
                    Voucher No
                </div>
                <div class="VoucherInnerSegment" style="font-weight: bold; color: #3399FF;">
                    <asp:Label ID="lblVoucherNo" runat="server" Text=""></asp:Label>
                    <asp:LinkButton ID="lbtnAmendment" Text="Make Amendment!!!" runat="server"  ToolTip="Click here to make amendment"
                        CssClass="blink" OnClick="lbtnAmendment_click"></asp:LinkButton>
                </div>
            </div>
        </div>
        <br />
        <br />
        <dx:ASPxGridView ID="gvMasterVoucher" runat="server" KeyFieldName="VOUCHER_ID" Width="100%"
            Theme="Office2010Blue" AutoGenerateColumns="False" ClientInstanceName="gvMasterVoucher">
            <Columns>
                <dx:GridViewDataColumn FieldName="VOUCHER_DATE" Caption="Date" VisibleIndex="1" ShowInCustomizationForm="True"
                    Width="80px">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="VOUCHER_NO" Caption="V.No" VisibleIndex="2" ShowInCustomizationForm="True"
                    Width="70px">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="VOUCHER_TYPE" Caption="Type" VisibleIndex="3" ShowInCustomizationForm="True"
                    Width="100px">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="LEDGER_NAME" Caption="Particulars" VisibleIndex="4"
                    Width="250px" ShowInCustomizationForm="True">
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="AMOUNT" Caption="Amount" VisibleIndex="5" ShowInCustomizationForm="True"
                    Width="150px">
                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="NAME" Caption="Donor" VisibleIndex="6" ShowInCustomizationForm="True">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="VOUCHER_ID" Caption="Voucher Id" Visible="False"
                    ShowInCustomizationForm="True">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="LEDGER_ID" Caption="Ledger Id" Visible="False"
                    ShowInCustomizationForm="True">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="PROJECT" Caption="PROJECT" ShowInCustomizationForm="True"
                    Visible="False">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="DEBIT" Caption="Debit" Visible="False" ShowInCustomizationForm="True">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="CREDIT" Caption="Credit" Visible="False" ShowInCustomizationForm="True">
                </dx:GridViewDataColumn>
            </Columns>
            <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="True" />
            <Settings ShowFilterRow="false" />
            <SettingsDetail ShowDetailRow="True" />
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="gvVoucherTrans" runat="server" KeyFieldName="VOUCHER_ID" OnBeforePerformDataSelect="gvVoucherTrans_BeforePerformDataSelect"
                        Theme="Office2010Blue" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="LEDGER_NAME" Caption="Name" VisibleIndex="0" ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="DEBIT" Caption="Debit" VisibleIndex="1" ShowInCustomizationForm="True">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CREDIT" Caption="Credit" VisibleIndex="2" ShowInCustomizationForm="True">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn FieldName="ACCOUNT_NUMBER" Caption="Account No" VisibleIndex="3"
                                ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="CHEQUE_NO" Caption="Ref.No / Cheque No" VisibleIndex="4"
                                ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="MATERIALIZED_ON" Caption="Materialized On" VisibleIndex="5"
                                ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="VOUCHER_ID" Caption="VoucherId" Visible="False"
                                ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="LEDGER_ID" Caption="Ledger Id" Visible="False"
                                ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="AMOUNT" Caption="Amount" Visible="False" ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="LEDGER_FLAG" Caption="Ledger Flag" Visible="False"
                                ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="TRANSMODE" Caption="TransMode" Visible="False"
                                ShowInCustomizationForm="True">
                            </dx:GridViewDataColumn>
                        </Columns>
                        <SettingsBehavior AllowDragDrop="False" AllowSort="false" />
                    </dx:ASPxGridView>
                </DetailRow>
            </Templates>
        </dx:ASPxGridView>
        <div class="div100" style="background-color: White; border-right: 1px solid #87CEEB;
            border-left: 1px solid #87CEEB;">
            <div class="VoucherInnerSegment bold">
                Name & Address</div>
            <div>
                <asp:TextBox ID="txtNAddress" runat="server" Text="" TextMode="MultiLine" CssClass="multiline textbox"
                    ReadOnly="true" Width="99%"></asp:TextBox>
            </div>
        </div>
        <div class="div100" style="background-color: White; border-right: 1px solid #87CEEB;
            border-left: 1px solid #87CEEB; border-bottom: 1px solid #87CEEB">
            <div class="VoucherInnerSegment bold">
                Narration</div>
            <div>
                <asp:TextBox ID="txtNarration" runat="server" Text="" TextMode="MultiLine" CssClass="multiline textbox"
                    ReadOnly="true" Width="99%"></asp:TextBox></div>
        </div>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
