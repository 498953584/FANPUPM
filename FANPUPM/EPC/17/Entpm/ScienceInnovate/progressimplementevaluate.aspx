<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressimplementevaluate.aspx.cs" Inherits="ProgressImplementEvaluate" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ProgressImplementEvaluate</title>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/ecmascript" src="/StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" language="javascript">
        function doShow() {
            var url = '/EPC/17/common/ScienceInnovate/ProgressImplementEvaluateAll.aspx';
            url += "?MainId=" + $('#hidMainId').val();
            top.ui._progressimplementevaluate = window;
            toolbox_oncommand(url, "实施评价 ");
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
        }
    </style>
</head>
<body class="body_popup">
    <form id="Form1" method="post" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style="height: 20px;">
                <td class="divHeader">
                    实施情况评价
                </td>
            </tr>
        </table>
    </div>
    <table id="Table1" class="tableContent2" cellpadding="5px" cellspacing="0">
        <tr>
            <td class="word">
                评价人<input type="hidden" id="hidMainId" runat="server" />

            </td>
            <td class="txt">
                <asp:TextBox ID="txtAppraisePeople" ReadOnly="true" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word">
                评价时间
            </td>
            <td class="txt">
                <JWControl:DateBox ID="txtAppraiseTime" runat="server"></JWControl:DateBox>
                <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
            </td>
        </tr>
        <tr>
            <td class="word">
                评价
            </td>
            <td class="txt">
                <asp:TextBox ID="txtAppraise" TextMode="MultiLine" Height="200px" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr class="tableFooter2">
                <td>
                    <asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />&nbsp;
                    <input type="button" value="取  消" onclick="window.returnValue=true;top.ui.closeTab();">
                    <input type="button" value="全部评价" onclick="doShow();" style="width: 70px;">&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
