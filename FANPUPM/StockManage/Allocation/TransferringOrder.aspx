<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferringOrder.aspx.cs" Inherits="StockManage_Allocation_TransferringOrder" %>
<%@ Import Namespace="cn.justwin.BLL"%>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink2_ascx" Src="~/EPC/UserControl1/FileLink2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>调拨单</title><link href="../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script src="../script/Config2.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/Script/jquery.tooltip/jquery.tooltip.js"></script>
	<script src="../script/JustWinTable.js" type="text/javascript"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript" src="../../Script/jwJson.js"></script>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			showTooltip('tooltip', 10);
			replaceEmptyTable('emptyContractType', 'GVMaterialList');
			var jwTable = new JustWinTable('GVMaterialList');
		});

		// 选择调出库
		function selectStorageOut() {
			selectStorage('txtOutDepository', 'HdnSelectOutDepo', '1');
		}

		// 选择调入库
		function selectStorageIn() {
			selectStorage('txtOutDepository', 'HdnSelectOutDepo', '0');
		}

		// 选择仓库
		function selectStorage(txt, hid, tmp) {
			var stockmodel = document.getElementById("HdnStockModel").value; //库存模式
			var isTotal = document.getElementById("HdnIsTotal").value;   //表示分库 总库
			var DepoCode = document.getElementById("HdnSelectOutDepo").value;  //库存编码
			var op = document.getElementById("HdnOperater").value;
			var url = '';
			if (tmp == "1") {
				// 调出库
				top.ui.callback = function (o) {
					$('#HdnSelectOutDepo').val(o.code)
					$('#txtOutDepository').val(o.name);
				}
				url = "/StockManage/Allocation/SelectDepository.aspx?tshow=" + txt + "&hid=" + hid + "&it=HdnIsTotal" + "&no=" + document.getElementById("HdnSecTotal").value + "&dc=" + DepoCode + "&op=" + op + "&sm=" + stockmodel;
			}
			else {
				// 调入库
				top.ui.callback = function (o) {
					$('#HdnSelectInDepo').val(o.code)
					$('#txtInDepository').val(o.name);
				}
				url = "/StockManage/Allocation/SelectDepository.aspx?tshow=" + txt + "&hid=" + hid + "&it=HdnSecTotal" + "&no=" + isTotal + "&dc=" + DepoCode + "&op=" + op + "&disable=" + tmp + "&sm=" + stockmodel;
			}
			top.ui.openWin({ title: '选择仓库', url: url });
		}

		function selectPerson(txt, hid) {
			var url = "PickSinglePerson.aspx?txt=" + txt + "&hid=" + hid;
			window.open(url, '', 'width=360px,height=420px,menubar=no,toolbar=no,location=no,scrollbar=no,state=no');
		}

		function SelectMaterial(checked, mCode) {
			var Mlist = document.getElementById("HdnMList").value;
			if (checked) {
				if (Mlist == "") {
					Mlist += "'" + mCode + "',";
				}
				else {
					Mlist = Mlist.replace("'0'", "");
					Mlist += "'" + mCode + "',";
				}
				Mlist += "'0'";
				document.getElementById("HdnMList").value = Mlist;
			}
			else {
				Mlist = Mlist.replace("'" + mCode + "',", "");
				document.getElementById("HdnMList").value = Mlist;
			}
			document.getElementById("HdnAcode").value = "";
		}

		function rowClick(ac) {
			document.getElementById("HdnAcode").value = "'" + ac + "'";
			document.getElementById("HdnMList").value = "'" + ac + "'," + "'0'";
		}

		function openSelectMaterial() {
			if (document.getElementById("txtOutDepository").value == "" || document.getElementById("HdnSelectOutDepo").value == "") {
				top.ui.alert("请先选择调出库！");
				return false;
			}
			else {
				var url = "/StockManage/Allocation/PickMaterialsOfDepository.aspx?did=" + $('#HdnSelectOutDepo').val() +
                    "&ac=" + $('#lblAllocationNo').val() + "&op=" + $("#HdnOperater").val();
				top.ui.callback = function (o) {
					if ($('#HdnViewCodeList').val() == '') {
						$('#HdnViewCodeList').val(o.code);
					} else {
						var _codelist = $('#HdnViewCodeList').val().replace("(scode='-1' and sprice='-1' and corp='-1')", "");
						_codelist += o.code;
						$('#HdnViewCodeList').val(_codelist);
					}
					$('#btnBind').click();
				}
				top.ui.openWin({ title: '从仓库选择资源', url: url, width: 1000, height: 520 });
			}
		}

		// 验证数据
		function checkData() {
			if (document.getElementById("txtInDate").value == "") {
				top.ui.alert("请选择调拨日期！");
				return false;
			}
			if (document.getElementById("txtOutDepository").value == "" || document.getElementById("HdnSelectOutDepo").value == "") {
				top.ui.alert("调出库无效或者为空 请重新选择！");
				return false;
			}
			if (document.getElementById("txtInDepository").value == "" || document.getElementById("HdnSelectInDepo").value == "") {
				top.ui.alert("调入库无效或者为空 请重新选择！");
				return false;
			}
			if (document.getElementById("txtOutAllocationPerson").value == "" || document.getElementById("HdnSelectOutPer").value == "") {
				top.ui.alert("拨出人无效或者为空 请重新选择！");
				return false;
			}
			if (document.getElementById("txtInAllocationPerson").value == "" || document.getElementById("HdnSelectInPer").value == "") {
				top.ui.alert("接收人无效或者为空 请重新选择！");
				return false;
			}
			if (document.getElementById("txtOutDepository").value == document.getElementById("txtInDepository").value) {
				top.ui.alert("调出库和调入库不能相同 请重新选择！");
				return false;
			}

			var $trArr = $('#GVMaterialList tr');
			var rowCount = $trArr.length;
			if (rowCount == 1) {
				top.ui.alert("请选择物资！");
				return false;
			}

			// 是否大于1页
			if ($('#HdnIsPage').val() == '1')
				rowCount -= 1;

			for (i = 1; i < rowCount - 1; i++) {
			    var allocatNum = parseFloat(removeComma($trArr.eq(i).find('.allocat_num').val()));
			    var totalNum = parseFloat(removeComma($trArr.eq(i).find('.total_num').text()))
				if (allocatNum > totalNum || allocatNum == 0) {
					top.ui.alert('调拨数量不能大于库存量，调拨数量不能等于零，请重新填写。');
					return false;
				}
            }
         }

         function removeComma(value) {
             if (value.search(/,/i) != -1) {
                 return value.replace(/,/g, "");
             }
             return value
         }

		function DelMaterial() {
			var op = document.getElementById("HdnOperater").value;
			var tab = document.getElementById("GVMaterialList");
			var viewList = document.getElementById("HdnViewCodeList").value;
			if (op == "Add") {
				var array = viewList.split(" or ");
				for (var i = 0; i < array.length; i++) {
					for (var j = i + 1; j < array.length; j++) {
						if (array[i] == array[j])
							array.splice(j, 1);
					}
				}
				document.getElementById("HdnViewCodeList").value = array.toString().replace(/,/g, " or ");
				var rowcount = tab.rows.length;
				if (document.getElementById("HdnIsPage").value == "1") {
					rowcount -= 1;
				}
				for (var m = 1; m < rowcount; m++) {
					var chkIt = tab.rows[m].cells[0].firstChild;
					if (chkIt.checked) {
						var sc = tab.rows[m].cells[2];
						var corp = tab.rows[m].cells[13].childNodes[2];
						var price = tab.rows[m].cells[9].firstChild;
						var vlist = "(scode='" + sc.innerText + "' and sprice='" + price.outerText + "' and corp='" + corp.value + "') or ";
						document.getElementById("HdnViewCodeList").value = document.getElementById("HdnViewCodeList").value.replace(vlist, "");
					}
				}
			}
		}

		function AnnexManage() {
			var lblno = document.getElementById("lblAllocationNo").value;
			var url = "/CommonWindow/Annex/AnnexList.aspx?mid=89&rc=" + lblno + "&at=0";
			var op = document.getElementById("HdnOperater").value;
			if (op == "View")
				url = "/CommonWindow/Annex/AnnexList.aspx?mid=89&rc=" + lblno + "&at=0&type=1";
			return window.showModalDialog(url, 'win', 'dialogHeight:260px;dialogWidth:560px;location:no;help:no;srcoll:no;resizable:no;center:yes;status:no');
		}


		function AllSelect(chked) {
			var op = document.getElementById("HdnOperater").value;
			if (op == "Edit") {
				document.getElementById("HdnAcode").value = "";
				document.getElementById("HdnMList").value = "";
				if (chked) {
					var tab = document.getElementById("GVMaterialList");
					var rowcount = tab.rows.length;
					if (document.getElementById("HdnIsPage").value == "1") {
						rowcount -= 1;
					}
					for (var i = 1; i < rowcount; i++) {
						document.getElementById("HdnMList").value += "'" + tab.rows[i].id + "',";
					}
					document.getElementById("HdnMList").value += "'0'";
				}

				else
					document.getElementById("HdnMList").value = "";
			}
		}


		//选择调出人
		function selectUserOut() {
			jw.selectOneUser({ codeinput: 'HdnSelectOutPer', nameinput: 'txtOutAllocationPerson' });
		}
		//选择接收人
		function selectUserIn() {
			jw.selectOneUser({ codeinput: 'HdnSelectInPer', nameinput: 'txtInAllocationPerson' });
		}

		//显示物资需求计划
		function displaySelectFromPurchase() {
//			var url = '/StockManage/SmPurchaseplan/SelectByNote.aspx?Type=check';
		    var url = '/StockManage/ProjectFrame.aspx?path=selectWantPlan&Type=check'
			top.ui.openWin({ title: '查看物资需求计划', url: url, width: 820, height: 500 });
        }

        function setGVTotalNumber() {
            var number = 0.00;
            var sprice = 0.00;
            var rowTotal = 0.00;
            var allTotal = 0.00;
            $('#GVMaterialList tr[id]').each(function () {
                number = $(this).find('input[id$=txtAllocationOutNum]').val();
                sprice = $(this).find('span[id$=txtUnitPrice]').text();
                rowTotal = sprice * number;
                $(this).find('span[id$=lbTotal]').text(rowTotal.toFixed(3));
                allTotal += rowTotal;
            })
            $('#GVMaterialList').find('span[class=_total_]').text(allTotal.toFixed(3));
        }
    </script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divHeader2">
		<table class="tableHeader">
			<tr>
				<td>
					<asp:Label ID="lblTitle" Font-Bold="true" Text="调拨管理" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<div class="divContent2">
		<table class="tableContent2" style="margin-left: auto;" cellpadding="5px" cellspacing="0">
			<tr>
				<td class="word">
					附件
				</td>
				<td colspan="3">
					<MyUserControl:epc_usercontrol1_filelink2_ascx ID="FileLink1" runat="server" />
				</td>
			</tr>
			<tr>
				<td class="word">
					调拨单号
				</td>
				<td class="txt readonly">
					<asp:TextBox ID="lblAllocationNo" runat="server"></asp:TextBox>
				</td>
				<td class="word">
					调拨日期
				</td>
				<td class="txt mustInput">
					<asp:TextBox ID="txtInDate" onclick="WdatePicker()" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td class="word">
					调出库
				</td>
				<td class="txt mustInput" style="padding-right: 3px">
					<asp:TextBox ID="txtOutDepository" Style="width: 99%;" CssClass="select_input" imgclick="selectStorageOut" runat="server"></asp:TextBox>
					<input id="HdnSelectOutDepo" type="hidden" style="width: 1px" runat="server" />

					<input id="HdnIsTotal" type="hidden" style="width: 1px;" value="-1" runat="server" />

				</td>
				<td class="word">
					调入库
				</td>
				<td class="txt mustInput" style="padding-right: 3px">
					<asp:TextBox ID="txtInDepository" Style="width: 99%;" CssClass="select_input" imgclick="selectStorageIn" runat="server"></asp:TextBox>
					<input id="HdnSelectInDepo" type="hidden" style="width: 1px" runat="server" />

					<input id="HdnSecTotal" type="hidden" style="width: 1px;" value="-1" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word">
					拨出人
				</td>
				<td class="txt mustInput" style="padding-right: 3px">
					<asp:TextBox ID="txtOutAllocationPerson" Style="width: 99%;" CssClass="select_input" imgclick="selectUserOut" runat="server"></asp:TextBox>
					<input id="HdnSelectOutPer" type="hidden" style="width: 1px" runat="server" />

				</td>
				<td class="word">
					接收人
				</td>
				<td class="txt mustInput" style="padding-right: 3px">
					<asp:TextBox ID="txtInAllocationPerson" Style="width: 99%;" CssClass="select_input" imgclick="selectUserIn" runat="server"></asp:TextBox>
					<input id="HdnSelectInPer" type="hidden" style="width: 1px" runat="server" />

				</td>
			</tr>
			<tr>
				<td class="word">
					备注
				</td>
				<td colspan="3">
					<asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="4" style="height: 10px">
					<hr class="sp" />
				</td>
			</tr>
			<tr>
				<td colspan="4" style="text-align: left;">
					<input type="button" id="btnSelectMaterial" value="从仓库选择资源" style="width: 100px;" onclick="openSelectMaterial();" runat="server" />

					<asp:Button ID="BtnDel" Text="删除资源" Style="width: 100px;" OnClick="BtnDel_ServerClick" runat="server" />
					<input type="button" id="btnSelectByd" value="查看需求计划" onclick="displaySelectFromPurchase();" style="width: 100px;" runat="server" />

					<input type="button" id="btnBind" style="display: none;" OnServerClick="btnBind_ServerClick" runat="server" />

					<input type="button" id="btnAllDel" style="display: none;" OnServerClick="btnAllDel_ServerClick" runat="server" />

				</td>
			</tr>
			<tr>
				<td colspan="4">
					<div class="pagediv">
						<asp:GridView CssClass="gvdata" ID="GVMaterialList" Width="100%" AutoGenerateColumns="false" OnRowDataBound="GVMaterialList_RowDataBound" DataKeyNames="scode,sprice,corp" runat="server">
