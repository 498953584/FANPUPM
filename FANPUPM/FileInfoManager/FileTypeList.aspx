<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileTypeList.aspx.cs" Inherits="FileInfoManager_FileTypeList" %>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>企业级目录设置</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvFile');
			setBtn(jwTable, 'hfldPurchaseChecked');
			$('#btnSave').hide();
		});

		function ondbSelectValue(id) {
			location = 'FileTypeList.aspx?id=' + id;
		}


		function valForm() {
			return $('#hfldFolderName').val() != '';
		}
		//新增目录
		function addFload(type) {
			var hdTypeValue = 'edit';
			var Dgtitle = '编辑目录';
			if (type != undefined && type == 'add') {
				hdTypeValue = 'add';
				Dgtitle = "新增目录";
			}
			document.getElementById("hdType").value = hdTypeValue
			//			document.getElementById("txtFileInfo").value = "";

			top.ui.prompt(Dgtitle, '目录名称', function (r) {
				if (r) {
					$('#hfldFolderName').val(r);
					$('#btnSave').click();
				}
			});
		}

		function setBtn(jwTable, hdChkId) {
			if (jwTable.table.firstChild.childNodes.length == 1) {
				//table中没有数据
				return;
			}
			jwTable.registClickTrListener(function () {
				document.getElementById(hdChkId).value = this.id;
				setDisabledButton('', 1);
			});
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson(checkedChk);
				if (checkedChk.length == 0) {
					setDisabledButton('disabled');
				}
				else
					if (checkedChk.length == 1) {
						setDisabledButton('', 1);
					}
					else {
						setDisabledButton('', 2);
					}

			});
			jwTable.registClickAllCHKLitener(function () {
				document.getElementById(hdChkId).value = jwTable.getCheckedChkIdJson();
				if (this.checked) {
					var checkedChks = jwTable.getAllChk();
					if (checkedChks.length > 0)
						setDisabledButton('', checkedChks.length);
				}
				else {
					setDisabledButton('disabled');
				}
			});
		}

		// 更新权限人员
		function returnUser(id, name) {
			$('#hfldUserCodes').val(id);
			$('#btnUpdateUserCodes').click();
		}

		// 选择人员
		function selectUser() {
			jw.selectMultiUser({
				codeinput: 'hfldUserCodes',
				func: function () {
					$('#btnUpdateUserCodes').click();
				}
			});
			return;
			var selId = $('#hfldPurchaseChecked').val();
			if (selId == "") {
				alert('系统提示: \n请选择要设置权限的节点！');
				return;
			}
			var url = "/Common/DivSelectAllUser.aspx?Method=returnUser&showSel=true&showType=1&showId=" + selId;
			document.getElementById("divFram").title = "选择人员";
			document.getElementById("ifFram").src = url;
			$('#divFram').dialog({
				open: function () {
					$('#divFram').append($('#btnUpdateUserCodes'));
					$(this).parent().appendTo("form:first");
				},
				modal: true,
				title: '资料权限',
				width: 610,
				height: 485
			});
		}

		//设置按钮权限
		function setDisabledButton(disabled, chkLength) {
			if (disabled != undefined && disabled != '') {
				$(':button[sign="1"]').each(function (i) {
					$(this).attr('disabled', 'disabled');
				});
				$(':submit[sign="1"]').each(function (i) {
					$(this).attr('disabled', 'disabled');
				});
			}
			else {
				if ($('#hfldPurchaseChecked').val() == '')
					return;
				arguments.callee('disabled');
				if (chkLength == 1) {
					var limit = $('#' + $('#hfldPurchaseChecked').val()).attr('limit');
					if (limit == '1') {
						$(':button[sign="1"]').each(function (i) {
							$(this).removeAttr('disabled');
						});
						$(':submit[sign="1"]').each(function (i) {
							$(this).removeAttr('disabled');
						});
					}
				}
				else {
					var selIds = $('#hfldPurchaseChecked').val();
					selIds = selIds.substring(1, selIds.length - 1).replace(/\"/g, '');
					var arrayId = selIds.split(',');
					var firstRowLimit = $('#' + arrayId[0]).get(0).limit;
					//多选都拥有权限
					var isAllLimit = true;
					if ($('#hfldManagerCode').val() == '') {
						for (i = 1; i < arrayId.length; i++) {
							var currentLimit = $('#' + arrayId[i]).get(0).limit;
							if (currentLimit != firstRowLimit) {
								isAllLimit = false;
								break;
							}
						}
					}
					if (isAllLimit && firstRowLimit == '1') {
						$('#btnDel').removeAttr('disabled');
					}
				}
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table width="100%" style="height: 100%;" cellpadding="0" cellspacing="0">
		<tr style="height: 20px; display: none;">
			<td class="divHeader">
				资料目录设置
			</td>
		</tr>
		<tr>
			<td style="height: 100%; width: 100%; vertical-align: top;">
				<table style="height: 100%;" width="100%" cellpadding="0" cellspacing="0">
					<tr>
						<td rowspan="2" style="width: 180px; height: 100%; vertical-align: top; border-right: solid 1px #AAAAAA;">
							<div id="departmentDiv" style="width: 180px; height: 100%; border: solid 0px red;
								overflow: auto;">
								<asp:TreeView ID="tvFile" ExpandDepth="1" ShowLines="true" OnSelectedNodeChanged="tvFile_SelectedNodeChanged" runat="server"><NodeStyle HorizontalPadding="5px" /><SelectedNodeStyle HorizontalPadding="5px" BackColor="#3399FF" ForeColor="White" /></asp:TreeView>
							</div>
						</td>
						<td style="vertical-align: top; height: 100%;">
							<table style="width: 100%; height: 100%;">
								<tr>
									<td class="divFooter">
										<table>
											<tr>
												<td>
													<input type="button" id="btnAdd" value="新增" style="cursor: pointer;" onclick="addFload('add')" />
													<input type="button" sign="1" id="btnEdit" disabled="disabled" onclick="addFload('edit')"
														value="重命名" />
													<asp:Button sign="1" ID="btnDel" disabled="disabled" Style="cursor: pointer;" Text="删除" OnClientClick="return confirm('您确定要删除吗??')" OnClick="btnDel_Click" runat="server" />
													<input type="button" sign="1" id="btnAleave" disabled="disabled" onclick="selectUser()"
														value="权限" />
													<asp:HiddenField ID="hfldUserCodes" runat="server" />
													<asp:Button ID="btnUpdateUserCodes" Style="display: none;" OnClick="btnUpdateUserCodes_Click" runat="server" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td style="height: 95%;">
										<div style="height: 100%; overflow: auto;">
											<asp:GridView CssClass="gvdata" ID="gvFile" AutoGenerateColumns="false" OnRowDataBound="gvFile_RowDataBound" DataKeyNames="id,fileName,FileOwner" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
															<asp:CheckBox ID="cbAllBox" runat="server" />
														</HeaderTemplate><ItemTemplate>
															<asp:CheckBox ID="cbBox" runat="server" />
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
															<%# Eval("No") %>
														</ItemTemplate>
</asp:TemplateField><asp:TemplateField><HeaderTemplate>
															目录名称
														</HeaderTemplate><ItemTemplate>
															<a href='#' onclick='ondbSelectValue(this.parentNode.parentNode.id)'>
																<%# Eval("FileName") %>
															</a>
														</ItemTemplate></asp:TemplateField><asp:TemplateField><HeaderTemplate>
															资料权限
														</HeaderTemplate><ItemTemplate>
															<asp:TextBox Style="height: 15px; border: solid 0px red;" TextMode="MultiLine" ReadOnly="true" Text='<%# System.Convert.ToString(base.GetUserName(Eval("userCodes").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="80px"><HeaderTemplate>
															创建者
														</HeaderTemplate><ItemTemplate>
															<%# PageHelper.QueryUser(this, Eval("FileOwner").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px"><HeaderTemplate>
															创建时间
														</HeaderTemplate><ItemTemplate>
															<%# Eval("CreateTime") %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="属性" HeaderStyle-Width="40px"><ItemTemplate>
														</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
											<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
											</webdiyer:AspNetPager>
										</div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
			</td>
		</tr>
	</table>
	<asp:Button ID="btnSave" OnClientClick="return valForm()" Text="保存" OnClick="btnSave_Click" runat="server" />
	<asp:HiddenField ID="hdGuid" runat="server" />
	<asp:HiddenField ID="hdId" runat="server" />
	<asp:HiddenField ID="hdUserCodes" runat="server" />
	<asp:HiddenField ID="hdType" runat="server" />
	<asp:Button ID="btnRe" Style="display: none;" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<!-- 目录名称-->
	<asp:HiddenField ID="hfldFolderName" runat="server" />
	<div id="divFram" title="" style="display: none">
		<iframe id="ifFram" frameborder="0" width="100%" height="100%" src=""></iframe>
	</div>
	<!-- 管理员编码-->
	<asp:HiddenField ID="hfldManagerCode" runat="server" />
	</form>
</body>
</html>
