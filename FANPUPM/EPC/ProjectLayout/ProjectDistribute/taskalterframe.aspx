<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taskalterframe.aspx.cs" Inherits="TaskAlterFrame" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>TaskAlterFrame</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script>
		function checkQuantity()
		{
			var vquan = document.getElementById('TxtAlterQuantity');
			if(vquan.value == "" || vquan.value.length==0)
			{
				alert('请输入变更数量!');
				vquan.focus();
				return false;
			}
			return true;
		}
		function CheckInputIsFloat(keyCode,e)
		{
			if((keyCode>95 && keyCode<106) || (keyCode>47 && keyCode<58) || keyCode == 8|| keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13)
			{
			}
			else if(keyCode == 110 || keyCode==190)
			{
				if(e.value == "" || e.value.indexOf(".") != -1)
				{
					return false;
				}
			} 
			else
			{
				return false;
			}
		}
		</script>
	</head>
	<body class="body_frame" scroll="no" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onload="selrow3('UProjectList1_tvProjectt1')">
		<form id="Formx" method="post" runat="server">
			<TABLE id="Tablx" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td>
					<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="15%" height="100%">
									<div style="border:solid 0px red; width:200px; height:100%; overflow:auto;"><MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList1" runat="server" /></div>
								</td>
								<td vAlign="top" width="85%"><iframe id="InfoList" name="InfoList" frameborder="0" width="100%" scrolling="no" height="100%" runat="server"></iframe>
								</td>
							</tr>
						</table>
					</td>	
				</TR>
				<tr>
					<td height="130"><iframe id="fraTask" name="fraTask" src="TaskEdit.aspx" width="100%" height="100%" frameBorder="0"></iframe>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
