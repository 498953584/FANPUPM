<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebManagerList.aspx.cs" Inherits="WebManagerList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>企业门户管理</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		function NewsInfo(t, NewsName, NewsCode) {
			//添加编辑	
			var url;
			var title;
			var NewsId = document.getElementById("HdnNewsId").value;
			var Newsbt = document.getElementById("HdnNewsbt").value;
			top.ui._WebManagerList = window;
			switch (t) {
				case "add":
					title = '新增';
					url = "/WEB/WebManager.aspx?na=" + escape(NewsName) + "&cd=" + NewsCode + "&nid=0&nbt=";
					break;
				case "update":
					title = '修改';
					url = "/WEB/WebManager.aspx?na=" + escape(NewsName) + "&cd=" + NewsCode + "&nid=" + NewsId + "&nbt=" + Newsbt + "";
					break;
				case "sel":
					title = '查看';
					url = "/WEB/WebSel.aspx?na=" + escape(NewsName) + "&cd=" + NewsCode + "&nid=" + NewsId + "&nbt=" + escape(Newsbt) + "";
					break;
			}

			toolbox_oncommand(url, title);
		}
		function NewsId(Newsbt, NewsId) {
			document.getElementById("HdnNewsId").value = NewsId;
			document.getElementById("HdnNewsbt").value = Newsbt;
			document.getElementById("btnEdit").disabled = false;
			document.getElementById("btnDel").disabled = false;
			document.getElementById("BtnSel").disabled = false;
		}
		$(function () {
			$("#tabDiv").find("div").css("display", "block");
		});
			
	</script>
	<style type="text/css">
	  div
	  {
	  	margin:0px;
	  	padding:0px;
	  	display:none;
	  }
	 </style>
</head>
<body ms_positioning="FlowLayout" class="body_clear" scroll="no">
	<form id="Form1" method="post" runat="server">
	<table class="table-none" id="Table1" height="100%" cellspacing="1" cellpadding="1"
		width="100%" border="1"  vertical="top" >
		<tr>
			<td class="td-search">
				<asp:Label ID="LbNewsName2" runat="server"></asp:Label>标题:
				<asp:TextBox ID="TxtSelBt" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
				发布日期:
				<asp:TextBox ID="DbSelDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
				&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" />
			</td>
		</tr>
		<tr id="trtool" style="height: 20px;" runat="server"><td class="td-toolsbar" align="right" colspan="3" style="height: 20px;" runat="server">
				<asp:Button ID="btnAdd" CssClass="button-normal" Text="新 增" OnClick="btnAdd_Click" runat="server" />&nbsp;
				<asp:Button ID="btnEdit" CssClass="button-normal" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />&nbsp;
				<asp:Button ID="btnDel" CssClass="button-normal" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;
				<asp:Button ID="BtnSel" CssClass="button-normal" Text="查 看" Enabled="false" runat="server" />&nbsp;
			</td></tr>
		<tr>
			<td width="100%" colspan="3" style="height: 400px">
				<div class="div-scroll" id="tabDiv" style="overflow: auto; width: 100%; height: 100%; display:block ">
					<asp:GridView ID="Dbg_item" CssClass="grid" AllowPaging="true" AutoGenerateColumns="false" Width="100%" PageSize="20" OnRowDataBound="Dbg_item_RowDataBound" OnPageIndexChanging="Dbg_item_PageIndexChanging" DataKeyNames="i_xw_id" runat="server"><RowStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row" VerticalAlign="Middle"></RowStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateField HeaderText="序号"></asp:TemplateField><asp:HyperLinkField DataTextField="v_xwbt" HeaderText="标题" ItemStyle-HorizontalAlign="Left" /><asp:BoundField DataField="dtm_fbsj" HeaderText="发布日期" /></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
					<input id="HdnNewsId" type="hidden" name="HdnNewsId" runat="server" />

					<input id="HdnNewsbt" type="hidden" name="HdnNewsbt" runat="server" />

				</div>
			</td>
		</tr>
	</table>
	<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</form>
</body>
</html>
