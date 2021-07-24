<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="OA2_Inbox" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>收件箱</title><link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/default/easyui.css" />
<link rel="Stylesheet" type="text/css" href="/Script/jquery.easyui/themes/icon.css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
	<script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			var gvEmail = new JustWinTable('gvEmail');
			replaceEmptyTable('gvEmailEmpty', 'gvEmail');
			setButton(gvEmail, 'btnDelete', '', '', 'hfldEmailID');
			displayLookAdjuncts();
			showTooltip('tooltip', 50);

			//按钮的点击后的样式
			function funonclickCss(name) {
				parent.document.getElementById(name).style.background = 'url(/images1/email/over.gif) repeat-x';
				parent.document.getElementById(name).style.fontWeight = 'bold';
				parent.document.getElementById(name).style.color = '#004592';
			}

			//按钮的点击前的样式
			function funfrontCss(name) {
				parent.document.getElementById(name).style.background = 'url(/images1/email/understood.gif) repeat-x';
				parent.document.getElementById(name).style.fontWeight = 'normal';
				parent.document.getElementById(name).style.color = '#333';
			}

			funfrontCss('spanwrite_mail');
			funfrontCss('write_mail');
			funonclickCss('spaninbox');
			funonclickCss('inbox');
			funfrontCss('spandraft');
			funfrontCss('draft');
			funfrontCss('spanrepeal');
			funfrontCss('repeal');
			funfrontCss('spanoutbox');
			funfrontCss('outbox');
			funfrontCss('spandeleted');
			funfrontCss('deleted');

			var btnMove = document.getElementById('btnMove');
			gvEmail.registClickTrListener(function () {
				document.getElementById('hfldCheckedIds').value = this.id;
				if (this.id != '') {
					btnMove.removeAttribute('disabled');
				}
				else {
					btnMove.setAttribute('disabled', 'disabled');
				}
			});

			gvEmail.registClickSingleCHKListener(function () {
				var checkedChk = gvEmail.getCheckedChk();
				if (checkedChk.length == 1) {
					var tr1 = getFirstParentElement(checkedChk[0].parentNode, 'TR');
					btnMove.removeAttribute('disabled');
					document.getElementById('hfldCheckedIds').value = tr1.id;
				}
				else if (checkedChk.length > 1) {
					var ids = "";
					for (var i = 0; i < checkedChk.length; i++) {
						var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						ids += trs.id + ",";
					}
					document.getElementById('hfldCheckedIds').value = ids;
					btnMove.removeAttribute('disabled');
				}
				else {
					btnMove.setAttribute('disabled', 'disabled');
				}
			});

			gvEmail.registClickAllCHKLitener(function () {
				if (gvEmail.isCheckedAll()) {
					btnMove.removeAttribute('disabled');
					var checkedChk = gvEmail.getAllChk();
					var ids = "";
					for (var i = 0; i < checkedChk.length; i++) {
						var trs = getFirstParentElement(checkedChk[i].parentNode, 'TR');
						ids += trs.id + ",";
					}
					document.getElementById('hfldCheckedIds').value = ids;
					btnMove.removeAttribute('disabled');
				}
				else {
					btnMove.setAttribute('disabled', 'disabled');
				}
			});
		})
		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + '/' + id;
			showFiles(path, 'divOpenAdjunct');
		}
		//附件显示
		function displayLookAdjuncts() {
			$('#gvEmail TR').each(function (i) {
				var id = $(this).attr('AnnexId');
				var imgLink = "<SPAN class=link onclick=\"queryAdjunct('" + id + "')\"><IMG style='BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none; CURSOR: pointer' src='/images1/icon_att0b3dfa.gif'> </SPAN>";
				var path = $('#hfldAdjunctPath').val() + '/' + id;
				var showCount = getFilesCount(path);
				if (showCount == 0)
					$(this).find('TD').eq(3).html('');
				else
					$(this).find('TD').eq(3).html(imgLink);

			});
		}
		function getFilesCount(path) {
			var count = 0;
			$.ajax({
				type: "GET",
				url: "/UserControl/FileUpload/GetFiles.ashx?" + new Date().getTime() + '&Path=' + path,
				async: false,
				dataType: "JSON",
				success: function (data) {
					count = data.length;
				}
			});
			return count;
		}

		// 查看邮件内容
		function btnQueryClick(MailId, AnnexId) {
			parent.parent.desktop.ViewMail = window;
			var url = '/OA2/Mail/ViewMail.aspx?mailId=' + MailId + "&reply=1";
			this.location.href = url;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table style="vertical-align: top; width: 99.99%; height: 100%;">
			<tr>
				<td style="vertical-align: top;">
					<table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
						<tr>
							<td align="left">
								<table cellpadding="5" cellspacing="5">
									<tr>
										<td class="descTd">
											主题
										</td>
										<td>
											<asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
										</td>
										<td class="descTd">
											内容
										</td>
										<td>
											<asp:TextBox ID="txtContent" Width="120px" runat="server"></asp:TextBox>
										</td>
										<td class="descTd">
											发件人
										</td>
										<td>
											<span class="spanSelect" style="width: 130px;">
												<asp:TextBox ID="txtFrom" Style="width: 97px; height: 15px; border: none;
													line-height: 16px; margin: 1px 2px" runat="server"></asp:TextBox>
												<img src="../../images/icon.bmp" style="float: right;" alt="选择" id="imgName" onclick="jw.selectOneUser({ nameinput: 'txtFrom' });" runat="server" />

											</span>
											<input id="hfldName" type="hidden" style="width: 1px" runat="server" />

										</td>
									</tr>
									<tr>
										<td class="descTd">
											日期
										</td>
										<td>
											<asp:TextBox ID="txtStartTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
										</td>
										<td class="descTd">
											至
										</td>
										<td>
											<asp:TextBox ID="txtEndTime" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
										</td>
										<td colspan="2">
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td id="td_buttons" class="divFooter" style="text-align: left;">
								<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
								<asp:Button ID="btnDelete" Text="删 除" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
								<asp:Button ID="btnMove" Text="转移到" disabled="disabled" OnClick="btnMove_Click" runat="server" />
								<asp:DropDownList ID="DDLtBox" runat="server"></asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td class="divFooter" style="text-align: left;">
								<asp:Label ID="LabMail" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td align="center">
								<asp:GridView ID="gvEmail" CssClass="gvdata" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvEmail_RowDataBound" DataKeyNames="MailId,IsReaded,AnnexId,MailName" runat="server">
<EmptyDataTemplate>
										<table class="gvdata" cellspacing="0" rules="all" border="1" id="gvEmailEmpty" style="width: 100%;
											border-collapse: collapse;">
											<tr class="header">
												<th scope="col" style="width: 20px;">
													<input id="gvEmail_ctl01_chkSelectAll" type="checkbox" />
												</th>
												<th scope="col" style="width: 100px;">
													发件人
												</th>
												<th scope="col">
													主题
												</th>
												<th scope="col" style="width: 25px;">
													附件
												</th>
												<td scope="col" visible="false" style="width: 80px;" runat="server">
													状态
												</td>
												<th scope="col" style="width: 100px;">
													时间
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"><HeaderTemplate>
												<asp:CheckBox ID="chkSelectAll" runat="server" />
											</HeaderTemplate><ItemTemplate>
												<asp:CheckBox ID="chkSelectSingle" runat="server" />
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="发件人" HeaderStyle-Width="100px"><ItemTemplate>
												<%# Eval("MailFromYhmc.v_xm").ToString() %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="主题" ItemStyle-HorizontalAlign="Left"><ItemTemplate>
												<span class="link tool" title='<%# Eval("MailName").ToString() %>' onclick="btnQueryClick('<%# Eval("MailId") %>','<%# Eval("AnnexId") %>')">
													<asp:Label ID="lbName" Width="100%" Text='<%# System.Convert.ToString(string.IsNullOrEmpty(Eval("MailName").ToString()) ? "【无主题】" : StringUtility.GetStr(Eval("MailName").ToString(), 50), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
												</span>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" HeaderStyle-Width="25px"><ItemTemplate>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="状态" HeaderStyle-Width="80px" Visible="false"><ItemTemplate>
												<%# Eval("IsReaded").ToString() %>
											</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="时间" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
												<%# Eval("InputDate") %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
								<webdiyer:AspNetPager ID="AspNetPager1" Width="100%" UrlPaging="false" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="转到: " FirstPageText="首页" LastPageText="末页" PrevPageText="上一页" NextPageText="下一页" HorizontalAlign="Left" EnableTheming="true" OnPageChanged="AspNetPager1_PageChanged" runat="server">
								</webdiyer:AspNetPager>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<asp:HiddenField ID="hfldCheckedIds" runat="server" />
	<asp:HiddenField ID="hfldEmailID" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	<asp:HiddenField ID="hfldAnnexId" runat="server" />
	</form>
</body>
</html>
