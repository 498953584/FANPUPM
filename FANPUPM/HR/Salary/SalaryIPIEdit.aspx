<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryIPIEdit.aspx.cs" Inherits="HR_Salary_SalaryIPIEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>工资调整</title>
     <script language="javascript" type="text/javascript">
     <!--
    window.name = "win"; 
      function ClickRow(uc)
    {        
          document.getElementById('hdnUserCode').value = uc;         
          document.getElementById('btnDel').disabled = false; 
        
    }
    function real_onblur(obj) 
    {
        var my_real;
        my_real=obj.value;

        if (my_real=="")  { 
          obj.value=0;
          return true;
        }  

        if (isNaN(parseFloat(my_real)))
        {
          alert("请正确输入数字型的小数！");
          obj .select();
          obj.focus();
          return false;  
        }
        s="0123456789.+-";
        myflag=0;
        l=my_real.length;         
        for(j=0;j<l;j++)
        {
           if(my_real.charAt(j)=='.')
              myflag=myflag+1; 
           if(s.search(my_real.charAt(j))==-1)
               myflag=4;             
        }  
        if(myflag>=2)
        {
          alert("请正确输入数字型的小数！");
          obj .select();
          obj.focus();
          return false; 
        } 
        return true;

     }
    --> 
    </script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
     
      <tr>               
        <td class="td-title" >
            工资调整<asp:HiddenField ID="hdnUserCode" runat="server" />
        </td>
      </tr>
    <tr>
        <td class="td-toolsbar" style="height: 24px">
            &nbsp;<asp:Button ID="btnDel" Enabled="false" Text="删 除" OnClick="btnDel_Click" runat="server" />
            <asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />&nbsp;<input
                id="btnClose" type="button" value="关 闭" onclick = "returnValue=true;window.close();";/>&nbsp;
        </td>
    </tr>
            
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
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
