<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeMaster.Master" AutoEventWireup="true"
    CodeBehind="ForgotPassword.aspx.cs" Inherits="AcMeERP.Module.User.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upForgotPassword" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <asp:Panel ID="pnlUserName" runat="server">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Label ID="lblUserName" runat="server" Text="Username *"></asp:Label>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox manfield" MaxLength="50"
                                OnTextChanged="txtUserName_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvfUsername" runat="server" ControlToValidate="txtUserName"
                                CssClass="requiredcolor" ErrorMessage="Username is required." ToolTip="Username is required.">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlConfirmationmsg" runat="server" Visible="false">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Label ID="lblConfirmation" runat="server" Text="Send Confirmation Code By *"></asp:Label>
                        </div>
                        <div class="span7">
                            <asp:RadioButtonList runat="server" ID="rblConfirmation" OnSelectedIndexChanged="rblConfirmation_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">SMS</asp:ListItem>
                                <asp:ListItem Value="1">Email</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlConfirmationCode" runat="server" Visible="false">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Label ID="lblConfirmationCode" runat="server" Text="Enter the confirmation code sent to your Mail/SMS *"></asp:Label>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="txtConfirmationCode" runat="server" CssClass="textbox manfield"  OnTextChanged="txtConfirmationCode_TextChanged" AutoPostBack="true"
                                onkeypress="return numbersonly(event, false, this);" MaxLength="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvfConfirmationCode" runat="server" ControlToValidate="txtConfirmationCode"
                                CssClass="requiredcolor" ErrorMessage="Confirmation Code is required." ToolTip="Confirmation Code  is required.">*</asp:RequiredFieldValidator>
                                <asp:ImageButton ID="imgbtnRefresh" runat="server" SkinID="refreshCode_ib" OnClick="imgbtnRefresh_Click" CausesValidation="false" ToolTip="Click here to resend confirmation code"/>
                               <%-- <span class="caption" style="text-align:center">Reset</span>--%>
                        </div>
                        
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="pnlResetPassword" runat="server" Visible="false">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password *</asp:Label>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="NewPassword" runat="server" CssClass="textbox manfield" TextMode="Password"
                                onkeypress="javascript:ShowPasswordHelp(true);" onblur="javascript:ShowPasswordHelp(false);"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                CssClass="requiredcolor" ErrorMessage="New Password is required." ToolTip="New Password is required.">*</asp:RequiredFieldValidator>
                            <ajax:PasswordStrength ID="pwdStrength" TargetControlID="NewPassword" StrengthIndicatorType="Text"
                                PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="8"
                                MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                                TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
                            AverageStrength;GoodStrength;ExcellentStrength" runat="server" />
                            <asp:RegularExpressionValidator ID="revPassword" runat="server" ErrorMessage="Password must be 8-10 characters long with at least one numeric,1 alphabet and 1 special character."
                                ControlToValidate="NewPassword" Display="Dynamic" CssClass="requiredcolor" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%^()-*#?&])[A-Za-z\d$@$!%^()-*#?&]{8,}$">*</asp:RegularExpressionValidator>
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
                            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password *</asp:Label>
                        </div>
                        <div class="span7">
                            <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="textbox manfield" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                CssClass="requiredcolor" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                                ToolTip="Confirm New Password is required.">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                                ErrorMessage="The Confirm New Password must match the New Password entry.">*</asp:CompareValidator>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <asp:Panel ID="pnlbutton" runat="server" Visible="false">
            <div class="textcenter" align="center">
                <asp:Button ID="SetPasswordButton" runat="server" CommandName="SetPassword" CausesValidation="true"
                    Text="Save" CssClass="button" OnClick="SetPasswordButton_click" />
                <asp:Button ID="CancelPushButton" runat="server" CssClass="button" CausesValidation="False"
                    CommandName="Cancel" Text="Cancel" OnClick="CancelPushButton_click" />
            </div>
            </asp:Panel>
            <div class="clearfix">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="changePasswordVs" runat="server" ShowSummary="false" EnableClientScript="true"
        ShowMessageBox="true" DisplayMode="BulletList"></asp:ValidationSummary>
    <script type="text/javascript" language="javascript">
        function ShowPasswordHelp(flag) {
            divPwdHelp.style.display = flag ? "block" : "none";
        }
    </script>
</asp:Content>
