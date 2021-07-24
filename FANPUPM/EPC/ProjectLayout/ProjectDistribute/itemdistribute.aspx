<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemdistribute.aspx.cs" Inherits="ItemDistribute" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ItemDistribute</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
</head>
	<body style="margin:0px;padding:0px;overflow:hidden;" onload="selrow3('UProjectList1_tvProjectt1')" >
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table id="Table3" cellSpacing="0" cellPadding="0" border="1" style="height:100%;width:100%;">
							<tr>
								<td vAlign="top" width="200">
									<DIV style="width:200px; height:100%; border:solid 0px red;overflow:auto;">
									    <MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList1" runat="server" />
									</DIV>
								</td>
								<td vAlign="top" width="90%">
								    <iframe id="InfoList" name="InfoList" frameborder="0" width="100%" scrolling="no" height="100%" src="../../../EPC/UserControl1/webTreeTS.aspx" runat="server"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="<%=this.height %>">
					    <iframe id="fraTask" name="fraTask" src="TaskEdit.aspx?jw=<%=this.jw %>" width="100%" height="100%" frameBorder="0"></iframe>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
