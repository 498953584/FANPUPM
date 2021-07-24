<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputreceiptsyearplan.aspx.cs" Inherits="InputReceiptsYearPlan" %>

<%@ Import Namespace="com.jwsoft.web.WebControls"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>InputReceiptsYearPlan</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script src="../../../Web_Client/Tree.js"></script>
		<script language="javascript">
		function ClickRow(obj,tbName,MainId)
		{
			//doClick(obj,tbName);
			document.getElementById("hidMainId").value = MainId;
			var url = "InputReceiptsItem.aspx?MainId="+MainId;
			url += "&Type="+document.getElementById("hidPageType").value;
			document.getElementById("frmList").src = url;
		}
		function CheckData()
		{
			var mainId = document.getElementById("hidMainId").value;
			if(mainId == "")
			{
				alert("请选择计划！");
				return false ;
			}
			else
				return true;
		}
		function doEdit(target)
		{
			var url = "/EPC/17/Entpm/ScienceInnovate/InputReceiptsEditOne.aspx";
			//url += "?prjCode="+document.getElementById("hidPrjCode").value;
			if(target == "Edit")
			{
				if(!CheckData())
					return false;
				else
					url += "?MainId="+document.getElementById("hidMainId").value;
			}
			//alert(url);
			return window.showModalDialog(url,window,"dialogHeight:250px;dialogWidth:450px;center:1;help:0;status:0;");
		}
		function doDel()
		{
			return CheckData();
		}
		function CreateTable()
		{
			var url = "InputReceiptsTable.aspx?MainId=";
			if(CheckData())
			{
				url += document.getElementById("hidMainId").value;
				url += "&id="+Math.random();
				return window.showModalDialog(url,window,"dialogHeight:700px;dialogWidth:800px;center:1;help:0;status:0;");
			}
		}
		</script>
	</head>
	<body class="body-normal">
		<form id="Form1" method="post" runat="server">
			<FONT face="宋体">
				<TABLE id="Table1" class="table-normal" cellSpacing="0" cellPadding="0" width="100%" height="100%">
					<tr>
						<td class="td-title">收益与投入年计划</td>
					</tr>
					<TR>
						<TD align="right" height="10" class="td-toolsbar">
							<input id="hidPageType" type="hidden" name="Hidden2" runat="server" />
 <input id="hidPrjCode" type="hidden" name="Hidden1" runat="server" />

							<JWControl:JavaScriptControl ID="js" runat="server"></JWControl:JavaScriptControl>
							<asp:Button ID="btnNew" Text="新\u3000建" CssClass="button-normal" OnClick="btnNew_Click" runat="server" />&nbsp;
							<asp:Button ID="btnEdit" Text="编\u3000辑" CssClass="button-normal" OnClick="btnEdit_Click" runat="server" />&nbsp;
							<asp:Button ID="btnDel" Text="删\u3000除" CssClass="button-normal" OnClick="btnDel_Click" runat="server" />&nbsp;<INPUT type="button" value="计划完成情况" id="btnCreateTable" onclick="CreateTable();" class="button-normal"
								style="WIDTH: 120px">
						</TD>
					</TR>
					<TR>
						<TD height="24%" valign="top"><div style="OVERFLOW:auto;WIDTH:100%;HEIGHT:100%">
								<asp:DataGrid ID="dgYearPlanList" CssClass="grid" Width="100%" AutoGenerateColumns="false" DataKeyField="MainId" runat="server"><HeaderStyle CssClass="grid_head"></HeaderStyle><ItemStyle Height="22px"></ItemStyle><Columns><asp:BoundColumn DataField="SerialNumber" HeaderText="编号"></asp:BoundColumn><asp:BoundColumn DataField="TabPeople" HeaderText="制表人"></asp:BoundColumn><asp:BoundColumn DataField="TabTime" HeaderText="制表日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="ExaminePeople" HeaderText="审核人"></asp:BoundColumn><asp:BoundColumn DataField="ExamineTime" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn><asp:BoundColumn DataField="PlanYear" HeaderText="计划年度"></asp:BoundColumn><asp:BoundColumn DataField="Remark" HeaderText="备注"></asp:BoundColumn></Columns></asp:DataGrid></div>
						</TD>
					</TR>
					<TR>
						<TD class="td-page" height="10">
							<JWControl:PaginationControl ID="pc" OnPageIndexChange="pc_PageIndexChange" runat="server"></JWControl:PaginationControl><input id="hidMainId" type="hidden" runat="server" />
</TD>
					</TR>
					<TR>
						<TD><iframe id="frmList" src="" frameborder="0" width="100%" height="100%"></iframe>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
