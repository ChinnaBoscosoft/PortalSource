<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="LedgerAdd.aspx.cs" Inherits="AcMeERP.Module.Master.LedgerAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Src="~/WebControl/DateControl.ascx" TagName="Datecontrol" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script language="javascript" type="text/javascript">
        function validateIncomeCheckboxes_ClientValidate() {
            var chkBank = document.getElementById("<%=chkBankInterestLedger.ClientID%>");
            var chkInKind = document.getElementById("<%=chkInKindLedger.ClientID%>");
            var chkAsset = document.getElementById("<%=chkAssetGainLedger.ClientID%>");

            if (chkBank.checked && chkInKind.checked) {
                return false;
            }
            else if (chkInKind.checked && chkAsset.checked) {
                return false;
            }
            else if (chkBank.checked && chkAsset.checked) {
                return false;
            }
            else {

                return true;
            }
        }
        function validateExpenseCheckboxes_ClientValidate() {

            var chkDisposal = document.getElementById("<%=chkDisposalLedger.ClientID%>");
            var chkAssetLoss = document.getElementById("<%=chkAssetLossLedger.ClientID%>");
            var chkDepre = document.getElementById("<%=chkDepreciationLedger.ClientID%>");

            if (chkDisposal.checked && chkDepre.checked) {
                return false;
            }
            else if (chkDepre.checked && chkAssetLoss.checked) {
                return false;
            }
            else if (chkDisposal.checked && chkAssetLoss.checked) {
                return false;
            }
            else {

                return true;
            }
        }
        function chkSelectAll_CheckChanged(s, e) {
            gvProjectCategory.SelectAllRowsOnPage(s.GetChecked());
        }
        function SelectionChanged(s, e) {
            colSelectAll.SetChecked(false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upLedgerView" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span10 offset1">
                    <asp:Panel ID="pnlLedgerInfo" GroupingText="Ledger Info" runat="server" meta:resourcekey="pnlLedgerInfoResource1">
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal4" runat="server" Text="Under the Group *" meta:resourcekey="Literal4Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="combobox manfield" Width="255px"
                                    AutoPostBack="True" ToolTip="Select Ledger Group" meta:resourcekey="ddlGroupResource1"
                                    OnTextChanged="ddlGroup_TextChanged">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Group is required"
                                    CssClass="requiredcolor" ControlToValidate="ddlGroup" Operator="NotEqual" Type="Integer"
                                    ValueToCompare="0" meta:resourcekey="CompareValidator2Resource1" Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal10" runat="server" Text="Code" meta:resourcekey="Literal10Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="textbox" MaxLength="7" ToolTip="Enter the Code"
                                    meta:resourcekey="txtCodeResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal2" runat="server" Text="Ledger Type *" meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlLedgerType" runat="server" CssClass="combobox manfield"
                                    Width="255px" ToolTip="Select Ledger Type" meta:resourcekey="ddlLedgerTypeResource1">
                                    <asp:ListItem Value="0" Text="-Select-" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="General" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="InKind" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmpRl" runat="server" ErrorMessage="Ledger Type is required"
                                    CssClass="requiredcolor" ControlToValidate="ddlLedgerType" Operator="NotEqual"
                                    Type="Integer" ValueToCompare="0" meta:resourcekey="cmpRlResource1" Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal3" runat="server" Text="Name *" meta:resourcekey="Literal3Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtName" runat="server" CssClass="textbox manfield" MaxLength="100"
                                    onkeyup="ChangeCase(this.id)" ToolTip="Enter Ledger Name" meta:resourcekey="txtNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                                    CssClass="requiredcolor" ErrorMessage="Ledger is required" SetFocusOnError="True"
                                    ControlToValidate="txtName" Display="Dynamic" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Lit" runat="server" Text="Budget Group" meta:resourcekey="LitResource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlBudgetGroup" runat="server" CssClass="combobox manfield"
                                    Width="255px" AutoPostBack="True" meta:resourcekey="ddlBudgetGroupResource1"
                                    OnTextChanged="ddlBudgetGroup_TextChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal15" runat="server" Text="Budget Sub Group" meta:resourcekey="Literal15Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlBudgetSubGroup" runat="server" CssClass="combobox manfield"
                                    Width="255px" AutoPostBack="True" ToolTip="Select Budget Sub Group" meta:resourcekey="ddlBudgetSubGroupResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-fluid" id="FDInvestments" runat="server">
                            <div class="span5 textright">
                                <asp:Literal ID="litFDType" runat="server" Text="FD Investment Type"></asp:Literal>
                            </div>
                            <div class="span7">
                                <div>
                                    <asp:DropDownList ID="ddlFDInvestmentType" runat="server" Width="255px" AutoPostBack="True"
                                        ToolTip="Select FD Investment" CssClass="combobox manfield">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%--<div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="literalGeneralateLedger" runat="server" Text="Under the Generalate Group" meta:resourcekey="literalGeneralateLedgerResource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlGeneralateGroup" runat="server" CssClass="combobox manfield"
                                    Width="255px" AutoPostBack="True" ToolTip="Select Generalate Group" meta:resourcekey="ddlGeneralateGroupResource1"
                                    OnTextChanged="ddlGroup_TextChanged">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Generalate Group is required"
                                    CssClass="requiredcolor" ControlToValidate="ddlGeneralateGroup" Operator="NotEqual" Type="Integer"
                                    ValueToCompare="0" meta:resourcekey="CompareValidator2Resource1" Text="*"></asp:CompareValidator>
                            </div>
                        </div>--%>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal5" runat="server" Text="Notes" meta:resourcekey="Literal5Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <div>
                                    <asp:TextBox ID="txtNotes" runat="server" CssClass="multiline textbox" TextMode="MultiLine"
                                        ToolTip="Enter Notes" onkeypress="return textboxMultilineMaxNumber(event,this,450);"
                                        meta:resourcekey="txtNotesResource1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                            </div>
                            <div class="span7">
                                <asp:CheckBox ID="chkIsTDSApplicable" runat="server" Text="Is TDS Applicable" ToolTip="Check Is TDS Applicable"
                                    Visible="true" AutoPostBack="True" OnCheckedChanged="chkIsTDSApplicable_CheckedChanged"
                                    meta:resourcekey="chkIsTDSApplicableResource1" />
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                            </div>
                            <div class="span7">
                                <asp:CheckBox ID="chkIncludeCostCenter" runat="server" Text="Include Cost Centre For this Ledger"
                                    ToolTip="Select Cost Centre enabled Ledgers" meta:resourcekey="chkIncludeCostCenterResource1" />
                            </div>
                        </div>
                        <div id="divIncomeNature" runat="server" visible="false">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkBankInterestLedger" runat="server" Text="Set as FD Interest Ledger"
                                        ToolTip="Select Fixed Deposit Interest Ledger" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkBankSBInterestLedger" runat="server" Text="Set as SB Interest Ledger"
                                        ToolTip="Select Saving Bank Account Interest Ledger" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkInKindLedger" runat="server" Text="Set as In-Kind Ledger" ToolTip="Set as In-Kind Ledger" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkAssetGainLedger" runat="server" Text="Set as Asset Gain Ledger"
                                        ToolTip="Set this as Asset Gain Ledger" />
                                </div>
                            </div>
                        </div>
                        <div id="divExpenseNature" runat="server" visible="false">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkFDPenaltyLedger" runat="server" Text="Set as FD Penalty Ledger"
                                        ToolTip="Select Fixed Deposit Penalty Ledger" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkBankCommissionLedger" runat="server" Text="Set as Bank Commission Ledger"
                                        ToolTip="Select Bank Commission Ledger" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkAssetLossLedger" runat="server" Text="Set as Asset Loss Ledger"
                                        ToolTip="Set as Asset Loss Ledger" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkDisposalLedger" runat="server" Text="Set as Asset Disposal Ledger"
                                        ToolTip="Set as Asset Disposal Ledger" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkDepreciationLedger" runat="server" Text="Set as Depreciation Ledger"
                                        ToolTip="Set as Depreciation Ledger" />
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                Closed On
                            </div>
                            <div class="span7">
                                <dx:ASPxDateEdit ID="dteClosedOn" runat="server" CssClass="combobox manfield" UseMaskBehavior="True"
                                    DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" Width="100px"
                                    meta:resourcekey="dteClosedOnResource1" EditFormat="Custom">
                                </dx:ASPxDateEdit>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlIsTDSApplicable" GroupingText="Statutory Information" runat="server"
                        Visible="false" meta:resourcekey="pnlIsTDSApplicableResource1">
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="ltrlNatureDeductee" runat="server" meta:resourcekey="ltrlNatureDeducteeResource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlNatureDeductee" runat="server" CssClass="combobox" Width="255px"
                                    OnSelectedIndexChanged="ddlNatureDeductee_OnSelectedIndexChanged" AutoPostBack="True"
                                    meta:resourcekey="ddlNatureDeducteeResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlLedgerProfile" GroupingText="Ledger Profile" runat="server" Visible="False"
                        meta:resourcekey="pnlLedgerProfileResource1">
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal6" runat="server" Text="Name" meta:resourcekey="Literal6Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtLedgerProfileName" CssClass="textbox" MaxLength="100" runat="server"
                                    ToolTip="Enter Name" Width="248px" meta:resourcekey="txtLedgerProfileNameResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal7" runat="server" Text="Address" meta:resourcekey="Literal7Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtProfileAddress" CssClass="multiline textbox" MaxLength="250"
                                    ToolTip="Enter Address" runat="server" TextMode="MultiLine" Width="248px" meta:resourcekey="txtProfileAddressResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal8" runat="server" Text="Email" meta:resourcekey="Literal8Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtProfileEmail" CssClass="textbox" MaxLength="100" runat="server"
                                    ToolTip="Enter Email" Width="248px" meta:resourcekey="txtProfileEmailResource1"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="reMail" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Invalid LedgerProfile Email Id" ControlToValidate="txtProfileEmail"
                                    Display="Dynamic" ValidationExpression="^((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*$"
                                    Text="*" meta:resourcekey="reMailResource1"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal9" runat="server" Text="Contact No" meta:resourcekey="Literal9Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtProfileContactNo" MaxLength="10" CssClass="textbox" runat="server"
                                    ToolTip="Enter Contact No" Width="248px" meta:resourcekey="txtProfileContactNoResource1"></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtProfileContactNo" ID="ProfileContactNo"
                                    Display="Dynamic" ValidationExpression="^[\s\S]{10,10}$" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Maximum 10 digits required." meta:resourcekey="ProfileContactNoResource2"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal11" runat="server" Text="Country" meta:resourcekey="Literal11Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlProfileCountry" runat="server" CssClass="combobox" Width="255px"
                                    ToolTip="Select Ledger Country" meta:resourcekey="ddlGroupResource1" AutoPostBack="True"
                                    OnTextChanged="ddlProfileCountry_TextChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal12" runat="server" Text="State" meta:resourcekey="Literal12Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlProfileState" runat="server" CssClass="combobox" Width="255px"
                                    ToolTip="Select Ledger State" meta:resourcekey="ddlGroupResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal13" runat="server" Text="PAN No" meta:resourcekey="Literal13Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtProfilePanNo" CssClass="textbox uppercase" MaxLength="10" runat="server"
                                    ToolTip="Enter PAN No" Width="248px" meta:resourcekey="txtProfilePanNoResource1"></asp:TextBox>
                                <div class="clearfix normitalic pad5 ">
                                    Ex:ABCDE1111F
                                </div>
                                <asp:MaskedEditExtender ID="mePAN" runat="server" Century="2000" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" Mask="LLLLL9999L" TargetControlID="txtProfilePanNo">
                                </asp:MaskedEditExtender>
                                <asp:MaskedEditValidator ID="mevPanNo" runat="server" ControlExtender="mePAN" ControlToValidate="txtProfilePanNo"
                                    Display="Dynamic" ErrorMessage="mevPanNo" meta:resourceKey="mevPanNoResource2"
                                    ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]"></asp:MaskedEditValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                <asp:Literal ID="Literal14" runat="server" Text="PIN Code" meta:resourcekey="Literal14Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtProfilePINCode" CssClass="textbox" MaxLength="10" runat="server"
                                    onkeypress="return numbersonly(event, true, this);" ToolTip="Enter PIN Code"
                                    Width="248px" meta:resourcekey="txtProfilePINCodeResource1"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row-fluid">
                    </div>
                </div>
            </div>
            <div style="width: 50%; margin-left: 30%;">
                <dx:ASPxGridView ID="gvProjectCategory" runat="server" SettingsBehavior-AllowDragDrop="false"
                    IncrementalFilteringMode="Contains" Settings-ShowFilterRow="true" Theme="Office2010Blue"
                    Width="100%" ClientInstanceName="gvProjectCategory" KeyFieldName="PROJECT_CATOGORY_ID"
                    AutoGenerateColumns="False">
                    <Columns>
                        <dx:GridViewCommandColumn Name="ColSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                            Width="25px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="colSelectAll" runat="server" ClientInstanceName="colSelectAll"
                                    Theme="Office2010Blue" ToolTip="Select/Unselect all rows on the page">
                                    <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                                </dx:ASPxCheckBox>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn Name="colProjectCategory" Caption="Project Category" FieldName="PROJECT_CATOGORY_NAME"
                            VisibleIndex="1">
                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Name="colProjectCategoryId" FieldName="PROJECT_CATOGORY_ID"
                            Visible="false">
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsPager Position="TopAndBottom" PageSize="300">
                        <PageSizeItemSettings Items="300,400,500" Visible="True">
                        </PageSizeItemSettings>
                    </SettingsPager>
                    <SettingsBehavior AllowSort="true" />
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="true">
                    </Settings>
                </dx:ASPxGridView>
            </div>
            <div class="textcenter" style="margin-top: 10px;" align="center">
                <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" runat="server" CssClass="button"
                    Text="Save" ToolTip="Click here to save" meta:resourcekey="btnSaveUserResource1">
                </asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for Ledger" CausesValidation="False" meta:resourcekey="btnNewResource1">
                </asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="LedgerVs" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="LedgerVsResource1"></asp:ValidationSummary>
</asp:Content>
