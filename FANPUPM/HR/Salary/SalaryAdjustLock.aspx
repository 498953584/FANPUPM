<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryAdjustLock.aspx.cs" Inherits="HR_Salary_SalaryAdjustLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>无标题页</title></head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title" colspan="4">工资调标准调整</td>
      </tr>
	
	<tr>
	<td class="td-label" style="width:15%;">
        姓名&nbsp;</td>
	<td colspan="3">
		<asp:Label ID="LbEmployeeCode" Width="70%" Enabled="false" runat="server"></asp:Label>
      
        </td></tr>
	<tr>
	<td class="td-label"  style="width:15%;">
		部门名称
	</td>
	<td style="width:35%; ">
		<asp:Label ID="LbDeptName" Width="90%" Enabled="false" runat="server"></asp:Label>
	</td>
	<td class="td-label"  style="width:15%; ">
		岗位名称
	</td>
	<td >
		<asp:Label ID="LbDutyName" Width="90%" Enabled="false" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td class="td-label" style="width:15%">
		原标准\r
	</td>
	<td >
		<asp:Label ID="LbOldSalary" Width="90%" Enabled="false" runat="server">0</asp:Label>
	</td>
	<td class="td-label" style="width:15%" >
		新标准\r
	</td>
	<td >
		<asp:Label ID="LbNewSalary" Width="90%" runat="server">0</asp:Label>
	</td></tr>	
		<tr>
	<td class="td-label">
		调整原因
	</td>
	<td colspan="3">
		<asp:Label ID="LbAdjustReason" Width="90%" Rows="10" TextMode="MultiLine" runat="server"></asp:Label>
	</td></tr>
	<tr>
		<td class="td-label" style="width:15%">
		情况说明
	    </td>
	    <td colspan="3">
		    <asp:Label ID="LbRemark" Width="90%" MaxLength="250" runat="server"></asp:Label>
	    </td>
	</tr>
	   
	   </table>
    </div>
     
    </form>
</body>
</html>
