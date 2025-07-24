<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="BranchOfficeAdd.aspx.cs" Inherits="AcMeERP.Module.Office.BranchOfficeAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upBranchOfficeCreate" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span10 offset1">
                    <asp:Panel ID="pnlHeadOfficeBasicInfo" GroupingText="Office Info" runat="server"
                        meta:resourcekey="pnlHeadOfficeBasicInfoResource1">
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal4" runat="server" Text="Head Office *" meta:resourcekey="Literal4Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlHeadOffice" ToolTip="Select Head Office" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlHeadOffice_SelectedIndexChanged"
                                    CssClass="combobox manfield open" Width="255px" meta:resourcekey="ddlHeadOfficeResource1">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmpRl" runat="server" ErrorMessage="Head Office is required"
                                    CssClass="requiredcolor" ControlToValidate="ddlHeadOffice" Operator="NotEqual"
                                    ValueToCompare="-Select-" meta:resourcekey="cmpRlResource1" Text="*"></asp:CompareValidator>
                                <div id="divpop" class="collapse" style="display: none">
                                    <div class='box'>
                                        <div title="Close" class="close" onclick="javascript:showpopupbox(false)">
                                            X</div>
                                        <div class='arrow'>
                                        </div>
                                        <div class='arrow-border'>
                                        </div>
                                        <div class="div100">
                                            <div class="clear pad5">
                                            </div>
                                            <div class="row-fluid">
                                                <div class="span5 textright padright25 bold">
                                                    <asp:Literal ID="ltHeadOffice" runat="server" Text="Head Office Name " meta:resourcekey="ltHeadOfficeResource1"></asp:Literal>
                                                </div>
                                                <div class="span7 wordwrap">
                                                    <asp:Literal ID="ltName" runat="server" meta:resourcekey="ltNameResource1"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <div class="span5 textright padright25 bold">
                                                    <asp:Literal ID="Literal19" runat="server" Text="Belongs to " meta:resourcekey="Literal19Resource1"></asp:Literal>
                                                </div>
                                                <div class="span7 wordwrap">
                                                    <asp:Literal ID="lthobelongsto" runat="server" meta:resourcekey="lthobelongstoResource1"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <div class="span5 textright padright25 bold">
                                                    <asp:Literal ID="Literal22" runat="server" Text="Mobile No " meta:resourcekey="Literal22Resource1"></asp:Literal>
                                                </div>
                                                <div class="span7 wordwrap">
                                                    <asp:Literal ID="lthomobileno" runat="server" meta:resourcekey="lthomobilenoResource1"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <div class="span5 textright padright25 bold">
                                                    <asp:Literal ID="Literal24" runat="server" Text="E-Mail " meta:resourcekey="Literal24Resource1"></asp:Literal>
                                                </div>
                                                <div class="span7 wordwrap">
                                                    <asp:Literal ID="lthoemail" runat="server" meta:resourcekey="lthoemailResource1"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <div class="span5 textright padright25 bold">
                                                    <asp:Literal ID="Literal26" runat="server" Text="Address" meta:resourcekey="Literal26Resource1"></asp:Literal>
                                                </div>
                                                <div class="span7 wordwrap">
                                                    <asp:Literal ID="lthoaddress" runat="server" meta:resourcekey="lthoaddressResource1"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <div class="span5 textright padright25 bold">
                                                    <asp:Literal ID="Literal27" runat="server" Text="City" meta:resourcekey="Literal27Resource1"></asp:Literal>
                                                </div>
                                                <div class="span7 wordwrap">
                                                    <asp:Literal ID="lthocity" runat="server" meta:resourcekey="lthocityResource1"></asp:Literal>
                                                </div>
                                            </div>
                                            <div class="row-fluid">
                                                <div class="span5 textright padright25 bold">
                                                    <asp:Literal ID="Literal30" runat="server" Text="Postal/Zip Code " meta:resourcekey="Literal30Resource1"></asp:Literal>
                                                </div>
                                                <div class="span7 wordwrap">
                                                    <asp:Literal ID="lthozipcode" runat="server" meta:resourcekey="lthozipcodeResource1"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal18" runat="server" Text="Code/Username *" meta:resourcekey="Literal18Resource1"></asp:Literal>
                            </div>
                            <div class="span7" style="width: 46.265%">
                                <asp:Literal ID="ltHeadofficecode" runat="server" meta:resourcekey="ltHeadofficecodeResource1"></asp:Literal>
                                <asp:TextBox ID="txtBOfficeCode" runat="server" CssClass="textbox manfield widthauto"
                                    OnTextChanged="txtBOfficeCode_TextChanged" AutoPostBack="True" MaxLength="10"
                                    ToolTip="Enter Branch Office Code(minimum 6 & Maximum 10 characters)" meta:resourcekey="txtBOfficeCodeResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfBranchOfficeCode" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Branch Office Code is required" SetFocusOnError="True" ControlToValidate="txtBOfficeCode"
                                    Display="Dynamic" meta:resourcekey="rfBranchOfficeCodeResource1"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cmpBr" runat="server" ErrorMessage="Branch Office code can'not be same as Head Office"
                                    CssClass="requiredcolor" ControlToValidate="txtBOfficeCode" Operator="NotEqual"
                                    ControlToCompare="ddlHeadOffice" Text="*" meta:resourcekey="cmpBrResource1"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtBOfficeCode" ID="rfvBranchOfficeCode"
                                    Display="Dynamic" ValidationExpression="^[\s\S]{6,10}$" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Code or Username is minimum of 6 and maximum of 10 characters required."
                                    meta:resourcekey="rfvBranchOfficeCodeResource1"></asp:RegularExpressionValidator>
                                <div id="checkOfficeCode" runat="server" visible="False" style="float: right;">
                                    <asp:Image ID="imgstatus" runat="server" Width="17px" Height="17px" ImageUrl="~/App_Themes/MainTheme/images/Icon_Available.png"
                                        meta:resourcekey="imgstatusResource1" />
                                    <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatusResource1"></asp:Label>
                                </div>
                                <div class="clearfix normitalic pad5">
                                    Ex:dbcylg-Don Bosco Yellagiri Hills
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal1" runat="server" Text="Name *" meta:resourcekey="Literal1Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtBOName" runat="server" CssClass="textbox manfield" MaxLength="50"
                                    ToolTip="Enter branchOffice name" meta:resourcekey="txtBONameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvBName" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Branch Office Name is required" SetFocusOnError="True" ControlToValidate="txtBOName"
                                    Display="Dynamic" meta:resourcekey="rfvBNameResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal2" runat="server" Text="Deployment Model " meta:resourcekey="Literal2Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:RadioButtonList ID="radbtnlstDeployment" runat="server" RepeatDirection="Horizontal"
                                    ToolTip="Select deployment model" meta:resourcekey="radbtnlstDeploymentResource1">
                                    <asp:ListItem Selected="True" Text="Standalone" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Text="Client-Server" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal6" runat="server" Text="Person Incharge " meta:resourcekey="Literal6Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtInchargeName" runat="server" CssClass="textbox" MaxLength="50"
                                    ToolTip="Enter incharge name" meta:resourcekey="txtInchargeNameResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid" runat="server" id="divSubBranch" visible="False">
                            <div class="span5 textright padright25">
                            </div>
                            <div class="span7">
                                <asp:RadioButton ID="rbtnIsSubbranch" Checked="True" Text="Is Sub Branch" runat="server"
                                    ToolTip="Is sub branch" meta:resourcekey="rbtnIsSubbranchResource1" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlCommunicatoinDetails" GroupingText="Communication Details" runat="server"
                        meta:resourcekey="pnlCommunicatoinDetailsResource1">
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal20" runat="server" Text="Mobile No *" meta:resourcekey="Literal20Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox" MaxLength="3"
                                    Text="91" onkeypress="return numbersonly(event, true, this);" ToolTip="Enter country code (Dialing code)"
                                    Width="25px" meta:resourcekey="txtCountryCodeResource1"></asp:TextBox>
                                ~
                                <asp:TextBox ID="txtOrgMobileNo" runat="server" CssClass="textbox manfield" MaxLength="10"
                                    onkeypress="return numbersonly(event, true, this);" Width="201px" ToolTip="Enter branch mobile no (Dialing code followed by number)"
                                    meta:resourcekey="txtOrgMobileNoResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPRmobileno" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Branch office mobile no is required" SetFocusOnError="True" ControlToValidate="txtOrgMobileNo"
                                    Display="Dynamic" meta:resourcekey="rfvPRmobilenoResource1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtOrgMobileNo" ID="RegularExpressionValidator2"
                                    Display="Dynamic" ValidationExpression="^[\s\S]{10,10}$" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Maximum 10 digits required." meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                <div class="clearfix normitalic pad5">
                                    Ex:91 9962663391 - Dialing code followed by number
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal21" runat="server" Text="Phone No *" meta:resourcekey="Literal21Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtOrgPhoneNo" runat="server" CssClass="textbox manfield" MaxLength="12"
                                    ToolTip="Enter branch phone no" onkeypress="return numbersonly(event, true, this);"
                                    meta:resourcekey="txtOrgPhoneNoResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtPRPhoneNo" runat="server" CssClass="requiredcolor"
                                    Text="*" ErrorMessage="Branch phone no is required" SetFocusOnError="True" ControlToValidate="txtOrgPhoneNo"
                                    Display="Dynamic" meta:resourcekey="rfvtxtPRPhoneNoResource1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtOrgPhoneNo" ID="revPhoneNo"
                                    Display="Dynamic" ValidationExpression="^[\s\S]{10,12}$" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Maximum 10 or 12 digits required." meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                <div class="clearfix normitalic pad5">
                                    Ex:03483253227 [(03483) 253227]
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal9" runat="server" Text="E-Mail *" meta:resourcekey="Literal9Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtBranchMail" runat="server" CssClass="textbox manfield" MaxLength="240"
                                    ToolTip="Enter Branch Mail Address" meta:resourcekey="txtBranchMailResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfBranchMail" runat="server" Text="*" ErrorMessage="Branch  Mail ID is required"
                                    SetFocusOnError="True" ControlToValidate="txtBranchMail" CssClass="requiredcolor"
                                    Display="Dynamic" meta:resourcekey="rvfBranchMailResource1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="reMail" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Invalid Branch Email Id" ControlToValidate="txtBranchMail" Display="Dynamic"
                                    ValidationExpression="^((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*$"
                                    meta:resourcekey="reMailResource1" Text="*"></asp:RegularExpressionValidator>
                                <div class="clearfix normitalic pad5 ">
                                    Ex:xxx@yyy.com,yyy@xxx.com,abc@gmail.com
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal8" runat="server" Text="Address" meta:resourcekey="Literal8Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox multiline" TextMode="MultiLine"
                                    onkeypress="return textboxMultilineMaxNumber(event,this,250);" ToolTip="Enter address"
                                    meta:resourcekey="txtAddressResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal3" runat="server" Text="City" meta:resourcekey="Literal3Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="30" ToolTip="Enter city"
                                    meta:resourcekey="txtCityResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal5" runat="server" Text="Country *" meta:resourcekey="Literal5Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlCountry" ToolTip="Select country" runat="server" CssClass="combobox manfield"
                                    AutoPostBack="True" Width="255px" OnSelectedIndexChanged="ddlCountrySelectedIndexChanged"
                                    meta:resourcekey="ddlCountryResource1">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmvCountry" runat="server" CssClass="requiredcolor" ErrorMessage="Country is required"
                                    ControlToValidate="ddlCountry" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                    meta:resourcekey="cmvCountryResource1" Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal7" runat="server" Text="State/Province *" meta:resourcekey="Literal7Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlState" ToolTip="Select state" runat="server" CssClass="combobox manfield"
                                    Width="255px" meta:resourcekey="ddlStateResource1">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmvState" runat="server" CssClass="requiredcolor" ErrorMessage="State is required"
                                    ControlToValidate="ddlState" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                    meta:resourcekey="cmvStateResource1" Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal15" runat="server" Text="Postal/Zip Code *" meta:resourcekey="Literal15Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtPinCode" runat="server" CssClass="textbox  manfield" MaxLength="6"
                                    ToolTip="Enter postal/zip code" onkeypress="return numbersonly(event, true, this);"
                                    meta:resourcekey="txtPinCodeResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPincode" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Postal/Zip Code is required" SetFocusOnError="True" ControlToValidate="txtPinCode"
                                    Display="Dynamic" meta:resourcekey="rfvPincodeResource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal10" runat="server" Text="Third Party Code" meta:resourcekey="Literal10Resource1"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtThirdParty" runat="server" CssClass="textbox" MaxLength="30"
                                    ToolTip="Enter Third Party" meta:resourcekey="txtThirdPartyResource1"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="textcenter Note clear">
                        <span><strong class="red">Note: </strong></span>All Communication Mail will be sent
                        to Branch Office Mail Id
                    </div>
                    <div class="clearfix marginbottom">
                    </div>
                    <div class="textcenter" align="center">
                        <asp:Button ID="btnSaveBranchOffice" OnClick="btnSaveBranchOffice_Click" runat="server"
                            CssClass="button" Text="Save" ToolTip="Click here to save branch office Details"
                            meta:resourcekey="btnSaveBranchOfficeResource1"></asp:Button>
                        <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                            ToolTip="Click  here for new branch office" CausesValidation="False" meta:resourcekey="btnNewResource1">
                        </asp:Button>
                        <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                            CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsBranchOffice" runat="server" ShowSummary="False" ShowMessageBox="True"
        HeaderText="You must enter a value in the following fields:" meta:resourcekey="vsBranchOfficeResource1">
    </asp:ValidationSummary>
    <script type='text/javascript'>

        function showpopupbox(flag) {
            if (flag == true) {
                document.getElementById('divpop').style.display = 'inline';
                $("#divpop").fadeIn("slow");

            }
            else {

                document.getElementById('divpop').style.display = 'none';
            }
        }
                       
    </script>
</asp:Content>
