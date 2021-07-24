<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showprivilage.aspx.cs" Inherits="showPrivilage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>showPrivilage</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
</head>
	<BODY style="OVERFLOW: auto">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE cellSpacing="1" cellPadding="1" width="100%" border="1">
					<TR>
						<TD><asp:Label ID="LabDept" runat="server"></asp:Label><input id="hdnDeptCode" style="WIDTH: 1px" type="hidden" name="hdnDeptCode" runat="server" />
</TD>
						<TD align="center"><asp:Button ID="BtnNew" Text="增加职务" CssClass="button-normal" OnClick="BtnNew_Click" runat="server" /></TD>
					</TR>
					<TR>
						<TD colSpan="2"><asp:Panel ID="PanNew" Visible="false" runat="server">
								<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="1">
									<TR>
										<TD style="WIDTH: 78px">职务名称:</TD>
										<TD>
											<asp:TextBox ID="TBoxDutyName" Columns="16" Width="100px" runat="server"></asp:TextBox></TD>
										<TD align="center">
											<asp:Button ID="BtnAdd" Text="新  增" CssClass="button-normal" OnClick="BtnAdd_Click" runat="server" /></TD>
										<TD align="center">
											<asp:Button ID="BtnCancel" Text="取 消" CssClass="button-normal" OnClick="BtnCancel_Click" runat="server" /></TD>
									</TR>
								</TABLE>
							</asp:Panel></TD>
					</TR>
					<TR>
						<TD colSpan="2"><asp:DataGrid ID="DGrdDuty" AutoGenerateColumns="false" Width="100%" DataKeyField="i_DutyID" runat="server"><ItemStyle Height="20px"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="职务名称">
<ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("v_DutyName")) %>' runat="server"></asp:Label>
										</ItemTemplate>

<EditItemTemplate>
											<asp:TextBox Width="100%" Text='<%# Convert.ToString(Eval("v_DutyName")) %>' runat="server"></asp:TextBox>
										</EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn HeaderText="职务权限"></asp:BoundColumn><asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn><asp:ButtonColumn Text="删除" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid>
							<DIV id="WarnBlock" runat="server"></DIV>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
