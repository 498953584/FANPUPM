<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PettyCashDetail.aspx.cs" Inherits="PettyCash_PettyCashDetail" %>
<%@ Register TagPrefix="MyUserControl" TagName="usercontrol_audit_ascx" Src="~/UserControl/Audit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>备用金查看</title><link rel="Stylesheet" type="text/css" href="/Script/Print/css/print-preview.css" media="screen" />
<link rel="Stylesheet" type="text/css" href="/Script/Print/css/print.css" media="print" />

	
	<script type="text/javascript" src="/Script/jquery.js"></script>
	<script type="text/javascript" src="/Script/Print/jquery.print-preview.js" charset="utf-8"></script>
	<script type="text/javascript" src="/Script/jw.js"></script>
	<script type="text/javascript" src="/Script/Print/jw.print.js"></script>
	<script type="text/javascript" src="/Script/DecimalInput.js"></script>
    <script type="text/javascript" src="../../StockManage/script/Config2.js"></script>
	<script type="text/javascript" src="../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {
	        // 显示大写金额
	        $('#spanCashWords').html(jw.amountInWords($('#lblCash').html()));

	        // 初始化打印页面样式
	        initialPrint();
	        // 打印
	        $('.tabHeader').css('width', '650px');
	        $('.tab2').css('width', '650px');
	        $('#tabAudit').css('border-top','none');

	        // 打印预览
	        $('#btnPrint').printPreview();
	    });
	</script>
    <script type="text/javascript">
        function printURL() {
            var id = $('#hfldPettyCashIds').val();
            //window.open("/PettyCash/PettyCashDetail2.aspx?showAudit=' + $('#hfldshowAudit').val() + '&ic=" + id);
            var url = '/PettyCash/PettyCashDetail2.aspx?ic=' + id;
            viewopen(url);
        }
    </script>
</head>
<body id="print">
	<form id="form1" runat="server">
	<div style="overflow: auto;">
		<table class="tabHeader" style="width: 650px;">
			<tr>
				<td colspan="3">
					<asp:Label ID="lblTitle" CssClass="title" Text="" runat="server"></asp:Label>
				</td>
				<td id="tdPrint" class="noprint">
					<input type="button" id="btnPrint" value="打印" class="noprint" />
                    <input type="button" id="btnPrint2" value="财务打印" class="noprint" style="width:60px" onclick="printURL()" />
				</td>
			</tr>
			<tr>
				<td style="text-align: left; margin: 0px, 10px,0px, 10px;">
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;付款单位:
					<asp:Label ID="lblPayer" Width="300px" runat="server"></asp:Label>
					&nbsp;&nbsp;&nbsp;&nbsp;申请日期:
					<asp:Label ID="lblApplicationDate" runat="server"></asp:Label>
				</td>
			</tr>
		</table>
		<table class="tab2">
			<tr>
				<td class="descTd" style="width: 16%;">
					&nbsp;&nbsp;&nbsp;申请部门
				</td>
				<td class="inputTd" style="width: 34%;">
					<asp:Label ID="lblDepart" runat="server"></asp:Label>
				</td>
				<td class="descTd" style="width: 16%;">
					&nbsp;&nbsp;&nbsp;申请人
				</td>
				<td class="inputTd" style="width: 34%;">
					<asp:Label ID="lblApplicant" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="descTd" style="">
					<span>* </span>申请人账号
				</td>
				<td>
					<asp:Label ID="lblAccount" runat="server"></asp:Label>
				</td>
				<td class="descTd">
					<span>* </span>开户行
				</td>
				<td>
					<asp:Label ID="lblBank" runat="server"></asp:Label>
				</td>
			</tr>
			<tr>
				<td class="descTd">
					<span>* </span>事项
				</td>
				<td>
					<asp:Label ID="lblMatter" runat="server"></asp:Label>
				</td>
				<td class="descTd">
					&nbsp;&nbsp;&nbsp;项目
				</td>
				<td>
					<asp:Label ID="lblProject" runat="server"></asp:Label>
				</td>
			</tr>
			<tr style="height: 40px;">
				<td class="descTd">
					<span>* </span>本次申请金额
				</td>
				<td style="vertical-align: top;">
					<asp:Label ID="lblCash" CssClass="decimal_input" runat="server"></asp:Label>
					<br />
					<div style="width: 95%; word-break: break-all;">
						<span id="spanCashWords"></span>
					</div>
				</td>
				<td class="descTd">
					<span>* </span>用款日期
				</td>
				<td>
					<asp:Label ID="lblCashDate" runat="server"></asp:Label>
				</td>
			</tr>
            <tr>
            <td>收款单位</td>
            <td colspan="3">
                <asp:Label ID="lblpayee" Text="" runat="server"></asp:Label>
            </td>
            </tr>
			<tr style="min-height: 50px;">
				<td class="descTd">
					&nbsp;&nbsp;&nbsp;申请事由
				</td>
				<td colspan="3">
					<div style="width: 95%; word-break: break-all; word-wrap: break-word; min-height:50px;">
						<asp:Label ID="lblApplicationReason" Style="min-height: 50px; vertical-align: top;" runat="server"></asp:Label>
					</div>
				</td>
			</tr>
		</table>
		<MyUserControl:usercontrol_audit_ascx ID="Audit1" runat="server" />
	</div>
    <asp:HiddenField ID="hfldPettyCashIds" runat="server" />
    <asp:HiddenField ID="hfldshowAudit" runat="server" />
	</form>
</body>
</html>
