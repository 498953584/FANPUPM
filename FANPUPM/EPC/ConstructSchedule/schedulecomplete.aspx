<%@ Page Language="C#" AutoEventWireup="true" CodeFile="schedulecomplete.aspx.cs" Inherits="ScheduleComplete" %>
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
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload ="selrow3('UProjectList1_tvProjectt1')">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<tr class="td-toolstbar">
								<td align="left">项目完成量</td>
								<td align="right">
									<input id="HdnProjectName" type="hidden" style="WIDTH: 10px" name="HdnProjectName" runat="server" />

									<input id="HdnProjectCode" type="hidden" style="WIDTH: 10px" name="HdnProjectCode" runat="server" />
&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="100%">
						<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="15%" height="100%">
								    <div style="width:200px;height:100%;overflow:auto;">
										<MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList1" runat="server" />
								    </div>
								</td>
								<td vAlign="top" width="85%"><iframe id="InfoList" name="InfoList" src="../UserControl1/webTreeTS.aspx"  frameBorder="0" width="100%" scrolling="no"
										height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
