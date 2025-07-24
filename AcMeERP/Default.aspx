<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPage/HomeMaster.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AcMeERP.Default"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cpHead">
    <script type="text/javascript" language="javascript">
        function toggle_visibility(id) {
            var e = document.getElementById(id);
            document.getElementById("divcontent").style.display = 'none';
            document.getElementById("divcontent1").style.display = 'none';
            document.getElementById("divcontent2").style.display = 'none';
            document.getElementById("divcontent3").style.display = 'none';
            document.getElementById("divcontent4").style.display = 'none';
            document.getElementById("divcontent5").style.display = 'none';
            document.getElementById("divcontent6").style.display = 'none';
            e.style.display = 'block';
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cpMain">
    <asp:UpdatePanel ID="upLogin" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Label ID="lblmsg" runat="server" CssClass="requiredcolor bold" meta:resourcekey="lblmsgResource1"></asp:Label>
            </div>
            <div style="font-weight: normal;">
                <div style="width: 100%;">
                    <div style="width: 50%; float: left;">
                        <div style="width: 48%; float: left;">
                            <div class="Imagebox divimg">
                                <asp:ImageButton ID="btnTest1" runat="server" ImageUrl="~/App_Themes/MainTheme/images/FinacialAccounting.png"
                                    OnClientClick="javascript:toggle_visibility('divcontent1');return false;" />
                            </div>
                            <div class="divimgheader">
                                 <a  href="#" onclick="javascript:toggle_visibility('divcontent1');return false;">Finance</a>
                            </div>
                        </div>
                        <div style="width: 48%; float: left;">
                            <div class="Imagebox divimg">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/MainTheme/images/stock.png"
                                    OnClientClick="javascript:toggle_visibility('divcontent2');return false;" />
                            </div>
                            <div class="divimgheader">
                              <a  href="#" onclick="javascript:toggle_visibility('divcontent2');return false;">Stock Management</a>
                            </div>
                        </div>
                        <div style="width: 48%; float: left;">
                            <div class="Imagebox divimg">
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/App_Themes/MainTheme/images/Asset.png"
                                    OnClientClick="javascript:toggle_visibility('divcontent3');return false;" />
                            </div>
                            <div class="divimgheader">
                              <a   href="#" onclick="javascript:toggle_visibility('divcontent3');return false;">Asset Management</a>
                            </div>
                        </div>
                        <div style="width: 48%; float: left;">
                            <div class="Imagebox divimg">
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/App_Themes/MainTheme/images/Payroll.png"
                                    OnClientClick="javascript:toggle_visibility('divcontent4');return false;" />
                            </div>
                            <div class="divimgheader">
                                 <a   href="#" onclick="javascript:toggle_visibility('divcontent4');return false;">Payroll Management</a>
                            </div>
                        </div>
                        <div style="width: 48%; float: left;">
                            <div class="Imagebox divimg">
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/App_Themes/MainTheme/images/legal.png"
                                    OnClientClick="javascript:toggle_visibility('divcontent5');return false;" />
                            </div>
                            <div class="divimgheader">
                               <a   href="#" onclick="javascript:toggle_visibility('divcontent5');return false;">Legal Compliance</a>
                            </div>
                        </div>
                        <div style="width: 48%; float: left;">
                            <div class="Imagebox divimg">
                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/App_Themes/MainTheme/images/reports.png"
                                    OnClientClick="javascript:toggle_visibility('divcontent6');return false;" />
                            </div>
                            <div class="divimgheader">
                               <a   href="#" onclick="javascript:toggle_visibility('divcontent6');return false;"> Reports</a>
                            </div>
                        </div>
                    </div>
                    <div style="width: 50%; float: left;">
                        <div id="divcontent" style="width: 100%; display: block;">
                            <div class="divMaincontent">
                                Acme.erp
                            </div>                           
                            <div class="divSubcontent">
                                <p>
                                    Acme.erp is a cloud-based, client-server and windows application for administration
                                    and management of religious institutes and NGOs of any size, at multiple levels.
                                </p>
                                <p>
                                    It is developed and packaged as an Enterprise Resource Planning solution. Acme.erp
                                    is deployed as Head Office & Branch Office Suite. It is hosted in cloud to be accessible
                                    by all affected parties.
                                </p>
                                <p>
                                    <b>Head Office Suite</b>
                                    <br />
                                    Head Office Suite is a web-based application for generating financial reports from
                                    all Branch Offices at different levels. The data from the individual Branch Offices
                                    are updated to the Head Office on demand basis or asynchronously (on scheduled intervals).
                                    It provides easy access to the financial statements of any Branch Office, drilling
                                    down to the level of single transaction. Consolidated reports combining various
                                    branches on multiple criteria can be generated.
                                </p>
                                <p>
                                    <b>Branch Office Suite</b>
                                    <br />
                                    Branch Office Suite is a windows application from a single or multiple terminals.
                                    The system can be configured for a single or multiple users.
                                </p>
                            </div>
                        </div>
                        <div id="divcontent1" style="width: 100%; display: none;">
                            <div class="divMaincontent">
                                Finance
                            </div>                           
                            <div class="divSubcontent">
                                <p>
                                    Acme.erp’s finance module gathers financial data and generate reports. It has the
                                    ability to centrallytrack all the financial transactions of multiple projects in
                                    a single system and give the instant report of cash, bank and FD balances as on
                                    date. The system helps to capture all the financial transactionsusing a user-friendly
                                    transaction window. This module includes cost centres to record the expenses, budgeting
                                    to plan income and expenditure of an organisation, and fixed deposit (FD) to keep
                                    record of FD transactions.
                                </p>
                                <ul>
                                    <li class="pad5">Receipt </li>
                                    <li class="pad5">Payment </li>
                                    <li class="pad5">Contra</li>
                                    <li class="pad5">Journal</li>
                                    <li class="pad5">Investments</li>
                                    <li class="pad5">Budgeting</li>
                                </ul>
                            </div>
                        </div>
                        <div id="divcontent2" style="width: 100%; display: none;">
                            <div class="divMaincontent">
                                Stock Management
                            </div>                           
                            <div class="divSubcontent">
                                <p>
                                    Acme.erp’s stock module provides an efficient inventory management to satisfy the
                                    requirements of an organisation. This module manages purchase, utilisation and storage
                                    of stock.
                                </p>
                                <ul>
                                    <li class="pad5">Purchase/Receive </li>
                                    <li class="pad5">Sale/Utilize </li>
                                    <li class="pad5">Transfer </li>
                                    <li class="pad5">Dispose </li>
                                    <li class="pad5">Return </li>
                                </ul>
                            </div>
                        </div>
                        <div id="divcontent3" style="width: 100%; display: none;">
                            <div class="divMaincontent">
                                Asset Management
                            </div>                          
                            <div class="divSubcontent">
                                <p>
                                    The sub-system Assets enables the users to maintain a complete list of all assets
                                    owned, including intangible assets. Asset management enables the users to maintain
                                    the details of Certification, Registration, Renewal, Maintenance and Insurance.</p>
                                <ul>
                                    <li class="pad5">Purchase/Receive </li>
                                    <li class="pad5">Register/Renew </li>
                                    <li class="pad5">Dispose </li>
                                    <li class="pad5">Sale </li>
                                    <li class="pad5">Maintenance </li>
                                    <li class="pad5">Insurance </li>
                                </ul>
                            </div>
                        </div>
                        <div id="divcontent4" style="width: 100%; display: none;">
                            <div class="divMaincontent">
                                Payroll Management
                            </div>                          
                            <div class="divSubcontent">
                                <p>
                                    Acme.erp makes the tedious and time consuming payroll preparation easy and quick.
                                    This module handles the employees' attendance departmentwise, payments, statutory
                                    payments, deduction, statutory deductions, increments, process salary, customise
                                    pay method and pay period.
                                </p>
                                <ul>
                                    <li class="pad5">Staffing </li>
                                    <li class="pad5">Welfare </li>
                                </ul>
                            </div>
                        </div>
                        <div id="divcontent5" style="width: 100%; display: none;">
                            <div class="divMaincontent">
                                Legal Compliance
                            </div>                           
                            <div class="divSubcontent">
                                <p>
                                    Acme.erp ensures compliance of all the laws and regulations of the government that
                                    has to be followed by an organisation. This includes payment of tax and filing returns.
                                    Legal compliances increase the transparency in the affairs of an organisation.
                                </p>
                                <ul>
                                    <li class="pad5">Legal Registration </li>
                                    <li class="pad5">TDS</li>
                                    <li class="pad5">Income Tax</li>
                                    <li class="pad5">Service Tax</li>
                                </ul>
                            </div>
                        </div>
                        <div id="divcontent6" style="width: 100%; display: none;">
                            <div class="divMaincontent">
                                Reports
                            </div>                           
                            <div class="divSubcontent">
                                <p>
                                    Statistical analysis reports for decision- making at the branch office (local) and
                                    head office (provincial) level consolidating financial reports from different units.
                                    <ul>
                                        <li class="pad5">Abstract </li>
                                        <li class="pad5">Books of Accounts</li>
                                        <li class="pad5">Bank Activities</li>
                                        <li class="pad5">Final Accounts</li>
                                        <li class="pad5">Foreign Contribution</li>
                                    </ul>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
