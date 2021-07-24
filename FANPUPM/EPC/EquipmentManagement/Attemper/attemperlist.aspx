<%@ Page Language="C#" AutoEventWireup="true" CodeFile="attemperlist.aspx.cs" Inherits="AttemperList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>机械器具调度</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
			function clickRow(obj)
			{
				document.getElementById('hdnAttemperId').value = obj;
				document.getElementById('btnSee').disabled = false;
				document.getElementById('btnDel').disabled = false;
				document.getElementById('btnEdit').disabled = false;
			}
		
			function OpType(obj)
			{
				var AttemperID        =document.getElementById('hdnAttemperId').value;
				var equipementcode  =document.getElementById('hdnequipmentcode').value;
				var url;
				var refresh = false;
				var type = obj
				switch(type)
				{
				case "SEE":
					url = "AttemperAdd.aspx?equipementcode="+equipementcode+"&AttemperID="+AttemperID+"&type=SEE";
					break;
				case "EDIT":
					url = "AttemperAdd.aspx?equipementcode="+equipementcode+"&AttemperID="+AttemperID+"&type=EDIT";
					break;
				case "ADD":
					url = "AttemperAdd.aspx?equipementcode="+equipementcode+"&type=ADD";
					break;
				} 
				return window.showModalDialog(url,window,'dialogHeight:310px;dialogWidth:600px;center:1;help:0;status:0;');					
			}
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="1">
				<tr>
					<td class="td-title">机械器具调度列表</td>
				</tr>
				<tr>
					<td class="td-toolsbar">&nbsp;
						<asp:Button ID="btnAdd" Text="新  增" CssClass="button-normal" OnClick="btnAdd_Click" runat="server" />&nbsp;<asp:Button ID="btnEdit" Text="编  辑" CssClass="button-normal" Enabled="false" OnClick="btnEdit_Click" runat="server" />
						<asp:Button ID="btnSee" Text="查  看" CssClass="button-normal" Enabled="false" runat="server" />&nbsp;<asp:Button ID="btnDel" Text="删  除" CssClass="button-normal" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top"><asp:DataGrid ID="GrdAttemper" CssClass="grid" Width="100%" DataKeyField="NoteSequenceID" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:BoundColumn DataField="AttemperBillCode" HeaderText="调度单号"></asp:BoundColumn><asp:BoundColumn DataField="Suite" HeaderText="随员"></asp:BoundColumn><asp:BoundColumn DataField="IntendingTime" HeaderText="台班"></asp:BoundColumn><asp:BoundColumn DataField="UnitPrice" HeaderText="台班单价(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="BeginDate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn HeaderText="相关项目"></asp:BoundColumn></Columns></asp:DataGrid></td>
				</tr>
				<tr>
					<td height="20"><asp:Label ID="labState" runat="server"></asp:Label></td>
				</tr>
			</table>
			<input id="hdnequipmentcode" type="hidden" name="hdnequipmentcode" runat="server" />

			<input id="hdnAttemperId" type="hidden" value="0" name="hdnAttemperId" runat="server" />

			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
