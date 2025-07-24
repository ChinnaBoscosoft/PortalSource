<%@ Page Title="Register" Language="C#" MasterPageFile="~/MasterPage/HomeMaster.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="AcMeERP.Account.Register" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="False" 
        OnCreatedUser="RegisterUser_CreatedUser" 
        meta:resourcekey="RegisterUserResource2">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server" 
                meta:resourcekey="RegisterUserWizardStepResource2">
                <ContentTemplate>
                    <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <p>
                        Passwords are required to be a minimum of characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server" 
                        meta:resourcekey="ErrorMessageResource2"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup" 
                        meta:resourcekey="RegisterUserValidationSummaryResource2"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                    meta:resourcekey="UserNameLabelResource2" Text="User Name:"></asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" 
                                    meta:resourcekey="UserNameResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup" 
                                    meta:resourcekey="UserNameRequiredResource2" Text="*"></asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" 
                                    meta:resourcekey="EmailLabelResource2" Text="E-mail:"></asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry" 
                                    meta:resourcekey="EmailResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup" 
                                    meta:resourcekey="EmailRequiredResource2" Text="*"></asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                    meta:resourcekey="PasswordLabelResource2" Text="Password:"></asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" 
                                    TextMode="Password" meta:resourcekey="PasswordResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup" 
                                    meta:resourcekey="PasswordRequiredResource2" Text="*"></asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmPassword" 
                                    meta:resourcekey="ConfirmPasswordLabelResource2" Text="Confirm Password:"></asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" 
                                    TextMode="Password" meta:resourcekey="ConfirmPasswordResource2"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" 
                                    CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." 
                                    ValidationGroup="RegisterUserValidationGroup" 
                                    meta:resourcekey="ConfirmPasswordRequiredResource2" Text="*"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                    ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup" 
                                    meta:resourcekey="PasswordCompareResource2" Text="*"></asp:CompareValidator>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup" 
                                meta:resourcekey="CreateUserButtonResource2"/>
                        </p>
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
<asp:CompleteWizardStep runat="server" meta:resourcekey="CompleteWizardStepResource1"></asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
