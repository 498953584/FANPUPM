<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckConstructReport.aspx.cs" Inherits="BudgetManage_Construct_ConstructReport" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>施工报量</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../../Script/lightbox/jquery.lightbox-0.5.css" media="screen" />

	<style type="text/css">
		#gallery img
		{
			border: 1px solid #3e3e3e;
			border-width: 5px 5px 20px;
		}
	</style>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../Script/Budget/BudgetPait.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/lightbox/jquery.lightbox-0.5.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			var gvConstruct = new JustWinTable('gvConstruct');
			setButton2(gvConstruct, 'btnDel', 'btnEdit', 'btnTemImport', 'hfldPurchaseChecked');
			setWidthAndHeight();
			$('.gallery').each(function () {
				$(this).find('a').lightBox({
					overlayBgColor: '#555',
					imageLoading: '../../Script/lightbox/images/lightbox-ico-loading.gif',
					imageBtnPrev: '../../Script/lightbox/images/lightbox-btn-prev.png',
					imageBtnNext: '../../Script/lightbox/images/lightbox-btn-next.png',
					imageBtnClose: '../../Script/lightbox/images/lightbox-btn-close.jpg',
					txtImage: '图片',
					txtOf: '共',
					maxWidth: 600,
					maxHeight: 450
				});
			});
			showTooltip('tooltip', 25);
			clickTree('tvBudget', 'hfldPrjId');
			//            if (arr = document.cookie.match(/scrollTop=([^;]+)(;|$)/))
			//                document.getElementById('div_project').scrollTop = parseInt(arr[1]);
		});
		//        //页面刷新前保存滚动条位置信息到cookie
		//        window.onbeforeunload = function () {
		//            var scrollPos;
		//            scrollPos = document.getElementById('div_project').scrollTop;
		//            document.cookie = "scrollTop=" + scrollPos;
		//        }
		//设置div高度和宽度
		function setWidthAndHeight() {
			$('#divBudget').height($(this).height() - $('#divTop').height() - 2);
			$('#divBudget').width($('#divContent').width() - $('#td_Left').width() - 8);
			$('#div_project').height($(this).height() - $('#ddlYear').height() - 4);
		}
		function setButton2(jwTable, btnDel, btnUpdate, btnQuery, hdChkId) {

			if (!jwTable.table) return;
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}
		};
		function setTableState(checkedChk) {
			var Edit = false;
			for (var i = 0; i < checkedChk.length; i++) {
				var tr = checkedChk[i].parentNode.parentNode.parentNode;
				var state = tr.state;
				if (state == "0" || state == "3") {
					Edit = true;
				} else {
					Edit = false;
				}
				if (Edit == true) {
					document.getElementById('btnDel').removeAttribute('disabled');
					document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
					document.getElementById('btnReported').removeAttribute('disabled');
				} else {
					document.getElementById('btnDel').setAttribute('disabled', 'disabled');
					document.getElementById('btnEdit').setAttribute('disabled', 'disabled');
					document.getElementById('btnReported').setAttribute('disabled', 'disabled');
					return;
				}
			}
		}

		function add() {
			parent.desktop.ConstructTask = window;
			var url = '/BudgetManage/Construct/ConstructTask.aspx?type=add&prjId=' + document.getElementById("hfldPrjId").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "选择任务");
		}
		function edit() {
			parent.desktop.ConstructTask = window;
			var url = '/BudgetManage/Construct/ConstructTask.aspx?type=edit&prjId=' + document.getElementById("hfldPrjId").value + '&conId=' + document.getElementById("hfldPurchaseChecked").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "选择任务");
		}
		function Query() {
			var url = '/BudgetManage/Construct/QueryConstructTask.aspx?type=edit&prjId=' + document.getElementById("hfldPrjId").value + '&conId=' + document.getElementById("hfldPurchaseChecked").value + '&year=' + document.getElementById("hfldYear").value;
			toolbox_oncommand(url, "查看施工报量");
		}

		//添加行进行显示资源信息
		var prevId;
		function showInfo(id, state) {
			var tab_Resource = null;
			$.ajax({
				type: 'GET',
				async: false,
				url: '/BudgetManage/Handler/ReturnConsRep.ashx?' + new Date().getTime() + '&consId=' + id,
				success: function (data) {
					tab_Resource = data;
				}
			});
			var isDisplay = $('#' + id + '1').get(0);
			if (isDisplay == undefined) {
				$('#' + id).after('<tr id="' + id + '1"><td align="center" colspan="12" style="border:solid 1px #CADEED;">' + tab_Resource + '</td></tr>');
				if (prevId != undefined && prevId != id) {
					$('#' + prevId + '1').remove();
				}
				prevId = id;
			}
			else {
				$('#' + id + '1').remove();
				isDisplay = undefined;
			}
		}    
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divContent" class="divContent2" style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%; overflow: hidden;">
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							
							<td style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								
								<div id="div1" class="divContent2" style="width: 100%; height: 99%; overflow: hidden;">
									<table style="width: 100%; height: 88%;">
										<tr>
											<td style="width: 100%; height: 100%; vertical-align: top">
												<div id="divBudget" class="pagediv">
													<asp:GridView ID="gvConstruct" Width="100%" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvConstruct_RowDataBound" DataKeyNames="id,state" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="50px">
<HeaderTemplate>
																	序号
																</HeaderTemplate>

<ItemTemplate>
																	<%# Container.DataItemIndex + 1 %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	上报时间
																</HeaderTemplate>

<ItemTemplate>
																	<%# System.Convert.ToDateTime(Eval("InputDate")).ToString("yyyy-MM-dd") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px">
<HeaderTemplate>
																	记录人
																</HeaderTemplate>

<ItemTemplate>
																	<%# Eval("InputUser") %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
																	安全工作纪录
																</HeaderTemplate>

<ItemTemplate>
																	<asp:HyperLink ID="hlinkShowName" Style="text-decoration: none; color: Black;" CssClass="tooltip" ToolTip='<%# System.Convert.ToString(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"><%# StringUtility.GetStr(StringUtility.StripTagsCharArray(Eval("WorkCard").ToString()), 25) %></asp:HyperLink>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
																	图片
																</HeaderTemplate>

<ItemTemplate>
																	<%# ConsReportImage.GetImages(System.Convert.ToString(Eval("Id"))) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField>
<HeaderTemplate>
																	流程状态
																</HeaderTemplate>

<ItemTemplate>
																	<%# Common2.GetState(Eval("FlowState").ToString()) %>
																</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												</div>
											</td>
										</tr>
									</table>
								</div>
								<asp:HiddenField ID="hfldPrjId" runat="server" />
								<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
								<asp:HiddenField ID="hfldYear" runat="server" />
								
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
