<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectAllUser.aspx.cs" Inherits="Common_SelectAllUser" EnableEventValidation="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>选择人员</title>
	<base target="_self" />
	<script type="text/javascript" src="../../Script/jquery.js"></script>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../Script/jw.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			$('#btnExecMethod').css('display', 'none');

			// 切换部门后保存
			if ($("#hfldCodeJson").val()) {
				var codeArr = eval($("#hfldCodeJson").val());
				var nameArr = eval($("#hfldNameJson").val());
				var len = codeArr.length;
				for (var i = 0; i < len; i++) {
					var opt = jw.format("<option value='{0}'>{1}</option>", codeArr[i], nameArr[i]);
					$('#lbUser').append(opt);
				}
			}
		});

		// 把值存到隐藏字段里
		function saveUser() {
			var options = $('#lbUser OPTION');
			var codeArr = new Array();
			var nameArr = new Array();
			for (var i = 0; i < options.length; i++) {
				codeArr.push(options[i].value);
				nameArr.push(options[i].text);
			}

			$('#hfldCodeJson').val(jw.array1dToJson(codeArr));
			$('#hfldNameJson').val(jw.array1dToJson(nameArr));
		}

		// 从隐藏字段里删除
		function delVal(code) {
			var codeArr = eval($("#hfldCodeJson").val());
			var nameArr = eval($("#hfldNameJson").val());
			for (var i = 0; i < codeArr.length; i++) {
				if (code == codeArr[i]) {
					delete codeArr[i];
					codeArr.splice(i, 1);
					delete nameArr[i];
					nameArr.splice(i, 1);
				}
			}
			$("#hfldCodeJson").val(jw.array1dToJson(codeArr));
			$("#hfldNameJson").val(jw.array1dToJson(nameArr));
		}

		function okEvent() {
			saveUser();

			var codeArr = eval($('#hfldCodeJson').val());
			var nameArr = eval($('#hfldNameJson').val());
			var codeCsv = jw.arrayToCsv(codeArr);
			var nameCsv = jw.arrayToCsv(nameArr);

			if (typeof (top.ui) == 'undefined') {
				var human = window.dialogArguments;
				human[0] = codeCsv;
				human[1] = nameCsv;
				window.returnvalue = human;
				window.close();
			} else {
				var winNo = jw.getRequestParam('winNo');
				top.ui.closeWin({ winNo: winNo });
				if (typeof top.ui.callback == 'function') {
					top.ui.callback({ code: codeCsv, name: nameCsv });
				}
			}
		}


		//添加options
		function lbselect(lb1, lb2, type) {
			lbselect2(lb1, lb2, type);
			saveUser();
		}

		function lbselect2(lb1, lb2, type) {
			var a = document.getElementById(lb1);
			var b = document.getElementById(lb2);
			var options = a.getElementsByTagName('OPTION');
			for (var i = 0; i < options.length; i++) {
				if (type == 1) {
					if (options[i].selected) {
						if ($('#hfldCodeJson').val().indexOf(options[i].value) == -1) {
							b.appendChild(options[i]);
							lbselect2(lb1, lb2, type);
						}
						else {
							top.ui.show("该人员已存在!");
						}
					}
				}
				else {
					if ($('#hfldCodeJson').val().indexOf(options[i].value) == -1) {
						b.appendChild(options[i]);
						lbselect2(lb1, lb2, type);
					}
				}
			}
		}

		//删除用户
		function delUser(type) {
			var options = document.getElementById("lbUser").getElementsByTagName('OPTION');
			for (var i = 0; i < options.length; i++) {

				if (type == 1) {
					if (options[i].selected) {
						delVal(options[i].value);
						document.getElementById("lbUser").remove(i);
						delUser(1);
					}
				}
				else {
					delVal(options[i].value);
					document.getElementById("lbUser").remove(i);
					delUser(0);
				}
			}
		}

		function calcelEvent() {
			var winNo = jw.getRequestParam('winNo');
			top.ui.closeWin({ winNo: winNo });
		}
	</script>
