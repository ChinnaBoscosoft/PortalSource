<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="LegalEntityAdd.aspx.cs" Inherits="AcMeERP.Module.Master.LegalEntityAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upLegalEntity" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span10 offset1">
                    <asp:Panel ID="pnlSociety" runat="server" GroupingText="Society Info" meta:resourcekey="pnlSocietyResource1">
                        <div class="row-fluid">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal1" runat="server" Text="Society Name *" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtSocietyName" runat="server" CssClass="textbox manfield" ToolTip="Enter Society Name"
                                        MaxLength="100" meta:resourcekey="txtSocietyNameResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal2" runat="server" Text="Contact Person" meta:resourcekey="Literal2Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="textbox" ToolTip="Enter Contact Person"
                                        MaxLength="30" meta:resourcekey="txtContactPersonResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal3" runat="server" Text="Address " meta:resourcekey="Literal3Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox multiline" onkeypress="return textboxMultilineMaxNumber(event,this,500);"
                                        ToolTip="Enter Address" TextMode="MultiLine" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal5" runat="server" Text="Place" meta:resourcekey="Literal5Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtPlace" runat="server" CssClass="textbox" MaxLength="30" ToolTip="Enter Place"
                                        meta:resourcekey="txtPlaceResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal9" runat="server" Text="Phone" meta:resourcekey="Literal9Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtPhone" CssClass="textbox" runat="server" MaxLength="15" ToolTip="Enter Phone No"
                                        onkeypress="return numbersonly(event, false, this);" meta:resourcekey="txtPhoneResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal7" runat="server" Text="Country *" meta:resourcekey="Literal7Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:DropDownList ID="ddlCountry" ToolTip="Select Country" runat="server" CssClass="combobox manfield"
                                        Width="255px" meta:resourcekey="ddlCountryResource1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal6" runat="server" Text="State/Province *" meta:resourcekey="Literal6Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtState" runat="server" CssClass="textbox manfield" MaxLength="25" ToolTip="Enter State"
                                        meta:resourcekey="txtStateResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal8" runat="server" Text="Pincode" meta:resourcekey="Literal8Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtPinCode" runat="server" MaxLength="10" CssClass="textbox" ToolTip="Enter Pincode"
                                        onkeypress="return numbersonly(event, false, this);" meta:resourcekey="txtPinCodeResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal10" runat="server" Text="Fax" meta:resourcekey="Literal10Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtFax" runat="server" MaxLength="20" CssClass="textbox" ToolTip="Enter Fax"
                                        meta:resourcekey="txtFaxResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal11" runat="server" Text="Email" meta:resourcekey="Literal11Resource2"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="textbox" ToolTip="Enter E-Mail Address"
                                        meta:resourcekey="txtEmailResource1" />
                                    <asp:RegularExpressionValidator ID="reMail" runat="server" ErrorMessage="Invalid Email"
                                        CssClass="requiredcolor" ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        meta:resourcekey="reMailResource1" Text="Invalid Email"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal12" runat="server" Text="URL" meta:resourcekey="Literal12Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtUrl" runat="server" MaxLength="100" CssClass="textbox" ToolTip="Enter URL"
                                        Text="http://" meta:resourcekey="txtUrlResource1" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnAssociationDetails" runat="server" GroupingText="Association Details"
                        meta:resourcekey="pnAssociationDetailsResource2">
                        <div class="row-fluid">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal13" runat="server" Text="Society/Reg.No *" meta:resourcekey="Literal13Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtRegNo" runat="server" MaxLength="50" CssClass="textbox manfield"
                                        ToolTip="Enter Register No" meta:resourcekey="txtRegNoResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal14" runat="server" Text="Reg.Date" meta:resourcekey="Literal14Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <dx:ASPxDateEdit ID="dteRegisterDate" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                        EditFormatString="dd/MM/yyyy" CssClass="combobox manfield" Width="100px" meta:resourcekey="dteRegisterDateResource1"
                                        EditFormat="Custom">
                                    </dx:ASPxDateEdit>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal15" runat="server" Text="Prior Permission No" meta:resourcekey="Literal15Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtPermissionNo" runat="server" MaxLength="50" CssClass="textbox"
                                        ToolTip="Enter Permission No" meta:resourcekey="txtPermissionNoResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal16" runat="server" Text="Prior Permission Date" meta:resourcekey="Literal16Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <dx:ASPxDateEdit ID="dtePermissionDate" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                        EditFormatString="dd/MM/yyyy" CssClass="combobox manfield" Width="100px" meta:resourcekey="dtePermissionDateResource1"
                                        EditFormat="Custom">
                                    </dx:ASPxDateEdit>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal17" runat="server" Text="12A No" meta:resourcekey="Literal17Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtAoneTwoNo" runat="server" MaxLength="50" CssClass="textbox" ToolTip="Enter A12 No"
                                        meta:resourcekey="txtAoneTwoNoResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal18" runat="server" Text="PAN No" meta:resourcekey="Literal18Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtPanNo" runat="server" MaxLength="10" CssClass="textbox uppercase" ToolTip="Enter PAN No"
                                        meta:resourcekey="txtPanNoResource1"></asp:TextBox>
                                    <div class="clearfix normitalic pad5 ">
                                        Ex:ABCDE1111F
                                    </div>
                                    <ajax:MaskedEditExtender ID="mePAN" runat="server" Mask="LLLLL9999L" TargetControlID="txtPanNo"
                                        Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True">
                                    </ajax:MaskedEditExtender>
                                    <ajax:MaskedEditValidator ID="mevPanNo" ControlExtender="mePAN" runat="server" ControlToValidate="txtPanNo"
                                        Display="Dynamic" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]" ErrorMessage="mevPanNo"
                                        meta:resourcekey="mevPanNoResource1"></ajax:MaskedEditValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal19" runat="server" Text="GIR No" meta:resourcekey="Literal19Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtGIRNo" runat="server" MaxLength="50" CssClass="textbox" ToolTip="Enter GIR No"
                                        meta:resourcekey="txtGIRNoResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal20" runat="server" Text="TAN No" meta:resourcekey="Literal20Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtTanNo" runat="server" MaxLength="10" CssClass="textbox uppercase" ToolTip="Enter TAN No"
                                        meta:resourcekey="txtTanNoResource1" />
                                    <div class="clearfix normitalic pad5 ">
                                        Ex:ABCD12345E
                                    </div>
                                    <ajax:MaskedEditExtender ID="meTanNo" runat="server" Mask="LLLL99999L" TargetControlID="txtTanNo"
                                        Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True">
                                    </ajax:MaskedEditExtender>
                                    <ajax:MaskedEditValidator ID="mevTanNo" ControlExtender="meTanNo" runat="server"
                                        ControlToValidate="txtTanNo" Display="Dynamic" ValidationExpression="[A-Za-z]{4}\d{5}[A-Za-z]"
                                        ErrorMessage="mevTanNo" meta:resourcekey="mevTanNoResource1"></ajax:MaskedEditValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="ltrFCRINo" runat="server" Text="FCRA No" meta:resourcekey="ltrFCRINoResource2"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtFCRINo" runat="server" MaxLength="30" CssClass="textbox" meta:resourcekey="txtFCRINoResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="ltrFCRIRegDate" runat="server" Text="FCRA Reg.Date" meta:resourcekey="ltrFCRIRegDateResource3"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <dx:ASPxDateEdit ID="dteFCRIRegDate" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                        EditFormatString="dd/MM/yyyy" CssClass="combobox manfield" Width="100px" EditFormat="Custom"
                                        meta:resourcekey="dteFCRIRegDateResource1">
                                    </dx:ASPxDateEdit>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="ltr80GNo" runat="server" Text="80G.No" meta:resourcekey="ltr80GNoResource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txt80GNo" runat="server" MaxLength="30" CssClass="textbox" meta:resourcekey="txt80GNoResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal23" runat="server" Text="80G.No.Reg.Date"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <dx:ASPxDateEdit ID="dte80GRegDate" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                        EditFormatString="dd/MM/yyyy" CssClass="combobox manfield" Width="100px" EditFormat="Custom">
                                    </dx:ASPxDateEdit>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal4" runat="server" Text="Is Foundation?" 
                                        meta:resourcekey="Literal4Resource2"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:CheckBox ID="chkFoundation" runat="server" meta:resourcekey="chkFoundationResource1" />
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal22" runat="server" Text="Principal Activity" 
                                        meta:resourcekey="Literal22Resource4"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtPrincipalActivity" runat="server" MaxLength="95" 
                                        CssClass="textbox" meta:resourcekey="txtPrincipalActivityResource1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal21" runat="server" Text="Association Nature *" meta:resourcekey="Literal21Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:CheckBoxList ID="rdAssociationNature" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rdAssociationNature_SelectedIndexChanged" AutoPostBack="True"
                                        CssClass="manfield" ToolTip="Select Association No" meta:resourcekey="rdAssociationNatureResource2">
                                        <asp:ListItem Value="0" Text="Cultural" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Economic" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Educational" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Religious" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Social" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Others" meta:resourcekey="ListItemResource12"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div id="divAssociationNature" class="row-fluid" style="display: none;" runat="server">
                                <div class="span5 ">
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtAssociationNatureOthers" runat="server" CssClass="textbox manfield"
                                        MaxLength="100" meta:resourcekey="txtAssociationNatureOthersResource1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="ltrDenomination" runat="server" Text="Denomination *" Visible="False"
                                        meta:resourcekey="ltrDenominationResource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:RadioButtonList ID="rdDenomination" runat="server" RepeatDirection="Horizontal"
                                        Visible="False" OnSelectedIndexChanged="rdDenomination_SelectedIndexChanged"
                                        AutoPostBack="True" CssClass="manfield" ToolTip="Select Denomination" meta:resourcekey="rdDenominationResource1">
                                        <asp:ListItem Value="1" Text="Hindu" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Sikh" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Muslim" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Christian" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Buddhist" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Others" meta:resourcekey="ListItemResource11"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div id="divDenomination" class="row-fluid" style="display: none;" runat="server">
                                <div class="span5 ">
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtDenominationOthers" runat="server" CssClass="textbox manfield"
                                        MaxLength="100" meta:resourcekey="txtDenominationOthersResource1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnSaveUser" OnClick="btnSave_Click" runat="server" CssClass="button"
                    Text="Save" ToolTip="Click here to save " meta:resourcekey="btnSaveUserResource1">
                </asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new Legal Entity" CausesValidation="False" meta:resourcekey="btnNewResource1">
                </asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
