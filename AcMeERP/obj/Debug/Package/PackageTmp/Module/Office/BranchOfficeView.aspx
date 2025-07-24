<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="BranchOfficeView.aspx.cs" Inherits="AcMeERP.Module.Office.BranchOfficeView"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../../WebControl/GridViewControl.ascx" TagName="GridViewControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="div100">
        <uc1:GridViewControl ID="gvBranchOffice" runat="server" EnableSort="true" />
    </div>
    <asp:UpdatePanel ID="upBranchOfficeView" runat="server">
        <ContentTemplate>
            <div id="ModalOverlay" class="modal_popup_overlay">
            </div>
            <asp:Panel ID="pnlUpdateDB" runat="server" meta:resourcekey="pnlUpdateDBResource1">
                <div id="Display" class="modal_popup_logo">
                    <div class="div100 modal_popup_title">
                        <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                            Branch Preview
                        </div>
                        <div class="floatright">
                            <asp:ImageButton runat="server" ImageAlign="AbsMiddle" OnClientClick="javascript:HideDisplayPopUp();"
                                CssClass="handcursor" ImageUrl="../../App_Themes/MainTheme/images/PopupClose.png"
                                ID="img2" ToolTip="Close" meta:resourcekey="img2Resource1"></asp:ImageButton>
                        </div>
                    </div>
                    <div style="width: 90%; margin: 5px 25px; float: left">
                        <div class="row-fluid" id="divHeadOfficeCode" runat="server">
                            <div class="span5 fieldcaption width35per textright">
                                Head Office Code
                            </div>
                            <div class="span7 wordwrap textleft">
                                <asp:Literal ID="ltHeadOfficeCode" runat="server" meta:resourcekey="ltHeadOfficeCodeResource1"></asp:Literal>
                            </div>
                        </div>
                        <div  class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Branch Office Code
                            </div>
                            <div class="span7 wordwrap textleft">
                                <asp:Literal ID="ltBranchOfficeCode" runat="server" meta:resourcekey="ltBranchOfficeCodeResource1"></asp:Literal>
                            </div>
                        </div>
                        <div  class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Branch Office Name
                            </div>
                            <div class="span7 wordwrap textleft">
                                <asp:Literal ID="ltBranchOfficeName" runat="server" meta:resourcekey="ltBranchOfficeNameResource1"></asp:Literal>
                            </div>
                        </div>
                        <div  class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Email
                            </div>
                            <div class="span7 wordwrap textleft">
                                <asp:Literal ID="ltMailId" runat="server" meta:resourcekey="ltMailIdResource1"></asp:Literal>
                            </div>
                        </div>
                        <div  class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Address
                            </div>
                            <div class="span7 wordwrap textleft">
                                <asp:Literal ID="ltAddress" runat="server" meta:resourcekey="ltAddressResource1"></asp:Literal>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Country
                            </div>
                            <div class="span6 wordwrap textleft textleft">
                                <asp:Literal ID="ltCountry" runat="server" meta:resourcekey="ltCountryResource1"></asp:Literal>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                State
                            </div>
                            <div class="span7 wordwrap textleft">
                                <asp:Literal ID="ltState" runat="server" meta:resourcekey="ltStateResource1"></asp:Literal>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                City
                            </div>
                            <div class="span7 wordwrap textleft">
                                <asp:Literal ID="ltCity" runat="server" meta:resourcekey="ltCityResource1"></asp:Literal>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Pin/PostalCode
                            </div>
                            <div class="span6 wordwrap textleft">
                                <asp:Literal ID="ltPincode" runat="server" meta:resourcekey="ltPincodeResource1"></asp:Literal>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Phone No
                            </div>
                            <div class="span6 wordwrap textleft">
                                <asp:Literal ID="ltPhoneNo" runat="server" meta:resourcekey="ltPhoneNoResource1"></asp:Literal>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 fieldcaption width35per textright">
                                Mobile No
                            </div>
                            <div class="span6 wordwrap textleft">
                                <asp:Literal ID="ltMobileNo" runat="server" meta:resourcekey="ltMobileNoResource1"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function ShowDisplayPopUp(modal) {
            $("#ModalOverlay").show();
            $("#Display").fadeIn(300);
            if (modal) {
                $("#ModalOverlay").unbind("click");
            }
            else {
                $("#ModalOverlay").click(function (e) {

                });
            }
        }

        function HideDisplayPopUp() {
            $("#ModalOverlay").hide();
            $("#Display").fadeOut(300);
        }
    </script>
</asp:Content>
