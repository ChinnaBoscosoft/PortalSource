<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ProjectLocationMapping.aspx.cs" Inherits="AcMeERP.Module.Office.ProjectLocationMapping" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function chkSelectAll_CheckChanged(s, e) {
            if (s.GetChecked()) {
                var intTopVisibleIndex = gvProject.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvProject.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvProject.name + '_DXSelBtn' + (intTopVisibleIndex + i)).disabled;
                    if (!blnDisabled)
                        gvProject.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
                }
            }
            else {
                var intTopVisibleIndex = gvProject.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvProject.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvProject.name + '_DXSelBtn' + (intTopVisibleIndex + i)).value;
                    //  alert(blnDisabled + "" + intTopVisibleIndex);
                    if (blnDisabled == "U")
                        gvProject.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
                }
            }


        }
        function SelectionChanged(s, e) {
            chkSelectAll.SetChecked(false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben">
        <div>
            <div style="float: right; padding-left: 5px; padding-right: 5px">
                <dx:ASPxButton ID="bntSaveOnTop" OnClick="bntSaveOnTop_Click" runat="server" Theme="Office2010Blue"
                    Height="20px" Text="Save" ToolTip="Click here Map projects to Branch">
                </dx:ASPxButton>
            </div>
            <div style="float: right; padding-left: 5px">
                <dx:ASPxComboBox ID="cmbBranch" runat="server" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged"
                    IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" Width="300px"
                    meta:resourcekey="cmbBranchResource2" AutoPostBack="True">
                </dx:ASPxComboBox>
            </div>
            <div class="Note floatleft">
                <span class="red"><strong>Note: </strong></span>Disabled Project(s) has vouchers/balance
                which cannot be unmapped.
            </div>
            <div class="floatright pad5">
                <span class="bold">Branch</span>
            </div>
             <div class="floatright">
                <span class="bold"><dx:ASPxButton ID="btnMapProjects" OnClick="btnMapProjects_Click" runat="server" Height="20px"
                Theme="Office2010Blue" Text="Map Projects" ToolTip="Click here Map Porjects to Branch">
            </dx:ASPxButton></span>
            </div>
        </div>
    </div>
    <div>
        <dx:ASPxGridView ID="gvProject" runat="server" Width="100%" KeyFieldName="PROJECT_ID"
            IncrementalFilteringMode="Contains" OnCommandButtonInitialize="gvProject_CommandButtonInitialize"
            OnLoad="gvProject_Load" SettingsBehavior-AllowDragDrop="false" ClientInstanceName="gvProject"
            Theme="Office2010Blue" AutoGenerateColumns="False">
            <Columns>
                <dx:GridViewCommandColumn Name="colchkSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                    Width="40">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="chkSelectAll" runat="server" ToolTip="Select/Unselect all rows on the page"
                            Theme="Office2010Blue" ClientInstanceName="chkSelectAll">
                            <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                        </dx:ASPxCheckBox>
                    </HeaderTemplate>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn Name="colProject" FieldName="PROJECT" VisibleIndex="1" Caption="Projects">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colProjectCategory" FieldName="PROJECT_CATEGORY" VisibleIndex="2"
                    Caption="Project Category">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Name="colLocation" VisibleIndex="4" Width="170px" Caption="Location"
                    FieldName="LOCATION_ID" Visible="true">
                    <Settings AllowAutoFilter="False"></Settings>
                    <DataItemTemplate>
                        <dx:ASPxComboBox runat="server" ID="cmbLocation" ToolTip="Click here to choose the location"
                            Height="20px" Width="156px" IncrementalFilteringMode="Contains" OnInit="cmbEditData_Init">
                        </dx:ASPxComboBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Name="colProjectId" FieldName="PROJECT_ID" VisibleIndex="0"
                    Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colMappedId" FieldName="MAPPED_PROJECT" VisibleIndex="0"
                    Visible="false" />
                <dx:GridViewDataColumn Name="colSelect" FieldName="SELECT" Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn ShowClearFilterButton="true" ShowApplyFilterButton="false"
                    Width="40px" VisibleIndex="2" Visible="false" />
            </Columns>
            <Settings VerticalScrollBarMode="Hidden" VerticalScrollableHeight="290" ShowFilterRow="true"
                ShowFilterRowMenu="true" />
            <SettingsPager Position="TopAndBottom" PageSize="300">
                <PageSizeItemSettings Items="300,400,500" Visible="false">
                </PageSizeItemSettings>
            </SettingsPager>
            <SettingsBehavior AllowSort="false" AllowFocusedRow="true" />
        </dx:ASPxGridView>
        <br />
        <div class="textcenter" align="center">
            <dx:ASPxButton ID="btnSaveMapping" OnClick="btnSaveMapping_Click" runat="server"
                Theme="Office2010Blue" Text="Save" ToolTip="Click here Map projects to Branch">
            </dx:ASPxButton>
        </div>
    </div>
</asp:Content>
