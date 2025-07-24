<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ReportCriteriaPortal.aspx.cs" Inherits="AcMeERP.Report.ReportCriteriaPortal"
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
    <asp:HiddenField ID="hdLGFilterMode" runat="server" />
    <asp:HiddenField ID="hdSCostCenterId" runat="server" />
    <asp:HiddenField ID="hdSBudgetId" runat="server" />
    <asp:HiddenField ID="hdSelectedCategoryID" runat="server" />
    <asp:HiddenField ID="hdCongregationLedgerId" runat="server" />
    <asp:HiddenField ID="hdConLedgerFilterMode" runat="server" />
    <asp:Panel ID="pnlPortalReportCriteria" runat="server" DefaultButton="btnOk" meta:resourcekey="pnlPortalReportCriteriaResource1">
        <div style="width: 97%; margin: 10px; float: left;">
            <div id="divCriteria" runat="server" style="width: 100%;">
                <ajax:TabContainer ID="TabReportCriteria" runat="server" ActiveTabIndex="1" Height="300px"
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
                            <div style="width: 100%; padding-left: 16px; padding-top: 10px; text-align: center;">
                                <div style="width: 50%; padding-left: 23px; float: left">
                                    <div style="float: left; width: 27%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        &nbsp;
                                    </div>
                                </div>
                                <div style="width: 36%; float: right">
                                    <div style="float: left; width: 29%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; padding-left: 16px; padding-top: 10px; text-align: center;">
                                <div style="width: 50%; padding-left: 23px; float: left">
                                    <div style="float: left; width: 27%">
                                        <dx:ASPxLabel ID="lblVerificationTitle" runat="server" Text="Show Detail Verification By"
                                            Style="float: left;">
                                        </dx:ASPxLabel>
                                    </div>
                                    <div style="float: left;">
                                        <dx:ASPxRadioButton ID="OptVerficationNone" runat="server" Text="None" GroupName="VerificationBy">
                                        </dx:ASPxRadioButton>
                                    </div>
                                </div>
                                <div style="width: 36%; float: right">
                                    <div style="float: left; width: 29%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; padding-left: 16px; padding-top: 10px; text-align: center;">
                                <div style="width: 50%; padding-left: 23px; float: left">
                                    <div style="float: left; width: 27%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        <dx:ASPxRadioButton ID="OptShowDetailInterAccount" runat="server" Text="Show Inter Account Detail"
                                            GroupName="VerificationBy">
                                        </dx:ASPxRadioButton>
                                    </div>
                                </div>
                                <div style="width: 36%; float: right">
                                    <div style="float: left; width: 29%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%; padding-left: 16px; padding-top: 10px; text-align: center;">
                                <div style="width: 50%; padding-left: 23px; float: left">
                                    <div style="float: left; width: 27%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        <dx:ASPxRadioButton ID="OptShowDetailProvinceFromTo" runat="server" Text="Show Province Contribution From/To Detail"
                                            GroupName="VerificationBy">
                                        </dx:ASPxRadioButton>
                                    </div>
                                </div>
                                <div style="width: 36%; float: right">
                                    <div style="float: left; width: 29%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100; padding-left: 16px; padding-top: 10; text-align: left">
                                <div style="width: 50%; padding-left: 23px; float: left">
                                    <div style="float: left; width: 27%">
                                        &nbsp
                                    </div>
                                    <div style="float: left">
                                        <dx:ASPxCheckBox ID="chkFilterCashOnly" runat="server" Text="Show All Cash Vouchers Above 10,000" 
                                            CheckState="Unchecked">
                                        </dx:ASPxCheckBox>
                                    </div>
                                </div>
                                <div style="width: 36%; float: right">
                                    <div style="float: left; width: 29%">
                                        &nbsp;
                                    </div>
                                    <div style="float: left;">
                                        &nbsp;
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
                                                                <asp:Literal ID="ltSearch" runat="server" Text="Filter" meta:resourcekey="ltSearchResource1"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtSearch" runat="server" EnableTheming="False" Width="300px" CssClass="textbox"
                                                                    TabIndex="1" meta:resourcekey="txtSearchResource1"></asp:TextBox>
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
                                                                <asp:Label ID="lblBranchRecordCount" runat="server" meta:resourcekey="lblBranchRecordCountResource1"></asp:Label>
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
                                                            <asp:Literal ID="Literal1" runat="server" Text="Filter" meta:resourcekey="Literal1Resource1"></asp:Literal>
                                                        </div>
                                                        <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                            <asp:TextBox ID="txtSocietySearch" runat="server" EnableTheming="False" Width="300px"
                                                                CssClass="textbox" meta:resourcekey="txtSocietySearchResource1"></asp:TextBox>
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
                                                            <asp:Literal ID="Literal2" runat="server" meta:resourcekey="Literal2Resource1"></asp:Literal>
                                                        </div>
                                                        <div class="clearfloat">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="div100" id="divSociety">
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
                                                                <asp:Literal ID="Literal11" runat="server" Text="Filter" meta:resourcekey="Literal11Resource1"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtProjectCategorySearch" runat="server" EnableTheming="False" Width="300px"
                                                                    CssClass="textbox" meta:resourcekey="txtProjectCategorySearchResource1"></asp:TextBox>
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
                                                                <asp:Literal ID="Literal12" runat="server" meta:resourcekey="Literal12Resource1"></asp:Literal>
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
                                                        <asp:BoundField HeaderText="Category" DataField="PROJECT_CATOGORY_NAME" ReadOnly="True"
                                                            meta:resourcekey="BoundFieldResource8" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
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
                                                                <asp:Literal ID="Literal3" runat="server" Text="Filter" meta:resourcekey="Literal3Resource1"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtProjectSearch" runat="server" EnableTheming="False" Width="150px"
                                                                    TabIndex="1" CssClass="textbox" meta:resourcekey="txtProjectSearchResource1"></asp:TextBox>
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
                                                                <asp:Literal ID="Literal4" runat="server" meta:resourcekey="Literal4Resource1"></asp:Literal>
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
                                            <div style="padding-top: 2px; width: 100%; float: left;" id="divBankFilter" runat="server"
                                                visible="False">
                                                <div id="divBankAccountFilter" runat="server" style="width: 100%; float: left">
                                                    <div style="width: 100%; float: left">
                                                        <div class="divrow toolbar">
                                                            <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <asp:Literal ID="Literal13" runat="server" Text="Filter" meta:resourcekey="Literal13Resource1"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtBankAccountFilter" runat="server" EnableTheming="False" Width="150px"
                                                                    TabIndex="1" CssClass="textbox" meta:resourcekey="txtBankAccountFilterResource1"></asp:TextBox>
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
                                                                <asp:Literal ID="Literal14" runat="server" meta:resourcekey="Literal14Resource1"></asp:Literal>
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
                                                                <asp:Literal ID="Literal5" runat="server" Text="Filter" meta:resourcekey="Literal5Resource1"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtLedgerGroupFilter" runat="server" EnableTheming="False" Width="150px"
                                                                    TabIndex="1" CssClass="textbox" meta:resourcekey="txtLedgerGroupFilterResource1"></asp:TextBox>
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
                                                                <asp:Literal ID="Literal6" runat="server" meta:resourcekey="Literal6Resource1"></asp:Literal>
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
                                                                <asp:Literal ID="Literal7" runat="server" Text="Filter" meta:resourcekey="Literal7Resource1"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtLedgerSearch" runat="server" EnableTheming="False" Width="150px"
                                                                    TabIndex="5" CssClass="textbox" meta:resourcekey="txtLedgerSearchResource1"></asp:TextBox>
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
                                                                <asp:Literal ID="Literal8" runat="server" meta:resourcekey="Literal8Resource1"></asp:Literal>
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
                                                                    AutoPostBack="true" OnCheckedChanged="chkLedgerSelect_CheckedChanged" TabIndex="8"
                                                                    onclick="javascript:GetSelectedLedgerId();InitiateCallBack('LG'); return true;"
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
                                    <asp:UpdatePanel ID="upCongregationLedger" runat="server">
                                        <ContentTemplate>
                                            <div style="padding-top: 2px; width: 100%; float: left;">
                                                <div id="divConLedgerFilter" runat="server" style="width: 100%; float: left">
                                                    <div style="width: 100%; float: left">
                                                        <div class="divrow toolbar">
                                                            <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <asp:Literal ID="Literal17" runat="server" Text="Filter" meta:resourcekey="Literal5Resource1"></asp:Literal>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                                <asp:TextBox ID="txtConLedgerFilter" runat="server" EnableTheming="False" Width="150px"
                                                                    TabIndex="1" CssClass="textbox" meta:resourcekey="txtConLedgerFilterResource"></asp:TextBox>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                                <a id="lbtConLedgerFilter" onclick="javascript:InitiateCallBack('CLF');" tabindex="2">
                                                                    <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                            </div>
                                                            <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <a id="lbtConLedgerRefresh" onclick="javascript:InitiateCallBack('CLFR');" tabindex="3">
                                                                    <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                            </div>
                                                            <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                                <asp:Literal ID="Literal18" runat="server" meta:resourcekey="Literal6Resource1"></asp:Literal>
                                                            </div>
                                                            <div class="clearfloat">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="div100">
                                                <asp:GridView ID="gvCongregationLedger" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" DataKeyNames="CON_LEDGER_ID" SkinID="Rpt_Criteria" meta:resourcekey="gvCongregationLedgerResource">
                                                    <Columns>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkConLedgerSelectAll" ToolTip="Click To Select All" runat="server"
                                                                    onclick="javascript:SelectAllCongregationLedgers();InitiateCallBack('CL');" meta:resourcekey="chkConLedgerSelectAllResource" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkConLedgerSelect" runat="server" ValidationGroup='<%# Eval("CON_LEDGER_ID") %>'
                                                                    TabIndex="4" Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                                                    onclick="javascript:GetSelectedCongregationLedgerId();InitiateCallBack('CL');"
                                                                    meta:resourcekey="chkConLedgerResource" />
                                                                <asp:HiddenField ID="hdConLedgerId" runat="server" Value='<%# Eval("CON_LEDGER_ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Congregation Group" DataField="CON_LEDGER_NAME" ReadOnly="True"
                                                            meta:resourcekey="BoundFieldResource5" />
                                                        <asp:BoundField HeaderText="Congregation Parent Group" DataField="CON_LEDGER_GROUP"
                                                            ReadOnly="True" meta:resourcekey="BoundFieldResource5" />
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
                        <HeaderTemplate>
                            Cost Centre
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="width: 100%; overflow-y: auto; overflow-x: hidden; height: 306px;">
                                <asp:UpdatePanel ID="upCostCenter" runat="server">
                                    <ContentTemplate>
                                        <div style="padding-top: 2px; width: 100%; float: left;">
                                            <div id="divCostCentreFilter" runat="server" style="width: 100%; float: left">
                                                <div style="width: 100%; float: left">
                                                    <div class="divrow toolbar">
                                                        <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                            <asp:Literal ID="Literal9" runat="server" Text="Filter" meta:resourcekey="Literal9Resource1"></asp:Literal>
                                                        </div>
                                                        <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                            <asp:TextBox ID="txtCostCentreFilter" runat="server" EnableTheming="False" Width="300px"
                                                                TabIndex="1" CssClass="textbox" meta:resourcekey="txtCostCentreFilterResource1"></asp:TextBox>
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
                                                            <asp:Literal ID="Literal10" runat="server" meta:resourcekey="Literal10Resource1"></asp:Literal>
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
                    <ajax:TabPanel ID="TabBudget" runat="server" HeaderText="Budget">
                        <ContentTemplate>
                            <div style="width: 100%; overflow-y: auto; overflow-x: hidden; height: 306px;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div style="padding-top: 2px; width: 100%; float: left;">
                                            <div id="div3" runat="server" style="width: 100%; float: left">
                                                <div style="width: 100%; float: left">
                                                    <div class="divrow toolbar">
                                                        <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                            <asp:Literal ID="Literal15" runat="server" Text="Filter" meta:resourcekey="Literal9Resource1"></asp:Literal>
                                                        </div>
                                                        <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                                                            <asp:TextBox ID="txtBudgetSearch" runat="server" EnableTheming="False" Width="300px"
                                                                TabIndex="1" CssClass="textbox" meta:resourcekey="txtBudgetSearchResource1"></asp:TextBox>
                                                        </div>
                                                        <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                                                            <a id="A1" onclick="javascript:InitiateCallBack('BU');" tabindex="2">
                                                                <img src="../App_Themes/MainTheme/images/ok.jpg" title="Search" alt="ok"></image></a>
                                                        </div>
                                                        <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                            <a id="A2" onclick="javascript:InitiateCallBack('BU');" tabindex="3">
                                                                <img src="../App_Themes/MainTheme/images/refresh.jpg" title="Refresh" alt="ok"></image></a>
                                                        </div>
                                                        <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                                                            <asp:Literal ID="Literal16" runat="server" meta:resourcekey="Literal10Resource1"></asp:Literal>
                                                        </div>
                                                        <div class="clearfloat">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="div100">
                                            <asp:GridView ID="gvBudget" runat="server" AutoGenerateColumns="False" SkinID="Rpt_Criteria"
                                                DataKeyNames="BUDGET_ID" meta:resourcekey="gvBudgetResource1">
                                                <Columns>
                                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkBudgetSelectAll" runat="server" onclick="javascript:SelectAllBudget();InitiateCallBack('BU');"
                                                                meta:resourcekey="chkBudgetSelectAllResource1" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkBudgetSelect" runat="server" ValidationGroup='<%# Eval("BUDGET_ID") %>'
                                                                TabIndex="4" onclick="javascript:GetSelectedBudgetId();InitiateCallBack('BU');"
                                                                Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>' meta:resourcekey="chkBudgetSelectResource1" />
                                                            <asp:HiddenField ID="hdBudgetId" runat="server" Value='<%# Eval("BUDGET_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Budget" DataField="BUDGET_NAME" ReadOnly="True" meta:resourcekey="BoundFieldResource7" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel ID="TabConLedger" runat="server" HeaderText="Congregation Ledger"
                        Visible="False" meta:resourcekey="TabConLedgerResource">
                        <ContentTemplate>
                            <div style="width: 100%;">
                                <div style="width: 98%; height: 307px; float: left; overflow-y: auto; overflow-x: hidden;">
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
                                        <div style="width: 100%; float: left; padding-top: 10px;">
                                            <div style="width: 50%; float: left; padding-top: 10px;">
                                                <asp:Label ID="Label1" runat="server" Text="Show All Project Name as " Style="float: left;"
                                                    Visible="False"></asp:Label>
                                            </div>
                                            <div style="float: left; margin-left: -100px; padding-top: 8px;">
                                                <asp:DropDownList ID="ddlConsolidate" runat="server" Visible="False">
                                                    <asp:ListItem Value="1" Selected="True" Text="Consolidated"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Show Individual Project"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="float: left; width: 49%; padding-top: 10px;">
                                    <div style="float: left; width: 100%;">
                                        <div style="width: 50%; float: left;">
                                            <asp:CheckBox ID="chkShowreportDate" Text="Report Date  " Style="float: left;" runat="server" />
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
                                        <div style="width: 50%; float: left;">
                                            <asp:Label ID="lblCode" runat="server" Text="Code" Style="float: left;" meta:resourcekey="lblTitleAlignmentResource1"
                                                Width="88px" Visible="False"></asp:Label>
                                        </div>
                                        <div style="float: left;">
                                            <asp:DropDownList ID="ddlCode" runat="server" meta:resourceKey="ddlCodeSelections"
                                                Visible="False">
                                                <asp:ListItem meta:resourceKey="ListItemResource8" Selected="True" Text="Province"
                                                    Value="1"></asp:ListItem>
                                                <asp:ListItem meta:resourceKey="ListItemResource9" Text="Generalate" Value="2"></asp:ListItem>
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
                                        <div style="width: 100%; float: left; padding-top: 10px;">
                                            <asp:CheckBox ID="chkShowLedgerCode" runat="server" Text="Show Ledger Code" Checked="True"
                                                Visible="False" />
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
                    Width="55px" meta:resourcekey="btnOkResource1" ClientInstanceName="btnOk" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" OnClick="btnCancel_OnClick"
                    meta:resourcekey="btnCancelResource1" />
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript" language="javascript">

        var buttonclass = "button";

        function HideDisplayPopUp() {
            document.getElementById('<%=txtSearch.ClientID %>').value = "";
            document.getElementById('<%=txtSocietySearch.ClientID %>').value = "";
            document.getElementById('<%=txtProjectSearch.ClientID %>').value = "";
            document.getElementById('<%=txtBudgetSearch.ClientID %>').value = "";
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
                    if ($(this).next($('#hdProjectId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdProjectId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSProjectId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSProjectId.ClientID %>').value = DataKeyName;
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
                    if ($(this).next($('#hdBankId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdBankId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSBankId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSBankId.ClientID %>').value = DataKeyName;
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
                    if ($(this).next($('#hdLedgerId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdLedgerId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSLedgerId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSLedgerId.ClientID %>').value = DataKeyName;
        }

        function SelectAllLedgerGroup() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedgerGroup').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedgerGroup_ctl01_chkLedgerGroupSelectAll').checked;
            //alert(RowCount);
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
            /*
            $("#<%=gvLedgerGroup.ClientID%> input[id*='chkLedgerGroupSelect']:checkbox").each(function (index) {
            if ($(this).is(':checked')) {
            if ($(this).next($('#hdLedgerGroupId')).val() != undefined) {
            DataKeyName += $(this).next($('#hdLedgerGroupId')).val() + ",";
            }
            }
            });*/

            DataKeyName = GetSelectedLedgerGroupId();
            //alert(DataKeyName);
            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value = RemoveUndefined(DataKeyName);
            //document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value = DataKeyName;
            //alert(document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value);
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
                    if ($(this).next($('#hdCostCenterId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdCostCenterId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSCostCenterId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSCostCenterId.ClientID %>').value = DataKeyName;
        }
        function SelectAllBudget() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabBudget_gvBudget').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabBudget_gvBudget_ctl01_chkBudgetSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabBudget_gvBudget_ctl0' + ControlId + '_chkBudgetSelect').checked = isChecked;
                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabBudget_gvBudget_ctl' + ControlId + '_chkBudgetSelect').checked = isChecked;
                }
            }
            var DataKeyName = "";
            $("#<%=gvBudget.ClientID%> input[id*='chkBudgetSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdBudgetId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdBudgetId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSBudgetId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSBudgetId.ClientID %>').value = DataKeyName;
        }

        function SelectAllCongregationLedgers() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvCongregationLedger').rows.length;
            var isChecked = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvCongregationLedger_ctl01_chkConLedgerSelectAll').checked;
            for (var i = 1; i < RowCount; i++) {
                ControlId = i + 1;
                if (ControlId < 10) {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvCongregationLedger_ctl0' + ControlId + '_chkConLedgerSelect').checked = isChecked;
                }
                else {
                    document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvCongregationLedger_ctl' + ControlId + '_chkConLedgerSelect').checked = isChecked;
                }
            }

            var DataKeyName = "";
            /*
            $("#<%=gvCongregationLedger.ClientID%> input[id*='chkConLedgerSelect']:checkbox").each(function (index) {
            if ($(this).is(':checked')) {
            if ($(this).next($('#hdCongregationLedgerId')).val() != undefined) {
            DataKeyName += $(this).next($('#hdCongregationLedgerId')).val() + ",";
            }
            }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSBudgetId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdCongregationLedgerId.ClientID %>').value = DataKeyName;
            document.getElementById('<%=hdCongregationLedgerId.ClientID %>').value = GetSelectedCongregationLedgerId();
            */

            DataKeyName = GetSelectedCongregationLedgerId();

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
                    if ($(this).next($('#hdBranchId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdBranchId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSBranchId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSBranchId.ClientID %>').value = DataKeyName;
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
                    if ($(this).next($('#hdSocietyId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdSocietyId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSSocietyId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSSocietyId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedBranchId() {

            var DataKeyName = "";
            $("#<%=gvBranch.ClientID%> input[id*='chkBranchSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdBranchId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdBranchId')).val() + ",";
                    }
                }
            });


            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSBranchId.ClientID %>').value =  RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSBranchId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedSocietyId() {
            var DataKeyName = "";
            $("#<%=gvSociety.ClientID%> input[id*='chkSocietySelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdSocietyId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdSocietyId')).val() + ",";
                    }
                }
            });

            //On 11/11/2019, removed undefined item in selection loop above
            //document.getElementById('<%=hdSSocietyId.ClientID %>').value = RemoveUndefined(DataKeyName);
            document.getElementById('<%=hdSSocietyId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedProjectId() {
            var DataKeyName = "";
            $("#<%=gvProject.ClientID%> input[id*='chkProjectSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdProjectId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdProjectId')).val() + ",";
                    }
                }
            });

            document.getElementById('<%=hdSProjectId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedBankId() {
            var DataKeyName = "";
            $("#<%=gvBankAccount.ClientID%> input[id*='chkBankSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdBankId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdBankId')).val() + ",";
                    }
                }
            });
            document.getElementById('<%=hdSBankId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedLedgerGroupId() {
            var DataKeyName = "";
            var LGFilterMode = false;
            var alreadyselectedvalue = "";


            //alert(document.getElementById('<%=hdLGFilterMode.ClientID %>').value);
            if (document.getElementById('<%=hdLGFilterMode.ClientID %>').value != undefined) {
                //alert(document.getElementById('<%=hdLGFilterMode.ClientID %>').value);
                LGFilterMode = (document.getElementById('<%=hdLGFilterMode.ClientID %>').value == 'Y');
                if (LGFilterMode == true) {
                    alreadyselectedvalue = document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value;
                    DataKeyName = alreadyselectedvalue;
                    //alert(DataKeyName);
                }
            }

            //alert(LGFilterMode);

            $("#<%=gvLedgerGroup.ClientID%> input[id*='chkLedgerGroupSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdLedgerGroupId')).val() != undefined) {
                        var selvalue = $(this).next($('#hdLedgerGroupId')).val();
                        if (LGFilterMode == true) {
                            var items = alreadyselectedvalue.split(/\s*,\s*/);
                            var isContained = items.some(function (v) { return v === '+ selvalue +'; });
                            if (isContained == false) {
                                DataKeyName += selvalue + ",";
                            }
                        }
                        else {
                            DataKeyName += selvalue + ",";
                        }
                    }
                }
            });
            //alert(DataKeyName);
            document.getElementById('<%=hdSLedgerGroupId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedLedgerId() {
            var DataKeyName = "";
            $("#<%=gvLedger.ClientID%> input[id*='chkLedgerSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdLedgerId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdLedgerId')).val() + ",";
                    }
                }
            });
            document.getElementById('<%=hdSLedgerId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedCostCenterId() {
            var DataKeyName = "";
            $("#<%=gvCostCentre.ClientID%> input[id*='chkCostCentreSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdCostCenterId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdCostCenterId')).val() + ",";
                    }
                }
            });
            document.getElementById('<%=hdSCostCenterId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedBudgetId() {
            var DataKeyName = "";
            $("#<%=gvBudget.ClientID%> input[id*='chkBudgetSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdBudgetId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdBudgetId')).val() + ",";
                    }
                }
            });

            document.getElementById('<%=hdSBudgetId.ClientID %>').value = DataKeyName;
        }

        function GetSelectedCongregationLedgerId() {
            var DataKeyName = "";
            var ConLedgerFilterMode = false;
            var alreadyselectedvalue = "";

            if (document.getElementById('<%=hdConLedgerFilterMode.ClientID %>').value != undefined) {
                //alert(document.getElementById('<%=hdConLedgerFilterMode.ClientID %>').value);
                ConLedgerFilterMode = (document.getElementById('<%=hdConLedgerFilterMode.ClientID %>').value == 'Y');
                if (ConLedgerFilterMode == true) {
                    alreadyselectedvalue = document.getElementById('<%=hdCongregationLedgerId.ClientID %>').value;
                    DataKeyName = alreadyselectedvalue;
                    //alert(DataKeyName);
                }
            }



            $("#<%=gvCongregationLedger.ClientID%> input[id*='chkConLedgerSelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdCongregationLedgerId')).val() != undefined) {
                        var selvalue = $(this).next($('#hdCongregationLedgerId')).val();
                        if (ConLedgerFilterMode == true) {
                            var items = alreadyselectedvalue.split(/\s*,\s*/);
                            var isContained = items.some(function (v) { return v === '+ selvalue +'; });
                            if (isContained == false) {
                                DataKeyName += selvalue + ",";
                            }
                        }
                        else {
                            DataKeyName += selvalue + ",";
                        }
                        //DataKeyName += $(this).next($('#hdCongregationLedgerId')).val() + ",";
                    }
                }
            });
            //alert(DataKeyName);
            document.getElementById('<%=hdCongregationLedgerId.ClientID %>').value = DataKeyName;
        }

        /*function GetSelectedCongregationLedgerId() {
        var DataKeyName = "";
        $("#<%=gvCongregationLedger.ClientID%> input[id*='chkConLedgerSelect']:checkbox").each(function (index) {
        if ($(this).is(':checked')) {
        if ($(this).next($('#hdCongregationLedgerId')).val() != undefined) {
        DataKeyName += $(this).next($('#hdCongregationLedgerId')).val() + ",";
        }
        }
        });
        //alert(DataKeyName);
        document.getElementById('<%=hdCongregationLedgerId.ClientID %>').value = DataKeyName;
        }*/

        function SelectSingleProjectCategory() {
            var total = 0;
            var type = document.getElementById('<%=hdSelectedCategoryID.ClientID %>').value.split(',');
            for (var i = 0; i < type.length; i++) {
                if (type[i] > 0) {
                    total = total + 1;
                }
                if (total > 1) {
                    alert("Please Select only three")
                    //type[i].checked = false;
                    return false;
                }
            }
        }

        function GetSelectedProjectCategoryId() {
            var DataKeyName = "";
            $("#<%=gvProjectCategory.ClientID%> input[id*='chkProjectCategorySelect']:checkbox").each(function (index) {
                if ($(this).is(':checked')) {
                    if ($(this).next($('#hdProjectCatogoryId')).val() != undefined) {
                        DataKeyName += $(this).next($('#hdProjectCatogoryId')).val() + ",";
                    }
                }
            });
            document.getElementById('<%=hdSelectedCategoryID.ClientID %>').value = DataKeyName;

        }

        //        function RemoveUndefined(DatakeyName) {
        //            var ids = DatakeyName.split(',');
        //            var idValues = "";
        //            for (i = 1; i < ids.length; i++) {
        //                idValues += ids[i] + ",";
        //            }
        //            return idValues;
        //        }

        function InitiateCallBack(typeValue) {
            // gets the input from input box

            //Disable Ok button before callback function is called
            var btn = document.getElementById('<%= btnOk.ClientID %>');
            var grdSociety = document.getElementById('<%=gvSociety.ClientID %>');
            var grdProject = document.getElementById('<%=gvProject.ClientID %>');
            var chkSelectAllSociety = document.getElementById('ctl00_cpMain_TabReportCriteria_TabSociety_gvSociety_ctl01_chkSocietySelectAll');
            var chkSelectAllProject = document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvProject_ctl01_chkProjectSelectAll');

            if (grdProject != null && chkSelectAllProject != null) {

                btn.disabled = grdSociety.disabled = grdProject.disabled = chkSelectAllSociety.disabled = chkSelectAllProject.disabled = true;
            }
            else {
                if (grdSociety != null) grdSociety.disabled = true;
                if (chkSelectAllSociety != null) chkSelectAllSociety.disabled = true;
                if (btn != null) btn.disabled = true;
            }
            buttonclass = btn.className;
            btn.className = "";

            if (typeValue == "BR") {
                var type = typeValue + "@" + document.getElementById('<%=hdSBranchId.ClientID %>').value;
                //alert(buttonclass);
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
            if (typeValue == "BU") {
                //document.getElementById('<%=txtBudgetSearch.ClientID %>').value = "";
                //var type = typeValue + "@" + document.getElementById('<%=txtBudgetSearch.ClientID %>').value;
                //document.getElementById('<%=hdSBudgetId.ClientID %>').value = type;
                var type = typeValue + "@" + document.getElementById('<%=hdSBudgetId.ClientID %>').value;
                CallServer(type, 'Budget');
            }

            if (typeValue == "CL") {

                var type = typeValue + "@" + document.getElementById('<%=hdCongregationLedgerId.ClientID %>').value;
                CallServer(type, 'ConLedger');
            }
            if (typeValue == "CLF" || typeValue == "CLFR") {

                document.getElementById('<%=hdConLedgerFilterMode.ClientID %>').value = "";
                if (typeValue == "CLFR") {
                    document.getElementById('<%=txtConLedgerFilter.ClientID %>').value = "";
                }
                else if (typeValue == "CLF" && document.getElementById('<%=txtConLedgerFilter.ClientID %>').value.trim() != "") {
                    document.getElementById('<%=hdConLedgerFilterMode.ClientID %>').value = "Y";
                }
                var type = typeValue + "@" + document.getElementById('<%=txtConLedgerFilter.ClientID %>').value;
                //alert(typeValue);
                //Initiate the callback
                CallServer(type, 'ConLedger');
            }
            if (typeValue == "LGGF") {
                var type = typeValue + "@" + document.getElementById('<%=txtLedgerGroupFilter.ClientID %>').value;
                document.getElementById('<%=hdLGFilterMode.ClientID %>').value = "";
                if (document.getElementById('<%=txtLedgerGroupFilter.ClientID %>').value.trim() != "") {
                    document.getElementById('<%=hdLGFilterMode.ClientID %>').value = "Y";
                }

                //Initiate the callback
                CallServer(type, 'LedgerGroup');
            }
            if (typeValue == "LGGFR") {
                document.getElementById('<%=txtLedgerGroupFilter.ClientID %>').value = "";
                document.getElementById('<%=hdLGFilterMode.ClientID %>').value = "";
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
                //alert(arg);
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
                if (projectCriteria.length > 0) {
                    if (projectCriteria[0].toString() == "BU") {
                        document.getElementById('<%=gvBudget.ClientID %>').innerHTML = projectCriteria[1].toString();
                    }
                }

            }

            if (context == "LedgerGroup") {
                var ledgerCriteria = arg.split("@");

                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "LG" && document.getElementById('<%=gvLedger.ClientID %>') != null) {
                        document.getElementById('<%=gvLedger.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                    else if (ledgerCriteria[0].toString() == "CL" && document.getElementById('<%=gvCongregationLedger.ClientID %>') != null) {
                        document.getElementById('<%=gvCongregationLedger.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
                //alert(ledgerCriteria);
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

            if (context == "ConLedger") {
                var ledgerCriteria = arg.split("@");
                if (ledgerCriteria.length > 0) {
                    if (ledgerCriteria[0].toString() == "CL") {
                        document.getElementById('<%=gvCongregationLedger.ClientID %>').innerHTML = ledgerCriteria[1].toString();
                    }
                }
            }

            if (context == "CostCentre") {
                var CcledgerCriteria = arg.split("@");
                if (CcledgerCriteria.length > 0) {
                    if (CcledgerCriteria[0].toString() == "CCF") {
                        document.getElementById('<%=gvCostCentre.ClientID %>').innerHTML = CcledgerCriteria[1].toString();
                    }
                }
            }

            if (context = "BankAccount") {
                var BankAccountCriteria = arg.split("@");
                if (BankAccountCriteria.length > 0) {
                    if (BankAccountCriteria[0].toString() == "BKF") {
                        document.getElementById('<%=gvBankAccount.ClientID %>').innerHTML = BankAccountCriteria[1].toString();
                    }
                }
            }

            if (context = "ProjectCategory") {
                var ProjectCategoryCriteria = arg.split("@");
                if (ProjectCategoryCriteria[0].toString() == "PJ") {
                    document.getElementById('<%=gvProject.ClientID %>').innerHTML = ProjectCategoryCriteria[1].toString();
                }
                if (ProjectCategoryCriteria.length > 0) {
                    if (ProjectCategoryCriteria[0].toString() == "PCF") {
                        document.getElementById('<%=gvProjectCategory.ClientID %>').innerHTML = ProjectCategoryCriteria[1].toString();
                    }
                }
            }

            //Enable Ok button after callback function is completed
            var btn = document.getElementById('<%= btnOk.ClientID %>');
            var grdSociety = document.getElementById('<%=gvSociety.ClientID %>');
            var grdProject = document.getElementById('<%=gvProject.ClientID %>');
            var chkSelectAllSociety = document.getElementById('ctl00_cpMain_TabReportCriteria_TabSociety_gvSociety_ctl01_chkSocietySelectAll');
            var chkSelectAllProject = document.getElementById('ctl00_cpMain_TabReportCriteria_TabProject_gvProject_ctl01_chkProjectSelectAll');

            if (grdProject != null && chkSelectAllProject != null) {
                btn.disabled = grdSociety.disabled = grdProject.disabled = chkSelectAllSociety.disabled = chkSelectAllProject.disabled = false;
            }
            else {
                if (btn != null) btn.disabled = false;
                if (grdSociety != null) grdSociety.disabled = false;
                if (chkSelectAllSociety != null) chkSelectAllSociety.disabled = false;
            }
            //alert(buttonclass);
            btn.className = buttonclass;

        }
    </script>
</asp:Content>
