<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codeclasslist.aspx.cs" Inherits="CodeClassList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>编码类型列表</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function doClickRow(TypeID)
			{				
				document.getElementById('BtnMod').disabled = false;				
				document.getElementById('HdnTypeID').value = TypeID;
			}
	
			function openContractEdit(op)
			{				
				var TypeID = 0;
				if (op==2)
				{
					TypeID = document.getElementById('HdnTypeID').value;
				}									
				var url= "CodeClassEdit.aspx?t=" + op + "&ti=" + TypeID;				
				return window.showModalDialog(url,window,"dialogHeight:410px;dialogWidth:600px;center:1;help:0;status:0;");
			}
			
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="96%" align="center" border="0">
				<tr>
					<td class="td-title">编码类别维护</td>
				</tr>
				<tr>
					<td class="td-toolsbar"><asp:Button ID="BtnAdd" Text="新  增" OnClick="BtnAdd_Click" runat="server" />&nbsp;
						<asp:Button ID="BtnMod" Text="编  辑" Enabled="false" OnClick="BtnMod_Click" runat="server" />&nbsp;&nbsp;&nbsp;
						<INPUT id="HdnTypeID" style="WIDTH: 1px" type="hidden" size="1"></td>
				</tr>
				<tr>
					<td>
						<div class="gridBox"><asp:DataGrid ID="DgdCodeType" CssClass="grid" Width="100%" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="TypeName" HeaderText="类型名称"></asp:BoundColumn><asp:BoundColumn DataField="signCode" HeaderText="特征码"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="TypeID" ReadOnly="true"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
