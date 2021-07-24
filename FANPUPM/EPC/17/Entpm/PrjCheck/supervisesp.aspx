<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supervisesp.aspx.cs" Inherits="SuperviseSp" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>项目监察审核</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
 <!-- 清除页面缓存--><base target="_self">
		 <script language="javascript">
		 window.onload = function() {
		 document.getElementById('Button_save').onclick= btnSaveClick;
		 }
		 function btnSaveClick()
		  {
            if (!document.getElementById('TextBox_spr').value) 
            {
                alert("系统提示：\n\n审核人不能为空");
                return false;
            }
          }
		   function checklen(e,maxlength)
	    {
			 if(e.value.length > maxlength)
			 {
				alert('输入长度不能大于'+maxlength);
				e.focus();
				 return false;
			 }
	    }
		 </script>
	</head>
	<body scroll="no">
		<form id="Form1" method="post" runat="server">
		<div class="divContent2">
		   <table width="100%">
		       <tr>
					<td class="td-head" align="center" colSpan="5">项目监察审核</td>
				</tr>
		   </table>
			<table class="tableContent2" cellSpacing="0" cellPadding="5px" width="100%" border="0">
				
				<tr>
					<td class="word" style=" white-space:nowrap;">
                        审核人</td>
					<td colspan="3" class="txt"><asp:TextBox ID="TextBox_spr" MaxLength="20" runat="server"></asp:TextBox></td>
					
				</tr>
				<tr>
				<td class="word" style=" white-space:nowrap;">
                    是否同意</td>
				<td colspan="3" class="txt"><asp:RadioButton ID="RadioButton_ok" Text="通过" GroupName="gr1" Checked="true" BorderStyle="None" runat="server" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton_no" Text="不通过" GroupName="gr1" BorderStyle="None" runat="server" /></td>
				</tr>
				<tr>
				<td class="word" style=" white-space:nowrap;">审核日期</td>
				<td colspan="3" class="txt"><JWControl:DateBox ID="DateBox_sprq" runat="server"></JWControl:DateBox></td>
				</tr>
				<tr>
					<td class="word" style=" white-space:nowrap;">审核意见</td>
					<td colspan="3" class="txt"><asp:TextBox ID="TextBox_spyj" TextMode="MultiLine" Height="72px" onkeyup="checklen(this,300);" runat="server"></asp:TextBox></td>
				</tr>
				
			</table>
		 <div class="divFooter2">
			 <table class="tableFooter2">			
			    <tr>
					<td>
						<asp:Button ID="Button_save" Text="保 存" OnClick="Button_save_Click" runat="server" /><FONT face="宋体">&nbsp;
						</FONT><input type="button" onclick='top.ui.closeTab();' value='取 消'>
                       
                    </td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
	   </div>
	 </form>
	</body>
</HTML>
