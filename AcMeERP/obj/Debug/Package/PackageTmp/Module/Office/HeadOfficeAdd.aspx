<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="HeadOfficeAdd.aspx.cs" Inherits="AcMeERP.Module.Office.HeadOfficeAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upHeadOfficeCreate" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span10 offset1">
                    <asp:Panel ID="pnlHeadOfficeBasicInfo" GroupingText="Office Info" runat="server"
                        meta:resourcekey="pnlHeadOfficeBasicInfoResource1">
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal4" runat="server" Text="Code/Username *" meta:resourcekey="Literal4Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:TextBox ID="txtHOfficeCode" runat="server" CssClass="textbox manfield" MaxLength="6"
                                    AutoPostBack="True" ToolTip="Enter Head Office Code" OnTextChanged="txtHOOffice_TextChanged"
                                    meta:resourcekey="txtHOfficeCodeResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfHeadOfficeCode" runat="server" CssClass="requiredcolor"
                                    Text="*" ErrorMessage="Head Office Code is required" SetFocusOnError="True" ControlToValidate="txtHOfficeCode"
                                    Display="Dynamic" meta:resourcekey="rfHeadOfficeCodeResource1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtHOfficeCode" ID="rfvHeadOfficeCode"
                                    Display="Dynamic" ValidationExpression="^[\s\S]{6,6}$" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Code or Username Maximum 6 characters required." meta:resourcekey="rfvHeadOfficeCodeResource1"></asp:RegularExpressionValidator>
                                <div id="checkOfficeCode" runat="server" visible="False" style="float: right;">
                                    <asp:Image ID="imgstatus" runat="server" Width="17px" Height="17px" ImageUrl="~/App_Themes/MainTheme/images/Icon_Available.png"
                                        meta:resourcekey="imgstatusResource1" />
                                    <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatusResource1"></asp:Label>
                                </div>
                                <div class="clearfix normitalic pad5">
                                    Ex:sdbinm
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal1" runat="server" Text="Name *" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtHOName" runat="server" CssClass="textbox manfield" MaxLength="100"
                                        ToolTip="Enter Head Office Name" meta:resourcekey="txtHONameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvHName" runat="server" CssClass="requiredcolor"
                                        Text="*" ErrorMessage="Head Office Name is required" SetFocusOnError="True" ControlToValidate="txtHOName"
                                        Display="Dynamic" meta:resourcekey="rfvHNameResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal2" runat="server" Text="Type *" meta:resourcekey="Literal2Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:DropDownList ID="ddlType" ToolTip="Select Head Office Type" runat="server" CssClass="combobox manfield"
                                        Width="255px" meta:resourcekey="ddlTypeResource1">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="cmpRl" runat="server" CssClass="requiredcolor" ErrorMessage="Head Office type is required"
                                        ControlToValidate="ddlType" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                        meta:resourcekey="cmpRlResource1" Text="*"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="ltAccountingPeriodType" runat="server" Text="Accounting Period Type "></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:DropDownList ID="ddlAccountingPeriodType" ToolTip="Select Accounting peroid Type" runat="server"
                                        CssClass="combobox manfield" Width="255px">
                                        <asp:ListItem Text="Financial Year" Value="0"></asp:ListItem>                                   
                                        <asp:ListItem Text="Calendar Year" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal3" runat="server" Text="Belongs to *" meta:resourcekey="Literal3Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtBelongsto" runat="server" CssClass="textbox manfield" MaxLength="100"
                                        ToolTip="Enter the congregation name of Head office" meta:resourcekey="txtBelongstoResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtBelongsto" runat="server" Text="*" CssClass="requiredcolor"
                                        ErrorMessage="Belongs to is required" SetFocusOnError="True" ControlToValidate="txtBelongsto"
                                        Display="Dynamic" meta:resourcekey="rfvtxtBelongstoResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal7" runat="server" Text="Person Incharge *" meta:resourcekey="Literal7Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtInchargeName" runat="server" CssClass="textbox manfield" MaxLength="50"
                                        ToolTip="Enter Incharge Name" meta:resourcekey="txtInchargeNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvftxtInchargeName" runat="server" Text="*" CssClass="requiredcolor"
                                        ErrorMessage="Incharge Name is required" SetFocusOnError="True" ControlToValidate="txtInchargeName"
                                        Display="Dynamic" meta:resourcekey="rvftxtInchargeNameResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal5" runat="server" Text="Role/Position *" meta:resourcekey="Literal5Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox manfield" MaxLength="100"
                                        ToolTip="Enter Designation" meta:resourcekey="txtDesignationResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfDesignation" runat="server" Text="*" CssClass="requiredcolor"
                                        ErrorMessage="Role/Position is required" SetFocusOnError="True" ControlToValidate="txtDesignation"
                                        Display="Dynamic" meta:resourcekey="rvfDesignationResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal20" runat="server" Text="Mobile No *" meta:resourcekey="Literal20Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox" MaxLength="3"
                                        Text="91" onkeypress="return numbersonly(event, true, this);" ToolTip="Enter Country Code"
                                        Width="25px" meta:resourcekey="txtCountryCodeResource1"></asp:TextBox>
                                    ~
                                    <asp:TextBox ID="txtOrgMobileNo" runat="server" CssClass="textbox manfield" MaxLength="10"
                                        onkeypress="return numbersonly(event, true, this);" Width="200px" ToolTip="Enter organization mobile no"
                                        meta:resourcekey="txtOrgMobileNoResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPRmobileno" runat="server" Text="*" CssClass="requiredcolor"
                                        ErrorMessage="Organization mobile no is required" SetFocusOnError="True" ControlToValidate="txtOrgMobileNo"
                                        Display="Dynamic" meta:resourcekey="rfvPRmobilenoResource1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="txtOrgMobileNo" ID="rvftxtOrgMobileNo"
                                        Display="Dynamic" ValidationExpression="^[\s\S]{10,10}$" runat="server" CssClass="requiredcolor"
                                        ErrorMessage="Maximum 10 digits required." meta:resourcekey="rvftxtOrgMobileNoResource1"></asp:RegularExpressionValidator>
                                    <div class="clearfix normitalic pad5 ">
                                        Ex:91 9962663391 - number followed by code
                                    </div>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal21" runat="server" Text="Phone No *" meta:resourcekey="Literal21Resource1"></asp:Literal>
                                </div>
                                <div class="span7" style="width: 46.265%">
                                    <asp:TextBox ID="txtOrgPhoneNo" runat="server" CssClass="textbox manfield" MaxLength="12"
                                        ToolTip="Enter organization phone no" onkeypress="return numbersonly(event, true, this);"
                                        meta:resourcekey="txtOrgPhoneNoResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtPRPhoneNo" runat="server" CssClass="requiredcolor"
                                        Text="*" ErrorMessage="Organization phone no is required" SetFocusOnError="True"
                                        ControlToValidate="txtOrgPhoneNo" Display="Dynamic" meta:resourcekey="rfvtxtPRPhoneNoResource1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ControlToValidate="txtOrgPhoneNo" ID="RegularExpressionValidator2"
                                        Display="Dynamic" ValidationExpression="^[\s\S]{10,12}$" runat="server" CssClass="requiredcolor"
                                        ErrorMessage="Maximum 10 or 12 digits required." meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                    <div class="clearfix normitalic pad5 ">
                                        Ex:03483253227 [(03483) 253227]
                                    </div>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal6" runat="server" Text="E-Mail *" meta:resourcekey="Literal6Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtOrgMailId" runat="server" CssClass="textbox manfield" MaxLength="240"
                                        ToolTip="Enter Organization Mail Id" meta:resourcekey="txtOrgMailIdResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfAuthorityMailId" runat="server" CssClass="requiredcolor"
                                        Text="*" ErrorMessage="Organization Mail Id is required" SetFocusOnError="True"
                                        ControlToValidate="txtOrgMailId" Display="Dynamic" meta:resourcekey="rvfAuthorityMailIdResource1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="reMail" runat="server" CssClass="requiredcolor"
                                        ErrorMessage="Invalid Organization Email Id" ControlToValidate="txtOrgMailId"
                                        Display="Dynamic" ValidationExpression="^((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*$"
                                        meta:resourcekey="reMailResource1" Text="*"></asp:RegularExpressionValidator>
                                    <div class="clearfix normitalic pad5 ">
                                        Ex:xxx@yyy.com,yyy@xxx.com,test@gmail.com
                                    </div>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal12" runat="server" Text="Address" meta:resourcekey="Literal12Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox multiline" onkeypress="return textboxMultilineMaxNumber(event,this,150);"
                                        ToolTip="Enter Address" TextMode="MultiLine" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal17" runat="server" Text="City" meta:resourcekey="Literal17Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="100" ToolTip="Enter City"
                                        meta:resourcekey="txtCityResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal16" runat="server" Text="Country *" meta:resourcekey="Literal16Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:DropDownList ID="ddlCountry" ToolTip="Select Country" runat="server" AutoPostBack="True"
                                        CssClass="combobox manfield" Width="255px" OnSelectedIndexChanged="ddlCountrySelectedIndexChanged"
                                        meta:resourcekey="ddlCountryResource1">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="cmvCountry" runat="server" ErrorMessage="Country is required"
                                        CssClass="requiredcolor" ControlToValidate="ddlCountry" Operator="NotEqual" Type="Integer"
                                        ValueToCompare="0" meta:resourcekey="cmvCountryResource1" Text="*"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal9" runat="server" Text="State/Province *" meta:resourcekey="Literal9Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:DropDownList ID="ddlState" ToolTip="Select State" runat="server" CssClass="combobox manfield"
                                        Width="255px" meta:resourcekey="ddlStateResource1">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="cmvState" runat="server" ErrorMessage="State is required"
                                        CssClass="requiredcolor" ControlToValidate="ddlState" Operator="NotEqual" Type="Integer"
                                        ValueToCompare="0" meta:resourcekey="cmvStateResource1" Text="*"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright padright25">
                                    <asp:Literal ID="Literal15" runat="server" Text="Postal/Zip Code *" meta:resourcekey="Literal15Resource1"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtPincode" runat="server" CssClass="textbox manfield" MaxLength="6"
                                        ToolTip="Enter Postal/Zip Code" onkeypress="return numbersonly(event, true, this);"
                                        meta:resourcekey="txtPincodeResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPincode" runat="server" CssClass="requiredcolor"
                                        Text="*" ErrorMessage="Postal/Zip Code is required" SetFocusOnError="True" ControlToValidate="txtPincode"
                                        Display="Dynamic" meta:resourcekey="rfvPincodeResource1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSecContact" GroupingText="Secondary Contact" runat="server" meta:resourcekey="pnlSecContactResource1">
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal22" runat="server" Text="Name" meta:resourcekey="Literal22Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtSRName" runat="server" CssClass="textbox" MaxLength="20" ToolTip="Enter Name of the secondary concat person"
                                    meta:resourcekey="txtSRNameResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal23" runat="server" Text="Mobile No" meta:resourcekey="Literal23Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:TextBox ID="txtMobileCountryCode" runat="server" CssClass="textbox" MaxLength="3"
                                    Text="91" onkeypress="return numbersonly(event, true, this);" ToolTip="Enter Country Code"
                                    Width="25px" meta:resourcekey="txtMobileCountryCodeResource1"></asp:TextBox>
                                ~
                                <asp:TextBox ID="txtSRMobileNo" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="200px" onkeypress="return numbersonly(event, true, this);" ToolTip="Enter secondary mobile no"
                                    meta:resourcekey="txtSRMobileNoResource1"></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtSRMobileNo" ID="rvftxtSRMobileNo"
                                    Display="Dynamic" ValidationExpression="^[\s\S]{10,10}$" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Maximum 10 digits required." meta:resourcekey="rvftxtSRMobileNoResource1"></asp:RegularExpressionValidator>
                                <div class="clearfix normitalic pad5 ">
                                    Ex:91 9962663391 - number followed by code
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal24" runat="server" Text="Phone No" meta:resourcekey="Literal24Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:TextBox ID="txtSRPhoneNo" runat="server" CssClass="textbox" MaxLength="12" ToolTip="Enter secondary mobile no"
                                    onkeypress="return numbersonly(event, true, this);" meta:resourcekey="txtSRPhoneNoResource1"></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtSRPhoneNo" ID="RegularExpressionValidator1"
                                    Display="Dynamic" ValidationExpression="^[\s\S]{10,12}$" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Maximum 10 or 12 digits required." meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                <div class="clearfix normitalic pad5 ">
                                    Ex:03483253227 [(03483) 253227]
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal10" runat="server" Text="E-Mail" meta:resourcekey="Literal10Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtSecMailId" runat="server" CssClass="textbox manfield" MaxLength="240"
                                    ToolTip="Enter Secondary Mail Id" meta:resourcekey="txtSecMailIdResource1"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="reSecMail" runat="server" ErrorMessage="Invalid Email Id"
                                    CssClass="requiredcolor" ControlToValidate="txtSecMailId" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    meta:resourcekey="reSecMailResource1" Text="*"></asp:RegularExpressionValidator>
                                <div class="clearfix normitalic pad5 ">
                                    Ex:xxx@yyy.com
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlDatabase" GroupingText="Database Info" runat="server" meta:resourcekey="pnlDatabaseResource1">
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal13" runat="server" Text="Host Name *" meta:resourcekey="Literal13Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtHostName" runat="server" CssClass="textbox" MaxLength="50" ToolTip="Enter Host Name"
                                    meta:resourcekey="txtHostNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfHostName" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Host Name is required" SetFocusOnError="True" ControlToValidate="txtHostName"
                                    Display="Dynamic" meta:resourcekey="rvfHostNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal14" runat="server" Text="Database Name *" meta:resourcekey="Literal14Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtDBName" runat="server" MaxLength="50" CssClass="textbox" ReadOnly="True"
                                    ToolTip="Enter Database Name" meta:resourcekey="txtDBNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfDBName" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Database Name is required" SetFocusOnError="True" ControlToValidate="txtDBName"
                                    Display="Dynamic" meta:resourcekey="rvfDBNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal18" runat="server" Text="Username *" meta:resourcekey="Literal18Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtDBUserName" runat="server" CssClass="textbox" MaxLength="100"
                                    ToolTip="Enter Username" meta:resourcekey="txtDBUserNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfDBUserName" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Username is required" SetFocusOnError="True" ControlToValidate="txtDBUserName"
                                    Display="Dynamic" meta:resourcekey="rvfDBUserNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal19" runat="server" Text="Password *" meta:resourcekey="Literal19Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtDBPassword" runat="server" CssClass="textbox" TextMode="Password"
                                    MaxLength="50" ToolTip="Enter Password" meta:resourcekey="txtDBPasswordResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfDBPassword" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Password is required" SetFocusOnError="True" ControlToValidate="txtDBPassword"
                                    Display="Dynamic" meta:resourcekey="rvfDBPasswordResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="textcenter Note clear">
                    <span class="red"><strong>Note: </strong></span>All Communication Mail will be sent
                    to Head Office Mail Id
                </div>
            </div>
            <div class="clearfix marginbottom">
            </div>
            <div class="textcenter">
                <asp:Button ID="btnSaveHeadOffice" OnClick="btnSaveHeadOffice_Click" runat="server"
                    CssClass="button" Text="Save" ToolTip="Click here to save head office Details"
                    meta:resourcekey="btnSaveHeadOfficeResource1"></asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new head office" CausesValidation="False" meta:resourcekey="btnNewResource1">
                </asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsHeadOffice" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="vsHeadOfficeResource1"></asp:ValidationSummary>
</asp:Content>
