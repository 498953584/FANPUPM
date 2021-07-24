<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cry_ManUse.aspx.cs" Inherits="ManUse" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>cry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body class="body_clear" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD class="td-title"><FONT face="宋体">人工耗用台帐</FONT></TD>
					<FONT face="宋体"></FONT>
				</TR>
				<TR>
					<TD><FONT face="宋体">单位工程名称：
							<asp:DropDownList ID="ddl_pn" Width="200px" AutoPostBack="true" runat="server"></asp:DropDownList></FONT></TD>
				</TR>
				<TR>
					<TD><FONT face="宋体"><asp:DataGrid ID="dg_ManPower" PageSize="6" AllowCustomPaging="true" AutoGenerateColumns="false" CssClass="grid" Width="100%" runat="server"><ItemStyle CssClass="grid_row"></ItemStyle><HeaderStyle Height="20px" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="Y" HeaderText="年"></asp:BoundColumn><asp:BoundColumn DataField="M" HeaderText="月"></asp:BoundColumn><asp:BoundColumn DataField="SL1" HeaderText="工日数"></asp:BoundColumn><asp:BoundColumn DataField="JE1" HeaderText="金额(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="SL2" HeaderText="工日数"></asp:BoundColumn><asp:BoundColumn DataField="JE2" HeaderText="金额(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="SL3" HeaderText="工日数"></asp:BoundColumn><asp:BoundColumn DataField="JE3" HeaderText="金额(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn DataField="SLsum" HeaderText="工日数"></asp:BoundColumn><asp:BoundColumn DataField="JEsum" HeaderText="金额(元)" DataFormatString="{0:F2}"></asp:BoundColumn><asp:BoundColumn HeaderText="备注"></asp:BoundColumn></Columns></asp:DataGrid></FONT></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
