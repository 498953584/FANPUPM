<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealWithApply.aspx.cs" Inherits="DealWithApply" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"><title>处理申请</title>
    <script language="javascript" type="text/javascript">
    window.name = "win";
    <!--
    
    -->
    </script>
</head>
<body class="body_popup" scroll="no">
    <form id="form1" target="win" runat="server">
        <table class="table-form" id="Table1" cellspacing="0" cellpadding="0" width="100%" border="1">
            <tr>
	            <td class="td-head" height="20">
                    退回申请</td>
            </tr>
            <tr>
	            <td><asp:TextBox ID="txtSendTo" CssClass="text-input" Width="100%" Enabled="false" TabIndex="1" BorderStyle="None" runat="server"></asp:TextBox></td>
	        </tr>
            <tr>
	            <td><asp:TextBox ID="txtContent" CssClass="text-input" TabIndex="2" Width="100%" TextMode="MultiLine" Height="38px" runat="server"></asp:TextBox><font color="#ff0000">&nbsp;</font> </td>
            </tr>
            <tr>
                <td align="center">
                <asp:CheckBox ID="CBRTX" Text="待办消息" Height="16px" Checked="true" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="CBoxSMS" Text="手机短信督办" Height="16px" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td-submit" height="20"><asp:Button ID="BtnSave" Text="确  定" OnClick="BtnSave_Click" runat="server" />&nbsp;
                    <input id="BtnClose" onclick="javascript:window.returnValue='0';window.close();" type="button" value="关  闭" name="BtnClose">
                    <asp:HiddenField ID="hdnMangeCode" runat="server" />
                </td>
            </tr>
        </table>
        <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
