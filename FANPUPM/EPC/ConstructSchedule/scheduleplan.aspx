<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scheduleplan.aspx.cs" Inherits="SchedulePlan" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_tasktreetable_ascx" Src="~/EPC/UserControl1/tasktreetable.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ItemDistribute</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
			function clickRow(projectCode,taskCode,isHaveChild,workLayer)
			{	
				var type = document.getElementById("HdnType").value;
				var projectName = document.getElementById("HdnProjectName").value;
				if (isHaveChild==false)
				//if ((workLayer==2))
				{
					if(type == "Plan")
					{
						window.InfoList.location.href = "SchedulePlanAdd.aspx?pc=" + projectCode + "&tc=" + taskCode +"&pn="+projectName;
					}
					if(type == "View")
					{
						window.InfoList.location.href = "ScheduleView.aspx?pc=" + projectCode + "&tc=" + taskCode +"&pn="+projectName;
					}
					if(type == "Resource")
					{
						window.InfoList.location.href = "../ProjectLayout/ProjectDistribute/ResourceDistribute.aspx?pc=" + projectCode + "&tc=" + taskCode +"&pn="+projectName + "&wbs=1";
					}
				}
				else
				{
				  window.InfoList.location.href ="../../EPC/UserControl1/webTreeTS.aspx?fj=fxrw";
				}
			}
			function switchDisplay(obj,t1,t2)
			{
				/*在这之前增加你的处理代码*/
				doSwitchDisplay(obj,'UCTaskTreeTable_tblTask','UCTaskTreeTable_HdnTaskCodeList',t1,t2,'../../');//调用样式设置
			}
			function selrow(obj)
			{

			    try
			    {
			      var table = document.getElementById(obj);
                    table.rows[3].click();
                 }
                catch(e){}
			
			}
        </script>
	</head>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0" onload ="selrow('UCTaskTreeTable_tblTask')">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0">
				<tr>
					<td height="100%">
						<table class="table-none" id="Tablexxx" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="30%" height="100%">
									<TABLE id="Tablev" cellSpacing="0" cellPadding="0" border="0" height="100%" width="100%">
										<TR>
											<TD class="td-toolsbar">
												<asp:Label ID="LblProjectName" runat="server"></asp:Label>工程进度计划</TD>
										</TR>
										<TR>
											<TD height="100%">
												<div class="gridBox">
													<MyUserControl:epc_usercontrol1_tasktreetable_ascx ID="UCTaskTreeTable" runat="server" /></div>
											</TD>
										</TR>
									</TABLE>
								</td>
								<td vAlign="top" width="70%"><iframe id="InfoList" name="InfoList" src="" frameBorder="1" width="100%" height="100%" scrolling=auto></iframe>
								</td>
							</tr>
						</table>
						<input id="HdnProjectName" name="HdnProjectName" type="hidden" runat="server" />
 <input id="HdnType" type="hidden" name="HdnType" runat="server" />

					</td>
				</tr>
				<tr>
					<td><JWControl:JavaScriptControl ID="JS" runat="server"></JWControl:JavaScriptControl></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
