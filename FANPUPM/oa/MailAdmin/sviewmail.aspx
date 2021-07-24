<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sviewmail.aspx.cs" Inherits="SViewMail" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SViewMail</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
		function download(filepath,OriginalName)
	    {
	        var url = "/EPC/uploadfile/down.aspx?fileName=" + escape(OriginalName) + "&filePath=" + escape(filepath) ;
            window.location.href = url;
	    }
		</script>
	</head>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<BR>
				<BR>
				<TABLE id="Table1" cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
					<TR align="center">
						<TD height="25">
							<TABLE cellpadding="0" cellspacing="0" border="0" width="100%">
								<TR>
									<TD>&nbsp;</TD>
									<TD width="100"><asp:Button ID="BtnDelN" Text="删除" Width="80px" CssClass="button-normal" OnClick="BtnDelN_Click" runat="server" /></TD>
									<TD width="100"><asp:Button ID="BtnDelY" Text="彻底删除" Width="80px" CssClass="button-normal" OnClick="BtnDelY_Click" runat="server" /></TD>
									<TD width="100">
										<asp:Button ID="BtnReply" Width="80px" Text="回复" CssClass="button-normal" OnClick="BtnReply_Click" runat="server" /></TD>
									<TD width="100">
										<asp:Button ID="BtnTrans" Width="80px" Text="转发" CssClass="button-normal" OnClick="BtnTrans_Click" runat="server" /></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD valign="top">
							<TABLE id="Table2" cellspacing="0" cellpadding="0" width="100%" border="1" style="table-layout:fixed">
								<TR>
									<TD align="center" width="80" height="20" bgcolor="#eeeeee">发信人：</TD>
									<TD><asp:Label ID="LabSender" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 22px" align="center" height="20" bgcolor="#eeeeee">收信人：</TD>
									<TD style="HEIGHT: 22px"><asp:Label ID="LabConsignee" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center" height="20" bgcolor="#eeeeee">主&nbsp;&nbsp;题：</TD>
									<TD><asp:Label ID="LabTitle" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center" height="20" bgcolor="#eeeeee">日&nbsp;&nbsp;期：</TD>
									<TD><asp:Label ID="LabDateTime" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD align="center" height="20" bgcolor="#eeeeee">附&nbsp;&nbsp;件：</TD>
									<TD>
										<DIV id="annexBlock" runat="server"></DIV>
									</TD>
								</TR>
								<TR>
									<TD colspan="2" valign="top" style="word-break:break-all">
										<DIV id="contentBlock" style="OVERFLOW:scroll" runat="server">&nbsp;</DIV>
									</TD>
								</TR>
								<TR>
									<TD colspan="2" align="center" height="20"><input type="hidden" id="HdnMailID" style="WIDTH: 1px; HEIGHT: 1px" name="HdnMailID" runat="server" />

										<asp:HyperLink ID="HLnkBack" runat="server">返回</asp:HyperLink></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
