<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="ExcuteDatabaseQuery.aspx.cs" Inherits="AcMeERP.Module.Office.ExcuteDatabaseQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1">
            <asp:UpdatePanel ID="upExcuteDBQuery" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="row-fluid">
                            <div class="span2 textright padright25">
                                <asp:Literal ID="ltrlHeadofficeName" runat="server" Text="Head Office *"></asp:Literal>
                            </div>
                            <div class="span5">
                                <asp:DropDownList ID="ddlHeadOffice" ToolTip="Select Head Office" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlHeadOffice_SelectedIndexChanged"
                                    CssClass="combobox manfield open" Width="255px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="cmpVHo" runat="server" ErrorMessage="Head Office is required"
                                    CssClass="requiredcolor" ControlToValidate="ddlHeadOffice" Operator="NotEqual"
                                    ValueToCompare="-Select-" Text="*"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 clearfix normitalic pad5 ">
                                Place Insert, update , Delete and Alter queries can be executed (Each query must
                                be ended with (;)).
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:TextBox ID="txtQuery" runat="server" CssClass="textbox multiline" Width="968px"
                                    Height="170px" onkeypress="return textboxMultilineMaxNumber(event,this,250);"
                                    Text="" ToolTip="Enter Query" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfMsg" runat="server" CssClass="requiredcolor" Text="Query is required"
                                    ErrorMessage="Query is required" SetFocusOnError="True" ControlToValidate="txtQuery"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="textcenter">
                            <asp:Button ID="btnExecute" Text="Execute" runat="server" CssClass="button" OnClick="btnExecute_Click"
                                CausesValidation="false" />
                            <asp:Button ID="btnExecuteClear" Text="Clear" runat="server" CssClass="button" OnClick="btnExecuteClear_Click" />
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="row-fluid">
                            <div class="span5 clearfix normitalic pad5 ">
                                Please Place only "Select" query (Each query must be ended with (;)).
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span5 textright padright25">
                                <asp:TextBox ID="txtSelectQuery" runat="server" CssClass="textbox multiline" Width="968px"
                                    Height="170px" onkeypress="return textboxMultilineMaxNumber(event,this,250);"
                                    Text="" ToolTip="Enter Query" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSelect" runat="server" CssClass="requiredcolor"
                                    Text="Select query is required" ErrorMessage="Select query is required" SetFocusOnError="True"
                                    ControlToValidate="txtSelectQuery" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="textcenter">
                            <asp:Button ID="btnSelect" Text="Get Records" runat="server" CssClass="button" OnClick="btnSelect_Click"
                                CausesValidation="false" />
                            <asp:Button ID="btnSelectClear" Text="Clear" runat="server" CssClass="button" OnClick="btnSelectClear_Click" />
                        </div>
                        <div class="row-fluid">
                            <asp:GridView ID="gvFetchData" runat="server" AutoGenerateColumns="True" SkinID="Rpt_Criteria"
                                GridLines="None">
                            </asp:GridView>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:Content>
