<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentPurchaseEdit.aspx.cs" Inherits="Equ_Purchase_EquipmentPurchaseEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectresource_ascx" Src="~/StockManage/UserControl/SelectResource.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>固定资产采购单</title><link href="../../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../script/Config2.js"></script>
	<script type="text/javascript" src="../../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../script/ValidateInput.js"></script>
	<script type="text/javascript" src="../../../Script/jw.js"></script>
	<script type="text/javascript" src="../../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="../../../Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnBindResource').hide();
			jw.tooltip();
			setTxtCorpDisabled();       //禁用供应商
			$('#btnCancel').click(btnCancelEvent); //取消按钮
			$('#btnDelete').click(deletePurchaseplan);
			$('#btnSave').click(btnSaveClick);
			var jwTable = new JustWinTable('gvwMaterialStock');
			//单击行
			jwTable.registClickTrListener(function () {
				document.getElementById('hfldMaterialChecked').value = '["' + this.id + '"]';
			});
			//单选
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					//没有选择行
					$('#btnDelete').setAttribute('disabled', 'disabled');
				}
				else if (checkedChk.length == 1) {
					//选择1行
					document.getElementById('hfldMaterialChecked').value = jwTable.getCheckedChkIdJson(checkedChk);
					document.getElementById('btnDelete').removeAttribute('disabled');
				}
				else {
					//选择多行
					document.getElementById('hfldMaterialChecked').value = jwTable.getCheckedChkIdJson(checkedChk);
					document.getElementById('btnDelete').removeAttribute('disabled');
				}
			});
			//全选
			jwTable.registClickAllCHKLitener(function () {
				if (this.checked) {
					document.getElementById('hfldMaterialChecked').value = jwTable.getCheckedChkIdJson();
					document.getElementById('btnDelete').removeAttribute('disabled');
				}
				else {
					document.getElementById('hfldMaterialChecked').value = '';
					document.getElementById('btnDelete').setAttribute('disabled', 'disabled');
				}
			});
			replaceEmptyTable('emptyStock', 'gvwMaterialStock');
		});

		// 从材料库中选择资源
		function selectResource() {
			if ($('#txtContract').val() == "") {
				top.ui.alert('请先选择合同');
				return false;
			}
			var typeCode = '0002,0003';     //控制选择资源的类型 0002代表材料  0003代表机械
			var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=' + typeCode;
			top.ui.callback = function (obj) {
				$('#hfldResourceId').val(obj.id);
				$('#btnBindResource').click();
			}
			top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
		}
		//删除
		function deletePurchaseplan() {
			if (!confirm("确定要删除吗？")) {
				return false;
			}
		}
		//取消
		function btnCancelEvent() {
			winclose('PurchaseEdit', 'Purchase.aspx', false)
		}
		// 选择供应商
		function pickCorp(txtCorp) {
			var txtID = txtCorp.id.replace('txt', 'hfld');
			jw.selectOneCorp({ idinput: txtID, nameinput: txtCorp.id });
		}
		//保存按钮的验证
		function btnSaveClick() {
			if (!validate()) {
				return false;
			}
		}
		//验证输入内容
		function validate() {
			if (!document.getElementById('txtContract').value) {
				top.ui.alert('合同不能为空');
				return false;
			}
			if (!document.getElementById('gvwMaterialStock') || document.getElementById('gvwMaterialStock').firstChild.childNodes.length == 1) {
				top.ui.alert('请选择采购物资');
				return false;
			}
			var inputs = document.getElementById('gvwMaterialStock').getElementsByTagName('INPUT');
			for (var i = 0; i < inputs.length; i++) {
				if (inputs[i].id.indexOf('hfldCorp') > 0) {
					if (!inputs[i].value) {
						top.ui.alert('供应商不能为空');
						return false;
					}
				}
			}
			return true;
		}
		//设置供应商不允许编辑
		function setTxtCorpDisabled() {
			var inputs = document.getElementsByTagName('INPUT');
			for (var i = 0; i < inputs.length; i++) {
				if (inputs[i].id.indexOf('txtCorp') > 0) {
					inputs[i].setAttribute('readonly', 'readonly');
				}
			}
		}
		//选择合同
		function selectContract() {
			jw.selectOutCon({
				func: function (con) {
					$('#hfldContract').val(con.id);
					$('#txtContract').val(con.name);
					$('#hfldBName').val(con.bname);
					$('#txtProject').val(con.prjName);
					$('#hfldProject').val(con.prjId);
				}
			});
		}
		//计算小计与合计
		function theMonenyChange(index, isOnblur) {
			tb = document.getElementById('gvwMaterialStock');
			var cells = tb.rows[parseFloat(index) + 1].cells;
			var quantity = parseFloat(cells[10].children[0].value.replace(/\,/g, ''));
			var price = parseFloat(cells[11].children[0].value.replace(/\,/g, ''));
			if (isOnblur == '1') {
				cells[10].children[0].value = getFormat(quantity);
			}
			if (isOnblur == '1') {
				cells[11].children[0].value = getFormat(price);
			}
			var total = getFormat(quantity) * getFormat(price);
			cells[12].innerText = getFormat(total);

			var contractMoney = 0;
			for (var i = 1; i < tb.rows.length - 1; i++) {
				contractMoney += parseFloat(tb.rows[i].cells[12].innerText);
			}
			tb.rows[tb.rows.length - 1].cells[1].innerText = getFormat(contractMoney);
		}
		//格式化三位小数
		function getFormat(num) {
			var numFormat;
			if (num.toFixed) {
				numFormat = num.toFixed(3);
			} else {
				var f3 = Math.pow(10, 3);
				numFormat = Math.round(num * f3) / f3;
			}
			return numFormat;
		}
		//当乘积过大时，禁止保存
		function computTotal(index, txt) {
			tb = document.getElementById('gvwMaterialStock');
			var cells = tb.rows[parseFloat(index) + 1].cells;
			var quantityStr = cells[10].children[0].value;
			var priceStr = cells[11].children[0].value;
			var quantity = parseFloat(parseFloat(quantityStr.replace(/\,/g, '')));
			var price = parseFloat(parseFloat(priceStr.replace(/\,/g, '')));
			if (!isNaN(quantity) && !isNaN(price)) {
				var total = quantity * price;
				if (total > 1500000000) {
					alert('系统提示：\n\n该资源的乘积过大!');
					txt.value = 0.000;
					theMonenyChange(index, '0');
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
					<asp:Label ID="lblTitle" Font-Bold="true" Text="采购单" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" style="" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					采购编号
				</td>
				<td class="txt readonly" id="ttt">
					<asp:TextBox ID="txtPpcode" ReadOnly="true" Height="15px" Style="width: 100%;" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					编制日期
				</td>
				<td class="txt readonly">
					<asp:TextBox ReadOnly="true" ID="txtInputDate" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					合同
				</td>
				<td class="txt mustInput" style="padding-right: 3px">
					<span class="spanSelect" style="width: 100%;">
						<input type="text" id="txtContract" style="width: 90%; height: 15px; border: none;
							line-height: 16px; margin: 1px 0px 1px 2px" readonly="readonly" runat="server" />

						<img src="../../../images/icon.bmp" style="float: right;" alt="选择合同" id="img1" onclick="selectContract();" />
					</span>
					<asp:HiddenField ID="hfldContract" runat="server" />
					<asp:HiddenField ID="hfldBName" runat="server" />
				</td>
				<td class="word">
					编制人
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtPerson" Height="15px" Style="width: 100%;" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="vertical-align: top; padding-top: 7px;">
					说明
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtExplain" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					附件
				</td>
				<td colspan="3" style="padding-right: 0px;">
					<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="附件" FileType="*.*" Class="EquipmentPurchase" runat="server" />
				</td>
			</tr>
		</table>
		<table class="tableContent2" cellpadding="5" cellspacing="0">
			<tr>
				<td>
					<hr class="sp" />
				</td>
			</tr>
			<tr>
				<td>
					<div class="headerDiv" style="text-align: left;">
						<input type="button" id="btnPickResource" style="width: 150px" value="从材料库中选择" onclick="selectResource();" />
						<div id="divSR" style="display: none">
							<MyUserControl:stockmanage_usercontrol_selectresource_ascx ID="SelectResource1" Text="从材料库中选择" ButtonId="btnBindResource" TypeCode="0002,0003" runat="server" />
						</div>
						<asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
					</div>
				</td>
			</tr>
			<tr style="vertical-align: top">
				<td style="text-align: right">
					<div class="pagediv">
						<asp:GridView ID="gvwMaterialStock" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvwMaterialStock_RowDataBound" DataKeyNames="ResourceCode" runat="server">
<EmptyDataTemplate>
								<table id="emptyStock" class="tab">
									<tr class="header">
										<th scope="col" style="width: 20px;">
											<asp:CheckBox ID="chkSelectAll" runat="server" />
										</th>
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col">
											物资编号
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
											库存量
										</th>
										<th scope="col">
											数量
										</th>
										<th scope="col">
											采购价格
										</th>
										<th scope="col">
											小计
										</th>
										<th scope="col">
											供应商
										</th>
										<th scope="col">
											到货日期
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
										<asp:CheckBox ID="chkSelectAll" runat="server" />
									</HeaderTemplate><ItemTemplate>
										<asp:CheckBox ID="chkSelectSingle" runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="物资编号">
<ItemTemplate>
										<%# Eval("ResourceCode") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="物资名称">
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
</asp:TemplateField><asp:TemplateField HeaderText="单位">
<ItemTemplate>
										<%# Eval("Name") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="库存量" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
										<%# WebUtil.GetStockNumberByCode(Eval("ResourceCode").ToString()).ToString() %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数量"><ItemTemplate>
										<asp:TextBox ID="txtNumber" Style="text-align: right;" Width="60px" onkeypress="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else this.t_value=this.value;if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onkeyup="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else {this.t_value=this.value;if(this.t_value.length>0)theMonenyChange(this.No,'0');}if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" CssClass="decimal_input" onblur="computTotal(this.No,this)" Text='<%# Convert.ToString((Eval("number").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("number")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价格"><ItemTemplate>
										<asp:TextBox ID="txtSprice" Style="text-align: right;" Width="60px" onkeypress="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else this.t_value=this.value;if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" onkeyup="if(!this.value.match(/^[\+\-]?\d*?\.?\d*?$/))this.value=this.t_value;else {this.t_value=this.value;if(this.t_value.length>0)theMonenyChange(this.No,'0');}if(this.value.match(/^(?:[\+\-]?\d+(?:\.\d+)?)?$/))this.o_value=this.value" CssClass="decimal_input" onblur="computTotal(this.No,this)" Text='<%# Convert.ToString((Eval("sprice").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("sprice")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="decimal_input"><ItemTemplate>
										<%# Convert.ToDecimal(Eval("Total")).ToString("0.000") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商">
<ItemTemplate>
										<asp:TextBox ID="txtCorp" Width="90px" CssClass="txtreadonly" ondblclick="pickCorp(this)" Style="background-color: #ffffc0;" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:TextBox>
										<asp:HiddenField ID="hfldCorp" Value='<%# Convert.ToString(Eval("corp")) %>' runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="到货日期">
<ItemTemplate>
										<asp:TextBox ID="txtarrivalDate" onclick="WdatePicker()" Style="height: 15px;
											width: 90px; text-align: left;" Text='<%# Convert.ToString((Eval("arrivalDate").ToString() == "") ? "" : Eval("arrivalDate").ToString().Substring(0, Eval("arrivalDate").ToString().LastIndexOf(" ") + 1)) %>' runat="server"></asp:TextBox>
									</ItemTemplate>
</asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	<div id="divSelectFromPurchase" title="选择采购计划" style="display: none">
		<iframe id="ifResoueceFromPurchase" frameborder="0" width="100%" height="100%"></iframe>
	</div>
	<asp:HiddenField ID="hfldPid" runat="server" />
	<!--项目编码-->
	<asp:HiddenField ID="hfldProject" runat="server" />
	<!--存放从弹出窗口返回的物质采购计划编号-->
	<asp:HiddenField ID="hfldPurchaseplan" runat="server" />
	<!--存放所有被选中物资采购计划编号-->
	<asp:HiddenField ID="hfldMaterialChecked" runat="server" />
	<!--存放从材料库选择的物资编码-->
	<asp:HiddenField ID="hdnCodeList" runat="server" />
	<!--从采购计划中选择物资的物资ID-->
	<asp:HiddenField ID="hfldResourceId" runat="server" />
	
	<asp:HiddenField ID="hfldppcode" runat="server" />
	<!--从采购计划中选择物资后执行-->
	<asp:Button ID="btnBindResource" Text="Button" OnClick="btnBindResource_Click" runat="server" />
	<asp:HiddenField ID="hidenppcode" runat="server" />
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
	<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />
	</form>
</body>
</html>
