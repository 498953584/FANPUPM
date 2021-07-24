<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSet.aspx.cs" Inherits="UserSet" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title></title>
	<style type="text/css">
		table
		{
			width: 100%;
		}
		.style1
		{
			height: 41px;
		}
		.style2
		{
			height: 41px;
			width: 108px;
			text-align: right;
		}
		.style3
		{
			width: 108px;
			text-align: right;
		}
	</style>
	<script src="../Script/jquery.js" type="text/javascript"></script>
	<script type="text/javascript">
		function success() {
			// 重新刷新我的桌面，采用下面的方法可以异步刷新，但是.my-dest没有从新计算
			top.ui.winSuccess({ parentName: 'myTab' });
			//			top.ui.closeWin();
			//			top.ui.show("操作成功");
			//			top.ui.generateDeskTop(); // 重新生成我的桌面
		}

		function failuer() {
			top.ui.closeWin();
			top.ui.alert("操作失败");
		}

	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table class="" style="width: 100%; height: 80%;" cellspacing="3px" cellpadding="3px">
		<tr>
			<td class="style2">
				列数 &nbsp;
			</td>
			<td class="style1">
				<asp:DropDownList ID="GirdColNum" Width="130px" runat="server"><asp:ListItem Value="2" Text="2" /><asp:ListItem Value="3" Text="3" /><asp:ListItem Value="4" Text="4" /><asp:ListItem Value="5" Text="5" /><asp:ListItem Value="6" Text="6" /><asp:ListItem Value="7" Text="7" /><asp:ListItem Value="8" Text="8" /></asp:DropDownList>
				&nbsp;
			</td>
		</tr>
		<tr>
			<td class="style3">
				栏目宽度 &nbsp;
			</td>
			<td class="txt">
				<asp:DropDownList ID="GirdWidth" Width="130px" runat="server"><asp:ListItem Value="250" Text="250" /><asp:ListItem Value="260" Text="260" /><asp:ListItem Value="270" Text="270" /><asp:ListItem Value="280" Text="280" /><asp:ListItem Value="290" Text="290" /><asp:ListItem Value="300" Text="300" /><asp:ListItem Value="310" Text="310" /><asp:ListItem Value="320" Text="320" /><asp:ListItem Value="330" Text="330" /><asp:ListItem Value="340" Text="340" /><asp:ListItem Value="350" Text="350" /><asp:ListItem Value="360" Text="360" /><asp:ListItem Value="370" Text="370" /><asp:ListItem Value="380" Text="380" /><asp:ListItem Value="390" Text="390" /><asp:ListItem Value="400" Text="400" /><asp:ListItem Value="410" Text="410" /><asp:ListItem Value="420" Text="420" /><asp:ListItem Value="430" Text="430" /><asp:ListItem Value="440" Text="440" /><asp:ListItem Value="450" Text="450" /></asp:DropDownList>
				&nbsp;
			</td>
		</tr>
		<tr class="word" style="display: none;">
			<td class="style3">
				栏目列间隙宽度 &nbsp;
			</td>
			<td class="txt">
				<asp:DropDownList ID="ColGapWidth" Width="130px" runat="server"><asp:ListItem Value="10" Text="10" /><asp:ListItem Value="20" Text="20" /><asp:ListItem Value="30" Text="30" /><asp:ListItem Value="40" Text="40" /><asp:ListItem Value="50" Text="50" /><asp:ListItem Value="60" Text="60" /><asp:ListItem Value="70" Text="70" /><asp:ListItem Value="80" Text="80" /><asp:ListItem Value="90" Text="90" /><asp:ListItem Value="100" Text="100" /></asp:DropDownList>
				&nbsp;
			</td>
		</tr>
		<tr class="word" style="display: none;">
			<td class="style3">
				栏目行间隙宽度 &nbsp;
			</td>
			<td class="txt">
				<asp:DropDownList ID="RowGapWidth" Width="130px" runat="server"><asp:ListItem Value="10" Text="10" /><asp:ListItem Value="20" Text="20" /><asp:ListItem Value="30" Text="30" /><asp:ListItem Value="40" Text="40" /><asp:ListItem Value="50" Text="50" /><asp:ListItem Value="60" Text="60" /><asp:ListItem Value="70" Text="70" /><asp:ListItem Value="80" Text="80" /><asp:ListItem Value="90" Text="90" /><asp:ListItem Value="100" Text="100" /></asp:DropDownList>
				&nbsp;
			</td>
		</tr>
		<tr>
			<td class="style3">
				表内记录数 &nbsp;
			</td>
			<td class="txt">
				<asp:DropDownList ID="RowInGrid" Width="130px" runat="server"><asp:ListItem Value="5" Text="5" /><asp:ListItem Value="6" Text="6" /><asp:ListItem Value="7" Text="7" /><asp:ListItem Value="8" Text="8" /><asp:ListItem Value="9" Text="9" /><asp:ListItem Value="10" Text="10" /></asp:DropDownList>
				&nbsp;
			</td>
		</tr>
		<tr style="display: none;">
			<td class="style3">
				隐藏空栏目 &nbsp;
			</td>
			<td>
				<asp:CheckBox ID="CheckBox1" runat="server" />
			</td>
		</tr>
	</table>
	</div>
	<div id="divFooter" class="divFooter2">
		<table class="">
			<tr>
				<td>
					<asp:Button ID="Btndefault" Width="100px" Text="恢复默认布局" OnClick="Btndefault_Click" runat="server" />
					<asp:Button ID="BtnSave" Text="保存" OnClick="BtnSave_Click" runat="server" />
					<asp:Button ID="BtnClear" Text="取消" OnClientClick="top.ui.closeWin()" runat="server" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
