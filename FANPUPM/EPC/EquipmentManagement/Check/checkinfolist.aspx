<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkinfolist.aspx.cs" Inherits="CheckInfoList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server"><title>机械器具检定信息</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="../../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"></base>
		<script language="javascript">
		
		function clickRow(obj)
			{
				document.getElementById('hdnChickId').value = obj;
				document.getElementById('btnSee').disabled = false;
				document.getElementById('btnDel').disabled = false;
				document.getElementById('btnEdit').disabled = false;
				//alert(obj);
				/*在这之前增加你的处理代码*/
			}
		 function OpType(obj)
		 {
			var Checkid        =document.getElementById('hdnChickId').value;
			var equipementcode  =document.getElementById('hdnequipmentcode').value;
			var url;
			var refresh = false;
			var type = obj
			//if(type =="SEE")
			//{
			switch(type)
			{
			case "SEE":
				url = "CheckAdd.aspx?checkId="+Checkid+"&type=SEE";
				break;
			case "EDIT":
				url = "CheckAdd.aspx?checkId="+Checkid+"&type=EDIT";
				break;
			case "ADD":
				url = "CheckAdd.aspx?ec="+equipementcode+"&type=ADD";
				break;
			} 
			//alert(url);
		    refresh = window.showModalDialog(url,window,'dialogHeight:280px;dialogWidth:600px;center:1;help:0;status:0;');	
		    if (refresh==true)
			{  	   
				return true;
			}
			else
			{
				return false;
			}
			     
		 }
		</script>
	</head>
	<body class="body_popup" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table1" height="100%" cellspacing="0" cellpadding="0" width="100%"
				border="1">
				<tr>
					<td class="td-title">机械器具检定列表</td>
				</tr>
				<tr>
					<td class="td-toolsbar">&nbsp;
						<asp:Button ID="btnAdd" CssClass="button-normal" Text="新  增" OnClick="btnAdd_Click" runat="server" />&nbsp;<asp:Button ID="btnEdit" Enabled="false" CssClass="button-normal" Text="编  辑" OnClick="btnEdit_Click" runat="server" />&nbsp;
						<asp:Button ID="btnSee" Text="查  看" CssClass="button-normal" Enabled="false" runat="server" />&nbsp;<asp:Button ID="btnDel" CssClass="button-normal" Text="删  除" Enabled="false" OnClick="btnDel_Click" runat="server" />&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td valign="top"><font face="宋体">
							<asp:DataGrid ID="GrdEquipmentCheckup" CssClass="grid" Width="100%" DataKeyField="NoteSequenceID" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:BoundColumn DataField="CheckBillCode" HeaderText="检定编号"></asp:BoundColumn><asp:BoundColumn DataField="CheckDate" HeaderText="检定日期" DataFormatString="{0:d}"></asp:BoundColumn><asp:TemplateColumn HeaderText="检定类型">
<ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("InOrOut").Equals(0) ? "内检" : "外检") %>' runat="server"></asp:Label>
										</ItemTemplate>

<EditItemTemplate>
											<asp:TextBox Text='<%# Convert.ToString(Eval("InOrOut").Equals(0) ? "内检" : "外检") %>' runat="server"></asp:TextBox>
										</EditItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="CheckDept" HeaderText="检定单位"></asp:BoundColumn><asp:BoundColumn DataField="CheckPerson" HeaderText="检定人"></asp:BoundColumn><asp:TemplateColumn HeaderText="检定结果">
<ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("Result").Equals(1) ? "合格" : "不合格") %>' runat="server"></asp:Label>
										</ItemTemplate>

<EditItemTemplate>
											<asp:TextBox Text='<%# Convert.ToString(Eval("Result").Equals(1) ? "合格" : "不合格") %>' runat="server"></asp:TextBox>
										</EditItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn Visible="false" HeaderText="是否最新">
<ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("IsLast").Equals(1) ? "是" : "否") %>' runat="server"></asp:Label>
										</ItemTemplate>

<EditItemTemplate>
											<asp:TextBox Text='<%# Convert.ToString(Eval("IsLast").Equals(1) ? "是" : "否") %>' runat="server"></asp:TextBox>
										</EditItemTemplate>
</asp:TemplateColumn></Columns></asp:DataGrid></font></td>
				</tr>
			</table>
			<input id="hdnequipmentcode" type="hidden" name="hdnequipmentcode" runat="server" />

			<input id="hdnChickId" type="hidden" value="0" name="hdnChickId" runat="server" />

		</form>
	</body>
</html>
