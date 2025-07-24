<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateControl.ascx.cs" Inherits="AcMeERP.WebControl.DateControl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<table cellpadding="0px" cellspacing="0px" border="0px">
    <tr>
        <td>
            <asp:TextBox ID="txtDate" runat="server" onblur="javascript:ucValidateDate(this);"
                Width="75px" MaxLength="10" CssClass="textbox"  />
        </td>
        <td style="padding-left: 2px;">
            <asp:ImageButton OnClientClick="return false;" ID="imgDate" runat="server" SkinID="date_img"
                TabIndex="-1" ImageAlign="Bottom"/>
        </td>
        <td style="padding-left: 2px;">        
            <asp:TextBox ID="txtTime" runat="server" Width="75px" CssClass="textbox" onchange="javascript:ucValidateTime(this);"
                Visible="False" TabIndex="-1" />
            <strong>
            <asp:Label ID="lblStar" ForeColor="red" runat="server" Text="*" Visible="False" /></strong>
        </td>
    </tr>
</table>
<div style="max-height:0px;overflow:hidden;">
    <asp:CalendarExtender ID="ceDate" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"
        PopupButtonID="imgDate">
    </asp:CalendarExtender>
            <asp:MaskedEditExtender ID="meDate" runat="server" TargetControlID="txtDate" Mask="99/99/9999"
                MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
            <asp:MaskedEditExtender ID="meTime" runat="server" TargetControlID="txtTime" Mask="99:99:99"
                MessageValidatorTip="true" MaskType="Time" AcceptAMPM="True" />
            <%--<asp:RequiredFieldValidator ID="rfvDate" runat="server" SetFocusOnError="True" ControlToValidate="txtDate" />
--%>            <asp:RequiredFieldValidator ID="rfvTime" runat="server" SetFocusOnError="True" Display="None"
                ControlToValidate="txtTime" Visible="False" />
    <asp:HiddenField ID="hfDate" runat="server" />
    <asp:HiddenField ID="hfTime" runat="server" />
</div>

<script language="javascript" type="text/javascript">
    var ucDateSuffix = "txtDate";
    var ucTimeSuffix = "txtTime";
    var ucLinkSuffix = "imgDate";
    var ucManSuffix = "lblStar";

    function ucValidateDate(ctrl) {
        if (ctrl.value == "__/__/____") {
            ctrl.value = "";
            return;
        }

        if (ValidateDate(ctrl.value, "<%=FormatString%>") == false) {
            ucResetDate(ctrl);
            return;
        }

        ucOnDatechange(ctrl);
    }

    function ucResetDate(ucDate) {
        var errMsg = ucDate.id;
        errMsg = errMsg.replace(ucDateSuffix, "hfDate");
        errMsg = ucDateGetCtrl(errMsg).value;
        alert(errMsg);
        ucDate.value = "";
        ucDate.focus();
    }

    function ucValidateTime(ctrl) {
        if (ValidateTime(ctrl.value) == false) {
            ucResetTime(ctrl);
            return;
        }
        ucOnDatechange(ctrl);
    }

    function ucResetTime(ucTime) {
        var errMsg = ucTime.id;
        errMsg = errMsg.replace(ucTimeSuffix, "hfTime");
        errMsg = ucDateGetCtrl(errMsg).value;
        alert(errMsg);
        ucTime.value = "";
        ucTime.focus();
    }

    function ucDateGetCtrl(elementId) {
        var retVal = null;
        var Ctrl = document.getElementById(elementId);
        if (Ctrl != null) { retVal = Ctrl; }
        return retVal;
    }

    var DaysOfMonth = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
    var DaysOfMonthLY = new Array(31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

    function LeapYear(year) {
        if ((year / 4) != Math.floor(year / 4)) return false;
        if ((year / 100) != Math.floor(year / 100)) return true;
        if ((year / 400) != Math.floor(year / 400)) return false;
        return true;
    }

    function ValidateDate(givenDate, dtFormat) {
        if (givenDate == "")
            return true;
        var givenFormat = dtFormat;
        var arrFormat = givenFormat.split("/");
        var arrDate = givenDate.split("/");

        if (arrDate.length != arrFormat.length) {
            return false;
        }

        if (arrFormat.length != 3) {
            alert("Wrong Date Format");
            return false;
        }

        var dayIndex;
        var monIndex;
        var yearIndex;

        switch (arrFormat[0]) {
            case "dd":
                dayIndex = 0;
                break;
            case "mm":
                monIndex = 0;
                break;
            case "yyyy":
                yearIndex = 0;
                break;
        }

        switch (arrFormat[1]) {
            case "dd":
                dayIndex = 1;
                break;
            case "mm":
                monIndex = 1;
                break;
            case "yyyy":
                yearIndex = 1;
                break;
        }

        switch (arrFormat[2]) {
            case "dd":
                dayIndex = 2;
                break;
            case "mm":
                monIndex = 2;
                break;
            case "yyyy":
                yearIndex = 2;
                break;
        }

        if (isMonth(arrDate[monIndex]) == false) {
            return false;
        }

        if (isYear(arrDate[yearIndex]) == false) {
            return false;
        }

        if (isDay(arrDate[dayIndex], arrDate[monIndex], arrDate[yearIndex]) == false) {
            return false;
        }
        return true;
    }

    function isDay(day, month, year) {
        if (day == 0)
            return false;
        if (LeapYear(year)) {
            if (day > DaysOfMonthLY[month - 1])
                return false;
        }
        else {
            if (day > DaysOfMonth[month - 1])
                return false;
        }
        return true;
    }

    function isMonth(month) {
        if (month > 12 || month == 0)
            return false;
        return true;
    }

    function isYear(year) {
        if (year <= 100) 
            return false;
        return true;
    }

    function ucOnDatechange(ctrl) { }
</script>

