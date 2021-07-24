<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjCompleted.aspx.cs" Inherits="PrjManager_Completed_PrjCompleted" EnableEventValidation="false" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html>
<html>
<head runat="server"><title></title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/tdToTxt.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable('emptyTable', 'gvComplete');
			var table = new JustWinTable('gvComplete');
			tdToTxt("lbltotxt");
			document.getElementById('div_cost').style.overflowY = 'auto';
			document.getElementById('div_cost').style.height = $(this).height() - 55 + "px";
			loadAnnex();
		});

		// 取消按钮事件
		function CancelClick() {
			winclose('PrjCompleted.aspx', 'PrjCompletedList.aspx', false);
		}

		// 设置目录
		function setDirectory(prjCompleteId, type) {
			var prjId = $('#hfldProjectId').val();
			var url = '/PrjManager/Completed/SetDirectory.aspx?prjId=' + prjId + '&prjComId=' + prjCompleteId + '&type=' + type;

			top.ui.callback = function() {
				window.location.href = window.location.href;
			}
			top.ui.openWin({ title: '设置目录', url: url, width: 600, height: 465 });

		}

		//DIv弹出IFrame
		//页面中必须包含jQuery
		function selectnEvent(divId, width, height) {
			width = width || 600;
			height = height || 485;
			var title = $('#' + divId).attr('title');
			$('#' + divId).dialog({ width: width, height: height, modal: true, title: title });
		}
		//查看其目录资料
		function lookFileInfo(prjId, prjCompletedId) {
			parent.desktop.lookAdjunct = window;
			var url = '/FileInfoManager/FileInfoLook.aspx?prjId=' + prjId + '&prjComId=' + prjCompletedId;
			toolbox_oncommand(url, "资料下载查看");
		}

		//加载目录资料
		function loadAnnex() {
			var tbData = $('#gvComplete').get(0);
			var adjunctCount = 10;
			var prjId = $('#hfldProjectId').val();
			var adjunctCount = 0;
			for (var i = 1; i < tbData.rows.length; i++) {
				var row = tbData.rows[i];
				var prjCompleteId = row.id;
				$.ajax({
					type: "GET",
					url: "/PrjManager/Handler/GetFilesCount.ashx?" + new Date().getTime() + "&prjId=" + prjId + "&prjComId=" + prjCompleteId,
					async: false,
					success: function (data) {
						adjunctCount = data;
					}
				});
				var isAnnex = row.isAnnex;
				if ((isAnnex != 'False' && $('#hfldNoSetAnnexId').val().indexOf(prjCompleteId) == -1) || $('#hfldSetAnnexId').val().indexOf(prjCompleteId) != -1)
					row.cells[row.cells.length - 1].innerHTML = '<a class="link" style="margin-right:8px;" id="lbtnSetDirectory" onclick="setDirectory(\'' + prjCompleteId + '\',\'edit\')"><img src="/images1/icon_att0b3dfa.gif" title="设置目录" /></a><a onclick="lookFileInfo(\'' + prjId + '\',\'' + prjCompleteId + '\')" class="link" title="查看附件">共' + adjunctCount + '个</a>';
			}
		}
	</script>
	<style type="text/css">
		.gvdata1
		{
			width: 100%; /*table-layout: fixed;*/
			border-collapse: separate;
			border-spacing: 0px 0px;
			border-width: 0px;
			border-bottom: solid 1px #CADEED;
			border-top: solid 1px #CADEED;
			border-left: solid 1px #CBDEED;
			border-right: solid 1px #CBDEED;
		}
		table.gvdata1 th
		{
			overflow: hidden;
			font-weight: normal; /*text-align: center;*/
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
		}
		table.gvdata1 td
		{
			/*overflow: hidden;*/
			vertical-align: top;
			padding-left: 6px;
			padding-right: 6px;
			font-weight: normal;
			border-color: #CADEED;
		}
		table.gvdata1 tr
		{
			height: 22px;
		}
		.header
		{
			background-color: #EEF2F5;
		}
		.rowa
		{
			background-color: #fbfbfb;
		}
		.rowb
		{
			background-color: #ffffff;
		}
		.footer
		{
		}
		.lbltotxt
		{
			width: 200px;
			word-break: break-all;
		}
	</style>
