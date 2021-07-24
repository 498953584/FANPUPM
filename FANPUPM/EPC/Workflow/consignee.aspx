<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consignee.aspx.cs" Inherits="Consignee" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>人员选择</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
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
					//human[0][i] = humanList.options[i].value;
					human[i] = humanList.options[i].text;
				}
				window.returnvalue= human;
				window.close();
			}
		</SCRIPT>
	</head>
	<BODY leftmargin="0" topmargin="0" scroll="no" rightmargin="0" onunload="confirmHuman()">
		<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing=1 cellPadding=0 width="95%" align=center border=0>
				<TBODY>
				<TR vAlign=top>
				<TD>
				    <TABLE height=100% cellSpacing=0 cellPadding=0 width="100%" border=0>
				    <TBODY>
				        <TR>
				            <TD height=18>部门：<input style="WIDTH: 1px; HEIGHT: 1px" id="HdnSysID" type="hidden" name="HdnSysID" runat="server" />

                            </TD>
                        </TR>
                        <TR>
                            <TD bgColor=#ffffff>
                            <IFRAME id="FraDept" name="FraDept" src="deptinfo.aspx" width="100%" scrolling="no" height="370" runat="server"></IFRAME>
                            </TD>
                        </TR>
                    </TBODY>
                    </TABLE>
                </TD>
                <TD style="WIDTH: 202px">
                    <TABLE cellSpacing=0 cellPadding=0 width=200 border=0>
                    <TBODY>
                        <TR>
                            <TD style="WIDTH: 201px">
                                <IFRAME id="FraHuman" name="FraHuman" src="Human.aspx" frameBorder="0" width=200 height=370></IFRAME>
                            </TD>
                        </TR>
                    </TBODY>
                    </TABLE>
                </TD>
                </TR>
                </TBODY>
                </TABLE>
		</form>
	</BODY>
</HTML>
