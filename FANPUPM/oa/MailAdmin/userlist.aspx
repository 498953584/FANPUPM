<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlist.aspx.cs" Inherits="UserList" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>UserList</title><meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		
		<SCRIPT language="JavaScript" type="text/JavaScript">
			function selectUser(userCode,userName)
			{
				parent.fraSecretary.document.Form1.TBoxUserName.value = userName;
				parent.fraSecretary.document.Form1.hdnUserCode.value = userCode;
			}
		</SCRIPT>
	</head>
	<BODY leftmargin="0" topmargin="0" style="OVERFLOW:auto">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<asp:DataGrid ID="DGrdUser" AutoGenerateColumns="false" Width="100%" PageSize="3" runat="server"><ItemStyle Height="22px"></ItemStyle><HeaderStyle Height="22px" CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="v_yhdm" HeaderText="用户编号"></asp:BoundColumn><asp:TemplateColumn HeaderText="用户姓名">
<ItemTemplate>
								<asp:HyperLink NavigateUrl="#" Text='<%# Convert.ToString(Eval("v_xm")) %>' runat="server"></asp:HyperLink>
							</ItemTemplate>
</asp:TemplateColumn></Columns><PagerStyle HorizontalAlign="Center"></PagerStyle></asp:DataGrid></FONT>
		</form>
	</BODY>
</HTML>
