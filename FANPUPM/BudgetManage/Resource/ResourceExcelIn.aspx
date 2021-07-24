<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceExcelIn.aspx.cs" Inherits="BudgetManage_Resource_ResourceExcelIn" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>导入Excel</title>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script src="../../Script/jquery.js" type="text/javascript"></script>
	<script src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {

			var resJWtable = new JustWinTable('gvResource');
			var jwtable = new JustWinTable('gvTemplateIn');
			if (getRequestParam('t') == '1') {
				setAllInputDisabled();
			}
			Val.validate('form1');
			tem();

			$('#btnCancel').click(closePage);
			//不能指定重复列
			$('#gvResource select').bind('change', function () {
				var $cSelect2 = $(this);
				var $cOption2 = $cSelect2.find('option:selected')
				$('#gvResource select').each(function () {
					if ($(this).attr('id') != $cSelect2.attr('id')) {
						if ($(this).find('option:selected').val() == $cOption2.val()) {
							if ($cOption2.val() != 'Invalid')
								$(this).find('option')[0].selected = true;
						}
					}
				});
				getSelectValue('gvResource select', 'hfldExcelColumns');
			});
			//Excel导入验证
			$('#btnInInfo').click(function () {
				return checkSelectItem();
			});
			//Excel模板导入验证
			$('#btnTemIn').click(function () {
				var templateCount = $('#ddlTemlpate option').length;
				if (templateCount == 0) {
					alert('系统提示：\n模板导入：当前不存在模板，无法使用模板导入！');
					return false;
				} else {
					return existGvResource();
				}
			});
		});
		function checkSelectItem() {
			var existGv = existGvResource();
			if (!existGv) return false;
			var columns = getSelectValue('gvResource select', 'hfldExcelColumns');
			if (columns.length <= $('#gvResource select').length) {
				alert('系统提示：\n普通导入：请选择要绑定的列！');
				return false;
			} else if (columns.indexOf('ResourceCode') == -1) {
				alert('系统提示：\n"普通导入：资源编号"必需选择！');
				return false;
			} else if (columns.indexOf('ResourceName') == -1) {
				alert('系统提示：\n"普通导入：资源名称"必需选择！');
				return false;
			}
			return true;
		}
		//是否存在资源信息
		function existGvResource() {
			var resourceCount = $('#gvResource tr:eq(0)').length;
			if (resourceCount == 0) {
				alert('系统提示：\n模板导入：请选择要导入的Excel！');
				return false;
			} else {
				return true;
			}
		}
		//获取select 值
		function getSelectValue(selectType, hfldId) {
			var columns = '';
			$('#' + selectType).each(function () {
				columns = columns + $(this).find('option:selected').val() + ',';
			});
			columns = columns.substring(0, columns.length - 1);
			$('#' + hfldId).val(columns);
			return columns;
		}
		//关闭页面
		function closePage() {
			var pid = getRequestParam("parentId");
			var nodeId = getRequestParam("parentId");
			var parentId = getRequestParam("nodeId");
			window.opener.location.href = window.opener.location = "ResourceDetail.aspx?id=" + pid + "&nodeId=" + nodeId + "&parentId=" + parentId;
			window.close();
		}
		//验证模板JS
		function chkTemlate() {
			var flag = checkSelectItem();
			if (!flag) return false;
			if (Trim(document.getElementById("txtTemplateName").value) == "") {
				alert("系统提示：\n请填写模板名称！");
				document.getElementById("txtTemplateName").focus();
				return false;
			}
			else {
				var exist;
				$.ajax({
					type: 'GET',
					async: false,
					url: "/BudgetManage/Handler/ExistResTemplate.ashx?templateName=" + $('#txtTemplateName').val(),
					success: function (data) {
						exist = data;
					}
				});
				if (exist == '1') {
					alert("系统提示：\n模板名称重复，请修改！");
					return false;
				}
			}
		}
		//设置模板选项卡
		function tem() {
			var num = $("#hdTem").val();
			//alert(num);
			if (num == "0") {
				//模板
				$("#SpTemplate").attr("class", "temShow");
				$("#SpNoTemplate").attr("class", "temHide");
				$("#SpErrorMsg").attr("class", "temHide");
				$("#DivErrorMsg").hide();
				$("#DivNoTemplate").hide();
				$("#DivTemplate").show();
			} else if (num == "1") {
				//普通            
				$("#SpTemplate").attr("class", "temHide");
				$("#DivTemplate").hide();
				$("#SpNoTemplate").attr("class", "temShow");
				$("#DivNoTemplate").show();
				$("#SpErrorMsg").attr("class", "temHide");
				$("#DivErrorMsg").hide();
			} else if (num == "2") {
				//错误列表
				$("#SpErrorMsg").attr("class", "temShow");
				$("#DivErrorMsg").show();
				$("#SpTemplate").attr("class", "temHide");
				$("#DivTemplate").hide();
				$("#SpNoTemplate").attr("class", "temHide");
				$("#DivNoTemplate").hide();
			}
		}
		//设置模板选项卡
		function setTem(num) {
			$("#hdTem").val(num);
			tem();
		}
	</script>
	<style type="text/css">
		#FileLink1_But_UpFile
		{
			width: auto;
		}
		#FileLink1_Btn_Upload
		{
			width: auto;
		}
		.temShow
		{
			height: 23px;
			width: 84px;
			border: 0px solid #aa0000;
			font-size: 12px;
			text-align: center;
			float: left;
			line-height: 25px;
			cursor: pointer;
			background-image: url('/images1/Res1.jpg');
			margin-left: 2px;
		}
		.temHide
		{
			height: 23px;
			width: 84px;
			border: 0px solid #aa0000;
			font-size: 12px;
			text-align: center;
			float: left;
			line-height: 25px;
			cursor: pointer;
			background-image: url('/images1/Res2.jpg');
			margin-left: 2px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="Excel导入数据" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent">
		<table width="98%" cellspacing="0">
			<tr>
				<td style="text-align: left;">
					<asp:FileUpload ID="fuExcel" Style="margin-left: 10px;" BackColor="#FEFEF4" Height="20px" Width="300px" runat="server" />
					<asp:Button ID="btnUp" Style="width: 80px; cursor: pointer;" Text="连接数据" OnClick="btnUp_Click" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="elemTd mustInput" style="text-align: left;">
					<span style="margin-left: 10px;"></span>请选择
					<asp:DropDownList Style="width: 120px; margin-left: 5px;" ID="ddlSheets" AutoPostBack="true" OnSelectedIndexChanged="ddlSheets_SelectedIndexChanged" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td>
					<div style="margin-left: 10px; height: 22px; line-height: 22px;">
						<span id="SpNoTemplate" onclick="setTem('1')" class="temShow" style="margin-left: 0px;" runat="server">
							普通导入</span> <span id="SpTemplate" onclick="setTem('0')" class="temHide" runat="server">
								模板导入 </span><span id="SpErrorMsg" onclick="setTem('2')" class="temHide" runat="server">
									错误列表 </span>
					</div>
				</td>
			</tr>
			<tr>
				<td>
					
					<div id="DivTemplate" style="height: 310px; display: none; border: solid 1px #d4d4d4;
						margin-left: 10px;">
						<div align="left" style="height: 20px;">
							&nbsp;模板:
							<asp:DropDownList AutoPostBack="true" ID="ddlTemlpate" OnSelectedIndexChanged="ddlTemlpate_SelectedIndexChanged" runat="server"></asp:DropDownList>
						</div>
						<div id="Div1" class="pagediv" align="center" style="background-color: #f6f9fe; height: 260px;
							border: solid 0px blue;">
							<asp:GridView ID="gvTemplateIn" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvTemplateIn_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
											<%# Container.DataItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="Excel列名"><ItemTemplate>
											<%# Eval("ExcelColumn") %>
											<asp:HiddenField ID="hfldExcelColumn" Value='<%# System.Convert.ToString(Eval("ExcelColumn"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数据库列名"><ItemTemplate>
											<%# GetColumnName(Eval("DbColumn").ToString()) %>
											<asp:HiddenField ID="hfldDbColumn" Value='<%# System.Convert.ToString(Eval("DbColumn"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
										</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							<div style="height: 25px; padding-top: 5px; text-align: right; padding-right: 5px;">
								<asp:Button ID="btnTemIn" Style="width: 80px; cursor: pointer;" Text="导入数据" OnClick="btnTemIn_Click" runat="server" />
							</div>
						</div>
					</div>
					
					<div id="DivNoTemplate" style="height: 310px; border: solid 1px #d4d4d4; margin-left: 10px;
						overflow: auto;">
						<div id="notem" class="pagediv" align="center" style="height: 280px; background-color: #f6f9fe;
							height: 280px; border: solid 0px blue;">
							<asp:GridView ID="gvResource" CssClass="gvdata" AutoGenerateColumns="false" OnRowDataBound="gvResource_RowDataBound" runat="server"><Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
											<%# Container.DataItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="Excel列名"><ItemTemplate>
											<%# Eval("ExcelColumn") %>
											<asp:HiddenField ID="hflddExcelColumn" Value='<%# System.Convert.ToString(Eval("ExcelColumn"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server" />
										</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="数据库列名"><ItemTemplate>
											<asp:DropDownList ID="ddlDBColmn" DataTextField="value" DataValueField="name" runat="server"></asp:DropDownList>
										</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							<div style="height: 25px; padding-top: 5px; text-align: right; padding-right: 5px;">
								模板名称
								<asp:TextBox ID="txtTemplateName" Text="" Width="120px" runat="server"></asp:TextBox>
								<asp:Button ID="btnSaveTemplate" Width="80px" OnClientClick="return chkTemlate()" Text="保存为模板" OnClick="btnSaveTemplate_Click" runat="server" />
								<asp:Button ID="btnInInfo" Style="width: 80px; cursor: pointer;" Text="导入数据" OnClick="btnInInfo_Click" runat="server" />
							</div>
						</div>
					</div>
					
					<div id="DivErrorMsg" style="height: 310px; display: none; border: solid 1px #d4d4d4;
						margin-left: 10px;">
						<div id="Div3" class="pagediv" align="center" style="height: 310px; background-color: #f6f9fe;
							border: solid 0px blue; text-align: left;">
							<asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
						</div>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter">
		<table class="tableFooter">
			<tr>
				<td>
					<input type="button" id="btnCancel" value="关闭" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hdGuid" runat="server" />
	<asp:HiddenField ID="hdTem" runat="server" />
	<asp:HiddenField ID="hfldExcelColumns" runat="server" />
	</form>
</body>
</html>
