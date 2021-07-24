<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="StartWorkReportView.aspx.cs" Inherits="StartStopReturnWork_StartWorkReportView" %>
<%@ Register TagPrefix="MyUserControl" TagName="stockmanage_common_showaudit_ascx" Src="~/StockManage/Common/ShowAudit.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><title>开工报告</title>
	<style type="text/css" media="print">
		.noprint
		{
			display: none;
		}
	</style>
	<style type="text/css">
		#bllProducer
		{
		}
		#bllProducer td
		{
		}
	</style>
	<script type="text/javascript" src="../Script/jquery.js"></script>
	<script type="text/javascript" src="../StockManage/script/Config2.js"></script>
	<script type="text/javascript">
		addEvent(window, 'load', function () {
			if ($('#hfldPrjState').val() == "5") {
				$('#trWinBid').show();
			} else {
				$('#trWinBid').hide();
			}
		});
	</script>
</head>
<body id="print">
	<form id="form2" runat="server">
	<table class="tab" style="vertical-align: top;">
		<tr>
			<td class="divHeader">
				开工报告
				<input type="button" id="Button1" style="float: right;" class="noprint" onclick="winPrint()"
					value=" 打 印 " />
			</td>
		</tr>
		<tr style="height: 1px;">
			<td>
				<table id="Table1" cellpadding="0" cellspacing="0" style="border-style: none;" class="viewTable">
					<tr>
						<td style="border-style: none;">
							制单人:&nbsp;&nbsp;<asp:Label ID="lblBllProducer" runat="server"></asp:Label>
						</td>
						<td style="border-style: none; text-align: right">
							打印日期:&nbsp;&nbsp;<asp:Label ID="lblPrintDate" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="height: 1px;">
			<td style="vertical-align: top;">
				<table cellpadding="0" cellspacing="0" class="viewTable">
					<tr>
						<td class="descTd">
							附件
						</td>
						<td class="td-content" id="FileUpload1" colspan="3" runat="server">
						</td>
					</tr>
					<tr>
						<td class="descTd">
							建设项目名称
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtPrjName" runat="server"></asp:Literal>
						</td>
						<td class="descTd">
							单项工程名称
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtSingleProjectName" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							工程地点
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtProjectPlace" runat="server"></asp:Literal>
						</td>
						<td class="descTd">
							施工单位
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtConstructionUnit" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							申请开工日期
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtApplyStartDate" runat="server"></asp:Literal>
						</td>
						<td class="descTd">
							实际开工日期
						</td>
						<td class="elemTd">
							<asp:Literal ID="txtRealityStartDate" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd" style="white-space: nowrap;">
							实际负责人
						</td>
						<td class="elemTd">
							<asp:Literal ID="lblActualPrincipal" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="white-space: nowrap;">
							主要负责人
						</td>
						<td class="elemTd">
							<asp:Literal ID="lblMainPrincipal" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr id="trWinBid">
						
						<td class="descTd">
							实施项目部
						</td>
						<td colspan="3">
							<asp:Literal ID="txtImplDep" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							开工项目的<br>
							主要<br />
							工程内容
						</td>
						<td colspan="3" class="elemTd">
							<asp:Literal ID="txtMainContent" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							准备工作情况
						</td>
						<td colspan="3" class="elemTd">
							<asp:Literal ID="txtPrepareCondition" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							存在问题
						</td>
						<td colspan="3" class="elemTd">
							<asp:Literal ID="txtExistenceProblem" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td class="descTd">
							监理单位意见
						</td>
						<td colspan="3" class="elemTd">
							<asp:Literal ID="txtSupervisorUnitOpinion" runat="server"></asp:Literal>
						</td>
					</tr>
					<tr>
						<td align="right" class="descTd">
							建设单位意见
						</td>
						<td colspan="3" class="elemTd">
							<asp:Literal ID="txtBuildUnitOpinion" runat="server"></asp:Literal>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr style="height: 1px;">
			<td>
				<table style="width: 100%;">
					<tr>
						<td class="descTd" style="width: 15%; display: inline;">
							申请开工单位:
						</td>
						<td class="elemTd" style="width: 40%;">
							<asp:Literal ID="txtApplyUnit" runat="server"></asp:Literal>
						</td>
						<td class="descTd" style="width: 10%;">
							审批单位:
						</td>
						<td class="elemTd" style="width: 40%;">
							<asp:Literal ID="txtAuditUnit" runat="server"></asp:Literal>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="vertical-align: top;">
				<table style="width: 100%;">
					<tr>
						<td align="center" style="width: 50%;">
							（盖章） &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日
						</td>
						<td align="center" style="width: 50%;">
							（盖章） &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;日
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr id="trAudit" style="vertical-align: top;">
			<td>
				<MyUserControl:stockmanage_common_showaudit_ascx ID="ShowAudit1" BusiCode="126" BusiClass="001" runat="server" />
			</td>
		</tr>
	</table>
	<asp:HiddenField ID="hfldPrjState" runat="server" />
	<asp:HiddenField ID="hfldStartReportId" runat="server" />
	</form>
</body>
</html>
