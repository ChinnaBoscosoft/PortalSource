<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="DeleteVouchers.aspx.cs" Inherits="AcMeERP.Module.Office.DeleteVouchers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript" language="javascript">
        function ChangeDate(s, e) {
            var dateFrom = dteDateFrom.GetDate();
            var DateTo = dateFrom;
            DateTo.setMonth(DateTo.getMonth() + 1);
            DateTo.setDate(DateTo.getDate() - 1);
            dteDateTo.SetDate(DateTo);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upDeleteVoucher" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <asp:Panel ID="pnlClearVoucher" GroupingText="Clear Voucher" runat="server" meta:resourcekey="pnlHeadOfficeBasicInfoResource1">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Head Office
                        </div>
                        <div class="span7">
                            <asp:DropDownList ID="ddlHeadOffice" runat="server" CssClass="combobox manfield"
                                OnSelectedIndexChanged="ddlHeadOffcie_SelectedIndexChanged" Width="275px" ToolTip="Select Head Office"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvHO" runat="server" ControlToValidate="ddlHeadOffice"
                                CssClass="failureNotification" ErrorMessage="Head Office Code is required." Text="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright">
                                Branch Office
                            </div>
                            <div class="span7">
                                <asp:DropDownList ID="ddlBranchOffice" runat="server" CssClass="combobox manfield"
                                    OnSelectedIndexChanged="ddlBranchOffice_SelectedIndexChanged" Width="275px" ToolTip="Select Ledger Group"
                                    meta:resourcekey="ddlGroupResource1" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranchOffice"
                                    CssClass="failureNotification" ErrorMessage="Branch Office Code is required."
                                    Text="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <br />
                        <div class="textcenter">
                            <asp:Button ID="btnDeleteVoucher" Text="Delete Voucher" runat="server" CssClass="button"
                                OnClick="btnDeleteVoucher_Click" />
                            <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="button" CausesValidation="false"
                                PostBackUrl="~/HomeLogin.aspx" />
                        </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
