<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceEdit.aspx.cs" Inherits="BudgetManage_Resource_ResourceEdit" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资源维护</title><link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/jquery.easyui.extension.css" />

	
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnSave')[0].onclick = function () { if (!$('#form1').form('validate')) return false; }
			if (getRequestParam('t') == '1') {
				setAllInputDisabled();
			}
			//			Val.validate('form1');


		});

		function pickCorp() {
			jw.selectOneCorp({ idinput: 'hfldSupplier', nameinput: 'txtSupplier' });
}
	</script>
	<style type="text/css">
		.descTds
		{
			text-align: right;
			width: 100px;
		}
		.elemTds
		{
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" style="" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="descTds">
					资源编号
				</td>
				<td class="elemTds">
					<asp:TextBox ID="txtResourceCode" Height="15px" Width="100%" CssClass="easyui-validatebox" data-options="required: true, validType:'validChars[30]'" runat="server"></asp:TextBox>
				</td>
				<td class="descTds">
					资源名称
				</td>
				<td class="elemTds txt mustInput">
					<asp:TextBox ID="txtResourceName" Height="15px" Width="100%" CssClass="easyui-validatebox" data-options="required: true, validType: 'validChars[30]'" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="descTds">
					品牌
				</td>
				<td class="elemTd">
					<asp:TextBox ID="txtBrand" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="descTds">
					税率
				</td>
				<td class="elemTd">
					<asp:TextBox ID="txtTaxRate" CssClass="{number:true, messages:{number:'税率格式不正确'}}" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="descTds">
					规格
				</td>
				<td class="elemTd">
					<asp:TextBox ID="txtSpecification" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="descTds">
					型号
				</td>
				<td class="elemTds">
					<asp:TextBox ID="txtModelNumber" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="descTds">
					系列
				</td>
				<td class="elemTds">
					<asp:TextBox ID="txtSeries" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
				<td class="descTds">
					技术参数
				</td>
				<td class="elemTd">
					<asp:TextBox ID="txtTechnicalParameter" Height="15px" Width="100%" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="descTds">
					资源单位
				</td>
				<td class="elemTds">
					<asp:DropDownList Width="210px" CssClass="{required:true, messages:{required:'资源单位不能为空'}}" DataValueField="unitid" DataTextField="name" ID="ddlUnit" runat="server"></asp:DropDownList>
				</td>
				<td class="descTds">
					资源属性
				</td>
				<td class="elemTds">
					<asp:DropDownList Width="210px" DataValueField="AttributeId" DataTextField="AttributeName" ID="ddlAttribute" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="descTds">
					图片
				</td>
				<td colspan="3">
					<MyUserControl:usercontrol_fileupload_fileupload_ascx ID="FileUpload1" Name="图片" FileType="*.jpg;*.gif;*.png" Class="ResourceImg" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="descTds">
					供应商
				</td>
				<td class="elemTds" style="padding-right: 3px;">
					<span class="spanSelect" style="width: 210px">
						<asp:TextBox ID="txtSupplier" Style="width: 180px; height: 15px; border: none; line-height: 16px;
							margin: 1px 2px;" ReadOnly="true" runat="server"></asp:TextBox>
						<img alt="选择乙方" onclick="pickCorp();" src="../../images/icon.bmp" style="float: right;" />
					</span>
					<asp:HiddenField ID="hfldSupplier" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="descTds">
					备注
				</td>
				<td colspan="3" class="elemTds">
					<asp:TextBox ID="txtNote" Height="50px" Width="100%" Columns="3" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4" align="center" style="padding-left: 35px; text-align: center;">
					<hr class="sp" />
					<div class="pagediv" style="height: 220px; width: 730px; text-align: center; border: solid 0px blue;">
						<asp:GridView ID="gvwPriceType" CssClass="gvdata" Width="530px" AutoGenerateColumns="false" OnRowDataBound="gvwPriceType_RowDataBound" DataKeyNames="PriceTypeId" runat="server">
<EmptyDataTemplate>
								<table id="emptyContractType" class="gvdata">
									<tr class="header">
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col" style="width: 300px">
											类型名称
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
										<%# Container.DataItemIndex + (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize + 1 %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="类型名称" HeaderStyle-Width="300px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
										<%# StringUtility.GetStr(Eval("PriceTypeName").ToString(), 35) %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="价格"><ItemTemplate>
										<asp:TextBox ID="txtPrice" CssClass="decimal_input {number:true, messages:{number:'价格格式不正确'}}" ToolTip='<%# System.Convert.ToString(Eval("PriceTypeId"), System.Globalization.CultureInfo.CurrentCulture) %>' Text='<%# System.Convert.ToString(base.GetPrice(Eval("PriceTypeId").ToString()), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
						<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" Visible="false" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Right" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
						</webdiyer:AspNetPager>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="top.ui.closeTab()" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hdGuid" runat="server" />
	</form>
</body>
</html>
