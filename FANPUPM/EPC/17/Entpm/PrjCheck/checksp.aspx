<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checksp.aspx.cs" Inherits="CheckSP" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>检查审核</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />

    <!-- 清除页面缓存-->
    <base target="_self"></base>

    <script language="javascript">
        window.onload = function() {
            document.getElementById('Button_save').onclick = btnSaveClick;
        }
        function btnSaveClick() {
            if (!document.getElementById('TextBox_spr').value) {
                alert("系统提示：\n\n审核人不能为空");
                return false;
            }
        }
    </script>

</head>
<body scroll="no">
    <form id="Form1" method="post" runat="server">
    <div class="divContent2">
        <table width="100%">
            <tr>
                <td class="divHeader">
                    项目检查审核
                </td>
            </tr>
        </table>
        <table class="tableContent2" cellspacing="0" cellpadding="5px" width="100%" border="0">
            <tr>
                <td class="word" style="white-space: nowrap;">
                    审核人
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_spr" MaxLength="20" Width="100%" CssClass="text-input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    是否同意
                </td>
                <td colspan="3" class="txt">
                    <asp:RadioButton ID="RadioButton_ok" Text="通过" GroupName="gr1" Checked="true" BorderStyle="None" runat="server" />
                    <asp:RadioButton ID="RadioButton_no" Text="驳回" GroupName="gr1" BorderStyle="None" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    审核日期
                </td>
                <td colspan="3" class="txt" style="white-space: nowrap;">
                    
                    <JWControl:DateBox ID="DateBox_sprq" Width="99%" runat="server"></JWControl:DateBox>
                </td>
            </tr>
            <tr>
                <td class="word" style="white-space: nowrap;">
                    审核意见
                </td>
                <td colspan="3" class="txt">
                    <asp:TextBox ID="TextBox_spyj" Width="100%" TextMode="MultiLine" MaxLength="500" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="divFooter2">
            <table class="tableFooter2">
                <tr>
                    <td>
                        <asp:Button ID="Button_save" Text="保 存" OnClick="Button_save_Click" runat="server" /><font face="宋体">&nbsp;&nbsp; </font>
                        <input type="button" onclick='window.close()' value='取 消' />
                    </td>
                </tr>
            </table>
        </div>
        <JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
    </div>
    </form>
</body>
</html>
