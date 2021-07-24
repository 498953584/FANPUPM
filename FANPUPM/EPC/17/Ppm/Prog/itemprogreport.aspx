<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemprogreport.aspx.cs" Inherits="ItemProgReport" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>ItemProgReport</title>
	<script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="../../../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../../../StockManage/script/JustWinTable.js"></script>
	<script type="text/javascript" src="../../../../StockManage/script/Config2.js"></script>
	<link href="../../../../Script/jquery.tooltip/jquery.tooltip.css" rel="stylesheet" type="text/css" />


	<style type="text/css">
		.dgheader
		{
			background-color: #EEF2F5;
			white-space: nowrap;
			overflow: hidden;
			font-weight: normal;
			text-align: center;
			border-color: #CADEED;
			color: #727FAA;
			padding-left: 6px;
			padding-right: 6px;
		}
	</style>
	  <script type="text/javascript">
	  	addEvent(window, 'load', function () {
	  		// 添加验证
	  		replaceEmptyTable('emptyPurchaseApply', 'tbReport');
	  		var jwTable = new JustWinTable('tbReport');
	  		showTooltip('tooltip', 25);
	  	});
	  
    </script>
</head>
<body>
	<form id="Form1" method="post" runat="server">
	<table id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td class="divHeader" height="25px">
				<font face="宋体">项目处罚汇总</font>
			</td>
		</tr>
		<tr class="td-search" id="TRS" runat="server"><td height="25px" runat="server">
				<table class="queryTable" cellpadding="3px" cellspacing="0px">
					<tr>
						<td class="descTd">
							请选择日期段
							<asp:TextBox ID="txtBeginDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							-
							<asp:TextBox ID="txtEndDate" Width="120px" onclick="WdatePicker()" runat="server"></asp:TextBox>
							<asp:Button ID="btnSearch" Text="查询" OnClick="btnSearch_Click" runat="server" />
						</td>
					</tr>
				</table>
			</td></tr>
		<tr>
			<td valign="top">
				<table class="tab">
					<tr>
						<td style="height: 100%; vertical-align: top;">
							<div>
								<asp:GridView ID="tbReport" CssClass="gvdata" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" OnPageIndexChanging="tbReport_PageIndexChanging" runat="server">
<EmptyDataTemplate>
										<table id="emptyPurchaseApply" class="gvdata">
											<tr class="header">
												<th scope="col" style="width:398px;">
													类别
												</th>
												<th scope="col">
													项目名称
												</th>
												<th scope="col">
													金额
												</th>
											</tr>
										</table>
									</EmptyDataTemplate>
<Columns><asp:BoundField DataField="ProgSortName" HeaderText="类别" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" /><asp:BoundField DataField="PrjName" HeaderText="项目名称" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center" /><asp:BoundField DataField="ProgMoney" HeaderText="金额" HeaderStyle-Width="25px" HeaderStyle-HorizontalAlign="Center" /></Columns><RowStyle CssClass="rowb"></RowStyle><HeaderStyle CssClass="header"></HeaderStyle><FooterStyle CssClass="footer"></FooterStyle></asp:GridView>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
