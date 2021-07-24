<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalaryGroupAdjustEdit.aspx.cs" Inherits="HR_Salary_SalaryGroupAdjustEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>成批工资调整申请</title>
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
             <td class="td-title" colspan="2">管理异动申请</td>
      </tr>
	
	<tr>
	<td class="td-label" style="width:15%;">
        申请人</td>
	    <td>
            <asp:TextBox ID="txtUserCode" Enabled="false" CssClass="text-normal" runat="server"></asp:TextBox>
            &nbsp;
            </td>
            </tr>
	<tr>
	<td class="td-label"  style="width:15%;">
		申请时间
	</td>
	<td>		
		  <JWControl:DateBox ID="DBApplyDate" CssClass="text-normal" runat="server"></JWControl:DateBox>
	</td>
	</tr>
	<tr>
		<td class="td-label" style="width:15%">
		情况说明
	    </td>
	    <td colspan="3">
		    <asp:TextBox ID="txtRemark" Width="90%" MaxLength="250" CssClass="text-normal" Rows="3" TextMode="MultiLine" runat="server"></asp:TextBox>
	    </td>
	</tr>
	    <tr>
			<td  align="center" colspan="4" class="td-submit" style="height: 59px">
					<asp:Button ID="BtnSave" Text="保 存" OnClick="BtnSave_Click" runat="server" />&nbsp;<input id="BtnClose" onclick="window.close();" type="button" value="关 闭"/>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
		</tr>
	   </table>
    </div>
     <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
