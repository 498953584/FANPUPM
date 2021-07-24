<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptTree.aspx.cs" Inherits="HR_Salary_DeptTree" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>部门树</title></head>
<body  class="body_frame" scroll="no">
    <form id="form1" runat="server">
   
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td>
                 <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
      <asp:TreeView ID="TVCorp" ShowLines="true" ExpandDepth="1" Width="100%" runat="server"><SelectedNodeStyle BorderColor="Transparent" ForeColor="Red" /></asp:TreeView></div>
     </td>
    </tr>
    </table>  
    
    </form>
</body>
</html>
