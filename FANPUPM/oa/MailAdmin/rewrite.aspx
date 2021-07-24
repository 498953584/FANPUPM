<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="rewrite.aspx.cs" Inherits="ReWrite" %>
<%@ Register TagPrefix="MyUserControl" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ReWrite</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<SCRIPT language="JavaScript" type="text/JavaScript">
			function showConsignee()
			{
				//document.Form1.HdnConsignee.value = "";
				document.Form1.TBoxConsignee.value = "";
				var url = "Consignee.aspx";
				var human = new Array();
				window.showModalDialog(url,human,"dialogHeight:422px;dialogWidth:659px;center:1;help:0;status:0;");
				for (var i=0;i<human.length;i++)
				{
					//document.Form1.HdnConsignee.value += human[0][i] + "!";
					document.Form1.TBoxConsignee.value += human[i] + ",";
				}
			}
				//抄送
			function showCopy()
			{
				document.Form1.txtCopy.value = "";
				document.getElementById('HdnCopy').value = "";
				var url = "Consignee2.aspx";
				var human = new Array();				
				window.showModalDialog(url,human,"dialogHeight:475px;dialogWidth:480px;center:1;help:0;status:0;");
				for (var i=0;i<human.length;i++)
				{
					//document.Form1.HdnConsignee.value += human[0][i] + "!";
					document.getElementById("txtCopy").value += human[i] + ",";
					document.getElementById('HdnCopy').value += human[i] + ",";
				}
			}
			//密送
			function showSecret()
			{
				document.Form1.txtSecret.value = "";
				document.getElementById('HdnSecret').value = "";
				var url = "Consignee3.aspx";
				var human = new Array();
				window.showModalDialog(url,human,"dialogHeight:475px;dialogWidth:480px;center:1;help:0;status:0;");
				for (var i=0;i<human.length;i++)
				{
					//document.Form1.HdnConsignee.value += human[0][i] + "!";
					document.getElementById("txtSecret").value += human[i] + ",";
					document.getElementById('HdnSecret').value += human[i] + ",";
				}
			}
			function showAnnex()
			{
				document.Form1.TBoxAnnex.value = "";
				var url = "broker.aspx?go=1";
				var annex = new Array();
				window.showModalDialog(url,annex,"dialogHeight:225px;dialogWidth:425px;center:1;help:0;status:0;");
				for (var i=0;i<annex.length;i++)
				{
					document.Form1.TBoxAnnex.value += annex[i] + ",";
				}
			}
		</SCRIPT>
	</head>
	<body class="body_clear" scroll="yes">
		<form id="Form1" method="post" runat="server">		
				<table width="100%"   cellpadding="0" cellspacing="0" border="1" id="table1" class="table-normal">
					<tr>
						<td  class="td-title" colspan="2">
							草稿箱</td>
					</tr>
				
					<tr>
						<td width="12%" height="25" align="right" class="td-label">收件人:&nbsp;&nbsp;</td>
						<td><asp:TextBox ID="TBoxConsignee" ReadOnly="true" Columns="60" runat="server"></asp:TextBox>&nbsp;
						<input class="button-normal" onclick="showConsignee()" type="button" value="选择收件人..." style="WIDTH: 100px"/>
						</td>
					</tr>
					<tr>
					    <td class="td-label">抄&nbsp;&nbsp;送:&nbsp;&nbsp;</td>
					    <td>
                            <asp:TextBox ID="txtCopy" Columns="60" ReadOnly="true" runat="server"></asp:TextBox>
                            &nbsp;<input class="button-normal" style="WIDTH: 100px" onclick="showCopy()" type="button"
								value="选择抄送人..." id="btnCopy"/>
                            <asp:HiddenField ID="HdnCopy" runat="server" />
                        </td>
					 </tr>
					 <tr>
					    <td class="td-label">密&nbsp;&nbsp;送:&nbsp;&nbsp;</td>
					    <td>
                            <asp:TextBox ID="txtSecret" Columns="60" ReadOnly="true" runat="server"></asp:TextBox>
                            &nbsp;<input class="button-normal" style="WIDTH: 100px" onclick="showSecret()" type="button"
								value="选择密送人..." id="btnSecret"/>
                            <asp:HiddenField ID="HdnSecret" runat="server" />
                        </td>
					 </tr>
					<tr>
						<td height="25" align="right" class="td-label">主&nbsp;&nbsp;题:&nbsp;&nbsp;</td>
						<td><asp:TextBox ID="TBoxTitle" Columns="60" runat="server"></asp:TextBox><input id="HdnDraft" style="WIDTH: 1px; HEIGHT: 1px" type="hidden" name="HdnAnnex" runat="server" />

						</td>
					</tr>
					<tr>
						<td height="25" align="right" class="td-label">附&nbsp;&nbsp;件:&nbsp;&nbsp;</td>
						<td><asp:TextBox ID="TBoxAnnex" Columns="60" ReadOnly="true" runat="server"></asp:TextBox>&nbsp;&nbsp;<INPUT class="button-normal" onclick="showAnnex()" type="button" value="添加/编辑" style="WIDTH: 100px"></TD>
					</tr>
					<tr>
						<td align="right" class="td-label">内&nbsp;&nbsp;容:&nbsp;&nbsp;</td>
						<td valign="top"><MyUserControl:FreeTextBox ID="TBoxContent" BackColor="#E0E0E0" GutterBackColor="#E0E0E0" ToolbarBackColor="#81A9E2" HelperFilesPath="/CommonWindow/FreeTextBox/" HelperFilesParameters="/CommonWindow/FreeTextBox/" ImageGalleryPath="UploadFiles/mail/img" Height="380px" ToolbarType="Office2003" runat="server" />
						</td>
					</tr>
					<tr>
						<td colspan="2" height="30" align="left">
							<table cellspacing="0" cellpadding="0" width="90%" border="0">
								<tr align="center">
									<TD width="140"><asp:Button ID="BtnSend" CssClass="button-normal" Text="发  送" OnClick="BtnSend_Click" runat="server" /></TD>
									<TD width="120"><asp:Button ID="BtnToDraft" CssClass="button-normal" Text="存草稿" OnClick="BtnToDraft_Click" runat="server" /></TD>
									<TD width="120"><asp:CheckBox ID="CBoxSend" Text="保存到发件箱" runat="server" /></TD>
									<TD width="120"><asp:CheckBox ID="CBoxSMS" Text="手机短信督办" runat="server" /></TD>
									<td><asp:CheckBox ID="CBRTX" Text="待办消息" Height="16px" Checked="true" runat="server" /></TD>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			
		</form>
	</body>
</html>
