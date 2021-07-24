<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fundapplicationlist.aspx.cs" Inherits="FundApplicationList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>资金申请列表</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function clickRow(FlowGuid,type,AuditState)
			{
				document.getElementById('hdnRecordID').value = FlowGuid;
				
				if(type == "Edit")
				{
					if(AuditState == "-1")
					{
						document.getElementById('BtnMod').disabled = false;
						document.getElementById('BtnDel').disabled = false;
						document.getElementById('BtnApply').disabled = false;
					}
					else
					{
						document.getElementById('BtnMod').disabled = true;
						document.getElementById('BtnDel').disabled = true;
						document.getElementById('BtnApply').disabled = true;
					}
				}
				
				//根据选择显示该记录的审核记录
				var url;
				if (type == "Audit")
				{
					url = "../Flow/flowlist.aspx?ic="+FlowGuid+"&pt=<%=this.pt %>" ;
				}
				else
				{
					url = "../Flow/flowview.aspx?ic="+FlowGuid;
				}
				
				document.getElementById("FraFlow").src = url;
				
			}
			function outRow(obj)
			{
				/*在这之前增加你的处理代码*/
				doMouseOut(obj);//调用样式设置
			}
			function overRow(obj)
			{
				/*在这之前增加你的处理代码*/
				doMouseOver(obj);//调用样式设置
			}
			function openWin(op,PrjCode)
			{
				var RID = "";
				RID = document.getElementById('hdnRecordID').value;
				var url= "EditFundApplication.aspx?op=" + op + "&id=" + RID + "&PrjCode=" + PrjCode;
				return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:400px;center:1;help:0;status:0;");
			}
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="td-title"><asp:Label ID="LabTitle" runat="server">借款申请列表</asp:Label></td>
				</tr>
				<tr>
					<TD class="td-toolsbar"><input id="hdnRecordID" style="WIDTH: 10px" type="hidden" runat="server" />

						<asp:Button ID="BtnAdd" Text="新  增" Enabled="false" OnClick="BtnAdd_Click" runat="server" />&nbsp;<asp:Button ID="BtnMod" Text="编  辑" Enabled="false" OnClick="BtnMod_Click" runat="server" />&nbsp;<asp:Button ID="BtnDel" Text="删  除" Enabled="false" OnClick="BtnDel_Click" runat="server" />&nbsp;<asp:Button ID="BtnApply" Text="提请审核" Enabled="false" OnClick="BtnApply_Click" runat="server" /></TD>
				</tr>
				<TR>
					<TD vAlign="top">
						<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%"><asp:DataGrid ID="DGrdList" Width="100%" CssClass="grid" AutoGenerateColumns="false" DataKeyField="GuidFlow" runat="server"><AlternatingItemStyle Wrap="false" CssClass="grid_row"></AlternatingItemStyle><ItemStyle Wrap="false" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head" VerticalAlign="Middle"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + this.DGrdList.PageSize * this.DGrdList.CurrentPageIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="FundAppUser" HeaderText="借款人"></asp:BoundColumn><asp:BoundColumn DataField="FundAppDate" HeaderText="借款日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="FundNum" HeaderText="本期请款" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="Content" HeaderText="借款事由"></asp:BoundColumn><asp:BoundColumn DataField="AuditState" HeaderText="流程状态"></asp:BoundColumn></Columns></asp:DataGrid></div>
					</TD>
				</TR>
				<TR>
					<TD class="td-page">
						<JWControl:PaginationControl ID="PageControl" OnPageIndexChange="PageControl_PageIndexChange" runat="server"></JWControl:PaginationControl></TD>
				</TR>
				<tr height="200">
					<td><iframe id="FraFlow" name="FraFlow" frameBorder="yes" width="100%" height="100%"></iframe>
					</td>
				</tr>
			</TABLE>
			<JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
		</form>
	</body>
</HTML>
