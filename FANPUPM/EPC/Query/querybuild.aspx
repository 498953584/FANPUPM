<%@ Page Language="C#" AutoEventWireup="true" CodeFile="querybuild.aspx.cs" Inherits="QueryBuild" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="Head1" runat="server"><title>�Զ����ѯ</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		    function checkState1(obj) {
		        document.getElementById('btnAdd').disabled = true;
		        document.getElementById('btnAdd').className = "button_addu";
		    }
		    function checkState2(obj) {
		        document.getElementById('btnAdd').disabled = false;
		        document.getElementById('btnAdd').className = "button_add";
		    }
		    function doClickRow(i) {
		        document.getElementById('hdnIndex').value = i;
		        if (document.getElementById('rb2').checked) {
		            document.getElementById('btnDel').disabled = false;
		            document.getElementById('btnDel').className = "button_del";
		        }
		    }
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<tr>
					<td class="td-head" colSpan="3" height="28">�Զ����ѯ</td>
				</tr>
				<tr>
					<td colSpan="3" height="24"><asp:RadioButton ID="rb1" Text="������" GroupName="term" Checked="true" AutoPostBack="true" OnCheckedChanged="rb1_CheckedChanged" runat="server" />&nbsp;&nbsp;<asp:RadioButton ID="rb2" Text="������������" GroupName="term" AutoPostBack="true" OnCheckedChanged="rb2_CheckedChanged" runat="server" />
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="3">
						<asp:DataGrid ID="dgdQuery" AutoGenerateColumns="false" ShowHeader="false" CellPadding="0" Width="100%" runat="server"><Columns><asp:TemplateColumn><ItemTemplate>
										<asp:DropDownList ID="ddlJoin" Width="50px" runat="server"><asp:ListItem Value="1" Text="Or" /><asp:ListItem Value="2" Text="And" /></asp:DropDownList>
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn><ItemTemplate>
										<asp:DropDownList ID="ddlName" Width="130px" runat="server"></asp:DropDownList>
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn><ItemTemplate>
										<asp:DropDownList ID="ddlOperator" Width="50px" runat="server"><asp:ListItem Value="=" Text="=" /><asp:ListItem Value="<>" Text="<>" /><asp:ListItem Value="<" Text="<" /><asp:ListItem Value=">" Text=">" /><asp:ListItem Value="<=" Text="<=" /><asp:ListItem Value=">=" Text=">=" /><asp:ListItem Value="like" Text="like" /></asp:DropDownList>
									</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn><ItemTemplate>
										<asp:TextBox ID="TextBox1" Width="200px" Text='<%# Convert.ToString(Eval("ItemValue")) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid></td>
				</tr>
				<tr>
					<td height="24">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="hdnIndex" style="WIDTH: 10px" type="hidden" name="hdnIndex" runat="server" />
</td>
					<td align="center" width="50"><asp:Button ID="btnDel" Enabled="false" CssClass="button_delu" OnClick="btnDel_Click" runat="server" /></td>
					<td align="center" width="50"><asp:Button ID="btnAdd" CssClass="button_addu" Enabled="false" OnClick="btnAdd_Click" runat="server" /></td>
				</tr>
				<tr>
					<td colSpan="3" height="28" align="right"><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><asp:Button ID="btnOk" Text="ȷ ��" CssClass="button-normal" OnClick="btnOk_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" Text="ȡ ��" CssClass="button-normal" OnClick="btnCancel_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
