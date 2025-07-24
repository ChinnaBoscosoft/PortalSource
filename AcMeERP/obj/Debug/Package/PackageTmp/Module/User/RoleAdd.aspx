<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="RoleAdd.aspx.cs" Inherits="AcMeERP.Module.User.RoleAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upUserRole" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal4" runat="server" Text="Role Name *"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="textbox manfield" MaxLength="30" ToolTip="Enter Role Name"  onkeyup="ChangeCase(this.id)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRoleName" runat="server" Text="*" ErrorMessage="Role Name is required" CssClass="requiredcolor"
                            SetFocusOnError="true" ControlToValidate="txtRoleName" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnSaveRole" OnClick="btnSaveRole_Click" runat="server" CssClass="button"
                    Text="Save" ToolTip="Click here to save role Details"></asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new role" CausesValidation="False"></asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsRole" runat="server" ShowSummary="false" EnableClientScript="true"
        ShowMessageBox="true" DisplayMode="BulletList"></asp:ValidationSummary>
</asp:Content>
