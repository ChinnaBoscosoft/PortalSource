<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="AcMeERP.Module.User.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upUser" runat="server">
        <ContentTemplate>
            <div class="row-fluid content-innerprofile" style="border: 1px solid #CCCCCC; border-radius: 3px;">
                <div class="span3">
                    <div style="float: left; position: relative; width: 100%;" id="leftside">
                        <h2 class="subtitle">
                            My Account</h2>
                        <div class="maintitle">
                            <div class="sidetitle">
                                Settings
                            </div>
                            <div class="overflowhidden">
                                <asp:LinkButton class="Subsidetitle aimg_cursor" ID="profile" runat="server" ToolTip="My Profile"
                                    Text="My Profile" CausesValidation="false" OnClick="profile_Click">
                                </asp:LinkButton>
                            </div>
                            <div class="overflowhidden">
                                <asp:LinkButton class="Subsidetitle aimg_cursor" ID="changepwd" runat="server" ToolTip="Change Password"
                                    Text="Change Password" CausesValidation="false" OnClick="changepwd_Click">
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="maintitle">
                            <div class="sidetitle">
                                My Search History
                            </div>
                            <div class="overflowhidden">
                                <%--<asp:LinkButton class="Subsidetitle aimg_cursor" ID="productsearch" runat="server"
                            ToolTip="My Product Search" Text="My Product Search" OnClick="productsearch_Click"
                            CausesValidation="false">
                        </asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="panel">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Profile Information</h3>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="LtrlFirsName" runat="server" Text="First Name"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Literal ID="ltFirstName" runat="server" Text=""></asp:Literal>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltrlLastName" runat="server" Text="Last Name"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Literal ID="ltLastName" runat="server" Text=""></asp:Literal>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltrlRole" runat="server" Text="Role"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Literal ID="ltrRole" runat="server" Text=""></asp:Literal>
                        </div>
                    </div>
                    
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltrlAddress" runat="server" Text="Address"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Literal ID="ltrAddress" runat="server" Text=""></asp:Literal>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltrlMobileNo" runat="server" Text="Mobile Number"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Literal ID="ltrMobileNo" runat="server" Text=""></asp:Literal>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltrlEmail" runat="server" Text="E-mail"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Literal ID="ltrEmail" runat="server" Text=""></asp:Literal>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltrlStatus" runat="server" Text="Status"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Label ID="ltrStatus" runat="server" Text="" CssClass="label_status"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            <asp:Literal ID="ltrlNote" runat="server" Text="Note"></asp:Literal>
                        </div>
                        <div class="span7">
                            <asp:Literal ID="ltrNote" runat="server" Text=""></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>--%>
                <asp:Panel ID="Profilediv" runat="server">
                    <div class="span6 margintop" style="float: left;">
                        <div class="div100">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal4" runat="server" Text="First Name *"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox manfield" MaxLength="50"
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
                                    <asp:Literal ID="Literal1" runat="server" Text="Role"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="ltRole" runat="server" CssClass="textbox" Enabled="false" ToolTip="User Role"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal5" runat="server" Text="Status"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="ltStatus" runat="server" CssClass="textbox" Enabled="false" ToolTip="User Status"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal18" runat="server" Text="Address"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="textbox multiline" onkeypress="return textboxMultilineMaxNumber(event,this,250);"
                                      ToolTip="Enter Address"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal6" runat="server" Text="Mobile Number *"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textbox" MaxLength="3"
                                        Text="91" onkeypress="return numbersonly(event, true, this);" ToolTip="Enter Country Code"
                                        Width="25px"></asp:TextBox>
                                    ~
                                    <asp:TextBox ID="txtContact" runat="server" CssClass="textbox manfield" MaxLength="10"
                                        onkeypress="return numbersonly(event, true, this);" ToolTip="Enter Mobile Number"
                                        Width="201px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfMobileNumber" runat="server" Text="*" ErrorMessage="Mobile Number is required"
                                        CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtContact"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="lt7" runat="server" Text="E-mail *"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox manfield" MaxLength="40"
                                        ToolTip="Enter Email Address"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="rvfEmail" runat="server" Text="*" ErrorMessage="E-mail is required"
                                CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtEmail"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="reMail" runat="server" ErrorMessage="Invalid Email"
                                        CssClass="requiredcolor" ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Literal ID="Literal8" runat="server" Text="Note"></asp:Literal>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="txtnotes" TextMode="MultiLine" runat="server" CssClass="textbox multiline"
                                        ToolTip="Enter Notes" onkeypress="return textboxMultilineMaxNumber(event,this,250);"></asp:TextBox>
                                    </iv>
                                </div>
                            </div>
                            <div class="textcenter marginbottom offset2" align="center">
                                <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" runat="server" CssClass="button"
                                    Text="Save" ToolTip="Click here to save Profile"></asp:Button>
                                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                                    CausesValidation="False"></asp:Button>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="changePwddiv" runat="server" Visible="false">
                    <div class="span6 margintop" style="float: left;">
                        <div class="alert-box notice" id="Notifydiv" runat="server" style="width: 130%;">
                            <span>notice: </span>Please Reset your Password. If Your Password is changed successfully
                            , You are allowed to access the site.</div>
                        <div class="row-fluid">
                            <div class="row-fluid">
                                <div class="span5 textright">
                                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password *</asp:Label>
                                </div>
                                <div class="span7">
                                    <asp:TextBox ID="CurrentPassword" runat="server" CssClass="textbox manfield" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                        CssClass="requiredcolor" ErrorMessage="Password is required." ToolTip="Old Password is required.">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
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
                        </div>
                        <div class="textcenter offset2" align="center">
                            <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                CausesValidation="true" Text="Change" CssClass="button" OnClick="ChangePasswordPushButton_click" />
                            <asp:Button ID="CancelPushButton" runat="server" CssClass="button" CausesValidation="False"
                                CommandName="Cancel" Text="Cancel" OnClick="CancelPushButton_click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="UserVs" runat="server" ShowSummary="false" EnableClientScript="true"
        ShowMessageBox="true" DisplayMode="BulletList"></asp:ValidationSummary>
    <asp:ValidationSummary ID="changePasswordVs" runat="server" ShowSummary="false" EnableClientScript="true"
        ShowMessageBox="true" DisplayMode="BulletList"></asp:ValidationSummary>
</asp:Content>
