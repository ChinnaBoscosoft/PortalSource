<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="LCBranchEnableTrackRequests.aspx.cs" Inherits="AcMeERP.Module.Office.LCBranchEnableTrackRequests" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function showNotification(inx) {
            alert(inx);
            e.processOnServer = false;  
        }  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="height:50px;">
            <div class="Note floatleft" style="padding-top:3px; padding-left:3px";>
                <span class="red"><strong>Note: </strong></span>Multi Location/Client Server Branches alone will have more than one approved requests.
                All the previous requests should be disabled. 
                <br />For Standalone : allowed only one approved requests.
                <br />For Client Server : allowed only one approved based on Individual Client System requests.
            </div>
            <div class="floatleft pad5" style="padding-top: 8px">
                <span class="bold">Branch</span>
            </div>
            <div style="float: left; padding-left: 5px;padding-top: 5px">
                <dx:ASPxComboBox ID="cmbBranch" runat="server" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged"
                    IncrementalFilteringMode="Contains" DropDownStyle="DropDownList" Width="300px"
                    meta:resourcekey="cmbBranchResource2" AutoPostBack="True">
                </dx:ASPxComboBox>
            </div>
            <div class="floatleft" style="padding:3px;padding-top: 5px">
                <asp:ImageButton ID="imgRefresh" runat="server" 
                    ImageUrl="~/App_Themes/MainTheme/images/captcha-refresh.png" 
                    onclick="imgRefresh_Click"/>&nbsp;&nbsp;
            </div>

            <div class="floatleft" style="padding-top: 3px">
            <dx:ASPxButton ID="btnSaveLCBranchRequest1" OnClick="btnSaveLCBranchRequest_Click" runat="server"
                Theme="Office2010Blue" Text="Save" ToolTip="Click here to save all the requests to enable & track Local Branch's Receipt Module">
            </dx:ASPxButton>
            </div>
             <div class="floatleft"> <dx:ASPxButton ID="btnClearAll" 
                     runat="server"
                Theme="Office2010Blue" Text="Clear All Branch Requests" 
                     ToolTip="Click here to clear all the Branchs requests" 
                     onclick="btnClearAll_Click" Visible="false">
                     <ClientSideEvents Click="function(s, e) {  
                            e.processOnServer = confirm('Are you sure to clear all the Branchs requests ?');  
                    }" /> 
            </dx:ASPxButton>&nbsp&nbsp</div>
            
            <div class="floatleft"> <dx:ASPxButton ID="btnClearCurrentBranch" runat="server"
                Theme="Office2010Blue" Text="Clear Current Branch Requests" 
                    ToolTip="Click here to clear all the selected Branch requests" 
                    onclick="btnClearCurrentBranch_Click" Visible="True">
                <ClientSideEvents Click="function(s, e) {  
                            e.processOnServer = confirm('Are you sure to clear selected Branch requests ?');  
                    }" /> 
            </dx:ASPxButton></div>
            
    </div>
    <div class="textcenter" align="center">&nbsp</div>
    <div>
        <dx:ASPxGridView ID="gvLCBranchEnableRequests" runat="server" Width="100%" KeyFieldName="LC_LICENSE_REQUEST_CODE"
            OnCommandButtonInitialize="gvProject_CommandButtonInitialize"
            OnLoad="gvProject_Load" SettingsBehavior-AllowDragDrop="false" ClientInstanceName="gvLCBranchEnableRequests"
            Theme="Office2010Blue" AutoGenerateColumns="False" 
            onhtmlrowprepared="gvLCBranchEnableRequests_HtmlRowPrepared">
            <Columns>
                <dx:GridViewDataColumn FieldName="LC_BRANCH_REQUESTED_ON" Caption="Requested On" VisibleIndex="0"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="LC_BRANCH_REQUESTED_BY" Caption="Requested By" VisibleIndex="1"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCLicenseRequestCode" FieldName="LC_BRANCH_REQUEST_CODE" VisibleIndex="2" Caption="Request Code" Visible="true">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCLicenseKey" FieldName="LC_BRANCH_LICENSE_KEY_NUMBER" VisibleIndex="3" Caption="License Key" Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCHeadOfficeCode" FieldName="LC_HEAD_OFFICE_CODE" VisibleIndex="4" Caption="Head Office Code" Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCBranchCode" FieldName="LC_BRANCH_OFFICE_CODE" VisibleIndex="5" Caption="Branch Code">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCBranchName" FieldName="LC_BRANCH_OFFICE_NAME" VisibleIndex="6" Caption="Branch Name">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCLocation" FieldName="LC_BRANCH_LOCATION" VisibleIndex="7" Caption="Location">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCBranchDepolymentType" FieldName="DEPLOYMENT_TYPE_NAME" VisibleIndex="8" Caption="Depolyment Type">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCClientIPaddress" FieldName="LC_BRANCH_CLIENT_IP_ADDRESS" VisibleIndex="9" Caption="IP Address">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCClientMACaddress" FieldName="LC_BRANCH_CLIENT_MAC_ADDRESS" VisibleIndex="10" Caption="MAC Address">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="colLCReceiptModuleStatus" FieldName="LC_BRANCH_RECEIPT_MODULE_STATUS_NAME" VisibleIndex="11" Caption="Receipt Module Status" Visible="false">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="PORTAL_UPDATED_ON" Caption="Updated On" VisibleIndex="12" Visible="false"></dx:GridViewDataColumn>
                 <dx:GridViewDataTextColumn Name="colReceiptModuleAction" VisibleIndex="13" Caption="Receipt Module Status" 
                 FieldName="LC_BRANCH_RECEIPT_MODULE_STATUS" HeaderStyle-HorizontalAlign ="Center" Width="250px">
                    <DataItemTemplate>
                   <%-- <dx:ASPxRadioButtonList ID="raReceiptModuleStatus" runat="server">
                        <Items>
                            <dx:ListEditItem Value="0" Text="Disabled"/>
                            <dx:ListEditItem Value="1" Text="Requested">
                            <dx:ListEditItem Value="2" Text="Approved"/>
                        </Items>
                    </dx:ASPxRadioButtonList>--%>
                    <dx:ASPxRadioButtonList ID="raReceiptModuleStatus" ClientInstanceName = "raReceiptModuleStatus" runat="server" Width="250px" RepeatLayout="Table" RepeatColumns="3" OnInit="raReceiptModuleStatus_Init">
                        <Items>
                            <dx:ListEditItem Value="0"  Text="Disabled"/>
                            <dx:ListEditItem Value="1" Text="Requested"/>
                            <dx:ListEditItem Value="2" Text="Approved"/>
                        </Items>
                    </dx:ASPxRadioButtonList>

                    </DataItemTemplate>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                
            </Columns>
            <SettingsEditing Mode="Inline">
            </SettingsEditing>
            <Settings VerticalScrollBarMode="Hidden" VerticalScrollableHeight="290" />
            <SettingsPager Position="TopAndBottom" PageSize="300">
                <PageSizeItemSettings Items="300,400,500" Visible="false">
                </PageSizeItemSettings>
            </SettingsPager>
            <SettingsBehavior AllowSort="false" AllowFocusedRow="false" />
        </dx:ASPxGridView>
        <br />
        <div class="textcenter" align="center">
            <dx:ASPxButton ID="btnSaveLCBranchRequest" OnClick="btnSaveLCBranchRequest_Click" runat="server"
                Theme="Office2010Blue" Text="Save" ToolTip="Click here Save Local Branch Requests to enable/disable Receipt Module">
            </dx:ASPxButton>
        </div>
    </div>
</asp:Content>
