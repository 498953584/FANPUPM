<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consignee.aspx.cs" Inherits="SalaryManage_SetSalary_consignee" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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
				for(var i=0;i<humanList.length;i++)
				{
					
					human[i] = humanList.options[i].text;
				}
				window.returnvalue = human;
				
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
									<td width="60" style="height: 30px"><em><img src="images/tl.gif" width="60" height="30"></em></td>
									<td background="images/tbg.gif" align="center" vAlign="bottom" style="height: 30px">&nbsp;</td>
									<td width="61" style="height: 30px"><img src="images/tr.gif" width="61" height="30"></td>
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
											<TR valign="top">
												<TD>
													<TABLE height="288" cellspacing="0" cellpadding="0" width="100%" border="0">
														<TR>
															<TD height="18">部门：<input id="HdnSysID" style="WIDTH: 1px; HEIGHT: 1px" type="hidden" name="HdnSysID" runat="server" />
</TD>
														</TR>
														<TR>
															<TD bgcolor="#ffffff"><IFRAME id="FraDept" name="FraDept" src="deptinfo.aspx?ht=1" width="100%" scrolling="auto" height="370" runat="server"></IFRAME>
															</TD>
														</TR>
													</TABLE>
												</TD>
												<TD>
													<TABLE cellspacing="0" cellpadding="0" width="200" border="0">
														<TR>
															<TD><IFRAME id="FraHuman" name="FraHuman" src="Human.aspx" width="200" height="392" frameborder="0"></IFRAME>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</table>
									</td>
									<td width="28" background="images/rbg.gif">&nbsp;
									</td>
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
		</form>
		
	</BODY>
</html>
