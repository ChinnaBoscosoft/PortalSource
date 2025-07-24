<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="PortalReportCriteria.aspx.cs" Inherits="AcMeERP.Report.PortalReportCriteria"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:HiddenField ID="hdSBranchId" runat="server" />
    <asp:HiddenField ID="hdSSocietyId" runat="server" />
    <asp:HiddenField ID="hdSProjectId" runat="server" />
    <asp:HiddenField ID="hdSBankId" runat="server" />
    <asp:HiddenField ID="hdSLedgerId" runat="server" />
    <asp:HiddenField ID="hdSLedgerGroupId" runat="server" />
    <asp:HiddenField ID="hdSCostCenterId" runat="server" />
    <asp:HiddenField ID="hdSelectedCategoryID" runat="server" />
    <asp:UpdatePanel ID="upReport" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="div100" style="z-index: 111 !important; top: 0; left: 0;">
                <dx:ASPxDocumentViewer ID="dvReportViewer" runat="server" ReportTypeName="Bosco.Report.ReportObject"
                    ClientInstanceName="dvReportViewer" OnCacheReportDocument="dvReportViewer_CacheReportDocument"
                    OnRestoreReportDocumentFromCache="dvReportViewer_RestoreReportDocumentFromCache"
                    meta:resourcekey="dvReportViewerResource1" OnUnload="dvReportViewer_Unload" Theme="Office2010Blue">
                    <StylesViewer>
                        <BookmarkSelectionBorder BorderColor="Gray" BorderStyle="Dashed" BorderWidth="3px" />
                    </StylesViewer>
                    <StylesSplitter>
                        <Pane>
                            <Paddings Padding="16px" />
                        </Pane>
                    </StylesSplitter>
                    <ToolbarItems>
                        <dx:ReportToolbarButton ImageUrl="~/App_Themes/MainTheme/images/Report.png" Name="Customization"
                            ToolTip="Customise" meta:resourcekey="ReportToolbarButtonResource1" />
                        <dx:ReportToolbarSeparator />
                        <dx:ReportToolbarButton ItemKind="PrintReport" meta:resourcekey="ReportToolbarButtonResource2" />
                        <%-- <dx:ReportToolbarButton ItemKind="PrintPage" meta:resourcekey="ReportToolbarButtonResource3" />
                        <dx:ReportToolbarSeparator />--%>
                        <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" meta:resourcekey="ReportToolbarButtonResource4" />
                        <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" meta:resourcekey="ReportToolbarButtonResource5" />
                        <dx:ReportToolbarLabel ItemKind="PageLabel" meta:resourcekey="ReportToolbarLabelResource1" />
                        <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px" meta:resourcekey="ReportToolbarComboBoxResource1">
                        </dx:ReportToolbarComboBox>
                        <dx:ReportToolbarLabel ItemKind="OfLabel" meta:resourcekey="ReportToolbarLabelResource2" />
                        <dx:ReportToolbarTextBox ItemKind="PageCount" meta:resourcekey="ReportToolbarTextBoxResource1" />
                        <dx:ReportToolbarButton ItemKind="NextPage" meta:resourcekey="ReportToolbarButtonResource6" />
                        <dx:ReportToolbarButton ItemKind="LastPage" meta:resourcekey="ReportToolbarButtonResource7" />
                        <dx:ReportToolbarSeparator />
                        <dx:ReportToolbarButton ItemKind="SaveToDisk" meta:resourcekey="ReportToolbarButtonResource8" />
                        <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px" meta:resourcekey="ReportToolbarComboBoxResource2">
                            <Elements>
                                <dx:ListElement Value="pdf" meta:resourcekey="ListElementResource1" />
                                <dx:ListElement Value="xls" meta:resourcekey="ListElementResource2" />
                                <dx:ListElement Value="xlsx" meta:resourcekey="ListElementResource3" />
                                <dx:ListElement Value="rtf" meta:resourcekey="ListElementResource4" />
                                <dx:ListElement Value="mht" meta:resourcekey="ListElementResource5" />
                                <dx:ListElement Value="html" meta:resourcekey="ListElementResource6" />
                                <dx:ListElement Value="txt" meta:resourcekey="ListElementResource7" />
                                <dx:ListElement Value="csv" meta:resourcekey="ListElementResource8" />
                                <dx:ListElement Value="png" meta:resourcekey="ListElementResource9" />
                            </Elements>
                        </dx:ReportToolbarComboBox>
                    </ToolbarItems>
                    <ClientSideEvents ToolbarItemClick="function(s, e) {
	if(e.item.name == &quot;Customization&quot;)
    {
      ReportCriteriaCallback.PerformCallback(e.item.name);
      ShowDisplayPopUp();
    }
    if(e.item.name==&quot;DrillDown&quot;)
    {
         window.location.href=window.location.href.split('&')[0]+'&hdva=true&DrillBack=true';
    }
   }" />
                </dx:ASPxDocumentViewer>
                <dx:ASPxCallback ID="ReportCriteriaCallback" runat="server" ClientInstanceName="ReportCriteriaCallback"
                    OnCallback="ReportCriteriaCallback_Callback">
                </dx:ASPxCallback>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="ModalOverlay" class="modal_popup_overlay">
    </div>
    <asp:Panel ID="pnlPortalReportCriteria" runat="server" DefaultButton="btnOk" meta:resourcekey="pnlPortalReportCriteriaResource1">
        <div id="Display" class="modal_popup_logo" style="width: 58%; left: 23%; top: 15%;
            z-index: 9999 !important;">
            <div class="div100 modal_popup_title">
                <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                    <asp:Label ID="lblReport" runat="server" meta:resourcekey="lblReportResource1"></asp:Label>
                </div>
                <div class="floatright">
                    <img alt="" onclick="javascript:HideDisplayPopUp();" class="handcursor" src="../../App_Themes/MainTheme/images/PopupClose.png"
                        id="img2" title="Close" />
                </div>
            </div>
            <div style="width: 97%; margin: 10px; float: left;">
                <div id="divCriteria" runat="server" style="width: 100%;">
                    <ajax:TabContainer ID="TabReportCriteria" runat="server" ActiveTabIndex="0" Height="300px"
                        Width="100%" meta:resourcekey="TabReportCriteriaResource1">
                        <ajax:TabPanel runat="server" HeaderText="Date" ID="tabDate" meta:resourcekey="tabDateResource1">
                            <ContentTemplate>
                                <div style="width: 100%; padding-left: 16px; padding-top: 10px; text-align: center;">
                                    <div style="width: 50%; padding-left: 23px; float: left">
                                        <div style="float: left; width: 27%">
                                            <asp:Label ID="lblDtFrom" runat="server" Text="Date From" Style="float: left;" meta:resourcekey="lblDtFromResource1"></asp:Label>
                                        </div>
                                        <div style="float: left;">
                                            <asp:UpdatePanel ID="upDateFrom" runat="server">
                                                <ContentTemplate>
                                                    <dx:ASPxDateEdit ID="dteFrom" runat="server" Width="94px" UseMaskBehavior="True"
                                                        ClientInstanceName="dteDateFrom" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                                                        meta:resourcekey="dteFromResource1" EditFormat="Custom">
                                                        <ClientSideEvents DateChanged="function(s, e) { ChangeDate(s, e); }" />
                                                    </dx:ASPxDateEdit>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div style="width: 36%; float: right">
                                        <div style="float: left; width: 29%">
                                            <asp:Label ID="lblDtTo" runat="server" Text="Date To " Style="float: left;" meta:resourcekey="lblDtToResource1"></asp:Label>
                                        </div>
                                        <div style="float: left;">
                                            <asp:UpdatePanel ID="upDateTo" runat="server">
                                                <ContentTemplate>
                                                    <dx:ASPxDateEdit ID="dteTo" runat="server" Width="94px" UseMaskBehavior="True" ClientInstanceName="dteDateTo"
                                                        DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" meta:resourcekey="dteToResource1"
                                                        EditFormat="Custom">
                                                    </dx:ASPxDateEdit>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 94%; float: left; padding-top: 25px; padding-left: 20px;">
                                    <asp:UpdatePanel ID="upDateLedger" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvDateLedger" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
                                                ShowHeader="False" SkinID="Rpt_Criteria" GridLines="None" meta:resourcekey="gvDateLedgerResource1">
                                                <Columns>
                                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDateLedger" runat="server" Text='<%# Eval("NAME") %>' Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                                                meta:resourcekey="chkDateLedgerResource1" />
                                                            <asp:DropDownList ID="ddlFD" runat="server" Visible="False" OnSelectedIndexChanged="ddlFD_SelectedIndexChanged"
                                                                meta:resourcekey="ddlFDResource1">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabBranch" runat="server" HeaderText="Branch" meta:resourcekey="TabBranchResource1"
                            TabIndex="1">
                            <ContentTemplate>
                                <div style="width: 100%;">
                                    <div style="width: 100%; overflow-y: auto; overflow-x: hidden; height: 306px;">
                                        <asp:UpdatePanel ID="upBranch" runat="server">
                                            <ContentTemplate>
                                                <div style="padding-top: 2px; width: 100%; float: left;">
                                                    <div id="toolBar" runat="server" style="width: 100%; float: left">
                                                        <div style="width: 100%; float: left">
                                                            <div class="divrow toolbar">
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="ltSearch" runat="server" Text="Filter"></asp:Literal>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                    <asp:TextBox ID="txtSearch" runat="server" EnableTheming="False" Width="300px" CssClass="textbox"
                                                                        TabIndex="1"></asp:TextBox>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                    <a onclick="javascript:InitiateCallBack('BRF');" tabindex="2">
                                                                        <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <a onclick="javascript:InitiateCallBack('BRFR');" tabindex="3">
                                                                        <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                                </div>
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Label ID="lblBranchRecordCount" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                <div class="clearfloat">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="width: 100%; float: left;">
                                                    <asp:GridView ID="gvBranch" runat="server" AutoGenerateColumns="False" SkinID="Rpt_Criteria"
                                                        DataKeyNames="BRANCH_OFFICE_ID" meta:resourcekey="gvBranchResource1">
                                                        <Columns>
                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkBranchSelectAll" runat="server" onclick="javascript:SelectAllBranch();InitiateCallBack('BR');"
                                                                        meta:resourcekey="chkBranchSelectAllResource1" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkBranchSelect" runat="server" ValidationGroup='<%# Eval("BRANCH_OFFICE_ID") %>'
                                                                        Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>' meta:resourcekey="chkBranchSelectResource1"
                                                                        onclick="javascript:GetSelectedBranchId();InitiateCallBack('BR');" TabIndex="4" />
                                                                    <asp:HiddenField ID="hdBranchId" runat="server" Value='<%# Eval("BRANCH_OFFICE_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Branch Name" DataField="BRANCH_OFFICE_NAME" meta:resourcekey="BoundFieldResource1" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabSociety" runat="server" HeaderText="Legal Entity" meta:resourcekey="TabSocietyResource1">
                            <ContentTemplate>
                                <div style="width: 100%; overflow-y: auto; overflow-x: hidden; height: 306px;">
                                    <asp:UpdatePanel ID="upSociety" runat="server">
                                        <ContentTemplate>
                                            <div style="padding-top: 2px; width: 100%; float: left;">
                                                <div id="divSocietyFilter" runat="server" style="width: 100%; float: left">
                                                    <div style="width: 100%; float: left">
                                                        <div class="divrow toolbar">
                                                            <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <asp:Literal ID="Literal1" runat="server" Text="Filter"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtSocietySearch" runat="server" EnableTheming="False" Width="300px"
                                                                    CssClass="textbox"></asp:TextBox>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                <a id="lbtSocietySearch" onclick="javascript:InitiateCallBack('SYF');">
                                                                    <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <a id="lbtSocietyRefresh" onclick="javascript:InitiateCallBack('SYFR');">
                                                                    <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                            </div>
                                                            <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <asp:Literal ID="Literal2" runat="server" Text=""></asp:Literal>
                                                            </div>
                                                            <div class="clearfloat">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="div100">
                                                <asp:GridView ID="gvSociety" runat="server" AutoGenerateColumns="False" SkinID="Rpt_Criteria"
                                                    DataKeyNames="CUSTOMERID" meta:resourcekey="gvSocietyResource1">
                                                    <Columns>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSocietySelectAll" runat="server" onclick="javascript:SelectAllSociety();InitiateCallBack('SY');"
                                                                    meta:resourcekey="chkSocietySelectAllResource1" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSocietySelect" runat="server" ValidationGroup='<%# Eval("CUSTOMERID") %>'
                                                                    onclick="javascript:GetSelectedSocietyId();InitiateCallBack('SY');" Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                                                    meta:resourcekey="chkSocietySelectResource1" />
                                                                <asp:HiddenField ID="hdSocietyId" runat="server" Value='<%# Eval("CUSTOMERID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Society Name" DataField="Society Name" ReadOnly="True"
                                                            meta:resourcekey="BoundFieldResource2" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabProject" runat="server" HeaderText="Project" Visible="False"
                            meta:resourcekey="TabProjectResource1">
                            <ContentTemplate>
                                <div style="width: 100%;">
                                    <div id="divProject" runat="server" style="width: 100%; height: 307px; float: left;
                                        overflow-y: auto; overflow-x: hidden;">
                                        <asp:UpdatePanel ID="upProject" runat="server">
                                            <ContentTemplate>
                                                <div style="padding-top: 2px; width: 100%; float: left;">
                                                    <div id="divProjectFilter" runat="server" style="width: 100%; float: left">
                                                        <div style="width: 100%; float: left">
                                                            <div class="divrow toolbar">
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal3" runat="server" Text="Filter"></asp:Literal>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                    <asp:TextBox ID="txtProjectSearch" runat="server" EnableTheming="False" Width="150px"
                                                                        TabIndex="1" CssClass="textbox"></asp:TextBox>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                    <a id="lbtLProjectFilter" onclick="javascript:InitiateCallBack('PJF');" tabindex="2">
                                                                        <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <a id="lbtLProjectRefresh" onclick="javascript:InitiateCallBack('PJFR');">
                                                                        <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                                </div>
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal4" runat="server" Text=""></asp:Literal>
                                                                </div>
                                                                <div class="clearfloat">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="div100">
                                                    <asp:GridView ID="gvProject" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="PROJECT_ID" SkinID="Rpt_Criteria" meta:resourcekey="gvProjectResource1">
                                                        <Columns>
                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkProjectSelectAll" ToolTip="Click To Select All" runat="server"
                                                                        onclick="javascript:SelectAllProject();InitiateCallBack('PJ');" meta:resourcekey="chkProjectSelectAllResource1" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkProjectSelect" runat="server" ValidationGroup='<%# Eval("PROJECT_ID") %>'
                                                                        onclick="javascript:GetSelectedProjectId();InitiateCallBack('PJ');" Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                                                        meta:resourcekey="chkProjectSelectResource1" />
                                                                    <asp:HiddenField ID="hdProjectId" runat="server" Value='<%# Eval("PROJECT_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Project" DataField="PROJECT" ReadOnly="True" meta:resourcekey="BoundFieldResource3" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div style="margin-left: 10px; height: 307px; float: left; overflow-y: auto; overflow-x: hidden;"
                                        runat="server" id="divBank">
                                        <asp:UpdatePanel ID="UpBankaccount" runat="server">
                                            <ContentTemplate>
                                                <div style="padding-top: 2px; width: 100%; float: left;" id="divBankFilter" runat="server" visible="false">
                                                    <div id="divBankAccountFilter" runat="server" style="width: 100%; float: left">
                                                        <div style="width: 100%; float: left">
                                                            <div class="divrow toolbar">
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal13" runat="server" Text="Filter"></asp:Literal>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                    <asp:TextBox ID="txtBankAccountFilter" runat="server" EnableTheming="False" Width="150px"
                                                                        TabIndex="1" CssClass="textbox"></asp:TextBox>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                    <a id="btnBankAccountSearch" onclick="javascript:InitiateCallBack('BKF');" tabindex="2">
                                                                        <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <a id="btnBankAccountRefresh" onclick="javascript:InitiateCallBack('BKFR');">
                                                                        <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                                </div>
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal14" runat="server" Text=""></asp:Literal>
                                                                </div>
                                                                <div class="clearfloat">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="div100">
                                                    <asp:GridView ID="gvBankAccount" runat="server" Width="100%" AutoGenerateColumns="False"
                                                        DataKeyNames="BANK_ID" SkinID="Rpt_Criteria" Visible="False" meta:resourcekey="gvBankAccountResource1">
                                                        <Columns>
                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkBankSelectAll" runat="server" onclick="javascript:SelectAllBank();InitiateCallBack('BK');"
                                                                        ToolTip="Click To Select All" meta:resourcekey="chkBankSelectAllResource1" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkBankSelect" runat="server" ValidationGroup='<%# Eval("BANK_ACCOUNT_ID") %>'
                                                                        onclick="javascript:GetSelectedBankId();InitiateCallBack('BK');" Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                                                        meta:resourcekey="chkBankSelectResource1" />
                                                                    <asp:HiddenField ID="hdBankId" runat="server" Value='<%# Eval("BANK_ACCOUNT_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Bank" DataField="BANK" ReadOnly="True" meta:resourcekey="BoundFieldResource4" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabLedger" runat="server" HeaderText="Ledger" Visible="False"
                            meta:resourcekey="TabLedgerResource1">
                            <ContentTemplate>
                                <div style="width: 100%;">
                                    <div style="width: 48%; height: 307px; float: left; overflow-y: auto; overflow-x: hidden;">
                                        <asp:UpdatePanel ID="upLedgerGroup" runat="server">
                                            <ContentTemplate>
                                                <div style="padding-top: 2px; width: 100%; float: left;">
                                                    <div id="divLedgerGroupFilter" runat="server" style="width: 100%; float: left">
                                                        <div style="width: 100%; float: left">
                                                            <div class="divrow toolbar">
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal5" runat="server" Text="Filter"></asp:Literal>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                    <asp:TextBox ID="txtLedgerGroupFilter" runat="server" EnableTheming="False" Width="150px"
                                                                        TabIndex="1" CssClass="textbox"></asp:TextBox>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                    <a id="lbtLedgerGroupFilter" onclick="javascript:InitiateCallBack('LGGF');" tabindex="2">
                                                                        <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <a id="lbtLedgerGroupRefresh" onclick="javascript:InitiateCallBack('LGGFR');" tabindex="3">
                                                                        <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                                </div>
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal6" runat="server" Text=""></asp:Literal>
                                                                </div>
                                                                <div class="clearfloat">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="div100">
                                                    <asp:GridView ID="gvLedgerGroup" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        DataKeyNames="GROUP_ID" SkinID="Rpt_Criteria" meta:resourcekey="gvLedgerGroupResource1">
                                                        <Columns>
                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkLedgerGroupSelectAll" ToolTip="Click To Select All" runat="server"
                                                                        onclick="javascript:SelectAllLedgerGroup();InitiateCallBack('LGG');" meta:resourcekey="chkLedgerGroupSelectAllResource1" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkLedgerGroupSelect" runat="server" ValidationGroup='<%# Eval("GROUP_ID") %>'
                                                                        TabIndex="4" Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                                                        onclick="javascript:GetSelectedLedgerGroupId();InitiateCallBack('LGG');" meta:resourcekey="chkLedgerGroupSelectResource1" />
                                                                    <asp:HiddenField ID="hdLedgerGroupId" runat="server" Value='<%# Eval("GROUP_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Ledger Group" DataField="GROUP" ReadOnly="True" meta:resourcekey="BoundFieldResource5" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div style="width: 50%; margin-left: 10px; float: left; height: 307px; overflow-y: auto;
                                        overflow-x: hidden;">
                                        <asp:UpdatePanel ID="upLedger" runat="server">
                                            <ContentTemplate>
                                                <div style="padding-top: 2px; width: 100%; float: left;">
                                                    <div id="divLedgerFilter" runat="server" style="width: 100%; float: left">
                                                        <div style="width: 100%; float: left">
                                                            <div class="divrow toolbar">
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal7" runat="server" Text="Filter"></asp:Literal>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                    <asp:TextBox ID="txtLedgerSearch" runat="server" EnableTheming="False" Width="150px"
                                                                        TabIndex="5" CssClass="textbox"></asp:TextBox>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                    <a id="lbtLedgerFilter" onclick="javascript:InitiateCallBack('LGF');" tabindex="6">
                                                                        <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <a id="lbtLedgerSearch" onclick="javascript:InitiateCallBack('LGFR');" tabindex="7">
                                                                        <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                                </div>
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal8" runat="server" Text=""></asp:Literal>
                                                                </div>
                                                                <div class="clearfloat">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="div100">
                                                    <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="False" SkinID="Rpt_Criteria"
                                                        DataKeyNames="LEDGER_ID" meta:resourcekey="gvLedgerResource1">
                                                        <Columns>
                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkLedgerSelectAll" runat="server" onclick="javascript:SelectAllLedger();InitiateCallBack('LG');"
                                                                        meta:resourcekey="chkLedgerSelectAllResource1" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkLedgerSelect" runat="server" ValidationGroup='<%# Eval("LEDGER_ID") %>'
                                                                        TabIndex="8" onclick="javascript:GetSelectedLedgerId();InitiateCallBack('LG');"
                                                                        Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>' meta:resourcekey="chkLedgerSelectResource1" />
                                                                    <asp:HiddenField ID="hdLedgerId" runat="server" Value='<%# Eval("LEDGER_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Ledger" DataField="LEDGER" ReadOnly="True" meta:resourcekey="BoundFieldResource6" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabCostCentre" runat="server" HeaderText="Cost Centre" Visible="False"
                            meta:resourcekey="TabCostCentreResource1">
                            <ContentTemplate>
                                <div style="width: 100%; overflow-y: auto; overflow-x: hidden; height: 306px;">
                                    <asp:UpdatePanel ID="upCostCenter" runat="server">
                                        <ContentTemplate>
                                            <div style="padding-top: 2px; width: 100%; float: left;">
                                                <div id="divCostCentreFilter" runat="server" style="width: 100%; float: left">
                                                    <div style="width: 100%; float: left">
                                                        <div class="divrow toolbar">
                                                            <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <asp:Literal ID="Literal9" runat="server" Text="Filter"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtCostCentreFilter" runat="server" EnableTheming="False" Width="300px"
                                                                    TabIndex="1" CssClass="textbox"></asp:TextBox>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                <a id="lbtCostCentreSearch" onclick="javascript:InitiateCallBack('CCF');" tabindex="2">
                                                                    <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <a id="lbtCostCentreRefresh" onclick="javascript:InitiateCallBack('CCFR');" tabindex="3">
                                                                    <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                            </div>
                                                            <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <asp:Literal ID="Literal10" runat="server" Text=""></asp:Literal>
                                                            </div>
                                                            <div class="clearfloat">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="div100">
                                                <asp:GridView ID="gvCostCentre" runat="server" AutoGenerateColumns="False" SkinID="Rpt_Criteria"
                                                    DataKeyNames="COST_CENTRE_ID" meta:resourcekey="gvCostCentreResource1">
                                                    <Columns>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkCostCentreSelectAll" runat="server" onclick="javascript:SelectAllCostCentre();InitiateCallBack('CC');"
                                                                    meta:resourcekey="chkCostCentreSelectAllResource1" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkCostCentreSelect" runat="server" ValidationGroup='<%# Eval("COST_CENTRE_ID") %>'
                                                                    TabIndex="4" onclick="javascript:GetSelectedCostCenterId();InitiateCallBack('CC');"
                                                                    Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>' meta:resourcekey="chkCostCentreSelectResource1" />
                                                                <asp:HiddenField ID="hdCostCenterId" runat="server" Value='<%# Eval("COST_CENTRE_ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Cost Centre" DataField="COST_CENTRE_NAME" ReadOnly="True"
                                                            meta:resourcekey="BoundFieldResource7" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabProjectCategory" runat="server" HeaderText="Category" Visible="False"
                            meta:resourcekey="TabProjectCategoryResource1">
                            <ContentTemplate>
                                <div style="width: 100%;">
                                    <div id="div1" runat="server" style="width: 100%; height: 307px; float: left; overflow-y: auto;
                                        overflow-x: hidden;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div style="padding-top: 2px; width: 100%; float: left;">
                                                    <div id="div2" runat="server" style="width: 100%; float: left">
                                                        <div style="width: 100%; float: left">
                                                            <div class="divrow toolbar">
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal11" runat="server" Text="Filter"></asp:Literal>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                    <asp:TextBox ID="txtProjectCategorySearch" runat="server" EnableTheming="False" Width="300px"
                                                                        CssClass="textbox"></asp:TextBox>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                    <a id="lbtProjectCategorySearch" onclick="javascript:InitiateCallBack('PCF');">
                                                                        <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                                </div>
                                                                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <a id="lbtProjectCategoryRefresh" onclick="javascript:InitiateCallBack('PCFR');">
                                                                        <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                                </div>
                                                                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                    <asp:Literal ID="Literal12" runat="server" Text=""></asp:Literal>
                                                                </div>
                                                                <div class="clearfloat">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="div100">
                                                    <asp:GridView ID="gvProjectCategory" Width="100%" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="PROJECT_CATOGORY_ID" SkinID="Rpt_Criteria" meta:resourcekey="gvProjectCategoryResource1">
                                                        <Columns>
                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource9">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkProjectCategorySelect" runat="server" ValidationGroup='<%# Eval("PROJECT_CATOGORY_ID") %>'
                                                                        onclick="javascript:GetSelectedProjectCategoryId();InitiateCallBack('PC');" Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                                                        meta:resourcekey="chkProjectCategorySelectResource1" />
                                                                    <asp:HiddenField ID="hdProjectCatogoryId" runat="server" Value='<%# Eval("PROJECT_CATOGORY_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Category" DataField="PROJECT_CATOGORY_NAME" ReadOnly="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="TabReportSetup" runat="server" HeaderText="Report Setup" meta:resourcekey="TabReportSetupResource1">
                            <ContentTemplate>
                                <div style="float: left; width: 100%">
                                    <div style="float: left; width: 50%; padding-top: 10px;">
                                        <div style="float: left; width: 100%; padding-top: 10px;">
                                            <div style="width: 100%; float: left;">
                                                <asp:CheckBox ID="chkShowTitle" runat="server" Text="Show Titles At Each Page" meta:resourcekey="chkShowTitleResource1" />
                                            </div>
                                            <div style="width: 100%; float: left; padding-top: 10px;">
                                                <asp:CheckBox ID="chkVerticalLine" runat="server" Text="Veritcal Line" Checked="True"
                                                    meta:resourcekey="chkVerticalLineResource1" />
                                            </div>
                                            <div style="width: 100%; float: left; padding-top: 10px;">
                                                <asp:CheckBox ID="chkHorizontalLine" runat="server" Text="Horizontal Line" Checked="True"
                                                    meta:resourcekey="chkHorizontalLineResource1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 49%; padding-top: 10px;">
                                        <div style="float: left; width: 100%;">
                                            <div style="width: 50%; float: left;">
                                                <asp:Label ID="lblReportDate" runat="server" Text="Report Date  " Style="float: left;"
                                                    meta:resourcekey="lblReportDateResource1"></asp:Label>
                                            </div>
                                            <div style="float: left;">
                                                <dx:ASPxDateEdit ID="dtreportdate" runat="server" Width="94px" UseMaskBehavior="True"
                                                    DisplayFormatString="dd/MM/yyyy" meta:resourcekey="dtreportdateResource1">
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 10px;">
                                            <div style="width: 50%; float: left;">
                                                <asp:Label ID="lblTitleAlignment" runat="server" Text="Title Alignment  " Style="float: left;"
                                                    meta:resourcekey="lblTitleAlignmentResource1"></asp:Label>
                                            </div>
                                            <div style="float: left;">
                                                <asp:DropDownList ID="ddlTitleAlignment" runat="server" meta:resourcekey="ddlTitleAlignmentResource1">
                                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource5" Text="Left"></asp:ListItem>
                                                    <asp:ListItem Value="2" Selected="True" meta:resourcekey="ListItemResource6" Text="Centre"></asp:ListItem>
                                                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource7" Text="Right"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="float: left; width: 100%; padding-top: 10px;">
                                            <div style="width: 100%; float: left;">
                                                <asp:CheckBox ID="chkShowReportLogo" runat="server" Text="Show Report Logo" meta:resourcekey="chkShowReportLogoResource1" />
                                            </div>
                                            <div style="width: 100%; float: left; padding-top: 10px;">
                                                <asp:CheckBox ID="chkShowPageNumber" runat="server" Text="Show Page Number" Checked="True"
                                                    meta:resourcekey="chkShowPageNumberResource1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div style="float: left; width: 100%; padding-top: 10px; visibility: hidden;">
                                        <div style="width: 24%; float: left; padding-top: 4px;">
                                            <asp:Label ID="ReportTitleAs" runat="server" Text="Show Report Title As" meta:resourcekey="ReportTitleAsResource1"></asp:Label>
                                        </div>
                                        <div style="float: left;">
                                            <asp:RadioButtonList ID="rgbTitle" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rgbTitleResource1">
                                                <asp:ListItem Selected="True" Value="0" meta:resourcekey="ListItemResource8" Text="Institute Name"></asp:ListItem>
                                                <asp:ListItem Value="1" meta:resourcekey="ListItemResource9" Text="Society Name"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </div>
                <div style="width: 100%; text-align: center; margin-top: 5px;">
                    <asp:Button ID="btnOk" Text="OK" runat="server" CssClass="button" OnClick="btnOk_Click"
                        Width="55px" meta:resourcekey="btnOkResource1" />
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" OnClientClick="javascript:HideDisplayPopUp()"
                        meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript" language="javascript">

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
            document.getElementById('<%=txtSearch.ClientID %>').value = "";
            document.getElementById('<%=txtSocietySearch.ClientID %>').value = "";
            document.getElementById('<%=txtProjectSearch.ClientID %>').value = "";
            document.getElementById('<%=txtLedgerGroupFilter.ClientID %>').value = "";
            document.getElementById('<%=txtLedgerSearch.ClientID %>').value = "";
            document.getElementById('<%=txtCostCentreFilter.ClientID %>').value = "";
            document.getElementById('<%=txtBankAccountFilter.ClientID %>').value = "";
            document.getElementById('<%=txtProjectCategorySearch.ClientID %>').value = "";           
        }

        function ChangeDate(s, e) {
            var dateFrom = dteDateFrom.GetDate();
            var DateTo = dateFrom;
            DateTo.setMonth(DateTo.getMonth() + 1);
            DateTo.setDate(DateTo.getDate() - 1);
            dteDateTo.SetDate(DateTo);
        }

        function SelectAllProject() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvProject').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvProject_ctl01_chkProjectSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvProject_ctl0' + ControlId + '_chkProjectSelect').checked = isChecked;

                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvProject_ctl' + ControlId + '_chkProjectSelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvProject.ClientID%> input[id*='chkProjectSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdProjectId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSProjectId.ClientID %>').value = RemoveUndefined(DataKeyName);
        }

        function SelectAllBank() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvBankAccount').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvBankAccount_ctl01_chkBankSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvBankAccount_ctl0' + ControlId + '_chkBankSelect').checked = isChecked;

                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvBankAccount_ctl' + ControlId + '_chkBankSelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvBankAccount.ClientID%> input[id*='chkBankSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdBankId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSBankId.ClientID %>').value = RemoveUndefined(DataKeyName);
        }

        function SelectAllLedger() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedger').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedger_ctl01_chkLedgerSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedger_ctl0' + ControlId + '_chkLedgerSelect').checked = isChecked;

                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedger_ctl' + ControlId + '_chkLedgerSelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvLedger.ClientID%> input[id*='chkLedgerSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdLedgerId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSLedgerId.ClientID %>').value = RemoveUndefined(DataKeyName);
        }

        function SelectAllLedgerGroup() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedgerGroup').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedgerGroup_ctl01_chkLedgerGroupSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedgerGroup_ctl0' + ControlId + '_chkLedgerGroupSelect').checked = isChecked;
                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedgerGroup_ctl' + ControlId + '_chkLedgerGroupSelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvLedgerGroup.ClientID%> input[id*='chkLedgerGroupSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdLedgerGroupId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value = RemoveUndefined(DataKeyName);
        }

        function SelectAllCostCentre() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabCostCentre_gvCostCentre').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabCostCentre_gvCostCentre_ctl01_chkCostCentreSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabCostCentre_gvCostCentre_ctl0' + ControlId + '_chkCostCentreSelect').checked = isChecked;
                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabCostCentre_gvCostCentre_ctl' + ControlId + '_chkCostCentreSelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvCostCentre.ClientID%> input[id*='chkCostCentreSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdCostCenterId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSCostCenterId.ClientID %>').value = RemoveUndefined(DataKeyName);
        }
        function SelectAllBranch() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabBranch_gvBranch').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabBranch_gvBranch_ctl01_chkBranchSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabBranch_gvBranch_ctl0' + ControlId + '_chkBranchSelect').checked = isChecked;

                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabBranch_gvBranch_ctl' + ControlId + '_chkBranchSelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvBranch.ClientID%> input[id*='chkBranchSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdBranchId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSBranchId.ClientID %>').value = RemoveUndefined(DataKeyName);
        }

        function SelectAllSociety() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabSociety_gvSociety').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabSociety_gvSociety_ctl01_chkSocietySelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabSociety_gvSociety_ctl0' + ControlId + '_chkSocietySelect').checked = isChecked;

                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabSociety_gvSociety_ctl' + ControlId + '_chkSocietySelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvSociety.ClientID%> input[id*='chkSocietySelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdSocietyId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSSocietyId.ClientID %>').value = RemoveUndefined(DataKeyName);
        }

        function GetSelectedBranchId() {
            var DataKeyName = "";
            $("#<%=gvBranch.ClientID%> input[id*='chkBranchSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdBranchId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSBranchId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedSocietyId() {
            var DataKeyName = "";
            $("#<%=gvSociety.ClientID%> input[id*='chkSocietySelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdSocietyId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSSocietyId.ClientID %>').value = DataKeyName;
        }
        function GetSelectedProjectId() {
            var DataKeyName = "";
            $("#<%=gvProject.ClientID%> input[id*='chkProjectSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdProjectId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSProjectId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedBankId() {
            var DataKeyName = "";
            $("#<%=gvBankAccount.ClientID%> input[id*='chkBankSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdBankId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSBankId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedLedgerGroupId() {
            var DataKeyName = "";
            $("#<%=gvLedgerGroup.ClientID%> input[id*='chkLedgerGroupSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdLedgerGroupId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedLedgerId() {
            var DataKeyName = "";
            $("#<%=gvLedger.ClientID%> input[id*='chkLedgerSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdLedgerId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSLedgerId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedCostCenterId() {
            var DataKeyName = "";
            $("#<%=gvCostCentre.ClientID%> input[id*='chkCostCentreSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    DataKeyName += $(this).next($('#hdCostCenterId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSCostCenterId.ClientID %>').value = DataKeyName;
        }

        function SelectSingleProjectCategory() {
            var total = 0;
            var type = typeValue + "@" + document.getElementById('<%=hdSelectedCategoryID.ClientID %>').value;
            for (var i = 0; i < type.length; i++) {
                if (type[i].checked) {
                    total = total + 1;
                }
                if (total > 1) {
                    alert("Please Select only three")
                    type[i].checked = false;
                    return false;
                }
            }
        }

        function GetSelectedProjectCategoryId() {
            var DataKeyName = "";
            $("#<%=gvProjectCategory.ClientID%> input[id*='chkProjectCategorySelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    SelectSingleProjectCategory();
                    DataKeyName += $(this).next($('#hdProjectCatogoryId')).val() + ",";
                }
            });
            document.getElementById('<%=hdSelectedCategoryID.ClientID %>').value = DataKeyName;
        }

        function RemoveUndefined(DatakeyName) {

            var ids = DatakeyName.split(',');
            var idValues = "";
            for (i = 1; i < ids.length; i++) {
                idValues += ids[i] + ",";
            }
            return idValues;
        }

        function InitiateCallBack(typeValue) {
            // gets the input from input box

            if (typeValue == "BR") {
                var type = typeValue + "@" + document.getElementById('<%=hdSBranchId.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Branch');
            }
            if (typeValue == "SY") {
                var type = typeValue + "@" + document.getElementById('<%=hdSSocietyId.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Society');
            }
            if (typeValue == "PJ") {
                var type = typeValue + "@" + document.getElementById('<%=hdSProjectId.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Project');
            }
            if (typeValue == "BK") {
                var type = typeValue + "@" + document.getElementById('<%=hdSBankId.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Bank');
            }
            if (typeValue == "LGG") {//Ledger Group
                var type = typeValue + "@" + document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'LedgerGroup');
            }
            if (typeValue == "LG") {
                var type = typeValue + "@" + document.getElementById('<%=hdSLedgerId.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Ledger');
            }
            if (typeValue == "CC") {
                var type = typeValue + "@" + document.getElementById('<%=hdSCostCenterId.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'CostCenter');
            }
            if (typeValue == "PC") {
                var type = typeValue + "@" + document.getElementById('<%=hdSelectedCategoryID.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'ProjectCategory');
            }
            if (typeValue == "BRF") {
                var type = typeValue + "@" + document.getElementById('<%=txtSearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Branch');
            }
            if (typeValue == "BRFR") {
                document.getElementById('<%=txtSearch.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtSearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Branch');
            }
            if (typeValue == "SYF") {
                var type = typeValue + "@" + document.getElementById('<%=txtSocietySearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Society');
            }
            if (typeValue == "SYFR") {
                document.getElementById('<%=txtSocietySearch.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtSocietySearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Society');
            }
            if (typeValue == "PJF") {
                var type = typeValue + "@" + document.getElementById('<%=txtProjectSearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Project');
            }
            if (typeValue == "PJFR") {
                document.getElementById('<%=txtProjectSearch.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtProjectSearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Project');
            }
            if (typeValue == "LGGF") {
                var type = typeValue + "@" + document.getElementById('<%=txtLedgerGroupFilter.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'LedgerGroup');
            }
            if (typeValue == "LGGFR") {
                document.getElementById('<%=txtLedgerGroupFilter.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtLedgerGroupFilter.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'LedgerGroup');
            }
            if (typeValue == "LGF") {
                var type = typeValue + "@" + document.getElementById('<%=txtLedgerSearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Ledger');
            }
            if (typeValue == "LGFR") {
                document.getElementById('<%=txtLedgerSearch.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtLedgerSearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'Ledger');
            }
            if (typeValue == "CCF") {
                var type = typeValue + "@" + document.getElementById('<%=txtCostCentreFilter.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'CostCentre');
            }
            if (typeValue == "CCFR") {
                document.getElementById('<%=txtCostCentreFilter.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtLedgerSearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'CostCentre');
            }
            if (typeValue == "BKF") {
                var type = typeValue + "@" + document.getElementById('<%=txtBankAccountFilter.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'BankAccount');
            }
            if (typeValue == "BKFR") {
                document.getElementById('<%=txtBankAccountFilter.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtBankAccountFilter.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'BankAccount');
            }
            if (typeValue == "PCF") {
                var type = typeValue + "@" + document.getElementById('<%=txtProjectCategorySearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'ProjectCategory');
            }
            if (typeValue == "PCFR") {
                document.getElementById('<%=txtProjectCategorySearch.ClientID %>').value = "";
                var type = typeValue + "@" + document.getElementById('<%=txtProjectCategorySearch.ClientID %>').value;
                //Initiate the callback
                CallServer(type, 'ProjectCategory');
            }
        }


        function ReceiveServerData(arg, context) {
            //arg: hold the result
            //Updating the UI
            if (context == "Branch") {
                var resCriteria = arg.split("@");
                for (i = 0; i < resCriteria.length; i++) {
                    if (resCriteria[i].toString() == "SY") {
                        document.getElementById('<%=gvSociety.ClientID %>').innerHTML = resCriteria[i + 1].toString();
                    }
                    if (resCriteria[i].toString() == "PJ") {
                        document.getElementById('<%=gvProject.ClientID %>').innerHTML = resCriteria[i + 1].toString();
                    }
                    if (resCriteria[i].toString() == "BRF") {
                        document.getElementById('<%=gvBranch.ClientID %>').innerHTML = resCriteria[i + 1].toString();
                    }
                }
            }
            if (context == "Society") {
                var societyCriteria = arg.split("@");
                if (societyCriteria[0].toString() == "PJ") {
                    document.getElementById('<%=gvProject.ClientID %>').innerHTML = societyCriteria[1].toString();
                }
                if (societyCriteria[0].toString() == "SYF") {
                    document.getElementById('<%=gvSociety.ClientID %>').innerHTML = societyCriteria[1].toString();
                }
            }
            if (context == "Project") {
                var projectCriteria = arg.split("@");
                if (projectCriteria.length > 0) {
                    if (projectCriteria[0].toString() == "BK") {
                        document.getElementById('<%=gvBankAccount.ClientID %>').innerHTML = projectCriteria[1].toString();
                    }
                }
                if (projectCriteria.length > 0) {
                    if (projectCriteria[0].toString() == "PJF") {
                        document.getElementById('<%=gvProject.ClientID %>').innerHTML = projectCriteria[1].toString();
                    }
                }
            }

            if (context == "LedgerGroup") {
                var ledgerCriteria = arg.split("@");
                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "LG") {
                        document.getElementById('<%=gvLedger.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "LGGF") {
                        document.getElementById('<%=gvLedgerGroup.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
            }

            if (context == "Ledger") {
                var ledgerCriteria = arg.split("@");
                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "LGF") {
                        document.getElementById('<%=gvLedger.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
            }

            if (context == "CostCentre") {
                var ledgerCriteria = arg.split("@");
                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "CCF") {
                        document.getElementById('<%=gvCostCentre.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
            }

            if (context = "BankAccount") {
                var ledgerCriteria = arg.split("@");
                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "BKF") {
                        document.getElementById('<%=gvBankAccount.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
            }

            if (context = "ProjectCategory") {
                var ledgerCriteria = arg.split("@");
                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "PCF") {
                        document.getElementById('<%=gvProjectCategory.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
            }
        }
    </script>
</asp:Content>
