<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileInfoLook.aspx.cs" Inherits="FileInfoManager_FileInfoList" %>
<%@ Import Namespace="com.jwsoft.pm.entpm"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资料管理</title>
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../Script/jquery.tooltip/jquery.tooltip.js"></script>
	<link type="text/css" rel="stylesheet" href="../../Script/jquery.tooltip/jquery.tooltip.css" />

	<script type="text/javascript">

		$(document).ready(function () {
			var aa = new JustWinTable('gvFile');
			showTooltip('tooltip', 25);
		});
		function ondbSelectValue(id) {
			document.getElementById("hdSeleValue").value = id;
			location = 'FileInfoList.aspx?id=' + id;
		}
		//下载
		function DownLoad(path) {
			viewopen('/Common/DownLoad.aspx?path=' + path);
		}
		//显示隐藏高级选项
		function dis(num) {
			document.getElementById("hdSearchShow").value = num;
			if (num == '1') {
				document.getElementById("searchTr").style.display = 'block';
				document.getElementById("heightS").style.display = 'none';
				document.getElementById("btnS").style.display = 'none';
			}
			else {
				document.getElementById("searchTr").style.display = 'none';
				document.getElementById("heightS").style.display = 'block';
				document.getElementById("btnS").style.display = 'block';
				document.getElementById("txtStartTime").value = "";
				document.getElementById("txtEndTime").value = "";
				document.getElementById("txtStartSize").value = "";
				document.getElementById("txtEndSize").value = "";
				document.getElementById("txtFileOwner").value = "";
			}
		}
		//验证大小是否是数字
		function chkInput() {
			if (document.getElementById("txtStartSize").value != "" && isNaN(document.getElementById("txtStartSize").value)) {
				alert("按大小查询时输入的必须为数字");
				return false;
			}
			if (document.getElementById("txtStartSize").value != "" && isNaN(document.getElementById("txtStartSize").value)) {
				alert("按大小查询时输入的必须为数字");
				return false;
			}
			return true;
		}
		//刷新本页面
		function reLoadPage() {
			window.location = "FileInfoList.aspx?id=" + document.getElementById("hdSeleValue").value;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table width="100%" style="height: 100%;" cellpadding="0" cellspacing="0">
		<tr style="height: 20px; display: none;">
			<td class="divHeader">
				资料管理
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
						<td valign="top" style="height: 100%;">
							<table style="width: 100%;">
								<tr>
									<td style="width: 100%; vertical-align: top;">
										<table class="queryTable" cellpadding="3px" cellspacing="0px">
											<tr>
												<td class="descTd" style="width: 48px;">
													文件名
												</td>
												<td>
													<asp:TextBox ID="txtFileName" Width="270px" runat="server"></asp:TextBox>
												</td>
												<td class="descTd">
													范围
												</td>
												<td>
													<asp:DropDownList DataTextField="fileName" Width="120px" DataValueField="id" ID="ddlScope" runat="server"></asp:DropDownList>
												</td>
												<td>
													<asp:Button ID="btnS" Text="检索" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
												</td>
												<td>
													<input type="button" id="heightS" onclick="dis('1')" value="高级" runat="server" />

												</td>
											</tr>
											<tr id="searchTr" style="display: none;" runat="server"><td colspan="6" runat="server">
													<table>
														<tr style="height: 25px;">
															<td class="descTd">
																按时间:从
															</td>
															<td>
																<JWControl:DateBox ID="txtStartTime" Height="15px" Width="80px" runat="server"></JWControl:DateBox>
																&nbsp;至&nbsp;<JWControl:DateBox ID="txtEndTime" Height="15px" Width="80px" runat="server"></JWControl:DateBox>
															</td>
														</tr>
														<tr style="height: 25px;">
															<td class="descTd">
																按大小:从
															</td>
															<td>
																<asp:TextBox ID="txtStartSize" Width="80px" runat="server"></asp:TextBox>
																&nbsp;至&nbsp;<asp:TextBox ID="txtEndSize" Width="80px" runat="server"></asp:TextBox>
															</td>
														</tr>
														<tr style="height: 25px;">
															<td class="descTd">
																按创建者:
															</td>
															<td>
																<asp:TextBox ID="txtFileOwner" Width="185px" runat="server"></asp:TextBox>
															</td>
														</tr>
														<tr style="height: 25px;">
															<td>
																<asp:Button ID="btn_Search" Text="检索" Style="cursor: pointer;" OnClientClick="return chkInput()" OnClick="btn_Search_Click" runat="server" />
															</td>
															<td>
																<input type="button" onclick="dis('0')" style="width: 120px" value="隐藏高级选项" />
															</td>
														</tr>
													</table>
												</td></tr>
										</table>
									</td>
								</tr>
								<tr>
									<td class="divFooter" valign="top" style="height: 20px;">
									</td>
								</tr>
								<tr>
									<td style="height: 81.5%;">
										<div style="height: 100%; width: 1040px;">
											<asp:GridView CssClass="gvdata" ID="gvFile" AutoGenerateColumns="false" OnRowDataBound="gvFile_RowDataBound" DataKeyNames="id,fileName,FileOwner,FileType" runat="server"><Columns><asp:TemplateField HeaderStyle-Width="20px"><HeaderTemplate>
															<asp:CheckBox ID="cbAllBox" runat="server" />
														</HeaderTemplate><ItemTemplate>
															<asp:CheckBox ID="cbBox" runat="server" />
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px"><ItemTemplate>
															<%# Eval("No") %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="250px"><HeaderTemplate>
															文件名称
														</HeaderTemplate><ItemTemplate>
															<%# GetFileName(Eval("FileName").ToString(), Eval("FileType").ToString(), Eval("FileNewName").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px"><HeaderTemplate>
															在线预览
														</HeaderTemplate><ItemTemplate>
															<%# GetLookFile(Eval("FileName").ToString(), Eval("FileType").ToString(), Eval("FileNewName").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px"><HeaderTemplate>
															大小
														</HeaderTemplate><ItemTemplate>
															<%# WebUtil.ConvertSize(Eval("FileSize").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px"><HeaderTemplate>
															创建者
														</HeaderTemplate><ItemTemplate>
															<%# PageHelper.QueryUser(this, Eval("FileOwner").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px"><HeaderTemplate>
															文件类型
														</HeaderTemplate><ItemTemplate>
															<%# GetFileType(Eval("FileName").ToString(), Eval("FileType").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px"><HeaderTemplate>
															创建时间
														</HeaderTemplate><ItemTemplate>
															<%# Eval("CreateTime") %>
														</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="属性" HeaderStyle-Width="40px"><ItemTemplate>
														</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
											<asp:Label ID="lblMsgAleave" Text="" runat="server"></asp:Label>
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
	<asp:HiddenField ID="hdGuid" runat="server" />
	<asp:HiddenField ID="hdUserCodes" runat="server" />
	<asp:HiddenField ID="hdSeleValue" runat="server" />
	<asp:HiddenField ID="hdUser" runat="server" />
	<asp:Button ID="btnRe" Style="display: none;" runat="server" />
	<asp:HiddenField ID="hdType" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hdFormat" runat="server" />
	<asp:HiddenField ID="hdFilType" runat="server" />
	<asp:HiddenField ID="hdPath" runat="server" />
	<asp:HiddenField ID="hdSearchShow" Value="0" runat="server" />
	<asp:Button ID="btnDown" Style="display: none;" OnClick="btnDown_Click" runat="server" />
	</form>
</body>
</html>
