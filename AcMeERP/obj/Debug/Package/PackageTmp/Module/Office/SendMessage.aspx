<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="AcMeERP.Module.Office.SendMessage" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
    <script type="text/javascript">
        function chkSelectAll_CheckChanged(s, e) {
            if (s.GetChecked()) {
                var intTopVisibleIndex = gvBranch.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvBranch.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvBranch.name + '_DXSelBtn' + (intTopVisibleIndex + i)).disabled;
                    if (!blnDisabled)
                        gvBranch.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
                }
            }
            else {
                var intTopVisibleIndex = gvBranch.GetTopVisibleIndex();
                var intVisibleRowsOnPage = gvBranch.GetVisibleRowsOnPage();
                for (var i = 0; i < intVisibleRowsOnPage; i++) {
                    var blnDisabled = document.getElementById(gvBranch.name + '_DXSelBtn' + (intTopVisibleIndex + i)).value;
                    //  alert(blnDisabled + "" + intTopVisibleIndex);
                    if (blnDisabled == "U")
                        gvBranch.SelectRowOnPage(intTopVisibleIndex + i, s.GetChecked());
                }
            }


        }
        function SelectionChanged(s, e) {
            chkSelectAll.SetChecked(false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upUserRole" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" GroupingText="Compose Message" runat="server">
                <div class="row-fluid">
                    <div class="row-fluid span5" style="float: left;">
                        <div class="row-fluid">
                            <div class="span5 textright" style="width: 20%">
                                <asp:Literal ID="Literal4" runat="server" Text="Subject *"></asp:Literal>
                            </div>
                            <div class="span7" style="margin-bottom: 12px">
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="textbox" BorderColor="LightBlue"
                                    MaxLength="100" ToolTip="Enter Subject" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfSubject" runat="server" Text="Subject is required"
                                    CssClass="requiredcolor" ErrorMessage="Subject is required" SetFocusOnError="True"
                                    ControlToValidate="txtSubject" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright" style="width: 20%">
                                <asp:Literal ID="Literal2" runat="server" Text="Message *"></asp:Literal>
                            </div>
                            <div class="span7" style="margin-bottom: 12px">
                                <asp:TextBox ID="txtContent" runat="server" CssClass="textbox multiline" BorderColor="LightBlue"
                                    TextMode="MultiLine" ToolTip="Enter Message" Height="170px" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfContent" runat="server" Text="Message is required"
                                    CssClass="requiredcolor" ErrorMessage="Message is required" SetFocusOnError="True"
                                    ControlToValidate="txtContent" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row-fluid" style="padding-left: 110px;">
                            <div class="span7">
                                <div style="float: left; padding-right: 20px;">
                                    <asp:CheckBox ID="chkMail" Text="Email" runat="server" />
                                </div>
                                <asp:CheckBox ID="chkBroadcast" Text="BroadCast" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid span5">
                        <div class="row-fluid">
                            <dx:ASPxGridView ID="gvBranch" Width="700px" Theme="Office2010Blue" IncrementalFilteringMode="Contains"
                                OnLoad="gvBranch_Load" runat="server" KeyFieldName="BRANCH_OFFICE_ID" ClientInstanceName="gvBranch"
                                AutoGenerateColumns="False">
                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                                <Columns>
                                    <dx:GridViewCommandColumn Name="colchkSelect" ShowSelectCheckbox="true" VisibleIndex="0"
                                        Width="20px">
                                        <HeaderTemplate>
                                            <dx:ASPxCheckBox ID="chkSelectAll" runat="server" ToolTip="Select/Unselect all rows on the page"
                                                Theme="Office2010Blue">
                                                <ClientSideEvents CheckedChanged="chkSelectAll_CheckChanged" />
                                            </dx:ASPxCheckBox>
                                        </HeaderTemplate>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataColumn Name="colBranch" FieldName="BRANCH_OFFICE_NAME" VisibleIndex="2"
                                        Caption="Branch" MinWidth="130" Width="130px">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                        <Settings AutoFilterCondition="Contains"></Settings>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Name="colMail" FieldName="Email" VisibleIndex="3" Caption="Email"
                                        MinWidth="250" Width="150px">
                                        <Settings AutoFilterCondition="Contains"></Settings>
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Name="colBranchId" FieldName="BRANCH_OFFICE_ID" VisibleIndex="1"
                                        Visible="false">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Name="colSelect" FieldName="SELECT" VisibleIndex="4" Visible="false">
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="row-fluid" style="float: right">
                <div class="textcenter" align="center">
                    <asp:Button ID="btnSend" runat="server" CssClass="button" Text="Send" ToolTip="Click here to save Location Details"
                        OnClick="btnSend_Click" />
                    <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close" ToolTip="Click here to close this page"
                        CausesValidation="False"></asp:Button>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
