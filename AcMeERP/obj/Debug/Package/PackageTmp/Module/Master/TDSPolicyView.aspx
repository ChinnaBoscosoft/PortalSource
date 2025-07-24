<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="TDSPolicyView.aspx.cs" Inherits="AcMeERP.Module.Master.TDSPolicyView" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="criteriaribben">
        <div style="width: 35%;">
            <div style="float: left; padding-top: 2px; padding-left: 5px;">
                <span class="bold">Deductee Type </span>
            </div>
            <div style="float: left; padding-left: 5px;">
                <dx:ASPxComboBox ID="cmbDeducteeType" runat="server" IncrementalFilteringMode="Contains"
                    TabIndex="1" Theme="Office2010Blue" OnSelectedIndexChanged="cmbDeducteeType_SelectedIndexChanged"
                    AutoPostBack="True" Width="200px">
                </dx:ASPxComboBox>
            </div>
        </div>
        <div style="width: 15%; float: left">
            <div class="floatright" style="padding-left: 0px; padding-right: 0px;">
                Status
                <asp:Label ID="lblStatus" runat="server" Text="" CssClass="bold" ForeColor="Green"
                    Font-Size="12px"></asp:Label>
            </div>
        </div>
        <div style="width: 20%; float: left">
            <div class="floatright" style="padding-left: 0px; padding-right: 0px;">
                Deductee Status
                <asp:Label ID="lblDeducteeStatus" runat="server" Text="" CssClass="bold" ForeColor="Green"
                    Font-Size="12px"></asp:Label>
            </div>
        </div>
        <div style="width: 20%; float: left">
            <div class="floatright" style="padding-left: 0px; padding-right: 0px;">
                Resident Status
                <asp:Label ID="lblResidentStatus" runat="server" Text="" CssClass="bold" ForeColor="Green"
                    Font-Size="12px"></asp:Label>
            </div>
        </div>
        <div style="width: 10%; float: left">
            <div class="floatright"  >
                <asp:ImageButton ID="imgExport" runat="server" AlternateText="Refresh" OnClick="imgExport_Click"
                    CausesValidation="false" SkinID="excel_ib" />
            </div>
            <div class="floatright" style="padding-left: 0px; padding-right: 8px;">
                Export
            </div>
        </div>
    </div>
    <div>
        <dx:ASPxGridView ID="gvTDSPolicy" runat="server" Theme="Office2010Blue" Width="100%"
            OnLoad="gvTDSPolicy_Load" AutoGenerateColumns="false" KeyFieldName="NATURE_PAY_ID">
            <Styles FocusedGroupRow-Font-Bold="true">
            </Styles>
            <Columns>
                <dx:GridViewBandColumn>
                    <Columns>
                        <dx:GridViewDataTextColumn Name="colNatureOfpayments" Caption="Nature of Payments"
                            Width="100px" FieldName="NATURE_OF_PAYMENTS">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn>
                    <Columns>
                        <dx:GridViewDataTextColumn Name="colApplicableFrom" Caption="Applicable From" FieldName="APPLICABLE_FROM"
                            Width="100px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="TDS With PAN">
                    <Columns>
                        <dx:GridViewDataTextColumn Name="colRate" Caption="Rate(%)" FieldName="TDS_RATE"
                            Width="80px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Name="colExemptionLimit" Caption="Exemption Limit" FieldName="TDS_EXEMPTION_LIMIT"
                            Width="150px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="TDS Without PAN">
                    <Columns>
                        <dx:GridViewDataTextColumn Name="colRate" Caption="Rate(%)" FieldName="TDSRATE_WITHOUT_PAN"
                            Width="80px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Name="colExemptionLimit" Caption="Exemption Limit" FieldName="TDSEXEMPTION_LIMIT_WITHOUT_PAN"
                            Width="150px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Surchage">
                    <Columns>
                        <dx:GridViewDataTextColumn Name="colRate" Caption="Rate(%)" FieldName="SUR_RATE"
                            Width="80px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Name="colExemptionLimit" Caption="Exemption Limit" FieldName="SUR_EXEMPTION"
                            Width="150px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Ed Cess">
                    <Columns>
                        <dx:GridViewDataTextColumn Name="colRate" Caption="Rate(%)" FieldName="ED_CESS_RATE"
                            Width="80px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Name="colExemptionLimit" Caption="Exemption Limit" FieldName="ED_CESS_EXEMPTION"
                            Width="150px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Sec Ed Cess">
                    <Columns>
                        <dx:GridViewDataTextColumn Name="colRate" Caption="Rate(%)" FieldName="SEC_ED_CESS_RATE"
                            Width="80px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Name="colExemptionLimit" Caption="Exemption Limit" FieldName="SEC_ED_CESS_EXEMPTION"
                            Width="110px">
                            <PropertiesTextEdit DisplayFormatString="{0:n2}" NullDisplayText="0.00">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
            </Columns>
            <SettingsBehavior AllowSort="False" AllowDragDrop="false" AllowFocusedRow="True"
                AllowGroup="true" />
            <Settings ShowFilterRow="True" ShowGroupPanel="false" VerticalScrollableHeight="500"
                VerticalScrollBarMode="Visible" />
            <%--<SettingsPager Position="TopAndBottom" PageSize="20">
                <PageSizeItemSettings Items="30, 40, 50" Visible="True">
                </PageSizeItemSettings>
            </SettingsPager>--%>
            <SettingsPager Mode="ShowAllRecords" />
        </dx:ASPxGridView>
    </div>
</asp:Content>
