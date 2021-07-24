<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScienceInnovateEditor.aspx.cs" Inherits="EPC_17_Entpm_ScienceInnovate_ScienceInnovateEditor" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>企业技术管理</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <script type="text/javascript" src="/StockManage/script/Config2.js"></script>
    <link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/jquery.js"></script>
    <!-- jQuery.EasyUI-->
    <link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <!-- jQuery.EasyUI-->
    <script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
    <script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
    <script type="text/javascript" src="/Script/jw.js"></script>
    <script type="text/javascript" src="/Script/jwJson.js"></script>
    <script type="text/javascript" src="/Script/wf.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var action = getRequestParam('Action');
            if (action == 'Query') {
                setAllInputDisabled();
            }
            document.getElementById('btnSave').onclick = btnSaveClick;
        }

        function btnSaveClick() {
            if (!document.getElementById('txtName').value) {
                alert("系统提示：\n\n仓库名称不能为空");
                return false;
            }
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divHeader2">
        <table class="tableHeader">
            <tr>
                <td>
                    <asp:Label ID="lblTitle" Font-Bold="true" Text="企业技术管理" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContent2">
        <table class="tableContent2" cellpadding="5px" cellspacing="0">
            <tr id="Tr1" visible="false" runat="server"><td class="word" runat="server">
                    企业技术编码
                </td><td class="txt mustInput" runat="server">
                    <asp:TextBox ID="txtCode" ReadOnly="false" runat="server"></asp:TextBox>
                </td></tr>
            <tr>
                <td class="word">
                    企业技术名称
                </td>
                <td class="txt mustInput">
                    <asp:TextBox ID="txtName" MaxLength="25" Width="50%" runat="server"></asp:TextBox><asp:Label ID="lab1" Text="*最多只能输入25个字" ForeColor="Red" BackColor="White" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="word">
                    备注
                </td>
                <td class="txt">
                    <asp:TextBox ID="txtRemark" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td>
                    <asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />
                    <JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl>
                    <input onclick="javascript: top.ui.closeTab();" type="button" value="取 消" id="BunClose" runat="server" />

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
