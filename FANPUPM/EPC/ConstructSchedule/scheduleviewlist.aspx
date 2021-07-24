<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scheduleviewlist.aspx.cs" Inherits="ScheduleViewList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SchedulePlanAdd</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		document.onmousedown=click   
          function   click()     
          {if   (event.button==2)   
            { alert('右键无效！') 
            }
          }
			function OpenScheduleView(projectCode,projectName)
			{
				var url = "ScheduleViewSelect.aspx?pc="+projectCode+"&pn="+escape(projectName);
				return window.showModalDialog(url,window,"dialogHeight:560px;dialogWidth:380px;center:1;help:0;status:0;");
				//return false;
			}
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'tblSchedule','hdnScheduleCodeList',t1,t2,'../../');//调用样式设置
			}
		</script>
	</head>
	<body style="margin:0px; padding:0px; overflow:hidden">
		<form id="Formx" method="post" runat="server">
			<TABLE class="table-none" id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%"	border="0">
				<TR>
					<TD vAlign="top" style="height: 20px">
						<TABLE id="Tablec" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD style="height: 23px">
									<asp:Button ID="BtnSearchPhase" Text="按阶段查询" Visible="false" runat="server" /></TD>
								<TD style="height: 23px">&nbsp;编码
									<asp:TextBox ID="TxtTaskCode" Width="90px" runat="server"></asp:TextBox>&nbsp;任务名称
									<asp:TextBox ID="TxtTaskName" Width="90px" runat="server"></asp:TextBox>&nbsp;
									<asp:DropDownList ID="DDLProjectType" Visible="false" runat="server"><asp:ListItem Value="1" Text="单位工程" /><asp:ListItem Value="2" Text="分部工程" /><asp:ListItem Value="3" Text="分项工程" /></asp:DropDownList></TD>
								<TD style="height: 23px"><FONT face="宋体">&nbsp;</FONT>
									<asp:Button ID="BtnSearchCTT" Text="查  询" runat="server" /></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" height="20">
					    <input id="hdnProjectCode" style="WIDTH: 10px" type="hidden" name="hdnProjectCode" runat="server" />

					    <input id="hdnTaskCode" style="WIDTH: 10px" type="hidden" name="hdnTaskCode" runat="server" />

					    <input id="hdnProjectName" style="WIDTH: 10px" type="hidden" name="hdnProjectName" runat="server" />

					    <input id="hdnScheduleCodeList" style="WIDTH: 10px" type="hidden" name="hdnScheduleCodeList" runat="server" />

						<TABLE id="Tablev" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<td>开始时间</td>
								<TD>
									<asp:RadioButtonList ID="RBtnDate" Width="120px" AutoPostBack="true" RepeatDirection="Horizontal" runat="server"><asp:ListItem Value="1" Text="年" /><asp:ListItem Value="2" Text="季" /><asp:ListItem Value="3" Text="月" /><asp:ListItem Value="4" Text="周" /></asp:RadioButtonList></TD>
								<TD>&nbsp;从
									<asp:DropDownList ID="DDLStartDate" Width="70px" runat="server"></asp:DropDownList>&nbsp;—
									<asp:DropDownList ID="DDLEndDate" Width="70px" runat="server"></asp:DropDownList>&nbsp;
									<asp:Button ID="BtnSearchDate" Text="查  询" runat="server" />
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<div id="dvScheduleBox" style="border:solid 0px red;overflow:auto;height:100%;width:100%;">
							<asp:Table ID="tblSchedule" CssClass="grid" Width="100%" BorderWidth="1px" BorderStyle="None" BorderColor="#E0E0E0" CellPadding="0" CellSpacing="0" GridLines="Both" runat="server"><asp:TableRow HorizontalAlign="Center" CssClass="grid_head" runat="server"><asp:TableCell Wrap="false" Text="任务名称" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="编码" runat="server"></asp:TableCell><asp:TableCell Width="50px" Wrap="false" Text="类型" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="开始时间" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="结束时间" runat="server"></asp:TableCell><asp:TableCell Width="40px" Wrap="false" Text="单位" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="工程量" runat="server"></asp:TableCell><asp:TableCell Width="70px" Wrap="false" Text="综合单价" runat="server"></asp:TableCell><asp:TableCell Width="40px" Wrap="false" Text="完成量" runat="server"></asp:TableCell><asp:TableCell Width="40px" Wrap="false" Text="完成百分比" runat="server"></asp:TableCell></asp:TableRow></asp:Table>
							<input id="Hdnstr" type="hidden" runat="server" />

                        </div>
					    <iframe id="Project" name="Project" src="project/pj_task_view.asp?pc=<%=Request["pc"] %>&pn=<%=Request["pn"] %>&yhdm=<%=this.Session["yhdm"] %>&strconn=<%=this.Session["HdnAspstr"] %>" frameBorder="0" width="100%" scrolling=auto	height="100%"  style="display:none"></iframe>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
