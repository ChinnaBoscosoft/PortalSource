<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="UserRights.aspx.cs" Inherits="AcMeERP.Module.User.UserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td style="clear: both" align="center">
                    <div style="width: 90%; text-align: center" class="divrow">
                        <div style="clear: both; width: 20%" class="divcolpad">
                            <div style="width: 95%" class="divcolpad containerbox">
                                <div style="width: 97%; padding-top: 5px;" class="subtitlebar">
                                    <asp:Label ID="lblUserRights" runat="server" Text="Users"></asp:Label>
                                </div>
                                <div id="treeview">
                                    <asp:TreeView ID="tvUsers" runat="server" ImageSet="Contacts" OnSelectedNodeChanged="tvUsers_SelectedNodeChanged">
                                        <SelectedNodeStyle Font-Bold="True"></SelectedNodeStyle>
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="upWfDefinition" runat="server">
                            <ContentTemplate>
                                <div style="width: 75%; text-align: center" class="divcolpad borderbox">
                                    <div style="width: 98%; padding-top: 5px; padding-bottom: 5px;" class="subtitlebar">
                                        <div class="divcolpad">
                                            <asp:Label ID="lblSelectedUser" runat="server" Text="Selected User"></asp:Label>
                                        </div>
                                        <div style="text-align: right">
                                            <asp:Label ID="lblComment" runat="server" Text="Rights will be set based on User Role"></asp:Label>
                                            <asp:Button ID="btnSaveRights" OnClick="btnSaveRights_Click" runat="server" CssClass="button" 
                    Text="Save" ToolTip="Click here to save user rights"></asp:Button>
                                        </div>
                                    </div>
                                    <div style="vertical-align: top; width: 100%; text-align: center">
                                        <asp:DataList ID="dlUserRights" runat="server" Width="100%" RepeatColumns="0" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" OnItemDataBound="dlUserRights_ItemDataBound">
                                            <ItemTemplate>
                                                <div style="clear: both; vertical-align: top; width: 98%; padding-bottom: 5px">
                                                    <div style="clear: both; vertical-align: top; width: 100%;" class="containerbox">
                                                        <div class="divrow">
                                                            <asp:CheckBox ID="chkModule" runat="server" CssClass="caption" Text='<%# DataBinder.Eval(Container.DataItem, "MODULE") %>'
                                                                OnCheckedChanged="chkModule_CheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                                            <asp:Label ID="lblModuleCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MODULE_CODE") %>'
                                                                Visible="false"></asp:Label>
                                                        </div>
                                                        <div style="clear: both; width: 95%;" class="divrow">
                                                            <asp:DataList Style="width: 98%" ID="dlActivities" runat="server" RepeatLayout="Table"
                                                                RepeatDirection="Horizontal" RepeatColumns="4" OnItemDataBound="dlActivities_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <div class="divrow">
                                                                        <div class="divcolpad" style="padding-left: 15px;">
                                                                            <asp:CheckBox ID="chkActivity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ACTIVITY") %>'
                                                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "ALLOW")) %>'
                                                                                AutoPostBack="True" OnCheckedChanged="chkActivity_CheckedChanged" />
                                                                            <asp:Label ID="lblActivityCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ACTIVITY_CODE") %>'></asp:Label>
                                                                            <asp:Label ID="lblModuleCode1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MODULE_CODE") %>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:DataList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
