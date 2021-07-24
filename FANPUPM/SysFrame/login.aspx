<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>登陆中……</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<SCRIPT language="javascript">
	function openForm()
	{
		var w = screen.availWidth-10;
		var h = screen.availHeight;	
		
		//window.open("FrameSet.aspx,_blank,left=0,top=0,width=" + w + ",height=" + (h - 29) + ",status=no,toolbar=no,menubar=no,location=no");
		window.open("FrameSet.aspx,_blank,left=0,top=0,width=" + w + ",height=" + (h - 29) + ",toolbar=yes,location=yes,directories=no,status=no,menubar=no,scrollbars=no,resizable=yes,copyhistory=yes");
		window.location.href = "../default.aspx";
		//window.close();
	}
	
		</SCRIPT>
	</head>
	<body onload="javascript:openForm();">
		<form id="Form1" method="get" runat="server">
			<FONT face="宋体"></FONT>
		</form>
	</body>
</HTML>
