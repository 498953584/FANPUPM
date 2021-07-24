<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resourceselect.aspx.cs" Inherits="ResourceSelect" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ResourceSelect</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<base target="_self">
		<script language="javascript">
		    function checkData() {
		        var isHave = false;
		        var objCol = document.all.tags('input');
		        if (objCol != null) {
		            var tempName
		            for (i = 0; i < objCol.length; i++) {
		                tempName = objCol[i].name;
		                if (tempName.lastIndexOf('ChkResourceItem') > 0) {
		                    if (objCol[i].checked) {
		                        isHave = true;
		                        break;
		                    }
		                }
		            }
		        }
		        return isHave;
		    }
		    function checkData1() {
		        var isHave = false;
		        var objCol = document.all.tags('input');
		        if (objCol != null) {
		            var tempName
		            for (i = 0; i < objCol.length; i++) {
		                tempName = objCol[i].name;
		                if (tempName.lastIndexOf('ChkUseResourceItem') > 0) {
		                    if (objCol[i].checked) {
		                        isHave = true;
		                        break;
		                    }
		                }
		            }
		        }
		        return isHave;
		    }
		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table5" style="BORDER-COLLAPSE: collapse" height="100%" cellSpacing="0" cellPadding="0"
				width="100%" border="1">
				<tr class="td-search">
					<td colSpan="2">类别
						<asp:TextBox ID="TxtCate" Width="96px" runat="server"></asp:TextBox>编号
						<asp:TextBox ID="TxtCode" Width="96px" runat="server"></asp:TextBox>名称
						<asp:TextBox ID="TxtName" Width="112px" runat="server"></asp:TextBox><asp:Button ID="BtnSearch" OnClick="BtnSearch_Click" runat="server" /></td>
				</tr>
				<TR>
					<TD vAlign="top" colSpan="2" height="60%">
						<DIV class="gridBox">
						<asp:DataGrid ID="DgdResourceList" DataKeyField="ResourceCode" CssClass="grid" Width="100%" AutoGenerateColumns="false" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<ItemTemplate>
											<asp:CheckBox ID="ChkResourceItem" EnableViewState="true" runat="server" />
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="ResourceCode" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="资源名称"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="规格"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="单位"></asp:BoundColumn></Columns></asp:DataGrid></DIV>
					</TD>
				</TR>
				<TR>
					<TD align="center" width="50%" style="height: 16px"><asp:Button ID="BtnAddResource" CssClass="button-normal" Width="60px" ToolTip="增加选中的资源！" Height="15px" Text="6" OnClick="BtnAddResource_Click" Font-Names="Marlett" runat="server" /></TD>
					<TD align="center" width="50%" style="height: 16px"><asp:Button ID="BtnDelResource" CssClass="button-normal" Width="60px" ToolTip="移除选中的资源！" Height="15px" Text="5" OnClick="BtnDelResource_Click" Font-Names="Marlett" runat="server" /></TD>
				</TR>
				<TR>
					<TD vAlign="top" colSpan="2" height="50%">
						<DIV class="gridBox">
						<asp:DataGrid ID="DgdResourceTemp" CssClass="grid" Width="100%" AutoGenerateColumns="false" DataKeyField="ResourceCode" runat="server"><AlternatingItemStyle Wrap="false"></AlternatingItemStyle><ItemStyle Wrap="false"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn>
<ItemTemplate>
											<asp:CheckBox ID="ChkUseResourceItem" EnableViewState="true" runat="server" />
										</ItemTemplate>
</asp:TemplateColumn><asp:TemplateColumn HeaderText="序号">
<ItemTemplate>
											<%# Container.ItemIndex + 1 %>
										</ItemTemplate>
</asp:TemplateColumn><asp:BoundColumn DataField="ResourceCode" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="ResourceName" HeaderText="资源名称"></asp:BoundColumn><asp:BoundColumn DataField="Specification" HeaderText="规格"></asp:BoundColumn><asp:BoundColumn DataField="UnitName" HeaderText="单位"></asp:BoundColumn></Columns></asp:DataGrid></DIV>
					</TD>
				</TR>
			</TABLE>
			<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
			<input id="HdnResource" style="WIDTH: 10px" type="hidden" name="HdnResource" runat="server" />

			<input id="HdnResourceUse" style="WIDTH: 10px" type="hidden" name="HdnResourceUse" runat="server" />

		</form>
	</body>
</HTML>
