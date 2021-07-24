<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taskedit.aspx.cs" Inherits="TaskEdit" %>


<HTML>
	<HEAD >
		<title>TaskEdit</title>
		<META http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Expires" content="0">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<script language="javascript">
			function checkDecimal(sourObj)
			{
				if (sourObj.value=="")
				{
					sourObj.value = 0;
				}
				if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
				{
					alert('数据类型不正确！');
					sourObj.focus();
					return;
				}
			}
		</script>
	</HEAD>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr class="td-toolsbar">
								<td width="120" align="left">项目分解</td>
								<td align=left><STRONG><FONT color="#ff0000">注：同级任务编码不能重复，且必须为三位！&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										</FONT></STRONG><input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />
&nbsp;
									<input id="hdnProjectName" style="WIDTH: 10px" type="hidden" name="hdnProjectName" runat="server" />

									<input id="hdnParentCode" style="WIDTH: 10px" type="hidden" name="hdnParentCode" runat="server" />

									<input id="hdnType" style="WIDTH: 10px" type="hidden" name="hdnType" runat="server" />

									<input id="hdnTaskCode" style="WIDTH: 10px" type="hidden" name="hdnTaskCode" runat="server" />

									<input id="hdnWbsType" name="hdnWbsType" type="hidden" style="WIDTH:10px" runat="server" />

									<input id="hdnNoteID" style="WIDTH: 10px" type="hidden" value="0" name="hdnNoteID" runat="server" />
&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top">
						<table class="table-normal" id="TABLE2" width="100%" border="1">
							<tr>
								<td class="td-label" width="10%">编码</td>
								<td style="WIDTH: 192px" width="192">
                                    <asp:TextBox ID="lbParentCode" Enabled="false" Columns="6" style=" background-color:#FFFFC0;" runat="server"></asp:TextBox><asp:TextBox ID="txtParentCode" Columns="5" Width="0px" style=" background-color:#FFFFC0;" runat="server"></asp:TextBox><FONT face="宋体">&nbsp;</FONT>
									<asp:TextBox ID="txtTaskCode" Columns="6" MaxLength="3" Width="64px" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="任务编号不能为空！" Display="None" ControlToValidate="txtTaskCode" runat="server"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="RegularExpressionValidator" Display="None" ControlToValidate="txtTaskCode" ValidationExpression="[A-Za-z0-9]{3}" runat="server"></asp:RegularExpressionValidator></td>
								<td class="td-label" width="10%">开始时间</td>
								<td width="23%"><font color="#ff0000"><b><JWControl:DateBox ID="DateStart" Width="120px" runat="server"></JWControl:DateBox></b></font></td>
								<td class="td-label" width="10%">结束时间</td>
								<td width="23%">
									<JWControl:DateBox ID="DateEnd" Width="120px" runat="server"></JWControl:DateBox></td>
							</tr>
							<TR>
								<TD class="td-label">任务名称</TD>
								<td style="WIDTH: 192px"><asp:TextBox ID="txtTaskName" Width="120px" MaxLength="20" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="任务名称不能为空！" Display="None" ControlToValidate="txtTaskName" Width="160px" runat="server"></asp:RequiredFieldValidator></td>
								<td class="td-label">单位</td>
								<td><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><asp:TextBox ID="txtUnit" Columns="10" Width="120px" MaxLength="30" runat="server"></asp:TextBox></td>
								<td class="td-label">工程量</td>
								<td><asp:TextBox ID="txtQuantity" Columns="12" Width="120px" Text="0" runat="server"></asp:TextBox></td>
							</TR>
							<TR>
								<TD class="td-label"><FONT face="宋体">单价(元)</FONT></TD>
								<TD style="WIDTH: 192px"><asp:TextBox ID="txtUnitPrice" Width="120px" Text="0" runat="server"></asp:TextBox><input id="hdnPrjGuid" type="hidden" name="hdnPrjGuid" style="WIDTH: 65px; HEIGHT: 21px" size="5" runat="server" />
(元)</TD>
								<TD class="td-label"><FONT face="宋体">类型</FONT></TD>
								<TD>
									<asp:DropDownList ID="ddlTaskType" Width="120px" runat="server"><asp:ListItem Value="1" Text="单位工程" /><asp:ListItem Value="2" Text="分部工程" /><asp:ListItem Value="3" Text="分项工程" /></asp:DropDownList></TD>
								<TD class="td-label"><FONT face="宋体"></FONT></TD>
								<TD><asp:HiddenField ID="HidRemark" runat="server" /></TD>
							</TR>
							<TR>
								<TD class="td-submit" colSpan="6">
									<asp:Button ID="btnSave" Text="保 存" CssClass="button-normal" Enabled="false" OnClick="btnSave_Click" runat="server" />&nbsp;
									<INPUT class="button-normal" type="reset" value="取 消">
									<asp:ValidationSummary ID="ValidationSummary1" ShowSummary="false" ShowMessageBox="true" runat="server"></asp:ValidationSummary></TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
