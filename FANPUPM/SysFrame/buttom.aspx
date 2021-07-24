<%@ Page Language="C#" AutoEventWireup="true" CodeFile="buttom.aspx.cs" Inherits="buttom" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server"><title>buttom</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" type="text/javascript">
			var tempWeek = "";
			var date;
			function DisplaySystemTime()
			{
				date = new Date();
				switch( date.getDay() )
				{
					case 0 : tempWeek = "星期日";
							break;
					case 1 : tempWeek = "星期一";
							break;
					case 2 : tempWeek = "星期二";
							break;
					case 3 : tempWeek = "星期三";
							break;
					case 4 : tempWeek = "星期四";
							break;
					case 5 : tempWeek = "星期五";
							break;
					case 6 : tempWeek = "星期六";
							break;
				}
			
				var hours = new String(date.getHours()+"");
				if( hours.length == 1 )
					hours = "0"+hours;
				var minus = new String(date.getMinutes()+"");
				if( minus.length == 1 )
					minus = "0"+minus;
				var seconds = new String(date.getSeconds()+"");
				if( seconds.length == 1 )
					seconds = "0" + seconds;
					
				var month = date.getMonth() + 1;
				obj = document.all.l_date;
				obj.innerText = date.getFullYear()+"年"+month+"月"+date.getDate()+"日 "+hours+(":"+minus+":"+seconds+" " + tempWeek );;	
			}
			
			function openUserList()
			{
				var url= "UserListPop.aspx";
				window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:340px;center:1;help:0;status:0;");
			}
			
			function shanshuo()
			{
				var msg = document.all.l_num.innerText;
				objLab.innerText = msg; 
			}
		</script>
	</head>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="20" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td height="23" align="center" background="<%=this.strSkinPath %>/bottom.gif" valign="middle"><table width="100%" border="0" cellpadding="0" cellspacing="0">
							<TR>
								<TD width="20%" valign="middle" align="center">
									<asp:Label ID="l_name" runat="server"> 登陆者姓名：</asp:Label></TD>
								<TD width="20%" valign="middle" align="center">
								<asp:Label ID="lb_cs" runat="server"></asp:Label>
									</TD>
								<TD width="30%" valign="middle" align="center">当前日期：
									<asp:Label ID="l_date" runat="server"></asp:Label></TD>
								<td width="20%" valign="middle" align="center"><FONT face="宋体">当前共有
										<asp:Label ID="l_num" OnClick="openUserList()" ForeColor="Red" style="CURSOR: hand; valign: bottom" ToolTip="点击查看在线用户信息" runat="server"></asp:Label>位用户在线</FONT></td>
								<td width="10%" valign="middle" align="center"><FONT face="宋体"><asp:Label ID="l_state" runat="server">状态：联机</asp:Label></FONT></td>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<script language="javascript" type="text/javascript">
	
		objLab = document.all.l_num;
		window.setInterval("DisplaySystemTime()",1000);
		window.setInterval("shanshuo()",16000);
			</script>
		</form>
	</body><script language="javascript" type="text/javascript">document.body.oncontextmenu=new Function("return false;");</script>
</html>
