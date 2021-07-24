<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSetFrame.aspx.cs" Inherits="oa_SysAdmin_UserSet_PasswordSet_UserSetFrame" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title>
	<script language="javascript">
		function demots() {
			alert("对不起，DEMO版无此权限！");
			return;
		}
	</script>
</head>
<body class="body_frame" scroll="no">
	<form id="form1" runat="server">
	<div>
		<table id="table1" width="100%" height="100%" cellpadding="0" cellspacing="0" border="1"
			class="table-normal">
			<tr>
				<td width="120px" valign="top">
					<table width="100%" cellpadding="0" cellspacing="0" border="1" class="table-normal"
						style="background-color: ghostwhite">
						<tr>
							<td style="height: 22px" align="center">
								<a class="firstpage" href="PwdSetting.aspx" target="iframeMail">密码设置</a>
							</td>
						</tr>
						<tr style="display: none">
							<td style="height: 22px" align="center">
								<a class="firstpage" onclick="demots()" target="iframeMail">常用菜单</a>
							</td>
						</tr>
						<tr style="display: none">
							<td style="height: 22px" align="center">
								<a class="firstpage" href="SkinSwitch.aspx" target="iframeMail">换肤设置</a>
							</td>
						</tr>
						<tr id="tr_popup" runat="server"><td style="height: 22px" align="center" runat="server">
								<a class="firstpage" href="PopupSetting.aspx" target="iframeMail">冒泡提醒</a>
							</td></tr>
						<tr id="tr_projectTree" runat="server"><td style="height: 22px" align="center" runat="server">
								<a class="firstpage" href="../../../../EPC/Pm/PmTypeTreeSet.aspx" target="iframeMail">
									项目树设置</a>
							</td></tr>
						<tr id="tr_rtxsetting" visible="false" runat="server"><td style="height: 22px" align="center" runat="server">
								<a class="firstpage" href="RTXSetting.aspx" target="iframeMail">RTX设置</a>
							</td></tr>
					</table>
				</td>
				<td valign="top">
					<iframe id="iframeMail" name="iframeMail" width="100%" height="100%" frameborder="0">
					</iframe>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
