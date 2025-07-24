<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ProjectUserMapping.aspx.cs" Inherits="AcMeERP.Module.Office.ProjectUserMapping"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function chkSelectAll_CheckChanged(s, e) {
            if (s.GetChecked()) {
                var intTopVisibleIndex = gvUserProject.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvUserProject.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvUserProject.name + '_DXSelBtn' + (intTopVisibleIndex + i)).disabled;
                    if (!blnDisabled)
                        gvUserProject.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
                }
            }
            else { // To change gvProject to gvUserProject for this function alone ( 10.10.2022)
                //var intTopVisibleIndex = gvProject.GetTopVisibleIndex();
                //var intVisibleRowsOnPage = gvProject.GetVisibleRowsOnPage();
                var intTopVisibleIndex = gvUserProject.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvUserProject.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvUserProject.name + '_DXSelBtn' + (intTopVisibleIndex + i)).value;
                    //  alert(blnDisabled + "" + intTopVisibleIndex);
                    if (blnDisabled == "U")
                        gvUserProject.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
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
                    Height="20px" Text="Save" ToolTip="Click here Map projects to User" meta:resourcekey="bntSaveOnTopResource1">
                </dx:ASPxButton>
            </div>
            <div style="float: right; padding-left: 5px">
                <dx:ASPxComboBox ID="cmbUser" runat="server" OnSelectedIndexChanged="cmbUser_SelectedIndexChanged"
                    IncrementalFilteringMode="Contains" Width="300px" AutoPostBack="True" meta:resourcekey="cmbUserResource1">
                </dx:ASPxComboBox>
            </div>
            <div class="Note floatleft">
                <span class="red"><strong>Note: </strong></span>Only Projects which are mapped to
                the user, will be accessible by the user.
            </div>
            <div class="floatright pad5">
                <span class="bold">User</span>
            </div>
        </div>
    </div>
    <div>
        <dx:ASPxGridView ID="gvUserProject" runat="server" Width="100%" KeyFieldName="PROJECT_ID"
            IncrementalFilteringMode="Contains" OnLoad="gvUserProject_Load" SettingsBehavior-AllowDragDrop="false"
            ClientInstanceName="gvUserProject" Theme="Office2010Blue" AutoGenerateColumns="False"
            meta:resourcekey="gvUserProjectResource1">
            <Columns>
                <dx:GridViewCommandColumn Name="colchkSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                    Width="40">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="chkSelectAll" runat="server" ToolTip="Select/Unselect all rows on the page"
                            Theme="Office2010Blue" ClientInstanceName="chkSelectAll" CheckState="Unchecked"
                            meta:resourcekey="chkSelectAllResource1">
                            <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                        </dx:ASPxCheckBox>
                    </HeaderTemplate>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn Name="colProject" FieldName="PROJECT" VisibleIndex="2" Caption="Projects">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colProjectCategory" FieldName="PROJECT_CATEGORY" VisibleIndex="3"
                    Caption="Project Category">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colProjectId" FieldName="PROJECT_ID" VisibleIndex="1"
                    Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colSelect" FieldName="SELECT" Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn ShowClearFilterButton="true" ShowApplyFilterButton="false"
                    Width="40px" VisibleIndex="5" Visible="false" />
                <dx:GridViewDataTextColumn Caption="BRANCH" FieldName="BRANCH" Name="colBranch" VisibleIndex="4"
                    Width="300">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings VerticalScrollBarMode="Hidden" VerticalScrollableHeight="290" ShowFilterRow="true"
                ShowFilterRowMenu="true" />
            <SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowFocusedRow="True">
            </SettingsBehavior>
            <SettingsPager Position="TopAndBottom" PageSize="300">
                <PageSizeItemSettings Items="300,400,500" Visible="True">
                </PageSizeItemSettings>
            </SettingsPager>
            <SettingsBehavior AllowSort="false" AllowFocusedRow="true" />
            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" VerticalScrollableHeight="290"
                ShowHeaderFilterButton="true"></Settings>
        </dx:ASPxGridView>
        <br />
        <div class="textcenter" align="center">
            <dx:ASPxButton ID="btnSaveMapping" OnClick="btnSaveMapping_Click" runat="server"
                Theme="Office2010Blue" Text="Save" ToolTip="Click here Map projects to User"
                meta:resourcekey="btnSaveMappingResource1">
            </dx:ASPxButton>
        </div>
    </div>
</asp:Content>
