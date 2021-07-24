<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncometContract.aspx.cs" Inherits="ContractManage_IncometContract_AddIncometContract" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>收入合同</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/jquery.easyui.extension.js"></script>
    <script type="text/javascript" src="/Script/jquery.easyui/locale/easyui-lang-zh_CN.js"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/jwJson.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="/StockManage/script/Validation.js"></script>
	<script type="text/javascript" src="../../Script/DecimalInput.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
		    $('#BtnShowTask').hide();
		    $('#BtnShowProjectInfo').hide();
			if (getRequestParam('t') == '1') {
				setAllInputDisabled();
			}
			var winTable = new JustWinTable('gvBudget');
			replaceEmptyTable('emptyBudget', 'gvBudget');
			$('#txtTypeName').attr('readOnly', 'readOnly');

			var b = getRequestParam('b');
			if (b != '') {
				$.ajax({
					type: 'GET',
					async: false,
					url: '/ContractManage/Handler/GetContractCodeSupply.ashx?contractID=' + b + "&reftime=" + (new Date()).toString(),
					success: function (data) {
						$('#txtContractCode').val(data);
					}
				});
			}

			jw.formatTreegrid('gvBudget', 1);

			$('#btnSave')[0].onclick = function () { jw.preventSubmit2('btnSave'); }
			//前台页面验证
			Val.validate('form1', 'btnSave');
		});


		function openProjPicker() {
			jw.selectOneProject({
				func: function (p) {
					$('#hdnProjectCode').val(p.prjId);
					$('#txtProject').val(p.prjName);
					$("#hfldPrjCode").val(p.prjCode);
					$('#BtnShowProjectInfo').click();

					// 根据项目和合同类型自动生成合同编号
					var action = getRequestParam('Action');
					var b = getRequestParam('b');
					if (b == '' && action != 'AddProtocol') {
						$.ajax({
							type: 'GET',
							async: false,
							url: '/ContractManage/Handler/GetContractCode.ashx?PrjCode=' + $('#hdnProjectCode').val() + '&ContractTypeID=' + $('#hfldTypeID').val() + "&reftime=" + (new Date()).toString(),
							success: function (data) {
								if (data) {
								    $('#txtContractCode').val(data);
								}
							}
						});
					}
				}
			});
		}

		//往来单位
		function pickCorp() {
			jw.selectOneCorp({ nameinput: 'txtParty', idinput: 'hdParty' });
		}

		//选择合同类型
		function selectContractType() {
			jw.selectOneConType({
				func: function (ct) {
					$('#hfldTypeID').val(ct.id);
					$('#txtTypeName').val(ct.name);
					var b = getRequestParam('b');
					if (b == '') {
						$.ajax({
							type: 'GET',
							async: false,
							url: '/ContractManage/Handler/GetContractCode.ashx?PrjCode=' + $('#hdnProjectCode').val() + '&ContractTypeID=' + $('#hfldTypeID').val() + "&reftime=" + (new Date()).toString(),
							success: function (data) {
								if (data) $('#txtContractCode').val(data);
							}
						});
					}
				}
			});
		}

		function pickBindResource() {
			var url = "/EPC/Basic/Resource/ResourceSelect/ResourceSelectFrame.aspx?pt=0&rs=2";
			winopen(url);
		}
		//选择人员
		function selectUser() {
			jw.selectOneUser({ nameinput: 'txtSignPeople', codeinput: 'hfldSignPeople' });
		}

		// 添加合同预算
		function addContractBuget() {
			var prjCode = $('#hdnProjectCode').val();
			if (prjCode == '') {
				top.ui.alert("请选择项目!");
				return false;
			}
			else {
				top.ui._IncometBudget = window;
				var url = "/ContractManage/IncometContract/IncometBudget.aspx";
				var prjId = $('#hdnProjectCode').val();
				url += "?prjId=" + prjId;
				if (getRequestParam('id')) {
					url += '&contractID=' + getRequestParam('id');
				}
				top.ui.callback = function (obj) {
					$('#hdTaskID').val(obj.taskId);
					$('#isDeteleConBudget').val('0');
					$('#BtnShowTask').click();
				}
				toolbox_oncommand(url, "添加合同预算");
			}
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
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader2">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="收入合同" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word" id="td1" style="white-space: nowrap;">
					附件
				</td>
				<td colspan="3" style="text-align: left; padding-right: 0px; width: 100%">
					<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					合同编号
				</td>
				<td class="txt  mustInput">
					<asp:TextBox ID="txtContractCode" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'合同编号必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					项目
				</td>
				<td class="mustInput" style="padding-right: 3px;">
					<input type="text" readonly="readonly" class="select_input {required:true, messages:{required:'请选择项目'}}" style="width: 100%;" id="txtProject" imgclick="openProjPicker" runat="server" />

					<input id="hdnProjectCode" type="hidden" name="hdnProjectCode" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					资金来源
				</td>
				<td class="txt readonly">
					<input id="txtPrjFundInfo" type="text" style="height: 15px; width: 100%;" readonly="readonly" runat="server" />

				</td>
				<td class="word" style="white-space: nowrap;">
					落实情况
				</td>
				<td class="txt readonly">
					<input id="txtPrjFundWorkable" type="text" style="height: 15px; width: 100%;" readonly="readonly" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					工程质量要求
				</td>
				<td class="txt readonly">
					<input id="txtQualityClass" type="text" style="height: 15px; width: 100%;" readonly="readonly" runat="server" />

				</td>
				<td class="word" style="white-space: nowrap;">
					预测利润率
				</td>
				<td class="txt readonly">
					<input id="txtForecastProfitRate" type="text" style="height: 15px;
						width: 100%;" readonly="readonly" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					项目类型
				</td>
				<td class="txt readonly">
					<input id="txtPrjType" type="text" style="height: 15px; width: 100%;" readonly="readOnly" runat="server" />

				</td>
				<td class="word" style="white-space: nowrap;">
					合同类型
				</td>
				<td class="mustInput" style="padding-right: 3px;">
					<asp:TextBox ID="txtTypeName" CssClass="select_input {required:true, messages:{required:'合同类型必须输入'}}" Width="100%" imgclick="selectContractType" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					合同名称
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtContractName" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'合同名称必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					合同金额
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtContractPrice" Height="15px" Width="100%" CssClass="decimal_input
                    {required:true,number:true, messages:{required:'合同金额必须输入',number:'合同金额格式错误'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					甲方
				</td>
				<td class="mustInput">
					<input type="text" class="select_input {required:true, messages:{required:'请选择甲方'}}" readonly="readonly" id="txtParty" style="width: 100%;" imgclick="pickCorp" runat="server" />

					<asp:HiddenField ID="hdParty" runat="server" />
				</td>
				<td class="word" style="white-space: nowrap;">
					乙方
				</td>
				<td class="txt mustInput" style="padding-right: 1px;" colspan="3">
					<asp:TextBox ID="txtSecond" Height="15px" Width="99%" CssClass="{required:true, messages:{required:'乙方必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					丙方
				</td>
				<td class="txt">
					<asp:TextBox ID="txtCParty" Height="15px" Width="100%" MaxLength="64" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					返还日期
				</td>
				<td class="txt">
					<asp:TextBox ID="txtReFundDate" Width="100%" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					签订人
				</td>
				<td>
					<input type="text" readonly="readonly" id="txtSignPeople" class="select_input" imgclick="selectUser" style="width: 100%;" runat="server" />

					<input id="hfldSignPeople" type="hidden" style="width: 1px" runat="server" />

				</td>
				<td class="word" style="white-space: nowrap;">
					签约日期
				</td>
				<td class="txt mustInput" style="padding-right: 1px">
					<asp:TextBox ID="txtSignedTime" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'签约日期必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					结算方式
				</td>
				<td class="txt mustInput" style="padding-right: 1px">
					<asp:DropDownList Width="100%" Height="20px" CssClass="{required:true, messages:{required:'请选择结算方式'}}" ID="ddlBalanceMode" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
				</td>
				<td class="word" style="white-space: nowrap;">
					付款方式
				</td>
				<td class="txt mustInput" style="padding-right: 1px">
					<asp:DropDownList Width="100%" Height="20px" CssClass="{required:true, messages:{required:'请选择付款方式'}}" ID="ddlPayMode" DataValueField="NoteID" DataTextField="CodeName" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					开始日期
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtStartDate" Width="100%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'开始日期必须输入'}}" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					结束日期
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtEndDate" Width="100%" onclick="WdatePicker()" CssClass="{required:true, messages:{required:'结束日期必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					质保期
				</td>
				<td class="txt">
					<input id="txtQualityPeriod" type="text" maxlength="200" style="height: 15px;
						width: 100%;" runat="server" />

				</td>
				<td class="word" style="white-space: nowrap;">
					签订状态
				</td>
				<td class="txt">
					<asp:RadioButton ID="rdbisSign" Text="已签订" Checked="true" GroupName="sign" runat="server" />&nbsp;&nbsp;
					<asp:RadioButton ID="rdbNoSign" Text="未签订" GroupName="sign" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					签约地点
				</td>
				<td class="txt mustInput" colspan="3">
					<asp:TextBox ID="txtSignedAddress" Height="15px" Width="100%" CssClass="{required:true, messages:{required:'签约地点必须输入'}}" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					收款条件
				</td>
				<td colspan="3">
					<asp:TextBox Height="60px" Rows="3" ID="txtCllectionCondition" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="right" style="white-space: nowrap;">
					主要条款
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtMainProvision" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td align="right" style="white-space: nowrap;">
					备注
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtRemark" Height="60px" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word" style="white-space: nowrap;">
					录入人
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="txtSubscriber" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
				</td>
				<td class="word" style="white-space: nowrap;">
					录入日期
				</td>
				<td class="txt readonly">
					<asp:TextBox Width="100%" ReadOnly="true" ID="txtRegisterTime" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<div id="div">
						<span id="spTarget" style="margin-left: 0px;" class="xxkd" runat="server">合同预算</span>
					</div>
				</td>
			</tr>
		</table>
		<div id='divTarget'>
			<table class="tableContent2" cellpadding="5px" cellspacing="0">
				<tr>
					<td>
						<input type="button" value="添加合同预算" style="width: 90px;" onclick="addContractBuget()" />
						
						<asp:Button ID="BtnDel" Width="90px" Text="删除合同预算" OnClick="BtnDel_click" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:GridView ID="gvBudget" AutoGenerateColumns="false" CssClass="gvdata" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="TaskId,OrderNumber" runat="server">
<EmptyDataTemplate>
								<table id="emptyBudget" class="tab">
									<tr class="header">
										<th scope="col" style="width: 25px;">
											序号
										</th>
										<th scope="col">
											任务名称
										</th>
										<th scope="col">
											任务编码
										</th>
										<th scope="col">
											类型
										</th>
										<th scope="col">
											单位
										</th>
										<th scope="col">
											工程量
										</th>
										<th scope="col">
											开始时间
										</th>
										<th scope="col">
											结束时间
										</th>
										<th scope="col">
											工期（天）
										</th>
										<th scope="col">
											综合单价
										</th>
										<th scope="col">
											小计
										</th>
										<th scope="col">
											备注
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderText="序号" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务名称"><ItemTemplate>
										<%# Eval("TaskName") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="任务编码"><ItemTemplate>
										<%# Eval("TaskCode") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="类型"><ItemTemplate>
										<%# Eval("TypeName") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
										<%# Eval("Unit") %>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="工程量" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("Quantity") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="开始时间">
<ItemTemplate>
										<%# Common2.GetTime(Eval("StartDate")) %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="结束时间">
<ItemTemplate>
										<%# Common2.GetTime(Eval("EndDate")) %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="工期（天）">
<ItemTemplate>
										<%# Eval("ConstructionPeriod") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="综合单价" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("UnitPrice") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="小计" ItemStyle-CssClass="decimal_input">
<ItemTemplate>
										<%# Eval("Total") %>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="备注"><ItemTemplate>
										<%# Eval("Note") %>
									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="winclose('AddIncometContract','IncometContractList.aspx',false)" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldTypeID" runat="server" />
	<asp:HiddenField ID="hdGuid" runat="server" />
	<asp:HiddenField ID="hdFileTime" runat="server" />
	<asp:HiddenField ID="hdFCode" runat="server" />
	<asp:HiddenField ID="hdIsArchived" runat="server" />
	<asp:HiddenField ID="hdisFContract" runat="server" />
	<asp:HiddenField ID="hdUserCodes" runat="server" />
	<asp:HiddenField ID="hdContractCode" runat="server" />
	<asp:HiddenField ID="hdReturnVal" runat="server" />
	
	<asp:HiddenField ID="hdTaskID" runat="server" />
	
	<asp:Button ID="BtnShowTask" OnClick="ShowTaskInfo_click" runat="server" />
	
	<asp:HiddenField ID="isDeteleConBudget" runat="server" />
	
	<asp:HiddenField ID="hdOrderNumber" runat="server" />
	
	<asp:HiddenField ID="hfldPrjCode" runat="server" />
    
    <asp:Button ID="BtnShowProjectInfo" OnClick="BtnShowProjectInfo_Click" runat="server" />
	</form>
</body>
</html>
