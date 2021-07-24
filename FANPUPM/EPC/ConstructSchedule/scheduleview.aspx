<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scheduleview.aspx.cs" Inherits="ScheduleView" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_constructschedule_usercontrol_uscheduleplan_ascx" Src="~/EPC/ConstructSchedule/UserControl/uscheduleplan.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_constructschedule_usercontrol_utaskrelation_ascx" Src="~/EPC/ConstructSchedule/UserControl/utaskrelation.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SchedulePlanAdd</title><meta Name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1" />
<meta Name="CODE_LANGUAGE" Content="C#" />
<meta Name="vs_defaultClientScript" Content="JavaScript" />
<meta Name="vs_targetSchema" Content="http://schemas.microsoft.com/intellisense/ie5" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function clickTenderRow(index)
			{
			}
			function BtnOpeartoin(type)
			{
				switch(type)
				{
					case "BtnClose":
						window.close();
						break;
					case "BtnCancel":
						break;
					case "BtnBack":
						window.parent.location.href = "SchedulePlanMain.aspx";
						break;
				}
				return false;
			}
			function OpenScheduleResource(projectCode,taskCode)
			{
				location.href = "ScheduleResource.aspx?pc="+projectCode+"&tc="+taskCode;
				return false;
			}
			//时间间隔判断
			function funChkInt()
			{
				if (event.keyCode < 48 || event.keyCode > 57 )
					event.returnValue = false;
			}
			function CheckInputIsFloat(keyCode,e)
			{
				if((keyCode>95 && keyCode<106) || (keyCode>47 && keyCode<58) || keyCode == 8|| keyCode == 46 || keyCode == 37 || keyCode == 39 || keyCode == 9 || keyCode == 13)
				{
			    }
				else if(keyCode == 110 || keyCode==190)
				{
					 if(e.value == "" || e.value.indexOf(".") != -1)
					 {
						 return false;
					 }
				} 
				else
				{
					return false;
				}
			}

		</script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Tablex" class="table-none" cellSpacing="0" cellPadding="0" width="100%" border="0"
				height="100%">
				<TR>
					<TD class="td-toolsbar"><input id="HdnProjectCode" type="hidden" name="HdnProjectCode" runat="server" />

						<input id="HdnProjectName" type="hidden" name="HdnProjectName" runat="server" />
</TD>
				</TR>
				<TR>
					<TD class="td-toolsbar">
						<asp:Button ID="BtnSchInfo" TabIndex="-1" Text="进度信息" Visible="false" runat="server" />&nbsp;
							<asp:Button ID="BtnResInfo" TabIndex="-1" Text="资源信息" Visible="false" runat="server" />&nbsp;<INPUT type="button" onclick="BtnOpeartoin('BtnBack');" value="返 回" id="Button1" name="Button1">&nbsp;&nbsp;</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<MyUserControl:epc_constructschedule_usercontrol_uscheduleplan_ascx ID="UCSchedulePlan" runat="server" /></TD>
				</TR>
				<TR>
					<TD vAlign="top" height="100%">
						<MyUserControl:epc_constructschedule_usercontrol_utaskrelation_ascx ID="UCTaskRelation" runat="server" /></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
