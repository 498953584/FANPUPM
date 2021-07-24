<%@ Page Language="C#" AutoEventWireup="true" CodeFile="linkmansearch.aspx.cs" Inherits="LinkmanSearch" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>LinkmanSearch</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl></head>
	<body class=body_clear  scroll=no >
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%"  height="100%" align="center" border="0">
				<TR>
					<TD align="center" class=td-title>
						<asp:Label ID="lblTitle" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD class=td-search><FONT face="宋体">
							<asp:Panel ID="Panel1" Visible="false" runat="server">
								<FONT face="宋体">公司地址：
									<asp:Label ID="lblAddress" runat="server">Label</asp:Label>&nbsp;邮政编码：
									<asp:Label ID="lblPostalCode" runat="server">Label</asp:Label></FONT></asp:Panel></FONT>
					</TD>
				</TR>
				<TR>
					<TD><div class="gridBox">
						<asp:DataGrid ID="dgLinkman" AutoGenerateColumns="false" Width="100%" DataKeyField="i_id" CellPadding="5" runat="server"><SelectedItemStyle Wrap="false"></SelectedItemStyle><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle Wrap="false" HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:HyperLinkColumn DataTextField="v_xm" HeaderText="姓名"></asp:HyperLinkColumn><asp:BoundColumn DataField="v_zw" HeaderText="办公电话（内线）"></asp:BoundColumn><asp:BoundColumn DataField="v_bgdh" HeaderText="办公电话（外线）"></asp:BoundColumn><asp:BoundColumn DataField="v_sj" HeaderText="手机"></asp:BoundColumn><asp:BoundColumn DataField="v_cz" HeaderText="传真"></asp:BoundColumn><asp:BoundColumn DataField="v_dzyx" HeaderText="Email"></asp:BoundColumn></Columns></asp:DataGrid></div></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
