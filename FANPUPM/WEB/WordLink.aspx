<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WordLink.aspx.cs" Inherits="WordLink" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>添加连接</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript">
		
		function Winopen()
		{
		 var ret = false;
		 var type = window.document.getElementById("htnType").value;
		 var url= "LinkAdd.aspx?type="+type
		 ret = window.showModalDialog(url,window,'dialogHeight:200px;dialogWidth:700px;center:1;help:0;status:0;');	
		 if (ret==true)
			{  	   
				return true;
			}
			else
			{
				return false;
			}
		}
		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1"
				align="center">
				<tr>
					<td class="td-head">
						<asp:Label ID="Label1" runat="server">Label</asp:Label></td>
				</tr>
				<TR>
					<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;网站名称：
						<asp:TextBox ID="txtName" runat="server"></asp:TextBox>&nbsp;连接地址：
						<asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>&nbsp;
						<asp:Button ID="btn_Search" CssClass="button-normal" runat="server" />&nbsp;
						<asp:Button ID="btnAdd" Text=" 新 增 " CssClass="button-normal" runat="server" /></TD>
				</TR>
				<TR height="100%">
					<TD valign="top">
						<asp:DataGrid ID="DataGrid1" AutoGenerateColumns="false" Width="100%" DataKeyField="LinkId" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
										<%# Container.ItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="LinkName" HeaderText="网站名称"></asp:BoundColumn><asp:BoundColumn DataField="LinkUrl" HeaderText="连接地址"></asp:BoundColumn><asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" HeaderText="编 辑" CancelText="取消" EditText="编辑"></asp:EditCommandColumn><asp:ButtonColumn Text="删 除" HeaderText="删 除" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid></TD>
				</TR>
				<TR>
					<TD class="td-page" align="center">
						<JWControl:PaginationControl ID="PaginationControl1" runat="server"></JWControl:PaginationControl>&nbsp;
						<input id="htnType" type="hidden" value="1" runat="server" />

						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
