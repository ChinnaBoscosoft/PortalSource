<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="FDRegisters.aspx.cs" Inherits="AcMeERP.Module.Master.FDRegisters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upFdRegistersView" runat="server">
        <ContentTemplate>
            <div class="criteriaribben">
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Branch </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbBranch" runat="server" AutoPostBack="true" IncrementalFilteringMode="Contains"
                            Width="200px" OnSelectedIndexChanged="cmbBranch_SelectedIndexChanged" Theme="Office2010Blue"
                            TabIndex="1">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Project </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxComboBox ID="cmbProject" runat="server" TabIndex="2" Theme="Office2010Blue">
                        </dx:ASPxComboBox>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Date From </span>
                    </div>
                    <div style="float: left; padding-left: 2px;">
                        <dx:ASPxDateEdit ID="dteDateFrom" runat="server" Width="90px" UseMaskBehavior="True"
                            Theme="Office2010Blue" TabIndex="3" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div>
                    <div style="float: left; padding-top: 2px; padding-left: 5px;">
                        <span class="bold">Date To </span>
                    </div>
                    <div style="float: left; padding-left: 5px;">
                        <dx:ASPxDateEdit ID="dteDateTo" runat="server" Width="90px" UseMaskBehavior="True"
                            Theme="Office2010Blue" TabIndex="4" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                        </dx:ASPxDateEdit>
                    </div>
                </div>
                <div style="float: left; padding-left: 5px; padding-bottom: 2px; padding-right: 5px">
                    <dx:ASPxButton ID="btnLoad" Text="Go" OnClick="btnLoad_Click" Theme="Office2010Blue"
                        TabIndex="5" runat="server" Height="20px" Width="80px">
                        <Image Url="~/App_Themes/MainTheme/images/go.png">
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div>
                <div>
                    <dx:ASPxGridView ID="gvFDRegistersView" runat="server" Theme="Office2010Blue" Width="100%" OnLoad="gvFDRegistersView_Load"
                        AutoGenerateColumns="False" KeyFieldName="FD_ACCOUNT_NUMBER">
                        <Columns>
                            <dx:GridViewDataTextColumn Name="colFDAccount" Caption="FD Account" VisibleIndex="0"
                                FieldName="FD_ACCOUNT_NUMBER">
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colBank" Caption="Bank" FieldName="BANK" VisibleIndex="1">
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colProject" Caption="Project" FieldName="PROJECT"
                                VisibleIndex="2">
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colInvestedOn" Caption="Inv.NO" FieldName="INVESTMENT_DATE"
                                VisibleIndex="3">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colMaturedOn" Caption="Mat.On" FieldName="MATURITY_DATE"
                                VisibleIndex="4">
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colInterestMode" Caption="Int.Mode" FieldName="INTEREST_AMOUNT"
                                VisibleIndex="5">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colInterest" Caption="Int (%)" FieldName="INTEREST_RATE"
                                VisibleIndex="6">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colPrincipleAmount" Caption="Principle Amt" FieldName="PRINCIPLE_AMOUNT"
                                VisibleIndex="7">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colInterestReceived" Caption="Int.Rec" FieldName="INTEREST_AMOUNT"
                                VisibleIndex="8">
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colAccumulatedInterest" Caption="Acc.Int" FieldName="ACCUMULATED_INTEREST_AMOUNT"
                                VisibleIndex="9">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colTotal" Caption="Total Amt" FieldName="TOTAL_AMOUNT"
                                VisibleIndex="10">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colWithdrawalAmount" Caption="Withdrawal Amt" FieldName="WITHDRAWAL_AMOUNT"
                                VisibleIndex="11">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colClosingBalance" Caption="Closing Balance" FieldName="BALANCE_AMOUNT"
                                VisibleIndex="12">
                                <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                                </PropertiesTextEdit>
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="CLOSING_STATUS"
                                VisibleIndex="13">
                                <Settings AutoFilterCondition="Contains"></Settings>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowSort="False" AllowDragDrop="False" AllowFocusedRow="true" />
                        <SettingsPager Position="TopAndBottom" PageSize="20">
                            <PageSizeItemSettings Items="10, 30, 40, 50" Visible="True">
                            </PageSizeItemSettings>
                        </SettingsPager>
                    </dx:ASPxGridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
