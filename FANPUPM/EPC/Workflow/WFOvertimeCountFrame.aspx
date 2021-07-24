<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WFOvertimeCountFrame.aspx.cs" Inherits="ModuleSet_Workflow_WFOvertimeCountFrame" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>流程超时统计</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
       <div>
      <table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td valign="top" width="20%" height="100%">
				<div class="gridBox">
                            <iewc:TreeView ID="TVRole" SelectExpands="true" ExpandLevel="1" runat="server"></iewc:TreeView></div>        
			</td>
			<td valign="top" width="80%">
			<iframe id="frmPage" name="frmPage" src="WFOvertimeCount.aspx" frameborder="0" width="100%" height="100%" runat="server"></iframe>
			</td>
			</tr>
			</table>
    </div>
    </form>
</body>
</html>
