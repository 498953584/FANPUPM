<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DivSetRole.aspx.cs" Inherits="Common_DivSetRole" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>权限设置</title>
	<style type="text/css">
		#departmentDiv
		{
			width: 40%;
			height: 100%;
			float: left;
			overflow: auto;
			border-right: solid 1px #AAAAAA;
		}
		#userDiv
		{
			width: 25%;
			height: 100%;
			float: left;
			overflow: auto;
			border-right: solid 1px #AAAAAA;
		}
		#userNameDiv
		{
			width: 34%;
			height: 100%;
			float: left;
			overflow: auto;
			border-right: solid 1px #AAAAAA;
		}
	</style>
	<script type="text/javascript" src="/StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="/StockManage/script/JustWinTable.js"></script>
	<script src="/Script/jquery.js" type="text/javascript"></script>
	<script src="/Script/jwJson.js" type="text/javascript"></script>
	<script type="text/javascript">

		//把值存到隐藏字段里
		function setVal() {
			var a = document.getElementById('lblUserNames');
			var options = a.getElementsByTagName('OPTION');
			document.getElementById('hfldUserCodes').value = "";
			for (var i = 0; i < options.length; i++) {
				if (i < options.length - 1) {
					document.getElementById("hfldUserCodes").value += options[i].value + "\",\"";
				}
				else {
					document.getElementById("hfldUserCodes").value += options[i].value;
				}
			}
		}
		//添加options
		function lbselect(lb1, lb2, type) {
			lbselect2(lb1, lb2, type);
			setVal();
		}
		function lbselect2(lb1, lb2, type) {
			var a = document.getElementById(lb1);
			var b = document.getElementById(lb2);
			var options = a.getElementsByTagName('OPTION');
			for (var i = 0; i < options.length; i++) {
				if (type == 1) {
					if (options[i].selected) {
						if (document.getElementById('hfldUserCodes').value.indexOf(options[i].value) == -1) {
							b.appendChild(options[i]);
							lbselect2(lb1, lb2, type);
						}
						else {
							alert("该人员已在右侧列表存在!");
						}

					}
				}
				else {
					if (document.getElementById('hfldUserCodes').value.indexOf(options[i].value) == -1) {
						b.appendChild(options[i]);
						lbselect2(lb1, lb2, type);
					}
				}

			}
		}
		//删除用户
		function delUser(type) {
			var options = document.getElementById("lblUserNames").getElementsByTagName('OPTION');
			for (var i = 0; i < options.length; i++) {
				if (type == 1) {
					if (options[i].selected) {
						if (options[i].value == '00000000') {
							alert("管理员不能删除!");
						}
						else {
							delVal(options[i].value);
							document.getElementById("lblUserNames").remove(i);
							delUser(1);
						}
					}
				}
				else {
					document.getElementById("lblUserNames").remove(i);
					document.getElementById("hfldUserCodes").value = '00000000';
					delUser(0);
				}
			}
		}

		//从隐藏字段里删除
		function delVal(id) {
			var str = document.getElementById("hfldUserCodes").value;
			var val = str.substring(1, str.length - 1).split(',');
			for (var i = 0; i < val.length; i++) {
				if (id == val[i].substring(1, val[i].length - 1)) {
					document.getElementById("hfldUserCodes").value = document.getElementById("hfldUserCodes").value.replace("," + val[i], "");
				}
			}
		}

		function saveEvent() {
			top.ui.show("设置成功");
			top.ui.closeWin();
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div class="divContent" style="height: 450px; margin-left: 20px; overflow: hidden;">
		<table class="tableContent" cellpadding="0px" cellspacing="0">
			<tr>
				<td style="vertical-align: top; border: solid 0px red; width: 200px; height: 400px;
					text-align: left;" rowspan="3">
					<div style="font-weight: bold; margin-left: 10px; margin-top: 10px; margin-bottom: 10px;">
						请选择</div>
					<div id="departmentDiv" style="width: 200px; height: 380px;">
						<asp:TreeView ID="trvwDepartment" ShowLines="true" OnSelectedNodeChanged="trvwDepartment_SelectedNodeChanged" runat="server"><SelectedNodeStyle Font-Underline="true" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" /></asp:TreeView>
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
									<asp:Button ID="Button1" Text="查询" OnClick="btnQuery_Click" runat="server" />
								</div>
								<div style="height: 30px;">
									<img src="../../../images1/4_03.jpg" alt="添加" style="cursor: pointer;" onclick="lbselect('lbSelect','lblUserNames',1)"
										id="btnAdd" />
								</div>
								<div style="height: 30px;">
									<img src="../../../images1/4_06.jpg" alt="全部" style="cursor: pointer;" onclick="lbselect('lbSelect','lblUserNames',2)"
										id="btnAddAll" />
								</div>
								<div style="height: 60px;">
								</div>
								<div style="height: 30px;">
									<img src="../../../images1/4_08.jpg" alt="删除" style="cursor: pointer;" onclick="delUser(1)"
										id="btnDel" />
								</div>
								<div style="height: 80px; border: solid 0px red;">
									<img src="../../../images1/4_10.jpg" alt="全部" style="cursor: pointer;" onclick="delUser(0)"
										id="btnDelAll" />
								</div>
							</td>
							<td style="border: solid 0px red; width: 140px; vertical-align: top; text-align: left;">
								<div style="height: 30px;">
								</div>
								<div style="margin-left: 10px;">
									<asp:ListBox ID="lblUserNames" Style="border: solid 1px #cdd4df;" Width="100px" Height="330px" DataTextField="v_xm" DataValueField="v_yhdm" SelectionMode="Multiple" runat="server"></asp:ListBox>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="text-align: right;">
					<asp:Button ID="btnSave" Text="保存" OnClick="btnSave_Click" runat="server" />
					<input type="button" value="取消" id="btnCanel" onclick="top.ui.closeWin();" />
				</td>
			</tr>
		</table>
	</div>
	<asp:HiddenField ID="hfldUserCodes" runat="server" />
	<asp:HiddenField ID="hdAdmDel" runat="server" />
	</form>
</body>
</html>
