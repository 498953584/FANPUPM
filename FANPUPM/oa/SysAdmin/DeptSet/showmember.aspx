<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showmember.aspx.cs" Inherits="ShowMember" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ShowMember</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<BODY>
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE width="100%" border="1" cellspacing="1" cellpadding="1">
					<TR>
						<TD>
							<asp:Label ID="LabDept" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD><asp:DataGrid ID="DGrdMember" Width="100%" AutoGenerateColumns="false" runat="server"><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="v_yhdm" HeaderText="用户代码"></asp:BoundColumn><asp:BoundColumn DataField="v_xm" HeaderText="用户姓名"></asp:BoundColumn><asp:BoundColumn DataField="c_sfyx" HeaderText="是否有效"></asp:BoundColumn></Columns></asp:DataGrid></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</BODY>
</HTML>
