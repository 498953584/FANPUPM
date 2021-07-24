<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateList.aspx.cs" Inherits="TemplateList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>公文模板列表</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

	<script language="javascript" type="text/javascript">
	function doClickRow(templeteId)
	{
		document.getElementById('btnEdit').disabled = false;
		document.getElementById('btnDel').disabled = false;
		document.getElementById('hdnTemplateID').value=templeteId;
	}
	function openClassEdit(op,classId , userCode)
	{				
		var templateId = 0;
		if (op==2)
		{
			templateId = document.getElementById('hdnTemplateID').value;
		}	
		var url= "DocTemplateEdit.aspx?t=" + op + "&cid=" + classId + "&code="+ userCode + "&tid="+ templateId;
		return window.showModalDialog(url,window,"dialogHeight:260px;dialogWidth:450px;center:1;help:0;status:0;");
	}
	</script>
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">公文模板列表</TD>
    </tr>
    <tr id="btnTr" height="24" runat="server"><td class="td-toolsbar" colspan="3" runat="server">
						<input type="hidden" id="hdnTemplateID" name="hdnTemplateID" style="WIDTH: 10px" runat="server" />

						<asp:Button ID="btnAdd" Text="新 增" OnClick="btnAdd_Click" runat="server" />
						<asp:Button ID="btnEdit" Text="修 改" Enabled="false" OnClick="btnEdit_Click" runat="server" />
						<asp:Button ID="btnDel" Text="删 除" Enabled="false" OnClick="btnDel_Click" runat="server" /></td></tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
				<asp:DataGrid ID="dgTemplate" CssClass="grid" DataKeyField="TempletID" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgTemplate_ItemDataBound" OnSelectedIndexChanged="dgTemplate_SelectedIndexChanged" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="模板名称"><ItemTemplate>
								<asp:Label ID="TxtTempletName" Text='<%# Convert.ToString(Eval("TempletName")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="上传人"><ItemTemplate>
								<asp:Label ID="TxtUserCode" Text='<%# Convert.ToString(Eval("UserName")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="UploadTime" HeaderText="上传时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:ButtonColumn Text="选择" DataTextField="OriginalName" HeaderText="附件" CommandName="Select"></asp:ButtonColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
								<asp:Label ID="TxtRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
			</div>
		</TD>
    </tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
