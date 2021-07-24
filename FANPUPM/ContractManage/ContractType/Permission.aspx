<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Permission.aspx.cs" Inherits="ContractManage_ContractType_Permission" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="cn.justwin.contractDAL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>合同类型权限</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var typeTable = new JustWinTable('gvwContractType');
			setbtn(typeTable, 'btnUserCodes');
			typeTable.registClickTrListener(function () {
				$('#hfContractTypeId').val(this.id);
				$('#btnUserCodes').removeAttr('disabled');
			});
			jw.tooltip();
			//权限按钮事件
			$('#btnUserCodes').click(function () {
				var url = "/ContractManage/SetRole.aspx?tbName=Con_ContractType&idName=TypeID&field=UserCodes&id=" +
                    $('#hfContractTypeId').val() + "&btnId=btnDataBind?type=contractType";
				top.ui.callback = function () {
					$('#btnDataBind').click();
				}
				top.ui.openWin({ title: '合同类型权限管理', url: url });
			});

			//注册管理事件
			showTooltip('tooltip', 10);
			replaceEmptyTable('emptyContractType', 'gvwContractType');
			$('#btnDataBind').css('display', 'none');
		});

		function CheckType(typeId) {
			parent.desktop.ContractTypeEdit = window;
			var url = '/ContractManage/ContractType/ContractTypeEdit.aspx?Action=Query&TypeID=' + typeId;
			toolbox_oncommand(url, "查看合同类型");
		}

		function setbtn(typeTable, btnUserCodes) {
			if (typeTable.table.firstChild.childNodes.length == 1) {
				return;
			}
			typeTable.registClickSingleCHKListener(function () {
				var checkChk = typeTable.getCheckedChk();
				if (checkChk.length == 0) {
					document.getElementById(btnUserCodes).setAttribute('disabled', 'disabled');
				}
				else {
					document.getElementById(btnUserCodes).removeAttribute('disabled');
					document.getElementById('hfContractTypeId').value = typeTable.getCheckedChkIdJson(checkChk);
				}
			});
			typeTable.registClickAllCHKLitener(function () {
				document.getElementById(btnUserCodes).removeAttribute('disabled');
				if (this.checked) {
					var checkChk = typeTable.getAllChk();
					if (checkChk.length == 1) {
						var values = typeTable.getCheckedChkIdJson(checkChk);
						document.getElementById('hfContractTypeId').value = values;
					}
					else {
						var values = typeTable.getCheckedChkIdJson(checkChk);
						if (values.toString().indexOf('[') == 0) {
							document.getElementById('hfContractTypeId').value = values;
						}
					}
				}
			});
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table style="vertical-align: top; width: 100%; height: 100%;">
		
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<table style="width: 100%">
					<tr>
						<td>
							<table class="queryTable" cellpadding="3px" cellspacing="0px">
								<tr>
									<td class="descTd">
										类型编码
									</td>
									<td>
										<asp:TextBox ID="txtTypeCode" Width="120px" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										类型名称
									</td>
									<td>
										<asp:TextBox ID="txtTypeName" Width="120px" runat="server"></asp:TextBox>
									</td>
									<td>
										<asp:Button ID="btnSelect" Text="查询" OnClick="btnSelect_Click" runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr style="vertical-align: top;">
						<td class="divFooter" style="text-align: left;">
							<input type="button" id="btnUserCodes" value="权限" disabled="disabled" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:GridView ID="gvwContractType" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwContractType_RowDataBound" DataKeyNames="TypeID" runat="server">
<EmptyDataTemplate>
									<table id="emptyContractType" class="gvdata">
										<tr class="header">
											<th scope="col" style="width: 25px;">
												序号
											</th>
											<th scope="col">
												类型编码
											</th>
											<th scope="col">
												类型名称
											</th>
											<th scope="col">
												权限人员
											</th>
											<th scope="col">
												成本科目
											</th>
											<th scope="col">
												录入人
											</th>
											<th scope="col">
												录入时间
											</th>
											<th scope="col">
												是否有效
											</th>
											<th scope="col">
												备注
											</th>
										</tr>
									</table>
								</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
											<asp:CheckBox ID="cbAllBox" runat="server" />
										</HeaderTemplate><ItemTemplate>
											<asp:CheckBox ID="cbBox" ToolTip='<%# System.Convert.ToString(Eval("TypeID"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
											<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
										</ItemTemplate></asp:TemplateField><asp:BoundField DataField="TypeCode" HeaderText="类型编码" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="类型名称" HeaderStyle-Width="200px"><ItemTemplate>
											<span class="tooltip link" title='<%# Eval("TypeName") %>' onclick="CheckType('<%# Eval("TypeID") %>')">
												<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
											</span>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="权限人员" HeaderStyle-Width="200px"><ItemTemplate>
											<span class="tooltip" title='<%# WebUtil.GetUserNames(Eval("UserCodes").ToString()) %>'>
												<%# StringUtility.GetStr(WebUtil.GetUserNames(Eval("UserCodes").ToString())) %>
											</span>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="成本科目" HeaderStyle-Width="100px"><ItemTemplate>
											<%# ContractType.GetCBSCodeName(Eval("CBSCode").ToString()) %></ItemTemplate></asp:TemplateField><asp:BoundField DataField="InputPerson" HeaderText="录入人" HeaderStyle-Width="80px" /><asp:TemplateField HeaderText="录入时间" HeaderStyle-Width="80px"><ItemTemplate>
											<%# Common2.GetTime(Eval("InputDate").ToString()) %>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="是否有效" HeaderStyle-Width="200px"><ItemTemplate>
											<%# Eval("IsValid") %>
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
											<span class="tooltip" title='<%# Eval("Notes").ToString() %>'>
												<%# StringUtility.GetStr(Eval("Notes").ToString()) %>
											</span>
										</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
							</webdiyer:AspNetPager>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<input type="button" id="btn" value="Button" style="display: none" onclick="Test()" />
	<asp:HiddenField ID="hfldSelectedPrj" runat="server" />
	<asp:HiddenField ID="hfldContractTypeGuid" runat="server" />
	<asp:HiddenField ID="hfContractTypeId" runat="server" />
	<asp:Button ID="btnDataBind" Text="Button" OnClick="btnDataBind_Click" runat="server" />
	</form>
</body>
</html>
