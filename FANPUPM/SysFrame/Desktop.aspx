<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Desktop.aspx.cs" Inherits="platform_czDesktop" %>
<%@ Register TagPrefix="cc1" Namespace="EeekSoft.Web" Assembly="EeekSoft.Web.PopupWin" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>桌面</title>
    <style type="text/css">
        #win
        {
            position: absolute;
            left: 100px;
            top: 100px;
            width: 0px;
            height: 0px;
        }
    </style>
    <script type="text/javascript">
        var ImagePath = 'images';
        var WINSHOW_LIST = "我的桌面";

        var x0 = 0, y0 = 0, x1 = 0, y1 = 0;
        var moveable = false;
        //开始拖动; 
        function startDrag(obj) {
            //alert(0)
            if (event.button == 1) {
                obj.setCapture();
                var win = obj.parentNode;
                x0 = event.clientX;
                y0 = event.clientY;
                x1 = parseInt(win.offsetLeft);
                y1 = parseInt(win.offsetTop);
                moveable = true;
            }
        }
        //拖动; 
        function drag(obj) {
            if (moveable) {
                var win = obj.parentNode;
                win.style.left = x1 + event.clientX - x0;
                win.style.top = y1 + event.clientY - y0;
            }
        }
        //停止拖动; 
        function stopDrag(obj) {
            if (moveable) {
                obj.releaseCapture();
                moveable = false;
            }
        }


    
    </script>
    <script type='text/javascript' language='javascript' src='jscript/JsControlWindow.js'></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <link rel="stylesheet" type="text/css" href="jscript/PLATFORM.css" />
<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="../Script/jquery.easyui/jquery.easyui.extension.css" />
<link rel="Stylesheet" href="/Script/jquery.jgrowl/jquery.jgrowl.css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/jquery.jgrowl/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
    <script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="../Script/Popup.js"></script>
    <script type="text/javascript" src="jscript/top.ui.js"></script>
    <script type="text/javascript" src="../SysFrame2/Script/top.ui.impl.js"></script>
    <script language="javascript" type="text/javascript">
        var myBar = null;
        function onLeft(obj) {
            obj.src = ImagePath + "/label_forward1.gif";
            myBar = setInterval(moveLeft, 1);
        }
        function offLeft(obj) {
            obj.src = ImagePath + "/label_forward.gif";
            clearInterval(myBar);
        }

        function onRight(obj) {
            obj.src = ImagePath + "/label_back1.gif";
            myBar = setInterval(moveRight, 1);
        }
        function offRight(obj) {
            obj.src = ImagePath + "/label_back.gif";
            clearInterval(myBar);
        }

        function moveLeft() {
            document.getElementById("ToolBarTd").childNodes(0).scrollLeft += 3;
        }

        function moveRight() {
            document.getElementById("ToolBarTd").childNodes(0).scrollLeft -= 3;
        }
        function CloseAbout() {
            document.getElementById("SysAbout").style.display = "none";
        }
        function Init() {
            var homebar = document.getElementById("ToolBarTd").childNodes(0).childNodes(0).childNodes(0);
            homebar.removeChild(homebar.childNodes(2));
            homebar.childNodes(0).childNodes(0).src = ImagePath + "/tab_left.gif";
            homebar.childNodes(1).style.backgroundImage = "url(" + ImagePath + "/tab_center.gif)";
            homebar.childNodes(2).childNodes(0).src = ImagePath + "/tab_right.gif";
            var homeimg = document.createElement("<IMG style='WIDTH: 11px; HEIGHT: 11px;margin-left:2px;margin-right:3px;' src='" + ImagePath + "/btn_home.gif'>");
            homebar.childNodes(1).insertAdjacentElement("afterBegin", homeimg);
        }

        function showAbout() {
            document.getElementById("SysAbout").style.display = "block";
        }

        function closeit() {
            window.top.window.opener = null;
            window.top.window.open('', '_self');
            window.top.window.close();
        }
        function closeAll() {
            window.desktop.closeAll();
            window.desktop.getTaskBar().getBtnList()[-100].getBtn().parentNode.parentNode.scrollLeft = 0;
        }
        function deskwidth() {
            var w = Math.floor((screen.availWidth)); //document.body.clientWidth
            document.getElementById("deskframe").src = "../TableTop/tableMain.aspx?deskwidth=" + w;
        }
    </script>
