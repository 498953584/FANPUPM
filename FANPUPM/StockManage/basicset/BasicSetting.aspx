<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicSetting.aspx.cs" Inherits="StockManage_basicset_BasicSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>参数设置</title>
	<style type="text/css">
		td
		{
			padding-right: 5px;
		}
	</style>
	<link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnSetNum').click(rowAdd);
		});

		function setBt(obj) {
			if (obj.checked)
				document.getElementById("btnSetNum").removeAttribute("disabled");
			else
				document.getElementById("btnSetNum").setAttribute("disabled", "disabled");
		}

		function rowAdd() {
			parent.desktop.AlarmNumFrame = window;
			var url = "/StockManage/basicset/AlarmNumFrame.aspx";
			toolbox_oncommand(url, "设置预警");
		}
	</script>
</head>
<body class="body_frame" style="height: 100%">
	<form id="form1" runat="server">
	<div style="height: 95%;" id="divContent2">
		<table border="1" style="border-collapse: collapse; width: 100%">
			
			<tr>
				<td style="width: 38%; text-align: right">
					仓库模式
				</td>
				<td style="width: 62%" colspan="1">
					<asp:RadioButtonList ID="rbModel" RepeatDirection="Horizontal" runat="server"><asp:ListItem Text="总分模式" Value="TotalMode" /><asp:ListItem Text="平行模式" Value="ParallelMode" /></asp:RadioButtonList>
				</td>
			</tr>
			<tr style="display: none;">
				<td style="text-align: right">
					项目与仓库绑定
				</td>
				<td colspan="1">
					<asp:CheckBox ID="chkBind" Text="绑定" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; vertical-align: top; padding-top: 7px;">
					物资需求计划量>预算
				</td>
				<td>
					<asp:RadioButtonList ID="radlAlarm" AutoPostBack="true" OnSelectedIndexChanged="radlAlarm_SelectedIndexChanged" runat="server"><asp:ListItem Value="UnAlarm" Text="拒绝" /><asp:ListItem Value="Alarm" Text="预警设置" /></asp:RadioButtonList>
					<table cellpadding="3px" cellspacing="0px" class="unlineTable">
						<tr visible="false" runat="server"><td runat="server">
								<asp:CheckBox ID="chkRefuse" AutoPostBack="true" Text=" 拒绝" OnCheckedChanged="chkRefuse_CheckedChanged" runat="server" />
							</td></tr>
						<tr visible="false" runat="server"><td style="padding-left: 30px;" runat="server">
								物资需求计划量 > 预算
							</td></tr>
						<tr visible="false" runat="server"><td runat="server">
								<asp:CheckBox ID="chkAlarm" AutoPostBack="true" Text=" 预警设置" OnCheckedChanged="chkAlarm_CheckedChanged" runat="server" />
							</td></tr>
						<tr>
							<td style="padding-left: 30px">
								<asp:CheckBox ID="chkHighAlarm" Visible="false" AutoPostBack="true" Text=" 高" OnCheckedChanged="chkHighAlarm_CheckedChanged" runat="server" />高 &nbsp;&nbsp; &nbsp;&nbsp;
								大于
								<asp:TextBox ID="txtHighAlarmLimit" Height="15px" Width="50px" CssClass="decimal_input" runat="server"></asp:TextBox>%
							</td>
						</tr>
						<tr>
							<td style="padding-left: 30px">
								<asp:CheckBox ID="chkMidAlarm" Visible="false" AutoPostBack="true" Text=" 中" OnCheckedChanged="chkMidAlarm_CheckedChanged" runat="server" />中 &nbsp;&nbsp; &nbsp;&nbsp;
								在...之间
								<asp:TextBox ID="txtMidAlarmLowerLimit" Height="15px" Width="50px" CssClass="decimal_input" runat="server"></asp:TextBox>%与
								<asp:TextBox ID="txtMidAlarmUpperLimit" Height="15px" Width="50px" CssClass="decimal_input" runat="server"></asp:TextBox>%
							</td>
						</tr>
						<tr>
							<td style="padding-left: 30px">
								<asp:CheckBox ID="chkLowAlarm" Visible="false" AutoPostBack="true" Text=" 低" OnCheckedChanged="chkLowAlarm_CheckedChanged" runat="server" />低 &nbsp;&nbsp; &nbsp;&nbsp;
								在...之间
								<asp:TextBox ID="txtLowAlarmLowerLimit" Height="15px" Width="50px" CssClass="decimal_input" runat="server"></asp:TextBox>%与
								<asp:TextBox ID="txtLowAlarmUpperLimit" Height="15px" Width="50px" CssClass="decimal_input" runat="server"></asp:TextBox>%
							</td>
						</tr>
						<tr>
							<td>
								<asp:CheckBox ID="chkContainPending" Text=" 是否包含待审数据" runat="server" />
							</td>
						</tr>
						<tr>
							<td>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td align="right">
					低库存量预警
				</td>
				<td>
					<asp:CheckBox ID="cbNumAlarm" onclick="setBt(this)" runat="server" />
					<input type="button" id="btnSetNum" value="设置" runat="server" />

				</td>
			</tr>
			<tr style="display: none;">
				<td style="text-align: right">
					透明设置
				</td>
				<td colspan="1">
					<asp:CheckBox ID="chkTransparent" Text="透明" runat="server" />
				</td>
			</tr>
		</table>
	</div>
	<div style="height: 5%;" id="divFooter2">
		<table class="tableFooter2" width="100%" cellpadding="0" cellspacing="0">
			<tr>
				<td class="divFooter" style="text-align: right">
					<asp:Button ID="butSubmit" Text="保存" OnClick="butSubmit_Click" runat="server" /><asp:Button ID="butReset" Text="取消" OnClick="butReset_Click" runat="server" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
