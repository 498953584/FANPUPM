<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tasktreetable.ascx.cs" Inherits="TaskTreeTable" %>

<table class="table-layout:fixed;" id="Table2" height="100%" cellSpacing="0" cellPadding="0"
	width="100%" border="0">
	<tr>
		<td>
			<DIV id="DVTaskBox" class="gridBox">
				<asp:Table ID="tblTask" GridLines="Both" CellSpacing="0" CellPadding="0" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" Width="100%" CssClass="grid" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Text="任务名称" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="120px" Text="任务编码" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="类型" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="工程量" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Text="单位" Wrap="false" runat="server"></asp:TableCell><asp:TableCell Width="80px" Text="综合单价(元)" runat="server"></asp:TableCell><asp:TableCell Text="合计(元)" runat="server"></asp:TableCell></asp:TableRow></asp:Table></DIV>
		</td>
	</tr>
</table>
<input id="HdnTaskCodeList" style="WIDTH: 10px" type="hidden" name="HdnTaskCodeList" runat="server" />
