<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSendNote.aspx.cs" Inherits="StockManage_sendGoods_AddSendNote" %>
<%@ Import Namespace="cn.justwin.BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>采购计划单</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script src="../../SysFrame/jscript/JsControlMenuTool.js" type="text/javascript"></script>
	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript" src="../script/ValidateInput.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvNeedNote');
			showTooltip('tooltip', 10);
			replaceEmptyTable('emptyNeedNote', 'gvNeedNote');
		});

		//表单验证
		function valForm() {
			if (document.getElementById("hf").value == "") {
				top.ui.alert("请选择有效收货人");
				return false;
			}
			if (document.getElementById("hdnProjectCode").value == "") {
				top.ui.alert("请选择项目");
				return false;
			}
			if (document.getElementById('gvNeedNote').rows.length <= 1) {
				top.ui.alert("请选择资源");
				return false;
			}
			var inputs = document.getElementById('gvNeedNote').getElementsByTagName('INPUT');
			return true;
		}

		// 选择供应商
		function pickCorp() {
			jw.selectOneCorp({
				func: function (c) {
					$('#hdfCropCode').val(c.id);
					$('#hdfCropName').val(c.name);
					$('#btnCrop').click();
				}
			});
		}

		//选择人员
		function showConsignee() {
			jw.selectMultiUser({ nameinput: 'TBoxConsignee', codeinput: 'hf' });
		}

		// 从需求计划中选择
		function displaySelectFromPurchase() {
			var url = '/StockManage/ProjectFrame.aspx?path=selectWantPlan'
			top.ui.callback = function (obj) {
				$('#hdlfWantplanCodes').val(obj.code);
				$('#hdwzId').val(obj.code);
				$('#btnShowGv').click();
			}
			top.ui.openWin({ title: '从需求计划中选择', url: url, width: 820, height: 500 });
		}

		//当成绩过大是，禁止保存
		function computTotal(index, txt) {
			tb = document.getElementById('gvNeedNote');
			var cells = tb.rows[parseFloat(index) + 1].cells;
			var quantityStr = cells[8].children[0].value;
			var priceStr = cells[9].children[0].value;
			var quantity = parseFloat(parseFloat(quantityStr.replace(/\,/g, '')));
			var price = parseFloat(parseFloat(priceStr.replace(/\,/g, '')));
			if (!isNaN(quantity) && !isNaN(price)) {
				var total = quantity * price;
				if (total > 1500000000) {
					top.ui.alert('该资源的乘积过大!');
					txt.value = 0.000;
				}
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word" style="white-space: nowrap;">
					单据号
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtsnCode" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					项目
				</td>
				<td class="txt readonly" style="padding-right: 3px">
					<input type="text" readonly="readonly" style="width: 100%; height: 15px;" id="txtProjectName" runat="server" />

					<input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

				</td>
			</tr>
			<tr>
				<td align="right" style="white-space: nowrap;">
					说明
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtremark" TextMode="MultiLine" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					录入人
				</td>
				<td class="txt readonly">
					<input type="text" readonly="readonly" class="txtreadonly" id="txtAddUser" runat="server" />

				</td>
				<td class="word" style="white-space: nowrap;">
					录入时间
				</td>
				<td class="txt readonly" style="padding-right: 1px">
					<asp:TextBox ID="txtsnAddTime" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					有效收货人
				</td>
				<td class="txt mustInput" colspan="3">
					<input type="text" readonly="readonly" id="TBoxConsignee" class="select_input" imgclick="showConsignee" runat="server" />

					<asp:HiddenField ID="hf" runat="server" />
				</td>
			</tr>
		</table>
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td colspan="4" style="height: 10px">
					<hr class="sp" />
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<div class="headerDiv" style="text-align: left;">
						<input type="button" id="btnSelectByd" value="从需求计划单中选择" onclick="displaySelectFromPurchase();" style="width: 130px;" runat="server" />

						<asp:Button ID="btnDel" Style="width: 80px;" Text="删除材料" OnClick="btnDel_Click" runat="server" />
						<asp:Button ID="btnCrop" Text="设置供应商" Width="80px" Style="display: none;" OnClick="btnCrop_Click" runat="server" />
						<input type="button" id="btnSelectCorp" value="设置供应商" style="width: 80px;" onclick="pickCorp()" />
						<asp:HiddenField ID="hdfCropCode" runat="server" />
						<asp:HiddenField ID="hdfCropName" runat="server" />
						<asp:HiddenField ID="hdfCount" runat="server" />
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<div class="pagediv" style="border: solid 0px red;">
						<asp:GridView ID="gvNeedNote" Width="100%" AutoGenerateColumns="false" CssClass="gvdata" runat="server">
<EmptyDataTemplate>
								<table id="emptyNeedNote" class="tab">
									<tr class="header">
										<th scope="col" style="width: 20px;">
											<asp:CheckBox ID="chkSelectAll" runat="server" />
										</th>
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col">
											物资名称
										</th>
										<th scope="col">
											规格
										</th>
										<th scope="col">
											型号
										</th>
										<th scope="col">
											品牌
										</th>
										<th scope="col">
											技术参数
										</th>
										<th scope="col">
											单位
										</th>
										<th scope="col">
											数量
										</th>
										<th scope="col">
											采购价格
										</th>
										<th scope="col">
											供应商
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
										<asp:CheckBox ID="chkAll" runat="server" />
									</HeaderTemplate><ItemTemplate>
										<asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Eval("scode")) %>' runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资名称">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="规格">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
											<%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="型号">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="品牌">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("brand").ToString() %>'>
											<%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="技术参数">
<ItemTemplate>
										<span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
											<%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
										</span>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="单位" HeaderStyle-Width="25px"><ItemTemplate>
										<%# Eval("Name") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
										<asp:TextBox Width="80px" ID="txtNum" CssClass="decimal_input" onblur="computTotal(this.No,this)" ToolTip='<%# Convert.ToString(Eval("scode")) %>' Text='<%# Convert.ToString(Eval("number")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格"><ItemTemplate>
										<asp:TextBox ID="txtPrice" Width="80px" CssClass="decimal_input" onblur="computTotal(this.No,this)" Text='<%# Convert.ToString(Eval("price")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
										<asp:Label ID="labCrop" Text='<%# Convert.ToString(Eval("CorpName")) %>' ToolTip='<%# Convert.ToString(Eval("suppyCode")) %>' runat="server"></asp:Label>
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClientClick="return valForm()" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="winclose('AddSendNote','SendNoteList.aspx',false)" />
				</td>
			</tr>
		</table>
	</div>
	<asp:Button ID="btnShowGv" Text="显示需求计划" Style="display: none;" OnClick="btnShowGv_Click" runat="server" />
	<asp:HiddenField ID="hdwzId" runat="server" />
	<asp:HiddenField ID="hdGuid" runat="server" />
	<asp:HiddenField ID="hdnCodeList" runat="server" />
	
	<asp:HiddenField ID="hdlfWantplanCodes" runat="server" />
	</form>
</body>
</html>
