<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCProjectList.ascx.cs" Inherits="UCProjectList" %>

<table cellSpacing="0" cellPadding="0" Height="100%" border="0">
	<tr>
		<td height="24"><asp:DropDownList ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" runat="server"></asp:DropDownList></td>
	</tr>
	<tr>
		<td valign="top">
		        <asp:TreeView ID="tvProject" Font-Size="12px" ShowLines="true" selectexpands="True" showtooltip="False" expandlevel="1" Height="100%" Width="200px" runat="server"><SelectedNodeStyle BorderWidth="3px" BorderColor="White" BorderStyle="Solid" BackColor="#0099CC" ForeColor="White" /><NodeStyle ForeColor="Black" Font-Names="Tahoma" /></asp:TreeView>
	    </td>
	</tr>
</table>
