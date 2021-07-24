<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datumseach.aspx.cs" Inherits="DatumSeach" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ȷ�����ϵĲ鿴</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		function clickRow(obj)
		{
			document.getElementById('hdnCode').value = obj;		
			document.getElementById('BtnSee').disabled = false;
		}
		function OpType(obj)
		{	
			var DatumCode   = document.getElementById('hdnCode').value;
			var url = "DatumAdd.aspx?DatumCode="+DatumCode+"&optype=SEE";		
			return window.showModalDialog(url,window,'dialogHeight:460px;dialogWidth:650px;center:1;help:0;status:0;');	
		}
		</script>
	</head>
	<body bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<TR>
					<TD class="td-title">�� �� �� ��</TD>
				</TR>
				<TR>
					<TD class="td-search" align="right">
						���⣺<asp:TextBox ID="TxtDatum" Width="100px" runat="server"></asp:TextBox>&nbsp; 
						������ڣ�<JWControl:DateBox ID="DBAdd" Width="75px" runat="server"></JWControl:DateBox>
						�������ڣ�<JWControl:DateBox ID="DBUpdate" Width="75px" runat="server"></JWControl:DateBox>
						<asp:Button ID="BtnSearch" Text="" CssClass="button-normal" Enabled="true" OnClick="BtnSeach_Click" runat="server" />&nbsp;
						<asp:Button ID="BtnSee" Enabled="false" CssClass="button-normal" Text="�� ��" runat="server" />
					</TD>
				</TR>
				<TR>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox"><asp:DataGrid ID="GrdDatum" CssClass="grid" Width="100%" AutoGenerateColumns="false" DataKeyField="KnowledgeCode" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="���"></asp:TemplateColumn><asp:HyperLinkColumn DataTextField="DatumName" HeaderText="����"></asp:HyperLinkColumn><asp:BoundColumn HeaderText="�� ��"></asp:BoundColumn><asp:BoundColumn DataField="AddDate" HeaderText="�������" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="UpdateDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="Author"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div><input id="hdnCode" type="hidden" name="hdnCode" style="WIDTH: 10px" size="1" runat="server" />

						<input id="hdnTypeId" type="hidden" name="hdnTypeId" style="WIDTH: 10px" size="1" runat="server" />

					</td>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
