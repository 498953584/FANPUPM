<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocClass.aspx.cs" Inherits="DocClass" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server"><title>公文分类</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<script language="javascript" type="text/javascript">
	function doClickRow(classId)
	{
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('hdnClassID').value=classId;
	}
	function openClassEdit(op,classTypeCode , userCode)
	{				
		var classId = 0;
		if (op==2)
		{
			classId = document.getElementById('hdnClassID').value;
		}	
		var url= "DocClassEdit.aspx?t=" + op + "&ctc=" + classTypeCode + "&code="+ userCode + "&cid="+ classId;
		return window.showModalDialog(url,window,"dialogHeight:200px;dialogWidth:450px;center:1;help:0;status:0;");
	}
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">公文分类</TD>
    </tr>
    <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
						<input type="hidden" id="hdnClassID" name="hdnClassID" runat="server" />

						<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
						<asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
						<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" /></td></tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
				<asp:DataGrid ID="dgClass" CssClass="grid" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgClass_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="分类编号"><ItemTemplate>
								<asp:Label ID="TxtClassCode" Width="100%" Text='<%# Convert.ToString(Eval("ClassCode")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="分类名称"><ItemTemplate>
								<asp:Label ID="TxtClassName" Width="100%" Text='<%# Convert.ToString(Eval("ClassName")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="是否有效">
<ItemTemplate>
								<asp:CheckBox ID="CkbIsValid" Enabled="false" Checked='<%# (Eval( "IsValid").ToString() == "1") %>' runat="server" />
							</ItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
			</div>
		</TD>
    </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
