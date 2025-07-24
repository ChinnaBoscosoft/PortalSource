<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="UploadBranchVouchers.aspx.cs" Inherits="AcMeERP.Module.Office.UploadBranchVouchers"
    Culture="auto" UICulture="auto" meta:resourcekey="PageResource2" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <%--to refresh page every after 1 mintue--%>
    <meta http-equiv="refresh" content="60" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben" style="height: 35px">
        <div style="float: right; padding-left: 2px; padding-right: 3px; padding-bottom: 2px;">
            <dx:ASPxButton ID="btnRefresh" runat="server" Image-Url="~/App_Themes/MainTheme/images/file-refresh.png"
                TabIndex="3" OnClick="btnRefresh_Click" Text="Refresh" Height="24px" ToolTip="Refresh"
                meta:resourcekey="btnRefreshResource2">
            </dx:ASPxButton>
        </div>
        <div id="divUploadButton" style="float: right; padding-left: 2px; padding-right: 3px;
            padding-bottom: 2px; display: none;">
            <dx:ASPxButton ID="btnFileUpload" runat="server" Text="Upload File" Height="20px"
                TabIndex="2" ClientInstanceName="btnFileUpload" OnClick="btnFileUpload_Click"
                ToolTip="Upload File" Image-Url="~/App_Themes/MainTheme/images/uploadfile.jpg"
                meta:resourcekey="btnFileUploadResource2">
            </dx:ASPxButton>
        </div>
        <div style="float: right; padding-left: 2px; padding-right: 3px;">
            <dx:ASPxUploadControl ID="UlcFileUpload" runat="server" Size="30" ShowProgressPanel="True"
                TabIndex="1" ClientInstanceName="UlcFileUpload" NullText="Click here browse files"
                meta:resourcekey="UlcFileUploadResource2">
                <ValidationSettings AllowedFileExtensions=".Xml,.xml" MaxFileSize="26000000">
                </ValidationSettings>
                <ClientSideEvents TextChanged="function(s,e){ ValidateUpload(); }" />
            </dx:ASPxUploadControl>
        </div>
        <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" UploadMode="Auto" Width="280px">
        </dx:ASPxUploadControl>
    </div>
    <asp:UpdatePanel ID="upSynchronization" runat="server">
        <ContentTemplate>
            <div>
                <div>
                    <dx:ASPxGridView ID="gvSynchronizationStatus" runat="server" Width="100%" Theme="Office2010Blue"
                        ClientInstanceName="gvSynchronizationStatus" KeyFieldName="HEAD_OFFICE_CODE"
                        OnLoad="gvSynchronizationStatus_Load" AutoGenerateColumns="False" meta:resourcekey="gvSynchronizationStatusResource2">
                        <Columns>
                            <dx:GridViewDataColumn Name="colHeadOfficeCode" Caption="H.O.Code" FieldName="HEAD_OFFICE_CODE"
                                VisibleIndex="4" Width="10%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colBranchOfficeCode" Caption="B.O.Code" FieldName="BRANCH_OFFICE_CODE"
                                VisibleIndex="5" Width="1%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colBranchOffice" Caption="Branch" FieldName="BRANCH_OFFICE_NAME"
                                VisibleIndex="6" Width="15%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn Name="colDateFrom" Caption="Date From" FieldName="DATE_FROM"
                                VisibleIndex="7" Width="1%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colDateTo" Caption="Date To" FieldName="DATE_TO"
                                VisibleIndex="8" Width="1%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn Name="colUploadOn" Caption="Upload On" FieldName="UPLOADED_ON"
                                Width="5%" VisibleIndex="9">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colStartedOn" Caption="Started On" FieldName="STARTED_ON"
                                Width="9%" VisibleIndex="10" Visible="false">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colCompletedOn" Caption="Completed On" FieldName="COMPLETED_ON"
                                Width="9%" VisibleIndex="11" Visible="false">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colXMLFileName" Caption="File Name" FieldName="XML_FILENAME"
                                Visible="False">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colBranchLocation" Caption="Location" FieldName="LOCATION"
                                VisibleIndex="12" Width="3%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colUploadedBy" Caption="Uploaded By" FieldName="UPLOADED_BY"
                                VisibleIndex="13" Width="2%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colStatus" Caption="Status" FieldName="STATUS" VisibleIndex="14"
                                Width="1%">
                                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList"></Settings>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colMail" VisibleIndex="15" Width="2%" Caption="Mail">
                                <DataItemTemplate>
                                    <asp:ImageButton ID="BtnEmail" ImageUrl="../../app_themes/maintheme/images/gmail1.png"
                                        runat="server" OnClientClick="ShowWindow()" ToolTip="Click to send mail" meta:resourcekey="BtnEmailResource2" />
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colRemarks" Caption="remarks" FieldName="REMARKS" Visible="False"
                                VisibleIndex="0">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colProjects" Caption="Project" FieldName="PROJECT" Visible="False"
                                VisibleIndex="16">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colHeadOfficeId" Caption="Head Office Id" FieldName="HEAD_OFFICE_ID"
                                Visible="False" VisibleIndex="1">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colBranchOfficeId" Caption="Branch Office Id" FieldName="BRANCH_OFFICE_ID"
                                Visible="False" VisibleIndex="2">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Name="colStatusId" Caption="Status Id" FieldName="ID" Visible="False"
                                VisibleIndex="3">
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowApplyFilterButton="True"
                                Width="10px" VisibleIndex="14" Visible="False" />
                        </Columns>
                        <SettingsBehavior AllowSort="False" AllowFocusedRow="True" />
                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="true" />
                        <ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }"
                            CustomButtonClick="function(s,e){ ShowWindow(); }" />
                        <SettingsPager Position="TopAndBottom">
                            <PageSizeItemSettings Items="10,20,30,40,50" Visible="True">
                            </PageSizeItemSettings>
                        </SettingsPager>
                    </dx:ASPxGridView>
                </div>
                <div>
                    <div>
                        <span class="bold" style="color: Teal">&nbsp;</span></div>
                    <div>
                        <span class="bold" style="color: Teal">Uploaded Projects</span>
                    </div>
                    <div>
                        <dx:ASPxMemo ID="meoProjects" Width="100%" runat="server" ClientInstanceName="meoProjects"
                            ForeColor="Red" Height="40px" ReadOnly="True" MaxLength="250" Theme="Office2010Blue"
                            meta:resourcekey="meoRemarksResource2" />
                    </div>
                    <div>
                        &nbsp;</div>
                    <div>
                        <span class="bold" style="color: Teal">Remarks</span>
                    </div>
                    <div>
                        <dx:ASPxMemo ID="meoRemarks" Width="100%" runat="server" ClientInstanceName="meoRemarks"
                            ForeColor="Red" Height="40px" ReadOnly="True" MaxLength="250" Theme="Office2010Blue"
                            meta:resourcekey="meoRemarksResource2" />
                    </div>
                    <div>
                        <dx:ASPxLabel ID="lblReceive" Text="(Received) - File is received and scheduled for synchronization"
                            CssClass="subtitlebar" runat="server" meta:resourcekey="lblReceiveResource1">
                        </dx:ASPxLabel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <dx:ASPxPopupControl ID="pcMailContent" runat="server" CloseAction="CloseButton"
            Theme="Office2010Blue" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            ClientInstanceName="pcMailContent" HeaderText="E-Mail" AllowDragging="True" PopupAnimationType="None"
            EnableViewState="False" meta:resourcekey="pcMailContentResource2">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" meta:resourcekey="PopupControlContentControl1Resource2">
                    <dx:ASPxPanel ID="Panel1" runat="server" Height="135px" Width="355px" Theme="Office2010Blue"
                        meta:resourcekey="Panel1Resource2">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server" meta:resourcekey="PanelContent1Resource2">
                                <div>
                                    <div class="divrow">
                                        <div class="divcolpad">
                                            <dx:ASPxMemo ID="memContent" runat="server" Height="90px" Width="320px" Theme="Office2010Blue"
                                                ClientInstanceName="memContent" EnableClientSideAPI="true" NullText="Enter the message here"
                                                meta:resourcekey="memContentResource2">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="true">
                                                    <RequiredField IsRequired="true" ErrorText="Message is required" />
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s,e){ CheckMessageEmpty(); }" />
                                            </dx:ASPxMemo>
                                        </div>
                                    </div>
                                    <div class="divrow">
                                        <div class="divcolpad" style="padding-left: 275px;">
                                            <dx:ASPxButton ID="btnSend" runat="server" ClientInstanceName="btnSend" Text="Send"
                                                Width="23px" Theme="Office2010Blue" meta:resourcekey="btnSendResource2" ToolTip="Click to send mail">
                                                <ClientSideEvents Click="function(s,e){ HideWindow(); }" />
                                            </dx:ASPxButton>
                                        </div>
                                    </div>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
    <script type="text/javascript" language="javascript">
        function ShowWindow() {
            memContent.SetText("");
            pcMailContent.Show();
        }
        function HideWindow() {
            if (memContent.GetText() == '') {
            } else {
                pcMailContent.Hide();
            }
        }

        function OnGridFocusedRowChanged() {
            meoRemarks.SetText('');
            meoProjects.SetText('');
            gvSynchronizationStatus.GetRowValues(gvSynchronizationStatus.GetFocusedRowIndex(), 'REMARKS', OnGetRowValues);
            gvSynchronizationStatus.GetRowValues(gvSynchronizationStatus.GetFocusedRowIndex(), 'PROJECT', OnGetRowProjectValues);
        }

        function OnGetRowValues(values) {
            meoRemarks.SetText(values.toString());
        }

        function OnGetRowProjectValues(values) {
            meoProjects.SetText(values.toString());
        }

        function CheckMessageEmpty(s, e) {
            var msg = e.value;
            if (msg == null || msg == "")
                e.isValid = false;
        }
        function ValidateUpload() {
            if (UlcFileUpload.GetText(0) == "") {
                document.getElementById('divUploadButton').style.display = 'none';
            } else {
                document.getElementById('divUploadButton').style.display = 'block';
            }
        }
    </script>
</asp:Content>
