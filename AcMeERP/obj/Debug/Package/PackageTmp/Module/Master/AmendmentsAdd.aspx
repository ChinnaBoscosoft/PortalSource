<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AmendmentsAdd.aspx.cs" Inherits="AcMeERP.Module.Master.AmendmentsAdd"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upAmendment" runat="server">
        <ContentTemplate>
            <div class="row-fluid">
                <div class="span10 offset1">
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Branch Office
                        </div>
                        <div class="span7 bold">
                            <asp:Label ID="lblBranchOffice" runat="server" meta:resourcekey="lblBranchOfficeResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Project
                        </div>
                        <div class="span7 bold">
                            <asp:Label ID="lblproject" runat="server" meta:resourcekey="lblprojectResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Voucher Date
                        </div>
                        <div class="span7 bold">
                            <asp:Label ID="lblVoucherDate" runat="server" meta:resourcekey="lblVoucherDateResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Voucher No
                        </div>
                        <div class="span7 bold">
                            <asp:Label ID="lblVoucherNo" runat="server" meta:resourcekey="lblVoucherNoResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Voucher Type
                        </div>
                        <div class="span7 bold">
                            <asp:Label ID="lblVoucherType" runat="server" meta:resourcekey="lblVoucherTypeResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Particulars
                        </div>
                        <div class="span7 bold">
                            <asp:Label ID="lblParticulars" runat="server" meta:resourcekey="lblParticularsResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Amount
                        </div>
                        <div class="span7 bold">
                            <asp:Label ID="lblAmount" runat="server" meta:resourcekey="lblAmountResource1"></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Description *
                        </div>
                        <div class="span7 bold">
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="80"
                                Width="400px" CssClass="textbox" MaxLength="500" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescription"
                                CssClass="failureNotification" ErrorMessage="Remark is required" Text="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row-fluid">
                        <div class="span5 textright">
                            Mail Receiver(s)
                        </div>
                        <div class="span7 wordwrap textleft bold">
                            <asp:Literal ID="ltlMailReceiver" runat="server"></asp:Literal>
                        </div>
                        <br />
                        <div class="textcenter">
                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" CssClass="button"
                                ToolTip="Click here to save" meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                                PostBackUrl="~/Module/Master/VoucherView.aspx" CausesValidation="False" meta:resourcekey="hlkCloseResource1">
                            </asp:Button>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="AmendmentVs" runat="server" ShowSummary="False" ShowMessageBox="True"
        meta:resourcekey="AmendmentVsResource1"></asp:ValidationSummary>
</asp:Content>
