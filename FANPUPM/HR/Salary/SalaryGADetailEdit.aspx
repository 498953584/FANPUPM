<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryGADetailEdit.aspx.cs" Inherits="HR_Salary_SalaryGADetailEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>工资调整申请明细</title>
<script type="text/javascript" language="javascript">
    <!--    
    window.name = "win";
       function pickPerson()
        {
            var p = new Array();
		    p[0] = "";
		    p[1] = "";
		    var url = "";
		    url = "/CommonWindow/PickSinglePerson.aspx";
		    window.showModalDialog(url,p,"dialogHeight:420px;dialogWidth:430px;center:1;help:0;status:0;");
		    if (p[0]!="")
		    {		       
			    document.getElementById('hdnEmployeeCode').value = p[0];
			    document.getElementById('txtEmployeeCode').value = p[1];				    		  
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
             <td class="td-title" colspan="4">工资调整申请明细</td>
      </tr>
	
	<tr>
	<td class="td-label" style="width:15%;">
        姓名&nbsp;</td>
	<td colspan="3">
		<asp:TextBox ID="txtEmployeeCode" Width="70%" runat="server"></asp:TextBox><asp:HiddenField ID="hdnEmployeeCode" runat="server" />
                <asp:Button ID="btnEmployeeCode" Text="选 择" OnClick="btnEmployeeCode_Click" runat="server" />
       
        </td></tr>
	<tr>
	<td class="td-label"  style="width:15%;">
		部门名称
	</td>
	<td style="width: 35%" >
		<asp:TextBox ID="txtDeptName" Width="90%" Enabled="false" runat="server"></asp:TextBox>
	</td>
	<td class="td-label"  style="width:15%; ">
		岗位名称
	</td>
	<td >
		<asp:TextBox ID="txtDutyName" Width="90%" Enabled="false" runat="server"></asp:TextBox>
	</td></tr>
	<tr>
	<td class="td-label" style="width:15%">
		原标准\r
	</td>
	<td >
		<asp:TextBox ID="txtOldSalary" Width="90%" Enabled="false" Text="0" runat="server"></asp:TextBox>
	</td>
	<td class="td-label" style="width:15%" >
		新标准\r
	</td>
	<td >
		<asp:TextBox ID="txtNewSalary" Width="90%" Text="0" runat="server"></asp:TextBox>
	</td></tr>	
		<tr>
	<td class="td-label" style="width:15%" >
		调整原因
	</td>
	<td colspan="3">
		<asp:TextBox ID="txtAdjustReason" Width="90%" Rows="10" TextMode="MultiLine" runat="server"></asp:TextBox>
	</td></tr>
	    <tr>
			<td  align="center" colspan="4" class="td-submit">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;&nbsp;&nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server"></asp:ValidationSummary>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmployeeCode" Display="None" ErrorMessage="姓名不能为空！" runat="server"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtNewSalary" Display="None" ErrorMessage="新标准数据格试不正确" ValidationExpression="(^[0-9]*$)|(^[0-9]+(.[0-9]*)$)" runat="server"></asp:RegularExpressionValidator></td>
		</tr>
	   </table>
    </div>
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
    <script language="javascript" type="text/javascript">
     <!--
    document.getElementById('txtEmployeeCode').disabled = true;
    -->
    </script>
    
</body>
</html>
