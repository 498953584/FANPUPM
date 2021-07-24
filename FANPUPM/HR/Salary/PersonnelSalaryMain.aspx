<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonnelSalaryMain.aspx.cs" Inherits="HR_Salary_PersonnelSalaryMain" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>员工工资模板</title></head>
<body class="body_frame" scroll="no">
    <form id="form1" runat="server">
    <div>
    <table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td valign="top" width="20%" height="100%">
				<iframe id="frmPageTree" name="frmPageTree" src="DeptTree.aspx?t=<%=Request["t"] %>"  frameborder="0" width="100%" height="100%"></iframe>
			</td>
			<td valign="top" width="80%">
			<iframe id="frmPage" name="frmPage" frameborder="0" width="100%" height="100%" runat="server"></iframe>
			</td>
			</tr>
			</table>
    </div>
    </form>
</body>
</html>
