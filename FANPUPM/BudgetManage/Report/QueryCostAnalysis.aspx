<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryCostAnalysis.aspx.cs" Inherits="BudgetManage_Report_QueryCostAnalysis" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>项目成本分析高级查询</title>
    <script type="text/javascript" src="../../Script/jquery.js"></script>
    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.core.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.widget.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.mouse.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.draggable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.position.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" src="../../Script/jquery.ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript">
        //关闭
        function closeDialog() {
            $(parent.document).find('.ui-icon-closethick').each(function() {
                this.click();
            });
        }
        function returnWidow() {
            var name= document.getElementById('txtPrjName').value;
            var code = document.getElementById('txtPrjCode').value;
            parent.document.location = 'CostAnalysis.aspx?name='+name+'&code='+code;
            closeDialog();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align:center;">
    <table cellpadding="0" cellspacing="0" style=" margin:auto; height:220px; width:350px; text-align:right;">
        <tr>
            <td colspan="2" style=" text-align:center; font-weight:bold; font-size:17px; height:35px;">
                综合查询
            </td>
        </tr>
        <tr>
            <td>
                项目名称:
            </td>
            <td style=" height:35px;">
            <asp:TextBox ID="txtPrjName" style=" width:250px;" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" height:35px;">
                项目编号:
            </td>
            <td>
            <asp:TextBox ID="txtPrjCode" style=" width:250px;" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style=" text-align:right; vertical-align:bottom;">
                <input type="button" value="查 询" onclick="returnWidow()" />
                &nbsp;&nbsp;
                <input type="button" onclick="closeDialog()" value="取 消" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
