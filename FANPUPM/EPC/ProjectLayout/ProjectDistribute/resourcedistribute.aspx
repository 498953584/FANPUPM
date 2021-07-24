<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourcedistribute.aspx.cs" Inherits="ResourceDistribute" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>资源配置</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript" type="text/javascript">
			var tempRow = null;
			function ClickRow(obj,noteId,resourceCode)
			{
				tempRow = obj;
				document.getElementById("BtnEdit").disabled = false;
				document.getElementById("BtnDel").disabled = false;
				document.getElementById("hdnNoteId").value = noteId;
				document.getElementById("hdnResourceCode").value = resourceCode;
				clickEdit();
			}
			
			function clickEdit()
			{
				var obj = tempRow;
				document.getElementById("TxtResourceCode").value =obj.cells[1].innerText
				document.getElementById("TxtResourceName").value = obj.cells[2].innerText
				document.getElementById("TxtSpecification").value = obj.cells[3].innerText
				document.getElementById("txtUnit").value = obj.cells[4].innerText
				document.getElementById("TxtPrice").value = obj.cells[5].innerText
				document.getElementById("TxtQuantity").value = obj.cells[6].innerText
				document.getElementById("TxtFee1").value = obj.cells[9].innerText
				document.getElementById("BtnSave").disabled = false;
			}
			
			function clickAdd()
			{
				document.getElementById('hdnNoteId').value = "";
				document.getElementById("BtnSave").disabled = true;
			}
			function pickResource()
			{
				var strGuid ="8D50B405-D418-4fe4-B204-0F654C79EF2B";
				var url= "/EPC/Basic/Resource/pickresource.aspx?ses=" + strGuid + "&rs=4";
				return window.showModalDialog(url,window,"dialogHeight:600px;dialogWidth:800px;center:1;help:0;status:0;");
				//return window.open(url);
			}
			function OpenSchedule(projectCode,taskCode)
			{
				window.location.href = "ScheduleView.aspx?pn=&tc="+taskCode+"&pc="+projectCode;
				return false;
			}
			function OpenResourceAdd(type,projectCode,taskCode)
			{
				if(type == "Add")
				{
					window.parent.location.href = "ScheduleResourceAdd.aspx?vc=896431D1-F875-47EC-8164-CED63F6E65F2&t=1&pc="+projectCode+"&tc="+taskCode;
				}
				if(type == "Budget")
				{
					location.href = "ScheduleResAddFromBudget.aspx?pc="+projectCode+"&tc="+taskCode;
				}
				return false;
			}
			function CheckInputIsFloat(keyCode,e)
			{
				if((keyCode>95 && keyCode<106) || (keyCode>47 && keyCode<58) || keyCode == 8|| keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13)
				{
			    }
				else if(keyCode == 110 || keyCode==190)
				{
					 if(e.value == "" || e.value.indexOf(".") != -1)
					 {
						 return false;
					 }
				} 
				else
				{
					return false;
				}
			}			
		</script>
	</head>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD vAlign="top" height="20">
						<TABLE id="Tablez" cellSpacing="1" cellPadding="1" width="100%" border="1">
							<TR class="td-toolsbar">
								<TD align="left">人材机清单</TD>
								<TD><asp:Button ID="BtnAdd" Text="新  增" OnClick="BtnAdd_Click" runat="server" />&nbsp;<asp:Button ID="BtnEdit" Text="编  辑" Enabled="false" style="DISPLAY:none" runat="server" /><asp:Button ID="BtnDel" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" />&nbsp;
									<input id="hdnResourceCode" style="WIDTH: 10px" type="hidden" name="hdnResourceCode" runat="server" />

									<input id="hdnNoteId" style="WIDTH: 10px" type="hidden" name="hdnNoteId" runat="server" />

									<input id="HdnResourceStyle" style="WIDTH: 10px" type="hidden" name="HdnResourceStyle" runat="server" />
</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" height="75%">
						<DIV class="gridBox" id="DGrdRBSx"><asp:DataGrid ID="DGrdRBS" AutoGenerateColumns="false" Width="100%" CssClass="grid" runat="server"><AlternatingItemStyle CssClass="grid_row"></AlternatingItemStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="ResourceCode" HeaderText="编码"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="名称"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="规格型号"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="单位"></asp:BoundColumn><asp:BoundColumn DataField="UnitPrice" HeaderText="单价(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="Quantity" HeaderText="数量" DataFormatString="{0:F3}"></asp:BoundColumn><asp:BoundColumn DataField="ResourceStyle" HeaderText="类型"></asp:BoundColumn><asp:BoundColumn DataField="Fee" HeaderText="金额(元)" DataFormatString="{0:F3}"></asp:BoundColumn><asp:BoundColumn DataField="Fee1" HeaderText="综合费率"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="NoteID" HeaderText="NoteID"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="UnitID" HeaderText="UnitID"></asp:BoundColumn></Columns></asp:DataGrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="1">
							<TR>
								<TD>编码</TD>
								<TD><asp:TextBox ID="TxtResourceCode" style="BACKGROUND-COLOR:#ffffc0" runat="server"></asp:TextBox></TD>
								<TD>名称</TD>
								<TD><asp:TextBox ID="TxtResourceName" ReadOnly="true" style="BACKGROUND-COLOR:#ffffc0" runat="server"></asp:TextBox></TD>
							</TR>
							<tr>
								<TD>规格型号</TD>
								<TD><asp:TextBox ID="TxtSpecification" style="BACKGROUND-COLOR:#ffffc0" runat="server"></asp:TextBox></TD>
								<TD>单位</TD>
								<TD><asp:TextBox ID="txtUnit" ReadOnly="true" style="BACKGROUND-COLOR:#ffffc0" runat="server"></asp:TextBox></TD>
							</tr>
							<TR>
								<TD>单价(元)</TD>
								<TD><asp:TextBox ID="TxtPrice" runat="server"></asp:TextBox>(元)</TD>
								<TD>数量</TD>
								<TD><asp:TextBox ID="TxtQuantity" runat="server"></asp:TextBox></TD>
							</TR>
							<tr style="DISPLAY:none">
								<td>综合费率</td>
								<td colSpan="3"><asp:TextBox ID="TxtFee1" runat="server"></asp:TextBox></td>
							</tr>
							<TR>
								<TD class="td-toolsbar" align="center" colSpan="4"><asp:Button ID="BtnSave" Text="保  存" Enabled="false" OnClick="BtnSave_Click" runat="server" />&nbsp;<asp:Button ID="BtnExit" Text="取  消" runat="server" />&nbsp;<INPUT onclick="javascript:window.returnValue = true;window.close();" type="button" value="关  闭">&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl><JWControl:PersistScrollPosition ID="PersistScrollPosition1" ControlToPersist="DGrdRBSx" runat="server"></JWControl:PersistScrollPosition></form>
	</body>
</HTML>
