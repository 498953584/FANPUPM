<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FirstStorageEdit.aspx.cs" Inherits="StockManage_Storage_FirstStorageEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_usercontrol_selectresource_ascx" Src="~/StockManage/UserControl/SelectResource.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="oa_vehicle_vehicleusercontrol_oausercontrol_ascx" Src="~/oa/Vehicle/VehicleUserControl/OaUserControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>甲供入库</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="../script/Config2.js"></script>
	<script type="text/javascript" src="../script/JustWinTable.js"></script>
	<script type="text/javascript" src="../script/ValidateInput.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnBindResource').hide();
			showTooltip('tooltip', 10);
			var action = getRequestParam('Action');
			if (action == 'Query') {
				setAllInputDisabled();
			}
			//仓库不能手动编辑
			document.getElementById('PickTreasury1_txtTreasuryName').setAttribute('readOnly', 'readOnly');
			//供应商不能手动编辑
			setTxtCorpDisabled();
			addEvent(document.getElementById('btnCancel'), 'click', btnCancelEvent);
			document.getElementById('btnDelete').onclick = deleteStorageStock;
			document.getElementById('btnSave').onclick = btnSaveEvent;

			var jwTable = new JustWinTable('gvwStorageStock');
			replaceEmptyTable('emptyStock', 'gvwStorageStock');
			jwTable.registClickTrListener(function () {
				document.getElementById('hfldResourceCode').value = '["' + this.id + '"]';
			});

			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 0) {
					document.getElementById('btnDelete').setAttribute('disabled', 'disabled');
				}
				else if (checkedChk.length == 1) {
					document.getElementById('hfldResourceCode').value = jwTable.getCheckedChkIdJson(checkedChk);
					document.getElementById('btnDelete').removeAttribute('disabled');
				}
				else {
					document.getElementById('hfldResourceCode').value = jwTable.getCheckedChkIdJson(checkedChk);
					document.getElementById('btnDelete').removeAttribute('disabled');
				}
			});

			jwTable.registClickAllCHKLitener(function () {
				if (this.checked) {
					document.getElementById('hfldResourceCode').value = jwTable.getCheckedChkIdJson();
					document.getElementById('btnDelete').removeAttribute('disabled');
				}
				else {
					document.getElementById('hfldResourceCode').value = '';
					document.getElementById('btnDelete').setAttribute('disabled', 'disabled');
				}
			});
		});


		//选择项目
		function pickProject() {
			jw.selectOneProject({ idinput: 'hfldProject', nameinput: 'txtProject' });
		}

		function btnCancelEvent() {
			winclose('FirstStorageEdit', 'FirstStorage.aspx', false)
		}

		function deleteStorageStock() {
			if (!confirm("确定要删除吗？")) {
				return false;
			}
		}

		function btnSaveEvent() {
			if (!validateInput()) {
				return false;
			}
		}

		function validateInput() {
			if (!document.getElementById('PickTreasury1_txtTreasuryName').value) {
				alert('系统提示：\n\n仓库名称不能为空');
				return false;
			}
			if (!document.getElementById('gvwStorageStock') || document.getElementById('gvwStorageStock').firstChild.childNodes.length == 1) {
				alert('系统提示：\n\n请从材料库中选择物资');
				return false;
			}
			var inputs = document.getElementById('gvwStorageStock').getElementsByTagName('INPUT');
			for (var i = 0; i < inputs.length; i++) {
				if (inputs[i].id.indexOf('txtNumber') >= 0) {
					var validateInput = new ValidateInput(inputs[i].id, '数量');
					if (!validateInput.validateMustInput()) return false;
					if (!validateInput.validateIntFormat()) return false;
					if (!validateInput.validateNoneZero()) return false;
				}

				if (inputs[i].id.indexOf('hfldCorp') >= 0) {
					var validateInput = new ValidateInput(inputs[i].id, '供应商');
					if (!validateInput.validateMustInput()) return false;
				}
			}
			return true;
		}

		function pickCorp(txtCorp) {
			var txtID = txtCorp.id.replace('txt', 'hfld');
			jw.selectOneCorp({ idinput: txtID, nameinput: txtCorp.id });
		}

		//选择 移交人员
		function selectTrustee() {
			jw.selectOneUser({ nameinput: 'txttrustee' });
		}

		//选择  监理 人员
		function selectSupervisor() {
			jw.selectOneUser({ nameinput: 'txtsupervisor' });
		}

		//设置供应商不能手动编辑
		function setTxtCorpDisabled() {
			var inputs = document.getElementsByTagName('INPUT');
			for (var i = 0; i < inputs.length; i++) {
				if (inputs[i].id.indexOf('txtCorp') > 0) {
					inputs[i].setAttribute('readonly', 'readonly');
				}
			}
		}

		// 从材料库中选择资源
		function selectResource() {
			if ($('#txtContract').val() == "") {
				top.ui.alert('请先选择合同');
				return false;
			}

			var typeCode = '0002,0003';
			var src = '/StockManage/UserControl/SelectResource.aspx?type=2&TypeCode=' + typeCode;
			top.ui.callback = function (obj) {
				$('#hfldResourceId').val(obj.id);
				$('#btnBindResource').click();
			}
			top.ui.openWin({ title: '选择资源', url: src, width: 1010, height: 500 });
		}

		function selectTrea() {
			jw.selectTreasury({ codeinput: 'hfldTrea', nameinput: 'txtTrea', module: 'import' });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divFramPrj" title="" style="display: none">
		<iframe id="ifFramPrj" frameborder="0" width="99%" height="99%" src=""></iframe>
	</div>
	<div class="divHeader2">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="甲供入库单" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word" style="white-space: nowrap;">
					入库编号
				</td>
				<td class="txt readonly" id="ttt">
					<asp:TextBox ID="txtScode" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					入库日期
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtInputDate" ReadOnly="true" Height="15px" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					仓库名称
				</td>
				<td class="txt mustInput" style="padding-right: 3px">
					<asp:TextBox ID="txtTrea" CssClass="select_input" imgclick="selectTrea" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hfldTrea" runat="server" />
				</td>
				<td class="word" style="white-space: nowrap;">
					项目
				</td>
				<td class="txt">
					<input type="text" id="txtProject" readonly="readonly" class="select_input" imgclick="pickProject" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					验收人
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtPerson" Height="15px" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					移交人
				</td>
				<td>
					<asp:TextBox ID="txttrustee" Height="15px" CssClass="select_input" imgclick="selectTrustee" runat="server"></asp:TextBox>
					<asp:HiddenField ID="hfldTypeID" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					监理
				</td>
				<td>
					<asp:TextBox ID="txtsupervisor" Height="15px" CssClass="select_input" imgclick="selectSupervisor" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					说明
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtExplain" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					附件
				</td>
				<td colspan="3" style="padding-right: 0px;">
					<MyUserControl:epc_usercontrol1_filelink2_ascx ID="flAnnx" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="4" style="height: 10px">
					<hr class="sp" />
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<div class="headerDiv" style="text-align: left;">
						<input type="button" id="btnPickResource" style="width: 150px" value="从材料库中选择" onclick="selectResource();" />
						<asp:Button ID="btnDelete" Text="删除" OnClick="btnDelete_Click" runat="server" />
					</div>
				</td>
			</tr>
			<tr style="vertical-align: top">
				<td colspan="4">
					<div class="pagediv">
						<asp:GridView ID="gvwStorageStock" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwStorageStock_RowDataBound" DataKeyNames="ResourceCode" runat="server">
<EmptyDataTemplate>
								<table id="emptyStock" class="gvdata">
									<tr class="header">
										<td style="width: 20px">
											<asp:CheckBox ID="chkSelectAll" runat="server" />
										</td>
										<td style="width: 25px">
											序号
										</td>
										<td align="center">
											物资编号
										</td>
										<td align="center">
											物资名称
										</td>
										<td align="center">
											规格
										</td>
										<td align="center">
											型号
										</td>
										<td align="center">
											品牌
										</td>
										<td align="center">
											技术参数
										</td>
										<td align="center">
											单位
										</td>
										<td align="center">
											数量
										</td>
										<td align="center">
											供应商
										</td>
										<td align="center">
											验收情况
										</td>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
										<asp:CheckBox ID="chkSelectAll" runat="server" />
									</HeaderTemplate>

<ItemTemplate>
										<asp:CheckBox ID="chkSelectSingle" runat="server" />
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceCode" HeaderText="物资编号" /><asp:TemplateField HeaderText="物资名称" HeaderStyle-Width="200px"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格" HeaderStyle-Width="150px"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
											<%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("brand").ToString() %>'>
											<%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
											<%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:BoundField DataField="UnitName" HeaderText="单位" /><asp:TemplateField HeaderText="数量"><ItemTemplate>
										<asp:TextBox ID="txtNumber" Style="text-align: right;" Width="70px" CssClass="decimal_input" Text='<%# System.Convert.ToString((Eval("number").ToString() == "") ? "0.000" : System.Convert.ToDecimal(Eval("number")).ToString("0.000"), System.Globalization.CultureInfo.CurrentCulture) %>' No='<%# System.Convert.ToString(Container.DataItemIndex, System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate>
										<asp:TextBox ID="txtCorp" Width="90px" CssClass="txtreadonly" ondblclick="pickCorp(this)" Style="background-color: #ffffc0;" ToolTip="请双击此处选择" Text='<%# System.Convert.ToString(Eval("CorpName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
										<asp:HiddenField ID="hfldCorp" Value='<%# System.Convert.ToString(Eval("corp"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="验收情况"><ItemTemplate>
										<asp:TextBox ID="txtcheckCondition" Style="width: 100%;" Text='<%# System.Convert.ToString(Eval("checkCondition"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
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
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" />
				</td>
			</tr>
		</table>
	</div>
	<!--项目编码-->
	<asp:HiddenField ID="hfldPid" runat="server" />
	<asp:HiddenField ID="hfldProject" runat="server" />
	<asp:HiddenField ID="hfldPurchaseCode" runat="server" />
	<asp:HiddenField ID="hfldResourceId" runat="server" />
	<asp:HiddenField ID="hfldResourceCode" runat="server" />
	<asp:HiddenField ID="hfldIsFirst" runat="server" />
	<!--存放从材料库选择的物资编号-->
	<asp:HiddenField ID="hdnCodeList" runat="server" />
	<!--从采购计划中选择物资后执行-->
	<asp:Button ID="btnBindResource" Text="Button" OnClick="btnBindResource_Click" runat="server" />
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	</form>
</body>
</html>