<EmptyDataTemplate>
								<table id="emptyContractType" class="gvdata" style="border: solid 0px;">
									<tr class="header">
										<th style="width: 20px">
											<input type="checkbox" />
										</th>
										<th style="width: 25px">
											序号
										</th>
										<th>
											物资编号
										</th>
										<th>
											物资名称
										</th>
										<th>
											规格
										</th>
										<th>
											型号
										</th>
										<th>
											品牌
										</th>
										<th>
											技术参数
										</th>
										<th>
											单位
										</th>
										<th>
											采购价
										</th>
										<th>
											调拨数量
										</th>
										<th>
											小计
										</th>
										<th>
											供应商
										</th>
									</tr>
								</table>
							</EmptyDataTemplate>
<Columns><asp:TemplateField>
<ItemTemplate>
										<asp:CheckBox ID="chkBox" ToolTip='<%# Convert.ToString(Eval("scode")) %>' runat="server" />
									</ItemTemplate>

<HeaderTemplate>
										<asp:CheckBox ID="chkAll" onclick="AllSelect(this.checked);" runat="server" />
									</HeaderTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="序号">
<ItemTemplate>
										<asp:Label ID="lblNo" runat="server"></asp:Label>
									</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="ResourceCode" HeaderText="物资编号" /><asp:TemplateField HeaderText="物资名称"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("ResourceName").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ResourceName").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="规格"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("Specification").ToString() %>'>
											<%# StringUtility.GetStr(Eval("Specification").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="型号"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("ModelNumber").ToString() %>'>
											<%# StringUtility.GetStr(Eval("ModelNumber").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="品牌"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("brand").ToString() %>'>
											<%# StringUtility.GetStr(Eval("brand").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="技术参数"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("TechnicalParameter").ToString() %>'>
											<%# StringUtility.GetStr(Eval("TechnicalParameter").ToString(), 10) %>
										</span>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="单位"><ItemTemplate>
										<asp:Label ID="lblUnit" runat="server"></asp:Label>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="采购价">
