<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="HomeLogin.aspx.cs" Inherits="AcMeERP.HomeLogin"
    Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Src="~/WebControl/ucAccountBalance.ascx" TagName="Balance" TagPrefix="UI" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
    <asp:UpdatePanel ID="upNonConformity" runat="server">
        <ContentTemplate>
            <div id="ModalOverlay" class="modal_popup_overlay">
            </div>
            <asp:Panel ID="pnlNonConformity" runat="server">
                <div id="Display" class="modal_popup_logo" style="width: 80% !important; left: 10% !important;
                    top: 10% !important;">
                    <div class="div100 modal_popup_title">
                        <div runat="server" id="imagePopupTitle" style="float: left; padding: 5px;">
                            <asp:Label runat="server" ID="lblPopTitle" Text="Non Conforming Branches "></asp:Label>
                        </div>
                        <div class="floatright">
                            <asp:ImageButton runat="server" ImageAlign="AbsMiddle" OnClientClick="javascript:HideDisplayPopUp();"
                                CssClass="handcursor" ImageUrl="~/App_Themes/MainTheme/images/PopupClose.png"
                                ID="img2" ToolTip="Close"></asp:ImageButton>
                        </div>
                    </div>
                    <div style="width: 98%; margin: 5px 25px 5px 5px; float: left">
                        <div class="div100" style="overflow-y: auto; overflow-x: auto; height: 300px; left: 5px">
                            <asp:GridView runat="server" ID="gvNonBranch" SkinID="Rpt_Dashboard" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="5%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkMsgAll" runat="server" TextAlign="Left" onclick="checkAllRow(this);" /></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMessage" runat="server" onclick="CheckRow(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BRANCH_OFFICE_NAME" HeaderText="Branch" ReadOnly="True"
                                        ControlStyle-Width="25%" />
                                    <asp:BoundField DataField="MONTH_NAME" HeaderText="Pending Month" ReadOnly="True"
                                        ControlStyle-Width="25%" />
                                    <asp:BoundField DataField="BRANCH_EMAIL_ID" HeaderText="Email" ReadOnly="True" />
                                    <asp:BoundField DataField="INCHARGE_NAME" HeaderText="Incharge Person" ReadOnly="True"
                                        ControlStyle-Width="25%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="div100 pad5">
                            &nbsp;&nbsp;</div>
                        <div class="div100 pad5">
                            <div class="div50 floatleft">
                                <asp:Label runat="server" ID="ltrMsg" Text="Message :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtMsg" runat="server" CssClass="textbox multiline" Width="700px"
                                    Height="35px" onkeypress="return textboxMultilineMaxNumber(event,this,250);"
                                    Text="Please upload the data to the Portal" ToolTip="Enter Message" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfMsg" runat="server" CssClass="requiredcolor" Text="*"
                                    ErrorMessage="Message is required" SetFocusOnError="True" ControlToValidate="txtMsg"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="div100 floatright">
                                <asp:Button ID="btnSendMail" OnClick="btnSendMail_Click" runat="server" CssClass="button"
                                    Text="send" ToolTip="Click here to send mail"></asp:Button>
                                <asp:Button ID="btnClose" runat="server" CssClass="button" OnClientClick="javascript:HideDisplayPopUp();"
                                    Text="Close" ToolTip="Click here to close"></asp:Button>
                            </div>
                            <div>
                                <asp:Label Text="" ID="lblMailIds" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upHomeLogin" runat="server">
        <ContentTemplate>
            <div style="width: 100%; float: left; overflow: hidden;" id="divDashBoard" runat="server"
                visible="False">
                <div class="accordion">
                    <div class="accordion-item" id="divprojects" runat="server">
                        Running Project(s)
                        <div class="type">
                        </div>
                    </div>
                    <div class="data" id="divgvprojects" runat="server">
                        <br />
                        <br />
                    </div>
                    <div class="accordion-item" id="divHeadHeader" runat="server">
                        Head Office - Pending Approval
                        <div class="type">
                        </div>
                    </div>
                    <div class="data" id="divHeadOffice" runat="server">
                        <asp:DataList ID="dlHeadOffice" runat="server" RepeatLayout="Flow">
                            <ItemTemplate>
                                <div style="color: Black; margin-right: 5px; font-weight: bold;">
                                    <span class="lilist">&nbsp; </span>
                                    <%#Eval("HEAD_OFFICE_NAME")%>
                                    (
                                    <%#(Eval("HEAD_OFFICE_CODE").ToString().Length > 25) ? Eval("HEAD_OFFICE_CODE").ToString().Substring(0, 25) + "..." : Eval("HEAD_OFFICE_CODE").ToString()%>)
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <br />
                    </div>
                    <div class="accordion-item" id="divBranchHeader" runat="server">
                        Branch Office - Pending Approval
                        <div class="type">
                        </div>
                    </div>
                    <div class="data" id="divBranchOffice" runat="server">
                        <div>
                            <asp:DataList ID="dlBranchOffice" runat="server" RepeatLayout="Flow">
                                <ItemTemplate>
                                    <div style="color: Black; margin-right: 5px; font-weight: bold;">
                                        <span class="lilist">&nbsp; </span>
                                        <%#Eval("BRANCH_OFFICE_NAME")%>
                                        (
                                        <%#(Eval("BRANCH_OFFICE_CODE").ToString().Length > 25) ? Eval("BRANCH_OFFICE_CODE").ToString().Substring(0, 25) + "..." : Eval("BRANCH_OFFICE_CODE").ToString()%>)
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <br />
                    </div>
                    <div class="accordion-item" id="divExpireLicenseHeader" style="height: 15px" runat="server">
                        <div class="floatleft">
                            Branch License Renewal List for 30 days&nbsp;&nbsp;</div>
                        <div class="floatleft" style="padding: 0px">
                            <dx:ASPxButton ID="aspxBtnLicenseRenewal" runat="server" Text="Refresh" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png"
                                OnClick="aspxBtnLicenseRenewal_Click" Height="20px">
                            </dx:ASPxButton>
                        </div>
                        <div class="type">
                        </div>
                    </div>
                    <div class="data" id="divExpireLicense" runat="server">
                        <div>
                            <dx:ASPxGridView ID="gvBranchesRenewal" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="BRANCH_ID" Theme="Office2010Blue" Width="80%" Style="margin-top: 0px"
                                ClientInstanceName="grd">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="HO Code" FieldName="HEAD_OFFICE_CODE" Name="colHOCode"
                                        VisibleIndex="1" Width="5%">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="HO Name" FieldName="HEAD_OFFICE_NAME" Name="colHOName"
                                        VisibleIndex="2" Width="25%">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="BO Code" FieldName="BRANCH_PART_CODE" Name="colBOCode"
                                        VisibleIndex="3" Width="5%">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Branch" FieldName="BRANCH_OFFICE_NAME" Name="colBranchName"
                                        VisibleIndex="4" Width="25%">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Year From" FieldName="YEAR_FROM" Name="colYearFrom"
                                        VisibleIndex="5" Width="5%" PropertiesTextEdit-DisplayFormatString="{0:dd/MM/yyyy}">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Year To" FieldName="YEAR_TO" Name="colYearTo"
                                        VisibleIndex="6" Width="5%" PropertiesTextEdit-DisplayFormatString="{0:dd/MM/yyyy}">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="false"
                                    FilterRowMode="Auto" />
                                <SettingsPager Mode="ShowAllRecords" Visible="False" />
                            </dx:ASPxGridView>
                        </div>
                        <br />
                    </div>
                    <div class="accordion-item" id="divMorethanoneBranchSystemHeader" style="height: 15px"
                        runat="server">
                        <div class="floatleft">
                            More than one Branch Location System's using Acme.erp&nbsp;&nbsp;</div>
                        <div class="floatleft" style="padding: 0px">
                            <dx:ASPxButton ID="aspxBtnMorethanOneRefreshBranch" Height="20px" runat="server"
                                Text="Refresh" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png" OnClick="aspxBtnMorethanOneRefreshBranch_Click">
                            </dx:ASPxButton>
                        </div>
                        <div class="type">
                        </div>
                    </div>
                    <div class="data" id="divMorethanoneBranchSystem" runat="server">
                        <div style="float: left; padding-top: 2px; padding-left: 5px; vertical-align: middle">
                            <dx:ASPxButton ID="aspxBtnExcel" runat="server" Text="" Image-Url="~/App_Themes/MainTheme/images/excel.png"
                                ToolTip="Export to Excel" RenderMode="Link" OnClick="aspxBtnExcel_Click" Visible="false">
                            </dx:ASPxButton>
                        </div>
                        <div>
                            <dx:ASPxGridViewExporter ID="gridExportBranchLocationLoggedHistory" runat="server"
                                GridViewID="gvMorethanoneBranchSystem" />
                            <dx:ASPxGridView ID="gvMorethanoneBranchSystem" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="BRANCH_OFFICE_CODE" Theme="Office2010Blue" Width="80%" Style="margin-top: 0px"
                                ClientInstanceName="grdBranchSystem" OnLoad="gvMorethanoneBranchSystem_Load">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="HO Code" FieldName="HEAD_OFFICE_CODE" Name="colHOCode1"
                                        VisibleIndex="1" Width="5%" Visible="true">
                                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="HO Name" FieldName="HEAD_OFFICE_NAME" Name="colHOName1"
                                        VisibleIndex="2" Width="30%">
                                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="BO Code" FieldName="BRANCH_OFFICE_CODE" Name="colBOCode1"
                                        VisibleIndex="3" Width="5%" Visible="false">
                                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Branch" FieldName="BRANCH_OFFICE_NAME" Name="colBranchName1"
                                        VisibleIndex="4" Width="30%">
                                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Location" FieldName="LOCATION_NAME" Name="colLocation1"
                                        VisibleIndex="5" Width="10%">
                                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="No.System" FieldName="IPs" Name="colSystem" VisibleIndex="6"
                                        Width="3%">
                                        <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowSort="true" AllowDragDrop="False" AllowFocusedRow="True" />
                                <SettingsPager Mode="ShowAllRecords" Visible="False" />
                                <Settings ShowFilterRow="true" />
                                <SettingsDetail ShowDetailRow="True" ExportMode="All" />
                                <ClientSideEvents SelectionChanged="function(s, e) { CheckSelect(s, e); }" />
                                <Templates>
                                    <DetailRow>
                                        <dx:ASPxGridView ID="gvMorethanoneBranchSystemDetails" runat="server" KeyFieldName="BRANCH_OFFICE_CODE"
                                            Theme="Office2010Blue" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="gvMorethanoneBranchSystemDetails_BeforePerformDataSelect">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="BO Code" FieldName="BRANCH_OFFICE_CODE1" Name="colBOCode2"
                                                    VisibleIndex="1" Width="5%" Visible="true">
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Location" FieldName="LOCATION_NAME" Name="colLocation2"
                                                    VisibleIndex="2" Width="20%">
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="License Key Number" FieldName="LICENSE_KEY_NUMBER"
                                                    Name="colLocation1" VisibleIndex="3" Width="15%">
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Logged On" FieldName="LOGGED_ON" Name="colLoggedOn1"
                                                    VisibleIndex="4" Width="5%" PropertiesTextEdit-DisplayFormatString="{0:dd/MM/yyyy}">
                                                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                                    </PropertiesTextEdit>
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Remarks" FieldName="REMARKS" Name="colSystemDetails"
                                                    VisibleIndex="5">
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFilterRow="true" ShowHeaderFilterButton="true" />
                                            <SettingsBehavior AllowDragDrop="False" />
                                        </dx:ASPxGridView>
                                    </DetailRow>
                                </Templates>
                            </dx:ASPxGridView>
                        </div>
                        <br />
                    </div>
                    <div class="accordion-item" id="divSoftHeader" runat="server">
                        Software Updates<span class="features_new_right"></span>
                        <div class="type">
                        </div>
                    </div>
                    <div class="data" id="divsSoftware" runat="server">
                        <asp:DataList ID="dlNewUpdates" runat="server" RepeatLayout="Flow">
                            <ItemTemplate>
                                <div style="color: Black; margin-right: 5px; font-weight: bold;">
                                    <span class="lilist">&nbsp; </span>
                                    <%#Eval("Title")%>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="divwd100" style="box-shadow: 0px 0px 2px rgba(0, 0, 0, 0.3);" id="divHoDashBoard"
        visible="False" runat="server">
        <div class="row-fluid">
            <div class="floatleft" style="width: 25%; padding: 5px; padding-right: 0px;">
                <div class="floatleft bold" style="padding-right: 5px; padding-top: 3px;">
                    <asp:Literal ID="ltrlBranch" runat="server" Text="Branch"></asp:Literal></div>
                <div>
                    <dx:ASPxComboBox ID="cmbBranches" runat="server" Theme="Office2010Blue" OnSelectedIndexChanged="cmbBranches_SelectedIndexChanged"
                        IncrementalFilteringMode="Contains" Width="80%" TabIndex="4" AutoPostBack="true">
                    </dx:ASPxComboBox>
                </div>
            </div>
            <div style="float: left; width: 35%; padding: 5px; padding-right: 0px;">
                <div class="floatleft bold" style="padding-right: 5px; padding-top: 3px">
                    <asp:Literal ID="ltrlProject" runat="server" Text="Project"></asp:Literal></div>
                <div>
                    <dx:ASPxComboBox ID="cmbProject" runat="server" IncrementalFilteringMode="Contains"
                        Width="85%" OnSelectedIndexChanged="cmbProject_SelectedIndexChanged" TabIndex="5"
                        Theme="Office2010Blue" AutoPostBack="false">
                    </dx:ASPxComboBox>
                </div>
            </div>
            <div style="float: left; width: 10%; padding: 5px; padding-right: 0px;">
                <dx:ASPxButton ID="btnViewStatus" runat="server" Text="View Status" Theme="Office2010Blue"
                    TabIndex="3" Height="20px" Width="100px" OnClick="btnViewStatus_Click">
                </dx:ASPxButton>
            </div>
            <div style="float: right; 20%; padding-top: 9px;">
                <asp:HyperLink NavigateUrl="~/HomeDashboard.aspx" ID="hlkProject" Text="Detail View"
                    Visible="true" ToolTip="Detail View" runat="server" CssClass="underline link font_size"></asp:HyperLink>
            </div>
        </div>
        <div>
            <UI:Balance ID="uiClosingBalance" runat="server" />
        </div>
        <div class="div100">
            <div class="row-fluid">
                <div class="div100 floatleft">
                    <div class="div100" style="float: left; padding-bottom: 2px;">
                        <dx:ASPxPivotGrid ID="pvtDataSynStatus" Width="100%" OnLoad="pvtDataSynStatus_Load"
                            OnFieldValueDisplayText="pvtDataSynStatus_FieldValueDisplayText" OnCustomFieldSort="pvtDataSynStatus_CustomFieldSort"
                            Theme="Office2003Blue" OnCustomCellStyle="pvtDataSynStatus_CustomCellStyle" runat="server"
                            ClientIDMode="AutoID">
                            <Fields>
                                <dx:PivotGridField Area="RowArea" FieldName="PROJECT" Caption="Project" ExpandedInFieldsGroup="False"
                                    ID="fieldBOName" SortMode="None" AreaIndex="0">
                                </dx:PivotGridField>
                                <dx:PivotGridField Area="ColumnArea" FieldName="MONTH_NAME" SortMode="Custom" AreaIndex="0"
                                    Caption="Month" />
                                <dx:PivotGridField Area="DataArea" FieldName="RESULT" AreaIndex="0" SortMode="None"
                                    CellFormat-FormatString="{0:N}" CellFormat-FormatType="None" SummaryType="Sum" />
                            </Fields>
                            <OptionsView ShowFilterHeaders="False" ShowFilterSeparatorBar="False" ShowColumnHeaders="False"
                                ShowRowTotals="False" ShowRowGrandTotals="false" ShowDataHeaders="False" />
                            <OptionsCustomization AllowDrag="False" AllowSort="False" AllowPrefilter="False"
                                AllowFilter="true" />
                            <OptionsPager AlwaysShowPager="false" Visible="false">
                            </OptionsPager>
                        </dx:ASPxPivotGrid>
                    </div>
                    <!--  <%--  <div>
                        <div class="div100 bold" style="float: left; padding-top: 2px;">
                            <asp:Literal ID="ltrlNC" runat="server" Text="Non Conformity Branches">
                            </asp:Literal>
                            <hr />
                        </div>
                        <div class="div100" style="overflow-y: auto; overflow-x: hidden; height: 250px;">
                            <asp:GridView runat="server" ID="gvNonBranch" SkinID="Rpt_Dashboard" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BRANCH_OFFICE_NAME" HeaderText="Branch" ReadOnly="True" />
                                    <asp:BoundField DataField="INCHARGE_NAME" HeaderText="Incharge Person" ReadOnly="True" />
                                    <asp:BoundField DataField="MONTH_NAME" HeaderText="Pending Month" ReadOnly="True" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkMsgAll" runat="server" TextAlign="Left" Enabled="false" /></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMessage" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>--%> -->
                </div>
                <!--<%--<div class="bold textcenter" style="padding: 3px; color: #00a9e7;">
                    <div class="floatleft">
                        <asp:Label ID="lblProject" runat="server" Text="" ForeColor="#DD5725"></asp:Label>
                    </div>
                    <%--<div class="floatright underline">
                        <asp:Label ID="lblMonth" runat="server" Text="" ForeColor="#00A9E7"></asp:Label></div>
                </div>--%> -->
                <div>
                    <!-- <%--<div class="bold textcenter">
                        <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
                    </div>--%> -->
                    <div class="div100" style="padding-top: 5px;">
                        <div style="width: 99.9%; border: 1px solid #3399FF;">
                            <div class="criteriaribben">
                                <!-- <%--<div class="floatright bold fontsize13 pad5" style="padding-right: 2px; color: black;">
                    <asp:HyperLink NavigateUrl="~/HomeDashboard.aspx" ID="hlkProject" Text="Detail View" Visible="true"
                        ToolTip="Detail View" runat="server" CssClass="underline link font_size"></asp:HyperLink>
                    <asp:Literal ID="ltrlACYear" runat="server"></asp:Literal>
                </div>--%> -->
                                <div class="floatright bold fontsize13 pad5" style="color: black;">
                                    <asp:HyperLink NavigateUrl="#" ID="hlkConformity" Text="Non Conforming Branches "
                                        onclick="javascript:ShowDisplayPopUp()" ToolTip="Non Conforming Branches" runat="server"
                                        CssClass="underline link font_size blink" Style="margin-left: 0px; padding: 0 0 0 0;"></asp:HyperLink>
                                </div>
                                <!-- <%--<div class="floatright bold fontsize13 pad5" style="padding-right: 2px; color: black;">
                                    <asp:Label ID="lblMonth" runat="server" Text="" ForeColor="#00A9E7" Style="float: left"></asp:Label>
                                </div>--%> -->
                                <div class="" id="Generate" style="float: left; padding-left: 5px; display: none">
                                    <div class="floatright" style="padding-left: 5px; padding-right: 5px;">
                                        <dx:ASPxButton ID="btnGo" runat="server" Text="Generate Receipts and Payments" Theme="Office2010Blue"
                                            TabIndex="3" OnClick="btnGo_Click" Height="20px" Width="100px">
                                            <Image Url="~/App_Themes/MainTheme/images/go.png">
                                            </Image>
                                        </dx:ASPxButton>
                                    </div>
                                    <div class="floatright" style="padding-left: 5px; padding-right: 5px;">
                                        <dx:ASPxDateEdit ID="dteTo" runat="server" Width="90px" UseMaskBehavior="True" TabIndex="2"
                                            Theme="Office2010Blue" EditFormat="Custom" DisplayFormatString="MMM yyyy" EditFormatString="dd/MM/yyyy">
                                        </dx:ASPxDateEdit>
                                    </div>
                                    <div class="bold" style="float: right; padding-top: 2px; padding-left: 5px;">
                                        <span class="bold">To</span>
                                    </div>
                                    <div class="floatright" style="padding-left: 5px; padding-right: 5px;">
                                        <dx:ASPxDateEdit ID="dteFrom" runat="server" Width="90px" UseMaskBehavior="True"
                                            TabIndex="1" Theme="Office2010Blue" EditFormat="Custom" DisplayFormatString="MMM yyyy"
                                            EditFormatString="dd/MM/yyyy">
                                        </dx:ASPxDateEdit>
                                    </div>
                                    <div class="bold floatright" style="padding-top: 2px;">
                                        <asp:Literal ID="ltrTPeriod" runat="server" Text="Period"></asp:Literal></div>
                                    <!-- <%--<div class="bold" style="float: right; padding-top: 2px; padding-left: 5px;">
                               <asp:Literal ID="ltrlBranchStatus" runat="server" Text="Branch Status"></asp:Literal></div>--%> -->
                                </div>
                            </div>
                        </div>
                        <dx:ASPxDocumentViewer ID="dvReportViewer" runat="server" ReportTypeName="Bosco.Report.ReportObject"
                            OnCacheReportDocument="dvReportViewer_CacheReportDocument" OnRestoreReportDocumentFromCache="dvReportViewer_RestoreReportDocumentFromCache"
                            OnUnload="dvReportViewer_Unload" ClientInstanceName="dvReportViewer" Theme="Office2010Blue"
                            Visible="false">
                        </dx:ASPxDocumentViewer>
                    </div>
                </div>
            </div>
        </div>
    </div>
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

        function CheckRow(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //Change the gridview row color when checkbox checked change
                row.style.backgroundColor = "#5CADFF";

            }
            else {
                //If checkbox not checked change default row color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#AED6FF";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }

        function checkAllRow(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "#5CADFF";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "#AED6FF";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }

        divExpireLicense.style.display = "none";
        Generate.style.display = "none";
    </script>
</asp:Content>
