<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModelList.aspx.cs" Inherits="TableTop_ModelList" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>桌面栏目设置</title>
	<script src="../Script/jquery-1.4.4.js" type="text/javascript"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.draggable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.position.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.resizable.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" src="/Script/jquery.ui/jquery.ui.sortable.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script src="../Script/json2.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('.nodisplay').css('display', 'none');
			var jwTable = new JustWinTable('gvwModelList');
			jwTable.registClickTrListener(function () {
				var btnDel = document.getElementById('btnDel');
				btnDel.removeAttribute('disabled');
			});

			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				var btnSelectPerson = document.getElementById('btnDel');
				if (checkedChk.length >= "1") {
					btnSelectPerson.removeAttribute('disabled');
				}
				else {
					document.getElementById('btnDel').setAttribute('disabled', 'disabled');
				}
			});

			jwTable.registClickAllCHKLitener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length >= 1) {
					document.getElementById('btnDel').setAttribute('disabled', 'disabled');
				}
				else {
					document.getElementById('btnDel').removeAttribute('disabled');
				}
			});

			$("#gvwModelList ").sortable({ stop: function (event, ui) {
				var str = GetTrIdStr();
				if (str != "") {
					$.get("ModelList.aspx", { orderid: str }, function (cdate) {
						if (cdate == "1") {
							$("#gvwModelList tr").each(function (i) {
								$(this).find("td").eq(1).html(i);
							});
						}
					});
				}
			}, axis: 'y', items: 'tr:not(.header)', delay: 300
			});

			$('#btnDel').get(0).onclick = function () {
				return jw.confirm('系统提示', '确认删除!', 'btnDel');
			}

		});

		function addList() {
			var url = '/TableTop/UserModelList.aspx?Method=returnModelId';
			top.ui.openWin({ title: '桌面栏目', url: url });

		}
		//选择模块返回值
		function returnModelId(id) {
			document.getElementById('hdfHD').value = id;
		}
		function ClickRow(model) {
			document.getElementById('hdfNotModel').value = "'" + model + "'";
			document.getElementById('btnDel').disabled = false;
		}

		function GetTrIdStr() {
			var str = "";
			$("#gvwModelList tr").each(function (i) {
				if ($(this).attr("id").toString() != "") {
					str += "|";
					str += i.toString();
					str += ":"
					str += $(this).attr("id").toString()
				}
			});
			$("#hfldtrorder").val(str);
			return str;
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
			<tr id="header" style="text-align: left;">
				<td>
					桌面显示栏目
				</td>
			</tr>
			<tr>
				<td style="vertical-align: top;">
					<table class="tab" style="vertical-align: top;">
						<tr>
							<td class="divFooter" style="text-align: left">
								<input id="btnAdd" type="button" value="新增" onclick="addList()" />
								<asp:Button ID="btnDel" Text="删除" Enabled="false" OnClick="btnDel_Click" runat="server" />
								<asp:HiddenField ID="hdfModel" runat="server" />
								<asp:HiddenField ID="hdfNotModel" runat="server" />
								<asp:HiddenField ID="hdfHD" runat="server" />
							</td>
						</tr>
						<tr style="vertical-align: top">
							<td>
								<div>
									<asp:GridView ID="gvwModelList" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvwModelList_RowDataBound" DataKeyNames="modelid" runat="server">
<EmptyDataTemplate>
											<table id="emptyContractType" class="gvdata">
												<tr class="header">
													<th scope="col" style="width: 20px;">
														<input id="chk1" type="checkbox" />
													</th>
													<th scope="col" style="width: 25px;">
														序号
													</th>
													<th scope="col" style="width: 50px;">
														模块编号
													</th>
													<th scope="col" style="width: 80px;">
														模块名称
													</th>
													<th scope="col" style="width: 80px;">
														自定栏目标题名称
													</th>
													<th scope="col">
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
													<asp:CheckBox ID="chkAll" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
												</HeaderTemplate><ItemTemplate>
													<asp:CheckBox ID="chk" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server" />
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
													<%# Container.DataItemIndex + 1 %>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="模块编号" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
													<%# Eval("modelid") %>
												</ItemTemplate></asp:TemplateField><asp:BoundField DataField="v_mkmc" HeaderText="模块名称" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="排列序号" HeaderStyle-Width="50px" Visible="false"><ItemTemplate>
													<asp:TextBox ID="txtOrderID" Style="text-align: right" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server"></asp:TextBox>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="自定义栏目名称"><ItemTemplate>
													<asp:TextBox ID="txtModelName" onkeydown="if(event.keyCode==13)event.keyCode=9" runat="server"></asp:TextBox>
												</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr style="height: 30px;">
				<td class="divFooter" style="text-align: right; padding-right: 10px;">
					<asp:Button ID="btnSave" Text="保存" OnClientClick="GetTrIdStr()" Enabled="false" OnClick="btnSave_Click" runat="server" />
				</td>
			</tr>
		</table>
		<div class="nodisplay">
			<asp:Button ID="Button1" Text="" OnClick="Button1_Click" runat="server" />
		</div>
	</div>
	<input id="hfldtrorder" type="hidden" runat="server" />

	</form>
</body>
</html>