</head>
<body id="testid" onload="deskwidth()">
    <form name="form1" method="post" action="aspx" id="form1" runat="server">
    <table style='width: 100%; height: 100%; position: relative; z-index: 0' cellspacing='0'
        cellpadding='0' id='desktop' border="0">
        <!--border: red 1px solid;-->
        <tr background="images/label_bg.jpg">
            <td style='width: 95%; height: 25px;' id="ToolBarTd">
                <div style="overflow: hidden; white-space: nowrap; height: 25px; width: 100%;">
                </div>
            </td>
            <td background="images/label_bg.jpg">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="images/label_back.gif" width="12" height="12" border="0" style="cursor: hand"
                                onmousedown="onRight(this);" onmouseup="offRight(this);" />
                        </td>
                        <td>
                            <img src="images/label_forward.gif" width="12" height="12" border="0" style="cursor: hand"
                                onmousedown="onLeft(this);" onmouseup="offLeft(this);" />
                        </td>
                        <td>
                            &nbsp;<img src="images/Del.gif" width="12" height="12" border="0" style="cursor: hand"
                                onmousedown="javascript:if(confirm('确定关闭所有吗？')){closeAll();;}else{return false;}"
                                alt="关闭所有" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;<img src="images/about.gif" width="12" height="12" border="0" style="cursor: hand"
                                onmousedown="showAbout()" alt="关于" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <div unselectable='on' style='padding-top: 2px; padding-bottom: 25px; padding-left: 2px;
                    padding-right: 2px; overflow: hidden; width: 100%; height: 100%;'>
                    <!-- 桌面背景开始 -->
                    <div class="ajaxWindow_window_body" style="border: #a9bce6 1px solid;" unselectable='on'>
                        <div class="ajaxWindow_window_caption_active" style="margin: 0px; width: 100%; background-image: url(images/window_bg.jpg);">
                            <table style="width: 100%; height: 18px" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td style="padding-left: 2px">
                                            <img style="display: none; width: 16px; height: 16px">
                                        </td>
                                        <td style="width: 100%; cursor: default">
                                            <div class="ajaxWindow_window_caption_active ajaxWindow_window_caption_text" style="overflow: hidden;
                                                width: 784px; white-space: nowrap; height: 18px; text-overflow: ellipsis">
                                                我的工作平台</div>
                                        </td>
                                        <td nowrap>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div style="width: 100%; height: 100%; position: relative; border: 0px;">
                            <iframe id="deskframe" width="100%" height="100%" src="" frameborder="0"></iframe>
                            <cc1:PopupWin ID="PopupWin_Email" Message="" Text="" Title="系统消息" AutoShow="false" HideAfter="10000" OffsetX="0" OffsetY="0" LinkTarget="_blank" Link="/oa/MailAdmin/InBox.aspx" ActionType="OpenLink" DockMode="BottomRight" runat="server"></cc1:PopupWin>
                            <cc1:PopupWin ID="PopupWin1" Title="系统消息" HideAfter="10000" OffsetX="0" OffsetY="0" Style="z-index: 112; left: 600px; position: absolute; top: 528px" Message="<img src="bob.gif" border="0" align="right"><p><b>收件箱" Width="216px" LinkTarget="_blank" Link="/oa/MailAdmin/InBox.aspx" ActionType="OpenLink" DockMode="BottomRight" LightShadow="#FFC080" Shadow="#804000" TextColor="System.Drawing.Color.Black" DarkShadow="System.Drawing.Color.Black" GradientLight="#FBEEBB" GradientDark="#FF9900" AutoShow="false" runat="server"></cc1:PopupWin>
                        </div>
                    </div>
                    <!-- 桌面背景结束 -->
                </div>
            </td>
        </tr>
        <tr>
            <td style='width: 100%; height: 18px;'>
            </td>
        </tr>
    </table>
    <!--system about-->
    <div id="win">
        <div id="SysAbout" style="display: none; position: absolute; top: 55px; left: 200px;"
            onmousedown="startDrag(this)" onmouseup="stopDrag(this)" onmousemove="drag(this)">
            <table width="418" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="right" background="images/about_top.jpg" style="height: 29px">
                        <table width="97%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="93%" height="29" align="left" style="font-size: 9pt; font-family: '宋体';">
                                    <strong>关于Easy-PM工程项目管理系统</strong>
                                </td>
                                <td width="7%" align="center">
                                    <img src="images/about_close.jpg" width="16" height="15" style="cursor: hand;" onclick="CloseAbout();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top" bgcolor="#EAF2FD" style="border-right: 1px solid #0956E4;
                        border-left: 1px solid #0956E4; height: 240px;">
                        <table width="337" border="0" cellspacing="0" cellpadding="0" style="font-size: 9pt;
                            color: #333333; font-family: '宋体';">
                            <tr>
                                <td height="25">
                                </td>
                            </tr>
                            <tr>
                                <td height="20" align="left">
                                    <img src="images/about_logo.jpg" width="94" height="21" />
                                </td>
                            </tr>
                            <tr>
                                <td height="24" align="left">
                                    工程项目管理系统(承包商版)
                                </td>
                            </tr>
                            <tr>
                                <td height="24" align="left">
                                    <asp:Label ID="lblVersion" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="24" align="left">
                                    <asp:Label ID="lblCopyright" Text="版权所有 (c) 2003-2011 justwin" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="337" border="0" cellspacing="0" cellpadding="0" style="font-size: 9pt;
                            color: #333333; line-height: 200%;">
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    本软件受版权法保护，任何单位或个人在未经合法授权的情况下，不得复制期限部分或全部内容。违者将承担法律责任。
                                </td>
                            </tr>
                            <tr>
                                <td height="15">
                                </td>
                            </tr>
                        </table>
                        <table width="99%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    . Easy-PM工程项目管理系统(承包商版)
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="center">
                                    <img src="images/about_bottom.jpg" height="12" />
                                </td>
                                
                            </tr>
                        </table>
                        <table width="88%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="20" align="right">
                                    <label>
                                        <input type="button" name="button" id="button" value="  确定  " onclick="CloseAbout();" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="1">
                        <img src="images/about_bottombg.jpg" width="418" height="2" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="div_win" data-options="modal:true, minimizable:false">
        <iframe frameborder="0" style="width: 100%; height: 98%;"></iframe>
    </div>
    <div id="div_win2" data-options="modal:true, minimizable:false">
        <iframe frameborder="0" style="width: 100%; height: 98%;"></iframe>
    </div>
    <div id="popup_setting" title="个人设置" style="display: none">
        <iframe id="ifFramPrj" frameborder="0" width="99%" height="99%" src="PopupSet.aspx">
        </iframe>
    </div>
    <asp:HiddenField ID="hfldVersion" runat="server" />
    </form>
</body>
<script type="text/javascript" language="javascript">
    var desktop = new AjaxDesktop("desktop");
    desktop.fire();

    Init();
</script>
</html>
