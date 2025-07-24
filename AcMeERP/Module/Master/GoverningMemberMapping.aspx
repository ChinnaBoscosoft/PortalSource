<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master" AutoEventWireup="true" CodeBehind="GoverningMemberMapping.aspx.cs" Inherits="AcMeERP.Module.Master.GoverningMemberMapping" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
 <script type="text/javascript">
     function chkSelectAll_CheckChanged(s, e) {
         gvLedger.SelectAllRowsOnPage(s.GetChecked());
     }
     function SelectionChanged(s, e) {
         colSelectAll.SetChecked(false);
     }
     function DisableSave(s, e) {
         var count = gvLedger.GetSelectedRowCount();
         if (count > 0) {
             document.getElementById('divSave').style.display = 'block';
         }
         else {
             document.getElementById('divSave').style.display = 'none';
         }
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
  <div>
        <div class="criteriaribben">
            <div class="floatright" style="padding-right: 5px; padding-left: 5px">
                <dx:ASPxButton ID="btnSaveOnTop" Text="Save" OnClick="btnSaveOnTop_Click" Height="20px" Theme="Office2010Blue" 
                    runat="server" />
            </div>
            <div class="floatright" style="padding-right: 5px">
                <dx:ASPxComboBox ID="cmbLegalEntity" runat="server" OnSelectedIndexChanged="cmbLegalEntity_IndexChanged" IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" AutoPostBack="true"
                    Width="250px">
                </dx:ASPxComboBox>
            </div>
            <div class="floatright">
                <span class="bold" style="padding: 5px;">Society Name</span>
            </div>
        </div>
        <div>
            <dx:ASPxGridView ID="gvGoverningMember" runat="server" SettingsBehavior-AllowDragDrop="false"
                IncrementalFilteringMode="Contains" Settings-ShowFilterRow="true" Theme="Office2010Blue"
                Width="100%" OnLoad="gvGoverningMember_Load" ClientInstanceName="gvLedger" KeyFieldName="EXECUTIVE_ID"
                AutoGenerateColumns="False">
                <Columns>
                    <dx:GridViewCommandColumn Name="ColSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                        Width="25px">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="colSelectAll" runat="server" ClientInstanceName="colSelectAll"
                                Theme="Office2010Blue" ToolTip="Select/Unselect all rows on the page">
                                <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn Name="colName" Caption="Name" VisibleIndex="1" FieldName="EXECUTIVE">
                     <Settings AutoFilterCondition="Contains"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colRole" Caption="Role" VisibleIndex="2"
                        FieldName="ROLE">
                         <Settings AutoFilterCondition="Contains"></Settings>
                    </dx:GridViewDataColumn>
                     <dx:GridViewDataColumn Name="colDateJoin" Caption="Date of Joining" VisibleIndex="3"
                        FieldName="Date of Joining">
                         <Settings AutoFilterCondition="Contains"></Settings>
                    </dx:GridViewDataColumn>
                     <dx:GridViewDataColumn Name="colDateExit" Caption="Date of Exit" VisibleIndex="4"
                        FieldName="Date of Exit">
                         <Settings AutoFilterCondition="Contains"></Settings>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="colExecutiveId" FieldName="EXECUTIVE_ID" Visible="false">
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsPager Position="TopAndBottom" PageSize="300">
                    <PageSizeItemSettings Items="300,400,500" Visible="True">
                    </PageSizeItemSettings>
                </SettingsPager>
                <SettingsBehavior AllowSort="true" />
                <Settings ShowFilterRow="True" ShowFilterRowMenu="true"></Settings>
            </dx:ASPxGridView>
        </div>
        <br />
        <div id="divSave" class="textcenter">
            <dx:ASPxButton ID="btnSave" Text="Save" OnClick="btnSave_Click" Theme="Office2010Blue" 
                runat="server" />
        </div>
    </div>
</asp:Content>
