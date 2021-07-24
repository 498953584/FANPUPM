<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CryFrame.aspx.cs" Inherits="CryFrame" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>
	<HEAD>
		<title>CryFrame</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0">
				<tr>
					<td height="100%">
						<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="25%" height="100%"><div class="gridBox"><FONT face="宋体">
											<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UCProjectList" runat="server" /></FONT></div>
								</td>
								<td vAlign="top" width="75%"><iframe id="InfoList" name="InfoList" src="" frameBorder="0" width="100%" scrolling="no"
										height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
