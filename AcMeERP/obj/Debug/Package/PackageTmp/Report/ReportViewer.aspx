<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="AcMeERP.Report.ReportViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
        namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="div100" style="z-index: 111 !important; top: 0; left: 0;">
        <dx:ASPxDocumentViewer ID="dvReportViewer" runat="server" ReportTypeName="Bosco.Report.ReportObject"
            OnCacheReportDocument="dvReportViewer_CacheReportDocument" OnRestoreReportDocumentFromCache="dvReportViewer_RestoreReportDocumentFromCache"
            OnUnload="dvReportViewer_Unload" ClientInstanceName="dvReportViewer" Theme="Office2010Blue">
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
                <dx:ReportToolbarButton ItemKind="PrintReport" meta:resourcekey="ReportToolbarButtonResource3" />
                <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" meta:resourcekey="ReportToolbarButtonResource9" />
                <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" meta:resourcekey="ReportToolbarButtonResource10" />
                <dx:ReportToolbarLabel ItemKind="PageLabel" meta:resourcekey="ReportToolbarLabelResource3" />
                <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px" meta:resourcekey="ReportToolbarComboBoxResource3">
                </dx:ReportToolbarComboBox>
                <dx:ReportToolbarLabel ItemKind="OfLabel" meta:resourcekey="ReportToolbarLabelResource4" />
                <dx:ReportToolbarTextBox ItemKind="PageCount" meta:resourcekey="ReportToolbarTextBoxResource2" />
                <dx:ReportToolbarButton ItemKind="NextPage" meta:resourcekey="ReportToolbarButtonResource11" />
                <dx:ReportToolbarButton ItemKind="LastPage" meta:resourcekey="ReportToolbarButtonResource12" />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton ItemKind="SaveToDisk" meta:resourcekey="ReportToolbarButtonResource13" />
                <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px" meta:resourcekey="ReportToolbarComboBoxResource4">
                    <Elements>
                        <dx:ListElement Value="pdf" meta:resourcekey="ListElementResource10" />
                        <dx:ListElement Value="xls" meta:resourcekey="ListElementResource11" />
                        <dx:ListElement Value="xlsx" meta:resourcekey="ListElementResource12" />
                        <dx:ListElement Value="rtf" meta:resourcekey="ListElementResource13" />
                        <dx:ListElement Value="mht" meta:resourcekey="ListElementResource14" />
                        <dx:ListElement Value="html" meta:resourcekey="ListElementResource15" />
                        <dx:ListElement Value="txt" meta:resourcekey="ListElementResource16" />
                        <dx:ListElement Value="csv" meta:resourcekey="ListElementResource17" />
                        <dx:ListElement Value="png" meta:resourcekey="ListElementResource18" />
                    </Elements>
                </dx:ReportToolbarComboBox>
            </ToolbarItems>
            <ClientSideEvents ToolbarItemClick="function(s, e) {
	if(e.item.name == &quot;Customization&quot;)
    {
    var reportId='/ReportCriteriaPortal.aspx?'+window.location.href.split('?')[1];
    var url=window.location.href.substring(0,window.location.href.lastIndexOf('/'))+reportId
    window.location.href=url;
    }
    if(e.item.name==&quot;DrillDown&quot;)
    {
         window.location.href=window.location.href.split('&')[0]+'&hdva=true&DrillBack=true';
    }
   }" />
        </dx:ASPxDocumentViewer>
    </div>
</asp:Content>
