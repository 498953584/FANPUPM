<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryItemList.aspx.cs" Inherits="HR_Salary_SalaryItemList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>薪酬科目设定</title>
    <script type="text/javascript" language="javascript">
     function ClickRow(recordid)
    {        
          document.getElementById('HdnRecoreId').value = recordid; 
          document.getElementById('btnEdit').disabled = false;  
          document.getElementById('btnDel').disabled = false; 
    }
     //添加\编辑
    function openEdit(t)
    {   
       
        var rid =     document.getElementById('HdnRecoreId').value ;                       
    	if(t=='Add')
    	{
    	    var url = 'SalaryItemEdit.aspx?t='+t+'&rid=0';
    	}
    	else
    	{
    	    var url = 'SalaryItemEdit.aspx?t='+t+'&rid='+rid;
    	} 	    	    	
		return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:400px;center:1;help:0;status:0;");
    } 
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
            <tr>               
                <td class="td-title">
                    薪酬科目设定</td>
            </tr>
                <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnAdd" Text="增 加" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />                                        
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />                    
                    </td>
            </tr>  
            <tr>
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVSalaryItem" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlSalaryItem" Width="100%" OnRowDataBound="GVSalaryItem_RowDataBound" DataKeyNames="ItemID" runat="server"><RowStyle CssClass="grid_row"></RowStyle><Columns><asp:BoundField DataField="ItemID" HeaderText="工资项ID" InsertVisible="false" ReadOnly="true" SortExpression="ItemID" /><asp:BoundField DataField="ItemName" HeaderText="工资项名称" SortExpression="ItemName" /><asp:BoundField DataField="ItemType" HeaderText="工资项类型" SortExpression="ItemType" /><asp:BoundField DataField="IsValid" HeaderText="是否有效" SortExpression="IsValid" /></Columns><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqlSalaryItem" SelectCommand="SELECT [ItemID], [ItemName], [ItemType], [IsValid] FROM [HR_Salary_Item] " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                     </div>
                </td>
           </tr>
       </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