</head>
<body>
	<form id="form1" style="overflow: hidden" runat="server">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td style="text-align: center; width: 100%; padding-top: 5px; padding-bottom: 5px;">
				<asp:Label ID="lblPrjName" CssClass="divHeader" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top; width: 100%;">
				<div id="div_cost">
					<asp:GridView ID="gvComplete" AutoGenerateColumns="false" Width="100%" CssClass="gvdata1" OnRowDataBound="gvComplete_RowDataBound" DataKeyNames="Id,AnnexAddress" runat="server">
<EmptyDataTemplate>
							<table id="emptyTable">
								<tr class="header">
									<th scope="col">
										序号
									</th>
									<th scope="col">
										准备项目
									</th>
									<th scope="col">
										准备情况
									</th>
									<th scope="col">
										未完成事项
									</th>
									<th scope="col">
										整改及完善措施
									</th>
									<th scope="col">
										附件
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="20px" HeaderStyle-Wrap="false">
<ItemTemplate>
									<asp:Label ID="IdLabel" Text='<%# System.Convert.ToString(Container.DataItemIndex + 1, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
								</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Name" HeaderText="准备项目" ItemStyle-Width="120px" ItemStyle-Wrap="false" /><asp:TemplateField HeaderText="准备情况" ItemStyle-CssClass="lbltotxt"><ItemTemplate>
									<asp:Label ID="lblPrepareStatus" Text='<%# System.Convert.ToString(StringUtility.ReplaceTxt(Eval("PrepareStatus").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
									<asp:TextBox ID="txtPrepareStatus" TextMode="MultiLine" Rows="3" Height="70px" Style="display: none;" Width="200px" Text='<%# System.Convert.ToString(Eval("PrepareStatus"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="未完成情况" ItemStyle-CssClass="lbltotxt"><ItemTemplate>
									<asp:Label ID="lblUncompletedTrans" Text='<%# System.Convert.ToString(StringUtility.ReplaceTxt(Eval("UncompletedTrans").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
									<asp:TextBox ID="txtUncompletedTrans" TextMode="MultiLine" Rows="3" Height="70px" Style="display: none" Width="200px" Text='<%# System.Convert.ToString(Eval("UncompletedTrans"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="整改及完善措施" ItemStyle-CssClass="lbltotxt"><ItemTemplate>
									<asp:Label ID="lblRectification" Text='<%# System.Convert.ToString(StringUtility.ReplaceTxt(Eval("Rectification").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
									<asp:TextBox ID="txtRectification" TextMode="MultiLine" Rows="3" Height="70px" Style="display: none" Width="200px" Text='<%# System.Convert.ToString(Eval("Rectification"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" HeaderStyle-Width="70px"><ItemTemplate>
									<a class="link" id="lbtnSetDirectory" onclick="setDirectory('<%# Eval("id") %>','add')">
										<img src="/images1/icon_att0b3dfa.gif" alt="" title="设置目录" /></a>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
				</div>
			</td>
		</tr>
		<tr>
			<td>
				<div id="Div1" class="divFooter2" runat="server">
					<table class="tableFooter2">
						<tr>
							<td>
								<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
								<input type="button" id="btnCancel" value="取消" onclick="CancelClick()" />
							</td>
						</tr>
					</table>
				</div>
			</td>
		</tr>
	</table>
	<div id="divDirectory" title="设置目录" style="display: none">
		<iframe id="frmDirectory" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<asp:HiddenField ID="hfldProjectId" runat="server" />
	
	<asp:HiddenField ID="hfldSetAnnexId" runat="server" />
	<asp:HiddenField ID="hfldNoSetAnnexId" runat="server" />
	</form>
</body>
</html>
