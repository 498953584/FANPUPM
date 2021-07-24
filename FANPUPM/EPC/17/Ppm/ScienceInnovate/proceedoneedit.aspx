<%@ Page Language="C#" AutoEventWireup="true" CodeFile="proceedoneedit.aspx.cs" Inherits="ProceedOneEdit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>进步收益额编写</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
		function checkNum(obj)
		{
			if (!(new RegExp(/^(\d+\.+\d)?(\d+$)/).test(obj.value)))
			{
				alert("应输入数值！");
				obj.select();
				obj.focus();
			}
		}
		function IsCanAudit()
		{
			var url = "ProceedPrjAudit.aspx?MainId="+document.getElementById("hidMainId").value;
			if(document.getElementById("hidIsNew").value == "true")
			{
				alert("请先保存当前页信息！");
			}
			else
			{
				window.showModalDialog(url,window,"dialogHeight:400px;dialogWidth:500px;center:1;help:0;status:0;");
			}
		}
		</script>
	</head>
	<body class="body_popup">
		<form id="Form1" method="post" runat="server">
			<table class="table-form" height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="td-head" colSpan="5">上报技术收益</td>
				</tr>
				<tr>
					<td class="td-toolsbar" colSpan="5"><INPUT type="button" value="项目部审核情况" onclick="IsCanAudit();" class="button-normal" style="WIDTH: 120px"></td>
				</tr>
				<tr>
					<td noWrap align="center" height="24" class="td-label" style="HEIGHT: 24px">成果名称</td>
					<td colSpan="4" height="24" style="HEIGHT: 24px"><asp:TextBox ID="txtFruitName" Width="100%" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td noWrap align="center" height="10" class="td-label">执行成果人/单位名</td>
					<td colSpan="2" height="10"><asp:TextBox ID="txtAdministerFruitUnit" Width="100%" runat="server"></asp:TextBox></td>
					<td align="center" height="10" class="td-label">其它人员数量</td>
					<td height="10"><asp:TextBox ID="txtEtcaeterasPeopleAmount" Width="100%" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td noWrap align="center" height="10" rowSpan="2" class="td-label">应用工程（生产）</td>
					<td align="center" colSpan="2" height="10" class="td-label">项目名称</td>
					<td colSpan="2" height="10"><asp:TextBox ID="txtAppPrejectName" Width="100%" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="center" height="10" class="td-label">开始时间
					</td>
					<td height="10"><JWControl:DateBox ID="txtStartDate" Width="100%" runat="server"></JWControl:DateBox></td>
					<td align="center" height="10" class="td-label">结束时间</td>
					<td height="10"><JWControl:DateBox ID="txtEndDate" Width="100%" runat="server"></JWControl:DateBox></td>
				</tr>
				<tr>
					<td align="center" height="10" class="td-label">技术进步收额(元)</td>
					<td colSpan="4" height="10"><asp:TextBox ID="txtPPMAdvancementIncomeCount" Width="100%" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="center" class="td-label">技术进步收益额计算书</td>
					<td colSpan="4"><asp:TextBox ID="txtAccount" Width="100%" TextMode="MultiLine" Height="60px" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="center" class="td-label">成果主要内容</td>
					<td colSpan="4"><asp:TextBox ID="txtFruitContent" Width="100%" TextMode="MultiLine" Height="60px" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td align="center" colSpan="3" height="10" class="td-label">总工程师</td>
					<td align="center" height="10" class="td-label">工程(技术)部长</td>
					<td align="center" height="10" class="td-label">经营(财务)部长</td>
				</tr>
				<tr>
					<td align="center" colSpan="3" height="10">
						<input id="hidIsNew" type="hidden" name="Hidden1" runat="server" />
 <input id="hidPrjCode" type="hidden" size="1" runat="server" />

						<input id="hidMainId" style="WIDTH: 46px; HEIGHT: 22px" type="hidden" size="2" runat="server" />
<asp:TextBox ID="txtEngineer" runat="server"></asp:TextBox></td>
					<td align="center" height="10"><asp:TextBox ID="txtPrejectMinister" runat="server"></asp:TextBox></td>
					<td align="center" height="10"><asp:TextBox ID="txtDealinMinister" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td colSpan="5" class="td-submit"><FONT face="宋体">
							<asp:Button ID="btnSave" Text="保  存" OnClick="btnSave_Click" runat="server" />&nbsp;
							<input type="button" value="取 消" onclick="window.returnValue=false;window.close();" id="Button1" runat="server" />
&nbsp;
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></FONT></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
