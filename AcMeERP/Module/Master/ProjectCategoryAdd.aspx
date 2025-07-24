<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ProjectCategoryAdd.aspx.cs" Inherits="AcMeERP.Module.Master.ProjectCategoryAdd"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upProjectCategroy" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal4" runat="server" Text="Project Category *" meta:resourcekey="Literal4Resource1"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtProjectCategory" runat="server" CssClass="textbox manfield" MaxLength="30"
                        onkeyup="ChangeCase(this.id)" ToolTip="Enter Project Category" meta:resourcekey="txtProjectCategoryResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfPC" runat="server" Text="*" ErrorMessage="Project Category is required"
                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtProjectCategory"
                        Display="Dynamic" meta:resourcekey="rfPCResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div id="HideShowGeneralateCategory" class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal1" runat="server" Text="Group*" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:DropDownList ID="ddlGeneralateCategory" runat="server" CssClass="combobox manfield"
                        Width="255px" AutoPostBack="True" ToolTip="Select Generalate Category" meta:resourcekey="ddlGeneralateCategoryResource1"
                        OnTextChanged="ddlGeneralateCategory_TextChanged">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Generalate Category is required"
                        CssClass="requiredcolor" ControlToValidate="ddlGeneralateCategory" Operator="NotEqual"
                        Type="Integer" ValueToCompare="0" meta:resourcekey="CompareValidator2Resource1"
                        Text="*"></asp:CompareValidator>
                </div>
            </div>
            <div id="ITRGroupDiv" class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ItrGroupName" runat="server" Text="ITR Group*" meta:resourcekey="Literal1Resource1"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:DropDownList ID="ddlITRGroup" runat="server" CssClass="combobox manfield" Width="255px"
                        AutoPostBack="True" ToolTip="Select ITR Category" meta:resourcekey="ddlITRGroupResource1"
                        OnTextChanged="ddlITRGroup_TextChanged">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="ITR Category is required"
                        CssClass="requiredcolor" ControlToValidate="ddlITRGroup" Operator="NotEqual"
                        Type="Integer" ValueToCompare="0" meta:resourcekey="CompareValidator2Resource1"
                        Text="*"></asp:CompareValidator>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnSaveProjectCategory" OnClick="btnSaveProjectCategory_Click" runat="server"
                    CssClass="button" Text="Save" ToolTip="Click here to save" meta:resourcekey="btnSaveProjectCategoryResource1">
                </asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new Project Category" CausesValidation="False" meta:resourcekey="btnNewResource1">
                </asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False" meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ProjectCategoryVs" runat="server" ShowSummary="False"
        ShowMessageBox="True" meta:resourcekey="ProjectCategoryVsResource1"></asp:ValidationSummary>
    <script type="text/javascript" language="javascript">
        function ShowHideGeneralateCategory(flag) {
            if (flag == "") {
                document.getElementById('HideShowGeneralateCategory').style.display = 'none';
            } else {
                document.getElementById('HideShowGeneralateCategory').style.display = 'block';
            }
        }
    </script>
</asp:Content>
