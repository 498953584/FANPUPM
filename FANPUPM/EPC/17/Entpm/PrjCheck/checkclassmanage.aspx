<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkclassmanage.aspx.cs" Inherits="CheckClassManage" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>项目检查-编辑-查看-新增 </title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />

    <!-- 清除页面缓存-->
    <base target="_self">
    <script src="../../../../Script/jquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //当备注长度超过150，弹出提示信息
        function save_click() {
            var txtBRemarklen = $('#TextBox_remark').val().length;
            if (txtBRemarklen > 150) {
                alert('你输入长度超过150字，信息将无法保存！');
                return false;
            }
        }
        //当备注长度超过150时截取字符串长度
        function limit_fontLen() {
            var txtBRemarklen = $('#TextBox_remark').val().length;
            if (txtBRemarklen > 150) {
                var reMarkTxt = $('#TextBox_remark').text().substr(0, 150);
                $('#TextBox_remark').html(reMarkTxt);
            }
        }
    </script>
</head>
<body class="divContent2" scroll="no">
    <form id="Form1" method="post" runat="server">
    <table width="100%">
        <tr>
            <td class="divHeader">
                项目检查类别
            </td>
        </tr>
    </table>
    <table class="tableContent2" cellspacing="0" cellpadding="5px" width="100%" border="0">
        <tr>
            <td class="word" style="white-space: nowrap">
                检查类别名称
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="TextBox_name" CssClass="text-input" Width="100%" MaxLength="100" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="word">
                备注
            </td>
            <td class="txt" colspan="3">
                <asp:TextBox ID="TextBox_remark" onpropertychange="limit_fontLen();" CssClass="text-input" Width="100%" TextMode="MultiLine" Height="56px" MaxLength="150" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="Button_save" Text="保 存" OnClientClick="save_click();" OnClick="Button_save_Click" runat="server" /><input type="button" value="取 消" onclick='top.ui.closeTab();'>
                </td>
            </tr>
        </table>
    </div>
    <JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