<ItemTemplate>
										<asp:Label ID="txtUnitPrice" Width="80px" CssClass="decimal_input" ReadOnly="true" Text='<%# Convert.ToString(Eval("sprice")) %>' runat="server"></asp:Label>
									</ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="snumber" HeaderText="库存量" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="decimal_input total_num" /><asp:TemplateField HeaderText="调拨数量"><ItemTemplate>
										<asp:TextBox ID="txtAllocationOutNum" CssClass="decimal_input allocat_num" onblur="setGVTotalNumber()" Style="text-align: right;" Text='<%# Convert.ToString((Eval("number").ToString() == "") ? "0.000" : Convert.ToDecimal(Eval("number")).ToString("0.000")) %>' No='<%# Convert.ToString(Container.DataItemIndex) %>' runat="server"></asp:TextBox>
									</ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="小计">
<ItemTemplate>
                                        <asp:Label ID="lbTotal" CssClass="decimal_input" Text='<%# Convert.ToString(Convert.ToDecimal(Eval("Total")).ToString("0.000")) %>' runat="server"></asp:Label>
									</ItemTemplate>
</asp:TemplateField><asp:TemplateField HeaderText="供应商"><ItemTemplate>
										<span class="tooltip" title='<%# Eval("CorpName").ToString() %>'>
											<%# StringUtility.GetStr(Eval("CorpName").ToString(), 10) %>
										</span>
										<input type="hidden" id="HdnCorpID" value='<%# Convert.ToString(Eval("corpID")) %>' runat="server" />

									</ItemTemplate></asp:TemplateField></Columns><RowStyle CssClass="rowa"></RowStyle><AlternatingRowStyle CssClass="rowb"></AlternatingRowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div class="divFooter2">
		<table class="tableFooter2">
			<tr>
				<td>
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" id="btnCancel" value="取消" onclick="winclose('TransferringOrder','AllocationManager.aspx',false)" />
				</td>
			</tr>
		</table>
	</div>
	<input type="hidden" id="HdnOperater" style="width: 1px" runat="server" />

	<input type="hidden" id="HdnMList" style="width: 1px" runat="server" />

	<input type="hidden" id="HdnAcode" style="width: 1px;" runat="server" />

	<input type="hidden" id="HdnViewCodeList" style="width: 1px" runat="server" />

	<input type="hidden" id="HdnIsPage" style="width: 1px" runat="server" />

	<input type="hidden" id="hdGuid" runat="server" />

	<asp:HiddenField ID="hdReturnVal" runat="server" />
	<!-- 仓库模式-->
	<input id="HdnStockModel" type="hidden" style="width: 1px;" runat="server" />

	</form>
</body>
</html>
