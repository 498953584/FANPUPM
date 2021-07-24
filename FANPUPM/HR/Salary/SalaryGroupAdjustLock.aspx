<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryGroupAdjustLock.aspx.cs" Inherits="HR_Salary_SalaryGroupAdjustLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>工资调整查看</title></head>
<body>
    <form id="form1" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title" colspan="2">管理异动申请</td>
      </tr>
	
	<tr>
	<td class="td-label" style="width:15%;">
        申请人</td>
	    <td>
            <asp:Label ID="LbUserCode" Enabled="false" CssClass="text-normal" runat="server"></asp:Label>           
            </td>
            </tr>
	<tr>
	<td class="td-label"  style="width:15%;">
		申请时间
	</td>
	<td>		
        <asp:Label ID="LbApplyDate" runat="server"></asp:Label></td>
	</tr>
	<tr>
		<td class="td-label" style="width:15%">
		情况说明
	    </td>
	    <td>
		    <asp:Label ID="LbRemark" Width="90%" runat="server"></asp:Label>
	    </td>
	</tr>
	  <tr>
                <td colspan="2">
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                        
                        <asp:GridView ID="GVGroupAdjustDetail" Width="100%" AutoGenerateColumns="false" DataSourceID="SqGroupAdjustDetail" AllowPaging="true" CssClass="grid" OnRowDataBound="GVGroupAdjustDetail_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                    border-collapse: collapse">
                                    <tr class="grid_head">
                                        <th align="center" scope="col" style="width: 80px">
                                            姓名</th>
                                        <th scope="col">
                                            部门</th>
                                        <th scope="col">
                                            岗位</th>
                                        <th align="right" scope="col">
                                            原标准</th>
                                        <th align="right" scope="col">
                                            新标准</th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:BoundField DataField="EmployeeCode" HeaderText="姓名" SortExpression="EmployeeCode" /><asp:BoundField DataField="DeptName" HeaderText="部门" SortExpression="DeptName" /><asp:BoundField DataField="DutyName" HeaderText="岗位" SortExpression="DutyName" /><asp:BoundField DataField="OldSalary" HeaderText="原标准" SortExpression="OldSalary" /><asp:BoundField DataField="NewSalary" HeaderText="新标准" SortExpression="NewSalary" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqGroupAdjustDetail" SelectCommand="SELECT [DetailID], [RecordID], [DeptName], [EmployeeCode], [DutyName], [OldSalary], [AdjustReason], [NewSalary] FROM [HR_Salary_GroupAdjustDetail] " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                         
                     </div>
                </td>
           </tr>
	</table>
    </div>
    </form>
</body>
</html>
