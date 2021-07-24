<%@ Page Language="C#" AutoEventWireup="true" CodeFile="developmentinput.aspx.cs" Inherits="DevelopmentInput" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>DevelopmentInput</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript">
		function ClickRow(obj,tbName,MainId,isAudit)
		{
			//doClick(obj,tbName);
			//alert(isAudit);
			if(document.getElementById("btnEdit") != null)
				document.getElementById("btnEdit").disabled = false;
			if(document.getElementById("btnDel") != null)
				document.getElementById("btnDel").disabled = false;
			if(isAudit!="")
			{
				if(document.getElementById("btnEdit") != null)
				document.getElementById("btnEdit").disabled = true;
			if(document.getElementById("btnDel") != null)
				document.getElementById("btnDel").disabled = true;
			}
			var url = "InputItem.aspx?MainId="+MainId;
			url += "&Type="+document.getElementById("hidType").value;
			document.getElementById("hidMainId").value = MainId;
			document.getElementById("frmItem").src=url;
		}
		function CheckData()
		{
			if(document.getElementById("hidMainId").value=="")
			{
				alert("请选择记录！");
				return false;
			}
			else			
				return true;
		}
		function doEdit(target,prjName)
		{
			var url = "/EPC/17/ppm/ScienceInnovate/InputEditOne.aspx?prjCode=";
			url += document.getElementById("hidPrjCode").value;
			url += "&prjName="+escape(prjName);
			if(target!="new")			
				if(CheckData())
				{
					//url += "?planId="+document.getElementById("hidPlanId").value;
					url += "&MainId="+document.getElementById("hidMainId").value;
				}
				else
				{
					return false;
				}
			return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:450px;center:1;help:0;status:0;");
		}
		function doDel()
		{
			if(CheckData())
			{
				if(window.confirm("确定删除?"))
					return true;
				else
					return false;
			}		
			else
			{
				return false;
			}
		}
		function doAudit()
		{
			var url = "/EPC/17/Entpm/ScienceInnovate/InputAuditOne.aspx";
			if(CheckData())
			{
				url += "?MainId="+document.getElementById("hidMainId").value;
			}
			else
			{
				return false;
			}
			return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:450px;center:1;help:0;status:0;");
		}
		</script>
	</head>
	<body topmargin=0 bottommargin=0 class="body-normal">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="tbList" class="table-normal" cellSpacing="0" cellPadding="0" width="100%" height="100%">
					<tr>
						<td class="td-title">科技开发投入</td>
					</tr>
					<TR>
						<TD class="td-toolsbar">
							<input id="hidType" type="hidden" name="Hidden1" runat="server" />
 <input id="hidMainId" type="hidden" name="Hidden2" runat="server" />

							<input id="hidPrjCode" type="hidden" name="Hidden1" runat="server" />

							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
							<asp:Button ID="btnNew" Text="新  建" CssClass="button-normal" OnClick="btnNew_Click" runat="server" />
							<asp:Button ID="btnEdit" Text="编  辑" CssClass="button-normal" OnClick="btnEdit_Click" runat="server" />
							<asp:Button ID="btnAudit" Text="审  核" CssClass="button-normal" OnClick="btnAudit_Click" runat="server" />
							<asp:Button ID="btnDel" Text="删  除" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />
							<asp:Button ID="btnNotAudit" Text="取消审核" CssClass="button-normal" OnClick="btnNotAudit_Click" runat="server" /></TD>
					</TR>
					<TR>
						<TD vAlign="top"><div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
							<asp:DataGrid ID="dgList" Width="100%" AutoGenerateColumns="false" DataKeyField="MainID" CssClass="grid" runat="server"><ItemStyle Height="22px"></ItemStyle><HeaderStyle CssClass="grid_head"></HeaderStyle><Columns><asp:TemplateColumn HeaderText="填报单位"><ItemTemplate>
											<asp:Label Text='<%# Convert.ToString(Eval("UnitName")) %>' runat="server"></asp:Label>
											<input type="hidden" id="auditResult" value='<%# Convert.ToString(Eval("AuditResult")) %>' runat="server" />

										</ItemTemplate><EditItemTemplate>
											<asp:TextBox Text='<%# Convert.ToString(Eval("UnitName")) %>' runat="server"></asp:TextBox>
										</EditItemTemplate></asp:TemplateColumn><asp:BoundColumn DataField="PrjName" HeaderText="项目名称"></asp:BoundColumn><asp:BoundColumn DataField="FillTime" HeaderText="填报日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="WeavePeople" HeaderText="编制人"></asp:BoundColumn><asp:BoundColumn DataField="Year" HeaderText="年份"></asp:BoundColumn><asp:BoundColumn DataField="Season" HeaderText="季度"></asp:BoundColumn><asp:BoundColumn DataField="AuditPeople" HeaderText="审核人"></asp:BoundColumn><asp:BoundColumn DataField="AuditTime" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="ScienceEmpolderMoney" HeaderText="科技开发投入额（元）"></asp:BoundColumn></Columns></asp:DataGrid></div></TD>
					</TR>
					<TR>
						<TD class="td-page" style="HEIGHT: 15px">
							<JWControl:PaginationControl ID="pc" OnPageIndexChange="pc_PageIndexChange" runat="server"></JWControl:PaginationControl></TD>
					</TR>
					<TR>
						<TD height="62%" vAlign="top">
							<iframe id="frmItem" src="" frameborder="0" width="100%" height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
