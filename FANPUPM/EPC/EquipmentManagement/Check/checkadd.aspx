<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkadd.aspx.cs" Inherits="CheckAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>检定信息编辑</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"/>
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		function checklen(e,maxlength)
	    {
			 if(e.value.length > maxlength)
			 {
				alert('输入长度不能大于'+maxlength);
				e.focus();
				 return false;
			 }

	    }
		</script>

	</head>
	
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-form" cellspacing="0" cellpadding="0" width="100%" border="1">
				<tr>
					<td class="td-head" colspan="4" style="HEIGHT: 17px">检定记录</td>
				</tr>
				<tr>
					<td class="td-label" align="right" width="15%">检定单编号：</td>
					<td width="35%"><asp:TextBox ID="txtCode" CssClass="textarea-input" Columns="25" runat="server"></asp:TextBox></td>
					<td class="td-label" align="right" width="15%">检定类型：</td>
					<td width="35%"><asp:RadioButton ID="RbtTypeIn" Text="内检" GroupName="c" Checked="true" runat="server" /><asp:RadioButton ID="RbtTypeOut" Text="外检" GroupName="c" runat="server" /></td>
				</tr>
				<tr>
					<td class="td-label" align="right">检定结果：</td>
					<td><asp:RadioButton ID="RbtResultOk" Text="合格" GroupName="r" Checked="true" runat="server" /><asp:RadioButton ID="RbtResultNo" Text="不合格" GroupName="r" runat="server" /></td>
					<td class="td-label" align="right">检定时间：</td>
					<td><JWControl:DateBox ID="txtTime" Columns="25" runat="server"></JWControl:DateBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="检定时间不能为空" ControlToValidate="txtTime" Display="None" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="td-label" align="right">检定单位：</td>
					<td><asp:TextBox ID="txtDept" CssClass="textarea-input" Columns="25" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="检定单位不能为空" ControlToValidate="txtDept" Display="None" runat="server"></asp:RequiredFieldValidator></td>
					<td class="td-label" align="right">检 定 人：</td>
					<td><asp:TextBox ID="txtPerson" CssClass="textarea-input" Columns="25" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="检定人不能为空" ControlToValidate="txtPerson" Display="None" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="td-label" align="right">结果描述：</td>
					<td colspan="3"><asp:TextBox ID="txtDescript" CssClass="textarea-input" TextMode="MultiLine" Rows="2" Columns="70" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-label" align="right">备&nbsp;&nbsp;&nbsp; 注：</td>
					<td colspan="3"><asp:TextBox ID="txtRemark" CssClass="textarea-input" TextMode="MultiLine" Rows="3" Columns="70" onkeyup="checklen(this,150);" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-submit" colspan="4"><asp:Button ID="btnAdd" Text="确  定" OnClick="btnAdd_Click" runat="server" /><font face="宋体">&nbsp;
							<input onclick="window.returnValue=false;window.close();" type="button" value="关  闭">
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
							<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary></font></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
