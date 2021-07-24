<%@ Page Language="C#" AutoEventWireup="true" CodeFile="corpaddresslist.aspx.cs" Inherits="CorpAddressList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN"> 
<html>
  <head runat="server"><title>内部通讯录</title></head>  
	<body scroll="no" class="body_frame" style="overflow:hidden"  >
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" height="100%">
					<TR>
						<TD width="180" height="100%">
                        <iframe width="100%" name="DeptList" id="DeptList" frameborder="1" height="100%" runat="server"></iframe>
						</TD>
						<TD height="100%">
                        <iframe width="100%" name="LinkmanList" id="LinkmanList" frameborder="1" height="100%" runat="server"></iframe>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</html>
