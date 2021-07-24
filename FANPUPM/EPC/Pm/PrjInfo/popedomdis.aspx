<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popedomdis.aspx.cs" Inherits="Popedomdis" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>项目人员设置</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
		 window.name="win";
		function WinGoBack()
		{
			window.close();
		}
		</script>
	</head>
	<body  scroll="yes" class="body_popup" >
		<form id="Form1" method="post" target="win" runat="server">
			<table width="100%" border="1" height="100%" >
				<tr class="td-title" >
				 <td>
					<asp:Label ID="lbPrjName" runat="server"></asp:Label>  项目人员设置</td>
				</tr>
				<tr>
					<td height="400">
						<table width="100%" height="100%" class="table-form" cellpadding="0" cellspacing="0" border="0">
							<tr >
								<td width="25%" valign="top" height="90%">
								<div style="height:400;"  class=gridbox >
									<JWControl:FreeTree ID="FillTrees1" ExpandLevel="10" runat="server"></JWControl:FreeTree></div></td>
								<td width="45%" height="90%"><iframe width="100%" height="100%" frameborder="0" name="InfoList1" id="InfoList1" scrolling="auto"
										src="UserList.aspx?ItemCode=<%=this.SubPrjGuid %>"></iframe>
								</td>
								<td valign="top" ><b>项目经理：</b>
						        <div style="height:20;" >
                                    <asp:Label ID="Lb_prjManage" Font-Bold="true" ForeColor="#3300CC" Text="" runat="server"></asp:Label>
                                    </div>
                                    <b>业务经理：</b>
						        <div style="height:20;" >
                                    <asp:Label ID="Label2" Font-Bold="true" ForeColor="#3300CC" Text="" runat="server"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:Label ID="Label1" Font-Bold="true" Text="已分配人员：" runat="server"></asp:Label>
						        <asp:Label ID="lbUseList" runat="server"></asp:Label>
						        <input id="hdnUserList" type="hidden" name="hdnUserList" runat="server" />

						        <input id="hdnItemCode" type="hidden" name="ItemCode" runat="server" />

						        </td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="td-submit"><JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
						<asp:Button ID="btnSave" CssClass="button-normal" Text="保 存" OnClick="btnSave_Click" runat="server" /><FONT face="宋体">&nbsp;</FONT><INPUT class="button-normal" onclick="WinGoBack();" type="button" value="返 回"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
