<%@ Page Title="Home Dashboard" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="HomeDashboard.aspx.cs" Inherits="AcMeERP.HomeDashboard"
    meta:resourcekey="PageResource3" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upHomeDashBoard" runat="server">
        <ContentTemplate>
            <div class="divwd100" style="box-shadow: 0px 0px 2px rgba(0, 0, 0, 0.3);" id="divHoDashBoard"
                visible="False" runat="server">
                <ajax:TabContainer ID="TabDashBoard" runat="server" ActiveTabIndex="0" CssClass="Tab"
                    AutoPostBack="True" OnActiveTabChanged="TabDashBoard_ActiveTabChanged" Width="100%"
                    meta:resourcekey="TabDashBoardResource1">
                    <ajax:TabPanel runat="server" HeaderText="Data Status" ID="tabUpload" meta:resourcekey="tabUploadResource1">
                        <ContentTemplate>
                            <div class="floatright bold fontsize13 pad5" style="padding-right: 2px; color: black;">
                                <asp:HyperLink NavigateUrl="~/Report/ReportCriteriaPortal.aspx?rid=RPT-061" ID="hlkDataStatus"
                                    Text="Branch Data Status" ToolTip="Branch Data Status" runat="server" CssClass="underline link font_size"></asp:HyperLink>
                            </div>
                            <div class="floatright bold fontsize13 pad5" style="padding-right: 2px; color: black;">
                                <asp:HyperLink NavigateUrl="~/HomeLogin.aspx" ID="hlkProject" Text="Quick View "
                                    ToolTip="Quick View" runat="server" CssClass="underline link font_size" meta:resourcekey="hlkProjectResource1"></asp:HyperLink>
                                <asp:Literal ID="ltrlACYear" runat="server" meta:resourcekey="ltrlACYearResource1"></asp:Literal>
                            </div>
                            <div class="div100">
                                <dx:ASPxPivotGrid ID="pvtDataSynStatus" Width="100%" OnFieldValueDisplayText="pvtDataSynStatus_FieldValueDisplayText"
                                    OnCustomFieldSort="pvtDataSynStatus_CustomFieldSort" OnHtmlFieldValuePrepared="pvtDataSynStatus_HtmlFieldValuePrepared"
                                    Theme="Office2003Blue" OnCustomCellStyle="pvtDataSynStatus_CustomCellStyle" runat="server"
                                    ClientIDMode="AutoID" meta:resourcekey="pvtDataSynStatusResource1">
                                    <Fields>
                                        <dx:PivotGridField Area="RowArea" FieldName="BRANCH_OFFICE_NAME" Caption="Branch Office"
                                            ExpandedInFieldsGroup="False" ID="fieldBOName" SortMode="None" AreaIndex="0"
                                            meta:resourcekey="PivotGridFieldResource1">
                                        </dx:PivotGridField>
                                        <dx:PivotGridField Area="ColumnArea" FieldName="MONTH_NAME" SortMode="Custom" AreaIndex="0"
                                            Caption="Month" />
                                        <dx:PivotGridField Area="DataArea" FieldName="RESULT" AreaIndex="0" SortMode="None"
                                            CellFormat-FormatString="{0:N}" />
                                    </Fields>
                                    <OptionsView ShowFilterHeaders="False" ShowFilterSeparatorBar="False" ShowColumnHeaders="False"
                                        ShowRowTotals="False" ShowRowGrandTotals="False" ShowDataHeaders="False" />
                                    <OptionsCustomization AllowDrag="False" AllowSort="False" AllowPrefilter="False" />
                                    <OptionsPager RowsPerPage="20" AlwaysShowPager="True">
                                    </OptionsPager>
                                </dx:ASPxPivotGrid>
                            </div>
                            <div class="Note floatleft pad7" style="background-color: rgb(255, 223, 214); box-shadow: 1px 0 3px rgba(52, 50, 50, 0.5);
                                width: 100%; font-weight: bold;">
                                <span class="red"><strong>Note: </strong></span>Displays number of transactions
                                by branch and month for the active financial year
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel runat="server" HeaderText="Branch" ID="tabBranch" TabIndex="1" meta:resourcekey="tabBranchResource1"
                        Visible="false">
                        <ContentTemplate>
                            <div class="criteriaribben">
                                <div style="float: right; padding-left: 5px;">
                                    <div class="floatright" style="padding-left: 5px; padding-right: 5px;">
                                        <dx:ASPxButton ID="btnGo" runat="server" Text="Go" Theme="Office2010Blue" OnClick="btnGo_Click"
                                            TabIndex="3" Height="20px" Width="100px" meta:resourcekey="btnGoResource2">
                                            <Image Url="~/App_Themes/MainTheme/images/go.png">
                                            </Image>
                                        </dx:ASPxButton>
                                    </div>
                                    <div class="floatright" style="padding-left: 5px; padding-right: 5px;">
                                        <dx:ASPxDateEdit ID="dteBalanceDate" runat="server" Width="90px" UseMaskBehavior="True"
                                            TabIndex="2" Theme="Office2010Blue" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy"
                                            EditFormatString="dd/MM/yyyy" meta:resourcekey="dteBalanceDateResource2">
                                        </dx:ASPxDateEdit>
                                    </div>
                                    <div style="float: right; padding-top: 2px; padding-left: 5px;">
                                        <span class="bold">Date As on</span>
                                    </div>
                                </div>
                            </div>
                            <div class="div100">
                                <dx:ASPxGridView ID="gvBranchDetail" runat="server" KeyFieldName="BRANCH_ID" Width="100%"
                                    OnLoad="gvBranchDetail_Load" Theme="Office2010Blue" AutoGenerateColumns="False"
                                    meta:resourcekey="gvBranchDetailResource1">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colBranchId" FieldName="BRANCH_ID" Visible="False"
                                            ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource8">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colBranchName" VisibleIndex="1" FieldName="NAME"
                                            Caption="Branch" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource9">
                                            <HeaderStyle Font-Bold="True" />
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colOpeningBalance" VisibleIndex="2" FieldName="OPAMOUNT"
                                            Caption="Opening Balance" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource10">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colReceiptAmount" VisibleIndex="3" FieldName="RC_AMOUNT"
                                            Caption="Receipt" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource11">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="True" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colPaymentAmount" VisibleIndex="4" FieldName="PY_AMOUNT"
                                            Caption="Payment" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource12">
                                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                            <HeaderStyle HorizontalAlign="Right" Font-Bold="True" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colClosingBalance" VisibleIndex="5" FieldName="CLAMOUNT"
                                            Caption="Closing Balance" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource13">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <PropertiesTextEdit DisplayFormatString="{0:n2} " NullDisplayText="0.00">
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings VerticalScrollableHeight="300" ShowFilterRow="True" ShowFilterRowMenu="True"
                                        ShowHeaderFilterButton="true" />
                                    <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="True" />
                                    <SettingsPager PageSize="15">
                                        <PageSizeItemSettings Visible="True" ShowAllItem="True" />
                                    </SettingsPager>
                                </dx:ASPxGridView>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel runat="server" HeaderText="Project" ID="tabProject" TabIndex="2" meta:resourcekey="tabProjectResource1"
                        Visible="false">
                        <ContentTemplate>
                            <div class="criteriaribben">
                                <div style="float: right; padding-left: 5px;">
                                    <asp:UpdatePanel ID="upBranchCombo" runat="server">
                                        <ContentTemplate>
                                            <div class="floatright" style="padding-left: 5px; padding-right: 5px;">
                                                <dx:ASPxButton ID="btnProjectGo" runat="server" Text="Go" Theme="Office2010Blue"
                                                    OnClick="btnProjectGo_Click" TabIndex="3" Height="20px" Width="100px" meta:resourcekey="btnProjectGoResource1">
                                                    <Image Url="~/App_Themes/MainTheme/images/go.png">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                            <div class="floatright" style="padding-left: 5px; padding-right: 5px;">
                                                <dx:ASPxDateEdit ID="dteProjectBalDate" runat="server" Width="90px" UseMaskBehavior="True"
                                                    TabIndex="2" Theme="Office2010Blue" EditFormat="Custom" DisplayFormatString="dd/MM/yyyy"
                                                    EditFormatString="dd/MM/yyyy" meta:resourcekey="dteProjectBalDateResource1">
                                                </dx:ASPxDateEdit>
                                            </div>
                                            <div class="floatright" style="padding-left: 5px;">
                                                <dx:ASPxComboBox ID="cmbBranches" runat="server" Theme="Office2010Blue" IncrementalFilteringMode="Contains"
                                                    OnSelectedIndexChanged="cmbBranches_SelectedIndexChanged" Width="350px" AutoPostBack="True"
                                                    TabIndex="1" meta:resourcekey="cmbBranchesResource1">
                                                </dx:ASPxComboBox>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div style="float: right; padding-top: 2px; padding-left: 5px;">
                                    <span class="bold">Branch</span>
                                </div>
                            </div>
                            <div class="div100">
                                <asp:UpdatePanel ID="UpProject" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="gvProjects" runat="server" KeyFieldName="PROJECT_ID" Width="100%"
                                            OnLoad="gvProjects_Load" Theme="Office2010Blue" AutoGenerateColumns="False" meta:resourcekey="gvProjectsResource2">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Name="colProjectId" FieldName="PROJECT_ID" Visible="False"
                                                    ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource14">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colProject" VisibleIndex="1" FieldName="PROJECT"
                                                    Caption="Project" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource15">
                                                    <HeaderStyle Font-Bold="True" />
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colSociety" VisibleIndex="2" FieldName="SOCIETYNAME"
                                                    Caption="Legal Entity" ShowInCustomizationForm="True" Width="300px" meta:resourcekey="GridViewDataTextColumnResource16">
                                                    <HeaderStyle Font-Bold="True" />
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colOpeningBalance" VisibleIndex="3" FieldName="OPAMOUNT"
                                                    Caption="Opening Balance" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource17">
                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                                    </PropertiesTextEdit>
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colClosingBalance" VisibleIndex="6" FieldName="CLAMOUNT"
                                                    Caption="Closing Balance" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource18">
                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <PropertiesTextEdit DisplayFormatString="{0:n2} " NullDisplayText="0.00">
                                                    </PropertiesTextEdit>
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colReceiptAmount" VisibleIndex="4" FieldName="RC_AMOUNT"
                                                    Caption="Receipt" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource19">
                                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                                    </PropertiesTextEdit>
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                                    <HeaderStyle HorizontalAlign="Right" Font-Bold="True" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colPaymentAmount" VisibleIndex="5" FieldName="PY_AMOUNT"
                                                    Caption="Payment" ShowInCustomizationForm="True" meta:resourcekey="GridViewDataTextColumnResource20">
                                                    <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                                    </PropertiesTextEdit>
                                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                                                    <HeaderStyle HorizontalAlign="Right" Font-Bold="True" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings VerticalScrollableHeight="300" ShowFilterRow="True" ShowFilterRowMenu="True"
                                                ShowHeaderFilterButton="true" />
                                            <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="True" />
                                            <SettingsPager PageSize="15">
                                                <PageSizeItemSettings Visible="True" ShowAllItem="True" />
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </ajax:TabPanel>
                </ajax:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
