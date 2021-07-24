<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSign.aspx.cs" Inherits="ViewSign" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server"><title>查看签收</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
</head>
<body class="body_frame" scroll="no" >
    <form id="form1" runat="server">
       <table width="100%"  height="100%" border="0" id="table1" class="table-normal">
    <tr>
        <TD class="td-title" align="center" height="28">文件签收记录</TD>
    </tr>
    <tr>
        <TD vAlign="top">
			<div style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
				<asp:DataGrid ID="dgSign" CssClass="grid" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" OnItemDataBound="dgSign_ItemDataBound" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
								<%# Container.ItemIndex + 1 %>
							</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="ReceiveDate" HeaderText="接收时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:TemplateColumn HeaderText="发文单位"><ItemTemplate>
								<asp:Label ID="TxtCorpCode" Text='<%# Convert.ToString(Eval("CorpName")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="主题"><ItemTemplate>
								<asp:Label ID="TxtTitle" Text='<%# Convert.ToString(Eval("Title")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="签收人"><ItemTemplate>
								<asp:Label ID="TxtSignUserName" Text='<%# Convert.ToString(Eval("SignUserName")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="SignDate" HeaderText="签收时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:TemplateColumn HeaderText="备注"><ItemTemplate>
								<asp:Label ID="TxtRemark" Text='<%# Convert.ToString(Eval("Remark")) %>' runat="server"></asp:Label>
							</ItemTemplate></asp:TemplateColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid>
			</div>
		</TD>
    </tr>
     <tr>
		<td class="td-submit"><input class="button-normal" id="btnClose" onclick="javascript:window.close();" type="button" value="关 闭">
		</td>
	</tr>
    </table>
    <JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl>
    </form>
</body>
</html>
