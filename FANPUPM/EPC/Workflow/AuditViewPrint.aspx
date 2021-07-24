<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditViewPrint.aspx.cs" Inherits="ModuleSet_Workflow_AuditViewPrint" %>


<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>流程审核记录表</title>
	<style type="text/css">
		.stytable
		{
			border: solid 1px #000000;
			border-collapse: collapse;
			empty-cells: show;
		}
		.stytable td
		{
			border-top: 0px solid #000000;
			border-bottom: 1px solid #000000;
			border-right: 1px solid #000000;
			height: 22px;
		}
	</style>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script language="javascript" type="text/javascript">
		var hkey_root, hkey_path, hkey_key
		hkey_root = "HKEY_CURRENT_USER"
		hkey_path = "\\Software\\Microsoft\\Internet Explorer\\PageSetup\\"
		function PrintPage() {
			form1.btnPrint.style.visibility = "hidden";
			//webForm.btnReturn.style.visibility = "hidden";
			window.print(); // 打印
			form1.btnPrint.style.visibility = "visible";
			//webForm.btnReturn.style.visibility = "visible";
			pagesetup_null();
		}
		//设置网页打印的页眉页脚为空 
		function pagesetup_null() {
			try {
				var RegWsh = new ActiveXObject("WScript.Shell")
				hkey_key = "header"
				RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
				hkey_key = "footer"
				RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
			} catch (e) { }
		} 
   
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div style="margin-left: auto; margin-right: auto">
		<div style="height: 22px; width: 100%; text-align: right;" id="dvprint">
			<asp:Button ID="btnPrint" Text="打  印" Width="80px" runat="server" />
			&nbsp;&nbsp;&nbsp;&nbsp;
		</div>
		<table cellpadding="0" cellspacing="0" border="0" width="98%" id="Table1" style="margin: auto;" runat="server"><tr runat="server"><td align="center" runat="server">
					<h2>
						<font style="font-size: 16pt; color: red; font-family: 黑体;">流程审核记录</font></h2>
					<img src="w.bmp" />
				</td></tr><tr runat="server"><td runat="server">
					&nbsp;
				</td></tr><tr runat="server"><td runat="server">
					<div style="width: 650px; height: auto; margin: auto;">
						<b>&nbsp;审核编号：</b>
						<asp:Label ID="LbAuditCode" runat="server"></asp:Label></div>
				</td></tr><tr runat="server"><td runat="server">
					<div style="width: 770px; height: auto; margin: auto;">
						<table cellpadding="0" cellspacing="0" border="1" class="stytable" id="printTable" style="table-layout: fixed; width: 702px;" align="center" runat="server"><tr runat="server"><td style="width: 115px; padding-right: 10px" align="right" class="word" runat="server">
									<b>流程名称</b>
								</td><td style="width: 115px; height: auto; text-align: left" runat="server">
									<div id="divTemplateName" style="width: auto; height: auto;" runat="server">
									</div>
									
								</td><td style="width: 115px; padding-right: 10px" align="right" class="word" runat="server">
									<b>发起人</b>
								</td><td style="width: 117px" runat="server">
									<asp:Label ID="LbUserName" runat="server"></asp:Label>
								</td><td align="right" style="width: 115px; padding-right: 10px" class="word" runat="server">
									<b>发起时间</b>
								</td><td style="width: 117px;" runat="server">
									<asp:Label ID="LbStartTime" runat="server"></asp:Label>
								</td></tr><tr runat="server"><td style="height: auto; color: Blue; padding-right: 10px" align="right" runat="server">
									<b>审核内容</b>
								</td><td colspan="5" style="text-align: left" runat="server">
									<asp:Label ID="LbConter" runat="server"></asp:Label>
									<asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
								</td></tr></table>
					</div>
				</td></tr><tr runat="server"><td runat="server">
					<div style="width: 770px; height: auto; margin: auto; font-size: 12px; padding-top: 3px;">
						注：“流程有效性审核人”是为本表打印出来后，供相关负责人签字而预留的空白栏。</div>
				</td></tr></table>
	</div>
	</form>
</body>
</html>
