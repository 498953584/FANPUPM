<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loantable.aspx.cs" Inherits="LoanTable" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>资金申请列表</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="td-title"><asp:Label ID="LabTitle" runat="server">借款一览表</asp:Label></td>
				</tr>
				<TR>
					<TD vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="DGrdList" Width="100%" CssClass="grid" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="v_xm" HeaderText="借款人"></asp:BoundColumn><asp:BoundColumn DataField="BorrowMoney" HeaderText="借款金额(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="ReturnMoney" HeaderText="已登记成本" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="Balance" HeaderText="余额" DataFormatString="{0:F2}"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
