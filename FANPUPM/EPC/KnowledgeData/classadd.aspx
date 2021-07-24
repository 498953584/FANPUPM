<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classadd.aspx.cs" Inherits="ClassAdd" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>类型管理</title>
    <script type="text/javascript">
        // 取消
        function btnCancelClick() {
            top.ui.closeTab();
        } 
    </script>
    <meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

    <base target="_self">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
    <form id="Form1" method="post" runat="server">
    <table width="100%">
        <tr>
            <td class="divHeader" style="width: 100%">
                资料类别管理
            </td>
        </tr>
    </table>
    <table class="tableContent2" id="Table1" cellspacing="0" cellpadding="5px" width="100%"
        align="center" border="0">
        <tr>
            <td class="word">
                类别名称：
            </td>
            <td colspan="3" class="txt">
                <asp:TextBox ID="txtTypeName" MaxLength="50" Width="100%" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RFVPTypeName" ErrorMessage="类别名称不能为空！" Display="None" ControlToValidate="txtTypeName" runat="server"></asp:RequiredFieldValidator></FONT>
            </td>
        </tr>
        <tr>
            <td class="word">
                说明：
            </td>
            <td colspan="3" class="txt">
                <asp:TextBox ID="TxtRemark" MaxLength="50" Width="100%" Rows="10" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td class="td-label">
                是否有效：
            </td>
            <td>
                <font face="宋体">
                    <asp:RadioButton ID="rdbIsValid_y" Checked="true" GroupName="va" Text="是" runat="server" /><asp:RadioButton ID="rdbIsValid_n" GroupName="va" Text="否" runat="server" /></font>
            </td>
            <td class="td-label">
                是否可见：
            </td>
            <td>
                <asp:RadioButton ID="rdbIsVisible_y" Checked="true" GroupName="vi" Text="是" runat="server" /><asp:RadioButton ID="rdbIsVisible_n" GroupName="vi" Text="否" runat="server" />
            </td>
        </tr>
        <tr style="display: none">
            <td class="td-label">
                <font face="宋体">是否固定：</font>
            </td>
            <td>
                <asp:RadioButton ID="rbdIsFixup_y" GroupName="fi" Text="是" runat="server" /><asp:RadioButton ID="rbdIsFixup_n" Checked="true" GroupName="fi" Text="否" runat="server" />
            </td>
            <td class="td-label">
                <font face="宋体">可否删除：</font>
            </td>
            <td>
                </FONT><asp:RadioButton ID="rdbIsDelete_y" Checked="true" GroupName="de" Text="是" runat="server" /><asp:RadioButton ID="rdbIsDelete_n" GroupName="de" Text="否" runat="server" />
            </td>
        </tr>
    </table>
    <div class="divFooter2">
        <table class="tableFooter2">
            <tr>
                <td colspan="4">
                    <asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;
                    <input onclick="btnCancelClick();" type="button" value="取 消">&nbsp;
                    <asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server"></asp:ValidationSummary>
                </td>
            </tr>
        </table>
    </div>
    <JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
