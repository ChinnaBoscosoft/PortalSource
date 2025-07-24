<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master" AutoEventWireup="true" CodeBehind="AssetUnitofMeasureAdd.aspx.cs" Inherits="AcMeERP.Module.Inventory.Asset.AssetUnitofMeasureAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
<asp:UpdatePanel ID="upUnitofMeasure" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrlUom" runat="server" Text="UoM *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtUoM" runat="server" CssClass="textbox manfield" MaxLength="5"
                        onkeyup="ChangeCase(this.id)" ToolTip="Enter the Unit of Measure"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfUoM" runat="server" Text="*" ErrorMessage="Unit of Measure is required"
                        CssClass="requiredcolor" SetFocusOnError="True" ControlToValidate="txtUoM"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
             <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="Literal1" runat="server" Text="Formal Name"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtFormalName" runat="server" CssClass="textbox manfield" MaxLength="50"
                        onkeyup="ChangeCase(this.id)" ToolTip="Enter the Formal Name"></asp:TextBox>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnUoM" OnClick="btnUoM_Click" runat="server"
                    CssClass="button" Text="Save" ToolTip="Click here to save">
                </asp:Button>
                <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                    ToolTip="Click here for new Unit of Measure" CausesValidation="False">
                </asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="UoMVs" runat="server" ShowSummary="False"
        ShowMessageBox="True"></asp:ValidationSummary>
</asp:Content>
