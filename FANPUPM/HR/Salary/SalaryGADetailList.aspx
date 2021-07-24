<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryGADetailList.aspx.cs" Inherits="HR_Salary_SalaryGADetailList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>成批工资调整申请明细</title>
<script type="text/javascript" language="javascript">
     function ClickRow(did)
    {        
          document.getElementById('HdnDetailID').value = did;    
          document.getElementById('btnEdit').disabled = false;  
          document.getElementById('btnDel').disabled = false; 
        
    }
     //添加\编辑
    function openEdit(t,rid)
    {   
       
        var did =     document.getElementById('HdnDetailID').value ;                       
    	if(t=='Add')
    	{
    	    var url = 'SalaryGADetailEdit.aspx?t='+t+'&rid='+rid+"&did=0";
    	}
    	else
    	{
    	    var url = 'SalaryGADetailEdit.aspx?t='+t+'&rid='+rid+"&did="+did;
    	}    	    	
		return window.showModalDialog(url,window,"dialogHeight:450px;dialogWidth:600px;center:1;help:0;status:0;");
    } 
        ///查看审核
  
    
    </script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
             <tr>               
                <td class="td-title">
                    工资调整申请明细</td>
            </tr>
                <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnAdd" Text="增 加" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />                                     
                    <asp:HiddenField ID="HdnDetailID" runat="server" />
                    </td>
            </tr>  
            <tr>
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                        
                        <asp:GridView ID="GVGroupAdjustDetail" Width="100%" AutoGenerateColumns="false" DataSourceID="SqGroupAdjustDetail" AllowPaging="true" CssClass="grid" OnRowDataBound="GVGroupAdjustDetail_RowDataBound" runat="server">
<EmptyDataTemplate>
                                <table id="Table2" border="1" cellspacing="0" class="grid" rules="all" style="width: 100%;
                                    border-collapse: collapse">
                                    <tr class="grid_head">
                                        <th align="center" scope="col" style="width: 80px">
                                            姓名</th>
                                        <th scope="col">
                                            部门</th>
                                        <th scope="col">
                                            岗位</th>
                                        <th align="right" scope="col">
                                            原标准</th>
                                        <th align="right" scope="col">
                                            新标准</th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
<Columns><asp:BoundField DataField="EmployeeCode" HeaderText="姓名" SortExpression="EmployeeCode" /><asp:BoundField DataField="DeptName" HeaderText="部门" SortExpression="DeptName" /><asp:BoundField DataField="DutyName" HeaderText="岗位" SortExpression="DutyName" /><asp:BoundField DataField="OldSalary" HeaderText="原标准" SortExpression="OldSalary" /><asp:BoundField DataField="NewSalary" HeaderText="新标准" SortExpression="NewSalary" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqGroupAdjustDetail" SelectCommand="SELECT [DetailID], [RecordID], [DeptName], [EmployeeCode], [DutyName], [OldSalary], [AdjustReason], [NewSalary] FROM [HR_Salary_GroupAdjustDetail] " ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                         
                     </div>
                </td>
           </tr>
       </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
