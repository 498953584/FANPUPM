<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consignee.aspx.cs" Inherits="Consignee" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server"><title>人员选择</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<SCRIPT language="JavaScript" type="text/JavaScript">
			function confirmHuman()
			{
				var humanList = FraHuman.Form1.LBoxSelected;
				var human = window.dialogArguments;
				
				human[0] = "";
				human[1] = "";
			    for(var i=0;i<humanList.length;i++)
			    {
				    //human[0][i] = humanList.options[i].value;
				    human[0] += humanList.options[i].value + ",";
				    human[1] += humanList.options[i].text + ",";
			    }
			    human[0] = human[0].substring(0,human[0].length - 1);
			    human[1] = human[1].substring(0,human[1].length - 1);
				window.returnvalue= human;
				window.close();
			}
		</SCRIPT>
	</head>
	<BODY leftmargin="0" topmargin="0" scroll="no" rightmargin="0" onunload="confirmHuman()">
		<form id="Form1" method="post" runat="server">
				<table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%">
					<tr>
						<td colspan="3">
							<table width="100%" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td style="height: 2px" colspan="3"><em></em>&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr valign="top">
						<td colspan="3">
							<table width="100%" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td class="td-head" style="width: 2px">&nbsp;</td>
									<td style="width: 963px">
										<table width="95%" border="0" align="center" cellpadding="0" cellspacing="1">
											<TR valign="top">
												<TD>
													<TABLE height="370" cellspacing="0" cellpadding="0" width="100%" border="0">
														<TR>
															<TD height="18">部门：<input id="HdnSysID" style="WIDTH: 1px; HEIGHT: 1px" type="hidden" name="HdnSysID" runat="server" />
</TD>
														</TR>
														<TR>
															<TD bgcolor="#ffffff"><IFRAME id="FraDept" name="FraDept" src="deptinfo.aspx" width="100%" scrolling="auto" height="370" border="1" runat="server"></IFRAME>
															</TD>
														</TR>
													</TABLE>
												</TD>
												<TD>
													<TABLE cellspacing="0" cellpadding="0" width="200" border="0">
														<TR>
															<TD><IFRAME id="FraHuman" name="FraHuman" src="Human.aspx" width="200" height="390" frameborder="0"></IFRAME>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</table>
									</td>
									<td style="width: 2px">&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="161" style="height: 21px" ></td>
						<td width="477" style="height: 21px">&nbsp;
						</td>
						<td width="164" align="right" style="height: 21px"></td>
					</tr>
				</table>
		</form>
		<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
    </BODY>
</HTML>
