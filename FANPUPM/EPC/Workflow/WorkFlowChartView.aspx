<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlowChartView.aspx.cs" Inherits="ModuleSet_Workflow_WorkFlowChartView" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>流程模板图</title>
    <script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
</head>
<body class="body_frame" scroll="yes">
		<form id="Form1" method="post" runat="server">
			<table class="table-normal" id="Table1" height="100%" width="100%" border="1">
			<tr ><td align="left" style="font-weight:bold;	padding-left: 8px;color:#FF7D00;height:24px;">流程说明：<asp:Label ID="LbRemark" runat="server"></asp:Label></td></tr>
				<tbody>
				    <tr>
					    <td valign="top" height="100%">
						     <div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
                                  <table id="tbFlowChart" border="0" cellpadding="0" cellspacing="0" runat="server"></table>    
                             </div>
					    </td>
				    </tr>
				</tbody>
			</table>
			
		</form>
	</body>
</html>
