<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="BranchLocation.aspx.cs" Inherits="AcMeERP.Module.Office.BranchLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upUserRole" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal4" runat="server" Text="Branch *"></asp:Literal>
                    </div>
                    <div class="span7">
                        <%--<dx:ASPxComboBox ID="cmbBranch" runat="server" MaxLength="30" 
                            ValueType="System.String" Height="16px" Width="247px">
                        </dx:ASPxComboBox>--%>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="combobox manfield" Width="255px"
                            AutoPostBack="True" ToolTip="Select Ledger Group">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpBranchName" runat="server" ErrorMessage="Branch is required"
                            CssClass="requiredcolor" ControlToValidate="ddlBranch" Operator="NotEqual" Type="Integer"
                            ValueToCompare="0" Text="*"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span5 textright">
                        <asp:Literal ID="Literal1" runat="server" Text="Location *"></asp:Literal>
                    </div>
                    <div class="span7">
                        <asp:TextBox ID="txtBranchLocation" runat="server" CssClass="textbox manfield" MaxLength="30"
                            ToolTip="Enter Location Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                            ErrorMessage="Location is required" CssClass="requiredcolor" SetFocusOnError="true"
                            ControlToValidate="txtBranchLocation" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="textcenter" align="center">
                <asp:Button ID="btnSaveBranchLocation" runat="server" CssClass="button" Text="Save"
                    ToolTip="Click here to save Location Details" OnClick="btnSaveBranchLocation_Click">
                </asp:Button>
                <asp:Button ID="btnNew" runat="server" CssClass="button" Text="New" ToolTip="Click here for new Location"
                    CausesValidation="False" OnClick="btnNew_Click1"></asp:Button>
                <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                    CausesValidation="False"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="BranchLocationVs" runat="server" ShowSummary="False" ShowMessageBox="True">
    </asp:ValidationSummary>
</asp:Content>
