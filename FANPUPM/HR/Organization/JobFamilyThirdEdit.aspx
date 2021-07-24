<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobFamilyThirdEdit.aspx.cs" Inherits="HR_Organization_JobFamilyThirdEdit" %>
<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<html>
<head id="Head1" runat="server"><title>岗位分类管理</title><meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
	<script language ="javascript">
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" method="post" runat="server">
    <table class="table-form" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="4" height="20">
                岗位信息</td>
		</tr>  
		<tr>
			<td class="td-label" width="15%">
                岗位编码</td>
			<td width="35%">
			    <asp:TextBox ID="txtRoleTypeCode" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                岗位名称</td>
			<td width="35%">
			    <asp:TextBox ID="txtRoleTypeName" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                职责任务</td>
			<td width="35%">
			    <asp:TextBox ID="txtDutyTask" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                业绩标准</td>
			<td width="35%">
			    <asp:TextBox ID="txtAchievement" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                职业操守</td>
			<td width="35%">
			    <asp:TextBox ID="txtMoralCharacter" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                职业发展</td>
			<td width="35%">
			    <asp:TextBox ID="txtExpand" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                年龄要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtAgeRequet" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                性别要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtSexRequest" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                身高要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtStatureRequest" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                体重要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtAvoirdupois" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                听力要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtAudition" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                视力要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtEye" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                学历要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtDucationalBackground" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                经历要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtExperience" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                语言要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtTalk" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
			<td class="td-label" width="15%">
                沟通要求</td>
			<td width="35%">
			    <asp:TextBox ID="txtCommunicate" CssClass="text-input" MaxLength="25" style="width:99%" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                岗位说明书</td>
			<td colSpan="3">
			    <asp:TextBox ID="txtDutyExplain" CssClass="text-input" MaxLength="100" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                任职资格</td>
			<td colSpan="3">
			    <asp:TextBox ID="txtCompetency" CssClass="text-input" MaxLength="100" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                职位描述</td>
			<td colSpan="3">
			    <asp:TextBox ID="txtDutyDescribe" CssClass="text-input" MaxLength="100" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-label" width="15%">
                备注</td>
			<td colSpan="3">
			    <asp:TextBox ID="txtRemark" CssClass="text-input" MaxLength="100" style="width:99%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="td-submit" colSpan="4" height="20">
			<asp:Button ID="btnAdd" Text="保  存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="取消">
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
