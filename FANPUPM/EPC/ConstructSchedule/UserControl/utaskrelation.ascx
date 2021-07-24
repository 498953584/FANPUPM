<%@ Control Language="C#" AutoEventWireup="true" CodeFile="utaskrelation.ascx.cs" Inherits="UTaskRelation" %>

<TABLE class="table-none" id="Tablew" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="td-toolsbar" colspan="2"><input id="HdnSelectedIndex" style="WIDTH: 10px" type="hidden" name="HdnSelectedIndex" runat="server" />

			<asp:Button ID="BtnAdd" Text="新 增" CommandName="Add" OnClick="BtnAdd_Click" runat="server" />&nbsp;
			<asp:Button ID="BtnEdit" Text="编  辑" Enabled="false" OnClick="BtnEdit_Click" runat="server" />&nbsp;
			<asp:Button ID="BtnDel" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" />&nbsp;
		</TD>
	</TR>
	<TR>
		<TD colSpan="2">
			<DIV class="gridBox"><asp:DataGrid ID="DGrdMarket" AutoGenerateColumns="false" CssClass="grid" Width="100%" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="关系名称">
<ItemTemplate>
								<asp:Label ID="LblRelation" runat="server"></asp:Label>
							</ItemTemplate>

<EditItemTemplate>
								<asp:DropDownList ID="DDLRelation" runat="server"><asp:ListItem Value="1" Text="完成开始[FS]" /><asp:ListItem Value="0" Text="完成完成[FF]" /><asp:ListItem Value="2" Text="开始完成[SF]" /><asp:ListItem Value="3" Text="开始开始[SS]" /></asp:DropDownList>
								<!--0--FF（完成-完成）1--FS（完成-开始）2--SF（开始-完成）3--SS（开始-开始）-->
							</EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="编号">
<ItemTemplate>
								<asp:Label ID="LblBeginTaskCode" Text='<%# Convert.ToString(Eval( "BeginTaskCode")) %>' runat="server"></asp:Label>
							</ItemTemplate>

<EditItemTemplate>
								<asp:TextBox ID="TxtBeginTaskCode" BackColor="#FFFFC0" style="cursor: hand" onclick="ShowTaskList(this);" Width="100%" Text='<%# Convert.ToString(Eval( "BeginTaskCode")) %>' runat="server"></asp:TextBox>
							</EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="任务名称">
<ItemTemplate>
								<asp:Label ID="LblTaskName" runat="server"></asp:Label>
							</ItemTemplate>

<EditItemTemplate>
								<asp:TextBox ID="TxtTaskName" style="cursor: hand" onclick="ShowTaskList(this);" Width="100%" runat="server"></asp:TextBox>
							</EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="时间偏差">
<ItemTemplate>
								<asp:Label ID="LblWaitDay" Text='<%# Convert.ToString(Eval( "WaitDay")) %>' runat="server"></asp:Label>
							</ItemTemplate>

<EditItemTemplate>
								<asp:TextBox ID="TxtWaitDay" onKeyPress="funChkInt();" Width="100%" Text='<%# Convert.ToString(Eval( "WaitDay")) %>' runat="server"></asp:TextBox>
							</EditItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></DIV>
		</TD>
	</TR>
</TABLE>