</head>
<body style="overflow: hidden;">
	<form id="form1" runat="server">
	<span id="lbmsg"></span>
	<div class="divContent" style="height: 450px; margin-left: 20px; overflow: hidden;">
		<table class="tableContent" cellpadding="5px" cellspacing="0">
			<tr>
				<td style="vertical-align: top; border: solid 0px red; width: 200px; text-align: left;"
					rowspan="3">
					<div style="font-weight: bold; margin-left: 10px; margin-top: 10px; margin-bottom: 10px;">
						请选择</div>
					<div id="departmentDiv" style="height: 400px; width: 200px; overflow: auto; border: solid 1px #cdd4df;">
						<asp:TreeView ID="TvBranch" Width="100%" ShowLines="true" ExpandDepth="1" OnSelectedNodeChanged="trvwDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle CssClass="trvw_select" /></asp:TreeView>
					</div>
				</td>
				<td style="border: solid 0px red; height: 30px; text-align: left; vertical-align: bottom;">
					<div style="margin-left: 10px;">
					</div>
				</td>
			</tr>
			<tr>
				<td style="border: solid 0px red; height: 320px; vertical-align: top;">
					<table style="height: 320px;" cellpadding="0px" cellspacing="0">
						<tr>
							<td style="border: solid 0px red; width: 130px; vertical-align: top; text-align: right;">
								<div style="height: 30px; margin-right: 10px;">
									<asp:TextBox ID="txtQuery" BorderColor="#CDD4DF" Height="15px" Width="95px" runat="server"></asp:TextBox>
								</div>
								<div style="margin-right: 10px;">
									<asp:ListBox ID="lbSelect" Style="border: solid 1px #cdd4df;" DataTextField="v_xm" DataValueField="v_yhdm" Width="100px" Height="330px" SelectionMode="Multiple" runat="server"></asp:ListBox>
								</div>
							</td>
							<td style="border: solid 0px red; width: 20px; text-align: center;">
								<div style="vertical-align: top; border: solid 0px red; height: 130px;">
									<asp:Button ID="btnQuery" Text="查询" OnClick="btnQuery_Click" runat="server" />
								</div>
								<div style="height: 30px;">
									<img src="../../images1/4_03.jpg" alt="添加" style="cursor: pointer;" onclick="lbselect('lbSelect','lbUser',1)"
										id="btnAdd" />
								</div>
								<div style="height: 30px;">
									<img src="../../images1/4_06.jpg" alt="全部" style="cursor: pointer;" onclick="lbselect('lbSelect','lbUser',2)"
										id="btnAddAll" />
								</div>
								<div style="height: 60px;">
								</div>
								<div style="height: 30px;">
									<img src="../../images1/4_08.jpg" alt="删除" style="cursor: pointer;" onclick="delUser(1)"
										id="btnDel" />
								</div>
								<div style="height: 80px; border: solid 0px red;">
									<img src="../../images1/4_10.jpg" alt="全部" style="cursor: pointer;" onclick="delUser(0)"
										id="btnDelAll" />
								</div>
							</td>
							<td style="border: solid 0px red; width: 140px; vertical-align: top; text-align: left;">
								<div style="height: 30px;">
								</div>
								<div style="margin-left: 10px;">
									<asp:ListBox ID="lbUser" Style="border: solid 1px #cdd4df;" Width="100px" Height="330px" DataTextField="v_xm" DataValueField="v_yhdm" SelectionMode="Multiple" runat="server"></asp:ListBox>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="border: solid 0px red; text-align: right;">
					<input type="button" id="btnOk" value="确定" onclick="okEvent();" />
					<input type="button" id="btnCancel" value="取消" onclick="calcelEvent();" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldCodeJson" runat="server" />
	<asp:HiddenField ID="hfldNameJson" runat="server" />
	</form>
</body>
</html>
