<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contactcorplist.aspx.cs" Inherits="ContactCorpList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>往来单位信息</title>
		<META http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<META http-equiv="Expires" content="0">
		<META http-equiv="Cache-Control" content="no-cache">
		<META http-equiv="Pragma" content="no-cache">
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function doClickRow(corpID,corpName,auditStat)
			{				
				document.getElementById('HdnCorpID').value = corpID;			
				document.getElementById('HdnCorpName').value = corpName;
				//alert(auditStat);
				
//				if (auditStat!="1")
//				{
//				//document.getElementById('BtnMod').disabled = false;
//				//document.getElementById('BtnDel').disabled = false;
//				}
//				else
//				{
//				document.getElementById('BtnMod').disabled = true;
//				document.getElementById('BtnDel').disabled = true;
//				}

			}
	
			function openContractEdit(op)
			{				
				var corpID = 0;
				if (op==2)
				{
					corpID = document.getElementById('HdnCorpID').value;
				}				
				var corpType = document.getElementById('HdnTypeID').value;				
				var url= "/EPC/Basic/ContactCorpEdit.aspx?t=" + op + "&ci=" + corpID +"&cti=" + corpType;				
				return window.showModalDialog(url,window,"dialogHeight:410px;dialogWidth:600px;center:1;help:0;status:0;");
			}
			
			function confirmCorp()
			{
				corpID = document.getElementById('HdnCorpID').value;
				corpName = document.getElementById('HdnCorpName').value;
				doDblClickRow(corpID,corpName);				
			}
			
			function doDblClickRow(corpID,corpName)
			{
				var corp = parent.window.dialogArguments;
				corp[0] = corpID;
				corp[1] = corpName;
				parent.window.returnvalue= corp;
				window.close();
			}
			function dbclose()
			{
				var strdate = parent.window.dialogArguments;
				strdate[0] = document.getElementById('HdnCorpName').value;
				window.close();
				//alert(strdate[0]); 
			}
		</script>
	</HEAD>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="td-title">往 来 单 位 信 息
					</td>
				</tr>
				<tr>
					<td class="td-toolsbar"><input id="HdnCorpID" style="WIDTH: 10px" type="hidden" name="HdnCorpID" runat="server" />

						<input id="HdnTypeID" style="WIDTH: 10px" type="hidden" name="HdnTypeID" runat="server" />

						<input id="HdnCorpName" style="WIDTH: 10px" type="hidden" name="HdnCorpName" runat="server" />

						<asp:Button ID="BtnAdd" Text="新  增" Visible="false" OnClick="BtnAdd_Click" runat="server" />
						<asp:Button ID="BtnMod" Text="编  辑" Enabled="false" Visible="false" OnClick="BtnMod_Click" runat="server" />
						<asp:Button ID="BtnDel" Text="删  除" Enabled="false" Width="0px" OnClick="BtnDel_Click" runat="server" />
					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<div class="gridBox"><asp:DataGrid ID="DgrdList" DataKeyField="CorpID" CssClass="grid" AutoGenerateColumns="false" Width="100%" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CorpName" HeaderText="单位名称"></asp:BoundColumn><asp:BoundColumn DataField="CorpKind" HeaderText="单位性质"></asp:BoundColumn><asp:BoundColumn DataField="WorkType" HeaderText="经营类别"></asp:BoundColumn><asp:BoundColumn DataField="Speciality" HeaderText="专业特长"></asp:BoundColumn><asp:BoundColumn DataField="LinkMan" HeaderText="联系人"></asp:BoundColumn><asp:BoundColumn DataField="Telephone" HeaderText="联系电话"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
				<tr><td class=td-submit>
						<asp:Button ID="BtnOK" Visible="false" Text="确 定" runat="server" />&nbsp;
						
						<asp:Button ID="BtnClose" Visible="false" Text="取 消" runat="server" />&nbsp;
				</td></tr>
			</table>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></form>
	</body>
</HTML>
