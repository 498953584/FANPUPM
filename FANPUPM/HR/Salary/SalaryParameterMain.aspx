<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryParameterMain.aspx.cs" Inherits="HR_Salary_SalaryParameterMain" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>薪酬参数</title>
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
    	    var url = 'SalaryIncomeTaxEdit.aspx?t='+t+'&rid=0';
    	}
    	else
    	{
    	    var url = 'SalaryIncomeTaxEdit.aspx?t='+t+'&rid='+rid;
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
                        薪酬参数
                </td>
            </tr>
            <tr height=22><td  align=right>个税起征点：<asp:TextBox ID="txtTaxLine" Width="80px" runat="server"></asp:TextBox>
                <asp:Button ID="btnSave" Text="保 存" CssClass="button-normal" OnClick="btnSave_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp; <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtTaxLine" Display="None" ErrorMessage="个税起征点格式不正确！" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0-9]*)$)" runat="server"></asp:RegularExpressionValidator>
            </td></tr>
                <tr>
                <td class="td-toolsbar" style="height: 24px">             
                    <asp:Button ID="btnAdd" Text="增 加" OnClick="btnAdd_Click" runat="server" />
                    <asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
                    <asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:HiddenField ID="HdnRecoreId" runat="server" />                    
                    </td>
            </tr>  
            <tr>
                <td>
                     <div id="dvDeptBox" class ="div-scroll" style="WIDTH:100%;HEIGHT:100%">
                         <asp:GridView ID="GVSalaryIncomeTax" AllowPaging="true" AutoGenerateColumns="false" CssClass="grid" DataSourceID="SqlSalaryIncomeTax" Width="100%" OnRowDataBound="GVSalaryIncomeTax_RowDataBound" DataKeyNames="TaxRateID" runat="server"><Columns><asp:BoundField HeaderText="序号" InsertVisible="false" ReadOnly="true" SortExpression="TaxRateID" /><asp:BoundField DataField="LowerLimit" HeaderText="纳税额下限" SortExpression="LowerLimit" /><asp:BoundField DataField="UpperLimit" HeaderText="纳税上限" SortExpression="UpperLimit" /><asp:BoundField DataField="TaxRate" HeaderText="税率" SortExpression="TaxRate" /><asp:BoundField DataField="Deduct" HeaderText="补偿数 " SortExpression="Deduct" /></Columns><RowStyle CssClass="grid_row"></RowStyle><HeaderStyle CssClass="grid_head"></HeaderStyle></asp:GridView>
                         <asp:SqlDataSource ID="SqlSalaryIncomeTax" SelectCommand="SELECT [TaxRateID], [LowerLimit], [UpperLimit], [TaxRate], [Deduct] FROM [HR_Salary_IncomeTax]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
                        
                         
                     </div>
                </td>
           </tr>
       </table>
    </div>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><asp:ValidationSummary ID="ValidationSummary1" EnableViewState="false" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
    </form>
</body>
</html>
