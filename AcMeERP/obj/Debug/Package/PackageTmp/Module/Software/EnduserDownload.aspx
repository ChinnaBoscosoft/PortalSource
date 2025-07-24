<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="EnduserDownload.aspx.cs" Inherits="AcMeERP.Module.Software.EnduserDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
     <asp:UpdatePanel ID="upendUser" runat="server">
        <ContentTemplate>
    <div class="row-fluid content-inner" style="border: 1px solid #CCCCCC; border-radius: 3px;">
        <div class="span9">
            <div class="divscroll">
                <div style="float: right;">
                    <asp:LinkButton ID="lnkPreviousUp" runat="server" navigateurl="~/EnduserDownload.aspx"
                        CssClass="PagingStyle" ToolTip="Previous Page" OnClick="lnkPrevious_Click" Text="<< Prev Page"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lnkNextUp" runat="server" navigateurl="~/EnduserDownload.aspx"
                        CssClass="PagingStyle" ToolTip="Next Page" OnClick="lnkNext_Click" Text="Nxt Page >>"></asp:LinkButton>
                </div>
                <asp:Repeater ID="rpdownloads" runat="server">
                    <ItemTemplate>
                        <div class="bodymain margin-left3px">
                            <div class="wlsub">
                                <div class="row-fluid">
                                    <div class="span4" id="img">
                                        <div id="lu-image">
                                            <a class="aimg_cursor" href='<%#  Bosco.Utility.CommonMember.DOWNLOAD_FOLDER + Eval("PhysicalFile").ToString() %>'
                                                title="Download File" target="_blank">
                                                <asp:Image ID="imgDownldFile" runat="server" ImageUrl="~/App_Themes/MainTheme/images/downloadpng.png"
                                                    CssClass="downloadmarginTop" AlternateText="No Image..."></asp:Image>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="span8">
                                        <div class="title_back">
                                            <%#(Eval("Title").ToString().Length > 50) ? Eval("Title").ToString().Substring(0, 50) + "..." : Eval("Title").ToString()%>
                                            <div class="price_border">
                                            </div>
                                        </div>
                                        <div class="price_title">
                                            <div class="further_details">
                                                <div class="pricetag">
                                                    <div class="pricebold">
                                                        <%#Eval("Released on")%>
                                                    </div>
                                                    <div class="pricetag fontsize11">
                                                        <strong>Version:
                                                            <%#Eval("Version")%>
                                                        </strong>
                                                    </div>
                                                    <div class="fontsize11">
                                                        <div class="listpricekhead">
                                                            <strong>
                                                                <%#Eval("Description")%></strong>
                                                        </div>
                                                    </div>
                                                    <div id="Div1" class="pricetag" runat="server">
                                                        <strong>File Size:
                                                            <%#Eval("FILESIZE")%></strong>
                                                        <div class="removelist"> 
                                                            <%--<a class="test" href='<%#Eval("PhysicalRelease").ToString()!=""? Bosco.Utility.CommonMember.DOWNLOAD_FOLDER+Eval("PhysicalRelease").ToString():"javascript:void(0);"%>'
                                                                title="Release Note" target="_blank" runat="server">Read Me.....</a>--%>
                                                                <asp:LinkButton ID="lnkReadMe" CssClass="aimg_cursor underline fontsize11" runat="server" CausesValidation="false"
                                                                Text="Read Me....." OnClick="lnkReadMe_Click" CommandArgument='<%#Eval("PhysicalRelease").ToString()%>' Visible='<%#Eval("PhysicalRelease").ToString()!="" ? true:false%>'  OnClientClick="javascript:HideDisplayPopUp()"></asp:LinkButton>
                                                          </div>     
                                                               
                                                        
                                                    </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div style="float: right;">
                    <asp:LinkButton ID="lnkPrevious" runat="server" navigateurl="~/EnduserDownload.aspx"
                        Text="<< Prev Page" CssClass="PagingStyle" ToolTip="Previous Page" OnClick="lnkPrevious_Click"></asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="lnkNext" runat="server" navigateurl="~/EnduserDownload.aspx"
                        Text="Nxt Page >>" CssClass="PagingStyle" ToolTip="Next Page" OnClick="lnkNext_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
        <div>
        </div>
        <div class="span3 ">
            <div class="maintitle ">
                <div class="subtitle">
                    Prerequisite
                </div>
            </div>
            <asp:DataList ID="dlPrerequisite" runat="server" RepeatLayout="Flow">
                <ItemTemplate>
                    <div class="title_back margintop9">
                        <%#(Eval("Title").ToString().Length > 25) ? Eval("Title").ToString().Substring(0, 25) + "..." : Eval("Title").ToString()%>
                    </div>
                    <a title="Download File" href='<%#  Bosco.Utility.CommonMember.DOWNLOAD_FOLDER +Eval("PhysicalFile").ToString()%>'
                        class="PreRequsiteColor">
                        <%#Eval("Attachments")%></a>
                    <div style="float: right; color: Black; margin-right: 5px; font-weight: bold;">
                        <%#Eval("FILESIZE")%>
                    </div>
                    <div style="border-top: 1px dotted rgb(220, 88, 7); padding-top: 4px;">
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div id="ModalOverlay" class="modal_popup_overlay">
            </div>
     <asp:Panel ID="Release" runat="server">
                <div id="Display" class="modal_popup_logo">
                    <div class="div100 modal_popup_title">
                        <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                           Release Note..........
                        </div>
                        <div class="floatright">
                            <img alt="" onclick="javascript:HideDisplayPopUp();" class="handcursor" src="../../App_Themes/MainTheme/images/PopupClose.png"
                                id="img2" title="Close" />
                        </div>
                    </div>
                    <div style="width: 90%; margin: 5px 25px; float: left" class="wordwrap releaseNote">
                       <asp:Label ID="lblReadMe" Text="" runat="server"></asp:Label>
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
    <%-- <script type='text/javascript'>

        function showpopupbox(flag) {
            var moveLeft = 20;
            var moveDown = 10;
            if (flag == true) {

                document.getElementById('divpop').style.display = 'inline';
                $("#divpop").fadeIn("slow");
                $('#ctl00_cpMain_rpdownloads_ctl01_lnkReadMe').click(function (e) {
                    $("#divpop").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
                });

            }
            else {

                document.getElementById('divpop').style.display = 'none';
            }
        }
    </script>--%>
  </asp:Content>
