<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ReportCriteria.aspx.cs" Inherits="AcMeERP.Report.ReportCriteria" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="~/WebControl/DateControl.ascx" TagName="Datecontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div id="main" runat="server" style="width: 100%;">
        <div id="divCriteria" runat="server" style="width: 100%;">
            <ajax:TabContainer ID="TabReportCriteria" runat="server" ActiveTabIndex="0" Height="300px"
                Width="90%" OnDemand="True" meta:resourcekey="TabReportCriteriaResource1">
                <ajax:TabPanel runat="server" HeaderText="Date" ID="tabDate" 
                    OnDemandMode="Once" meta:resourcekey="tabDateResource1">
                    <ContentTemplate>
                        <div style="padding-bottom: 10px; padding-top: 10px; padding-left: 10px; padding-right: 10px;">
                            <div style="width: 50%; float: left">
                                <div style="float: left; width: 30%">
                                    <asp:Label ID="lblDtFrom" runat="server" Text="Date From" Style="float: left;" 
                                        meta:resourcekey="lblDtFromResource1"></asp:Label>
                                </div>
                                <div style="float: left; width: 20%">
                                    <uc1:Datecontrol ID="dtFrom" runat="server" showMandatory="true" showCalender="true"
                                        ShowTime="false"  Visible="False"/>
                                </div>
                            </div>
                            <div style="width: 50%; float: right">
                                <div style="float: left; width: 25%">
                                    <asp:Label ID="lblDtTo" runat="server" Text="Date To " Style="float: left;" 
                                        meta:resourcekey="lblDtToResource1"></asp:Label>
                                </div>
                                <div style="float: left; width: 21%">
                                    <uc1:Datecontrol ID="dtTo" runat="server" showMandatory="true" showCalender="true"
                                        ShowTime="false"  Visible="False"/>
                                </div>
                            </div>
                        </div>
                        <div style="width: 85%; float: left; padding-top: 25px; padding-left: 20px;">
                            <asp:GridView ID="gvDateLedger" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                GridLines="None" meta:resourcekey="gvDateLedgerResource1">
                                <Columns>
                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDateLedger" runat="server" Text='<%# Eval("NAME") %>' Checked='<%# Eval("SELECT").ToString().Equals("1")?true:false %>'
                                            AutoPostBack="True" OnCheckedChanged="chkDateLedger_CheckedChanged" 
                                                meta:resourcekey="chkDateLedgerResource1"/>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>
                <ajax:TabPanel ID="TabProject" runat="server" HeaderText="Project" 
                    Visible="false" meta:resourcekey="TabProjectResource1">
                    <ContentTemplate>
                        <div style="width: 100%;">
                            <div id="divProject" runat="server" style="width:100%;height: 307px; float: left; overflow-y: auto; overflow-x: hidden;">
                                <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="False" 
                                    SkinID="Rpt_Criteria" meta:resourcekey="gvProjectResource1">
                                    <Columns>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkProjectSelectAll" AutoPostBack="True" ToolTip="Click To Select All"
                                                    runat="server" onclick="javascript:SelectAllProject();" 
                                                    OnCheckedChanged="chkProjectSelectAll_CheckedChanged" 
                                                    meta:resourcekey="chkProjectSelectAllResource1" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkProjectSelect" runat="server" ValidationGroup='<%# Eval("PROJECT_ID") %>'
                                                    OnCheckedChanged="chkProjectSelect_CheckedChanged" AutoPostBack="True" 
                                                    Checked='<%# Eval("SELECT").Equals("1")?true:false %>' 
                                                    meta:resourcekey="chkProjectSelectResource1" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Project" DataField="PROJECT" ReadOnly="True" 
                                            meta:resourcekey="BoundFieldResource1" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="margin-left: 10px; height: 307px; float: left; overflow-y: auto;
                                overflow-x: hidden;" runat="server" id="divBank">
                                <asp:GridView ID="gvBankAccount" runat="server" AutoGenerateColumns="False" 
                                    SkinID="Rpt_Criteria" Visible="False" meta:resourcekey="gvBankAccountResource1">
                                    <Columns>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkBankSelectAll" runat="server" onclick="javascript:SelectAllBank();"
                                                AutoPostBack="True" OnCheckedChanged="chkBankSelectAll_CheckedChanged" 
                                                    ToolTip="Click To Select All" meta:resourcekey="chkBankSelectAllResource1" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkBankSelect" runat="server" ValidationGroup='<%# Eval("BANK_ID") %>'
                                                OnCheckedChanged="chkBankSelect_CheckedChanged" AutoPostBack="True" 
                                                    Checked='<%# Eval("SELECT").Equals("1")?true:false %>' 
                                                    meta:resourcekey="chkBankSelectResource1"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Bank" DataField="BANK" ReadOnly="True" 
                                            meta:resourcekey="BoundFieldResource2" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>
                <ajax:TabPanel ID="TabLedger" runat="server" HeaderText="Ledger" 
                    Visible="false" meta:resourcekey="TabLedgerResource1">
                    <ContentTemplate>
                        <div style="width: 100%;">
                            <div style="width: 48%; height: 307px; float: left; overflow-y: auto; overflow-x: hidden;">
                                <asp:GridView ID="gvLedgerGroup" runat="server" AutoGenerateColumns="False" Width="100%"
                                    SkinID="Rpt_Criteria" meta:resourcekey="gvLedgerGroupResource1">
                                    <Columns>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                            <HeaderTemplate>
                                                    <asp:CheckBox ID="chkLedgerGroupSelectAll" ToolTip="Click To Select All" AutoPostBack="True"
                                                    runat="server" onclick="javascript:SelectAllLedgerGroup();" 
                                                        OnCheckedChanged="chkLedgerGroupSelectAll_CheckedChanged" 
                                                        meta:resourcekey="chkLedgerGroupSelectAllResource1" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkLedgerGroupSelect" runat="server" 
                                                    ValidationGroup='<%# Eval("GROUP_ID") %>' AutoPostBack="True"
                                                 OnCheckedChanged="chkLedgerGroupSelect_CheckedChanged" 
                                                    Checked='<%# Eval("SELECT").Equals("1")?true:false %>' 
                                                    meta:resourcekey="chkLedgerGroupSelectResource1" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Ledger Group" DataField="GROUP" ReadOnly="True" 
                                            meta:resourcekey="BoundFieldResource3" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="width: 50%; margin-left: 10px; float: left; height: 307px; overflow-y: auto;
                                overflow-x: hidden;">
                                <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="False" 
                                    SkinID="Rpt_Criteria" meta:resourcekey="gvLedgerResource1">
                                    <Columns>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkLedgerSelectAll" runat="server" OnCheckedChanged="chkLedgerSelectAll_CheckedChanged"
                                                    AutoPostBack="True"  onclick="javascript:SelectAllLedger();" 
                                                    meta:resourcekey="chkLedgerSelectAllResource1"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkLedgerSelect" runat="server" 
                                                    ValidationGroup='<%# Eval("LEDGER_ID") %>' AutoPostBack="True"
                                                OnCheckedChanged="chkLedgerSelect_CheckedChanged" 
                                                    Checked='<%# Eval("SELECT").Equals("1")?true:false %>' 
                                                    meta:resourcekey="chkLedgerSelectResource1" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Ledger" DataField="LEDGER" ReadOnly="True" 
                                            meta:resourcekey="BoundFieldResource4" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>
                <ajax:TabPanel ID="TabCostCentre" runat="server" HeaderText="Cost Centre" 
                    OnDemandMode="Once" Visible="false" meta:resourcekey="TabCostCentreResource1">
                    <ContentTemplate>
                        <div style="width: 100%; overflow-y: auto; overflow-x: hidden; height: 306px;">
                            <asp:GridView ID="gvCostCentre" runat="server" AutoGenerateColumns="False" 
                                SkinID="Rpt_Criteria" meta:resourcekey="gvCostCentreResource1">
                                <Columns>
                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkCostCentreSelectAll" runat="server" 
                                                OnCheckedChanged="chkCostCentreSelectAll_CheckedChanged" AutoPostBack="True" 
                                                onclick="javascript:SelectAllCostCentre();" 
                                                meta:resourcekey="chkCostCentreSelectAllResource1"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCostCentreSelect" runat="server" 
                                                ValidationGroup='<%# Eval("COST_CENTRE_ID") %>' OnCheckedChanged="chkCostCentreSelect_CheckedChanged" 
                                            AutoPostBack="True" Checked='<%# Eval("SELECT").Equals("1")?true:false %>' 
                                                meta:resourcekey="chkCostCentreSelectResource1"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Cost Centre" DataField="COST_CENTRE_NAME" 
                                        ReadOnly="True" meta:resourcekey="BoundFieldResource5" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>
                <ajax:TabPanel ID="TabNarration" runat="server" HeaderText="Narration" 
                    OnDemandMode="Once" Visible="false" meta:resourcekey="TabNarrationResource1">
                    <ContentTemplate>
                        I'm tab 4. Hey, I&#39;m loaded only once!<br />
                        I was rendered at
                    </ContentTemplate>
                </ajax:TabPanel>
                <ajax:TabPanel ID="TabBranch" runat="server" HeaderText="Branch" 
                    OnDemandMode="Once" meta:resourcekey="TabBranchResource1">
                    <ContentTemplate>
                        <div style="width: 100%;">
                            <div style="width: 9%; float: left;">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch" 
                                    meta:resourcekey="lblBranchResource1"></asp:Label>
                            </div>
                            <div style="margin-left: 2px;">
                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" 
                                    CssClass="combobox"  ontextchanged="ddlBranch_TextChanged" 
                                    meta:resourcekey="ddlBranchResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>
                <%--Report Setup--%>
                <ajax:TabPanel ID="TabReportSetup" runat="server" HeaderText="Report Setup" 
                    OnDemandMode="Once" meta:resourcekey="TabReportSetupResource1">
                    <ContentTemplate>
                        <div style="float: left; width: 100%">
                            <div style="float: left; width: 50%; padding-top: 10px;">
                                <div style="float: left; width: 100%;">
                                    <div style="width: 50%; float: left;">
                                        <asp:CheckBox ID="chkShowLedgerCode" runat="server" Text="Show Ledger Code" 
                                            meta:resourcekey="chkShowLedgerCodeResource1" />
                                    </div>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="ddlSortByLedger" runat="server" 
                                            meta:resourcekey="ddlSortByLedgerResource1">
                                            <asp:ListItem Value="1" Selected="True" meta:resourcekey="ListItemResource1">Sort By Ledger Code</asp:ListItem>
                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">Sort By Ledger Name</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 10px;">
                                    <div style="width: 50%; float: left;">
                                        <asp:CheckBox ID="chkShowGroupCode" runat="server" Text="Show Group Code" 
                                            meta:resourcekey="chkShowGroupCodeResource1" />
                                    </div>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="ddlSortByGroup" runat="server" 
                                            meta:resourcekey="ddlSortByGroupResource1">
                                            <asp:ListItem Value="1" Selected="True" meta:resourcekey="ListItemResource3">Sort By Group Code</asp:ListItem>
                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource4">Sort By Group Name</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 10px;">
                                    <div style="width: 100%; float: left;">
                                        <asp:CheckBox ID="chkShowTitle" runat="server" Text="Show Titles At Each Page" 
                                            meta:resourcekey="chkShowTitleResource1" />
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 10px;">
                                        <asp:CheckBox ID="chkVerticalLine" runat="server" Text="Veritcal Line" 
                                            meta:resourcekey="chkVerticalLineResource1" />
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 10px;">
                                        <asp:CheckBox ID="chkHorizontalLine" runat="server" Text="Horizontal Line" 
                                            meta:resourcekey="chkHorizontalLineResource1" />
                                    </div>
                                </div>
                            </div>
                            <div style="float: left; width: 49%; padding-top: 10px;">
                                <div style="float: left; width: 100%;">
                                    <div style="width: 50%; float: left;">
                                        <asp:Label ID="lblReportDate" runat="server" Text="Report Date  " 
                                            Style="float: left;" meta:resourcekey="lblReportDateResource1"></asp:Label>
                                    </div>
                                    <div style="float: left;">
                                        <uc1:Datecontrol ID="ReportDate" runat="server" showCalender="true" showMandatory="true"
                                            ShowTime="false" />
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 10px;">
                                    <div style="width: 50%; float: left;">
                                        <asp:Label ID="lblTitleAlignment" runat="server" Text="Title Alignment  " 
                                            Style="float: left;" meta:resourcekey="lblTitleAlignmentResource1"></asp:Label>
                                    </div>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="ddlTitleAlignment" runat="server" 
                                            meta:resourcekey="ddlTitleAlignmentResource1">
                                            <asp:ListItem Value="1" Selected="True" meta:resourcekey="ListItemResource5">Left</asp:ListItem>
                                            <asp:ListItem Value="2" meta:resourcekey="ListItemResource6">Centre</asp:ListItem>
                                            <asp:ListItem Value="3" meta:resourcekey="ListItemResource7">Right</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="float: left; width: 100%; padding-top: 10px;">
                                    <div style="width: 100%; float: left;">
                                        <asp:CheckBox ID="chkShowReportLogo" runat="server" Text="Show Report Logo" 
                                            meta:resourcekey="chkShowReportLogoResource1" />
                                    </div>
                                    <div style="width: 100%; float: left; padding-top: 10px;">
                                        <asp:CheckBox ID="chkShowPageNumber" runat="server" Text="Show Page Number" 
                                            meta:resourcekey="chkShowPageNumberResource1" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </ajax:TabPanel>
            </ajax:TabContainer>
        </div>
        <div style="width: 90%; text-align: right; margin-top: 5px;">
            <asp:Button ID="btnOk" Text="OK" runat="server" CssClass="button" 
                OnClick="btnOk_Click" OnClientClick="javascript:OpenReportViewer();" 
                meta:resourcekey="btnOkResource1" />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" 
                OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
        </div>
    </div>
    <script type="text/javascript">
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
        }

        function SelectAllLedger() {
            var RowCount = document.getElementById('ctl00_cpMain_TabReportCriteria_TabLedger_gvLedger').rows.length;
//            alert(RowCount);
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
        }
        function OpenReportViewer() {
            window.open("<%= Page.ResolveClientUrl("~/Report/ReportViewerPage.aspx") %>", "_blank", "toolbar=no, scrollbars=yes, resizable=yes, top=500, left=500, width=600, height=600");
        }
    </script>
</asp:Content>
