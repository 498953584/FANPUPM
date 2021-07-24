<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileCatalogManageEdit.aspx.cs" Inherits="oa_FileManage_FileCatalogManageEdit" %>

<html>
<head runat="server"><title>管理档案目录</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<base target="_self">
	<script language ="javascript">
	function IsInteger(sourObj)
	{
		if (sourObj.value == "")
		{
			sourObj.value = 0;
		}
		else
		{
		    if (!(new RegExp(/^\d+$/).test(sourObj.value)))
			{
			    alert('数据类型不正确!');
			    sourObj.focus();
			    return;
			 }
		}
	}
	function Datetimes(sourObj)
	{
	   if (sourObj.value == "")
		{
			sourObj.value = 0;
		}
		else
		{
		    if (!(new RegExp(/^\d+$/).test(sourObj.value)))
			{
			    alert('请输数字!');
			    sourObj.focus();
			    return;
			 }
		}
	}
	</script>
</head>
<body scroll="no" class="body_popup">
    <form id="Formx" method="post" runat="server">
    <table class="table-form" id="tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
		<tr>
			<td class="td-head" colSpan="4" height="20">
                管理档案目录
            </td>
		</tr>  
		<tr>
			<td class="td-label" width="20%">
                档案年份</td>
			<td>
			    <asp:TextBox ID="txtFileAge" CssClass="text-input" runat="server"></asp:TextBox></td>
			<td class="td-label" width="20%">
                保存期限</td>
			<td>
                <asp:DropDownList ID="DDLTimeLimit" runat="server"><asp:ListItem Value="1" Text="永久" /><asp:ListItem Value="2" Text="长期" /><asp:ListItem Value="3" Text="短期" /></asp:DropDownList></td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                档案分类</td>
			<td>
                <asp:DropDownList ID="DDLClassID" DataSourceID="SQLDSFileType" DataTextField="ClassName" DataValueField="ClassID" runat="server"></asp:DropDownList>
                <asp:SqlDataSource ID="SQLDSFileType" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [OA_File_Classes] WHERE ([LibraryCode] = @LibraryCode) ORDER BY [ClassCode]" ConnectionString='<%$ ConnectionStrings:Sql %>' runat="server"><SelectParameters><asp:QueryStringParameter Name="LibraryCode" QueryStringField="lc" Type="String"></asp:QueryStringParameter></SelectParameters></asp:SqlDataSource>
            </td>
			<td class="td-label" width="20%">
                档案标题</td>
			<td>
			    <asp:TextBox ID="txtFileName" CssClass="text-input" MaxLength="100" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                卷号</td>
			<td>
			    <asp:TextBox ID="txtVolume" CssClass="text-input" runat="server"></asp:TextBox></td>
			<td class="td-label" width="20%">
                档案盒号</td>
			<td>
			    <asp:TextBox ID="txtBoxNumber" CssClass="text-input" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                件号</td>
			<td>
			    <asp:TextBox ID="txtFileNumber" CssClass="text-input" runat="server"></asp:TextBox></td>
			<td class="td-label" width="20%">
                责任者</td>
			<td>
			    <asp:TextBox ID="txtPrincipal" CssClass="text-input" MaxLength="25" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                密级</td>
			<td>
                <asp:DropDownList ID="DDLSecretLevel" runat="server"><asp:ListItem Value="1" Text="密秘" /><asp:ListItem Value="2" Text="机密" /><asp:ListItem Value="3" Text="绝密" /></asp:DropDownList></td>
			<td class="td-label" width="20%">
                所属项目</td>
			<td>
			    <asp:TextBox ID="txtPrjName" CssClass="text-input" MaxLength="100" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                存放位置</td>
			<td>
			    <asp:TextBox ID="txtSavePlace" CssClass="text-input" MaxLength="100" runat="server"></asp:TextBox></td>
			<td class="td-label" width="20%">
                档案页数</td>
			<td>
			    <asp:TextBox ID="txtPageNumber" CssClass="text-input" runat="server"></asp:TextBox></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                文件字号</td>
			<td>
			    <asp:TextBox ID="txtFileCode" CssClass="text-input" MaxLength="100" runat="server"></asp:TextBox></td>
			<td class="td-label" width="20%">
                归档日期</td>
			<td>
			    <JWControl:DateBox ID="txtPigeonholeDate" ToolTip="点击选择日期" runat="server"></JWControl:DateBox></td>
		</tr>
		<tr>
			<td class="td-label" width="20%">
                文件类型
            </td>
			<td colSpan="3">
                <asp:DropDownList ID="DDLFileType" runat="server"><asp:ListItem Value="1" Text="原件" /><asp:ListItem Value="2" Text="复印件" /></asp:DropDownList></td>
		</tr> 
		<tr>
			<td class="td-label" width="20%">
                备注
            </td>
			<td colSpan="3">
			    <asp:TextBox ID="txtContent" CssClass="text-input" MaxLength="500" style="width:100%" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
			</td>
		</tr> 
		<tr>
			<td class="td-submit" colSpan="4" height="20">
			<asp:Button ID="btnAdd" Text="保 存" OnClick="btnAdd_Click" runat="server" />&nbsp;
			<INPUT id="BtnClose" onclick="javascript:returnValue=false;window.close();" type="button" value="取 消">&nbsp;
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
