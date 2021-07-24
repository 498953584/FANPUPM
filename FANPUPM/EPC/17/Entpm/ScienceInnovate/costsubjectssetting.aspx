<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costsubjectssetting.aspx.cs" Inherits="CostSubjectsSetting" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>CostSubjectsSetting</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script src="../../../Web_Client/Tree.js"></script>
		<script language="javascript">
		function ClickRow(obj,tbName,subjectId,firstNum)
		{
			
			//doClick(obj.parent.parent,tbName);
			document.getElementById("hidSubjectID").value = subjectId;
			document.getElementById("hidFirst").value = firstNum;
			//alert(document.getElementById("hidSubjectID").value);
			return true;
		}
		function DelRow(obj,tbName,subjectId,firstNum)
		{
			//doClick(obj,tbName);
			document.getElementById("hidSubjectID").value = subjectId;
			document.getElementById("hidFirst").value = firstNum;
			if(window.confirm("确定删除！"))
				return true;
			else
				return false;
		}
		</script>
	</head>
	<body class="body_normal" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
			<TABLE id="Table1" class="normal" height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="td-title">费用科目设置</td>
				</tr>
				<TR>
					<TD align="right"><FONT face="宋体"> <input id="hidSubjectID" type="hidden" name="Hidden1" runat="server" />

							<input id="hidFirst" type="hidden" name="Hidden1" runat="server" />
<asp:Button ID="btnNew" Text="新 增" CssClass="button-normal" OnClick="btnNew_Click" runat="server" /></FONT></TD>
				</TR>
				<TR>
					<TD vAlign="top" height="100%"><div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgSubject" CssClass="grid" DataKeyField="SubjectID" AutoGenerateColumns="false" Width="100%" runat="server"><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn><asp:TemplateColumn HeaderText="名称" ItemStyle-HorizontalAlign="Center"><ItemTemplate>
											<asp:Label ID="lbSubjectName" Text='<%# Convert.ToString(Eval("SubjectName")) %>' runat="server"></asp:Label>
										</ItemTemplate><EditItemTemplate>
											<asp:TextBox Width="100%" ID="txtSubjectName" Text='<%# Convert.ToString(Eval("SubjectName")) %>' runat="server"></asp:TextBox>
										</EditItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="操作" ItemStyle-Width="28%"><ItemTemplate>
											<asp:Button ID="btn1" CssClass="button-normal" Text="添加" OnClick="CreateNewItem" runat="server" />
											<asp:Button ID="btn3" CssClass="button-normal" Text="修改" OnClick="EidtItem" runat="server" />
											<asp:Button ID="btn4" CssClass="button-normal" Text="保存" OnClick="SaveItem" runat="server" />
											<asp:Button ID="btn2" CssClass="button-normal" Text="删除" OnClick="DelItem" runat="server" />
											<input type="hidden" id="hidSecNum" value='<%# Convert.ToString(Eval("SecNum")) %>' runat="server" />

											<input type="hidden" id="hidFirstNum" value='<%# Convert.ToString(Eval("FirstNum")) %>' runat="server" />

											<input type="hidden" id="hidIsHaveChild" value='<%# Convert.ToString(Eval("IsHaveChild")) %>' runat="server" />

											<input type="hidden" id="hidIsEdit" name="Hidden1" value='<%# Convert.ToString(Eval("IsEdit")) %>' runat="server" />

										</ItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid>
						</div>
					</TD>
				</TR>
				<TR>
					<TD class="td-page"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
