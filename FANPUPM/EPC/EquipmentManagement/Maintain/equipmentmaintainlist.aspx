<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipmentmaintainlist.aspx.cs" Inherits="EquipmentRecord" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>机械器具维护信息列表</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript">
			function clickRow(maintainUniqueCode)
			{
				document.getElementById('BtnUpdate').disabled = false;
				document.getElementById('BtnDelete').disabled = false;
				document.getElementById('hdnMaintainCode').value = maintainUniqueCode
			}
			function openMaintainEdit(type)
            {
				var url;
				var equipmentCode = document.getElementById('hdnEquipmentCode').value; //设备编号
				var maintainCode = document.getElementById('hdnMaintainCode').value;//维护编号
				url = "MaintainEdit.aspx?t=" + type + "&ec="+equipmentCode+"&mc=" + maintainCode ;
				return window.showModalDialog(url,'机械器具信息','dialogHeight: 400px; dialogWidth: 600px; center: Yes; help: No; resizable: No; status: No;');
            }
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" class="table-normal" cellspacing="0" cellpadding="0" width="100%" height="100%"
				border="1">
				<tr>
					<td class="td-title">机械器具维修信息列表</td>
				</tr>
				<tr>
					<td class="td-toolsbar" align="right">
						<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><input id="hdnMaintainCode" type="hidden" name="hdnMaintainCode" style="WIDTH: 10px" runat="server" />
<input id="hdnEquipmentCode" type="hidden" name="hdnEquipmentCode" style="WIDTH: 10px" size="1" runat="server" />
&nbsp;
						<asp:Button ID="BtnAdd" Text="新 增" OnClick="BtnAdd_Click" runat="server" />&nbsp;
						<asp:Button ID="BtnUpdate" Text="编 辑" Enabled="false" OnClick="BtnUpdate_Click" runat="server" />&nbsp;
						<asp:Button ID="BtnDelete" Text="删 除" Enabled="false" OnClick="BtnDelete_Click" runat="server" />&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td valign="top">
						<div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgdRepairList" Width="100%" AutoGenerateColumns="false" CssClass="grid" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="MaintainType" ReadOnly="true" HeaderText="维修类型"></asp:BoundColumn><asp:BoundColumn DataField="MaintainCost" ReadOnly="true" HeaderText="维修费用"></asp:BoundColumn><asp:BoundColumn DataField="Fault" ReadOnly="true" HeaderText="出现问题"></asp:BoundColumn><asp:BoundColumn DataField="MaintainceMan" ReadOnly="true" HeaderText="维修人"></asp:BoundColumn><asp:BoundColumn DataField="MaintainDate" ReadOnly="true" HeaderText="维修时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="Appraisal" ReadOnly="true" HeaderText="鉴定结果"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
				<tr>
					<td height="20">
						<asp:Label ID="lblState" runat="server"></asp:Label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
