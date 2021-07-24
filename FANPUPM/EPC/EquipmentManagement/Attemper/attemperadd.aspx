<%@ Page Language="C#" AutoEventWireup="true" CodeFile="attemperadd.aspx.cs" Inherits="AttemperAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>调度信息编辑</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"/>
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function openProjPicker()
			{
			
				var prj = new Array();
				prj[0] = "";
				prj[1] = "";
				var url= "/CommonWindow/ProjectPop.aspx";
				window.showModalDialog(url,prj,"dialogHeight:280px;dialogWidth:400px;center:1;help:0;status:0;");
				if (prj[0]!="")
				{
					document.getElementById('hdnProjectCode').value = prj[0];
					document.getElementById('txtToproject').value = prj[1];
				}
			}
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
					<td class="td-head" colspan="4">机械器具调度操作</td>
				</tr>
				<tr>
					<td class="td-label" align="right" width="15%">调度单号：</td>
					<td width="35%">
						<asp:TextBox ID="txtAttemperCode" Columns="25" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="调度单号不能为空！" ControlToValidate="txtAttemperCode" Display="None" runat="server"></asp:RequiredFieldValidator></td>
					<td class="td-label" align="right" width="15%">相关项目：</td>
					<td width="35%">
						<asp:TextBox ID="txtToproject" Columns="25" ReadOnly="true" style="BACKGROUND-COLOR:#ffffc0" runat="server"></asp:TextBox>
						<img style="CURSOR: hand;valign: bottom" onclick="openProjPicker();"  src="../../../Images/corp.gif"
							width="16" border="0"><input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />

						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="调往项目不能为空！" ControlToValidate="txtToproject" Display="None" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="td-label" align="right" width="15%">台&nbsp;&nbsp;&nbsp; 班：</td>
					<td width="35%">
						<asp:TextBox ID="txtIntendingTime" Columns="25" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="台班数不能为空！" ControlToValidate="txtIntendingTime" Display="None" runat="server"></asp:RequiredFieldValidator>
						<asp:RangeValidator ID="RangeValidator1" ErrorMessage="台班数必须为整数！" ControlToValidate="txtIntendingTime" Display="None" Type="Integer" MinimumValue="1" MaximumValue="65535" runat="server"></asp:RangeValidator></td>
					<td class="td-label" align="right" width="15%">台班单价(元)：</td>
					<td width="35%">
						<asp:TextBox ID="txtPrice" runat="server"></asp:TextBox><FONT face="宋体">元
							<asp:RangeValidator ID="RVPrice" Display="None" ControlToValidate="txtPrice" ErrorMessage="台班单价必须是数字！" Type="Double" MaximumValue="65535" MinimumValue="1" runat="server"></asp:RangeValidator>
							<asp:RequiredFieldValidator ID="RFVPrice" Display="None" ControlToValidate="txtPrice" ErrorMessage="台班单价不能为空！" runat="server"></asp:RequiredFieldValidator></FONT></td>
				</tr>
				<tr>
					<td class="td-label" align="right" width="15%">开始时间：</td>
					<td width="35%">
						<JWControl:DateBox ID="txtStarDate" Columns="25" runat="server"></JWControl:DateBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="开始时间不能为空！" ControlToValidate="txtStarDate" Display="None" runat="server"></asp:RequiredFieldValidator></td>
					<td class="td-label" align="right" width="15%">结束时间：</td>
					<td width="35%">
						<JWControl:DateBox ID="txtEndDate" Columns="25" runat="server"></JWControl:DateBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="结束时间不能为空！" ControlToValidate="txtEndDate" Display="None" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="td-label" align="right" width="15%">随&nbsp;&nbsp; 员：</td>
					<td colspan="3">
						<asp:TextBox ID="txtSuite" Columns="70" Rows="2" TextMode="MultiLine" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="随员不能为空！" ControlToValidate="txtSuite" Display="None" runat="server"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="td-label" align="right" width="15%">备注说明：</td>
					<td width="35%" colspan="3">
						<asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="4" CssClass="textarea-input" Columns="70" onkeyup="checklen(this,200);" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="td-submit" colspan="4"><asp:Button ID="btnAdd" Text="确  定" OnClick="btnAdd_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<input onclick="javascript:window.returnValue=false;window.close();" type="button" value="关  闭">
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
						<asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
