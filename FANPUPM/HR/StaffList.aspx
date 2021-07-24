<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StaffList.aspx.cs" Inherits="HR_StaffList" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="Wuqi.Webdiyer"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>部门</title>
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			replaceEmptyTable("emptyTable", "gvEmployee");
		});
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="tab" border="1" style="border-collapse: collapse;" cellpadding="0"
		cellspacing="0">
		<tr style="height: 100%;">
			<td style="width: 200px; height: 99%" valign="top">
				<div class="pagediv" style="heigth: 100%; width: 195px;">
					<div id="div_project">
						<asp:TreeView ID="tvDepartment" Font-Size="12px" ShowLines="true" OnSelectedNodeChanged="tvDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
					</div>
				</div>
			</td>
			<td valign="top" style="height: 99%; padding-top: 0; padding-left: 0; padding-bottom: 0;
				margin-top: 0; margin-left: 0; margin-bottom: 0; margin: 0">
				<div class="pagediv" style="height: 100%;">
					<table border="0" cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td>
								<table class="queryTable" cellpadding="3px" cellspacing="0px">
									<tr>
										<td class="descTd">
											员工编号
										</td>
										<td>
											<asp:TextBox ID="txtCode" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
										</td>
										<td class="descTd">
											员工姓名
										</td>
										<td>
											<asp:TextBox ID="txtName" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
										</td>
										<td class="descTd">
											岗位
										</td>
										<td>
											<asp:TextBox ID="txtPost" Width="120px" CssClass="easyui-validatebox" data-options="validType:'validQueryChars[50]'" runat="server"></asp:TextBox>
										</td>
										<td class="descTd">
											人员类型
										</td>
										<td>
											<asp:DropDownList ID="ddlState" Width="120px" runat="server"><asp:ListItem Value="" /><asp:ListItem Value="1" Text="在职" /><asp:ListItem Value="2" Text="离职" /></asp:DropDownList>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table border="0" class="tab">
									<tr>
										<td class="divFooter" style="text-align: left">
											<asp:Button ID="Button1" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
											<asp:Button ID="Button2" CssClass="button-normal" Text="导 出" OnClick="btnExp_Click1" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="width: 100%;" valign="top">
											<div class="pagediv">
												<asp:GridView ID="gvEmployee" AutoGenerateColumns="false" CssClass="gvdata" ShowFooter="false" OnRowDataBound="gvEmployee_RowDataBound" DataKeyNames="v_yhdm" runat="server">
<EmptyDataTemplate>
														<table id="emptyTable">
															<tr class="header">
																<th scope="col">
																	序号
																</th>
																<th scope="col">
																	岗位
																</th>
																<th scope="col">
																	员工编号
																</th>
																<th scope="col">
																	员工姓名
																</th>
																<th scope="col">
																	入司日期
																</th>
																<th scope="col">
																	离职日期
																</th>
																<th scope="col">
																	在职天数
																</th>
																<th scope="col">
																	联系电话
																</th>
																<th scope="col">
																	备注
																</th>
															</tr>
														</table>
													</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
																<%# Eval("Num") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工编号" HeaderStyle-Width="50px">
<ItemTemplate>
																<%# Eval("UserCode") %>
															</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="员工姓名" HeaderStyle-Width="50px"><ItemTemplate>
																<%# Eval("v_xm") %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="岗位" HeaderStyle-Width="100px"><ItemTemplate>
																<%# Eval("RoleTypeName") %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="入司日期" HeaderStyle-Width="80px"><ItemTemplate>
																<%# Common2.GetTime(Eval("EnterCorpDate")) %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="离职日期" HeaderStyle-Width="80px"><ItemTemplate>
																<%# Common2.GetTime(Eval("LeaveDate")) %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="在职天数" HeaderStyle-Width="80px"><ItemTemplate>
																<%# Eval("Workingdates") %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="状态" HeaderStyle-Width="80px"><ItemTemplate>
																<%# Eval("StateName") %>
															</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="联系电话" HeaderStyle-Width="80px"><ItemTemplate>
																<%# Eval("MobilePhoneCode") %>
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
					<asp:HiddenField ID="hfldEmployee" runat="server" />
				</div>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
