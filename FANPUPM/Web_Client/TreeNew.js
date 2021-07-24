function doClick(obj, tbName) {
    var table = document.getElementById(tbName);
    for (var i = 0; i < table.rows.length; i++) {
        if (table.rows[i].className == 'trm_focus') {
            table.rows[i].className = 'trm_out';
        }
    }

    obj.className = 'trm_focus';

   
    try {
        var cellObj = obj.cells[0].all.tags("input")[0];
        if (cellObj.type == "radio") {
            cellObj.checked = true;
        }
        //else if(cellObj.type == "checkbox")
        //{
        //	cellObj.checked = !cellObj.checked;
        //}
    }
    catch (e)
	{ }
}

function doClear(tbName) {
    var table = document.getElementById(tbName);
    for (var i = 0; i < table.rows.length; i++) {
        if (table.rows[i].className == 'trm_focus') {
            table.rows[i].className = 'trm_out';
        }
    }
}


function doMouseOut(obj) {
    if (obj.className.toLowerCase() == 'trm_focus')
        return;
    obj.className = 'trm_out';
}

function doMouseOver(obj) {
    if (obj.className.toLowerCase() == 'trm_focus')
        return;
    obj.className = 'trm_over';
}

function doSwitchDisplay(obj, tbName, theShadow, t1, t2, imgurl) {
    var show = false;
    var table = document.getElementById(tbName);
    var coll = table.all(t1);
    if (coll != null) {
        if (coll.length) {
            for (i = 0; i < coll.length; i++) {
                if (coll(i).style.display == 'none') {
                    coll(i).style.display = 'block';
                }
                else {
                    coll(i).style.display = 'none';
                }
            }
        }
        else {
            if (coll.style.display == 'none') {
                coll.style.display = 'block';
            }
            else {
                coll.style.display = 'none';
            }
        }
    }
    if (obj.src.toLowerCase().indexOf("minus") > 0) {
        obj.src = imgurl + "/images/tree/" + t2 + "plus.gif";
        doCodeList(t1, 'd', theShadow);
        hideChild(t1, tbName, theShadow);
    }
    else {
        obj.src = imgurl + "/images/tree/" + t2 + "minus.gif";
        doCodeList(t1, 'a', theShadow);
        showChild(t1, tbName, theShadow);
    }
}

function showChild(value, tbName, theShadow) {
    var table = document.getElementById(tbName);
    var treeCode = document.getElementById(theShadow);
    var treeCodeList = treeCode.value.split('^');
    var childlist;
    for (var i = 0; i < treeCodeList.length; i++) {
        if ((treeCodeList[i].indexOf(value) == 0) && (treeCodeList[i] != value)) {
            childlist = table.all(treeCodeList[i]);
            if (childlist != null) {
                if (childlist.length) {
                    for (var j = 0; j < childlist.length; j++) {
                        childlist(j).style.display = 'block';
                    }
                }
                else {
                    childlist.style.display = 'block';
                }
            }
        }
    }
}

function hideChild(value, tbName, theShadow) {
    var table = document.getElementById(tbName);
    var treeCode = document.getElementById(theShadow);
    var treeCodeList = treeCode.value.split('^');
    var childlist;

    //alert(treeCodeList.value);
    for (var i = 0; i < treeCodeList.length; i++) {
        if ((treeCodeList[i].indexOf(value) == 0) && (treeCodeList[i] != value)) {
            childlist = table.all(treeCodeList[i]);

            if (childlist != null) {
                if (childlist.length) {
                    for (var j = 0; j < childlist.length; j++) {
                        childlist(j).style.display = 'none';
                    }
                }
                else {
                    childlist.style.display = 'none';
                }
            }
        }
    }
}

function doCodeList(value, op, theShadow) {
    var exists = false;
    var treeCode = document.getElementById(theShadow);
    var treeCodeList = treeCode.value.split('^');

    if (treeCode.value == "") {
        treeCodeList.splice(0, 1);
    }

    if (op == 'a') {
        for (var i = 0; i < treeCodeList.length; i++) {
            if (treeCodeList[i] == value) {
                exists = true;
                break;
            }
        }
        if (!exists) {
            treeCodeList.push(value);
        }
    }
    else {
        for (var i = 0; i < treeCodeList.length; i++) {
            if (treeCodeList[i] == value) {
                treeCodeList.splice(i, 1);
            }
        }
    }
    document.getElementById(theShadow).value = treeCodeList.join('^');
}

var j;
function ReplaceDemo(ss) {
    var r, re;                    // 		
    re = /minus/g             // 
    r = ss.replace(re, 'plus');    // 
    return (r);                   // 
}
//
function ReplaceDemo2(ss) {
    var r, re;                    // 		
    re = /plus/g             // 
    r = ss.replace(re, 'minus');    // 
    return (r);                   // 
}

