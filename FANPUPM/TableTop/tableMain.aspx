<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tableMain.aspx.cs" Inherits="test" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>无标题页</title>
	<base target="_self" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="css/style1.css" rel="stylesheet" type="text/css" />

	<script language="javascript" src="../../../Web_Client/Tree.js" type="text/javascript"></script>
	
	<link href="/Script/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
<link href="/Script/jquery.treeview/jquery.treeview.css" rel="Stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.dialog.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../Script/ShowToolTip.js"></script>
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			$('table[modelId]').each(function (index) {
				var modelId = this.modelId;
				// 删除该记录
				$(this).find('tr[id]').click(function () {
					var pk = this.id;
					$.ajax({
						type: 'GET',
						async: true,
						url: '/TableTop/Handler/UpdateTopFlag.ashx?' + new Date().getTime() + '&modelId=' + modelId + '&pk=' + pk
					});
				});
			});
		});

		function click_ts() {
			$('#divFram').dialog({ width: 600, height: 450, modal: false });
		}
		function grid_del(mkid) {
			//alert('隐藏此栏目？'+mkid);
			document.getElementById("hdnmkid").value = mkid;
			window.document.all("btndel").click();
		}
		function selectVehicleName() {
			document.getElementById("divFramPrj").title = "桌面布局设置";
			var w = Math.floor((screen.availWidth)); //document.body.clientWidth
			document.getElementById("ifFramPrj").src = "/TableTop/UserSet.aspx?deskwidth=" + w;
			$('#divFramPrj').dialog({ width: 300, height: 285, modal: true, position: ['right', 'top']
			});
		}
		$(function () {
			$("#Img_set").mouseover(function () {
				$(this).attr("src", "image/bg_set2.png")
			});
			$("#Img_set").mouseout(function () {
				$(this).attr("src", "image/bg_set.png")
			});
			$("#Img_add").mouseover(function () {
				$(this).attr("src", "image/bg_add2.png")
			});
			$("#Img_add").mouseout(function () {
				$(this).attr("src", "image/bg_add.png")
			});
			$("#Img_set").click(function () {
				selectVehicleName();
			});
			//关闭栏目（删除）按钮,鼠标经过的样式
			$("img[id^='Img_del']").mouseover(function () {
				$(this).attr("src", "image/bg_03del1.gif")
			});
			$("img[id^='Img_del']").mouseout(function () {
				$(this).attr("src", "image/bg_03.gif")
			});
		});
		function showDel(id) {
			$("#" + id).attr("src", "image/bg_03del1.gif");
			$("#" + id).css("cursor", "pointer");
		}
		function showDelT(id) {
			$("#" + id).attr("src", "image/bg_03.gif")
		}
		function openTab(url, modelName) {
			//(根目录下的路径)
			parent.desktop.flowclass = window; //不可少
			toolbox_oncommand(url, modelName); //引用js
			//window.location.reload();
			//            $.ajax({
			//                type: 'GET',
			//                async: true,
			//                url: '/TableTop/Handler/UpdateTopFlag.ashx?' + new Date().getTime() + '&modelId=' + modelId + '&pk=' + pk
			//            });
		}
		//屏蔽右键
		//        document.oncontextmenu=function(){return   false}

		//外部页面打开
		function OpenWeb(url) {
			return window.open(url, '外部链接', 'toolbar=yes,menubar=yes, scrollbars=yes,location=yes, status=yes, resizable=yes');
		}
		function query(region) {
			parent.desktop.regionDetail = window;
			var url = "/ProgressManagement/Analysis/AnalysisDetail.aspx?region=" + encodeURI(region);
			toolbox_oncommand(url, "进度明细");
		}
	</script>
	<style type="text/css">
		.fadingTooltip
		{
			border-right: darkgray 1px outset;
			border-top: darkgray 1px outset;
			font-size: 10pt;
			border-left: darkgray 1px outset;
			width: auto;
			color: black;
			border-bottom: darkgray 1px outset;
			height: 15px;
			background-color: lemonchiffon;
			margin: 3px 3px 3px 3px;
			padding: 3px 3px 3px 3px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divFramPrj" title="" style="display: none">
		<iframe id="ifFramPrj" frameborder="0" width="99%" height="99%" src=""></iframe>
	</div>
	<div align="right" cellspacing="0">
		
		<img id="Img_set" alt="设置桌面布局" title="设置桌面布局" src="/TableTop/image/bg_set.png" class="header-cursor" />
		<img id="Img_add" alt="桌面栏目管理" title="桌面栏目管理" src="/TableTop/image/bg_add.png" onclick="javascript:parent.desktop.flowclass = window;toolbox_oncommand('/TableTop/ModelList.aspx','桌面栏目设置');  "
			class="header-cursor" />
	</div>
	<div style="display: none">
		<input id="hdnmkid" type="hidden" runat="server" />

		<asp:Button ID="btndel" title="删除" Width="0px" Height="0px" OnClick="btndel_Click" runat="server" />
	</div>
	<div align="center">
		<table id="tblData" runat="server"></table>
	</div>
	<div id="divProject" align="center" style="margin: auto; display: none;
		text-align: center;" runat="server">
		<table border="0" cellspacing="0" cellpadding="0" align="center" id="tabProject" runat="server"><tr onmousemove="showDel('Img_del780501')" onmouseout="showDelT('Img_del780501')" runat="server"><td runat="server">
					<img src="image/bg_01.gif" style="float: left; width: 12px; height: 25px" />
				</td><td background="image/bg_02.gif" class="header-b" style="text-align: left;" runat="server">
					进度柱状图
				</td><td runat="server">
					<img src="image/bg_03.gif" title="隐藏此栏目" id="Img_del780501" onclick="grid_del(780501)"
						style="float: right; width: 12px; height: 25px" />
				</td></tr><tr runat="server"><td background="image/bg_11.gif" width="12px" runat="server">
				</td><td valign="top" id="tdProject" runat="server">
					<div class="pagebody">
						<table>
							<tr>
								<td align="center">
									<asp:Chart ID="Chart1" Height="200px" BorderColor="#1A3B69" Palette="BrightPastel" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2px" runat="server"><Series><asp:Series IsValueShownAsLabel="false" ChartArea="ChartArea1" Name="Default" CustomProperties="LabelStyle=Bottom"></asp:Series></Series><ChartAreas /></asp:Chart><!--BorderColor="FromArgb(180, 26, 59, 105)"-->
								</td>
							</tr>
						</table>
					</div>
				</td><td background="image/bg_13.gif" wdith="12px" runat="server">
					&nbsp;
				</td></tr><tr valign="top" runat="server"><td background="image/bg_11.gif" width="12px" runat="server">
					&nbsp;
				</td><td width="100%" runat="server">
					<span class="readmore"><a href='javascript:openTab("/ProgressManagement/Analysis/Analysis.aspx,进度柱状图")'>
						更多>></a>
				</td><td background="image/bg_13.gif" width="11px" runat="server">
					&nbsp
				</td></tr><tr runat="server"><td height="10px" valign="top" runat="server">
					<img src="image/bg_21.gif" style="float: left; width: 12px; height: 10px;" />
				</td><td height="10px" valign="top" background="image/bg_22.gif" runat="server">
				</td><td valign="top" runat="server">
					<img src="image/bg_23.gif" style="float: left; width: 12px; height: 10px;" />
				</td></tr></table>
	</div>
	<div id="divFram" title="添加" style="display: none">
		<iframe id="ifAdd" frameborder="0" width="100%" height="100%" src="/TableTop/UserModelList.aspx?Method=returnModelId">
		</iframe>
	</div>
	<div class="fadingTooltip" id="fadingTooltip" style="z-index: 999; visibility: hidden;
		position: absolute">
	</div>
	</form>
</body>
</html>
