<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gonggongbufen.aspx.cs" Inherits="CostFrameAnalyze" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title></title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<base target="_self" />
	</head>
	<body class="body_clear" scroll="no" style="margin:0; height:100%;">
		<form id="Form1" method="post" runat="server">
			<table class="table-none" id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<tr class="td-toolstbar"  style="height:1px;">
				    <td>
				    </td>
				</tr>
				<tr>
				    <td vAlign="top">
				       <DIV class="gridBox">
				           <asp:DataGrid ShowFooter="false" ID="DG_list" Width="100%" CssClass="grid" AutoGenerateColumns="false" OnItemDataBound="DG_list_ItemDataBound" runat="server"><FooterStyle CssClass="grid_footer"></FooterStyle><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn HeaderText="任务名称" DataField="TaskName"></asp:BoundColumn><asp:BoundColumn HeaderText="任务编码" DataField="TaskCode"></asp:BoundColumn><asp:BoundColumn HeaderText="预算量" DataFormatString="{0:F2}" DataField="Quantity"></asp:BoundColumn><asp:BoundColumn HeaderText="实际完成量" DataFormatString="{0:F2}" DataField="SumCompleteQuantity"></asp:BoundColumn><asp:BoundColumn HeaderText="预算额度" DataFormatString="{0:F2}" DataField="total"></asp:BoundColumn><asp:BoundColumn HeaderText="实际成本" DataFormatString="{0:F2}" DataField="cctotal"></asp:BoundColumn><asp:BoundColumn HeaderText="完成度" DataFormatString="{0:F2}"></asp:BoundColumn></Columns></asp:DataGrid>
				    </DIV>
				    </td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
