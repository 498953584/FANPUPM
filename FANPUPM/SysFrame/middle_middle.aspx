<%@ Page Language="C#" AutoEventWireup="true" CodeFile="middle_middle.aspx.cs" Inherits="WebForm8" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>WebForm8</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript">
		var showIden = true; //是否显示菜单标识
		var showLink = true; //是否显示相关链接标识
		var tempMenuFrame = "";
		var tempLinkMenu = "";

		function ShowMenu(direct)
		{
			if( showIden == true )
			{
				tempMenuFrame = top.menu.cols;
				top.menu.cols="0,14,*";
				//document.all.handler.src = "images/handlerback.gif";
				document.all("arrow").src = direct + "/fenge1.gif";
				showIden = false;
			}	
			else
			{
			top.menu.cols=tempMenuFrame;
			//document.all.handler.src = "images/middle_middle.gif";
			document.all("arrow").src = direct + "/fenge.gif";
			showIden = true;
			}
			/*
			var obj = top.middle_left_top.shjw_mainWindow;
			if( obj != null )
			{
				obj.setBounds(0,0,top.document.all.main.width,"100%");
			}
			*/	
		}
		</script>
	</head>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<table width="11" border="0" cellspacing="0" cellpadding="0" height="100%">
				<tr>
					<td background="<%=this.strSkinPath %>/fenge_bg.gif" valign="middle"><img name="arrow" src="<%=this.strSkinPath %>/fenge.gif" width="11" height="45" border="0" style="CURSOR:hand" onclick="ShowMenu('<%=this.strSkinPath %>')"></td>
				</tr>
			</table>
		</form>
	</body><script language="javascript">document.body.oncontextmenu=new Function("return false;");</script>
</HTML>
