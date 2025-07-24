<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPage/HomeMaster.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AcMeERP.Account.Portal.Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
    <asp:UpdatePanel ID="upLogin" runat="server">
        <ContentTemplate>
            <div style="text-align: center; margin-top: 106px;">
                <div class="loginRepeat">
                    <div class="loginTitle" align="center">
                    </div>
                    <div class="loginBottom" style="margin-top: 25px">
                        <asp:Panel runat="server" ID="pnlLogin" DefaultButton="btnSignIn" 
                            meta:resourcekey="pnlLoginResource1">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                ValidationGroup="LoginUserValidationGroup" ShowMessageBox="True"
                                ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />
                            <div style="text-align: left">
                                <div class="div100">
                                    <div class="div100 margintop15">
                                        <div class="logincaption leftfloat">
                                            <strong>
                                                <asp:Label ID="UserNameLabel" runat="server" 
                                                AssociatedControlID="txtUserName" 
                                                meta:resourcekey="UserNameLabelResource1" Text="Username:"></asp:Label></strong>
                                        </div>
                                        <div class="div50 leftfloat">
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="50" 
                                                Width="215px" meta:resourcekey="txtUserNameResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" 
                                                ControlToValidate="txtUserName" CssClass="requiredcolor"
                                                ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                                ValidationGroup="LoginUserValidationGroup" 
                                                meta:resourcekey="rfvtxtUserNameResource1" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="div100 margintop5">
                                        <div class="logincaption leftfloat">
                                            <strong>
                                                <asp:Label ID="PasswordLabel" runat="server" 
                                                AssociatedControlID="txtPassword" 
                                                meta:resourcekey="PasswordLabelResource1" Text="Password:"></asp:Label></strong>
                                        </div>
                                        <div class="div50 leftfloat">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" MaxLength="30" Width="215px"
                                                TextMode="Password" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" 
                                                ControlToValidate="txtPassword" CssClass="requiredcolor"
                                                ErrorMessage="Password is required." ToolTip="Password is required." 
                                                ValidationGroup="LoginUserValidationGroup" 
                                                meta:resourcekey="rfvtxtPasswordResource1" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="div100 margintop5">
                                        <div class="logincaption leftfloat">
                                            &nbsp;
                                        </div>
                                        <div class="div50 leftfloat">
                                            <asp:Button ID="btnSignIn" runat="server" CssClass="button" CommandName="Login" Text="Login"
                                                ValidationGroup="LoginUserValidationGroup" OnClick="btnSignIn_Click" 
                                                meta:resourcekey="btnSignInResource1" />
                                                 <a  href='<%= Page.ResolveUrl("~/Module/User/ForgotPassword.aspx")%>' id="A1" tabindex="-1" class="link margin-left20px">Forgot Password?</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
