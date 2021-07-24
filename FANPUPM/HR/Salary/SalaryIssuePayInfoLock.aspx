<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryIssuePayInfoLock.aspx.cs" Inherits="HR_Salary_SalaryIssuePayInfoLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>员工工资查看</title></head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">     
      <tr>
             <td class="td-head" colspan="2">员工工资查看</td>
      </tr>
      <tr>
        <td class="td-label" style="width:25%">发放日期</td>
        <td>
            <asp:Label ID="LbDate" runat="server"></asp:Label></td>        
      </tr>     
     <tr>
        <td class="td-label" style="width:25%">情况说明</td> 
        <td>
            <asp:Label ID="LbRemark" runat="server"></asp:Label></td>
     </tr>   
    <tr>
           <td colspan="2">
            <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                <asp:Table ID="tbEST" CssClass="grid" Width="100%" CellPadding="0" CellSpacing="0" runat="server"></asp:Table>
            
            </div> 
           </td>
            </tr>
     </table>
    </div>
    </form>
</body>
</html>
