<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewControl.ascx.cs"
    Inherits="AcMeERP.WebControl.GridViewControl" %>
<div style="padding-top: 2px; width: 100%; float: left;">
    <div id="toolBar" runat="server" style="width: 100%; float: left">
        <div style="width: 100%; float: left">
            <div class="divrow toolbar">
                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:Literal ID="ltFilter" runat="server" Text="Filter"></asp:Literal>
                </div>
                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                    <asp:DropDownList ID="cboFilter" runat="server" Width="100px" Font-Size="11px" CssClass="combobox">
                    </asp:DropDownList>
                </div>
                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                    <asp:TextBox ID="txtFilter" runat="server" EnableTheming="False" Width="100px" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                    <asp:ImageButton ID="imgFilterGo" runat="server" AlternateText="Ok" OnClick="imgFilterGo_Click"
                        CausesValidation="false" SkinID="ok_ib" />
                </div>
                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:ImageButton ID="imgRefresh" runat="server" AlternateText="Refresh" OnClick="imgRefresh_Click"
                        CausesValidation="false" SkinID="refresh_ib" />
                </div>
                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:ImageButton ID="imgExport" runat="server" AlternateText="Refresh" OnClick="imgExport_Click"
                        CausesValidation="false" Visible="false" SkinID="excel_ib" />
                </div>
                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:Literal ID="ltFilterExternal" runat="server" Text="Filter"></asp:Literal>
                </div>
                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                    <asp:DropDownList ID="cboFilterExternal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboFilterExternal_SelectedIndexChanged"
                        CssClass="combobox" Font-Size="11px">
                    </asp:DropDownList>
                </div>
                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:Literal ID="ltPage" runat="server" Text="Page"></asp:Literal>
                </div>
                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 2px;">
                    <asp:DropDownList ID="cboPage" runat="server" Font-Size="11px" AutoPostBack="True"
                        OnSelectedIndexChanged="cboPage_SelectedIndexChanged" Width="100px" CssClass="combobox">
                    </asp:DropDownList>
                </div>
                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:Literal ID="ltRecord" runat="server" Text=""></asp:Literal>
                </div>
                <div style="float: right; padding-right: 5px; padding-top: 2px; text-align: center;">
                    <asp:HyperLink ID="hlkAdd" CssClass="button" runat="server" Target="_self" Visible="False">Add</asp:HyperLink>
                    <asp:LinkButton ID="lbAdd" CssClass="button" runat="server" OnClick="lbAdd_Click">Add</asp:LinkButton>
                </div>
                <div class="clearfloat">
                </div>
            </div>
        </div>
        <div id="gridview" style="width: 100%; float: left; text-align: left; margin-top: 2px;
            line-height: 2;">
            <asp:GridView ID="GridViewUserControl" runat="server" OnPageIndexChanging="GridViewUserControl_PageIndexChanging"
                OnRowCommand="GridViewUserControl_RowCommand" OnSorting="GridViewUserControl_Sorting"
                OnRowDeleting="GridViewUserControl_RowDeleting" OnRowEditing="GridViewUserControl_RowEditing"
                Width="100%" OnRowDataBound="GridViewUserControl_RowDataBound">
                <PagerSettings Visible="False" />
            </asp:GridView>
        </div>
    </div>
</div>
