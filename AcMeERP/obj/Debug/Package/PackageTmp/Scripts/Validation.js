function numbersonly(e, decimal, objtxt) {
    //alert('call');
    var key;
    var keychar;

    if (window.event) {
        key = window.event.keyCode;
    }
    else if (e) {
        key = e.which;
    }
    else {
        return true;
    }
    keychar = String.fromCharCode(key);

    if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
        return true;
    }
    else if (decimal) {
        if (("1234567890").indexOf(keychar) > -1) {
            return true;
        }
        else {
            return false;

        }
    }
    else if (!decimal && (("0123456789").indexOf(keychar) > -1)) {
        return true;
    }
    else if (decimal && (keychar == ".") && objtxt.value.indexOf(keychar) == -1 && objtxt.value.length < 6) {
        return true;
    }
    else
        return false;
}

function textboxMultilineMaxNumber(e, txt, maxLen) {

    if (txt.value.length > (maxLen - 1)) {

        var keyID = (window.event) ? event.keyCode : e.keyCode;
        if ((keyID >= 37 && keyID <= 40) || (keyID == 8) || (keyID == 46)) {
            if (window.event)
                return true;
        }
        else {
            return false;
        }
    }
    else {
        return true;

    }
}

//Change First letter of sentence to capital
function ChangeCase(id) {
    var txt = document.getElementById(id);
    txt.value = txt.value.replace(/^[a-z]/, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
}
       