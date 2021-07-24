<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConstructionLogmain.aspx.cs" Inherits="SchedulePlanMain" %>
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
					case "Plan":
						self.location = "SchedulePlan.aspx?t=Plan&pc="+ProjectCode+"&pn="+ProjectName;
						break;
					case "Resource":
						self.location = "SchedulePlan.aspx?t=Resource&pc="+ProjectCode+"&pn="+ProjectName;
						break;
					case "View":
						self.location = "SchedulePlan.aspx?t=View&pc="+ProjectCode+"&pn="+ProjectName;
						break;
					case "GTT":
					//alert(document.getElementById("Hdnstr").value);
			
					//&str='+document.getElementById("Hdnstr").value+'
						var url = 'project/pj_task_view.asp?pc='+ProjectCode+'&strconn='+document.getElementById("Hdnstr").value+'&pn='+escape(ProjectName)+'&yhdm=';
						//return window.showModalDialog(url,window,'dialogHeight:660px;dialogWidth:880px;center:1;resizable:1;help:0;status:0;scroll:0;');
						window.open(url,'','left=50,top=50,width=880,height=660,toolbar=no,status=yes,scrollbars=yes,resizable=no');
						break;
				}
				return false;
			}

        </script>
	</head>
	<body class="body_frame" bottomMargin="0" leftMargin="0" topMargin="0" scroll="no" rightMargin="0"  onload ="selrow3('UProjectList1_tvProjectt1')"> 
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%" border="0" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<table class="table-none" id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<tr class="td-toolstbar">
								<td align="left">进度计划编制</td>
								<td align="right">
                                    &nbsp;<input id="Hdnstr" type="hidden" style="width: 84px" runat="server" />

                                    &nbsp;
                                    &nbsp;<input id="HdnProjectName" style="WIDTH: 10px" type="hidden" name="HdnProjectName" runat="server" />

									<input id="HdnProjectCode" style="WIDTH: 10px" type="hidden" name="HdnProjectCode" runat="server" />

									<asp:Button ID="BtnSchedulePlan" Enabled="false" Text="进度计划编制" Width="100px" Visible="false" runat="server" />&nbsp;<asp:Button ID="BtnView" Enabled="false" Text="查  看" Visible="false" runat="server" />&nbsp;<asp:Button ID="BtnGTT" Enabled="false" Text="甘特图" Visible="false" runat="server" />&nbsp;
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
									<div style="width:200px;height:100%;overflow:auto;"><MyUserControl:epc_usercontrol1_uprojectlist_ascx ID="UProjectList1" runat="server" /></div>
								</td>
								<td vAlign="top" width="85%"><iframe id="InfoList"   name="InfoList" src="" frameBorder="0" width="100%" scrolling="no"
										height="100%"></iframe>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
