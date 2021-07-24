<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryTIEdit.aspx.cs" Inherits="HR_Salary_SalaryTIEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>薪金构成表</title>
<script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
    function OpenFormulaEdit(tempid)
    {
        //window.open("Controls/FormulaEdit.aspx","o");
        var url = "Controls/FormulaEdit.aspx?tid="+tempid;
        return window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:600px;center:1;help:0;status:0;");
    }
               
   function fnUpdate() 
    {
       var aa=jsgs
       var bb=aa.replace("S","")     
       document.getElementById("txtFormula").value=bb
      
    }
    function sel(sign)
    {
        if (document.getElementById("txtItemName")!=null)
        { 
         document.getElementById("txtItemName").value=sign.options[sign.selectedIndex].text
        }
    }
       -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title" colspan="2">薪金构成设置</td>
      </tr>
	    <tr>
	           <td  class="td-label">薪金项目</td>
	           <td>
                   <asp:DropDownList ID="DDLItemID" DataSourceID="SqlLBSalaryItem" DataTextField="ItemName" DataValueField="ItemID" Width="50%" runat="server"></asp:DropDownList></td>
	        </tr>
           <tr>
	        <td class="td-label" style="width:25%">
		        工资项名称\r
	        </td>
	        <td >
		       <asp:TextBox ID="txtItemName" Width="90%" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtItemName" Display="None" ErrorMessage="工资项名称不能为空！" runat="server"></asp:RequiredFieldValidator></td></tr>
	        <tr>
	        <td class="td-label">
		        
	        </td>
	        <td >
                <asp:CheckBox ID="CBIsEdit" Text="是否可编辑" Checked="true" runat="server" /></td></tr>
	        <tr>
	        <td class="td-label">
		        
	        </td>
	        <td >
                <asp:CheckBox ID="CBIsDisplay" Text="是否显示" Checked="true" runat="server" />
            </td>
           </tr>
           
	        <tr>	       
	        <td class="td-label">
		        计算公式
	        </td>
	        <td >
		        <asp:TextBox ID="txtFormula" Width="80%" runat="server"></asp:TextBox>
                <asp:Button ID="BtnFormula" Text="定制公式" OnClick="BtnFormula_Click" runat="server" />
            </td>
           </tr>
             
            	<tr>
	                <td class="td-label">
		                序号
	                </td>
	                <td >
		                <asp:TextBox ID="txtTheOrder" Width="50%" Text="0" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtTheOrder" Display="None" ErrorMessage="序号的数据格式不正确！" ValidationExpression="\d+$" runat="server"></asp:RegularExpressionValidator></td></tr> 
	    <tr>
			<td  align="center" colspan="2" class="td-submit">
                <asp:SqlDataSource ID="SqlLBSalaryItem" SelectCommand="SELECT [ItemID], [ItemName] FROM [HR_Salary_Item] WHERE isvalid = '1'" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;&nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" EnableViewState="false" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
            
            </td>
		</tr>
	   </table>
    </div>
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
