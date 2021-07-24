<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progmanage.aspx.cs" Inherits="ProgManage" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>奖罚类别</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
 <!-- 清除页面缓存--><base target="_self">
        <script src="../../../../Script/jquery.js" type="text/javascript"></script>
		<script language="javascript">
		    window.onload = function() {
		        //document.getElementById('Button_save').onclick = btnSaveClick;
		    }
		    function btnSaveClick() {
		        var tf = true;
		        if (!document.getElementById('TextBox_name').value) {
		            alert("系统提示：\n\n奖罚类别名称不能为空");
		            tf = false;
		        } 
		        return tf;
		    }
		 
        </script>
	</head>
	<body scroll="no">
		<form id="Form1" method="post" onsubmit="return btnSaveClick();" runat="server">
		<div class="divContent2">
		    <table width="100%">
		        <tr>
					<td class="divHeader" align="center" colSpan="4">项目奖罚类别</td>
				</tr>
		    </table>
			<table class="tableContent2" cellSpacing="0" cellPadding="5px" width="100%" border="0">				
				<tr>
					<td class="word" style="white-space:nowrap">奖罚类别名称</td>
					<td colspan="3" class="txt mustInput"><asp:TextBox ID="TextBox_name" ToolTip="长度不能超过50个字" MaxLength="50" CssClass="text-input" Width="100%" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="word" style="white-space:nowrap">备注</td>
					<td colspan="3" class="txt"><asp:TextBox ID="TextBox_remark" CssClass="text-input" Width="100%" TextMode="MultiLine" Height="56px" MaxLength="300" runat="server"></asp:TextBox>
					</td>
				</tr>
			</table>
			 <div class="divFooter2">
			   <table class="tableFooter2">
				<tr>
					<td>
						<asp:Button ID="Button_save" Text="保 存" OnClick="Button_save_Click" runat="server" /> 
						<INPUT type="button" value="取消" onclick='top.ui.closeTab();'></td>
				</tr>
			</table>
			</div>
			<JWControl:JavaScriptControl ID="JavaScriptControl1" runat="server"></JWControl:JavaScriptControl>
		</div>
        <asp:HiddenField ID="HiddenField1" runat="server" />
		</form>
	</body>
</HTML>
