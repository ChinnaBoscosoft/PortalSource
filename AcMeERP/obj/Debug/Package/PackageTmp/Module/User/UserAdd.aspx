<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdd.aspx.cs" Inherits="AcMeERP.Module.User.UserAdd"
    MasterPageFile="~/MasterPage/HomeLoginMaster.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
    <asp:UpdatePanel ID="upUser" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <asp:Panel ID="pnlUserInfo" runat="server">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal4" runat="server" Text="First Name *"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtName" runat="server" CssClass="textbox manfield" MaxLength="50"  onkeyup="ChangeCase(this.id)"
                                ToolTip="Enter First Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfNm" runat="server" Text="*" ErrorMessage="First Name is required"
                                CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal9" runat="server" Text="Last Name"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtLastname" runat="server" CssClass="textbox" MaxLength="50" ToolTip="Enter Last Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal1" runat="server" Text="Role *"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="combobox manfield" ToolTip="Select Role"
                                Width="255px">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="cmpRole" runat="server" ErrorMessage="Role is required"
                                ControlToValidate="ddlRole" CssClass="requiredcolor" Operator="NotEqual" Type="Integer"
                                ValueToCompare="0">*</asp:CompareValidator>
                            <asp:Label ID="lblRole" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlLogin" runat="server">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal5" runat="server" Text="Username *"></asp:Literal>
                        </div>
                        <div class="span7" style="width:36.265%">
                           <asp:TextBox ID="txtUserName" runat="server" OnTextChanged="txtUserName_TextChanged"
                                CssClass="textbox manfield" MaxLength="50" AutoPostBack="true" ToolTip="Enter User Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvUn" runat="server" Text="*" ErrorMessage="Username is required"
                                CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtUserName"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                                <div id="checkusername" runat="server" visible="false" style="float:right;">
                            <asp:Image ID="imgstatus" runat="server" Width="17px" Height="17px"  ImageUrl="~/App_Themes/MainTheme/images/Icon_Available.png"/>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </div>
                        </div>
                        
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltpwd" runat="server" Text="Password *"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox manfield" MaxLength="50"
                               onkeypress="javascript:ShowPasswordHelp(true);" onblur="javascript:ShowPasswordHelp(false);"
                                TextMode="Password" ToolTip="Enter Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvPwd" runat="server" Text="*" ErrorMessage="Password is required"
                                CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtPassword"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <ajax:PasswordStrength ID="pwdStrength" TargetControlID="txtPassword" StrengthIndicatorType="Text"
                                PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="8"
                                MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                                TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
                            AverageStrength;GoodStrength;ExcellentStrength" runat="server" />
                            <asp:RegularExpressionValidator ID="revPassword" runat="server" CssClass="requiredcolor"
                                ErrorMessage="Password must be 8-10 characters long with at least 1 numeric,1 alphabet and 1 special character."
                                ControlToValidate="txtPassword" Display="Dynamic" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$">*</asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row-fluid" id="divPwdHelp" style="display: none;">
                        <div class="span5 textright">
                        </div>
                        <div class="span7">
                            <asp:Label ID="lblhelp" runat="server" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal7" runat="server" Text="Confirm Password *"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textbox manfield" MaxLength="50"
                                TextMode="Password" ToolTip="Enter Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcpw" runat="server" Text="*" ErrorMessage="Confirm Password is required"
                                CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtConfirmPassword"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvPwd" runat="server" Text="*" ErrorMessage="Password doesn't match" CssClass="requiredcolor"
                                ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword">*</asp:CompareValidator>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal18" runat="server" Text="Address (Personal)"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="textbox multiline" onkeypress="return textboxMultilineMaxNumber(event,this,250);"
                               ToolTip="Enter Address"></asp:TextBox>
                        </div>
                    </div>
                    <%--<div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal2" runat="server" Text="Place"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtPlace" runat="server" CssClass="textbox" MaxLength="50" ToolTip="Enter Place"></asp:TextBox>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal3" runat="server" Text="City"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="50" ToolTip="Enter City"></asp:TextBox>
                    </div>
                </div>--%>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal6" runat="server" Text="Mobile Number *"></asp:Literal>
                        </div>
                        <div class="span7">
                        <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox" MaxLength="3" Text="91"
                                onkeypress="return numbersonly(event, true, this);" ToolTip="Enter Country Code (Dialing Code)" Width="25px"></asp:TextBox> ~
                            <asp:TextBox ID="txtContact" runat="server" CssClass="textbox manfield" MaxLength="10"
                                onkeypress="return numbersonly(event, true, this);" ToolTip="Enter Mobile Number (Dialing code followed by number)" Width="201px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvfMobileNumber" runat="server" Text="*" ErrorMessage="Mobile Number is required"
                                CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtContact"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ControlToValidate="txtContact" ID="rvftxtContact"
                                        Display="Dynamic" ValidationExpression="^[\s\S]{10,10}$" runat="server" CssClass="requiredcolor"
                                        ErrorMessage="Maximum 10 digits required.">
                                    </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="lt7" runat="server" Text="E-mail *"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox manfield" MaxLength="240"
                                ToolTip="Enter Email Address"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvfEmail" runat="server" Text="*" ErrorMessage="E-mail is required"
                                CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtEmail"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="reMail" runat="server" ErrorMessage="Invalid E-mail"
                                CssClass="requiredcolor" ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="^((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*$">*</asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="Literal8" runat="server" Text="Note"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtnotes" TextMode="MultiLine" runat="server" CssClass="textbox multiline" onkeypress="return textboxMultilineMaxNumber(event,this,250);"
                                ToolTip="Enter Notes"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" runat="server" CssClass="button"
                    Text="Save" ToolTip="Click here to save user Details"></asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new user" CausesValidation="False"></asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False"></asp:Button>
            </div>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="UserVs" runat="server" ShowSummary="false" EnableClientScript="true"
        ShowMessageBox="true" DisplayMode="BulletList"></asp:ValidationSummary>
</asp:Content>
