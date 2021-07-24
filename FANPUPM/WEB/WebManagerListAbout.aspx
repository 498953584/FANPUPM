<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebManagerListAbout.aspx.cs" Inherits="WebManagerListAbout" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>企业门户管理</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<base target="_self">
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		    <script language="javascript" type="text/javascript">

		function NewsInfo(t,NewsName,NewsCode)
		{
			//添加编辑	
			var URL ;
			var NewsId = document.getElementById("HdnNewsId").value;
			var Newsbt = document.getElementById("HdnNewsbt").value;				
			switch(t)
			{
				case "add":
					URL = "WebManager.aspx?na="+NewsName+"&cd="+NewsCode+"&nid=0&nbt=";
					break;
				case "update":
					URL = "WebManager.aspx?na="+NewsName+"&cd="+NewsCode+"&nid="+NewsId+"&nbt="+Newsbt+"";
					break;
				case "sel":
					URL = "WebSel.aspx?na="+NewsName+"&cd="+NewsCode+"&nid="+NewsId+"&nbt="+Newsbt+"";
					break;
			}					
			return window.showModalDialog(URL,window,'dialogHeight:440px;dialogWidth:630px;center:1;help:0;status:0;');										
			//window.open(URL,'news','height=440px,Width=600px');
		}
		function NewsId(Newsbt,NewsId)
		{	
	   		
			document.getElementById("HdnNewsId").value = NewsId;
			document.getElementById("HdnNewsbt").value = Newsbt;
			document.getElementById("Btn_update").disabled = false;
			document.getElementById("Btn_del").disabled = false;
			document.getElementById("BtnSel").disabled = false;
		}
	
		</script>
	</head>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%"
				border="1">
				<tr>
					<td class="td-title" colSpan="3"><asp:Label ID="LbNewsName" runat="server"></asp:Label>管理
					</td>
				</tr>
				<tr>
					<td class="td-search">
						<asp:Label ID="LbNewsName2" runat="server"></asp:Label>标题:
						<asp:TextBox ID="TxtSelBt" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp; 
						发布日期:
						<JWControl:DateBox ID="DbSelDate" runat="server"></JWControl:DateBox>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button ID="BtnSearch" runat="server" />
					</td>
				</tr>
				<tr style="height: 20px">
					<td class="td-toolsbar" align="right" colSpan="3" style="height: 20px"><asp:Button ID="Btn_add" CssClass="button-normal" Text="新 增" runat="server" />&nbsp;
						<asp:Button ID="Btn_update" CssClass="button-normal" Text="修 改" Enabled="false" runat="server" />&nbsp;
						<asp:Button ID="Btn_del" CssClass="button-normal" Text="删 除" Enabled="false" runat="server" />&nbsp;
						<asp:Button ID="BtnSel" CssClass="button-normal" Text="查 看" Enabled="false" runat="server" />&nbsp;
					</td>
				</tr>
				<tr>
					<td width="100%" colSpan="3" style="height:400px">
						<div class="div-scroll" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
						<asp:GridView ID="Dbg_item" CssClass="grid" AllowPaging="true" AutoGenerateColumns="false" Width="100%" OnRowDataBound="Dbg_item_RowDataBound" OnPageIndexChanging="Dbg_item_PageIndexChanging" DataKeyNames="i_xw_id" runat="server"><RowStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row" VerticalAlign="Middle"></RowStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号"></asp:TemplateField><asp:BoundField DataField="v_xwbt" HeaderText="标题" /><asp:BoundField DataField="dtm_fbsj" HeaderText="发布日期" /></Columns></asp:GridView><input id="HdnNewsId" type="hidden" name="HdnNewsId" runat="server" />

							<input id="HdnNewsbt" type="hidden" name="HdnNewsbt" runat="server" />

						</div>
					</td>
				</tr>
				
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
