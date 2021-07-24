<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addlog.aspx.cs" Inherits="addlog" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>添加维护日志</title>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<a href="log.aspx">返回列表页面</a>
		<hr />
		类型：<asp:DropDownList ID="dropLogType" runat="server"><asp:ListItem /><asp:ListItem Text="新增" /><asp:ListItem Text="修正" /><asp:ListItem Text="重构" /><asp:ListItem Text="增强" /></asp:DropDownList>
		<br />
		内容：<asp:TextBox ID="txtLog" Width="600px" runat="server"></asp:TextBox><br />
		日期：<asp:TextBox ID="txtDate" onclick="WdatePicker({isShowClear: false})" runat="server"></asp:TextBox>
		<br />
		<asp:Button ID="btnOk" Text="保存" OnClick="btnOk_Click" runat="server" />
		<br />
		<hr />
		TODO:
		<br />
		格式化log.aspx
	</div>
	</form>
</body>
</html>
