<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivPup.aspx.cs" Inherits="SMS_Fram_DivPup" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="../StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../Script/jw.js"></script>
    <script type="text/javascript" src="../../Script/jwJson.js"></script>
    <script type="text/javascript" src="../../Script/wf.js"></script>
    <script type="text/javascript">
        function selectUser() {
            document.getElementById("divFramsms").title = "短信发送";
            document.getElementById("divFramsms").style.display.bold;
            document.getElementById("ifFramsms").src = "SMSOneAdd.aspx?Method=returnType";
            //selectnEvent('divFramsms');
            //$("#divFramsms").dialog("option", "position", 'left');
            $("#dialog:ui-dialog").dialog("destroy");
            $('#divFramsms').dialog({ width: 520, height: 550, modal: true, bgiframe: true }); //min:570,max:940
            //var width = $('#divFramsms').dialog("option", "width");
            //alert(width);
        }
        //接收传过来记录短信记录的值并根据值改变div宽度
        function returnType(myValue) {
            //alert(myValue);
            document.getElementById('hdOCValue').value = myValue;
            var hdvalue = document.getElementById('hdOCValue').value == "" ? '0' : document.getElementById('hdOCValue').value;
            //alert(hdvalue);
            if (hdvalue == '0') {
                $('#divFramsms').dialog("option", "width", 520);
            }
            if (hdvalue == "1") {
                $('#divFramsms').dialog("option", "width", 800);
                $('#divFramsms').dialog("option", "position", 'center');
            }
        }
        function selectUser1() {
            document.getElementById("divFramPrj").title = "选择人员";
            document.getElementById("divFramPrj").style.display.bold;
            document.getElementById("ifFramPrj").src = "/Common/DivSelectAllUser.aspx?Method=returnUser&parurl=SMSOne";
            //selectnEvent('divFramPrj');            
            $('#divFramPrj').dialog({ width: 600, height: 485, modal: true });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="divFramsms" style="display: none;">
        <iframe id="ifFramsms" frameborder="0" src="" width="100%" height="100%"></iframe>
    </div>
    <div id="newDivSms" title="" style="display: none">
        <iframe id="newFram" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <div id="divFramPrj" title="" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="100%" height="100%" src=""></iframe>
    </div>
    <input id="btnnew" onclick="selectUser()" type="button" value="发短信"/>
    <input id="hdOCValue" type="hidden" runat="server" />

    </form>
</body>
</html>
