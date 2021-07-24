<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkAdd.aspx.cs" Inherits="LinkAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>友情连接</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
		
		function ISImg(obj)
		{
			var chked = obj.checked;
			if(chked)
			{
				//eval("document.Form1.tr1.style ='DISPLAY:'");
				window.document.getElementById("tr1").style.display ="";
			}
			else
			{
			//	eval("document.Form1.tr1.style ='DISPLAY:none'");
				window.document.getElementById("tr1").style.display ="none";
			}
		}
	
		function AddPic(editor) 
		{
			editor.focus();	
			var folder = 'WebLink';
			var galleryscript = 'ftb.imagegallery.aspx?rif='+folder+'&cif='+folder;
			
			imgArr = showModalDialog(galleryscript,window,'dialogWidth:760px; dialogHeight:500px;help:0;status:0;resizeable:1;');

			if (imgArr != null) {
				imagestring =imgArr['filename'];
				editor.value=imagestring;
				//sel.pasteHTML(imagestring);
			} else {
				//alert("You did not select an image");
			}
			
		}
		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-normal" id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="1"
				align="center">
				<TR>
					<TD class="td-head" align="center" colSpan="2">友情连接</TD>
				</TR>
				<TR>
					<TD class="TD-lable">网站名称</TD>
					<TD><asp:TextBox ID="txtName" Columns="60" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="网站名称不能为空！" ControlToValidate="txtName" runat="server"></asp:RequiredFieldValidator><asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary></TD>
				</TR>
				<TR>
					<TD class="TD-lable">连接地址</TD>
					<TD><asp:TextBox ID="txtUrl" Columns="60" Text="http://" runat="server"></asp:TextBox>是否图片连接
						<input id="cb" onclick="ISImg(this);" type="checkbox" disabled="true" runat="server" />
</TD>
				</TR>
				<tr id="tr1" style="DISPLAY: none" runat="server"><td class="TD-lable" runat="server">LOGO地址</td><td runat="server"><asp:TextBox ID="txtLogo" Columns="60" runat="server"></asp:TextBox><FONT face="宋体" color="red">必须为jpg格式</FONT><INPUT class="button-normal" onclick="AddPic(txtLogo)" type="button" value="添加图片"></td></tr>
				<TR>
					<TD class="td-submit" colSpan="2"><asp:Button ID="btnSave" Text=" 保 存 " CssClass="button-normal" runat="server" />&nbsp;
						<INPUT class="button-normal" type="button" value=" 取 消 " onclick="window.close();">&nbsp;<input id="hdnType" type="hidden" runat="server" />

						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
