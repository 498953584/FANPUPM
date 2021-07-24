<%@ Page Language="C#" AutoEventWireup="true" CodeFile="annex.aspx.cs" Inherits="Annex" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>邮件附件</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
</head>
	<BODY leftmargin="0" topmargin="0" scroll="no" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%">
					<tr>
						<td colspan="3">
							<table width="100%" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td width="60"><em><img src="images/tl.gif" width="60" height="30"></em></td>
									<td background="images/tbg.gif" align="center" vAlign="bottom">&nbsp;</td>
									<td width="61"><img src="images/tr.gif" width="61" height="30"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr valign="top">
						<td colspan="3">
							<table width="100%" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td width="28" background="images/lbg.gif">&nbsp;</td>
									<td>
										<table width="95%" border="0" align="center" cellpadding="0" cellspacing="1">
											<TR>
												<TD valign="top">
													<asp:ListBox ID="LBoxAnnex" Rows="8" Width="360px" runat="server"></asp:ListBox>
												</TD>
											</TR>
											<TR>
												<TD height="30" align="center"><input id="fileAnnex" type="file" size="20" name="fileAnnex" runat="server" />
&nbsp;<asp:Button ID="BtnAddAnnex" CssClass="button-normal" Text="上 传" Width="50px" OnClick="BtnAddAnnex_Click" runat="server" />&nbsp;<asp:Button ID="BtnDel" Text="删\u3000除" CssClass="button-normal" Width="50px" OnClick="BtnDel_Click" runat="server" />&nbsp;<INPUT type="button" id="BtnOK" value="确\u3000定" onclick="window.close()" class="button-normal"
														style="WIDTH: 50px"><DIV id="warnBlock" runat="server"></DIV>
												</TD>
											</TR>
										</table>
									</td>
									<td width="28" background="images/rbg.gif">&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="161" background="images/dbg.gif"><img src="images/ld.gif" width="32" height="35"></td>
						<td width="477" background="images/dbg.gif">&nbsp;
						</td>
						<td width="164" align="right" background="images/dbg.gif"><img src="images/rd.gif" width="32" height="35"></td>
					</tr>
				</table>
			</FONT>
		</form>
	</BODY>
</HTML>
