<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryTempletMain.aspx.cs" Inherits="HR_Salary_SalaryTempletMain" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>薪酬模板</title>
    <script type="text/javascript" language="javascript">
     function ClickRow(recordid,us)
    {        
          document.getElementById('HdnRecoreId').value = recordid; 
          document.getElementById('btnEdit').disabled = false;  
          document.getElementById('btnDel').disabled = false;           
          document.getElementById('btnUse').disabled = false;
          document.getElementById('frmPage').src = "SalaryTIList.aspx?tid="+recordid+"&us="+us;
          if(us == '1')
          {
              document.getElementById('btnEdit').disabled = true;  
              document.getElementById('btnDel').disabled = true;           
              document.getElementById('btnUse').disabled = true;
              
          }
    }
       //添加\编辑
    function openEdit(t)
    {   
       
     var rid =     document.getElementById('HdnRecoreId').value ;                       
    	if(t=='Add')
    	{
    	    var url = 'SalaryTempletEdit.aspx?t='+t+'&rid=0';
    	}
    	else
    	{
    	    var url = 'SalaryTempletEdit.aspx?t='+t+'&rid='+rid;
    	} 	    	    	
		return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:400px;center:1;help:0;status:0;");
	}
    </script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">                    
                        薪酬模板
                </td>
            </tr>
                <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnAdd" Text="增 加" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;<asp:Button ID="btnUse" Enabled="false" Text="启用" Width="80px" OnClick="btnUse_Click" runat="server" />&nbsp;
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />                    
                    </td>
            </tr>  
            <tr style="height:50%">
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVSalaryTemplet" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="SqlSalaryTemplet" Width="100%" CssClass="grid" OnRowDataBound="GVSalaryTemplet_RowDataBound" DataKeyNames="TempletID" runat="server"><Columns><asp:BoundField HeaderText="序号" InsertVisible="false" ReadOnly="true" SortExpression="TempletID" /><asp:BoundField DataField="TempletName" HeaderText="薪酬模板名称 " SortExpression="TempletName" /><asp:BoundField DataField="UseState" HeaderText="适用人员类别" SortExpression="UseState" /><asp:BoundField DataField="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="建立时间" HtmlEncode="false" SortExpression="RecordDate" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqlSalaryTemplet" SelectCommand="SELECT [TempletID], [TempletName], [UseState], [UserCode], [RecordDate], [ClassCode], [EndDate], [BeginDate] FROM [HR_Salary_Templet] order by RecordDate desc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                         
                     </div>
                </td>
           </tr>
           <tr style="height:50%">
                <td>
                <iframe id="frmPage" name="frmPage" src="SalaryTIList.aspx?tid=0&us=1" frameborder="0" width="100%" height="100%" runat="server"></iframe>
                </td>
           </tr>
       </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
