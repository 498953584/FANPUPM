<%@ Page Language="C#" AutoEventWireup="true" CodeFile="managerlist.aspx.cs" Inherits="ManagerList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ManagerList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
<style>
.clkSpan{color:blue; text-decoration:underline; cursor:pointer}
</style>
</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<table class="table-normal" id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD colspan="2"></TD>
					</TR>
					<TR>
						<TD width="70%" height="30"><asp:Label ID="lblCaption" runat="server"></asp:Label></TD>
						<TD align="right" height="30">&nbsp;&nbsp;</TD>
					</TR>
					<TR>
						<TD colspan="2"><div id="Box" class="gridBox"><asp:DataGrid ID="dgUserList" CellPadding="5" AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" CssClass="grid" DataKeyField="v_yhdm" OnItemDataBound="dgUserList_ItemDataBound" runat="server"><SelectedItemStyle BackColor="Maroon"></SelectedItemStyle><AlternatingItemStyle HorizontalAlign="Center"></AlternatingItemStyle><ItemStyle HorizontalAlign="Center" CssClass="grid_row"></ItemStyle><HeaderStyle HorizontalAlign="Center" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn HeaderText="序号" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_yhdm" HeaderText="用户代码" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_xm" HeaderText="姓名" DataFormatString="{0}"></asp:BoundColumn><asp:BoundColumn DataField="v_yhdm" HeaderText=" " DataFormatString="<span class='clkSpan' onclick=JavaScript:window.showModalDialog('Broker.aspx?Op=PrivMaintenance&yhdm={0}',window,'dialogHeight:600px;dialogWidth:650px;center:1;help:0;status:0;');return false;>权限维护</span>"></asp:BoundColumn><asp:BoundColumn DataField="v_yhdm" HeaderText=" " DataFormatString="<span class='clkSpan' onclick=JavaScript:window.showModalDialog('Broker.aspx?Op=deptMaintenance&yhdm={0}',window,'dialogHeight:600px;dialogWidth:350px;center:1;help:0;status:0;');return false;>部门维护</span>"></asp:BoundColumn></Columns></asp:DataGrid></div></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
