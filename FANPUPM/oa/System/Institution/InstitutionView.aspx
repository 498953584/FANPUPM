<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstitutionView.aspx.cs" Inherits="Enterprise_InstitutionView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>制度查看</title>
	<style type="text/css">
		*
		{
			margin: 0px;
		}
		.div_footer
		{
			height: 20px;
			width: 100%;
			position: absolute;
			bottom: 0px;
			text-align: right;
		}
		input[type=button]
		{
			background: #fff url(/images1/btnBack3.jpg);
			text-align: center;
			vertical-align: middle;
			width: 51px;
			height: 20px;
			border-style: none;
			border-left: solid 1px #dadada;
			border-right: solid 1px #dadada;
		}
	</style>
	<style type="text/css">
		ul, li
		{
			list-style: none;
		}
	</style>
	<script type="text/javascript" src="../../../Script/jquery.js"></script>
	<script type="text/javascript" src="../../../StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		addEvent(window, "load", function () {
			if (document.getElementById("hfldReadOnly").value.toUpperCase() == 'TRUE') {
				//制度只读
				//禁止Copy
				document.body.onselectstart = function () { return false; };
				//禁止右键菜单
				document.oncontextmenu = function () { event.returnValue = false; };
			}

			addEvent(document.getElementById('btnClose'), 'click', function () {
				//没有记录父窗体
				winclose()
			});
		});

        

	</script>
</head>
<body class="body_clear" scroll="auto">
	<form id="form1" runat="server">
	<table style="width: 100%; height: 100%" cellspacing="5" border="0">
		<tr>
			<td style="text-align: center;">
				<div style="margin-top: 28px;">
					<asp:Label ID="lblInsTitle" Font-Bold="true" Font-Size="26px" ForeColor="Red" Font-Names="宋体" runat="server"></asp:Label>
				</div>
				<br />
			</td>
		</tr>
		<tr>
			<td style="text-align: center; font-size: 18px; font-family: 宋体;">
				<div style="margin-top: 20px">
					<asp:Label ID="lblInsCode" Text="" ForeColor="Red" runat="server"></asp:Label>
				</div>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table style="width: 100%;" border="0" cellspacing="0">
					<tr>
						<td style="width: 10%;">
						</td>
						<td style="text-align: right; font-size: 18px; font-family: 宋体; color: #ff0000;">
							<div style="margin-top: 24px; margin-bottom: 10px;">
								签发人：
								<asp:Label ID="lblIssuePerson" Text="" runat="server"></asp:Label>
							</div>
						</td>
						<td style="width: 10%;">
						</td>
					</tr>
					<tr>
						<td style="width: 10%;">
						</td>
						<td style="border-top: solid 3px #ff0000; padding-left: 10px;">
							<div style="line-height: 40px; font-size: 20px; font-family: 楷体; color: #333333;
								margin-top: 10px;">
								<asp:Literal ID="ltrCont" runat="server"></asp:Literal>
							</div>
						</td>
						<td style="width: 10%;">
						</td>
					</tr>
					<tr style="display: none;">
						<td style="width: 10%;">
						</td>
						<td style="text-align: right;">
							签发时间：
							<asp:Label ID="lblIssueDate" Text="" runat="server"></asp:Label>
						</td>
						<td style="width: 10%;">
						</td>
					</tr>
					<tr>
						<td style="width: 10%">
						</td>
						<td>
							<asp:Literal ID="ltrAnnex" runat="server"></asp:Literal>
						</td>
						<td>
						</td>
					</tr>
					<tr id="trBack" style="display: none">
						<td>
						</td>
						<td class="td-submit">
							<a href="javascript:history.back();" target="_self">
								<br />
								<br />
								返 回</a>
						</td>
						<td>
						</td>
					</tr>
					<tr id="trRtn" style="display: none">
						<td>
						</td>
						<td class="td-submit">
							<a href="InstitutionList.aspx" target="_self">
								<br />
								<br />
								返 回</a>
						</td>
						<td>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldReadOnly" runat="server" />
	<div>
		<ul>
			<asp:Literal ID="Literal1" runat="server"></asp:Literal>
		</ul>
	</div>
	<div class="div_footer">
		<input type="button" id="btnClose" value="关闭" />
	</div>
	</form>
</body>
</html>