function NodeWimple(r, PCode, sty) {
    myrows = document.getElementById("table").rows;
    if (myrows.length < 2) {
        return;
    }
    for (i = 2; i < myrows.length; i++)//
    {
        var parentcode = myrows[i].children("parentschedulecode", 0).innerHTML;
        if (parentcode == PCode) {
            j = i;
            if (sty > 0)//
            {
                myrows[i].style.display = 'none';
                r.src = ReplaceDemo(r.src);
                NodeWimple(r, myrows[i].children("ScheduleCode", 0).innerHTML, sty);
            }
            else//
            {
                myrows[i].style.display = 'block';
                myrows[i].children("ScheduleName", 0).innerHTML = ReplaceDemo2(myrows[i].children("ScheduleName", 0).innerHTML);
                r.src = ReplaceDemo2(r.src);
                NodeWimple(r, myrows[i].children("ScheduleCode", 0).innerHTML, sty);
            }
        }
    }
    i = j;
}
//
function SetDisplay(r, obj) {
    var bz = r.src.indexOf('minus.gif');
    NodeWimple(r, obj, bz);
}
//
function SetNodeCheck(PCode, IsChecked) {
    myrows = document.getElementById("table").rows;
    if (myrows.length < 2) {
        return;
    }
    for (i = 2; i < myrows.length; i++)//
    {
        var parentcode = myrows[i].children("parentschedulecode", 0).innerHTML;
        if (parentcode == PCode) {
            j = i;
            myrows[i].children[4].children[0].checked = IsChecked;
            SetNodeCheck(myrows[i].children("ScheduleCode", 0).innerHTML, IsChecked);
        }
    }
    i = j;
}
function selrow1(obj) {
    /**************
    *hxp
    *2008-8-1
    *onload="selrow1('DG_list')"			    
    ***************/

    try {
        var table = document.getElementById(obj);
        table.rows[1].click();
    }
    catch (e) { }

}

function selrow3(obj) {
    /**************
    *treeview 的 定位
    *hxp
    *2008-8-1
    *onload="selrow3('DG_list')"
    ***************/
    try {
        document.getElementById(obj).click();
    }
    catch (e) { }
}
function selrow2(obj) {
    /**************
    *hxp
    *2008-8-1
    *onload="selrow1('DG_list')"			    
    ***************/

    try {
        var table = document.getElementById(obj);
        table.rows[2].click();
    }
    catch (e) { }

}	

/**********************
set window height
2009-11-5
hxp
********************/
function divTdHeight(ifselect, ifmenu, iftitle, obj) {
    var height = document.body.clientHeight;
    if (ifselect == 1) { height = height - 31; }
    if (ifmenu == 1) { height = height - 31; }
    if (iftitle == 1) { height = height - 38; }
    document.getElementById(obj).style.height = height;
}
/******************
set my window height
2009-12-4
hxp
**********************/
function myTdHeight(ifselect, ifmenu, iftitle, obj, other) {
    var height = document.body.clientHeight;
    if (ifselect == 1) { height = height - 31; }
    if (ifmenu == 1) { height = height - 31; }
    if (iftitle == 1) { height = height - 38; }
    height = height - other;
    document.getElementById(obj).style.height = height;
}
/******************
set my window height
2009-12-4
hxp
**********************/
function divHeight(obj,windowheight, other) {
    var  height = windowheight - other;
    document.getElementById(obj).style.height = height;
}
/********************
del char
2009-12-5
hxp
******************************/

function CheckStr(keyCode, e) {

    if (event.keyCode == 222) {

        return false;
    }
    // alert(event.keyCode);
}
/**********************
get month controls
***********************/
var ctrlID;
function SetMonth(monthObj) {
    writeFrame();
    ctrlID = monthObj.id;
    showDiv(monthObj, "M");
}
function SetYear(yearObj) {
    writeFrame();
    ctrlID = yearObj.id;
    showDiv(yearObj, "Y");
}


