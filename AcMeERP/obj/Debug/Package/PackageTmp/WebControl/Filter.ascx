<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Filter.ascx.cs" Inherits="AcMeERP.WebControl.Filter" %>
<div style="padding-top: 2px; width: 100%; float: left;">
    <div id="toolBar" runat="server" style="width: 100%; float: left">
        <div style="width: 100%; float: left">
            <div class="divrow toolbar">
                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:Literal ID="ltSearch" runat="server" Text="Search"></asp:Literal>
                </div>
                <div class="leftfloat" style="padding-left: 5px; padding-top: 2px;">
                    <asp:TextBox ID="txtSearch" runat="server" EnableTheming="False" Width="300px" CssClass="textbox"></asp:TextBox>
                </div>
                <div class="leftfloat" style="padding-left: 2px; padding-top: 5px;">
                    <asp:ImageButton ID="imgFilterGo" runat="server" AlternateText="Ok" OnClick="imgFilterGo_Click"
                        CausesValidation="false" SkinID="ok_ib" />
                </div>
                <div class="leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:ImageButton ID="imgRefresh" runat="server" AlternateText="Refresh" OnClick="imgRefresh_Click"
                        CausesValidation="false" SkinID="refresh_ib" />
                </div>
                <div class="caption leftfloat" style="padding-left: 5px; padding-top: 5px;">
                    <asp:Literal ID="ltRecord" runat="server" Text=""></asp:Literal>
                </div>
                <div class="clearfloat">
                </div>
            </div>
        </div>
    </div>
</div>
