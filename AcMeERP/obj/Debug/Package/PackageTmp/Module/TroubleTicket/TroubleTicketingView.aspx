<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="TroubleTicketingView.aspx.cs" Inherits="AcMeERP.Module.TroubleTicket.TroubleTicketingView" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Src="~/WebControl/DateControl.ascx" TagName="Datecontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:HiddenField ID="hfmode" runat="server" />
    <asp:HiddenField ID="hfFileName" runat="server" />
    <asp:HiddenField ID="hfdelete" runat="server" />
    <asp:UpdatePanel ID="upTicket" runat="server">
        <ContentTemplate>
            <div class="data" id="divTroubleTicket" style="display: block; padding: 5px !important;width:98%;
                float: left" runat="server">
                <div id="divPostTicket" style="float: right; margin-bottom: 25px;" runat="server">
                    <dx:ASPxButton ID="btnPostTicket" runat="server" Text="Post Ticket" OnClick="btnPostTicket_Click"
                        CausesValidation="false" Image-Url="~/App_Themes/MainTheme/images/add16.png"
                        AutoPostBack="true">
                    </dx:ASPxButton>
                </div>
                <br />
                <asp:Repeater ID="dlTroubleTicketView" runat="server" OnItemDataBound="dlTroubleTicketView_ItemDataBound">
                    <ItemTemplate>
                        <div style="visibility: hidden;">
                            <%#(Eval("TICKET_ID").ToString().Length > 50) ? Eval("TICKET_ID").ToString().Substring(0, 100)+ "..." : Eval("TICKET_ID").ToString()%>
                        </div>
                        <div class="comment-wrap.thread-starter">
                            <div class="comment-nav">
                                <div class="comment-nave-text">
                                    <span class="bold" style="color: Teal"><span style="color: Teal">
                                        <%#Eval("SUBJECT") %></span> </span>
                                    <div class="pricebold">
                                        <%#Eval("POSTED_DATE")%>
                                    </div>
                                </div>
                                <hr width="25%" size="1" align="left">
                                <div class="fontsize11">
                                    <div class="description lighter">
                                        <strong>
                                            <p style="text-indent: 30px; line-height: 20px; font-family: Trebuchet MS; font-size: 13px">
                                                <%#Eval("DESCRIPTION")%></p>
                                        </strong>
                                    </div>
                                </div>
                                <div>
                                    <asp:Repeater ID="rptReplies" runat="server">
                                        <ItemTemplate>
                                            <div class="ReplyContent">
                                                <div class="ReplySubject bold">
                                                    <%#Eval("SUBJECT") %>
                                                </div>
                                                <div class="ReplyUser">
                                                    <span style="color: #4C841F; font-size: 12px;">Replied By: </span><span style="color: #BDA815;
                                                        font-size: 12px;">
                                                        <%#Eval("USER_NAME")%></span>
                                                </div>
                                                <div class="ReplyDate">
                                                    <div style="font-size: 12px;">
                                                        <%#Eval("REPLY_DATE")%>
                                                    </div>
                                                </div>
                                                <br />
                                                <div style="font-size: 13px;">
                                                    <p style="text-indent: 20px; font-family: Trebuchet MS; font-size: 12px;">
                                                        <%#Eval("DESCRIPTION")%>
                                                    </p>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="further_details" style="background-color: #E5EECC;">
                                    <div class="pricetag fontsize11">
                                        <div>
                                            <div id="divEdit" runat="server" style="float: left; padding-top: 6px; padding-left: 6px">
                                                <asp:ImageButton runat="server" ID="btnEdit" CausesValidation="false" OnClick="btnEdit_Click"
                                                    ImageUrl="~/App_Themes/MainTheme/images/edit4.png" CommandArgument='<%#Eval("TICKET_ID")%>'
                                                    ToolTip="Modify Ticket" Text="Edit"></asp:ImageButton><span style="font-weight: bold"></span>
                                            </div>
                                            <div id="divDelete" runat="server" style="float: left; padding-top: 6px; padding-left: 6px">
                                                <asp:ImageButton runat="server" ID="btnDelete" CausesValidation="false" OnClick="btnDelete_Click"
                                                    OnClientClick="javascript:return DeleteConfirmation();" Visible="true" ImageUrl="~/App_Themes/MainTheme/images/Delete1.png"
                                                    CommandArgument='<%#Eval("TICKET_ID")%>' ToolTip="Delete Ticket" Text="Delete">
                                                </asp:ImageButton><span style="font-weight: bold"></span>
                                            </div>
                                            <div id="divReply" runat="server" style="float: left; padding-top: 6px; padding-left: 6px">
                                                <asp:ImageButton runat="server" ID="btnReplytemp" OnClick="btnReply_Click" CausesValidation="false"
                                                    ImageUrl="~/App_Themes/MainTheme/images/reply2.png" CommandArgument='<%#Eval("TICKET_ID")%>'
                                                    ToolTip="Reply to posted ticket" Text="Reply"></asp:ImageButton><span style="font-weight: bold"></span>
                                            </div>
                                            <div style="float: left; padding-top: 6px; padding-left: 6px">
                                                <strong class="bold">Priority: <span style="color: Teal">
                                                    <%#Eval("PRIORITY")%></span> </strong>
                                            </div>
                                            <div style="float: left; padding-top: 6px; padding-left: 6px">
                                                <strong class="bold">User: <span style="color: Teal">
                                                    <%#Eval("USER_NAME")%>
                                                </span></strong>
                                            </div>
                                        </div>
                                        <div style="float: right; padding-top: 5px; padding-left: 6px">
                                            <asp:ImageButton ID="btnComplete" runat="server" ImageUrl="~/App_Themes/MainTheme/images/Completed.png"
                                                CausesValidation="false" CommandArgument='<%#Eval("TICKET_ID")%>' OnClick="btnComplete_Click"
                                                ToolTip="Click to change status to Complete " />
                                            <asp:ImageButton ID="btnInprogress" runat="server" ImageUrl="~/App_Themes/MainTheme/images/progress.png"
                                                CausesValidation="false" CommandArgument='<%#Eval("TICKET_ID")%>' OnClick="btnInprograss_Click"
                                                ToolTip="Click to reopne the ticket" Visible="false" />
                                            <asp:ImageButton ID="btnClarification" runat="server" ImageUrl="~/App_Themes/MainTheme/images/question.png"
                                                CausesValidation="false" CommandArgument='<%#Eval("TICKET_ID")%>' OnClick="btnClarification_Click"
                                                ToolTip="Click to change status to Clarification" Visible="false" />
                                        </div>
                                        <div style="text-align: center; padding-top: 6px; padding-left: 6px">
                                            <strong class="bold">Status: <span style="color: Teal">
                                                <%#Eval("STATUS")%></span> </strong>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblNullText" Text="No Ticket is posted" runat="server" Visible="false"></asp:Label>
                    </FooterTemplate>
                </asp:Repeater>
                <div style="float: right; margin: 0 50px; padding-top: 3px;">
                    <asp:LinkButton ID="lnkPreviousUp" runat="server" navigateurl="~/TroubleTicketingView.aspx"
                        CssClass="PagingStyle" CausesValidation="false" ToolTip="Previous Page" OnClick="lnkPrevious_Click"
                        Text="<< Prev Page"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lnkNextUp" runat="server" navigateurl="~/TroubleTicketingView.aspx"
                        CssClass="PagingStyle" CausesValidation="false" ToolTip="Next Page" OnClick="lnkNext_Click"
                        Text="Nxt Page >>"></asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="ModalOverlay" class="modal_popup_overlay">
    </div>
    <asp:Panel ID="pnlTicket" runat="server" DefaultButton="btnSave">
        <div id="Display" class="modal_popup_logo" style="left: 20%; width: 65%; top: 16%;">
            <div class="div100 modal_popup_title">
                <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                    <asp:UpdatePanel ID="upPopupTitle" runat="server">
                        <ContentTemplate>
                            <asp:Literal ID="ltrPageTitle" runat="server" Text="Post Ticket"></asp:Literal>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="floatright">
                    <img alt="" onclick="javascript:HideDisplayPopUp();" class="handcursor" src="../../App_Themes/MainTheme/images/PopupClose.png"
                        id="img2" title="Close" />
                </div>
            </div>
            <div style="width: 100%; margin: 5px; float: left">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <div class="textcenter red">
                            <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <div class="row-fluid">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="span2 textright">
                                <asp:Literal ID="ltrSubject" runat="server" Text="Subject *"></asp:Literal>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="span8">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtSubject" runat="server" Wrap="true" Width="510px" CssClass="textbox manfield"
                                    AutoCompleteType="Disabled" MaxLength="100" ToolTip="Enter the Subject"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfSubject" runat="server" Text="*" CssClass="requiredcolor"
                                    ErrorMessage="Subject is required" SetFocusOnError="true" ControlToValidate="txtSubject"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span2 textright">
                        <asp:Literal ID="ltDescription" runat="server" Text="Description *"></asp:Literal>
                    </div>
                    <div class="span7">
                        <span class="Subsidetitle lighter">Please describe the nature of the problem, this will
                            help to solve your problem faster.</span>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span2 textright">
                    </div>
                    <div class="span8">
                        <div class="wordwrap">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" Width="510px" CssClass="textbox multiline manfield"
                                        onkeypress="return textboxMultilineMaxNumber(event,this,1000);" Height="200px"
                                        TextMode="MultiLine" ToolTip="Enter the Description here" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfDescription" runat="server" Text="*" ErrorMessage="Description is required."
                                        CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtDescription"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span2 textright">
                        <asp:UpdatePanel ID="uptPriority" runat="server">
                            <ContentTemplate>
                                <asp:Literal ID="ltPriority" runat="server" Text="Priority *"></asp:Literal>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="span7">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlPriority" ToolTip="Select the Priority" runat="server" CssClass="combobox manfield"
                                    Width="250px">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row-fluid" style="display: none" id="divfilename" runat="server">
                    <div class="span2 textright">
                        &nbsp;
                    </div>
                    <div class="span7">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <span id="spFileName" runat="server" class="normitalic"></span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="textcenter" align="center">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="button"
                                CausesValidation="true" AutoPostBack="false" Text="Save" ToolTip="Click here to save the Ticket">
                            </asp:Button>
                            <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                                ToolTip="Click  here for new ticket" CausesValidation="False"></asp:Button>
                            <asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close"
                                CausesValidation="False" OnClientClick="javascript:HideDisplayPopUp()"></asp:Button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script language="javascript" type="text/javascript">

        function ShowDisplayPopUp(modal) {
            $("#ModalOverlay").show();
            $("#Display").fadeIn(300);
            document.getElementById('<%=txtSubject%>').focus();
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

        function ValidateImg(ctrlname, lblctrl) {
            //alert('ValidateImg');
            var status;
            var img = document.getElementById(ctrlname);
            var ctrl = ctrlname;
            var imgCol = new Array();
            var imgName = new Array();
            var lblName = document.getElementById(lblctrl);
            imgCol = img.value.split(':');
            //If user Enter the path rather selecting then need to check
            if (imgCol[0].length > 0) {
                imgName = img.value.split('.');
                var fext = imgName[imgName.length - 1].toLowerCase();
                if (fext == "jpg" || fext == "jpeg" || fext == "png" || fext == "tif" || fext == "xls" || fext == "xlsx" || fext == "doc" || fext == "docx" || fext == "zip" || fext == "rar" || fext == "txt" || fext == "pdf") {
                    document.getElementById('<%=divfilename.ClientID %>').style.display = 'block';
                    document.getElementById('<%=spFileName.ClientID %>').innerHTML = img.value;
                    document.getElementById('<%=hfFileName.ClientID %>').value = img.value;
                    return true;
                }
                else {
                    alert('[Select only jpg,jpeg,png,tif,xls,xlsx,doc,docx,pdf and txt Files]');
                    //Clearing in the IE browser
                    if (navigator.appName.indexOf("Microsoft") != -1) {
                        var who = document.getElementById(ctrl);
                        var who2 = who.cloneNode(false);
                        who2.onchange = who.onchange;
                        who.parentNode.replaceChild(who2, who);
                    }
                    else //Clearing in the FireFox browser
                    {
                        document.getElementById(ctrl).value = "";
                    }
                    return false;
                }
            }
        }

        function DeleteConfirmation() {
            var DeleteStatus = confirm("Are sure to delete ?");
            if (DeleteStatus == true) {
                return true;
            }
            else {
                return false;
            }
        }
        
    </script>
</asp:Content>
