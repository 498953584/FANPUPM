<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowClassEdit.aspx.cs" Inherits="oa_UserDefineFlow_FlowClassEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>自定义流程</title>
    <base target="_self" />
    <script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js">
    </script>
    <style type="text/css">
        .bodyStyle
        {
            filter: Alpha(Opacity="0",FinishOpacity='75',Style='2');
            background-color: blue;
        }
    </style>
</head>
<body>
    <form id="Formx" runat="server">
    <div align="center">
        <table class="tableAudit" border="1">
            <tr>
                <td class="td-label">
                    流程名称
                </td>
                <td class="td-content">
                    <asp:TextBox ID="txtBookLibraryName" MaxLength="25" Style="width: 200px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
                    <input id="BtnClose" onclick="top.ui.closeWin();"
                        type="button" value="关  闭" />
                    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
