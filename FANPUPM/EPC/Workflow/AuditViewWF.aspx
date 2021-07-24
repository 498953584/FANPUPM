<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditViewWF.aspx.cs" Inherits="ModuleSet_Workflow_AuditViewWF" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>查看流程状态</title>
	<script type="text/javascript">
		function setHeight() {
			var auditHeight = document.getElementById("td-auditstatus").clientHeight;
			document.getElementById("frmAuditStatus").style.height = auditHeight - 5;

			var flowHeight = document.getElementById("td-flowstatus").clientHeight;
			document.getElementById("frmFlowStatus").style.height = flowHeight - 5;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<table style="height: 100%; width: 100%;">
		<tr style="" id="td-auditstatus">
			<td valign="top" style="border-bottom: solid 2px #cccccc;">
				<iframe id="frmAuditStatus" name="frmAuditStatus" src="about:blank" style="overflow: auto;
					height: 300px;" frameborder="0" width="100%" runat="server"></iframe>
			</td>
		</tr>
		<tr style="height: 35%;">
			<td valign="top" id="td-flowstatus">
				<iframe id="frmFlowStatus" name="frmFlowStatus" src="about:blank" style="overflow: auto;" frameborder="0" width="100%" height="100px" runat="server"></iframe>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
