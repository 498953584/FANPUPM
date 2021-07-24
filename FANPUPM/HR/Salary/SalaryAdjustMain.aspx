<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryAdjustMain.aspx.cs" Inherits="HR_Salary_SalaryAdjustMain" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>工资调标准调整</title>
<script type="text/javascript" language="javascript">
     function ClickRow(recordid,as,ic)
    {        
          document.getElementById('HdnRecoreId').value = recordid;  
         // alert(document.getElementById('HdnRecoreId').value) 
         // alert(as)        
         if(as == '-1' )
        {
          document.getElementById('btnEdit').disabled = false;  
          document.getElementById('btnDel').disabled = false; 
          document.getElementById('btnStartWF').disabled = false;  
          document.getElementById('btnViewWF').disabled = true; 
          document.getElementById('btnWFPrint').disabled = true; 
          document.getElementById('BtnIsConfirm').disabled = true;
              
        }
        else
        {
          document.getElementById('btnEdit').disabled = true;  
          document.getElementById('btnDel').disabled = true; 
          document.getElementById('btnStartWF').disabled = true;  
          document.getElementById('btnViewWF').disabled = false; 
          document.getElementById('btnWFPrint').disabled = false;                 
          document.getElementById('BtnIsConfirm').disabled = false;
        }
        document.getElementById('BtnView').disabled = false;
    }
     //添加\编辑
    function openEdit(t)
    {   
       
        var rid =     document.getElementById('HdnRecoreId').value ;                       
    	if(t=='Add')
    	{
    	    var url = 'SalaryAdjustEdit.aspx?t='+t+'&rid=00000000-0000-0000-0000-000000000000';
    	}
    	else
    	{
    	    var url = 'SalaryAdjustEdit.aspx?t='+t+'&rid='+rid;
    	}    	    	
    	//alert(url)
		return window.showModalDialog(url,window,"dialogHeight:450px;dialogWidth:600px;center:1;help:0;status:0;");
    } 
        ///查看审核
    function OpenViewWF(BusinessCode,BusinessClass)
    {
        var rid =  document.getElementById('HdnRecoreId').value ;
      var url = "/EPC/Workflow/AuditViewWF.aspx?ic="+rid+'&bc='+BusinessCode+'&bcl='+BusinessClass;
      return window.showModalDialog(url,window,"dialogHeight:460px;dialogWidth:600px;center:1;help:0;status:0;");		    
    }
    //查看审核记录
    function OpenPrintWF(BusinessCode,BusinessClass)
    {
           var rid =  document.getElementById('HdnRecoreId').value ;
      var url = "/EPC/Workflow/AuditViewPrint.aspx?ic="+rid+'&bc='+BusinessCode+'&bcl='+BusinessClass;
      // window.open(url,"");
     return window.showModalDialog(url,window,"dialogHeight:760px;dialogWidth:800px;center:1;help:0;status:0;");		        
    }
          //查看
    function OpenLock()
    {
      var rid =  document.getElementById('HdnRecoreId').value ;
      var url = "SalaryAdjustLock.aspx?ic="+rid;
      return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:600px;center:1;help:0;status:0;");		        
    }
    
    </script>
</head>
<body  class="body_clear" scroll="no">
    <form id="form1" runat="server">
    <div>
     <table id="Table1" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" border="0" class="table-normal">
           
                <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnAdd" Text="增 加" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />
                     <asp:Button ID="btnStartWF" Text="提交审核" Width="80px" Enabled="false" OnClick="btnStartWF_Click" runat="server" />
                    <asp:Button ID="BtnIsConfirm" Visible="false" Text="确认调整" Enabled="false" OnClick="BtnIsConfirm_Click" runat="server" /> 
                    <asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />
                    
                    <asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />
                    <asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />                   
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />
                    </td>
            </tr>  
            <tr>
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVSalaryAdjust" Width="100%" AutoGenerateColumns="false" DataSourceID="SqSalaryAdjust" AllowPaging="true" CssClass="grid" OnRowDataBound="GVSalaryAdjust_RowDataBound" DataKeyNames="RecordID" runat="server"><Columns><asp:BoundField HeaderText="序号" ReadOnly="true" SortExpression="RecordID" /><asp:BoundField DataField="EmployeeCode" HeaderText="姓名" SortExpression="EmployeeCode" /><asp:BoundField DataField="DeptName" HeaderText="部门名称" SortExpression="DeptName" /><asp:BoundField DataField="DutyName" HeaderText="岗位" SortExpression="DutyName" /><asp:BoundField DataField="OldSalary" DataFormatString="{0:0.00}" HeaderText="原标准" HtmlEncode="false" SortExpression="OldSalary" /><asp:BoundField DataField="NewSalary" DataFormatString="{0:0.00}" HeaderText="新标准" HtmlEncode="false" SortExpression="NewSalary" /><asp:BoundField DataField="RecordDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请日期" HtmlEncode="false" SortExpression="RecordDate" /><asp:BoundField DataField="AuditState" HeaderText="流程状态" HtmlEncode="false" SortExpression="AuditState" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqSalaryAdjust" SelectCommand="SELECT [RecordID], [AuditState], [EmployeeCode], [DeptName], [DutyName], [OldSalary], [AdjustReason], [RecordDate], [UserCode], [IsConfirm], [NewSalary] FROM [HR_Salary_SingleAdjust]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                 
                        
                         
                     </div>
                </td>
           </tr>
       </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
