<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryGroupAdjustList.aspx.cs" Inherits="HR_Salary_SalaryGroupAdjustList" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>成批工资调整申请</title>
<script type="text/javascript" language="javascript">
     function ClickRow(recordid,as,ic)
    {            
          document.getElementById('HdnRecoreId').value = recordid;           
         if(as == '-1' || as == '-2')
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
        document.getElementById('frmPage').src = "SalaryGADetailList.aspx?rid="+recordid;
    }
     //添加\编辑
    function openEdit(t)
    {   
       
        var rid =     document.getElementById('HdnRecoreId').value ;                       
    	if(t=='Add')
    	{
    	    var url = 'SalaryGroupAdjustEdit.aspx?t='+t+'&rid=00000000-0000-0000-0000-000000000000';
    	}
    	else
    	{
    	    var url = 'SalaryGroupAdjustEdit.aspx?t='+t+'&rid='+rid;
    	}    		    	
		return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:600px;center:1;help:0;status:0;");
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
      var url = "SalaryGroupAdjustLock.aspx?ic="+rid;
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
                    <asp:Button ID="BtnIsConfirm" Text="确认调整" Enabled="false" runat="server" /> 
                    <asp:Button ID="btnViewWF" Enabled="false" Text="查看审核" Width="80px" runat="server" />
                    
                    <asp:Button ID="btnWFPrint" Text="查看审核记录" Enabled="false" Width="100px" runat="server" />
                    <asp:Button ID="BtnView" Enabled="false" Text="查 看" runat="server" />                   
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />
                    </td>
            </tr>  
            <tr style="height:50%">
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVGroupAdjust" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlGroupAdjust" Width="100%" AllowPaging="true" OnRowDataBound="GVGroupAdjust_RowDataBound" DataKeyNames="RecordID" runat="server"><Columns><asp:BoundField DataField="UserCode" HeaderText="申请人" SortExpression="UserCode" /><asp:BoundField DataField="ApplyDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请时间" HtmlEncode="false" SortExpression="AuditState" /><asp:BoundField DataField="AuditState" HeaderText="流程状态" SortExpression="ApplyDate" /><asp:BoundField DataField="Remark" HeaderText="备 注" SortExpression="Remark" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqlGroupAdjust" SelectCommand="SELECT [RecordID], [AuditState], [ApplyDate], [Remark], [UserCode], [RecordDate], [IsConfirm] FROM [HR_Salary_GroupAdjust] ORDER BY [ApplyDate] desc" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                         
                         
                     </div>
                </td>
           </tr>
           <tr style="height:50%">
               <td><iframe id="frmPage" name="frmPage" src="SalaryGADetailList.aspx?rid=00000000-0000-0000-0000-000000000000" frameborder="0" width="100%" height="100%" runat="server"></iframe></td>
           </tr>
       </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
