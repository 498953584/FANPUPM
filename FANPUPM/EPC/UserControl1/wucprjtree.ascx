<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucprjtree.ascx.cs" Inherits="WUCPrjTree" %>


<TABLE id="Table1" cellSpacing="0" cellPadding="0" Height="100%"  border="0">
    <tr>
		<td height="24"><asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList></td>
	</tr>
	<TR>
		<TD vAlign="top">
		
			<asp:TreeView ID="tvProject" ShowLines="true" expandlevel="1" showtooltip="False" selectexpands="True" Height="100%" Width="200px" runat="server"><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView></TD>
	</TR>
</TABLE>
<BR>
</FONT>
