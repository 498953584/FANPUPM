<%@ Page Language="C#" AutoEventWireup="true" CodeFile="worklogadd.aspx.cs" Inherits="workLogAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>workLogAdd</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
		<SCRIPT language="javascript">
<!--
//函数名称：trimAll(s)
//功能：去掉空格
function trimAll(s) {
 var i;
  for (i = 0; i < s.length && s.charCodeAt(i) == 32; i ++);
  s = s.substring(i, s.length);
  for (i = s.length - 1; i >= 0 && s.charCodeAt(i) == 32; i--);
  s = s.substring(0, i + 1);
 return s;
}

//函数名称：getLength(moji)
//功能：得到字符串的长度，一个汉字占2个字符
function getLength(moji)
{	moji=trimAll(moji);
    var i,cnt = 0;
    for(i=0; i<moji.length; i++) if (escape(moji.charAt(i)).length >= 4 ) cnt+=2;
    else cnt++;
    return cnt;
}
function textCounter(str) 
	{
	   if (str =='999999999')
	   {
		 document.Form1.numLbl.value =getLength(document.Form1.logContent.value)/2;
	   }
	   else
	   {
		 document.Form1.numLbl.value =getLength(str)/2;
	   }
	}
-->
		</SCRIPT>
	</head>
	<body bgColor="#ffffff">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" width="50%" align="center" border="0">
				<tr>
					<td align="center" colSpan="4"><img src="img/diary.gif" width="245" height="27"></td>
				</tr>
				<tr bgColor="#eeeeee">
					<td align="right" width="20%">日期：</td>
					<td align="left" width="30%"><asp:Label ID="dateLbl" BackColor="White" runat="server"></asp:Label></td>
					<td align="right" width="20%">天气：</td>
					<td align="left" width="30%"><asp:TextBox ID="weather" Columns="10" BorderStyle="None" MaxLength="25" runat="server"></asp:TextBox></td>
				</tr>
				<tr bgColor="#eeeeee">
					<td align="center" colSpan="4"><asp:TextBox ID="logContent" style="BACKGROUND-IMAGE: url(img/Txt_bg.jpg); OVERFLOW: auto; LINE-HEIGHT: 1.25" Height="300px" Width="450px" BorderStyle="None" Wrap="true" Rows="15" TextMode="MultiLine" runat="server"></asp:TextBox></td>
				</tr>
				<tr bgColor="#eeeeee">
					<td align="center" colSpan="4">您已经输入<input type="text" name="numLbl" id="numLbl" value="0" size="6" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
							readOnly>字
					</td>
				</tr>
				<tr>
					<td align="center" colspan="4">
						<asp:Button ID="btnAdd" CssClass="button-normal" Text=" 提交 " OnClick="btnAdd_Click" runat="server" />
						<asp:Button ID="btnAddTemp" CssClass="button-normal" Text=" 暂存 " OnClick="btnAddTemp_Click" runat="server" />
						<input class="button-normal" type="reset" value=" 重写 " name="btnReset">
						<input class="button-normal" type="button" value=" 取消 " name="btnCancel" onclick="javascript:location='workLogList.aspx'">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
