<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scheduleviewmain.aspx.cs" Inherits="ScheduleViewMain" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_usercontrol1_uprojectlist_ascx" Src="~/EPC/UserControl1/uprojectlist.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>ItemDistribute</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="/web_client/tree.js" type="text/javascript"></script>
		<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

		<script language="javascript">
		 
			function OpenScheudleWin(type)
			{
				ProjectCode = document.getElementById("HdnProjectCode").value;
				ProjectName = document.getElementById("HdnProjectName").value; 
				switch(type)
				{
				//alert(type);
					case "Plan":
						self.location = "SchedulePlan.aspx?t=Plan&pc="+ProjectCode+"&pn="+ProjectName;
						break;
					case "Resource":
						self.location = "SchedulePlan.aspx?t=Resource&pc="+ProjectCode+"&pn="+ProjectName;
						//self.location = "ScheduleResourceColl.aspx?t=Resource&pc="+ProjectCode+"&pn="+ProjectName;
						break;
					case "View":
						self.location = "SchedulePlan.aspx?t=View&pc="+ProjectCode+"&pn="+ProjectName;
						break;
					case "GTT":
						self.location = "ScheduleGTT.aspx";
						break;
				}
				return false;
			}
			
			function showBlock(flag)
			{
				try
				{
					if(flag == "1")
					{
						InfoList.window.document.getElementById("dvScheduleBox").style.display = '';
						InfoList.window.document.getElementById("Project").style.display = 'none';
					}
					else if(flag == "2")
					{
									
						InfoList.window.document.getElementById("dvScheduleBox").style.display = 'none';
						InfoList.window.document.getElementById("Project").style.display = '';
						//进行编码中文的项目名称
						var regExpChina=/[\u4E00-\u9FA5]+/;
                        var arrayChina=InfoList.window.document.getElementById("Project").src.match(regExpChina);
                        var connectionStr='';
                        for(i=0;i<arrayChina.length;i++){
                        connectionStr+=arrayChina[i];
                        }
                        InfoList.window.document.getElementById("Project").src=InfoList.window.document.getElementById("Project").src.replace(regExpChina,escape(connectionStr));
					}
				}
				catch(e){}
			}
		</script>
	</head>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0"  onload ="selrow3('UCProjectList_tvProjectt1')">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0" cellSpacing="0" cellPadding="0" >
				<tr>
					<td>
						<table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<tr class="td-toolstbar">
								<td align="left">进度视图</td>
								<td id="tdgtt" align="right" runat="server"><input id="HdnProjectName" style="WIDTH: 10px" type="hidden" name="HdnProjectName" runat="server" />

									<input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />
&nbsp;&nbsp;&nbsp;
									<A onclick="javascript:showBlock(1);return false;" href="#">列表显示</A>&nbsp;
									 <A onclick="javascript:showBlock(2);return false;" href="#">甘特图</A>&nbsp;
								<a href="../../EPC/ConstructSchedule/project/GTTsetup.CAB">
                                         安装甘特图控件</a>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="100%">
						<table class="table-none" id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="1">
							<tr>
								<td vAlign="top" width="15%" height="100%">
									<div style="border:solid 0px red;overflow:auto;width:200px;height:100%;"><MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UCProjectList" runat="server" /></div>
								</td>
								<td vAlign="top" width="85%"><iframe id="InfoList" name="InfoList" src="" frameBorder="0" width="100%" scrolling="auto"	height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
