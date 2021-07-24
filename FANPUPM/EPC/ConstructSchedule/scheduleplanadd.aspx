<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scheduleplanadd.aspx.cs" Inherits="SchedulePlanAdd" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_constructschedule_usercontrol_utaskrelation_ascx" Src="~/EPC/ConstructSchedule/UserControl/utaskrelation.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="epc_constructschedule_usercontrol_uscheduleplan_ascx" Src="~/EPC/ConstructSchedule/UserControl/uscheduleplan.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat="server"><title>SchedulePlanAdd</title><meta Content="Microsoft Visual Studio .NET 7.1" Name="GENERATOR" />
<meta Content="C#" Name="CODE_LANGUAGE" />
<meta Content="JavaScript" Name="vs_defaultClientScript" />
<meta Content="http://schemas.microsoft.com/intellisense/ie5" Name="vs_targetSchema" />

		<script language="javascript" src="../../web_client/tree.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			function clickTenderRow(index)
			{
				document.getElementById('UCTaskRelation_HdnSelectedIndex').value = index;
				document.getElementById('UCTaskRelation_BtnDel').disabled = false;
				document.getElementById('UCTaskRelation_BtnEdit').disabled = false;
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
			//任务列表
			function ShowTaskList(txtchecker)
			{	
				var projectCode = document.getElementById('HdnProjectCode').value;
				var projectName = escape(document.getElementById('HdnProjectName').value);
		
				var url= "TaskList.aspx?pn="+projectName+"&pc="+projectCode;
				var Man = new Array();
				Man[0] = "";
				Man[1] = "";
				Man[2] = "";
				window.showModalDialog(url,Man,"dialogHeight:475px;dialogWidth:600px;center:1;help:0;status:0;");
				//window.open(url);
				if(Man[0].length > 0)
				{
					txtchecker.parentElement.parentElement.children[1].children[0].value=Man[0];
					txtchecker.parentElement.parentElement.children[2].children[0].value=Man[1];
				}
			}
			//时间操作
			function checkWorkDay(sourObj)
			{
				if (sourObj.value!="")
				{
					if (!(new RegExp(/^\d+$/).test(sourObj.value)))
					{
						alert('工期应该是整数！');
						sourObj.focus();
						return;
					}
					else
					{
						var wd = parseInt(sourObj.value);
						bd = document.getElementById('UCSchedulePlan_DtxStartDate').value;
						ed = document.getElementById('UCSchedulePlan_DtxEndDate').value;
						if (wd == 0)
						{
							if ((bd!="")&&(ed!=""))
							{
								bd = getTheDate(bd);
								ed = getTheDate(ed);
								sourObj.value = (ed.getTime()-bd.getTime())/(1000*60*60*24) + 1;
							}
						}
						else
						{
							bd = document.getElementById('UCSchedulePlan_DtxStartDate').value;
							ed = document.getElementById('UCSchedulePlan_DtxEndDate').value;
							if (bd!="")
							{
								var newdate=getTheDate(bd);
								var newtimems=newdate.getTime()+((wd-1)*24*60*60*1000);
								newdate.setTime(newtimems);
								document.getElementById('UCSchedulePlan_DtxEndDate').value = newdate.getFullYear() + "-" + (newdate.getMonth()+1) + "-" + newdate.getDate();
							}
							else
							{
								if (ed!="")
								{
									var newdate=getTheDate(ed);
									var newtimems=newdate.getTime()-((wd-1)*24*60*60*1000);
									newdate.setTime(newtimems);
									document.getElementById('UCSchedulePlan_DtxStartDate').value = newdate.getFullYear() + "-" + (newdate.getMonth()+1) + "-" + newdate.getDate();
								}
							}
						}
					}
				}
				else
				{
					sourObj.value = 0;
				}
			}
			function getTheDate(dateStr)
			{
				var theYear;
				var theMonth;
				var theDay;
				var i = dateStr.indexOf("-");
				var j = dateStr.lastIndexOf("-");
				theYear = parseInt(dateStr.substr(0,i));
				theMonth = parseInt(dateStr.substr(i+1,j-i-1))-1;
				theDay = parseInt(dateStr.substr(j+1));
				return new Date(theYear,theMonth,theDay);
			}
			
			function getEndDate(obj,targetObj)
			{
				if (obj.value!="")
				{
					var bd = getTheDate(obj.value);
					var ed = document.getElementById(targetObj).value;
					var wd = parseInt(document.getElementById('UCSchedulePlan_TxtWorkDay').value);
					if (ed!="")
						{
							ed = getTheDate(ed);
							if (bd == ed)
							{
								document.getElementById('UCSchedulePlan_TxtWorkDay').value = "1";
							}
							if (bd > ed)
							{
								alert('开始时间不能晚于结束时间！');
								obj.value = "";
								obj.blur();
							}
							if (bd<ed)
							{
								document.getElementById('UCSchedulePlan_TxtWorkDay').value = (ed.getTime()-bd.getTime())/(1000*60*60*24) + 1;
							}
						}
				}
			}
			
			function getBeginDate(obj,targetObj)
			{
				if (obj.value!="")
				{
					var bd = document.getElementById(targetObj).value;
					var ed = getTheDate(obj.value);
					var wd = parseInt(document.getElementById('UCSchedulePlan_TxtWorkDay').value);
					if (bd!="")
						{
							bd = getTheDate(bd);
							if (bd == ed)
							{
								document.getElementById('UCSchedulePlan_TxtWorkDay').value = "1";
							}
							if (bd>ed)
							{
								alert('结束时间不能早于开始时间！');
								obj.value = "";
								obj.blur();
							}
							if (bd<ed)
							{
								document.getElementById('UCSchedulePlan_TxtWorkDay').value = (ed.getTime()-bd.getTime())/(1000*60*60*24) + 1;
							}
						}
				}
			}
			function goback()
			{
			 window.parent.self.location='../../EPC/ConstructSchedule/scheduleplanmain.aspx';
			 //desktop.getActive().getWindow().getElementsByTagName("iframe")[0].src ='';
			}
        </script>
	</head>
	<body class="body_frame" scroll="no">
		<form id="Form1" method="post" runat="server">
			<TABLE class="table-none" id="Tablex" height="100%" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD class="td-toolsbar"><asp:Button ID="BtnSave" Text="保  存" OnClick="BtnSave_Click" runat="server" />&nbsp;
						<asp:Button ID="BtnCancel" Text="重  置" OnClick="BtnCancel_Click" runat="server" />&nbsp;
                        <input id="Button1" type="button" value="返  回" onclick="goback();"    />&nbsp;
						 <input id="HdnProjectCode" type="hidden" name="HdnProjectCode" runat="server" />

						<input id="HdnProjectName" type="hidden" name="HdnProjectName" runat="server" />
</TD>
				</TR>
				<TR>
					<TD vAlign="top"><MyUserControl:epc_constructschedule_usercontrol_uscheduleplan_ascx ID="UCSchedulePlan" runat="server" /></TD>
				</TR>
				<TR>
					<TD vAlign="top" height="100%"><MyUserControl:epc_constructschedule_usercontrol_utaskrelation_ascx ID="UCTaskRelation" runat="server" /></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
