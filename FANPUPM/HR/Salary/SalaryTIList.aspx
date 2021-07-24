<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryTIList.aspx.cs" Inherits="HR_Salary_SalaryTIList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>薪金子项</title>
<script type="text/javascript" language="javascript">
     function ClickRow(recordid)
    {        
          document.getElementById('HdnRecoreId').value = recordid; 
          document.getElementById('btnEdit').disabled = false;  
          document.getElementById('btnDel').disabled = false;           
    }
     //添加\编辑
    function openEdit(t,TempletID)
    {   
       
        var rid =     document.getElementById('HdnRecoreId').value ;                       
    	if(t=='Add')
    	{
    	    var url = 'SalaryTIEdit.aspx?t='+t+'&rid=0&tid='+TempletID;
    	}
    	else
    	{
    	    var url = 'SalaryTIEdit.aspx?t='+t+'&rid='+rid+'&tid='+TempletID;
    	} 	    	    	
		return window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:600px;center:1;help:0;status:0;");
    } 
    </script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
           
                <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnAdd" Text="添加薪金子项" Width="100px" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />                    
                    </td>
            </tr>  
            <tr>
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVSalaryTempletItem" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlSalaryTempletItem" Width="100%" OnRowDataBound="GVSalaryTempletItem_RowDataBound" DataKeyNames="TempletID,ItemID" runat="server">
<EmptyDataTemplate>
                                 <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                     border-collapse: collapse">
                                     <tr class="grid_head">
                                         <th scope="col">
                                             薪金子项名称</th>
                                         <th scope="col">
                                             字段编辑</th>
                                         <th scope="col">
                                             计算公式</th>
                                         <th scope="col">
                                             薪金项目</th>
                                         <th scope="col">
                                             序号</th>
                                     </tr>
                                 </table>
                             </EmptyDataTemplate>
<Columns><asp:BoundField DataField="ItemName" HeaderText="薪金子项名称" SortExpression="ItemName" /><asp:TemplateField HeaderText="是否编辑" SortExpression="IsEdit">
<ItemTemplate>
                                         <asp:CheckBox ID="CBIsEdit" Enabled="false" runat="server" />
                                     </ItemTemplate>
</asp:TemplateField><asp:BoundField DataField="Formula" HeaderText="计算公式" SortExpression="Formula" /><asp:BoundField DataField="ItemID" HeaderText="薪金项目" ReadOnly="true" SortExpression="ItemID" /><asp:BoundField DataField="TheOrder" HeaderText="序号" SortExpression="TheOrder" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqlSalaryTempletItem" SelectCommand="SELECT [TempletID], [ItemID], [ItemName], [IsEdit], [IsDisplay], [Formula], [TheOrder] FROM [HR_Salary_TempletItem] where [TempletID] = @TempletID" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="TempletID" QueryStringField="tid"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
                        
                         
                     </div>
                </td>
           </tr>          
       </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
