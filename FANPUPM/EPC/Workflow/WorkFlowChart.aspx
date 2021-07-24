<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowChart.aspx.cs" Inherits="WorkFlowChart" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>NodeList</title>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
    <script type="text/javascript">
        function changebkcolor(obj) //单元格选中时改变背景颜色
        {
            for (i = 0; i < document.getElementById("tbFlowChart").rows.length; i++) {
                for (var j = 0; j < document.getElementById("tbFlowChart").rows[i].cells.length; j++) {
                    document.getElementById("tbFlowChart").rows[i].cells[j].style.backgroundColor = "#ff0066";
                }
            }
            obj.style.backgroundColor = "#ff0066";
        }
	
    </script>
</head>
<body style="margin: 0 0 0 0;">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" width="100%" style="height: 100%">
        <tbody>
            <tr>
                <td valign="top" height="100%">
                    <div style="overflow: auto; width: 100%;">
                        <table id="tbFlowChart" cellpadding="0" cellspacing="0" style="white-space: nowrap" runat="server"></table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
