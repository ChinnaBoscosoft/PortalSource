<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="BranchUserMapping.aspx.cs" Inherits="AcMeERP.Module.Office.BranchUserMapping"
    meta:resourcekey="PageResource1" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function chkSelectAll_CheckChanged(s, e) {
            if (s.GetChecked()) {
                var intTopVisibleIndex = gvUserBranch.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvUserBranch.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvUserBranch.name + '_DXSelBtn' + (intTopVisibleIndex + i)).disabled;
                    if (!blnDisabled)
                        gvUserBranch.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
                }
            }
            else {
                var intTopVisibleIndex = gvUserBranch.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvUserBranch.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvUserBranch.name + '_DXSelBtn' + (intTopVisibleIndex + i)).value;
                    //  alert(blnDisabled + "" + intTopVisibleIndex);
                    if (blnDisabled == "U")
                        gvUserBranch.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
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
                    Height="20px" Text="Save" ToolTip="Click here Map Branch to User" meta:resourcekey="bntSaveOnTopResource1">
                </dx:ASPxButton>
            </div>
            <div style="float: right; padding-left: 5px">
                <dx:ASPxComboBox ID="cmbUser" runat="server" OnSelectedIndexChanged="cmbUser_SelectedIndexChanged"
                    IncrementalFilteringMode="Contains" Width="300px" AutoPostBack="True" meta:resourcekey="cmbUserResource1">
                </dx:ASPxComboBox>
            </div>
            <div class="Note floatleft">
                <span class="red"><strong>Note: </strong></span>Only Branch which are mapped to
                the user, will be accessible by the user.
            </div>
            <div class="floatright pad5">
                <span class="bold">User</span>
            </div>
        </div>
    </div>
    <div>
        <dx:ASPxGridView ID="gvUserBranch" runat="server" Width="100%" KeyFieldName="BRANCH_OFFICE_ID"
            IncrementalFilteringMode="Contains" OnLoad="gvUserBranch_Load" SettingsBehavior-AllowDragDrop="false"
            ClientInstanceName="gvUserBranch" Theme="Office2010Blue" AutoGenerateColumns="False"
            meta:resourcekey="gvUserBranchResource1">
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
                <dx:GridViewDataColumn Name="colBranch" FieldName="BRANCH" VisibleIndex="1" Caption="Branch">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colBranchId" FieldName="BRANCH_OFFICE_ID" VisibleIndex="0"
                    Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colSelect" FieldName="SELECT" Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn ShowClearFilterButton="true" ShowApplyFilterButton="false"
                    Width="40px" VisibleIndex="2" Visible="false" />
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
                Theme="Office2010Blue" Text="Save" ToolTip="Click here Map Branch to User" meta:resourcekey="btnSaveMappingResource1">
            </dx:ASPxButton>
        </div>
    </div>
</asp:Content>
