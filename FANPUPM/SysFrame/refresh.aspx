<%@ Page Language="C#" AutoEventWireup="true" CodeFile="refresh.aspx.cs" Inherits="Refresh" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>Refresh</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
		function editUserInfo( objValue,objState )
		{
			window.parent.buttom.window.document.all.l_num.innerText = objValue+" ";
			window.parent.buttom.window.document.all.l_state.innerText = "状态："+objState;
			
		}
		
		function loginOut()
		{
			//alert('您的登陆时间过期，请点击确定重新登陆!');
			//window.top.close();
		}
		
		function returnOut(ip)
		{
			 alert('IP地址为 '+ip+' 的用户使用了相同的用户名登录!您被强行退出，请与系统管理员联系');
			 window.top.close();
		}
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></FONT>
		</form>
	</body><script language="javascript">document.body.oncontextmenu=new Function("return false;");</script>
</HTML>
