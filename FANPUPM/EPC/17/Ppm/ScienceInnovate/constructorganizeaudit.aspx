<%@ Page Language="C#" AutoEventWireup="true" CodeFile="constructorganizeaudit.aspx.cs" Inherits="ConstructOrganizeAudit" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>施工组织审核</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
	</head>
	<script language="JavaScript">
		function GoBack()
		{
			window.parent.history.go(-1)
		}
	</script>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
			<div class="divContent2">
			<table width="100%">
			        <TR>
						<TD class="divHeader">施工组织审核</TD>
					</TR>
			</table>
				<TABLE id="Table1" cellSpacing="0" cellPadding="5px" border="0" class="tableContent2" style="width: 90%">
					
					<TR>
						<TD class="word">
                            审 核 人：</TD>
						<TD class="txt">
							<asp:TextBox ID="TxtAuditPerson" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word" style="HEIGHT: 19px">
                            审核时间：</TD>
						<TD class="txt">
							<JWControl:DateBox ID="DateAuditTime" runat="server"></JWControl:DateBox></TD>
					</TR>
					<TR>
						<TD class="word" style="HEIGHT: 19px">审核结果：</TD>
						<TD class="txt" style="HEIGHT: 19px">
							<asp:DropDownList ID="DDLAuditResult" Width="144px" Visible="false" runat="server"></asp:DropDownList>
                            <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" Width="197px" runat="server"><asp:ListItem Value="0" Text="通过" /><asp:ListItem Value="1" Text="不通过" /></asp:RadioButtonList></TD>
					</TR>
					<TR>
						<TD class="word" >审核意见：</TD>
						<TD class="txt" >
							<asp:TextBox ID="TxtAuditIdea" TextMode="MultiLine" Height="57px" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="word">备注：</TD>
						<TD class="txt">
							<asp:TextBox ID="TxtRemark" TextMode="MultiLine" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="txt" colSpan="2" align =right>
							&nbsp; &nbsp;&nbsp;<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl></TD>
					</TR>
				</TABLE>
				<div class="divFooter2">
				  <table class="tableFooter2">
				    <tr>
				      <td>
							<asp:Button ID="BtnSave" Text="提\u3000交" OnClick="BtnSave_Click" runat="server" />
                           <INPUT type="button" value="取　消" onclick="javascript:top.frameWorkArea.window.desktop.getActive().close();" id="Button1" onserverclick="Button1_ServerClick"> &nbsp;
                     </td>
				   </tr>
				</table>
			   </div>
			</div>
		</form>
	</body>
</HTML>
