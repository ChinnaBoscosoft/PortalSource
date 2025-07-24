<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="VoucherView.aspx.cs" Inherits="AcMeERP.Module.Master.VoucherView"
    Culture="auto" UICulture="auto" meta:resourcekey="PageResource3" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Src="~/WebControl/ucAccountBalance.ascx" TagName="Balance" TagPrefix="UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <script type="text/javascript" language="javascript">

        function OnCheckChangedReceipt(s, e) {
            var checkState = chkReceipt.GetCheckState();
            var rtchecked = chkReceipt.GetChecked();
            var pychecked = chkPayment.GetChecked();
            var Conchecked = chkContra.GetChecked();
            var jourchecked = chkJournal.GetChecked();
            if (!rtchecked && !pychecked && !Conchecked && !jourchecked) {
                chkPayment.SetChecked(true);
            }

        }
        function OnCheckChangedPayment(s, e) {
            var checkState = chkReceipt.GetCheckState();
            var rtchecked = chkReceipt.GetChecked();
            var pychecked = chkPayment.GetChecked();
            var Conchecked = chkContra.GetChecked();
            var jourchecked = chkJournal.GetChecked();
            if (!rtchecked && !pychecked && !Conchecked && !jourchecked) {
                chkContra.SetChecked(true);
            }

        }

        function OnCheckChangedContra(s, e) {
            var checkState = chkReceipt.GetCheckState();
            var rtchecked = chkReceipt.GetChecked();
            var pychecked = chkPayment.GetChecked();
            var Conchecked = chkContra.GetChecked();
            var jourchecked = chkJournal.GetChecked();
            if (!rtchecked && !pychecked && !Conchecked && !jourchecked) {
                chkJournal.SetChecked(true);
            }

        }
        function OnCheckChangedJournal(s, e) {
            var checkState = chkReceipt.GetCheckState();
            var rtchecked = chkReceipt.GetChecked();
            var pychecked = chkPayment.GetChecked();
            var Conchecked = chkContra.GetChecked();
            var jourchecked = chkJournal.GetChecked();
            if (!rtchecked && !pychecked && !Conchecked && !jourchecked) {
                chkReceipt.SetChecked(true);
            }

        }


        function ShowWindow() {

            pcAmendments.Show();
        }
        function showMessage() {
            document.getElementById('ltMessage').innerText = "Saved";
        }
      
    </script>
    <asp:UpdatePanel ID="upVoucherView" runat="server">
        <ContentTemplate>
            <div class="criteriaribben">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Branch </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbBranch" runat="server" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" TabIndex="1"
                            Theme="Office2010Blue" IncrementalFilteringMode="Contains" meta:resourcekey="cmbBranchResource2"
                            AutoPostBack="True" Width="250px">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Date From </span>
                    </div>
                    <div style="float: left; padding-left: 2px;">
                        <dx:ASPxDateEdit ID="dteDateFrom" runat="server" Width="90px" UseMaskBehavior="True" TabIndex="2"
                            Theme="Office2010Blue" EditFormat="Custom" meta:resourcekey="ASPxDateEdit1Resource2"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Date To </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxDateEdit ID="dteDateTo" runat="server" Width="90px" UseMaskBehavior="True"
                            Theme="Office2010Blue" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" TabIndex="3"
                            EditFormat="Custom" meta:resourcekey="ASPxDateEdit2Resource2">
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div style="float: right">
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxCheckBox ID="chkReceipt" runat="server" Text="Receipt" CheckState="Checked" TabIndex="4"
                            Theme="Office2010Blue" EnableClientSideAPI="True" ClientInstanceName="chkReceipt"
                            meta:resourcekey="ASPxCheckBox1Resource2" Checked="True">
                            <ClientSideEvents CheckedChanged="function(s, e) { OnCheckChangedReceipt(s, e); }" />
                        </dx:ASPxCheckBox>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxCheckBox ID="chkPayment" runat="server" Text="Payment" CheckState="Checked" TabIndex="5"
                            Theme="Office2010Blue" ClientInstanceName="chkPayment" meta:resourcekey="ASPxCheckBox2Resource2"
                            Checked="True">
                            <ClientSideEvents CheckedChanged="function(s, e) { OnCheckChangedPayment(s, e); }" />
                        </dx:ASPxCheckBox>
                    </div>
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                        <dx:ASPxCheckBox ID="chkContra" runat="server" Text="Contra" CheckState="Unchecked" TabIndex="6"
                            Theme="Office2010Blue" ClientInstanceName="chkContra" meta:resourcekey="ASPxCheckBox3Resource2">
                            <ClientSideEvents CheckedChanged="function(s, e) { OnCheckChangedContra(s, e); }" />
                        </dx:ASPxCheckBox>
                    </div>
                    <div style="float: left; padding-left: 5px; padding-right: 5px">
                        <dx:ASPxCheckBox ID="chkJournal" runat="server" Text="Journal" CheckState="Unchecked" TabIndex="7"
                            Theme="Office2010Blue" ClientInstanceName="chkJournal" meta:resourcekey="chkJournalResource1">
                            <ClientSideEvents CheckedChanged="function(s, e) { OnCheckChangedJournal(s, e); }" />
                        </dx:ASPxCheckBox>
                    </div>
                </div>
            </div>
            <div class="criteriaribben">
                <div>
                    <div style="float: right; padding-left: 5px; padding-bottom: 2px; padding-right: 5px">
                        <dx:ASPxButton ID="btnLoad" Text="Go" OnClick="btnLoad_Click" runat="server" Height="20px" TabIndex="9"
                            Theme="Office2010Blue" Width="100px" meta:resourcekey="btnLoadResource1">
                            <Image Url="~/App_Themes/MainTheme/images/go.png">
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbProject" runat="server" Width="300px" IncrementalFilteringMode="Contains" TabIndex="8"
                            Theme="Office2010Blue" meta:resourcekey="cmbProjectResource2">
                        </dx:ASPxComboBox>
                    </div>
                    <div style="float: right; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Project </span>
                    </div>
                </div>
            </div>
            <div>
                <UI:Balance ID="uiOpeningBalance" runat="server" />
            </div>
            <dx:ASPxGridView ID="gvMasterVoucher" runat="server" KeyFieldName="KeyValue" Width="100%"
                Theme="Office2010Blue" AutoGenerateColumns="False" ClientInstanceName="gvMasterVoucher"
                meta:resourcekey="gvMasterVoucherResource2" OnLoad="gvMasterVoucher_Load" OnHtmlRowPrepared="gvMasterVoucher_HtmlRowPrepared"
                OnRowCommand="gvMasterVoucher_RowCommand">
                <Columns>
                    <dx:GridViewDataColumn Name="ColAmendment" Width="30px" VisibleIndex="0" 
                        meta:resourcekey="GridViewDataColumnResource51" ShowInCustomizationForm="True">
                        <DataItemTemplate>
                            <dx:ASPxButton ID="btnAmendment" runat="server" AllowFocus="False" ToolTip="Click to make amendment"
                                RenderMode="Link" EnableTheming="False" CommandName="Amendments" meta:resourcekey="btnAmendmentResource1">
                                <Image>
                                    <SpriteProperties CssClass="blueBall" />
                                </Image>
                            </dx:ASPxButton>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VOUCHER_DATE" Caption="Date" VisibleIndex="1" 
                        Width="80px" meta:resourcekey="GridViewDataColumnResource52" 
                        ShowInCustomizationForm="True">
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VOUCHER_NO" Caption="V.No" VisibleIndex="2" 
                        Width="70px" meta:resourcekey="GridViewDataColumnResource53" 
                        ShowInCustomizationForm="True">
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VOUCHERTYPE" Caption="Type" VisibleIndex="3" 
                        Width="100px" meta:resourcekey="GridViewDataColumnResource54" 
                        ShowInCustomizationForm="True">
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="LEDGER_NAME" Caption="Particulars" VisibleIndex="4"
                        Width="250px" meta:resourcekey="GridViewDataColumnResource55" 
                        ShowInCustomizationForm="True">
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="AMOUNT" Caption="Amount" VisibleIndex="5" 
                        Width="150px" meta:resourcekey="GridViewDataTextColumnResource2" 
                        ShowInCustomizationForm="True">
                        <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                        </PropertiesTextEdit>
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn FieldName="DONOR_NAME" Caption="Donor" VisibleIndex="6" 
                        Width="150px" meta:resourcekey="GridViewDataColumnResource56" 
                        ShowInCustomizationForm="True">
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AMENDMENT_FLAG" Caption="Amendment" VisibleIndex="7"
                        Visible="False" meta:resourcekey="GridViewDataColumnResource57" 
                        ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="REMARKS" Caption="Amendment" VisibleIndex="7"
                        Width="200px" meta:resourcekey="GridViewDataColumnResource58" 
                        ShowInCustomizationForm="True">
                         <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VOUCHER_ID" Caption="Voucher Id" 
                        Visible="False" meta:resourcekey="GridViewDataColumnResource59" 
                        ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FD_ACCOUNT_ID" Caption="FD Account Id" 
                        Visible="False" meta:resourcekey="GridViewDataColumnResource60" 
                        ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="LEDGER_ID" Caption="Voucher Id" 
                        Visible="False" meta:resourcekey="GridViewDataColumnResource61" 
                        ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PROJECT" Caption="PROJECT" Visible="False" 
                        meta:resourcekey="GridViewDataColumnResource62" ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VOUCHER_SUB_TYPE" Caption="Voucher Sub type" 
                        Visible="False" meta:resourcekey="GridViewDataColumnResource63" 
                        ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DEBIT" Caption="Debit" Visible="False" 
                        meta:resourcekey="GridViewDataColumnResource64" ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CREDIT" Caption="Credit" Visible="False" 
                        meta:resourcekey="GridViewDataColumnResource65" ShowInCustomizationForm="True">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="BRANCH_ID" Caption="BranchId" 
                        Visible="False" meta:resourcekey="GridViewDataTextColumnResource3" 
                        ShowInCustomizationForm="True">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="LOCATION_ID" Caption="LocationId" 
                        Visible="False"
                        ShowInCustomizationForm="True">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="True" />
                <Settings ShowFilterRow="True" ShowHeaderFilterButton="true" />
                <SettingsDetail ShowDetailRow="True" />
                <SettingsPager Position="TopAndBottom" PageSize="20">
                    <PageSizeItemSettings Items="30, 40, 50" Visible="True">
                    </PageSizeItemSettings>
                </SettingsPager>
                <ClientSideEvents SelectionChanged="function(s, e) { CheckSelect(s, e); }" />
                <Templates>
                    <DetailRow>
                        <dx:ASPxGridView ID="gvVoucherTrans" runat="server" KeyFieldName="VOUCHER_ID" OnBeforePerformDataSelect="gvVoucherTrans_BeforePerformDataSelect"
                            Theme="Office2010Blue" Width="100%" AutoGenerateColumns="False" meta:resourcekey="gvVoucherTransResource2">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="LEDGER_NAME" Caption="Name" VisibleIndex="0" meta:resourcekey="GridViewDataColumnResource38"
                                    ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataTextColumn FieldName="DEBIT" Caption="Debit" VisibleIndex="1" meta:resourcekey="GridViewDataTextColumnResource1"
                                    ShowInCustomizationForm="True">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CREDIT" Caption="Credit" VisibleIndex="2" meta:resourcekey="GridViewDataColumnResource40"
                                    ShowInCustomizationForm="True">
                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataColumn FieldName="ACCOUNT_NUMBER" Caption="Account No" VisibleIndex="3"
                                    meta:resourcekey="GridViewDataColumnResource41" ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="CHEQUE_NO" Caption="Ref.No / Cheque No" VisibleIndex="4"
                                    meta:resourcekey="GridViewDataColumnResource42" ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MATERIALIZED_ON" Caption="Materilaized On" VisibleIndex="5"
                                    meta:resourcekey="GridViewDataColumnResource43" ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="VOUCHER_ID" Caption="VoucherId" Visible="False"
                                    meta:resourcekey="GridViewDataColumnResource44" ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LEDGER_ID" Caption="Ledger Id" Visible="False"
                                    meta:resourcekey="GridViewDataColumnResource45" ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="AMOUNT" Caption="Amount" Visible="False" meta:resourcekey="GridViewDataColumnResource46"
                                    ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="LEDGER_FLAG" Caption="Ledger Flag" Visible="False"
                                    meta:resourcekey="GridViewDataColumnResource47" ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="TRANSMODE" Caption="TransMode" Visible="False"
                                    meta:resourcekey="GridViewDataColumnResource48" ShowInCustomizationForm="True">
                                </dx:GridViewDataColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" />
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
            </dx:ASPxGridView>
            <div>
                <UI:Balance ID="uiClosingBalance" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upAmendments" runat="server">
        <ContentTemplate>
            <div>
                <div id="ModalOverlay" class="modal_popup_overlay">
                </div>
                <asp:Panel ID="pnlUpdateDB" runat="server" meta:resourcekey="pnlUpdateDBResource1">
                    <div id="Display" class="modal_popup_logo">
                        <div class="div100 modal_popup_title">
                            <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                                Amendment Notes
                            </div>
                            <div class="floatright">
                                <asp:ImageButton runat="server" ImageAlign="AbsMiddle" OnClientClick="javascript:HideDisplayPopUp();"
                                    CssClass="handcursor" ImageUrl="../../App_Themes/MainTheme/images/PopupClose.png"
                                    ID="img2" ToolTip="Close" meta:resourcekey="img2Resource1"></asp:ImageButton>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="row-fluid textcenter red">
                                <asp:Literal ID="ltMessage" runat="server" meta:resourcekey="ltMessageResource1"></asp:Literal>
                            </div>
                            <br />
                            <div class="row-fluid">
                                <div class="span5 textright fieldcaption width35per">
                                    <asp:Literal ID="Literal2" runat="server" Text="Voucher Date" meta:resourcekey="Literal2Resource1"></asp:Literal>
                                </div>
                                <div class="span7 ">
                                    <asp:Literal ID="ltVoucherDate" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright fieldcaption width35per">
                                    <asp:Literal ID="Literal6" runat="server" Text="Voucher No" meta:resourcekey="Literal6Resource1"></asp:Literal>
                                </div>
                                <div class="span7 ">
                                    <asp:Literal ID="ltVoucherNo" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright fieldcaption width35per">
                                    <asp:Literal ID="Literal10" runat="server" Text="Voucher Type" meta:resourcekey="Literal10Resource1"></asp:Literal>
                                </div>
                                <div class="span7 ">
                                    <asp:Literal ID="ltVoucherType" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright fieldcaption width35per">
                                    <asp:Literal ID="Literal13" runat="server" Text="Particulars" meta:resourcekey="Literal13Resource1"></asp:Literal>
                                </div>
                                <div class="span7 ">
                                    <asp:Literal ID="ltLedger" runat="server" meta:resourcekey="ltLedgerResource1"></asp:Literal>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright fieldcaption width35per">
                                    <asp:Literal ID="Literal15" runat="server" Text="Amount" meta:resourcekey="Literal15Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:Literal ID="ltAmount" runat="server" meta:resourcekey="ltAmountResource1"></asp:Literal>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright fieldcaption width35per">
                                    <asp:Literal ID="Literal17" runat="server" Text="Description *" meta:resourcekey="Literal17Resource1"></asp:Literal>
                                </div>
                                <div class="span7 bold">
                                    <asp:TextBox ID="txtDescription" runat="server" Width="250px" CssClass="textbox multiline manfield"
                                        CausesValidation="True" MaxLength="500" TextMode="MultiLine" ToolTip="Enter the Description here"
                                        meta:resourcekey="txtDescriptionResource2"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div style="text-align: center">
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7 bold">
                                    <asp:Button ID="BtnOk" Text="Ok" runat="server" CssClass="button" OnClick="btnOk_Click"
                                        meta:resourcekey="BtnOkResource1"></asp:Button>
                                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close"
                                        CausesValidation="False" OnClientClick="javascript:HideDisplayPopUp()" meta:resourcekey="btnCloseResource1">
                                    </asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function ShowDisplayPopUp(modal) {
            $("#ModalOverlay").show();
            $("#Display").fadeIn(300);
            if (modal) {
                $("#ModalOverlay").unbind("click");
            }
            else {
                $("#ModalOverlay").click(function (e) {

                });
            }
        }

        function HideDisplayPopUp() {
            $("#ModalOverlay").hide();
            $("#Display").fadeOut(300);
        }

    </script>
</asp:Content>
