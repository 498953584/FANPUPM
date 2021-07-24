<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputeditone.aspx.cs" Inherits="InputEditOne" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>InputEditOne</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script src="../../../Web_Client/Tree.js"></script>
		<script language="javascript">
		function SelDepartment()
		{
			/*var ret = window.showModalDialog("/Tender/ProjectInfoManage/PopDpt.aspx",window,"dialogWidth:240px;dialogHeight:400px");
			if(ret != null)
			{
				if(ret[1] != null)
					document.getElementById("txtUnitName").value = ret[1];
				if(ret[0] != null)
					document.getElementById("hidUnitCode").value = ret[0];			
			}*/
		}
		function checkNum(obj)
		{
			//if (!(new RegExp(/^(\d+\.+\d)?(\d+$)/).test(obj.value)))
			//{
			//	alert("应输入数值！");
			//	obj.select();
			//	obj.focus();
			//}
		}
		</script>
	</head>
	<body class="body_popup">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE class="table-form" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%">
					<tr>
						<td colspan="2" class="td-head">上报开发投入</td>
					</tr>
					<TR>
						<TD align="center" class="td-label">编制人</TD>
						<TD>
							<asp:TextBox ID="txtWeavePeople" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD align="center" class="td-label">填报日期</TD>
						<TD><JWControl:DateBox ID="txtFillTime" ReadOnly="true" runat="server"></JWControl:DateBox></TD>
					</TR>
					<TR>
						<TD align="center" width="25%" class="td-label">填报单位</TD>
						<TD><input id="hidUnitCode" type="hidden" name="Hidden1" runat="server" />
 <input style="WIDTH: 100%; HEIGHT: 100%" id="txtUnitName" onclick="javascript:SelDepartment();" type="text" runat="server" />
</TD>
					</TR>
					<TR>
						<TD align="center" class="td-label">项目名称</TD>
						<TD>
							<input id="txtPrjName" style="WIDTH: 100%; HEIGHT: 100%" type="text" name="Text1" runat="server" />
<input id="hidPrjCode" type="hidden" name="Hidden1" runat="server" />
</TD>
					</TR>
					<TR>
						<TD align="center" class="td-label">科技开发投入额</TD>
						<TD>
							<asp:TextBox ID="txtScienceEmpolderMoney" ReadOnly="true" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD class="td-submit" colSpan="2">
							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl><input id="hidMainId" type="hidden" name="Hidden1" runat="server" />
&nbsp;
							<asp:Button ID="btnSave" Text="保  存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />&nbsp;
							<INPUT class="button-normal" onclick="window.returnValue=false;window.close();" type="button"
								value="退  出"></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
