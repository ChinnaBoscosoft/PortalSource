<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="upload_downloadadd.aspx.cs" Inherits="AcMeERP.Module.Software.upload_downloadadd" %>

<%@ Register Src="~/WebControl/DateControl.ascx" TagName="Datecontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:HiddenField ID="hfmode" runat="server" />
    <asp:HiddenField ID="hfFileName" runat="server" />
    <asp:HiddenField ID="hfReleasefileName" runat="server" />
    <asp:HiddenField ID="hfdelete" runat="server" />
    <asp:HiddenField ID="hfreleasedelete" runat="server" />
    <asp:Panel DefaultButton="btnUpload" ID="pnlSoftware" runat="server">
        <div class="row-fluid">
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrFile" runat="server" Text="Build/File *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:FileUpload ID="fuUploadFile" runat="server" ToolTip="Select File to Upload"
                        onchange="javascript:ValidateImg(this.id,this.id);" />
                </div>
            </div>
            <div class="row-fluid" style="display: none" id="divfilename" runat="server">
                <div class="span5 textright">
                    &nbsp;
                </div>
                <div class="span7">
                    <span id="spFileName" runat="server" class="normitalic"></span>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrlRelease" runat="server" Text="Release Note"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:FileUpload ID="fuUploadRelease" runat="server" ToolTip="Select Release Note to Upload"
                        onchange="javascript:ValidateRelease(this.id,this.id);" />
                </div>
            </div>
            <div class="row-fluid" style="display: none" id="divReleaseName" runat="server">
                <div class="span5 textright">
                    &nbsp;
                </div>
                <div class="span7">
                    <span id="spReleaseNoteName" runat="server" class="normitalic"></span>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrlReleaseDate" runat="server" Text="Release Date *"></asp:Literal>
                </div>
                <div class="span7" >
                    <%-- <uc1:Datecontrol ID="dtReleaseDate" runat="server" showMandatory="true" showCalender="true"
                        ShowTime="true"/>--%>
                    <dx:ASPxDateEdit runat="server" ID="dteReleaseDate" EditFormat="DateTime" CssClass="combobox manfield"  UseMaskBehavior="true" DisplayFormatString="dd/MM/yyyy hh:mm tt"  EditFormatString="dd/MM/yyyy hh:mm tt">
                      <%--  <TimeSectionProperties Visible="true" >                           
                        </TimeSectionProperties>--%>
                    </dx:ASPxDateEdit>
                  
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrReleaseVersion" runat="server" Text="Release Version *"></asp:Literal>
                </div>
                <div class="span7" >
                    <asp:TextBox ID="txtReleaseVersion" runat="server" CssClass="textbox manfield" MaxLength="30"
                        ToolTip="Enter Release Version"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvfReleaseVersion" runat="server" Text="*" ErrorMessage="Release Version is required"
                        CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtReleaseVersion"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrTitle" runat="server" Text="Title *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox manfield" MaxLength="30"
                        ToolTip="Enter Title"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvfTitle" runat="server" Text="*" ErrorMessage="Title is required"
                        CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtTitle"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrDescription" runat="server" Text="Description *"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox multiline manfield"
                        onkeypress="return textboxMultilineMaxNumber(event,this,250);" TextMode="MultiLine"
                        ToolTip="Enter Description"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvfDescription" runat="server" Text="*" ErrorMessage="Description is required"
                        CssClass="requiredcolor" SetFocusOnError="true" ControlToValidate="txtDescription"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span5 textright">
                    <asp:Literal ID="ltrFileType" runat="server" Text="Upload Type"></asp:Literal>
                </div>
                <div class="span7">
                    <asp:CheckBox ID="chkUploadType" runat="server" Checked="false" Text="Is Prerequisite?" />
                </div>
            </div>
        </div>
        <div class="textcenter" align="center">
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" CssClass="button"
                CausesValidation="true" OnClientClick="javascript:return validateEmptyFile();"
                Text="Upload" ToolTip="Click here to upload File"></asp:Button>
            <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" CssClass="button" Text="New"
                ToolTip="Click here for new file upload" CausesValidation="False"></asp:Button>
            <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                CausesValidation="False"></asp:Button>
        </div>
        <div class="clearfix">
        </div>
    </asp:Panel>
    <script language="javascript" type="text/javascript">

        function validateEmptyFile() {

            var val = true;
            var controls = true;
            var alertmsg = '';
            var ctrl = document.getElementById('<%=fuUploadFile.ClientID %>');
            if (document.getElementById('<%=hfmode.ClientID %>').value == 0) {
                if (ctrl.value == '') {
                    val = false;
                    alert("Select a file to be uploaded");
                }

                if (document.getElementById('<%=txtTitle.ClientID %>').value == '')
                    alertmsg += 'Title is required \n';
                if (document.getElementById('<%=txtDescription.ClientID %>').value == '')
                    alertmsg += 'Description is required \n';
                if (document.getElementById('<%=txtReleaseVersion.ClientID %>').value == '')
                    alertmsg += 'Release Version is required \n';

                if (document.getElementById('<%=txtTitle.ClientID %>').value == '' || document.getElementById('<%=txtDescription.ClientID %>').value == '' || document.getElementById('<%=txtReleaseVersion.ClientID %>').value == '') {
                    alert(alertmsg);
                    controls = false;
                }
            }
            return (val && controls);

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
                    alert('[Select only jpg,jpeg,png,tif,xls,xlsx,doc,docx,zip,rar,pdf and txt Files]');
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
        function ValidateRelease(ctrlname, lblctrl) {
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
                if (fext == "txt") {
                    document.getElementById('<%=divReleaseName.ClientID %>').style.display = 'block';
                    document.getElementById('<%=spReleaseNoteName.ClientID %>').innerHTML = img.value;
                    document.getElementById('<%=hfReleasefileName.ClientID %>').value = img.value;
                    return true;
                }
                else {
                    alert('[Select only txt File]');
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
    </script>
</asp:Content>
