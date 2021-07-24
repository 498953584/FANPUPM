<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryCount.aspx.cs" Inherits="HR_Salary_SalaryCount" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>工资统计</title>
    <script language="javascript" type="text/javascript">
       function SelDept()
    {
        var url = '/CommonWindow/DeptSing.aspx';       
       var al = new Array();
       al[0] = "";
       al[1] = "";
       window.showModalDialog(url,al,"dialogHeight:450px;dialogWidth:360px;center:1;help:0;status:0;");       
       if(al[0]!="")
       {
            document.getElementById('txtDeptName').value = al[0];
            document.getElementById('hdnDept').value = al[1];        
        }
        
    }
    </script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
         <tr>               
            <td class="td-title">
                工资统计</td>
        </tr>
          <tr>
                <td class="td-toolsbar" style="height: 24px">             
                   模板选择 :&nbsp;&nbsp;<asp:DropDownList ID="DDLTempID" DataSourceID="SqlTempID" DataTextField="TempletName" DataValueField="TempletID" runat="server"></asp:DropDownList>&nbsp; 年份：<asp:DropDownList ID="DDLBeginYear" runat="server"></asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLBeginMonth" runat="server"><asp:ListItem Value="1" /><asp:ListItem Value="2" /><asp:ListItem Value="3" /><asp:ListItem Value="4" /><asp:ListItem Value="5" /><asp:ListItem Value="6" /><asp:ListItem Value="7" /><asp:ListItem Value="8" /><asp:ListItem Value="9" /><asp:ListItem Value="10" /><asp:ListItem Value="11" /><asp:ListItem Value="12" /></asp:DropDownList>
                    月至
                    <asp:DropDownList ID="DDLEndYear" runat="server"></asp:DropDownList>
                    年\r
                    <asp:DropDownList ID="DDLEndMonth" runat="server"><asp:ListItem Value="1" /><asp:ListItem Value="2" /><asp:ListItem Value="3" /><asp:ListItem Value="4" /><asp:ListItem Value="5" /><asp:ListItem Value="6" /><asp:ListItem Value="7" /><asp:ListItem Value="8" /><asp:ListItem Value="9" /><asp:ListItem Value="10" /><asp:ListItem Value="11" /><asp:ListItem Value="12" /></asp:DropDownList>&nbsp; 月部门：<asp:TextBox ID="txtDeptName" Width="90px" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnSelect" Text="选 择" runat="server" />
                    <asp:Button ID="btnCount" Text="工资统计" OnClick="btnCount_Click" runat="server" />&nbsp;
                    <asp:SqlDataSource ID="SqlTempID" SelectCommand="SELECT [TempletID], [TempletName] FROM [HR_Salary_Templet] WHERE ([BeginDate] <> @BeginDate) and usestate=1" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:Parameter DefaultValue="1900-01-01" Name="BeginDate" Type="DateTime" /></SelectParameters></asp:SqlDataSource>
                    <asp:HiddenField ID="hdnDept" runat="server" />
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
</body>
</html>
