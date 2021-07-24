<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonnelSalaryList.aspx.cs" Inherits="HR_Salary_PersonnelSalaryList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>员工工资列表</title>
 
    <script language="javascript" type="text/javascript">
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
    </script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title" >
                    员工工资列表<asp:HiddenField ID="hdnUserCode" runat="server" />
                </td>
            </tr>
             <tr>
                <td class="td-toolsbar" style="height: 24px">模板选择 :&nbsp;&nbsp;<asp:DropDownList ID="DDLTempID" DataSourceID="SqlTempID" DataTextField="TempletName" DataValueField="TempletID" runat="server"></asp:DropDownList><asp:SqlDataSource ID="SqlTempID" SelectCommand="SELECT [TempletID], [TempletName] FROM [HR_Salary_Templet] WHERE ([BeginDate] <> @BeginDate) and UseState= 1" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="1900-01-01" Name="BeginDate" Type="DateTime" /></SelectParameters></asp:SqlDataSource>
                    &nbsp;
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" />&nbsp;
                    <asp:Button ID="Button1" Text="生成员工资模板" Width="120px" OnClick="Button1_Click" runat="server" />&nbsp;</td></tr>
                <tr><td class="td-toolsbar" style="height: 24px">
                    &nbsp;<asp:Button ID="btnDel" Enabled="false" Text="删 除" OnClick="btnDel_Click" runat="server" />
                    <asp:Button ID="btnSave" Text="保 存" OnClick="btnSave_Click" runat="server" />&nbsp;</td></tr>
            <tr>
               <td>
                <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                    <asp:Table ID="tbEST" CssClass="grid" Width="100%" CellPadding="0" CellSpacing="0" runat="server"></asp:Table>
                    <input id="hidsave" type="hidden" runat="server" />

                    <input id="hidestId" type="hidden" runat="server" />

                    
                
                </div> 
               </td>
            </tr>
     </table>
    </div>
    </form>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
</body>
</html>
