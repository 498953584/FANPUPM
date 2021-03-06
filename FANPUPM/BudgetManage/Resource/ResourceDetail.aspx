<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceDetail.aspx.cs" Inherits="BudgetManage_Resource_ResourceDetail" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源维护</title><link rel="stylesheet" type="text/css" href="../../Script/lightbox/jquery.lightbox-0.5.css" media="screen" />

	<style type="text/css">
		#gallery img
		{
			border: 1px solid #3e3e3e;
			border-width: 5px 5px 20px;
		}
		
		.gv_corp
		{
			color: Blue;
			cursor: pointer;
		}
	</style>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/lightbox/jquery.lightbox-0.5.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script src="../../Script/jw.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(window).load(function () {
			replaceEmptyTable('emptyTable', 'gvResource');
			var resTable = new JustWinTable('gvResource');
			setButton(resTable, 'btnDel', 'btnEdit', 'btnLook', 'hfldPurchaseChecked');

			// 添加查看供应商事件
			$('.gv_corp').click(function () {
				if ($(this).parent().attr('corpId') != '') {
					openContractEdit($(this).parent().attr('corpId'));
				}
			});

			if (document.getElementById('hdParentId').value == '') {
				document.getElementById('btnAdd').setAttribute('disabled', 'disabled');
				document.getElementById('btnExcelIn').setAttribute('disabled', 'disabled');
			}
			addEvent($('#btnEdit')[0], 'click', rowEdit);
			addEvent($('#btnAdd')[0], 'click', rowAdd);
			addEvent($('#btnExcelIn')[0], 'click', rowIn);
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
					maxHeight: 600
				});
			});
			//setWidthHeight();
		});
		function setWidthHeight() {
			$('#divBudget').width(($(this).width()) - ($('#td_Left').width()) - 5);
			$('#divBudget').height($(this).height() - 105);
		}
		function rowEdit() {
			var url = "/BudgetManage/Resource/ResourceEdit.aspx?id=" + document.getElementById("hfldPurchaseChecked").value +
				"&parentId=" + $("#hdParentId").val() + "&nodeId=" + $("#hdPtNodeId").val();
			toolbox_oncommand(url, '编辑资源');
		}
		function rowAdd() {
			var parentId = $("#hdParentId").val();
			if (!parentId) {
				top.ui.alert('请选择资源类型');
				return false;
			}
			var url = "/BudgetManage/Resource/ResourceEdit.aspx?parentId=" + parentId + "&nodeId=" + $("#hdPtNodeId").val();
			toolbox_oncommand(url, '新增资源');
		}
		//Excel导入
		function rowIn() {
			var url = "ResourceExcelIn.aspx?parentId=" + $("#hdParentId").val() + "&nodeId=" + $("hdPtNodeId").val();
			winopen(url);
		}

		//对文本框输入数字的限制
		function keyPress(ob) {
			if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
				ob.value = ob.t_value;
			else
				ob.t_value = ob.value;
			if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
				ob.o_value = ob.value;
		}
		function keyUp(ob) {
			if (!ob.value.match(/^[\+\-]?\d*?\.?\d*?$/))
				ob.value = ob.t_value;
			else
				ob.t_value = ob.value;
			if (ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))
				ob.o_value = ob.value;
		}
		function onBlur(ob) {
			if (!ob.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?|\.\d*?)?$/))
				ob.value = ob.o_value;
			else {
				if (ob.value.match(/^\.\d+$/)) ob.value = 0 + ob.value;
				if (ob.value.match(/^\.$/)) ob.value = 0; ob.o_value = ob.value
			};
		}

		// 查看供应商
		function openContractEdit(Id) {
			var url = "/EPC/Basic/ContactCorpEdit.aspx?t=2&ci=" + Id + "&Action=Query";
			return window.showModalDialog(url, window, "dialogHeight:410px;dialogWidth:700px;center:1;help:0;status:0;");
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							资源编号
						</td>
						<td>
							<asp:TextBox ID="txtResourceCode" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							资源名称
						</td>
						<td>
							<asp:TextBox ID="txtResourceName" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							规格
						</td>
						<td>
							<asp:TextBox ID="txtSpecification" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							技术参数
						</td>
						<td>
							<asp:TextBox ID="txtTechnicalParameter" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							品牌
						</td>
						<td>
							<asp:TextBox ID="txtBrand" Width="120px" runat="server"></asp:TextBox>
						</td>
						<td class="descTd">
							价格类型
						</td>
						<td style="text-align: right">
							<asp:DropDownList ID="dropPriceType" DataTextField="PriceTypeName" DataValueField="PriceTypeId" Width="100%" runat="server"></asp:DropDownList>
						</td>
						<td>
							大于等于
						</td>
						<td>
							<asp:TextBox ID="txtLowPrice" Width="120px" onkeypress="keyPress(this)" onkeyup="keyUp(this)" onblur="onBlur(this)" runat="server"></asp:TextBox>
						</td>
						<td>
							小于等于
						</td>
						<td>
							<asp:TextBox ID="txtHighPrice" Width="120px" onkeypress="keyPress(this)" onkeyup="keyUp(this)" onblur="onBlur(this)" runat="server"></asp:TextBox>
						</td>
						<td>
							<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td class="divFooter" style="text-align: left;">
				<input type="button" id="btnAdd" value="新增" />
				<input type="button" id="btnEdit" disabled="disabled" value="编辑" />
				<asp:Button ID="btnDel" Text="删  除" disabled="disabled" OnClientClick="return confirm('系统提示：\n\n删除资源会将与资源相关的数据全部删掉。\n您确定要删除吗？');" OnClick="btnDel_Click" runat="server" />
				<input type="button" id="btnLook" style="display: none;" disabled="disabled" value="查看" />
				<asp:Button ID="btnexecl" CssClass="button-normal" Text="导出Excel" Width="80px" OnClick="btnexecl_Click" runat="server" />
				<input type="button" id="btnExcelIn" value="Excel导入" style="width: 80px" />
				<a id="linkADownLoad" class="link" style="float: right; margin-top: 5px; margin-right: 5px;"
					href="../../Download/资源模板.xls">资源模板</a>
			</td>
		</tr>
		<tr>
			<td style="height: 100%; width: 100%; vertical-align: top;">
				<div id="divBudget" style="width: 100%; height: 100%">
					<asp:GridView ID="gvResource" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvPurchaseplan_RowDataBound" DataKeyNames="ResourceId,CorpID,Note" runat="server">
<EmptyDataTemplate>
							<table id="emptyTable">
								<tr class="header">
									<th scope="col" style="width: 20px;">
									</th>
									<th scope="col">
										序号
									</th>
									<th scope="col">
										资源编号
									</th>
									<th scope="col">
										资源名称
									</th>
									<th scope="col">
										规格
									</th>
									<th scope="col">
										品牌
									</th>
									<th scope="col">
										税率
									</th>
									<th scope="col">
										技术参数
									</th>
									<th scope="col">
										单位
									</th>
									<th scope="col">
										供应商
									</th>
									<th scope="col">
										系列
									</th>
									<th scope="col">
										型号
									</th>
									<th scope="col">
										备注
									</th>
									<th scope="col">
										图片
									</th>
									<th scope="col">
										成本价
									</th>
									<th scope="col">
										目标成本价
									</th>
									<th scope="col">
										采购价
									</th>
									<th scope="col">
										售价
									</th>
									<th scope="col">
										出厂价
									</th>
								</tr>
							</table>
						</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
									<asp:CheckBox ID="cbAllBox" runat="server" />
								</HeaderTemplate><ItemTemplate>
									<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("ResourceId"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
								</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="序号" DataField="Number" /><asp:BoundField HeaderText="资源编号" DataField="ResourceCode" /><asp:BoundField HeaderText="资源名称" DataField="ResourceName" /><asp:BoundField HeaderText="规格" DataField="Specification" /><asp:BoundField HeaderText="型号" DataField="ModelNumber" /><asp:BoundField HeaderText="品牌" DataField="Brand" /><asp:BoundField HeaderText="单位" DataField="Name" /><asp:BoundField HeaderText="供应商" DataField="CorpName" ItemStyle-CssClass="gv_corp" /><asp:BoundField HeaderText="系列" DataField="Series" /><asp:BoundField HeaderText="技术参数" DataField="TechnicalParameter" /><asp:BoundField HeaderText="税率" DataField="TaxRate" /><asp:TemplateField HeaderText="备注"><ItemTemplate>
									<%# StringUtility.GetStr(Eval("Note").ToString(), 10) %>
								</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="图片"><ItemTemplate>
									<%# ResourceImage.GetImages(System.Convert.ToString(Eval("ResourceId"))) %>
								</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" ShowPageIndexBox="Always" UrlPaging="false" PageIndexBoxType="TextBox" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" SubmitButtonText="确定" SubmitButtonStyle="width:40px; vertical-align:top;" TextAfterPageIndexBox="页" OnPageChanged="AspNetPager1_PageChanged" runat="server">
					</webdiyer:AspNetPager>
				</div>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hdParentId" runat="server" />
	<asp:HiddenField ID="hdPtNodeId" runat="server" />
	</form>
</body>
</html>
