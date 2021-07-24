<%@ Page Language="C#" AutoEventWireup="true" CodeFile="progressproceeds.aspx.cs" Inherits="ProgressProceeds" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>进步收益列表</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script src="/web_client/tree.js"></script>
		<script language="javascript">
		function ClickRow(obj,tbName,MainId,action)
		{
			//doClick(obj,tbName);
			if(action!="")
			{
				if(document.getElementById("btnEdit") != null)
					document.getElementById("btnEdit").disabled =true;
				if(document.getElementById("btnDel") != null)
					document.getElementById("btnDel").disabled =true;
			}
			else
			{
				if(document.getElementById("btnEdit") != null)
					document.getElementById("btnEdit").disabled =false;
				if(document.getElementById("btnDel") != null)
					document.getElementById("btnDel").disabled =false;
			}
			document.getElementById("hidMainId").value = MainId;
		}
		function CheckData()
		{
			var mainId = document.getElementById("hidMainId").value;
			if(mainId =="")
			{
				alert("请选择记录！");
				return false;
			}
			else
			{
				return true;
			}
		}
		function doEdit(target)
		{
			var url = "/EPC/17/ppm/ScienceInnovate/ProceedOneEdit.aspx?prjCode="+document.getElementById("hidPrjCode").value;
			//alert(target);
			if(target != "New")
			{
				if(CheckData())
				{
					url += "&mainId="+document.getElementById("hidMainId").value;
				}
				else
					return false;
			}
			url += "&Type="+target;
			return window.showModalDialog(url,window,"dialogHeight:420px;dialogWidth:650px;center:1;help:0;status:0;");
		}
		function doAudit()
		{
			var url = "/EPC/17/Entpm/ScienceInnovate/ProceedEntAudit.aspx";
			if(CheckData())
			{
				url += "?mainId="+document.getElementById("hidMainId").value;
			}
			else
				return false;
			return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:450px;center:1;help:0;status:0;");
		}
		</script>
	</head>
	<body class="body-normal" topmargin="0" rightmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" class="table-normal" height="100%" cellSpacing="0" cellPadding="0" width="100%">
					<tr>
						<td class="td-title">技术进步收益额</td>
					</tr>
					<TR>
						<TD class="td-toolsbar"><input id="hidMainId" type="hidden" name="Hidden2" runat="server" />

							<input id="hidPrjCode" type="hidden" name="Hidden1" runat="server" />

							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl><asp:Button ID="btnNew" CssClass="button-normal" Text="新  建" OnClick="btnNew_Click" runat="server" />
							<asp:Button ID="btnEdit" CssClass="button-normal" Text="编  辑" OnClick="btnEdit_Click" runat="server" /><asp:Button ID="btnAudit" CssClass="button-normal" Text="审  批" OnClick="btnAudit_Click" runat="server" />
							<asp:Button ID="btnDel" CssClass="button-normal" Text="删  除" OnClick="btnDel_Click" runat="server" /><asp:Button ID="btnCanncle" CssClass="button-normal" Text="撤销审核" OnClick="btnCanncle_Click" runat="server" />
							<asp:Button ID="btnView" Text="查  看" runat="server" /></TD>
					</TR>
					<TR>
						<TD vAlign="top"><div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%"><asp:DataGrid ID="dgList" CssClass="grid" DataKeyField="MainId" AutoGenerateColumns="false" Width="100%" runat="server"><ItemStyle Height="22px"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:BoundColumn DataField="PPMSerialNumber" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="FruitName" HeaderText="成果名称"></asp:BoundColumn><asp:BoundColumn DataField="AdministerFruitUnit" HeaderText="执行成果单位/人"></asp:BoundColumn><asp:BoundColumn DataField="AppPrejectName" HeaderText="应用工程名称"></asp:BoundColumn><asp:BoundColumn DataField="EndDate" HeaderText="完成时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="PPMAdvancementIncomeCount" HeaderText="申报值" DataFormatString="{0:f2}"></asp:BoundColumn><asp:BoundColumn DataField="AuditValue" HeaderText="审核值" DataFormatString="{0:f2}"></asp:BoundColumn><asp:TemplateColumn HeaderText="状态"><ItemTemplate>
												<asp:Label Text='<%# Convert.ToString(Eval("AuditResult")) %>' runat="server"></asp:Label>
												<input id="auditResult" type="hidden" value='<%# Convert.ToString(Eval("EntAuditResult")) %>' runat="server" />

											</ItemTemplate><EditItemTemplate>
												<asp:TextBox Text='<%# Convert.ToString(Eval("AuditResult")) %>' runat="server"></asp:TextBox>
											</EditItemTemplate></asp:TemplateColumn></Columns></asp:DataGrid></div>
						</TD>
					</TR>
					<TR>
						<TD class="td-page" height="10"><JWControl:PaginationControl ID="pc" runat="server"></JWControl:PaginationControl></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
