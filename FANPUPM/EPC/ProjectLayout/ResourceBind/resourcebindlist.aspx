<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourcebindlist.aspx.cs" Inherits="ResourceBindList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>�󶨲����б�</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
		function dorows(rc,style)
		{
			document.getElementById("HdnResourceCode").value = rc;
			document.getElementById("HdnResourceStyle").value = style;
			document.getElementById("BtnSave").disabled = false;
		}
		</script>
	</head>
	<BODY bottomMargin="0" leftMargin="2" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<tr>
					<td class="td-title">��Դ����Դ�б�</td>
				</tr>
				<tr>
					<td class="td-search">
						��Դ��ţ�
						<asp:TextBox ID="TxtResourceCode" runat="server"></asp:TextBox>
						��Դ���ƣ�
						<asp:TextBox ID="TxtResourceName" runat="server"></asp:TextBox>
						<asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<DIV style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="dgd_List" Width="100%" AutoGenerateColumns="false" CssClass="grid" HeaderStyle-HorizontalAlign="Center" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn><ItemTemplate>
											<INPUT id="Quote" type="radio" name="Quote">
										</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="ResourceCode" HeaderText="��Դ���"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="��Դ����"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="���"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="��λ"></asp:BoundColumn><asp:BoundColumn DataField="Price" HeaderText="����" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="ResourceStyle" HeaderText="�������"></asp:BoundColumn></Columns></asp:DataGrid></DIV>
					</td>
				</tr>
				<tr>
					<td class="td-submit"><asp:Button ID="BtnSave" Text="�� ��" Enabled="false" OnClick="BtnSave_Click" runat="server" />
						<input type="button" id="btnclose" name="btnclose" value="ȡ ��" onclick="javascript:window.close();">
						<input id="HdnResourceCode" type="hidden" name="HdnResourceCode" runat="server" />
<input id="HdnResourceStyle" type="hidden" name="HdnResourceStyle" runat="server" />
</td>
				</tr>
			</table>
		</form>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</BODY>
</HTML>
