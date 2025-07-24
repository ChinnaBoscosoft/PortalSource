<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.master"
    AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="AcMeERP.Module.User.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
    <asp:UpdatePanel ID="upLogin" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
            <div class="alert-box notice" id="Notifydiv" runat="server"><span>notice: </span>Please Reset your Password. If Your Password is changed successfully, You are allowed to access the site.</div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password *</asp:Label>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="CurrentPassword"  runat="server" CssClass="textbox manfield"
                            TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                            CssClass="requiredcolor" ErrorMessage="Password is required." ToolTip="Old Password is required.">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password *</asp:Label>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="textbox manfield"
                            TextMode="Password" onkeypress="javascript:ShowPasswordHelp(true);" onblur="javascript:ShowPasswordHelp(false);"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            CssClass="requiredcolor" ErrorMessage="New Password is required." ToolTip="New Password is required.">*</asp:RequiredFieldValidator>
                        <ajax:PasswordStrength ID="pwdStrength" TargetControlID="NewPassword" StrengthIndicatorType="Text"
                            PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="8"
                            MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent"
                            TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
                            AverageStrength;GoodStrength;ExcellentStrength" runat="server" />
                        <asp:RegularExpressionValidator ID="revPassword" runat="server" ErrorMessage="Password must be 8-10 characters long with at least one numeric,1 alphabet and 1 special character."
                            ControlToValidate="NewPassword" Display="Dynamic"   CssClass="requiredcolor" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%^()-*#?&])[A-Za-z\d$@$!%^()-*#?&]{8,}$">*</asp:RegularExpressionValidator>
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
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="textbox manfield"
                            TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                            CssClass="requiredcolor" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                            ToolTip="Confirm New Password is required.">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="The Confirm New Password must match the New Password entry.">*</asp:CompareValidator>
                    </div>
                </div>
            </div>
            <div class="textcenter" align="center">
                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                        CausesValidation="true" Text="Change" CssClass="button" OnClick="ChangePasswordPushButton_click" />
                    <asp:Button ID="CancelPushButton" runat="server" CssClass="button" CausesValidation="False"
                        CommandName="Cancel" Text="Cancel" OnClick="CancelPushButton_click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="changePasswordVs" runat="server" ShowSummary="false" EnableClientScript="true"
        ShowMessageBox="true" DisplayMode="BulletList"></asp:ValidationSummary>
</asp:Content>
