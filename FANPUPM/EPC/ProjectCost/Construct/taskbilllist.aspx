<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taskbilllist.aspx.cs" Inherits="TaskBillList" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_filelink_ascx" Src="~/EPC/UserControl1/FileLink.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>施工任务书</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self"/>
		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<SCRIPT language="javascript">
		 function doClickRow(strTaskBookCode,t)
		 {
			document.getElementById('HdnNoteID').value = strTaskBookCode;
			document.getElementById('HdnTaskBookCode').value = strTaskBookCode;
			if(t != "1")
			{
				document.getElementById('BtnEdit').disabled = false;			
			}
			else
			{
				document.getElementById('BtnMod').disabled = false;
				document.getElementById('BtnDel').disabled = false;
			}
		 }
		 function doTaskBook(t,prjcode)
		 {
			var vtbc = document.getElementById('HdnTaskBookCode').value;
			if(t == 1)	//添加
			{					
				window.document.location.href = "TaskBillAdd.aspx?pc="+prjcode;								
			}
			else if( t == 2)
			{
				var NoteID = document.getElementById('HdnNoteID').value;				
				window.document.location.href = "TaskBookEdit.aspx?pc="+prjcode+"&nd="+NoteID+"&tbc="+vtbc;
			}
			else//修改
			{
				var NoteID = document.getElementById('HdnNoteID').value;
				window.document.location.href = "TaskBillMod.aspx?pc="+prjcode+"&nd="+NoteID+"&tbc="+vtbc;			
			}			
		 }
		 function OpenResourceGather(prjcode)
		 {
			var bookcode = document.getElementById('HdnTaskBookCode').value;
			var url = "ResourceGather.aspx?pc="+prjcode+"&bc="+bookcode;
			window.showModalDialog(url,window,'dialogHeight:360px;dialogWidth:560px;center:1;help:0;status:0;');
		}
		function OpenQuantityList(prjcode)
		 {
			var bookcode = document.getElementById('HdnTaskBookCode').value;
			var url = "QuantityGather.aspx?pc="+prjcode+"&bc="+bookcode;
			window.showModalDialog(url,window,'dialogHeight:360px;dialogWidth:560px;center:1;help:0;status:0;');
		}
		</SCRIPT>
	</head>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<table class="table_form" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="td-toolsbar" >
						<asp:Button ID="BtnAdd" TabIndex="-1" Text="新  建" runat="server" />
						<asp:Button ID="BtnMod" TabIndex="-1" Enabled="false" Text="编  辑" runat="server" />
						<asp:Button ID="BtnDel" TabIndex="-1" Enabled="false" Text="删  除" OnClick="BtnDel_Click" runat="server" />
						<input id="HdnNoteID" style="WIDTH: 10px" type="hidden" size="1" runat="server" />

						<input id="HdnTaskBookCode" type="hidden" style="WIDTH: 10px" runat="server" />

					</td>
				</tr>
				<tr>
					<td vAlign="top">
						<div class="gridBox" id="dvRelation"><asp:DataGrid ID="dgdRelation" CssClass="grid" CellPadding="0" AutoGenerateColumns="false" Width="1000px" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn DataField="ConstructDate" HeaderText="上报时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="RecordPerson" HeaderText="记录人"></asp:BoundColumn><asp:TemplateColumn HeaderText="查看工程量">
<ItemTemplate>
											<asp:Button ID="btnQuantity" Text="查看工程量" CssClass="button-normal" style="width:70" runat="server" />
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="资源汇总">
<ItemTemplate>
											<asp:Button ID="btnRes" Text="资源汇总" CssClass="button-normal" style="width:70" runat="server" />
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderImageUrl="~/images1/fujian.gif">
<ItemTemplate>
                                            <MyUserControl:epc_usercontrol1_filelink_ascx ID="FileLink1" Visible="true" runat="server" />
                                        </ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn Visible="false" DataField="TaskBookCode"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
