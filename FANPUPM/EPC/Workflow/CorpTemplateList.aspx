<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CorpTemplateList.aspx.cs" Inherits="CorpTemplateList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>CorpTemplateList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function doClickRow(businessCode , businessClass ,templateId , isGeneral)
			{
				if (businessClass.length > 0)
				{
				    document.getElementById('btnEdit').disabled = false;
				}
				else
				{
				    document.getElementById('btnEdit').disabled = true;
				}
				document.getElementById('btnDel').disabled = false;				    
				var url;
				document.getElementById('hdnTemplateID').value=templateId;
				url = "FlowChart.aspx?tid="+templateId + "&bncode="+businessCode + "&class=" + businessClass + "&flag=" + isGeneral;
				parent.frames[1].location = url;
			}
			function openRoleEdit(op,businessCode , businessClass)
			{				
				var templateID = 0;
				var isGeneral = document.getElementById('hdnIsGeneral').value;
				if (op==2)
				{
					templateID = document.getElementById('hdnTemplateID').value;
				}	
				var url= "TemplateEdit.aspx?t=" + op + "&ti=" + templateID + "&code="+ businessCode + "&class=" + businessClass+ "&flag=" + isGeneral;
				return window.showModalDialog(url,window,"dialogHeight:300px;dialogWidth:450px;center:1;help:0;status:0;");
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-normal" id="Table1" height="100%" width="100%" border="1">
				<TR>
					<TD class="td-title" align="center" colSpan="3" height="28">流程模板设置</TD>
				</TR>
				<tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
					    <input type="hidden" id="hdnIsGeneral" name="hdnIsGeneral" runat="server" />

						<input type="hidden" id="hdnTemplateID" name="hdnTemplateID" runat="server" />

						<asp:Button ID="btnAdd" Text="新 增" runat="server" />
						<asp:Button ID="btnEdit" Text="修 改" Enabled="false" runat="server" />
						<asp:Button ID="btnDel" Text="删 除" Enabled="false" runat="server" /></td></tr>
				<TR>
					<TD vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
							<asp:DataGrid ID="dgFlow" CssClass="grid" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="名称">
<ItemTemplate>
											<asp:Label ID="TxtFlowName" Width="100%" Text='<%# System.Convert.ToString(Eval("TemplateName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="异常流程">
<ItemTemplate>
                                        <asp:CheckBox ID="CkbIsAbnormality" Enabled="false" Checked='<%# (Eval( "IsAbnormality").ToString() == "1") %>' runat="server" />
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="通用流程" Visible="false">
<ItemTemplate>
                                            <asp:CheckBox ID="CkbIsGeneral" Enabled="false" Checked='<%# (Eval( "IsGeneral").ToString() == "1") %>' runat="server" />
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="子公司编码">
<ItemTemplate>
											<asp:Label ID="TxtCorpCode" Width="100%" Text='<%# System.Convert.ToString(Eval("CorpCode"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
											<asp:Label ID="TxtRemark" Width="100%" Text='<%# System.Convert.ToString(Eval("Remark"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
										</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="完成">
<ItemTemplate>
											<asp:CheckBox ID="CkbIsComplete" Enabled="false" Checked='<%# (Eval( "IsComplete").ToString() == "1") %>' runat="server" />
										</ItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
						</div>
					</TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
