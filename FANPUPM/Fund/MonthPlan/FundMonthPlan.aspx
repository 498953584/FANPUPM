<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FundMonthPlan.aspx.cs" Inherits="Fund_FundMonthPlan" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_wf_ascx" Src="~/EPC/UserControl1/WF.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>资金管理-资金计划</title>
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/SysFrame/jscript/JsControlMenuTool.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	
    <script src="../../Script/jw.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Script/jquery.cookie.js"></script>
	<script type="text/javascript" src="/UserControl/FileUpload/GetFiles.js"></script>
	<script type="text/javascript" src="/Script/wf.js"></script>
	<script src="../../Web_Client/Tree.js" type="text/javascript"></script>
    <script src="../../Script/DecimalInput.js" type="text/javascript"></script>
	<script type="text/javascript">
		var flowStateIndex = 8;
		$(document).ready(function () {
		    //设置宽高
		    setWidthHeight();
		    var jwTable = new JustWinTable('gvBudget');
		    replaceEmptyTable('emptyContractType', 'gvBudget');
		    setButton(jwTable, 'hfldPurchaseChecked');
		    if (!jwTable.table) return;
		    setWfButtonState2(jwTable, 'WF1');
		    var cheks = jwTable.getCheckedChk();
		    displayLookAdjuncts();
		    //提升管理员权限
		    var userCode = getCookie('UserCode');
		    if (userCode != '') {
		        $('#btnDelete').removeAttr('disabled');
		    }
		});

		function isValidatePlanEdit() {
			var $tr = $("#gvBudget tr");
			if ($tr.length == 1) return true;

			var beginDate = $('#hdnBeginDate').val(); 	// 开始日期
			var isBeginDate = $('#hdnIsBeginDate').val();
			var curDate = new Date(); 				// 当前时间
			var planMonth = curDate.getMonth() + 1; 		// 可以编制的月份
			if (curDate.getDate() > beginDate) {
				planMonth++;
			}
			var planName = '';
			if (planMonth < 10) {
				planName = curDate.getFullYear() + '年0' + planMonth + '月';
			} else {
				planName = curDate.getFullYear() + '年' + planMonth + '月';
			}
			if (isBeginDate == '1') {
				for (var i = 1; i < $tr.length; i++) {
					var thePlanName = $($tr[i]).find('td:eq(2)').html();
					if (thePlanName.indexOf(planName) > -1) return false;
				}
			}
			return true;
		}

		function hasChecboxChecked() {
			var objTab = $("#gvBudget").find("tr");
			var rowCount = objTab.length;
			for (var i = 1; i < rowCount; i++) {
				if (objTab.eq(i).find('td').eq(0).children().prop("checked")) {
					return true;
				}
			}
			return false;
		}

		//月入计划编辑
		function planEdit(action) {
			top.ui._Flowclass = window;
			if (!hasChecboxChecked()) {
				if (!isValidatePlanEdit()) {
					jw.alert("系统提示", '还没有到编制时间，如果要更改选项，请选中在编制');
					return;
				}
			}
			var id = document.getElementById('hfldPurchaseChecked').value;
			var year = document.getElementById('hfldYear').value;
			var prjId = document.getElementById('hfldPrjId').value;
			var plantype = document.getElementById('hfplantype').value;
			var prjname = document.getElementById('hfPrjName').value;
			var plandate = document.getElementById('hdnplandate').value;
			var url = '../fund/monthplan/MonthPlanSet.aspx?year=' + year + '&prjId=' + prjId;
			url = url + '&plandate=' + plandate;
			url = url + '&plantype=' + plantype;
			url = url + '&prjname=' + escape(prjname);
			url = url + '&planMainId=' + id;
			var titleText = '月支出计划编制';
			if (plantype == 'income') {
				if (id != '')
					titleText = '月收入计划编制';
				else
					titleText = '月收入计划新增';
			}
			else {
				if (id == '')
					titleText = '月支出计划新增';
			}
			toolbox_oncommand(url, titleText);
		}
		//设置宽高
		function setWidthHeight() {
			$('#divright').height($(this).height() - 45);
			$('#divmain').height(160);
			$('#divDetails').height($('#divright').height() - $('#divmain').height());
			$('#div_project').height($(this).height() - 20);
		}
		//查看
		function openQuery() {
			var pt = $("#hfplantype").val();
			// var id = $("#hfldMonthPlanID").val();
			var id = $('#hfldPurchaseChecked').val();
			var _prjcode = $("#hfldPrjId").val();
			if (id != "") {
				var url = '/Fund/MonthPlan/MonthPlanView.aspx?ic=' + id + "&plantype=" + pt + "&prjcode=" + _prjcode;
				var title = "";
				title = "资金计划";
				title += '明细';
				toolbox_oncommand(url, title);
			}
		}

		//单击行事件
		function clickRows(temMID, trt, plandate) {
			$("#hdnplandate").val(plandate);
			$("#btnLook").removeAttr("disabled");
			$("#hfldMonthPlanID").val(temMID);
			//获取月入计划明细
			getDeitalByMonthPlanID(temMID, trt);
			//附件显示
			displayLookAdjuncts();
			// checkBtnWeave();
		}
		//绑定月入计划明细
		function getDeitalByMonthPlanID(_MID, trt) {
			var path = $('#hfldAdjunctPath').val();
			if (_MID != "") {
				var _PlanYear = $(trt).attr("PlanYear");
				var _PlanMonth = $(trt).attr("PlanMonth");
				var _PrjGuid = $(trt).attr("PrjGuid");
				url = "showDetailMonthPlan.aspx?e=" + Math.random() + "&mpid=" + _MID;
				document.getElementById("framShowDetail").src = url;
			}
		}
		//查看月入计划查看页面
		function alertMonthPlanView(id, pt) {
			var _prjcode = $("#hfldPrjId").val();
			var url = '/Fund/MonthPlan/MonthPlanView.aspx?ic=' + id;
			viewopen(url);
		}
		//查看附件
		function queryAdjunct(id) {
			var path = $('#hfldAdjunctPath').val() + id;
			$('#divOpenAdjunct').css('display', 'block');
			showFiles(path, 'divOpenAdjunct');
		}
		//附件图标是否显示
		function displayLookAdjuncts() {
			var tab = document.getElementById('gvBudget');
			if (tab != null) {
				if (tab.rows.length > 0) {
					for (i = 1; i < tab.rows.length; i++) {
						var id = tab.rows[i].id;
						var path = $('#hfldAdjunctPath').val() + id;
						var showCount = getFilesCount(path);
						if (showCount == 0)
							tab.rows[i].cells[9].innerText = '';
					}
				}
			}
		}
		//查看附件的个数
		function getFilesCount(path) {
			var count = 1;
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
		function checkAlone(id) {
			var objTab = $("#gvBudget").find("tr");
			var rowCount = objTab.length;
			for (var i = 1; i < rowCount; i++) {
				if (objTab.eq(i).find('td').eq(0).children().attr('id') != id) {
					objTab.eq(i).find('td').eq(0).children().prop("checked", false);
				}

			}
			return false;
		}


		function setButton(jwTable, hdChkId) {		   
			if (!jwTable.table) return;
			if (jwTable.table.getElementsByTagName('tr').length == 1) {
				//table中没有数据
				return;
			}

			jwTable.registClickTrListener(function () {

			    if (hdChkId != '') {
			        $('#hfldPurchaseChecked').val(this.id);
			    }
			    checkedSingle(this.id);

			    //			    var flowState = $(this).attr('flowState');
			    //			    if (flowState == '-1' || flowState == '-3' || flowState == '1') {
			    //			             未提交或者重报
			    //			            $('#btnAdd').attr('disabled', false);
			    //			            $('#btnDelete').attr('disabled', false);
			    //			    }
			    //			    else{
			    //			        $('#btnAdd').attr('disabled', true);
			    //			        $('#btnDelete').attr('disabled', true);
			    //			    }

			    upAdminPrivilege();
			    var flowState = $(this).attr('flowState');
			    var userCode = getCookie('UserCode');
			    if (flowState == '-1' || flowState == '-3') {
			        $('#btnAdd').attr('disabled', false);
			        if (userCode != '00000000') {
			            $('#btnDelete').attr('disabled', false);
			        }
			    }
			    else {
			        $('#btnAdd').attr('disabled', true);
			        $('#btnDelete').attr('disabled', true);
			    }
			});

			jwTable.registClickSingleCHKListener(function () {
			    var checkedChk = jwTable.getCheckedChk();
			    var values = jwTable.getCheckedChkIdJson(checkedChk);
			    if (checkedChk.length == 0) {
			        disabledButton();
			        values = '';
			    }
			    else if (checkedChk.length == 1) {
			        checkedSingle(values);
			    }
			    else {
			        $('#btnAdd').attr('disabled', 'disabled');
			        $('#btnLook').attr('disabled', 'disabled');
			    }

			    $('#hfldPurchaseChecked').val(values);


			    var flowState = $(this).parent().parent().attr("flowState");
			    var userCode = getCookie('UserCode');
			    if (flowState == '-1' || flowState == '-3') {
			        $('#btnAdd').attr('disabled', false);
			        if (userCode != '00000000') {
			            $('#btnDelete').attr('disabled', false);
			        }
			    }
			    else {
			        $('#btnAdd').attr('disabled', true);
			        $('#btnDelete').attr('disabled', true);
			    }



			});
		};
		//单选
		function checkedSingle(id) {
		    //启用按钮
		    $('#btnDelete').removeAttr('disabled');
			$('#btnLook').removeAttr('disabled');
			//$('#'+id).children().eq(flowStateIndex).children().eq(0);
			if ($('#' + id).children().eq(flowStateIndex).children().eq(0)) {
				var state = $('#' + id).children().eq(flowStateIndex).children().eq(0).attr("state");
				if (state == '0' || state == '1' || state == '-2') {
					$('#btnDelete').attr('disabled', 'disabled');
				}
				else {
				    $('#btnDelete').removeAttr('disabled');
				}
            }
            var userCode = getCookie('UserCode');
            if (userCode != '') {
                $('#btnDelete').removeAttr('disabled');
            }
		}
		//禁用按钮
		function disabledButton() {
			$('#btnDelete').attr('disabled', 'disabled');
			$('#btnLook').attr('disabled', 'disabled');
		}
		function showFiles(folder, divId) {
			$('#' + divId + ' table tr:gt(0)').remove();
			$.getJSON('/UserControl/FileUpload/GetFiles.ashx?' + new Date().getTime(), { Path: folder }, function (data) {
				$('#' + divId + ' table').empty();
				var content;
				//table 头
				var $thName = "<td class=tdStyle style=' width:280px; height:20px;border:1px solid rgb(202,222,222);text-align:center;background-color:rgb(238,242,242)'>文件名称</td>";
				var $thLength = "<td  style=' width:140px; height:20px;border:1px solid rgb(202,222,222);text-align:center;background-color:rgb(238,242,242)' >文件大小</td>"
				var $trTitle = "<tr class style='width:420px;border:1px solid rgb(202,204,204)；'>" + $thName + $thLength + "</tr>";
				content = $trTitle;
				$.each(data, function (index, annex) {
					var style;
					var $a = "<a target='_blank' href=" + jw.downloadUrl + "?path=" + folder + '/' + annex.Name + " style='cursor:pointer' class='linkAnnex'>" + decodeURIComponent(annex.Name) + "</a>";
					if (index % 2 == 0) {
						style = 'rowa';
					} else {
						style = 'rowb';
					}
					var $tdName = "<td class=tdStyle style=' width:280px; height:20px;border:1px solid rgb(202,222,222);text-align:center;'>" + $a + "</td>";
					var $tdLength = "<td  style=' width:140px; height:20px;border:1px solid rgb(202,222,222);text-align:center;' >" + annex.Length + "</td>"
					var $tr = "<tr class=" + style + " style='width:420px;border:1px solid rgb(202,222,222)'>" + $tdName + $tdLength + "</tr>";
					content += $tr;
				});
				var table = $("<div style='width:420px;height:200px;padding:0px;margin:0px auto;text-align:center;'><table id=" + divId + " style=' text-align:center; border-collapse:collapse; width:418px; height:auto; padding:0px; margin:1px; border:1px solid rgb(202,222,222);border-bottom:0px;'>" + content + "</table></div>");
				$(table).dialog({ width: 434, height: 300, modal: true, title: '附件' });
			});
		}
    </script>
</head>
<body>
	<form id="form1" runat="server">
	<div id="divOpenAdjunct" title="查看附件" style="display: none;">
		<table id="annexTable" class="gvdata" style="width: 100%;" runat="server"><tr class="header" runat="server"><td style="width: 60%" runat="server">
						名称
					</td><td style="width: 30%" runat="server">
						大小
					</td><td runat="server">
					</td></tr></table>
	</div>
	<div style="width: 100%; height: 100%; overflow: hidden;">
		<table style="width: 100%; height: 100%;">
			<tr>
				<td style="width: 100%; height: 100%;">
					<table style="width: 100%; height: 100%;">
						<tr>
							<td id="td_Right" style="width: 100%; vertical-align: top; height: 100%; border-left: solid 2px #CADEED;">
								<table class="tab">
									<tr id="header">
										<td>
											<asp:Label ID="lblTitle" Text="资金计划" runat="server"></asp:Label>
										</td>
									</tr>
									<tr class="divFooter" style="height: 1px;">
										<td class="divFooter" style="text-align: left;">
											<input type="button" value="编制" id="btnAdd" onclick="planEdit()" runat="server" />

											<input type="button" value="查看" id="btnLook" disabled="disabled" onclick="openQuery()" />
											<asp:Button Text="删除" ID="btnDelete" disabled="disabled" OnClick="btnDelete_Click" runat="server" />
											<MyUserControl:epc_usercontrol1_wf_ascx ID="WF1" BusiClass="001" runat="server" />
										</td>
									</tr>
									<tr>
										<td style="vertical-align: top; height: 100%;">
											<div id="divright" style="overflow: hidden;">
												<div id="divmain" style="overflow: auto; border-top: solid 1px #CADEED;">
													<asp:GridView ID="gvBudget" CssClass="gvdata" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvBudget_RowDataBound" DataKeyNames="MonthPlanID" runat="server">
<EmptyDataTemplate>
															<table id="emptyContractType" class="gvdata">
																<tr class="header">
																	<th scope="col" style="width: 25px;">
																		序号
																	</th>
																	<th scope="col" style="width: 80px;">
																		计划月份
																	</th>
																	<th scope="col" style="width: 80px;">
																		上期结余
																	</th>
																	<th scope="col" style="width: 80px;">
																		本期计划
																	</th>
																	<th scope="col" style="width: 80px;">
																		本期计划金额
																	</th>
																	<th scope="col" style="width: 80px;">
																		本期实际发生金额
																	</th>
																	<th scope="col" style="width: 80px;">
																		本期计划执行情况
																	</th>
																	<th scope="col" style="width: 80px;">
																		填报时间
																	</th>
																	<th scope="col" style="width: 80px;">
																		填报人
																	</th>
																	<th scope="col" style="width: 80px;">
																		流程状态
																	</th>
																	<th scope="col">
																		备注
																	</th>
																</tr>
															</table>
														</EmptyDataTemplate>
<Columns><asp:TemplateField HeaderStyle-Width="20px">
<HeaderTemplate>
																	<asp:CheckBox ID="cbBoxAll" Enabled="false" runat="server" />
																</HeaderTemplate>

<ItemTemplate>
																	<asp:CheckBox ID="cbBox" runat="server" />
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
																	<%# Container.DataItemIndex + 1 %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="计划月份" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
																	<a style="cursor: pointer">
																		<asp:Label ID="Label1" CssClass="plan-month" Text="" runat="server"></asp:Label></a>
																</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="上期结余" DataField="OldBalance" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Right" Visible="false" ItemStyle-CssClass="decimal_input" /><asp:BoundField HeaderText="本期计划金额" DataField="PlanMoney" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input" /><asp:TemplateField HeaderText="本期实际发生金额" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="decimal_input"><ItemTemplate>
																	
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="本期计划执行情况" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right"><ItemTemplate>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="填报时间">
<ItemTemplate>
																	<%# Common2.GetTime(Eval("OperateTime")) %>
																</ItemTemplate>
</asp:TemplateField><asp:BoundField HeaderText="填报人" DataField="yhmc" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" /><asp:TemplateField HeaderText="流程状态" HeaderStyle-Width="80px">
<ItemTemplate>
																	<%# Common2.GetState(Eval("FlowState").ToString()) %>
																</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="项目" Visible="false"><ItemTemplate>
																	<%# this.hfPrjName.Value %>
																</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="附件" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px"><ItemTemplate>
																	<span class="link" onclick="queryAdjunct('<%# Eval("MonthPlanID") %>')">
																		<img src='/images1/icon_att0b3dfa.gif' style='cursor: pointer; border-style: none' />
																	</span>
																</ItemTemplate></asp:TemplateField><asp:BoundField HeaderText="备注" DataField="Remark" /></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
												</div>
												<div id="divDetails" style="overflow: auto; border-top: solid 2px #CADEED;">
													<iframe id="framShowDetail" src="showDetailMonthPlan.aspx" style="width: 100%;
														height: 98%;" frameborder="none" border="0" runat="server"></iframe>
												</div>
											</div>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldAdjunctPath" runat="server" />
	<asp:HiddenField ID="hfldPurchaseChecked" runat="server" />
	<asp:HiddenField ID="hfldPrjId" runat="server" />
	<asp:HiddenField ID="hfldYear" runat="server" />
	<asp:HiddenField ID="hfplantype" runat="server" />
	<asp:HiddenField ID="hfPrjName" runat="server" />
	<asp:HiddenField ID="hfldMonthPlanID" runat="server" />
	<asp:HiddenField ID="hdnplandate" runat="server" />
	<input type="hidden" id="hdnfiles" runat="server" />

	<input type="hidden" id="hdnBeginDate" runat="server" />

	<input type="hidden" id="hdnIsBeginDate" runat="server" />

	</form>
</body>
</html>
