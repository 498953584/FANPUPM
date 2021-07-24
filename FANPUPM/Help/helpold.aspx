<%@ Page Language="C#" AutoEventWireup="true" CodeFile="helpold.aspx.cs" Inherits="helpold" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>帮助页面</title></head>
<body bottommargin="5" leftmargin="5" rightmargin="5" topmargin="5" scroll=no>
    <form id="form1" runat="server">
    <TABLE id="Table2" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="1" >
		<TR>
			<td height="100%" vAlign="top" align="left" style="width: 20%" atomicselection="false">
				<DIV class="gridBox" align="left"><P align="left">
                    <asp:TreeView ID="TrvHelp" expandlevel="1" showtooltip="False" selectexpands="True" Height="100%" Width="300px" ImageSet="WindowsHelp" runat="server"><ParentNodeStyle Font-Bold="false" /><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView>
                    &nbsp;</P>
				</DIV>
			</td>
			<TD>
				<iframe name="ifrMain" id="ifrMain" width="100%" height="100%" scrolling="no" src="../EPC/UserControl1/webNullTS.aspx" runat="server">
				</iframe>
			</TD>
		</TR>
	</TABLE>
    </form>
</body>
</html>
