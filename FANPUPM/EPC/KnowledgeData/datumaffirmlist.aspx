<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datumaffirmlist.aspx.cs" Inherits="DatumAffirm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>资料确认</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		function clickRow(obj)
		{
			//alert(obj2);
			document.getElementById('hdnCode').value = obj;		
			document.getElementById('BtnAffirm').disabled = false;
			document.getElementById('BtnSee').disabled = false;
		}
		function OpType(obj)
		{
			var TypeId    = document.getElementById('hdnTypeId').value;			
			var DatumCode   = document.getElementById('hdnCode').value;
			var	url = "DatumAffirm.aspx?DatumCode="+DatumCode;
			refresh = window.showModalDialog(url,window,'dialogHeight:250px;dialogWidth:550px;center:1;help:0;status:0;');	
			if (refresh==true)
			{  	   
				return true;
			}
			else
			{
				return false;
			}
		}
		 
		function lookDatum(obj)
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
					<TD class="td-title">资 料 列 表</TD>
				</TR>
				<TR>
					<TD class="td-toolsbar" align="right"><input id="hdnCode" type="hidden" name="hdnCode" runat="server" />

						<input id="hdnTypeId" type="hidden" name="hdnTypeId" runat="server" />

						<asp:Button ID="BtnAffirm" Enabled="false" CssClass="button-normal" Text="归 档" OnClick="BtnAffirm_Click" runat="server" />&nbsp;<asp:Button ID="BtnSee" Enabled="false" CssClass="button-normal" Text="查 看" runat="server" />
					</TD>
				</TR>
				<TR>
					<td vAlign="top" colSpan="2" height="100%">
						<div class="gridBox"><asp:DataGrid ID="GrdDatum" CssClass="grid" DataKeyField="KnowledgeCode" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanged="DataGrid1_PageIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号"></asp:TemplateColumn><asp:HyperLinkColumn DataTextField="DatumName" HeaderText="标题"></asp:HyperLinkColumn><asp:BoundColumn HeaderText="作 者"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="Author"></asp:BoundColumn><asp:BoundColumn DataField="AffirmState" ReadOnly="true" HeaderText="状态"></asp:BoundColumn><asp:BoundColumn DataField="AffirmDateTime" HeaderText="归档日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn HeaderText="归档人"></asp:BoundColumn><asp:BoundColumn DataField="AffirmInfo" HeaderText="归档内容"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="Affirmyhdm" HeaderText="Affirmyhdm"></asp:BoundColumn></Columns><PagerStyle CssClass="GD-Pager" Mode="NumericPages"></PagerStyle></asp:DataGrid></div>
					</td>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
