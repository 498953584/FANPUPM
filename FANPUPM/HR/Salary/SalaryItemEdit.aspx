<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryItemEdit.aspx.cs" Inherits="HR_Salary_SalaryItemEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>薪酬科目设定</title>
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
             <td class="td-title" colspan="2">薪酬科目设定</td>
      </tr>
	    <tr>
	    <td class="td-label" style="width:25%">
            工资项名称:</td>
	    <td>
            <asp:TextBox ID="txtItemName" runat="server"></asp:TextBox>
        </td>        
       
	   </tr>
	    <tr>
            <td  class="td-label" style="width:25%">工资项类型:</td>
            <td>
                <asp:DropDownList ID="DDLItemType" runat="server"><asp:ListItem Value="0" Text="标准工资项" /><asp:ListItem Value="1" Text="可维护项" /></asp:DropDownList></td>
        </tr>
        <tr>
            <td class="td-label"></td>
            <td>
                <asp:RadioButtonList ID="RBIsValid" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="0" Text="无效" /><asp:ListItem Selected="true" Value="1" Text="有效" /></asp:RadioButtonList></td>
        </tr>
	    <tr>
			<td  align="center" colspan="2" class="td-submit">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtItemName" Display="None" ErrorMessage="工资项名称不能为空！" runat="server"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" EnableViewState="false" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
            
            </td>
		</tr>
	   </table>
    </div>
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
