<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequirePlanList.aspx.cs" Inherits="Equ_RequirePlan_RequirePlanList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>需求计划</title>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#divDateTime').css('display', 'none');
			//编制工程进度
			$('#btnProgress').click(function () {
				$('#divDateTime').css('display', '').window({
					width: 250,
					height: 150,
					shadow: false,
					modal: true
				});
			});
			$('#btnRefresh').hide();
			var jwTable = new JustWinTable('gvRequirePlan');
			replaceEmptyTable('emptyRequirePlan', 'gvRequirePlan');
			setButton(jwTable, 'btnDel', 'btnEdit', 'btnQuery', 'hfldIdsChecked');
			jw.tooltip();
			//新增
			$('#btnAdd').click(function () {
				openNewTab('add');
			});
			//编辑
			$('#btnEdit').click(function () {
				openNewTab('edit');
			});
			//查看维修申请
			$('#btnQuery').click(function () {
				var url = '/Equ/RequirePlan/RequirePlanView.aspx?ic=' + $('#hfldIdsChecked').val();
				viewopen(url, '查看设备需求计划');
			});
			var flowstateIndex = getFlowStateIndex(jwTable);
			setWfButtonState2(jwTable, 'WF1');  //审核
			var userCode = getCookie('UserCode');
			//单击行
			jwTable.registClickTrListener(function () {
				var state = $(this).find('TD:eq(' + flowstateIndex + ') SPAN').attr('state');
				//非管理员未提交和重报的状态允许编制工程进度
				if (state == '-1' || state == '-3') {
					$('#btnProgress').removeAttr('disabled');
				} else {
					$('#btnProgress').attr('disabled', 'disabled');
				}
				//管理员可以编辑任何状态下的工程进度
				if (userCode == '00000000') {
					$('#btnProgress').removeAttr('disabled');
				}
			});
			//单选
			jwTable.registClickSingleCHKListener(function () {
				var checkedChk = jwTable.getCheckedChk();
				if (checkedChk.length == 1) {
					var tr = getFirstParentElement(checkedChk[0], 'TR');
					var state = $(tr).find('TD:eq(' + flowstateIndex + ') SPAN').attr('state')
					//非管理员未提交和重报的状态允许编制工程进度
					if (state == '-1' || state == '-3') {
						$('#btnProgress').removeAttr('disabled');
					} else {
						$('#btnProgress').attr('disabled', 'disabled');
					}
					//管理员可以编辑任何状态下的工程进度
					if (userCode == '00000000') {
						$('#btnProgress').removeAttr('disabled');
					}
				}
				else {
					$('#btnProgress').attr('disabled', 'disabled');
				}
			});
			//全选
			jwTable.registClickAllCHKLitener(function () {
				$('#btnProgress').attr('disabled', 'disabled');
			});
			//选择年月后，编制工程进度按钮
			$('#btnOK').click(makeProgress);
		});
		//打开新的标签页 新增/编辑
		function openNewTab(action) {
			var title = '新增设备需求计划';
			var urlStr = '/Equ/RequirePlan/RequirePlanEdit.aspx?' + new Date().getTime() + '&action=' + action;
			if (action == 'edit') {
				urlStr = urlStr + '&id=' + $('#hfldIdsChecked').val();
				title = '编辑设备需求计划';
			}
			top.ui.callback = function () {
				$('#btnRefresh').click();
			}
			top.ui.openWin({ title: title, url: urlStr, width: 450, height: 300 });
		}
		//标签页查看
		function Query(id) {
			parent.desktop.RepairPlanView = window;
			var url = '/Equ/RequirePlan/RequirePlanView.aspx?ic=' + id;
			toolbox_oncommand(url, "查看设备需求计划");
		}
		// 选择项目
		function openProjPicker() {
			jw.selectOneProject({ idinput: 'hfldPrjId', nameinput: 'txtPrjName' });
		}
		//编制工程进度明细
		function makeProgress() {
			var year = $('select#ddlYear  option:selected').val();
			var month = $('select#ddlMonth  option:selected').val();
			parent.desktop.ProgressDetailEdit = window;
			var url = '/Equ/RequirePlan/ProgressDetailEdit.aspx?planId=' + $('#hfldIdsChecked').val() + "&year=" + year + "&month=" + month;
			toolbox_oncommand(url, "编制工程进度");
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
			height: 90%; vertical-align: top;">
			<tr style="height: 1px;">
				<td style="vertical-align: top;">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								编号
							</td>
							<td>
								<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								项目
							</td>
							<td>
								<span class="spanSelect" style="width: 122px">
									<asp:TextBox ID="txtPrjName" Style="width: 97px; height: 15px; border: none;
										line-height: 16px; margin: 1px 0px 1px 2px;" runat="server"></asp:TextBox>
									<img alt="选择项目" onclick="openProjPicker();" src="../../images/icon.bmp" style="float: right;" />
								</span>
								<asp:HiddenField ID="hfldPrjId" runat="server" />
							</td>
							<td>
								<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="divFooter" style="text-align: left;">
					<input type="button" id="btnAdd" value="新增" />
					<input type="button" id="btnEdit" value="编辑" disabled="disabled" />
					<input type="button" id="btnQuery" value="查看" disabled="disabled" />
					<input type="button" id="btnProgress" value="编制工程进度" disabled="disabled" style="width: 90px;" />
					<asp:Button ID="btnDel" Text="删除" disabled="disabled" OnClientClick="return confirm('您确定要删除吗?')" OnClick="btnDel_Click" runat="server" />
					<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" BusiCode="150" runat="server" />
				</td>
			</tr>
			<tr>
				<td style="width: 100%;">
					<table class="tab">
						<tr>
							<td style="height: 100%; vertical-align: top;">
								<div class="">
									<asp:GridView ID="gvRequirePlan" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvRequirePlan_RowDataBound" DataKeyNames="Id,PrjId" runat="server">
<EmptyDataTemplate>
											<table id="emptyRequirePlan" class="gvdata">
												<tr class="header">
													<th scope="col" style="width: 20px;">
														<input id="chk1" type="checkbox" />
													</th>
													<th scope="col" style="width: 25px;">
														序号
													</th>
													<th scope="col" style="width: 80px;">
														编号
													</th>
													<th scope="col">
														项目
													</th>
													<th scope="col" style="width: 50px;">
														流程状态
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
													<asp:CheckBox ID="chkAll" runat="server" />
												</HeaderTemplate><ItemTemplate>
													<asp:CheckBox ID="chk" runat="server" />
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
													
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="编号" HeaderStyle-Width="150px"><ItemTemplate>
													<span class="link tooltip" title='' onclick="Query('')">
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="项目"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="流程状态" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"><ItemTemplate>
													
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
	</div>
	<div id="divDateTime" title="选择年月">
		<table class="queryTable" cellpadding="3px" cellspacing="0px" style="width: 100%;
			height: 95%;">
			<tr>
				<td class="descTd">
					年份
				</td>
				<td>
					<asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
				</td>
				<td class="descTd">
					月份
				</td>
				<td>
					<asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td colspan="3">
				</td>
				<td>
					<input type="button" id="btnOK" value="确定" />
				</td>
			</tr>
		</table>
	</div>
	
	<asp:HiddenField ID="hfldIdsChecked" runat="server" />
	
	<asp:Button ID="btnRefresh" OnClick="btnRefresh_Click" runat="server" />
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
	</form>
</body>
</html>
