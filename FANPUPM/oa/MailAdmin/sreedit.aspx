<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sreedit.aspx.cs" Inherits="SReEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SReEdit</title><meta Name="CODE_LANGUAGE" Content="C#" />
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
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<BR>
				<BR>
				<TABLE id="Table1" cellspacing="0" cellpadding="0" width="90%" align="center" border="1">
					<TR>
						<TD width="12%" height="25" align="right">收件人:&nbsp;&nbsp;</TD>
						<TD><asp:TextBox ID="TBoxConsignee" ReadOnly="true" Columns="60" runat="server"></asp:TextBox>&nbsp;<input id="BtnConsignee" class="button-normal" onclick="showConsignee()" type="button" value="选择收件人..." style="WIDTH: 100px" name="BtnConsignee" runat="server" />
</TD>
					</TR>
					<TR>
						<TD height="25" align="right">主&nbsp;&nbsp;题:&nbsp;&nbsp;</TD>
						<TD><asp:TextBox ID="TBoxTitle" Columns="60" runat="server"></asp:TextBox><input id="HdnDraft" style="WIDTH: 1px; HEIGHT: 1px" type="hidden" name="HdnAnnex" runat="server" />

						</TD>
					</TR>
					<TR>
						<TD height="25" align="right">附&nbsp;&nbsp;件:&nbsp;&nbsp;</TD>
						<TD><asp:TextBox ID="TBoxAnnex" Columns="60" ReadOnly="true" runat="server"></asp:TextBox>&nbsp;<INPUT class="button-normal" onclick="showAnnex()" type="button" value="添加/编辑" style="WIDTH: 100px"></TD>
					</TR>
					<TR>
						<TD align="right">内&nbsp;&nbsp;容:&nbsp;&nbsp;</TD>
						<TD valign="top"><asp:TextBox ID="TBoxContent" Columns="78" TextMode="MultiLine" Rows="15" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD colspan="2" height="30">
							<TABLE cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
								<TR align="center">
									<TD><asp:Button ID="BtnSend" CssClass="button-normal" Text="发  送" OnClick="BtnSend_Click" runat="server" /></TD>
									<TD><asp:Button ID="BtnToDraft" CssClass="button-normal" Text="存草稿" runat="server" /></TD>
									<TD><asp:CheckBox ID="CBoxSend" Text="保存到发件箱" runat="server" /></TD>
									<TD><asp:CheckBox ID="CBoxSMS" Text="手机短信督办" Visible="false" runat="server" /></TD>
									<TD><asp:Button ID="BtnBack" Text="返  回" CssClass="button-normal" OnClick="BtnBack_Click" runat="server" /></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR style="display:none">
						<TD height="25" align="right">类&nbsp;&nbsp;型:&nbsp;&nbsp;</TD>
						<TD>
							<asp:RadioButtonList ID="RBtnMailType" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="200px" CellPadding="0" CellSpacing="0" runat="server"><asp:ListItem Value="0" Text="公开" /><asp:ListItem Value="1" Text="个人" /></asp:RadioButtonList></TD>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
