<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectdept.aspx.cs" Inherits="SelectDept" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN">
<HTML>
	<head runat="server"><title>垂直拆分框架集</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
		<script language="javascript" type="text/javascript">
		function confirmMan()
		{
			var manList =rFrame.Form1.LBoxSelectedMan;					
			var SysInfo = window.dialogArguments;

			try{
				SysInfo[0]=manList.options[0].text;
				SysInfo[1]=manList.options[0].value;
			}
			catch(e)
			{
				SysInfo[0]="";
				SysInfo[1]="";
			}

		    window.returnvalue=SysInfo;
		    window.close();	  		
		}
		</script>
	</head>
	<body onunload="javascrpt:confirmMan();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
				height="100%">
				<TR>
					<TD width="50%">
						<iframe id="lFrame" name="lFrame" src="SysInfo.aspx" width="100%" height="100%" frameborder="1" runat="server"></iframe>
					</TD>
					<TD width="50%">
						<iframe id="rFrame" name="rFrame" src="SelectedDept.aspx" width="100%" height="100%" frameborder="1" runat="server"></iframe>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
