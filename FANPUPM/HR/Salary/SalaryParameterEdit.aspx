<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryParameterEdit.aspx.cs" Inherits="HR_Salary_SalaryParameterEdit" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>计税金额设置</title>
    <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
       -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title"colspan="2">个人所得税设置</td>
      </tr>
     <tr>
           <td class="td-label" style="width:25%">计税金额</td> 
           <td>
               <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td> 
     </tr> 
     </table> 
    
    </div>
    </form>
</body>
</html>
