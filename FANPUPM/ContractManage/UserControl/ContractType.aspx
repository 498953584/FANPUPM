<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractType.aspx.cs" Inherits="ContractManage_UserControl_SelectContractType" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title><link type="text/css" rel="Stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var table = new JustWinTable('gvwContractType');
			registerConTypeClickListener(table);        //注册单击事件
			registerConTypeDbClickListener(table);      //注册双击事件
			replaceEmptyTable('emptyContractType', 'gvwContractType');
			jw.tooltip();
			$('#btnSave').click(function () {
				saveEvent();
			});

		});

		function saveEvent() {
			if (typeof top.ui.callback == 'function') {
				top.ui.callback({ id: $('#hfldTypeId').val(), name: name = $('#hfldTypeName').val() });
			}
			top.ui.closeWin();
		}

		//注册单击合同类型的事件
		function registerConTypeClickListener(jwTable) {
			jwTable.registClickTrListener(function () {
				var id = this.id;
				var name = $(this).attr('name')
				setHd(id, name);
			});
		}

		//注册双击合同类型的事件
		function registerConTypeDbClickListener(jwTable) {
			jwTable.registDbClickListener(function () {
				saveEvent();
			});
		}

		function setHd(TypeId, TypeName) {
			document.getElementById("hfldTypeId").value = TypeId;
			document.getElementById("hfldTypeName").value = TypeName;
			document.getElementById("btnSave").disabled = false;
		}

		function save(TypeId, TypeName) {
			document.getElementById("hfldTypeId").value = TypeId;
			document.getElementById("hfldTypeName").value = TypeName;
			if (!getRequestParam('TypeIdControl')) return false;
			if (!getRequestParam('TypeNameControl')) return false;
			$(parent.document).find('#' + getRequestParam('TypeIdControl')).val($('#hfldTypeId').val());
			$(parent.document).find('#' + getRequestParam('TypeNameControl')).val($('#hfldTypeName').val());
			$(parent.document).find('.ui-icon-closethick').each(function () {
				this.click();
			});
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="pagediv">
		<table class="tab" style="height: 85%;">
			<tr>
				<td style="text-align: left; border: solid 0px red;">
					类型编号：<asp:TextBox ID="txtTypeCode" Width="120px" runat="server"></asp:TextBox>&nbsp;
					类型名称：<asp:TextBox ID="txtTypeName" Width="120px" runat="server"></asp:TextBox>&nbsp;
					<asp:Button ID="SearchBt" Text="查 询" OnClick="SearchBt_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td valign="top" align="center" style="border: solid 0px red; height: 100%; padding-top: 10px;">
					<div id="pagediv" style="width: 100%; border: solid 0px red; text-align: left;" align="center">
						<asp:GridView ID="gvwContractType" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvwContractType_RowDataBound" DataKeyNames="TypeID,TypeName" runat="server">
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
											录入人
										</th>
										<th scope="col">
											录入时间
										</th>
										<th scope="col">
											备注
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center"><ItemTemplate>
										<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
									</ItemTemplate></asp:TemplateField><asp:BoundField DataField="TypeCode" HeaderText="类型编码" HeaderStyle-Width="100px" /><asp:TemplateField HeaderText="类型名称" HeaderStyle-Width="200px"><ItemTemplate>
										<span class="tooltip" flag="typeName" title='<%# Eval("TypeName") %>'>
											<%# StringUtility.GetStr(Eval("TypeName").ToString()) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:BoundField DataField="InputPerson" HeaderText="录入人" HeaderStyle-Width="80px" /><asp:TemplateField HeaderText="录入时间" HeaderStyle-Width="80px"><ItemTemplate>
										<%# Common2.GetTime(Eval("InputDate").ToString()) %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("Notes") %>'>
											<%# StringUtility.GetStr(Eval("Notes").ToString()) %>
										</span>
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
						</webdiyer:AspNetPager>
					</div>
				</td>
			</tr>
		</table>
		<div style="text-align: right">
			<input id="btnSave" type="button" class="button-normal" value="确 定" disabled="disabled" />
			<input id="btnCancel" type="button" value="取 消" class="button-normal" onclick="top.ui.closeWin();" />
		</div>
		<asp:HiddenField ID="hfldTypeName" runat="server" />
		<asp:HiddenField ID="hfldTypeId" runat="server" />
	</div>
	</form>
</body>
</html>
