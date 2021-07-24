<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datummanagewin.aspx.cs" Inherits="DatumManageWin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DatumManageWin</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
	<body class="body_frame" scroll=no>
		<form id="Form1" method="post" runat="server">
			<table width="100%" cellSpacing="0" cellPadding="0" border="0" height="100%">
				<tr>
					<td><table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr class=td-search>
								<td width="35%" align=left>
									<asp:Label ID="lbPrjName" runat="server"></asp:Label> 资料管理</td>
								<td align="right">
								     
									<label style ="display:none">项目编号<asp:TextBox ID="txtPrjCode" runat="server"></asp:TextBox>项目名称	<asp:TextBox ID="txtPrjName" runat="server"></asp:TextBox>&nbsp;
										<asp:Button ID="btn_Search" OnClick="btnSelect_Click" runat="server" />
									</label>										
										项目列表&nbsp;
										<asp:DropDownList ID="ddlPrj" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddlPrj_SelectedIndexChanged" runat="server"></asp:DropDownList>
										分配项目
										<asp:DropDownList ID="ddlItem" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" runat="server"></asp:DropDownList>&nbsp;

								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="100%">
						<table width="100%" height="100%" class="table-form" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td width="0%" valign="top">
									<iframe width="100%" height="100%" frameborder="0" name="lframe" id="lframe" src="<%=this.strTree %>">
									</iframe>
								</td>
								<td width="100%" height="100%"><iframe width="100%" height="100%" frameborder="0" name="InfoList" id="InfoList" scrolling="no"
										src="<%=this.strRur %>"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
