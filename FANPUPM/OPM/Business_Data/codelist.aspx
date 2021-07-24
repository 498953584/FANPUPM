<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codelist.aspx.cs" Inherits="CodeList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>基本编码</title>
    <base target="_self" />
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script language="javascript" src="/SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
    <script lang="javascript">
        var globalCodeID = 0;
        var globalParentCodeID = 0;

        function clickCodeRow(codeID, parentCodeID, isAllowUpd, isAllowDel) {
            globalCodeID = codeID;
            globalParentCodeID = parentCodeID;
            document.getElementById('HdnCodeID').value = codeID;
            document.getElementById('BtnUpd').disabled = !isAllowUpd;
            document.getElementById('BtnDel').disabled = !isAllowDel;
        }

        function switchDisplay(obj, t1, t2) {
            /*在这之前增加你的处理代码*/
            doSwitchDisplay(obj, 'TblCodeTree', 'HdnCodeUse', t1, t2, '../../'); //调用样式设置
        }
        function isChange() {
            return (document.getElementById('HdnIsChange').value == "1");
        }

        function OpenBasicCodeEdit(opType) {
            var state = document.getElementById("hdnstate").value;
            var typeID = document.getElementById('HdnTypeID').value;

            var url = "/OPM/Business_Data/CodeEdit.aspx?t=" + opType + "&cid=" + globalCodeID + "&pid=" +
                globalParentCodeID + "&tid=" + typeID + "&Random=" + new Date().getTime();

            top.ui._codeedit = window; //不可少

            toolbox_oncommand(url, "类别管理"); //引用js
        }
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
    </script>
    <style type="text/css">
        .dgheader
        {
            background-color: #EEF2F5;
            white-space: nowrap;
            overflow: hidden;
            font-weight: normal;
            text-align: center;
            border-color: #CADEED;
            color: #727FAA;
            padding-left: 6px;
            padding-right: 6px;
            height: 25px;
        }
        .divFooter
        {
            height: 24px;
            background: url(/images1/divBottomBack.jpg) repeat-x;
            vertical-align: middle;
        }
    </style>
</head>
<body class="body_frame" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table class="table-none" id="Table2" height="100%" cellspacing="0" cellpadding="0"
        width="100%" border="1">
        <tr>
            <td class="td-toolsbar" style="text-align: left">
                <input id="HdnTypeID" type="hidden" size="1" name="HdnTypeID" runat="server" />

                <input id="HdnCodeID" type="hidden" size="1" name="HdnCodeID" runat="server" />

                <input id="HdnCodeUse" type="hidden" size="1" name="HdnCodeUse" runat="server" />

                <input id="HdnIsChange" type="hidden" size="1" name="HdnIsChange" value="0" runat="server" />

                <asp:Button ID="BtnClose" Text="关  闭" CssClass="button-1" runat="server" />&nbsp;
                <asp:Button ID="BtnAdd" Text="新增" OnClick="BtnAdd_Click" runat="server" />
                <asp:Button ID="BtnUpd" Text="编辑" Enabled="false" OnClick="BtnUpd_Click" runat="server" />
                <asp:Button ID="BtnDel" Text="删除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div id="dvScheduleBox" class="gridBox">
                    <asp:Table ID="TblCodeTree" CssClass="grid" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="None" BorderWidth="0px" Width="100%" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="编码名称" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="是否默认" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="是否固定" Visible="false" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="简码" runat="server"></asp:TableCell><asp:TableCell Width="60px" Wrap="false" Text="序号" runat="server"></asp:TableCell></asp:TableRow></asp:Table>
                </div>
            </td>
        </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    <input type="hidden" id="hdnstate" runat="server" />

    </form>
</body>
</html>
