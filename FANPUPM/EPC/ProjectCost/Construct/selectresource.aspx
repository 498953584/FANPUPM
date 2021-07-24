<%@ Page Language="C#" AutoEventWireup="true" CodeFile="selectresource.aspx.cs" Inherits="SelectResource" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>人材机选择</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
		function clickRow(Id)
		{
			document.getElementById("HdnId").value = Id;
			document.getElementById('BtnDel').disabled = false;
		}
		function checkDecimal(sourObj)
		{
			if (sourObj.value=="")
			{
				sourObj.value = 0;
			}
			if (!(new RegExp(/^\d+(\.\d+)?$/).test(sourObj.value)))
			{
				alert('数据类型不正确！');
				sourObj.focus();
				return;
			}
		}
		function test(obj)
		{
		}			
		function pickResource()
		{
			var strGuid ="18A25555-0936-4517-BCAB-7DC1E12C1B5B";
			var url= "/EPC/Basic/Resource/pickresource.aspx?ses=" + strGuid + "&rs=4";
			return window.showModalDialog(url,window,"dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;");
		}
		
			function getMoney(obj1,obj2,obj3)
			{
				var a = parseFloat(document.getElementById(obj1).value);
				var b = parseFloat(document.getElementById(obj2).value);
				var c = parseFloat(document.getElementById(obj3).value);
				document.getElementById(obj3).innerText = a*b;
				
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" style="TABLE-LAYOUT: fixed" height="100%" cellSpacing="0" cellPadding="0"
				width="100%" border="1">
				<tr>
					<td class="td-toolsbar" colSpan="4" height="22">
						<asp:Button ID="btnSave" Text="保  存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />&nbsp;<INPUT id="btn_close" class="button-normal" onclick="javascript:window.returnValue=false;window.close();"
							type="button" value="关 闭"> <input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" OnServerChange="HdnProjectCode_ServerChange" runat="server" />

					</td>
				</tr>
				<tr style="<%=this.display %>">
					<td align="center" width="100%" colSpan="4" height="21" style="HEIGHT: 21px"><asp:Button ID="btn_Add" Text="选择资源" CssClass="button-normal" OnClick="btn_Add_Click" runat="server" />
						<asp:Button ID="BtnDel" Text="删  除" Enabled="false" CssClass="button-normal" OnClick="BtnDel_Click" runat="server" /><input id="HdnId" type="hidden" name="HdnId" runat="server" />
</td>
				</tr>
				<tr vAlign="top">
					<td width="100%" colSpan="4">
						<DIV class="gridBox" id="dvResourceBox"><asp:DataGrid ID="DGrdResource" Width="100%" AutoGenerateColumns="false" CssClass="grid" PageSize="100" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="ResourceCode" HeaderText="资源编号"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="资源名称"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="规格"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="单位"></asp:BoundColumn><asp:TemplateColumn HeaderText="数量">
<ItemTemplate>
											<asp:TextBox ID="TxtAmount" Width="100%" Text='<%# Convert.ToString(Eval("Quantity", "{0:0.00}")) %>' runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="单价(元)">
<ItemTemplate>
											<asp:TextBox ID="TxtPrice" Width="100%" Text='<%# Convert.ToString(Eval("Price", "{0:0.00}")) %>' runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn Visible="false" DataField="ResourceStyle" HeaderText="style"></asp:BoundColumn><asp:TemplateColumn HeaderText="小计">
<ItemTemplate>
											<asp:TextBox ID="TxtTotal" Enabled="false" Width="100%" Text="0" runat="server"></asp:TextBox>
										</ItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></DIV>
					</td>
				</tr>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
