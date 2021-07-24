<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlarmNumFrame.aspx.cs" Inherits="StockManage_basicset_AlarmNumFrame" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>设置库存预警数量</title></head>
<body>
    <form id="form1" runat="server">
        <table class="table-none" width="100%" border="1" style="border-collapse: collapse;
            height: 100%;">
            <tr>
                <td style="width: 200px; height:99%;" valign="top">
                    <div style="border: solid 0px red; height:100%; width:200px; overflow: auto; padding-top: 0px;
                        margin-top: 0px;">
                        <asp:TreeView ID="tvTreasury" Target="InfoList" Height="99%" ShowLines="true" runat="server"><SelectedNodeStyle Font-Underline="true" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" /></asp:TreeView>
                    </div>
                </td>
                <td valign="top" align="left" style="border:solid 0px red; height:99%;">
                    <iframe id="InfoList" name="InfoList" frameborder="0" width="100%;" scrolling="no" height="100%" src="SetAlarmNum.aspx" runat="server"></iframe>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdTname" runat="server" />
    </form>
</body>
</html>
