<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryTempletEdit.aspx.cs" Inherits="HR_Salary_SalaryTempletEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>薪酬模板信息</title>
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
             <td class="td-title" colspan="2">薪酬模板信息</td>
      </tr>
	    <tr>
	    <td class="td-label" style="width:25%">
            模板名称:</td>
	    <td>
            <asp:TextBox ID="txtTempletName" runat="server"></asp:TextBox>
        </td>        
       
	   </tr>
	   <tr>
	       <td class="td-label" style="height: 23px">适用人员： </td>
	       <td style="height: 23px">
               <asp:DropDownList ID="DDLClassCode" DataSourceID="SqlClass" DataTextField="ClassName" DataValueField="ClassCode" runat="server"></asp:DropDownList><asp:SqlDataSource ID="SqlClass" SelectCommand="SELECT [ClassID], [ClassName], [Remark], [IsValid], [ClassCode], [CorpCode], [ClassTypeCode] FROM [PT_SingleClasses]    WHERE [ClassTypeCode] =
 (select [ClassTypeCode] from PT_D_SingleClass where FilterField = 'HumanType')" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"></asp:SqlDataSource>
           </td>
	   </tr>
	   <tr>
			<td  align="center" colspan="2" class="td-submit">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTempletName" Display="None" ErrorMessage="模板名称不能为空！" runat="server"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" EnableViewState="false" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
            
            </td>
		</tr>
	   </table>
    
    </div>
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
