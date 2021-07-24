<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Business_Data_Main.aspx.cs" Inherits="OPM_Business_Data_Business_Data_Main" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_fileupload_fileupload_ascx" Src="~/UserControl/FileUpload/FileUpload.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>图纸管理</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('gvBusiness_Data');
			replaceEmptyTable('emptyBusiness_Data', 'gvBusiness_Data');
		});
		function ClickRow(Fid) {
			if (Fid != "") {
				$("#hfldUID").val(Fid);
			}
			$("#iframe1").attr("src", changeURLPar($("#iframe1").attr("src"), "uid", Fid));
		}
		//修改url参数值
		function changeURLPar(destiny, par, par_value) {
			var pattern = par + '=([^&]*)';
			var replaceText = par + '=' + par_value;

			if (destiny.match(pattern)) {
				var tmp = '/' + par + '=[^&]*/';
				tmp = destiny.replace(eval(tmp), replaceText);
				return (tmp);
			}
			else {
				if (destiny.match('[\?]')) {
					return destiny + '&' + replaceText;
				}
				else {
					return destiny + '?' + replaceText;
				}
			}

			return destiny + '\n' + par + '\n' + par_value;
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table width="100%" height="50%" cellpadding="0" cellspacing="0">
		<tr style="height: 20px; display: none;">
			<td class="divHeader">
				<asp:Label ID="lblTitle" Text="Label" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td valign="top" style="height: 100%;">
				<table style="width: 100%;">
					<tr id="query" runat="server"><td style="width: 100%; vertical-align: top;" runat="server">
							<table cellpadding="3px" cellspacing="0px">
								<tr>
									<td class="descTd" style="display: none;">
										项目
									</td>
									<td style="display: none;">
										<asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										责任人
									</td>
									<td>
										<asp:TextBox ID="txtDutyUser" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										图纸计划编号
									</td>
									<td>
										<asp:TextBox ID="txtBCode" runat="server"></asp:TextBox>
									</td>
									<td class="descTd">
										计划完成时间
									</td>
									<td>
										<JWControl:DateBox ID="txtEndDate" Height="15px" Width="120px" runat="server"></JWControl:DateBox>
									</td>
									<td>
										<asp:Button ID="btnSearch" Text="查询" Style="cursor: pointer;" OnClick="btn_Search_Click" runat="server" />
									</td>
								</tr>
							</table>
						</td></tr>
					<tr id="trName" class="td-toolstbar" style="display: none" runat="server"><td runat="server">
							<table>
								<tr>
									<td style="font-weight: bold;">
										项目名称：
									</td>
									<td>
										<asp:Label ID="lblName1" Text="..." Style="font-weight: bold;" runat="server"></asp:Label>
									</td>
									<td style="width: 30px">
									</td>
									<td style="font-weight: bold;">
										阶段名称：
									</td>
									<td>
										<asp:Label ID="lblName2" Text="..." Style="font-weight: bold;" runat="server"></asp:Label>
									</td>
								</tr>
							</table>
						</td></tr>
					<tr>
						<td style="height: 100%;">
							<div style="height: 100%; width: 100%;">
								<asp:GridView CssClass="gvdata" ID="gvBusiness_Data" AllowPaging="true" PageSize="5" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvBusiness_Data_RowDataBound" OnPageIndexChanging="gvBusiness_Data_PageIndexChanging" DataKeyNames="uid" runat="server">
<EmptyDataTemplate>
										<table id="emptyBusiness_Data" class="gvdata" cellspacing="0" rules="all" border="1"
											style="width: 100%; border-collapse: collapse;">
											<tr class="header">
												<th scope="col" style="width: 20px;">
													<input id="chk1" type="checkbox" />
												</th>
												<th scope="col" style="width: 25px;">
													序号
												</th>
												<th scope="col">
													图纸计划编号
												</th>
												<th scope="col">
													图纸计划名称
												</th>
												<th scope="col">
													责任人
												</th>
												<th scope="col">
													设计时间
												</th>
												<th scope="col">
													计划完成时间
												</th>
												<th scope="col">
													流程状态
												</th>
												<th scope="col">
													备注
												</th>
												<th scope="col">
													附件
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:TemplateField>
<HeaderTemplate>
												<asp:CheckBox ID="cbAllBox" runat="server" />
											</HeaderTemplate>

<ItemTemplate>
												<asp:CheckBox ID="cbBox" runat="server" />
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" HeaderStyle-Width="25px">
<ItemTemplate>
												<%# Container.DataItemIndex + 1 %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="150px" HeaderText="图纸计划编号">
<ItemTemplate>
												<span class="link" onclick="toolbox_oncommand('/OPM/Business_Data/Business_Data_View.aspx?ic=<%# Eval("UID") %>', '图纸信息查看')">
													<%# Eval("BCode") %></span>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="图纸计划名称">
<ItemTemplate>
												<%# Eval("BName") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px" HeaderText="责任人">
<ItemTemplate>
												<%# Eval("DutyUser") %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="设计时间">
<ItemTemplate>
												<%# Common2.GetTime(Eval("BeginDate").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="计划完成时间">
<ItemTemplate>
												<%# Common2.GetTime(Eval("EndDate").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="流程状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
<ItemTemplate>
												<%# Common2.GetState(Eval("FlowState").ToString()) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="图审人" Visible="false" HeaderStyle-Width="70px">
<ItemTemplate>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注" HeaderStyle-Width="150px">
<ItemTemplate>
												<%# StringUtility.GetStr(Eval("Remark").ToString(), 15) %>
											</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px" HeaderText="附件"><ItemTemplate>
												<%# GetAnnx(Eval("UID").ToString()) %>
											</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 100%;">
				<div id="divFram" style="width: 100%; height: 90%" runat="server">
					<iframe id="iframe1" frameborder="0" width="100%" height="100%" src="<%=getSrc() %>">
					</iframe>
				</div>
			</td>
		</tr>
	</table>
	<input type="hidden" id="hdnType" runat="server" />

	<asp:HiddenField ID="hfldUID" runat="server" />
	<asp:HiddenField ID="hdUserCodes" runat="server" />
	<asp:HiddenField ID="hfldIDArr" runat="server" />
	<asp:HiddenField ID="hdUser" runat="server" />
	<asp:HiddenField ID="hdnCodeId" runat="server" />
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	<asp:HiddenField ID="hdnPrjCode" runat="server" />
	<asp:HiddenField ID="hdnChk" runat="server" />
	</form>
</body>
</html>
