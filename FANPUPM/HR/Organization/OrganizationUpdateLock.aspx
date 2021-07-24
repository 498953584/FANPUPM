<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrganizationUpdateLock.aspx.cs" Inherits="HR_Organization_OrganizationUpdateLock" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>组织机构变动审核</title></head>
<body class="body_popup">
    <form id="form1" runat="server">
    <div>
     <table class="table-normal" id="tablex"  cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="4" height="20">
                组织机构变动审核
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                流程发起人</td>
            <td colspan="3">
                   <asp:Label ID="LbUserName" style="width:99%" runat="server"></asp:Label>
            </td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                调整时间</td>
			<td>
                <asp:Label ID="LbRecordDate" runat="server"></asp:Label></td>
			<td class="td-label" width="20%">
                申请部门</td>
			<td>
			    <asp:Label ID="LbCorpCode" style="width:99%" runat="server"></asp:Label>
			</td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                调整内容</td>
            <td colspan="3">
                   <asp:Label ID="LbAdjustContent" style="width:99%" runat="server"></asp:Label>
            </td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                调整原因
            </td>
            <td colspan="3">
                <asp:Label ID="LbAdjustReason" style="width:99%" runat="server"></asp:Label>
            </td>
		</tr>   
		<tr>
			<td class="td-label" width="20%">
                情况说明</td>
            <td colspan="3">
                <asp:Label ID="LbRemark" style="width:99%" runat="server"></asp:Label>
            </td>
		</tr> 
		</table>
    </div>
    </form>
</body>
</html>
