<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BorrowManageMain.aspx.cs" Inherits="oa_eFile_BorrowManageMain" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>员工查询借阅</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			var jwTable = new JustWinTable('GVeFileLend');
			setWfButtonState2(jwTable, 'WF1');
		});
		function ClickRow(recordid, sl, as, ls) {
			document.getElementById('HdnRecoreId').value = recordid;
			if (as == "1" && ls == "0" && sl == "1") {
				document.getElementById('BtnApply').disabled = false;

			}
			else {
				document.getElementById('BtnApply').disabled = true;
			}
			if (as == "-1" && ls == "0") {
				document.getElementById('BtnDel').disabled = false;
			}
			else {
				document.getElementById('BtnDel').disabled = true;
			}
			if (as == "1" && ls == "2" && sl == "1") {
				document.getElementById('BtnPlanReturn').disabled = false;

			}
			else {
				document.getElementById('BtnPlanReturn').disabled = true;
				//				document.getElementById('BtnView').disabled = true;
			}

			//			if (as == "-1") {
			//				document.getElementById('btnStartWF').disabled = false;
			//				document.getElementById('btnViewWF').disabled = true;
			//				document.getElementById('btnWFPrint').disabled = true;
			//			}
			//			else {
			//				document.getElementById('btnStartWF').disabled = true;
			//				document.getElementById('btnViewWF').disabled = false;
			//				document.getElementById('btnWFPrint').disabled = false;
			//			}
			document.getElementById('BtnView').disabled = false;
		}
		function openEdit(t, classid, recordtype) {

			var rid = document.getElementById('HdnRecoreId').value;
			var pj = document.getElementById('HdnPrjGuid').value;
			if (t == 'Add') {
				var url = 'ProjectFileManageEdit.aspx?t=' + t + '&cid=' + classid + '&rid=0&rt=' + recordtype + '&prj=' + pj;
			}
			else {
				var url = 'ProjectFileManageEdit.aspx?t=' + t + '&cid=' + classid + '&rid=' + rid + '&rt=' + recordtype + '&prj=' + pj;
			}
			return window.showModalDialog(url, window, "dialogHeight:360px;dialogWidth:650px;center:1;help:0;status:0;");
		}

		function openSelect(p) {
			var url = "BorroweFileSelectMainTree.aspx?p=" + p;
			// var url = "BorroweFileSelectAdd.aspx?p="+p
			// window.open(url,'w');
			// window.open(url ,",width=850,height=700;");
			return window.showModalDialog(url, window, "dialogHeight:660px;dialogWidth:850px;center:1;help:0;status:0;");
		}
		function OpenPlan() {
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "BorroweFileLendAdd.aspx?rid=" + rid;
			return window.showModalDialog(url, window, "dialogHeight:260px;dialogWidth:350px;center:1;help:0;status:0;");
		}
		///查看审核
		function OpenViewWF() {
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "/ModuleSet/Workflow/AuditViewWF.aspx?ic=" + rid;
			return window.showModalDialog(url, window, "dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");
		}
		//查看审核记录
		function OpenPrintWF() {
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "/ModuleSet/Workflow/AuditViewPrint.aspx?ic=" + rid;
			// window.open(url,"");
			return window.showModalDialog(url, window, "dialogHeight:760px;dialogWidth:800px;center:1;help:0;status:0;");
		}

		//查看
		function OpenLock() {
			var rid = document.getElementById('HdnRecoreId').value;
			var url = "eFileLock.aspx?ic=" + rid;
			return window.showModalDialog(url, window, "dialogHeight:260px;dialogWidth:600px;center:1;help:0;status:0;");
		}
		function download(url) {
			window.location.href = url;
		}
	</script>
</head>
<body class="body_clear">
	<form id="form1" runat="server">
	<div id="">
		<table id="Table2" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
			class="table-normal" style="height: 100%">
			<tr valign="top">
				<td>
					<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
					<asp:Menu ID="Menu1" Orientation="Horizontal" StaticEnableDefaultPopOutImage="false" OnMenuItemClick="Menu1_MenuItemClick" runat="server"><Items><asp:MenuItem Selected="true" Text="『个人借阅记录』" Value="0"></asp:MenuItem></Items></asp:Menu>
					<asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
						<asp:View ID="View1" runat="server">
							<table id="Table1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
								class="table-normal" style="height: 100%">
								<tr>
									<td class="td-title">
										档案借阅记录
										<asp:HiddenField ID="HdnRecoreId" runat="server" />
										<asp:HiddenField ID="HdnPrjGuid" Value="00000000-0000-0000-0000-000000000000" runat="server" />
									</td>
								</tr>
								<tr>
									<td class="td-toolsbar" style="height: 24px">
										<asp:Button ID="BtnPrject" Text="从项目档案申请" Width="100px" Visible="false" OnClientClick="openSelect(1);" OnClick="BtnPrject_Click" runat="server" />
										<asp:Button ID="BtnManager" Text="从管理档案申请" Width="100px" OnClientClick="openSelect(2);" OnClick="BtnManager_Click" runat="server" />
										<asp:Button ID="BtnApply" Text="提交申请" Width="70px" Enabled="false" OnClick="BtnApply_Click" runat="server" />
										<asp:Button ID="BtnPlanReturn" Text="归还档案" Width="70px" Enabled="false" OnClick="BtnPlanReturn_Click" runat="server" />
										<input type="button" value="查看" id="BtnView" onclick="OpenLock();" />
										<asp:Button ID="BtnDel" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" />
										
										<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiCode="010" BusiClass="001" runat="server" />
										
									</td>
								</tr>
								<tr>
									<td>
										<div id="dvDeptBox" class="div-scroll" style="width: 100%; height: 100%">
											<asp:GridView ID="GVeFileLend" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="SqleFileLend" CssClass="grid" Width="100%" PageSize="20" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
													<table class="grid" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse: collapse;">
														<tr class="grid_head">
															<th scope="col">
																&nbsp;
															</th>
															<th scope="col">
																档案编号
															</th>
															<th scope="col">
																档案名称
															</th>
															<th scope="col" style="width: 70px;">
																借阅时间
															</th>
															<th scope="col" style="width: 70px;">
																计划归还日期
															</th>
															<th scope="col">
																借阅次数
															</th>
															<th scope="col">
																密级
															</th>
															<th scope="col">
																流程状态
															</th>
															<th scope="col">
																借出状态
															</th>
															<th scope="col">
																归还类型
															</th>
															<th scope="col">
																备注
															</th>
														</tr>
													</table>
												</EmptyDataTemplate>
<RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundField DataField="FileCode" HeaderText="档案编号" SortExpression="FileCode" /><asp:BoundField DataField="FileTitle" HeaderText="档案名称" SortExpression="FileTitle" /><asp:BoundField DataField="LendDate" HeaderText="借阅时间" SortExpression="ReturnApplyDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" /><asp:BoundField DataField="PlanReturnDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="计划归还日期" HtmlEncode="false" SortExpression="PlanReturnDate" /><asp:BoundField HeaderText="借阅次数" ReadOnly="true" SortExpression="LendNumber" DataField="LendNumber" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" SortExpression="SecretLevel" /><asp:TemplateField HeaderText="流程状态"><ItemTemplate>
															<%# Common2.GetState(Eval("AuditState").ToString()) %>
														</ItemTemplate></asp:TemplateField><asp:BoundField DataField="LendState" HeaderText="借出状态" SortExpression="LendState" /><asp:BoundField HeaderText="归还类型" ReadOnly="true" SortExpression="RecordType" DataField="ReturnType" /><asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" /><asp:TemplateField HeaderText="查看"><ItemTemplate>
															<asp:HyperLink ID="HyperLink2" NavigateUrl='<%# Convert.ToString(Eval("FilePath")) %>' Text='<%# Convert.ToString(Eval("OriginalName")) %>' runat="server"></asp:HyperLink>
														</ItemTemplate></asp:TemplateField></Columns><PagerStyle HorizontalAlign="Right"></PagerStyle></asp:GridView>
											<asp:SqlDataSource ID="SqleFileLend" SelectCommand="SELECT [AuditState], [BorrowMan], [FileRecordID], [LendDate], [RecordID], [LendState], [ReturnApplyDate], [ReturnDate], [PlanReturnDate], [RecordType], [CorpCode], [PrjGuid], [FileTitle], [SubmitMan], [UserCode], [FileType], [FileCopy], [ClassID], [FileCode], [Remark], [SubmitDate], [SecretLevel], [SaveLimit], [RecordDate], [OriginalName], [FilePath],[ReturnType],[LendNumber] FROM [v_OA_eFileLend] WHERE ([BorrowMan] = @BorrowMan)" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:SessionParameter Name="BorrowMan" SessionField="yhdm" Type="String"></asp:SessionParameter></SelectParameters></asp:SqlDataSource>
										</div>
									</td>
								</tr>
							</table>
						</asp:View>
						<asp:View ID="View2" runat="server">
							<table id="Table3" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
								class="table-normal" style="height: 100%">
								<tr>
									<td class="td-title">
										领导授权查阅
									</td>
								</tr>
								<tr>
									<td>
										<div id="Div1" class="div-scroll" style="width: 100%; height: 100%">
											<asp:GridView ID="GVeFile" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="sqlProjecteFile" PageSize="20" Width="100%" OnRowDataBound="GVeFile_RowDataBound" DataKeyNames="RecordID" runat="server">
<EmptyDataTemplate>
													<table id="GVProjectFile" border="1" cellspacing="0" class="grid" rules="all" style="border-collapse: collapse;">
														<tr class="grid_head">
															<th scope="col" style="width: 70px;">
																项目名称
															</th>
															<th scope="col" style="width: 70px;">
																档案编号
															</th>
															<th scope="col" style="width: 70px;">
																文档名称
															</th>
															<th scope="col" style="width: 70px">
																密级
															</th>
															<th scope="col" style="width: 120px">
																备注
															</th>
														</tr>
													</table>
												</EmptyDataTemplate>
<Columns><asp:BoundField DataField="FileTitle" HeaderText="档案名称" SortExpression="FileTitle" /><asp:BoundField DataField="FileCode" HeaderText="档案编号" SortExpression="FileCode" /><asp:BoundField DataField="SecretLevel" HeaderText="密级" SortExpression="SecretLevel" /><asp:BoundField DataField="Remark" HeaderText="备 注" SortExpression="Remark" /><asp:TemplateField HeaderText="查看" SortExpression="FilePath"><EditItemTemplate>
															<asp:TextBox ID="TextBox1" Text='<%# Convert.ToString(Eval("OriginalName")) %>' runat="server"></asp:TextBox>
														</EditItemTemplate><ItemTemplate>
															<asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Convert.ToString(Eval("FilePath")) %>' Text='<%# Convert.ToString(Eval("FilePath")) %>' runat="server"></asp:HyperLink>
														</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="grid_row"></RowStyle><PagerStyle HorizontalAlign="Right"></PagerStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
											<asp:SqlDataSource ID="sqlProjecteFile" SelectCommand="SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info] " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
										</div>
									</td>
								</tr>
							</table>
						</asp:View>
					</asp:MultiView>
				</td>
			</tr>
		</table>
		<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
	</div>
	</form>
</body>
</html>
