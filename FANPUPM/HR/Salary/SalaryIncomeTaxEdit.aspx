<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryIncomeTaxEdit.aspx.cs" Inherits="HR_Salary_SalaryIncomeTaxEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>个人所得税设置 </title>
 <script type="text/javascript" language="javascript">
    <!--
    window.name = "win";
       -->
    </script>
</head>
<body class="body_clear" scroll="no">
    <form id="form1" target="win" runat="server">
    <div>
     <table class="table-normal" cellspacing="0" cellpadding="0" width="100%" border="1">
      <tr>
             <td class="td-title" colspan="2">个人所得税设置</td>
      </tr>
	   
                <tr>
	        <td class="td-label" style="width:25%">
		        纳税下限
	        </td>
	        <td >
		        <asp:TextBox ID="txtLowerLimit" Width="200px" Text="0" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtLowerLimit" Display="None" ErrorMessage="纳税下限格式不正确！" ValidationExpression="\d+$" runat="server"></asp:RegularExpressionValidator></td></tr>
	        <tr>
	        <td class="td-label">
		        纳税上限
	        </td>
	        <td >
		        <asp:TextBox ID="txtUpperLimit" Width="200px" Text="0" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtUpperLimit" Display="None" ErrorMessage="纳税上限格式不正确！" ValidationExpression="\d+$" runat="server"></asp:RegularExpressionValidator></td></tr>
	        <tr>
	        <td class="td-label">
		        税率
	        </td>
	        <td >
		        <asp:TextBox ID="txtTaxRate" Width="200px" Text="0" runat="server"></asp:TextBox>%
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtTaxRate" Display="None" ErrorMessage="税率格式不正确！" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0-9])$)" runat="server"></asp:RegularExpressionValidator></td></tr>
	        <tr>
	        <td class="td-label">
		        补偿数\r
	        </td>
	        <td >
		        <asp:TextBox ID="txtDeduct" Width="200px" Text="0" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtDeduct" Display="None" ErrorMessage="税率格式不正确！" ValidationExpression="\d+$" runat="server"></asp:RegularExpressionValidator></td></tr>
	    <tr>
			<td  align="center" colspan="2" class="td-submit">
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
