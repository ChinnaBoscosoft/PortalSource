<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="GoverningMemberAdd.aspx.cs" Inherits="AcMeERP.Module.Master.GoverningMemberAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upGoverningMember" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span10 offset1">
                    <asp:Panel ID="pnlMembersInfo" GroupingText="Members Info" runat="server">
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="ltrlName" runat="server" Text="Name *"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtMemberName" runat="server" CssClass="textbox manfield" MaxLength="50" onkeyup="ChangeCase(this.id)" 
                                    ToolTip="Enter member name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMName" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Name is required" SetFocusOnError="True" ControlToValidate="txtMemberName"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal1" runat="server" Text="Occupation"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtOccupation" runat="server" CssClass="textbox" MaxLength="50"
                                    ToolTip="Enter Occupation"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal2" runat="server" Text="Date of Birth"></asp:Literal>
                            </div>
                            <div class="span7">
                                <dx:ASPxDateEdit ID="dtDOB" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                    EditFormatString="dd/MM/yyyy" CssClass="combobox" Width="100px" EditFormat="Custom">
                                </dx:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal3" runat="server" Text="Nationality *"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtNationality" runat="server" CssClass="textbox manfield" MaxLength="50"
                                    ToolTip="Enter Nationality"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNationality" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Nationality is required" SetFocusOnError="True" ControlToValidate="txtNationality"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal4" runat="server" Text="Religion"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtReligion" runat="server" CssClass="textbox" MaxLength="50"
                                    ToolTip="Enter Religion"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal5" runat="server" Text="Role"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtRole" runat="server" CssClass="textbox" MaxLength="50"
                                    ToolTip="Enter Role"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal7" runat="server" Text="Father/Husband's Name"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtFHusName" runat="server" CssClass="textbox" MaxLength="50"
                                    ToolTip="Enter Father/Husband's Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal8" runat="server" Text="Relationship with Office Bearer"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtRelationOB" runat="server" CssClass="textbox" MaxLength="50"
                                    ToolTip="Enter Relationship with Office Bearer"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal9" runat="server" Text="Office Held in Association"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtOfficeHeld" runat="server" CssClass="textbox" MaxLength="50"
                                    ToolTip="Enter Office Held in Association"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal10" runat="server" Text="Country *"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlCountry" ToolTip="Select country" runat="server" CssClass="combobox manfield"
                                    AutoPostBack="True" Width="255px" OnSelectedIndexChanged="ddlCountrySelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmvCountry" runat="server" CssClass="requiredcolor" ErrorMessage="Country is required"
                                    ControlToValidate="ddlCountry" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                    Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal11" runat="server" Text="State/Province *"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlState" ToolTip="Select state" runat="server" CssClass="combobox manfield"
                                    Width="255px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmvState" runat="server" CssClass="requiredcolor" ErrorMessage="State is required"
                                    ControlToValidate="ddlState" Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                    Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal12" runat="server" Text="PAN/UID #"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtPanNo" CssClass="textbox uppercase" MaxLength="10" runat="server"
                                    ToolTip="Enter PAN No" Width="248px"></asp:TextBox>
                                <div class="clearfix normitalic pad5 ">
                                    Ex:ABCDE1111F
                                </div>
                                <ajax:MaskedEditExtender ID="mePAN" runat="server" Century="2000" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" Mask="LLLLL9999L" TargetControlID="txtPanNo">
                                </ajax:MaskedEditExtender>
                                <ajax:MaskedEditValidator ID="mevPanNo" runat="server" ControlExtender="mePAN" ControlToValidate="txtPanNo"
                                    Display="Dynamic" ErrorMessage="mevPanNo" ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]">
                                </ajax:MaskedEditValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal13" runat="server" Text="Fax"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtFax" CssClass="textbox" MaxLength="10" runat="server" ToolTip="Enter Fax"
                                    Width="248px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal14" runat="server" Text="Pin/Zip Code"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtPinCode" CssClass="textbox" MaxLength="10" runat="server" ToolTip="Enter Pin/Zip Code"
                                    Width="248px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal15" runat="server" Text="Address"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox multiline" TextMode="MultiLine" MaxLength="150"
                                    onkeypress="return textboxMultilineMaxNumber(event,this,150);" ToolTip="Enter address"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal16" runat="server" Text="Place"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtPlace" runat="server" CssClass="textbox" ToolTip="Enter Place" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal6" runat="server" Text="Contact No"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtContactNo" runat="server" CssClass="textbox" MaxLength="50" ToolTip="Enter Contact No"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal17" runat="server" Text="Email"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtMail" runat="server" CssClass="textbox" MaxLength="100" ToolTip="Enter Email"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="reMail" runat="server" CssClass="requiredcolor"
                                    ErrorMessage="Invalid Email" ControlToValidate="txtMail" Display="Dynamic" ValidationExpression="^((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*$"
                                    Text="*"></asp:RegularExpressionValidator>
                                <div class="clearfix normitalic pad5 ">
                                    abc@gmail.com
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal18" runat="server" Text="URL"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtUrl" runat="server" CssClass="textbox" MaxLength="100" ToolTip="Enter URL"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal20" runat="server" Text="Date of Joining"></asp:Literal>
                            </div>
                            <div class="span7">
                                <dx:ASPxDateEdit ID="dtDateofJoin" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                    EditFormatString="dd/MM/yyyy" CssClass="combobox" Width="100px" EditFormat="Custom">
                                </dx:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal21" runat="server" Text="Date of Exit"></asp:Literal>
                            </div>
                            <div class="span7">
                                <dx:ASPxDateEdit ID="dtDateofExit" runat="server" UseMaskBehavior="True" DisplayFormatString="dd/MM/yyyy"
                                    EditFormatString="dd/MM/yyyy" CssClass="combobox" Width="100px" EditFormat="Custom">
                                </dx:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:Literal ID="Literal19" runat="server" Text="Notes"></asp:Literal>
                            </div>
                            <div class="span7">
                                <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox multiline" TextMode="MultiLine" MaxLength="500"
                                    onkeypress="return textboxMultilineMaxNumber(event,this,500);" ToolTip="Enter Notes"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="textcenter" align="center">
                        <asp:Button ID="btnGMSave" OnClick="btnGMSave_Click" runat="server" CssClass="button"
                            Text="Save" ToolTip="Click here to save governing member details"></asp:Button>
                        <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                            ToolTip="Click  here for new governing member" CausesValidation="False"></asp:Button>
                        <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                            CausesValidation="False"></asp:Button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsGoverningMember" runat="server" ShowSummary="False"
        ShowMessageBox="True" HeaderText="You must enter a value in the following fields:">
    </asp:ValidationSummary>
</asp:Content>
