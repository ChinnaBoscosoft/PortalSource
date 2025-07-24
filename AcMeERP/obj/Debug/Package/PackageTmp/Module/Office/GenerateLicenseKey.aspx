<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="GenerateLicenseKey.aspx.cs" Inherits="AcMeERP.Module.Office.GenerateLicenseKey"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upLicenseKey" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span10 offset1">
                    <asp:Panel ID="pnlHeadOfficeBasicInfo" GroupingText="License Info" runat="server"
                        meta:resourcekey="pnlHeadOfficeBasicInfoResource1">
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal4" runat="server" Text="Head Office Code *" meta:resourcekey="Literal4Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtHeadOfficeCode" runat="server" CssClass="textbox manfield" MaxLength="6"
                                    Enabled="False" ToolTip="Head Office Code" meta:resourcekey="txtHeadOfficeCodeResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal18" runat="server" Text="Branch Office Code *" meta:resourcekey="Literal18Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:TextBox ID="txtBOfficeCode" runat="server" CssClass="textbox manfield" MaxLength="12"
                                    Enabled="False" ToolTip="Branch Office Code" meta:resourcekey="txtBOfficeCodeResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal1" runat="server" Text="Branch Office Name *" meta:resourcekey="Literal1Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtBOName" runat="server" CssClass="textbox manfield" MaxLength="50"
                                    Enabled="False" ToolTip="BranchOffice Name" meta:resourcekey="txtBONameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvBName" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Branch Office Name is required" SetFocusOnError="True" ControlToValidate="txtBOName"
                                    Enabled="False" Display="Dynamic" meta:resourcekey="rfvBNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal10" runat="server" Text="Institute Name *" meta:resourcekey="Literal10Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtInstituteName" runat="server" CssClass="textbox manfield" ToolTip="Enter Institute Name"
                                    MaxLength="150" meta:resourcekey="txtInstituteNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfNm" runat="server" Text="*" ErrorMessage="Institute Name is required"
                                    CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtInstituteName"
                                    Display="Dynamic" meta:resourcekey="rfNmResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal2" runat="server" Text="Deployment Model " meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:RadioButtonList ID="radbtnlstDeployment" runat="server" RepeatDirection="Horizontal"
                                    Enabled="False" ToolTip="Select deployment model" meta:resourcekey="radbtnlstDeploymentResource1">
                                    <asp:ListItem Selected="True" Text="Standalone" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Text="Client-Server" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal11" runat="server" Text="Number of Nodes * " meta:resourcekey="Literal11Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtNoofCode" runat="server" CssClass="textbox manfield" ToolTip="Enter No of Nodes"
                                    MaxLength="5" meta:resourcekey="txtNoofCodeResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfNoOfNode" runat="server" Text="*" ErrorMessage="No of Node is required"
                                    CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtNoofCode"
                                    Display="Dynamic" meta:resourcekey="rvfNoOfNodeResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="ltrLicenseCost" runat="server" Text="License Cost * " meta:resourcekey="ltrLicenseCostResource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtLicenseCost" runat="server" CssClass="textbox manfield" ToolTip="Enter License Cost"
                                    MaxLength="5" meta:resourcekey="txtLicenseCostResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfLicenseCost" runat="server" Text="*" ErrorMessage="License Cost is required"
                                    CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtLicenseCost"
                                    Display="Dynamic" meta:resourcekey="rvfLicenseCostResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                            </div>
                            <div class="span7">
                                <asp:CheckBox ID="chkLicenseModel" runat="server" Text="License Model" meta:resourcekey="chkLicenseModelResource1"
                                    Checked="true" />
                            </div>
                        </div>
                        <div class="row-fluid" id="divPortal" runat="server">
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkPortal" runat="server" Text="Enable Portal" meta:resourcekey="chkPortalResource1"
                                        Checked="true" />
                                    <asp:CheckBox ID="chkMultilocation" runat="server" Text="Multilocation" meta:resourcekey="chkMultilocationResource1" />
                                    <asp:CheckBox ID="chkAutomaticBackup" runat="server" Text="Automatic Backup" meta:resourcekey="chkAutomaticBackupResource1" />
                                </div>
                                <div class="row-fluid">
                                    <div class="span5 textright padright25">
                                    </div>
                                    <div class="span7">
                                        <asp:CheckBox ID="chkLockMaster" runat="server" Text="Lock Master" meta:resourcekey="chkLockMasterResource1" />
                                        <asp:CheckBox ID="chkMapLedger" runat="server" Text="Map Ledger" meta:resourcekey="chkMapLedgerResource1"
                                            Checked="true" />
                                        <asp:CheckBox ID="chkAllowMultiCurrency" runat="server" Text="Multi Currency" meta:resourcekey="chkAllowMultiCurrencyResource1"
                                            Checked="false" />
                                        <asp:CheckBox ID="chkAttachVouchersFiles" runat="server" Text="Attach Vouchers Files" meta:resourcekey="chkAttachVouchersFilesResource1"
                                            Checked="false" />
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span5 textright padright25">
                                    </div>
                                    <div class="span7">
                                        <asp:CheckBox ID="chkBudgetApprovalbyPortal" runat="server" Text="Approve Budget by Online"
                                            Checked="false" />
                                        <asp:CheckBox ID="chkBudgetApprovalbyExcel" runat="server" Text="Approve Budget by Excel"
                                            Checked="false" />
                                        <asp:CheckBox ID="chkBudgetTwoMonth" runat="server" Text=" Two Month Budget" Checked="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="ltrTransaction" runat="server" Text=" Transaction Period *" meta:resourcekey="ltrTransactionResource1"></asp:Literal>
                            </div>
                            <div class="span2">
                                <asp:Literal ID="ltrYearFrom" runat="server" Text="Year From" meta:resourcekey="ltrYearFromResource1"></asp:Literal>
                                <dx:ASPxDateEdit ID="dtYearFrom" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                    EditFormatString="dd/MM/yyyy" CssClass="combobox manfield" Width="100px" meta:resourcekey="dtYearFromResource1"
                                    EditFormat="Custom">
                                </dx:ASPxDateEdit>
                            </div>
                            <div class="span2">
                                <asp:Literal ID="ltrYearTo" runat="server" Text="Year To" meta:resourcekey="ltrYearToResource1"></asp:Literal>
                                <dx:ASPxDateEdit ID="dtYearTo" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                    EditFormatString="dd/MM/yyyy" CssClass="combobox manfield" Width="100px" meta:resourcekey="dtYearToResource1"
                                    EditFormat="Custom">
                                </dx:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="ltrModule" runat="server" Text="Module" meta:resourcekey="ltrModuleResource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:CheckBoxList ID="chkModuleItems" runat="server" meta:resourcekey="chkModuleItemsResource1"
                                    AutoPostBack="True" OnSelectedIndexChanged="chkModuleItems_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="ltrLicenseReports" runat="server" Text="License Report" meta:resourcekey="ltrLicenseReportsResource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:CheckBoxList ID="chkLicenseReport" runat="server" meta:resourcekey="chkLicenseReportResource1"
                                    AutoPostBack="True">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="row-fluid" id="divParish" runat="server">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal9" runat="server" Text="Parish *"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlParish" ToolTip="Select Parish" runat="server" CssClass="combobox manfield"
                                    Width="255px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmvState" runat="server" ErrorMessage="Parish is required"
                                    CssClass="requiredcolor" ControlToValidate="ddlParish" Operator="NotEqual" Type="Integer"
                                    ValueToCompare="0" Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal13" runat="server" Text="Login URL" meta:resourcekey="Literal13Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtURL" runat="server" CssClass="textbox" ToolTip="Head Office Login url"
                                    MaxLength="30" meta:resourcekey="txtURLResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal40" runat="server" Text="Third Party Code *" meta:resourcekey="Literal40Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:TextBox ID="txtThirdParty" runat="server" CssClass="textbox manfield" MaxLength="12"
                                    Enabled="False" ToolTip="Third Party Code" meta:resourcekey="txtThirdPartyResource1"></asp:TextBox>
                                &nbsp;</div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal3" runat="server" Text="Third Party Mode" meta:resourcekey="Literal40Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:TextBox ID="txtthirdpartymode" runat="server" CssClass="textbox manfield" MaxLength="12"
                                    Enabled="true" ToolTip="Third Party Mode" meta:resourcekey="txtthirdpartymodeResource1"></asp:TextBox>
                                &nbsp;</div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal6" runat="server" Text="Third Party URL" meta:resourcekey="Literal40Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:TextBox ID="txtthirdpartyurl" runat="server" CssClass="textbox manfield" MaxLength="50"
                                    Enabled="true" ToolTip="Third Party URL" meta:resourcekey="txtthirdpartyurlResource1"></asp:TextBox>
                                &nbsp;</div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                            </div>
                            <div class="span7">
                                <asp:CheckBox ID="chkAccessToMultiDB" runat="server" Text="Access To Multi DB" meta:resourcekey="chkAccessToMultiDBResource1" />
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="clearfix marginbottom">
                    </div>
                    <div class="textcenter" align="center">
                        <asp:Button ID="btnSaveLicense" runat="server" CssClass="button" Text="Save" OnClick="btnSaveLicense_Click"
                            ToolTip="Click here to save License Details" meta:resourcekey="btnSaveLicenseResource1">
                        </asp:Button>
                        <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                            CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsLicenseSummary" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="vsLicenseSummaryResource1"></asp:ValidationSummary>
</asp:Content>
