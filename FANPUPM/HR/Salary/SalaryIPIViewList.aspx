<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryIPIViewList.aspx.cs" Inherits="HR_Salary_SalaryIPIViewList" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>员工资详细信息</title></head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
     <tr>
               <td>
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
