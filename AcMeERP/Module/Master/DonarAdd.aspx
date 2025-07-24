<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HomeLoginMaster.Master"
    AutoEventWireup="true" CodeBehind="DonarAdd.aspx.cs" Inherits="AcMeERP.Module.Master.DonarAdd" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="~/WebControl/DateControl.ascx" TagName="Datecontrol" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMain" runat="server">
    <asp:UpdatePanel ID="upDonarAdd" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnPersonalInfo" runat="server" GroupingText="Personal Info" 
                meta:resourcekey="pnPersonalInfoResource1">
                <div style="margin-left: 80px; width: 80%">
                    <div class="divrow">
                        <div class="divcolumn fieldcaption">
                            <asp:Literal ID="Literal4" runat="server" Text="Name *:" 
                                meta:resourcekey="Literal4Resource1"></asp:Literal>
                        </div>
                        <div class="divcolpad">
                            <asp:TextBox ID="txtName" runat="server" CssClass="textbox" Width="200px" 
                                MaxLength="50" meta:resourcekey="txtNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfNm" runat="server" Text="*" ErrorMessage="Name is required"
                                SetFocusOnError="True" ControlToValidate="txtName" Display="Dynamic" 
                                meta:resourcekey="rfNmResource1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="divrow">
                        <div class="divcolumn fieldcaption">
                            <asp:Literal ID="Literal1" runat="server" Text="Type" 
                                meta:resourcekey="Literal1Resource1"></asp:Literal>
                        </div>
                        <div class="divcolpad">
                            <asp:RadioButton ID="rdType" runat="server" Text="Institutional" 
                                GroupName="type" meta:resourcekey="rdTypeResource1" />
                            <asp:RadioButton ID="rdType1" runat="server" Text="Individual" GroupName="type" 
                                meta:resourcekey="rdType1Resource1" />
                        </div>
                    </div>
                    <div class="divrow">
                        <div class="divcolumn fieldcaption">
                            <asp:Literal ID="Literal2" runat="server" Text="Country" 
                                meta:resourcekey="Literal2Resource1"></asp:Literal>
                        </div>
                        <div class="divcolpad">
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="combobox" 
                                Width="200px" meta:resourcekey="ddlCountryResource1">
                            </asp:DropDownList>
                        </div>
                    </div>
            </asp:Panel>

            <asp:Panel ID="pnContactInfo" runat="server" GroupingText="Contact Info" 
                meta:resourcekey="pnContactInfoResource1">
            <div style="margin-left: 80px; width: 80%">
                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal3" runat="server" Text="City/Town" 
                            meta:resourcekey="Literal3Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" Width="200px" 
                            MaxLength="30" meta:resourcekey="txtCityResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal5" runat="server" Text="State/Province" 
                            meta:resourcekey="Literal5Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtStateProvince" runat="server" CssClass="textbox" 
                            Width="200px" MaxLength="30" meta:resourcekey="txtStateProvinceResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal6" runat="server" Text="Address" 
                            meta:resourcekey="Literal6Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox multiline" 
                            Width="200px" MaxLength="250" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal7" runat="server" Text="Pin/Zip Code" 
                            meta:resourcekey="Literal7Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtPinZipCode" runat="server" CssClass="textbox" Width="200px" 
                            MaxLength="10" meta:resourcekey="txtPinZipCodeResource1"></asp:TextBox>
                    </div>
                </div>

                 <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal8" runat="server" Text="PAN/UID #" 
                            meta:resourcekey="Literal8Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtPan" runat="server" CssClass="textbox" Width="200px" 
                            MaxLength="10" meta:resourcekey="txtPanResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal9" runat="server" Text="Phone" 
                            meta:resourcekey="Literal9Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" Width="200px" 
                            MaxLength="10" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal10" runat="server" Text="Fax" 
                            meta:resourcekey="Literal10Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtFax" runat="server" CssClass="textbox" Width="200px" 
                            MaxLength="20" meta:resourcekey="txtFaxResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal11" runat="server" Text="Email" 
                            meta:resourcekey="Literal11Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="200px" 
                            MaxLength="20" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal12" runat="server" Text="URL" 
                            meta:resourcekey="Literal12Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtURL" runat="server" CssClass="textbox" Width="200px" 
                            MaxLength="250" meta:resourcekey="txtURLResource1"></asp:TextBox>
                    </div>
                </div>

                <div class="divrow">
                    <div class="divcolumn fieldcaption">
                        <asp:Literal ID="Literal13" runat="server" Text="Notes" 
                            meta:resourcekey="Literal13Resource1"></asp:Literal>
                    </div>
                    <div class="divcolpad">
                        <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox multiline" 
                            Width="200px" MaxLength="250" meta:resourcekey="txtNotesResource1"></asp:TextBox>
                    </div>
                </div>
                <div style="margin-left: 80px; width: 80%">
            </asp:Panel>
            <div style="text-align: center" class="divrow">
                <asp:Button ID="btnSaveUser" OnClick="btnSaveUser_Click" runat="server" CssClass="button"
                    Text="Save"  ToolTip="Click here to save user Details" 
                    meta:resourcekey="btnSaveUserResource1"></asp:Button>
                 <asp:Button ID="hlkClose" runat="server" CssClass="button" Text="Close"  
                    ToolTip="Click here to close this page" CausesValidation="False" 
                    meta:resourcekey="hlkCloseResource1"></asp:Button>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
