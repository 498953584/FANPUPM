<%@ Page Language="C#" AutoEventWireup="true" CodeFile="costinputbottom.aspx.cs" Inherits="CostInputBottom" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>CostInputBottom</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-normal" id="Table1" height="100%" width="100%" border="1">
				<TR>
					<TD vAlign="top" height="100%"><div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgCostInputSlave" CssClass="grid" AutoGenerateColumns="false" Width="100%" AllowCustomPaging="true" AllowPaging="true" PageSize="8" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号"></asp:BoundColumn><asp:BoundColumn Visible="false" DataField="ChildID" HeaderText="成本明细序号"></asp:BoundColumn><asp:BoundColumn DataField="ItemName" HeaderText="名称"></asp:BoundColumn><asp:BoundColumn DataField="Price" HeaderText="金额(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="CBSName" HeaderText="归属成本科目"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn></Columns><PagerStyle Visible="false"></PagerStyle></asp:DataGrid></div>
					</TD>
				</TR>
				<JWControl:JavaScriptControl ID="Js" runat="server"></JWControl:JavaScriptControl></TABLE>
		</form>
	</body>
</HTML>