/***************
draw month control
******************/
function writeFrame() {
var ifMonth = document.createElement("ifMonth");
var code = "";
code += "<iframe bgcolor=\"#000000\" id=\"ifMonth\" frameborder=\"0\" style=\"position: absolute;";
code += "width: 200; height: 150; z-index: 9998; display: none\"></iframe>";
ifMonth.innerHTML = code;
document.body.appendChild(ifMonth);
}
/**********************
show month div
************************/
function showDiv(ctrlObj, type) {
if (type == "M") {        
var url = "/Web_Client/SelMonth2.html?id=" + ctrlObj.id;
document.getElementById("ifMonth").src = url;
       
}
if (type == "Y" ) {      
var url = "/Web_Client/SelYear.html?id=" + ctrlObj.id;
document.getElementById("ifMonth").src = url;
      
}
document.getElementById("ifMonth").style.display = "";
setCtrl(ctrlObj);
}
/********************
page month click
*********************/
function document.onclick() {
try {
with (window.event) {
if (srcElement != document.getElementById(ctrlID))
document.getElementById("ifMonth").style.display = "none";
}
} catch (e) { }
}
/*****************************
set month select  div
******************************/
function setCtrl(senderObj) {
    var dads = document.getElementById("ifMonth").style;
    var th = senderObj;
    var ttop = senderObj.offsetTop;       //senderObj控件的定位点高
    var thei = senderObj.clientHeight;   //senderObj控件本身的高
    var tleft = senderObj.offsetLeft;    //senderObj控件的定位点宽
    var ttyp = senderObj.type;          //senderObj控件的类型
    while (senderObj = senderObj.offsetParent) {
        ttop += senderObj.offsetTop;
        tleft += senderObj.offsetLeft;
    }
    /*****************
    go to ，get control wz
    ************************/

    if ((tleft + 200) > document.body.clientWidth) {
        tleft = document.body.clientWidth - 200;
    }
    if ((ttop + 150) > document.body.clientHeight) {
        ttop = document.body.clientHeight - 150;
    }
    dads.top = (ttyp == "image") ? ttop + thei : ttop + thei + 4;
    dads.left = tleft;
    outObject = th;
}


/***************
调用公用选择层，hxp 
******************/

function showjwDiv(ctrlObj, url, type) {
    writejwDivFrame(url, type);
    var ctrlID = ctrlObj.id;
    //  alert(ctrlID);+"&id=" + ctrlObj.id
    document.getElementById("ifrmjwDiv").src = url;
    document.getElementById("ifrmjwDiv").style.display = "";

    //  $('#ifrmjwDiv').dialog({ width: 300, height: 285, modal: false, position: ['right', 'top']
    //  });
    setjwCtrl(ctrlObj, type);
}

function writejwDivFrame(url, type) {
    var ifrmjwDiv = document.createElement("ifrmjwDiv");
    var code = "";
    //项目效果图
    if (type == "prjbmp") {
        code = "<iframe bgcolor=\"#000000\" id=\"ifrmjwDiv\" frameborder=\"0\" width=\"328\" height=\"220\"  src=\"" + url + "\" style=\"position: absolute;  z-index: 9998; display: none\"></iframe>";
    } else if (type == "tree") {
        code = "<iframe bgcolor=\"#000000\" id=\"ifrmjwDiv\" frameborder=\"0\" width=\"240\" height=\"365\"  src=\"" + url + "\" style=\"position: absolute;  z-index: 9998; display: none\"></iframe>";
    }
    else {

        code = "<iframe bgcolor=\"#000000\" id=\"ifrmjwDiv\" frameborder=\"0\"   width=\"240\" height=\"180\" src=\"" + url + "\" style=\"position: absolute;";
        code += "z-index: 9998; display: none\"></iframe>";
    }
    ifrmjwDiv.innerHTML = code;
    document.body.appendChild(ifrmjwDiv);

}

/*****************************
set  select  div
******************************/
function setjwCtrl(senderObj, type) {
    var dads = document.getElementById("ifrmjwDiv").style;
    var th = senderObj;
    var ttop = senderObj.offsetTop;       //senderObj控件的定位点高
    var thei = senderObj.clientHeight;   //senderObj控件本身的高
    var tleft = senderObj.offsetLeft;    //senderObj控件的定位点宽
    var ttyp = senderObj.type;          //senderObj控件的类型
    while (senderObj = senderObj.offsetParent) {
        ttop += senderObj.offsetTop - 1;
        tleft += senderObj.offsetLeft;
    }
    /*****************
    go to ，get control wz
    ************************/
    //项目效果图
    if (type == "prjbmp") {
        //tleft = document.body.clientWidth - 200;
        tleft = 180;

        if ((ttop + 280) > document.body.clientHeight) {
            ttop = document.body.clientHeight - 220;
        }
    } else {
        if ((tleft + 200) > document.body.clientWidth) {
            tleft = document.body.clientWidth - 220;
        }
        if ((ttop + 330) > document.body.clientHeight) {
            ttop = document.body.clientHeight - 280;
        }
    }
    dads.top = (ttyp == "image") ? ttop + thei : ttop + thei + 4;
    dads.left = tleft;
    outObject = th;
}
