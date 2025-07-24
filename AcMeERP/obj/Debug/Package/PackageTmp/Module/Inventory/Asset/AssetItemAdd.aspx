<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AssetItemAdd.aspx.cs" Inherits="AcMeERP.Module.Inventory.Asset.AssetItemAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upAssetItems" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal4" runat="server" Text="Asset Class *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:DropDownList ID="ddlAssetClass" runat="server" CssClass="combobox manfield"
                        Width="255px" ToolTip="Select Asset Class">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cmpAssetClass" runat="server" ErrorMessage="Asset Class is required"
                        CssClass="requiredcolor" ControlToValidate="ddlAssetClass" Operator="NotEqual"
                        Type="Integer" ValueToCompare="0" Text="*"></asp:CompareValidator>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrAssetItem" runat="server" Text="Asset Item *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtAssetItem" runat="server" CssClass="textbox manfield" MaxLength="50"
                        onkeyup="ChangeCase(this.id)" ToolTip="Enter the Asset Item"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfUoM" runat="server" Text="*" ErrorMessage="Asset Item is required"
                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtAssetItem"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal2" runat="server" Text="Asset ID Generation"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:DropDownList ID="ddlAssetIDGeneration" runat="server" CssClass="combobox manfield"
                        Width="255px" ToolTip="Select Asset ID Generation">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal3" runat="server" Text="UoM *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:DropDownList ID="ddlUOM" runat="server" CssClass="combobox manfield" AutoPostBack="true"
                        Width="255px"  ToolTip="Select Asset Class">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cmpUom" runat="server" ErrorMessage="Unit of Measure is required"
                        CssClass="requiredcolor" ControlToValidate="ddlUOM" Operator="NotEqual"
                        Type="Integer" ValueToCompare="0" Text="*"></asp:CompareValidator>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal1" runat="server" Text="Starting No *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtStartingNo" runat="server" CssClass="textbox manfield" MaxLength="5" onkeypress="return numbersonly(event, true, this);"
                         ToolTip="Enter the Starting Number"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rvfStartingNo" runat="server" Text="*" ErrorMessage="Starting No is required"
                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtStartingNo"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
             <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal5" runat="server" Text="Prefix *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtPrefix" runat="server" CssClass="textbox manfield" MaxLength="4"
                         ToolTip="Enter the Prefix"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rvfPrefix" runat="server" Text="*" ErrorMessage="Prefix is required"
                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtPrefix"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
             <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal6" runat="server" Text="Suffix"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtSuffix" runat="server" CssClass="textbox manfield" MaxLength="4"
                       ToolTip="Enter the Suffix"></asp:TextBox>
                </div>
            </div>
             <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal7" runat="server" Text="Retention Yrs *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtRetentionYear" runat="server" CssClass="textbox manfield" MaxLength="8"
                         ToolTip="Enter the Retention Year Name"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rvfReYear" runat="server" Text="*" ErrorMessage="Retention Year is required"
                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtRetentionYear"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
             <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal8" runat="server" Text="Depreciation Yrs *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtDepreYear" runat="server" CssClass="textbox manfield" MaxLength="8"
                         ToolTip="Enter the Formal Name"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rvDepyear" runat="server" Text="*" ErrorMessage="Depreciation Year is required"
                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtDepreYear"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
             <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal9" runat="server" Text="Applicable ?"></asp:Literal>
                </div>
                <div class="span7">
                   <asp:CheckBoxList runat="server" ID="chkAssetItemsApplicable" AutoPostBack="true" OnSelectedIndexChanged="chkAssetItemsApplicable_OnSelectedIndexChanged">
                   <asp:ListItem Value="0">Insurance</asp:ListItem>
                   <asp:ListItem Value="1">Annual Maintenance</asp:ListItem>
                   <asp:ListItem Value="2">Depreciation</asp:ListItem>
                   </asp:CheckBoxList>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnAssetItem" OnClick="btnAssetItem_Click" runat="server" CssClass="button"
                    Text="Save" ToolTip="Click here to save"></asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new Asset Item" CausesValidation="False"></asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="AssetItemVs" runat="server" ShowSummary="False" ShowMessageBox="True">
    </asp:ValidationSummary>
</asp:Content>
