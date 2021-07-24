<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeList.aspx.cs" Inherits="NodeList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>NodeList</title>
		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function doClickRow(noteId)
			{
				document.getElementById('btnEdit').disabled = false;
				document.getElementById('hdnNodeID').value=noteId;
				document.getElementById('btn_Del').disabled = false;
			}
			function openRoleEdit(op)
			{
				
				var templateId = document.getElementById('hdnTemplateID').value;;
				var nodeId = 0;
				if (op==2)
				{
					nodeId = document.getElementById('hdnNodeID').value;;
				}
				var url= "NodeEdit.aspx?t=" + op + "&ti=" + templateId+"&ni="+nodeId;
				return window.showModalDialog(url,window,"dialogHeight:200px;dialogWidth:450px;center:1;help:0;status:0;");
			}
			function checkOrder(sourObj)
			{
				if (sourObj.value=="")
				{
					sourObj.value = 0;
				}
				if (!(new RegExp(/^[0-9]*[1-9][0-9]*$/).test(sourObj.value)))
				{
					alert('数据类型不正确！');
					sourObj.focus();
					return;
				}
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-normal" id="Table1" height="100%" width="100%" border="1">
				<TBODY>
					<tr>
						<TD class="td-title" align="left" colSpan="3">流程节点设置</TD></tr>
					<TR>
						<TD class="td-toolsbar"><asp:Button ID="btnSave" Text="保 存" runat="server" /><asp:Button ID="btnAdd" Text="新 增" runat="server" /><asp:Button ID="btnEdit" Text="编 辑" Enabled="false" runat="server" /><asp:Button ID="btn_Del" Text="删 除" Enabled="false" runat="server" /><input id="hdnTemplateID" type="hidden" name="hdnTemplateID" runat="server" />
 <input id="hdnNodeID" type="hidden" name="hdnNodeID" runat="server" />
</TD></TR>
					<TR>
						<TD vAlign="top" height="100%">
							<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="dgFlowChild" DataKeyField="NodeID" PageSize="5" AllowCustomPaging="true" AutoGenerateColumns="false" Width="100%" CssClass="grid" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
												
											</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="NodeName" HeaderText="节点名称"></asp:BoundColumn><asp:TemplateColumn HeaderText="执行人"><ItemTemplate>
												<asp:Label ID="txtName" Text='<%# System.Convert.ToString(Eval("RoleName"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:Label>
											</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="顺序">
<ItemTemplate>
												<asp:TextBox ID="TxtFLowChildXH" Width="100%" Text='<%# System.Convert.ToString(Eval("TheOrder"), System.Globalization.CultureInfo.CurrentCulture) %>' runat="server"></asp:TextBox>
											</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid></div></TD></TR></TBODY></TABLE><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
