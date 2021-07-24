<%@ Page Language="C#" AutoEventWireup="true" CodeFile="readmail.aspx.cs" Inherits="ReadMail" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ReadMail</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript">
		function download(filepath,OriginalName)
	    {
	        var url = "/EPC/uploadfile/down.aspx?fileName=" + escape(OriginalName) + "&filePath=" + escape(filepath) ;
            window.location.href = url;
	    }
		</script>
		
	</head>
	<BODY class="body_frame">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<BR>
				<TABLE id="Table1" class="table-normal" cellspacing="0" cellpadding="0" width="95%" align="center" border="0" style="table-layout: fixed; border-top-style: none; border-right-style: none; border-left-style: none; border-collapse: collapse; border-bottom-style: none">
					<TR align="center">
						<TD style="height: 25px">
							<TABLE id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
								<TR align="center" class="td-toolsbar">
									<TD></TD>
									<TD width="100"><asp:Button ID="BtnDelN" Text="删除" Width="80px" CssClass="button-normal" OnClick="BtnDelN_Click" runat="server" /></TD>
									<TD width="100"><asp:Button ID="BtnDelY" Text="彻底删除" Width="80px" CssClass="button-normal" OnClick="BtnDelY_Click" runat="server" /></TD>
									<TD width="100"><asp:Button ID="BtnEdit" Text="编辑" Width="80px" CssClass="button-normal" OnClick="BtnEdit_Click" runat="server" /></TD>
									<TD width="100"></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD valign="top">
							<TABLE id="Table1" cellspacing="0" cellpadding="0" style="table-layout:fixed; border-top-style: none; border-right-style: none; border-left-style: none; border-collapse: collapse; border-bottom-style: none;" width="100%"
								border="1">
								<TR>
									<TD align="center" width="80" class="td-label">发信人：</TD>
									<TD><asp:Label ID="LabSender" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 22px" align="center" class="td-label">收信人：</TD>
									<TD style="HEIGHT: 22px"><asp:Label ID="LabConsignee" runat="server"></asp:Label></TD>
								</TR>
								<tr>
								    <td align="center" class="td-label">抄送人：</td>
								    <td><asp:Label ID="LbCSR" runat="server"></asp:Label></td>
								</tr>
								<TR>
									<TD align="center" class="td-label" style="height: 20px">主&nbsp;&nbsp;题：</TD>
									<TD style="height: 20px"><asp:Label ID="LabTitle" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center" class="td-label">日&nbsp;&nbsp;期：</TD>
									<TD><asp:Label ID="LabDateTime" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center" class="td-label" style="height: 20px">附&nbsp;&nbsp;件：</TD>
									<TD style="height: 20px" valign=top>
										<DIV id="annexBlock" runat="server"></DIV>
									</TD>
								</TR>
								<TR height=260>
									<TD valign="top" colspan="2" style="word-break:break-all">
										<DIV id="contentBlock" style="OVERFLOW: scroll" runat="server">&nbsp;</DIV>
									</TD>
								</TR>
								<TR>
									<TD align="center" colspan="2" height=25><A href="JavaScript:history.go(-1)">返 回</A><input id="HdnMailID" style="WIDTH: 1px; HEIGHT: 1px" type="hidden" name="HdnMailID" runat="server" />
</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
