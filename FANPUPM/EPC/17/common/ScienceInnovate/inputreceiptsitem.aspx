<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputreceiptsitem.aspx.cs" Inherits="InputReceiptsItem" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>InputReceiptsItem</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script src="../../../Web_Client/Tree.js"></script>
		<script language="javascript">
		function checkNum(obj)
		{
			if (!(new RegExp(/^(\d+\.+\d)?(\d+$)/).test(obj.value)))
			{
				alert("应输入数值！");
				obj.select();
				obj.focus();
			}
		}
		function SelDepartment(obj,hidId)
		{
			var ret = window.showModalDialog("/Tender/ProjectInfoManage/PopDpt.aspx",window,"dialogWidth:240px;dialogHeight:400px");
			obj.value = ret[1];
			document.getElementById(hidId).value = ret[0];			
		}
		</script>
	</head>
	<body class="body-normal" leftmargin="0" rightmargin="0" topmargin="0" bottommargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" class="table-normal" height="100%" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD class="td-toolsbar">
						<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl><asp:Button ID="btnNew" Text="新 增" CssClass="button-normal" OnClick="btnNew_Click" runat="server" /></TD>
				</TR>
				<TR>
					<TD vAlign="top"><div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgItemList" Width="100%" AutoGenerateColumns="false" CssClass="grid" DataKeyField="ChildMainID" runat="server"><ItemStyle Height="22px"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="部门名称"><ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("DepartmentName")) %>' runat="server"></asp:Label>
										</ItemTemplate><EditItemTemplate>
											<asp:TextBox ReadOnly="false" CssClass="text-normal" ID="txtDepartmentName" Text='<%# Convert.ToString(Eval("DepartmentName")) %>' runat="server"></asp:TextBox>
											<input type="hidden" id="hidDepartCode" value='<%# Convert.ToString(Eval("DepartCode")) %>' runat="server" />

										</EditItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="计划收益额(万元)"><ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("IncomeMoney")) %>' runat="server"></asp:Label>
										</ItemTemplate><EditItemTemplate>
											<asp:TextBox CssClass="text-normal" ID="txtIncomeMoney" Text='<%# Convert.ToString(Eval("IncomeMoney")) %>' runat="server"></asp:TextBox>
										</EditItemTemplate></asp:TemplateColumn><asp:TemplateColumn HeaderText="计划投入额(万元)"><ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("DevotionMoney")) %>' runat="server"></asp:Label>
										</ItemTemplate><EditItemTemplate>
											<asp:TextBox CssClass="text-normal" ID="txtDevotionMoney" Text='<%# Convert.ToString(Eval("DevotionMoney")) %>' runat="server"></asp:TextBox>
										</EditItemTemplate></asp:TemplateColumn><asp:EditCommandColumn ButtonType="LinkButton" UpdateText="更新" CancelText="取消" EditText="编辑"></asp:EditCommandColumn><asp:ButtonColumn Text="删除" ButtonType="LinkButton" CommandName="Delete"></asp:ButtonColumn></Columns></asp:DataGrid>
						</div>
					</TD>
				</TR>
				<TR>
					<TD class="td-page" height="10"><JWControl:PaginationControl ID="pc" OnPageIndexChange="pc_PageIndexChange" runat="server"></JWControl:PaginationControl></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
