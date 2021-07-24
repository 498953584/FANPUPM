<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentUser.aspx.cs" Inherits="Equ_Equipment_EquipmentUser" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>设备台账</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/default/easyui.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.easyui/themes/icon.css" />
<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="../../Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvEquipment');
			replaceEmptyTable('emptyEquipment', 'gvEquipment');
			setButton(jwTable, 'btnAllotUsers', '', '', 'hfldIdsChecked');
			jw.tooltip();
			$('#btnAllotUsers').click(allotUsers);
		});
		//打开新的标签页
		function allotUsers() {
			var url = '/Common/AllocUser.aspx?type=equipmentUser&tableName=Equ_EquipmentUser&idJson=' + $('#hfldIdsChecked').val();
			top.ui.openWin({ title: '选择人员', url: url });
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table border="0" cellpadding="0" cellspacing="0" style="border: 0px; width: 100%;
			height: 97%; vertical-align: top;">
			<tr style="height: 1px;">
				<td style="vertical-align: top;">
					<table class="queryTable" cellpadding="3px" cellspacing="0px">
						<tr>
							<td class="descTd">
								设备状态
							</td>
							<td>
								<asp:DropDownList ID="ddlState" Width="120px" runat="server"></asp:DropDownList>
							</td>
							<td class="descTd">
								设备编号
							</td>
							<td>
								<asp:TextBox ID="txtCode" Width="120px" runat="server"></asp:TextBox>
							</td>
							<td class="descTd">
								设备名称
							</td>
							<td>
								<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
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
					<input type="button" id="btnAllotUsers" value="分配人员" disabled="disabled" style="width: 60px;" />
				</td>
			</tr>
			<tr>
				<td style="width: 100%;">
					<table class="tab">
						<tr>
							<td style="height: 100%; vertical-align: top;">
								<div>
									<asp:GridView ID="gvEquipment" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvEquipment_RowDataBound" DataKeyNames="Id" runat="server">
<EmptyDataTemplate>
											<table id="emptyEquipment" class="gvdata">
												<tr class="header">
													<th scope="col" style="width: 20px;">
														<input id="chk1" type="checkbox" />
													</th>
													<th scope="col" style="width: 25px;">
														序号
													</th>
													<th scope="col">
														设备编号
													</th>
													<th scope="col">
														设备名称
													</th>
													<th scope="col">
														设备类别
													</th>
													<th scope="col">
														设备人员
													</th>
													<th scope="col">
														规格
													</th>
													<th scope="col">
														精度
													</th>
													<th scope="col">
														制造厂家
													</th>
													<th scope="col">
														设备状态
													</th>
													<th scope="col">
														备注
													</th>
												</tr>
											</table>
										</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
													<asp:CheckBox ID="chkAll" runat="server" />
												</HeaderTemplate><ItemTemplate>
													<asp:CheckBox ID="chk" runat="server" />
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
													
												</ItemTemplate></asp:TemplateField><asp:BoundField DataField="EquipmentCode" HeaderText="设备编号" /><asp:TemplateField HeaderText="设备名称"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备类别"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="人员"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="精度"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="制造厂家"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="设备状态" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
													
												</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
													<span class="tooltip" title=''>
														
													</span>
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
	<asp:HiddenField ID="hfldIdsChecked" runat="server" />
	</form>
</body>
</html>
